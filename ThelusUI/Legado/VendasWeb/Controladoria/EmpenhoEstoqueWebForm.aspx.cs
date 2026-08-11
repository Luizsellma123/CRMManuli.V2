using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace VendasWeb.Controladoria
{
    public partial class EmpenhoEstoqueWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        producao objProducao = new producao();
        DataTable ListaEmpenhoEstoque = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                Session["ListaEmpenhoEstoque"] = null;

                BuscarButton_Click(null, null);
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void PedidosLinkButton_Click(object sender, EventArgs e)
        {
            objProducao.IDProduto = Convert.ToInt32(((Label)((Control)sender).FindControl("IDProdutoLabel")).Text);
            objProducao.Produto = ((Label)((Control)sender).FindControl("ProdutoLabel")).Text;
            Session["EmpenhoEstoque"] = objProducao;
            Response.Redirect("~/Controladoria/EmpenhoEstoqueDetalheWebForm.aspx?indmnu=5");
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            objProducao.Produto = ProdutoTextBox.Text;

            ListaEmpenhoEstoque = objProducao.RetornaListaEmpenhoEstoque();

            ControladoriaGridView.DataSource = ListaEmpenhoEstoque;
            ControladoriaGridView.DataBind();
            ControladoriaMultiView.Visible = true;

            Session["ListaEmpenhoEstoque"] = ListaEmpenhoEstoque;
        }

        protected void ControladoriaGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ControladoriaGridView.PageIndex = e.NewPageIndex;

            if (Session["ListaEmpenhoEstoque"] != null)
            {
                ListaEmpenhoEstoque = (DataTable)Session["ListaEmpenhoEstoque"];
            }
            else
            {
                objProducao.Produto = ProdutoTextBox.Text;
                ListaEmpenhoEstoque = objProducao.RetornaListaEmpenhoEstoque();
            }

            ControladoriaGridView.DataSource = ListaEmpenhoEstoque;
            ControladoriaGridView.DataBind();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/HomeControladoriaWebForm.aspx?indmnu=5");
        }
    }
}