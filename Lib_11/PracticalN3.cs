
using System.Runtime.InteropServices;
using System.Windows.Automation;

namespace Lib_11
{
    public class PracticalN3
    {
        /// <summary>
        /// В данной матрице находит минимальный элемент в каждой строке. а потом среди них находит максимальное.
        /// </summary>
        /// <param name="array">целочисленная проинициализированная матрица</param>
        /// <returns>целочисленное значение, обозначающее среди минимальных элементов каждой строки матрицы максимальное</returns>
        public static int maxAmongTheMin(int[,] array)
        {
            int min = 1000;
            int max = -1000;
            int[] arrayMinElem = new int[array.GetLength(0)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] < min) min = array[i, j];
                }
                arrayMinElem[i] = min;
                min = 1000;
            }
            for (int i = 0; i < arrayMinElem.Length; i++)
            {
                if (arrayMinElem[i] > max) max = arrayMinElem[i];
            }
            return max;
        }
        public static void PrintHello()
        {
            Console.WriteLine("Hello");
        }
    }

}
