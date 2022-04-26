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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Divisionsmatch
{
    /// <summary>
    /// dialog til at lave/rette en konfiguration af et stævne
    /// </summary>
    public partial class frmConfig : Form
    {
        private string _currentDirectory = string.Empty;

        private Config _config = null;

        private int indexLoebsklasse = -1;

        /// <summary>
        /// config som oprettes/rettes
        /// </summary>
        public Config Config
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

        /// <summary>
        /// flag som styrer om man retter konfiguration
        /// </summary>
        public bool editOnly { get; set; }

        /// <summary>
        /// fil navn
        /// </summary>
        public string saveFile { get; internal set; }

        /// <summary>
        /// constructor
        /// </summary>
        public frmConfig(Config config)
        {
            InitializeComponent();

#if DEBUG
            //// txtTXTFileKlasser.Text = @"C:\temp\divisionsturnering\klasser.txt";
            //// txtXMLFile.Text = @"C:\temp\divisionsturnering\startliste.xml";
#endif

            _config = config;
            editOnly = false;
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            _LoadConfig();

            _PreselectKlasser();

            _MakeMatches();

            _updateButtons();

            dataGridView1.Enabled = true;
            checkedListClubs.Enabled = true;

            textBox1.BackColor = System.Drawing.Color.White;
            textBox1.ForeColor = System.Drawing.Color.Red;

            if (!btnOK.Enabled)
            {
                MessageBox.Show("Konfigurationen er ikke komplet. Indlæs evt. en fil med løbsklasser og/eller kontroller flueben for klubberne", "Kontroller konfigurationen");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "IOF XML tilmeldingsliste, startliste, resultatliste (*.xml)|*.xml|OE2003 resultatliste (*.csv)|*.csv|EResults Pro løber export (*.txt)|*.txt|OE2003 klasser (*.txt)|*.txt";
            openFile.CheckPathExists = true;
            openFile.AddExtension = true;
            openFile.DefaultExt = ".xml";
            openFile.SupportMultiDottedExtensions = true;
            openFile.Title = "Åbn liste";
            openFile.Multiselect = false;
            if (_currentDirectory != string.Empty)
            {
                openFile.InitialDirectory = _currentDirectory;
            }
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _currentDirectory = Path.GetDirectoryName(openFile.FileName);

                try
                {
                    Config.LoadKlasserOgBaner(openFile.FileName);

                    _LoadConfig();

                    _MakeMatches();

                    _PreselectKlasser();

                    string msg = "Kontroller at alle reglementsklasser har en korrekt tilknyttet løbsklasse eller '-', og at klubber er markeret med rigtige flueben.";
                    if (Config.classes.Count>2)
                    {
                        dataGridView1.Enabled = true;
                        checkedListClubs.Enabled = true;

                        msg = string.Format("Der blev fundet {0} baner og {1} klasser.\n{2}\n", Config.baner.Count, Config.classes.Count, (Config.baner.Count>0 ? string.Empty : "Baner er ikke påkrævet.\n")) + msg;
                    }
                    else
                    {
                        msg = "Der blev ikke fundet klasser og baner i den indlæste fil. Kontroller lige om den er OK";
                    }

                    MessageBox.Show(msg, "Check konfigurationen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Der skete en fejl: " + ex.Message);
                }
            }
        }

        private void _LoadConfig()
        {
            // fyld lister
            checkedListClubs.DataSource = Config.Klubber;
            _unCheckUdeblevne();

            // fyld grid med grupper og klasser
            dataGridView1.DataSource = null;

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // reset løbsklasse for grupperne
            foreach (GruppeOgKlasse gk in Config.gruppeOgKlasse)
            {
                gk.LøbsKlasse = null;
            }

            dataGridView1.DataSource = Config.gruppeOgKlasse;
            dataGridView1.Columns[0].DataPropertyName = "Gruppe";
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[0].Visible = false;

            dataGridView1.Columns[1].DataPropertyName = "Klasse";
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[1].HeaderText = "Reglementsklasse";

            indexLoebsklasse = 2;
            dataGridView1.Columns.RemoveAt(indexLoebsklasse);

            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            column.DataPropertyName = "Løbsklasse";
            column.HeaderText = "Løbsklasse";
            column.DisplayMember = "DisplayName";
            column.ValueMember = "Navn";
            dataGridView1.Columns.Add(column);

            (dataGridView1.Columns[indexLoebsklasse] as DataGridViewComboBoxColumn).DataSource = Config.classes;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dateTimePickerDato.Value = Config.Dato;

            if (!string.IsNullOrEmpty(Config.Skov))
            {
                textBoxSkov.Text = Config.Skov;
            }
            if (Config.Type != null)
            {
                textBoxType.Text = Config.Type;
            }
            if (Config.Kreds != null)
            {
                textBoxKreds.Text = Config.Kreds;
            }
            if (Config.KredsId != null)
            {
                textBoxKredsId.Text = Config.KredsId;
            }
            textBoxRunde.Text = string.Empty;
            if (Config.Division >= 1 && Config.Division <= 6)
            {
                textBoxDivision.Text = Config.Division + ". division";
                textBoxRunde.Text = Config.Runde.ToString();
            }
            else if (Config.Division == 8)
            {
                textBoxDivision.Text = "Op/Ned";
            }
            else if (Config.Division == 9)
            {
                textBoxDivision.Text = "Finale";
            }
            else
            {
                textBoxDivision.Text = string.Empty;
            }
            textBoxBeskriv.Text = Config.Beskrivelse;
        }

        private void _PreselectKlasser()
        {
            string gruppe = "";
            string klasse = "";
            string lobsklasse = "";
            for (int r = 0; r < dataGridView1.Rows.Count; r++)
            {
                gruppe = (dataGridView1.Rows[r].DataBoundItem as GruppeOgKlasse).Gruppe;
                klasse = (dataGridView1.Rows[r].DataBoundItem as GruppeOgKlasse).Klasse;
                lobsklasse = (dataGridView1.Rows[r].DataBoundItem as GruppeOgKlasse).LøbsKlasse;

                // sæt farve på baggrunden
                if (r > 0)
                {
                    if (dataGridView1.Rows[r - 1].Cells[0].Value.ToString() != gruppe)
                    {
                        if (dataGridView1.Rows[r - 1].Cells[0].Style.BackColor != Color.LightGray)
                        {
                            for (int c = 0; c < dataGridView1.Rows[r].Cells.Count; c++)
                            {
                                dataGridView1.Rows[r].Cells[c].Style.BackColor = Color.LightGray;
                            }
                        }
                    }
                    else
                    {
                        for (int c = 0; c < dataGridView1.Rows[r].Cells.Count; c++)
                        {
                            dataGridView1.Rows[r].Cells[c].Style.BackColor = dataGridView1.Rows[r - 1].Cells[c].Style.BackColor;
                        }
                    }
                }
                dataGridView1.Rows[r].DefaultCellStyle.ForeColor = Color.Red;

                // prøv at beregne løbsklasse
                object setValue = null;
                if (!string.IsNullOrEmpty(lobsklasse))
                {
                    string testklasse = lobsklasse.Replace('-', ' ').Replace(" ", string.Empty);
                    // forsøg at anvende
                    foreach (var k in (dataGridView1.Columns[indexLoebsklasse] as DataGridViewComboBoxColumn).Items)
                    {
                        if (k.ToString() != string.Empty && (lobsklasse.Equals(k.ToString()) || testklasse.Equals(k.ToString().Replace('-', ' ').Replace(" ", string.Empty))))
                        {
                            setValue = k;
                            break;
                        }
                    }
                }

                if (setValue == null)
                {
                    // prøv at matche reglement til aktuelle klasser
                    string lowercaseKlasse = klasse.ToLowerInvariant().Replace('-', ' ').Replace(" ", string.Empty);
                    foreach (var k in (dataGridView1.Columns[indexLoebsklasse] as DataGridViewComboBoxColumn).Items)
                    {
                        if (k.ToString() != string.Empty && (klasse.Equals(k.ToString()) || lowercaseKlasse.Equals(k.ToString().Replace('-', ' ').Replace(" ", string.Empty).ToLowerInvariant())))
                        {
                            setValue = k;
                            break;
                        }
                    }
                }
                (dataGridView1.Rows[r].Cells[indexLoebsklasse] as DataGridViewComboBoxCell).Value = setValue;

            
                // opdater farve svarene til valg
                _updateGrid(r);            
            }
        }
     
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == indexLoebsklasse)
            {
                _updateGrid(e.RowIndex);
                _updateButtons();
            }
        }

        private void _MakeMatches()
        {
            List<Klub> klubber = new List<Klub>();
            for (int k = 0; k < checkedListClubs.Items.Count; k++)
            {
                klubber.Add(checkedListClubs.Items[k] as Klub);
            }

            IList<Match> matcher = Staevne.GetMatcher(null, klubber);

            StringBuilder sb = new StringBuilder();
            int j = 1;
            foreach (Match m in matcher)
            {
                sb.AppendLine(j.ToString().PadLeft(2) + " : " + m.Klub1.Navn.ToString().PadRight(25) + " - " + m.Klub2.Navn.ToString().PadRight(25));
                j++;
            }

            txtMatcher.Text = sb.ToString();
        }

        private bool checkduplicate()
        {
            bool bDouble = false;
            for (int k = 0; k < dataGridView1.Rows.Count; k++)
            {
                string kl = dataGridView1.Rows[k].Cells[indexLoebsklasse].Value as string;
                if (kl != null && (kl != "" && kl != " - "))
                {
                    for (int r = k + 1; r < dataGridView1.Rows.Count; r++)
                    {
                        if (kl == dataGridView1.Rows[r].Cells[indexLoebsklasse].Value as string)
                        {
                            bDouble = true;
                            break;
                        }
                    }
                }
            }

            return bDouble;
        }

        private void _updateButtons()
        {
            string msg = string.Empty;
            bool bKlasserOK = true;

            // er alle klasser tildelt?
            if (Config.gruppeOgKlasse.Exists(item => item.LøbsKlasse == null || item.LøbsKlasse == string.Empty))
            {
                msg += "Ikke alle klasser har fået matchende løbsklasse. Reglementsklasser, som ikke har løbere i løbet, skal markeres med '-'.\n";
                bKlasserOK = false;
            }

            if (checkduplicate())
            {
                msg += msg != string.Empty ? System.Environment.NewLine : string.Empty;
                msg += "Mindst en løbsklasse er brugt flere gange\n";
                bKlasserOK = false;
            }

            if (Config.Klubber.Count > 0 && Config.Klubber.Count == Config.udeblevneKlubber.Count)
            {
                msg += msg != string.Empty ? System.Environment.NewLine : string.Empty;
                msg += "Er alle klubber udeblevet? Du mangler at vælge klubber\n";
            }

            if (string.IsNullOrWhiteSpace(textBoxSkov.Text))
            {
                msg += msg != string.Empty ? System.Environment.NewLine : string.Empty;
                msg += "Du mangler at angive en skov\n";
            }

            textBox1.Text = msg;
            textBox1.Visible = (msg != string.Empty);
            btnOK.Enabled = msg == string.Empty;
            btnLoad.BackColor = bKlasserOK ? Control.DefaultBackColor : Color.LightPink;
        }

        private void _updateGrid(int r)
        {
            if (string.IsNullOrEmpty((dataGridView1.Rows[r].DataBoundItem as GruppeOgKlasse).LøbsKlasse))
            {
                (dataGridView1.Rows[r] as DataGridViewRow).DefaultCellStyle.ForeColor = Color.Red;
            }
            else
            {
                (dataGridView1.Rows[r] as DataGridViewRow).DefaultCellStyle.ForeColor = Color.FromName("WindowText");
            }
        }

        private void _fyldUdeBlevne()
        {
            Config.udeblevneKlubber.Clear();
            for(int i=0; i< checkedListClubs.Items.Count;i++)
            {
                (checkedListClubs.Items[i] as Klub).Udeblevet = !checkedListClubs.GetItemChecked(i);
                if ((checkedListClubs.Items[i] as Klub).Udeblevet)
                {
                    Config.udeblevneKlubber.Add(checkedListClubs.Items[i] as Klub);
                }
            }
        }

        private void _unCheckUdeblevne()
        {
            // check alle undtagen de udeblevne
            for (int i=0; i< checkedListClubs.Items.Count;i++)
            {
                if (!Config.udeblevneKlubber.Exists(k=> k.Navn == (checkedListClubs.Items[i] as Klub).Navn))
                {
                    checkedListClubs.SetItemChecked(i, true);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Config.Dato = dateTimePickerDato.Value;
            Config.NeedEdit = false;
            _fyldUdeBlevne();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void checkedListClubs_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Klub club = checkedListClubs.Items[e.Index] as Klub;
            if (e.NewValue == CheckState.Checked)
            {
                Klub c = Config.udeblevneKlubber.FirstOrDefault(k => k.Navn == club.Navn);
                if (c!=null)
                {
                    Config.udeblevneKlubber.Remove(c);
                }
            }
            else
            {
                Klub c = Config.udeblevneKlubber.FirstOrDefault(k => k.Navn == club.Navn);
                if (c == null)
                {
                    Config.udeblevneKlubber.Add(club);
                }
            }

            _updateButtons();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Vil du annullere konfigureringen?", "??", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                // luk uden at gemme
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void txtXMLFile_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSkov_Leave(object sender, EventArgs e)
        {
            _updateButtons();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _updateButtons();
        }

        private void comboBoxKreds_SelectedIndexChanged(object sender, EventArgs e)
        {
            _updateButtons();
        }

        private void lstAllClubs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBoxSkov_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBoxDivision_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnOpenKlasser_Click(object sender, EventArgs e)
        {

        }

        private void txtTXTFileKlasser_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimePickerDato_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void textBoxBeskriv_TextChanged(object sender, EventArgs e)
        {

        }
    } 
}
