using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class WSFinanceiroClass
    {
        public string CodigoEmpresa { get; set; }
        public string NumeroPedidoCRM { get; set; }
        public string NumeroPedidoSAP { get; set; }
        public string NumeroEsbocoSAP { get; set; }
        public string ConsultaCliente { get; set; }
        public string StatusPedidos { get; set; }
        public string SituacaoPedido { get; set; }
        public int IDUsuarioSAP { get; set; }
        public string AnalisePedido { get; set; }
        public string HistoricoDetalhado { get; set; }
        public string Historico { get; set; }
        public string HistoricoPedido { get; set; }
        public int IDMotivo { get; set; }
        public int IDUsuarioCRM { get; set; }
        public int IDEmpresa { get; set; }
        public int IDPedido { get; set; }
        public int IDStatus { get; set; }
        public int IDCliente { get; set; }
        public string DataHistorico { get; set; }
        public string UsuarioAprovacao { get; set; }
        public string UsuarioAprovacaoSenha { get; set; }
    }
}