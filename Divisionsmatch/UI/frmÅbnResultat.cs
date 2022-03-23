using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Divisionsmatch
{
    public partial class frmÅbnResultat : Form
    {
        private ResultatDefinition _resultatDefinition;

        public frmÅbnResultat()
        {
            InitializeComponent();
        }

        public ResultatDefinition resultatDefinition
        {
            get
            {
                if (_resultatDefinition == null)
                {
                    _resultatDefinition = new ResultatDefinition();
                }
                return _resultatDefinition;
            }

            set
            {
                _resultatDefinition = value;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txtMeOS.Enabled = radioButton2.Checked;
            numericUpDownMeOS.Enabled = radioButton2.Checked;
            if (txtMeOS.Enabled && string.IsNullOrWhiteSpace(txtMeOS.Text))
            {
                txtMeOS.Text = "http://localhost:2009/meos/?get=iofresult";
            }
            if (txtMeOS.Enabled)
            {
                txtMeOS.Focus();
                txtMeOS.SelectAll();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            btnOpenResultFile.Enabled = radioButton1.Checked;
            if (btnOpenResultFile.Enabled)
                btnOpenResultFile.Focus();  
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked)
                {
                    _resultatDefinition = new ResultatDefinition(txtResultFile.Text);
                }
                else if (radioButton2.Checked)
                {
                    _resultatDefinition = new ResultatDefinition(txtMeOS.Text, (int)numericUpDownMeOS.Value);
                }
                else
                {
                    _resultatDefinition = new ResultatDefinition();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Fejl");
                this.DialogResult = DialogResult.None;
            }
        }

        private void frmÅbnResultat_Load(object sender, EventArgs e)
        {
            if (resultatDefinition!=null)
            {
                if (resultatDefinition.IsMeOS)
                {
                    txtMeOS.Text = resultatDefinition.MeOSUrl;
                    numericUpDownMeOS.Value = resultatDefinition.IntervalMeOS;
                    radioButton2.Checked = true;
                }
                else
                {
                    txtResultFile.Text = resultatDefinition.Filnavn;
                    radioButton1.Checked = true;
                }
            }
        }

        private void btnOpenResultFile_Click(object sender, EventArgs e)
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

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtResultFile.Text = openFile.FileName;
                btnOK.Focus();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
