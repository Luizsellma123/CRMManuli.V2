using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using VendasWeb.classes;

namespace VendasWeb.telasRelatorio
{
    public partial class FrmRelTabelaManausLocal : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            funcoes mdlfuncoes = new funcoes();
            clasRelatorios ObjclasRelatorios = new clasRelatorios();
            GerencialVendas.ControleReportViwerClass ObjControleReportViwerClass = new GerencialVendas.ControleReportViwerClass();

            if (!IsPostBack)
            {

                ReportDataSource ReportDataSource = new ReportDataSource();
                DataTable tabListaUser;

                tabListaUser = ObjclasRelatorios.relatorioTabelaPrecoManausLocal();

                ReportDataSource.Name = "USER_SP_REL_TABELA_PRECO_MANAUS_LOCAL";
                ReportDataSource.DataMember = "USER_SP_REL_TABELA_PRECO_MANAUS_LOCAL";
                ReportDataSource.Value = tabListaUser;


                this.ReportViewer.LocalReport.DataSources.Clear();
                this.ReportViewer.LocalReport.DataSources.Add(ReportDataSource);
                this.ReportViewer.DataBind();

                ObjControleReportViwerClass.DisableUnwantedExportFormat(ReportViewer, "EXCEL");
                ObjControleReportViwerClass.DisableUnwantedExportFormat(ReportViewer, "WORD");
            }
        }
    }
}