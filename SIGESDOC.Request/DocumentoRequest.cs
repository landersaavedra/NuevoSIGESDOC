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
    
    public partial class DocumentoRequest
    {
        public int id_documento { get; set; }
        public int numero { get; set; }
        public byte id_tipo_documento { get; set; }
        public Nullable<int> numero_documento { get; set; }
        public string anexos { get; set; }
        public int folios { get; set; }
        public int oficina_crea { get; set; }
        public System.DateTime fecha_envio { get; set; }
        public string usuario_crea { get; set; }
        public string nom_doc { get; set; }
        public string persona_crea { get; set; }
        public Nullable<byte> id_indicador_documento { get; set; }
        public string ruta_pdf { get; set; }
        public Nullable<int> num_ext { get; set; }
        public string nom_oficina_crea { get; set; }
        public bool flag_estado_firma { get; set; }
    
        public virtual DocumentoIndicadorRequest documento_indicador { get; set; }
        public virtual HojaTramiteRequest hoja_tramite { get; set; }
        public virtual TipoDocumentoRequest tipo_documento { get; set; }
        public virtual List<DocumentoAnexoRequest> documento_anexo { get; set; }
        public virtual List<DocumentoDetalleRequest> documento_detalle { get; set; }
        public virtual List<UidAlfrescoRequest> uid_alfresco { get; set; }
        public virtual List<FirmaDigitalRequest> firma_digital { get; set; }
    }
}
