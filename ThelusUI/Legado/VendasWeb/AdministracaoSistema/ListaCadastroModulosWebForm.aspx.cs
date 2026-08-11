using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.AdministracaoSistema
{
    public partial class ListaCadastroModulosWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        modulo ObjModulo = new modulo();

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
                CarregaDadosTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        public void CarregaDadosTela()
        {
            ObjModulo.Filtro = ModuloTextBox.Text;

            DataTable OBJDataTable = new DataTable();

            OBJDataTable = ObjModulo.RetornaModulos();
            ModulosGridView.DataSource = OBJDataTable;
            ModulosGridView.DataBind();
            ModulosMultiView.Visible = true;
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosTela();
        }

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            ObjModulo.Nome = ModuloTextBox.Text;

            if (ModuloTextBox.Text == "" || ModuloTextBox.Text == null)
            {
                erro = "Escreva o nome do módulo";
            }

            if (erro == "")
            {
                erro = ObjModulo.AdicionaModulo();
            }

            if (erro != "")
            {
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(erro.ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Dados gravados com sucesso.", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                ModuloTextBox.Text = "";

                CarregaDadosTela();
            }

        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroGrupoWebForm.aspx?indmnu=2");
        }

        protected void ExcluirLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            ObjModulo.Codigo = ((Label)((Control)sender).FindControl("CodigoLabel")).Text;

            erro = ObjModulo.ExcluiModulo();

            if (erro != "")
            {
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(erro.ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                ModuloTextBox.Text = "";

                CarregaDadosTela();
            }
        }
    }
}