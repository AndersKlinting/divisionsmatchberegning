using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Divisionsmatch.DivisionsResultat
{
    //[XmlRoot(ElementName = "Klub")]
    /// <summary>
    /// klub i en divisionsmatch
    /// </summary>
    public class Klub : ICloneable
    {
        private string _navnkort = string.Empty;

        /// <summary>
        /// klubbens id
        /// </summary>
        [XmlElement(ElementName = "Id")]
        public KlubId Id { get; set; }

        /// <summary>
        /// klubbens navn
        /// </summary>
        [XmlElement(ElementName = "Navn")]
        public string Navn { get; set; }

        /// <summary>
        /// klubbens navn
        /// </summary>
        [XmlElement(ElementName = "NavnKort")]
        public string NavnKort { get
            {
                if (_navnkort != string.Empty)
                    return _navnkort;
                else
                    return Navn;
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
        /// liste af klubber i klubsamarbejde
        /// </summary>
        public List<Klub> Klubber { get; set; }

        /// <summary>
        /// klubbens placering i en divisionsmatch
        /// </summary>
        [XmlElement(ElementName = "Placering")]
        public int Placering { get; set; }

        /// <summary>
        /// klubbens Matchpoint i en divisionsmatch
        /// </summary>
        [XmlElement(ElementName = "MatchPoint")]
        public double MatchPoint { get; set; }

        /// <summary>
        /// klubbens løbspoint  i en divisionsmatch
        /// </summary>
        [XmlElement(ElementName = "LoebsPoint")]
        public double LøbsPoint { get; set; }

        /// <summary>
        /// klubbens løbspoint  i en divisionsmatch
        /// </summary>
        [XmlElement(ElementName = "ModstanderLoebsPoint")]
        public double ModstanderLøbsPoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(ElementName = "Kommentar")]
        public string Kommentar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(ElementName = "Udebleven")]
        public bool Udebleven { get; set; }

        /// <summary>
        /// skal be klon
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Klub clone = new Klub();
            clone.Id = this.Id.Clone() as KlubId;
            clone.Navn = this.Navn;
            clone.NavnKort = this.NavnKort;
            clone.Placering = this.Placering;
            clone.MatchPoint = this.MatchPoint;
            clone.LøbsPoint = this.LøbsPoint;
            clone.ModstanderLøbsPoint = this.ModstanderLøbsPoint;
            clone.Kommentar = this.Kommentar;
            clone.Udebleven = this.Udebleven;

            return clone;
        }

        public override string ToString()
        {
            return this.Navn;
        }
    }

    /// <summary>
    /// klasse til at holde id og type
    /// </summary>
    public class KlubId : ICloneable
    {
        private string _id = string.Empty;
        private string _type = string.Empty;

        /// <summary>
        /// constructor
        /// </summary>
        public KlubId()
        {
        }

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

        /// <summary>
        /// skal be klon
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            KlubId clone = new KlubId();
            clone.Id = this.Id;
            clone.Type = this.Type;

            return clone;
        }
    }

    //[XmlRoot(ElementName = "Match")]    
    /// <summary>
    /// match imellem 2 klubber i et stævne
    /// </summary>
    public class Match : ICloneable
    {
        private MatchKlub[] _MatchKlubber = new MatchKlub[2];

        public Match()
        {
            _MatchKlubber[0] = new MatchKlub();
            _MatchKlubber[1] = new MatchKlub();
        }

        public int Id
        {
            get;
            set;
        }

        public MatchKlub[] MatchKlubber
        {
            get
            {
                return _MatchKlubber;
            }

            set
            {
                _MatchKlubber = value;
            }
        }

        public List<GruppePoint> MatchGruppePoint { get; set; }

        ///// <summary>
        ///// navn på klub 1
        ///// </summary>
        //[XmlElement(ElementName = "KlubNavn1")]
        //public string KlubNavn1 { get; set; }

        ///// <summary>
        ///// navn på klub 2
        ///// </summary>
        //[XmlElement(ElementName = "KlubNavn2")]
        //public string KlubNavn2 { get; set; }

        ///// <summary>
        ///// score for klub 1
        ///// </summary>
        //[XmlElement(ElementName = "Score1")]
        //public double Score1 { get; set; }

        ///// <summary>
        ///// score for klub 2
        ///// </summary>
        //[XmlElement(ElementName = "Score2")]
        //public double Score2 { get; set; }

        /// <summary>
        /// find point for en klub mod en anden
        /// </summary>
        /// <param name="klub"></param>
        /// <param name="modKlub"></param>
        /// <returns></returns>
        public double PointMod(string klub, string modKlub)
        {
            double resultat = 0.0;

            if (klub == MatchKlubber[0].Navn && modKlub == MatchKlubber[1].Navn)
            {
                resultat = MatchKlubber[0].Score;
            }
            else if (klub == MatchKlubber[1].Navn && modKlub == MatchKlubber[0].Navn)
            {
                resultat = MatchKlubber[1].Score;
            }
            return resultat;
        }

        /// <summary>
        /// find point imod en klub fra en anden
        /// </summary>
        /// <param name="klub"></param>
        /// <param name="modKlub"></param>
        /// <returns></returns>
        public double ModstanderPoint(string klub, string modKlub)
        {
            double resultat = 0.0;

            if (klub == MatchKlubber[0].Navn && modKlub == MatchKlubber[1].Navn)
            {
                resultat = MatchKlubber[1].Score;
            }
            else if (klub == MatchKlubber[1].Navn && modKlub == MatchKlubber[0].Navn)
            {
                resultat = MatchKlubber[0].Score;
            }
            return resultat;
        }

        /// <summary>
        /// make a clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Match clone = new Match{Id = this.Id, MatchKlubber = new MatchKlub[this.MatchKlubber.Length]};
            for (int i = 0; i < this.MatchKlubber.Length; i++)
            {
                clone.MatchKlubber[i] = new MatchKlub() { Navn = this.MatchKlubber[i].Navn, Score = this.MatchKlubber[i].Score };
            }

            clone.MatchGruppePoint = new List<GruppePoint>();
            foreach (var gp in this.MatchGruppePoint)
            {
                clone.MatchGruppePoint.Add(new GruppePoint() { GruppeNavn = gp.GruppeNavn, PointsTilFordeling = gp.PointsTilFordeling, KlubPoint1 = gp.KlubPoint1, KlubPoint2 = gp.KlubPoint2 });
            }

            return clone;
        }
    }

    /// <summary>
    /// klubber i en match
    /// </summary>
    public class MatchKlub
    {
        private string _navn = string.Empty;
        private double _score;
        public MatchKlub()
        {
        }

        public MatchKlub(string navn, double score)
        {
            _navn = navn;
            _score = score;
        }

        public string Navn
        {
            get
            {
                return _navn;
            }

            set
            {
                _navn= value;
            }
        }

        public double Score
        {
            get
            {
                return _score;
            }

            set
            {
                _score = value;
            }
        }
    }

    public class GruppePoint
    {
        public string GruppeNavn { get; set; }
        public string PointsTilFordeling { get; set; }
        public double KlubPoint1 { get; set; }
        public double KlubPoint2 { get; set; }
    }

    //[XmlRoot(ElementName = "Løber")]
    /// <summary>
    /// løber i et stævne
    /// </summary>
    public class Løber : ICloneable
    {
        /// <summary>
        /// løberens startummer
        /// </summary>
        [XmlElement(ElementName = "StNr")]
        public string StNr { get; set; }

        /// <summary>
        /// løberens fornavn
        /// </summary>
        [XmlElement(ElementName = "Fornavn")]
        public string Fornavn { get; set; }

        /// <summary>
        /// løberens efternavn
        /// </summary>
        [XmlElement(ElementName = "EfterNavn")]
        public string EfterNavn { get; set; }

        /// <summary>
        /// løbklassen
        /// </summary>
        [XmlElement(ElementName = "Loebsklasse")]
        public string Løbsklasse { get; set; }

        /// <summary>
        /// Løberens gruppeklasse
        /// </summary>
        [XmlElement(ElementName = "GruppeKlasse")]
        public string GruppeKlasse { get; set; }

        /// <summary>
        /// Løberens gruppe
        /// </summary>
        [XmlElement(ElementName = "Gruppe")]
        public string Gruppe { get; set; }

        /// <summary>
        /// navn på løberens klub
        /// </summary>
        [XmlElement(ElementName = "KlubNavn")]
        public string KlubNavn { get; set; }

        /// <summary>
        /// løberens start tid
        /// </summary>
        [XmlElement(ElementName = "StartTid")]
        public string StartTid { get; set; }

        /// <summary>
        /// løberens Tid
        /// </summary>
        [XmlElement(ElementName = "Tid")]
        public string Tid { get; set; }

        /// <summary>
        /// løberens placering i klassen
        /// </summary>
        [XmlElement(ElementName = "Placering")]
        public string Placering { get; set; }

        /// <summary>
        /// løberens status
        /// </summary>
        [XmlElement(ElementName = "Status")]
        public string Status { get; set; }

        /// <summary>
        /// liste af løberns point i matcherne i stævnet
        /// </summary>
        public List<MatchPoint> MatchPoints { get; set; }

        /// <summary>
        /// lav en klon
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Løber clone = new Løber();
            clone.StNr = this.StNr;
            clone.Fornavn = this.Fornavn;
            clone.EfterNavn = this.EfterNavn;
            clone.Gruppe = this.Gruppe;
            clone.GruppeKlasse = this.GruppeKlasse;
            clone.Løbsklasse = this.Løbsklasse;
            clone.KlubNavn = this.KlubNavn;
            clone.StartTid = this.StartTid;
            clone.Tid = this.Tid;
            clone.Placering = this.Placering;
            clone.Status = this.Status;

            clone.MatchPoints = new List<MatchPoint>();
            foreach (var mp in this.MatchPoints)
            {
                clone.MatchPoints.Add((MatchPoint) mp.Clone());
            }
            return clone;
        }
    }


    //[XmlRoot(ElementName = "MatchPoint")]
    /// <summary>
    /// en løbers point i en match
    /// </summary>
    public class MatchPoint : ICloneable
    {
        /// <summary>
        /// reference til match
        /// </summary>
        [XmlElement(ElementName = "MatchId")]
        public int MatchId { get; set; }

        /// <summary>
        /// point
        /// </summary>
        [XmlElement(ElementName = "Point")]
        public double Point{ get; set; }

        /// <summary>
        /// lav en klon
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            MatchPoint clone = new MatchPoint();
            clone.MatchId = this.MatchId;
            clone.Point= this.Point;

            return clone;
        }
    }


    //[XmlRoot(ElementName = "DivisionsMatchResultat")]
    /// <summary>
    /// klasse for en stævne
    /// </summary>
    public class DivisionsMatchResultat : ICloneable
    {
        /// <summary>
        /// datoen for stævnet
        /// </summary>
        [XmlElement(ElementName = "Dato")]
        public string Dato { get; set; }

        /// <summary>
        /// Stævnets runde i divisionen
        /// </summary>
        [XmlElement(ElementName = "Runde")]
        public int Runde { get; set; }

        /// <summary>
        /// Stævnets skov
        /// </summary>
        [XmlElement(ElementName = "Skov")]
        public string Skov { get; set; }

        /// <summary>
        /// beskrivelse
        /// </summary>
        [XmlElement(ElementName = "Beskriv")]
        public string Beskriv { get; set; }

        /// <summary>
        /// liste af klubber i matchen
        /// </summary>
        public List<Klub> Klubber { get; set; }

        /// <summary>
        /// liste af klubmatcher i stævnet
        /// </summary>
        public List<Match> Matcher { get; set; }

        /// <summary>
        /// løbere i stævnet
        /// </summary>
        [XmlArray("Loebere")]
        [XmlArrayItem("Loeber")]
        public List<Løber> Løbere { get; set; }

        /// <summary>
        /// find sum af point for en klub mod en anden i alle matcher
        /// </summary>
        /// <param name="klub"></param>
        /// <param name="klubber"></param>
        /// <returns>point</returns>
        public double PointMod(string klub, List<string> klubber)
        {
            double resultat = 0.0;

            foreach (var m in this.Matcher)
            {
                foreach (string k in klubber)
                {
                    resultat += m.PointMod(klub, k);
                }
            }
            return resultat;
        }

        /// <summary>
        /// find sum af point for andre klubber mod en klub i alle matcher
        /// </summary>
        /// <param name="klub"></param>
        /// <param name="klubber"></param>
        /// <returns>point</returns>
        public double ModstanderPoint(string klub, List<string> klubber)
        {
            double resultat = 0.0;

            foreach (var m in this.Matcher)
            {
                foreach (string k in klubber)
                {
                    resultat += m.ModstanderPoint(klub, k);
                }
            }
            return resultat;
        }

        /// <summary>
        /// lav en klon
        /// </summary>
        /// <returns>klon</returns>
        public object Clone()
        {
            DivisionsMatchResultat clone = new DivisionsMatchResultat();
            clone.Dato = this.Dato;
            clone.Runde = this.Runde;
            clone.Skov = this.Skov;
            clone.Klubber = new List<Divisionsmatch.DivisionsResultat.Klub>();
            foreach (Divisionsmatch.DivisionsResultat.Klub k in this.Klubber)
            {
                clone.Klubber.Add(k.Clone() as Klub);
            }
            clone.Matcher = new List<Divisionsmatch.DivisionsResultat.Match>();
            foreach (var m in this.Matcher)
            {
                clone.Matcher.Add(m.Clone() as Match);
            }
            clone.Løbere = new List<Divisionsmatch.DivisionsResultat.Løber>();
            foreach(var l in this.Løbere)
            {
                clone.Løbere.Add(l.Clone() as Løber);
            }

            return clone;
        }   
    }
    
    /// <summary>
    /// top klasse for en liste af stævner i en division
    /// </summary>
    [XmlRoot(ElementName = "DivisionsResultat")]
    public class DivisionsResultat : ICloneable
    {
        private string _version = "1";

        /// <summary>
        /// vesion of the XML schema
        /// </summary>
        [XmlAttribute("version")]
        public string Version
        {
            get
            {
                return _version;
            }

            set
            {
                _version = value;

            }
        }

        /// <summary>
        /// året for divisionsmatcherne - alle stævner skal have samme år
        /// </summary>
        [XmlElement(ElementName = "Aar")]
        public int År { get; set; }

        /// <summary>
        /// divisionen - alle stævner skal have samme division
        /// </summary>
        [XmlElement(ElementName = "Division")]
        public int Division { get; set; }

        /// <summary>
        /// Kredsen for divisionen - alle stævner skal have samme kreds
        /// </summary>
        [XmlElement(ElementName = "Kreds")]
        public KredsId Kreds { get; set; }

        /// <summary>
        /// liste af stævner
        /// </summary>
        public List<DivisionsMatchResultat> DivisionsMatchResultater { get; set; }

        /// <summary>
        /// liste af stævner
        /// </summary>
        public List<TotalKlubResultat> DivisionsStilling
        {
            get;
            set;
        }

        /// <summary>
        /// constructor
        /// </summary>
        public DivisionsResultat()
        {
        }

        /// <summary>
        /// find point for en klub mod andre klubber
        /// </summary>
        /// <param name="klub"></param>
        /// <param name="klubber"></param>
        /// <returns>point</returns>
        public double PointMod(string klub, List<string> klubber)
        {
            double resultat = 0.0;

            if (this.DivisionsMatchResultater != null)
            {
                foreach (var m in this.DivisionsMatchResultater)
                {
                    resultat += m.PointMod(klub, klubber);
                }
            }

            return resultat;
        }

        /// <summary>
        /// find point for andre klubber mod en klub
        /// </summary>
        /// <param name="klub"></param>
        /// <param name="klubber"></param>
        /// <returns>point</returns>
        public double ModstanderPoint(string klub, List<string> klubber)
        {
            double resultat = 0.0;

            if (this.DivisionsMatchResultater != null)
            {
                foreach (var m in this.DivisionsMatchResultater)
                {
                    resultat += m.ModstanderPoint(klub, klubber);
                }
            }

            return resultat;
        }

        /// <summary>
        /// check om stævnet er kompatibelt med divisionsresultatet
        /// </summary>
        /// <param name="staevne">stævne</param>
        /// <param name="msg">evt retur fejlbesked</param>
        /// <returns>true/false</returns>
        public bool CheckStaevne(Staevne staevne, out string msg)
        {
            bool ok = true;
            msg = string.Empty;
            if (this.År != staevne.Dato.Year)
            {
                ok = false;
                msg = "Stævnets dato er ikke i samme år som divisionsresultaterne";
            }

            if (staevne.Config.Type == "Divisionsmatch" && this.Division != staevne.Config.Division)
            {
                ok = false;
                msg = "Stævnets division passer ikke med divisionsresultaterne";
            }

            if (staevne.Config.Type != "Finale" && this.Kreds.Navn != staevne.Config.Kreds)
            {
                ok = false;
                msg = "Stævnets kreds stemmer ikke med divisionsresultaterne";
            }

            if (this.DivisionsMatchResultater != null && this.DivisionsMatchResultater.Count > 0)
            {
                foreach (var k in staevne.Config.Klubber)
                {
                    if (!this.DivisionsMatchResultater[0].Klubber.Exists(K => K.Navn.Equals(k.Navn, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        ok = false;
                        msg = "Klubben " + k + " findes ikke i divisionsresultaterne";
                    }
                }
            }

            if (this.DivisionsMatchResultater != null && this.DivisionsMatchResultater.Count > 0)
            {
                // scenario 1 - ny fil fra o-service uden resultater
                // hvis det ene resultat har forkert dato er det en fejl
                if (this.DivisionsMatchResultater.Count == 1 && 
                    (this.DivisionsMatchResultater[0].Matcher == null || this.DivisionsMatchResultater[0].Matcher.Count == 0))
                {
                    if (!this.DivisionsMatchResultater[0].Dato.Equals(staevne.Dato.ToString("yyyy-MM-dd")))
                    {
                        ok = false;
                        msg = "Stævnets dato matcher ikke data fra o-service";
                    }
                }
                else
                {
                    // scenario 2 - fil fra oservice med resultater
                    // her kan vi ikke tillade dubletter
                    foreach (var r in this.DivisionsMatchResultater)
                    {
                        if (r.Dato.Equals(staevne.Dato.ToString("yyyy-MM-dd")))
                        {
                            ok = false;
                            msg = "Stævnets dato findes allerede i divisionsresultaterne";
                        }
                    }
                }
            }

            return ok;
        }

        public void FixMatcher()
        {
            foreach (var dr in DivisionsMatchResultater)
            {
                int i = 1;
                foreach (var m in dr.Matcher)
                {
                    m.Id = i++;
                }
            }
        }


        /// <summary>
        /// lave en klon
        /// </summary>
        /// <returns>klon</returns>
        public object Clone()
        {
            DivisionsResultat clone = new DivisionsResultat();
            clone.År = this.År;
            clone.Division = this.Division;
            clone.Kreds = this.Kreds.Clone() as Divisionsmatch.DivisionsResultat.KredsId; // new KredsId(this.Kreds.Kreds, this.Kreds.Id);
            clone.DivisionsMatchResultater = new List<Divisionsmatch.DivisionsResultat.DivisionsMatchResultat>();
            if (this.DivisionsMatchResultater != null)
            {
                foreach (var d in this.DivisionsMatchResultater)
                {
                    clone.DivisionsMatchResultater.Add(d.Clone() as DivisionsMatchResultat);
                }
            }

            return clone;
        }

        /// <summary>
        /// konstruer samlet divisionsresultat ved at lægge stævnet til
        /// </summary>
        /// <param name="staevne">stævnet som skal lægges til tidligere resultater</param>
        /// <returns>nyt divisionresultat</returns>
        public DivisionsResultat TotalDivisionsResultat(Staevne staevne)
        {
            // copy current  
            DivisionsResultat totalDivisionsResultat = this.Clone() as DivisionsResultat;

            ////if (totalDivisionsResultat.DivisionsMatchResultater.Count > 0)
            ////{
            ////    // fjern seneste stævne
            ////    DivisionsMatchResultat denneRunde = totalDivisionsResultat.DivisionsMatchResultater [totalDivisionsResultat.DivisionsMatchResultater.Count-1];
            ////    totalDivisionsResultat.DivisionsMatchResultater.Remove(denneRunde);
            ////}
            DivisionsMatchResultat detteResultat = new DivisionsMatchResultat();

            // info fra dialog
            detteResultat.Dato = staevne.Config.Dato.ToString("yyyy-MM-dd");
            detteResultat.Skov = staevne.Config.Skov;
            detteResultat.Runde = staevne.Config.Runde;

            // fyld med detaljer fra stævnet
            detteResultat.Klubber = new List<Divisionsmatch.DivisionsResultat.Klub>();
            var klubresultatliste = staevne.KlubberEfterPlacering;
            foreach (var k in staevne.Klubber)
            {
                Divisionsmatch.DivisionsResultat.Klub klub = new Divisionsmatch.DivisionsResultat.Klub();
                if (k.Id != null)
                {
                    klub.Id = new KlubId(k.Id.Id, k.Id.Type);
                }
                klub.Navn = k.Navn;
                klub.NavnKort = k.NavnKort;
                klub.Klubber = new List<Klub>();
                foreach (var kk in k.Klubber)
                {
                    Klub newKlub = new Klub() { Id = new KlubId(kk.Id.Id, kk.Id.Type), Navn = kk.Navn, NavnKort = kk.NavnKort };
                    klub.Klubber.Add(newKlub);
                }

                klub.Placering = klubresultatliste.IndexOf(k) + 1;
                klub.MatchPoint = k.Point;
                klub.LøbsPoint = k.Score1;
                klub.ModstanderLøbsPoint = k.Score2;
                klub.Kommentar = k.Kommentar;

                detteResultat.Klubber.Add(klub);
            }
            detteResultat.Matcher = new List<Divisionsmatch.DivisionsResultat.Match>();
            foreach (var m in staevne.Matcher)
            {
                Divisionsmatch.DivisionsResultat.Match match = new Divisionsmatch.DivisionsResultat.Match();
                match.Id = m.Id;
                match.MatchKlubber[0].Navn = m.Klub1.Navn;
                match.MatchKlubber[1].Navn = m.Klub2.Navn;
                match.MatchKlubber[0].Score = m.Score1();
                match.MatchKlubber[1].Score = m.Score2();

                match.MatchGruppePoint = new List<GruppePoint>();
                foreach (var g in staevne.Grupper)
                {
                    double p1 = m.Loebspoint1(g.Loebere);
                    double p2 = m.Loebspoint2(g.Loebere);
                    match.MatchGruppePoint.Add(new GruppePoint() { GruppeNavn = g.navn, PointsTilFordeling = g.PrintPointsForGruppe(), KlubPoint1 = p1, KlubPoint2 = p2 });
                }

                detteResultat.Matcher.Add(match);
            }
            detteResultat.Løbere = new List<Divisionsmatch.DivisionsResultat.Løber>();
            foreach (var l in staevne.Loebere)
            {
                // if klubben findes i matchen
                if (l.Value.Inkl && staevne.Klubber.Exists(k => k.Navn.Equals(l.Value.Klub.Navn) || k.NavnKort.Equals(l.Value.Klub.Navn)))
                {
                    Divisionsmatch.DivisionsResultat.Løber løber = new Divisionsmatch.DivisionsResultat.Løber();
                    løber.StNr = l.Value.Stnr;
                    løber.Fornavn = l.Value.Fornavn;
                    løber.EfterNavn = l.Value.Efternavn;
                    løber.KlubNavn = l.Value.Klub.Navn;
                    løber.Løbsklasse = l.Value.Løbsklassenavn;
                    løber.GruppeKlasse = l.Value.Gruppeklasse;
                    løber.Gruppe = staevne.Config.gruppeOgKlasse.FirstOrDefault(g => g.LøbsKlasse == l.Value.Løbsklassenavn).Gruppe;
                    løber.Placering = l.Value.Placering;
                    løber.StartTid = l.Value.StartTid;
                    løber.Tid = l.Value.Tid;
                    løber.Status = l.Value.Status;

                    løber.MatchPoints = new List<MatchPoint>();
                    foreach (var m in staevne.Matcher)
                    {
                        double p = l.Value.GetPoint(m);
                        if (p > 0)
                        {
                            MatchPoint mp = new MatchPoint() { MatchId = m.Id, Point = p };
                            løber.MatchPoints.Add(mp);
                        }
                    }

                    detteResultat.Løbere.Add(løber);
                }
            }
            totalDivisionsResultat.DivisionsMatchResultater.Add(detteResultat);

            totalDivisionsResultat.DivisionsStilling = _BeregnStilling(totalDivisionsResultat);

            return totalDivisionsResultat;
        }

        /// <summary>
        /// Beregn stilling ved at lægge stævnet til
        /// </summary>
        /// <param name="staevne">stævnet som skal lægges til tidligere resultater</param>
        /// <returns>data table med data</returns>
        public DataTable BeregnStilling(Staevne staevne)
        {
            // først evt tidligere matcher
            return ToDataTable(_BeregnStilling(this.TotalDivisionsResultat(staevne)));
        }

        private List<TotalKlubResultat> _BeregnStilling(DivisionsResultat divisionsResultat)
        {
            // hver klub har placering, navn, matchpoint, løbspoint, evt indbyrdes point
            // i tilfælde af match pointlighed tælles point i indbyrdes matcher parvis.
            List<TotalKlubResultat> TotalResultat = new List<TotalKlubResultat>();

            foreach (DivisionsMatchResultat r in divisionsResultat.DivisionsMatchResultater)
            {
                foreach (var k in r.Klubber)
                {
                    TotalKlubResultat totalKlubResultat;
                    if (TotalResultat.Exists(t => t.Klubnavn.Equals(k.Navn, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        totalKlubResultat = TotalResultat.Find(t => t.Klubnavn.Equals(k.Navn, StringComparison.InvariantCultureIgnoreCase));
                    }
                    else
                    {
                        totalKlubResultat = new TotalKlubResultat() { Klubnavn = k.Navn };
                        TotalResultat.Add(totalKlubResultat);
                    }
                    var rundeResultat = new RundeResultat() { Runde = r.Runde.ToString(), LøbsPoint = Convert.ToDouble(k.LøbsPoint), ModstanderLøbsPoint = Convert.ToDouble(k.ModstanderLøbsPoint), MatchPoint = Convert.ToDouble(k.MatchPoint), Kommentar = k.Kommentar, Matcher = new List<RundeMatch>() };
                    foreach (var m in r.Matcher)
                    {
                        if (m.MatchKlubber[0].Navn.Equals(k.Navn, StringComparison.InvariantCultureIgnoreCase))
                        {
                            rundeResultat.Matcher.Add(new RundeMatch() { egenscore = m.MatchKlubber[0].Score, modstander = m.MatchKlubber[1].Navn, modstanderscore = m.MatchKlubber[1].Score});
                        }
                        if (m.MatchKlubber[1].Navn.Equals(k.Navn, StringComparison.InvariantCultureIgnoreCase))
                        {
                            rundeResultat.Matcher.Add(new RundeMatch() { egenscore = m.MatchKlubber[1].Score, modstander = m.MatchKlubber[0].Navn, modstanderscore = m.MatchKlubber[0].Score });
                        }
                    }
                    totalKlubResultat.Runder.Add(rundeResultat);
                }
            }

            // sorter på MatchPoint
            TotalResultat.Sort(delegate (TotalKlubResultat x, TotalKlubResultat y)
            {
                // sort first by matchpoint
                int result= -1 * x.Stilling.MatchPoint.CompareTo(y.Stilling.MatchPoint);
                return result;
            });

            // find ud af om der er MatchPoint lighed
            int i = 0;
            double prevMatchPoint = double.MaxValue;
            int prevPlacering = 0;
            string prevKlub = string.Empty;
            Dictionary<int, Dictionary<string, double>> commonResult = new Dictionary<int, Dictionary<string, double>>();
            foreach ( var t in TotalResultat)
            {
                i = i + 1;
                if (t.Stilling.MatchPoint < prevMatchPoint)
                {
                    t.Placering = i;
                }
                else
                {
                    t.Placering = prevPlacering;
                    if (!commonResult.ContainsKey(prevPlacering))
                    {
                        commonResult[prevPlacering] = new Dictionary<string, double>(){ { prevKlub, 0 } };
                    }
                    commonResult[prevPlacering].Add(t.Klubnavn, 0);
                }

                prevPlacering = t.Placering;
                prevMatchPoint = t.Stilling.MatchPoint;
                prevKlub = t.Klubnavn;
            }

            // hvis der er klubber med lige mange matchpoint, skal de sorteres på basis af indbyrdes score
            if (commonResult.Count>0)
            {
                // for hver placering med dubletter
                foreach (var p in commonResult.Keys.ToList())
                {
                    // for hver klub for denne placering finde point imode de andre klubber
                    foreach (var k in commonResult[p].Keys.ToList())
                    {
                        List<string> andre = commonResult[p].Keys.Where(item => !item.Equals(k)).ToList();
                        commonResult[p][k] += TotalResultat.First(item => item.Klubnavn.Equals(k)).PointMod(andre);
                    }

                    // sæt en ny placering og en kommentar
                    int placering = p;
                    double prevValue = 0;
                    prevPlacering = p;
                    // sorter klubber imod indbyrdes point
                    foreach (var item in commonResult[p].OrderByDescending(item => item.Value))
                    {
                        // find klubben i totalresult
                        var tkr = TotalResultat.Find(k => k.Klubnavn == item.Key);
                        tkr.kommentar = "(" + item.Value.ToString() + ")";

                        if (item.Value != prevValue)
                        {
                            prevPlacering = placering;
                        }
                        tkr.Placering = prevPlacering;

                        placering++;
                        prevValue = item.Value;
                    }
                }

                // rank klubber med samme placering efter indbyrdes bane placeringer
                // åååå
                foreach (int pl in TotalResultat.Select(t => t.Placering).Distinct())
                {
                    if (TotalResultat.Count(t => t.Placering.Equals(pl)) > 1)
                    {
                        // flere klubber med samme placring efter sammenligning af matchpoint, og indbyrdes løbspoint
                        // så skal antallet af 1.pladser, 2.pladser,... afgøre det
                        int p = pl;
                        foreach (var totklub in _rankPlaceringer(divisionsResultat, 1, TotalResultat.Where(t => t.Placering.Equals(pl)).ToList()))
                        {
                            // sæt placringer
                            totklub.Placering = p++;
                        }
                    }
                }

                // sorter igen på placering
                TotalResultat.Sort(delegate (TotalKlubResultat x, TotalKlubResultat y)
                {
                    int result = x.Placering.CompareTo(y.Placering);
                    return result;
                });
            }

            return TotalResultat;
        }

        private List<TotalKlubResultat> _rankPlaceringer(DivisionsResultat divisionsResultat, int placering, List<TotalKlubResultat> klubber)
        {
            List<TotalKlubResultat> placeringKlub = new List<TotalKlubResultat>();

            Dictionary<TotalKlubResultat, int> taelPlacering = new Dictionary<TotalKlubResultat, int>();

            foreach (var d in divisionsResultat.DivisionsMatchResultater)
            {
                foreach (string g in d.Løbere.Select(l => l.Gruppe).Distinct())
                {
                    // find løberne fra klubberne i denne gruppe som har OK resultat
                    var ll = d.Løbere.Where(lob => lob.Gruppe.Equals(g) && lob.Status == "OK" && klubber.Count(k => k.Klubnavn.Equals(lob.KlubNavn)) > 0);

                    // er der mindst 'placering' antal løbere fra klubberne i denne gruppe, så find pick løberen
                    var l = ll.ElementAtOrDefault(placering - 1);
                    if (l != null)
                    {
                        // find kliubben
                        var t = klubber.FirstOrDefault(k => k.Klubnavn.Equals(l.KlubNavn));
                        if (t != null)
                        {
                            if (taelPlacering.ContainsKey(t))
                            {
                                taelPlacering[t] += 1;
                            }
                            else
                            {
                                taelPlacering.Add(t, 1);
                            }
                        }
                    }
                }
            }

            // fordel klubber med disse placering
            foreach (int pl in taelPlacering.Values.Distinct().OrderByDescending(item => item))
            {
                var kk = taelPlacering.Where(kv => kv.Value == pl);
                if (kk.Count() > 1)
                {
                    // sorter disse klubber på næste placering og tilføj dem til resultatlisten
                    placeringKlub.AddRange(_rankPlaceringer(divisionsResultat, (placering + 1), kk.Select(kv => kv.Key).ToList()));
                }
                else
                {
                    // tilføj klubben til resultatlisten
                    foreach (var k in kk.Select(kv => kv.Key))
                    {
                        k.kommentar += ", " + pl.ToString() + " " + placering.ToString() + ". pl ";
                    }
                    
                    placeringKlub.AddRange(kk.Select(kv => kv.Key));
                }
            }

            // tilføj klubber uden denne placering i enden
            placeringKlub.AddRange(klubber.Where(k => !taelPlacering.ContainsKey(k)));

            return placeringKlub;
        }

        private DataTable ToDataTable(IList<TotalKlubResultat> totalResultat)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(TotalKlubResultat));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.Name == "Runder" && totalResultat.Count > 0)
                {
                    PropertyDescriptorCollection props2 = TypeDescriptor.GetProperties(typeof(RundeResultat));
                    for (int r = 1; r <= totalResultat[0].Runder.Count; r++)
                    {
                        for (int j = 0; j < props2.Count; j++)
                        {
                            PropertyDescriptor prop2 = props2[j];
                            if (prop2.Name != "Matcher" && props2[j].Name != "Kommentar" && props2[j].Name != "Runde" && props2[j].Name != "ModstanderLøbsPoint")
                            {
                                if (props2[j].Name == "LøbsPoint")
                                {
                                    table.Columns.Add(string.Format("{0}. runde\r{1}", r, prop2.Name), typeof(string));
                                }
                                else
                                {
                                    table.Columns.Add(string.Format("{0}. runde\r{1}", r, prop2.Name), prop2.PropertyType);
                                }
                            }
                        }
                    }
                }
                else if (prop.Name == "Stilling" && totalResultat.Count > 0)
                {
                    PropertyDescriptorCollection props2 = TypeDescriptor.GetProperties(typeof(RundeResultat));
                    for (int j = 0; j < props2.Count; j++)
                    {
                        PropertyDescriptor prop2 = props2[j];
                        if (prop2.Name != "Matcher" && props2[j].Name != "Kommentar" && props2[j].Name != "Runde" && props2[j].Name != "ModstanderLøbsPoint")
                        {
                            if (props2[j].Name == "LøbsPoint")
                            {
                                table.Columns.Add(string.Format("{0}\r{1}", prop.Name, prop2.Name), typeof(string));
                            }
                            else
                            {
                                table.Columns.Add(string.Format("{0}\r{1}", prop.Name, prop2.Name), prop2.PropertyType);
                            }
                        }
                    }
                }
                else
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
            }
            object[] values = new object[table.Columns.Count];
            foreach (TotalKlubResultat item in totalResultat)
            {
                int vx = 0;
                for (int i = 0; i < props.Count; i++)
                {
                    if (props[i].Name == "Runder")
                    {
                        PropertyDescriptorCollection props2 = TypeDescriptor.GetProperties(typeof(RundeResultat));
                        for (int r = 0; r < item.Runder.Count; r++)
                        {
                            for (int j = 0; j < props2.Count; j++)
                            {
                                if (props2[j].Name != "Matcher" && props2[j].Name != "Kommentar" && props2[j].Name != "Runde" && props2[j].Name != "ModstanderLøbsPoint")
                                {
                                    if (props2[j].Name == "LøbsPoint")
                                    {
                                        double p1 = (double) props2[j].GetValue(item.Runder[r]);
                                        for (int jj = j + 1; jj < props2.Count; jj++)
                                        {
                                            if (props2[jj].Name == "ModstanderLøbsPoint")
                                            {
                                                double p2 = (double)props2[jj].GetValue(item.Runder[r]);
                                                values[vx++] = string.Format("{0} - {1}", p1.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo), p2.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        values[vx++] = props2[j].GetValue(item.Runder[r]);
                                    }
                                }
                            }
                        }
                    }
                    else if (props[i].Name == "Stilling")
                    {
                        PropertyDescriptorCollection props2 = TypeDescriptor.GetProperties(typeof(RundeResultat));
                            for (int j = 0; j < props2.Count; j++)
                        {
                            if (props2[j].Name != "Matcher" && props2[j].Name != "Kommentar" && props2[j].Name != "Runde" && props2[j].Name != "ModstanderLøbsPoint")
                            {
                                if (props2[j].Name == "LøbsPoint")
                                {
                                    double p1 = (double) props2[j].GetValue(item.Stilling);
                                    for (int jj = j + 1; jj < props2.Count; jj++)
                                    {
                                        if (props2[jj].Name == "ModstanderLøbsPoint")
                                        {
                                            double p2 = (double) props2[jj].GetValue(item.Stilling);
                                            values[vx++] = string.Format("{0} - {1}", p1.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo), p2.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo));
                                        }
                                    }
                                }
                                else
                                {
                                    values[vx++] = props2[j].GetValue(item.Stilling);
                                }
                            }
                        }
                    }
                    else
                    {
                        values[vx++] = props[i].GetValue(item);
                    }
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public class RundeMatch
        {
            public string modstander { get; set; }
            public double egenscore { get; set; }
            public double modstanderscore { get; set; }

            /// <summary>
            /// find sum af point for en klub mod en anden i alle matcher
            /// </summary>
            /// <param name="klubber"></param>
            /// <returns></returns>
            public double PointMod(List<string> klubber)
            {
                return klubber.Contains(modstander) ? egenscore: 0.0;
            }
        }

        public class RundeResultat
        {
            public string Runde { get; set; }

            [XmlElement("LoebsPoint")]
            public double LøbsPoint { get; set; }

            [XmlElement("ModstanderLoebsPoint")]
            public double ModstanderLøbsPoint { get; set; }


            public double MatchPoint { get; set; }

            public string Kommentar { get; set; }

            public List<RundeMatch> Matcher { get; set; }

            /// <summary>
            /// find sum af point for en klub mod en anden i alle matcher
            /// </summary>
            /// <param name="klub"></param>
            /// <param name="klubber"></param>
            /// <returns></returns>
            public double PointMod(string klub, List<string> klubber)
            {
                double resultat = 0.0;

                foreach (var m in this.Matcher)
                {
                    resultat += m.PointMod(klubber);
                }
                return resultat;
            }
        }

        public class TotalKlubResultat
        {
            private List<RundeResultat> _runder = null;
            public int Placering { get; set; }
            public string Klubnavn { get; set; }
            public List<RundeResultat> Runder
            {
                get
                {
                    if (_runder == null)
                    {
                        _runder = new List<RundeResultat>();
                    }
                    return _runder;
                }
            }

            public RundeResultat Stilling
            {
                get
                {
                    RundeResultat x = new RundeResultat();
                    x.LøbsPoint = 0;
                    x.MatchPoint = 0;
                    foreach (var r in Runder)
                    {
                        x.MatchPoint += r.MatchPoint;
                        x.LøbsPoint += r.LøbsPoint;
                        x.ModstanderLøbsPoint += r.ModstanderLøbsPoint;
                    }
                    return x;
                }
            }

            /// <summary>
            /// kommentar for et resultat som forklarer indbyrdes score eller pladser
            /// </summary>
            public string kommentar { get; set; }

            /// <summary>
            /// find sum af point for klubben mod de andre i alle matcher
            /// </summary>
            /// <param name="klubber"></param>
            /// <returns></returns>
            public double PointMod(List<string> klubber)
            {
                double resultat = 0.0;

                foreach (var r in this.Runder)
                {
                    resultat += r.PointMod(Klubnavn, klubber);
                }
                return resultat;
            }
        }

        /// <summary>
        /// formatter hele resultatlisten som txt
        /// </summary>
        /// <returns>hele resultatet som txt</returns>
        public string PrintResultText(Staevne staevne)
        {
            if (!staevne.Config.PrintDiviResultat)
            {
                return string.Empty;

            }
            StringBuilder output = new StringBuilder();

            string line = "Stilling " + (this.Division > 6 ? string.Empty : (this.Division.ToString() + ". division")) + (this.Kreds == null ? string.Empty : (", " + this.Kreds.ToString()));
            if (this.Division == 8)
            {
                // op/ned
                line += ", op/ned " + staevne.Config.Beskrivelse;
            }
            else if (this.Division == 9)
            {
                // finale
                line += ", finale" + staevne.Config.Beskrivelse;
            }
            output.AppendLine(line);
            output.AppendLine(new string('-', line.Count()));
            output.AppendLine();

            DataTable dt = this.BeregnStilling(staevne);
            output.Append(ConvertDataTableToString(dt));

            if (this.DivisionsMatchResultater != null && staevne.Config.InklTidligere)
            {
                if (this.DivisionsMatchResultater.Count > 0)
                {
                    foreach (var r in this.DivisionsMatchResultater)
                    {
                        output.AppendLine();
                        output.AppendLine(r.Runde + ". runde, " + r.Dato + ", " + r.Skov);
                        output.AppendLine();
                        foreach (var klub in r.Klubber.OrderBy(k => k.Placering))
                        {
                            //output.AppendLine(k.Navn.PadRight(40) + "   " + k.LøbsPoint.ToString().PadRight(6).Substring(0,6)                                             + " - " + r.ModstanderPoint(k.Navn, r.Klubber.Select(klub => klub.Navn).Where(n => n != k.Navn).ToList()).ToString().PadRight(6).Substring(0,6)                                             + "  " + k.MatchPoint.ToString());
                            output.AppendLine(klub.Navn.PadRight(40) + "   " + klub.LøbsPoint.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(5) + " - " + r.ModstanderPoint(klub.Navn, r.Klubber.Select(k => k.Navn).Where(k => k != klub.Navn).ToList()).ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(5) + "  " + klub.MatchPoint.ToString("##0").PadLeft(5) + "   " + klub.Kommentar);
                        }
                        output.AppendLine();
                        int n = 1;
                        int maxL = 0;
                        foreach (var match in r.Matcher)
                        {
                            int L = (n.ToString() + " : " + match.MatchKlubber[0].Navn + " - " + match.MatchKlubber[1].Navn).PadRight(40).Length;
                            if (L > maxL)
                            {
                                maxL = L;
                            }
                            n++;
                        }
                        n = 1;
                        foreach (var match in r.Matcher)
                        {
                            output.Append((n.ToString() + " : " + match.MatchKlubber[0].Navn + " - " + match.MatchKlubber[1].Navn).PadRight(maxL) + " : ");
                            output.AppendLine(match.MatchKlubber[0].Score.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(5) + " - " + match.MatchKlubber[1].Score.ToString("##0.#", System.Globalization.NumberFormatInfo.InvariantInfo).PadLeft(5));
                            n++;
                        }
                    }
                }
            }

            output.AppendLine();
            output.AppendLine(PadCenter("---ooo---", 60));
            output.AppendLine();
            return output.ToString();
        }

        /// <summary>
        /// formatter hele resultatlisten som txt
        /// </summary>
        /// <returns>hele resultatet som txt</returns>
        public string PrintResultHtml(Staevne staevne)
        {
            if (!staevne.Config.PrintDiviResultat)
            {
                return string.Empty;

            }

            StringBuilder output = new StringBuilder();
            string line = string.Empty;
            if (staevne.Config.Layout == "Standard")
            {
                line = "<h3 class=\"matcher\">Stilling " + (this.Division > 6 ? string.Empty : (this.Division.ToString() + ". division")) + (this.Kreds == null ? string.Empty : (", " + this.Kreds.ToString()));
            }
            else if (staevne.Config.Layout == "Blå overskrifter")
            {
                line = "<h3 class=\"matchnavn\">Stilling " + (this.Division > 6 ? string.Empty : (this.Division.ToString() + ". division")) + (this.Kreds == null ? string.Empty : (", " + this.Kreds.ToString()));
            }

            if (this.Division == 8)
            {
                // op/ned
                line += ", op/ned " + staevne.Config.Beskrivelse;
            }
            else if (this.Division == 9)
            {
                // finale
                line += ", finale " + staevne.Config.Beskrivelse;
            }
            line += "</h3>";
            output.AppendLine(line);

            DataTable dt = this.BeregnStilling(staevne);
            output.Append(ConvertDataTableToHtml(dt, staevne.Config.Layout));

            if (this.DivisionsMatchResultater != null && staevne.Config.InklTidligere)
            {
                if (this.DivisionsMatchResultater.Count > 0)
                {
                    if (staevne.Config.Layout == "Standard")
                    {
                        output.AppendLine("<table width='100%'>");
                    }
                    else if (staevne.Config.Layout == "Blå overskrifter")
                    {
                        output.AppendLine("<div class=\"stilling0Container\">");
                    }

                    foreach (var r in this.DivisionsMatchResultater)
                    {
                        if (staevne.Config.Layout == "Standard")
                        {
                            output.AppendLine("<tr valign='top'><td colspan='2'>");
                            output.AppendLine("<b>" + r.Runde + ". runde, " + r.Dato + ", " + r.Skov + "</b>");
                            output.AppendLine("</td></tr>");

                            output.AppendLine("<tr valign='top'><td width='50%'>");

                            output.AppendLine("<table class=\"matcher\">");
                        }
                        else if (staevne.Config.Layout == "Blå overskrifter")
                        {
                            output.AppendLine("<div class=\"stilling0Header\">" + r.Runde + ". runde, " + r.Dato + ", " + r.Skov + "</div>");

                            output.AppendLine("<div class=\"stilling0\">");
                            output.AppendLine("<table class=\"stilling0\">");
                            output.AppendLine("<thead>");
                            output.AppendLine("<tr><th class=\"knavn\">Klubnavn</th><th colspan=3 style=\"text-align:center\">score</th><th>point</th><th>&nbsp;</th></tr>");
                            output.AppendLine("</thead>");
                            output.AppendLine("<tbody>");
                        }

                        foreach (var k in r.Klubber.OrderBy(item => item.Placering))
                        {
                            if (staevne.Config.Layout == "Standard")
                            {
                                output.AppendLine("<tr class=\"matcher\"><td class=\"matcher\">" + k.Navn + "</td><td class=\"matcher\">" + k.LøbsPoint.ToString() + "</td><td class=\"matcher\">-</td><td class=\"matcher\">" + r.ModstanderPoint(k.Navn, r.Klubber.Select(klub => klub.Navn).Where(n => n != k.Navn).ToList()).ToString() + "</td><td class=\"matcher\">" + k.MatchPoint.ToString() + "</td><td class=\"matcher\"  style=\"text-align:left\">" + k.Kommentar + "</td></tr>");
                            }
                            else if (staevne.Config.Layout == "Blå overskrifter")
                            {
                                output.AppendLine("<tr><td class=\"knavn\">" + k.Navn + "</td><td>" + k.LøbsPoint.ToString() + "</td><td>-</td><td>" + r.ModstanderPoint(k.Navn, r.Klubber.Select(klub => klub.Navn).Where(n => n != k.Navn).ToList()).ToString() + "</td><td>" + k.MatchPoint.ToString() + "</td><td class=\"kommentar\"  style=\"text-align:left\">" + k.Kommentar + "</td></tr>");
                            }
                        }

                        if (staevne.Config.Layout == "Standard")
                        {
                            output.AppendLine("</table>");
                            output.AppendLine("</td><td width='50%'>");

                            output.AppendLine("<table class=\"matcher\">");
                        }
                        else if (staevne.Config.Layout == "Blå overskrifter")
                        {
                            output.AppendLine("</tbody>");
                            output.AppendLine("</table>");
                            output.AppendLine("</div>");
                            output.AppendLine("<div class=\"matcher0\">");
                            output.AppendLine("<table class=\"matcher0\">");
                        }

                        foreach (var m in r.Matcher)
                        {
                            if (staevne.Config.Layout == "Standard")
                            {
                                output.AppendLine("<tr class=\"matcher\"><td class=\"matcher\">" + m.MatchKlubber[0].Navn + "</td><td class=\"matcher\">-</td><td class=\"matcher\">" + m.MatchKlubber[1].Navn + "</td><td class=\"matcher\">" + m.MatchKlubber[0].Score + "</td><td class=\"matcher\">-</td><td>" + m.MatchKlubber[1].Score + "</td></tr>");
                            }
                            else if (staevne.Config.Layout == "Blå overskrifter")
                            {
                                output.AppendLine("<tr><td class=\"knavn\">" + m.MatchKlubber[0].Navn + "</td><td>-</td><td class=\"knavn\">" + m.MatchKlubber[1].Navn + "</td><td>" + m.MatchKlubber[0].Score + "</td><td>-</td><td>" + m.MatchKlubber[1].Score + "</td></tr>");
                            }
                        }
                        if (staevne.Config.Layout == "Standard")
                        {
                            output.AppendLine("</table>");
                            output.AppendLine("</td></tr>");
                        }
                        else if (staevne.Config.Layout == "Blå overskrifter")
                        {
                            output.AppendLine("</table>");
                            output.AppendLine("</div>");
                        }
                    }
                    if (staevne.Config.Layout == "Standard")
                    {
                        output.AppendLine("</table>");
                    }
                    else if (staevne.Config.Layout == "Blå overskrifter")
                    {
                        output.AppendLine("</div>");
                    }
                }
            }
            return output.ToString();
        }

        /// <summary>
        /// convert a data table to a nice txt table
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private string ConvertDataTableToString(DataTable dataTable)
        {
            var output = new StringBuilder();

            var columnsWidths = new int[dataTable.Columns.Count];
            bool[] skipColumn = new bool[dataTable.Columns.Count];

            // Get column widths
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var length = row[i].ToString().Length;
                    if (columnsWidths[i] < length)
                        columnsWidths[i] = length;
                }
            }

            // Get Column Titles
            int multiline = 0;
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                if (dataTable.Columns[i].ColumnName.Contains('\r'))
                {
                    if (dataTable.Columns[i].ColumnName.Split('\r').Length > multiline)
                    {
                        multiline = dataTable.Columns[i].ColumnName.Split('\r').Length;
                    }
                }
                foreach (string s in dataTable.Columns[i].ColumnName.Split('\r'))
                {
                    var length = s.Length;
                    if (columnsWidths[i] < length)
                    {
                        columnsWidths[i] = length;
                    }
                }

                // undlad rundenummer
                skipColumn[i] = dataTable.Columns[i].ColumnName.EndsWith("Runde");
            }

            // Write Column titles
            string header = string.Empty;
            for (int m = 0; m < multiline; m++)
            {
                header = string.Empty;
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (!skipColumn[i])
                    {
                        var text = dataTable.Columns[i].ColumnName;
                        if (dataTable.Columns[i].ColumnName.Split('\r').Length > m)
                        {
                            text = dataTable.Columns[i].ColumnName.Split('\r')[m];
                        }
                        else
                        {
                            text = string.Empty;
                        }
                        header += "|" + PadCenter(text, columnsWidths[i] + 2);
                    }
                }
                header += "|";
                output.AppendLine(header);
            }
            output.AppendLine((new string('-', header.Length)));

            // Write Rows
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (!skipColumn[i])
                    {
                        var text = Convert.ToString(row[i], System.Globalization.CultureInfo.InvariantCulture);
                        if (dataTable.Columns[i].ColumnName.ToLower().Equals("klubnavn") || dataTable.Columns[i].ColumnName.ToLower().Equals("kommentar"))
                        {
                            output.Append("| " + text.PadRight(columnsWidths[i] + 1, ' '));
                        }
                        else
                        {
                            output.Append("|" + PadCenter(text, columnsWidths[i] + 2));
                        }
                    }
                }
                output.AppendLine("|");
            }
            return output.ToString();
        }

        private static string PadCenter(string text, int maxLength)
        {
            int diff = maxLength - text.Length;
            return new string(' ', diff / 2) + text + new string(' ', (int)(diff / 2.0 + 0.5));
        }

        /// <summary>
        /// convert a data table to a nice txt table
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="layout"></param>
        /// <returns></returns>
        private string ConvertDataTableToHtml(DataTable dataTable, string layout)
        {
            var output = new StringBuilder();

            var columnsWidths = new int[dataTable.Columns.Count];
            bool[] skipColumn = null;
            string[] columnClass = null;

            if (layout == "Standard")
            {
                output.AppendLine("<table class='matchgruppe'>");
                output.AppendLine("<thead class='matchgruppe'>");
            }
            else if (layout == "Blå overskrifter")
            {
                output.AppendLine("<div class=\"matchresultat\">");
                output.AppendLine("<table class=\"matchresultat\">");

                output.AppendLine("<thead>");
                skipColumn = new bool[dataTable.Columns.Count];
                columnClass = new string[dataTable.Columns.Count];
            }

            // Write Column titles
            string header = string.Empty;
            if (layout == "Standard")
            {
                header = "<tr class='matchgruppe'>";
            }
            else if (layout == "Blå overskrifter")
            {
                output.Append("<tr>");
            }
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                if (layout == "Standard")
                {
                    header += "<th class='matchgruppe'>" + dataTable.Columns[i].ColumnName.Replace("\r", "<br>") + "</th>";
                }
                else if (layout == "Blå overskrifter")
                {
                    string columnName = dataTable.Columns[i].ColumnName;
                    columnName = columnName.Replace("Placering", "pl");
                    if (columnName.EndsWith("Runde"))
                        skipColumn[i] = true;
                    else if (columnName.EndsWith("Klubnavn"))
                    {
                        columnClass[i] = " class=\"knavn\"";
                        output.Append("<th class=\"knavn\">" + columnName.Replace("\r", "<br>") + "</th>");
                    }
                    else
                        output.Append("<th>" + columnName.Replace("\r", "<br>") + "</th>");
                }
            }

            if (layout == "Standard")
            {
                header += "</tr>";
                output.AppendLine(header);
                output.AppendLine("</thead><tbody class=\"matchgruppe\">");
            }
            else if (layout == "Blå overskrifter")
            {
                output.AppendLine("</tr>");
                output.AppendLine("</thead>");
                output.AppendLine("<tbody>");
            }
            // Write Rows
            foreach (DataRow row in dataTable.Rows)
            {
                string tr = string.Empty;
                if (layout == "Standard")
                {
                    tr = "<tr class='matchgruppe'>";
                }
                else if (layout == "Blå overskrifter")
                {
                    output.Append("<tr>");
                }
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (layout == "Standard")
                    {
                        var text = Convert.ToString(row[i], System.Globalization.CultureInfo.InvariantCulture);
                        tr += "<td class='matchgruppe'>&nbsp;" + text +"</td>";
                    }
                    else if (layout == "Blå overskrifter")
                    {
                        if (!skipColumn[i])
                        {
                            var text = Convert.ToString(row[i], System.Globalization.CultureInfo.InvariantCulture);
                            output.Append("<td" + (columnClass[i] ?? "") + ">" + text + "</td>");
                        }
                    }
                }
                if (layout == "Standard")
                {
                    tr += "</tr>";
                    output.AppendLine(tr);
                }
                else if (layout == "Blå overskrifter")
                {
                    output.AppendLine("</tr>");
                }
            }

            if (layout == "Standard")
            {
                output.AppendLine("</tbody></table>");
            }
            else if (layout == "Blå overskrifter")
            {
                output.AppendLine("</tbody>");

                output.AppendLine("</table>");
                output.AppendLine("</div>");
            }
            return output.ToString();
        }
    }

    /// <summary>
    /// klasse til at holde id og type
    /// </summary>
    public class KredsId : ICloneable
    {
        private string _kredsnavn = string.Empty;
        private string _id = string.Empty;

        /// <summary>
        /// constructor
        /// </summary>
        public KredsId()
        {
        }

        /// <summary>
        /// constructor
        /// </summary>
        public KredsId(string kreds, string id)
        {
            _id = id;
            _kredsnavn= kreds;
        }

        /// <summary>
        /// navn på kredsen
        /// </summary>
        [XmlText]
        public string Navn
        {
            get { return _kredsnavn; }

            set { _kredsnavn = value; }
        }

        /// <summary>
        /// kredsens id
        /// </summary>
        [XmlAttribute("Id")]
        public string Id
        {
            get { return _id; }

            set { _id = value; }
        }

        /// <summary>
        /// skal be klon
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            KredsId clone = new KredsId();
            clone.Id = this.Id;
            clone.Navn = this.Navn;

            return clone;
        }

        /// <summary>
        /// return KredsId as string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _kredsnavn;
        }
    }
}
