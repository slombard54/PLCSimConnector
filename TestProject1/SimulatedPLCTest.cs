using PLCSimConnector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using S7PROSIMLib;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for SimulatedPLCTest and is intended
    ///to contain all SimulatedPLCTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SimulatedPLCTest
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
        ///A test for SimulatedPLC Constructor
        ///</summary>
        [TestMethod()]
        public void SimulatedPLCConstructorTest()
        {
            var target = new SimulatedPLC();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for SimulatedPLC Constructor
        ///</summary>
        [TestMethod()]
        public void SimulatedPLCConstructorTest1()
        {
            IS7ProSim plcSimObject = null; // TODO: Initialize to an appropriate value
            var target = new SimulatedPLC(plcSimObject);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        public void DisposeTest()
        {
            var target = new SimulatedPLC(); // TODO: Initialize to an appropriate value
            target.Dispose();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for OutputImageOffestRequest
        ///</summary>
        [TestMethod()]
        public void OutputImageOffestRequestTest()
        {
            var target = new SimulatedPLC(); // TODO: Initialize to an appropriate value
            int offset = 50; // TODO: Initialize to an appropriate value
            target.OutputImageOffestRequest(offset);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UpdateImages
        ///</summary>
        [TestMethod()]
        public void UpdateImagesTest()
        {
            var testData = new Byte[] {1, 2, 3, 4, 5};
            var simSystem = new PLCSimConnector.Fakes.StubPLCSim()
                {

                    ReadOutputImageInt32Int32ImageDataTypeConstantsObjectRef =
                    (int a, int b, ImageDataTypeConstants c,ref object pData) =>
                        {
                            pData = testData;
                        }

                }; // TODO: Initialize to an appropriate value
            var target = new SimulatedPLC(simSystem); // TODO: Initialize to an appropriate value
            simSystem.Connect();
            target.OutputImageOffestRequest(5);
            target.UpdateImages();
            simSystem.Disconnect();

            Assert.IsTrue(target.OutputImageBuffer.GetBuffer().SequenceEqual(testData));
        }
    }
}
