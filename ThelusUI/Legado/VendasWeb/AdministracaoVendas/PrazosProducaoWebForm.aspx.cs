using System;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.AdministracaoVendas
{
    public partial class PrazosProducaoWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        producao ObjProducao = new producao();

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
                CarregaCombos();
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

        }

        protected void CarregaCombos()
        {
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.Items.Insert(0, new ListItem("Todas", ""));

            GrupoDropDownList.DataSource = ObjProducao.RetornaListaGruposProdutos();
            GrupoDropDownList.DataTextField = "Grupo";
            GrupoDropDownList.DataValueField = "IDGrupo";
            GrupoDropDownList.DataBind();
            GrupoDropDownList.Items.Insert(0, new ListItem("Todos", ""));
        }

        protected void OrdensServicoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OrdensServicoGridView.PageIndex = e.NewPageIndex;
            CarregaDadosNaTela();
        }

        protected void CarregaDadosNaTela()
        {

            if (EmpresaDropDownList.SelectedValue != null && EmpresaDropDownList.SelectedValue != "")
            {
                ObjProducao.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
            }
            else
            {
                ObjProducao.IDEmpresa = 0;
            }

            if (GrupoDropDownList.SelectedValue != null && GrupoDropDownList.SelectedValue != "")
            {
                ObjProducao.IDGrupo = Convert.ToInt32(GrupoDropDownList.SelectedValue);
            }
            else
            {
                ObjProducao.IDGrupo = 0;
            }

            DataTable OBJDataTable = new DataTable();

            OBJDataTable = ObjProducao.RetornaListaPrazosProducao();
            OrdensServicoGridView.DataSource = OBJDataTable;
            OrdensServicoGridView.DataBind();
            OrdensServicoMultiView.Visible = true;
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosNaTela();
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoVendas/HomeWebForm.aspx?indmnu=3");
        }
    }
}

