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
    
    public partial class DAT_DET_SEG_DOC_DHCPA
    {
        public int ID_DET_DSDHCPA { get; set; }
        public int ID_SEGUIMIENTO { get; set; }
        public int ID_DOC_DHCPA { get; set; }
        public string ACTIVO { get; set; }
    
        public virtual MAE_SEGUIMIENTO_DHCPA MAE_SEGUIMIENTO_DHCPA { get; set; }
        public virtual MAE_DOCUMENTO_DHCPA MAE_DOCUMENTO_DHCPA { get; set; }
    }
}
