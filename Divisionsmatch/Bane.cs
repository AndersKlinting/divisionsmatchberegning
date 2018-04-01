using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Divisionsmatch
{
    /// <summary>
    /// klasse til en Bane
    /// </summary>
    public class Bane
    {
        string _printOutput = string.Empty;

        /// <summary>
        /// constructor
        /// </summary>
        public Bane()
        {
        }

        /// <summary>
        /// constructor for en bestemt bane
        /// </summary>
        /// <param name="banenavn"></param>
        public Bane(string banenavn)
        {
            Navn = banenavn;
        }

        /// <summary>
        /// banens navn
        /// </summary>
        public string Navn { get; set; }

        /// <summary>
        /// metode til at give en ovrskrift som TXT
        /// </summary>
        /// <returns></returns>
        public string LavTXToverskrift()
        {
            if (_printOutput != string.Empty)
            {
                return _printOutput;
            }
            else
            {
                StringBuilder output = new StringBuilder();

                string lin = "Bane " + Navn;

                output.AppendLine(lin);
                output.AppendLine("".PadLeft(lin.Length, '-'));

                return output.ToString();
            }
        }

        /// <summary>
        /// lave en overskrift for banen i HTML
        /// </summary>
        /// <returns></returns>
        public string LavHTMLoverskrift()
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("<h3 class=\"bane\" id=\"" + Navn + "\">Bane " + Navn + "</h3>");

            return output.ToString();
        }
    }
}