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
    
    public partial class DocumentoAnexoRequest
    {
        public int id_documento_anexo { get; set; }
        public Nullable<int> id_documento { get; set; }
        public string ruta { get; set; }
        public string descripcion { get; set; }
        public string extension { get; set; }
        public string usuario_crea { get; set; }
        public Nullable<System.DateTime> fecha_crea { get; set; }
        public string activo { get; set; }
    
        public virtual DocumentoRequest documento { get; set; }
    }
}
