using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class PosicaoDiariaJsonModel
    {
        public string IDUsuario { get; set; }

        public string PeriodoInicial { get; set; }

        public string PeriodoFinal { get; set; }

        public string Automatico { get; set; }
    }
}