using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClassePedidoAprovacaoPrincipal : clsConexao
    {
        public List<WSClassePedidoAprovacao> ListaPedidoAprovacao { get; set; }
    }
}