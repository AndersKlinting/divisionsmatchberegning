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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Xml;
using LumenWorks.Framework.IO.Csv;
using System.Xml.Serialization;
using System.Linq;

namespace Divisionsmatch
{
    /// <summary>
    /// divisioner i DK
    /// </summary>
    /// <summary>
    /// kredse i DK
    /// </summary>
    public enum Kreds
    {
        /// <summary>
        /// Østkredsen
        /// </summary>
        Østkredsen,
        /// <summary>
        /// Nordkredsen
        /// </summary>
        Nordkredsen,
        /// <summary>
        /// Sydkredsen
        /// </summary>
        Sydkredsen
    }

    /// <summary>
    /// klasse til divisionmatch konfiguration
    /// </summary>
    public class Config : ICloneable
    {
        private bool _sideskift = true;
        private bool _printBaner = false;
        private bool _printAlle = false;
        private bool _printNye = false;
        private bool _printAlleGrupper = true;
        private bool _autoPrint = false;
        private bool _autoExport = false;
        private bool _printDiviResultat = true;
        private bool _inklTidligere = true;
        private string _configClassSrc = string.Empty;
        private string _cssFile = string.Empty;

        private string _diviFile = string.Empty;
        private string _startTid = "00:00:00";

        /// <summary>
        /// constructor
        /// </summary>
        public Config()
        {
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="initialize"> start med en tom config</param>
        public Config(bool initialize)
        {
            if (initialize)
            {
                Version = 3;
                divisioner = new List<Division>();
                gruppeOgKlasse = new List<GruppeOgKlasse>();
                allClubs = new BindingList<Klub>();
                selectedClubs = new BindingList<Klub>();
                udeblevneKlubber = new List<Klub>();
                classes = new List<Klasse>();
                selectedDivision = 1;
                ////printerSettings = new PrinterSettings();
                ////printerSettings.PrintFileName = "dummy.txt";
                pageSettings = new PageSettings();
                int mm10 = Convert.ToInt32(1000 / 25.4);
                pageSettings.Margins.Top = mm10;
                pageSettings.Margins.Bottom = mm10;
                pageSettings.Margins.Left = 2 * mm10;
                pageSettings.Margins.Right = 2 * mm10;
                pageSettings.PrinterSettings.PrintFileName = "dummy.txt";
                font = new SerializableFont(new Font("Courier New", 10));
                classes = new List<Klasse>();
                baner = new List<Bane>();
                Dato = DateTime.Now.Date;

                _TilfoejGruppeOgKlasse("H1", "H20", true);
                _TilfoejGruppeOgKlasse("H1", "H21", false);
                _TilfoejGruppeOgKlasse("H1", "H35", false);
                _TilfoejGruppeOgKlasse("H2", "H40", false);
                _TilfoejGruppeOgKlasse("H2", "H45", false);
                _TilfoejGruppeOgKlasse("H3", "H16", true);
                _TilfoejGruppeOgKlasse("H3", "H50", false);
                _TilfoejGruppeOgKlasse("H3", "H55", false);
                _TilfoejGruppeOgKlasse("H4", "H60", false);
                _TilfoejGruppeOgKlasse("H4", "H65", false);
                _TilfoejGruppeOgKlasse("H5", "H70", false);
                _TilfoejGruppeOgKlasse("H6", "H14", true);
                _TilfoejGruppeOgKlasse("H6", "H16B", false);
                _TilfoejGruppeOgKlasse("H7", "H20B", false);
                _TilfoejGruppeOgKlasse("H7", "H21B", false);
                _TilfoejGruppeOgKlasse("H7", "H35B", false);
                _TilfoejGruppeOgKlasse("H8", "H12", true);
                _TilfoejGruppeOgKlasse("H8", "H14B", false);
                _TilfoejGruppeOgKlasse("H8", "H20C", false);
                _TilfoejGruppeOgKlasse("H8", "H21C", false);
                _TilfoejGruppeOgKlasse("D1", "D20", true);
                _TilfoejGruppeOgKlasse("D1", "D21", false);
                _TilfoejGruppeOgKlasse("D1", "D35", false);
                _TilfoejGruppeOgKlasse("D2", "D40", false);
                _TilfoejGruppeOgKlasse("D2", "D45", false);
                _TilfoejGruppeOgKlasse("D3", "D16", true);
                _TilfoejGruppeOgKlasse("D3", "D50", false);
                _TilfoejGruppeOgKlasse("D3", "D55", false);
                _TilfoejGruppeOgKlasse("D4", "D60", false);
                _TilfoejGruppeOgKlasse("D4", "D65", false);
                _TilfoejGruppeOgKlasse("D5", "D70", false);
                _TilfoejGruppeOgKlasse("D6", "D14", true);
                _TilfoejGruppeOgKlasse("D6", "D16B", false);
                _TilfoejGruppeOgKlasse("D7", "D20B", false);
                _TilfoejGruppeOgKlasse("D7", "D21B", false);
                _TilfoejGruppeOgKlasse("D7", "D35B", false);
                _TilfoejGruppeOgKlasse("D8", "D12", true);
                _TilfoejGruppeOgKlasse("D8", "D14B", false);
                _TilfoejGruppeOgKlasse("D8", "D20C", false);
                _TilfoejGruppeOgKlasse("D8", "D21C", false);
                _TilfoejGruppeOgKlasse("9", "H10", true);
                _TilfoejGruppeOgKlasse("9", "D10", true);
                _TilfoejGruppeOgKlasse("9", "H12B", false);
                _TilfoejGruppeOgKlasse("9", "D12B", false);
                _TilfoejGruppeOgKlasse("9", "Begynder", false);

                divisioner.Add(new Division(1, "1. division"));
                divisioner.Add(new Division(2, "2. division"));
                divisioner.Add(new Division(3, "3. division"));
                divisioner.Add(new Division(4, "4. division"));
                divisioner.Add(new Division(5, "5. division"));
                divisioner.Add(new Division(6, "6. division"));
            }
        }

        #region public properties
        /// <summary>
        /// version af XML format
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// mulige divisioner
        /// </summary>
        public List<Division> divisioner { get; set; }

        /// <summary>
        /// valgt division
        /// </summary>
        public int selectedDivision { get; set; }

        /// <summary>
        /// liste af grupper og klasser i divisionmatchen
        /// </summary>
        public List<GruppeOgKlasse> gruppeOgKlasse { get; set; }

        /// <summary>
        /// klubberne i løbet
        /// </summary>
        public BindingList<Klub> allClubs { get; set; }

        /// <summary>
        /// klubberne i matchen
        /// </summary>
        public BindingList<Klub> selectedClubs { get; set; }

        /// <summary>
        /// listen af udeblevne klubber
        /// </summary>
        public List<Klub> udeblevneKlubber { get; set; }

        /// <summary>
        /// page settings
        /// </summary>
        public PageSettings pageSettings { get; set; }

        /// <summary>
        /// liste af klasser
        /// </summary>
        public List<Klasse> classes { get; set; }

        /// <summary>
        /// lister af baner
        /// </summary>
        public List<Bane> baner { get; set; }

        /// <summary>
        /// font som kan gemmes til XML
        /// </summary>
        public SerializableFont font { get; set; }

        /// <summary>
        /// Stævnets løsdato
        /// </summary>
        public DateTime Dato { get; set; }

        /// <summary>
        /// stævnets kreds
        /// </summary>
        public Kreds? Kreds { get; set; }

        /// <summary>
        /// stævnets skov
        /// </summary>
        public string Skov { get; set; }

        /// <summary>
        /// stævnets Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// validering om en åbnet konfiguration er komplet
        /// </summary>
        [XmlIgnore]
        public bool NeedEdit { get; set; }

        /// <summary>
        /// skal print ske med sideskift
        /// </summary>
        public bool SideSkift
        {
            get
            {
                return _sideskift;
            }
            set
            {
                _sideskift = value;
            }
        }

        /// <summary>
        /// skal resultater printes per bane
        /// </summary>
        public bool PrintBaner
        {
            get
            {
                return _printBaner;
            }
            set
            {
                _printBaner = value;
            }
        }

        /// <summary>
        /// skal alle grupper/baner printes?
        /// </summary>
        public bool PrintAlle
        {
            get
            {
                return _printAlle;
            }
            set
            {
                _printAlle = value;
            }
        }

        /// <summary>
        /// skal vi kun printe nye sider
        /// </summary>
        public bool PrintNye
        {
            get
            {
                return _printNye;
            }
            set
            {
                _printNye = value;
            }
        }

        /// <summary>
        /// skal alle grupper printes?
        /// </summary>
        public bool PrintAlleGrupper
        {
            get
            {
                return _printAlleGrupper;
            }
            set
            {
                _printAlleGrupper = value;
            }
        }

        /// <summary>
        /// skal der printes når ny beregning er foretaget?
        /// </summary>
        public bool AutoPrint
        {
            get
            {
                return _autoPrint;
            }
            set
            {
                _autoPrint = value;
            }
        }

        /// <summary>
        /// skal der divisionsresultat inkluderes i print
        /// </summary>
        public bool PrintDiviResultat
        {
            get
            {
                return _printDiviResultat;
            }
            set
            {
                _printDiviResultat = value;
            }
        }

        /// <summary>
        /// skal der divisionsresultat printes inkl tidligere runder?
        /// </summary>
        public bool InklTidligere
        {
            get
            {
                return _inklTidligere;
            }
            set
            {
                _inklTidligere = value;
            }
        }

        /// <summary>
        /// skal der eksporteres når ny beregning er foretaget?
        /// </summary>
        public bool AutoExport
        {
            get
            {
                return _autoExport;
            }
            set
            {
                _autoExport = value;
            }
        }

        /// <summary>
        /// type afinput til klasser csv/xml 
        /// </summary>
        public string ConfigClassSrc
        {
            get
            {
                return _configClassSrc;
            }
            set
            {
                _configClassSrc = value;
            }
        }

        /// <summary>
        /// giv stien til CSS filen som laves
        /// </summary>
        public string CssFile
        {
            get
            {
                return _cssFile != string.Empty ? Path.Combine(Path.GetDirectoryName(_diviFile), _cssFile) : string.Empty;
            }

            set
            {
                _cssFile = value;
                if (_diviFile != string.Empty && _cssFile != string.Empty)
                {
                    _cssFile = Uri.UnescapeDataString(new Uri(_diviFile).MakeRelativeUri(new Uri(_cssFile)).ToString());
                }
            }
        }

        /// <summary>
        /// løbets starttid (til startliste)
        /// </summary>
        public string StartTid
        {
            get
            {
                return _startTid;
            }

            set
            {
                _startTid = value;
            }
        }
        #endregion

        #region static methods
        /// <summary>
        /// metode til at loade en divi fil
        /// </summary>
        /// <param name="diviFil">sti til divi fil</param>
        /// <returns>returner en Config</returns>
        public static Config LoadDivi(string diviFil)
        {
            Config config = null;

            XmlDocument xmlDoc = new XmlDocument();
            string xml = File.ReadAllText(diviFil);
            xmlDoc.LoadXml(xml); // Parse the string to an XDocument
            int version = 1; // Set default version number to 1

            // Only search for version number in the xml if the Version node exists.
            // The Version node exists in version 2 and higher.
            // No version node is interpreted as "version 1".
            if (xmlDoc.GetElementsByTagName("Version").Count > 0)
            {
                version = int.Parse(xmlDoc.GetElementsByTagName("Version")[0].InnerText);
            }
            if (version == 1)
            {
                config = new Config(true);

                // load klasser
                foreach (XmlElement x in xmlDoc.GetElementsByTagName("classes"))
                {
                    foreach (XmlNode s in x.ChildNodes)
                    {
                        config.classes.Add(new Klasse(s.InnerText));
                    }
                }

                // load selectedDivision
                foreach (XmlElement x in xmlDoc.GetElementsByTagName("selectedDivision"))
                {
                    config.selectedDivision = int.Parse(x.InnerText);
                }

                // load gruppeOgKlasse
                foreach (XmlElement x in xmlDoc.GetElementsByTagName("GruppeOgKlasse"))
                {
                    // læs gruppe og klasse som det er defineret i 2017 reglement, dvs uden blanktegn og bindestreg
                    string gruppe = x.ChildNodes[0].InnerText;
                    string klasse = x.ChildNodes[1].InnerText.Replace(" ", string.Empty).Replace("-", string.Empty);
                    GruppeOgKlasse GK = config.gruppeOgKlasse.Find(gk => gk.Gruppe == gruppe && gk.Klasse == klasse);
                    GK.LøbsKlasse = x.ChildNodes[3].InnerText;
                }

                // load allClubs
                foreach (XmlElement x in xmlDoc.GetElementsByTagName("allClubs"))
                {
                    foreach (XmlNode s in x.ChildNodes)
                    {
                        config.allClubs.Add(new Klub(string.Empty, s.InnerText));
                    }
                }

                // load selectedCubs
                foreach (XmlElement x in xmlDoc.GetElementsByTagName("selectedClubs"))
                {
                    foreach (XmlNode s in x.ChildNodes)
                    {
                        config.selectedClubs.Add(new Klub(string.Empty, s.InnerText));
                    }
                }

                // load selected page settings
                XmlElement ps = xmlDoc.GetElementsByTagName("pageSettings")[0] as XmlElement;
                string newxml = ps.OuterXml.Replace("pageSettings", "PageSettings");
                config.pageSettings = XmlDeserializeFromString<PageSettings>(newxml);

                // load selected printer settings
                ////ps = xmlDoc.GetElementsByTagName("printerSettings")[0] as XmlElement;
                ////newxml = ps.OuterXml.Replace("printerSettings", "PrinterSettings");
                ////config.printerSettings = XmlDeserializeFromString<PrinterSettings>(newxml);

                // load font
                foreach (XmlElement x in xmlDoc.GetElementsByTagName("FontValue"))
                {
                    config.font.SerializeFontAttribute = x.InnerText;
                }

                // load sideskift
                foreach (XmlElement x in xmlDoc.GetElementsByTagName("SideSkift"))
                {
                    config.SideSkift = bool.Parse(x.InnerText);
                }

                //throw new Exception("Divi-filen er version 1 og kan ikke bruges");
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                FileStream fs = new FileStream(diviFil, FileMode.Open, FileAccess.Read);
                config = (Config)serializer.Deserialize(fs);

                if (version < 3)
                {
                    xmlDoc = new XmlDocument();
                    xml = File.ReadAllText(diviFil);
                    xmlDoc.LoadXml(xml); // Parse the string to an XDocument

                    // load allClubs
                    foreach (XmlElement x in xmlDoc.GetElementsByTagName("allClubs"))
                    {
                        foreach (XmlNode s in x.ChildNodes)
                        {
                            config.allClubs.Add(new Klub(string.Empty, s.InnerText));
                        }
                    }

                    // load selectedCubs
                    foreach (XmlElement x in xmlDoc.GetElementsByTagName("selectedClubs"))
                    {
                        foreach (XmlNode s in x.ChildNodes)
                        {
                            config.selectedClubs.Add(new Klub(string.Empty, s.InnerText));
                        }
                    }

                    // load udeblevne
                    foreach (XmlElement x in xmlDoc.GetElementsByTagName("udeblevneKlubber"))
                    {
                        foreach (XmlNode s in x.ChildNodes)
                        {
                            config.udeblevneKlubber.Add(new Klub(string.Empty, s.InnerText));
                        }
                    }
                }
            }

            if (version < 3)
            {
                config.NeedEdit = true;
                config.Dato = DateTime.Now.Date;
            }

            // hack
            // ensure printer settings work on this machine
            string defaultPrinterName = new PrinterSettings().PrinterName;
            ////foreach (string prtName in PrinterSettings.InstalledPrinters)
            ////{
            ////    if (config.printerSettings.PrinterName == prtName)
            ////    {
            ////        defaultPrinterName = prtName;
            ////    }
            ////}

            // ensure the default printer is being used.
            ////config.printerSettings.PrinterName = defaultPrinterName;
            config.pageSettings.PrinterSettings = new PrinterSettings();
            
            // and that it is serializable
            config.pageSettings.PrinterSettings.PrintFileName = "dummy.txt";

            // husk hvor den kommer fra
            config._diviFile = diviFil;

            // sæt seneste nummer
            config.Version = 3;

            return config;
        }
        #endregion

        #region private static methods
        private static T XmlDeserializeFromString<T>(string objectData)
        {
            return (T)XmlDeserializeFromString(objectData, typeof(T));
        }

        private static object XmlDeserializeFromString(string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }
        #endregion

        #region public methods
        /// <summary>
        /// metode til at loade klubber og klasser fra csv/xml fil
        /// </summary>
        /// <param name="txtXMLFile"></param>
        /// <param name="txtTXTFileKlasser"></param>
        public void LoadKlubberOgKlasser(string txtXMLFile, string txtTXTFileKlasser)
        {
            ////// xml eller csv?
            ////string version = string.Empty;
            ////XmlDocument xmlDoc = new XmlDocument();
            ////bool isCSV = false;
            ////bool isStartXml = false;
            ////bool isResultXml = false;

            ////try
            ////{
            ////    xmlDoc.XmlResolver = null;
            ////    xmlDoc.Load(txtXMLFile);

            ////    // validate
            ////    XmlNodeList rootNodes = xmlDoc.GetElementsByTagName("StartList");
            ////    if (rootNodes == null || rootNodes.Count != 1)
            ////    {
            ////        rootNodes = xmlDoc.GetElementsByTagName("ResultList");
            ////        if (rootNodes == null || rootNodes.Count != 1)
            ////        {
            ////            throw new Exception("Dette er vist hverken en startliste eller en resultatliste");
            ////        }
            ////        else
            ////        {
            ////            isResultXml = true;
            ////        }
            ////    }
            ////    else
            ////    {
            ////        isStartXml = true;
            ////    }

            ////    // find ud af hvilken version vi har med at gøre
            ////    XmlAttribute iofVersion = rootNodes[0].Attributes["iofVersion"];
            ////    if (iofVersion == null)
            ////    {
            ////        XmlNodeList iofNodes = xmlDoc.GetElementsByTagName("IOFVersion");
            ////        if (iofNodes != null && iofNodes.Count == 1)
            ////        {
            ////            iofVersion = iofNodes[0].Attributes["Version"];
            ////        }
            ////    }
            ////    if (iofVersion != null)
            ////    {
            ////        version = iofVersion.Value;
            ////    }
            ////}
            ////catch
            ////{
            ////    // vi antager det er en csv-fil
            ////    isCSV = true;
            ////}
            allClubs.Clear();
            classes.Clear();
            selectedClubs.Clear();

            bool isEntryXml = false;
            bool isStartXml = false;
            bool isResultXml = false;
            XmlDocument xmlDoc = new XmlDocument();
            string fileVersion = Util.CheckFileVersion(txtXMLFile, out isEntryXml, out isStartXml, out isResultXml);

            if (fileVersion == "csv")
            {
                _loadclubsfromcsv(txtXMLFile);
            }
            else if (fileVersion.StartsWith("xml"))
            {
                xmlDoc.XmlResolver = null;
                xmlDoc.Load(txtXMLFile);

                XmlNodeList clubNodes = (fileVersion == "xml3") ? xmlDoc.GetElementsByTagName("Organisation") : xmlDoc.GetElementsByTagName("ShortName");
                this.allClubs.Clear();
                foreach (XmlNode clubNode in clubNodes)
                {
                    string clubtext = string.Empty;
                    string clubId = string.Empty;
                    string sourceType = string.Empty;
                    if (fileVersion == "xml3")
                    {
                        foreach (XmlNode node in clubNode.ChildNodes)
                        {
                            if (node.Name == "Name")
                            {
                                clubtext = node.InnerText;
                            }
                            else if (node.Name == "Id")
                            {
                                clubId = node.InnerText;
                                try
                                {
                                    sourceType = node.Attributes["type"].Value;
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    else
                    {
                        clubtext = clubNode.InnerText;
                    }

                    if (this.allClubs.FirstOrDefault(k => k.Navn.Equals(clubtext)) == null)
                    {
                        Klub newKlub = new Klub(clubId, clubtext);
                        if (sourceType != string.Empty)
                        {
                            newKlub.Id.Type = sourceType;
                        }
                        this.allClubs.Add(newKlub);
                    }
                }
            }
            else
            {
                throw new Exception("Dette er vist hverken en entryliste, en startliste eller en resultatliste");
            }

            this.baner.Clear();
            this.classes.Clear();
            this.classes.Add(new Klasse(""));
            this.classes.Add(new Klasse(" - "));
            bool classesLoaded = false;
            // tjek om der er angivet en txt fil til klasse/bane
            if (txtTXTFileKlasser != string.Empty)
            {
                string[] classLines = File.ReadAllLines(txtTXTFileKlasser, Encoding.Default);

                if (classLines[0].Contains(";"))
                {
                    classesLoaded = true;
                    // csv file with class and course and no header lines:
                    // H21-;bane 1
                    // H35-;bane 2A
                    foreach (string line in classLines)
                    {
                        string[] arrLine = line.Split(';');
                        string aClass = arrLine[0].Trim();
                        string aBane = (arrLine.Length > 1) ? arrLine[1].Trim() : "";
                        _LavBaneOgKlasse(aClass, aBane);
                    }
                }
                else if (classLines[0].Contains("-------------------------------------------------------------"))
                {
                    classesLoaded = true;
                    // assume OE2003 classes text file
                    // class is the long name

                    ////    Nr Klasse            Bane               Længde/stigning      
                    ////   101 D-10              8                  2.5 km  12 K        
                    ////|----+----|----+----|----+----|----+----|----+----|----+----|----
                    ////0         10        20        30        40        50        60
                    ////
                    //// Nr = 0-5
                    //// klasse = 7-23
                    //// Bane = 25-43
                    for (int l = 8; l < classLines.Length; l++)
                    {
                        if (classLines[l].Length > 7)
                        {
                            string aBane = classLines[l].PadRight(80).Substring(25, 18).Trim();
                            string aClass = classLines[l].Substring(7, 17).Trim();
                            _LavBaneOgKlasse(aClass, aBane);
                        }
                    }
                }
            }

            if (!classesLoaded)
            {
                if (fileVersion == "csv")
                {
                    // brug klasserne fra startliste CSV filen
                    _loadclassesfromcsv(txtXMLFile);
                    ConfigClassSrc = "csv";
                }
                else
                {
                    // brug klasserne fra startliste XML filen
                    XmlNodeList classList = null;
                    if (isStartXml)
                    {
                        classList = xmlDoc.GetElementsByTagName("ClassStart");
                    }
                    else if (isResultXml)
                    {
                        classList = xmlDoc.GetElementsByTagName("ClassResult");
                    }
                    else if (isEntryXml)
                    {
                        classList = xmlDoc.GetElementsByTagName("PersonEntry");
                    }

                    if (classList != null)
                    {
                        ConfigClassSrc = fileVersion;

                        // loop over classlist (either start or result)
                        foreach (XmlNode classListNode in classList)
                        {
                            string classtext = string.Empty;
                            string coursetext = string.Empty;
                            foreach (XmlNode node in classListNode.ChildNodes)
                            {
                                if (node.Name == "Class" || node.Name == "ClassShortName")
                                {
                                    if (fileVersion == "xml3")
                                    {
                                        foreach (XmlNode cnode in node.ChildNodes)
                                        {
                                            if (cnode.Name == "Name")
                                            {
                                                classtext = cnode.InnerText;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        classtext = node.InnerText;
                                    }
                                }
                                else if (node.Name == "Course" && fileVersion == "xml3")
                                {
                                    foreach (XmlNode cnode in node.ChildNodes)
                                    {
                                        if (cnode.Name == "Name")
                                        {
                                            coursetext = cnode.InnerText;
                                        }
                                    }
                                }
                            }

                            if (string.IsNullOrWhiteSpace(coursetext) && classListNode.Name == "ClassResult" && fileVersion == "xml3")
                            {
                                // søg bane navn i personresult
                                foreach (XmlElement coursenode in (classListNode as XmlElement).GetElementsByTagName("Course"))
                                {
                                    foreach (XmlNode cnode in coursenode.ChildNodes)
                                    {
                                        if (cnode.Name == "Name")
                                        {
                                            coursetext = cnode.InnerText;
                                            break;
                                        }
                                    }
                                    if (!string.IsNullOrWhiteSpace(coursetext))
                                    {
                                        break;
                                    }
                                }
                            }

                            // here we have class and perhaps course
                            _LavBaneOgKlasse(classtext, coursetext);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gem config i divi-fil
        /// </summary>
        /// <param name="filename">divi-fil</param>
        public void SaveDivi(string filename)
        {
            // make paths relative

            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            StreamWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, this);
            writer.Close();
        }
        
        /// <summary>
        /// lav en klon af config
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Config c = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            using (MemoryStream ms = new MemoryStream ())
            {
                serializer.Serialize(ms, this);
                ms.Position = 0;

                c = (Config)serializer.Deserialize(ms);
                c.NeedEdit = this.NeedEdit;
            }
            return c;
        }
        #endregion

        #region private methods
        private void _LavBaneOgKlasse(string klasse, string bane)
        {
            if (klasse != string.Empty)
            {
                Klasse k = this.classes.Find(kk => kk.Navn == klasse);
                if (k == null)
                {
                    k = new Klasse(klasse);
                    this.classes.Add(k);
                }

                if (bane != string.Empty)
                {
                    Bane b = this.baner.Find(bb => bb.Navn == bane);
                    if (b == null)
                    {
                        b = new Bane(bane);
                        this.baner.Add(b);
                    }

                    k.Bane = b;
                }
            }
        }

        private void _TilfoejGruppeOgKlasse(string gruppe, string klasse, bool ungdom)
        {
            GruppeOgKlasse gk = new GruppeOgKlasse();
            gk.Gruppe = gruppe;
            gk.Klasse = klasse;
            gk.LøbsKlasse = null;
            gk.Ungdom = ungdom;
            gruppeOgKlasse.Add(gk);
        }

        private void _loadclubsfromcsv(string filnavn)
        {
            bool isOE = true;

            // find løbere og tilføj dem til gruppen
            // open the file "data.csv" which is a CSV file with headers
            using (CsvReader csv = new CsvReader(new StreamReader(filnavn, ASCIIEncoding.Default), false, ';'))
            {
                // csv.MissingFieldAction = MissingFieldAction.ReplaceByNull;

                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty;
                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine;

                int idxKlub = 0;
                int idxKlubId = -1;
                if (csv.FieldCount <= 15)
                {
                    // EResults Pro Tilpasset csv format - no header
                    idxKlub = 3;
                    isOE = false;
                }
                else
                {
                    // treat header
                    csv.ReadNextRecord();

                    idxKlubId = 14;
                    idxKlub = 15; // oe2003

                    if (csv[0].StartsWith("OE"))
                    {
                        idxKlubId = 19;
                        idxKlub = 20;  // oe02010
                    }
                }

                while (csv.ReadNextRecord())
                {
                    string klub = csv[idxKlub];
                    string klubId = idxKlubId!=-1 ? csv[idxKlubId] : string.Empty;

                    if (string.IsNullOrWhiteSpace(klub) && isOE)
                    {
                        klub = csv[idxKlub - 1]; // navn er alternativ til city 
                    }
                    if (klub != string.Empty && this.allClubs.FirstOrDefault(k => k.Navn.Equals(klub)) == null)
                    {
                        this.allClubs.Add(new Klub(klubId, klub));
                    }
                }
            }
        }

        private void _loadclassesfromcsv(string filnavn)
        {
            bool isOE = true;

            // find klasser og evt baner
            // open the file "data.csv" which is a CSV file with headers
            using (CsvReader csv = new CsvReader(new StreamReader(filnavn, ASCIIEncoding.Default), false, ';'))
            {
                // csv.MissingFieldAction = MissingFieldAction.ReplaceByNull;

                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty;
                csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine;

                int idxKlasse = 0;
                int idxBane = 0;
                if (csv.FieldCount <= 15)
                {
                    // EResults Pro Tilpasset csv format - no header
                    idxKlasse = 0;
                    idxBane = csv.FieldCount - 1;
                    isOE = false;
                }
                else
                {
                    // treat header
                    csv.ReadNextRecord();

                    // oe2003
                    idxKlasse = 18;
                    idxBane = 53;
                    if (csv[0].StartsWith("OE"))
                    {
                        // oe2010
                        idxKlasse = 25;
                        idxBane = 53;
                    }
                }

                while (csv.ReadNextRecord())
                {
                    string klasse = string.Empty;
                    string bane = string.Empty;
                    if (csv.FieldCount > idxKlasse && csv[idxKlasse] != string.Empty)
                    {
                        klasse = csv[idxKlasse];
                    }

                    if (csv.FieldCount > idxBane && csv[idxBane] != string.Empty)
                    {
                        bane = csv[idxBane];
                    }

                    _LavBaneOgKlasse(klasse, bane);
                }
            }
        }
        #endregion 
    }
}
