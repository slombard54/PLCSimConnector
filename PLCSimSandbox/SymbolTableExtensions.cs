using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLCSimSandbox
{
    public static class  SymbolTableExtensions
    {
        public static string[,] JaggedToMultidimensional<T>(this IEnumerable<T> source)
        {
            string[][] jaggedArray;
            int rows = jaggedArray.Length;
            int cols = jaggedArray.Max(subArray => subArray.Length);
            var array = new T[rows, cols];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    array[i, j] = jaggedArray[i][j];
                }
            }
            return array;
        }
    }
}
