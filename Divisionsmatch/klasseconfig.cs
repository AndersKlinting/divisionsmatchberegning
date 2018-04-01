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
    /// klasse til at holde reglementets kombination af grupper og klasser
    /// </summary>
    public class Klasseconfig
    {
        /// <summary>
        /// reglementets klassebetegnelse
        /// </summary>
        public string GruppeKlasse { get; private set; }

        /// <summary>
        /// løbets klassebetegnelse
        /// </summary>
        public Klasse LøbsKlasse { get; private set; }

        /// <summary>
        /// er det en ungdomsklasse?
        /// </summary>
        public bool Ungdom { get; private set; }

        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="gruppeklasse">reglementets klassebetegnelse</param>
        /// <param name="løbsklasse">løbets klassebetegnelse</param>
        /// <param name="ungdom">er det en ungdomsklasse?</param>
        public Klasseconfig(string gruppeklasse, Klasse løbsklasse, bool ungdom)
        {
            GruppeKlasse = gruppeklasse;
            LøbsKlasse = løbsklasse;
            Ungdom = ungdom;
        }
    }
}
