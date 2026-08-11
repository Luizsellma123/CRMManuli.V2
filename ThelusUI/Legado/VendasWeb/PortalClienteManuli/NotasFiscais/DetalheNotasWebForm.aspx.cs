using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.PortalClienteManuli.NotasFiscais
{
    public partial class DetalheNotasWebForm : System.Web.UI.Page
    {
        PedidoClass PedidoClass = new PedidoClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica se tem usuário logado no Portal
            if (Session["usuarioPortal"] == null)
            {
                //Redireciona para tela de login
                Response.Redirect("LoginPortal.aspx");

            }

            if (!IsPostBack)
            {
                PedidoClass = (GerencialVendas.PedidoClass)Session["PedidoClass"];

                EmpresaLabel.Text = PedidoClass.EmpCod + " - " + PedidoClass.EmpNome;
                PedVendaNumLabel.Text = PedidoClass.PedVendaNum;
                NomeEntidadeLabel.Text = PedidoClass.EntNome;
                NotaLabel.Text = PedidoClass.NfNum;
                CondPagamentoLabel.Text = PedidoClass.CondPagPedVendaNome;
                TotalPedidoLabel.Text = Convert.ToDecimal(PedidoClass.PedVendaValTotal).ToString("C");

                //Função para carregar itens na tela
                carregaItemPedido();
            }
        }

        public void carregaItemPedido()
        {
            DataTable RetornoDados = new DataTable();
            PedidoClass = (PedidoClass)Session["PedidoClass"];

            RetornoDados = PedidoClass.Lista_Item_Pedido_Portal();

            GridViewItemPedidosClientes.DataSource = RetornoDados;
            GridViewItemPedidosClientes.DataBind();
        }

        protected void GridViewItemPedidosClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewItemPedidosClientes.PageIndex = e.NewPageIndex;
            carregaItemPedido();
        }
    }
}
