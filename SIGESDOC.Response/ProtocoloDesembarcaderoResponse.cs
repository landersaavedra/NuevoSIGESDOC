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
    
    public partial class ProtocoloDesembarcaderoResponse
    {
        public int id_det_pro_desemb { get; set; }
        public Nullable<int> id_protocolo { get; set; }
        public Nullable<int> id_desembarcadero { get; set; }
        public string derecho_uso_area_acuatica { get; set; }
        public Nullable<int> direccion_legal { get; set; }
        public Nullable<int> representante_legal { get; set; }
    
        public virtual ProtocoloResponse protocolo { get; set; }
    }
}
