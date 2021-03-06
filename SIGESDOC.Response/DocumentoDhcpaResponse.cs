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
    
    public partial class DocumentoDhcpaResponse
    {
        public int id_doc_dhcpa { get; set; }
        public Nullable<int> id_tipo_documento { get; set; }
        public int num_doc { get; set; }
        public string nom_doc { get; set; }
        public Nullable<System.DateTime> fecha_doc { get; set; }
        public string asunto { get; set; }
        public string anexos { get; set; }
        public Nullable<System.DateTime> fecha_registro { get; set; }
        public string usuario_registro { get; set; }
        public Nullable<int> id_archivador { get; set; }
        public Nullable<int> id_filial { get; set; }
        public Nullable<int> numero_ht { get; set; }
        public string pdf { get; set; }
        public Nullable<int> id_oficina_direccion { get; set; }
        public string evaluador_cdl_notif { get; set; }
        public string direccion_cdl_notif { get; set; }
        public string empresa_cdl_notif { get; set; }
        public Nullable<int> folia_cdl_notif { get; set; }
        public string doc_notificar_cdl_notif { get; set; }
        public string exp_o_ht_cdl_notif { get; set; }
        public string exp_o_ht_n_cdl_notif { get; set; }
        public string ruc { get; set; }
        public Nullable<int> id_det_documento { get; set; }
        public bool flag_estado_firma { get; set; }
    
        public virtual List<DetSegDocDhcpaResponse> det_seg_doc_dhcpa { get; set; }
        public virtual List<DocumentoDhcpaDetalleResponse> documento_dhcpa_detalle { get; set; }
        public virtual List<FirmaDigitalResponse> firma_digital { get; set; }
    }
}
