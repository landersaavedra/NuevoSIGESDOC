//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIGESDOC.Response
{
    using System;
    using System.Collections.Generic;
    
    public partial class UidAlfrescoResponse
    {
        public int id_uid_alfresco { get; set; }
        public string nom_doc { get; set; }
        public string tipo_doc { get; set; }
        public string uid_alfresco { get; set; }
        public int version_documento { get; set; }
        public System.DateTime fecha_registro { get; set; }
        public string estado_doc { get; set; }
        public string num_doc { get; set; }
        public string pathdoc_alfresco { get; set; }
        public string pathdoc_ftp { get; set; }
        public byte[] bpdf_doc { get; set; }
        public Nullable<int> id_documento { get; set; }
    
        public virtual DocumentoResponse documento { get; set; }
        public virtual List<FirmaDigitalResponse> firma_digital { get; set; }
    }
}
