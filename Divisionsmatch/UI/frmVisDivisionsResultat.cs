using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Divisionsmatch
{
    public partial class frmVisDivisionsResultat : Form
    {
        Staevne _staevne = null;
        public frmVisDivisionsResultat()
        {
            InitializeComponent();
        }

        public DivisionsResultat.DivisionsResultat DivisionsResultat
        {
            get;
            set;
        }

        public Staevne Staevne
        {
            set
            {
                if (_staevne != null)
                {
                    _staevne.BeregningSlut -= new EventHandler(BeregningSlut);
                }
                _staevne = value;
                if (_staevne != null)
                {
                    _staevne.BeregningSlut += new EventHandler(BeregningSlut);
                }
            }
        }
        public override void Refresh()
        {
            _VisResultat();
        }

        internal void BeregningSlut(object sender, EventArgs e)
        {
            _VisResultat();
        }

        private void frmVisDivisionsResultat_FormClosing(object sender, FormClosingEventArgs e)
        {
            Staevne = null;
        }

        private void frmVisDivisionsResultat_Load(object sender, EventArgs e)
        {
            if (DivisionsResultat == null)
            {
                DivisionsResultat = new Divisionsmatch.DivisionsResultat.DivisionsResultat();
            }
            _VisResultat();
        }

        private void _VisResultat()
        {
            this.Text = "Divisions Resultat - " +DateTime.Now.ToShortTimeString();
            DataTable dt = DivisionsResultat.BeregnStilling(_staevne);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;

            textBox1.Font = _staevne.Config.font;
            textBox1.ScrollBars = ScrollBars.Both;
            textBox1.WordWrap = false;
            textBox1.Text = DivisionsResultat.PrintResultText(_staevne);
        }
    }
}
