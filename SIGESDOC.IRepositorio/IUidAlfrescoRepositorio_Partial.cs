using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGESDOC.Response;


namespace SIGESDOC.IRepositorio
{
    public partial interface IUidAlfrescoRepositorio
    {
        IEnumerable<UidAlfrescoResponse> ObtenerUIDAlfresco(string nom_doc, string tipo_doc, string uid_alfresco, string num_doc);
        void InserirNuevoUIIDALfresco(string NOM_DOC, string TIPO_DOC, string UID_ALFRESCO, int VERSION_DOCUMENTO, DateTime FECHA_REGISTRO, string ESTADO_DOC, string NUM_DOC, string PATHDOC_ALFRESCO, string PATHDOC_FTP, string BPDF_DOC, int ID_DOCUMENTO);
    }
}
