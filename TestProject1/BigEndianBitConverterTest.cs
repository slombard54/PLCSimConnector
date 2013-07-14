using System.Diagnostics;
using PLCSimConnector.DataPoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for BigEndianBitConverterTest and is intended
    ///to contain all BigEndianBitConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BigEndianBitConverterTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ToInt32
        ///</summary>
        [TestMethod()]
        public void ToInt32Test()
        {

            const int index = 0; 
            const int expected = 20000;
            byte[] array = BitConverter.GetBytes(expected);
            array = array.Reverse().ToArray();
            int actual = BigEndianBitConverter.ToInt32(array, index);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for ToInt32
        ///</summary>
        [TestMethod()]
        public void ToInt32ExceptionTest()
        {

            int index = 0;
            byte[] array = null;
            try
            {
                int actual = BigEndianBitConverter.ToInt32(array, index);
            }
            catch (ArgumentNullException e)
            {
                Assert.IsInstanceOfType(e,typeof(ArgumentNullException));
            }

            index = 30;
            array = new byte[5];
            try
            {
                int actual = BigEndianBitConverter.ToInt32(array, index);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException));
            }

            index = 4;
            try
            {
                int actual = BigEndianBitConverter.ToInt32(array, index);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException));
            }

        }

        /// <summary>
        ///A test for ToInt32
        ///</summary>
        [TestMethod()]
        public void ToInt16ExceptionTest()
        {

            int index = 0;
            byte[] array = null;
            try
            {
                BigEndianBitConverter.ToInt16(array, index);
            }
            catch (ArgumentNullException e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentNullException));
            }

            index = 30;
            array = new byte[5];
            try
            {
                int actual = BigEndianBitConverter.ToInt16(array, index);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException));
            }

            index = 4;
            try
            {
                BigEndianBitConverter.ToInt16(array, index);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException));
            }

        }

        /// <summary>
        ///A test for ToBESingle
        ///</summary>
        [TestMethod()]
        public void ToSingleTest()
        {
            const int index = 0; 
            const float expected = 120.5F; 
            
            byte[] array = BitConverter.GetBytes(expected);
            array = array.Reverse().ToArray();
            float actual = BigEndianBitConverter.ToSingle(array, index);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToInt16
        ///</summary>
        [TestMethod()]
        public void ToInt16Test()
        {
            const int index = 0;
            const short expected = 2000;
            byte[] array = BitConverter.GetBytes(expected);
            array = array.Reverse().ToArray();
            short actual = BigEndianBitConverter.ToInt16(array, index);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetBytes
        ///</summary>
        [TestMethod()]
        public void GetBytesInt32Test()  
        {
            const int value = 30000;
            var watch = new Stopwatch();
            watch.Start();
            byte[] expected = BitConverter.GetBytes(value).Reverse().ToArray();
            watch.Stop();
            Debug.WriteLine("Linq array reversal {0}", watch.ElapsedTicks);
            watch.Restart();
            byte[] actual = BigEndianBitConverter.GetBytes(value);
            watch.Stop();
            Debug.WriteLine("BigEndianBitConverter {0}", watch.ElapsedTicks);
            Assert.IsTrue(expected.SequenceEqual(actual));
            //Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for GetBytes
        ///</summary>
        [TestMethod()]
        public void GetBytesSingleTest()
        {
            const float value = 123.5F;
            var watch = new Stopwatch();
            watch.Start();
            byte[] expected = BitConverter.GetBytes(value).Reverse().ToArray();
            watch.Stop();
            Debug.WriteLine("Linq array reversal {0}", watch.ElapsedTicks);
            watch.Restart();
            byte[] actual = BigEndianBitConverter.GetBytes(value);
            watch.Stop();
            Debug.WriteLine("BigEndianBitConverter {0}", watch.ElapsedTicks);
            Assert.IsTrue(expected.SequenceEqual(actual));
            //Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for GetBytes
        ///</summary>
        [TestMethod()]
        public void GetBytesInt16Test()
        {
            const short value = 2000;
            var watch = new Stopwatch();
            watch.Start();
            byte[] expected = BitConverter.GetBytes(value).Reverse().ToArray();
            watch.Stop();
            Debug.WriteLine("Linq array reversal {0}", watch.ElapsedTicks);
            watch.Restart();
            byte[] actual = BigEndianBitConverter.GetBytes(value);
            watch.Stop();
            Debug.WriteLine("BigEndianBitConverter {0}", watch.ElapsedTicks);
            Assert.IsTrue(expected.SequenceEqual(actual));
            //Assert.AreEqual(expected, actual);
        }
    }
}
