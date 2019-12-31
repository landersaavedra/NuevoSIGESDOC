using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGESDOC.Request
{
    public class FirmaDigital
    {
        public string id_firma_digital { get; set; }
        public string id_documento { get; set; }
        public string persona_firmar { get; set; }
        public string id_usuario_envio { get; set; }
        public string estado_envio { get; set; }
        public string fecha_firma { get; set; }
        public string id_uid_alfresco { get; set; }
        public string estado_persona_firmar { get; set; }
        public string pathFilePDF { get; set; }
        public string filename { get; set; }
        public string contentype { get; set; }

    }
}
