﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGESDOC.Request
{
    public class CargaCartaWord
    {
        public string FECHA_ACTUAL { get; set; }
        public string ASUNTO { get; set; }
        public string NOM_DOC { get; set; }
        public string REFERENCIA { get; set; }
        public string NOMBRES { get; set; }
        public string EMPRESA { get; set; }
        public string DIRECCION { get; set; }

        public string TIPO_DOCUMENTO { get; set; }
        public string ID_DOCUMENTO { get; set; }
    }
}
