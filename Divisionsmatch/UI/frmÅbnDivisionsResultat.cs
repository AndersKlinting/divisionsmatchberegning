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
    public partial class frmÅbnDivisionsResultat : Form
    {
        public frmÅbnDivisionsResultat()
        {
            InitializeComponent();
        }

        public DivisionsResultat.DivisionsResultat DivisionsResultat { get; internal set; }
        public Staevne Staevne { get; set; }
        public string InitialDirectory { get; set; }
        private void frmDivisionsResultat_Load(object sender, EventArgs e)
        {
            if (Staevne != null)
            {
                this.dateTimePicker1.Value = Staevne.Dato;
                this.textBoxStaevneDivision.Text = Staevne.Config.selectedDivision.ToString();
                this.textBoxStaevneKreds.Text = Staevne.Config.Kreds.ToString();

                this.textBoxStaevneSkov.Text = Staevne.Config.Skov;
                this.textBoxStaevneType.Text = Staevne.Config.Type;

                foreach (var k in Staevne.Config.selectedClubs)
                {
                    listBoxMatchKlubber.Items.Add(k);
                }
            }
        }

        private void btnOpenFileXML_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "O-service Divisionsresultat (*.xml)|*.xml";
            openFile.CheckPathExists = true;
            openFile.AddExtension = true;
            openFile.DefaultExt = ".xml";
            openFile.SupportMultiDottedExtensions = true;
            openFile.Title = "Åbn divisionsresultat";
            openFile.Multiselect = false;
            if (InitialDirectory != string.Empty)
            {
                openFile.InitialDirectory = InitialDirectory;
            }
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // load file
                try
                {
                    DivisionsResultat = null;
                    buttonOK.Enabled = false;

                    txtXMLFile.Text = openFile.FileName;

                    DivisionsResultat.DivisionsResultat mitDivisionsResultat = Util.OpenDivisionsResultat(openFile.FileName);
                    if (mitDivisionsResultat != null)
                    {
                        // fyld dialogen
                        this.textBoxÅr.Text = mitDivisionsResultat.År.ToString();
                        this.textBoxDivision.Text = mitDivisionsResultat.Division.ToString();
                        this.textBoxKreds.Text = mitDivisionsResultat.Kreds.ToString();
                        this.listBoxMatcher.Items.Clear();
                        foreach (var m in mitDivisionsResultat.DivisionsMatchResultater)
                        {
                            this.listBoxMatcher.Items.Add(string.Format("{0} - {1} - {2}", m.Runde, m.Dato, m.Skov));
                        }

                        if (mitDivisionsResultat.DivisionsMatchResultater.Count > 0)
                        {
                            foreach (var k in mitDivisionsResultat.DivisionsMatchResultater[0].Klubber)
                            {
                                this.listBoxDiviKlubber.Items.Add(k.Navn);
                            }
                        }

                        buttonOK.Enabled = true;

                        // check om divisionsresultat om matchen passer
                        string msg = string.Empty;
                        bool ok = mitDivisionsResultat.CheckStaevne(Staevne, out msg);
                        if (!ok)
                        {
                            DivisionsResultat = null;
                            buttonOK.Enabled = false;
                            MessageBox.Show("Konfigurationen passer ikke med Divisions Resultater.\n" + msg + "\nIndlæs eller opret divisionsresultater på ny.", "Fejl");
                        }

                        DivisionsResultat = mitDivisionsResultat;
                        //MessageBox.Show("Tidligere resultater er indlæst. Tryk OK for at benytte dem \n");
                    }
                    else
                    {
                        MessageBox.Show("Tidligere resultater kunnne ikke åbnes \n");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Tidligere resultater kunnne ikke åbnes \n" + ex.Message + "\n" + ex.StackTrace, "Fejl");
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // scenario 1 - inital data fra o-service
            // vær sikker på at vi benytter o-service Id i stævnet
            if (DivisionsResultat.DivisionsMatchResultater != null &&
                DivisionsResultat.DivisionsMatchResultater.Count == 1 &&
                DivisionsResultat.DivisionsMatchResultater[0].Dato.Equals(Staevne.Dato.ToString("yyyy-MM-dd")) &&
                (DivisionsResultat.DivisionsMatchResultater[0].Matcher == null || DivisionsResultat.DivisionsMatchResultater[0].Matcher.Count == 0))
            {
                foreach (Klub sk in Staevne.Config.selectedClubs)
                {
                    foreach (var rk in DivisionsResultat.DivisionsMatchResultater[0].Klubber)
                    {
                        if (sk.Navn == rk.Navn && (rk.Id != null && (sk.Id == null || sk.Id.Id != rk.Id.Id || sk.Id.Type != rk.Id.Type)))
                        {
                            if (sk.Id == null)
                            {
                                sk.Id = new KlubId();
                            }
                            sk.Id.Id = rk.Id.Id;
                            sk.Id.Type = rk.Id.Type;
                        }
                    }
                }

                foreach (Klub sk in Staevne.Config.allClubs)
                {
                    foreach (var rk in DivisionsResultat.DivisionsMatchResultater[0].Klubber)
                    {
                        if (sk.Navn == rk.Navn && (rk.Id != null && (sk.Id == null || sk.Id.Id != rk.Id.Id || sk.Id.Type != rk.Id.Type)))
                        {
                            if (sk.Id == null)
                            {
                                sk.Id = new KlubId();
                            }
                            sk.Id.Id = rk.Id.Id;
                            sk.Id.Type = rk.Id.Type;
                        }
                    }
                }

                foreach (Klub sk in Staevne.Klubber)
                {
                    foreach (var rk in DivisionsResultat.DivisionsMatchResultater[0].Klubber)
                    {
                        if (sk.Navn == rk.Navn && (rk.Id != null && (sk.Id == null || sk.Id.Id != rk.Id.Id || sk.Id.Type != rk.Id.Type)))
                        {
                            if (sk.Id == null)
                            {
                                sk.Id = new KlubId();
                            }
                            sk.Id.Id = rk.Id.Id;
                            sk.Id.Type = rk.Id.Type;
                        }
                    }
                }

                // fjern o-service match, da den er uden resulater
                DivisionsResultat.DivisionsMatchResultater.Clear();
                DivisionsResultat.DivisionsMatchResultater = null;

            }
        }
    }
}
