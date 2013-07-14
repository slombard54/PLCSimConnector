using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

        unsafe public static byte[] GetBytes(int value)
        {
            var bytes = new byte[4];
            fixed (byte* b = bytes)
            {
                var pbyte = (byte*) &value;
                *((int*)b) = ((*pbyte << 24) | (*(pbyte + 1) << 16) | (*(pbyte + 2) << 8) | (*(pbyte + 3)));
            }
            return bytes; 
        }

        unsafe public static byte[] GetBytes(short value)
        {
            var bytes = new byte[2];
            fixed (byte* b = bytes)
            {
                var pbyte = (byte*)&value;
                *((int*)b) = ((*(pbyte) << 8) | (*(pbyte + 1)));
            }
            return bytes;
        }

        unsafe public static byte[] GetBytes(float value)
        {
            var bytes = new byte[4];
            fixed (byte* b = bytes)
            {
                var pbyte = (byte*)&value;
                *((int*)b) = ((*pbyte << 24) | (*(pbyte + 1) << 16) | (*(pbyte + 2) << 8) | (*(pbyte + 3)));
            }
            return bytes;
        }

        public static MemoryStream ToSingle(this MemoryStream s, int offset)
        {
            ToSingle(s.GetBuffer(), offset);
            return s;
        }
        
    }
}
