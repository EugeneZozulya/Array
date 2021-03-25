using System;
using System.IO;
using System.Windows.Forms;

namespace Matrix
{
    public partial class Form1 : Form
    {
        MyArray array;
        bool isError = false;
        char key = default;
        ErrorProvider error = new ErrorProvider();
        public Form1()
        {
            InitializeComponent();
            toolTip1.SetToolTip(saveArray, "Сохранить массив в файл.");
            toolTip1.SetToolTip(saveResult, "Сохранить результат выполнения операции в файл.");
            toolTip1.SetToolTip(downloadArray, "Загрузить массив из файл.");
        }
        /// <summary>
        /// MouseClick event of button "Генерация".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of class EventArgs. </param>
        private void generate_Click(object sender, EventArgs e)
        {
            array = new MyArray((int)size.Value, (int)borderA.Value, (int)borderB.Value);
            GenerateArray('g', (int)size.Value);
        }
        /// <summary>
        /// MouseClick event of button "Выполнить".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of class EventArgs. </param>
        private void execute_Click(object sender, EventArgs e)
        {
            if (isError)
            {
                error.Clear();
                isError = false;
            }
            if (operation1.Checked)
            {
                array.CalculateSum((int)C.Value);
                result.Text = "Сумма элементов: " + array.Sum.ToString();
                key = '1';
            }
            else if (operation2.Checked)
            {
                array.SortByDescend();
                GenerateArray('s', array.Length);
                result.Text = "Массив отсортирован. Посмотреть можно в таблице.";
                key = '2';
            }
            else if (operation3.Checked)
            {
                array.CalculateSumOutSideTheRange((int)C1.Value, (int)C2.Value);
                result.Text = "Сумма элементов: " + array.Sum.ToString() + "; количество элементов: " + array.Count;
                key = '3';
            }
            else if (operation4.Checked)
            {
                array.SumOfSpecialElements();
                result.Text = "Сумма элементов: " + array.Sum.ToString() + "; количество элементов: " + array.Count;
                key = '4';
            }
            else
            {
                error.SetError(operation1, "Choose an operation.");
                error.SetError(operation2, "Choose an operation.");
                error.SetError(operation3, "Choose an operation.");
                error.SetError(operation4, "Choose an operation.");
                isError = true;
            }
        }
        /// <summary>
        /// MouseClick event of pictureBox "saveArray".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of class MouseEventArgs. </param>
        private void saveArray_MouseClick(object sender, MouseEventArgs e)
        {
            if (!execute.Enabled) return;
            try
            {
                SaveInFile('a');
            }
            catch (Exception ex) { error.SetError(saveArray, ex.Message); isError = true; }
        }
        /// <summary>
        /// MouseClick event of pictureBox "saveResult".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of class MouseEventArgs. </param>
        private void saveResult_MouseClick(object sender, MouseEventArgs e)
        {
            if (!execute.Enabled || key == default) return;
            try
            {
                SaveInFile('r');
            }
            catch (Exception ex) { error.SetError(saveResult, ex.Message); isError = true; }
        }
        /// <summary>
        /// MouseClick event of pictureBox "downloadArray".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of class MouseEventArgs. </param>
        private void downloadArray_MouseClick(object sender, MouseEventArgs e)
        {
            if (isError == true) isError = false;
            try
            {
                openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|XML-документ (*.xml)|*.xml";
                OpenFileDialog1.FilterIndex = 1;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fInfo = new FileInfo(openFileDialog1.FileName);
                    array = MyArray.OpenArrayFromFile(array, fInfo.FullName);
                    GenerateArray('d', array.Length);
                }
            }
            catch (Exception ex) { error.SetError(downloadArray, ex.Message); isError = true; }
        }
        /// <summary>
        /// Method for saving array or results in the file.
        /// </summary>
        /// <param name="keySave"> Saving key: a - to save an array, r - to save a result. </param>
        private void SaveInFile(char keySave)
        {
            if (isError == true) isError = false;
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "xml files(*.xml)|*.xml|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileInfo fInfo = new FileInfo(saveFileDialog1.FileName);
                if(keySave == 'a') array.SaveArrayInFile(fInfo.FullName, nameof(array));
                if (keySave == 'r') array.SaveResultInFile(fInfo.FullName, key);
            }
        }
        /// <summary>
        /// Method for generate an array.
        /// </summary>
        /// <param name="keyGen"> Generation key: d - to download an array, g - to generate an array, s - sorted array </param>
        /// <param name="size"> Size of an array. </param>
        private void GenerateArray(char keyGen, int size)
        {
            int arrSize = size;
            if (arrSize > 100) arrSize = 100;
            dataGridView1.ColumnCount = 2;
            dataGridView1.RowCount = arrSize;
            dataGridView1.Columns[0].HeaderText = "i";
            dataGridView1.Columns[1].HeaderText = "array[i]";
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 0; i < arrSize; i++)
            {
                dataGridView1[0, i].Value = i;
                dataGridView1[1, i].Value = array[i];
            }
            execute.Enabled = true;
            if (size <= 10)
            {
                if (result.Text.Length > 0) result.Clear();
                for (int i = 0; i < size; i++)
                {
                    result.Text += array[i].ToString();
                    if (i != size - 1) result.Text += ", ";
                }
                if (keyGen == 's') return;
            }
            else
            {
                if (keyGen == 'g') result.Text = "Массив сгенерирован. Посмотреть можно в таблице.";
                else result.Text = "Массив загружен. Посмотреть можно в таблице.";
            }
        }
    }
}
