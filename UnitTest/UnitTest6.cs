using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Divisionsmatch;
using System.Threading;
using System.Globalization;

namespace UnitTest
{
    [TestClass]
    public class UnitTest6
    {
        private static Config config = null;
        private static Staevne teststaevne = null;

        #region Test Initialization

        /// <summary>
        /// Initializes a new instance of the UnitTest1 class
        /// </summary>
        public UnitTest6()
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
            teststaevne = new Staevne("unittest6-OS2010");
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
        [DeploymentItem(@"UnitTest\TestData\unittest6_culture.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest6_culture.xml")]
        [DeploymentItem(@"UnitTest\TestData\unittest6_resultat_xml.txt")]        
        public void unittest6_01()
        {
            // set decimal comma
            CultureInfo culture = CultureInfo.CurrentCulture.Clone() as CultureInfo;
            culture.NumberFormat.NumberDecimalSeparator = ",";
            Thread.CurrentThread.CurrentCulture = culture;

            string test = (1.234).ToString();
            System.Diagnostics.Debug.Print(test);

            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest6_culture.divi");
            teststaevne.Beregnpoint(config, "unittest6_culture.xml");

            string x = "";
            string resultat = "";

            x = teststaevne.Printmatcher() + Environment.NewLine + string.Concat(teststaevne.LavTXTafsnit(config).ToArray());
            System.Diagnostics.Debug.Print(x);
            resultat = System.IO.File.ReadAllText(@"unittest6_resultat_xml.txt", Encoding.Default);
            _TestLines(resultat, x);
        }

        [TestMethod]
        [DeploymentItem(@"UnitTest\TestData\unittest6_culture.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest6_culture.xml")]
        [DeploymentItem(@"UnitTest\TestData\unittest6_resultat_xml.txt")]        
        public void unittest6_02()
        {
            // set decimal point
            CultureInfo culture = CultureInfo.CurrentCulture.Clone() as CultureInfo;
            culture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = culture;

            string test = (1.234).ToString();
            System.Diagnostics.Debug.Print(test);
            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest6_culture.divi");
            teststaevne.Beregnpoint(config, "unittest6_culture.xml");

            string x = "";
            string resultat = "";

            x = teststaevne.Printmatcher() + Environment.NewLine + string.Concat(teststaevne.LavTXTafsnit(config).ToArray());
            System.Diagnostics.Debug.Print(x);
            resultat = System.IO.File.ReadAllText("unittest6_resultat_xml.txt", Encoding.Default);
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
