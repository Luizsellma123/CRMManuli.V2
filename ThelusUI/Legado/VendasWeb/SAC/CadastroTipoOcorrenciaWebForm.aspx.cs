using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.SAC
{
    public partial class CadastroTipoOcorrenciaWebForm : System.Web.UI.Page
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
                BuscarButton_Click(null, null);
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            ObjSAC.TipoOcorrencia = TipoOcorrenciaTextBox.Text;

            SACGridView.DataSource = ObjSAC.RetornaListaTipoOcorrencia();
            SACGridView.DataBind();
            SACMultiView.Visible = true;
        }

        protected void NovoTipoLinkButton_Click(object sender, EventArgs e)
        {
            Session["CadastroTipoOcorrencia"] = null;
            Response.Redirect("~/SAC/CadastroTipoOcorrenciaDetalheWebForm.aspx?indmnu=5");
        }

        protected void EditarLinkButton_Click(object sender, EventArgs e)
        {
            ObjSAC.Operacao = "Alteracao";
            ObjSAC.IDTipoOcorrencia = Convert.ToInt32(((Label)((Control)sender).FindControl("CodigoLabel")).Text);
            Session["CadastroTipoOcorrencia"] = ObjSAC;
            Response.Redirect("~/SAC/CadastroTipoOcorrenciaDetalheWebForm.aspx?indmnu=5");
        }

        protected void PadraoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ObjSAC.IDTipoOcorrencia = Convert.ToInt32(((Label)((Control)sender).FindControl("CodigoLabel")).Text);
            ObjSAC.Padrao = ((CheckBox)((Control)sender).FindControl("PadraoCheckBox")).Checked;
            if (Session["IDUsuario"] != null) ObjSAC.IDUsuario = Convert.ToInt32(Session["IDUsuario"].ToString());

            ApresentaMensagem(ObjSAC.GravaTipoOcorrenciaPadrao());

            BuscarButton_Click(null, null);
        }

        protected void SACGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SACGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/HomeSACWebForm.aspx?indmnu=5");
        }

        protected void ApresentaMensagem(string erro = "")
        {
            if (erro != "")
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                erro = "Operação realizada com sucesso.";
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

    }
}