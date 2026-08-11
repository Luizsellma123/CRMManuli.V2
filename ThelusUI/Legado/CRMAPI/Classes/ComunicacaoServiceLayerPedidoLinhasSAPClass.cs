using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ComunicacaoServiceLayerPedidoLinhasSAPClass
    {
        public string CodigoDeposito { get; set; }
        public int Utilizacao { get; set; }
        public string CodigoArruela { get; set; }
        public string CodigoCliche { get; set; }
        public string CodigoItem { get; set; }
        public string CodigoUnidadeMedida { get; set; }
        public string PosicaoItem { get; set; }
        public string NaturezaDestinacao { get; set; }
        public string NomeUnidadeDeMedida { get; set; }
        public double Valorunitario { get; set; }
        public double Quantidade { get; set; }
        public string ObservacaoItem { get; set; }
        public string NumeroPedidoCliente { get; set; }

    }
}