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
    
    public partial class VISTA_DB_GENERAL_MAE_TRANSPORTE
    {
        public int ID_TRANSPORTE { get; set; }
        public string PLACA { get; set; }
        public Nullable<int> ID_TIPO_CARROCERIA { get; set; }
        public string NOMBRE_CARROCERIA { get; set; }
        public Nullable<int> ID_UM { get; set; }
        public string NOMBRE_UM { get; set; }
        public string SIGLAS_UM { get; set; }
        public Nullable<decimal> CARGA_UTIL { get; set; }
        public string ESTADO { get; set; }
        public string NOMBRE_ESTADO { get; set; }
        public string COD_HABILITACION { get; set; }
        public int ID_TIPO_FURGON { get; set; }
        public string NOMBRE_FURGON { get; set; }
    }
}
