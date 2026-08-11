using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseRetornoDadosNotas
    {
        public string PedidoVenda { get; set; }
        public string NotaFiscal { get; set; }
        public string DataEmissao { get; set; }
        public int SeqCode { get; set; }
        public int NumeroPrimarioNota { get; set; }
    }
}