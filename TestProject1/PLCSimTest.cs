using PLCSimConnector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using S7PROSIMLib;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for PLCSimTest and is intended
    ///to contain all PLCSimTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PLCSimTest
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
        ///A test for PLCSim Constructor
        ///</summary>
        [TestMethod()]
        public void PLCSimConstructorTest()
        {

                var target = new PLCSim();
                Assert.IsInstanceOfType(target,typeof(PLCSim));
                target.Dispose();
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        public void DisposeTest()
        {
            PLCSim target = new PLCSim(); // TODO: Initialize to an appropriate value
            target.Dispose();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for OutputImageOffestRequest
        ///</summary>
        [TestMethod()]
        public void OutputImageOffestRequestTest()
        {
            PLCSim target = new PLCSim(); // TODO: Initialize to an appropriate value
            int offset = 0; // TODO: Initialize to an appropriate value
            //target.OutputImageOffestRequest(offset);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ReadOutputImage
        ///</summary>
        [TestMethod()]
        public void ReadOutputImageTest()
        {
            PLCSim target = new PLCSim(); // TODO: Initialize to an appropriate value
            int startIndex = 0; // TODO: Initialize to an appropriate value
            int elementsToRead = 0; // TODO: Initialize to an appropriate value
            ImageDataTypeConstants dataType = new ImageDataTypeConstants(); // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            actual = target.ReadOutputImage(startIndex, elementsToRead, dataType);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ReadOutputImage
        ///</summary>
        [TestMethod()]
        public void ReadOutputImageTest1()
        {
            PLCSim target = new PLCSim(); // TODO: Initialize to an appropriate value
            int startIndex = 0; // TODO: Initialize to an appropriate value
            int elementsToRead = 0; // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            actual = target.ReadOutputImage(startIndex, elementsToRead);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateImages
        ///</summary>
        [TestMethod()]
        public void UpdateImagesTest()
        {
            PLCSim target = new PLCSim(); // TODO: Initialize to an appropriate value
            target.Connect();
            //target.UpdateImages();
            target.Disconnect();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
