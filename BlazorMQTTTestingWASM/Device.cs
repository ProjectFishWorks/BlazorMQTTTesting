using System.Text;
using MQTTnet;

using System.Text.Json;

namespace BlazorMQTTTestingWASM.Models
{
    //Base class for all devices
    public class Device
    {   
        //MQTT Service
        private MQTTnet.ClientLib.MqttService mqttService;

        //System ID of the device
        private int systemID;

        //Basestation ID of the device
        private int basestationID;
        public Device(MQTTnet.ClientLib.MqttService mqttService,int systemID, int basestationID)
        {
            this.mqttService = mqttService;
            this.systemID = systemID;
            this.basestationID = basestationID;
        }

        //Request historical data from a device
        public void requestHistoricalData(int nodeID, int messageID, int hours)
        {   

            //Construct the request JSON
            MQTTData mQTTDataOut = new MQTTData();

            //The number of hours of data to request
            mQTTDataOut.data = (ulong)hours;

            string json = JsonSerializer.Serialize(mQTTDataOut);

            //The topic to send the request on
            string requestTopic = $"historyIn/{systemID}/{basestationID}/{nodeID}/{messageID}";

            //Send the request to the basestation
            MqttApplicationMessage message = new MqttApplicationMessageBuilder()
                .WithTopic(requestTopic)
                .WithPayload(Encoding.UTF8.GetBytes(json))
                .Build();
            mqttService.Publish(message);

            //Subscribe to the response topic
            mqttService.Subscribe($"historyOut/{systemID}/{basestationID}/{nodeID}/{messageID}/#");
        }

        //Update the historical data (called after requestHistoricalData) returns a list of time and data pairs
        public List<HistoryDataRow>? updateHistoricalData(int nodeID, int messageID, int hours)
        {

            //The topic to look for the response on
            string responseTopic = $"historyOut/{systemID}/{basestationID}/{nodeID}/{messageID}";

            //Do we have any history data responses?
            if(mqttService.AllMessages.Keys.Where(k => k.StartsWith(responseTopic)).Count() > 0)
            {
                Dictionary<DateTime, ulong> data = new Dictionary<DateTime, ulong>();

                //Get all the history responses
                var responses = mqttService.AllMessages.Where(kvp => kvp.Key.StartsWith(responseTopic)).OrderBy(kvp => kvp.Key);

                //List of time and data pairs
                List<HistoryDataRow> chartData = new List<HistoryDataRow>();


                //A date time representing the start of the requested data
                var startHour = int.Parse(DateTime.UtcNow.AddHours(hours * -1).ToString("yyyyMMddHH"));

                //For all history responses received
                foreach (var response in responses)
                {
                    //Get what hour the response is for
                    var topicHour = int.Parse(response.Key.Split('/').Last());

                    //If the response is for an hour before the start hour, ignore it
                    if(topicHour < startHour)
                    {
                        continue;
                    }

                    //Parse the JSON response
                    MQTTHistoricalData? historicalData;
                    try
                    {
                        historicalData = JsonSerializer.Deserialize<MQTTHistoricalData>(response.Value);
                    }
                    catch
                    {
                        Console.WriteLine("JSON Parsing Error");
                        return null;
                    }

                    if (historicalData == null)
                    {
                        Console.WriteLine("Historical Data is null");
                        return null;
                    }
                    
                    //For all the data in the response
                    foreach (var entry in historicalData.history)
                    {
                        //Add the time and data to the list
                        HistoryDataRow chartDataRow = new HistoryDataRow();
                        chartDataRow.data = entry.data;
                        //Convert from Unix time(UTC) to a local date time
                        chartDataRow.time = DateTimeOffset.FromUnixTimeSeconds((long)entry.time).LocalDateTime;
                        chartData.Add(chartDataRow);
                    }
                }

                //Return the list of time and data pairs from all applicable history responses
                return chartData;
            }
            else
            {
                return null;
            }
        }

        //Get the message payload for a given node and message ID
        public MQTTData? getMessagePayload(int nodeID, int messageID)
        {
            //Search all messages for the requested nodeID and messageID
            foreach (var kvp in mqttService.AllMessages)
            {
                //   out/0/0/170/45056
                string[] parts = kvp.Key.Split('/');
                if (parts.Length == 5)
                {
                    if (parts[0] == "out" && parts[1] == systemID.ToString() && parts[2] == basestationID.ToString() && parts[3] == nodeID.ToString() && parts[4] == messageID.ToString())
                    {
                        //Deserialize the JSON payload
                        MQTTData? mQTTData = JsonSerializer.Deserialize<MQTTData>(kvp.Value);
                        if (mQTTData == null)
                        {
                            return null;
                        }
                        return mQTTData;
                    }
                }
            }
            //No message found
            return new MQTTData();
        }

        //Send a message to a node
        async public void sendMessageData(int nodeID, int messageID, ulong data)
        {
            //Construct the JSON message
            MQTTData mQTTData = new MQTTData();
            mQTTData.data = data;
            DateTime currentTime = System.DateTime.UtcNow;
            mQTTData.time = (ulong)((DateTimeOffset)currentTime).ToUnixTimeSeconds();
            string json = JsonSerializer.Serialize(mQTTData);
            string topic = $"in/{systemID}/{basestationID}/{nodeID}/{messageID}";

            //Send the message
            MqttApplicationMessage message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(Encoding.UTF8.GetBytes(json))
                .WithRetainFlag()
                .Build();
            await mqttService.Publish(message);
        }

    }
}


