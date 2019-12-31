
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGESDOC.Contexto;
using SIGESDOC.IRepositorio;
using SIGESDOC.Response;

namespace SIGESDOC.Repositorio
{
    public partial class FirmaDigitalRepositorio : IFirmaDigitalRepositorio
    {
        public IEnumerable<FirmaDigitalResponse> Listar_FirmaDigital(int id)
        {

            DB_GESDOCEntities _datacontext = base.Context.GetContext() as DB_GESDOCEntities;

            var result = (from FIRMA in _datacontext.MAE_FIRMA_DIGITAL
                          .Where(x => x.ID_DOCUMENTO == id)
                          select new FirmaDigitalResponse
                          {
                              id_firma_digital = FIRMA.ID_FIRMA_DIGITAL,
                              id_uid_alfresco = FIRMA.ID_UID_ALFRESCO, 
                              id_usuario_envio = FIRMA.ID_USUARIO_ENVIO, 
                              id_documento = FIRMA.ID_DOCUMENTO, 
                              estado_envio = FIRMA.ESTADO_ENVIO, 
                              fecha_firma = FIRMA.FECHA_FIRMA,
                              persona_firmar = FIRMA.PERSONA_FIRMAR

                          });
            return result;
        }

        public void Create_Firma_Digital(int id_documento, string persona_firmar, int id_usuario_envio, bool estado_envio, DateTime fecha_firma, int id_uid_alfresco, bool estado_persona_firmar)
        {
            DB_GESDOCEntities _dataContext = base.Context.GetContext() as DB_GESDOCEntities;

            _dataContext.SP_INSERT_FIRMA_DIGITAL(id_documento, persona_firmar, id_usuario_envio, estado_envio, fecha_firma, id_uid_alfresco, estado_persona_firmar);
        }

        public void Update_Firma_Digital_Activar_Visado(int id_documento, bool estado_persona_firmar, string persona_firmar)
        {
            DB_GESDOCEntities _dataContext = base.Context.GetContext() as DB_GESDOCEntities;

            _dataContext.UPDATE_ESTADO_PERSONA_FIRMAR(id_documento, estado_persona_firmar, persona_firmar);
        }

        
    }
}
