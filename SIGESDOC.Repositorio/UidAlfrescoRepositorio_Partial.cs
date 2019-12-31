using SIGESDOC.Contexto;
using SIGESDOC.IRepositorio;
using SIGESDOC.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGESDOC.Repositorio
{
    public partial class UidAlfrescoRepositorio: IUidAlfrescoRepositorio
    {
      public IEnumerable<UidAlfrescoResponse> ObtenerUIDAlfresco(string nom_doc, string tipo_doc, string uid_alfresco, string num_doc) 
      {
            DB_GESDOCEntities _dataContext = base.Context.GetContext() as DB_GESDOCEntities;

            var result = from r in _dataContext.OBTENER_UID_ALFRESCO(nom_doc, tipo_doc, uid_alfresco, num_doc)
                         select new UidAlfrescoResponse()
                         {
                             //uid_alfresco = r.
                         };
            return result;

        }

        public void InserirNuevoUIIDALfresco(string NOM_DOC, string TIPO_DOC, string UID_ALFRESCO, int VERSION_DOCUMENTO, DateTime FECHA_REGISTRO, string ESTADO_DOC, string NUM_DOC, string PATHDOC_ALFRESCO, string PATHDOC_FTP, string BPDF_DOC, int ID_DOCUMENTO)
        {
            DB_GESDOCEntities _dataContext = base.Context.GetContext() as DB_GESDOCEntities;

            try
            {
                _dataContext.SP_INSERT_LOG_UUID_ALFRESCO(NOM_DOC, TIPO_DOC, UID_ALFRESCO, VERSION_DOCUMENTO, FECHA_REGISTRO, ESTADO_DOC, NUM_DOC, PATHDOC_ALFRESCO, PATHDOC_FTP, BPDF_DOC, ID_DOCUMENTO);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


    }
}
