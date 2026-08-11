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
    public partial class OrdensDeServicoPrazosWebForm : System.Web.UI.Page
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
                CarregaCombos();
                CarregaDadosTela();
            }
            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        public void CarregaCombos()
        {
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.Items.Insert(0, new ListItem("Todas", ""));

            GrupoDropDownList.DataSource = ObjProducao.RetornaListaGruposProdutos();
            GrupoDropDownList.DataTextField = "Grupo";
            GrupoDropDownList.DataValueField = "IDGrupo";
            GrupoDropDownList.DataBind();
            GrupoDropDownList.Items.Insert(0, new ListItem("Todos", ""));
        }

        public void CarregaDadosTela()
        {
            if (EmpresaDropDownList.SelectedValue != null && EmpresaDropDownList.SelectedValue != "")
            {
                ObjProducao.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
            }
            else
            {
                ObjProducao.IDEmpresa = 0;
            }

            if (GrupoDropDownList.SelectedValue != null && GrupoDropDownList.SelectedValue != "")
            {
                ObjProducao.IDGrupo = Convert.ToInt32(GrupoDropDownList.SelectedValue);
            }
            else
            {
                ObjProducao.IDGrupo = 0;
            }

            if (ProducaoTextBox.Text != "" && ProducaoTextBox.Text != null)
            {
                ObjProducao.PrazoProducao = Convert.ToInt32(ProducaoTextBox.Text);
            }
            else
            {
                ObjProducao.PrazoProducao = 0;
            }

            if (ExpedicaoTextBox.Text != "" && ExpedicaoTextBox.Text != null)
            {
                ObjProducao.PrazoExpedicao = Convert.ToInt32(ExpedicaoTextBox.Text);
            }
            else
            {
                ObjProducao.PrazoExpedicao = 0;
            }

            DataTable OBJDataTable = new DataTable();

            OBJDataTable = ObjProducao.RetornaListaPrazosProducao();
            OrdensServicoGridView.DataSource = OBJDataTable;
            OrdensServicoGridView.DataBind();
            OrdensServicoMultiView.Visible = true;
        }

        protected void ProducaoTextBox_TextChanged(object sender, EventArgs e)
        {
            ObjProducao.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaLabel")).Text);
            ObjProducao.IDGrupo = Convert.ToInt32(((Label)((Control)sender).FindControl("IDGrupoLabel")).Text);
            ObjProducao.PrazoProducao = Convert.ToInt32(((TextBox)((Control)sender).FindControl("ProducaoGridTextBox")).Text);

            ObjProducao.AtualizaPrazoProducao();

            CarregaDadosTela();
        }

        protected void ExpedicaoTextBox_TextChanged(object sender, EventArgs e)
        {
            ObjProducao.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaLabel")).Text);
            ObjProducao.IDGrupo = Convert.ToInt32(((Label)((Control)sender).FindControl("IDGrupoLabel")).Text);
            ObjProducao.PrazoExpedicao = Convert.ToInt32(((TextBox)((Control)sender).FindControl("ExpedicaoGridTextBox")).Text);

            ObjProducao.AtualizaPrazoProducao();

            CarregaDadosTela();
        }

        protected void ExcluirLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            ObjProducao.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaLabel")).Text);
            ObjProducao.IDGrupo = Convert.ToInt32(((Label)((Control)sender).FindControl("IDGrupoLabel")).Text);

            erro = ObjProducao.ExcluiPrazoProducao();
            ApresentaMensagem(erro);

            CarregaDadosTela();
        }

        protected void OrdensServicoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OrdensServicoGridView.PageIndex = e.NewPageIndex;
            CarregaDadosTela();
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/HomeProducaoWebForm.aspx?indmnu=3");
        }

        protected void NovoLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            erro = ValidaCamposPreenchidos();

            if (erro == "")
            {
                ObjProducao.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
                ObjProducao.IDGrupo = Convert.ToInt32(GrupoDropDownList.SelectedValue);
                ObjProducao.PrazoProducao = Convert.ToInt32(ProducaoTextBox.Text);
                ObjProducao.PrazoExpedicao = Convert.ToInt32(ExpedicaoTextBox.Text);

                erro = ObjProducao.GravaPrazoProducao();
            }

            ApresentaMensagem(erro);

            if (erro == "")
            {
                EmpresaDropDownList.SelectedValue = "";
                GrupoDropDownList.SelectedValue = "";
                ProducaoTextBox.Text = "";
                ExpedicaoTextBox.Text = "";

                CarregaDadosTela();
            }

        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosTela();
        }

        public void ApresentaMensagem(string erro)
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

        public string ValidaCamposPreenchidos()
        {
            string erro = "";

            if (EmpresaDropDownList.SelectedValue == "" || EmpresaDropDownList.SelectedValue == null)
            {
                erro = "Escolha uma empresa";
            }
            else if (GrupoDropDownList.SelectedValue == "" || GrupoDropDownList.SelectedValue == null)
            {
                erro = "Escolha uma grupo";
            }
            else if (ProducaoTextBox.Text == "" || ProducaoTextBox.Text == null)
            {
                erro = "Digite um prazo de produção";
            }
            else if (ExpedicaoTextBox.Text == "" || ExpedicaoTextBox.Text == null)
            {
                erro = "Digite um prazo de expedição";
            }

            return erro;
        }
    }
}