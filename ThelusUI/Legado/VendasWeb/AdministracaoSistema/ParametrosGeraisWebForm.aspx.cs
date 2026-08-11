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
    public partial class ParametrosGeraisWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        usuario ObjUsuario = new usuario();
        ParametroGeral ObjParametroGeral = new ParametroGeral();

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
                CarregaCombos();
                CarregaDadosTela();
            }
            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        public void CarregaCombos()
        {
            EmpresaDropDownList.DataSource = ObjUsuario.RetornaEmpresas();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.Items.Insert(0, new ListItem("Todas", "0"));
        }

        public void CarregaDadosTela()
        {
            ObjParametroGeral.Filtro = ParametroTextBox.Text;
            ObjParametroGeral.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedItem.Value);

            DataTable OBJDataTable = new DataTable();

            OBJDataTable = ObjParametroGeral.RetornaListaParametrosGerais();
            ParametrosGeraisGridView.DataSource = OBJDataTable;
            ParametrosGeraisGridView.DataBind();
            ParametrosGeraisMultiView.Visible = true;
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosTela();
        }

        protected void NovoLinkButton_Click(object sender, EventArgs e)
        {
            ObjParametroGeral.Operacao = "inclusao";
            Session["ParametroGeral"] = ObjParametroGeral;
            Response.Redirect("~/AdministracaoSistema/CadastroParametroGeralWebForm.aspx?indmnu=5");
        }

        protected void SelLinkButton_Click(object sender, EventArgs e)
        {
            ObjParametroGeral.IDParametro = Convert.ToInt32(((Label)((Control)sender).FindControl("IDParametroLabel")).Text);
            ObjParametroGeral.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaLabel")).Text);
            ObjParametroGeral.NomeParametro = ((Label)((Control)sender).FindControl("NomeLabel")).Text;
            ObjParametroGeral.IDModulo = Convert.ToInt32(((Label)((Control)sender).FindControl("IDModuloLabel")).Text);
            ObjParametroGeral.DescricaoParametro = ((Label)((Control)sender).FindControl("DescricaoLabel")).Text;
            ObjParametroGeral.ValorTexto = ((Label)((Control)sender).FindControl("ValorTextoLabel")).Text;
            ObjParametroGeral.ValorNumerico = Convert.ToDecimal(((Label)((Control)sender).FindControl("ValorNumericoLabel")).Text);

            ObjParametroGeral.Operacao = "alteracao";
            Session["ParametroGeral"] = ObjParametroGeral;
            Response.Redirect("~/AdministracaoSistema/CadastroParametroGeralWebForm.aspx?indmnu=5");
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoSistema/AdministracaoHomeWebForm.aspx?indmnu=5");
        }

        protected void ParametrosGeraisGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ParametrosGeraisGridView.PageIndex = e.NewPageIndex;
            CarregaDadosTela();
        }


    }
}