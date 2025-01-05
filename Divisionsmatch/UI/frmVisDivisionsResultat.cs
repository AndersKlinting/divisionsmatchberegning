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
    /// <summary>
    /// klasse til dialog med divisionsresultat
    /// </summary>
    public partial class frmVisDivisionsResultat : Form
    {
        Staevne _staevne = null;
        
        /// <summary>
        /// creator
        /// </summary>
        public frmVisDivisionsResultat()
        {
            InitializeComponent();
        }

        /// <summary>
        /// egenskab til at holde divisionsresultat for denne dialog
        /// </summary>
        public DivisionsResultat.DivisionsResultat DivisionsResultat
        {
            get;
            set;
        }

        /// <summary>
        /// reference til nærværende stævne
        /// </summary>
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
        /// <summary>
        /// metode til at gentegne resultatet
        /// </summary>
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
