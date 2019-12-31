using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;

namespace SIGESDOC.Web.App_Start
{
    public class getFileRefirma
    {
        private static  long serialVersionUID = 1L;
        private static String UPLOAD_DIRECTORY = "upload";
        public void doGet(string documentName)
        {

            //const string pattern =  @"http://localhost:8089/getFileRefirma?"+documentName;

            //var url = Patt

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(documentName);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                       
            byte[] data = System.IO.File.ReadAllBytes(documentName);
            response.ContentType = "application/pdf";
            response.Headers.Add("Content-disposition" , "attachment; filename=" + documentName);
            response.Headers.Add("Cache-Control", "max-age=30");
            response.Headers.Add("Pragma", "No-cache");
            response.Headers.Add("Expires", "0");

            response.ContentLength = data.Length;

            Stream stream = response.GetResponseStream();
            //System.IO.File.OpenWrite(stream, 0, stream.Length);
            //HttpContext.Current.Response.
        }
            
        
    }
}