//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIGESDOC.IRepositorio
{
    using System;
    
    public partial class P_ASIGNA_OFICINA_PERSONA_Result
    {
        public int id_per_empresa { get; set; }
        public string persona_num_documento { get; set; }
        public bool activo { get; set; }
        public int id_oficina_direccion { get; set; }
        public string usuario_crea { get; set; }
        public Nullable<System.DateTime> fecha_crea { get; set; }
        public string usuario_modifica { get; set; }
        public Nullable<System.DateTime> fecha_modifica { get; set; }
    }
}