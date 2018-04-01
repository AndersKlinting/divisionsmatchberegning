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
    public partial class frmNytDivisionsResultat : Form
    {
        public frmNytDivisionsResultat()
        {
            InitializeComponent();
        }

        public Staevne Staevne { get; set; }
        public Kreds Kreds
        {
            get
            {
                return (Kreds) Enum.Parse(typeof(Kreds), comboBoxKreds.SelectedItem.ToString());
            }
        }

        public int Division
        {
            get
            {
                return (comboBoxDivision.SelectedItem as Division).Nr;
            }
        }

        public int År
        {
            get
            {
                return dateTimePickerÅr.Value.Year;
            }
        }

        private void frmDivisionsResultat_Load(object sender, EventArgs e)
        {
            this.comboBoxKreds.Items.AddRange(Enum.GetNames(typeof(Kreds)));
            if (Staevne != null && Staevne.Config != null)
            {
                this.dateTimePickerÅr.Value = Staevne.Config.Dato;
                this.dateTimePickerÅr.Enabled = false;
                this.comboBoxDivision.Items.AddRange(Staevne.Config.divisioner.ToArray());
                this.comboBoxDivision.SelectedIndex = Staevne.Config.selectedDivision - 1;
                this.comboBoxDivision.Enabled = false;

                this.comboBoxKreds.SelectedIndex = (int)Staevne.Config.Kreds;
                this.comboBoxKreds.Enabled = false;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBoxDivision.SelectedIndex == -1)
            {
                MessageBox.Show("Vælg en division");
                return;
            }
            else if (comboBoxKreds.SelectedIndex == -1)
            {
                MessageBox.Show("Vælg en Kreds");
                return;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
