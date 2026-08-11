using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;

namespace VendasWeb.financeiro
{
    public partial class ListaRemessasWebForm : System.Web.UI.Page
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
                if (Session["OBJFinanceiro"] != null)
                {
                    //Descarega a session da Entidade
                    OBJFinanceiro = (FinanceiroClass)Session["OBJFinanceiro"];

                    //Carrega dados
                    //CarregaDadosNaTela();

                }
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            string teste = "";

        }

        protected void RemessasGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RemessasGridView.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        public void CarregaGrid()
        {

        }

        protected void SelecionarRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CheckBox = (CheckBox)sender;
            GridViewRow oldGridViewRow = (GridViewRow)CheckBox.NamingContainer;

            //Desmarca todos os check
            foreach (GridViewRow OldGridView in RemessasGridView.Rows)
            {
                //Seta todos como falso
                ((RadioButton)OldGridView.FindControl("SelecionarRadioButton")).Checked = false;
            }

            //marcando o RadioButton selecionado
            RadioButton RadioButton = (RadioButton)sender;
            GridViewRow GridViewRow = (GridViewRow)RadioButton.NamingContainer;
            ((RadioButton)GridViewRow.FindControl("SelecionarRadioButton")).Checked = true;
        }
    }
}