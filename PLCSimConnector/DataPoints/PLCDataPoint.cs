using System;
using System.Linq;
using System.Dynamic;
using DotNetSiemensPLCToolBoxLibrary.DataTypes.Projectfolders;

namespace PLCSimConnector.DataPoints
{
    public delegate dynamic GetValue(int offset);
    public delegate void SetValue(int offset, dynamic setValue);

    public class PLCDataPoint : IPLCDataPoint, IComparable<PLCDataPoint>
    {
        public PLCDataPoint()
        {
            
        }

        public PLCDataPoint(SymbolTableEntry entry)
        {
            Address = entry.OperandIEC;
            DataType = entry.DataType;
            Symbol = entry.Symbol;
        }

        public virtual dynamic Value
        {
            get { return ValueGetAction(Offset); }
            set { ValueSetAction(offset,value); } //TODO: Check Type
        }

        public string Symbol { get; set; }
        public string Address { get; set; }
        public string DataType { get; set; }

        public int Offset
        {

            get
            {
                if (offset == -1) offset = Convert.ToInt32(new string(Address.ToCharArray().Where(Char.IsDigit).ToArray()));
                
                return offset; 
            }
        }
        
        public GetValue ValueGetAction;
        public SetValue ValueSetAction;

        private int offset = -1;
        public static explicit operator PLCDataPoint(SymbolTableEntry entry)
        {
            var temp = new PLCDataPoint
                {
                    Address =  entry.OperandIEC,
                    DataType = entry.DataType,
                    Symbol = entry.Symbol
                };
            return temp;
        }

        public int CompareTo(PLCDataPoint other)
        {
            if (this == other) return 0;
            if (other == null) return 1;

            return String.CompareOrdinal(Symbol, other.Symbol);
        }

        public int CompareTo(object obj)
        {
            return CompareTo((PLCDataPoint) obj);
        }

        public int CompareTo(IPLCDataPoint other)
        {
            return CompareTo((PLCDataPoint) other);
        }
    }




}
