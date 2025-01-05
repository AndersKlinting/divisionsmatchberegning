using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Divisionsmatch
{
    /// <summary>
    /// klasse til konfiguration af automatisk opdatering af resultater
    /// </summary>
    public class ResultatDefinition
    {
        private string _filnavn = null;
        private string _tmpFileName = null;
        private string _meosUrl = null;
        private int _interval = -1;

        /// <summary>
        /// default creator
        /// </summary>
        public ResultatDefinition()
        {
        }

        /// <summary>
        /// creator for klasse med sti til fil med resultater
        /// </summary>
        /// <param name="filnavn">sti til fil med resultater</param>
        public ResultatDefinition(string filnavn):base()
        {
            _filnavn = filnavn;
        }

        /// <summary>
        /// creator for klasse link til MeOS information serevr
        /// </summary>
        /// <param name="meosUrl">url til MeOS</param>
        /// <param name="intervalSecs">antal sekuknder for opdateringsinterval</param>
        public ResultatDefinition(string meosUrl, int intervalSecs):base()
        {
            _meosUrl = meosUrl;
            _tmpFileName = Path.GetTempFileName();
            GetMeOSData();

            _filnavn = _tmpFileName;
            _interval = intervalSecs;
        }

        /// <summary>
        /// egenskab for filnavn med resultater
        /// </summary>
        public string Filnavn
        {
            get
            {
                return _filnavn;
            }
        }

        /// <summary>
        /// egenskab for url til MeOS informatin server
        /// </summary>
        public string MeOSUrl
        {
            get
            {
                return _meosUrl;
            }
        }

        /// <summary>
        /// antal sekuner imellem opdatering af data fra MeOS
        /// </summary>
        public int IntervalMeOS
        {
            get
            {
                return _interval;
            }
        }

        /// <summary>
        /// flag for om MeOS url er konfigureret
        /// </summary>
        public bool IsMeOS
        {
            get
            {
                return !string.IsNullOrWhiteSpace(_meosUrl);
            }
        }

        /// <summary>
        /// formatter meos url
        /// </summary>
        /// <returns>meos url</returns>
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(_filnavn) ? string.Empty: (string.IsNullOrWhiteSpace(_meosUrl) ? _filnavn : _meosUrl);
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            try
            {
                GetMeOSData();
            }
            finally
            {
            }
        }

        /// <summary>
        /// hente MeOS resultater fra MeOS information server
        /// </summary>
        public void GetMeOSData()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(_meosUrl);
            req.Method = "GET";
            using (WebResponse resp = req.GetResponse())
            {
                using (Stream dataStream = resp.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        File.WriteAllText(_tmpFileName, reader.ReadToEnd());
                    }
                }
            }
        }
    }
}
