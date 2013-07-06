using PLCSimConnector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DotNetSiemensPLCToolBoxLibrary.DataTypes.Projectfolders.Step7V5;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for PCS7ProjectTest and is intended
    ///to contain all PCS7ProjectTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PCS7ProjectTest
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
        ///A test for PCS7Project Constructor
        ///</summary>
        [TestMethod()]
        public void PCS7ProjectConstructorTest()
        {
            PCS7Project target = new PCS7Project();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for PCS7Project Constructor
        ///</summary>
        [TestMethod()]
        public void PCS7ProjectConstructorTest1()
        {
            string projectFile = string.Empty; // TODO: Initialize to an appropriate value
            PCS7Project target = new PCS7Project(projectFile);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for GetOutputImageSymbols
        ///</summary>
        [TestMethod()]
        public void GetOutputImageSymbolsTest()
        {
            PCS7Project target = new PCS7Project(); // TODO: Initialize to an appropriate value
            string[] expected = null; // TODO: Initialize to an appropriate value
            string[] actual;
            actual = target.GetOutputImageSymbols();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetOutputImageSymbolsOperands
        ///</summary>
        [TestMethod()]
        public void GetOutputImageSymbolsOperandsTest()
        {
            PCS7Project target = new PCS7Project(); // TODO: Initialize to an appropriate value
            string[,] expected = null; // TODO: Initialize to an appropriate value
            string[,] actual;
            actual = target.GetOutputImageSymbolsOperands();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for File
        ///</summary>
        [TestMethod()]
        public void FileTest()
        {
            PCS7Project target = new PCS7Project(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.File = expected;
            actual = target.File;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PCS7SymbolTable
        ///</summary>
        [TestMethod()]
        public void PCS7SymbolTableTest()
        {
            PCS7Project target = new PCS7Project(); // TODO: Initialize to an appropriate value
            SymbolTable actual;
            actual = target.PCS7SymbolTable;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PCS7Project Constructor
        ///</summary>
        [TestMethod()]
        public void PCS7ProjectConstructorTest2()
        {
            PCS7Project target = new PCS7Project();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for PCS7Project Constructor
        ///</summary>
        [TestMethod()]
        public void PCS7ProjectConstructorTest3()
        {
            string projectFile = string.Empty; // TODO: Initialize to an appropriate value
            PCS7Project target = new PCS7Project(projectFile);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for GetOutputImageSymbols
        ///</summary>
        [TestMethod()]
        public void GetOutputImageSymbolsTest1()
        {
            PCS7Project target = new PCS7Project(); // TODO: Initialize to an appropriate value
            string[] expected = null; // TODO: Initialize to an appropriate value
            string[] actual;
            actual = target.GetOutputImageSymbols();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetOutputImageSymbolsOperands
        ///</summary>
        [TestMethod()]
        public void GetOutputImageSymbolsOperandsTest1()
        {
            PCS7Project target = new PCS7Project(); // TODO: Initialize to an appropriate value
            string[,] expected = null; // TODO: Initialize to an appropriate value
            string[,] actual;
            actual = target.GetOutputImageSymbolsOperands();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for File
        ///</summary>
        [TestMethod()]
        public void FileTest1()
        {
            PCS7Project target = new PCS7Project(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.File = expected;
            actual = target.File;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PCS7SymbolTable
        ///</summary>
        [TestMethod()]
        public void PCS7SymbolTableTest1()
        {
            PCS7Project target = new PCS7Project(); // TODO: Initialize to an appropriate value
            SymbolTable actual;
            actual = target.PCS7SymbolTable;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
