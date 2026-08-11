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
    public partial class ListaCadastroSetoresWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        setor objSetor = new setor();

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
            objSetor.Filtro = NomeSetorTextBox.Text;
            objSetor.Status = StatusDropDownList.SelectedValue;
            DataTable OBJDataTable = new DataTable();

            OBJDataTable = objSetor.ListaSetores();
            SetoresGridView.DataSource = OBJDataTable;
            SetoresGridView.DataBind();
            SetoresMultiView.Visible = true;
        }

        public void CarregaDadosDaTela(object sender, EventArgs e)
        {
            objSetor.IDSetor = Convert.ToInt32(((Label)((Control)sender).FindControl("IDSetorLabel")).Text);
            objSetor.Operacao = "alteracao";
            Session["AdministracaoSetor"] = objSetor;
        }

        protected void SetoresGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SetoresGridView.PageIndex = e.NewPageIndex;
            carregaDadosTela();
        }

        protected void NovoSetorLinkButton_Click(object sender, EventArgs e)
        {
            objSetor.Operacao = "inclusao";
            Session["AdministracaoSetor"] = objSetor;
            Response.Redirect("~/AdministracaoSistema/CadastroSetorWebForm.aspx?indmnu=5");
        }

        protected void AcessarLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela(sender, e);
            Response.Redirect("~/AdministracaoSistema/CadastroSetorWebForm.aspx?indmnu=5");
        }

        protected void UsuariosLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela(sender, e);
            Response.Redirect("~/AdministracaoSistema/ListaUsuariosSetorWebForm.aspx?indmnu=5");
        }

        protected void GruposLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela(sender, e);
            Response.Redirect("~/AdministracaoSistema/ListaGruposSetorWebForm.aspx?indmnu=5");
        }

    }
}