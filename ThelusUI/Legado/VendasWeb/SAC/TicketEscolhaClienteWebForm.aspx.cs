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
    public partial class TicketEscolhaClienteWebForm : System.Web.UI.Page
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
            ObjSAC.TipoFiltro = FiltroDropDownList.SelectedItem.Text ?? ""; //se for nulo atribui branco
            CarregaClienteDaTela();

            SACGridView.DataSource = ObjSAC.RetornaListaClientes();
            SACGridView.DataBind();
            SACMultiView.Visible = true;
        }

        protected void SelLinkButton_Click(object sender, EventArgs e)
        {
            if (Session["TicketsDetalhe"] != null)
            {
                ObjSAC = (SACClass)Session["TicketsDetalhe"];
            }

            ObjSAC.Operacao = "Inclusao";
            ObjSAC.Cliente = ((Label)((Control)sender).FindControl("NomeLabel")).Text;
            ObjSAC.IDCliente = Convert.ToInt32(((Label)((Control)sender).FindControl("IDClienteLabel")).Text);
            ObjSAC.CodigoCliente = ((Label)((Control)sender).FindControl("CodigoLabel")).Text;
            Session["TicketsDetalhe"] = ObjSAC;
            Response.Redirect("~/SAC/TicketsDetalheWebForm.aspx?indmnu=5");
        }

        protected void SACGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SACGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/TicketsWebForm.aspx?indmnu=5");
        }

        protected void CarregaClienteDaTela()
        {
            string erro = "erro";

            ObjSAC.Filtro = "";

            if (FiltroDropDownList.SelectedItem.Text == "CNPJ")
            {
                string ClienteAux = ProcurarTextBox.Text;

                ClienteAux = ClienteAux.Replace("/", "");

                ClienteAux = ClienteAux.Replace(".", "");

                ClienteAux = ClienteAux.Replace("-", "");

                if (ClienteAux.Length == 14)
                {
                    try
                    {
                        erro = "";
                        UInt64 teste = Convert.ToUInt64(ClienteAux);
                        ObjSAC.Filtro = ObjUtilClass.FormataCNPJCPF(ClienteAux);
                    }
                    catch
                    {
                        erro = "erro";
                    }
                }
            }

            if (erro == "erro")
            {
                ObjSAC.Filtro = ProcurarTextBox.Text;
            }

        }

    }
}