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
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Divisionsmatch
{
    /// <summary>
    /// dialog til print setup
    /// </summary>
    public partial class frmSetup : Form
    {
        /// <summary>
        /// egenskab med senete brugte printer navn
        /// </summary>
        public string printerName = string.Empty;

        private Config _config = new Config(true);
        private SerializableFont _font = null;
        private PrintDocument _pd = new PrintDocument();

        /// <summary>
        /// constructor
        /// </summary>
        public frmSetup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// reference til nærværende konfiguration
        /// </summary>
        public Config config
        {
            get
            {
                return _config;
            }

            set
            {
                _config = value;
            }                
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // page
            ////config.printerSettings = _pd.PrinterSettings;
            config.pageSettings = _pd.DefaultPageSettings;
            
            // set the seelcted printer
            ////config.printerSettings.PrinterName = myPrinters.GetDefaultPrinter();

            // set sidskfift
            config.SideSkift = checkBox1.Checked;

            // font
            config.font = _font;

            // set print baner
            config.PrintBaner = checkBox2.Checked;

            // set print alle
            config.PrintAlle = checkBox3.Checked;

            // set print kun nye sider
            config.PrintNye = checkBox4.Checked;

            // set automatisk print
            config.AutoPrint = checkBox5.Checked;

            // set automatisk print
            config.AutoExport = chkAutoExport.Checked;

            // set print alle grupper
            config.PrintAlleGrupper = checkBox6.Checked;

            // set print divi resultat
            config.PrintDiviResultat = checkBox7.Checked;
            config.InklTidligere = checkBox8.Checked;

            // set CssFile
            config.CssFile = txtHtmlCss.Text;

            // set CssFile
            config.Layout = cmbLayout.Text;
        }

        private void Setup_Load(object sender, EventArgs e)
        {
            // load the printer settings
            ////printerName = config.printerSettings.PrinterName;

            ////foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            ////{
            ////    cmbPrinter.Items.Add(strPrinter);
            ////    if (strPrinter == printerName)
            ////    {
            ////        cmbPrinter.SelectedIndex = cmbPrinter.Items.IndexOf(strPrinter);
            ////    }
            ////}

            // show defaut printer
            lblPrinter.Text = myPrinters.GetDefaultPrinter();
            
            // set checkbox
            checkBox1.Checked = config.SideSkift;

            checkBox2.Checked = config.PrintBaner && config.baner.Count > 0;
            checkBox2.Enabled = config.baner.Count > 0;

            checkBox3.Checked = config.PrintAlle;
            checkBox4.Checked = config.PrintNye;
            checkBox5.Checked = config.AutoPrint;
            checkBox6.Checked = config.PrintAlleGrupper;
            chkAutoExport.Checked = config.AutoExport;

            checkBox7.Checked = config.PrintDiviResultat;
            checkBox8.Checked = config.InklTidligere;
            checkBox8.Enabled = checkBox7.Checked;

            // font
            _font = config.font;
            txtFont.Text = config.font.FontValue.ToString();
            txtFont.Font = config.font.FontValue;

            // page
            _pd.DefaultPageSettings = config.pageSettings;
            ////_pd.PrinterSettings = config.printerSettings;

            txtHtmlCss.Text = config.CssFile;
            cmbLayout.SelectedIndex = cmbLayout.Items.IndexOf(config.Layout);
            chkUseCss.Checked = config.CssFile != string.Empty;
            btnCssFile.Enabled = config.CssFile != string.Empty;
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.FixedPitchOnly = true;
            fd.ShowColor = false;
            fd.ShowApply = false;
            fd.Font = config.font;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _font = fd.Font;
                txtFont.Text = _font.FontValue.ToString();
                txtFont.Font = _font.FontValue;
            }
        }

        private void bntPage_Click(object sender, EventArgs e)
        {
            PageSetupDialog pageSetupDialog1 = new PageSetupDialog();
            pageSetupDialog1.EnableMetric = true;
            pageSetupDialog1.Document = _pd;

            if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
            {
                ////_pd.PrinterSettings = pageSetupDialog1.PrinterSettings;
                _pd.DefaultPageSettings = pageSetupDialog1.PageSettings;
            }
        }

        private void chkUseCss_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkUseCss.Checked)
            {
                txtHtmlCss.Text = string.Empty;
                btnCssFile.Enabled = false;
            }
            else
            {
                btnCssFile.Enabled = true;
            }
        }

        private void btnCssFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Stylesheet (*.css)| *.css";
            openFile.CheckPathExists = true;
            openFile.AddExtension = true;
            openFile.DefaultExt = ".css";
            openFile.SupportMultiDottedExtensions = true;
            openFile.Title = "Åbn stylesheet";
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtHtmlCss.Text = openFile.FileName;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            checkBox8.Enabled = checkBox7.Checked;
        }
    }
}
