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
    public class ResultatDefinition
    {
        private string _filnavn = null;
        private string _tmpFileName = null;
        private string _meosUrl = null;
        private int _interval = -1;

        public ResultatDefinition()
        {
        }

        public ResultatDefinition(string filnavn):base()
        {
            _filnavn = filnavn;
        }

        public ResultatDefinition(string meosUrl, int intervalSecs):base()
        {
            _meosUrl = meosUrl;
            _tmpFileName = Path.GetTempFileName();
            GetMeOSData();

            _filnavn = _tmpFileName;
            _interval = intervalSecs;
        }

        public string Filnavn
        {
            get
            {
                return _filnavn;
            }
        }

        public string MeOSUrl
        {
            get
            {
                return _meosUrl;
            }
        }

        public int IntervalMeOS
        {
            get
            {
                return _interval;
            }
        }

        public bool IsMeOS
        {
            get
            {
                return !string.IsNullOrWhiteSpace(_meosUrl);
            }
        }

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
