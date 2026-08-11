using System;
using System.Runtime.Serialization;

namespace VendasWeb.classes
{
    [DataContract]
    public class ImpostosPedido
    {
        [DataMember]
        public string NumeroPedidoSAP { get; set; }

        public ImpostosPedido(string NumeroPedidoSAP)
        {
            this.NumeroPedidoSAP = NumeroPedidoSAP;
        }
    }
}