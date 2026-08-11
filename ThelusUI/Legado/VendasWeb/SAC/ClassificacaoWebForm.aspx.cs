using System;
using System.Web;
using System.Data;
using System.Linq;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Collections.Generic;

namespace VendasWeb.SAC
{
    public partial class ClassificacaoWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        SACClass ObjSAC = new SACClass();

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
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            ObjSAC.Tela = "Lista";
            ObjSAC.Filtro = ClassificacaoTextBox.Text;

            SACGridView.DataSource = ObjSAC.RetornaListaClassificacao();
            SACGridView.DataBind();
            SACMultiView.Visible = true;
        }

        protected void PadraoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Session["IDUsuario"] != null)
            {
                ObjSAC.IDUsuario = Convert.ToInt32(Session["IDUsuario"].ToString());
            }

            ObjSAC.IDClassificacao = Convert.ToInt32(((Label)((Control)sender).FindControl("CodigoLabel")).Text);
            ObjSAC.Padrao = ((CheckBox)((Control)sender).FindControl("PadraoCheckBox")).Checked;

            ObjSAC.AlteraClassificacaoPadrao();

            CarregaDadosNaTela();
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosNaTela();
        }

        protected void NovoLinkButton_Click(object sender, EventArgs e)
        {
            ObjSAC.Operacao = "Inclusao";
            Session["ClassificacaoDetalhe"] = ObjSAC;
            Response.Redirect("~/SAC/ClassificacaoDetalheWebForm.aspx?indmnu=5");
        }

        protected void EditarLinkButton_Click(object sender, EventArgs e)
        {
            ObjSAC.Operacao = "Alteracao";
            ObjSAC.IDClassificacao = Convert.ToInt32(((Label)((Control)sender).FindControl("CodigoLabel")).Text);
            Session["ClassificacaoDetalhe"] = ObjSAC;
            Response.Redirect("~/SAC/ClassificacaoDetalheWebForm.aspx?indmnu=5");
        }

        protected void SACGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SACGridView.PageIndex = e.NewPageIndex;
            CarregaDadosNaTela();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/HomeSACWebForm.aspx?indmnu=5");
        }

    }
}