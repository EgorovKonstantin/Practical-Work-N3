
using Microsoft.Win32;
using System.Data;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace LibMas
{
    /// <summary>
    /// Класс для работы с массивами. В нем содержаться методы для Сохранения массива, 
    /// Создания и Заполнения случайными числами, Удаления и Открытия из файла.
    /// </summary>
    public class ArrayFunctions
    {
        /// <summary>
        /// Создает и инициализирует массив числами [-20;0)U(0;20]
        /// </summary>
        /// <param name="row"> Количество строк, для создания массива </param>
        /// <param name="column"> Количество полей, для создания массива </param>
        /// <returns> Массив, проинициализированный случайными числами </returns>
        public static int[,] FillArray(int row, int column)
        {
            Random rnd = new Random();
            int randomNum;
            int[,] arr = new int[row,column];
            for (int i = 0; i<arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    do
                    {
                        randomNum = rnd.Next(-20,21);
                    } while (randomNum == 0);
                    arr[i, j] = randomNum;
                }
            }
            return arr;
        }
        /// <summary>
        /// Удаляет ссылку на массив, установленную в параметре.
        /// </summary>
        /// <param name="matr"> Ссылка на массив целых чисел. Этот параметр передается по ссылке, 
        /// что позволяет методу изменять фактическую ссылку на массив. </param>
        /// <returns> Возвращает null, указывая на то, что ссылка больше не указывает на какой-либо массив. </returns>
        public static int[,]? DelArray(ref int[,]? matr)
        {
            return matr = null;
        }
        /// <summary>
        /// Метод читает из файла разрешения *.txt числовые символы, и записывает их в массив
        /// </summary>
        /// <returns>
        /// Возвращает массив, содержащий символы из прочитанного файла, или null, если в файле есть не числовые символы или 
        /// если пользователь закрыл диалоговое окно.
        /// </returns>
        public static int[,]? OpenArray()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Все файлы (*.*)|*.*| Текстовые файлы (.txt) | *.txt";
            open.FilterIndex = 2;
            open.Title = "Открытие таблицы";
            int row = 0;
            int column = 0;
            List<int> values = new List<int>();
            if (open.ShowDialog() == true)
            {
                using (StreamReader file = new StreamReader(open.FileName))
                {                    
                    while (!file.EndOfStream)
                    {
                        string line = file.ReadLine();
                        string[] valuesStr = line.Split(' ');
                        foreach (string valueStr in valuesStr)
                        {
                            if (Int32.TryParse(valueStr, out int value))
                            {
                                values.Add(value);
                                column++;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        row++;
                    }
                }
                column /= row;
                int indexList = 0;
                int[,] matr = new int[row, column];
                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    for (int j = 0; j < matr.GetLength(1); j++)
                    {
                        matr[i, j] = values[indexList];
                        indexList++;
                    }
                }return matr;
            }return null;
        }
        /// <summary>
        /// Метод записывает массив в файл формата *.txt
        /// </summary>
        /// <param name="matr">Массив, который необходимо сохранить</param>
        public static void SaveArray(int[,] matr)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Текстовые файлы (.txt) | *.txt";
            save.Title = "Сохранение файла";
            if (save.ShowDialog() == true && matr != null)
            {
                using (StreamWriter file = new StreamWriter(save.FileName))
                {
                    for (int i = 0; i < matr.GetLength(0); i++)
                    {
                        for (int j = 0; j < matr.GetLength(1); j++)
                        {
                            file.Write(matr[i, j].ToString());

                            // Добавляем пробел только если это не последний элемент строки
                            if (j < matr.GetLength(1) - 1)
                            {
                                file.Write(" ");
                            }
                        }
                        file.WriteLine();
                    }
                }
            }
        }
    }
    /// <summary>
    /// Класс для взаимодействия массива и DataGrid, формирует названия полей и заполняет ячейки числами
    /// </summary>
    public static class VisualArray
    {
        public static DataTable ToDataTable<T>(this T[,] matrix)
        {
            var res = new DataTable();
            if(matrix != null)
            {
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    res.Columns.Add("" + (i), typeof(T));
                }

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    var row = res.NewRow();

                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        row[j] = matrix[i, j];
                    }

                    res.Rows.Add(row);
                }
            }
            
            return res;
        }
    }

}
