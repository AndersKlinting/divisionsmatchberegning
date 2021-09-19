using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using LumenWorks.Framework.IO.Csv;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Divisionsmatch
{
    public static class Util
    {
        public static string CheckFileVersion(string filePath, out bool isEntryXml, out bool isStartXml, out bool isResultXml, out bool isTxt)
        {
            // xml eller csv?
            string version = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            bool isCSV = false;
            bool isXml = true;
            isEntryXml = false;
            isStartXml = false;
            isResultXml = false;
            isTxt = false;

            string fileVersion = "";

            try
            {
                xmlDoc.XmlResolver = null;
                xmlDoc.Load(filePath);

                // validate
                XmlNodeList rootNodes = xmlDoc.GetElementsByTagName("StartList");
                if (rootNodes != null && rootNodes.Count == 1)
                {
                    isStartXml = true;
                }
                else
                {
                    rootNodes = xmlDoc.GetElementsByTagName("ResultList");
                    if (rootNodes != null && rootNodes.Count == 1)
                    {
                        isResultXml = true;
                    }
                    else
                    {
                        rootNodes = xmlDoc.GetElementsByTagName("EntryList");
                        if (rootNodes != null && rootNodes.Count == 1)
                        {
                            isEntryXml = true;
                        }

                    }
                }

                if (isEntryXml || isStartXml || isResultXml)
                {
                    // find ud af hvilken version vi har med at gøre
                    XmlAttribute iofVersion = rootNodes[0].Attributes["iofVersion"];
                    if (iofVersion == null)
                    {
                        XmlNodeList iofNodes = xmlDoc.GetElementsByTagName("IOFVersion");
                        if (iofNodes != null && iofNodes.Count == 1)
                        {
                            iofVersion = iofNodes[0].Attributes["version"];
                        }
                    }
                    if (iofVersion != null)
                    {
                        version = iofVersion.Value;
                    }
                }
            }
            catch
            {
                isXml = false;
            }

            if (!isXml)
            {
                // checkom det er csv eller en klasser.txt
                fileVersion = "csv";
                string[] classLines = File.ReadAllLines(filePath, Encoding.Default);
                if (classLines[0].Contains("-------------------------------------------------------------") || classLines[0].Split(';').Count() == 2)
                {
                    isTxt = true;
                    fileVersion = "txt";
                }
                else
                {
                    isCSV = true;
                    fileVersion = "csv";
                }
            }
            else if (isEntryXml || isStartXml || isResultXml)
            {
                if (version == "3.0")
                {
                    fileVersion = "xml3";
                }
                else if (version == "2.0.3")
                {
                    fileVersion = "xml2";
                }
            }

            return fileVersion;
        }

        public static IList<Loeber> ReadRunnersFromCsv(string filnavn, Config config, List<Klub> klubber)
        {
            string msg = string.Empty;

            IList<Loeber> loebere = new List<Loeber>();

            // find løbere og tilføj dem til gruppen
            // open the file "data.csv" which is a CSV file with headers
            using (CsvReader csv = new CsvReader(new StreamReader(filnavn, ASCIIEncoding.Default), false, ';'))
            {
                if (csv.FieldCount < 43)
                {
                    msg += "det er vist ikke en csv-fil...\n";
                }
                else
                {
                    // csv.MissingFieldAction = MissingFieldAction.ReplaceByNull;

                    csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty;
                    csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine;

                    // treat header
                    csv.ReadNextRecord();

                    int idxStartnr = 0;
                    int idxBriknr = 1;
                    int idxEfternavn = 3;
                    int idxFornavn = 4;
                    int idxStartTid = 9;
                    int idxTid = 11;
                    int idxStatus = 12;
                    int idxKlubId = 14;
                    int idxKlub = 15;
                    int idxKlasse = 18;
                    int idxPl = 43;
                    if (csv[0].StartsWith("OE"))
                    {
                        idxStartnr = 1;
                        idxBriknr = 3;
                        idxEfternavn = 5;
                        idxFornavn = 6;
                        idxStartTid = 11;
                        idxTid = 13;
                        idxStatus = 14;
                        idxKlubId = 19;
                        idxKlub = 20;
                        idxKlasse = 25;
                        idxPl = 57;
                    }

                    while (csv.ReadNextRecord())
                    {
                        try
                        {
                            // løberen må have en klasse som bruges i matchen
                            var gk = config.gruppeOgKlasse.Find(k => k.LøbsKlasse == csv[idxKlasse]);
                            if (gk != null)
                            {
                                Loeber l = new Loeber();
                                l.Stnr = csv[idxStartnr]; //"startnr"];
                                l.Fornavn = csv[idxFornavn]; //"fornavn"];
                                l.Efternavn = csv[idxEfternavn]; // "efternavn"];
                                string klub = string.IsNullOrWhiteSpace(csv[idxKlub]) ? csv[idxKlub - 1] : csv[idxKlub]; // klub city eller navn
                                string klubId = csv[idxKlubId - 1];
                                l.Klub = klubber.Find(item => item.Navn.Equals(klub) || item.NavnKort.Equals(klub)); //"klub"
                                if (l.Klub == null)
                                {
                                    // add a temporay klub for runners outside the match
                                    l.Klub = new Klub(klubId, klub);
                                }
                                l.Løbsklassenavn = csv[idxKlasse];
                                l.Gruppeklasse = gk.Klasse;
                                l.Placering = csv[idxPl]; //"pl"];
                                l.Tid = _lavtid(csv[idxTid]); //"tid"];
                                l.Status = csv[idxStatus]; //"status"];
                                l.StartTid = _lavtid(csv[idxStartTid], config.StartTid);
                                l.Brik = csv[idxBriknr] != "0" ? csv[idxBriknr] : string.Empty;

                                loebere.Add(l);
                            }
                        }
                        catch
                        {
                            msg += "linje: " + csv.CurrentRecordIndex + "\n";
                        }
                    }
                }
            }

            if (msg != string.Empty)
            {
                throw new Exception("Problemer med indlæsning af csv-data: \n" + msg);
            }

            return loebere;
        }

        public static IList<Loeber> ReadRunnersFromResultXML2(string filnavn, Config config, List<Klub> klubber)
        {
            string msg = string.Empty;

            IList<Loeber> loebere = new List<Loeber>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.XmlResolver = null;
            xmlDoc.Load(filnavn);

            XmlNodeList classNodes = xmlDoc.GetElementsByTagName("ClassResult");
            foreach (XmlElement classNode in classNodes)
            {
                // class detail
                string classtext = string.Empty;
                XmlNodeList classes = classNode.GetElementsByTagName("ClassShortName");
                if (classes.Count > 0)
                {
                    classtext = classNode.GetElementsByTagName("ClassShortName")[0].InnerText;
                }

                // deltagere
                foreach (XmlElement personResult in classNode.GetElementsByTagName("PersonResult"))
                {
                    // person details
                    XmlElement person = personResult.GetElementsByTagName("Person")[0] as XmlElement;
                    XmlElement personName = person.GetElementsByTagName("PersonName")[0] as XmlElement;

                    string efternavn = personName.GetElementsByTagName("Family")[0].InnerText;
                    string fornavn = personName.GetElementsByTagName("Given")[0].InnerText;

                    // club details
                    XmlElement club = personResult.GetElementsByTagName("Club")[0] as XmlElement;
                    string klubnavn = (club != null) ? club.GetElementsByTagName("ShortName")[0].InnerText : string.Empty;
                    string klubId = (club != null) ? club.GetElementsByTagName("ClubId")[0].InnerText : string.Empty;

                    // time details
                    XmlElement result = personResult.GetElementsByTagName("Result")[0] as XmlElement;
                    XmlNodeList resultTimes = result.GetElementsByTagName("Time");
                    string tid = string.Empty;
                    if (resultTimes.Count > 0)
                    {
                        tid = resultTimes[0].InnerText;
                    }

                    XmlNodeList ccards = result.GetElementsByTagName("CCardId");
                    string brik = string.Empty;
                    if (ccards.Count > 0)
                    {
                        brik = ccards[0].InnerText;
                    }

                    string placering = string.Empty;
                    XmlNodeList resultPositions = result.GetElementsByTagName("ResultPosition");
                    if (resultPositions.Count > 0)
                    {
                        placering = resultPositions[0].InnerText;
                    }
                    string status = result.GetElementsByTagName("CompetitorStatus")[0].Attributes["value"].Value;

                    try
                    {
                        // løberen må have en klasse som bruges i matchen
                        var gk = config.gruppeOgKlasse.Find(k => k.LøbsKlasse == classtext);
                        if (gk != null)
                        {
                            Loeber l = new Loeber();
                            l.Stnr = "0"; //""];
                            l.Fornavn = fornavn; //"fornavn"];
                            l.Efternavn = efternavn; // "efternavn"];
                            l.Klub = klubber.Find(item => item.Navn.Equals(klubnavn) || item.NavnKort.Equals(klubnavn));//"klub"
                            if (l.Klub == null)
                            {
                                // add a temporay klub for runners outside the match
                                l.Klub = new Klub(klubId, klubnavn);
                            }
                            l.Løbsklassenavn = classtext;
                            l.Gruppeklasse = gk.Klasse;
                            l.Placering = placering; //"pl"];
                            l.Tid = _lavtid(tid); //"tid"];
                            l.Status = status; //"status"];
                            l.Brik = brik;

                            loebere.Add(l);
                        }
                    }
                    catch
                    {
                        msg += "løber: " + fornavn + " " + efternavn + " kunne ikke tilføjes\n";
                    }
                }
            }

            if (msg != string.Empty)
            {
                throw new Exception("Problemer med indlæsning af xml-data: \n" + msg);
            }
            return loebere;
        }

        public static IList<Loeber> ReadRunnersFromResultXML3(string filnavn, Config config, List<Klub> klubber)
        {
            string msg = string.Empty;
            IList<Loeber> loebere = new List<Loeber>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.XmlResolver = null;
            xmlDoc.Load(filnavn);

            XmlNodeList classNodes = xmlDoc.GetElementsByTagName("ClassResult");
            foreach (XmlElement classNode in classNodes)
            {
                // class detail
                string classtext = (classNode.GetElementsByTagName("Class")[0] as XmlElement).GetElementsByTagName("Name")[0].InnerText;

                foreach (XmlElement personResult in classNode.GetElementsByTagName("PersonResult"))
                {
                    // person details
                    XmlElement person = personResult.GetElementsByTagName("Person")[0] as XmlElement;
                    XmlElement personName = person.GetElementsByTagName("Name")[0] as XmlElement;

                    string efternavn = personName.GetElementsByTagName("Family")[0].InnerText;
                    string fornavn = personName.GetElementsByTagName("Given")[0].InnerText;

                    // club details
                    XmlElement club = personResult.GetElementsByTagName("Organisation")[0] as XmlElement;
                    string klubnavn = (club != null) ? club.GetElementsByTagName("Name")[0].InnerText : "";
                    string klubId = string.Empty;
                    if (club != null)
                    {
                        XmlElement clubid = club.GetElementsByTagName("Id")[0] as XmlElement;
                        if (clubid != null)
                        {
                            klubId = club.InnerText;
                        }
                    }
                    string klubSource = string.Empty;
                    if (klubId != string.Empty)
                    {
                        foreach (XmlAttribute a in club.GetElementsByTagName("Id")[0].Attributes)
                        {
                            if (a.Name == "type")
                            {
                                klubSource = a.Value;
                            }
                        }
                    }

                    // time details
                    XmlElement result = personResult.GetElementsByTagName("Result")[0] as XmlElement;

                    XmlNodeList resultTimes = result.GetElementsByTagName("Time");
                    string tid = string.Empty;
                    if (resultTimes.Count > 0)
                    {
                        tid = resultTimes[0].InnerText;
                    }

                    XmlNodeList ccards = result.GetElementsByTagName("ControlCard");
                    string brik = string.Empty;
                    if (ccards.Count > 0)
                    {
                        brik = ccards[0].InnerText;
                    }

                    string placering = string.Empty;
                    XmlNodeList resultPositions = result.GetElementsByTagName("Position");
                    if (resultPositions.Count > 0)
                    {
                        placering = resultPositions[0].InnerText;
                    }

                    string status = string.Empty;
                    XmlNodeList resultStatus = result.GetElementsByTagName("Status");
                    if (resultStatus.Count > 0)
                    {
                        status = resultStatus[0].InnerText;
                    }

                    try
                    {
                        // løberen må have en klasse som bruges i matchen
                        var gk = config.gruppeOgKlasse.Find(k => k.LøbsKlasse == classtext);

                        Loeber l = new Loeber();
                        l.Stnr = "0"; //""];
                        l.Fornavn = fornavn; //"fornavn"];
                        l.Efternavn = efternavn; // "efternavn"];
                        l.Klub = klubber.Find(item => item.Navn.Equals(klubnavn) || item.NavnKort.Equals(klubnavn)); //"klub"
                        if (l.Klub == null)
                        {
                            // add a temporay klub for runners outside the match
                            l.Klub = new Klub(klubId, klubnavn);
                            if (klubSource != string.Empty)
                            {
                                l.Klub.Id.Type = klubSource;
                            }
                        }

                        l.Løbsklassenavn = classtext;
                        l.Gruppeklasse = gk != null ? gk.Klasse : string.Empty;
                        l.Placering = placering; //"pl"];
                        l.Tid = _lavtid(tid); //"tid"];
                        l.Status = status; //"status"];
                        l.Brik = brik;

                        loebere.Add(l);
                    }
                    catch
                    {
                        msg += "løber: " + fornavn + " " + efternavn + "\n";
                    }
                }
            }

            if (msg != string.Empty)
            {
                throw new Exception("Problemer med indlæsning af xml-data: \n" + msg);
            }

            return loebere;
        }

        public static IList<Loeber> ReadRunnersFromStartCsv(string filnavn, Config config, List<Klub> klubber)
        {
            return ReadRunnersFromCsv(filnavn, config, klubber);
        }

        public static IList<Loeber> ReadRunnersFromStartlistXML2(string filnavn, Config config, List<Klub> klubber)
        {
            string msg = string.Empty;

            IList<Loeber> loebere = new List<Loeber>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.XmlResolver = null;
            xmlDoc.Load(filnavn);

            XmlNodeList classNodes = xmlDoc.GetElementsByTagName("ClassStart");
             if (classNodes.Count == 0)
            {
                throw new Exception("Dette er ikke en fil med startliste data");
            }
           foreach (XmlElement classNode in classNodes)
            {
                // class detail
                string classtext = string.Empty;
                XmlNodeList classes = classNode.GetElementsByTagName("ClassShortName");
                if (classes.Count > 0)
                {
                    classtext = classNode.GetElementsByTagName("ClassShortName")[0].InnerText;
                }

                // deltagere
                foreach (XmlElement personStart in classNode.GetElementsByTagName("PersonStart"))
                {
                    // person details
                    XmlElement person = personStart.GetElementsByTagName("Person")[0] as XmlElement;
                    XmlElement personName = person.GetElementsByTagName("PersonName")[0] as XmlElement;

                    string efternavn = personName.GetElementsByTagName("Family")[0].InnerText;
                    string fornavn = personName.GetElementsByTagName("Given")[0].InnerText;

                    // club details
                    XmlElement club = personStart.GetElementsByTagName("Club")[0] as XmlElement;
                    string klubnavn = (club != null) ? club.GetElementsByTagName("ShortName")[0].InnerText : string.Empty;
                    string klubId = (club != null) ? club.GetElementsByTagName("ClubId")[0].InnerText : string.Empty;

                    // start details
                    string tid = string.Empty;
                    XmlElement start = personStart.GetElementsByTagName("StartTime")[0] as XmlElement;
                    if (start != null)
                    {
                        XmlNodeList startTime = start.GetElementsByTagName("Clock");
                        if (startTime !=null && startTime.Count > 0)
                        {
                            tid = startTime[0].InnerText;
                        }
                    }

                    // start details
                    string brik = string.Empty;
                    XmlNodeList ccardid = personStart.GetElementsByTagName("CCardId");
                    if (ccardid != null && ccardid.Count == 1)
                    {
                        brik = ccardid[0].InnerText;
                    }

                    try
                    {
                        // løberen må have en klasse som bruges i matchen
                        var gk = config.gruppeOgKlasse.Find(k => k.LøbsKlasse == classtext);
                        if (gk != null)
                        {
                            Loeber l = new Loeber();
                            l.Stnr = "0"; //""];
                            l.Fornavn = fornavn; //"fornavn"];
                            l.Efternavn = efternavn; // "efternavn"];
                            l.Klub = klubber.Find(item => item.Navn.Equals(klubnavn) || item.NavnKort.Equals(klubnavn));//"klub"
                            if (l.Klub == null)
                            {
                                // add a temporay klub for runners outside the match
                                l.Klub = new Klub(klubId, klubnavn);
                            }
                            l.Løbsklassenavn = classtext;
                            l.Gruppeklasse = gk.Klasse;
                            l.StartTid = _lavtid(tid); //"tid"];                            
                            l.Brik = brik;

                            loebere.Add(l);
                        }
                    }
                    catch
                    {
                        msg += "løber: " + fornavn + " " + efternavn + " kunne ikke tilføjes\n";
                    }
                }
            }

            if (msg != string.Empty)
            {
                throw new Exception("Problemer med indlæsning af xml-data: \n" + msg);
            }
            return loebere;
        }

        public static IList<Loeber> ReadRunnersFromStartlistXML3(string filnavn, Config config, List<Klub> klubber)
        {
            string msg = string.Empty;
            IList<Loeber> loebere = new List<Loeber>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.XmlResolver = null;
            xmlDoc.Load(filnavn);

            XmlNodeList classNodes = xmlDoc.GetElementsByTagName("ClassStart");
            if (classNodes.Count == 0)
            {
                throw new Exception("Dette er ikke en fil med startliste data");
            }
            foreach (XmlElement classNode in classNodes)
            {
                // class detail
                string classtext = (classNode.GetElementsByTagName("Class")[0] as XmlElement).GetElementsByTagName("Name")[0].InnerText;

                foreach (XmlElement personStart in classNode.GetElementsByTagName("PersonStart"))
                {
                    // person details
                    XmlElement person = personStart.GetElementsByTagName("Person")[0] as XmlElement;
                    XmlElement personName = person.GetElementsByTagName("Name")[0] as XmlElement;

                    string efternavn = personName.GetElementsByTagName("Family")[0].InnerText;
                    string fornavn = personName.GetElementsByTagName("Given")[0].InnerText;

                    // club details
                    XmlElement club = personStart.GetElementsByTagName("Organisation")[0] as XmlElement;
                    string klubnavn = (club != null) ? club.GetElementsByTagName("Name")[0].InnerText : "";
                    string klubId = (club != null) ? club.GetElementsByTagName("Id")[0].InnerText : "";
                    string klubSource = string.Empty;
                    if (klubId != string.Empty)
                    {
                        foreach (XmlAttribute a in club.GetElementsByTagName("Id")[0].Attributes)
                        {
                            if (a.Name == "type")
                            {
                                klubSource = a.Value;
                            }
                        }
                    }

                    // time details
                    string brik = string.Empty;
                    string tid = string.Empty;
                    XmlNodeList start = personStart.GetElementsByTagName("Start");
                    if (start != null && start.Count == 1)
                    {
                        XmlNodeList startTime = (start[0] as XmlElement).GetElementsByTagName("StartTime");
                        if (startTime != null && startTime.Count == 1)
                        {
                            tid = startTime[0].InnerText;
                        }

                        // brik detlajer
                        XmlNodeList ccard = (start[0] as XmlElement).GetElementsByTagName("ControlCard");
                        if (ccard != null && ccard.Count == 1)
                        {
                            brik = ccard[0].InnerText;
                        }
                    }

                    try
                    {
                        // løberen må have en klasse som bruges i matchen
                        var gk = config.gruppeOgKlasse.Find(k => k.LøbsKlasse == classtext);
                        if (gk != null)
                        {
                            Loeber l = new Loeber();
                            l.Stnr = "0"; //""];
                            l.Fornavn = fornavn; //"fornavn"];
                            l.Efternavn = efternavn; // "efternavn"];
                            l.Klub = klubber.Find(item => item.Navn.Equals(klubnavn) || item.NavnKort.Equals(klubnavn));//"klub"
                            if (l.Klub == null)
                            {
                                // add a temporay klub for runners outside the match
                                l.Klub = new Klub(klubId, klubnavn);
                                if (klubSource != string.Empty)
                                {
                                    l.Klub.Id.Type = klubSource;
                                }
                            }
                            l.Løbsklassenavn = classtext;
                            l.Gruppeklasse = gk.Klasse;
                            l.StartTid = _lavtid(tid); //"tid"];
                            l.Brik = brik;

                            loebere.Add(l);
                        }
                    }
                    catch
                    {
                        msg += "løber: " + fornavn + " " + efternavn + "\n";
                    }
                }
            }

            if (msg != string.Empty)
            {
                throw new Exception("Problemer med indlæsning af xml-data: \n" + msg);
            }

            return loebere;
        }

        public static string BaneStartListe(bool isTxt, bool includeAll, IList<Loeber> loebere, Config config)
        {
            StringBuilder sb = new StringBuilder();
            if (isTxt)
            {
                sb.AppendLine("Start liste");
                sb.AppendLine("===========");
                sb.AppendLine();
                foreach (Bane b in config.baner.OrderBy(bb => bb.Navn))
                {
                    var kl = config.classes.Where(k => k.Bane != null && k.Bane.Navn.Equals(b.Navn)).Select(kk => kk.Navn);
                    // find løbere på samme bane - og vælg dem i matchen, eller alle
                    var lll = loebere.Where(l => kl.Contains(l.Løbsklassenavn) && (includeAll || config.Klubber.FirstOrDefault(k => k.Navn.Equals(l.Klub.Navn) || k.NavnKort.Equals(l.Klub.Navn)) != null)).ToList();
                    if (lll.Count > 0)
                    {
                        sb.AppendLine();
                        sb.AppendLine("Bane " + b.Navn);
                        sb.AppendLine("--------------");
                        sb.Append(txtTable(lll.OrderBy(l => l.StartTid).ToList()));
                    }
                }
            }
            else
            {
                sb.AppendLine("<html>");
                sb.AppendLine("<body>");
                sb.AppendLine("<h1>Startliste</h1>");

                foreach (Bane b in config.baner.OrderBy(bb => bb.Navn))
                {
                    var kl = config.classes.Where(k => k.Bane != null && k.Bane.Navn.Equals(b.Navn)).Select(kk => kk.Navn);
                    // find løbere på samme bane - og alle
                    var lll = loebere.Where(l => kl.Contains(l.Løbsklassenavn) && (includeAll || config.Klubber.FirstOrDefault(k => k.Navn.Equals(l.Klub.Navn) || k.NavnKort.Equals(l.Klub.Navn)) != null)).ToList();
                    if (lll.Count > 0)
                    {
                        sb.AppendLine("<h3>Bane " + b.Navn + "</h3>");
                        sb.Append(htmlTable(b.Navn, lll.OrderBy(l => l.StartTid).ToList()));
                    }
                }

                sb.AppendLine("</body>");
                sb.AppendLine("</html>");
            }

            return sb.ToString();
        }

        public static string GruppeStartListe(bool isTxt, bool includeAll, IList<Loeber> loebere, Config config)
        {
            List<Gruppe> grupper = new List<Gruppe>();
            foreach (GruppeOgKlasse gk in config.gruppeOgKlasse) //.Where(item => item.LøbsKlasse.Trim() != "-" && item.LøbsKlasse.Trim() != string.Empty))
            {
                if (gk.LøbsKlasse != null && gk.LøbsKlasse.Trim() != "-" && gk.LøbsKlasse.Trim() != string.Empty)
                {
                    Gruppe g = grupper.Find(item => item.navn == gk.Gruppe);
                    if (g == null)
                    {
                        // lav gruppen
                        g = new Gruppe(gk.Gruppe, 0);
                        grupper.Add(g);
                    }

                    // tilføj klasse definitionen
                    if (!g.Klasser.Exists(k => k.LøbsKlasse != null && k.LøbsKlasse.Navn.Equals(gk.LøbsKlasse)))
                    {
                        Klasse kk = config.classes.Find(c => c.Navn == gk.LøbsKlasse);
                        g.Klasser.Add(new Klasseconfig(gk.Klasse, kk));
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            if (isTxt)
            {
                sb.AppendLine("Start liste");
                sb.AppendLine("===========");
                sb.AppendLine();
                foreach (Gruppe g in grupper)
                {
                    var kl = config.gruppeOgKlasse.Where(gk => gk.Gruppe == g.navn).ToList().ConvertAll(kg => kg.LøbsKlasse);
                    var lll = loebere.Where(l => kl.Contains(l.Løbsklassenavn) && (includeAll || config.Klubber.FirstOrDefault(k=>k.Navn.Equals(l.Klub.Navn) || k.NavnKort.Equals(l.Klub.Navn)) != null)).ToList();
                    if (lll.Count > 0)
                    {
                        sb.AppendLine();
                        sb.AppendLine("Gruppe " + g.navn);
                        sb.AppendLine("--------------");
                        sb.AppendLine(txtTable(lll.OrderBy(l => l.StartTid).ToList()));
                    }
                }
            }
            else
            {
                sb.AppendLine("<html>");
                sb.AppendLine("<body>");
                sb.AppendLine("<h1>Startliste</h1>");
                foreach (Gruppe g in grupper)
                {
                    var kl = config.gruppeOgKlasse.Where(gk => gk.Gruppe == g.navn).ToList().ConvertAll(kg => kg.LøbsKlasse);
                    var lll = loebere.Where(l => kl.Contains(l.Løbsklassenavn) && (includeAll || config.Klubber.FirstOrDefault(k => k.Navn.Equals(l.Klub.Navn) || k.NavnKort.Equals(l.Klub.Navn)) != null)).ToList();
                    if (lll.Count > 0)
                    {
                        sb.AppendLine("<h3>Gruppe " + g.navn + "</h3>");
                        sb.AppendLine(htmlTable(g.navn, lll.OrderBy(l => l.StartTid).ToList()));
                    }
                }
                sb.AppendLine("</body>");
                sb.AppendLine("</html>");
            }

            return sb.ToString();
        }

        public static DivisionsResultat.DivisionsResultat OpenDivisionsResultat(string stillingsFil)
        {
            XmlAttributeOverrides xmlAttributeOverrides = new XmlAttributeOverrides();
            XmlAttributes attrs = new XmlAttributes();
            attrs.XmlIgnore = true;
            xmlAttributeOverrides.Add(typeof(DivisionsResultat.DivisionsResultat), "DivisionsStilling", attrs);

            string fil = File.Exists(stillingsFil) ? stillingsFil : Path.GetFileName(stillingsFil);

            try
            {
                DivisionsResultat.DivisionsResultat mitDivisionsResultat = DeSerializeObject<DivisionsResultat.DivisionsResultat>(fil, xmlAttributeOverrides);

                return mitDivisionsResultat;
            }
            catch (Exception e)
            {
                throw new Exception("Stillingsfilen " + stillingsFil + "kunne ikke findes. Ret evt divifilen eller læg filen fra o-service sammen med divi-filen", e);
            }
        }

        public static void SaveDivisionsResultat(DivisionsResultat.DivisionsResultat gemDivisionsResultat, Staevne staevne, string stillingsFil)
        {
            XmlAttributeOverrides xOver = new XmlAttributeOverrides();
            XmlAttributes attrs = new XmlAttributes();

            /* Setting XmlIgnore to false overrides the XmlIgnoreAttribute applied to the Comment field. Thus it will be serialized.*/
            attrs.XmlIgnore = false;
            xOver.Add(typeof(DivisionsResultat.DivisionsResultat), "DivisionsStilling", attrs);
            Util.SerializeObjectToFile(gemDivisionsResultat.TotalDivisionsResultat(staevne), stillingsFil, xOver);
        }

        public static string SerializeDivisionsResultat(DivisionsResultat.DivisionsResultat gemDivisionsResultat, Staevne staevne)
        {
            XmlAttributeOverrides xOver = new XmlAttributeOverrides();
            XmlAttributes attrs = new XmlAttributes();

            /* Setting XmlIgnore to false overrides the XmlIgnoreAttribute applied to the Comment field. Thus it will be serialized.*/
            attrs.XmlIgnore = false;
            xOver.Add(typeof(DivisionsResultat.DivisionsResultat), "DivisionsStilling", attrs);
            return Util.SerializeObject(gemDivisionsResultat.TotalDivisionsResultat(staevne), xOver);
        }

        private static string _lavtid(string t, string starttid)
        {
            string nytid = _lavtid(t);
            if (starttid != "00:00:00")
            {
                string[] arrTid = nytid.Split(':');
                nytid = DateTime.Parse(starttid).Add(TimeSpan.Parse(nytid)).ToString("HH:mm:ss");
            }

            return nytid;
        }

        private static string _lavtid(string t)
        {
            string nytid = t;

            if (t.IndexOf("T") >= 0)
            {
                t = t.Substring(t.IndexOf("T")+1);
            }

            if (t.IndexOf("+") >= 0)
            {
                t = t.Substring(0,t.IndexOf("+"));
            }

            if (t.IndexOf("Z") >= 0)
            {
                t = t.Substring(0, t.IndexOf("Z"));
            }

            // fjern evt decimalsekunder
            if (t.IndexOf(",") >= 0)
            {
                t = t.Substring(0, t.IndexOf(","));
            }
            if (t.IndexOf(".") >= 0)
            {
                t = t.Substring(0, t.IndexOf("."));
            }

            string[] timeparts = t.Split(':');
            if (timeparts.Length == 1)
            {
                if (!t.Trim().Equals(string.Empty))
                {
                    // antag at det er antal sekunder
                    int secs = 0;
                    if (Int32.TryParse(t, out secs))
                    {
                        TimeSpan ts = new TimeSpan(secs * 10000000L);
                        nytid = string.Format("{0:c}", ts);
                    }
                }
            }
            else if (timeparts.Length == 2)
            {
                // antag at det er minutter og sekunder
                nytid = "00:" + t.PadLeft(5, '0');
            }
            else if (timeparts.Length == 3)
            {
                // foranstil med nuller
                nytid = t.PadLeft(8, '0');
            }

            return nytid;
        }

        private static string txtTable(IList<Loeber> loebere)
        {
            StringBuilder output = new StringBuilder();

            //output.Append("----+----1----+----2----+----3----+----4----+----5----+----6----+----7----+----8")
            output.Append("Navn                          Klub                Klasse    Brik      Starttid  ");
            output.AppendLine();

            int cnt = 0;
            foreach (var l in loebere) // print onlu included runners
            {
                cnt++;
                //Loeber l = kv.Value;
                string line = string.Empty;
                line += (l.Fornavn + " " + l.Efternavn).PadRight(30).Substring(0, 30);
                line += l.Klub.Navn.PadRight(20).Substring(0, 20);
                line += l.Løbsklassenavn.PadRight(9).Substring(0, 9);
                line += " "+ l.Brik.PadRight(9).Substring(0, 9);
                line += " " + l.StartTid;
                output.AppendLine(line);
            }

            output.AppendLine();

            return output.ToString();
        }

        private static string htmlTable(string navn, IList<Loeber> loebere)
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("<table class=\"bane\" id=\"" + navn + "\">");
            output.Append("<thead class=\"bane\"><tr class=\"bane\"><th class='bane'>Navn</th><th class='bane'>Klub</th><th class='bane'>Klasse</th><th class='bane'>Brik</th><th class='bane'>Starttid</th>");
            output.AppendLine("</tr></thead>");
            output.AppendLine("<tbody>");

            int cnt = 0;
            foreach (var l in loebere)
            {
                cnt++;
                // Loeber l = kv.Value;
                string line = "<tr class='bane'>";
                line += "<td class='bane' style=\"white-space:nowrap\">" + l.Fornavn + " " + l.Efternavn + "</td>";
                line += "<td class='bane' style=\"white-space:nowrap\">" + l.Klub.Navn + "</td>";
                line += "<td class='bane'>" + l.Løbsklassenavn + "</td>";
                line += "<td class='bane'>" + l.Brik + "</td>";
                line += "<td class='bane'>" + l.StartTid + "</td>";
                line += "</tr>";

                output.AppendLine(line);
            }

            output.AppendLine("</tbody>");
            output.AppendLine("</table>");

            return output.ToString();
        }

        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="xmlAttributeOverrides"></param>
        public static string SerializeObject<T>(T serializableObject, XmlAttributeOverrides xmlAttributeOverrides = null)
        {
            string result = string.Empty;
            if (serializableObject != null)
            {

                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    XmlSerializer serializer = new XmlSerializer(serializableObject.GetType(), xmlAttributeOverrides);

                    StringBuilder xmlBuilder = new StringBuilder();
                    using (var xmlwriter = System.Xml.XmlWriter.Create(xmlBuilder, new XmlWriterSettings()
                    {
                        Indent = true,
                        OmitXmlDeclaration = false,
                        Encoding = Encoding.GetEncoding("ISO-8859-1")
                    }))
                    {
                        serializer.Serialize(xmlwriter, serializableObject);
                    }

                    result = xmlBuilder.ToString();
                }
                catch (Exception ex)
                {
                    //Log exception here
                }
            }

            return result;
        }

        public static void SerializeObjectToFile<T>(T serializableObject, string fileName, XmlAttributeOverrides xmlAttributeOverrides = null)
        {
            if (serializableObject == null) { return; }
            File.WriteAllText(fileName, SerializeObject(serializableObject, xmlAttributeOverrides));
        }

        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="xmlAttributeOverrides"></param>
        /// <returns></returns>
        public static T DeSerializeObject<T>(string fileName, XmlAttributeOverrides xmlAttributeOverrides)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType, xmlAttributeOverrides);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                throw; //Log exception here
            }

            return objectOut;
        }


        /// <summary>
        /// Make the filepath relative to baseDirectory. If not succeding, return original file path.
        /// </summary>
        /// <param name="filePath">Absolute file path to find relative file path for.</param>
        /// <param name="baseDirectory">Directory to use as base. Must be rooted and must only contain directory info (no file name part)</param>
        /// <returns></returns>
        public static string GetRelativeFilePath(string filePath, string baseDirectory)
        {
            // Validate baseDirectory, must be rooted
            if (string.IsNullOrEmpty(baseDirectory))
                return filePath;
            if (!System.IO.Path.IsPathRooted(baseDirectory))
                return filePath;

            // If the path is already relative (not rooted), do nothing.
            if (!System.IO.Path.IsPathRooted(filePath))
                return filePath;

            // baseDirectory must end in a slash, to indicate folder
            if (!baseDirectory[baseDirectory.Length - 1].Equals(System.IO.Path.DirectorySeparatorChar))
                baseDirectory += System.IO.Path.DirectorySeparatorChar;

            // If the roots are different, relative path is not possible, do nothing.
            if (!StringComparer.OrdinalIgnoreCase.Equals(System.IO.Path.GetPathRoot(filePath), System.IO.Path.GetPathRoot(baseDirectory)))
                return filePath;

            Uri fileUri = new Uri(filePath);
            Uri baseDirUri = new Uri(baseDirectory);

            if (fileUri.Scheme != baseDirUri.Scheme)
                return filePath;

            Uri relativeUri = baseDirUri.MakeRelativeUri(fileUri);

            // If relativeUri does not differ from fileUri, it was not possible to make relative URI
            if (StringComparer.OrdinalIgnoreCase.Equals(relativeUri.OriginalString, fileUri.OriginalString))
                return filePath;

            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());
            if (fileUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
            {
                relativePath = relativePath.Replace(System.IO.Path.AltDirectorySeparatorChar, System.IO.Path.DirectorySeparatorChar);
            }

            return relativePath;
        }
    }
}
