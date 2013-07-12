using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


namespace PLCSimConnector.DataPoints
{
    public class PLCDataPoints : IEnumerable<IPLCDataPoint>
    {
        private readonly List<IPLCDataPoint> dataPoints;

        public List<IPLCDataPoint> DataPoints
        {
            get { return dataPoints; }
        }


        public PLCDataPoints()
        {
            dataPoints = new List<IPLCDataPoint>();
        }
        
        public IEnumerator<IPLCDataPoint> GetEnumerator()
        {
            return dataPoints.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IPLCDataPoint this[int index] 
        {
            get { return dataPoints[index]; }
            set { dataPoints[index] = value; }
        }

        /// <summary>
        /// Returns PLCDataPoint if it is in the List
        /// Returns Null no matching symbol is found.
        /// </summary>
        /// <returns></returns>
        public IPLCDataPoint GetPLCDataPoint(string symbol)
        {   
            Debug.Print("Enter {0}:GetPLCDataPoint",GetType());
            var i = dataPoints.BinarySearch(new PLCDataPoint<float> { Symbol = symbol });
            Debug.Indent();
            Debug.Print("Search for Symbol {0} returned {1}", symbol, i);
            Debug.Unindent();
            Debug.Print("Exit {0}:GetPLCDataPoint", GetType());
            return i>=0 ? dataPoints[i] : null;
        }
    }
}
