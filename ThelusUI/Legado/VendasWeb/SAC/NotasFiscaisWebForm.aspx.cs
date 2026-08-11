using System;
using System.Web;
using System.Data;
using System.Linq;
using System.Web.UI;
using VendasWeb.Email;
using VendasWeb.classes;
using VendasWeb.usercontrol;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Collections.Generic;

namespace VendasWeb.SAC
{
    public partial class NotasFiscaisWebForm : System.Web.UI.Page
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
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();

            if (Session["TicketsDetalhe"] != null) ObjSAC = (SACClass)Session["TicketsDetalhe"];

            EmpresaDropDownList.SelectedValue = ObjSAC.IDEmpresa.ToString();
            ClienteTextBox.Text = ObjSAC.Cliente.ToString();
            TicketTextBox.Text = ObjSAC.IDTicket.ToString();

            EmpresaDropDownList.Enabled = false;
            ClienteTextBox.Enabled = false;
            TicketTextBox.Enabled = false;

            BuscarButton_Click(null, null);
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            if (Session["TicketsDetalhe"] != null)
                ObjSAC = (SACClass)Session["TicketsDetalhe"];

            ObjSAC.NumeroSerial = Convert.ToInt32(NotaFiscalTextBox.Text == "" ? "0" : NotaFiscalTextBox.Text);

            SACGridView.DataSource = ObjSAC.RetornaListaNotaFiscal();
            SACGridView.DataBind();
            SACMultiView.Visible = true;
        }

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            if (Session["TicketsDetalhe"] != null)
                ObjSAC = (SACClass)Session["TicketsDetalhe"];

            string erro = "";

            ObjSAC.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            try
            {
                ObjSAC.NumeroSerial = Convert.ToInt32(NotaFiscalTextBox.Text);
            }
            catch
            {
                erro = "O número serial deve ser um número.";
            }

            if (erro == "") erro = ObjSAC.RetornaDataFaturamentoNotaFiscal();

            if (erro == "") erro = ObjSAC.GravaNotaFiscal();

            if (erro == "")
            {
                NotaFiscalTextBox.Text = "";
                BuscarButton_Click(null, null);
            }

            ApresentaMensagem(erro);
        }

        protected void ExcluirLinkButton_Click(object sender, EventArgs e)
        {
            if (Session["TicketsDetalhe"] != null)
                ObjSAC = (SACClass)Session["TicketsDetalhe"];

            ObjSAC.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            ObjSAC.IDNota = Convert.ToInt32(((Label)((Control)sender).FindControl("IDNotaLabel")).Text);

            ObjSAC.NumeroSerial = Convert.ToInt32(((Label)((Control)sender).FindControl("NumeroSerialLabel")).Text);

            string erro = ObjSAC.ExcluiNotaFiscal();

            if (erro == "") BuscarButton_Click(null, null);

            ApresentaMensagem(erro);
        }

        protected void SACGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SACGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/TicketsDetalheWebForm.aspx?indmnu=3");
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