using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;

namespace Matrix
{
    /// <summary>
    /// Class MyArray.
    /// </summary>
    [Serializable]
    public class MyArray
    {
        private int[] array;
        private int sum, count;
        private int length;
        /// <summary>
        /// Constructor with parametrs.
        /// </summary>
        /// <param name="size"> Size of the array. </param>
        /// <param name="A"> Minimal value of the range. </param>
        /// <param name="B"> Maximum value of the range. </param>
        public MyArray(int size, int A, int B)
        {
            array = new int[size];
            Random rnd = new Random();
            for (int i = 0; i < size; i++) array[i] = rnd.Next(A, B);
            length = size;
        }
        /// <summary>
        /// Constructor with parametrs.
        /// </summary>
        /// <param name="arr"> Array. </param>
        public MyArray(int[] arr)
        {
            array = new int[arr.Length];
            arr.CopyTo(array, 0);
        }
        /// <summary>
        /// Constructor without parametrs.
        /// </summary>
        public MyArray() { }
        /// <summary>
        /// Indextor.
        /// </summary>
        /// <param name="index"> Index of the array. </param>
        /// <returns></returns>
        public int this[int index]
        {
            get
            {
                return this.array[index];
            }
        }
        /// <summary>
        /// Property of "lenght" variable.
        /// </summary>
        public int Length { get => length; }
        /// <summary>
        /// Property of "sum" variable.
        /// </summary>
        public int Sum { get => sum; }
        /// <summary>
        /// Property of "count" variable.
        /// </summary>
        public int Count { get => count; }
        /// <summary>
        /// Calculate sum of the array elements that are greater or less than the number and their index is even.
        /// </summary>
        /// <param name="num"> Number. </param>
        public void CalculateSum(int num)
        {
            sum = 0;
            for(int i = 0; i<array.Length; i++)
            {
                if (i % 2 == 0 && (array[i] > num || array[i] < num)) sum += array[i];
            }
        }
        /// <summary>
        /// Sort array by descend.
        /// </summary>
        public void SortByDescend()
        {
            Array.Sort(array);
            Array.Reverse(array);
        }
        /// <summary>
        /// Calculate sum of the array elements and their count that are not in a range.
        /// </summary>
        /// <param name="num1"> Minimal value of the range. </param>
        /// <param name="num2"> Maximum value of the range. </param>
        public void CalculateSumOutSideTheRange(int num1, int num2)
        {
            sum = 0;
            count = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < num1 || array[i] > num2)
                {
                    sum += array[i];
                    count++;
                }
            }
        }
        /// <summary>
        /// Calculate sum of the array elements which have a number composed of the first two digits is divisible by 5.
        /// </summary>
        public void SumOfSpecialElements()
        {
            sum = 0; count = 0;
            for(int i = 0; i < array.Length; i++)
            {
                int temp = Math.Abs(array[i]);
                List<int> digits = new List<int>();
                string number = "";
                while (temp > 0)
                {
                    int digit = temp % 10;
                    temp /= 10;
                    digits.Add(digit);
                }
                number += digits[digits.Count - 1];
                if (digits.Count > 1) number += digits[digits.Count - 2];
                temp = Convert.ToInt32(number);
                if (temp % 5 == 0)
                {
                    sum += array[i];
                    count++;
                }
            }
        }
        /// <summary>
        /// Save an array in the file.
        /// </summary>
        public void SaveArrayInFile(string filename, string arrayName)
        {
            if (File.Exists(filename)) File.Delete(filename);
            CreateFile(filename);
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(filename);
            XmlElement root = xdoc.DocumentElement;
            XmlElement arrName = xdoc.CreateElement(arrayName);
            for (int i = 0; i<array.Length; i++)
            {
                XmlElement element = xdoc.CreateElement("int");
                XmlText nameElement = xdoc.CreateTextNode(array[i].ToString());
                element.AppendChild(nameElement);
                arrName.AppendChild(element);
            }
            root.AppendChild(arrName);
            xdoc.Save(filename);
        }
        /// <summary>
        /// Save result in the file.
        /// </summary>
        public void SaveResultInFile(string filename, char operation)
        {
            if (File.Exists(filename)) File.Delete(filename);
            CreateFile(filename);
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(filename);
            XmlElement root = xdoc.DocumentElement;
            if(operation == '1' || operation == '3' || operation == '4')
            {
                XmlElement sum = xdoc.CreateElement("sum");
                XmlText nameSum = xdoc.CreateTextNode(Sum.ToString());
                sum.AppendChild(nameSum);
                root.AppendChild(sum);
            }
            if(operation == '3' || operation == '4')
            {
                XmlElement count = xdoc.CreateElement("count");
                XmlText nameSum = xdoc.CreateTextNode(Count.ToString());
                count.AppendChild(nameSum);
                root.AppendChild(count);
            }
            if(operation == '2')
            {
                XmlElement arr = xdoc.CreateElement("array");
                for (int i = 0; i < array.Length; i++)
                {
                    XmlElement element = xdoc.CreateElement("int");
                    XmlText nameElement = xdoc.CreateTextNode(array[i].ToString());
                    element.AppendChild(nameElement);
                    arr.AppendChild(element);
                }
                root.AppendChild(arr);
            }
            xdoc.Save(filename);
        }
        /// <summary>
        /// Open an array, that is in the file.
        /// </summary>
        public static MyArray OpenArrayFromFile(MyArray array, string filename)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(filename);
            XmlElement root = xdoc.DocumentElement;
            root = (XmlElement)root.FirstChild;
            List<int> arr = new List<int>();
            foreach (XmlNode xnode in root)
            {
                arr.Add(int.Parse(xnode.InnerText)); 
            }
            int[] temp = new int[arr.Count];
            arr.CopyTo(temp);
            array = new MyArray(temp);
            array.length = arr.Count;
            return array;
        }
        /// <summary>
        /// Create file, if not alreade created.
        /// </summary>
        /// <param name="filename"> Name of file. </param>
        /// <param name="documentName"> Name of document. </param>
        private void CreateFile(string filename)
        {
            XmlTextWriter textWriter;
            textWriter = new XmlTextWriter(filename, Encoding.UTF8);
            textWriter.WriteStartDocument();
            textWriter.WriteStartElement("MyArray");
            textWriter.WriteEndDocument();
            textWriter.Close();
        }
    }
}
