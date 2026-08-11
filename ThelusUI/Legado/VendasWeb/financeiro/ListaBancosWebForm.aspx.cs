using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.financeiro
{
    public partial class ListaBancosWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        FiltroClass ObjFiltroClass = new FiltroClass();
        FinanceiroClass OBJFinanceiro = new FinanceiroClass();
        UtilClass ObjUtilClass = new UtilClass();

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
                if (Session["ObjFiltroClass"] != null)
                {
                    ObjFiltroClass = (FiltroClass)Session["ObjFiltroClass"];
                }

                //Faz primeira carga
                CarregaGrid();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void BancosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BancosGridView.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void CarregaGrid()
        {
            OBJFinanceiro.Banco = BancoTextBox.Text;
            OBJFinanceiro.Agencia = AgenciaTextBox.Text;
            OBJFinanceiro.ContaCorrente = ContaCorrenteTextBox.Text;
            

            DataTable OBJDataTable = new DataTable();
            OBJDataTable = OBJFinanceiro.RecuperaBancosAgenciaContas();
            BancosGridView.DataSource = OBJDataTable;
            BancosGridView.DataBind();
            BancosMultiView.Visible = true;
        }

        protected void SelecionarRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CheckBox = (CheckBox)sender;
            GridViewRow oldGridViewRow = (GridViewRow)CheckBox.NamingContainer;

            //Desmarca todos os check
            foreach (GridViewRow OldGridView in BancosGridView.Rows)
            {
                //Seta todos como falso
                ((RadioButton)OldGridView.FindControl("SelecionarRadioButton")).Checked = false;
            }

            //marcando o RadioButton selecionado
            RadioButton RadioButton = (RadioButton)sender;
            GridViewRow GridViewRow = (GridViewRow)RadioButton.NamingContainer;
            ((RadioButton)GridViewRow.FindControl("SelecionarRadioButton")).Checked = true;

            OBJFinanceiro = new FinanceiroClass();
            OBJFinanceiro.IDBanco = Convert.ToInt32(((Label)((Control)sender).FindControl("IDBancoLabel")).Text);
            OBJFinanceiro.IDAgencia = Convert.ToInt32(((Label)((Control)sender).FindControl("IDAgenciaLabel")).Text);
            OBJFinanceiro.IDContaCorrente = Convert.ToInt32(((Label)((Control)sender).FindControl("IDContaCorrenteLabel")).Text);
            OBJFinanceiro.Banco = ((Label)((Control)sender).FindControl("NomeBancoLabel")).Text;
            OBJFinanceiro.Agencia = ((Label)((Control)sender).FindControl("NomeAgenciaLabel")).Text;
            OBJFinanceiro.ContaCorrente = ((Label)((Control)sender).FindControl("ContaLabel")).Text;

            Session["OBJFinanceiro"] = OBJFinanceiro;

            this.FinanceiroBancosWebUserControl.LiberaRemessasRetorno();
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/financeiro/HomeFinanceiroWebForm.aspx?indmnu=5");
        }
    }
}