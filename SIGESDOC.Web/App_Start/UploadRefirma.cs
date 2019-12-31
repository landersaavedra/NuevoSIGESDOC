using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SIGESDOC.Web.App_Start
{
    public class UploadRefirma
    {
        private long serialVersionUID = 1L;
        private String UPLOAD_DIRECTORY = "upload";
        private int THRESHOLD_SIZE = 1024 * 1024 * 3; 	// MB
        private int MAX_FILE_SIZE = 1024 * 1024 * 100; // MB
        private int MAX_REQUEST_SIZE = 1024 * 1024 * 110; // MB 


        public UploadRefirma()
        {

        }

        protected void doGet(HttpWebRequest request, HttpWebResponse response)
        {
            try
            {
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void doPost()
        {

        }
    }
}