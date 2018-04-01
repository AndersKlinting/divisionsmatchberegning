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
using System.Windows.Forms;
using AutoUpdaterDotNET;

namespace Divisionsmatch
{
    /// <summary>
    /// dialog
    /// </summary>
    public partial class frmOm : Form
    {
        /// <summary>
        /// constructor
        /// </summary>
        public frmOm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("http://www.fiforientering.dk"); 
        }

        private void frmOm_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel2.LinkVisited = true;
            System.Diagnostics.Process.Start("http://opensource.org/licenses/GPL-3.0"); 

        }

        private void label3_Click(object sender, System.EventArgs e)
        {

        }

        private void frmOm_Load(object sender, System.EventArgs e)
        {
            label3.Text = "v " + Application.ProductVersion;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start(frmDivi.autoUpdateSiteURL);
        }
    }
}
