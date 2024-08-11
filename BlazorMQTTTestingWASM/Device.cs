using static System.Net.Mime.MediaTypeNames;
using System.Net.NetworkInformation;

using System.Text;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet;

using System.Text.Json;

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

        public ulong? getMessageData(int nodeID, int messageID)
        {
            foreach(var kvp in mqttService.AllMessages)
            {
                //   out/0/0/170/45056
                string[] parts = kvp.Key.Split('/');
                if(parts.Length == 5)
                {
                    if(parts[0] == "out" && parts[1] == systemID.ToString() && parts[2] == basestationID.ToString() && parts[3] == nodeID.ToString() && parts[4] == messageID.ToString())
                    {   
                        MQTTData? mQTTData = JsonSerializer.Deserialize<MQTTData>(kvp.Value);
                        if(mQTTData == null)
                        {
                            return null;
                        }
                        return mQTTData.data;
                    }
                }
            }
            return null;
        }
    }
}


