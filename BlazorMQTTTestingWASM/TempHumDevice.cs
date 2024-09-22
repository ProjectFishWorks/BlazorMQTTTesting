namespace BlazorMQTTTestingWASM.Models
{
    public class TempHumDevice : Device
    {
        private int nodeID;
        private float _CanopyTemp = 0;
        private float _CanopyHum = 0;
        private float _TankTemp = 0;
        private float _SumpTemp = 0;

        public TempHumDevice(MQTTnet.ClientLib.MqttService mqttService, int systemID,int basestationID,int nodeID) : base(mqttService, systemID, basestationID)
        {
            this.nodeID = nodeID;
        }

        public float CanopyTemp
        {
            get
            {
                return _CanopyTemp;
            }
            set
            {
                _CanopyTemp = value;
                sendMessageData(nodeID, 2560, (ulong)_CanopyTemp);
            }
        }

        public float CanopyHum
        {
            get
            {
                return _CanopyHum;
            }
            set
            {
                _CanopyHum = value;
                sendMessageData(nodeID, 2561, (ulong)_CanopyHum);
            }
        }

        public float TankTemp
        {
            get
            {
                return _TankTemp;
            }
            set
            {
                _TankTemp = value;
                sendMessageData(nodeID, 2562, (ulong)_TankTemp);
            }
        }

        public float SumpTemp
        {
            get
            {
                return _SumpTemp;
            }
            set
            {
                _SumpTemp = value;
                sendMessageData(nodeID, 2563, (ulong)_SumpTemp);
            }
        }
    }
}
