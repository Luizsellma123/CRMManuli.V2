using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ComunicacaoSAPClienteClass
    {
        public string CodigoClienteSAP { get; set; }
        public string CodigoClienteTipoContato { get; set; }
        public int CodigoClienteLinha { get; set; }
        public string CodigoClientePrimeiroNome { get; set; }
        public string CodigoClienteUltimoNome { get; set; }
        public string CodigoClienteEmail { get; set; }
        public string CodigoClienteTelefone1 { get; set; }

    }
}