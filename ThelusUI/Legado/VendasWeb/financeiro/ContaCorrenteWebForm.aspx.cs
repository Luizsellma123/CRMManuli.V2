using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;

namespace VendasWeb.financeiro
{
    public partial class ContaCorrenteWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        ClienteClasse OBJClienteClasse = new ClienteClasse();

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
                BuscarButton_Click(sender, e);
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            if (FiltroDropDownList.SelectedValue == "1")
            {
                OBJClienteClasse.RazaoSocial = FiltroTextBox.Text.ToString();
            }
            else if (FiltroDropDownList.SelectedValue == "2")
            {
                OBJClienteClasse.CodigoCliente = FiltroTextBox.Text.ToString();
            }
            else if (FiltroDropDownList.SelectedValue == "3")
            {
                OBJClienteClasse.NomeFantasia = FiltroTextBox.Text.ToString();
            }
            else if (FiltroDropDownList.SelectedValue == "4")
            {
                OBJClienteClasse.CNPJCliente = FiltroTextBox.Text.ToString();
            }

            DataTable OBJDataTable = new DataTable();
            OBJDataTable = OBJClienteClasse.RecuperaContaCorrenteClienteSAP();
            ContaCorrenteGridView.DataSource = OBJDataTable;
            ContaCorrenteGridView.DataBind();
            ContaCorrenteMultiView.Visible = true;
        }

        protected void AcessarLinkButton_Click(object sender, EventArgs e)
        {
            Session["ContaCorrente"] = null;
            Session["ContaCorrenteDetalhe"] = null;
            Session["ContaCorrenteReturn"] = "~/financeiro/ContaCorrenteWebForm.aspx?indmnu=5";

            OBJClienteClasse.VendedorCliente = ((Label)((Control)sender).FindControl("VendedorLabel")).Text;
            OBJClienteClasse.CodigoCliente = ((Label)((Control)sender).FindControl("CodigoLabel")).Text;
            OBJClienteClasse.CodigoAux = ((Label)((Control)sender).FindControl("CodigoLabel")).Text;
            OBJClienteClasse.NomeCliente = ((Label)((Control)sender).FindControl("NomeLabel")).Text;
            OBJClienteClasse.CNPJCliente = ((Label)((Control)sender).FindControl("CNPJouCPFLabel")).Text;
            OBJClienteClasse.LimiteCredito = Convert.ToDecimal(((Label)((Control)sender).FindControl("LimiteLabel")).Text.Replace("R$", ""));

            Session["ContaCorrente"] = OBJClienteClasse;
            Response.Redirect("~/financeiro/ContaCorrenteDetalheWebForm.aspx?indmnu=5");
        }

        protected void ContaCorrenteGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ContaCorrenteGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }

    }
}