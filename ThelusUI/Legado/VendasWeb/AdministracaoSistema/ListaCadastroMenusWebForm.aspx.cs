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
    public partial class ListaCadastroMenusWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        menu objMenu = new menu();

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
                carregaDadosTela();
            }
            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            carregaDadosTela();
        }

        public void carregaDadosTela()
        {
            objMenu.Filtro = MenuTextBox.Text;
            objMenu.Status = StatusDropDownList.SelectedValue;
            DataTable OBJDataTable = new DataTable();

            OBJDataTable = objMenu.ListaMenus();
            MenusGridView.DataSource = OBJDataTable;
            MenusGridView.DataBind();
            MenusMultiView.Visible = true;
        }

        public void CarregaDadosDaTela(object sender, EventArgs e)
        {
            objMenu.IDMenu = Convert.ToInt32(((Label)((Control)sender).FindControl("IDMenuLabel")).Text);
            objMenu.Operacao = "alteracao";
            Session["AdministracaoMenu"] = objMenu;
        }

        protected void MenusGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            MenusGridView.PageIndex = e.NewPageIndex;
            carregaDadosTela();
        }

        protected void NovoMenuLinkButton_Click(object sender, EventArgs e)
        {
            objMenu.Operacao = "inclusao";
            Session["AdministracaoMenu"] = objMenu;
            Response.Redirect("~/AdministracaoSistema/CadastroMenuWebForm.aspx?indmnu=5");
        }

        protected void AcessarLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela(sender, e);
            Response.Redirect("~/AdministracaoSistema/CadastroMenuWebForm.aspx?indmnu=5");
        }

        protected void UsuariosLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela(sender, e);
            Response.Redirect("~/AdministracaoSistema/ListaUsuariosMenuWebForm.aspx?indmnu=5");
        }

        protected void GruposLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela(sender, e);
            Response.Redirect("~/AdministracaoSistema/ListaGruposMenuWebForm.aspx?indmnu=5");
        }
    }
}