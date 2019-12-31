using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SIGESDOC.Request;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace SIGESDOC.Web.App_Start
{
    public class Alfresco
    {
        public class FormFile
        {
            public string Name { get; set; }

            public string ContentType { get; set; }

            public string FilePath { get; set; }

            public Stream Stream { get; set; }
        }
        public string DevuelveTicket(string connection)
        {
            //variable de salida del token
            string salida_token = string.Empty;

            //variable de desearealizacion de Username y Password de ALfresco
            string connect = ConfigurationManager.AppSettings[connection].ToString();
            login acceso = new login();
            acceso = JsonConvert.DeserializeObject<login>(System.IO.File.ReadAllText(connect));
            string outjson = JsonConvert.SerializeObject(acceso, Formatting.Indented);

            //path de alfresco para el servicio
            string connectAlfresco = ConfigurationManager.AppSettings["Alfresco"].ToString();

            //path de llamado Alfresco para token
            string pathAlfresco = connectAlfresco + "/api/login";

            //configuracion de llamado de servicio 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pathAlfresco);
            request.KeepAlive = true;
            request.Method = "POST";
            byte[] postBytes = Encoding.UTF8.GetBytes(outjson);
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.MediaType = "application/json";
            request.ContentLength = postBytes.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream reStream = response.GetResponseStream();
                var sr = new StreamReader(response.GetResponseStream());
                string salida = sr.ReadToEnd();
                var data = ToObject(salida) as IDictionary<string, object>;

                foreach (var token in data)
                {
                    var tikets = data[token.Key] as IDictionary<string, object>;

                    foreach (var tiket in tikets)
                    {
                        acceso.token = tiket.Value.ToString();
                    }

                    salida_token = acceso.token;
                }

            }

            return salida_token;
        }

        public static object ToObject(string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;
            return ToObject(JToken.Parse(json));
        }

        public static object ToObject(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    return token.Children<JProperty>()
                                .ToDictionary(prop => prop.Name,
                                              prop => ToObject(prop.Value),
                                              StringComparer.OrdinalIgnoreCase);

                case JTokenType.Array:
                    return token.Select(ToObject).ToList();

                default:
                    return ((JValue)token).Value;
            }
        }
        public string POSTFormDataAlfresco(string uuid, string url, string method, string ticket)
        {

            string JsonSalida = string.Empty;
            string remoteURL = url + method + "?alf_ticket=" + ticket;
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            string posString = String.Format("{0}", uuid);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(remoteURL);

            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                string data = "Content-Disposition: form-data; name=\"" + "uuid" + "\"\r\n\r\n" + uuid;
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
                requestStream.Write(bytes, 0, bytes.Length);
                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                requestStream.Write(trailer, 0, trailer.Length);
                requestStream.Close();
            }

            using (WebResponse response = request.GetResponse())
            {
                System.Text.StringBuilder sb = new StringBuilder();
                using (Stream responseStream = response.GetResponseStream())

                using (StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public string PostPDFsFormDataAlfresco(string filedata, string contenttype, string fileName, string uploadDirectory, string url, string method, string ticket)
        {
            Stream file = System.IO.File.OpenRead(filedata);

            string JsonSalida = string.Empty;
            string remoteURL = url + method + "?alf_ticket=" + ticket;
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            string posString = String.Format("{0}, {1}, {2}", file, fileName, uploadDirectory);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(remoteURL);

            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Dictionary<string, object> parameters = new Dictionary<string, object>() {
                //application/vnd.openxmlformats-officedocument.wordprocessingml.document
                { "filedata",  new FormFile() { Name = fileName, ContentType = contenttype, FilePath = filedata }},
                { "fileName",  fileName},
                { "uploadDirectory",  uploadDirectory},
            };

            if (parameters != null && parameters.Count > 0)
            {

                using (Stream requestStream = request.GetRequestStream())
                {

                    foreach (KeyValuePair<string, object> pair in parameters)
                    {

                        requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                        if (pair.Value is FormFile)
                        {
                            FormFile archivo = pair.Value as FormFile;
                            string header = "Content-Disposition: form-data; name=\"" + pair.Key + "\"; filename=\"" + archivo.Name + "\"\r\nContent-Type: " + archivo.ContentType + "\r\n\r\n";
                            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(header);
                            requestStream.Write(bytes, 0, bytes.Length);
                            byte[] buffer = new byte[32768];
                            int bytesRead;
                            if (archivo.Stream == null)
                            {
                                using (FileStream fileStream = System.IO.File.OpenRead(archivo.FilePath))
                                {
                                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                                        requestStream.Write(buffer, 0, bytesRead);
                                    fileStream.Close();
                                }
                            }
                            else
                            {
                                while ((bytesRead = archivo.Stream.Read(buffer, 0, buffer.Length)) != 0)
                                    requestStream.Write(buffer, 0, bytesRead);
                            }
                        }
                        else
                        {
                            string data = "Content-Disposition: form-data; name=\"" + pair.Key + "\"\r\n\r\n" + pair.Value;
                            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
                            requestStream.Write(bytes, 0, bytes.Length);
                        }
                    }

                    byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                    requestStream.Write(trailer, 0, trailer.Length);
                    requestStream.Close();
                }
            }


            using (WebResponse response = request.GetResponse())
            {
                System.Text.StringBuilder sb = new StringBuilder();
                using (Stream responseStream = response.GetResponseStream())

                using (StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }



    }
}