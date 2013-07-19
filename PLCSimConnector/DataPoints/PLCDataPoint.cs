#region

using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using DotNetSiemensPLCToolBoxLibrary.DataTypes.Projectfolders;

#endregion

namespace PLCSimConnector.DataPoints
{
    public delegate dynamic GetValue(PLCDataPoint dataPoint);

    public delegate dynamic PostGetValue(dynamic value);

    public delegate dynamic PreSetValue(dynamic setValue);

    public delegate void SetValue(PLCDataPoint dataPoint, dynamic setValue);

    public class PLCDataPoint : IPLCDataPoint, IComparable<PLCDataPoint>
    {
        public GetValue ValueGetAction;
        public PostGetValue ValuePostGetAction;
        public PreSetValue ValuePreSetAction;
        public SetValue ValueSetAction;

        private int offset = -1;
        private int bit = -1;
        public PLCDataPoint()
        {
            ValuePostGetAction = value => value;
            ValuePreSetAction = value => value;
        }

        public PLCDataPoint(SymbolTableEntry entry) : this()
        {
            Address = entry.OperandIEC;
            DataType = entry.DataType;
            Symbol = entry.Symbol;
        }

        public int CompareTo(PLCDataPoint other)
        {
            if (this == other) return 0;
            if (other == null) return 1;

            return String.CompareOrdinal(Symbol, other.Symbol);
        }

        public virtual dynamic Value
        {
            get { return ValuePostGetAction(ValueGetAction(this)); }
            set { ValueSetAction(this, ValuePreSetAction(value)); }
        }

        public int Bit
        {
            get
            {
                if (bit != -1) return bit;
                ParseAddress();
                return bit;
            }
        }

        public string Symbol { get; set; }
        public string Address { get; set; }
        public string DataType { get; set; }

        public int Offset
        {
            get
            {
                if (offset != -1) return offset;
                ParseAddress();
                return offset;
            }
        }

        public int CompareTo(object obj)
        {
            return CompareTo((PLCDataPoint) obj);
        }

        public int CompareTo(IPLCDataPoint other)
        {
            return CompareTo((PLCDataPoint) other);
        }

        public void DataPointScaling( float engHi, float engLow, float rawHi, float rawLow)
        {
            Debug.Print("Enter {0}:DataPointScaling", GetType());
            Debug.Indent();
            ValuePostGetAction = value => (((value - rawLow) * (engHi - engLow)) / (rawHi - rawLow)) + engLow;
            ValuePreSetAction = value => (((value - engLow) * (rawHi - rawLow)) / (engHi - engLow)) + rawLow;

            Debug.Unindent();
            Debug.Print("Exit {0}:DataPointScaling", GetType());
        }

        public static explicit operator PLCDataPoint(SymbolTableEntry entry)
        {
            var temp = new PLCDataPoint
                {
                    Address = entry.OperandIEC,
                    DataType = entry.DataType,
                    Symbol = entry.Symbol
                };
            return temp;
        }

        private void ParseAddress()
        {
            var m = Regex.Match(Address, @"(?<offset>\d+)(?:\.(?<bit>\d*))?$");
            offset = Convert.ToInt32(m.Groups["offset"].Value);
            bit = m.Groups["bit"].Value.Length > 0 ? Convert.ToInt32(m.Groups["bit"].Value) : 0;
        }
        
    }
}