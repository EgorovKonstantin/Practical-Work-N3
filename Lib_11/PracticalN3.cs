
using System.Runtime.InteropServices;
using System.Windows.Automation;

namespace Lib_11
{
    public class PracticalN3
    {
        /// <summary>
        /// � ������ ������� ������� ����������� ������� � ������ ������. � ����� ����� ��� ������� ������������.
        /// </summary>
        /// <param name="array">������������� ��������������������� �������</param>
        /// <returns>������������� ��������, ������������ ����� ����������� ��������� ������ ������ ������� ������������</returns>
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
