using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisionsmatch
{
    public class HostingAPI
    {
        private NancyHost hostNancy;

        private string _hostUrl;
        private string _port;

        public HostingAPI()
        {
            InformationServerModule ism = new InformationServerModule();
        }

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

        public void Stop()
        {
            hostNancy.Stop();
            hostNancy.Dispose();
        }
    }
}
