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
    
    public partial class ConsultarDniPersonalLegalRequest
    {
        public int id_dni_persona_legal { get; set; }
        public string dni { get; set; }
        public string documento { get; set; }
        public string nombres_y_apellidos { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string fecha_registro { get; set; }
        public string fecha_desactivado { get; set; }
        public Nullable<int> activo { get; set; }
    }
}