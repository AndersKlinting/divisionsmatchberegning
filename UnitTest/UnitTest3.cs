using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Divisionsmatch;

namespace UnitTest
{
    [TestClass]
    public class UnitTest3
    {
        private static Config config = null;
        private static Staevne teststaevne = null;

        #region Test Initialization

        /// <summary>
        /// Initializes a new instance of the UnitTest1 class
        /// </summary>
        public UnitTest3()
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
        [DeploymentItem(@"UnitTest\TestData\unittest3_Mønsted_divisionsmatch.divi")]
        [DeploymentItem(@"UnitTest\TestData\unittest3_iofres.xml")]
        [DeploymentItem(@"UnitTest\TestData\unittest3_resultat.txt")]
        [DeploymentItem(@"UnitTest\TestData\unittest3_resultat.htm")]
        public void unittest3_01()
        {
            // kør kommandlinjen og test at output er OK.
            config = Config.LoadDivi("unittest3_Mønsted_divisionsmatch.divi");
            teststaevne.Beregnpoint(config, "unittest3_iofres.xml");

            string x = "";
            string resultat = "";

            x = teststaevne.Printmatcher() + Environment.NewLine + string.Concat(teststaevne.LavTXTafsnit().ToArray());
            System.Diagnostics.Debug.Print(x);
            resultat = System.IO.File.ReadAllText("unittest3_resultat.txt", Encoding.Default);
            _TestLines(resultat, x);
            
            string p = new Uri(teststaevne.LavHTML(teststaevne.LavHTMLafsnit())).LocalPath;
            x = System.IO.File.ReadAllText(p, UTF8Encoding.Default);
            resultat = System.IO.File.ReadAllText("unittest3_resultat.htm", Encoding.Default);

            int p1= x.IndexOf("<title>") + 7;
            int p2= x.IndexOf("</title>");
            x = x.Replace(x.Substring(p1,p2-p1),"");
            p1 = resultat.IndexOf("<title>") + 7;
            p2 = resultat.IndexOf("</title>");
            resultat = resultat.Replace(resultat.Substring(p1, p2 - p1), "");

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
