using System;
using System.IO;

namespace PLCSimConnector.DataPoints
{
    public static class BigEndianBitConverter
    {
        unsafe public static float ToSingle(Byte[] array, int index)
        {
            int val = ToInt32(array, index);
            return *(float*) &val;
        }

        unsafe public static int ToInt32(Byte[] array, int index)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (index >= array.Length) throw new ArgumentOutOfRangeException("index", "index not in array");
            if (index > array.Length - 4) throw new ArgumentOutOfRangeException("index", "index to large for type");

            fixed (byte* pbyte = &array[index])
            {
                return (*pbyte << 24) | (*(pbyte + 1) << 16) | (*(pbyte + 2) << 8) | (*(pbyte + 3));
            }
        }
        unsafe public static short ToInt16(Byte[] array, int index)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (index >= array.Length) throw new ArgumentOutOfRangeException("index", "index not in array");
            if (index > array.Length - 2) throw new ArgumentOutOfRangeException("index", "index to large for type");

            fixed (byte* pbyte = &array[index])
            {
                return (short)((*(pbyte) << 8) | (*(pbyte + 1)));
            }
        }

        unsafe public static byte ToByte(Byte[] array, int index)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (index >= array.Length) throw new ArgumentOutOfRangeException("index", "index not in array");
            if (index > array.Length - 1) throw new ArgumentOutOfRangeException("index", "index to large for type");

            fixed (byte* pbyte = &array[index])
            {
                return *(pbyte);
            }
        }

        public static byte[] GetBytes(int value)
        {
            var bytes = new byte[4];
            bytes.WriteBE(value);
            return bytes; 
        }

        public static byte[] GetBytes(short value)
        {
            var bytes = new byte[2];
            bytes.WriteBE(value);
            return bytes;
        }

        public static byte[] GetBytes(byte value)
        {
            var bytes = new byte[1];
            bytes.WriteBE(value);
            return bytes;
        }

        public static byte[] GetBytes(float value)
        {                       
            var bytes = new byte[4];
            bytes.WriteBE(value);
            return bytes;
        }

        /// <summary>
        /// Memory Stream extension to allow to float item at offset
        /// </summary>
        /// <param name="s"></param>
        /// <param name="offset"></param>
        /// <returns></returns>

        public static dynamic ToBESingle(this MemoryStream s, int offset)
        {
            return ToSingle(s.GetBuffer(), offset);
        }

        public static dynamic ToBEInt32(this MemoryStream s, int offset)
        {
            return ToInt32(s.GetBuffer(), offset);
        }
        public static dynamic ToBEInt16(this MemoryStream s, int offset)
        {
            return ToInt16(s.GetBuffer(), offset);
        }

        public static dynamic ToBEByte(this MemoryStream s, int offset)
        {
            return ToByte(s.GetBuffer(), offset);
        }

        unsafe public static void WriteBE(this byte[] buffer, float value, int offset = 0)
        {

            WriteBE(buffer, *(int*)&value, offset);
            
        }
        unsafe public static void WriteBE(this byte[] buffer, int value, int offset = 0)
        {

            fixed (byte* b = &buffer[offset])
            {
                var pbyte = (byte*)&value;
                *((int*)b) = ((*pbyte << 24) | (*(pbyte + 1) << 16) | (*(pbyte + 2) << 8) | (*(pbyte + 3)));
            }

        }

        public static unsafe void WriteBE(this byte[] buffer, short value, int offset = 0)
        {
            fixed (byte* b = &buffer[offset])
            {
                var pbyte = (byte*) &value;
                *((int*) b) = ((*(pbyte) << 8) | (*(pbyte + 1)));
            }
        }

        public static void WriteBE(this byte[] buffer, byte value, int offset = 0)
        {
            buffer[offset] = value;
        }
    }
}
