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
    
    public partial class ConsultarDniResponse
    {
        public string persona_num_documento { get; set; }
        public Nullable<byte> tipo_doc_iden { get; set; }
        public string nom_tipo_doc { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string nombres { get; set; }
        public Nullable<System.DateTime> fecha_nacimiento { get; set; }
        public string ubigeo { get; set; }
        public string sexo { get; set; }
        public string direccion { get; set; }
        public string ruc { get; set; }
    }
}