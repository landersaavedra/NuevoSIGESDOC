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
    
    public partial class MAE_EXPEDIENTES
    {
        public int ID_EXPEDIENTE { get; set; }
        public Nullable<int> NUMERO_EXPEDIENTE { get; set; }
        public int ID_TIPO_EXPEDIENTE { get; set; }
        public Nullable<System.DateTime> FECHA_REGISTRO { get; set; }
        public string USUARIO_REGISTRO { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICO { get; set; }
        public string USUARIO_MODIFICO { get; set; }
        public string INDICADOR_SEGUIMIENTO { get; set; }
        public string NOM_EXPEDIENTE { get; set; }
        public Nullable<int> AÑO_CREA { get; set; }
    
        public virtual MAE_TIPO_EXPEDIENTE MAE_TIPO_EXPEDIENTE { get; set; }
    }
}
