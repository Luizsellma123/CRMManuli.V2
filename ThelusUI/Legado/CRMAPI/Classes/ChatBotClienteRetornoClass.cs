using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ChatBotClienteRetornoClass
    {
        public string CodigoClienteSAP { get; set; }
        public string CodigoClienteCRM { get; set; }
        public string NomeCliente { get; set; }
        public string NomeVendedor { get; set; }
        public string Encontrado { get; set; }
    }
}