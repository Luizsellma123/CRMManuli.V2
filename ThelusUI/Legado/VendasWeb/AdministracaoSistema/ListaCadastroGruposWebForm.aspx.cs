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
    public partial class ListaCadastroGruposWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
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
            objGrupo.Filtro = GrupoTextBox.Text;
            objGrupo.Status = StatusDropDownList.SelectedValue;
            DataTable OBJDataTable = new DataTable();

            OBJDataTable = objGrupo.ListaGrupos();
            GruposGridView.DataSource = OBJDataTable;
            GruposGridView.DataBind();
            GruposMultiView.Visible = true;
        }

        protected void GruposGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GruposGridView.PageIndex = e.NewPageIndex;
            carregaDadosTela();
        }

        protected void NovoGrupoLinkButton_Click(object sender, EventArgs e)
        {
            objGrupo.Operacao = "inclusao";
            Session["AdministracaoGrupo"] = objGrupo;
            Response.Redirect("~/AdministracaoSistema/CadastroGrupoWebForm.aspx?indmnu=5");
        }

        protected void AcessarLinkButton_Click(object sender, EventArgs e)
        {
            objGrupo.IDGrupo = Convert.ToInt32(((Label)((Control)sender).FindControl("IDGrupoLabel")).Text);
            objGrupo.Operacao = "alteracao";
            Session["AdministracaoGrupo"] = objGrupo;
            Response.Redirect("~/AdministracaoSistema/CadastroGrupoWebForm.aspx?indmnu=5");
        }

        protected void UsuariosLinkButton_Click(object sender, EventArgs e)
        {
            objGrupo.IDGrupo = Convert.ToInt32(((Label)((Control)sender).FindControl("IDGrupoLabel")).Text);
            objGrupo.Operacao = "alteracao";
            Session["AdministracaoGrupo"] = objGrupo;
            Response.Redirect("~/AdministracaoSistema/ListaUsuariosGruposWebForm.aspx?indmnu=5");
        }

        protected void MenusLinkButton_Click(object sender, EventArgs e)
        {
            objGrupo.IDGrupo = Convert.ToInt32(((Label)((Control)sender).FindControl("IDGrupoLabel")).Text);
            objGrupo.Operacao = "alteracao";
            Session["AdministracaoGrupo"] = objGrupo;
            Response.Redirect("~/AdministracaoSistema/ListaMenusGruposWebForm.aspx?indmnu=5");
        }


    }
}