using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.Clientes
{
    public partial class RelatorioPendentesVendedorWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                string usuario = Session["usuario"].ToString();
                ReportDocument MyReport = new ReportDocument();
                MyReport.Load(Server.MapPath("~/relatorios/RelatorioPendentesVendedor.rpt"));
                MyReport.Refresh();
                ConnectionInfo myConnectionInfo = new ConnectionInfo();
                myConnectionInfo.ServerName = "192.168.0.15"; // Utilize o nome do servidor ou IP
                myConnectionInfo.DatabaseName = "SBO_ManuliFitasa_PROD";
                myConnectionInfo.UserID = "sa";
                myConnectionInfo.Password = "bdsapb12019@!1";
                MyReport.SetParameterValue(0, Convert.ToInt32(Session["IDUsuario"].ToString()));
                SetDBLogonForReport(myConnectionInfo, MyReport);

                CrystalReportViewer1.ReportSource = MyReport;
                CrystalReportViewer1.DataBind();
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