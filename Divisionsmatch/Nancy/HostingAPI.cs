using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisionsmatch
{
    /// <summary>
    /// klasse til at hoste en Nancy server
    /// </summary>
    public class HostingAPI
    {
        private NancyHost hostNancy;

        private string _hostUrl;
        private string _port;

        /// <summary>
        /// klasse til at hoste en Nancy server
        /// </summary>
        public HostingAPI()
        {
            InformationServerModule ism = new InformationServerModule();
        }

        /// <summary>
        /// metode til at starte serveren
        /// </summary>
        /// <param name="port"></param>
        public void Start(string port)
        {
            _port = port;
            var hostConfig = new HostConfiguration
            {
                UrlReservations = new UrlReservations
                {
                    CreateAutomatically = true
                },
            };

            if (_hostUrl == null) _hostUrl = "http://localhost:" + port + "/";

            hostNancy = new NancyHost(hostConfig, new Uri(_hostUrl));

            hostNancy.Start();
        }

        /// <summary>
        /// metode til at stoppe serveren
        /// </summary>
        public void Stop()
        {
            hostNancy.Stop();
            hostNancy.Dispose();
        }
    }
}
