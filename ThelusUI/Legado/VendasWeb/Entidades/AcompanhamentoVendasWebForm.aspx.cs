using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidades
{
    public partial class AcompanhamentoVendasWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        funcoes mdlFuncoes = new funcoes();
        ReportDocument MyReport = new ReportDocument();
        VendedorClass ObjVendedorClass = new VendedorClass();

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

                EmpresaDropDownList.DataSource = mdlFuncoes.Consulta_Empresa(Session["usuario"].ToString());
                EmpresaDropDownList.DataValueField = "IDEmpresa";
                EmpresaDropDownList.DataTextField = "NomeEmpresa";
                EmpresaDropDownList.DataBind();

                EmpresaDropDownList.Items.Insert(0, new ListItem("Todas", "0"));
                EmpresaDropDownList.Focus();
            }
        }

        protected void RelatorioPassoButton_Click(object sender, EventArgs e)
        {
            Session["reportDocument"] = null;

            MyReport.Load(Server.MapPath("~/relatorios/RelatorioAcompanhamentoVendedor.rpt"));
            MyReport.Refresh();
            ConnectionInfo myConnectionInfo = new ConnectionInfo();
            myConnectionInfo.ServerName = "192.168.0.35\\SAP10"; // Utilize o nome do servidor ou IP
            myConnectionInfo.DatabaseName = "CRM_MANULI";
            myConnectionInfo.UserID = "sa";
            myConnectionInfo.Password = "bdsapb12023@!1";
            MyReport.SetParameterValue("@IDEmpresa", Convert.ToInt32(EmpresaDropDownList.SelectedValue.ToString()));
            MyReport.SetParameterValue("@IDUsuario", Convert.ToInt32(Session["IDUsuario"].ToString()));
            MyReport.SetParameterValue("@Condicao", SelecaoDropDownList.SelectedValue.ToString());
            MyReport.SetParameterValue("@DataInicial", Convert.ToDateTime(DataInicialTextBox.Text.ToString()));
            MyReport.SetParameterValue("@DataFinal", Convert.ToDateTime(DataFinalTextBox.Text.ToString()));
            SetDBLogonForReport(myConnectionInfo, MyReport);

            //Desabilita árvore de grupos
            CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;

            //Habilita somente formato Excel e PDF
            CrystalReportViewer1.AllowedExportFormats = (int)(ViewerExportFormats.ExcelRecordFormat | ViewerExportFormats.PdfFormat);
            CrystalReportViewer1.ReportSource = MyReport;
            CrystalReportViewer1.DataBind();

            Session["reportDocument"] = MyReport;
        }

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