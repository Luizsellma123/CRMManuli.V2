using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using VendasWeb.GerencialVendas;

namespace VendasWeb.telasRelatorio
{
    public partial class FrmRelAgendaVisita : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
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

                DtDataTable = ObjclasRelatorios.RelatorioAgendaVisitaDetalhe(ObjAgendaVisitaClass.AGENDA_VISITA_ID);

                ReportDataSource.Name = "User_SP_Agenda_Visita_ID";
                ReportDataSource.DataMember = "User_SP_Agenda_Visita_ID";
                ReportDataSource.Value = DtDataTable;


                this.ReportViewer.LocalReport.DataSources.Clear();
                this.ReportViewer.LocalReport.DataSources.Add(ReportDataSource);
                this.ReportViewer.DataBind();

                ObjControleReportViwerClass.DisableUnwantedExportFormat(ReportViewer, "EXCEL");
                ObjControleReportViwerClass.DisableUnwantedExportFormat(ReportViewer, "WORD");



                #region Gerando PDF para Impressao direto
                //Cria arquivo PDF
                Warning[] warn = null;
                string[] streaminds = null;
                string mimeType = "application/pdf";
                string encoding = string.Empty;
                string extension = string.Empty;
                byte[] byteViewer = null;

                //Carrega o Report Viewer sem preview
                byteViewer = this.ReportViewer.LocalReport.Render("pdf", null, out mimeType, out encoding, out extension, out streaminds, out warn);


                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "inline; filename=AgendaVisitaDetalhe.pdf");
                Response.BinaryWrite(byteViewer);

                Response.Flush();
                Response.End();

                #endregion


            }





        }
    }
}