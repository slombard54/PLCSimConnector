using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using PLCSimConnector.DataPoints;
using S7PROSIMLib;

namespace PLCSimConnector
{
    public class SimulatedPLC : IDisposable
    {
        public PCS7Project Project { set; get; }
        private MemoryStream outputImageBuffer;
        private MemoryStream inputImageBuffer;
        private int maxOutputOffset;
        private bool disposed;
        private readonly PLCDataPoints dataPointList;
        public Action UpdateImages;  

        public SimulatedPLC() : this(new PLCSim())
        {
        }

        public SimulatedPLC(PLCSim plcSimObject)
        {
            SimPLC = plcSimObject;
            
            dataPointList = new PLCDataPoints();

            outputImageBuffer = new MemoryStream();
            inputImageBuffer = new MemoryStream();
            UpdateImages += UpdateInputImage;
            UpdateImages += UpdateOutputImage;
        }

        
        public MemoryStream OutputImageBuffer
        {
            get { return outputImageBuffer; }
            set { outputImageBuffer = value; }
        }

        public MemoryStream InputImageBuffer
        {
            get { return inputImageBuffer; }
            set { inputImageBuffer = value; }
        }

        public PLCSim SimPLC { get; set; }

        public void OutputImageOffestRequest(int offset)
        {
            if (offset > outputImageBuffer.Capacity)
            {
                outputImageBuffer.Capacity = offset;
            }
            if (offset > maxOutputOffset)
            {
                maxOutputOffset = offset;
            }
        }

        public IPLCDataPoint AddScaledDataPoint(string point, float engHi, float engLow, float rawHi, float rawLow) 
        {
            Debug.Print("Enter {0}:AddScaledDataPoint", GetType());
            Debug.Indent();
            PostGetValue getValueAction = value => (((value - rawLow) * (engHi - engLow)) / (rawHi - rawLow)) + engLow;
            PreSetValue setValueAction = value => (((value - engLow) * (rawHi - rawLow)) / (engHi - engLow)) + rawLow;
            
            var dataPoint = (PLCDataPoint)AddDataPoint(point, getValueAction, setValueAction);

            Debug.Unindent();
            Debug.Print("Exit {0}:AddScaledDataPoint", GetType());
            return dataPoint;
        }

        private IPLCDataPoint AddDataPoint(string point, PostGetValue postGetAction = null, PreSetValue preSetAction = null)
        {
            Debug.Print("Enter {0}:AddDataPoint", GetType());
            if (point == null) throw new ArgumentNullException("point");
            

            // Look for duplicate point in list
            var existingDataPoint = dataPointList.GetPLCDataPoint(point);
            // IDEA: Verify that Points are equal
            if (existingDataPoint != null)
            {
                Debug.Print("Symbol found in list returning item");
                Debug.Unindent();
                Debug.Print("Exit {0}:AddDataPoint", GetType());
                return existingDataPoint;
            }

            PLCDataPoint dataPoint;
            if (point.Trim(' ').Contains(" "))
            {
                Debug.Print("Address Detected");

                string dataType = "";
                switch (point.Substring(2,1))
                {
                    case "D":
                        {
                            dataType = "DWORD";
                            break;
                        }         
                    case "W":
                        {
                            dataType = "WORD";
                            break;
                        }
                    case "B":
                        {
                            dataType = "BYTE";
                            break;
                        }
                }

                dataPoint = new PLCDataPoint { Address = point, Symbol = point, DataType = dataType };
            }
            else
            {
                Debug.Print("Symbol Detected");
                try
                {
                    dataPoint = (PLCDataPoint)Project.PCS7SymbolTable.GetEntryFromSymbol(point);
                }
                catch (NullReferenceException e)
                {
                    
                    throw new ApplicationException("PCS7Project is not intialized ", e);
                }
                

            }
            if (dataPoint == null) throw new NoNullAllowedException(string.Format("Null Returned from PCS7Project Call for point {0}", point));
            
            GetValue getAction = offset => 0;
            SetValue setAction = (offset, value) => { };
            switch(dataPoint.DataType)
            {
                case "WORD":
                    getAction = outputImageBuffer.ToBEInt16;
                    setAction = (offset, val) => inputImageBuffer.Write(BigEndianBitConverter.GetBytes((Int16)val), offset, 8);
                    break;

                case "DWORD":
                    getAction = outputImageBuffer.ToBEInt32;
                    setAction = (offset,val) => outputImageBuffer.GetBuffer().WriteBE((int)val,offset);
                    break;

                case "REAL":
                    getAction = outputImageBuffer.ToBESingle;
                    setAction = (offset, val) => inputImageBuffer.Write(BigEndianBitConverter.GetBytes((float)val), offset, 8);
                    break;

                case "BYTE":
                    getAction = offset =>
            {
                outputImageBuffer.Seek(offset, SeekOrigin.Begin);
                return outputImageBuffer.ReadByte();
            };
                    setAction = (offset, val) => inputImageBuffer.Write(BigEndianBitConverter.GetBytes((byte)val), offset, 8);
                   break;

            }
            switch (dataPoint.Address.Trim().ToUpper().First())
            {
                case 'Q':
                    dataPoint.ValueGetAction = getAction;
                    break;
                case 'I':
                    dataPoint.ValueSetAction = setAction;
                    break;
                default:
                    dataPoint.ValueGetAction = getAction;
                    dataPoint.ValueSetAction = setAction;
                    break;
            }
            if (postGetAction != null)
                dataPoint.ValuePostGetAction = postGetAction;
            if (preSetAction != null)
                dataPoint.ValuePreSetAction = preSetAction;


            dataPointList.Add(dataPoint);
            UpdateImages();

            Debug.Unindent();
            Debug.Print("Exit {0}:AddDataPoint", GetType());
            return dataPoint;
        }

        public void UpdateOutputImage()
        {
            object pData = null;
            int max = dataPointList.Max(item => item.Offset);
            SimPLC.ReadOutputImage(0, max+8, ImageDataTypeConstants.S7Byte, ref pData);
            var byteData = (byte[])pData;
            outputImageBuffer.Seek(0, SeekOrigin.Begin);
            if (byteData != null) outputImageBuffer.Write(byteData, 0, byteData.GetLength(0));

        }
        public void UpdateInputImage()
        {
            object pData = inputImageBuffer.GetBuffer();
            SimPLC.WriteInputImage(0, ref pData); 
        }

        #region DestructorAndDispose
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~SimulatedPLC()
        {
            Dispose(false);
        }



        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue 
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if(!disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if(disposing)
                {
                    // Dispose managed resources.
                    ////component.Dispose();
                    outputImageBuffer.Dispose();
                    inputImageBuffer.Dispose();
                    SimPLC = null;
                    Project = null;
                }
		 
                // Call the appropriate methods to clean up 
                // unmanaged resources here.
                // If disposing is false, 
                // only the following code is executed.
                ////CloseHandle(handle);
                ////handle = IntPtr.Zero;
			
            }
            disposed = true;         
        }
    #endregion



    }
}
