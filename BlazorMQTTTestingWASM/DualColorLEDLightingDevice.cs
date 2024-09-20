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
        private bool _BrightnessOverrideSwitch;
        private int _OverrideWhiteIntensity = 0;
        private int _OverrideBlueIntensity = 0;

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
        public int DawnTime
        {
            get
            {
                return _DawnTime;
            }
            set
            {
                _DawnTime = value;
                sendMessageData(nodeID, 2562, (ulong)_DawnTime * 1000);
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
                sendMessageData(nodeID, 2563, (ulong)_DuskTime * 1000);
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
                sendMessageData(nodeID, 2564, (ulong)_SunriseTime * 1000);
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
                sendMessageData(nodeID, 2566, (ulong)_HighNoon * 1000);
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
                sendMessageData(nodeID, 2567, (ulong)_NightTime * 1000);
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

        public int CurrentWhiteIntensity
        {
            get
            {
                return (int)getMessagePayload(nodeID, 2569).data;
            }
        }


        public int CurrentBlueIntensity
        {
            get
            {
                return (int)getMessagePayload(nodeID, 2570).data;
            }
        }

        public bool BrightnessOverrideSwitch
        {
            get
            {
                return _BrightnessOverrideSwitch;
            }
            set
            {
                _BrightnessOverrideSwitch = value;
                sendMessageData(nodeID, 2571, (ulong)(_BrightnessOverrideSwitch ? 1 : 0));
            }
        }

        public int OverrideWhiteIntensity
        {
            get
            {
                return _OverrideWhiteIntensity;
            }
            set
            {
                _OverrideWhiteIntensity = value;
                sendMessageData(nodeID, 2572, (ulong)_OverrideWhiteIntensity);
            }
        }

        public int OverrideBlueIntensity
        {
            get
            {
                return _OverrideBlueIntensity;
            }
            set
            {
                _OverrideBlueIntensity = value;
                sendMessageData(nodeID, 2573, (ulong)_OverrideBlueIntensity);
            }
        }

        private int nodeID;
        public DualColorLEDLightingDevice(MQTTnet.ClientLib.MqttService mqttService, int systemID, int basestationID, int nodeID) : base(mqttService, systemID, basestationID)
        {
            this.nodeID = nodeID;
        }

    }
}
