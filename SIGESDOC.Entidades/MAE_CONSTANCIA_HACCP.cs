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
    
    public partial class MAE_CONSTANCIA_HACCP
    {
        public int ID_CONSTANCIA_HACCP { get; set; }
        public Nullable<int> ID_SEGUIMIENTO { get; set; }
        public string NOMBRE { get; set; }
        public string ACTIVO { get; set; }
        public Nullable<System.DateTime> FECHA_REGISTRO { get; set; }
        public string USUARIO_REGISTRO { get; set; }
    
        public virtual MAE_SEGUIMIENTO_DHCPA MAE_SEGUIMIENTO_DHCPA { get; set; }
    }
}
