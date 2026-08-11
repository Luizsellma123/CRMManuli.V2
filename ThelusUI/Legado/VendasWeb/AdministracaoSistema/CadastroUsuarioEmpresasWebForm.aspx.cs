using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.AdministracaoSistema
{
    public partial class CadastroUsuarioEmpresasWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        usuario OBJUsuario = new usuario();

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

            //Recupera objeto usuário da sessao do usuário
            if (Session["AdministrcaoUsuario"] != null)
            {
                OBJUsuario = (usuario)Session["AdministrcaoUsuario"];
            }

            if (!IsPostBack)
            {
                //Carrega dados na tela
                carregaDadosTela();
                CarregaCombos();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        public void carregaDadosTela()
        {
            OBJUsuario.CarregaDadosPrincipais();

            DataTable OBJDataTable = new DataTable();

            //Carrega Nome e Código do Usuário
            CodigoUsuarioTextBox.Text = OBJUsuario.CodigoUsuario.ToString();
            StatusTextBox.Text = OBJUsuario.Status.ToString();

            OBJDataTable = OBJUsuario.RetornaEmpresasUsuario();
            EmpresasUsuarioGridView.DataSource = OBJDataTable;
            EmpresasUsuarioGridView.DataBind();
            EmpresasUsuarioMultiView.Visible = true;
        }

        public void CarregaCombos()
        {
            EmpresasDropDownList.DataSource = OBJUsuario.RetornaEmpresas();
            EmpresasDropDownList.DataTextField = "NomeEmpresa";
            EmpresasDropDownList.DataValueField = "IDEmpresa";
            EmpresasDropDownList.DataBind();
        }

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            SalvarEmpresa();
        }

        public void SalvarEmpresa()
        {
            string erro = "";

            if (Session["AdministrcaoUsuario"] != null)
            {
                OBJUsuario = (usuario)Session["AdministrcaoUsuario"];
            }

            OBJUsuario.IDEmpresa = Convert.ToInt32(EmpresasDropDownList.SelectedValue);
            erro = OBJUsuario.AdicionaEmpresasUsuario();

            carregaDadosTela();

            if (erro != "")
            {
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(erro.ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                Session["Msg"] = "Dados gravados com sucesso.";
            }
        }

        protected void ExcluirLinkButton_Click(object sender, EventArgs e)
        {
            ExcluiEmpresa(sender, e);
        }

        public void ExcluiEmpresa(object sender, EventArgs e)
        {
            if (Session["AdministrcaoUsuario"] != null)
            {
                OBJUsuario = (usuario)Session["AdministrcaoUsuario"];
            }

            OBJUsuario.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaLabel")).Text);
            OBJUsuario.ExcluiEmpresasUsuario();

            carregaDadosTela();
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroUsuarioWebForm.aspx?indmnu=2");
        }

        protected void EmpresasUsuarioGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EmpresasUsuarioGridView.PageIndex = e.NewPageIndex;
            carregaDadosTela();
        }
    }
}