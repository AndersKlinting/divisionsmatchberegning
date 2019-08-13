using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Divisionsmatch
{
    /// <summary>
    /// dialog til at lave en startliste
    /// </summary>
    public partial class frmStartListe : Form
    {
        private IList<Loeber> alleloebere = null;
        private Config _config = null;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="config">stævnets config</param>
        public frmStartListe(Config config)
        {
            InitializeComponent();
            _config = config;
            dateTimePicker1.Value = DateTime.Parse(_config.StartTid);
            radioBane.Enabled = config.baner.Count > 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnLavListe_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.CheckPathExists = true;
            saveFile.AddExtension = true;
            saveFile.SupportMultiDottedExtensions = true;
            saveFile.Title = "Gem Start Liste";

            if (radioTxt.Checked)
            {
                saveFile.Filter = "TXT startliste (*.txt)|*.txt";
                saveFile.DefaultExt = ".txt";
            }
            else
            {
                saveFile.Filter = "HTML startliste (*.htm)|*.htm";
                saveFile.DefaultExt = ".htm";
            }

            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // genlæs hvis tiden er ændret
                if (_config.StartTid != dateTimePicker1.Text)
                {
                    _config.StartTid = dateTimePicker1.Text;
                    _LoadRunners();                    
                }

                string listeText = string.Empty;

                if (radioGruppe.Checked)
                {
                    listeText = Util.GruppeStartListe(radioTxt.Checked, radioAlle.Checked, alleloebere, _config);
                }
                else
                {
                    listeText = Util.BaneStartListe(radioTxt.Checked, radioAlle.Checked, alleloebere, _config);
                }

                File.WriteAllText(saveFile.FileName, listeText, Encoding.Default);

                // open the viewer
                Process.Start(saveFile.FileName);
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "IOF XML startliste, resultatliste (*.xml)|*.xml|OE2003/OE2010 liste (*.csv)|*.csv";
            openFile.CheckPathExists = true;
            openFile.AddExtension = true;
            openFile.DefaultExt = ".xml";
            openFile.SupportMultiDottedExtensions = true;
            openFile.Title = "Åbn startliste";
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtXMLFile.Text = openFile.FileName;
                
                _LoadRunners();

                btnLavListe.Enabled = (alleloebere != null && alleloebere.Count > 0);
            }
        }

        private void _LoadRunners()
        {
            bool isTxt = false;
            bool isEntryXml = false;
            bool isStartXml = false;
            bool isResultXml = false;

            List<Klub> klubber = new List<Klub>();
            alleloebere = null;

            if (txtXMLFile.Text == string.Empty)
            {
                return;
            }

            try
            {
                string fileVersion = Util.CheckFileVersion(txtXMLFile.Text, out isEntryXml, out isStartXml, out isResultXml, out isTxt);

                if (fileVersion == "csv")
                {
                    alleloebere = Util.ReadRunnersFromStartCsv(txtXMLFile.Text, _config, klubber);
                }
                else
                {
                    // load fra XML
                    if (fileVersion == "xml3")
                    {
                        alleloebere = Util.ReadRunnersFromStartlistXML3(txtXMLFile.Text, _config, klubber);
                    }
                    else
                    {
                        alleloebere = Util.ReadRunnersFromStartlistXML2(txtXMLFile.Text, _config, klubber);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Der skete en fejl ved indlæsning: " + e.Message);
                txtXMLFile.Text = string.Empty;
            }
        }

    }
}
