namespace BlazorMQTTTestingWASM.Models
{
    public class TempHumDevice : Device
    {
        private int nodeID;                         // 0x1000
        private float? _CanopyTemp = 0;              //
        private float? _CanopyHum = 0;
        private float? _TankTemp = 0;
        private float? _SumpTemp = 0;
        private bool _CanopyTempAlarmOnOff = false;
        private float? _CanopyTempAlarmLow = 0;
        private float? _CanopyTempAlarmHigh = 0;
        private bool _CanopyHumAlarmOnOff = false;
        private float? _CanopyHumAlarmLow = 0;
        private float? _CanopyHumAlarmHigh = 0;
        private bool _TankTempAlarmOnOff = false;
        private float? _TankTempAlarmLow = 0;
        private float? _TankTempAlarmHigh = 0;
        private bool _SumpTempAlarmOnOff = false;
        private float? _SumpTempAlarmLow = 0;
        private float? _SumpTempAlarmHigh = 0;


        public TempHumDevice(MQTTnet.ClientLib.MqttService mqttService, int systemID,int basestationID,int nodeID) : base(mqttService, systemID, basestationID)
        {
            this.nodeID = nodeID;
            Console.Write("Node ID");
            Console.WriteLine(nodeID);
        }

        public float? CanopyTemp
        {
            get
            {
                _CanopyTemp = getMessagePayload(nodeID, 2560).dataFloat;
                return _CanopyTemp;
            }
        }

        public bool CanopyTempAlarmOnOff
        {
            get
            {
           
                return _CanopyTempAlarmOnOff;
            }
            set
            {
                _CanopyTempAlarmOnOff = value;
                sendMessageData(nodeID, 2561, (ulong)(_CanopyTempAlarmOnOff ? 1 : 0));
            }
        }

        public float? CanopyTempAlarmLow
        {
            get
            {
                return _CanopyTempAlarmLow;
            }
            set
            {
                _CanopyTempAlarmLow = value;
                sendMessageData(nodeID, 2562, (ulong)_CanopyTempAlarmLow);
            }
        }

        public float? CanopyTempAlarmHigh
        {
            get
            {
                return _CanopyTempAlarmHigh;
            }
            set
            {
                _CanopyTempAlarmHigh = value;
                sendMessageData(nodeID, 2563, (ulong)_CanopyTempAlarmHigh);
            }
        }

        public float? CanopyHum
        {
            get
            {
                _CanopyHum = getMessagePayload(nodeID, 2564).dataFloat;
                return _CanopyHum;
            }
        }

        public bool CanopyHumAlarmOnOff
        {
            get
            {
                return _CanopyHumAlarmOnOff;
            }
            set
            {
                _CanopyHumAlarmOnOff = value;
                sendMessageData(nodeID, 2565, (ulong)(_CanopyHumAlarmOnOff ? 1 : 0));
            }
        }

        public float? CanopyHumAlarmLow
        {
            get
            {
                return _CanopyHumAlarmLow;
            }
            set
            {
                _CanopyHumAlarmLow = value;
                sendMessageData(nodeID, 2566, (ulong)_CanopyHumAlarmLow);
            }
        }

        public float? CanopyHumAlarmHigh
        {
            get
            {
                return _CanopyHumAlarmHigh;
            }
            set
            {
                _CanopyHumAlarmHigh = value;
                sendMessageData(nodeID, 2567, (ulong)_CanopyHumAlarmHigh);
            }
        }

        public float? TankTemp
        {
            get
            {
                _TankTemp = getMessagePayload(nodeID, 2568).dataFloat;
                return _TankTemp;
            }
            set
            {
                _TankTemp = value;
                sendMessageData(nodeID, 2568, (ulong)_TankTemp);
            }
        }

        public bool TankTempAlarmOnOff
        {
            get
            {
                return _TankTempAlarmOnOff;
            }
            set
            {
                _TankTempAlarmOnOff = value;
                sendMessageData(nodeID, 2569, (ulong)(_TankTempAlarmOnOff ? 1 : 0));
            }
        }

        public float? TankTempAlarmLow
        {
            get
            {
                return _TankTempAlarmLow;
            }
            set
            {
                _TankTempAlarmLow = value;
                sendMessageData(nodeID, 2570, (ulong)_TankTempAlarmLow);
            }
        }

        public float? TankTempAlarmHigh
        {
            get
            {
                return _TankTempAlarmHigh;
            }
            set
            {
                _TankTempAlarmHigh = value;
                sendMessageData(nodeID, 2571, (ulong)_TankTempAlarmHigh);
            }
        }


        public float? SumpTemp
        {
            get
            {
                _SumpTemp = getMessagePayload(nodeID, 2572).dataFloat;
                return _SumpTemp;
            }
            set
            {
                _SumpTemp = value;
                sendMessageData(nodeID, 2572, (ulong)_SumpTemp);
            }
        }

        public bool SumpTempAlarmOnOff
        {
            get
            {
                return _SumpTempAlarmOnOff;
            }
            set
            {
                _SumpTempAlarmOnOff = value;
                sendMessageData(nodeID, 2573, (ulong)(_SumpTempAlarmOnOff ? 1 : 0));
            }
        }

        public float? SumpTempAlarmLow
        {
            get
            {
                return _SumpTempAlarmLow;
            }
            set
            {
                _SumpTempAlarmLow = value;
                sendMessageData(nodeID, 2574, (ulong)_SumpTempAlarmLow);
            }
        }

        public float? SumpTempAlarmHigh
        {
            get
            {
                return _SumpTempAlarmHigh;
            }
            set
            {
                _SumpTempAlarmHigh = value;
                sendMessageData(nodeID, 2575, (ulong)_SumpTempAlarmHigh);
            }
        }
    }
}
