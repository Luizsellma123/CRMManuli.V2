using System;
using System.Web;
using System.Linq;
using System.Data;
using System.Web.UI;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace VendasWeb.Producao
{
    public partial class PrazoProducaoProdutosWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        producao ObjProducao = new producao();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

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
                CarregaDadosNaTela();
                BuscarButton_Click(null, null);
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            ObjProducao.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue ?? "0");
            ObjProducao.Produto = ProdutoTextBox.Text ?? "";

            ProducaoGridView.DataSource = ObjProducao.RetornaListaPrazoProducaoProdutos();
            ProducaoGridView.DataBind();
            ProducaoMultiView.Visible = true;
        }

        protected void ProducaoGridTextBox_TextChanged(object sender, EventArgs e)
        {
            ObjProducao.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaLabel")).Text);
            ObjProducao.IDProduto = Convert.ToInt32(((Label)((Control)sender).FindControl("IDProdutoLabel")).Text);
            ObjProducao.PrazoProducao = Convert.ToInt32(((TextBox)((Control)sender).FindControl("ProducaoGridTextBox")).Text);

            ApresentaMensagem(ObjProducao.AtualizaPrazoProducaoProdutos());

            BuscarButton_Click(null, null);
        }

        protected void ExpedicaoGridTextBox_TextChanged(object sender, EventArgs e)
        {
            ObjProducao.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaLabel")).Text);
            ObjProducao.IDProduto = Convert.ToInt32(((Label)((Control)sender).FindControl("IDProdutoLabel")).Text);
            ObjProducao.PrazoExpedicao = Convert.ToInt32(((TextBox)((Control)sender).FindControl("ExpedicaoGridTextBox")).Text);

            ApresentaMensagem(ObjProducao.AtualizaPrazoProducaoProdutos());

            BuscarButton_Click(null, null);
        }

        protected void EstoqueGridTextBox_TextChanged(object sender, EventArgs e)
        {
            ObjProducao.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaLabel")).Text);
            ObjProducao.IDProduto = Convert.ToInt32(((Label)((Control)sender).FindControl("IDProdutoLabel")).Text);
            ObjProducao.QuantidadeEstoque = Convert.ToDecimal(((TextBox)((Control)sender).FindControl("EstoqueGridTextBox")).Text);

            ApresentaMensagem(ObjProducao.AtualizaPrazoProducaoProdutos());

            BuscarButton_Click(null, null);
        }

        protected void ExcluirLinkButton_Click(object sender, EventArgs e)
        {
            ObjProducao.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaLabel")).Text);
            ObjProducao.IDProduto = Convert.ToInt32(((Label)((Control)sender).FindControl("IDProdutoLabel")).Text);

            ApresentaMensagem(ObjProducao.ExcluiPrazoProducaoProdutos());

            BuscarButton_Click(null, null);
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/HomeProducaoWebForm.aspx?indmnu=3");
        }

        protected void ApresentaMensagem(string erro)
        {
            if (erro != "" && erro != null)
            {
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                //Retorna Mensagem de Sucesso
                Session["Msg"] = "Sucesso na operação.";
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }
        }

        protected void ProducaoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProducaoGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }

    }
}