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
    
    public partial class DAT_LOG_DESARCHIVO_DESATENDIDO
    {
        public int ID_LOG_DES_AR_AT { get; set; }
        public Nullable<int> ID_DET_DOCUMENTO { get; set; }
        public Nullable<byte> OLD_ID_EST_TRAMITE { get; set; }
        public Nullable<System.DateTime> OLD_FECHA { get; set; }
        public string OLD_USUARIO { get; set; }
        public string OLD_OBSERVACION { get; set; }
        public Nullable<System.DateTime> FECHA_DESACTIVO { get; set; }
        public string USUARIO_DESACTIVO { get; set; }
        public string OBSERVACION { get; set; }
    
        public virtual MAE_ESTADO_TRAMITE MAE_ESTADO_TRAMITE { get; set; }
        public virtual DAT_DOCUMENTO_DETALLE DAT_DOCUMENTO_DETALLE { get; set; }
    }
}
