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

namespace Divisionsmatch
{
    /// <summary>
    /// dialog til at vise pop-up ifm beregning
    /// </summary>
    public partial class frmBeregnPop : Form
    {
        /// <summary>
        /// constructor
        /// </summary>
        public frmBeregnPop()
        {
            InitializeComponent();
        }

        /// <summary>
        /// constructor med message
        /// </summary>
        /// <param name="msg"></param>
        public frmBeregnPop(string msg)
        {
            InitializeComponent();

            label1.Text = msg;
        }
    }
}
