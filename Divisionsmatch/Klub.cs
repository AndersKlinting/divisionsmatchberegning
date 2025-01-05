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
using System.Xml.Serialization;

namespace Divisionsmatch
{
    /// <summary>
    /// klasse for en klub med navn og point
    /// </summary>
    public class Klub
    {
        private bool _udeblevet = false;
        private string _navn = string.Empty;
        private KlubId _id = new KlubId();
        private string _navnkort = string.Empty;

        /// <summary>
        /// constructor
        /// </summary>
        public Klub()
        {
        }

        /// <summary>
        /// constructor for en klub med et navn
        /// </summary>
        /// <param name="id"></param>
        /// <param name="navn"></param>
        public Klub(string id, string navn)
        {
            _id.Id = id;
            _navn = navn;
        }

        /// <summary>
        /// o-service id på klubben
        /// </summary>
        public KlubId Id
        {
            get
            {
                return _id;
            }

            set
            {
                if (value != null)
                {
                    _id = value;
                }
            }
        }

        /// <summary>
        /// navnet på klubben
        /// </summary>
        public string Navn
        {
            get
            {
                return _navn;
            }

            set
            {
                if (value != null)
                {
                    _navn = value;
                }
            }
        }

        /// <summary>
        /// kort navn for klubben
        /// </summary>
        public string NavnKort
        {
            get
            {
                if (_navnkort != string.Empty)
                    return _navnkort;
                else
                    return _navn;
            }

            set
            {
                if (value != null)
                {
                    _navnkort = value;
                }
            }
        }

        /// <summary>
        /// liste af klubber i i klubsamarbejde
        /// </summary>
        public List<Klub> Klubber { get; set; }

        /// <summary>
        /// er klubben udenblevet?
        /// </summary>
        public bool Udeblevet
        {
            get { return _udeblevet; }
            set { _udeblevet = value; }
        }

        /// <summary>
        ///  klubbens point
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// klubbens score
        /// </summary>
        public double Score1 { get; set; }

        /// <summary>
        /// klubbens score imod sig
        /// </summary>
        public double Score2 { get; set; }

        /// <summary>
        /// klubbens egnescore mod andre klubber med samme matchpoint
        /// </summary>
        public double EgenScore { get; set; }


        /// <summary>
        /// evt kommentar til klubbens resultat
        /// </summary>
        public string Kommentar { get; set; }

        /// <summary>
        /// standard text
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _navn;
        }
    }

    /// <summary>
    /// klasse til at holde id og type
    /// </summary>
    public class KlubId
    {
        private string _id = string.Empty;
        private string _type = string.Empty;

        /// <summary>
        /// constructor
        /// </summary>
        public KlubId()
        {
        }

        /// <summary>
        /// klasse tilat holde o-service id og type xml attributter
        /// </summary>
        /// <param name="id">o-service id</param>
        /// <param name="type">o-service type</param>
        public KlubId(string id, string type)
        {
            _id = id;
            _type = type;
        }

        /// <summary>
        /// Id for en klub
        /// </summary>
        [XmlText]
        public string Id
        {
            get { return _id; }

            set { _id = value; }
        }

        /// <summary>
        /// kilde til id
        /// </summary>
        [XmlAttribute("type")]
        public string Type
        {
            get { return _type; }

            set { _type = value; }
        }
    }

}
