using System;
using DotNetSiemensPLCToolBoxLibrary.DataTypes.Projectfolders;

namespace PLCSimConnector.DataPoints
{
    public class PLCScaledDataPoint : PLCDataPoint<float>
    {
        public PLCScaledDataPoint()
        {
            ScaleValue = (val, eHi, eLow, rHi, rLow) => (((val - rLow)*(eHi - eLow))/(rHi - rLow)) + eLow;
        }

        public override dynamic Value
        {
            get
            {
                return ScaleValue(base.Value, ScaleEngHigh, ScaleEngLow, ScaleRawHigh, ScaleRawLow); 
            }

        }

        public float ScaleEngHigh { get; set; }
        public float ScaleEngLow { get; set; }
        public float ScaleRawHigh { get; set; }
        public float ScaleRawLow { get; set; }
    
        
        public Func<float, float, float, float, float, float> ScaleValue;

        public static explicit operator PLCScaledDataPoint(SymbolTableEntry entry)
        {
            var temp = new PLCScaledDataPoint
                {
                Address = entry.OperandIEC,
                DataType = entry.DataType,
                Symbol = entry.Symbol
            };
            return temp;
        }

    }
    public static class PLCScaledDataPointsExtention
    {
        public static PLCScaledDataPoint AddScaledDataPoint(this PLCDataPoints list)
        {
            return null;
        }
    }

}
