using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WsHubClienteResponseClass
    {

        public string msg { get; set; }
        public string resultPositivo { get; set; }
        public string DocNum { get; set; }
        public string ObjType { get; set; }
        public string Codigo { get; set; }
        public string Status { get; set; }
        public string lista { get; set; }

    }
}