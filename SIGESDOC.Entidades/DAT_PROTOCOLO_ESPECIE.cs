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
    
    public partial class DAT_PROTOCOLO_ESPECIE
    {
        public int ID_PRO_ESPE { get; set; }
        public Nullable<int> ID_PROTOCOLO { get; set; }
        public Nullable<int> ID_DET_ESPEC_HAB { get; set; }
        public string ACTIVO { get; set; }
    
        public virtual DAT_ESPECIES_HABILITACIONES DAT_ESPECIES_HABILITACIONES { get; set; }
        public virtual MAE_PROTOCOLO MAE_PROTOCOLO { get; set; }
    }
}
