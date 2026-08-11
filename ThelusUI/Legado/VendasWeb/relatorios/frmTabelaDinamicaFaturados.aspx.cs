using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting;
using Microsoft.Reporting.WebForms;

namespace VendasWeb.relatorios
{
    public partial class frmTabelaDinamicaFaturados : System.Web.UI.Page
    {
        clasRelatorios mdlRelatorios = new clasRelatorios();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                ReportDataSource rptDados = new ReportDataSource();
                DataTable dadosTable = new DataTable();

                //Função para criar relatórios
                dadosTable = mdlRelatorios.relatorioTabelaDinamicaFaturados(Session["empresa"].ToString(), Session["natureza"].ToString(), Session["produto"].ToString(),
                    Session["linha"].ToString(), Session["dataInicial"].ToString(), Session["datafinal"].ToString(), Session["entidade"].ToString(),
                    Session["subFamilia"].ToString());

                //Preenche ReportDataSource
                rptDados.Name = "dtTabelaDinamicaFaturados"; //Nome precisa ser igual ao nome do DataSet do relatorio, obrigatoriamente.
                rptDados.DataMember = "USER_SP_SelecionatabelaDinamicaFitasa";
                rptDados.Value = dadosTable;

                //Atribui ReportDataSource aos dados do sistema
                this.rptVendas.LocalReport.DataSources.Add(rptDados);
                this.rptVendas.DataBind();

                //Remove variaveis de sessão
                Session.Remove("empresa");
                Session.Remove("natureza");
                Session.Remove("produto");
                Session.Remove("linha");
                Session.Remove("dataInicial");
                Session.Remove("datafinal");
                Session.Remove("entidade");
                Session.Remove("subFamilia");
            }         

        }
    }
}