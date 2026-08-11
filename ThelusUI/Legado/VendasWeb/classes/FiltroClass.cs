using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.GerencialVendas
{
    public class FiltroClass
    {

        public string DropOpcaoFiltro { get; set; } //Opcao do Combo disponivel na tela
        public string TextoFiltro { get; set; } //Valor digitado 

        public string EmpCod { get; set; }
        public string PedVendaStatDescr { get; set; }
        public string PedVendaTipo { get; set; }
        public List<produto> itemProdutoList { get; set; }

        //Metodo para inserir produtos items do pedido
        public void incluiItem(produto itemProduto)
        {
            //Verifica se esta instanciado
            if (this.itemProdutoList == null)
            {
                this.itemProdutoList = new List<produto>();
            }
            this.itemProdutoList.Add(itemProduto);
        }

        /*Filtros Liberar Orçamento*/
        public string EmpCodLiberarOrcamento { get; set; }
        public string PedVendaNumOrcamento { get; set; }
        public string EntidadeOrcamento { get; set; }
        public string SituacaoOrcamento { get; set; }
        public string AprovadoOrcamento { get; set; }
        public string UsuCodOrcamento { get; set; }
        public int indice { get; set; }

        /*Filtros liberar Pedidos Financeiro*/
        public string FinanceiroEmpresa { get; set; }
        public string FinanceiroSituacao { get; set; }
        public string FinanceiroCliente { get; set; }
        public string FinanceiroPedidoCRM { get; set; }
        public string FinanceiroPedidoSAP { get; set; }
        public string FinanceiroEsbocoSAP { get; set; }

    }
}