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
    
    public partial class LogDesarchivoDesatendidoRequest
    {
        public int id_log_des_ar_at { get; set; }
        public Nullable<int> id_det_documento { get; set; }
        public Nullable<byte> old_id_est_tramite { get; set; }
        public Nullable<System.DateTime> old_fecha { get; set; }
        public string old_usuario { get; set; }
        public string old_observacion { get; set; }
        public Nullable<System.DateTime> fecha_desactivo { get; set; }
        public string usuario_desactivo { get; set; }
        public string observacion { get; set; }
    
        public virtual EstadoTramiteRequest estado_tramite { get; set; }
        public virtual DocumentoDetalleRequest documento_detalle { get; set; }
    }
}
