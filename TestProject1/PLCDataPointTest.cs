using PLCSimConnector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using PLCSimConnector.DataPoints;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for PLCScaledDataPointTest and is intended
    ///to contain all PLCScaledDataPointTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PLCDataPointTest
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
        ///A test for Value
        ///</summary>
        [TestMethod()]
        public void ValueTest()
        {
            /*var target = new ScaledDataPoint();
            const float rawValue = 12;
            const float expected = 50;
            target.ValueGetAction = i => rawValue;
            target.ScaleEngHigh = 100;
            target.ScaleEngLow = 0;
            target.ScaleRawHigh = 4;
            target.ScaleRawLow = 20; 
            var actual = target.Value;
            Assert.AreEqual(expected, actual);*/
        }
    }
}
