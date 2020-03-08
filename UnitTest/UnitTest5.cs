using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Divisionsmatch;

namespace UnitTest
{
    [TestClass]
    public class UnitTest5
    {
        private static Config config = null;
        private static Staevne teststaevne = null;

        #region Test Initialization

        /// <summary>
        /// Initializes a new instance of the UnitTest1 class
        /// </summary>
        public UnitTest5()
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
            teststaevne = new Staevne("unittest5-EResults");
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
        [DeploymentItem(@"UnitTest\TestData\unittest5_2+3div_eresults.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest5_2+3div_eresults.xml")]
        [DeploymentItem(@"UnitTest\TestData\unittest5_resultat_2+3div_eresults.txt")]        
        public void unittest5_01()
        {
            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest5_2+3div_eresults.divi");
            teststaevne.Beregnpoint(config, "unittest5_2+3div_eresults.xml");

            string x = "";
            string resultat = "";

            x = teststaevne.Printmatcher() + Environment.NewLine + string.Concat(teststaevne.LavTXTafsnit().ToArray());
            System.Diagnostics.Debug.Print(x);
            resultat = System.IO.File.ReadAllText("unittest5_resultat_2+3div_eresults.txt", Encoding.Default);
            _TestLines(resultat, x);
        }

        private void _TestLines(string a, string b)
        {
            string[] alines = a.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string[] blines = b.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            Assert.AreEqual(alines.Count(), blines.Count(), "ikke lige mange linjer i resultaterne");
            for (int i = 0; i < alines.Count(); i++)
            {
                Assert.AreEqual(alines[i], blines[i], "linje " + i.ToString() + " er forskellig");
            }
        }
    }
}
