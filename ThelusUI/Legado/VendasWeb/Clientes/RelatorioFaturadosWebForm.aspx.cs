using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace VendasWeb.Clientes 
{
    public partial class RelatorioFaturadosWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();

        public clsConexao teste = new clsConexao();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

            }
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                 
                ReportDocument MyReport = new ReportDocument();
                MyReport.Load(Server.MapPath("../relatorios/CrystalReport1.rpt"));
                MyReport.Refresh();
                MyReport.SetDataSource(ds);
                ConnectionInfo myConnectionInfo = new ConnectionInfo();
                myConnectionInfo.ServerName = "192.168.0.15"; // Utilize o nome do servidor ou IP
                myConnectionInfo.DatabaseName = "SBO_ManuliFitasa_PROD";
                myConnectionInfo.UserID = "sa";
                myConnectionInfo.Password = "bdsapb12019@!1";
                SetDBLogonForReport(myConnectionInfo, MyReport);
                //MyReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, true, "CONTATO_ANALITICO");
                //MyReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "CARTA_CONTATO");

                MyReport.SetParameterValue(0,"0001");
                CrystalReportViewer1.ReportSource = MyReport;
                //CrystalReportViewer1.RefreshReport();
                CrystalReportViewer1.DataBind();

                //myrpt.Load(Server.MapPath("../Reporting/CrFechamentoOrdemProducao.rpt"));
                //myrpt.SetDataSource(dadosTable);
                //myrpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "FECHAMENTO_OP");
                /*
                DataSet ds = new DataSet();
                // Antes, faço uma consulta no banco e jogo no DataSet para ser usado logo abaixo
                ReportDocument relatorio = new ReportDocument();
                relatorio.Load(Server.MapPath("~/relatorios/CrystalReport1.rpt"));
                relatorio.Database.Tables[0].SetDataSource(ds.Tables[0]);
                // Esse é Viewer que puxamos antes
                CrystalReportViewer1.ReportSource = relatorio;
                CrystalReportViewer1.DataBind();
                */
            }
            catch (Exception ex)
            {

            }
        }

        // Conexao de logon do relatorio 
        private void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument ArquivoReport)
        {
            Tables tables = ArquivoReport.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }
        }
    }
}