/*
 * Divisionsmatch - beregning af resultater
 * Copyright (C) 2013 Anders Klinting
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using CommandLineParser.Arguments;
using Microsoft.Win32;
using AutoUpdaterDotNET;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.Xml;

namespace Divisionsmatch
{
    /// <summary>
    /// main form
    /// </summary>
    public partial class frmDivi : Form
    {
        #region member variables
        /// <summary>
        /// URL for auto update
        /// </summary>
        public static string autoUpdateSiteURL = @"http://www.fif-orientering.dk/divisionsmatch";

        /// <summary>
        /// URL for auto update
        /// </summary>
        public static string autoUpdateURL = @"http://www.fif-orientering.dk/divisionsmatch/versioninfo.xml";

        /// <summary>
        /// URL for auto update
            /// </summary>
        public static string autoUpdateTestURL = @"http://www.fif-orientering.dk/divisionsmatch/test/versioninfo.xml";

        private Hashtable ieOriginalPageSetup = new Hashtable();
        private Hashtable ieNewPageSetup = new Hashtable();
        private Config config = null;
        private string configFile = string.Empty;
        private Staevne _mitstaevne = null; // new Staevne(Application.ProductVersion);
        private FileSystemWatcher watcher = null;
        private string _txtResultatFil = string.Empty;

        private DivisionsResultat.DivisionsResultat _mitDivisionsResultat = null;
        private frmVisDivisionsResultat frmVisDivisionsResultat = null;

        private string _apptitle = string.Empty;

        private string _currentDiviDirectory = string.Empty;
        private string _currentResultDirectory = string.Empty;
        private string _currentExportDirectory = string.Empty;

        private bool bGetIESetup = false;
        private bool bIEprint = false;
        string oldPrinterName = string.Empty;
        private bool _isModified = false;
        private bool isModified
        {
            get
            {
                return _isModified;
            }

            set
            {
                _isModified = value;
                setTitle();
            }
        }

        private Staevne mitstaevne
        {
            get
            {
                if (_mitstaevne == null)
                {
                    _mitstaevne = new Staevne(Application.ProductVersion);
                }
                return _mitstaevne;
            }

            set
            {
                if (_mitstaevne != null)
                {
                    _mitstaevne.Dispose();
                }
                _mitstaevne = value;
           
                if (frmVisDivisionsResultat != null)
                {
                    frmVisDivisionsResultat.Staevne = _mitstaevne;
                }
            }
        }

        private DivisionsResultat.DivisionsResultat mitDivisionsResultat
        {
            get
            {
                return _mitDivisionsResultat;
            }

            set
            {
                _mitDivisionsResultat = value;

                if (frmVisDivisionsResultat != null)
                {
                    frmVisDivisionsResultat.Close();
                    frmVisDivisionsResultat = null;
                }
            }
        }
        #endregion

        #region constructor
        /// <summary>
        /// lav en dialog
        /// </summary>
        /// <param name="args"></param>
        public frmDivi(string[] args)
        {
            InitializeComponent();
            recentsToolStripMenuItem.UpdateList();
            _apptitle = "Divisionsmatch " + Application.ProductVersion;
            isModified = false;
#if DEBUG
            //// txtCSVFile.Text = @"C:\temp\divisionsturnering\divi20100530klasse.csv";
#endif

            try
            {
                // get original pageSetup values for IE
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\PageSetup", true))
                {
                    if (key == null)
                        return;

                    ieOriginalPageSetup.Add("margin_left", (string)key.GetValue("margin_left"));
                    ieOriginalPageSetup.Add("margin_right", (string)key.GetValue("margin_right"));
                    ieOriginalPageSetup.Add("margin_top", (string)key.GetValue("margin_top"));
                    ieOriginalPageSetup.Add("margin_bottom", (string)key.GetValue("margin_bottom"));
                    ieOriginalPageSetup.Add("header", (string)key.GetValue("header"));
                    ieOriginalPageSetup.Add("footer", (string)key.GetValue("footer"));
                    ieOriginalPageSetup.Add("orientation", (string)key.GetValue("orientation"));

                    // duplicate
                    foreach (var x in ieOriginalPageSetup.Keys)
                    {
                        ieNewPageSetup.Add(x, ieOriginalPageSetup[x]);
                    }
                }
            }
            catch
            {
            }

            webOutput.Navigate("about:blank");


            AutoUpdater.LetUserSelectRemindLater = true;
            if ((args.Length > 0) && (args[0].Equals("test", StringComparison.InvariantCultureIgnoreCase)))
            {
                AutoUpdater.Start(autoUpdateTestURL);
            }
            else if (args.Length > 0)
            {
                if (_doBatch(args))
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                AutoUpdater.Start(autoUpdateURL);
            }


        }
        #endregion

        #region løb menu handlers
        private void nytLøbToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // åben en divisionsresultat (en o-service exportfil.)
            frmÅbnDivisionsResultat openFile = new frmÅbnDivisionsResultat();
            openFile.Staevne = new Staevne(Application.ProductVersion);
            openFile.InitialDirectory = _currentDiviDirectory;

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                frmConfig frm = new frmConfig(openFile.Staevne.Config);
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        mitstaevne = openFile.Staevne;
                        config = mitstaevne.Config;
                        mitDivisionsResultat = openFile.DivisionsResultat;
                        _currentDiviDirectory = openFile.InitialDirectory;
                                                
                        configFile = string.Empty;
                        isModified = true;

                        panel2.Enabled = true;
                        gemLøbToolStripMenuItem.Enabled = true;
                        gemsomToolStripMenuItem.Enabled = true;
                        redigerToolStripMenuItem.Enabled = true;
                        startlisteToolStripMenuItem.Enabled = true;
                        gemDivisionsresultatToolStripMenuItem1.Enabled = (mitDivisionsResultat != null);

                        _resetBeregnOgPrint();
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }

        private void åbnLøbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // åben en gemt konfiguration
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Divisionsmatch filer (*.divi)|*.divi";
            openFile.CheckPathExists = true;
            openFile.AddExtension = true;
            openFile.DefaultExt = ".divi";
            openFile.SupportMultiDottedExtensions = true;
            openFile.Title = "Åbn konfiguration";
            openFile.Multiselect = false;
            ////openFile.RestoreDirectory = true;
            if (_currentDiviDirectory != string.Empty)
            {
                openFile.InitialDirectory = _currentDiviDirectory;
            }
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _currentDiviDirectory = Path.GetDirectoryName(openFile.FileName);
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    if (_åbnDivi(openFile.FileName))
                    {
                        recentsToolStripMenuItem.AddRecentItem(openFile.FileName);
                        
                        MessageBox.Show("Løbskonfigurationen er indlæst. Vælg en resultat-fil for at starte beregning");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Konfigurationen kunnne ikke åbnes \n" + ex.Message + "\n" + ex.StackTrace, "Fejl");
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private bool _åbnDivi(string fileName)
        {
            // load file
            Config c = Config.LoadDivi(fileName);
            DivisionsResultat.DivisionsResultat divisionsResultat = Util.OpenDivisionsResultat(c.DivisionsResultatFil);
            DivisionsResultat.DivisionsMatchResultat denneMatch = divisionsResultat.DivisionsMatchResultater.OrderBy(item => item.Runde).Last();
            divisionsResultat.DivisionsMatchResultater.Remove(denneMatch);

            Staevne staevne = new Staevne(Application.ProductVersion);
            staevne.Config = c;
            if (divisionsResultat != null)
            {
                string msg = string.Empty;
                if (!divisionsResultat.CheckStaevne(staevne, out msg))
                {
                    throw new Exception("Konfigurationen stemmer ikke med DivisionsResultatet i " + c.DivisionsResultatFil + "\n" + msg + "\nLav en ny konfigurering");
                }
            }

            mitstaevne = staevne;
            config = c;
            mitDivisionsResultat = divisionsResultat;
            configFile = fileName;

            // juster UI
            _makeNewPageSetup(config.pageSettings);

            panel2.Enabled = true;
            gemLøbToolStripMenuItem.Enabled = true;
            gemsomToolStripMenuItem.Enabled = true;
            redigerToolStripMenuItem.Enabled = true;
            startlisteToolStripMenuItem.Enabled = true;
            gemDivisionsresultatToolStripMenuItem1.Enabled = (mitDivisionsResultat != null);

            _resetBeregnOgPrint();

            return true;
        }

        private bool redigerConfig(Config cfg, string configFile)
        {
            // Redigerløb  - vis konfiguration
            frmConfig frm = new frmConfig(cfg.Clone() as Config);
            frm.editOnly = true;
            frm.saveFile = configFile;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.config = frm.Config;
                this.configFile = frm.saveFile;
                isModified = true;
                _resetBeregnOgPrint();

                if (_txtResultatFil != string.Empty)
                {
                    btnBeregn.Enabled = true;
                }
                return true;
            }
            else
            {
                if (frm.Config.NeedEdit)
                {
                    MessageBox.Show("Redigeringen er afsluttet uden at den er blevet komplet. Konfigurationen kan ikke bruges.");
                    return false;
                }
            }
            return true;
        }

        private void redigerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (redigerConfig(config, this.configFile))
            {
                mitstaevne.Config = config;
            }
        }

        private void gemLøbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (configFile != string.Empty)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    config.SaveDivi(configFile);
                    isModified = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Konfigurationen kunnne ikke gemmes \n" + ex.Message + "\n" + ex.StackTrace, "Fejl");
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                gemsomToolStripMenuItem_Click(sender, e);
            }
        }

        private void gemsomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Divisionsmatch filer (*.divi)|*.divi";
            saveFile.OverwritePrompt = true;
            saveFile.CheckPathExists = true;
            saveFile.AddExtension = true;
            saveFile.DefaultExt = ".divi";
            saveFile.SupportMultiDottedExtensions = true;
            saveFile.Title = "Gem konfiguration";
            ////saveFile.RestoreDirectory = true;
            if (_currentDiviDirectory != string.Empty)
            {
                saveFile.InitialDirectory = _currentDiviDirectory;
            }
            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    config.SaveDivi(saveFile.FileName);

                    configFile = saveFile.FileName;

                    recentsToolStripMenuItem.AddRecentItem(saveFile.FileName);

                    isModified = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Konfigurationen kunnne ikke gemmes \n" + ex.Message + "\n" + ex.StackTrace, "Fejl");
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        #endregion

        #region Beregn handlers
        private void btnBeregn_Click(object sender, EventArgs e)
        {
            _beregn();
        }

        private void bntOpenResultFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Resultat filer (*.xml, *.csv)|*.xml;*.csv"; //|OE resultat filer (*.csv)|*.csv";
            openFile.CheckPathExists = true;
            openFile.AddExtension = true;
            openFile.DefaultExt = ".csv";
            openFile.SupportMultiDottedExtensions = true;
            openFile.Title = "Åbn resultat";
            openFile.Multiselect = false;
            openFile.RestoreDirectory = true;
            if (_currentResultDirectory != string.Empty)
            {
                openFile.InitialDirectory = _currentResultDirectory;
            }
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // check om resultat er samme type som blev brugt til konfiguration
                bool isTxt = false;
                bool isEntryXml = false;
                bool isStartXml = false;
                bool isResultXml = false;
                string fileVersion = Util.CheckFileVersion(openFile.FileName, out isEntryXml, out isStartXml, out isResultXml, out isTxt);
                if (config.ConfigClassSrc != "" && config.ConfigClassSrc != fileVersion)
                {
                    if (MessageBox.Show("Du brugte " + config.ConfigClassSrc + " data til at konfigurere klubber og klasser.\nNu læser du resultater i " + fileVersion + " format.\nDet kan give problemer at bruge forskelligt format. Ønsker du at fortsætte?", "Advarsel", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }

                _currentResultDirectory = Path.GetDirectoryName(openFile.FileName);
                _loadResultatFil(openFile.FileName);

                // juster UI

                txtCSVFile.Text = _txtResultatFil;
                btnBeregn.Enabled = true;

                _watchFile();
                _beregn();
            }
        }
        #endregion

        #region print og font og export
        private void btnPrint_Click(object sender, EventArgs e)
        {
            _doPrint(false);
        }

        List<PageText> gamleSider = null;
        List<PageText> nyeSider = null;

        string HeaderText;
        string TextToPrint;
        List<string> AllTextToPrint = new List<string>();
        int pageNum;
        int grpNum;
        bool firstPage;
        string printDate = string.Empty;

        /// <summary>
        /// print
        /// </summary>
        /// <returns></returns>
        public PrintDocument InitPrint()
        {
            // document skal indeholde en liste af text output, som hver skal skifte side
            nyeSider = new List<PageText>();
            firstPage = true;
            pageNum = 0;
            grpNum = 0;
            this.HeaderText = string.Empty;
            this.AllTextToPrint.Clear();
            if (mitDivisionsResultat != null && config.PrintDiviResultat)
            {
                this.AllTextToPrint.Add(mitDivisionsResultat.PrintResultText(mitstaevne));
            }
            this.AllTextToPrint.Add(mitstaevne.Printmatcher());
            this.AllTextToPrint.AddRange(mitstaevne.LavTXTafsnit(config));
            if (!config.SideSkift)
            {
                // ikke sideskift, dvs alle printes som første side
                string alltext = string.Empty;
                foreach (string s in AllTextToPrint)
                {
                    if (s != string.Empty)
                    {
                        alltext += System.Environment.NewLine;
                    }

                    alltext += s;
                }

                AllTextToPrint.Clear();
                AllTextToPrint.Add(alltext);
            }

            // initialiser
            TextToPrint = AllTextToPrint[0];
            printDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            nyeSider.Clear();

            PrintDocument PD = new PrintDocument();
            ////PD.PrinterSettings = config.printerSettings;
            PD.DefaultPageSettings = config.pageSettings;
            PD.OriginAtMargins = true;
            PD.PrintPage += PrintPage;
            PD.QueryPageSettings += MyPrintQueryPageSettingsEvent;
            return PD;
        }

        private void MyPrintQueryPageSettingsEvent(object sender, QueryPageSettingsEventArgs e)
        {
            if (grpNum==0 && !config.pageSettings.Landscape)
            {
                // switch to landscape for too wide divisionsresultat
                e.PageSettings.Landscape = true;
            }
            else
            {
                e.PageSettings.Landscape = config.pageSettings.Landscape;
            }
        }

        /// <summary>
        ///  print en side
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PrintPage(object sender, PrintPageEventArgs e)
        {
            ////bool siderAtPrinte = true;
            ////if (firstPage)
            ////{
            ////    siderAtPrinte = _lavSider(e);
            ////    pageNum = 0;
            ////    firstPage = false;
            ////}

            ////if (siderAtPrinte)
            ////{
            ////    // find og print næste side
            ////    Font Font = config.font;
            ////    Rectangle R = new Rectangle(Point.Empty, e.MarginBounds.Size);
            ////    StringFormat SF = StringFormat.GenericTypographic;

            ////    if (pageNum < nyeSider.Count)
            ////    {
            ////        e.Graphics.DrawString(nyeSider[pageNum].AllText, Font, Brushes.Black, R, SF);
            ////    }
            ////    pageNum++;
            ////    e.HasMorePages = false;
            ////    while (pageNum < nyeSider.Count && !nyeSider[pageNum].NySide)
            ////    {
            ////        pageNum++;
            ////    }
            ////    if (pageNum < nyeSider.Count)
            ////    {
            ////        e.HasMorePages = true;
            ////    }
            ////    else
            ////    {
            ////        firstPage = true;
            ////    }
            ////}
            ////else
            ////{
            ////    e.HasMorePages = false;
            ////    e.Cancel = true;
            ////}

            // første side/gruppe
            if (pageNum == nyeSider.Count && grpNum < AllTextToPrint.Count)
            {
                _lavSiderForGruppe(e);
            }

            // hvis vi ikke har printet alle sider, som vi har lavet for en gruppe
            if (pageNum <= nyeSider.Count)
            {
                // find og print næste side
                Font Font = config.font;
                Rectangle R = new Rectangle(Point.Empty, e.MarginBounds.Size);
                StringFormat SF = StringFormat.GenericTypographic;
                e.Graphics.DrawString(nyeSider[pageNum].AllText, Font, Brushes.Black, R, SF);
            }
            pageNum++;

            // skip uændrede sider
            while (pageNum < nyeSider.Count && !nyeSider[pageNum].NySide)
            {
                pageNum++;
            }

            // stadig sider som mangler at blive printet fra denne grupppe
            if (pageNum < nyeSider.Count)
            {
                e.HasMorePages = true;
            }
            else
            {
                grpNum++;
                e.HasMorePages = grpNum < AllTextToPrint.Count;
            }
        }

        private void _lavSiderForGruppe(PrintPageEventArgs e)
        {
            Font Font = config.font;
            StringFormat SF = StringFormat.GenericTypographic;
            Rectangle R = new Rectangle(Point.Empty, e.MarginBounds.Size);

            // calculate the width
            int Chars = 0;
            int Lines = 0;
            string t = string.Empty;
            while (Lines < 2)
            {
                t = t + "-";
                e.Graphics.MeasureString(t, Font, R.Size, SF, out Chars, out Lines);
            }

            // lav en header
            HeaderText = ("Divisionsmatch v" + Application.ProductVersion + " / " + printDate).PadLeft(t.Length - 1) + "\n" + "".PadLeft(t.Length - 1, '-') + "\n\n";
            TextToPrint = AllTextToPrint[grpNum];

            bool morePages = true;
            int sideNr = pageNum;
            while (morePages)
            {
                sideNr++;
                string sideNum = "side " + sideNr;

                string h = sideNum + HeaderText.Substring(sideNum.Length);
                TextToPrint = h + TextToPrint;

                e.Graphics.MeasureString(TextToPrint, Font, R.Size, SF, out Chars, out Lines);

                PageText side = new PageText();
                side.Header = h;
                side.Body = TextToPrint.Substring(h.Length, Chars - h.Length);
                nyeSider.Add(side);

                //e.Graphics.DrawString(TextToPrint, Font, Brushes.Black, R, SF);

                TextToPrint = TextToPrint.Substring(Chars);
                morePages = TextToPrint.Length > 0;
            }
        }

        ////private bool _lavSider(PrintPageEventArgs e)
        ////{
        ////    int Chars = 0;
        ////    int Lines = 0;
        ////    Font Font = config.font;
        ////    StringFormat SF = StringFormat.GenericTypographic;

        ////    bool morePages = true;
        ////    while (morePages)
        ////    {
        ////        if (grpNum == 1 && TextToPrint.Length > 0 &&config.PrintDiviResultat)
        ////        {
        ////            e.PageSettings.Landscape = true;
        ////        }
        ////        else
        ////        {
        ////            e.PageSettings.Landscape = config.pageSettings.Landscape;
        ////        }

        ////        Rectangle R = new Rectangle(Point.Empty, e.MarginBounds.Size);

        ////        // calculate the width
        ////        string t = string.Empty;
        ////        Lines = 0;
        ////        while (Lines < 2)
        ////        {
        ////            t = t + "-";
        ////            e.Graphics.MeasureString(t, Font, R.Size, SF, out Chars, out Lines);
        ////        }

        ////        HeaderText = ("Divisionsmatch v" + Application.ProductVersion + " / " + printDate).PadLeft(t.Length - 1) + "\n" + "".PadLeft(t.Length - 1, '-') + "\n\n";


        ////        pageNum++;
        ////        string sideNum = "side " + pageNum;

        ////        // test if first page is printed
        ////        if (pageNum == 1 || TextToPrint.Length > 0)
        ////        {
        ////            string h = sideNum + HeaderText.Substring(sideNum.Length);
        ////            TextToPrint = h + TextToPrint;

        ////            e.Graphics.MeasureString(TextToPrint, Font, R.Size, SF, out Chars, out Lines);

        ////            PageText side = new PageText();
        ////            side.Header = h;
        ////            side.Body = TextToPrint.Substring(h.Length, Chars - h.Length);
        ////            nyeSider.Add(side);

        ////            //e.Graphics.DrawString(TextToPrint, Font, Brushes.Black, R, SF);

        ////            TextToPrint = TextToPrint.Substring(Chars);
        ////            morePages = TextToPrint.Length > 0 || grpNum < AllTextToPrint.Count();
        ////        }
        ////        else
        ////        {
        ////            if (grpNum < AllTextToPrint.Count())
        ////            {
        ////                TextToPrint = AllTextToPrint[grpNum++];

        ////                string h = sideNum + HeaderText.Substring(sideNum.Length);
        ////                TextToPrint = h + TextToPrint;

        ////                e.Graphics.MeasureString(TextToPrint, Font, R.Size, SF, out Chars, out Lines);
        ////                PageText side = new PageText();
        ////                side.Header = h;
        ////                side.Body = TextToPrint.Substring(h.Length, Chars - h.Length);
        ////                nyeSider.Add(side);

        ////                //e.Graphics.DrawString(TextToPrint, Font, Brushes.Black, R, SF);

        ////                TextToPrint = TextToPrint.Substring(Chars);
        ////                morePages = TextToPrint.Length > 0 || grpNum < AllTextToPrint.Count();
        ////            }
        ////            else
        ////            {
        ////                morePages = false;
        ////            }
        ////        }

        ////        if (morePages == false)
        ////        {
        ////            // gør klar til evt print fra PrintPreview
        ////            pageNum = 0;
        ////            grpNum = 0;
        ////            firstPage = true;
        ////            TextToPrint = AllTextToPrint[0];
        ////        }
        ////    }

        ////    // check om siderne har ændret sig
        ////    bool siderAtPrinte = false;
        ////    for (int i = 0; i < nyeSider.Count; i++)
        ////    {
        ////        if (gamleSider == null || gamleSider.Count == 0 || gamleSider.FirstOrDefault(p => p.Body == nyeSider[i].Body) == null)
        ////        {
        ////            nyeSider[i].NySide = true;
        ////            siderAtPrinte = true;
        ////        }
        ////        else
        ////        {
        ////            nyeSider[i].NySide = false;
        ////        }
        ////    }

        ////    return siderAtPrinte;
        ////}

        private void _doPrint(bool auto)
        {
            try
            {
                if (tabControl1.SelectedTab == tabPage1)
                {
                    // print text versionen
                    _printTxt(auto);
                }
                else
                {
                    // print webpage
                    _printWebPage(auto);
                }
            }
            finally
            {
                btnPrint.BackColor = Control.DefaultBackColor;
                btnPrint.UseVisualStyleBackColor = true;
            }
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //PageSetupDialog settings
            PrintDocument PD = new PrintDocument();
            ////PD.PrinterSettings = config.printerSettings;
            PD.DefaultPageSettings = config.pageSettings;
            pageSetupDialog1.Document = PD;

            if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
            {
                ////config.printerSettings = pageSetupDialog1.PrinterSettings;
                config.pageSettings = pageSetupDialog1.PageSettings;
                isModified = true;

                _makeNewPageSetup(config.pageSettings);
            }
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                PrintPreviewDialog PPDlg = new PrintPreviewDialog();
                PPDlg.Document = InitPrint();
                PPDlg.ShowDialog();
                HeaderText = "";
            }
            else
            {
                // set the web browser page settings
                _setIEPageSetup(ieNewPageSetup);
                // display the preview dialog
                webOutput.ShowPrintPreviewDialog();

                // flag that IE values are to be read
                bGetIESetup = true;
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnPrint_Click(sender, e);
        }

        private void vælgFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.FixedPitchOnly = true;
            fd.ShowColor = false;
            fd.ShowApply = false;
            fd.Font = config.font;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                config.font = fd.Font;
                textBox1.Font = fd.Font;

                textBox3.Font = fd.Font;

                _fillOutput();

                isModified = true;
            }
        }

        private void btnEksport_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                // eksport af HTML
                // webOutput.ShowSaveAsDialog();
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "HTML filer (*.htm)|*.htm";
                saveFile.OverwritePrompt = true;
                saveFile.CheckPathExists = true;
                saveFile.AddExtension = true;
                saveFile.DefaultExt = ".htm";
                saveFile.FileName = Path.GetFileNameWithoutExtension(configFile);
                saveFile.SupportMultiDottedExtensions = true;
                saveFile.Title = "Exporter resultat";
                saveFile.RestoreDirectory = true;
                if (_currentExportDirectory != string.Empty)
                {
                    saveFile.InitialDirectory = _currentExportDirectory;
                }
                if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        _currentExportDirectory = Path.GetDirectoryName(saveFile.FileName);
                        _exportHtml(saveFile.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Resultatet kunnne ikke gemmes \n" + ex.Message + "\n" + ex.StackTrace, "Fejl");
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            else
            {
                // eksport af TXT
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Text filer (*.txt)|*.txt";
                saveFile.OverwritePrompt = true;
                saveFile.CheckPathExists = true;
                saveFile.AddExtension = true;
                saveFile.DefaultExt = ".txt";
                saveFile.FileName = Path.GetFileNameWithoutExtension(configFile);
                saveFile.SupportMultiDottedExtensions = true;
                saveFile.Title = "Exporter resultat";
                if (_currentExportDirectory != string.Empty)
                {
                    saveFile.InitialDirectory = _currentExportDirectory;
                }
                if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        _currentExportDirectory = Path.GetDirectoryName(saveFile.FileName);
                        _exportTxt(saveFile.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Resultatet kunnne ikke gemmes \n" + ex.Message + "\n" + ex.StackTrace, "Fejl");
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }

        #endregion

        #region UI event handlers
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isModified)
            {
                if (MessageBox.Show("Løbskonfigurationen er ændret men ikke gemt. Vil du stadig lukke?\nTryk på " + DialogResult.OK.ToString() + " for at afslutte uden at gemme. Tryk på " + DialogResult.Cancel.ToString() + " for at returnere så du kan gemme.", "Afslut?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }

                // reset page setup settings before closing
                _setIEPageSetup(ieOriginalPageSetup);
            }

            mitstaevne = null;
        }

        private void lukToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void indholdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _showHelp(false);
        }

        private void omToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOm frmOm = new frmOm();
            frmOm.ShowDialog();
        }

        #endregion

        #region internal methods

        internal void _beregn()
        {
            try
            {
                if (!string.IsNullOrEmpty(_txtResultatFil))
                {
                    mitstaevne.Beregnpoint(config, _txtResultatFil);

                    _fillOutput();

                    _autoPrint();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ifm beregning: " + ex.Message, "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // mark print is ready
                btnPrint.BackColor = Color.LightGreen;
            }
        }

        #endregion

        #region private methods

        private void setTitle()
        {
            this.Text = _apptitle + " - " + (configFile != string.Empty ? Path.GetFileName(configFile) : "(uden titel)");
            if (isModified == true)
            {
                this.Text += " *";
            }
        }

        private void _resetBeregnOgPrint()
        {
            // stop med at holde øje med resultatfil
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = true;
                watcher.Dispose();
                watcher = null;
            }

            textBox1.Clear();
            textBox3.Clear();
            txtCSVFile.Text = string.Empty;
            _txtResultatFil = string.Empty;
            webOutput.Navigate("about:blank");
            btnBeregn.Enabled = false;
            btnPrint.Enabled = false;
            btnEksport.Enabled = false;
            printPreviewToolStripMenuItem.Enabled = false;
            printToolStripMenuItem.Enabled = false;
            printMainToolStripMenuItem.Enabled = true;
            btnPrint.BackColor = Control.DefaultBackColor;
            btnPrint.UseVisualStyleBackColor = true;

            if (gamleSider != null)
            {
                gamleSider.Clear();
            }

            if (gamleHTMLsections != null)
            {
                gamleHTMLsections.Clear();
            }

        }

        private void _setPrintReady()
        {
            textBox1.Enabled = true;
            textBox3.Enabled = true;
            btnPrint.Enabled = true;
            btnEksport.Enabled = true;
            printPreviewToolStripMenuItem.Enabled = true;
            printToolStripMenuItem.Enabled = true;
            printMainToolStripMenuItem.Enabled = true;
            //// vælgFontToolStripMenuItem.Enabled = true;
            //// vælgFontToolStripMenuItem.Enabled = true;
            //// vælgPrinterToolStripMenuItem.Enabled = true;
        }

        private void _fillOutput()
        {
            if (mitstaevne.Loebere.Count > 0)
            {
                if (tabControl1.SelectedTab == tabPage1)
                {
                    int pos = textBox1.SelectionStart;
                    string text = DateTime.Now.ToString("HH:mm:ss") + System.Environment.NewLine;
                    if (mitDivisionsResultat!=null)
                    {
                        text += mitDivisionsResultat.PrintResultText(mitstaevne) + System.Environment.NewLine;
                    }
                    text += mitstaevne.Printmatcher();
                    textBox1.Text = text;
                    textBox1.Font = config.font.FontValue;
                    textBox1.SelectionStart = pos;
                    textBox1.ScrollToCaret();

                    pos = textBox3.SelectionStart;
                    textBox3.Text = string.Concat(mitstaevne.LavTXTafsnit(config).ToArray());
                    textBox3.Font = config.font.FontValue;
                    textBox3.SelectionStart = pos;
                    textBox3.ScrollToCaret();
                }
                else
                {

                    webOutput.Tag = new List<string>();
                    if (mitDivisionsResultat != null)
                    {
                        (webOutput.Tag as List<string>).Add(mitDivisionsResultat.PrintResultHtml(mitstaevne));
                    }
                    (webOutput.Tag as List<string>).AddRange(mitstaevne.LavHTMLafsnit(config));
                    webOutput.Navigate(mitstaevne.LavHTML(webOutput.Tag as List<string>));
                    Application.DoEvents();
                }

                _setPrintReady();
            }
            else
            {
                textBox1.Clear();
                textBox3.Clear();
                webOutput.Tag = null;
                webOutput.DocumentText = string.Empty;
            }
        }

        private void _watchFile()
        {
            // Create a new FileSystemWatcher and set its properties.
            if (watcher == null)
            {
                watcher = new FileSystemWatcher();
                watcher.Path = Path.GetDirectoryName(txtCSVFile.Text);
                watcher.Filter = Path.GetFileName(txtCSVFile.Text);

                /* Watch for changes in LastWrite times*/
                watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Size;

                // Add event handlers.
                watcher.Created += new FileSystemEventHandler(_csvFileOnChanged);
                watcher.Changed += new FileSystemEventHandler(_csvFileOnChanged);

                // link to the form
                watcher.SynchronizingObject = this;

                // Begin watching.
                watcher.EnableRaisingEvents = true;
            }
            else
            {
                // update the file to watch for
                watcher.EnableRaisingEvents = false;
                watcher.Path = Path.GetDirectoryName(txtCSVFile.Text);
                watcher.Filter = Path.GetFileName(txtCSVFile.Text);
                watcher.EnableRaisingEvents = true;
            }
        }

        // Define the event handlers. 
        private static DateTime lastRead = DateTime.Now;

        private static void _csvFileOnChanged(object source, FileSystemEventArgs e)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(e.FullPath);
            if (lastWriteTime != lastRead)
            {
                frmBeregnPop pop = new frmBeregnPop();
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    (source as FileSystemWatcher).EnableRaisingEvents = false;

                    frmDivi frmDivi = ((source as FileSystemWatcher).SynchronizingObject as frmDivi);

                    pop.StartPosition = FormStartPosition.Manual;
                    pop.Location = new Point(frmDivi.Location.X + (frmDivi.Width - pop.Width) / 2, frmDivi.Location.Y + (frmDivi.Height - pop.Height) / 2);
                    pop.ShowInTaskbar = false;
                    pop.Show();
                    pop.Update();
                    Thread.Sleep(1000);
                    frmDivi._beregn();
                }
                finally
                {
                    pop.Close();
                    (source as FileSystemWatcher).EnableRaisingEvents = true;
                    lastRead = lastWriteTime;
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void _setIEPageSetup(Hashtable values)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\PageSetup", true))
            {
                if (key == null)
                    return;

                foreach (DictionaryEntry item in values)
                {
                    string value = (string)key.GetValue(item.Key.ToString());

                    if (value != null && (item.Value == null || value != item.Value.ToString()))
                    {
                        key.SetValue(item.Key.ToString(), item.Value);
                    }
                }
            }
        }

        private void _getIEPageSetup(ref Hashtable values)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\PageSetup", true))
            {
                if (key == null)
                    return;

                values["margin_left"] = (string)key.GetValue("margin_left");
                values["margin_right"] = (string)key.GetValue("margin_right");
                values["margin_top"] = (string)key.GetValue("margin_top");
                values["margin_bottom"] = (string)key.GetValue("margin_bottom");
                values["header"] = (string)key.GetValue("header");
                values["footer"] = (string)key.GetValue("footer");
                values["orientation"] = (string)key.GetValue("orientation");
            }
        }

        private void _makeNewPageSetup(PageSettings pageSettings)
        {
            ieNewPageSetup["margin_left"] = (pageSettings.Margins.Left / 100.0).ToString();
            ieNewPageSetup["margin_right"] = (pageSettings.Margins.Right / 100.0).ToString();
            ieNewPageSetup["margin_top"] = (pageSettings.Margins.Top / 100.0).ToString();
            ieNewPageSetup["margin_bottom"] = (pageSettings.Margins.Bottom / 100.0).ToString();
            ieNewPageSetup["orientation"] = pageSettings.Landscape ? "2" : "1";
        }

        private void _getNewPageSetup(ref Config config)
        {
            config.pageSettings.Margins.Left = Convert.ToInt32(Convert.ToSingle(ieNewPageSetup["margin_left"]) * 100.00);
            config.pageSettings.Margins.Right = Convert.ToInt32(Convert.ToSingle(ieNewPageSetup["margin_right"]) * 100);
            config.pageSettings.Margins.Top = Convert.ToInt32(Convert.ToSingle(ieNewPageSetup["margin_top"]) * 100);
            config.pageSettings.Margins.Bottom = Convert.ToInt32(Convert.ToSingle(ieNewPageSetup["margin_bottom"]) * 100);
            config.pageSettings.Landscape = "2".Equals(ieNewPageSetup["orientation"]);
        }

        private void _showHelp(bool batch)
        {
            string helpFilePath = Path.Combine(Application.StartupPath, "Divisionsmatchberegning.chm");
            if (File.Exists(helpFilePath))
            {
                if (batch)
                {
                    System.Diagnostics.Process.Start(helpFilePath);
                }
                else
                {
                    Help.ShowHelp(this, "file://" + helpFilePath);
                }
            }
            else
            {
                MessageBox.Show("Hjælpefilen mangler");
            }
        }

        private Config _loadDiviFil(string diviFil)
        {
            Config config = Config.LoadDivi(diviFil);
            DivisionsResultat.DivisionsResultat divisionsResultat = Util.OpenDivisionsResultat(config.DivisionsResultatFil);
            DivisionsResultat.DivisionsMatchResultat denneMatch = divisionsResultat.DivisionsMatchResultater.OrderBy(item => item.Runde).Last();
            divisionsResultat.DivisionsMatchResultater.Remove(denneMatch);

            mitstaevne = new Staevne(Application.ProductVersion);
            mitstaevne.Config = config;
            if (divisionsResultat != null)
            {
                string msg = string.Empty;
                if (!divisionsResultat.CheckStaevne(mitstaevne, out msg))
                {
                    throw new Exception("Konfigurationen stemmer ikke med DivisionsResultatet i " + config.DivisionsResultatFil + "\n" + msg + "\nLav en ny konfigurering");
                }
            }
            configFile = diviFil;
            mitDivisionsResultat = divisionsResultat;

            isModified = false;
            return config;
        }

        private void _loadStillingsFil(string stillingsFil)
        {
            DivisionsResultat.DivisionsResultat divisionsResultat = null;
            if (string.IsNullOrEmpty(stillingsFil))
            {
                divisionsResultat = new DivisionsResultat.DivisionsResultat();
                divisionsResultat.År = mitstaevne.Dato.Year;
                divisionsResultat.Division = mitstaevne.Config.Division;
                divisionsResultat.Kreds = mitstaevne.Config.Kreds;
            }
            else
            {
                divisionsResultat = Util.OpenDivisionsResultat(stillingsFil);
            }
            if (divisionsResultat != null)
            {
                string msg = string.Empty;
                bool ok = divisionsResultat.CheckStaevne(mitstaevne, out msg);
                if (!ok)
                {
                    mitDivisionsResultat = null;
                    Console.WriteLine("Indlæsning af divisionsresultat fejlede. Stilling blive ikke beregnet");
                    Console.WriteLine(msg);
                }

                // sæt resultatet i stævnet
                mitDivisionsResultat = divisionsResultat;
            }
            else
            {
                Console.WriteLine("Indlæsning af divisionsresultat fejlede. Stilling blive ikke beregnet");
            }
        }

        private void _loadResultatFil(string txtFil)
        {
            _txtResultatFil = txtFil;
        }

        private void _printTxt(bool auto)
        {
            PrintDialog pDlg = new PrintDialog();
            pDlg.Document = InitPrint();
            pDlg.ShowNetwork = true;
            pDlg.AllowPrintToFile = false;
            pDlg.AllowSelection = false;
            pDlg.AllowSomePages = false;
            pDlg.AllowCurrentPage = false;
            ////pDlg.PrinterSettings = config.printerSettings;
            if (auto || pDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pDlg.Document.Print();

                    HeaderText = "";
                    gamleSider = nyeSider;
                }
                catch (Exception e)
                {
                    throw new Exception("Fejl ifm print: " + e.Message, e);
                }
            }
        }

        private void _autoPrint()
        {
            if (config.AutoPrint)
            {
                _doPrint(true);
            }

            if (config.AutoExport)
            {
                _doExport();
            }
        }

        private void _doExport()
        {
            if (File.Exists(configFile))
            {
                if (tabControl1.SelectedTab == tabPage1)
                {
                    // print text versionen
                    _exportTxt(Path.ChangeExtension(configFile, ".txt"));
                }
                else
                {
                    // print webpage
                    _exportHtml(Path.ChangeExtension(configFile, ".htm"));
                }
            }
            else
            {
                frmBeregnPop pop = new frmBeregnPop("Automatisk eksport er først aktiv når configurationen er gemt");
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    pop.StartPosition = FormStartPosition.Manual;
                    pop.Location = new Point(this.Location.X + (this.Width - pop.Width) / 2, this.Location.Y + (this.Height - pop.Height) / 2);
                    pop.ShowInTaskbar = false;
                    pop.Show();
                    pop.Update();
                    Thread.Sleep(1000);
                }
                finally
                {
                    pop.Close();
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void _exportHtml(string exportFil)
        {
            List<string> sections = new List<string>();
            if (mitDivisionsResultat != null)
            {
                sections.Add(mitDivisionsResultat.PrintResultHtml(mitstaevne));
            }
            sections.AddRange(mitstaevne.LavHTMLafsnit(config));
            string path = new Uri(mitstaevne.LavHTML(sections)).LocalPath;
            File.Copy(path, exportFil, true);

            // also copy css - if not configfured to be there
            string cssFile = mitstaevne.GetHTMLStyle();
            string sourceCssFile = Path.Combine(Path.GetDirectoryName(path), Path.GetFileName(cssFile));
            string targetCssFile = Path.Combine(Path.GetDirectoryName(exportFil), Path.GetFileName(cssFile));

            // hvis der er specificeret en CSS file i konfigurationen så kopierer vi den over såfremt den ikke hedder divi.css og ligger 
            // divi filen (da det så er samme fil)
            if (string.IsNullOrEmpty(mitstaevne.Config.CssFile) || string.Compare(mitstaevne.Config.CssFileFullPath, targetCssFile, true) != 0)
            {
                if (!File.Exists(targetCssFile))
                    File.Copy(sourceCssFile, targetCssFile, true);
            }
        }

        private void _exportTxt(string exportFil)
        {
            string allText = string.Empty;
            if (mitDivisionsResultat != null)
            {
                allText += mitDivisionsResultat.PrintResultText(mitstaevne) + Environment.NewLine;
            }
            allText += mitstaevne.Printmatcher() + Environment.NewLine + string.Concat(mitstaevne.LavTXTafsnit(config).ToArray());
            File.WriteAllText(exportFil, allText, Encoding.Default);
        }

        List<string> gamleHTMLsections = new List<string>();

        private void _printWebPage(bool auto)
        {
            _setIEPageSetup(ieNewPageSetup);
            string oldWebDoc = File.ReadAllText(webOutput.Url.AbsolutePath, Encoding.Default);
            List<string> newHTMLsections = new List<string>();

            if (config.PrintNye)
            {
                // remake document to only changed sections
                foreach (string s in ((List<string>)webOutput.Tag))
                {
                    if (!gamleHTMLsections.Contains(s))
                    {
                        newHTMLsections.Add(s);
                    }
                }
            }
            else
            {
                if (webOutput.Tag == null)
                {
                    newHTMLsections = new List<string>();
                    newHTMLsections.Add(mitDivisionsResultat.PrintResultHtml(mitstaevne));
                    newHTMLsections.AddRange(mitstaevne.LavHTMLafsnit(config));
                }
                else
                {
                    newHTMLsections = (List<string>)webOutput.Tag;
                }
            }

            if (newHTMLsections.Count > 0)
            {
                string docPath = mitstaevne.LavHTML(newHTMLsections);
                if (auto)
                {
                    PrintHtmlPage(docPath);
                }
                else
                {
                    // load an print new content
                    webOutput.Navigate(docPath);
                    webOutput.ShowPrintDialog();

                    // reset
                    File.WriteAllText(new Uri(docPath).LocalPath, oldWebDoc, Encoding.Default);
                    webOutput.Navigate(docPath);
                    Application.DoEvents();
                }

                gamleHTMLsections = (List<string>)webOutput.Tag;
            }
            else
            {
                if (!auto)
                {
                    MessageBox.Show("Ingen nye sider at printe");
                }
            }
        }

        private void PrintHtmlPage(string doc)
        {
            // Create a WebBrowser instance. 
            WebBrowser webBrowserForPrinting = new WebBrowser();

            // Add an event handler that prints the document after it loads.
            webBrowserForPrinting.DocumentCompleted +=
                new WebBrowserDocumentCompletedEventHandler(PrintDocument);

            // load the document
            // open a document
            webBrowserForPrinting.Navigate(doc);
        }

        private void PrintDocument(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Print the document now that it is fully loaded.
            ((WebBrowser)sender).Print();

            // Dispose the WebBrowser now that the task is complete. 
            ((WebBrowser)sender).Dispose();
        }

        // sample commandline arguments: -d "3-4 Division Op-Ned.divi" -c resultat.csv -p TXT
        private bool _doBatch(string[] args)
        {
            CommandLineParser.CommandLineParser parser = new CommandLineParser.CommandLineParser();

            try
            {
                FileArgument diviFil = new FileArgument('d', "Divi", "Divi fil");
                FileArgument resultatFil = new FileArgument('c', "C", "resultat fil");
                ////FileArgument stillingsFil = new FileArgument('s', "Stilling", "stillings xml fil");
                ValueArgument<string> outFil = new ValueArgument<string>('o', "Output", "output stillings fil");
                ValueArgument<string> exportFil = new ValueArgument<string>('e', "Export", "Export fil");
                ValueArgument<string> format = new ValueArgument<string>('f', "Format", "print/eksport TXT eller WWW");
                SwitchArgument print = new SwitchArgument('p', "Print", false);
                SwitchArgument help = new SwitchArgument('h', "Hjælp", false);
                SwitchArgument nologo = new SwitchArgument('n', "nologo", false);
                SwitchArgument debug = new SwitchArgument('V', "Debug", false);

                parser.Arguments.Add(diviFil);
                parser.Arguments.Add(resultatFil);
                ////parser.Arguments.Add(stillingsFil);
                parser.Arguments.Add(outFil);
                parser.Arguments.Add(exportFil);
                parser.Arguments.Add(format);
                parser.Arguments.Add(print);
                parser.Arguments.Add(help);
                parser.Arguments.Add(debug);

                parser.ParseCommandLine(args);
                //// parser.ShowParsedArguments();

                if (!nologo.Parsed)
                {
                    Console.WriteLine("Divisionsmatch -  Copyright (C) 2013 Anders Klinting");
                    Console.WriteLine("");
                    Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY; for details see the About page");
                    Console.WriteLine("This is free software, and you are welcome to redistribute it");
                    Console.WriteLine("under certain conditions");
                    Console.WriteLine("");
                }

                if (debug.Value)
                    System.Diagnostics.Debugger.Launch();

                if (help.Parsed)
                {
                    _showHelp(true);
                }
                else
                {
                    if (diviFil.Parsed)
                    {
                        Console.WriteLine("Loader divi-fil " + diviFil.Value.FullName);
                        config = _loadDiviFil(diviFil.Value.FullName);

                        ////if (stillingsFil.Parsed)
                        ////{
                        ////    Console.WriteLine("Loader divisionsstilling-fil " + diviFil.Value.FullName);
                        ////    _loadStillingsFil(stillingsFil.Value.FullName);
                        ////}

                        if (resultatFil.Parsed)
                        {
                            Console.WriteLine("Loader resultat-fil " + resultatFil.Value.FullName);
                            _loadResultatFil(resultatFil.Value.FullName);

                            Console.WriteLine("Beregner resultat");
                            mitstaevne.Beregnpoint(config, _txtResultatFil);

                            if (print.Parsed)
                            {
                                _fillOutput();
                                if (format.Value.ToUpperInvariant() == "TXT")
                                {
                                    Console.WriteLine("printer TXT");
                                    _printTxt(true);
                                }
                                else if (format.Value.ToUpperInvariant() == "WWW")
                                {
                                    Console.WriteLine("priner HTML");
                                    _printWebPage(true);
                                }
                                else
                                {
                                    throw new Exception("hust at skrive '-f TXT' eller '-f WWW' for at printe i batch");
                                }
                            }

                            if (outFil.Parsed)
                            {
                                ////if (stillingsFil.Parsed)
                                ////{
                                Console.WriteLine("gemmer ny divisionsstilling");
                                _gemStilling(outFil.Value);
                                ////}
                                ////else
                                ////{
                                ////    Console.WriteLine("ny divisionsstilling kan kke gemmes uden input stillingsfil");
                                ////}
                            }

                            if (exportFil.Parsed)
                            {
                                if (format.Value.ToUpperInvariant() == "TXT")
                                {
                                    Console.WriteLine("export til TXT");
                                    _exportTxt(exportFil.Value);
                                }
                                else if (format.Value.ToUpperInvariant() == "WWW")
                                {
                                    Console.WriteLine("export til HTML");
                                    _exportHtml(exportFil.Value);
                                }
                                else
                                {
                                    throw new Exception("hust at skrive '-f TXT' eller '-f WWW' for at exportere i batch");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Noget gik galt: " + Environment.NewLine + e.Message, "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }

        private void _gemStilling(string stillingsFil)
        {
            if (mitDivisionsResultat != null)
            {
                Console.WriteLine("gemmer ny stilling");
                Util.SaveDivisionsResultat(mitDivisionsResultat, mitstaevne, stillingsFil);
            }
            else
            {
                Console.WriteLine("kan ikke gemme ny stilling");
            }
        }

        #endregion

        private void setupPrinterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSetup sp = new frmSetup();
            sp.config = config;

            if (sp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                config = sp.config;

                isModified = true;

                textBox1.Font = config.font;
                textBox3.Font = config.font;

                mitstaevne.Config = config;

                if (!config.PrintNye)
                {
                    if (gamleSider != null)
                    {
                        gamleSider.Clear();
                    }

                    if (gamleHTMLsections != null)
                    {
                        gamleHTMLsections.Clear();
                    }
                }

                _fillOutput();

                _makeNewPageSetup(config.pageSettings);
            }
        }

        private void checkForOpdateringerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(autoUpdateSiteURL);
        }

        private void frmDivi_Activated(object sender, EventArgs e)
        {
            if (bGetIESetup)
            {
                // grab IE page setup settings
                _getIEPageSetup(ref ieNewPageSetup);
                _getNewPageSetup(ref config);
                bGetIESetup = false;
            }

            if (bIEprint)
            {
                ////myPrinters.SetDefaultPrinter(oldPrinterName);

                bIEprint = false;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _fillOutput();
        }

        private void rødToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.MistyRose;
        }

        private void grønToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.LightGreen;
        }

        private void gulToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.LightGoldenrodYellow;
        }

        private void blåToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.LightBlue;
        }

        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromName("Control");
        }

        private void startlisteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStartListe startListe = new frmStartListe(config);
            startListe.ShowDialog();
        }

        private void recentsToolStripMenuItem_ItemClick(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                fileName = (string)((ToolStripDropDownItem)sender).Tag;
                if (_åbnDivi(fileName))
                {
                    recentsToolStripMenuItem.AddRecentItem(fileName);

                    MessageBox.Show("Løbskonfigurationen er indlæst. Vælg en resultat-fil for at starte beregning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Konfigurationen kunnne ikke åbnes \n" + ex.Message + "\n" + ex.StackTrace, "Fejl");
                recentsToolStripMenuItem.RemoveRecentItem(fileName);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void gemDivisionsresultatToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmGemDivisionsResultat frmGemDivisionsResultat = new frmGemDivisionsResultat();
            frmGemDivisionsResultat.Staevne = mitstaevne;
            frmGemDivisionsResultat.DivisionsResultat = mitDivisionsResultat;
            frmGemDivisionsResultat.ShowDialog();
        }
    }

    internal static class myPrinters
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Name);

        public static string GetDefaultPrinter()
        {
            return new PrinterSettings().PrinterName;
        }
    }
}