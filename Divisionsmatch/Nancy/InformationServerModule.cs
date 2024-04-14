using Nancy;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Divisionsmatch
{
    public class InformationServerModule : NancyModule
    {
        private frmNancy _frmNancy = null;

        public InformationServerModule()
        {
            if (_frmNancy == null)
            {
                if (frmDivi.Instance != null)
                {
                    _frmNancy = frmDivi.Instance.frmNancy;
                }
            }
            //log(this.Request);

            try
            {
                Get("/", args =>
                {
                    log(this.Request);

                    string html = string.Empty;
                    html = "<!DOCTYPE html>";
                    html += "<html>";
                    html += frmDivi.Instance.mitstaevne.GetHTMLHead();
                    html += "<body>";
                    html += "<h1>Divisionsmatch web server  og REST API</h1>";
                    html += Nancy.Helpers.HttpUtility.HtmlEncode("Divisionsmatch (" + frmDivi.Instance.mitstaevne.Config.Skov + ", " + frmDivi.Instance.mitstaevne.Config.Dato.ToString("yyyy-MM-dd") + ")");

                    string scrollspeed = "&scroll&refresh=300&speed=32";
                    html += "<h2>Fuld resultat</h2>";
                    html += "<p>HTML format: ";
                    html += string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>    med autoscroll: <a target=\"_blank\" href=\"{0}{1}\">{0}{1}</a>", "all?format=html", scrollspeed);
                    html += "<p>TXT format: ";
                    html += string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>    med autoscroll: <a target=\"_blank\" href=\"{0}{1}\">{0}{1}</a>", "all?format=txt", scrollspeed);
                    html += "<p>XML format: ";
                    html += string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>", "all?format=xml");

                    html += "<h2>Divisionsstilling</h2>";
                    html += "<p>HTML format: ";
                    html += string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>    med autoscroll: <a target=\"_blank\" href=\"{0}{1}\">{0}{1}</a>", "divi?format=html", scrollspeed);
                    html += "<p>TXT format: ";
                    html += string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>    med autoscroll: <a target=\"_blank\" href=\"{0}{1}\">{0}{1}</a>", "divi?format=txt", scrollspeed);

                    html += "<h2>Match-stilling</h2>";
                    html += "<p>HTML format: ";
                    html += string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>    med autoscroll: <a target=\"_blank\" href=\"{0}{1}\">{0}{1}</a>", "matchstilling?format=html", scrollspeed);
                    html += "<p>TXT format: ";
                    html += string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>    med autoscroll: <a target=\"_blank\" href=\"{0}{1}\">{0}{1}</a>", "matchstilling?format=txt", scrollspeed);

                    html += "<h2>Matcher detaljer</h2>";
                    html += "<p>HTML format: ";
                    html += string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>    med autoscroll: <a target=\"_blank\" href=\"{0}{1}\">{0}{1}</a>", "matchdetaljer?format=html", scrollspeed);
                    html += "<p>TXT format: ";
                    html += string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>    med autoscroll: <a target=\"_blank\" href=\"{0}{1}\">{0}{1}</a>", "matchdetaljer?format=txt", scrollspeed);

                    html += "<h2>Match-resultat</h2>";
                    html += "<p>HTML format: ";
                    html += string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>    med autoscroll: <a target=\"_blank\" href=\"{0}{1}\">{0}{1}</a>", "matchresultater?format=html", scrollspeed);
                    html += "<p>TXT format: ";
                    html += string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>    med autoscroll: <a target=\"_blank\" href=\"{0}{1}\">{0}{1}</a>", "matchresultater?format=txt", scrollspeed);

                    html += "</body></html>";
                    
                    //return Response.AsXml(html);

                    return HtmlResponse(html);
                });
                
                Get("/divi.css", args =>
                {
                    log(this.Request);

                    string output = string.Empty;
                    if (this.Request.Url.ToString().EndsWith("divi.css"))
                    {
                        output = getResource("divi.css");                        
                    }

                    return HtmlResponse(output);
                });

                Get("/scroll/", args =>
                {
                    log(this.Request);

                    string output = string.Empty;
                    string outputType = string.Empty;
                    if (this.Request.Url.ToString().EndsWith("top.html"))
                    {
                        output = getResource("top.html");
                        outputType = "html";
                    }
                    else if (this.Request.Url.ToString().EndsWith("divi.css"))
                    {
                        output = getResource("divi.css");
                        outputType = "html";
                    }
                    else if (this.Request.Url.Query.ToString().Contains("jquery"))
                    {
                        int idx = this.Request.Url.Query.ToString().IndexOf("jquery");
                        string jq = this.Request.Url.Query.ToString().Substring(idx);
                        output = getResource(jq);
                        outputType = "js";
                    }

                    if (outputType=="html")
                    {
                        return HtmlResponse(output);
                    }
                    else
                    {
                        return Response.AsText(output);
                    }
                });

                Get("/all/", args =>
                {
                    log(this.Request);

                    string output = "resultatet findes ikke";
                    var form1 = frmDivi.Instance;
                    string format = this.Request.Query["format"];
                    bool scroll = this.Request.Query["scroll"] != null;
                    string refresh = this.Request.Query["refresh"];
                    string speed = this.Request.Query["speed"];
                    bool formatTxt = format.ToLower() == "txt";
                    bool formatHtml = format.ToLower() == "html";
                    bool formatXml = format.ToLower() == "xml";
                    if (scroll)
                    {
                        // scroll informsus that a frame page is to be loaded

                        output = getResource("frame.html");
                        Url newUrl = this.Request.Url;
                        newUrl.Query = "format=" + (formatTxt ? "txtx" : format) + (string.IsNullOrWhiteSpace(refresh) ? "" : "&refresh=" + refresh) + (string.IsNullOrWhiteSpace(speed) ? "" : "&speed=" + speed);
                        output = output.Replace("@@url@@", newUrl.ToString());
                        return HtmlResponse(output);
                    }

                    if (form1._mitDivisionsResultat != null)
                    {
                        if (formatHtml)
                        {
                            List<string> afsnit = new List<string>();

                            afsnit.Add(form1._mitDivisionsResultat.PrintResultHtml(form1.mitstaevne));
                            //afsnit.Add(form1.mitstaevne.Printstilling(true));
                            //afsnit.AddRange(form1.mitstaevne.LavHTMLafsnit());

                            afsnit.AddRange(form1.mitstaevne.LavResultatSektioner(true, true, true, true));

                            output = System.IO.File.ReadAllText(new System.Uri(form1.mitstaevne.LavHTML(afsnit)).LocalPath, Encoding.Default);
                        }
                        else if (formatXml)
                        {
                            output = Util.SerializeDivisionsResultat(form1._mitDivisionsResultat, form1.mitstaevne);
                        }
                        else
                        {
                            StringBuilder sb = new StringBuilder();
                            //sb.Append(form1._mitDivisionsResultat.PrintResultText(form1.mitstaevne));
                            //sb.AppendLine(form1.mitstaevne.Printmatcher());
                            //foreach (string t in form1.mitstaevne.LavTXTafsnit())
                            //{
                            //    sb.AppendLine(t);
                            //}

                            sb.Append(form1._mitDivisionsResultat.PrintResultText(form1.mitstaevne));
                            foreach (string t in form1.mitstaevne.LavResultatSektioner(false, true, true, true))
                            {
                                sb.AppendLine(t);
                            }

                            output = sb.ToString();
                        }
                    }

                    if (formatHtml)
                    {
                        return HtmlResponse(output);
                    }
                    else
                    {
                        if (formatTxt || formatXml)
                        {
                            return Response.AsText(output);
                        }
                        else 
                        {
                            // wrap txt in pre-tags to make it html
                            output = "<html><head><meta content=\"text/html;charset=utf-8\" http-equiv=\"Content-Type\"></head><body><pre>\n" + output + "\n</pre></body></html>";
                            return HtmlResponse(output);
                         }
                    }
                });

                Get("/divi/", args =>
                {
                    log(this.Request);

                    string output = "resultatet findes ikke";
                    var form1 = frmDivi.Instance;
                    bool scroll = this.Request.Query["scroll"] != null;
                    string refresh = this.Request.Query["refresh"];
                    string speed = this.Request.Query["speed"];
                    string format = this.Request.Query["format"];
                    bool formatHtml = format.ToLower() == "html";
                    bool formatTxt = format.ToLower() == "txt";
                    if (scroll)
                    {
                        output = getResource("frame.html");
                        Url newUrl = this.Request.Url;
                        newUrl.Query = "format=" + (formatTxt ? "txtx" : format)  + (string.IsNullOrWhiteSpace(refresh) ? "" : "&refresh=" + refresh) + (string.IsNullOrWhiteSpace(speed) ? "" : "&speed=" + refresh);
                        output = output.Replace("@@url@@", newUrl.ToString());
                        return HtmlResponse(output);
                    }

                    if (form1._mitDivisionsResultat != null)
                    {
                        if (formatHtml)
                        {
                            output = System.IO.File.ReadAllText(new System.Uri(form1.mitstaevne.LavHTML(new List<string>() { form1._mitDivisionsResultat.PrintResultHtml(form1.mitstaevne) })).LocalPath, Encoding.Default);
                        }
                        else
                        {
                            output = form1._mitDivisionsResultat.PrintResultText(form1.mitstaevne);
                        }
                    }
                    if (formatHtml)
                    {
                        return HtmlResponse(output);
                    }
                    else
                    {
                        if (formatTxt)
                        {
                            return Response.AsText(output);
                        }
                        else 
                        {
                            // wrap txt in pre-tags to make it html
                            output = "<html><head><meta content=\"text/html;charset=utf-8\" http-equiv=\"Content-Type\"></head><body><pre>\n" + output + "\n</pre></body></html>";
                            return HtmlResponse(output);
                         }
                    }
                });
                
                Get("/matchstilling/", args =>
                {
                    log(this.Request);

                    string output = "resultatet findes ikke";
                    var form1 = frmDivi.Instance;
                    bool scroll = this.Request.Query["scroll"] != null;
                    string format = this.Request.Query["format"];
                    string refresh = this.Request.Query["refresh"];
                    string speed = this.Request.Query["speed"];
                    bool formatHtml = format.ToLower() == "html";
                    bool formatTxt = format.ToLower() == "txt";

                    if (scroll)
                    {
                        output = getResource("frame.html");
                        Url newUrl = this.Request.Url;
                        newUrl.Query = "format=" + (formatTxt ? "txtx" : format)  + (string.IsNullOrWhiteSpace(refresh) ? "" : "&refresh=" + refresh) + (string.IsNullOrWhiteSpace(speed) ? "" : "&speed=" + refresh);
                        output = output.Replace("@@url@@", newUrl.ToString());
                        return HtmlResponse(output);
                    }

                    if (form1._mitDivisionsResultat != null)
                    {
                        if (formatHtml)
                        {
                            //output = System.IO.File.ReadAllText(new System.Uri(form1.mitstaevne.LavHTML(new List<string>() { form1.mitstaevne.Printstilling(true), form1.mitstaevne.LavHTMLStilling(form1.mitstaevne.Config) })).LocalPath, Encoding.Default);
                            output = System.IO.File.ReadAllText(new System.Uri(form1.mitstaevne.LavHTML(form1.mitstaevne.LavResultatSektioner(true, true, false, false))).LocalPath, Encoding.Default);
                        }
                        else
                        {
                            //output = form1.mitstaevne.Printmatcher();
                            StringBuilder sb = new StringBuilder();
                            foreach (string t in form1.mitstaevne.LavResultatSektioner(false, true, false, false))
                            {
                                sb.AppendLine(t);
                            }

                            output = sb.ToString();
                        }
                    }

                    if (formatHtml)
                    {
                        return HtmlResponse(output);
                    }
                    else
                    {
                        if (formatTxt)
                        {
                            return Response.AsText(output);
                        }
                        else 
                        {
                            // wrap txt in pre-tags to make it html
                            output = "<html><head><meta content=\"text/html;charset=utf-8\" http-equiv=\"Content-Type\"></head><body><pre>\n" + output + "\n</pre></body></html>";
                            return HtmlResponse(output);
                         }
                    }
                });

                Get("/matchdetaljer/", args =>
                {
                    log(this.Request);

                    string output = "resultatet findes ikke";
                    var form1 = frmDivi.Instance;
                    bool scroll = this.Request.Query["scroll"] != null;
                    string format = this.Request.Query["format"];
                    string refresh = this.Request.Query["refresh"];
                    string speed = this.Request.Query["speed"];
                    bool formatHtml = format.ToLower() == "html";
                    bool formatTxt = format.ToLower() == "txt";

                    if (scroll)
                    {
                        output = getResource("frame.html");
                        Url newUrl = this.Request.Url;
                        newUrl.Query = "format=" + (formatTxt ? "txtx" : format)  + (string.IsNullOrWhiteSpace(refresh) ? "" : "&refresh=" + refresh) + (string.IsNullOrWhiteSpace(speed) ? "" : "&speed=" + refresh);
                        output = output.Replace("@@url@@", newUrl.ToString());
                        return HtmlResponse(output);
                    }

                    if (form1._mitDivisionsResultat != null)
                    {
                        if (formatHtml)
                        {
                            //output = System.IO.File.ReadAllText(new System.Uri(form1.mitstaevne.LavHTML(new List<string>() { form1.mitstaevne.LavHTMLStilling(form1.mitstaevne.Config) })).LocalPath, Encoding.Default);
                            output = System.IO.File.ReadAllText(new System.Uri(form1.mitstaevne.LavHTML(form1.mitstaevne.LavResultatSektioner(true,false,true,false))).LocalPath, Encoding.Default);
                        }
                        else
                        {
                            //output = form1.mitstaevne.Printmatcher(false);
                            StringBuilder sb = new StringBuilder();
                            //foreach (string t in form1.mitstaevne.LavTXTafsnit())
                            foreach (string t in form1.mitstaevne.LavResultatSektioner(false, false, true, false))
                            {
                                sb.AppendLine(t);
                            }

                            output = sb.ToString();
                        }
                    }

                    if (formatHtml)
                    {
                        return HtmlResponse(output);
                    }
                    else
                    {
                        if (formatTxt)
                        {
                            return Response.AsText(output);
                        }
                        else 
                        {
                            // wrap txt in pre-tags to make it html
                            output = "<html><head><meta content=\"text/html;charset=utf-8\" http-equiv=\"Content-Type\"></head><body><pre>\n" + output + "\n</pre></body></html>";
                            return HtmlResponse(output);
                         }
                    }
                });

                Get("/matchresultater/", args =>
                {
                    log(this.Request);

                    string output = "resultatet findes ikke";
                    var form1 = frmDivi.Instance;
                    bool scroll = this.Request.Query["scroll"] != null;
                    string refresh = this.Request.Query["refresh"];
                    string speed = this.Request.Query["speed"];
                    string format = this.Request.Query["format"];
                    bool formatHtml = format.ToLower() == "html";
                    bool formatTxt = format.ToLower() == "txt";

                    if (scroll)
                    {
                        output = getResource("frame.html");
                        Url newUrl = this.Request.Url;
                        newUrl.Query = "format=" + (formatTxt ? "txtx" : format)  + (string.IsNullOrWhiteSpace(refresh) ? "" : "&refresh=" + refresh) + (string.IsNullOrWhiteSpace(speed) ? "" : "&speed=" + refresh);
                        output = output.Replace("@@url@@", newUrl.ToString());
                        return HtmlResponse(output);
                    }

                    if (form1._mitDivisionsResultat != null)
                    {
                        if (formatHtml)
                        {
                            // output = System.IO.File.ReadAllText(new System.Uri(form1.mitstaevne.LavHTML(form1.mitstaevne.LavHTMLafsnit())).LocalPath, Encoding.Default);
                            output = System.IO.File.ReadAllText(new System.Uri(form1.mitstaevne.LavHTML(form1.mitstaevne.LavResultatSektioner(true, false, false, true))).LocalPath, Encoding.Default);
                        }
                        else
                        {
                            StringBuilder sb = new StringBuilder();
                            //foreach (string t in form1.mitstaevne.LavTXTafsnit())
                            foreach (string t in form1.mitstaevne.LavResultatSektioner(false, false, false, true))
                            {
                                sb.AppendLine(t);
                            }

                            output = sb.ToString();
                        }
                    }

                    if (formatHtml)
                    {
                        return HtmlResponse(output);
                    }
                    else
                    {
                        if (formatTxt)
                        {
                            return Response.AsText(output);
                        }
                        else 
                        {
                            // wrap txt in pre-tags to make it html
                            output = "<html><head><meta content=\"text/html;charset=utf-8\" http-equiv=\"Content-Type\"></head><body><pre>\n" + output + "\n</pre></body></html>";
                            return HtmlResponse(output);
                         }
                    }
                });
            }
            catch
            {
                log(this.Request);
            }
        }

        private Response HtmlResponse(string html)
        {
            byte[]b = Encoding.UTF8.GetBytes(html);
            return new Response() { StatusCode = HttpStatusCode.OK, ContentType = "text/html", Contents = c => c.Write(b, 0, b.Length) }; 
        }

        private void log(Request req)
        {
            if (_frmNancy != null && req!=null)
            {
                _frmNancy.SetText(req.Url.ToString());
            }
        }

        private string getResource(string resName)
        {
            string result = string.Empty;
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(resName));
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        private string htmlEncode(string html)
        {
            string result = html;
            for (int i=160; i < 256;i++)
            {
                result = result.Replace(((char)i).ToString(), string.Format("&#{0};",i));
            }
            return result;
        }           
    }
}
