using MQTTnet.ClientLib;

namespace BlazorMQTTTestingWASM.Models
{
    public class TesterHatDevice : Device
    {
        private int nodeID;
        public TesterHatDevice(MQTTnet.ClientLib.MqttService mqttService, int systemID,int basestationID,int nodeID) : base(mqttService, systemID, basestationID)
        {
            this.nodeID = nodeID;
        }

        public string PotentiometerValue
        {
            get
            {
                ulong? value = getMessageData(nodeID, 45056);
                if(value.HasValue)
                {
                    return value.Value.ToString();
                }
                return "-";
            }
        }

        public List<Boolean?> ButtonValues
        {
            get
            {
                List<Boolean?> values = new List<Boolean?>();
                for(int i = 0; i < 4; i++)
                {
                    ulong? value = getMessageData(nodeID, 45057 + i);
                    if(value.HasValue)
                    {
                        values.Add(value.Value == 0);
                    }
                    else
                    {
                        values.Add(null);
                    }
                }
                return values;
            }
        }

    }
}
