using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using VendasWeb.GerencialVendas;

namespace VendasWeb.telasRelatorio
{
    public partial class FrmRelAgendaVisitaGeral : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionClass OBJSessao = new SessionClass();
            AgendaVisitaClass ObjAgendaVisitaClass = new AgendaVisitaClass();
            funcoes mdlfuncoes = new funcoes();
            clasRelatorios ObjclasRelatorios = new clasRelatorios();
            GerencialVendas.ControleReportViwerClass ObjControleReportViwerClass = new GerencialVendas.ControleReportViwerClass();

            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                ReportDataSource ReportDataSource = new ReportDataSource();
                DataTable DtDataTable;

                if (Session["ObjAgendaVisitaClass"] != null)
                {
                    ObjAgendaVisitaClass = (GerencialVendas.AgendaVisitaClass)Session["ObjAgendaVisitaClass"];
                    Session["ObjAgendaVisitaClass"] = null;
                }

                DtDataTable = ObjAgendaVisitaClass.CONSULTA_AGENDA_GERAL();

                ReportDataSource.Name = "USER_SP_CONSULTA_AGENDA_GERAL";
                ReportDataSource.DataMember = "USER_SP_CONSULTA_AGENDA_GERAL";
                ReportDataSource.Value = DtDataTable;


                this.ReportViewer.LocalReport.DataSources.Clear();
                this.ReportViewer.LocalReport.DataSources.Add(ReportDataSource);
                this.ReportViewer.DataBind();

                ObjControleReportViwerClass.DisableUnwantedExportFormat(ReportViewer, "PDF");
                //ObjControleReportViwerClass.DisableUnwantedExportFormat(ReportViewer, "EXCEL");
                ObjControleReportViwerClass.DisableUnwantedExportFormat(ReportViewer, "WORD");


                #region Gerando Excel para Impressao direto
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                string filename;

                byte[] bytes = ReportViewer.LocalReport.Render(
                   "Excel", null, out mimeType, out encoding,
                    out extension,
                   out streamids, out warnings);

                filename = string.Format("{0}.{1}", "ExportToExcel", "xls");
                Response.ClearHeaders();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + "AgendaVisitaGeral.xls");
                Response.ContentType = mimeType;
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();

                #endregion

                
            }

        }
    }
}