using static System.Net.Mime.MediaTypeNames;
using System.Net.NetworkInformation;

using System.Text;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet;

using System.Text.Json;
using System.Collections;

namespace BlazorMQTTTestingWASM.Models
{
    public class Device
    {   
        private MQTTnet.ClientLib.MqttService mqttService;
        private int systemID;
        private int basestationID;
        public Device(MQTTnet.ClientLib.MqttService mqttService,int systemID, int basestationID)
        {
            this.mqttService = mqttService;
            this.systemID = systemID;
            this.basestationID = basestationID;
        }

        public void requestHistoricalData(int nodeID, int messageID, int hours)
        {
            //ulong lastMessageTime = getMessagePayload(nodeID, messageID).time.Value;

            //MQTTData? mQTTDataIn = getMessagePayload(nodeID, messageID);

            //if (mQTTDataIn != null)
            //{
            //    Console.WriteLine("We have a message");
            //    lastMessageTime = mQTTDataIn.time.Value;
            //    if (mqttService.AllMessages.ContainsKey(responseTopic))
            //    {
            //        Console.WriteLine("We have history");

            //        string response = mqttService.AllMessages[responseTopic];
            //        MQTTHistoricalData? historicalData = JsonSerializer.Deserialize<MQTTHistoricalData>(response);
            //        if (historicalData.history.Count > 0)
            //        {
            //            var lastHistoryMessageTime = historicalData.history.Max(t => t.time);

            //            Console.WriteLine("Last Message Time:");
            //            Console.WriteLine(lastMessageTime);
            //            Console.WriteLine("Last History Message Time:");
            //            Console.WriteLine(lastHistoryMessageTime);

            //            if (lastHistoryMessageTime == lastMessageTime)
            //            {
            //                Dictionary<DateTime, ulong> data = new Dictionary<DateTime, ulong>();
            //                foreach (var item in historicalData.history)
            //                {
            //                    DateTime datetime = DateTimeOffset.FromUnixTimeSeconds((long)item.time).LocalDateTime;
            //                    if (data.ContainsKey(datetime) == false)
            //                    {
            //                        data.Add(datetime, item.data);
            //                    }
            //                }

            //                Console.WriteLine("Returning stored history");

            //                return;
            //            }
            //        }
            //    }
            //}

            MQTTData mQTTDataOut = new MQTTData();
            mQTTDataOut.data = (ulong)hours;
            string json = JsonSerializer.Serialize(mQTTDataOut);
            string requestTopic = $"historyIn/{systemID}/{basestationID}/{nodeID}/{messageID}";

            MqttApplicationMessage message = new MqttApplicationMessageBuilder()
                .WithTopic(requestTopic)
                .WithPayload(Encoding.UTF8.GetBytes(json))
                .Build();

            mqttService.Publish(message);

            mqttService.Subscribe($"historyOut/{systemID}/{basestationID}/{nodeID}/{messageID}/#");
        }

        public List<HistoryChartData>? getHistoricalData(int nodeID, int messageID, int hours)
        {

            string responseTopic = $"historyOut/{systemID}/{basestationID}/{nodeID}/{messageID}";

            if(mqttService.AllMessages.Keys.Where(k => k.StartsWith(responseTopic)).Count() > 0)
            {
                Dictionary<DateTime, ulong> data = new Dictionary<DateTime, ulong>();

                var responses = mqttService.AllMessages.Where(kvp => kvp.Key.StartsWith(responseTopic)).OrderBy(kvp => kvp.Key);

                List<HistoryChartData> chartData = new List<HistoryChartData>();

                var startHour = int.Parse(DateTime.UtcNow.AddHours(hours * -1).ToString("yyyyMMddHH"));


                foreach (var response in responses)
                {

                    var topicHour = int.Parse(response.Key.Split('/').Last());

                    if(topicHour < startHour)
                    {
                        continue;
                    }

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

                    foreach (var entry in historicalData.history)
                    {
                        HistoryChartData chartDataRow = new HistoryChartData();
                        chartDataRow.data = entry.data;
                        chartDataRow.time = DateTimeOffset.FromUnixTimeSeconds((long)entry.time).LocalDateTime;
                        chartData.Add(chartDataRow);
                    }
                }

                return chartData;
            }
            else
            {
                return null;
            }
        }

        public MQTTData? getMessagePayload(int nodeID, int messageID)
        {
            foreach (var kvp in mqttService.AllMessages)
            {
                //   out/0/0/170/45056
                string[] parts = kvp.Key.Split('/');
                if (parts.Length == 5)
                {
                    if (parts[0] == "out" && parts[1] == systemID.ToString() && parts[2] == basestationID.ToString() && parts[3] == nodeID.ToString() && parts[4] == messageID.ToString())
                    {
                        MQTTData? mQTTData = JsonSerializer.Deserialize<MQTTData>(kvp.Value);
                        if (mQTTData == null)
                        {
                            return null;
                        }
                        return mQTTData;
                    }
                }
            }
            return new MQTTData();
        }

        async public void sendMessageData(int nodeID, int messageID, ulong data)
        {
            MQTTData mQTTData = new MQTTData();
            mQTTData.data = data;
            DateTime currentTime = System.DateTime.UtcNow;
            mQTTData.time = (ulong)((DateTimeOffset)currentTime).ToUnixTimeSeconds();
            string json = JsonSerializer.Serialize(mQTTData);
            string topic = $"in/{systemID}/{basestationID}/{nodeID}/{messageID}";
            MqttApplicationMessage message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(Encoding.UTF8.GetBytes(json))
                .WithRetainFlag()
                .Build();
            await mqttService.Publish(message);
        }

    }
}


