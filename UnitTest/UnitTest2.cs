using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Divisionsmatch;

namespace UnitTest
{
    [TestClass]
    public class UnitTest2
    {
        private static Config config = null;
        private static Staevne teststaevne = null;

        #region Test Initialization

        /// <summary>
        /// Initializes a new instance of the UnitTest1 class
        /// </summary>
        public UnitTest2()
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
        [DeploymentItem(@"UnitTest\TestData\unittest2_3-4 Division Op-Ned-v2.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest2_resultat.csv")]
        [DeploymentItem(@"UnitTest\TestData\unittest2_3-4div.txt")]
        public void unittes2_01()
        {
            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest2_3-4 Division Op-Ned-v2.divi");
            teststaevne.Beregnpoint(config, "unittest2_resultat.csv");

            string x = teststaevne.Printmatcher() + Environment.NewLine + string.Concat(teststaevne.LavTXTafsnit(config).ToArray()); ;
            System.Diagnostics.Debug.Print(x);
            string resultat = System.IO.File.ReadAllText("unittest2_3-4div.txt", Encoding.Default);
            _TestLines(resultat, x);
        }

        [TestMethod]
        [DeploymentItem(@"UnitTest\TestData\unittest2_4-5 Division Op-Ned-v2.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest2_resultat.csv")]
        [DeploymentItem(@"UnitTest\TestData\unittest2_4-5div.txt")]
        public void unittes2_02()
        {
            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest2_4-5 Division Op-Ned-v2.divi");
            teststaevne.Beregnpoint(config, "unittest2_resultat.csv");

            string x = teststaevne.Printmatcher() + Environment.NewLine + string.Concat(teststaevne.LavTXTafsnit(config).ToArray()); ;
            System.Diagnostics.Debug.Print(x);
            string resultat = System.IO.File.ReadAllText("unittest2_4-5div.txt", Encoding.Default);
            _TestLines(resultat, x);
        }

        [TestMethod]
        [DeploymentItem(@"UnitTest\TestData\unittest2_5-6 Division Op-Ned-v2.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest2_resultat.csv")]
        [DeploymentItem(@"UnitTest\TestData\unittest2_5-6div.txt")]
        public void unittes2_03()
        {
            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest2_5-6 Division Op-Ned-v2.divi");
            config.PrintAlle = true;
            teststaevne.Beregnpoint(config, "unittest2_resultat.csv");

            string x = teststaevne.Printmatcher() + Environment.NewLine + string.Concat(teststaevne.LavTXTafsnit(config).ToArray()); ;
            System.Diagnostics.Debug.Print(x);
            string resultat = System.IO.File.ReadAllText("unittest2_5-6div.txt", Encoding.Default);
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
