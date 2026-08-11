using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClassePedidoImpostos
    {
        public string DocNum { get; set; }
        public string ItemCode { get; set; }
        public string Name { get; set; }
        public decimal Imposto { get; set; }
        public decimal DocTotal { get; set; }
        public decimal PercentualImpostos { get; set; }
    }
}