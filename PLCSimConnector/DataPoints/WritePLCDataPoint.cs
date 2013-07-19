using System;
using System.Collections;

namespace PLCSimConnector.DataPoints
{
    public class WritePLCDataPoint : IComparable<WritePLCDataPoint>
    {
        public WritePLCDataPoint(int address, byte[] getBytes)
        {
            AddressStart = address;
            AddressEnd = address + getBytes.Length;
            Buffer = getBytes;
        }

        public int AddressStart { get; set; }

        public int AddressEnd { get; set; }

        public byte[] Buffer { get; set; }

        public int CompareTo(WritePLCDataPoint other)
        {
            throw new NotImplementedException();
        }
    }
}