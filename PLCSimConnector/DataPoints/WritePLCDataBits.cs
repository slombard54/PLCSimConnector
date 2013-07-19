using System;

namespace PLCSimConnector.DataPoints
{
    public class WritePLCDataBits : IComparable<WritePLCDataBits>
    {
        public WritePLCDataBits(int address, int bit, bool data)
        {
            Address = address;
            Bit = bit;
            Buffer = data;
        }

        public int Address { get; set; }

        public int Bit { get; set; }

        public bool Buffer { get; set; }

        public int CompareTo(WritePLCDataBits other)
        {
            throw new NotImplementedException();
        }
    }
}