using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class NotasFiscaisPedidoClass
    {
        public int Basetype { get; set; }

        public int PedidoVenda { get; set; }

        public int NotaFiscal { get; set; }

        public DateTime DataEmissao { get; set; }

        public int SeqCode { get; set; }

        public int NumeroPrimarioNota { get; set; }
    }
}