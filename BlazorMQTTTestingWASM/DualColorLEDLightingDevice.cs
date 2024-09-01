using BlazorMQTTTestingWASM.Models;

namespace BlazorMQTTTestingWASM
{
    public class DualColorLEDLightingDevice : Device
    {
        private int _WhiteLED = 0;
        private int _BlueLED = 0;
        public int WhiteLED
        {
            get
            {
                return _WhiteLED;
            }
            set
            {
                _WhiteLED = value;
                sendMessageData(nodeID, 2560, (ulong)_WhiteLED);
            }
        }

        public int BlueLED
        {
            get
            {
                return _BlueLED;
            } 
            set 
            {
                _BlueLED = value;
                sendMessageData(nodeID, 2561, (ulong)_BlueLED);
            } 
        }
        private int nodeID;
        public DualColorLEDLightingDevice(MQTTnet.ClientLib.MqttService mqttService, int systemID, int basestationID, int nodeID) : base(mqttService, systemID, basestationID)
        {
            this.nodeID = nodeID;
        }

    }
}
