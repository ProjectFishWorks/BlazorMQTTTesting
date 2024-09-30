namespace BlazorMQTTTestingWASM
{
    public class HistoryChartDataEventArgs
    {
        public int NodeID { get; set; }
        public int MessageID { get; set; }
        public int Hours { get; set; }

        public HistoryChartDataEventArgs(int nodeID, int messageID, int hours)
        {
            NodeID = nodeID;
            MessageID = messageID;
            Hours = hours;
        }
    }
}
