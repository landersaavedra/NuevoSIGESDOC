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
    
    public partial class VW_CONSULTA_PROTOCOLOS_AI
    {
        public int ID_PROTOCOLO { get; set; }
        public Nullable<int> ID_SEGUIMIENTO { get; set; }
        public string NOMBRE { get; set; }
        public Nullable<System.DateTime> FECHA_INICIO { get; set; }
        public Nullable<System.DateTime> FECHA_FIN { get; set; }
        public Nullable<System.DateTime> FECHA_REGISTRO { get; set; }
        public string EVALUADOR { get; set; }
        public string ACTIVO { get; set; }
        public Nullable<int> ID_EST_PRO { get; set; }
        public Nullable<int> ID_PROTOCOLO_REEMPLAZA { get; set; }
        public string RUTA_PDF { get; set; }
    }
}