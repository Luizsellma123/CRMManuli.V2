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
    public partial class ListaMenusGruposWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        grupos objGrupo = new grupos();
        menu objMenu = new menu();

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

            //Recupera objeto usuário da sessao do usuário
            if (Session["AdministracaoGrupo"] != null)
            {
                objGrupo = (grupos)Session["AdministracaoGrupo"];
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
            objGrupo.CarregaDadosPrincipais();
            DataTable OBJDataTable = new DataTable();

            //Carrega Nome e Código do grupo
            CodigoGrupoTextBox.Text = objGrupo.Nome.ToString();
            StatusTextBox.Text = objGrupo.Status.ToString();

            OBJDataTable = objGrupo.RetornaMenusGrupo();
            MenusGruposGridView.DataSource = OBJDataTable;
            MenusGruposGridView.DataBind();
            MenusGruposMultiView.Visible = true;
        }

        public void CarregaCombos()
        {
            MenuDropDownList.DataSource = objMenu.RetornaMenus();
            MenuDropDownList.DataTextField = "Nome";
            MenuDropDownList.DataValueField = "IDMenu";
            MenuDropDownList.DataBind();
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            DataTable OBJDataTable = new DataTable();

            objGrupo.Filtro = MenuDropDownList.SelectedValue;

            OBJDataTable = objGrupo.ListaMenusGrupos();
            MenusGruposGridView.DataSource = OBJDataTable;
            MenusGruposGridView.DataBind();
            MenusGruposMultiView.Visible = true;
        }

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            SalvarMenu();
        }

        public void SalvarMenu()
        {
            string erro = "";

            if (Session["AdministracaoGrupo"] != null)
            {
                objGrupo = (grupos)Session["AdministracaoGrupo"];
            }

            objGrupo.IDMenu = Convert.ToInt32(MenuDropDownList.SelectedValue);
            erro = objGrupo.AdicionaMenuGrupo();

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
            ExcluiMenu(sender, e);
        }

        public void ExcluiMenu(object sender, EventArgs e)
        {
            if (Session["AdministracaoGrupo"] != null)
            {
                objGrupo = (grupos)Session["AdministracaoGrupo"];
            }

            objGrupo.IDMenu = Convert.ToInt32(((Label)((Control)sender).FindControl("IDMenuLabel")).Text);
            objGrupo.ExcluiMenuGrupo();

            carregaDadosTela();
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroGrupoWebForm.aspx?indmnu=2");
        }
    }
}