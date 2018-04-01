using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Divisionsmatch
{
    /// <summary>
    /// klasse til at holde en division
    /// </summary>
    public class Division
    {
        /// <summary>
        /// divisionsnummer
        /// </summary>
        [XmlElement(ElementName = "nr")]
        public int Nr { get; set; }

        /// <summary>
        /// beskrivelse af divisionen
        /// </summary>
        [XmlElement(ElementName = "beskrivelse")]
        public string Beskrivelse { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public Division()
        {
            Nr = 0;
            Beskrivelse = "";
        }

        /// <summary>
        /// constructor med bestemt nummer og beskrivelse
        /// </summary>
        /// <param name="d">nummer</param>
        /// <param name="b">beskrivelse</param>
        public Division(int d, string b)
        {
            Nr = d;
            Beskrivelse = b;
        }

        /// <summary>
        /// default beskrivelse
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Beskrivelse;
        }
    }
}
