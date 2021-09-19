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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Divisionsmatch
{
    /// <summary>
    /// klasse for et stævne
    /// </summary>
    public class Staevne : IDisposable
    {
        #region member variables
        private List<Klub> _klubber;
        private List<Match> _matcher;
        private List<Gruppe> _grupper;
        private SortedList<string, Loeber> _loebere;
        private Config _config;
        private string tempFolder = string.Empty;

        private string _printOutput = string.Empty;
        private string _productVersion = string.Empty;

        // Flag: Has Dispose already been called? 
        private bool disposed = false;
        #endregion

        #region constuctors
        /// <summary>
        /// Lav et tomt stævne
        /// </summary>
        /// <param name="Productversion">versionen af programmet</param>
        public Staevne(string Productversion)
        {
            _productVersion = Productversion;

            Config = new Config(true);

            // make temp folder for this session
            tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempFolder);
        }
        #endregion

        #region static methods

        /// <summary>
        /// find ud af kombinationerne af klubber som løber mod hinanden
        /// </summary>
        /// <param name="staevne">stævnet det handler om</param>
        /// <param name="klubber">listen af klubber i stævnet</param>
        /// <returns>liste af matcher</returns>
        public static IList<Match> GetMatcher(Staevne staevne, IList<Klub> klubber)
        {
            return makeMatcher(staevne, klubber);
        }

        private static IList<Match> makeMatcher(Staevne staevne, IList<Klub> klubber)
        {
            IList<Match> matcher = new List<Match>();
            for (int k = 0; k < klubber.Count; k++)
            {
                for (int l = k + 1; l < klubber.Count; l++)
                {
                    matcher.Add(new Match(staevne, klubber[k], klubber[l]));
                }
            }

            return matcher;
        }
        #endregion

        #region public properties
        /// <summary>
        /// nummer på divisionen
        /// </summary>
        public int division
        {
            get
            {
                return _config.Division;
            }
        }

        /// <summary>
        /// listen af klubber i stævnet
        /// </summary>
        public List<Klub> Klubber
        {
            get
            {
                if (_klubber == null)
                {
                    _klubber = new List<Klub>();
                }
                return _klubber;
            }
        }

        /// <summary>
        /// listen af matcher i stævnet
        /// </summary>
        public List<Match> Matcher
        {
            get
            {
                if (_matcher == null)
                {
                    _matcher = new List<Match>();
                }
                return _matcher;
            }
        }

        /// <summary>
        ///  listen af grupper i stævnet
        /// </summary>
        public List<Gruppe> Grupper
        {
            get
            {
                if (_grupper == null)
                {
                    _grupper = new List<Gruppe>();
                }
                return _grupper;
            }
        }

        /// <summary>
        /// løberne i stævnet sorteret efter klub
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

        /// <summary>
        /// configurationen af divisionsmatchprogrammet
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
        /// få en list af klubber sorteret efter deres placering i stævnet
        /// </summary>
        public List<Klub> KlubberEfterPlacering
        {
            get
            {
                foreach (Klub k in Klubber)
                {
                    int p = 0;
                    double score1 = 0.0;
                    double score2 = 0.0;
                    string ude = string.Empty;
                    foreach (Match m in Matcher)
                    {
                        if (m.HarKlub(k))
                        {
                            double mp1 = (m.Klub1 == k) ? m.Score1() : m.Score2();
                            double mp2 = (m.Klub1 == k) ? m.Score2() : m.Score1();

                            // summer scores - 1 for egen, 2 for andres
                            score1 += mp1;
                            score2 += mp2;

                            // tildeler point 0-1-2
                            p += (m.Klub1 == k) ? m.Matchpoint1(Loebere) : m.Matchpoint2(Loebere);
                        }
                    }

                    k.Point = p;
                    k.Score1 = score1;
                    k.Score2 = score2;
                }

                // tjek om der er matchpoint lighed - i så fald skal man vurdere hvilken klub som har flest løbspoint
                // blandt klubber med ens point
                IList<int> ensPoint = new List<int>();
                int oldP = -1;
                foreach (var kp in Klubber.OrderByDescending(k => k.Point))
                {
                    if (kp.Point == oldP && !ensPoint.Contains(kp.Point))
                    {
                        ensPoint.Add(kp.Point);
                    }
                    oldP = kp.Point;
                }

                // for hver point som har flere klubber skal man
                // finde sum af klubbens løbspoint mod de andre klubber med samme point
                SortedList<string, string> stilling = new SortedList<string, string>();
                foreach (Klub k1 in Klubber)
                {
                    double score1 = 0;
                    if (ensPoint.Contains(k1.Point))
                    {
                        foreach (Klub k2 in Klubber.Where(k => k.Point == k1.Point && !k.Equals(k1)))
                        {
                            foreach (Match m in Matcher)
                            {
                                if (m.HarKlub(k1) && m.HarKlub(k2))
                                {
                                    double mp1 = (m.Klub1 == k1) ? m.Score1() : m.Score2();

                                    // summer scores - 1 for egen, 2 for andres
                                    score1 += mp1;
                                }
                            }
                        }

                        // noter egenscore
                        k1.Kommentar = "(" + score1.ToString() + ")";
                    }
                    else
                    {
                        score1 = k1.Score1;
                    }

                    k1.EgenScore = score1;
                }

                // arrangere klubber efter point og egenscore - og rank så dem som er ens efter antal placeringer
                List<Klub> klubberEfterScore = Klubber.OrderByDescending(item => item.EgenScore).OrderByDescending(item => item.Point).ToList();
                List<Klub> klubberEfterPlacering = new List<Klub>();
                int i = 0;
                while (i < klubberEfterScore.Count)
                {
                    Klub k = klubberEfterScore[i];
                    var kk = Klubber.Where(k1 => k1.Point.Equals(k.Point) && k1.EgenScore.Equals(k.EgenScore));
                    if (kk.Count() > 1)
                    {
                        klubberEfterPlacering.AddRange(_rankPlaceringer(1, kk.OrderByDescending(item => item.EgenScore).ToList()));
                        i += kk.Count();
                    }
                    else
                    {
                        klubberEfterPlacering.Add(k);
                        i++;
                    }
                }

                return klubberEfterPlacering;
            }
        }

        /// <summary>
        /// Stævnets dato
        /// </summary>
        public DateTime Dato
        {
            get
            {
                return Config.Dato;
            }
            set
            {
                Config.Dato = value;
            }
        }
        #endregion

        #region public methods

        /// <summary>
        /// beregn point i stævnet
        /// </summary>
        /// <param name="config">configurationen der skal bruges</param>
        /// <param name="filnavn">sti til resultat filen</param>
        public virtual void Beregnpoint(Config config, string filnavn)
        {
            _load(config);
            Beregnpoint(filnavn);
        }

        /// <summary>
        /// beregn for dette stævne (config er indlæst)
        /// </summary>
        /// <param name="filnavn">stil til resultat filen</param>
        public virtual void Beregnpoint(string filnavn)
        {
            // reset output
            _printOutput = string.Empty;

            // xml eller csv?
            string version = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            bool isCSV = false;
            bool isTxt = false;
            bool isEntryXml = false;
            bool isStartXml = false;
            bool isResultXml = false;
            string fileVersion = "";
            try
            {
                while (IsFileLocked(filnavn))
                {
                    System.Diagnostics.Debug.Print(DateTime.Now.ToString() + " locked");
                }

                fileVersion = Util.CheckFileVersion(filnavn, out isEntryXml, out isStartXml, out isResultXml, out isTxt);
                if (fileVersion == "csv")
                {
                    isCSV = true;
                }
                else if (!isResultXml)
                {
                    throw new Exception("Dette er vist ikke en XML ResultList");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Fejl ved load af XML: " + e.Message);
            }

            IList<Loeber> alleloebere = null;
            if (isCSV)
            {
                alleloebere = Util.ReadRunnersFromCsv(filnavn, Config, Klubber);
            }
            else
            {
                // load fra XML
                if (fileVersion == "xml3")
                {
                    alleloebere = Util.ReadRunnersFromResultXML3(filnavn, Config, Klubber);
                }
                else
                {
                    alleloebere = Util.ReadRunnersFromResultXML2(filnavn, Config, Klubber);
                }
            }

            Loebere.Clear();
            foreach (Gruppe g in Grupper)
            {
                g.Loebere.Clear();
            }

            foreach (Loeber l in alleloebere)
            {
                // hvis løberen er fra en klub i divisionsmatchen
                // bool bKlub = Klubber.Exists(k => l.Klub != null && k.Navn.ToLowerInvariant().Equals(l.Klub.Navn.ToLowerInvariant()));
                bool bKlub = Klubber.Exists(k => l.Klub != null && k.Equals(l.Klub));
                // placer løberen i løbet og i en gruppe
                Gruppe g = Grupper.FirstOrDefault(grp => grp.Harløbsklasse(l.Løbsklassenavn));
                if (g != null)
                {
                    l.Inkl = bKlub; // er løberen i matchen                    

                    // og registrer løberen til gruppen
                    g.Loebere.Add(l.NumStatus + " - " + l.Tid + " - " + this.Loebere.Count.ToString().PadLeft(5), l);
                }

                // tilføj løber til løebet
                // 18 ~ "kort", 15 ~ klub
                this.Loebere.Add(l.NumStatus + " - " + l.Tid + " - " + this.Loebere.Count.ToString().PadLeft(5), l);
            }

            // for hver gruppe - fordel point
            foreach (Gruppe g in Grupper)
            {
                foreach (Match m in Matcher)
                {
                    g.FordelPoint(m);
                }
            }

            // raise event
            OnBeregningSlut(new EventArgs());
        }

        /// <summary>
        /// formatter stævnets resultatet som HTML eller txt
        /// </summary>
        /// <param name="html">er formatet HTML (true) eller txt(false)</param>
        /// <returns></returns>
        public string Printstilling(bool html)
        {
            StringBuilder output = new StringBuilder();

            ////klub                                     :     score      point
            ////Allerød OK                               :   325 -   283      4
            ////OK Midtvest                              :   190 -   378      0
            ////OK Roskilde                              :   307 -   296      2
            ////OK Øst Birkerød                          :   369 -   234      6
            if (!html)
            {
                string line = "Divisionsmatch (" + this.Config.Skov + ", " + this.Config.Dato.ToString("yyyy-MM-dd") + ")";
                output.AppendLine(line);
                output.AppendLine(new string('-', line.Length));
                output.AppendLine("".PadRight(40) + "   " + "    score      point");
            }
            else
            {
                if (_config.Layout == "Standard")
                {
                    output.AppendLine("<h3 class=\"stilling\">Divisionsmatch (" + this.Config.Skov + ", " + this.Config.Dato.ToString("yyyy-MM-dd") + ")</h3>");
                    output.AppendLine("<table class=\"stilling\">");
                    output.AppendLine("<tbody class=\"stilling\">");
                    output.AppendLine("<tr class=\"stilling\"><td>&nbsp;</td><td colspan=3>score</td><td>point</td><td>&nbsp;</td></tr>");
                }
                else if (_config.Layout == "Blå overskrifter")
                {
                    output.AppendLine("<div class=\"stillingContainer\">");
                    output.AppendLine("<div class=\"stillingHeader\">Divisionsmatch (" + this.Config.Skov + ", " + this.Config.Dato.ToString("yyyy-MM-dd") + ")</div>");
                    output.AppendLine("<div class=\"stilling\">");
                    output.AppendLine("<table class=\"stilling\">");
                    output.AppendLine("<thead>");
                    output.AppendLine("<tr><th class=\"knavn\">Klubnavn</th><th colspan=3 style=\"text-align:center\">score</th><th>point</th><th>&nbsp;</th></tr>");
                    output.AppendLine("</thead>");
                    output.AppendLine("<tbody>");
                }
            }

            foreach (Klub k in KlubberEfterPlacering)
            {
                string ude = k.Udeblevet ? "(udeblevet)" : string.Empty;

                if (html)
                {
                    if (_config.Layout == "Standard")
                    {
                        output.AppendLine("<tr class=\"stilling\"><td>" + k.Navn.PadRight(40) + "</td><td>" + k.Score1.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(5) + "</td><td>-</td><td>" + k.Score2.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(5) + "</td><td style=\"text-align:right\">" + k.Point.ToString("##0").PadLeft(5) + "</td><td style=\"text-align:left\">" + k.Kommentar + "&nbsp;" + ude + "</td></tr>");
                    }
                    else if (_config.Layout == "Blå overskrifter")
                    {
                        output.AppendLine("<tr><td class=\"knavn\">" + k.Navn + "</td><td>" + k.Score1.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo) + "</td><td>-</td><td>" + k.Score2.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo) + "</td><td>" + k.Point.ToString("##0") + "</td><td style=\"text-align:left\">" + k.Kommentar + "&nbsp;" + ude + "</td></tr>");
                    }
                }
                else
                {
                    output.AppendLine(k.Navn.PadRight(40) + "   " + k.Score1.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(5) + " - " + k.Score2.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(5) + "  " + k.Point.ToString("##0").PadLeft(5) + "   " + k.Kommentar + " " + ude);
                }
            }

            if (html)
            {
                if (_config.Layout == "Standard")
                {
                    output.AppendLine("</tbody></table>");
                }
                else if (_config.Layout == "Blå overskrifter")
                {
                    output.AppendLine("</tbody></table>");
                    output.AppendLine("</div></div>");
                }
            }
            return output.ToString();
        }

        /// <summary>
        /// formatter hele resultatlisten som txt
        /// </summary>
        /// <returns>hele resultatet som txt</returns>
        public string Printmatcher()
        {
            if (_printOutput != string.Empty)
            {
                return _printOutput;
            }
            else
            {
                StringBuilder output = new StringBuilder();

                // lav stilling
                output.AppendLine(Printstilling(false));

                int n = 1;
                int maxL = 0;
                foreach (Match m in Matcher)
                {
                    int L = (n.ToString() + " : " + m.Klub1.Navn + " - " + m.Klub2.Navn).PadRight(40).Length;
                    if (L > maxL)
                    {
                        maxL = L;
                    }
                    n++;
                }
                n = 1;
                foreach (Match m in Matcher)
                {
                    output.Append(((n.ToString() + " : " + m.Klub1.Navn + " - " + m.Klub2.Navn).PadRight(maxL) + " : "));
                    double p1 = m.Score1();
                    double p2 = m.Score2();

                    output.AppendLine(p1.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(5) + " - " + p2.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(5));

                    n++;
                }

                output.AppendLine();

                // first calculate the width of each column
                List<int> p1widths = new List<int>();
                List<int> p2widths = new List<int>();
                int mm = 0;
                n = 0;
                foreach (Gruppe g in Grupper)
                {
                    mm = 0;
                    foreach (Match m in Matcher)
                    {
                        // double p1 = g.loebere.Where(item => item.Value.klub == m.klub1).Sum(item => item.Value.GetSumPoint(m));
                        // double p2 = g.loebere.Where(item => item.Value.klub == m.klub2).Sum(item => item.Value.GetSumPoint(m));

                        double p1 = m.Loebspoint1(g.Loebere);
                        double p2 = m.Loebspoint2(g.Loebere);

                        int o1 = p1.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).Length;
                        int o2 = p2.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).Length;

                        if (n == 0)
                        {
                            p1widths.Add(o1);
                            p2widths.Add(o2);
                        }
                        else
                        {
                            p1widths[mm] = p1widths[mm] < o1 ? o1 : p1widths[mm];
                            p2widths[mm] = p2widths[mm] < o2 ? o2 : p2widths[mm];
                        }
                        mm++;
                    }
                    n++;
                }

                // og for 'ialt'
                mm = 0;
                foreach (Match m in Matcher)
                {
                    double p1 = m.Loebspoint1(this.Loebere);
                    double p2 = m.Loebspoint2(this.Loebere);

                    int o1 = p1.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).Length;
                    int o2 = p2.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).Length;

                    if (n == 0)
                    {
                        p1widths.Add(o1);
                        p2widths.Add(o2);
                    }
                    else
                    {
                        p1widths[mm] = p1widths[mm] < o1 ? o1 : p1widths[mm];
                        p2widths[mm] = p2widths[mm] < o2 ? o2 : p2widths[mm];
                    }
                    mm++;
                }

                // make match numbers
                int w = 0;
                foreach (Gruppe g in Grupper)
                {
                    int wx = g.navn.PadRight(6).Length; 
                    if (wx > w) w = wx;
                }

                output.Append("".PadRight(w, ' '));
                n = 1;
                mm = 0;
                foreach (Match m in Matcher)
                {
                    int w1 = p1widths[mm];
                    int w2 = p2widths[mm];
                    output.Append("m".PadLeft(w1) + n.ToString().PadRight(w2) + "  ");
                    mm++;
                    n++;
                }
                output.AppendLine();

                // print underline
                output.Append("".PadRight(w, ' '));
                mm = 0;
                foreach (Match m in Matcher)
                {
                    int w1 = p1widths[mm];
                    int w2 = p2widths[mm];
                    output.Append(" ".PadLeft(w1 + w2 + 1 + 1, '-'));
                    mm++;
                }
                output.AppendLine();

                // print data
                foreach (Gruppe g in Grupper)
                {
                    mm = 0;
                    output.Append(g.navn.PadRight(w)); 
                    foreach (Match m in Matcher)
                    {
                        int w1 = p1widths[mm];
                        int w2 = p2widths[mm];

                        //double p1 = g.loebere.Where(item => item.Value.klub == m.klub1).Sum(item => item.Value.GetSumPoint(m));
                        //double p2 = g.loebere.Where(item => item.Value.klub == m.klub2).Sum(item => item.Value.GetSumPoint(m));
                        double p1 = m.Loebspoint1(g.Loebere);
                        double p2 = m.Loebspoint2(g.Loebere);

                        output.Append(p1.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(w1) + "-" + p2.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadRight(w2) + " ");
                        mm++;
                    }
                    output.AppendLine();
                }

                // tilføj ialt:
                mm = 0;
                output.Append("Ialt:".PadRight(w, ' '));
                foreach (Match m in Matcher)
                {
                    int w1 = p1widths[mm];
                    int w2 = p2widths[mm];

                    //double p1 = g.loebere.Where(item => item.Value.klub == m.klub1).Sum(item => item.Value.GetSumPoint(m));
                    //double p2 = g.loebere.Where(item => item.Value.klub == m.klub2).Sum(item => item.Value.GetSumPoint(m));
                    double p1 = m.Loebspoint1(this.Loebere);
                    double p2 = m.Loebspoint2(this.Loebere);

                    output.Append(p1.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(w1) + "-" + p2.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadRight(w2) + " ");
                    mm++;
                }
                output.AppendLine();


                return output.ToString();
            }
        }

        /// <summary>
        /// returner en lista af stillinger i txt per gruppe eller bane til brug ved sammenlingning
        /// </summary>
        /// <param name="config">configurationen som definerer hvad der skal laves</param>
        /// <returns></returns>
        public List<string> LavTXTafsnit()
        {
            List<string> output = new List<string>();

            if (!Config.PrintBaner)
            {
                foreach (Gruppe g in Grupper)
                {
                    var lg = g.Loebere.Where(l => l.Value.Inkl == true || Config.PrintAlle);
                    if (lg.Count() > 0 || Config.PrintAlleGrupper)
                    {
                        output.Add(g.LavTXTOverskrift() + txtTable(g.Loebere.Where(l => l.Value.Inkl == true || Config.PrintAlle).Select(ll => ll.Value).ToList()));
                    }
                }
            }
            else
            {
                foreach (Bane b in Config.baner.OrderBy(bb => bb.Navn))
                {
                    var kl = Config.classes.Where(k => k.Bane != null && k.Bane.Navn.Equals(b.Navn)).Select(kk => kk.Navn);
                    // find løbere på samme bane - og vælg dem i matchen, eller alle
                    var lll = Loebere.Where(l => kl.Contains(l.Value.Løbsklassenavn) && (l.Value.Inkl == true || Config.PrintAlle)).Select(ll => ll.Value).ToList();
                    if (lll.Count > 0 || Config.PrintAlleGrupper)
                    {
                        output.Add(b.LavTXToverskrift() + txtTable(lll));
                    }
                }
            }
            return output;
        }

        /// <summary>
        /// lav en liste af HTML formatterede afsnit tilburg ved sammenligning
        /// </summary>
        /// <returns>liste af html strings</returns>
        public List<string> LavHTMLafsnit()
        {
            List<string> htmlSections = new List<string>();

            if (!Config.PrintBaner)
            {
                // gruppe detalje resultater
                foreach (Gruppe g in Grupper)
                {
                    var lll = g.Loebere.Where(l => l.Value.Inkl == true || Config.PrintAlle);
                    if (lll.Count() > 0 || Config.PrintAlleGrupper)
                    {
                        StringBuilder html = new StringBuilder();
                        if (_config.Layout == "Blå overskrifter")
                            html.AppendLine("<div class=\"gruppe\">");
                        html.AppendLine(g.LavHTMLoverskrift(Config.Layout));
                        html.AppendLine(htmlTable(g.navn, g.Loebere.Where(l => l.Value.Inkl == true || Config.PrintAlle).Select(ll => ll.Value).ToList()));
                        if (_config.Layout == "Blå overskrifter")
                            html.AppendLine("</div>");

                        htmlSections.Add(html.ToString());
                    }
                }
            }
            else
            {
                foreach (Bane b in Config.baner.OrderBy(bb => bb.Navn))
                {
                    StringBuilder html = new StringBuilder();

                    var kl = Config.classes.Where(k => k.Bane != null && k.Bane.Navn.Equals(b.Navn)).Select(kk => kk.Navn);
                    // find løbere på samme bane - og vælg dem i matchen, eller alle
                    var lll = Loebere.Where(l => kl.Contains(l.Value.Løbsklassenavn) && (l.Value.Inkl == true || Config.PrintAlle)).Select(ll => ll.Value).ToList();
                    if (lll.Count > 0 || Config.PrintAlleGrupper)
                    {
                        html.AppendLine(b.LavHTMLoverskrift());
                        html.AppendLine(htmlTable(b.Navn, lll));

                        htmlSections.Add(html.ToString());
                    }
                }
            }

            return htmlSections;
        }

        /// <summary>
        /// lav HTML head sektion
        /// </summary>
        /// <returns>html string</returns>
        public string GetHTMLHead()
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<head><title> Divisionsmatch v " + _productVersion + " - (" + DateTime.Now.ToString("yyyy - MM - dd HH: mm: ss") + ")</title> ");
            html.AppendLine("<meta content=\"text/html;charset=utf-8\" http-equiv=\"Content-Type\">");

            string cssFile = GetHTMLStyle();
            var style =
            html.AppendLine("<!-- make default style in case css-file is missing -->");
            html.AppendLine("<style>");
            html.AppendLine(File.ReadAllText(cssFile, Encoding.UTF8));
            html.AppendLine("</style>");
            html.AppendLine("<link rel='stylesheet' href='" + Path.GetFileName(cssFile) + "'/>");
            html.AppendLine("</head>");
            return html.ToString();
        }

        /// <summary>
        /// Lav et afsnit i HTML
        /// </summary>
        /// <param name="sections">de sektioner som skal indgå i HTML</param>
        /// <returns>html string</returns>
        public string LavHTML(List<string> sections)
        {
            StringBuilder html = new StringBuilder();

            //html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html lang=\"da\">");
            html.AppendLine(GetHTMLHead());
            html.AppendLine("<body>");

            bool bFirst = true;
            foreach (string s in sections)
            {
                if (!bFirst)
                {
                    if (_config != null && _config.SideSkift)
                    {
                        html.AppendLine("<div class='page-break'></div>");
                    }
                }
                else
                {
                    bFirst = false;
                }

                html.AppendLine(s);
            }

            html.AppendLine("</body></html>");

            string targetFilePath = Path.Combine(tempFolder, Guid.NewGuid().ToString("N") + "_divi.htm");
            try
            {
                File.WriteAllText(targetFilePath, html.ToString(), Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine("HTML filen " + targetFilePath + " kunne ikke skrives: " + e.Message);
            }

            return new Uri(targetFilePath).AbsoluteUri;
        }
        
        /// <summary>
        /// returner CSS tag
        /// </summary>
        /// <returns>string med CSS style eller tag</returns>
        public string GetHTMLStyle()
        {
            string cssFile = string.Empty;
            string style = string.Empty;

            if (string.IsNullOrEmpty(_config.CssFile) || !File.Exists(_config.CssFileFullPath))
            {
                if (string.IsNullOrEmpty(_config.Layout) || _config.Layout == "Standard")
                {
                    string fontName = _config != null ? _config.font.FontValue.Name : "Courier New";
                    string fontSize = _config != null ? _config.font.FontValue.SizeInPoints.ToString() : "10";
                    bool bold = _config != null ? _config.font.FontValue.Bold : false;
                    bool italic = _config != null ? _config.font.FontValue.Italic : false;
                    bool strike = _config != null ? _config.font.FontValue.Strikeout : false;
                    bool underline = _config != null ? _config.font.FontValue.Underline : false;

                    style = @"table, h3, body {font-family:" + fontName + @";font-size:" + fontSize + @"pt";
                    if (bold)
                    {
                        style += @";font-weight:bold";
                    }
                    if (italic)
                    {
                        style += @";font-style:italic";
                    }
                    if (underline || strike)
                    {
                        style += @";text-decoration:" + (underline ? "underline" : "normal") + " normal " + (strike ? "line-through" : "normal") + " normal";
                    }
                    style += @"}" + Environment.NewLine;

                    style += @"th, td {padding: 2px;} " + Environment.NewLine;
                    style += @"h3 {font-size: larger;} " + Environment.NewLine;
                    style += @"tr {page-break-inside: avoid;} " + Environment.NewLine;
                    style += @"th.bane, th.matchgruppe, td.matchgruppe, td.bane {border-bottom: solid lightgrey 1px;border-left: solid lightgrey 1px;}   " + Environment.NewLine;
                    style += @".point {font-size:8pt}" + Environment.NewLine;
                    style += @".page-break {page-break-before:always;}" + Environment.NewLine;
                    style += @"thead { display:table-header-group;}" + Environment.NewLine;
                    style += @"tbody { display:table-row-group; } " + Environment.NewLine;

                    style = "@media print, screen {" + Environment.NewLine + style + "}";
                    //// style += "<script>setTimeout(\"document.body.style.zoom=1.5\", 500)</script>";
                }
                else if (_config.Layout == "Blå overskrifter")
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("divi.css"));
                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        style = reader.ReadToEnd();
                    }
                }
                cssFile = "divi.css";
                string targetFilePath = Path.Combine(tempFolder, cssFile);
                try
                {
                    File.WriteAllText(targetFilePath, style);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("CSS filen " + targetFilePath + " kunne ikke skrives: " + ex.Message);
                }
                ////tag = "<link rel='stylesheet' media='print,screen' type='text/css' href='" + targetFileName + "'/>";
                cssFile = targetFilePath;
            }   
            else
            {
                string targetFileName = Path.GetFileName(_config.CssFileFullPath);
                string targetFilePath = Path.Combine(tempFolder, targetFileName);
                try
                {
                    File.Copy(_config.CssFileFullPath, targetFilePath, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("CSS filen " + targetFilePath + " kunne ikke skrives: " + ex.Message);
                }

                cssFile = targetFilePath;
            }

            return cssFile;
        }        
        #endregion

        #region private methods
        public string LavHTMLStilling(Config config)
        {
            StringBuilder html = new StringBuilder();
            if (_config.Layout == "Standard")
            {
                html.AppendLine("<p>");

                html.AppendLine("<table class=\"matcher\">");
                html.AppendLine("<tbody class=\"matcher\">");
            }
            else if (_config.Layout == "Blå overskrifter")
            {
                html.AppendLine("<div class=\"matcher\">");
                html.AppendLine("<table class=\"matcher\">");
                html.AppendLine("<tbody>");
            }

            int n = 1;
            foreach (Match m in Matcher)
            {
                if (_config.Layout == "Standard")
                {
                    html.Append("<tr class=\"matcher\"><td>" + n.ToString() + "</td><td> : </td><td>" + m.Klub1.Navn + "</td><td> - </td><td>" + m.Klub2.Navn + "</td><td> : </td><td style=\"text-align:right\">");
                }
                else if (_config.Layout == "Blå overskrifter")
                {
                    html.Append("<tr><td>" + n.ToString() + "</td><td> : </td><td class=\"knavn\">" + m.Klub1.Navn + "</td><td> - </td><td class=\"knavn\">" + m.Klub2.Navn + "</td><td> : </td><td style=\"text-align:right\">");
                }

                //double p1 = loebere.Where(item => item.Value.klub == m.klub1).Sum(item => item.Value.GetSumPoint(m));
                //double p2 = loebere.Where(item => item.Value.klub == m.klub2).Sum(item => item.Value.GetSumPoint(m));

                double p1 = m.Score1();
                double p2 = m.Score2();

                html.AppendLine(p1.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo) + "</td><td> - </td><td style=\"text-align:right\">" + p2.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo) + "</td></tr>");

                n++;
            }
            html.AppendLine("</tbody></table>");
            if (_config.Layout == "Blå overskrifter")
            {
                html.AppendLine("</div>");
                html.AppendLine("</div>");
            }
            // matcherneper gruppe
            // make match numbers
            if (_config.Layout == "Standard")
            {
                html.AppendLine("<p><table class=\"matchgruppe\">");
                html.AppendLine("<thead class=\"matchgruppe\">");
                html.Append("<tr class=\"matchgruppe\"><th>&nbsp;</th>");
            }
            else if (_config.Layout == "Blå overskrifter")
            {
                html.AppendLine("<div class=\"matchgruppe\">");
                html.AppendLine("<div class=\"matchgruppeHeader\">Klasse oversigt</div>");
                html.AppendLine("<table class=\"matchgruppe\">");
                html.AppendLine("<thead>");
                html.Append("<tr><th>&nbsp;</th>");
            }

            n = 1;
            foreach (Match m in Matcher)
            {
                if (_config.Layout == "Standard")
                {
                    html.Append("<th class='matchgruppe' >m" + n.ToString() + "</th>");
                }
                else if (_config.Layout == "Blå overskrifter")
                {
                    html.Append("<th>m" + n.ToString() + "</th>");
                }
                n = n + 1;
            }
            html.AppendLine("</tr>");
            if (_config.Layout == "Standard")
            {
                html.AppendLine("</thead><tbody class=\"matchgruppe\">");
            }
            else if (_config.Layout == "Blå overskrifter")
            {
                html.AppendLine("</thead><tbody>");
            }
            // print data
            foreach (Gruppe g in Grupper)
            {
                if (_config.Layout == "Standard")
                {
                    html.Append("<tr class=\"matchgruppe\"><td class='matchgruppe'>" + g.navn);
                }
                else if (_config.Layout == "Blå overskrifter")
                {
                    html.Append("<tr><td class=\"bnavn\">" + g.navn);
                }
                html.Append("</td>");

                foreach (Match m in Matcher)
                {
                    //double p1 = g.loebere.Where(item => item.Value.klub == m.klub1).Sum(item => item.Value.GetSumPoint(m));
                    //double p2 = g.loebere.Where(item => item.Value.klub == m.klub2).Sum(item => item.Value.GetSumPoint(m));
                    double p1 = m.Loebspoint1(g.Loebere);
                    double p2 = m.Loebspoint2(g.Loebere);
                    ////if (m.harUdeblevne)
                    ////{
                    ////    // halver point antallet
                    ////    p1 /= 2;
                    ////    p2 /= 2;
                    ////}

                    if (_config.Layout == "Standard")
                    {
                        html.Append("<td class='matchgruppe'>" + p1.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo) + "-" + p2.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo) + "</td>");
                    }
                    else if (_config.Layout == "Blå overskrifter")
                    {
                        html.Append("<td>" + p1.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo) + "-" + p2.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo) + "</td>");
                    }
                }
                html.AppendLine("</tr>");
            }
            // for 'ialt'
            if (_config.Layout == "Standard")
            {
                html.Append("<tr class=\"matchgruppe\"><td class='matchgruppe'>Ialt:");
            }
            else if (_config.Layout == "Blå overskrifter")
            {
                html.Append("<tr><td>Ialt:");
            }
            html.Append("</td>");
            foreach (Match m in Matcher)
            {
                double p1 = m.Loebspoint1(this.Loebere);
                double p2 = m.Loebspoint2(this.Loebere);
                if (_config.Layout == "Standard")
                {
                    html.Append("<td class='matchgruppe'>" + p1.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo) + "-" + p2.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo) + "</td>");
                }
                else if (_config.Layout == "Blå overskrifter")
                {
                    html.Append("<td>" + p1.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo) + "-" + p2.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo) + "</td>");
                }
            }
            html.AppendLine("</tr>");

            html.AppendLine("</tbody></table>");
            if (_config.Layout == "Blå overskrifter")
            {
                html.AppendLine("</div>");
            }
            return html.ToString();
        }

        private void _load(Config config)
        {
            // clear det hele
            _klubber = null;
            _grupper = null;
            _matcher = null;

            _config = config;

            foreach (var k in _config.Klubber)
            {
                if (config.udeblevneKlubber.Contains(k))
                {
                    k.Udeblevet = true;
                }
                Klubber.Add(k);
            }

            foreach (GruppeOgKlasse gk in _config.gruppeOgKlasse) //.Where(item => item.LøbsKlasse.Trim() != "-" && item.LøbsKlasse.Trim() != string.Empty))
            {
                if (gk.LøbsKlasse != null && gk.LøbsKlasse.Trim() != "-" && gk.LøbsKlasse.Trim() != string.Empty)
                {
                    Gruppe g = Grupper.Find(item => item.navn == gk.Gruppe);
                    if (g == null)
                    {
                        // lav gruppen
                        g = new Gruppe(gk.Gruppe, _config.Division);
                        Grupper.Add(g);
                    }

                    // tilføj klasse definitionen
                    if (!g.Klasser.Exists(k => k.LøbsKlasse != null && k.LøbsKlasse.Navn.Equals(gk.LøbsKlasse)))
                    {
                        Klasse kk = _config.classes.Find(c => c.Navn == gk.LøbsKlasse);
                        g.Klasser.Add(new Klasseconfig(gk.Klasse, kk));
                    }
                }
            }

            Matcher.AddRange(makeMatcher(this, Klubber));
        }

        private string htmlTable(string navn, IList<Loeber> loebere)
        {
            StringBuilder output = new StringBuilder();

            if (_config.Layout == "Standard")
            {
                output.AppendLine("<table class=\"bane\" id=\"table-" + navn + "\">");
                output.Append("<thead class=\"bane\"><tr class=\"bane\"><th class='bane'>pl</th><th class='bane'>navn</th><th class='bane'>klub</th><th class='bane'>klasse</th><th class='bane'>tid</th>");
            }
            else if (_config.Layout == "Blå overskrifter")
            {
                output.AppendLine("<table class=\"gruppe\">");
                output.Append("<thead><tr><th>pl</th><th class=\"lnavn\">navn</th><th class=\"knavn\">klub</th><th>klasse</th><th>tid</th>");
            }

            for (int i = 1; i <= Matcher.Count; i++)
            {
                if (_config.Layout == "Standard")
                {
                    output.Append("<th class='bane'>m" + i.ToString() + "</th>");
                }
                else if (_config.Layout == "Blå overskrifter")
                {
                    output.Append("<th>m" + i.ToString() + "</th>");
                }
            }
            output.AppendLine("</tr></thead>");
            output.AppendLine("<tbody>");

            string oldTid = "";
            int pl = 0;
            int cnt = 0;
            foreach (var l in loebere)
            {
                if (l.PrintStatus != "Inaktiv")
                {
                    cnt++;
                    // Loeber l = kv.Value;
                    string line = string.Empty;
                    if (_config.Layout == "Standard")
                    {
                        line = "<tr class='bane'>";
                    }
                    else if (_config.Layout == "Blå overskrifter")
                    {
                        line = "<tr>";
                    }
                    if (l.IsStatusOK)
                    {
                        if (oldTid != l.Tid)
                        {
                            pl = cnt;
                        }
                        oldTid = l.Tid;
                    }
                    if (_config.Layout == "Standard")
                    {
                        line += "<td class='bane' style=\"text-align:right\">" + (l.IsStatusOK ? pl.ToString() : "&nbsp;") + "</td>";
                        line += "<td class='bane' style=\"white-space:nowrap\">" + l.Fornavn + " " + l.Efternavn + "</td>";
                        line += "<td class='bane' style=\"white-space:nowrap\">" + l.Klub.Navn + "</td>";
                        line += "<td class='bane'>" + l.Løbsklassenavn + "</td>";
                        line += "<td class='bane'>" + (l.IsStatusOK ? l.Tid : l.PrintStatus) + "</td>";
                    }
                    else if (_config.Layout == "Blå overskrifter")
                    {
                        line += "<td style=\"text-align:right\">" + (l.IsStatusOK ? pl.ToString() : "&nbsp;") + "</td>";
                        line += "<td class=\"lnavn\">" + l.Fornavn + " " + l.Efternavn + "</td>";
                        line += "<td class=\"knavn\" style=\"white-space:nowrap\">" + l.Klub.Navn + "</td>";
                        line += "<td>" + l.Løbsklassenavn + "</td>";
                        line += "<td>" + (l.IsStatusOK ? l.Tid : l.PrintStatus) + "</td>";
                    }
                    foreach (Match m in Matcher)
                    {
                        string up = l.GetUPoint(m) > 0 ? "*" : "&nbsp;";
                        double p = l.GetSumPoint(m);
                        if (_config.Layout == "Standard")
                        {
                            line += "<td class='bane' style=\"text-align:right\">" + (p > 0 ? p.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo) : "&nbsp;") + up + "</td>";
                        }
                        else if (_config.Layout == "Blå overskrifter")
                        {
                            line += "<td>" + (p > 0 ? p.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo) : "&nbsp;") + up + "</td>";
                        }
                    }

                    line += "</tr>";

                    output.AppendLine(line);
                }
            }

            output.AppendLine("</tbody>");
            output.AppendLine("</table>");

            return output.ToString();
        }

        private string txtTable(IList<Loeber> loebere)
        {
            StringBuilder output = new StringBuilder();

            output.Append("pl navn                    klub            klasse    tid  ");
            for (int i = 1; i <= Matcher.Count; i++)
            {
                output.Append(("m" + i.ToString()).PadLeft(5));
            }
            output.AppendLine();

            string oldTid = "";
            int pl = 0;
            int cnt = 0;
            foreach (var l in loebere) // print only included runners
            {
                if (l.PrintStatus != "Inaktiv")
                {
                    cnt++;
                    //Loeber l = kv.Value;
                    string line = string.Empty;
                    if (l.IsStatusOK)
                    {
                        if (oldTid != l.Tid)
                        {
                            pl = cnt;
                        }
                        oldTid = l.Tid;
                    }
                    line += (l.IsStatusOK ? pl.ToString().PadRight(3) : "   ");
                    line += (l.Fornavn + " " + l.Efternavn).PadRight(23).Substring(0, 23);
                    line += " ";
                    line += l.Klub.Navn.PadRight(15).Substring(0, 15);
                    line += " ";
                    line += l.Løbsklassenavn.PadRight(7).Substring(0, 6);
                    line += " ";
                    line += (l.IsStatusOK ? l.Tid : l.PrintStatus).PadLeft(8).Substring(0, 8);


                    string up = string.Empty;
                    foreach (Match m in Matcher)
                    {
                        double p = l.GetSumPoint(m);
                        if (up != string.Empty)
                        {
                            line += (p > 0) ? p.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(4) : p.ToString("#.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(4);
                        }
                        else
                        {
                            line += (p > 0) ? p.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(5) : p.ToString("#.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(5);
                        }
                        up = l.GetUPoint(m) > 0 ? "*" : string.Empty;
                        line += up;
                    }

                    output.AppendLine(line);
                }
            }

            output.AppendLine();

            return output.ToString();
        }

        private bool IsFileLocked(string file)
        {

            FileStream stream = null;

            try
            {
                stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        private List<Klub> _rankPlaceringer(int placering, List<Klub> klubber)
        {
            List<Klub> placeringKlub = new List<Klub>();

            Dictionary<Klub, int> taelPlacering = new Dictionary<Klub, int>();
            foreach (Gruppe g in Grupper)
            {
                var ll = g.Loebere.Where(l => klubber.Contains(l.Value.Klub) && l.Value.IsStatusOK);
                var kv = ll.ElementAtOrDefault(placering-1);
                if (kv.Key != null)
                {
                    if (taelPlacering.ContainsKey(kv.Value.Klub))
                    {
                        taelPlacering[kv.Value.Klub] += 1;
                    }
                    else
                    {
                        taelPlacering.Add(kv.Value.Klub, 1);
                    }
                }
            }

            // fordel klubber med disse placering
            foreach (int pl in taelPlacering.Values.Distinct().OrderByDescending(item => item))
            {
                var kk = taelPlacering.Where(kv => kv.Value == pl);
                if (kk.Count() > 1)
                {
                    // sorter disse klubber på næste placering og tilføj dem til resultatlisten
                    placeringKlub.AddRange(_rankPlaceringer((placering + 1), kk.Select(kv => kv.Key).ToList()));
                }
                else
                {
                    // tilføj klubben til resultatlisten
                    foreach (var k in kk.Select(kv => kv.Key))
                    {
                        k.Kommentar += ", " + pl.ToString() + " " + placering.ToString() + ". pl";
                    }
                    placeringKlub.AddRange(kk.Select(kv => kv.Key));
                }
            }

            // tilføj klubber uden denne placering i enden
            placeringKlub.AddRange(klubber.Where(k => !taelPlacering.ContainsKey(k)));

            return placeringKlub;
        }
        #endregion

        #region IDisposable
        /// Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                //
                try
                {
                    // delete tempFolder
                    if (Directory.Exists(tempFolder))
                    {
                        Directory.Delete(tempFolder, true);
                    }
                }
                catch
                {
                }
            }

            // Free any unmanaged objects here. 
            //
            disposed = true;
        }
        
        /// desctuctor
        ~Staevne()
        {
            Dispose(false);
        }
        #endregion
 
        #region events
        /// <summary>
        /// event som sende når beregningen er slut
        /// </summary>
        public event EventHandler BeregningSlut;
        #endregion

        #region eventhandlers
        /// <summary>
        /// event handler
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBeregningSlut(EventArgs e)
        {
            if (BeregningSlut != null)
            {
                BeregningSlut(this, e);
            }
        }
        #endregion
    }
}
