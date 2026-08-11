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
    public partial class ListaMenusUsuariosWebForm : System.Web.UI.Page
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

            //Recupera objeto usuário da sessao do usuário
            if (Session["AdministrcaoUsuario"] != null)
            {
                OBJUsuario = (usuario)Session["AdministrcaoUsuario"];
            }

            if (!IsPostBack)
            {

                carregaDadosTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

        }

        public void carregaDadosTela()
        {
            OBJUsuario.CarregaDadosPrincipais();
            DataTable OBJDataTable = new DataTable();

            //Carrega Nome e Código do Usuário
            UsuarioTextBox.Text = OBJUsuario.CodigoUsuario;
            NomeUsuarioTextBox.Text = OBJUsuario.nome;

            OBJDataTable = OBJUsuario.RetornaMenusUsuario();
            MenusUsuariosGridView.DataSource = OBJDataTable;
            MenusUsuariosGridView.DataBind();
            MenusUsuariosMultiView.Visible = true;
        }

        protected void voltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroUsuarioWebForm.aspx?indmnu=2");
        }

        protected void AtivoUsuarioCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Atualiza Dados do usuário
            AtualizaDadosUsuario(sender, e);

            //Recarrega Tela
            carregaDadosTela();
        }

        public void AtualizaDadosUsuario(object sender, EventArgs e)
        {
            OBJUsuario.IDMenu = Convert.ToInt32(((Label)((Control)sender).FindControl("IDMenuLabel")).Text);
            OBJUsuario.Ativo = ((CheckBox)((Control)sender).FindControl("AtivoUsuarioCheckBox")).Checked;
            OBJUsuario.Administrador = ((CheckBox)((Control)sender).FindControl("AdministradorUsuarioCheckBox")).Checked;

            OBJUsuario.GravaMenusUsuario();

        }

        protected void AdministradorUsuarioCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Atualiza Dados do usuário
            AtualizaDadosUsuario(sender, e);

            //Recarrega Tela
            carregaDadosTela();
        }

        protected void MenusUsuariosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            MenusUsuariosGridView.PageIndex = e.NewPageIndex;
            carregaDadosTela();
        }
    }
}