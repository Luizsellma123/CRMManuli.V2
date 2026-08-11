using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClassePedidoInclusaoItens
    {
        public string Dep { get; set; }
        public int Usage { get; set; }
        public string arruela { get; set; }
        public string cliche_prod { get; set; }
        public string cod_item { get; set; }
        public string cod_uni_med { get; set; }
        public string nItem { get; set; }
        public string nat_dest { get; set; }
        public string nome_uni_med { get; set; }
        public double preco { get; set; }
        public double quantidade { get; set; }
        public string texto_livre { get; set; }
        public string xPed { get; set; }
    }
}