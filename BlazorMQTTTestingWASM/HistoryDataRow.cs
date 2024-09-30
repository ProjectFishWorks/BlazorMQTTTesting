using MudBlazor;

namespace BlazorMQTTTestingWASM
{

    //Data structure for storing a single row historical data, passed as a list to the chart
    public class HistoryDataRow
    {
        public ulong data;

        public DateTime time;
    }
}
