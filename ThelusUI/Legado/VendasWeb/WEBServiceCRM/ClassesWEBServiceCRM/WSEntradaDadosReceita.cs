using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class WSEntradaDadosReceita
    {
        public string TipoConsulta { get; set; }
        public string NumeroDocumento { get; set; }

        public WSEntradaDadosReceita()
        {
            this.TipoConsulta = "";
            this.NumeroDocumento = "0";
        }
    }
}