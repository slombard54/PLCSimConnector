#region

using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using DotNetSiemensPLCToolBoxLibrary.DataTypes.Projectfolders;

#endregion

namespace PLCSimConnector.DataPoints
{
    public delegate dynamic GetValue(int offset);

    public delegate dynamic PostGetValue(dynamic value);

    public delegate dynamic PreSetValue(dynamic setValue);

    public delegate void SetValue(int offset, dynamic setValue);

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
            get { return ValuePostGetAction(ValueGetAction(Offset)); }
            set { ValueSetAction(offset, ValuePreSetAction(value)); }
        }

        public int Bit
        {
            get { return bit; }
        }

        public string Symbol { get; set; }
        public string Address { get; set; }
        public string DataType { get; set; }

        public int Offset
        {
            get
            {
                var m = Regex.Matches(Address, @"(\d+)(?:\.(\d*))?$");
                if (offset == -1)
                {
                    offset = Convert.ToInt32(m[0].ToString());
                    //new string(Address.ToCharArray().Where(Char.IsDigit).ToArray()));
                }
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
        
    }
}