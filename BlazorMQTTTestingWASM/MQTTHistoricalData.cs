namespace BlazorMQTTTestingWASM
{
    public class MQTTHistoricalData
    {

        public class historicalData
        {
            public ulong time { get; set; }
            public ulong data { get; set; }
        }

        public int hoursToRead { get; set; }
        public int systemID { get; set; }

        public int baseStationID { get; set; }

        public int nodeID { get; set; }

        public int messageID { get; set; }

        public List<historicalData> history { get; set; } = new List<historicalData>();
    }
}
