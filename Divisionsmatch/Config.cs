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

        private BindingList<Klub> _klubber = null;
        private List<Klub> _udeblevneKlubber = null;

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
                Version = 4;
                gruppeOgKlasse = new List<GruppeOgKlasse>();
                Klubber = new BindingList<Klub>();
                Klubber = new BindingList<Klub>();
                udeblevneKlubber = new List<Klub>();
                classes = new List<Klasse>();
                Division = 1;
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

                _TilfoejGruppeOgKlasse("Beg", "Beg");
                _TilfoejGruppeOgKlasse("D10", "D10");
                _TilfoejGruppeOgKlasse("D12", "D12");
                _TilfoejGruppeOgKlasse("D12B", "D12B");
                _TilfoejGruppeOgKlasse("D14", "D14");
                _TilfoejGruppeOgKlasse("D14B", "D14B");
                _TilfoejGruppeOgKlasse("D16", "D16");
                _TilfoejGruppeOgKlasse("D18", "D18");
                _TilfoejGruppeOgKlasse("D20", "D20");
                _TilfoejGruppeOgKlasse("D20B", "D20B");
                _TilfoejGruppeOgKlasse("D21", "D21");
                _TilfoejGruppeOgKlasse("D21B", "D21B");
                _TilfoejGruppeOgKlasse("D40", "D40");
                _TilfoejGruppeOgKlasse("D45B", "D45B");
                _TilfoejGruppeOgKlasse("D50", "D50");
                _TilfoejGruppeOgKlasse("D60", "D60");
                _TilfoejGruppeOgKlasse("D70", "D70");
                _TilfoejGruppeOgKlasse("D-Let", "D-Let");
                _TilfoejGruppeOgKlasse("H10", "H10");
                _TilfoejGruppeOgKlasse("H12", "H12");
                _TilfoejGruppeOgKlasse("H12B", "H12B");
                _TilfoejGruppeOgKlasse("H14", "H14");
                _TilfoejGruppeOgKlasse("H14B", "H14B");
                _TilfoejGruppeOgKlasse("H16", "H16");
                _TilfoejGruppeOgKlasse("H18", "H18");
                _TilfoejGruppeOgKlasse("H20", "H20");
                _TilfoejGruppeOgKlasse("H20B", "H20B");
                _TilfoejGruppeOgKlasse("H21", "H21");
                _TilfoejGruppeOgKlasse("H21B", "H21B");
                _TilfoejGruppeOgKlasse("H40", "H40");
                _TilfoejGruppeOgKlasse("H45B", "H45B");
                _TilfoejGruppeOgKlasse("H50", "H50");
                _TilfoejGruppeOgKlasse("H60", "H60");
                _TilfoejGruppeOgKlasse("H70", "H70");
                _TilfoejGruppeOgKlasse("H80", "H80");
                _TilfoejGruppeOgKlasse("H-Let", "H-Let");
            }
        }

        #region public properties
        /// <summary>
        /// version af XML format
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// relativ sti til filen fra o-service
        /// </summary>
        public string DivisionsResultatFil{ get; set; }

        /// <summary>
        /// division
        /// </summary>
        public int Division { get; set; }

        /// <summary>
        /// liste af grupper og klasser i divisionmatchen
        /// </summary>
        public List<GruppeOgKlasse> gruppeOgKlasse { get; set; }

        /// <summary>
        /// klubberne i matchen
        /// </summary>
        public BindingList<Klub> Klubber 
        {
            get
            {
                if (_klubber == null)
                {
                    _klubber = new BindingList<Klub>();
                }
                return _klubber;
            }
            set
            {
                _klubber = value;
            }
        }

        /// <summary>
        /// listen af udeblevne klubber
        /// </summary>
        public List<Klub> udeblevneKlubber {
            get
            {
                if (_udeblevneKlubber == null)
                {
                    _udeblevneKlubber = new List<Klub>();
                }
                return _udeblevneKlubber;
            }
            set
            {
                _udeblevneKlubber = value;
            }
        }

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
        public string Kreds { get; set; }

        /// <summary>
        /// stævnets skov
        /// </summary>
        public string Skov { get; set; }

        /// <summary>
        /// stævnets Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Beskrivelse af stævnet
        /// </summary>
        public string Beskrivelse { get; set; }

        /// <summary>
        /// stævnets runde
        /// </summary>
        public int Runde { get; set; }

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
                return _cssFile ?? string.Empty;
            }

            set
            {
                _cssFile = value;
                if (_diviFile != string.Empty && _cssFile != string.Empty)
                {
                    _cssFile = Util.GetRelativeFilePath(_cssFile, Path.GetDirectoryName(_diviFile));
                }
            }
        }

        /// <summary>
        /// giver fuld sti til CSS filen som laves
        /// </summary>
        public string CssFileFullPath
        {
            get
            {
                return _cssFile != string.Empty ? (!string.IsNullOrEmpty(_diviFile) ? Path.Combine(Path.GetDirectoryName(_diviFile), _cssFile) : _cssFile) : string.Empty;
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
            if (version < 4)
            {
                throw new Exception("Divi-filen er ikke version 4 (2019 reglement) og kan derfor ikke åbnes");
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                FileStream fs = new FileStream(diviFil, FileMode.Open, FileAccess.Read);
                config = (Config)serializer.Deserialize(fs);
                config.DivisionsResultatFil = Path.GetFullPath(Path.Combine(Directory.GetParent(diviFil).FullName, config.DivisionsResultatFil));
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
            config.Version = 4;

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
        public void LoadKlasserOgBaner(string txtXMLFile)
        {
            bool isEntryXml = false;
            bool isStartXml = false;
            bool isResultXml = false;
            bool isTxt = false;
            XmlDocument xmlDoc = new XmlDocument();
            string fileVersion = Util.CheckFileVersion(txtXMLFile, out isEntryXml, out isStartXml, out isResultXml, out isTxt);


            this.baner.Clear();
            this.classes.Clear();
            this.classes.Add(new Klasse(""));
            this.classes.Add(new Klasse(" - "));
            bool classesLoaded = false;

            // tjek om der er angivet en txt fil til klasse/bane
            if (isTxt)
            {
                string[] classLines = File.ReadAllLines(txtXMLFile, Encoding.Default);

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
                    xmlDoc.Load(txtXMLFile);
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
                    else
                    {
                        throw new Exception("Dette er vist ikke en korrekt fil. Klasser (og baner) kan ikke findes");
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
            if (Path.IsPathRooted(filename) && Path.GetPathRoot(filename).ToUpperInvariant() == Path.GetPathRoot(this.DivisionsResultatFil).ToUpperInvariant())
            {
                string relativeP = string.Empty;
                string p = Path.GetDirectoryName(filename).ToUpperInvariant();
                while (!this.DivisionsResultatFil.ToUpperInvariant().StartsWith(p + Path.DirectorySeparatorChar))
                {
                    relativeP += ".." + Path.DirectorySeparatorChar;
                    p = Directory.GetParent(p).FullName.ToUpperInvariant();
                }
                this.DivisionsResultatFil = this.DivisionsResultatFil.ToUpperInvariant().Replace(p + Path.DirectorySeparatorChar, relativeP);
            }
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

        private void _TilfoejGruppeOgKlasse(string gruppe, string klasse)
        {
            GruppeOgKlasse gk = new GruppeOgKlasse();
            gk.Gruppe = gruppe;
            gk.Klasse = klasse;
            gk.LøbsKlasse = null;
            ////gk.Ungdom = ungdom;
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
