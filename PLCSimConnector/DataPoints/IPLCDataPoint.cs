
namespace PLCSimConnector.DataPoints
{
    public interface IPLCDataPoint
    {
        dynamic Value { get; set; }
        int Offset { get; }

        string Symbol { get; set; }
        string Address { get; set; }
        string DataType { get; set; }

    }
}
