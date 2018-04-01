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

namespace Divisionsmatch
{
    /// <summary>
    /// klasse for kombination of divisionsmatchklasse i en gruppe og tilsvarende løbsklasse
    /// </summary>
    public class GruppeOgKlasse
    {
        /// <summary>
        /// gruppen
        /// </summary>
        public string Gruppe { get; set; }

        /// <summary>
        /// klassens navn i gruppen
        /// </summary>
        public string Klasse { get; set; }

        /// <summary>
        ///  er det en ungdomsklasse
        /// </summary>
        public bool Ungdom { get; set; }

        /// <summary>
        /// løbsklasses som er knyttet til 
        /// </summary>
        public string LøbsKlasse { get; set; }
    }
}
