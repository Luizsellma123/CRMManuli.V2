using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseClienteContato
    {
        public int CodigoSAP { get; set; }
        public string CodigoClienteSAP { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string TipoContato { get; set; }
    }
}