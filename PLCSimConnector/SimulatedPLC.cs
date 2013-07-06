using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using S7PROSIMLib;

namespace PLCSimConnector
{
    public class SimulatedPLC : IDisposable
    {
        private PLCSim simPLC;
        private MemoryStream outputImageBuffer;
        private MemoryStream inputImageBuffer;
        private int maxOutputOffset;
        private bool disposed;


        public SimulatedPLC() : this(new PLCSim())
        {
        }

        public SimulatedPLC(IS7ProSim plcSimObject)
        {
            simPLC = plcSimObject as PLCSim;
            
            outputImageBuffer = new MemoryStream();
            inputImageBuffer = new MemoryStream();
           
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

        public void UpdateImages()
        {
            object pData = null;
            simPLC.ReadOutputImage(0, maxOutputOffset, ImageDataTypeConstants.S7Byte, ref pData);
            var byteData = (byte[])pData;
            outputImageBuffer.Write(byteData, 0, byteData.GetLength(0));

            pData = inputImageBuffer.GetBuffer();
            simPLC.WriteInputImage(0, ref pData); 
        }
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


    }
}
