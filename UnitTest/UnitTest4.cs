using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Divisionsmatch;

namespace UnitTest
{
    [TestClass]
    public class UnitTest4
    {
        private static Config config = null;
        private static Staevne teststaevne = null;

        #region Test Initialization

        /// <summary>
        /// Initializes a new instance of the UnitTest1 class
        /// </summary>
        public UnitTest4()
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
            teststaevne = new Staevne("unittest4-OS2010");
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
        [DeploymentItem(@"UnitTest\TestData\unittest4_div match 2010 format.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest4_klasse resultat xml v 203.xml")]
        [DeploymentItem(@"UnitTest\TestData\unittest4_resultat_xml.txt")]        
        public void unittest4_01()
        {
            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest4_div match 2010 format.divi");
            teststaevne.Beregnpoint(config, "unittest4_klasse resultat xml v 203.xml");

            string x = "";
            string resultat = "";

            x = teststaevne.Printmatcher() + Environment.NewLine + string.Concat(teststaevne.LavTXTafsnit(config).ToArray());
            System.Diagnostics.Debug.Print(x);
            resultat = System.IO.File.ReadAllText("unittest4_resultat_xml.txt", Encoding.Default);
            _TestLines(resultat, x);
        }

        [TestMethod]
        [DeploymentItem(@"UnitTest\TestData\unittest4_div match 2010 format.csv")]
        [DeploymentItem(@"UnitTest\TestData\unittest4_div match 2010 format.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest4_resultat_csv.txt")]
        public void unittest4_02()
        {
            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest4_div match 2010 format.divi");
            teststaevne.Beregnpoint(config, "unittest4_div match 2010 format.csv");

            string x = "";
            string resultat = "";

            x = teststaevne.Printmatcher() + Environment.NewLine + string.Concat(teststaevne.LavTXTafsnit(config).ToArray());
            System.Diagnostics.Debug.Print(x);
            resultat = System.IO.File.ReadAllText("unittest4_resultat_csv.txt", Encoding.Default);
            _TestLines(resultat, x);
        }

        [TestMethod]
        [DeploymentItem(@"UnitTest\TestData\unittest4_div match 2010 format.csv")]
        [DeploymentItem(@"UnitTest\TestData\unittest4_div match 2010 format.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest4_startliste_csv.txt")]
        public void unittest4_03_startliste()
        {
            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest4_div match 2010 format.divi");

            string x = "";
            string resultat = "";

            List<Klub> klubber = new List<Klub>();
            var alleloebere = Util.ReadRunnersFromStartCsv("unittest4_div match 2010 format.csv", config, klubber);

            x = Util.BaneStartListe(true, true, alleloebere, config); 
            System.Diagnostics.Debug.Print(x);
            resultat = System.IO.File.ReadAllText("unittest4_startliste_csv.txt", Encoding.Default);
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
