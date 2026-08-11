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
    public partial class ListaCadastroUsuarioWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        usuario OBJUsuario = new usuario();

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
            OBJUsuario.Filtro = UsuarioTextBox.Text;
            OBJUsuario.Status = StatusDropDownList.SelectedValue;
            DataTable OBJDataTable = new DataTable();

            OBJDataTable = OBJUsuario.ListaUsuarios();
            UsuariosGridView.DataSource = OBJDataTable;
            UsuariosGridView.DataBind();
            UsuariosMultiView.Visible = true;
        }

        protected void UsuariosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UsuariosGridView.PageIndex = e.NewPageIndex;
            carregaDadosTela();
        }

        protected void NovoUsuarioLinkButton_Click(object sender, EventArgs e)
        {
            OBJUsuario.Operacao = "inclusao";
            Session["AdministrcaoUsuario"] = OBJUsuario;
            Response.Redirect("~/AdministracaoSistema/CadastroUsuarioWebForm.aspx?indmnu=5");
        }

        protected void AcessarLinkButton_Click(object sender, EventArgs e)
        {
            OBJUsuario.CodigoUsuario = ((Label)((Control)sender).FindControl("CodigoUsuarioLabel")).Text;
            OBJUsuario.Operacao = "alteracao";
            Session["AdministrcaoUsuario"] = OBJUsuario;
            Response.Redirect("~/AdministracaoSistema/CadastroUsuarioWebForm.aspx?indmnu=5");
        }

        protected void GruposLinkButton_Click(object sender, EventArgs e)
        {
            OBJUsuario.CodigoUsuario = ((Label)((Control)sender).FindControl("CodigoUsuarioLabel")).Text;
            OBJUsuario.Operacao = "alteracao";
            Session["AdministrcaoUsuario"] = OBJUsuario;
            Response.Redirect("~/AdministracaoSistema/ListaGruposUsuariosWebForm.aspx?indmnu=5");
        }

        protected void MenusLinkButton_Click(object sender, EventArgs e)
        {
            OBJUsuario.CodigoUsuario = ((Label)((Control)sender).FindControl("CodigoUsuarioLabel")).Text;
            OBJUsuario.Operacao = "alteracao";
            Session["AdministrcaoUsuario"] = OBJUsuario;
            Response.Redirect("~/AdministracaoSistema/ListaMenusUsuariosWebForm.aspx?indmnu=5");
        }
    }
}