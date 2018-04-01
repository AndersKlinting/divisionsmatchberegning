using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Divisionsmatch;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private static Config config = null;
        private static Staevne teststaevne = null;

        #region Test Initialization
        
        /// <summary>
        /// Initializes a new instance of the UnitTest1 class
        /// </summary>
        public UnitTest1()
        {
        }

        private TestContext _testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }

            set
            {
                _testContextInstance = value;
            }
        }
        /// <summary>
        /// Initializes myclass
        /// </summary>
        /// <param name="testContext">Current test context</param>
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            teststaevne = new Staevne("unittest");
        }

        /// <summary>
        /// Cleaning up of database
        /// </summary>
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
        }

        #endregion Test Initialization
        
        [TestMethod]
        [DeploymentItem(@"UnitTest\TestData\unittest1.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest1.csv")]
        [DeploymentItem(@"UnitTest\TestData\unittest1stilling.txt")]
        public void unittest1__01_stilling()
        {
            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest1.divi");
            teststaevne.Beregnpoint(config, "unittest1.csv");

            string x = teststaevne.Printstilling(false);
            string resultat = System.IO.File.ReadAllText("unittest1stilling.txt", Encoding.Default);
            _TestLines(resultat, x);                
        }

        [TestMethod]
        [DeploymentItem(@"UnitTest\TestData\unittest1.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest1.csv")]
        [DeploymentItem(@"UnitTest\TestData\unittest1matcher.txt")]
        public void unittest1_02_matcher()
        {
            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest1.divi");
            teststaevne.Beregnpoint(config, "unittest1.csv");

            string x = teststaevne.Printmatcher();
            System.Diagnostics.Debug.Print(x);
            string resultat = System.IO.File.ReadAllText("unittest1matcher.txt", UTF8Encoding.Default);
            _TestLines(resultat, x);
        }

        [TestMethod]
        [DeploymentItem(@"UnitTest\TestData\unittest1.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest1.csv")]
        [DeploymentItem(@"UnitTest\TestData\unittest1lister.txt")]
        public void unittest1_03_lister()
        {
            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest1.divi");
            teststaevne.Beregnpoint(config, "unittest1.csv");

            string x = teststaevne.Printmatcher() + Environment.NewLine + string.Concat(teststaevne.LavTXTafsnit(config).ToArray());
            System.Diagnostics.Debug.Print(x);
            string resultat = System.IO.File.ReadAllText("unittest1lister.txt", Encoding.Default);
            _TestLines(resultat, x);
        }

        [TestMethod]
        [DeploymentItem(@"UnitTest\TestData\unittest1.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest1.csv")]
        [DeploymentItem(@"UnitTest\TestData\unittest1baner.txt")]
        public void unittest1_04_baner()
        {
            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest1.divi");
            config.PrintBaner = true;
            teststaevne.Beregnpoint(config, "unittest1.csv");

            string x = teststaevne.Printmatcher() + Environment.NewLine + string.Concat(teststaevne.LavTXTafsnit(config).ToArray()); ;
            System.Diagnostics.Debug.Print(x);
            string resultat = System.IO.File.ReadAllText("unittest1baner.txt", Encoding.Default);
            _TestLines(resultat, x);
        }

        [TestMethod]
        [DeploymentItem(@"UnitTest\TestData\unittest1.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest1.csv")]
        [DeploymentItem(@"UnitTest\TestData\unittest1alle.txt")]
        public void unittest1_05_alle()
        {
            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest1.divi");
            config.PrintAlle = true;
            teststaevne.Beregnpoint(config, "unittest1.csv");

            string x = teststaevne.Printmatcher() + Environment.NewLine + string.Concat(teststaevne.LavTXTafsnit(config).ToArray()); ;
            System.Diagnostics.Debug.Print(x);
            string resultat = System.IO.File.ReadAllText("unittest1alle.txt", Encoding.Default);
            _TestLines(resultat, x);
        }

        [TestMethod]
        [DeploymentItem(@"UnitTest\TestData\unittest1.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest1.csv")]
        [DeploymentItem(@"UnitTest\TestData\unittest1allebaner.txt")]
        public void unittest1_06_allebaner()
        {
            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest1.divi");
            config.PrintBaner = true;
            config.PrintAlle = true;
            teststaevne.Beregnpoint(config, "unittest1.csv");

            string x = teststaevne.Printmatcher() + Environment.NewLine + string.Concat(teststaevne.LavTXTafsnit(config).ToArray()); ;
            System.Diagnostics.Debug.Print(x);
            string resultat = System.IO.File.ReadAllText("unittest1allebaner.txt", Encoding.Default);
            _TestLines(resultat, x);
        }

        private void _TestLines(string a, string b)
        {
            string[] alines = a.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string[] blines = b.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            //Assert.AreEqual(alines.Count(), blines.Count(), "ikke lige mange linjer i resultaterne");
            for (int i = 0; i < alines.Count(); i++)
            {
                Assert.AreEqual(alines[i], blines[i], "linje " + i.ToString() + " er forskellig");
            }
        }
    }
}
