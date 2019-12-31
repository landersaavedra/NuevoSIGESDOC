using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGESDOC.Response;

namespace SIGESDOC.IRepositorio
{
    public partial interface IFirmaDigitalRepositorio
    {
        IEnumerable<FirmaDigitalResponse> Listar_FirmaDigital(int id);
        void Create_Firma_Digital(int id_documento, string persona_firmar, int id_usuario_envio, bool estado_envio, DateTime fecha_firma, int id_uid_alfresco, bool estado_persona_firmar);
        void Update_Firma_Digital_Activar_Visado(int id_documento, bool estado_persona_firmar, string persona_firmar);
    }
}
