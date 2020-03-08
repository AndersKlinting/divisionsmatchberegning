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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Divisionsmatch
{
    /// <summary>
    /// gruppe i divisionsmatch reglementet
    /// </summary>
    public class Gruppe
    {
        #region member variables
        private List<Klasseconfig> _klasser;
        private SortedList<string, Loeber> _loebere;

        private int _division = 1;
        private string _navn = string.Empty;

        private int[] _points;
        private int[] _upoints;

        private int[] _pointTilDeling;

        private string _printOutput = string.Empty;
        #endregion

        /// <summary>
        /// constructor. kaldes med navn og liste af divisioner
        /// </summary>
        /// <param name="n">navnet</param>
        /// <param name="division">listen af divisioner</param>
        public Gruppe(string n, int division)
        {
            _navn = n;
            _division = division;
            _setPoints();
        }

        #region public properties
        
        /// <summary>
        /// navnet på gruppen
        /// </summary>
        public string navn
        {
            get
            {
                return _navn;
            }
        }

        /// <summary>
        /// returnerer en liste af klasser i denne gruppe
        /// </summary>
        public List<Klasseconfig> Klasser
        {
            get
            {
                if (_klasser == null)
                {
                    _klasser = new List<Klasseconfig>();
                }
                return _klasser;
            }
        }
        
        /// <summary>
        /// returnerer en sorteret liste af løbere i denne gruppe
        /// </summary>
        public SortedList<string, Loeber> Loebere
        {
            get
            {
                if (_loebere == null)
                {
                    _loebere = new SortedList<string, Loeber>();
                }
                return _loebere;
            }
        }
        #endregion

        #region public metods
        /// <summary>
        /// har gruppen en klasse for ne bestemt løbsklasse
        /// </summary>
        /// <param name="klasse">løbsklasse</param>
        /// <returns>true/false</returns>
        public bool Harløbsklasse(string klasse)
        {
            return Klasser.Exists(item => item.LøbsKlasse != null && item.LøbsKlasse.Navn.Equals(klasse)); //.ToList().Exists(item => item.Replace(" ", "").ToLowerInvariant().Equals(klasse.Replace(" ", "").ToLowerInvariant()));
        }

        /// <summary>
        /// få de mulige point i denne gruppe
        /// </summary>
        /// <returns>point</returns>
        public int MuligePoint()
        {
            int p = _pointTilDeling.Sum();
            return p;
        }

        /// <summary>
        /// fordel point på løbere i gruppen for en bestemt match
        /// </summary>
        /// <param name="match">match</param>
        public void FordelPoint(Match match)
        {
            // reset output
            _printOutput = string.Empty;

            _tildelpoint(match, _pointTilDeling.Count() / 2, _pointTilDeling);
        }

        /// <summary>
        /// Print en gruppe overskrift som TXT
        /// </summary>
        /// <returns>overskriften</returns>
        public string LavTXTOverskrift()
        {
            if (_printOutput != string.Empty)
            {
                return _printOutput;
            }
            else
            {
                StringBuilder output = new StringBuilder();

                string grp = navn;
                grp += " - " + _printPointsForGruppe();
                output.AppendLine(grp);
                output.AppendLine("".PadLeft(grp.Length, '-'));

                return output.ToString();
            }
        }

        /// <summary>
        /// Lav Gruppens resultat som HTML
        /// </summary>
        /// <returns>html</returns>
        public string LavHTMLoverskrift(string layout)
        {
            StringBuilder output = new StringBuilder();

            if (layout == "Standard")
            {
                output.AppendLine("<h3 class=\"gruppe\" id=\"" + navn + "\">" + navn + "</h3>");
                output.AppendLine("<p>" + _printPointsForGruppe() + "</p>");
            }
            else if (layout == "Blå overskrifter")
            {
                output.AppendLine("<div class=\"gruppeHeader\" id=\"" + navn + "\">" + navn + "</div>");
                output.AppendLine("<div class=\"gruppepoint\">" + _printPointsForGruppe() + "</div>");
            }
            return output.ToString();
        }
        #endregion

        #region private methods
        private void _setPoints()
        {
            if (navn == "Beg") { _pointTilDeling = new int[] { 1, 1, 1, 1, 1, 1 }; }
            else if (navn == "D10") { _pointTilDeling = new int[] { 1, 1, 1, 1, 1, 1 }; }
            else if (navn == "D12") { _pointTilDeling = new int[] { 4, 3, 2, 1 }; }
            else if (navn == "D12B") { _pointTilDeling = new int[] { 1, 1, 1, 1, 1, 1 }; }
            else if (navn == "D14") { _pointTilDeling = new int[] { 4, 3, 2, 1 }; }
            else if (navn == "D14B") { _pointTilDeling = new int[] { 2, 2, 1, 1 }; }
            else if (navn == "D16") { _pointTilDeling = new int[] { 4, 3, 2, 1 }; }
            else if (navn == "D18") { _pointTilDeling = new int[] { 4, 3, 2, 1 }; }
            else if (navn == "D20") { _pointTilDeling = new int[] { 4, 3, 2, 1 }; }
            else if (navn == "D20B") { _pointTilDeling = new int[] { 2, 2, 1, 1 }; }
            else if (navn == "D21") { _pointTilDeling = new int[] { 8, 7, 6, 5, 4, 3, 2, 1 }; }
            else if (navn == "D21B") { _pointTilDeling = new int[] { 2, 2, 2, 1, 1, 1 }; }
            else if (navn == "D40") { _pointTilDeling = new int[] { 6, 5, 4, 3, 2, 1 }; }
            else if (navn == "D45B") { _pointTilDeling = new int[] { 2, 2, 2, 1, 1, 1 }; }
            else if (navn == "D50") { _pointTilDeling = new int[] { 6, 5, 4, 3, 2, 1 }; }
            else if (navn == "D60") { _pointTilDeling = new int[] { 6, 5, 4, 3, 2, 1 }; }
            else if (navn == "D70") { _pointTilDeling = new int[] { 6, 5, 4, 3, 2, 1 }; }
            else if (navn == "D-Let") { _pointTilDeling = new int[] { 2, 2, 2, 1, 1, 1 }; }
            else if (navn == "H10") { _pointTilDeling = new int[] { 1, 1, 1, 1, 1, 1 }; }
            else if (navn == "H12") { _pointTilDeling = new int[] { 4, 3, 2, 1 }; }
            else if (navn == "H12B") { _pointTilDeling = new int[] { 1, 1, 1, 1, 1, 1 }; }
            else if (navn == "H14") { _pointTilDeling = new int[] { 4, 3, 2, 1 }; }
            else if (navn == "H14B") { _pointTilDeling = new int[] { 2, 2, 1, 1 }; }
            else if (navn == "H16") { _pointTilDeling = new int[] { 4, 3, 2, 1 }; }
            else if (navn == "H18") { _pointTilDeling = new int[] { 4, 3, 2, 1 }; }
            else if (navn == "H20") { _pointTilDeling = new int[] { 4, 3, 2, 1 }; }
            else if (navn == "H20B") { _pointTilDeling = new int[] { 2, 2, 1, 1 }; }
            else if (navn == "H21") { _pointTilDeling = new int[] { 8, 7, 6, 5, 4, 3, 2, 1 }; }
            else if (navn == "H21B") { _pointTilDeling = new int[] { 2, 2, 2, 1, 1, 1 }; }
            else if (navn == "H40") { _pointTilDeling = new int[] { 6, 5, 4, 3, 2, 1 }; }
            else if (navn == "H45B") { _pointTilDeling = new int[] { 2, 2, 2, 1, 1, 1 }; }
            else if (navn == "H50") { _pointTilDeling = new int[] { 6, 5, 4, 3, 2, 1 }; }
            else if (navn == "H60") { _pointTilDeling = new int[] { 6, 5, 4, 3, 2, 1 }; }
            else if (navn == "H70") { _pointTilDeling = new int[] { 6, 5, 4, 3, 2, 1 }; }
            else if (navn == "H80") { _pointTilDeling = new int[] { 6, 5, 4, 3, 2, 1 }; }
            else if (navn == "H-Let") { _pointTilDeling = new int[] { 2, 2, 2, 1, 1, 1 }; }
        }

        private string _printPointsForGruppe()
        {
            string grp = string.Empty;
            if (_points != null && _points.Length > 0)
            {
                grp += "[" + string.Join(", ", _points) + "]";
            }
            if (_upoints != null && _upoints.Length > 0)
            {
                grp += " + [";
                grp += _upoints[0].ToString();
                for (int p = 1; p < _upoints.Length; p++) grp += ", " + _upoints[p].ToString();
                grp += "]";
            }

            return grp;
        }

        private void _tildelpoint(Match match, int antal, int[] point)
        {
            _points = point;
            SortedList<string, Loeber> toploebere = new SortedList<string, Loeber>();

            int n = 0; // tildelte point
            double p = 0.0;

            IList<Loeber> loebereK1 = new List<Loeber>();
            IList<Loeber> loebereK2 = new List<Loeber>();
            IList<Loeber> loebereRestrict = new List<Loeber>();

            List<string> tider = new List<string>();

            // for gennemførte løbere fra de 2 klubber i matchen
            var matchloebere = Loebere.Where(item => ((item.Value.Klub == match.Klub1 && !match.Klub1.Udeblevet) || (item.Value.Klub == match.Klub2 && !match.Klub2.Udeblevet)) && item.Value.IsStatusOK);
            foreach (var kv in matchloebere)
            {
                Loeber l = kv.Value;

                // lav kun beregningen 1 gang for hver tid
                if (!tider.Contains(l.Tid))
                {
                    tider.Add(l.Tid);

                    // noter om vi allerede har restrictioner
                    bool restrict = loebereRestrict.Count > 0;

                    // find alle løbere med denne tid og foretag beregning hvis der er nogle                    
                    var loebereMedSammeTid = matchloebere.Where(pair => pair.Value.Tid == l.Tid);

                    foreach (var samme in loebereMedSammeTid)
                    {
                        bool tildel = false;
                        Loeber ll = samme.Value;

                        // afgør om en løber er kandidat til at få point
                        if (ll.Klub == match.Klub1)
                        {
                            if (loebereK1.Count < antal)
                            {
                                // fra klub1 og under antal har fået tildelt point                            
                                tildel = true;
                            }
                        }
                        else if (ll.Klub == match.Klub2)
                        {
                            if (loebereK2.Count < antal)
                            {
                                // fra klub2 og under antal har fået tildelt point                            
                                tildel = true;
                            }
                        }

                        if (tildel)
                        {
                            // hvis der er points at tildele så gør det
                            if (n < point.Length)
                            {
                                p = point[n];
                            }
                            else
                            {
                                p = 0.0;
                            }

                            // giv antal point
                            ll.SetPoint(match, p);

                            // noter hvem der har fået point
                            toploebere.Add(samme.Key, samme.Value);

                            if (ll.Klub == match.Klub1)
                            {
                                loebereK1.Add(ll);
                            }
                            else
                            {
                                loebereK2.Add(ll);
                            }

                            n++;
                        }
                    }
                }
            }

            // fordel point for pointgivende løbere med samme tid
            // løbere med samme tid på tværs af klubber, skal dele de point som
            // en eller flere har fået jvf placering (som vil være tilfældig da de alle har samme tid)
            // loop over løbere som er sorteret på tid
            // for hver ny tid
            // hvis en løber har samme tid som den foregående startes en beregning 
            // for alle løbere med denne tid. alle deres point summeres og fordeles på antallet af løbere
            tider.Clear();
            foreach (Loeber l in toploebere.Values)
            {
                // lav kun beregningen 1 gang for hver tid
                if (!tider.Contains(l.Tid))
                {
                    tider.Add(l.Tid);

                    // find alle løbere med denne tid og foretag beregning hvis der er nogle
                    var loebereMedSammeTid = toploebere.Where(kv => kv.Value.Tid == l.Tid);
                    int cntP = loebereMedSammeTid.Count();
                    if (cntP > 1)
                    {
                        // summer point tildelt for dene match
                        double sumP = 0;
                        foreach (KeyValuePair<string, Loeber> kv in loebereMedSammeTid)
                        {
                            sumP += kv.Value.GetPoint(match);
                        }

                        // fordel points på alle med samme match
                        foreach (KeyValuePair<string, Loeber> kv in loebereMedSammeTid)
                        {
                            kv.Value.SetPoint(match, sumP / cntP);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
