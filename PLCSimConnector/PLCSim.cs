using System;
using System.Globalization;
using S7PROSIMLib;


namespace PLCSimConnector
{

    public class PLCSim : S7ProSimClass, IDisposable
    {
        private bool disposed;



        public PLCSim()
        {
            base.ConnectionError += ps_ConnectionError;
        }

        public static void ps_ConnectionError(string controlEngine, int error)
        {
            throw new ApplicationException(error.ToString(CultureInfo.InvariantCulture));
        }



        public object ReadOutputImage(int startIndex, int elementsToRead, ImageDataTypeConstants dataType)
        {
            object pData = null;
            ReadOutputImage(startIndex, elementsToRead, dataType, ref pData);
            return pData;
        }

        public object ReadOutputImage( int startIndex, int elementsToRead)
        {
            object pData = null;
            ReadOutputImage(startIndex, elementsToRead, ImageDataTypeConstants.S7Byte, ref pData);
            return pData;
        }

        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~PLCSim()
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
                    Disconnect();
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