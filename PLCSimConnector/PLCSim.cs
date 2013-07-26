using System;
using S7PROSIMLib;


namespace PLCSimConnector
{



    public class PLCSim : S7ProSimClass, IDisposable
    {
        private bool disposed;
        


        public PLCSim()
        {
            var connectionErrorHandler = new IS7ProSimEvents_ConnectionErrorEventHandler(ps_ConnectionError);
            base.ConnectionError += connectionErrorHandler;
        }

        public void SetScanMode(int newMode)
        {
            SetScanMode((ScanModeConstants)newMode);
        }

        public static void ps_ConnectionError(string controlEngine, int error)
        {
            string ecode ="UNKNOWN";
            string edescription = "Error Code not Defined In Documentation";
            switch ((uint)error)
            {
                case 0x80040205:
                    {
                        ecode = "PS_E_BADBITNDX";
                        edescription = error.ToString("X8") + ": Byte index is invalid";
                        break;
                    }
                case 0x80040202:
                    {
                        ecode = "PS_E_BADBYTECOUNT";
                        edescription = error.ToString("X8") + ": Size of data array is invalid for given start byte index";
                        break;
                    }
                case 0x80040201:
                    {
                        ecode = "PS_E_BADBYTENDX";
                        edescription = error.ToString("X8") + ": Byte index is invalid";
                        break;
                    }
                case 0x80040206:
                    {
                        ecode = "PS_E_BADTYPE";
                        edescription = error.ToString("X8") + ": Invalid data type";
                        break;
                    }
                case 0x80040207:
                    {
                        ecode = "PS_E_INVALIDCALLBACK";
                        edescription = error.ToString("X8") + ": Invalid callback";
                        break;
                    }
                case 0x80040208:
                    {
                        ecode = "PS_E_INVALIDDISPATCH";
                        edescription = error.ToString("X8") + ": Invalid dispatch";
                        break;
                    }
                case 0x80040213:
                    {
                        ecode = "PS_E_INVALIDINPUT";
                        edescription = error.ToString("X8") + ": Invalid input";
                        break;
                    }
                case 0x8004020B:
                    {
                        ecode = "PS_E_INVALIDSCANTYPE";
                        edescription = error.ToString("X8") + ": Invalid scan type, must be one of the ScanModeConstansts.";
                        break;
                    }
                case 0x80040214:
                    {
                        ecode = "PS_E_MAXINSTANCE";
                        edescription = error.ToString("X8") + ": Maximum number of open S7-PLCSIM instances reached";
                        break;
                    }
                case 0x8004020C:
                    {
                        ecode = "PS_E_MODENOTPOSSIBLE";
                        edescription = error.ToString("X8") + ": S7-PLCSIM could not set the specified operating mode";
                        break;
                    }
                case 0x8004020F:
                    {
                        ecode = "PS_E_NOTALLREADSWORKED";
                        edescription = error.ToString("X8") + ": Not all read operations were successful";
                        break;
                    }
                case 0x80040210:
                    {
                        ecode = "PS_E_NOTALLWRITESWORKED";
                        edescription = error.ToString("X8") + ": Not all write operations were successful";
                        break;
                    }
                case 0x80040211:
                    {
                        ecode = "PS_E_NOTCONNECTED";
                        edescription = error.ToString("X8") + ": S7ProSim is not connected to S7-PLCSIM";
                        break;
                    }
                case 0x8004020D:
                    {
                        ecode = "PS_E_NOTIFICATION_EXIST";
                        edescription = error.ToString("X8") + ": S7ProSim is already registered for notification";
                        break;
                    }
                case 0x80040209:
                    {
                        ecode = "PS_E_NOTREGISTERED";
                        edescription = error.ToString("X8") + ": S7ProSim is not registered for callbacks from S7-PLCSIM";
                        break;
                    }
                case 0x8004020A:
                    {
                        ecode = "PS_E_NOTSINGLESCAN";
                        edescription = error.ToString("X8") + ": Single scan program execution is not set in S7-PLCSIM";
                        break;
                    }
                case 0x8004020E:
                    {
                        ecode = "PS_E_PLCNOTRUNNING";
                        edescription = error.ToString("X8") + ": S7-PLCSIM is not running";
                        break;
                    }
                case 0x80040212:
                    {
                        ecode = "PS_E_POWEROFF";
                        edescription = error.ToString("X8") + ": S7-PLCSIM is powered off";
                        break;
                    }
                case 0x80040203:
                    {
                        ecode = "PS_E_READFAILED";
                        edescription = error.ToString("X8") + ": Read operation failed";
                        break;
                    }
                case 0x80040204:
                    {
                        ecode = "PS_E_WRITEFAILED";
                        edescription = error.ToString("X8") + ": Write operation failed";
                        break;
                    }
                case 0x80004005:
                    {
                        ecode = "E_FAIL";
                        edescription = error.ToString("X8") + ": Unspecified error";
                        break;
                    }
                case 0x80008002:
                    {
                        ecode = "E_INVALID_STATE";
                        edescription = error.ToString("X8") + ": Invalid state";
                        break;
                    }
                case 0x80030103:
                    {
                        ecode = "STG_E_CANTSAVE";
                        edescription = error.ToString("X8") + ": Cannot save";
                        break;
                    }
            }
            System.Diagnostics.Trace.TraceError("PLCSim engine {0} Error {1} - [{2}] {3}", controlEngine, error, ecode, edescription);
            //throw new ApplicationException(error.ToString(CultureInfo.InvariantCulture));
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