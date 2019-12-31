using Newtonsoft.Json;
using SIGESDOC.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace SIGESDOC.Web.Controllers
{
    public class InvokerController : Controller
    {
        
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult doGet(HttpRequest request, HttpResponse response)
        {
            var filename = "CARTA_191219_191202[R].pdf";
            return Json(filename, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void doPost(HttpRequest request, HttpResponse response)
        {
            String pathServlet = request.UserHostAddress;
            String fullPathServlet = request.RawUrl.ToString();
            int resInt = fullPathServlet.Length - pathServlet.Length;
            String serverPath = fullPathServlet.Substring(0, resInt + 1);

            if (!serverPath.Contains("localhost"))
            {
                serverPath = ConfigurationManager.AppSettings["PROTOCOL"].ToString() + "://" + serverPath.Replace("http://", " ").Replace("https://", " ");
            }
            try
            {
                String type = request.Params["type"].ToString();
                String arguments = "";

                String protocol = "";

                if (serverPath.Contains("https://"))
                {
                    protocol = "S";
                }
                else
                {
                    protocol = "T";
                }

                if (type.Equals("W"))
                {
                    arguments = paramWeb(protocol, serverPath);
                }
                else if (type.Equals("L"))
                {
                    arguments = paramLocal(protocol, serverPath);
                }

                var sr = new StreamReader(arguments, UTF8Encoding.UTF8);
                sr.ReadToEnd();
                // System.IO.File.CreateText(arguments);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string paramLocal(string protocol, string ServerPath)
        {

            UrlHelper urlHelper = new UrlHelper();
            string base64String = null;
            string CLIENTID = ConfigurationManager.AppSettings["CLIENTID"];
            string CLIENTSECRET = ConfigurationManager.AppSettings["CLIENTSECRET"];
            string fileDownloadLogoUrl = urlHelper.Content(string.Format("{0}", ConfigurationManager.AppSettings["fileDownloadLogoUrl"], "firma1.jpg"));
            string fileDownloadStampUrl = urlHelper.Content(string.Format("{0}", ConfigurationManager.AppSettings["fileDownloadStampUrl"], "firma2.jpg"));

            Dictionary<string, object> param = new Dictionary<string, object>() {

                { "app",  "pdf"},
                { "fileUploadUrl",  ""},
                { "reason",  "Soy el autor del documento"},
                { "type",  "L"},
                { "clientId",  CLIENTID},
                { "clientSecret",  CLIENTSECRET},
                { "dcfilter",  "pdf"},
                { "fileDownloadUrl",  ""},
                { "posx",  "5"},
                { "posy",  "5"},
                { "outputFile",  "Nombre documento"},
                { "protocol",  "T"},
                { "contentFile", "demo.pdf"},
                { "stampAppearanceId", "0" },
                { "isSignatureVisible","true" },
                { "stampAppearanceId", "0" },
                { "idFile", "MyForm" },
                { "fileDownloadLogoUrl", fileDownloadLogoUrl },
                { "fileDownloadStampUrl", fileDownloadStampUrl },
                { "pageNumber", "0" },
                { "maxFileSize", "5242880" },
                { "fontSize", "7" },
                { "timestamp", "false" },
            };
            string json = JsonConvert.SerializeObject(param, Formatting.None);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            base64String = Convert.ToBase64String(bytes);
            return base64String;
        }

        public string paramWeb(string protocol, string ServerPath)
        {
            UrlHelper urlHelper = new UrlHelper();
            string base64String = null;
            string CLIENTID = ConfigurationManager.AppSettings["CLIENTID"];
            string CLIENTSECRET = ConfigurationManager.AppSettings["CLIENTSECRET"];
            string fileDownloadLogoUrl = urlHelper.Content(string.Format("{0}", ConfigurationManager.AppSettings["fileDownloadLogoUrl"], "firma1.jpg"));
            string fileDownloadStampUrl = urlHelper.Content(string.Format("{0}", ConfigurationManager.AppSettings["fileDownloadStampUrl"], "firma2.jpg"));

            Dictionary<string, object> param = new Dictionary<string, object>() {
                { "app",  "pdf"},
                { "fileUploadUrl",  ""},
                { "reason",  "Soy el autor del documento"},
                { "type",  "W"},
                { "clientId",  CLIENTID},
                { "clientSecret",  CLIENTSECRET},
                { "dcfilter",  "pdf"},
                { "fileDownloadUrl",  ""},
                { "posx",  "5"},
                { "posy",  "5"},
                { "outputFile",  "Nombre documento"},
                { "protocol",  "T"},
                { "contentFile", "demo.pdf"},
                { "stampAppearanceId", "0" },
                { "isSignatureVisible","true" },
                { "stampAppearanceId", "0" },
                { "idFile", "MyForm" },
                { "fileDownloadLogoUrl", fileDownloadLogoUrl },
                { "fileDownloadStampUrl", fileDownloadStampUrl },
                { "pageNumber", "0" },
                { "maxFileSize", "5242880" },
                { "fontSize", "7" },
                { "timestamp", "false" },

               };

            string json = JsonConvert.SerializeObject(param, Formatting.None);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            base64String = Convert.ToBase64String(bytes);

            return base64String;
        }

    }
}