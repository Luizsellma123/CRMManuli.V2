using System;
using System.Collections.Generic;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;

namespace VendasWeb.Entidades
{
    public partial class TabelaPrecoWebForm : System.Web.UI.Page
    {
        funcoes mdlFuncoes = new funcoes();
        SessionClass OBJSessao = new SessionClass();
        RelatorioCrystalClass OBJRelatorioCrystal = new RelatorioCrystalClass();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Session["reportDocument"] = null;
            }
            else
            {
                if (Session["reportDocument"] != null)
                {
                    CrystalReportViewer1.ReportSource = Session["reportDocument"];
                    CrystalReportViewer1.DataBind();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                //EmpresaGridView.DataSource = mdlFuncoes.Consulta_Empresa(Session["usuario"].ToString());
                //EmpresaGridView.DataBind();

                TabelaDropDownList.DataSource = mdlFuncoes.Consulta_Tabela_Preco();
                TabelaDropDownList.DataValueField = "IDTabela";
                TabelaDropDownList.DataTextField = "Nome";
                TabelaDropDownList.DataBind();

                TabelaDropDownList.Items.Insert(0, new ListItem("Selecione", ""));
                TabelaDropDownList.Focus();
            }
        }

        protected void RelatorioTabelaButton_Click(object sender, EventArgs e)
        {
            //Zera session para não dar problema
            Session["reportDocument"] = null;

            OBJRelatorioCrystal.GeraRelatorioTabelaPrecoTela(Convert.ToInt32(TabelaDropDownList.SelectedValue.ToString()), Session["usuario"].ToString(), "TabelaPreco", ref CrystalReportViewer1);

            //Atribui o report para a Session
            Session["reportDocument"] = OBJRelatorioCrystal.MyReport;
        }

        protected void LinkButton_Click(object sender, EventArgs e)
        {
            OBJRelatorioCrystal.GeraRelatorioTabelaPrecoArquivo(Convert.ToInt32(TabelaDropDownList.SelectedValue.ToString()), TabelaDropDownList.SelectedItem.Text.ToString(), ref CrystalReportViewer1,"PD");
        }
    }
}