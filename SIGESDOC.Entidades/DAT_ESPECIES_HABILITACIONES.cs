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
    
    public partial class DAT_ESPECIES_HABILITACIONES
    {
        public DAT_ESPECIES_HABILITACIONES()
        {
            this.DAT_PROTOCOLO_ESPECIE = new HashSet<DAT_PROTOCOLO_ESPECIE>();
        }
    
        public int ID_DET_ESPEC_HAB { get; set; }
        public string CODIGO_ESPECIE { get; set; }
        public string ACTIVO { get; set; }
    
        public virtual ICollection<DAT_PROTOCOLO_ESPECIE> DAT_PROTOCOLO_ESPECIE { get; set; }
    }
}