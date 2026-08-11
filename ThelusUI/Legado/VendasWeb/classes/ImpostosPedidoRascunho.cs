using System;
using System.Runtime.Serialization;

namespace VendasWeb.classes
{
    [DataContract]
    public class ImpostosPedidoRascunho
    {
        [DataMember]
        public string NumeroEsbocoSAP { get; set; }

        public ImpostosPedidoRascunho(string NumeroEsbocoSAP)
        {
            this.NumeroEsbocoSAP = NumeroEsbocoSAP;
        }
    }
}
