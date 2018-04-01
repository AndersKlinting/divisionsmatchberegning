using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Divisionsmatch
{
    /// <summary>
    /// dialog klasse for at gemme divisionsresultat
    /// </summary>
    public partial class frmGemDivisionsResultat : Form
    {
        /// <summary>
        /// constructor
        /// </summary>
        public frmGemDivisionsResultat()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Divisionsresultat
        /// </summary>
        public DivisionsResultat.DivisionsResultat DivisionsResultat { get; set; }

        /// <summary>
        /// staevne
        /// </summary>
        public Staevne Staevne { get; set; }

        private void frmDivisionsResultat_Load(object sender, EventArgs e)
        {
            if (DivisionsResultat != null)
            {
                this.textBoxÅr.Text = DivisionsResultat.År.ToString();
                this.textBoxDivision.Text = DivisionsResultat.Division.ToString();
                this.textBoxKreds.Text = DivisionsResultat.Kreds.ToString();
                if (DivisionsResultat.DivisionsMatchResultater != null)
                {
                    foreach (var m in DivisionsResultat.DivisionsMatchResultater)
                    {
                        this.listBoxMatcher.Items.Add(string.Format("{0} - {1} - {2}", m.Runde, m.Dato, m.Skov));
                    }
                }
            }

            if (Staevne != null)
            {
                this.dateTimePicker1.Value = Staevne.Dato;
                this.textBoxDivision.Text = Staevne.division.ToString();
                this.textBoxRunde.Text = (this.listBoxMatcher.Items.Count + 1).ToString();
                this.textBoxSkov.Text = Staevne.Config.Skov;
                this.textBoxType.Text = Staevne.Config.Type;
            }
        }

        private void buttonGem_Click(object sender, EventArgs e)
        {
            if (txtXMLFile.Text != string.Empty)
            {
                
                DivisionsResultat.DivisionsResultat gemDivisionsResultat = DivisionsResultat != null ? DivisionsResultat.Clone() as DivisionsResultat.DivisionsResultat : new DivisionsResultat.DivisionsResultat();
                if (DivisionsResultat == null)
                {
                    gemDivisionsResultat.År = Convert.ToInt32(this.textBoxÅr.Text);
                    gemDivisionsResultat.Division = Convert.ToInt32(this.textBoxDivision.Text);
                    gemDivisionsResultat.Kreds = (Kreds) Enum.Parse( typeof(Kreds), this.textBoxKreds.Text);
                    gemDivisionsResultat.DivisionsMatchResultater = new List<Divisionsmatch.DivisionsResultat.DivisionsMatchResultat>();
                }

                Util.SaveDivisionsResultat(gemDivisionsResultat, Staevne, txtXMLFile.Text);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Vælg en fil først", "Fejl!");
            }
        }

        private void btnOpenFileXML_Click(object sender, EventArgs e)
        {
            SaveFileDialog openFile = new SaveFileDialog();
            openFile.Filter = "O-service Divisionsresultat (*.xml)|*.xml";
            openFile.OverwritePrompt = true;
            openFile.AddExtension = true;
            openFile.DefaultExt = ".xml";
            openFile.SupportMultiDottedExtensions = true;
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtXMLFile.Text = openFile.FileName;
            }
        }
    }
}
