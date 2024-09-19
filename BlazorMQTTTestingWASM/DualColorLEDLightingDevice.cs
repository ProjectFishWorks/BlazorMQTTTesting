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
        private decimal _BlueOnlyMaxIntensity = 0;
        private int _CurrentWhiteIntensity = 0;
        private int _CurrentBlueIntensity = 0;

        public int DawnTime
        {
            get
            {
                return _DawnTime;
            }
            set
            {
                _DawnTime = value * 1000;
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
                _DuskTime = value * 1000;
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
                _SunriseTime = value * 1000;
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
                _SunsetTime = value * 1000;
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
                _HighNoon = value * 1000;
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
                _NightTime = value * 1000;
                sendMessageData(nodeID, 2567, (ulong)_NightTime);
            }
        }

        public decimal BlueOnlyMaxIntensity
        {
            get
            {
                return _BlueOnlyMaxIntensity;
            }
            set
            {
                _BlueOnlyMaxIntensity = value;
                sendMessageData(nodeID, 2568, (ulong)_BlueOnlyMaxIntensity);
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

        public int CurrentWhiteIntensity
        {
            get
            {
                return _CurrentWhiteIntensity;
            }
            set
            {
                _CurrentWhiteIntensity = value;
                sendMessageData(nodeID, 2569, (ulong)_CurrentWhiteIntensity);
            }
        }

        

        public int CurrentBlueIntensity
        {
            get
            {
                return _CurrentBlueIntensity;
            }
            set
            {
                _CurrentBlueIntensity = value;
                sendMessageData(nodeID, 2570, (ulong)_CurrentBlueIntensity);
            }
        }

        private int nodeID;
        public DualColorLEDLightingDevice(MQTTnet.ClientLib.MqttService mqttService, int systemID, int basestationID, int nodeID) : base(mqttService, systemID, basestationID)
        {
            this.nodeID = nodeID;
        }

    }
}
