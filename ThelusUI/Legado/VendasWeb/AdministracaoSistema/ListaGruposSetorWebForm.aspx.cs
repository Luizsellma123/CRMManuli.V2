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
    public partial class ListaGruposSetorWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        setor objSetor = new setor();
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

            OBJDataTable = objSetor.RetornaGruposSetor();
            GruposSetorGridView.DataSource = OBJDataTable;
            GruposSetorGridView.DataBind();
            GruposSetorMultiView.Visible = true;
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

            objSetor.Filtro = GrupoDropDownList.SelectedValue;

            OBJDataTable = objSetor.ListaGruposSetor();
            GruposSetorGridView.DataSource = OBJDataTable;
            GruposSetorGridView.DataBind();
            GruposSetorMultiView.Visible = true;
        }

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            SalvarGrupo();
        }

        public void SalvarGrupo()
        {
            string erro = "";

            if (Session["AdministracaoSetor"] != null)
            {
                objSetor = (setor)Session["AdministracaoSetor"];
            }

            objSetor.IDGrupo = Convert.ToInt32(GrupoDropDownList.SelectedValue);
            erro = objSetor.AdicionaGruposSetor();

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
            if (Session["AdministracaoSetor"] != null)
            {
                objSetor = (setor)Session["AdministracaoSetor"];
            }

            objSetor.IDGrupo = Convert.ToInt32(((Label)((Control)sender).FindControl("IDGrupoLabel")).Text);
            objSetor.ExcluiGruposSetor();

            carregaDadosTela();
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroSetorWebForm.aspx?indmnu=2");
        }
    }
}