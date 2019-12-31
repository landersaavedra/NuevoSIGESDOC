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
    
    public partial class TupaRequest
    {
        public int id_tupa { get; set; }
        public Nullable<int> numero { get; set; }
        public Nullable<int> id_tipo_procedimiento { get; set; }
        public string asunto { get; set; }
        public string activo { get; set; }
        public Nullable<int> dias_tupa { get; set; }
        public Nullable<int> id_tipo_tupa { get; set; }
        public Nullable<int> id_oficina { get; set; }
        public Nullable<int> prorroga_dias { get; set; }
        public Nullable<double> costo { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<System.DateTime> fecha_caducacion { get; set; }
        public Nullable<int> anio_servicio { get; set; }
    
        public virtual List<SubTupaRequest> sub_tupa { get; set; }
        public virtual TipoProcedimientoRequest tipo_procedimiento { get; set; }
        public virtual TipoTupaRequest tipo_tupa { get; set; }
    }
}