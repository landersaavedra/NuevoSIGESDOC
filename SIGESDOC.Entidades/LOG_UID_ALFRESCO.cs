//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIGESDOC.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOG_UID_ALFRESCO
    {
        public LOG_UID_ALFRESCO()
        {
            this.MAE_FIRMA_DIGITAL = new HashSet<MAE_FIRMA_DIGITAL>();
        }
    
        public int ID_UID_ALFRESCO { get; set; }
        public string NOM_DOC { get; set; }
        public string TIPO_DOC { get; set; }
        public string UID_ALFRESCO { get; set; }
        public int VERSION_DOCUMENTO { get; set; }
        public System.DateTime FECHA_REGISTRO { get; set; }
        public string ESTADO_DOC { get; set; }
        public string NUM_DOC { get; set; }
        public string PATHDOC_ALFRESCO { get; set; }
        public string PATHDOC_FTP { get; set; }
        public byte[] BPDF_DOC { get; set; }
        public Nullable<int> ID_DOCUMENTO { get; set; }
    
        public virtual MAE_DOCUMENTO MAE_DOCUMENTO { get; set; }
        public virtual ICollection<MAE_FIRMA_DIGITAL> MAE_FIRMA_DIGITAL { get; set; }
    }
}