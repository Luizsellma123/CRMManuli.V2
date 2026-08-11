using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClassePedidoAtualizacaoAprovacao
    {
        public string cod_esboco { get; set; }
        public DateTime data_entrega { get; set; }
        public DateTime data_lancamento { get; set; }

        public int IDEmpresa { get; set; }
        public int IDPedido { get; set; }
        public string LiberadoProducaoClicheCRM { get; set; }
    }
}