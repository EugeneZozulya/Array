using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Matrix;

namespace TestArray
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Test of metod Calculate Sum.
        /// </summary>
        [TestMethod]
        public void TestMethodCalculateSum()
        {
            int[] array = new int[] { 3, 4, 6, 1, 2, 5, 5, 10 };
            int expected = 16;
            MyArray myarray = new MyArray(array);
            myarray.CalculateSum(1);
            Assert.AreEqual(expected, myarray.Sum);
        }
        /// <summary>
        /// Test of metod SortedByDescend.
        /// </summary>
        [TestMethod]
        public void TestMethodSortByDescend()
        {
            int[] array = new int[] { 3, 4, 6, 1, 2, 5, 5, 10 };
            int[] expected = new int[] { 10, 6, 5, 5, 4, 3, 2, 1 };
            MyArray myarray = new MyArray(array);
            myarray.SortByDescend();
            for (int i = 0; i < array.Length; i++)
                Assert.AreEqual(expected[i], myarray[i]);
        }
        /// <summary>
        /// Test of metod CalculateSumInTheRange.
        /// </summary>
        [TestMethod]
        public void TestMethodCalculateSumOutSideTheRange()
        {
            int[] array = new int[] { 3, 4, 6, 1, 2, 5, 5, 10 };
            int expectedSum = 16, expectedCount = 4;
            MyArray myarray = new MyArray(array);
            myarray.CalculateSumOutSideTheRange(4,6);
            Assert.AreEqual(expectedSum, myarray.Sum);
            Assert.AreEqual(expectedCount, myarray.Count);
        }
        /// <summary>
        /// Test of metod SumOfSpecialElements.
        /// </summary>
        [TestMethod]
        public void TestMethodSumOfSpecialElements()
        {
            int[] array = new int[] { 302, 40, 6, 10, 2, 5, 5, 10 };
            int expected = 372, expectedCount = 6;
            MyArray myarray = new MyArray(array);
            myarray.SumOfSpecialElements();
            Assert.AreEqual(expected, myarray.Sum);
            Assert.AreEqual(expectedCount, myarray.Count);
        }
    }
}
