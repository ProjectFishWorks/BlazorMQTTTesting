using BlazorMQTTTestingWASM.Models;

namespace BlazorMQTTTestingWASM
{
    public class DualColorLEDLightingDevice : Device
    {
        private int _WhiteLED = 0;
        private int _BlueLED = 0;
        private int _DawnTime = 0;
        private int _DuskTime = 0;
        private int _SunriseTime = 0;
        private int _SunsetTime = 0;
        private int _HighNoon = 0;
        private int _NightTime = 0;

        public int DawnTime
        {
            get
            {
                return _DawnTime;
            }
            set
            {
                _DawnTime = value;
                sendMessageData(nodeID, 2562, (ulong)_DawnTime);
            }
        }

        public int DuskTime
        {
            get
            {
                return _DuskTime;
            }
            set
            {
                _DuskTime = value;
                sendMessageData(nodeID, 2563, (ulong)_DuskTime);
            }
        }

        public int SunriseTime
        {
            get
            {
                return _SunriseTime;
            }
            set
            {
                _SunriseTime = value;
                sendMessageData(nodeID, 2564, (ulong)_SunriseTime);
            }
        }

        public int SunsetTime
        {
            get
            {
                return _SunsetTime;
            }
            set
            {
                _SunsetTime = value;
                sendMessageData(nodeID, 2565, (ulong)_SunsetTime);
            }
        }

        public int HighNoon
        {
            get
            {
                return _HighNoon;
            }
            set
            {
                _HighNoon = value;
                sendMessageData(nodeID, 2566, (ulong)_HighNoon);
            }
        }

        public int NightTime
        {
            get
            {
                return _NightTime;
            }
            set
            {
                _NightTime = value;
                sendMessageData(nodeID, 2567, (ulong)_NightTime);
            }
        }
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
