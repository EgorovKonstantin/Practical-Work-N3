using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibMas;
using Lib_11;

namespace Practical_Work_N3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool flagCellEditEnding = false;
        bool flagDoneCellEditEnding = false;
        int[,]? array;
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Заполняет таблицу значениями из файла txt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miOpen_Click(object sender, RoutedEventArgs e)
        {
            array = ArrayFunctions.OpenArray();
            dataGrid.ItemsSource = VisualArray.ToDataTable(array).DefaultView;
        }
        /// <summary>
        /// Сохраняет данные из таблицы в файл *.txt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miSave_Click(object sender, RoutedEventArgs e)
        {
            ArrayFunctions.SaveArray(array);
        }
        /// <summary>
        /// Выход из программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Заполняет таблицу. Размер таблицы зависит от значений в TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miFillTable_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbRow.Text, out int numberRow) && int.TryParse(tbColumn.Text, out int numberColumn))
            {
                array = ArrayFunctions.FillArray(numberRow, numberColumn);
                dataGrid.ItemsSource = VisualArray.ToDataTable(array).DefaultView;
            }
            else MessageBox.Show("Вводите корректные значения", "Error");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miCalcul_Click(object sender, RoutedEventArgs e)
        {
            if(array != null && flagCellEditEnding == flagDoneCellEditEnding)
            {               
                tbResult.Text = PracticalN3.maxAmongTheMin(array).ToString();
            }
        }
        /// <summary>
        /// Удаляет таблицу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDelTable_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = VisualArray.ToDataTable(ArrayFunctions.DelArray(ref array)).DefaultView;
        }
        /// <summary>
        /// Обработчик события окончани редактирования таблицы, с помощью котороо мы можем изменить значения таблицы вручную.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            flagCellEditEnding = true;
            int indexRow = e.Row.GetIndex();
            int indexColumn = e.Column.DisplayIndex;
            if (Int32.TryParse(((TextBox)e.EditingElement).Text, out int newValue))
            {
                flagDoneCellEditEnding = true;
                array[indexRow, indexColumn] = newValue;
                dataGrid.ItemsSource = VisualArray.ToDataTable(array).DefaultView;
            }
            else
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// Информация о разработчике.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDeveloper_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Программу разработал Егоров Константин из ИСП-31", "О разработчике");
        }
        /// <summary>
        /// Информация об условии программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miTaskCondition_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Дана матрица размера M × N.\nНайти максимальный среди минимальных элементов ее строк", "О программе");
        }
    }
}