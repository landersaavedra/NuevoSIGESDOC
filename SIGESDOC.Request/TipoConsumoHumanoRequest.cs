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
    
    public partial class TipoConsumoHumanoRequest
    {
        public int id_tipo_ch { get; set; }
        public string siglas { get; set; }
        public string nombre { get; set; }
    
        public virtual List<ProtocoloAlmacenRequest> protocolo_almacen { get; set; }
        public virtual List<ProtocoloPlantaRequest> protocolo_planta { get; set; }
        public virtual List<ProtocoloRequest> protocolo { get; set; }
    }
}
