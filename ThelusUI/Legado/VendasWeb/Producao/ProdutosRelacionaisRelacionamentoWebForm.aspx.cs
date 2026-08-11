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
    public partial class ProdutosRelacionaisRelacionamentoWebForm : System.Web.UI.Page
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

            if (Session["ProdutosRelacionaisRelacionamento"] != null)
            {
                ObjProducao = (producao)Session["ProdutosRelacionaisRelacionamento"];
            }

            if (!IsPostBack)
            {
                CarregaDadosTela();
            }
            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosTela();
        }

        public void CarregaDadosTela()
        {
            ProdutoOrigemTextBox.Text = ObjProducao.ProdutoOrigem.ToString();

            ObjProducao.ProdutoRelacionado = ProdutoRelacionalTextBox.Text;

            ObjProducao.IDProdutoOrigem = ObjProducao.IDProdutoOrigem;

            DataTable OBJDataTable = new DataTable();

            OBJDataTable = ObjProducao.ListaProdutosRelacionaisRelacionamento();
            ProdutosRelacionaisGridView.DataSource = OBJDataTable;
            ProdutosRelacionaisGridView.DataBind();
            ProdutosRelacionaisMultiView.Visible = true;
        }

        protected void SelCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AtualizaDados(sender, e);

            //Recarrega Tela
            CarregaDadosTela();
        }

        public void AtualizaDados(object sender, EventArgs e)
        {
            ObjProducao.IDProdutoRelacionado = Convert.ToInt32(((Label)((Control)sender).FindControl("IDProdutoRelacionadoLabel")).Text);
            //ObjProducao.ProdutoRelacionado = ((Label)((Control)sender).FindControl("ProdutoRelacionadoLabel")).Text;
            ObjProducao.Relacionado = ((CheckBox)((Control)sender).FindControl("SelCheckBox")).Checked;

            ObjProducao.AtualizaListaProdutosRelacionais();

        }

        protected void ProdutosRelacionaisGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProdutosRelacionaisGridView.PageIndex = e.NewPageIndex;
            CarregaDadosTela();
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/ProdutosRelacionaisWebForm.aspx?indmnu=3");
        }
    }
}