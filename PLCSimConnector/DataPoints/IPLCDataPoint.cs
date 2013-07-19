
using System;

namespace PLCSimConnector.DataPoints
{
    public interface IPLCDataPoint : IComparable<IPLCDataPoint>, IComparable 
    {
        dynamic Value { get; set; }
        int Offset { get; }
        int Bit { get; }

        string Symbol { get; set; }
        string Address { get; set; }
        string DataType { get; set; }
    }
}
