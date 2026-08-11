using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ComunicacaoServiceLayerPedidoSAPClass
    {
        public int CodigoEmpresaSAP { get; set; }
        public string CodigoClienteSAP { get; set; }
        public string NumeroEsbocoSAP { get; set; }
        public int CodigoVendedorSAP { get; set; }
        public int CondicaoPagamentoSAP { get; set; }
        public int NumeroPedidoCRM { get; set; }
        public DateTime DataEntrega { get; set; }
        public DateTime DataLancamento { get; set; }
        public string HistoricoPedido { get; set; }
        public string NumeroReferenciaCliente { get; set; }
        public string ObservacaoNotaFiscal { get; set; }
        public string PedidoCliente { get; set; }

        public List<ComunicacaoServiceLayerPedidoLinhasSAPClass> OBJPedidoLinhas { get; set; }
        public List<ComunicacaoServiceLayerPedidoDespesasAdicionaisSAPClass> OBJPedidoDespesasAdicionais { get; set; }
        public List<ComunicacaoServiceLayerPedidoExtensaoImpostosSAPClass> OBJPedidoExtensaoImpostos { get; set; }

        public void LimparDados()
        {
            this.CodigoEmpresaSAP = 0;
            this.CodigoClienteSAP = null; // Pode ser necessário atribuir um valor específico dependendo do tipo
            this.NumeroEsbocoSAP = null;
            this.CodigoVendedorSAP = 0;
            this.CondicaoPagamentoSAP = 0;
            this.NumeroPedidoCRM = 0;
            this.DataEntrega = DateTime.MinValue; // ou outra data padrão
            this.DataLancamento = DateTime.MinValue; // ou outra data padrão
            this.HistoricoPedido = null;
            this.NumeroReferenciaCliente = null;
            this.ObservacaoNotaFiscal = null;
            this.PedidoCliente = null;

            // Limpar listas, se necessário
            this.OBJPedidoLinhas?.Clear();
            this.OBJPedidoDespesasAdicionais?.Clear();
            this.OBJPedidoExtensaoImpostos?.Clear();
        }
    }
}