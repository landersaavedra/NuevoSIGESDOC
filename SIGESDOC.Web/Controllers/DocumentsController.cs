using SIGESDOC.IAplicacionService;
using SIGESDOC.Response;
using SIGESDOC.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using RazorPDF;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Globalization;
using System.Runtime.Remoting.Contexts;
using System.Web.UI.WebControls;

namespace SIGESDOC.Web.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IHojaTramiteService _HojaTramiteService;
        private readonly IHabilitacionesService _HabilitacionesService;

        public DocumentsController(IHojaTramiteService HojaTramiteService, IHabilitacionesService HabilitacionesService)
        {
            _HojaTramiteService = HojaTramiteService;
            _HabilitacionesService = HabilitacionesService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }



        #region Cedula de Notificacion
        public void CedulaNotificacionWord(CargaWordCedulaNotificacion tableData)
        {
            DateTime fecha_PATH = DateTime.Now;
            DateTime fecha = DateTime.Now;
            tableData.FECHA_ACTUAL = fecha.ToString("dd MMMM yyyy");

            //desarrollo variables de alfresco
            DocExtGetProperties docExt = new DocExtGetProperties();
            string uuidCedulaNotificacion = ConfigurationManager.AppSettings["templateCedulaNotificacion"].ToString();

            _HabilitacionesService.BuscarNuevoUIIDALfresco(null, null, uuidCedulaNotificacion);

            //conexion con alfresco
            string login = "login";
            string ticket = DevuelveTicket(login);

            //para obtener el documento modelo 
            string pathAlfresco = ConfigurationManager.AppSettings["alfresco"];
            string metodoAlfresco = @"/getProperties";
            string json = POSTFormDataAlfresco(uuidCedulaNotificacion, pathAlfresco, metodoAlfresco, ticket);

            docExt = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<DocExtGetProperties>(json);

            
            //Desarrollo Uri Alfresco
            string path = ConfigurationManager.AppSettings["UriAlfresco"];
            string filename = System.IO.Path.Combine(path + docExt.urlDownload);

            //Instancio Llamada por WebClient
            WebClient web = new WebClient();
            web.Credentials = CredentialCache.DefaultCredentials;
            web.Credentials = CredentialCache.DefaultNetworkCredentials;
            web.UseDefaultCredentials = true;

            //Llamo otro ticket de Permiso de acceso a Alfresco sin Usuario y Contraseña
            string ticket2 = DevuelveTicket(login);
            string down = filename + "?alf_ticket=" + ticket2;

            byte[] byteArray = web.DownloadData(down);

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);

                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(stream, true))
                {

                    IDictionary<String, BookmarkStart> bookmarkMaps = new Dictionary<String, BookmarkStart>();

                    foreach (BookmarkStart bookmarkStart in wordDocument.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                    {
                        bookmarkMaps[bookmarkStart.Name] = bookmarkStart;
                    }

                    Run NOM_DOC_TITULO = bookmarkMaps["A_NON_DOC"].NextSibling<Run>();
                    NOM_DOC_TITULO.GetFirstChild<Text>().Text = tableData.NON_DOC;

                    Run NUM_DOC = bookmarkMaps["NUM_DOC"].NextSibling<Run>();
                    NUM_DOC.GetFirstChild<Text>().Text = tableData.EXP_O_HT_N_CDL_NOTIF;

                    Run ASUNTO = bookmarkMaps["ASUNTO"].NextSibling<Run>();
                    ASUNTO.GetFirstChild<Text>().Text = tableData.ASUNTO;

                    Run DIRECCION_CDL_NOTIF = bookmarkMaps["DIRECCION_CDL_NOTIF"].NextSibling<Run>();
                    DIRECCION_CDL_NOTIF.GetFirstChild<Text>().Text = tableData.DIRECCION_CDL_NOTIF;

                    Run EMPRESA_CDL_NOTIF = bookmarkMaps["EMPRESA_CDL_NOTIF"].NextSibling<Run>();
                    EMPRESA_CDL_NOTIF.GetFirstChild<Text>().Text = tableData.EMPRESA_CDL_NOTIF;

                    Run FOLIA_CDL_NOTIF = bookmarkMaps["FOLIA_CDL_NOTIF"].NextSibling<Run>();
                    FOLIA_CDL_NOTIF.GetFirstChild<Text>().Text = tableData.FOLIA_CDL_NOTIF;

                    Run DOC_NOTIFICAR_CDL_NOTIF = bookmarkMaps["DOC_NOTIFICAR_CDL_NOTIF"].NextSibling<Run>();
                    DOC_NOTIFICAR_CDL_NOTIF.GetFirstChild<Text>().Text = tableData.DOC_NOTIFICAR_CDL_NOTIF;

                    //Run EXP_O_HT_N_CDL_NOTIF = bookmarkMaps["EXP_O_HT_N_CDL_NOTIF"].NextSibling<Run>();
                    //EXP_O_HT_N_CDL_NOTIF.GetFirstChild<Text>().Text = tableData.EXP_O_HT_N_CDL_NOTIF;

                    #region Acta de CD notificacion 1
                    Run A_NON_DOC1 = bookmarkMaps["A_NON_DOC1"].NextSibling<Run>();
                    A_NON_DOC1.GetFirstChild<Text>().Text = tableData.NON_DOC;

                    Run A_DIRECCION_CDL_NOTIF1 = bookmarkMaps["A_DIRECCION_CDL_NOTIF1"].NextSibling<Run>();
                    A_DIRECCION_CDL_NOTIF1.GetFirstChild<Text>().Text = tableData.DIRECCION_CDL_NOTIF;

                    Run A_DOC_NOTIFICAR_CDL_NOTIF1 = bookmarkMaps["A_DOC_NOTIFICAR_CDL_NOTIF1"].NextSibling<Run>();
                    A_DOC_NOTIFICAR_CDL_NOTIF1.GetFirstChild<Text>().Text = tableData.DOC_NOTIFICAR_CDL_NOTIF;

                    Run A_EXP_O_HT_N_CDL_NOTIF1 = bookmarkMaps["A_EXP_O_HT_N_CDL_NOTIF1"].NextSibling<Run>();
                    A_EXP_O_HT_N_CDL_NOTIF1.GetFirstChild<Text>().Text = tableData.EXP_O_HT_N_CDL_NOTIF;

                    #endregion

                    #region Acta de CD notificacion 2
                    Run A_NON_DOC2 = bookmarkMaps["A_NON_DOC2"].NextSibling<Run>();
                    A_NON_DOC2.GetFirstChild<Text>().Text = tableData.NON_DOC;

                    Run A_DIRECCION_CDL_NOTIF2 = bookmarkMaps["A_DIRECCION_CDL_NOTIF2"].NextSibling<Run>();
                    A_DIRECCION_CDL_NOTIF2.GetFirstChild<Text>().Text = tableData.DIRECCION_CDL_NOTIF;

                    Run A_DOC_NOTIFICAR_CDL_NOTIF2 = bookmarkMaps["A_DOC_NOTIFICAR_CDL_NOTIF2"].NextSibling<Run>();
                    A_DOC_NOTIFICAR_CDL_NOTIF2.GetFirstChild<Text>().Text = tableData.DOC_NOTIFICAR_CDL_NOTIF;

                    Run A_EXP_O_HT_N_CDL_NOTIF2 = bookmarkMaps["A_EXP_O_HT_N_CDL_NOTIF2"].NextSibling<Run>();
                    A_EXP_O_HT_N_CDL_NOTIF2.GetFirstChild<Text>().Text = tableData.EXP_O_HT_N_CDL_NOTIF;

                    #endregion

                    #region Acta de CD notificacion 3
                    Run A_NON_DOC3 = bookmarkMaps["A_NON_DOC3"].NextSibling<Run>();
                    A_NON_DOC3.GetFirstChild<Text>().Text = tableData.NON_DOC;

                    Run A_DIRECCION_CDL_NOTIF3 = bookmarkMaps["A_DIRECCION_CDL_NOTIF3"].NextSibling<Run>();
                    A_DIRECCION_CDL_NOTIF3.GetFirstChild<Text>().Text = tableData.DIRECCION_CDL_NOTIF;

                    Run A_DOC_NOTIFICAR_CDL_NOTIF3 = bookmarkMaps["A_DOC_NOTIFICAR_CDL_NOTIF3"].NextSibling<Run>();
                    A_DOC_NOTIFICAR_CDL_NOTIF3.GetFirstChild<Text>().Text = tableData.DOC_NOTIFICAR_CDL_NOTIF;

                    Run A_EXP_O_HT_N_CDL_NOTIF3 = bookmarkMaps["A_EXP_O_HT_N_CDL_NOTIF3"].NextSibling<Run>();
                    A_EXP_O_HT_N_CDL_NOTIF3.GetFirstChild<Text>().Text = tableData.EXP_O_HT_N_CDL_NOTIF;

                    #endregion 

                    #region  Acta de CD notificacion 4
                    Run A_NON_DOC4 = bookmarkMaps["A_NON_DOC4"].NextSibling<Run>();
                    A_NON_DOC4.GetFirstChild<Text>().Text = tableData.NON_DOC;

                    Run A_DIRECCION_CDL_NOTIF4 = bookmarkMaps["A_DIRECCION_CDL_NOTIF4"].NextSibling<Run>();
                    A_DIRECCION_CDL_NOTIF4.GetFirstChild<Text>().Text = tableData.DIRECCION_CDL_NOTIF;

                    Run A_DOC_NOTIFICAR_CDL_NOTIF4 = bookmarkMaps["A_DOC_NOTIFICAR_CDL_NOTIF4"].NextSibling<Run>();
                    A_DOC_NOTIFICAR_CDL_NOTIF4.GetFirstChild<Text>().Text = tableData.DOC_NOTIFICAR_CDL_NOTIF;

                    Run A_EXP_O_HT_N_CDL_NOTIF4 = bookmarkMaps["A_EXP_O_HT_N_CDL_NOTIF4"].NextSibling<Run>();
                    A_EXP_O_HT_N_CDL_NOTIF4.GetFirstChild<Text>().Text = tableData.EXP_O_HT_N_CDL_NOTIF;


                    #endregion

                    wordDocument.MainDocumentPart.Document.Save();
                    wordDocument.Close();
                }
                string path_word = @"C:\SIGESDOC\WORD\";
                string path_pdf = @"C:\SIGESDOC\PDF\";

                if (!Directory.Exists(path_word) && !Directory.Exists(path_pdf))
                {
                    Directory.CreateDirectory(path_word);
                    Directory.CreateDirectory(path_pdf);

                    string nuevoWord = Path.Combine(path_word, "CEDULANOTIFICACION_"+ fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "CEDULANOTIFICACION_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                    Process.Start(nuevoWord);

                }
                else
                {
                    string nuevoWord = Path.Combine(path_word, "CEDULANOTIFICACION_"+ fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "CEDULANOTIFICACION_"+fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                    Process.Start(nuevoWord);
                }
            }
        }

        #endregion

        #region RESOLUCION DIRECTORAL
        [HttpGet]
        public void ResolucionDirectoralWord(CargaWordResolucionDirectoral tableData)
        {
            DateTime fecha_PATH = DateTime.Now;
            DateTime fecha = DateTime.Now;
            tableData.FECHA_ACTUAL = fecha.ToString("dd MMMM yyyy");

            //desarrollo variables de alfresco
            DocExtGetProperties docExt = new DocExtGetProperties();
            string uuidResolucionDirectoral= ConfigurationManager.AppSettings["templateResolucionDirectoral"].ToString();

            //conexion con alfresco
            string login = "login";
            string ticket = DevuelveTicket(login);

            //para obtener el documento modelo 
            string pathAlfresco = ConfigurationManager.AppSettings["alfresco"];
            string metodoAlfresco = @"/getProperties";
            string json = POSTFormDataAlfresco(uuidResolucionDirectoral, pathAlfresco, metodoAlfresco, ticket);

            docExt = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<DocExtGetProperties>(json);

            //Desarrollo Uri Alfresco
            string path = ConfigurationManager.AppSettings["UriAlfresco"];
            string filename = System.IO.Path.Combine(path + docExt.urlDownload);

            //Instancio Llamada por WebClient
            WebClient web = new WebClient();
            web.Credentials = CredentialCache.DefaultCredentials;
            web.Credentials = CredentialCache.DefaultNetworkCredentials;
            web.UseDefaultCredentials = true;

            //Llamo otro ticket de Permiso de acceso a Alfresco sin Usuario y Contraseña
            string ticket2 = DevuelveTicket(login);
            string down = filename + "?alf_ticket=" + ticket2;

            byte[] byteArray = web.DownloadData(down);

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);

                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(stream, true))
                {

                    IDictionary<String, BookmarkStart> bookmarkMaps = new Dictionary<String, BookmarkStart>();

                    foreach (BookmarkStart bookmarkStart in wordDocument.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                    {
                        bookmarkMaps[bookmarkStart.Name] = bookmarkStart;
                    }

                    Run EMPRESA = bookmarkMaps["EMPRESA"].NextSibling<Run>();
                    EMPRESA.GetFirstChild<Text>().Text = tableData.EMPRESA_CDL_NOTIF;

                    Run EMPRESA_1 = bookmarkMaps["EMPRESA_1"].NextSibling<Run>();
                    EMPRESA_1.GetFirstChild<Text>().Text = tableData.EMPRESA_CDL_NOTIF;

                    Run EMPRESA_2 = bookmarkMaps["EMPRESA_2"].NextSibling<Run>();
                    EMPRESA_2.GetFirstChild<Text>().Text = tableData.EMPRESA_CDL_NOTIF;

                    Run FECHA_ACTUAL = bookmarkMaps["FECHA_ACTUAL"].NextSibling<Run>();
                    FECHA_ACTUAL.GetFirstChild<Text>().Text = tableData.FECHA_ACTUAL;

                    Run NOM_DOC = bookmarkMaps["NOM_DOC"].NextSibling<Run>();
                    NOM_DOC.GetFirstChild<Text>().Text = tableData.NOM_DOC;

                    Run RUC = bookmarkMaps["RUC"].NextSibling<Run>();
                    RUC.GetFirstChild<Text>().Text = tableData.RUC;

                    Run RUC_1 = bookmarkMaps["RUC_1"].NextSibling<Run>();
                    RUC_1.GetFirstChild<Text>().Text = tableData.RUC;

                    Run RUC_2 = bookmarkMaps["RUC_2"].NextSibling<Run>();
                    RUC_2.GetFirstChild<Text>().Text = tableData.RUC;

                    Run EXPEDIENTE = bookmarkMaps["EXPEDIENTE"].NextSibling<Run>();
                    EXPEDIENTE.GetFirstChild<Text>().Text = tableData.EXPEDIENTE;

                    Run EXPEDIENTE_1 = bookmarkMaps["EXPEDIENTE_1"].NextSibling<Run>();
                    EXPEDIENTE_1.GetFirstChild<Text>().Text = tableData.EXPEDIENTE;

                    Run EXPEDIENTE_2 = bookmarkMaps["EXPEDIENTE_2"].NextSibling<Run>();
                    EXPEDIENTE_2.GetFirstChild<Text>().Text = tableData.EXPEDIENTE;
                    wordDocument.MainDocumentPart.Document.Save();
                    wordDocument.Close();
                }

                string path_word = @"C:\SIGESDOC\WORD\";
                string path_pdf = @"C:\SIGESDOC\PDF\";

                if (!Directory.Exists(path_word) && !Directory.Exists(path_pdf))
                {
                    Directory.CreateDirectory(path_word);
                    Directory.CreateDirectory(path_pdf);

                    string nuevoWord = Path.Combine(path_word, "RESOLUCIONDIRECTORAL_"+  fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "RESOLUCIONDIRECTORAL_"+  fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                    Process.Start(nuevoWord);

                }
                else
                {
                    string nuevoWord = Path.Combine(path_word, "RESOLUCIONDIRECTORAL_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "RESOLUCIONDIRECTORAL_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                    Process.Start(nuevoWord);
                }
            }
        }
        #endregion

        #region Informe uti
        public void informeUTIWord(CargaWordInformeUTI tableData)
        {
            DateTime fecha_PATH = DateTime.Now;

            DateTime fecha = DateTime.Now;
            //tableData.FECHA_ACTUAL = fecha.ToString("dd MMMM yyyy");
            //desarrollo
            string path  = @"C:\Users\PSSERU-TI\Source\Repos\landersaavedra\sigesdoc\documentos externos";

           // string path = ConfigurationManager.AppSettings["informe"];
            byte[] byteArray = System.IO.File.ReadAllBytes(path + @"\RESOLUCION_DIRECTORAL.docx");

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);

                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(stream, true))
                {

                    IDictionary<String, BookmarkStart> bookmarkMaps = new Dictionary<String, BookmarkStart>();

                    foreach (BookmarkStart bookmarkStart in wordDocument.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                    {
                        bookmarkMaps[bookmarkStart.Name] = bookmarkStart;
                    }
                }
            }

        }

        #endregion

        #region OFICIO

        [HttpGet]
        public void OficioWord(CargaOficioWord tableData)
        {
            DateTime fecha_PATH = DateTime.Now;
            DateTime fecha = DateTime.Now;
            tableData.FECHA_ACTUAL = fecha.ToString("dd MMMM yyyy");

            string NOMBREDOC = string.Empty;

            //desarrollo variables de alfresco
            DocExtGetProperties docExt = new DocExtGetProperties();
            string uuidOficio = ConfigurationManager.AppSettings["templateOficio"].ToString();

            //conexion con alfresco
            string login = "login";
            string ticket = DevuelveTicket(login);

            //para obtener el documento modelo 
            string pathAlfresco = ConfigurationManager.AppSettings["alfresco"];
            string metodoAlfresco = @"/getProperties";
            string json = POSTFormDataAlfresco(uuidOficio, pathAlfresco, metodoAlfresco, ticket);
            
            docExt = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<DocExtGetProperties>(json);

            //Desarrollo Uri Alfresco
            string path = ConfigurationManager.AppSettings["UriAlfresco"];
            string filename = System.IO.Path.Combine(path + docExt.urlDownload);

            //Instancio Llamada por WebClient
            WebClient web = new WebClient();
            web.Credentials = CredentialCache.DefaultCredentials;
            web.Credentials = CredentialCache.DefaultNetworkCredentials;
            web.UseDefaultCredentials = true;

            //Llamo otro ticket de Permiso de acceso a Alfresco sin Usuario y Contraseña
            string ticket2 = DevuelveTicket(login);
            string down = filename + "?alf_ticket=" + ticket2;

            byte[] byteArray = web.DownloadData(down);

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);

                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(stream, true))
                {

                    IDictionary<String, BookmarkStart> bookmarkMaps = new Dictionary<String, BookmarkStart>();

                    foreach (BookmarkStart bookmarkStart in wordDocument.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                    {
                        bookmarkMaps[bookmarkStart.Name] = bookmarkStart;
                    }

                    if (tableData.TIPO_DOCUMENTO == "Consulta General de Documentos de la Oficina")
                    {
                        
                        int id_documento = Convert.ToInt32(tableData.ID_DOCUMENTO);

                            
                        var documento = _HojaTramiteService.Listar_Detalle_Documento_Interno(id_documento);
                        //detalle = _HojaTramiteService.Listar_Detalle_Documento(documento.First().id_tipo_documento);
                        var detalle_documento = _HojaTramiteService.GetDocumentoDetalleInterno(id_documento);
                        NOMBREDOC = documento.First().nom_doc;
                        Run NOM_DOC = bookmarkMaps["NOM_DOC"].NextSibling<Run>();
                        NOM_DOC.GetFirstChild<Text>().Text = documento.First().nom_doc;

                        Run EMPRESA = bookmarkMaps["EMPRESA"].NextSibling<Run>();
                        EMPRESA.GetFirstChild<Text>().Text = detalle_documento.First().nom_oficina_crea;

                        Run FECHA_ACTUAL = bookmarkMaps["FECHA_ACTUAL"].NextSibling<Run>();
                        FECHA_ACTUAL.GetFirstChild<Text>().Text = tableData.FECHA_ACTUAL;

                        Run ASUNTO = bookmarkMaps["ASUNTO"].NextSibling<Run>();
                        ASUNTO.GetFirstChild<Text>().Text = documento.First().asunto;

                        //Run CARGO = bookmarkMaps["CARGO"].NextSibling<Run>();
                        //CARGO.GetFirstChild<Text>().Text = tableData.CARGO;

                        Run DIRECCION = bookmarkMaps["DIRECCION"].NextSibling<Run>();
                        DIRECCION.GetFirstChild<Text>().Text = detalle_documento.First().nom_oficina_destino;

                        Run NOMBRES = bookmarkMaps["NOMBRES"].NextSibling<Run>();
                        NOMBRES.GetFirstChild<Text>().Text = documento.First().nombres;

                        //Run REFERENCIA = bookmarkMaps["REFERENCIA"].NextSibling<Run>();
                        //REFERENCIA.GetFirstChild<Text>().Text = string.Empty;


                        wordDocument.MainDocumentPart.Document.Save();
                        wordDocument.Close();

                    }
                    else
                    {
                        string obtenernuevo = tableData.ID_DOCUMENTO;

                       
                        int id_documento_Dhcpa = Convert.ToInt32(obtenernuevo);

                        var documento = _HojaTramiteService.Listar_Documento_DHCPA_Externo(id_documento_Dhcpa);
                        var documento_detalle = _HojaTramiteService.Listar_Documento_DHCPA_Detalle_Externo(id_documento_Dhcpa);
                        NOMBREDOC = documento.First().nom_doc;
                        Run NOM_DOC = bookmarkMaps["NOM_DOC"].NextSibling<Run>();
                        NOM_DOC.GetFirstChild<Text>().Text = documento.First().nom_doc;

                        Run EMPRESA = bookmarkMaps["EMPRESA"].NextSibling<Run>();
                        EMPRESA.GetFirstChild<Text>().Text = documento_detalle.First().oficina_externa;

                        Run FECHA_ACTUAL = bookmarkMaps["FECHA_ACTUAL"].NextSibling<Run>();
                        FECHA_ACTUAL.GetFirstChild<Text>().Text = tableData.FECHA_ACTUAL;

                        Run CARGO = bookmarkMaps["CARGO"].NextSibling<Run>();
                        CARGO.GetFirstChild<Text>().Text = documento_detalle.First().cargo;

                        Run ASUNTO = bookmarkMaps["ASUNTO"].NextSibling<Run>();
                        ASUNTO.GetFirstChild<Text>().Text = documento.First().asunto;

                        Run DIRECCION = bookmarkMaps["DIRECCION"].NextSibling<Run>();
                        DIRECCION.GetFirstChild<Text>().Text = documento_detalle.First().direccion_externo;

                        Run NOMBRES = bookmarkMaps["NOMBRES"].NextSibling<Run>();
                        NOMBRES.GetFirstChild<Text>().Text = documento_detalle.First().encargado;

                        wordDocument.MainDocumentPart.Document.Save();
                        wordDocument.Close();
                    }
                }

                string path_word = @"C:\SIGESDOC\WORD\";
                string path_pdf = @"C:\SIGESDOC\PDF\";

                if (!Directory.Exists(path_word) && !Directory.Exists(path_pdf))
                {
                    Directory.CreateDirectory(path_word);
                    Directory.CreateDirectory(path_pdf);

                    string nuevoWord = Path.Combine(path_word, "OFICIO_"+  fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "OFICIO_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                    Process.Start(nuevoWord);

                }
                else
                {
                    string nuevoWord = Path.Combine(path_word, "OFICIO_"  + fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "OFICIO_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                    Process.Start(nuevoWord);
                }
            }
        }

        #endregion

        #region INVITACION

        [HttpGet]
        public void InvitacionWord(CargaInvitacionWord tableData)
        {

            DateTime fecha_PATH = DateTime.Now;
            DateTime fecha = DateTime.Now;
            tableData.FECHA_ACTUAL = fecha.ToString("dd MMMM yyyy");

            //desarrollo variables de alfresco
            DocExtGetProperties docExt = new DocExtGetProperties();
            string uuidInvitacion = ConfigurationManager.AppSettings["templateInvitacion"].ToString();

            //conexion con alfresco
            string login = "login";
            string ticket = DevuelveTicket(login);

            //para obtener el documento modelo 
            string pathAlfresco = ConfigurationManager.AppSettings["alfresco"];
            string metodoAlfresco = @"/getProperties";
            string json = POSTFormDataAlfresco(uuidInvitacion, pathAlfresco, metodoAlfresco, ticket);

            docExt = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<DocExtGetProperties>(json);

            //Desarrollo Uri Alfresco
            string path = ConfigurationManager.AppSettings["UriAlfresco"];
            string filename = System.IO.Path.Combine(path + docExt.urlDownload);

            //Instancio Llamada por WebClient
            WebClient web = new WebClient();
            web.Credentials = CredentialCache.DefaultCredentials;
            web.Credentials = CredentialCache.DefaultNetworkCredentials;
            web.UseDefaultCredentials = true;

            //Llamo otro ticket de Permiso de acceso a Alfresco sin Usuario y Contraseña
            string ticket2 = DevuelveTicket(login);
            string down = filename + "?alf_ticket=" + ticket2;

            byte[] byteArray = web.DownloadData(down);

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);

                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(stream, true))
                {

                    IDictionary<String, BookmarkStart> bookmarkMaps = new Dictionary<String, BookmarkStart>();

                    foreach (BookmarkStart bookmarkStart in wordDocument.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                    {
                        bookmarkMaps[bookmarkStart.Name] = bookmarkStart;
                    }

                    int id_documento = Convert.ToInt32(tableData.ID_DOCUMENTO);


                    var documento = _HojaTramiteService.Listar_Detalle_Documento_Interno(id_documento);
                    //detalle = _HojaTramiteService.Listar_Detalle_Documento(documento.First().id_tipo_documento);
                    var detalle_documento = _HojaTramiteService.GetDocumentoDetalleInterno(id_documento);

                    Run NOM_DOC = bookmarkMaps["NOM_DOC"].NextSibling<Run>();
                    NOM_DOC.GetFirstChild<Text>().Text = documento.First().nom_doc;

                    Run FECHA_ACTUAL = bookmarkMaps["FECHA_ACTUAL"].NextSibling<Run>();
                    FECHA_ACTUAL.GetFirstChild<Text>().Text = tableData.FECHA_ACTUAL;

                    Run ASUNTO = bookmarkMaps["ASUNTO"].NextSibling<Run>();
                    ASUNTO.GetFirstChild<Text>().Text = documento.First().asunto;


                    wordDocument.MainDocumentPart.Document.Save();
                    wordDocument.Close();

                }
                string path_word = @"C:\SIGESDOC\WORD\";
                string path_pdf = @"C:\SIGESDOC\PDF\";

                if (!Directory.Exists(path_word) && !Directory.Exists(path_pdf))
                {
                    Directory.CreateDirectory(path_word);
                    Directory.CreateDirectory(path_pdf);

                    string nuevoWord = Path.Combine(path_word, "INVITACION_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "INVITACION_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                    Process.Start(nuevoWord);

                }
                else
                {
                    string nuevoWord = Path.Combine(path_word, "INVITACION_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "INVITACION_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                    Process.Start(nuevoWord);
                }
            }
        }

        #endregion

        #region RESOLUCION

        [HttpGet]
        public void ResolucionWord(CargaResolucionWord tableData)
        {
            DateTime fecha_PATH = DateTime.Now;
            DateTime fecha = DateTime.Now;
            tableData.FECHA_ACTUAL = fecha.ToString("dd MMMM yyyy");


            //desarrollo variables de alfresco
            DocExtGetProperties docExt = new DocExtGetProperties();
            string uuidResolucion = ConfigurationManager.AppSettings["templateResolucion"].ToString();

            //conexion con alfresco
            string login = "login";
            string ticket = DevuelveTicket(login);

            //para obtener el documento modelo 
            string pathAlfresco = ConfigurationManager.AppSettings["alfresco"];
            string metodoAlfresco = @"/getProperties";
            string json = POSTFormDataAlfresco(uuidResolucion, pathAlfresco, metodoAlfresco, ticket);

            docExt = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<DocExtGetProperties>(json);

            //Desarrollo Uri Alfresco
            string path = ConfigurationManager.AppSettings["UriAlfresco"];
            string filename = System.IO.Path.Combine(path + docExt.urlDownload);

            //Instancio Llamada por WebClient
            WebClient web = new WebClient();
            web.Credentials = CredentialCache.DefaultCredentials;
            web.Credentials = CredentialCache.DefaultNetworkCredentials;
            web.UseDefaultCredentials = true;

            //Llamo otro ticket de Permiso de acceso a Alfresco sin Usuario y Contraseña
            string ticket2 = DevuelveTicket(login);
            string down = filename + "?alf_ticket=" + ticket2;

            byte[] byteArray = web.DownloadData(down);



            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);

                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(stream, true))
                {

                    IDictionary<String, BookmarkStart> bookmarkMaps = new Dictionary<String, BookmarkStart>();

                    foreach (BookmarkStart bookmarkStart in wordDocument.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                    {
                        bookmarkMaps[bookmarkStart.Name] = bookmarkStart;
                    }

                    int id_documento = Convert.ToInt32(tableData.ID_DOCUMENTO);


                    var documento = _HojaTramiteService.Listar_Detalle_Documento_Interno(id_documento);
                    //detalle = _HojaTramiteService.Listar_Detalle_Documento(documento.First().id_tipo_documento);
                    var detalle_documento = _HojaTramiteService.GetDocumentoDetalleInterno(id_documento);

                    Run NOM_DOC = bookmarkMaps["NOM_DOC"].NextSibling<Run>();
                    NOM_DOC.GetFirstChild<Text>().Text = documento.First().nom_doc;

                    //Run EMPRESA = bookmarkMaps["EMPRESA"].NextSibling<Run>();
                    //EMPRESA.GetFirstChild<Text>().Text = detalle_documento.First().nom_oficina_crea;

                    Run FECHA_ACTUAL = bookmarkMaps["FECHA_ACTUAL"].NextSibling<Run>();
                    FECHA_ACTUAL.GetFirstChild<Text>().Text = tableData.FECHA_ACTUAL;

                    //Run ASUNTO = bookmarkMaps["ASUNTO"].NextSibling<Run>();
                    //ASUNTO.GetFirstChild<Text>().Text = documento.First().asunto;

                    ////Run CARGO = bookmarkMaps["CARGO"].NextSibling<Run>();
                    ////CARGO.GetFirstChild<Text>().Text = tableData.CARGO;

                    //Run DIRECCION = bookmarkMaps["DIRECCION"].NextSibling<Run>();
                    //DIRECCION.GetFirstChild<Text>().Text = detalle_documento.First().nom_oficina_destino;

                    //Run NOMBRES = bookmarkMaps["NOMBRES"].NextSibling<Run>();
                    //NOMBRES.GetFirstChild<Text>().Text = documento.First().nombres;

                    //Run REFERENCIA = bookmarkMaps["REFERENCIA"].NextSibling<Run>();
                    //REFERENCIA.GetFirstChild<Text>().Text = string.Empty;

                    wordDocument.MainDocumentPart.Document.Save();
                    wordDocument.Close();

                }

                string path_word = @"C:\SIGESDOC\WORD\";
                string path_pdf = @"C:\SIGESDOC\PDF\";

                if (!Directory.Exists(path_word) && !Directory.Exists(path_pdf))
                {
                    Directory.CreateDirectory(path_word);
                    Directory.CreateDirectory(path_pdf);

                }
                else
                {
                    string nuevoWord = Path.Combine(path_word, "RESOLUCION_" + fecha_PATH.ToString("ddMMyy_HHMMSS") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "RESOLUCION_" + fecha_PATH.ToString("ddMMyy_HHMMSS") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                    Process.Start(nuevoWord);
                }
            }
        }

        #endregion

        #region INFORME

        [HttpGet]
        public void InformeWord(CargaInformeWord tableData)
        {
            DateTime fecha_PATH = DateTime.Now;
            DateTime fecha = DateTime.Now;
            tableData.FECHA_ACTUAL = fecha.ToString("dd MMMM yyyy");

            string NOMBREDOC = string.Empty;

            //desarrollo variables de alfresco
            DocExtGetProperties docExt = new DocExtGetProperties();
            string uuidInforme = ConfigurationManager.AppSettings["templateInforme"].ToString();

            //conexion con alfresco
            string login = "login";
            string ticket = DevuelveTicket(login);

            //para obtener el documento modelo 
            string pathAlfresco = ConfigurationManager.AppSettings["alfresco"];
            string metodoAlfresco = @"/getProperties";
            string json = POSTFormDataAlfresco(uuidInforme, pathAlfresco, metodoAlfresco, ticket);

            docExt = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<DocExtGetProperties>(json);

            //Desarrollo Uri Alfresco
            string path = ConfigurationManager.AppSettings["UriAlfresco"];
            string filename = System.IO.Path.Combine(path + docExt.urlDownload);

            //Instancio Llamada por WebClient
            WebClient web = new WebClient();
            web.Credentials = CredentialCache.DefaultCredentials;
            web.Credentials = CredentialCache.DefaultNetworkCredentials;
            web.UseDefaultCredentials = true;

            //Llamo otro ticket de Permiso de acceso a Alfresco sin Usuario y Contraseña
            string ticket2 = DevuelveTicket(login);
            string down = filename + "?alf_ticket=" + ticket2;

            byte[] byteArray = web.DownloadData(down);

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);

                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(stream, true))
                {

                    IDictionary<String, BookmarkStart> bookmarkMaps = new Dictionary<String, BookmarkStart>();

                    foreach (BookmarkStart bookmarkStart in wordDocument.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                    {
                        bookmarkMaps[bookmarkStart.Name] = bookmarkStart;
                    }

                    int id_documento = Convert.ToInt32(tableData.ID_DOCUMENTO);


                    var documento = _HojaTramiteService.Listar_Detalle_Documento_Interno(id_documento);
                    //detalle = _HojaTramiteService.Listar_Detalle_Documento(documento.First().id_tipo_documento);
                    var detalle_documento = _HojaTramiteService.GetDocumentoDetalleInterno(id_documento);
                    NOMBREDOC = documento.First().nom_doc;
                    Run NOM_DOC = bookmarkMaps["NOM_DOC"].NextSibling<Run>();
                    NOM_DOC.GetFirstChild<Text>().Text = documento.First().nom_doc;

                    //Run EMPRESA = bookmarkMaps["EMPRESA"].NextSibling<Run>();
                    //EMPRESA.GetFirstChild<Text>().Text = detalle_documento.First().nom_oficina_crea;

                    Run FECHA_ACTUAL = bookmarkMaps["FECHA_ACTUAL"].NextSibling<Run>();
                    FECHA_ACTUAL.GetFirstChild<Text>().Text = tableData.FECHA_ACTUAL;

                    Run ASUNTO = bookmarkMaps["ASUNTO"].NextSibling<Run>();
                    ASUNTO.GetFirstChild<Text>().Text = documento.First().asunto;

                    //Run CARGO = bookmarkMaps["CARGO"].NextSibling<Run>();
                    //CARGO.GetFirstChild<Text>().Text = tableData.CARGO;

                    //Run DIRECCION = bookmarkMaps["DIRECCION"].NextSibling<Run>();
                    //DIRECCION.GetFirstChild<Text>().Text = detalle_documento.First().nom_oficina_destino;

                    Run NOMBRES = bookmarkMaps["NOMBRES"].NextSibling<Run>();
                    NOMBRES.GetFirstChild<Text>().Text = documento.First().nombres;

                    Run REFERENCIA = bookmarkMaps["REFERENCIA"].NextSibling<Run>();
                    REFERENCIA.GetFirstChild<Text>().Text = documento.First().asunto;


                    wordDocument.MainDocumentPart.Document.Save();
                   
                    wordDocument.Close();

                    
                }

                string path_word = @"C:\SIGESDOC\WORD\";
                string path_pdf = @"C:\SIGESDOC\PDF\";

                if (!Directory.Exists(path_word) && !Directory.Exists(path_pdf))
                {
                    Directory.CreateDirectory(path_word);
                    Directory.CreateDirectory(path_pdf);

                    string nuevoWord = Path.Combine(path_word, "INFORME_"+fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "INFORME_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                    Process.Start(nuevoWord);

                }
                else
                {
                    string nuevoWord = Path.Combine(path_word, "INFORME_"+ NOMBREDOC+"_"+ fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "INFORME_" + NOMBREDOC+"_"+ fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                    Process.Start(nuevoWord);
                }
            }
        }

        #endregion

        #region COMUNICADO

        [HttpGet]
        public void ComunicadoWord(CargaComunicadoWord tableData)
        {
            DateTime fecha_PATH = DateTime.Now;
            DateTime fecha = DateTime.Now;
            tableData.FECHA_ACTUAL = fecha.ToString("dd MMMM yyyy");

            //desarrollo variables de alfresco
            DocExtGetProperties docExt = new DocExtGetProperties();
            string uuidComunicado = ConfigurationManager.AppSettings["templateComunicado"].ToString();

            //conexion con alfresco
            string login = "login";
            string ticket = DevuelveTicket(login);

            //para obtener el documento modelo 
            string pathAlfresco = ConfigurationManager.AppSettings["alfresco"];
            string metodoAlfresco = @"/getProperties";
            string json = POSTFormDataAlfresco(uuidComunicado, pathAlfresco, metodoAlfresco, ticket);

            docExt = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<DocExtGetProperties>(json);

            //Desarrollo Uri Alfresco
            string path = ConfigurationManager.AppSettings["UriAlfresco"];
            string filename = System.IO.Path.Combine(path + docExt.urlDownload);

            //Instancio Llamada por WebClient
            WebClient web = new WebClient();
            web.Credentials = CredentialCache.DefaultCredentials;
            web.Credentials = CredentialCache.DefaultNetworkCredentials;
            web.UseDefaultCredentials = true;

            //Llamo otro ticket de Permiso de acceso a Alfresco sin Usuario y Contraseña
            string ticket2 = DevuelveTicket(login);
            string down = filename + "?alf_ticket=" + ticket2;

            byte[] byteArray = web.DownloadData(down);
            string NOMBREDOC = string.Empty;


            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);

                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(stream, true))
                {

                    IDictionary<String, BookmarkStart> bookmarkMaps = new Dictionary<String, BookmarkStart>();

                    foreach (BookmarkStart bookmarkStart in wordDocument.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                    {
                        bookmarkMaps[bookmarkStart.Name] = bookmarkStart;
                    }

                    int id_documento = Convert.ToInt32(tableData.ID_DOCUMENTO);


                    var documento = _HojaTramiteService.Listar_Detalle_Documento_Interno(id_documento);
                    //detalle = _HojaTramiteService.Listar_Detalle_Documento(documento.First().id_tipo_documento);
                    var detalle_documento = _HojaTramiteService.GetDocumentoDetalleInterno(id_documento);
                    NOMBREDOC = documento.First().nom_doc;
                    Run NOM_DOC = bookmarkMaps["NOM_DOC"].NextSibling<Run>();
                    NOM_DOC.GetFirstChild<Text>().Text = documento.First().nom_doc;

                    Run FECHA_ACTUAL = bookmarkMaps["FECHA_ACTUAL"].NextSibling<Run>();
                    FECHA_ACTUAL.GetFirstChild<Text>().Text = tableData.FECHA_ACTUAL;

                   
                    wordDocument.MainDocumentPart.Document.Save();
                    wordDocument.Close();

                }
                string path_word = @"C:\SIGESDOC\WORD\";
                string path_pdf = @"C:\SIGESDOC\PDF\";

                if (!Directory.Exists(path_word) && !Directory.Exists(path_pdf))
                {
                    Directory.CreateDirectory(path_word);
                    Directory.CreateDirectory(path_pdf);

                    string nuevoWord = Path.Combine(path_word, "COMUNICADO_"+ fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "COMUNICADO_"+ fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                    Process.Start(nuevoWord);

                }
                else
                {
                    string nuevoWord = Path.Combine(path_word, "COMUNICADO_"+ fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "COMUNICADO_"+ fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                    Process.Start(nuevoWord);
                }
            }
        }

        #endregion

        #region CARTA MULTIPLE

        [HttpGet]
        public void CartaMultipleWord(CargaCartaMultipleWord tableData)
        {
            DateTime fecha_PATH = DateTime.Now;
            DateTime fecha = DateTime.Now;
            tableData.FECHA_ACTUAL = fecha.ToString("dd MMMM yyyy");

            //desarrollo
            DocExtGetProperties docExt = new DocExtGetProperties();
            string uuidCartaMultiple = ConfigurationManager.AppSettings["templateCartaMultiple"].ToString();

            //conexion a alfresco
            string login = "login";
            string ticket = DevuelveTicket(login);

            string pathAlfresco = ConfigurationManager.AppSettings["alfresco"];
            string metodoAlfresco = @"/getProperties";
            string json = POSTFormDataAlfresco(uuidCartaMultiple, pathAlfresco, metodoAlfresco, ticket);

            docExt = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<DocExtGetProperties>(json);

            int id_documento = Convert.ToInt32(tableData.ID_DOCUMENTO);

            IEnumerable<DetalleMaeDocumentoResponse> documentoRequest = new List<DetalleMaeDocumentoResponse>();

            //desarrollo
            string path = ConfigurationManager.AppSettings["UriAlfresco"];
            string filename = System.IO.Path.Combine(path + docExt.urlDownload);

            WebClient web = new WebClient();
            web.Credentials = CredentialCache.DefaultCredentials;
            web.Credentials = CredentialCache.DefaultNetworkCredentials;
            web.UseDefaultCredentials = true;
            string ticket2 = DevuelveTicket(login);
            string down = filename + "?alf_ticket=" + ticket2;
            byte[] byteArray = web.DownloadData(down);

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);

                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(stream, true))
                {

                    IDictionary<String, BookmarkStart> bookmarkMaps = new Dictionary<String, BookmarkStart>();

                    foreach (BookmarkStart bookmarkStart in wordDocument.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                    {
                        bookmarkMaps[bookmarkStart.Name] = bookmarkStart;
                    }
                    documentoRequest = _HojaTramiteService.Listar_Detalle_Documento_Interno(id_documento);

                    Run NOM_DOC = bookmarkMaps["NOM_DOC"].NextSibling<Run>();
                    NOM_DOC.GetFirstChild<Text>().Text = documentoRequest.First().nom_doc;

                    Run ASUNTO = bookmarkMaps["ASUNTO"].NextSibling<Run>();
                    ASUNTO.GetFirstChild<Text>().Text = documentoRequest.First().asunto;

                    Run FECHA_ACTUAL = bookmarkMaps["FECHA_ACTUAL"].NextSibling<Run>();
                    FECHA_ACTUAL.GetFirstChild<Text>().Text = tableData.FECHA_ACTUAL;


                    foreach (var memorandomultiple in documentoRequest)
                    {
                        Body body = wordDocument.MainDocumentPart.Document.GetFirstChild<Body>();
                        Run UNO = bookmarkMaps["NOMBRES_1"].NextSibling<Run>();
                        UNO.GetFirstChild<Text>().Text = memorandomultiple.nombres;

                        Paragraph para = body.AppendChild(new Paragraph());
                        Run run = para.AppendChild(new Run());
                        run.AppendChild(new Text(memorandomultiple.nombres));
                    }

                    wordDocument.MainDocumentPart.Document.Save();
                    wordDocument.Close();
                }

                string path_word = @"C:\SIGESDOC\WORD\";
                string path_pdf = @"C:\SIGESDOC\PDF\";

                if (!Directory.Exists(path_word) && !Directory.Exists(path_pdf))
                {
                    Directory.CreateDirectory(path_word);
                    Directory.CreateDirectory(path_pdf);

                    string nuevoWord = Path.Combine(path_word, "CARTA_MULTIPLE_"  + fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "CARTA_MULTIPLE_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());

                    Process.Start(nuevoWord);

                }
                else
                {
                    string nuevoWord = Path.Combine(path_word, "CARTA_MULTIPLE_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "CARTA_MULTIPLE_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());

                    Process.Start(nuevoWord);
                }
            }
        }
        #endregion

        #region OFICIO MULTIPLE

        [HttpGet]
        public void OficioMultipleWord(CargaOficioMultipleWord tableData)
        {
            DateTime fecha_PATH = DateTime.Now;
            DateTime fecha = DateTime.Now;
            tableData.FECHA_ACTUAL = fecha.ToString("dd MMMM yyyy");

            //desarrollo
            DocExtGetProperties docExt = new DocExtGetProperties();
            string uuidOficioMultiple = ConfigurationManager.AppSettings["templateOficioMultiple"].ToString();
            
            //conexion de acceso con alfresco
            string login = "login";
            string ticket = DevuelveTicket(login);

            // para obtener el documento modelo
            string pathAlfresco = ConfigurationManager.AppSettings["alfresco"];
            string metodoAlfresco = @"/getProperties";
            string json = POSTFormDataAlfresco(uuidOficioMultiple, pathAlfresco, metodoAlfresco, ticket);

            docExt = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<DocExtGetProperties>(json);

            int id_documento = Convert.ToInt32(tableData.ID_DOCUMENTO);

            IEnumerable<DetalleMaeDocumentoResponse> documentoRequest = new List<DetalleMaeDocumentoResponse>();

            //Desarrollo Uri Alfresco
            string path = ConfigurationManager.AppSettings["UriAlfresco"];
            string filename = System.IO.Path.Combine(path + docExt.urlDownload);

            WebClient web = new WebClient();
            web.Credentials = CredentialCache.DefaultCredentials;
            web.Credentials = CredentialCache.DefaultNetworkCredentials;
            web.UseDefaultCredentials = true;
            string ticket2 = DevuelveTicket(login);
            string down = filename + "?alf_ticket=" + ticket2;

            byte[] byteArray = web.DownloadData(down);

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);

                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(stream, true))
                {

                    IDictionary<String, BookmarkStart> bookmarkMaps = new Dictionary<String, BookmarkStart>();

                    foreach (BookmarkStart bookmarkStart in wordDocument.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                    {
                        bookmarkMaps[bookmarkStart.Name] = bookmarkStart;
                    }

                    documentoRequest = _HojaTramiteService.Listar_Detalle_Documento_Interno(id_documento);
                    Run NOM_DOC = bookmarkMaps["NOM_DOC"].NextSibling<Run>();
                    NOM_DOC.GetFirstChild<Text>().Text = documentoRequest.First().nom_doc;

                    Run ASUNTO = bookmarkMaps["ASUNTO"].NextSibling<Run>();
                    ASUNTO.GetFirstChild<Text>().Text = documentoRequest.First().asunto;

                    Run FECHA_ACTUAL = bookmarkMaps["FECHA_ACTUAL"].NextSibling<Run>();
                    FECHA_ACTUAL.GetFirstChild<Text>().Text = tableData.FECHA_ACTUAL;


                    foreach (var memorandomultiple in documentoRequest)
                    {
                        Body body = wordDocument.MainDocumentPart.Document.GetFirstChild<Body>();
                        Run UNO = bookmarkMaps["NOMBRES_1"].NextSibling<Run>();
                        UNO.GetFirstChild<Text>().Text = memorandomultiple.nombres;

                        Paragraph para = body.AppendChild(new Paragraph());
                        Run run = para.AppendChild(new Run());
                        run.AppendChild(new Text(memorandomultiple.nombres));
                    }

                    wordDocument.MainDocumentPart.Document.Save();
                    wordDocument.Close();

                }
                string path_word = @"C:\SIGESDOC\WORD\";
                string path_pdf = @"C:\SIGESDOC\PDF\";

                if (!Directory.Exists(path_word) && !Directory.Exists(path_pdf))
                {
                    Directory.CreateDirectory(path_word);
                    Directory.CreateDirectory(path_pdf);

                    string nuevoWord = Path.Combine(path_word, "OFICIO_MULTIPLE_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "OFICIO_MULTIPLE_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());

                    Process.Start(nuevoWord);

                }
                else
                {
                    string nuevoWord = Path.Combine(path_word, "OFICIO_MULTIPLE_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "OFICIO_MULTIPLE_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());

                    Process.Start(nuevoWord);
                }
            }
        }

        #endregion

        #region MEMORANDO

        [HttpGet]
        public void MemorandoWord(CargaMemorandoWord tableData)
        {

            DateTime fecha_PATH = DateTime.Now;
            DateTime fecha = DateTime.Now;
            tableData.FECHA_ACTUAL = fecha.ToString("dd MMMM yyyy");
            string NOMBREDOC = string.Empty;
            //desarrollo variables de alfresco
            DocExtGetProperties docExt = new DocExtGetProperties();
            string uuidMemorando = ConfigurationManager.AppSettings["templateMemorando"].ToString();

            //conexion con alfresco
            string login = "login";
            string ticket = DevuelveTicket(login);


            //para obtener el documento modelo 
            string pathAlfresco = ConfigurationManager.AppSettings["alfresco"];
            string metodoAlfresco = @"/getProperties";
            string json = POSTFormDataAlfresco(uuidMemorando, pathAlfresco, metodoAlfresco, ticket);

            docExt = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<DocExtGetProperties>(json);

            //Desarrollo Uri Alfresco
            string path = ConfigurationManager.AppSettings["UriAlfresco"];
            string filename = System.IO.Path.Combine(path + docExt.urlDownload);

            //Instancio Llamada por WebClient
            WebClient web = new WebClient();
            web.Credentials = CredentialCache.DefaultCredentials;
            web.Credentials = CredentialCache.DefaultNetworkCredentials;
            web.UseDefaultCredentials = true;

            //Llamo otro ticket de Permiso de acceso a Alfresco sin Usuario y Contraseña
            string ticket2 = DevuelveTicket(login);
            string down = filename + "?alf_ticket=" + ticket2;

            byte[] byteArray = web.DownloadData(down);

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);

                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(stream, true))
                {

                    IDictionary<String, BookmarkStart> bookmarkMaps = new Dictionary<String, BookmarkStart>();

                    foreach (BookmarkStart bookmarkStart in wordDocument.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                    {
                        bookmarkMaps[bookmarkStart.Name] = bookmarkStart;
                        int id_documento = Convert.ToInt32(tableData.ID_DOCUMENTO);


                        var documento = _HojaTramiteService.Listar_Detalle_Documento_Interno(id_documento);
                        //detalle = _HojaTramiteService.Listar_Detalle_Documento(documento.First().id_tipo_documento);
                        var detalle_documento = _HojaTramiteService.GetDocumentoDetalleInterno(id_documento);
                        NOMBREDOC = documento.First().nom_doc;
                        Run NOM_DOC = bookmarkMaps["NOM_DOC"].NextSibling<Run>();
                        NOM_DOC.GetFirstChild<Text>().Text = documento.First().nom_doc;

                        //Run EMPRESA = bookmarkMaps["EMPRESA"].NextSibling<Run>();
                        //EMPRESA.GetFirstChild<Text>().Text = detalle_documento.First().nom_oficina_crea;

                        Run FECHA_ACTUAL = bookmarkMaps["FECHA_ACTUAL"].NextSibling<Run>();
                        FECHA_ACTUAL.GetFirstChild<Text>().Text = tableData.FECHA_ACTUAL;

                        //Run ASUNTO = bookmarkMaps["ASUNTO"].NextSibling<Run>();
                        //ASUNTO.GetFirstChild<Text>().Text = documento.First().asunto;

                        //Run CARGO = bookmarkMaps["CARGO"].NextSibling<Run>();
                        //CARGO.GetFirstChild<Text>().Text = tableData.CARGO;

                        Run DIRECCION = bookmarkMaps["DIRECCION"].NextSibling<Run>();
                        DIRECCION.GetFirstChild<Text>().Text = detalle_documento.First().nom_oficina_destino;

                        Run NOMBRES = bookmarkMaps["NOMBRES"].NextSibling<Run>();
                        NOMBRES.GetFirstChild<Text>().Text = documento.First().nombres;

                        //Run REFERENCIA = bookmarkMaps["REFERENCIA"].NextSibling<Run>();
                        //REFERENCIA.GetFirstChild<Text>().Text = string.Empty;


                        wordDocument.MainDocumentPart.Document.Save();
                        wordDocument.Close();
                    }

                    string path_word = @"C:\SIGESDOC\WORD\";
                    string path_pdf = @"C:\SIGESDOC\PDF\";

                    if (!Directory.Exists(path_word) && !Directory.Exists(path_pdf))
                    {
                        Directory.CreateDirectory(path_word);
                        Directory.CreateDirectory(path_pdf);

                        string nuevoWord = Path.Combine(path_word, "MEMORANDO_"+  fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                        string nuevoPDF = Path.Combine(path_pdf, "MEMORANDO_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                        stream.Close();
                        System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                        System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                        Process.Start(nuevoWord);

                    }
                    else
                    {
                        string nuevoWord = Path.Combine(path_word, "MEMORANDO_" +  fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                        string nuevoPDF = Path.Combine(path_pdf, "MEMORANDO_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                        stream.Close();
                        System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                        System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());

                        string filenameWord = Path.GetFileName(nuevoWord);
                        string Metodoupload = @"/upload";
                        string Carpeta = "SIGESDOC/INTERNO/MEMORANDO/" + DateTime.Today.Year + "/" + DateTime.Today.Month + "/PDF";
                        string GuardaPDF = PostPDFsFormDataAlfresco(nuevoWord, filenameWord, Carpeta, pathAlfresco, Metodoupload, ticket2);
                        DocExtUpload upload = new DocExtUpload();
                        upload = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<DocExtUpload>(GuardaPDF);

                        //para obtener el nuevo camino donde se guardó la nueva version en alfresco
                        string json_new = POSTFormDataAlfresco(upload.uuid, pathAlfresco, metodoAlfresco, ticket);
                        docExt = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<DocExtGetProperties>(json_new);

                        UidAlfrescoRequest alfrescoRequest = new UidAlfrescoRequest();
                        alfrescoRequest.uid_alfresco = upload.uuid;
                        alfrescoRequest.tipo_doc = "MEMORANDO";
                        alfrescoRequest.nom_doc = tableData.NOM_DOC;
                        alfrescoRequest.version_documento = 1;
                        alfrescoRequest.fecha_registro = DateTime.Now;
                        alfrescoRequest.estado_doc = "EN PROCESO";
                        alfrescoRequest.pathdoc_alfresco = docExt.urlDownload;

                        //_HabilitacionesService.InserirNuevoUIIDALfresco(alfrescoRequest);

                        Process.Start(nuevoWord);
                    }

                }
            }
        }

        #endregion

        #region MEMORANDO MULTIPLE

        [AllowAnonymous]
        [HttpGet]
        public void MemorandoMultipleWord(CargaMemorandoMultipleWord tableData)
        {

            DateTime fecha_PATH = DateTime.Now;
            string NOMBREDOC = string.Empty;
            DateTime fecha = DateTime.Now;
            tableData.FECHA_ACTUAL = fecha.ToString("dd MMMM yyyy");

            DocExtGetProperties docExt = new DocExtGetProperties();
            string uuidMemorandoMultiple = ConfigurationManager.AppSettings["templateMemorandoMultiple"].ToString();

            //conexion a alfresco
            string login = "login";
            string ticket = DevuelveTicket(login);

            //string path = ConfigurationManager.AppSettings["memorando"];
            string pathAlfresco = ConfigurationManager.AppSettings["alfresco"];
            string metodoAlfresco = @"/getProperties";
            string json = POSTFormDataAlfresco(uuidMemorandoMultiple, pathAlfresco, metodoAlfresco, ticket);

            docExt = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<DocExtGetProperties>(json);


            int id_documento = Convert.ToInt32(tableData.ID_DOCUMENTO);

            IEnumerable<DetalleMaeDocumentoResponse> documentoRequest = new List<DetalleMaeDocumentoResponse>();

            //desarrollo
            // string path  = @"C:\Users\PSSERU-TI\Source\Repos\landersaavedra\sigesdoc\documentos externos";

            string path = ConfigurationManager.AppSettings["UriAlfresco"];

            string filename = System.IO.Path.Combine(path + docExt.urlDownload);
           // string path2 = System.IO.Path.GetFullPath(path);
           // string descFilePathAndName = System.IO.Path.Combine(path2, filename);

            WebClient web = new WebClient();
            web.Credentials = CredentialCache.DefaultCredentials;
            web.Credentials = CredentialCache.DefaultNetworkCredentials;
            web.UseDefaultCredentials = true;
            string ticket2 = DevuelveTicket(login);
            string down = filename + "?alf_ticket=" + ticket2;
            //byte[] byteArray = System.IO.File.ReadAllBytes(request.Address.OriginalString);
            byte[] byteArray = web.DownloadData(down);

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);

                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(stream, true))
                {

                    IDictionary<String, BookmarkStart> bookmarkMaps = new Dictionary<String, BookmarkStart>();

                    foreach (BookmarkStart bookmarkStart in wordDocument.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                    {

                        bookmarkMaps[bookmarkStart.Name] = bookmarkStart;
                    }

                    documentoRequest = _HojaTramiteService.Listar_Detalle_Documento_Interno(id_documento);

                    Run NOM_DOC = bookmarkMaps["NOM_DOC"].NextSibling<Run>();
                    NOM_DOC.GetFirstChild<Text>().Text = documentoRequest.First().nom_doc;

                    Run ASUNTO = bookmarkMaps["ASUNTO"].NextSibling<Run>();
                    ASUNTO.GetFirstChild<Text>().Text = documentoRequest.First().asunto;

                    Run FECHA_ACTUAL = bookmarkMaps["FECHA_ACTUAL"].NextSibling<Run>();
                    FECHA_ACTUAL.GetFirstChild<Text>().Text = tableData.FECHA_ACTUAL;


                    foreach (var memorandomultiple in documentoRequest)
                    {
                        Body body = wordDocument.MainDocumentPart.Document.GetFirstChild<Body>();
                        Run UNO = bookmarkMaps["NOMBRES_1"].NextSibling<Run>();
                        UNO.GetFirstChild<Text>().Text = memorandomultiple.nombres;

                        Paragraph para = body.AppendChild(new Paragraph());
                        Run run = para.AppendChild(new Run());
                        run.AppendChild(new Text(memorandomultiple.nombres));
                    }
     
                    wordDocument.MainDocumentPart.Document.Save();
                    wordDocument.Close();
                }

                    string path_word = @"C:\SIGESDOC\WORD\";
                    string path_pdf = @"C:\SIGESDOC\PDF\";

                    if (!Directory.Exists(path_word) && !Directory.Exists(path_pdf))
                    {
                        Directory.CreateDirectory(path_word);
                        Directory.CreateDirectory(path_pdf);

                        string nuevoWord = Path.Combine(path_word, "MEMORANDO_MULTIPLE_" + fecha_PATH.ToString("ddMMyy_HHMMSS") + ".docx");
                        string nuevoPDF = Path.Combine(path_pdf, "MEMORANDO_MULTIPLE_" + fecha_PATH.ToString("ddMMyy_HHMMSS") + ".pdf");

                        stream.Close();
                        System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                        System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());

                        Process.Start(nuevoWord);

                    }
                    else
                    {
                        string nuevoWord = Path.Combine(path_word, "MEMORANDO_MULTIPLE_" + fecha_PATH.ToString("ddMMyy_HHMMSS") + ".docx");
                        string nuevoPDF = Path.Combine(path_pdf, "MEMORANDO_MULTIPLE_"+ fecha_PATH.ToString("ddMMyy_HHMMSS") + ".pdf");

                        stream.Close();
                        System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                        System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());

                      

                        Process.Start(nuevoWord);
                    }
             }
        }

        #endregion

        #region CARTA

        [HttpGet]
        public void CartaWord(CargaCartaWord tableData)
        {
            DateTime fecha_PATH = DateTime.Now;
            DateTime fecha = DateTime.Now;
            tableData.FECHA_ACTUAL = fecha.ToString("dd MMMM yyyy");
            string NOMBREDOC = string.Empty;


            //desarrollo variables de alfresco
            DocExtGetProperties docExt = new DocExtGetProperties();
            string uuidOficio = ConfigurationManager.AppSettings["templateCarta"].ToString();

            //conexion con alfresco
            string login = "login";
            string ticket = DevuelveTicket(login);

            //para obtener el documento modelo 
            string pathAlfresco = ConfigurationManager.AppSettings["alfresco"];
            string metodoAlfresco = @"/getProperties";
            string json = POSTFormDataAlfresco(uuidOficio, pathAlfresco, metodoAlfresco, ticket);

            docExt = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<DocExtGetProperties>(json);

            //Desarrollo Uri Alfresco
            string path = ConfigurationManager.AppSettings["UriAlfresco"];
            string filename = System.IO.Path.Combine(path + docExt.urlDownload);

            //Instancio Llamada por WebClient
            WebClient web = new WebClient();
            web.Credentials = CredentialCache.DefaultCredentials;
            web.Credentials = CredentialCache.DefaultNetworkCredentials;
            web.UseDefaultCredentials = true;

            //Llamo otro ticket de Permiso de acceso a Alfresco sin Usuario y Contraseña
            string ticket2 = DevuelveTicket(login);
            string down = filename + "?alf_ticket=" + ticket2;

            byte[] byteArray = web.DownloadData(down);

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, (int)byteArray.Length);

                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(stream, true))
                {

                    IDictionary<String, BookmarkStart> bookmarkMaps = new Dictionary<String, BookmarkStart>();

                    foreach (BookmarkStart bookmarkStart in wordDocument.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                    {
                        bookmarkMaps[bookmarkStart.Name] = bookmarkStart;
                    }

                    if (tableData.TIPO_DOCUMENTO == "Consulta General de Documentos de la Oficina")
                    {

                        int id_documento = Convert.ToInt32(tableData.ID_DOCUMENTO);

                         var documento = _HojaTramiteService.Listar_Detalle_Documento_Interno(id_documento);
                        //detalle = _HojaTramiteService.Listar_Detalle_Documento(documento.First().id_tipo_documento);
                        var detalle_documento = _HojaTramiteService.GetDocumentoDetalleInterno(id_documento);
                        NOMBREDOC = documento.First().nom_doc;
                        Run NOM_DOC = bookmarkMaps["NOM_DOC"].NextSibling<Run>();
                        NOM_DOC.GetFirstChild<Text>().Text = documento.First().nom_doc;

                        Run EMPRESA = bookmarkMaps["EMPRESA"].NextSibling<Run>();
                        EMPRESA.GetFirstChild<Text>().Text = detalle_documento.First().nom_oficina_crea;

                        Run FECHA_ACTUAL = bookmarkMaps["FECHA_ACTUAL"].NextSibling<Run>();
                        FECHA_ACTUAL.GetFirstChild<Text>().Text = tableData.FECHA_ACTUAL;

                        Run ASUNTO = bookmarkMaps["ASUNTO"].NextSibling<Run>();
                        ASUNTO.GetFirstChild<Text>().Text = documento.First().asunto;

                        //Run CARGO = bookmarkMaps["CARGO"].NextSibling<Run>();
                        //CARGO.GetFirstChild<Text>().Text = tableData.CARGO;

                        Run DIRECCION = bookmarkMaps["DIRECCION"].NextSibling<Run>();
                        DIRECCION.GetFirstChild<Text>().Text = detalle_documento.First().nom_oficina_destino;

                        Run NOMBRES = bookmarkMaps["NOMBRES"].NextSibling<Run>();
                        NOMBRES.GetFirstChild<Text>().Text = documento.First().nombres;

                        //Run REFERENCIA = bookmarkMaps["REFERENCIA"].NextSibling<Run>();
                        //REFERENCIA.GetFirstChild<Text>().Text = string.Empty;

                    }
                    else
                    {
                        string obtenernuevo = tableData.ID_DOCUMENTO;

                        int id_documento_Dhcpa = Convert.ToInt32(obtenernuevo);

                        var documento = _HojaTramiteService.Listar_Documento_DHCPA_Externo(id_documento_Dhcpa);
                        var documento_detalle = _HojaTramiteService.Listar_Documento_DHCPA_Detalle_Externo(id_documento_Dhcpa);
                        NOMBREDOC = documento.First().nom_doc;
                        Run NOM_DOC = bookmarkMaps["NOM_DOC"].NextSibling<Run>();
                        NOM_DOC.GetFirstChild<Text>().Text = documento.First().nom_doc;

                        Run EMPRESA = bookmarkMaps["EMPRESA"].NextSibling<Run>();
                        EMPRESA.GetFirstChild<Text>().Text = documento_detalle.First().oficina_externa;

                        Run FECHA_ACTUAL = bookmarkMaps["FECHA_ACTUAL"].NextSibling<Run>();
                        FECHA_ACTUAL.GetFirstChild<Text>().Text = tableData.FECHA_ACTUAL;

                        Run CARGO = bookmarkMaps["CARGO"].NextSibling<Run>();
                        CARGO.GetFirstChild<Text>().Text = documento_detalle.First().cargo;

                        Run ASUNTO = bookmarkMaps["ASUNTO"].NextSibling<Run>();
                        ASUNTO.GetFirstChild<Text>().Text = documento.First().asunto;

                        Run DIRECCION = bookmarkMaps["DIRECCION"].NextSibling<Run>();
                        DIRECCION.GetFirstChild<Text>().Text = documento_detalle.First().direccion_externo;

                        Run NOMBRES = bookmarkMaps["NOMBRES"].NextSibling<Run>();
                        NOMBRES.GetFirstChild<Text>().Text = documento_detalle.First().encargado;

                        wordDocument.MainDocumentPart.Document.Save();
                        wordDocument.Close();
                    }
                }

                string path_word = @"C:\SIGESDOC\WORD\";
                string path_pdf = @"C:\SIGESDOC\PDF\";

                if (!Directory.Exists(path_word) && !Directory.Exists(path_pdf))
                {
                    Directory.CreateDirectory(path_word);
                    Directory.CreateDirectory(path_pdf);

                    string nuevoWord = Path.Combine(path_word, "CARTA_"+fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "CARTA_" +fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                    Process.Start(nuevoWord);

                }
                else
                {
                    string nuevoWord = Path.Combine(path_word, "CARTA_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".docx");
                    string nuevoPDF = Path.Combine(path_pdf, "CARTA_" + fecha_PATH.ToString("ddMMyy_HHMMss") + ".pdf");

                    stream.Close();
                    System.IO.File.WriteAllBytes(nuevoWord, stream.ToArray());
                    System.IO.File.WriteAllBytes(nuevoPDF, stream.ToArray());
                    Process.Start(nuevoWord);
                }
            }
        }

        #endregion

        /// <summary>
        /// Metodos de Conexion con Alfresco
        /// </summary>
        /// <param name="connection">Devuelve el Ticket de Acceso pasando como parametros el usuario y la contraseña</param>
        /// <returns></returns>
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

            if(response.StatusCode == HttpStatusCode.OK)
            {
                Stream reStream = response.GetResponseStream();
                var sr = new StreamReader(response.GetResponseStream());
                string salida = sr.ReadToEnd();
                var data = ToObject(salida) as IDictionary<string, object>;

                foreach(var token in data)
                {
                    var tikets = data[token.Key] as IDictionary<string, object>;

                    foreach(var tiket in tikets)
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
        
        /// <summary>
        /// Metodo para llamado de Alfresco, para traer los documentos
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        private string POSTFormDataAlfresco(string uuid, string url, string method, string ticket)
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

       private string SendPostFormDataAlfresco( string filedata ,string fileName, string uploadDirectory, string url,  string method, string ticket)
       {
            string JSonSalida = null;

            using(var client = new HttpClient())
            {
                using (var content = 
                    new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))

                {
                    string url_ = url + method + "?alf_ticket=" + ticket;
                    byte[] data =  System.IO.File.ReadAllBytes(filedata);

                    //, "filedata", "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                    content.Add(new StreamContent(new MemoryStream(data)), "");
                    content.Add(new StringContent(fileName), "fileName");
                    content.Add(new StringContent(uploadDirectory), "uploadDirectory");
                    
                    var response = client.PostAsync(url_, content).Result;

                    if(response.Content != null)
                    {
                        JSonSalida = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            return JSonSalida;
       }

        private StreamContent CreateFileContent(Stream stream, string filename, string contenType)
        {
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
            {
                Name = "\"files\"",
                FileName = "\"" + filename + "\""
            };

            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contenType);
            return fileContent;
        }

        private string PostPDFsFormDataAlfresco(string filedata, string fileName, string uploadDirectory, string url, string method, string ticket)
        {
            Stream file = System.IO.File.OpenRead(filedata);

            string JsonSalida = string.Empty;
            string remoteURL = url + method + "?alf_ticket=" + ticket;
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            string posString = String.Format("{0}, {1}, {2}",file, fileName, uploadDirectory);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(remoteURL);

            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Dictionary<string, object> parameters = new Dictionary<string, object>() {
                //application/vnd.openxmlformats-officedocument.wordprocessingml.document
                { "filedata",  new FormFile() { Name = fileName, ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document", FilePath = filedata }},
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
                                using (FileStream fileStream =System.IO.File.OpenRead(archivo.FilePath))
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

         /// <summary>
        /// Metodo de Carga para Enviar por FTP para Archivo Compartido.
        /// </summary>
        /// <param name="strServer"></param>
        /// <param name="filename"></param>
        /// <param name="strFileNameLocal"></param>
        /// <param name="strPathFTP"></param>
        public void UploadFileFTP(string strServer, string filename, string strFileNameLocal, string strPathFTP)
        {
            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(string.Format("ftp://{0}/{1}", strServer,
                                                                 Path.Combine(strPathFTP, Path.GetFileName(strFileNameLocal))));
            int cnstIntLengthBuffer = Convert.ToInt32(ConfigurationManager.AppSettings["intbuffer"].ToString());

            //En caso de solicitar credenciales
            ftpWebRequest.Credentials = new NetworkCredential("", "");
            ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;

            ftpWebRequest.UsePassive = true;
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.KeepAlive = false;

            using (FileStream stmFile = System.IO.File.OpenRead(strFileNameLocal))
            { // Obtiene el stream sobre la comunicación FTP
                using (Stream stmFTP = ftpWebRequest.GetRequestStream())
                {
                    byte[] arrBytBuffer = new byte[cnstIntLengthBuffer];
                    int intRead;

                    // Lee y escribe el archivo en el stream de comunicaciones
                    while ((intRead = stmFile.Read(arrBytBuffer, 0, cnstIntLengthBuffer)) != 0)
                        stmFTP.Write(arrBytBuffer, 0, intRead);
                    // Cierra el stream FTP
                    stmFTP.Close();
                }
                // Cierra el stream del archivo
                stmFile.Close();
            }

        }

        public void PostUploadFileRefirma()
        {
            long serialVersion = 1L;
            String UPLOAD_DIRECTORY = "upload";
            int THRESHOLD_SIZE = 1024 * 1024 * 3;
            int MAX_FILE_SIZE = 1024 * 1024 * 100;
            int MAX_REQUEST_SIZE = 1024 * 1024 * 110;

            Uri pathFile = Request.Url;
            Directory.CreateDirectory(pathFile.ToString() + "/" + UPLOAD_DIRECTORY);
            FileUpload fileUpload = new FileUpload();

            if (fileUpload.HasFile)
            {

            }

        }

        [HttpGet]
        public void doGetFile(string documentName)
        {
            string MetodoGetFile = ConfigurationManager.AppSettings["MetodoGetFiles"];


            Uri paths = Request.Url;
            WebClient web = new WebClient();
            web.Credentials = CredentialCache.DefaultCredentials;
            web.Credentials = CredentialCache.DefaultNetworkCredentials;
            web.UseDefaultCredentials = true;
            byte[] byteArray = web.DownloadData(paths);
            HttpContext.Response.ContentType = "application/pdf";
            HttpContext.Response.AddHeader("Content-disposition", "attachment; filename=" + documentName);
            HttpContext.Response.AddHeader("Cache-Control", "max-age=30");
            HttpContext.Response.AddHeader("Pragma", "No-cache");
            HttpContext.Response.OutputStream.Write(byteArray, 0, byteArray.Length);
       
        }

        [HttpGet]
        public ActionResult doGet()
        {
            var filename = "CARTA_191219_191202[R].pdf";
            return Json(filename, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void doPost(string type)
        {
          
            String serverPath = string.Empty;

            if (Request.Url.Host.Equals("localhost"))
            {
                serverPath = ConfigurationManager.AppSettings["PROTOCOL"].ToString() + "://" + Request.Url.Host.Replace("http://", " ").Replace("https://", " ");
            }
            try
            {
                //type = request.Params["type"].ToString();
                String arguments = "";

                String protocol = "";

                if (Request.Url.Host.Equals("https://"))
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
     
                String path = Path.Combine(@"\\Desktop-o2hga1l\sigesdoc\file64\files.txt");
                //StreamWriter fs = System.IO.File.CreateText(path);
                //byte[] info = new UTF8Encoding(true).GetBytes(arguments);
                //fs.Write(arguments);
                byte[] arg__ = Encoding.ASCII.GetBytes(arguments);
                System.IO.File.WriteAllBytes(path, arg__);
                //System.IO.File.CreateText(arguments);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string paramWeb(string protocol, string ServerPath)
        {
            UrlHelper urlHelper = new UrlHelper();
            string base64String = null;
            string CLIENTID = ConfigurationManager.AppSettings["CLIENTID"];
            string CLIENTSECRET = ConfigurationManager.AppSettings["CLIENTSECRET"];
            string fileDownloadLogoUrl = @"http://localhost:8089/Content/img/firma2.jpg";
            string fileDownloadStampUrl = @"http://localhost:8089/Content/img/firma1.jpg";

            Dictionary<string, object> param = new Dictionary<string, object>() {

                //{ "app", "pdf" },
                //{ "clientId", CLIENTID },
                //{ "clientSecret", CLIENTSECRET},
                //{ "idFile", "001" },
                //{ "type", "W" },
                //{ "protocol", protocol },
                //{ "fileDownloadUrl", @"C:\SIGESDOC\PDF\INFORME_221219_211248.pdf"},
                //{ "fileDownloadLogoUrl", fileDownloadLogoUrl},
                //{ "fileDownloadStampUrl", fileDownloadStampUrl},
                //{ "fileUploadUrl", ServerPath + "uploadServlet" },
                //{ "contentFile", "application/pdf"},
                //{ "reason", "Soy el autor del documento" },
                //{ "isSignatureVisible", "true"},
                //{ "stampAppearanceId", "0"},
                //{ "pageNumber", "0"},
                //{ "posx", "5"},
                //{ "posy", "5"},
                //{ "fontSize", "7"},
                //{ "dcfilter", ".*FIR.*|.*FAU.*"},
                //{ "timestamp", "fasle"},
                //{ "signatureLevel", ""},
                //{ "outputFile", "INFORME_221219_211248[R].pdf"},
                //{ "maxFileSize", "5242880"}

                { "fileUploadUrl", @"http://localhost:8089/Document/Upload" },
                { "reason", "Soy el autor del documento"},
                { "pageNumber", "0"},
                { "maxFileSize", "5242880" },
                { "type", "W" },
                { "app", "pdf" },
                { "clientId", CLIENTID },
                { "clientSecret", CLIENTSECRET},
                { "posx", "5"},
                { "dcfilter", ".*FIR.*|.*FAU.*" },
                { "fileDownloadUrl", @"http://localhost:8089/Content/pdf/OFICIO_231219_121220.pdf"},
                { "posy", "Soy el autor del documento" },
                { "outputFile", "OFICIO_231219_121220[R].pdf" },
                { "protocol", "T"},
                { "contentFile", "OFICIO_231219_121220.pdf" },
                { "stampAppearanceId","0" },
                { "isSignatureVisible","true" },
                { "idFile","001" },
                { "fileDownloadLogoUrl", fileDownloadLogoUrl},
                { "fontSize", "7"},
                { "fileDownloadStampUrl", fileDownloadStampUrl},
                { "timestamp", "true"}
                
            };

            string json = JsonConvert.SerializeObject(param, Formatting.None);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            base64String = Convert.ToBase64String(bytes);

            return base64String;
        }

   }



    public class FormFile
    {
        public string Name { get; set; }

        public string ContentType { get; set; }

        public string FilePath { get; set; }

        public Stream Stream { get; set; }
    }

}


