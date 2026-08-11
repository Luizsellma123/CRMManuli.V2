using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace VendasWeb.Producao
{
    public partial class ProdutosRelacionaisWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        producao ObjProducao = new producao();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = false;
            }

            if (!IsPostBack)
            {
                CarregaDadosTela();
            }
            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        public void CarregaDadosTela()
        {
            ObjProducao.Produto = ProdutoTextBox.Text;

            DataTable OBJDataTable = new DataTable();

            OBJDataTable = ObjProducao.ListaProdutosRelacionais();
            ProdutosRelacionaisGridView.DataSource = OBJDataTable;
            ProdutosRelacionaisGridView.DataBind();
            ProdutosRelacionaisMultiView.Visible = true;
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosTela();
        }

        protected void SelecionarLinkButton_Click(object sender, EventArgs e)
        {
            ObjProducao.IDProdutoOrigem = Convert.ToInt32(((Label)((Control)sender).FindControl("IDProdutoOrigemLabel")).Text);
            ObjProducao.ProdutoOrigem = ((Label)((Control)sender).FindControl("ProdutoOrigemLabel")).Text;
            Session["ProdutosRelacionaisRelacionamento"] = ObjProducao;
            Response.Redirect("~/Producao/ProdutosRelacionaisRelacionamentoWebForm.aspx?indmnu=3");
        }

        protected void ProdutosRelacionaisGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProdutosRelacionaisGridView.PageIndex = e.NewPageIndex;
            CarregaDadosTela();
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/HomeProducaoWebForm.aspx?indmnu=3");
        }
    }
}