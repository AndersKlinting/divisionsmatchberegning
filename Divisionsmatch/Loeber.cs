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

namespace Divisionsmatch
{
    /// <summary>
    /// klasse eller struktur for en løber
    /// </summary>
    public class Loeber : IComparable
    {
        private Dictionary<Match, double> point = new Dictionary<Match, double>();
        private Dictionary<Match, double> upoint = new Dictionary<Match, double>();

        private string _status = string.Empty;

        /// <summary>
        /// startnummer
        /// </summary>
        public string Stnr { get; set; }

        /// <summary>
        /// løberens fornavn
        /// </summary>
        public string Fornavn { get; set; }

        /// <summary>
        /// efternavnet
        /// </summary>
        public string Efternavn { get; set; }

        /// <summary>
        /// løberens løbsklasse
        /// </summary>
        public string Løbsklassenavn { get; set; }

        /// <summary>
        /// løbsklassens divisionsmatch klasse
        /// </summary>
        public string Gruppeklasse { get; set; }

        /// <summary>
        /// løberens klub
        /// </summary>
        public Klub Klub { get; set; }

        /// <summary>
        /// løberens starttid
        /// </summary>
        public string StartTid { get; set; }

        /// <summary>
        /// løberens tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// Løberens placering i klassen
        /// </summary>
        public string Placering { get; set; }

        /// <summary>
        /// løberens status løbet
        /// </summary>
        public string Status 
        {
            get
            {
                return _status;
            }

            set
            {
                _status = _parseOEstatus(value);
            } 
        }

        /// <summary>
        /// løberens briknummmer (til startlisten)
        /// </summary>
        public string Brik { get; set; }

        /// <summary>
        /// er løberen med i matchen?
        /// </summary>
        public bool Inkl{ get; set; }

        /// <summary>
        /// udtræk løberens point i en match
        /// </summary>
        /// <param name="m">matchen</param>
        /// <returns>point</returns>
        public double GetPoint(Match m)
        {
            double p = 0;
            if (point.ContainsKey(m))
            {
                p = (double)point[m];
            }
            return p;
        }

        /// <summary>
        /// udtræk løberens ungdomspoint i en match
        /// </summary>
        /// <param name="m">match</param>
        /// <returns>point</returns>
        public double GetUPoint(Match m)
        {
            double p = 0;
            if (upoint.ContainsKey(m))
            {
                p = (double)upoint[m];
            }
            return p;
        }

        /// <summary>
        /// udtræk løberens samlede point (alm + ungdom) i en match
        /// </summary>
        /// <param name="m">m</param>
        /// <returns>point</returns>
        public double GetSumPoint(Match m)
        {
            double p = 0;
            if (point.ContainsKey(m))
            {
                p += (double)point[m];
            }

            if (upoint.ContainsKey(m))
            {
                p += (double)upoint[m];
            }
            return p;
        }

        /// <summary>
        /// set point for løberen i en match
        /// </summary>
        /// <param name="m">match</param>
        /// <param name="p">point</param>
        public void SetPoint(Match m, double p)
        {
            if (point.ContainsKey(m))
            {
                point[m] = p;
            }
            else
            {
                point.Add(m, p);
            }
        }

        /// <summary>
        ///  sæt ungdomspoint for en løber i en match
        /// </summary>
        /// <param name="m">match</param>
        /// <param name="p">point</param>
        public void SetUPoint(Match m, double p)
        {
            if (upoint.ContainsKey(m))
            {
                upoint[m] = p;
            }
            else
            {
                upoint.Add(m, p);
            }
        }

        /// <summary>
        ///  tilføj point til en løber for en match
        /// </summary>
        /// <param name="m">match</param>
        /// <param name="p">point</param>
        public void AddPoint(Match m, double p)
        {
            if (point.ContainsKey(m))
            {
                point[m] += p;
            }
            else
            {
                point.Add(m, p);
            }
        }

        /// <summary>
        /// tilføj ungdomspoint til en løbe for en match
        /// </summary>
        /// <param name="m">match</param>
        /// <param name="p">point</param>
        public void AddUPoint(Match m, double p)
        {
            if (upoint.ContainsKey(m))
            {
                upoint[m] += p;
            }
            else
            {
                upoint.Add(m, p);
            }
        }

        /// <summary>
        /// sammenlign en løber med en anden - på startnummer
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            return Stnr.CompareTo((obj as Loeber).Stnr);
        }

        /// <summary>
        /// tjek om løberens Status er OK
        /// </summary>
        public bool IsStatusOK
        {
            get
            {
                return Status == "OK"; // || status == "Finished";
            }
        }

        /// <summary>
        /// få en formatteret status
        /// </summary>
        public string PrintStatus
        {
            get
            {
                return _printStatus(Status);
            }
        }

        /// <summary>
        /// få status som et nummer
        /// </summary>
        public string NumStatus
        {
            get
            {
                return _lavNumStatus(Status);
            }
        }


        private string _lavNumStatus(string st)
        {
            if (IsStatusOK)
            {
                return "0";
            }
            else
            {
                switch (st)
                {
                    case "OK":
                        return "0";
                    case "DidNotStart":
                        return "1";
                    case "MissingPunch":
                    case "MisPunch":
                        return "2";
                    case "DidNotFinish":
                        return "3";
                    case "Disqualified":
                        return "4";
                    case "OverTime":
                        return "5";
                    case "NotCompeting":
                        return "6";
                    default:
                        return "9";
                }
            }
        }

        ////private string _parseXMLstatus(string st)
        ////{
        ////    switch (st)
        ////    {
        ////        case "OK":
        ////            return "0";
        ////        case "DidNotStart":
        ////            return "1";
        ////        case "MissingPunch":
        ////            return "2";
        ////        case "DidNotFinish":
        ////            return "3";
        ////        case "Disqualified":
        ////            return "4";
        ////        case "OverTime":
        ////            return "5";
        ////        case "NotCompeting":
        ////            return "6";
        ////        default:
        ////            return "-1";
        ////    }
        ////}

        private string _parseOEstatus(string st)
        {
            switch (st)
            {
                case "0":
                    return "OK";
                case "1":  // Ej start
                    return "DidNotStart";
                case "2":  // Utg.
                    return "DidNotFinish";
                case "3":  // Felst.
                    return "MisPunch";
                case "4": //Disk
                    return "Disqualified";
                case "5": //Maxtid 
                    return "Overtime";
                default://ukendt
                    return st;
            }
        }

        private string _printStatus(string st)
        {
            switch (st)
            {
                case "OK":
                    return "OK";
                case "DidNotStart":  // Ej start
                    return "Ej start";
                case "DidNotFinish":  // Utg.
                    return "Ej slut";
                case "MissingPunch":  // Felst.
                case "MisPunch":  // Felst.
                    return "Fejlst";
                case "Disqualified": //Disk
                    return "Disk";
                case "OverTime": //Maxtid 
                case "Overtime": //Maxtid 
                    return "Maxtid";
                case "Inactive": //inaktiv 
                    return "Inaktiv";
                default://ukendt
                    return st;
            }
        }
    }
}
