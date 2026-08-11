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
    public partial class ListaUsuariosGruposWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        grupos objGrupo = new grupos();
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

            CodigoGrupoTextBox.Text = objGrupo.Nome.ToString();
            StatusTextBox.Text = objGrupo.Status.ToString();

            OBJDataTable = objGrupo.RetornaUsuariosGrupo();
            UsuariosGruposGridView.DataSource = OBJDataTable;
            UsuariosGruposGridView.DataBind();
            UsuariosGruposMultiView.Visible = true;
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

            objGrupo.Filtro = UsuarioDropDownList.SelectedValue;

            OBJDataTable = objGrupo.ListaUsuariosGrupos();
            UsuariosGruposGridView.DataSource = OBJDataTable;
            UsuariosGruposGridView.DataBind();
            UsuariosGruposMultiView.Visible = true;

        }

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            SalvarUsuario();
        }

        public void SalvarUsuario()
        {
            string erro = "";

            if (Session["AdministracaoGrupo"] != null)
            {
                objGrupo = (grupos)Session["AdministracaoGrupo"];
            }

            objGrupo.IDUsuario = Convert.ToInt32(UsuarioDropDownList.SelectedValue);
            erro = objGrupo.AdicionaUsuariosGrupo();

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
            ExcluiUsuario(sender, e);
        }

        public void ExcluiUsuario(object sender, EventArgs e)
        {
            if (Session["AdministracaoGrupo"] != null)
            {
                objGrupo = (grupos)Session["AdministracaoGrupo"];
            }

            objGrupo.IDUsuario = Convert.ToInt32(((Label)((Control)sender).FindControl("IDUsuarioLabel")).Text);
            objGrupo.ExcluiUsuariosGrupo();

            carregaDadosTela();
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroGrupoWebForm.aspx?indmnu=2");
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
            objUsuario.IDGrupo = objGrupo.IDGrupo;
            objUsuario.IDUsuario = Convert.ToInt32(((Label)((Control)sender).FindControl("IDUsuarioLabel")).Text);
            objUsuario.Administrador = ((CheckBox)((Control)sender).FindControl("AdministradorUsuarioCheckBox")).Checked;

            objUsuario.GravaUsuariosGrupo();

        }

        protected void UsuariosGruposGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UsuariosGruposGridView.PageIndex = e.NewPageIndex;
            carregaDadosTela();
        }
    }
}