using System;
using System.Globalization;
using S7PROSIMLib;


namespace PLCSimConnector
{

    public class PLCSim : IDisposable
    {
        private S7ProSim ps;
        private bool disposed;

        public PLCSim()
        {
            ps = new S7ProSim();
            ps.Connect();
            ps.ConnectionError += ps_ConnectionError;
        }

        void ps_ConnectionError(string controlEngine, int error)
        {
            //System.Diagnostics.Debug.Print("%d", Error);
            throw new ApplicationException(error.ToString(CultureInfo.InvariantCulture));
        }

        public void BeginScanNotify()
        {
            ps.BeginScanNotify();
        }

        public void Connect()
        {
            ps.Connect();
        }

        public void ConnectExt(int instanceNumber)
        {
            ps.ConnectExt(instanceNumber);
        }

        public void Continue()
        {
            ps.Continue();
        }

        public void Disconnect()
        {
            ps.Disconnect();
        }

        public void EndScanNotify()
        {
            ps.EndScanNotify();
        }

        public void ExecuteNScans(int nScanNumber)
        {
            ps.ExecuteNScans(nScanNumber);
        }

        public void ExecuteNmsScan(int msNumber)
        {
            ps.ExecuteNmsScan(msNumber);
        }

        public void ExecuteSingleScan()
        {
            ps.ExecuteSingleScan();
        }

        public PauseStateConstants GetPauseState()
        {
            return ps.GetPauseState();
        }

        public ScanModeConstants GetScanMode()
        {
            return ps.GetScanMode();
        }

        public RestartSwitchPositionConstants GetStartUpSwitch()
        {
            return ps.GetStartUpSwitch();
        }

        public void HotStartWithSavedValues(int val)
        {
            ps.HotStartWithSavedValues(val);
        }

        public void Pause()
        {
            ps.Pause();
        }

        public object ReadDataBlockValue(int blockNum, int byteIndex, int bitIndex, PointDataTypeConstants dataType)
        {
            object pData = null;
            ps.ReadDataBlockValue(blockNum, byteIndex, bitIndex, dataType, ref pData);
            return pData;
        }

        public object ReadFlagValue(int byteIndex, int bitIndex, PointDataTypeConstants dataType)
        {
            object pData = null;
            ps.ReadFlagValue(byteIndex, bitIndex, dataType, ref pData);
            return pData;
        }

        public object ReadOutputImage(int startIndex, int elementsToRead, ImageDataTypeConstants dataType)
        {
            object pData = null;
            ps.ReadOutputImage(startIndex, elementsToRead, dataType, ref pData);
            return pData;
        }

        public object ReadOutputPoint(int byteIndex, int bitIndex, PointDataTypeConstants dataType)
        {
            object pData = null;
            ps.ReadOutputPoint(byteIndex, bitIndex, dataType, ref pData);
            return pData;
        }

        public void SavePLC(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");
            ps.SavePLC(fileName);
        }

        public void SetScanMode(ScanModeConstants newVal)
        {
            ps.SetScanMode(newVal);
        }

        public void SetStartUpSwitch(RestartSwitchPositionConstants postion)
        {
            ps.SetStartUpSwitch(postion);
        }

        public void SetState(string newVal)
        {
            ps.SetState(newVal);
        }

        public void StartPLCSim(string plcFile)
        {
            ps.StartPLCSim(plcFile);
        }

        public void StartPLCSimExt(string plcFile)
        {
            ps.StartPLCSimExt(plcFile);
        }

        public void WriteDataBlockValue(int blockNum, int byteIndex, int bitIndex, ref object pData)
        {
            ps.WriteDataBlockValue(blockNum,byteIndex,bitIndex, ref pData);
        }

        public void WriteFlagValue(int byteIndex, int bitIndex, ref object pData)
        {
            ps.WriteFlagValue(byteIndex, bitIndex, ref pData);
        }

        public void WriteInputImage(int startIndex, ref object pData)
        {
            ps.WriteInputImage(startIndex, ref pData);
        }

        public void WriteInputPoint(int byteIndex, int bitIndex, ref object pData)
        {
            ps.WriteInputPoint(byteIndex, bitIndex, ref pData);
        }
        public string GetState()
        {
            string val = ps.GetState();
            return val;
        }

        public object ReadOutputImage( int startIndex, int elementsToRead)
        {
            object pData = null;
            ps.ReadOutputImage(startIndex, elementsToRead, ImageDataTypeConstants.S7Byte, ref pData);
            return pData;
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method 
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~PLCSim()
        {

            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
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
                    ps.Disconnect();
                    ps = null; 
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

/*
 *  public static void Set(ref byte aByte, int pos, bool value)
 {
 if (value)
 {
  //left-shift 1, then bitwise OR
  aByte = (byte)(aByte | (1 << pos));
 }
 else
 {
  //left-shift 1, then take complement, then bitwise AND
  aByte = (byte)(aByte & ~(1 << pos));
 }
 }
 
 public static bool Get(byte aByte, int pos)
 {
  //left-shift 1, then bitwise AND, then check for non-zero
  return ((aByte & (1 << pos)) != 0);
 }
*/