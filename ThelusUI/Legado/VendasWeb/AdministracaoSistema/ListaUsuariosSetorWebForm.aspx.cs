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
    public partial class ListaUsuariosSetorWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        setor objSetor = new setor();
        usuario objUsuario = new usuario();

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
            if (Session["AdministracaoSetor"] != null)
            {
                objSetor = (setor)Session["AdministracaoSetor"];
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
            objSetor.CarregaDadosPrincipais();

            DataTable OBJDataTable = new DataTable();

            IDSetorTextBox.Text = objSetor.Nome.ToString();

            if (objSetor.Status == "1")
            {
                StatusTextBox.Text = "Ativo";
            }
            else
            {
                StatusTextBox.Text = "Desligado";
            }

            OBJDataTable = objSetor.RetornaUsuariosSetor();
            UsuariosSetorGridView.DataSource = OBJDataTable;
            UsuariosSetorGridView.DataBind();
            UsuariosSetorMultiView.Visible = true;
        }

        public void CarregaCombos()
        {
            UsuarioDropDownList.DataSource = objUsuario.RetornaUsuarios();
            UsuarioDropDownList.DataTextField = "Nome";
            UsuarioDropDownList.DataValueField = "IDUsuario";
            UsuarioDropDownList.DataBind();
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            DataTable OBJDataTable = new DataTable();

            objSetor.Filtro = UsuarioDropDownList.SelectedValue;

            OBJDataTable = objSetor.ListaUsuariosSetor();
            UsuariosSetorGridView.DataSource = OBJDataTable;
            UsuariosSetorGridView.DataBind();
            UsuariosSetorMultiView.Visible = true;
        }

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            SalvarUsuario();
        }

        public void SalvarUsuario()
        {
            string erro = "";

            if (Session["AdministracaoSetor"] != null)
            {
                objSetor = (setor)Session["AdministracaoSetor"];
            }

            objSetor.IDUsuario = Convert.ToInt32(UsuarioDropDownList.SelectedValue);
            erro = objSetor.AdicionaUsuariosSetor();

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
                Session["Msg"] = "Usuário incluido com sucesso.";
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }
        }

        protected void ExcluirLinkButton_Click(object sender, EventArgs e)
        {
            ExcluiUsuario(sender, e);
        }

        public void ExcluiUsuario(object sender, EventArgs e)
        {
            if (Session["AdministracaoSetor"] != null)
            {
                objSetor = (setor)Session["AdministracaoSetor"];
            }

            objSetor.IDUsuario = Convert.ToInt32(((Label)((Control)sender).FindControl("IDUsuarioLabel")).Text);
            objSetor.ExcluiUsuariosSetor();

            carregaDadosTela();                      
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroSetorWebForm.aspx?indmnu=2");
        }

        protected void AdministradorUsuarioCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Atualiza Dados do usuário
            AtualizaDadosUsuario(sender, e);

            //Recarrega Tela
            carregaDadosTela();
        }

        public void AtualizaDadosUsuario(object sender, EventArgs e)
        {
            objUsuario.IDSetor = objSetor.IDSetor;
            objUsuario.IDUsuario = Convert.ToInt32(((Label)((Control)sender).FindControl("IDUsuarioLabel")).Text);
            objUsuario.Administrador = ((CheckBox)((Control)sender).FindControl("AdministradorUsuarioCheckBox")).Checked;

            objUsuario.GravaSetoresUsuario();

        }

    }
}