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

        public Dictionary<ulong,ulong>? getHistoricalData(int nodeID, int messageID, int hours)
        {

            MQTTData mQTTData = new MQTTData();
            mQTTData.data = (ulong)hours;
            string json = JsonSerializer.Serialize(mQTTData);
            string requestTopic = $"historyIn/{systemID}/{basestationID}/{nodeID}/{messageID}";

            ulong lastMessageTime = getMessagePayload(nodeID, messageID).time.Value;

            string responseTopic = $"historyOut/{systemID}/{basestationID}/{nodeID}/{messageID}";

            if (getMessagePayload(nodeID,messageID).time != null)
            {
                Console.WriteLine("We have a message");
                lastMessageTime = getMessagePayload(nodeID, messageID).time.Value;
                if (mqttService.AllMessages.ContainsKey(responseTopic))
                {
                    Console.WriteLine("We have history");
                    Dictionary<ulong, ulong> data = new Dictionary<ulong, ulong>();
                    string response = mqttService.AllMessages[responseTopic];
                    MQTTHistoricalData? historicalData = JsonSerializer.Deserialize<MQTTHistoricalData>(response);
                    var lastHistoryMessageTime = historicalData.history.Max(t => t.time);

                    Console.WriteLine("Last Message Time:");
                    Console.WriteLine(lastMessageTime);
                    Console.WriteLine("Last History Message Time:");
                    Console.WriteLine(lastHistoryMessageTime);

                    if(lastHistoryMessageTime == lastMessageTime)
                    {
                        foreach (var item in historicalData.history)
                        {
                            if(data.ContainsKey(item.time) == false)
                            {
                                data.Add(item.time, item.data);
                            }
                        }

                        return data;
                    }
                }
            }

            MqttApplicationMessage message = new MqttApplicationMessageBuilder()
                .WithTopic(requestTopic)
                .WithPayload(Encoding.UTF8.GetBytes(json))
                .Build();

            mqttService.Publish(message);

            mqttService.Subscribe(responseTopic);

            //get current unix time
            int timeOut = 10; //Seconds

            var currentTime = (ulong)((DateTimeOffset)(System.DateTime.UtcNow)).ToUnixTimeSeconds();

            while (mqttService.AllMessages.ContainsKey(responseTopic) == false && (((DateTimeOffset)(System.DateTime.UtcNow)).ToUnixTimeSeconds() + timeOut) > ((DateTimeOffset)(System.DateTime.UtcNow)).ToUnixTimeSeconds())
            {
                Task.Delay(10);
            }

            if(mqttService.AllMessages.ContainsKey(responseTopic))
            {
                Dictionary<ulong, ulong> data = new Dictionary<ulong, ulong>();
                string response = mqttService.AllMessages[responseTopic];
                MQTTHistoricalData? historicalData = JsonSerializer.Deserialize<MQTTHistoricalData>(response);

                if(historicalData == null)
                {
                    Console.WriteLine("Historical Data is null");
                    return null;
                }
                Console.WriteLine("Historical Data:");
                Console.WriteLine(historicalData.messageID);

                foreach (var item in historicalData.history)
                {
                    data.Add(item.time, item.data);
                }

                return data;
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


