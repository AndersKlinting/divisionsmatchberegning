using System.ComponentModel;
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
using System.Drawing;
using System.Xml.Serialization;

namespace Divisionsmatch
{
    /// <summary>
    /// klasse til en font som kan gemmes som XML
    /// </summary>
    public class SerializableFont
    {
        /// <summary>
        /// constructor
        /// </summary>
        public SerializableFont()
        {
            FontValue = null;
        }

        /// <summary>
        /// constructor på windows font
        /// </summary>
        /// <param name="font"></param>
        public SerializableFont(Font font)
        {
            FontValue = font;
        }

        /// <summary>
        /// font
        /// </summary>
        [XmlIgnore]
        public Font FontValue { get; set; }

        /// <summary>
        /// converteret font
        /// </summary>
        [XmlElement("FontValue")]
        public string SerializeFontAttribute
        {
            get
            {
                return FontXmlConverter.ConvertToString(FontValue);
            }
            set
            {
                FontValue = FontXmlConverter.ConvertToFont(value);
            }
        }

        /// <summary>
        /// operator
        /// </summary>
        /// <param name="serializeableFont"></param>
        public static implicit operator Font(SerializableFont serializeableFont)
        {
            if (serializeableFont == null)
                return null;
            return serializeableFont.FontValue;
        }

        /// <summary>
        /// operator
        /// </summary>
        /// <param name="font"></param>
        public static implicit operator SerializableFont(Font font)
        {
            return new SerializableFont(font);
        }
    }

    /// <summary>
    /// static class o convert font to string
    /// </summary>
    public static class FontXmlConverter
    {
        /// <summary>
        /// lav fint som string 
        /// </summary>
        /// <param name="font"></param>
        /// <returns>string</returns>
        public static string ConvertToString(Font font)
        {
            try
            {
                if (font != null)
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                    return converter.ConvertToString(font);
                }
                else
                    return null;
            }
            catch { System.Diagnostics.Debug.WriteLine("Unable to convert"); }
            return null;
        }

        /// <summary>
        /// metode til at konvereter font fra string
        /// </summary>
        /// <param name="fontString">font som string</param>
        /// <returns>font</returns>
        public static Font ConvertToFont(string fontString)
        {
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                return (Font)converter.ConvertFromString(fontString);
            }
            catch { System.Diagnostics.Debug.WriteLine("Unable to convert"); }
            return null;
        }
    }
}
