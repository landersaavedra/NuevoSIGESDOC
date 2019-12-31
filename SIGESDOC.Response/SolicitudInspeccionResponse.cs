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
    
    public partial class SolicitudInspeccionResponse
    {
        public int id_sol_ins { get; set; }
        public Nullable<int> id_seguimiento { get; set; }
        public Nullable<int> numero_documento { get; set; }
        public Nullable<System.DateTime> fecha_crea { get; set; }
        public Nullable<int> oficina_crea { get; set; }
        public string usuario_crea { get; set; }
        public Nullable<int> año_crea { get; set; }
        public Nullable<int> id_version_solicitud { get; set; }
        public string resolucion { get; set; }
        public string persona_contacto { get; set; }
        public string telefono_oficina { get; set; }
        public string telefono_planta { get; set; }
        public string correo { get; set; }
        public string observaciones { get; set; }
        public Nullable<int> id_tipo_ser_hab { get; set; }
        public Nullable<int> id_filial { get; set; }
        public Nullable<int> id_dest_sol_ins { get; set; }
        public string cond_manuales { get; set; }
        public string norma_aplica { get; set; }
        public Nullable<int> id_estado { get; set; }
        public string usuario_recepcion { get; set; }
        public Nullable<System.DateTime> fecha_recepcion { get; set; }
        public string nom_ofi_crea { get; set; }
        public Nullable<int> id_oficina_destino { get; set; }
        public string nom_oficina_destino { get; set; }
        public string inspector { get; set; }
        public Nullable<System.DateTime> fecha_inspeccion { get; set; }
        public Nullable<System.DateTime> fecha_recepcion_inspector { get; set; }
    
        public virtual VersionSolicitudResponse version_solicitud { get; set; }
        public virtual List<ActaInspeccionDsfpaResponse> acta_inspeccion_dsfpa { get; set; }
        public virtual List<InformeInspeccionDsfpaResponse> informe_inspeccion_dsfpa { get; set; }
        public virtual List<PruebaInspeccionDsfpaResponse> prueba_inspeccion_dsfpa { get; set; }
        public virtual List<CheckListInspeccionDsfpaResponse> check_list_inspeccion_dsfpa { get; set; }
        public virtual SeguimientoDhcpaResponse seguimiento_dhcpa { get; set; }
    }
}
