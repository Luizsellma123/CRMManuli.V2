using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace VendasWeb.AdministracaoSistema
{
    public partial class ListaGruposMenuWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        menu objMenu = new menu();
        grupos objGrupo = new grupos();

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

            //Recupera objeto grupo da sessao do usuário
            if (Session["AdministracaoMenu"] != null)
            {
                objMenu = (menu)Session["AdministracaoMenu"];
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
            objMenu.CarregaDadosPrincipais();

            DataTable OBJDataTable = new DataTable();

            CodigoMenuTextBox.Text = objMenu.Nome.ToString();

            if (objMenu.Status == "1")
            {
                StatusTextBox.Text = "Ativo";
            }
            else
            {
                StatusTextBox.Text = "Desligado";
            }

            OBJDataTable = objMenu.RetornaGruposMenu();
            GruposMenusGridView.DataSource = OBJDataTable;
            GruposMenusGridView.DataBind();
            GruposMenusMultiView.Visible = true;
        }

        public void CarregaCombos()
        {
            GrupoDropDownList.DataSource = objGrupo.RetornaGrupos();
            GrupoDropDownList.DataTextField = "Nome";
            GrupoDropDownList.DataValueField = "IDGrupo";
            GrupoDropDownList.DataBind();
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            DataTable OBJDataTable = new DataTable();

            objMenu.Filtro = GrupoDropDownList.SelectedValue;

            OBJDataTable = objMenu.ListaGruposMenus();
            GruposMenusGridView.DataSource = OBJDataTable;
            GruposMenusGridView.DataBind();
            GruposMenusMultiView.Visible = true;
        }

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            SalvarGrupo();
        }

        public void SalvarGrupo()
        {
            string erro = "";

            if (Session["AdministracaoMenu"] != null)
            {
                objMenu = (menu)Session["AdministracaoMenu"];
            }

            objMenu.IDGrupo = Convert.ToInt32(GrupoDropDownList.SelectedValue);
            erro = objMenu.AdicionaGruposMenu();

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
            ExcluiGrupo(sender, e);
        }

        public void ExcluiGrupo(object sender, EventArgs e)
        {
            if (Session["AdministracaoMenu"] != null)
            {
                objMenu = (menu)Session["AdministracaoMenu"];
            }

            objMenu.IDGrupo = Convert.ToInt32(((Label)((Control)sender).FindControl("IDGrupoLabel")).Text);
            objMenu.ExcluiGruposMenu();

            carregaDadosTela();
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroMenuWebForm.aspx?indmnu=2");
        }

    }
}