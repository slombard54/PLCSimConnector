using DotNetSiemensPLCToolBoxLibrary.DataTypes.Projectfolders;
using Microsoft.QualityTools.Testing.Fakes;
using PLCSimConnector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using PLCSimConnector.DataPoints;
using PLCSimConnector.Fakes;
using DotNetSiemensPLCToolBoxLibrary.DataTypes.Projectfolders.Step7V5.Fakes;
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
            target.Dispose();
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
            Assert.IsTrue(offset<=target.OutputImageBuffer.Capacity);
        }

        /// <summary>
        ///A test for UpdateImages verifing Data is updated in memory streams
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

                }; 
            var target = new SimulatedPLC(simSystem); 
            simSystem.Connect();
            target.OutputImageOffestRequest(5);
            target.UpdateImages();
            simSystem.Disconnect();

            Assert.IsTrue(target.OutputImageBuffer.GetBuffer().SequenceEqual(testData));
        }
        /// <summary>
        ///A test for UpdateImages when there is no connection to PLCSim
        ///</summary>
        [TestMethod()]
        public void UpdateImagesTestNullData()
        {
            var simSystem = new StubPLCSim()
            {

                ReadOutputImageInt32Int32ImageDataTypeConstantsObjectRef =
                (int a, int b, ImageDataTypeConstants c, ref object pData) =>
                {
                    pData = null;
                }

            }; 
            var target = new SimulatedPLC(simSystem);
            simSystem.Connect();
            target.OutputImageOffestRequest(5);
            try
            {
                target.UpdateImages();
            }
            catch (Exception e)
            {
                
                Assert.Fail("Exception thrown by UpdateImages call\n" + e.ToString());
            }
            finally
            {
                simSystem.Disconnect();
            }
            

            //Assert.Fail("Exception not found");
        }

        /// <summary>
        ///A test for AddScaledDataPoint
        ///</summary>
        [TestMethod()]
        public void AddScaledDataPointTest()
        {
            using (ShimsContext.Create())
            {
                ShimPCS7Project.AllInstances.PCS7SymbolTableGet = (proj)    => new DotNetSiemensPLCToolBoxLibrary.DataTypes.Projectfolders.Step7V5.SymbolTable();
                ShimSymbolTable.AllInstances.GetEntryFromSymbolString = (table, s) => new SymbolTableEntry()
                    {
                        Comment = "Test Point",
                        DataType = "Real",
                        Operand =  "EW 1001",
                        OperandIEC = "IW 1001",
                        Symbol = s
                    };
            
                var target = new SimulatedPLC {Project = new PCS7Project()};

                string point = "Test"; 
                float engHi = 100; 
                float engLow = 0; 
                float rawHi = 4; 
                float rawLow = 20; 
                var actual = (PLCDataPoint) target.AddDataPoint(point);
                var i = actual.Offset;
                actual.DataPointScaling(engHi, engLow, rawHi, rawLow);
                var lactual = (PLCDataPoint)target.AddDataPoint("IB 456.1");
                lactual.DataPointScaling(engHi, engLow, rawHi, rawLow);
                i = lactual.Offset;
                Assert.IsNotNull(actual);
                Assert.AreEqual(point, actual.Symbol);
                /*Assert.AreEqual(engHi, actual.ScaleEngHigh);
                Assert.AreEqual(engLow, actual.ScaleEngLow);
                Assert.AreEqual(rawHi, actual.ScaleRawHigh);
                Assert.AreEqual(rawLow, actual.ScaleRawLow);*/
            }

        }

        /// <summary>
        ///A test for AddScaledDataPoint
        ///</summary>
        [TestMethod()]
        public void AddScaledDataPointNullTest()
        {
            using (ShimsContext.Create())
            {

                var target = new SimulatedPLC {Project = null};

                string point = "Test";
                float engHi = 100;
                float engLow = 0;
                float rawHi = 4;
                float rawLow = 20;
                var actual = (PLCDataPoint) target.AddDataPoint(point);
                actual.DataPointScaling( engHi, engLow, rawHi, rawLow);
                Assert.IsNotNull(actual);
                Assert.AreEqual(point, actual.Symbol);
                /*Assert.AreEqual(engHi, actual.ScaleEngHigh);
                Assert.AreEqual(engLow, actual.ScaleEngLow);
                Assert.AreEqual(rawHi, actual.ScaleRawHigh);
                Assert.AreEqual(rawLow, actual.ScaleRawLow);*/
            }
        }
    }
}
