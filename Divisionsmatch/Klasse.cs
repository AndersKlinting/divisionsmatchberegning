using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Divisionsmatch
{
    /// <summary>
    /// klasse for Klasse
    /// </summary>
    public class Klasse
    {
        private string _navn = string.Empty;
        private Bane _bane = null;

        /// <summary>
        /// constructor
        /// </summary>
        public Klasse()
        {
        }

        /// <summary>
        /// lav en Klasse med et bestemt navn
        /// </summary>
        /// <param name="navn"></param>
        public Klasse(string navn)
        {
            _navn = navn;
        }

        /// <summary>
        /// lav en klasse med navn og bane
        /// </summary>
        /// <param name="navn">navnet</param>
        /// <param name="bane">banen</param>
        public Klasse(string navn, Bane bane)
        {
            _navn = navn;
            _bane = bane;
        }

        /// <summary>
        /// Navnet på klassen
        /// </summary>
        public string Navn
        {
            get
            {
                return _navn;
            }
            
            set
            {
                _navn = value;
            }
        }

        /// <summary>
        /// Banen for klassen
        /// </summary>
        public Bane Bane
        {
            get
            {
                return _bane;
            }
            
            set
            {
                _bane = value;
            }
        }

        /// <summary>
        /// formattering af klassen
        /// </summary>
        [XmlIgnore]
        public string DisplayName
        {
            get
            {
                string n = Navn;
                if (Bane != null)
                {
                    n += " (" + Bane.Navn + ")";
                }
                return n;
            }
        }


        /// <summary>
        /// default beskrivelse for klassen
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Navn;
        }
    }
}
