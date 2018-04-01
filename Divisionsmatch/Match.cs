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

namespace Divisionsmatch
{
    /// <summary>
    /// klasse for match imellem 2 klubber i stævnet
    /// </summary>
    public class Match
    {
        private Staevne _staevne = null;

        /// <summary>
        /// klub1
        /// </summary>
        public Klub Klub1 { get; set; }

        /// <summary>
        /// klub2
        /// </summary>
        public Klub Klub2 { get; set; }

        /// <summary>
        /// constructor for en match imellem klub1 og klub2 i stævnet
        /// </summary>
        /// <param name="staevne"></param>
        /// <param name="k1"></param>
        /// <param name="k2"></param>
        public Match(Staevne staevne, Klub k1, Klub k2)
        {
            _staevne = staevne;
            Klub1 = k1;
            Klub2 = k2;
        }

        /// <summary>
        /// stævnets division
        /// </summary>
        public int Division
        {
            get { return _staevne.division; }
        }

        /// <summary>
        /// er en af klubberne udeblevet?
        /// </summary>
        public bool HarUdeblevne
        {
            get { return Klub1.Udeblevet || Klub2.Udeblevet; }
        }

        /// <summary>
        /// findes klub i denne match?
        /// </summary>
        /// <param name="klub">klub som der spørges på</param>
        /// <returns>true/false</returns>
        public bool HarKlub(Klub klub)
        {
            return (Klub1.Equals(klub) || Klub2.Equals(klub));
        }

        /// <summary>
        /// points for en gruppe af løbere i klub1. 0 if the klub1 er udeblevet. det halve hvis klub2 er udeblevet
        /// </summary>
        /// <param name="loebere">liste af løbere</param>
        /// <returns>point</returns>
        public double Loebspoint1(SortedList<string, Loeber> loebere)
        {
            double p = loebere.Where(item => item.Value.Klub == Klub1).Sum(item => item.Value.GetSumPoint(this));
            if (Klub2.Udeblevet)
            {
                p /= 2;
            }
            return p;
        }

        /// <summary>
        /// points for en gruppe af løbere i klub2. 0 if the klub2 er udeblevet. det halve hvis klub1 er udeblevet
        /// </summary>
        /// <param name="loebere">liste af løbere</param>
        /// <returns>point</returns>
        public double Loebspoint2(SortedList<string, Loeber> loebere)
        {
            double p = loebere.Where(item => item.Value.Klub == Klub2).Sum(item => item.Value.GetSumPoint(this));
            if (Klub1.Udeblevet)
            {
                p /= 2;
            }
            return p;
        }

        /// <summary>
        /// match score for klub1 i kampen
        /// </summary>
        /// <returns></returns>
        public double Score1()
        {
            double p = Loebspoint1(_staevne.Loebere);
            if (Klub1.Udeblevet)
            {
                p = 0;
            }
            return p;
        }
        /// <summary>
        /// match score for klub2 i kampen
        /// </summary>
        /// <returns></returns>
        public double Score2()
        {
            double p = Loebspoint2(_staevne.Loebere);
            if (Klub2.Udeblevet)
            {
                p = 0;
            }
            return p;
        }

        /// <summary>
        /// match point for klub1
        /// </summary>
        /// <param name="loebere"></param>
        /// <returns></returns>
        public int Matchpoint1(SortedList<string, Loeber> loebere)
        {
            int p = 0;

            if (Klub1.Udeblevet)
            {
                p = 0;
            }
            else if (Klub2.Udeblevet)
            {
                p = 2;
            }
            else
            {
                double p1 = Loebspoint1(loebere);
                double p2 = Loebspoint2(loebere);
                if (p1 > p2)
                {
                    p = 2;
                }
                else if (p1 == p2)
                {
                    p = 1;
                }
                else
                {
                    p = 0;
                }
            }

            return p;
        }

        /// <summary>
        /// match point for klub2
        /// </summary>
        /// <param name="loebere"></param>
        /// <returns></returns>
        public int Matchpoint2(SortedList<string, Loeber> loebere)
        {
            int p = 0;

            if (Klub2.Udeblevet)
            {
                p = 0;
            }
            else if (Klub1.Udeblevet)
            {
                p = 2;
            }
            else
            {
                double p1 = Loebspoint1(loebere);
                double p2 = Loebspoint2(loebere);
                if (p1 < p2)
                {
                    p = 2;
                }
                else if (p1 == p2)
                {
                    p = 1;
                }
                else
                {
                    p = 0;
                }
            }

            return p;
        }
        
        private double _vindUdenKamp()
        {
            double p = _staevne.Grupper.Sum(g => g.MuligePoint());
            return p/2;
        }
    }
}

