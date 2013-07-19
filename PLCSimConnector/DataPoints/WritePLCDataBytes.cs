using System;
using System.Collections;

namespace PLCSimConnector.DataPoints
{
    public class WritePLCDataBytes : IComparable<WritePLCDataBytes>
    {
        public WritePLCDataBytes(int address, byte[] getBytes)
        {
            AddressStart = address;
            AddressEnd = address + getBytes.Length;
            Buffer = getBytes;
        }

        public int AddressStart { get; set; }

        public int AddressEnd { get; set; }

        public byte[] Buffer { get; set; }

        public int CompareTo(WritePLCDataBytes other)
        {
            throw new NotImplementedException();
        }
    }
}