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
    
    public partial class DetDocFactResponse
    {
        public int id_det_doc_fact { get; set; }
        public Nullable<int> id_documento_seg { get; set; }
        public Nullable<int> id_factura { get; set; }
        public string activo { get; set; }
    
        public virtual DocumentoSeguimientoResponse documento_seguimiento { get; set; }
    }
}
