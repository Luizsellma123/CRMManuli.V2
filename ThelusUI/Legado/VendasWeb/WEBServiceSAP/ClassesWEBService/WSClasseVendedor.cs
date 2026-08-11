using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseVendedor
    {
        public string NomeVendedor { get; set; }
        public int CodigoVendedorSAP { get; set; }
        public string ClasseVendedor { get; set; }
        public string EmailVendedor { get; set; }
        public string TipoVendedor { get; set; }
        public string StatusVendedor { get; set; }
    }
}