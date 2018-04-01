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

        private Config _config = new Config(true);

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
        public frmConfig()
        {
            InitializeComponent();

#if DEBUG
            //// txtTXTFileKlasser.Text = @"C:\temp\divisionsturnering\klasser.txt";
            //// txtXMLFile.Text = @"C:\temp\divisionsturnering\startliste.xml";
#endif
            
            editOnly = false;
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            // fyld liste med divisioner
            int nr = Config.selectedDivision;
            cmbDivision.DataSource = Config.divisioner;
            cmbDivision.DisplayMember = "beskrivelse";
            cmbDivision.ValueMember = "nr";
            cmbDivision.SelectedValue = nr;

            this.comboBoxKreds.Items.AddRange(Enum.GetNames(typeof(Kreds)));

            if (editOnly)
            {
                label2.Visible = false;
                label6.Visible = false;
                txtTXTFileKlasser.Visible = false;
                txtXMLFile.Visible = false;
                btnOpenFileXML.Visible = false;
                btnOpenKlasser.Visible = false;
                //btnGem.Visible = false;
                btnLoad.Visible = false;
            }

            _LoadConfig();

            _PreselectKlasser();

            _MakeMatches();

            _updateButtons();

            dataGridView1.Enabled = true;
            lstAllClubs.Enabled = true;
            checkedListClubs.Enabled = true;

            textBox1.BackColor = System.Drawing.Color.White;
            textBox1.ForeColor = System.Drawing.Color.Red;
}

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                Config.LoadKlubberOgKlasser(txtXMLFile.Text, txtTXTFileKlasser.Text);

                _LoadConfig();

                _PreselectKlasser();

                txtMatcher.Text = string.Empty;
                dataGridView1.Enabled = true;
                lstAllClubs.Enabled = true;
                checkedListClubs.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Der skete en fejl: " + ex.Message);
            }
        }

        private void _LoadConfig()
        {
            // fyld lister
            lstAllClubs.DataSource = Config.allClubs;
            checkedListClubs.DataSource = Config.selectedClubs;
            _unCheckUdeblevne();

            // fyld grid med grupper og klasser
            dataGridView1.DataSource = null;

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dataGridView1.DataSource = Config.gruppeOgKlasse;
            dataGridView1.Columns[0].DataPropertyName = "Gruppe";
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].DataPropertyName = "Klasse";
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].DataPropertyName = "Ungdom";
            dataGridView1.Columns[2].ReadOnly = true;

            dataGridView1.Columns.RemoveAt(3);

            DataGridViewComboBoxColumn column3 = new DataGridViewComboBoxColumn();
            column3.DataPropertyName = "Løbsklasse";
            column3.HeaderText = "Løbsklasse";
            column3.DisplayMember = "DisplayName";
            column3.ValueMember = "Navn";
            dataGridView1.Columns.Add(column3);

            (dataGridView1.Columns[3] as DataGridViewComboBoxColumn).DataSource = Config.classes;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (Config.Dato != DateTime.Now.Date)
            {
                dateTimePickerDato.Value = Config.Dato;
            }
            if (!string.IsNullOrEmpty(Config.Skov))
            {
                textBoxSkov.Text = Config.Skov;
            }
            if (Config.Type != null)
            {
                comboBoxType.SelectedIndex = comboBoxType.Items.IndexOf(Config.Type);
            }
            if (Config.Kreds != null)
            {
                comboBoxKreds.SelectedIndex = comboBoxKreds.Items.IndexOf(Config.Kreds.ToString());
            }
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
                            dataGridView1.Rows[r].Cells[0].Style.BackColor = Color.LightGray;
                            dataGridView1.Rows[r].Cells[1].Style.BackColor = Color.LightGray;
                            dataGridView1.Rows[r].Cells[2].Style.BackColor = Color.LightGray;
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[r].Cells[0].Style.BackColor = dataGridView1.Rows[r - 1].Cells[0].Style.BackColor;
                        dataGridView1.Rows[r].Cells[1].Style.BackColor = dataGridView1.Rows[r - 1].Cells[1].Style.BackColor;
                        dataGridView1.Rows[r].Cells[2].Style.BackColor = dataGridView1.Rows[r - 1].Cells[1].Style.BackColor;
                    }
                }
                dataGridView1.Rows[r].DefaultCellStyle.ForeColor = Color.Red;

                // prøv at beregne løbsklasse
                object setValue = null;
                if (!string.IsNullOrEmpty(lobsklasse))
                {
                    string testklasse = lobsklasse.Replace('-', ' ').Replace(" ", string.Empty);
                    // forsøg at anvende
                    foreach (var k in (dataGridView1.Columns[3] as DataGridViewComboBoxColumn).Items)
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
                    foreach (var k in (dataGridView1.Columns[3] as DataGridViewComboBoxColumn).Items)
                    {
                        if (k.ToString() != string.Empty && (klasse.Equals(k.ToString()) || lowercaseKlasse.Equals(k.ToString().Replace('-', ' ').Replace(" ", string.Empty).ToLowerInvariant())))
                        {
                            setValue = k;
                            break;
                        }
                    }
                }
                (dataGridView1.Rows[r].Cells[3] as DataGridViewComboBoxCell).Value = setValue;

            
                // opdater farve svarene til valg
                _updateGrid(r);            
            }
        }

        private void lstAllClubs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstAllClubs.SelectedIndex >= 0)
            {
                Config.selectedClubs.Add(lstAllClubs.SelectedItem as Klub);

                int topIndex = lstAllClubs.TopIndex;
                Config.allClubs.Remove(lstAllClubs.SelectedItem as Klub);
                if (lstAllClubs.Items.Count > topIndex)
                    lstAllClubs.TopIndex = topIndex;

                _MakeMatches();

                _unCheckUdeblevne();

                _updateButtons();
            }
        }
     
        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDivision.SelectedItem != null)
            {
                Config.selectedDivision = ((Division)cmbDivision.SelectedItem).Nr;
                _updateButtons();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
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
                Klasse kl = dataGridView1.Rows[k].Cells[3].Value as Klasse;
                if (kl != null && (kl.Navn != "" && kl.Navn != " - "))
                {
                    for (int r = k + 1; r < dataGridView1.Rows.Count; r++)
                    {
                        if (kl == dataGridView1.Rows[r].Cells[3].Value)
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

            // er alle klasser tildelt?
            if (Config.gruppeOgKlasse.Exists(item => item.LøbsKlasse == null || item.LøbsKlasse == string.Empty))
            {
                msg += "Ikke alle klasser har fået matchende løbsklasse";
            }

            if (checkduplicate())
            {
                msg += msg != string.Empty ? System.Environment.NewLine : string.Empty;
                msg += "Mindst en løbsklasse er brugt flere gange";
            }

            if (checkedListClubs.Items.Count == 0)
            {
                msg += msg != string.Empty ? System.Environment.NewLine : string.Empty;
                msg += "Du mangler at vælge klubber";
            }

            if (comboBoxType.SelectedIndex < 0)
            {
                msg += msg != string.Empty ? System.Environment.NewLine : string.Empty;
                msg += "Du mangler at vælge løbs type";
            }
            else if (comboBoxType.SelectedItem == "Divisionsmatchfinale")
            {
                // tving 1 division men ingen kreds
                if (!cmbDivision.SelectedValue.Equals(1))
                {
                    cmbDivision.SelectedValue = 1;                    
                }
                if (comboBoxKreds.SelectedIndex != -1)
                {
                    comboBoxKreds.SelectedIndex = -1;
                }
            }
            else
            {
                if (cmbDivision.SelectedIndex < 0)
                {
                    msg += msg != string.Empty ? System.Environment.NewLine : string.Empty;
                    msg += "Du mangler at vælge division";
                }

                if (comboBoxKreds.SelectedIndex < 0)
                {
                    msg += msg != string.Empty ? System.Environment.NewLine : string.Empty;
                    msg += "Du mangler at vælge kreds";
                }
            }

            if (string.IsNullOrWhiteSpace(textBoxSkov.Text))
            {
                msg += msg != string.Empty ? System.Environment.NewLine : string.Empty;
                msg += "Du mangler at angive en skov";
            }

            cmbDivision.Enabled = (comboBoxType.SelectedIndex >= 0 && comboBoxType.SelectedItem != "Divisionsmatchfinale");
            comboBoxKreds.Enabled = (comboBoxType.SelectedIndex >= 0 && comboBoxType.SelectedItem != "Divisionsmatchfinale");
            textBox1.Text = msg;
            textBox1.Visible = (msg != string.Empty);
            btnOK.Enabled = msg == string.Empty;
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

        private void btnOpenFileXML_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "IOF XML tilmeldingsliste, startliste, resultatliste (*.xml)|*.xml|OE2003 resultatliste (*.csv)|*.csv|EResults Pro løber export (*.txt)|*.txt";
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
                txtXMLFile.Text = openFile.FileName;
            }
        }

        private void btnOpenKlasser_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "OE2003 klasser (*.txt)|*.txt";
            openFile.CheckPathExists = true;
            openFile.AddExtension = true;
            openFile.DefaultExt = ".txt";
            openFile.SupportMultiDottedExtensions = true;
            openFile.Title = "Åbn OE2003 klasser";
            openFile.Multiselect = false;
            if (_currentDirectory != string.Empty)
            {
                openFile.InitialDirectory = _currentDirectory;
            }
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _currentDirectory = Path.GetDirectoryName(openFile.FileName); 
                txtTXTFileKlasser.Text = openFile.FileName;
            }
        }

        private void checkedListClubs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (checkedListClubs.SelectedIndex >= 0)
            {
                Config.allClubs.Add(checkedListClubs.SelectedItem as Klub);

                int topIndex = checkedListClubs.TopIndex;
                Config.selectedClubs.Remove(checkedListClubs.SelectedItem as Klub);
                if (checkedListClubs.Items.Count > topIndex)
                {
                    checkedListClubs.TopIndex = topIndex;
                }

                // fjern også fra udeblevne
                if (Config.udeblevneKlubber.Contains(checkedListClubs.SelectedItem as Klub))
                {
                    Config.udeblevneKlubber.Remove(checkedListClubs.SelectedItem as Klub);
                }
                _unCheckUdeblevne();
                
                _MakeMatches();
                
                _updateButtons();
            }
        }

        private void _fyldUdeBlevne()
        {
            Config.udeblevneKlubber.Clear();
            for(int i=0; i< checkedListClubs.Items.Count;i++)
            {
                if (!checkedListClubs.GetItemChecked(i))
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
                if (!Config.udeblevneKlubber.Contains(checkedListClubs.Items[i] as Klub))
                {
                    checkedListClubs.SetItemChecked(i, true);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Config.Dato = dateTimePickerDato.Value;
            Config.Type = comboBoxType.Text;
            Config.Skov = textBoxSkov.Text;
            Config.Kreds = (comboBoxKreds.Text != string.Empty) ? (Kreds)Enum.Parse(typeof(Kreds), comboBoxKreds.Text) : (Kreds?)null;
            Config.NeedEdit = false;
            _fyldUdeBlevne();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void checkedListClubs_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Klub club = checkedListClubs.Items[e.Index] as Klub;
            if (e.NewValue == CheckState.Checked)
            {
                if (Config.udeblevneKlubber.Contains(club))
                {
                    Config.udeblevneKlubber.Remove(club);
                }
            }
            else
            {
                if (!Config.udeblevneKlubber.Contains(club))
                {
                    Config.udeblevneKlubber.Add(club);
                }
            }
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListClubs_SelectedIndexChanged(object sender, EventArgs e)
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
    } 
}
