using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGESDOC.Request
{
    public class DocExtUpload
    {
        public string uuid { get; set; }
        public string fileName { get; set; }
        public string message { get; set; }
        public List<Status> status { get; set; }
        public string code { get; set; }

    }

    
}

public class Status
{
    public string code { get; set; }
    public string description { get; set; }
}
