//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIGESDOC.Request
{
    using System;
    using System.Collections.Generic;
    
    public partial class FirmaDigitalRequest
    {
        public int id_firma_digital { get; set; }
        public Nullable<int> id_documento { get; set; }
        public string persona_firmar { get; set; }
        public Nullable<int> id_usuario_envio { get; set; }
        public Nullable<bool> estado_envio { get; set; }
        public Nullable<System.DateTime> fecha_firma { get; set; }
        public Nullable<int> id_uid_alfresco { get; set; }
        public Nullable<bool> estado_persona_firmar { get; set; }
    
        public virtual UidAlfrescoRequest uid_alfresco { get; set; }
        public virtual DocumentoRequest documento { get; set; }
        public virtual DocumentoDhcpaRequest documento_dhcpa { get; set; }
    }
}
