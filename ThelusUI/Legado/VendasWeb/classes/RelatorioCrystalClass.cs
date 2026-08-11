using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.classes
{
    public class RelatorioCrystalClass
    {
        public ReportDocument MyReport { get; set; }
        public ConnectionInfo myConnectionInfo { get; set; }
        public string ServerNameCRM { get; set; }
        public string DatabaseNameCRM { get; set; }
        public string UserIDCRM { get; set; }
        public string PasswordCRM { get; set; }
        public string ServerNameSAP { get; set; }
        public string DatabaseNameSAP { get; set; }
        public string UserIDSAP { get; set; }
        public string PasswordSAP { get; set; }
        public string NomeRelatorio { get; set; }

        //Seta se é CRM ou SAP
        public string Sistema { get; set; }

        string erro = "";

        public RelatorioCrystalClass()
        {
            //Inicializa valores
            this.MyReport = new ReportDocument();
            this.myConnectionInfo = new ConnectionInfo();
        }

        public void InicializaDadosConexao()
        {
            try
            {

                //Atribui Valores para conexão do CRM
                ServerNameCRM = System.Configuration.ConfigurationManager.AppSettings["ServerCRM"];
                DatabaseNameCRM = System.Configuration.ConfigurationManager.AppSettings["BancoDadosCRM"];
                UserIDCRM = System.Configuration.ConfigurationManager.AppSettings["UsuarioBancoCRM"];
                PasswordCRM = System.Configuration.ConfigurationManager.AppSettings["SenhaBancoCRM"];

                //Atribui Valores para conexão do SAP
                ServerNameSAP = System.Configuration.ConfigurationManager.AppSettings["ServerSAP"];
                DatabaseNameSAP = System.Configuration.ConfigurationManager.AppSettings["BancoDadosSAP"];
                UserIDSAP = System.Configuration.ConfigurationManager.AppSettings["UsuarioBancoSAP"];
                PasswordSAP = System.Configuration.ConfigurationManager.AppSettings["SenhaBancoSAP"];

                switch (Sistema)
                {
                    case "CRM":
                        this.myConnectionInfo.ServerName = this.ServerNameCRM;
                        this.myConnectionInfo.DatabaseName = this.DatabaseNameCRM;
                        this.myConnectionInfo.UserID = this.UserIDCRM;
                        this.myConnectionInfo.Password = this.PasswordCRM;
                        break;
                    case "SAP":
                        this.myConnectionInfo.ServerName = this.ServerNameSAP;
                        this.myConnectionInfo.DatabaseName = this.DatabaseNameSAP;
                        this.myConnectionInfo.UserID = this.UserIDSAP;
                        this.myConnectionInfo.Password = this.PasswordSAP;
                        break;
                }

                SetDBLogonForReport(this.myConnectionInfo, this.MyReport);

            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }
        }

        public void GeraRelatorioPDF()
        {
            try
            {

                //Inicializa Crystal
                InicializaDadosConexao();

                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream = MyReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + NomeRelatorio + ".PDF");
                HttpContext.Current.Response.BinaryWrite(byteArray);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.Close();
                MyReport.Close();
                MyReport.Dispose();

            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }
        }

        public void GeraRelatorioEXCELFORMATADO()
        {
            //Inicializa Crystal
            InicializaDadosConexao();

            System.IO.Stream oStream = null;
            byte[] byteArray = null;
            oStream = MyReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            byteArray = new byte[oStream.Length];
            oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/xls";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + NomeRelatorio + ".xls");
            HttpContext.Current.Response.BinaryWrite(byteArray);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
            MyReport.Close();
            MyReport.Dispose();
        }

        public void GeraRelatorioEXCELDADOS()
        {
            //Inicializa Crystal
            InicializaDadosConexao();

            System.IO.Stream oStream = null;
            byte[] byteArray = null;
            oStream = MyReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
            byteArray = new byte[oStream.Length];
            oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/xlsx";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + NomeRelatorio + ".xlsx");
            HttpContext.Current.Response.BinaryWrite(byteArray);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
            MyReport.Close();
            MyReport.Dispose();
        }

        private void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument ArquivoReport)
        {
            try
            {

                Tables tables = ArquivoReport.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
                {
                    TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    table.ApplyLogOnInfo(tableLogonInfo);
                }

            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }
        }

        public void GeraRelatorioTabelaPrecoTela(int IDTabela, string CodigoUsuario, string NomeRelatorio, ref CrystalReportViewer OBJCrystalReportViewer)
        {
            usuario Objusuario = new usuario();
            Objusuario.CodigoUsuario = CodigoUsuario;
            Objusuario.ConsultaGrupos("Ativo");

            HttpContext.Current.Session["reportDocument"] = null;

            this.MyReport.Load(HttpContext.Current.Server.MapPath("~/relatorios/TabelaPreco.rpt"));
            this.MyReport.Refresh();

            //Sistema CRM/SAP
            this.Sistema = "CRM";
            //Nome do relatório gerado
            this.NomeRelatorio = NomeRelatorio;

            //Parâmetros da Procedure quando existir
            this.MyReport.SetParameterValue("@IDTabela", IDTabela);

            this.InicializaDadosConexao();

            OBJCrystalReportViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;

            //Verifica se usuário é do grupo controladoria ou manutenção tabela de Preço
            if (Objusuario.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 3).Count() > 0 || Objusuario.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 5).Count() > 0)
            {
                OBJCrystalReportViewer.AllowedExportFormats = (int)(ViewerExportFormats.ExcelRecordFormat | ViewerExportFormats.PdfFormat);
            }
            else
            {
                OBJCrystalReportViewer.AllowedExportFormats = (int)(ViewerExportFormats.PdfFormat);
            }

            OBJCrystalReportViewer.ReportSource = this.MyReport;
            OBJCrystalReportViewer.DataBind();
        }

        public void GeraRelatorioTabelaPrecoArquivo(int IDTabela, string NomeRelatorio, ref CrystalReportViewer OBJCrystalReportViewer, string TipoArquivo)
        {
            HttpContext.Current.Session["reportDocument"] = null;

            this.MyReport.Load(HttpContext.Current.Server.MapPath("~/relatorios/TabelaPreco.rpt"));
            this.MyReport.Refresh();

            //Sistema CRM/SAP
            this.Sistema = "CRM";
            //Nome do relatório gerado
            this.NomeRelatorio = NomeRelatorio;

            //Parâmetros da Procedure quando existir
            this.MyReport.SetParameterValue("@IDTabela", IDTabela);

            //Verifica se vai gerar em EF(Excel Formatado), ED(Excel Dados) ou PD(PDF).
            switch (TipoArquivo)
            {
                case "EF":
                    this.GeraRelatorioEXCELFORMATADO();
                    break;
                case "ED":
                    this.GeraRelatorioEXCELDADOS();
                    break;
                case "PD":
                    this.GeraRelatorioPDF();
                    break;
                default:
                    this.GeraRelatorioPDF();
                    break;
            }
        }

        public void GeraRelatorioIndicadoresTI(int IDUsuarioResponsavel, int IDUsuarioSolicitante, string DataInicial, string DataFinal, string Sistema, string NomeRelatorio)
        {
            try
            {
                HttpContext.Current.Session["reportDocument"] = null;

                this.MyReport.Load(HttpContext.Current.Server.MapPath("~/relatorios/IndicadoresTI_V2.rpt"));
                this.MyReport.Refresh();

                //Sistema CRM/SAP
                this.Sistema = "CRM";
                //Nome do relatório gerado
                this.NomeRelatorio = NomeRelatorio;

                //Parâmetros da Procedure quando existir
                this.MyReport.SetParameterValue("@IDUsuarioResponsavel", IDUsuarioResponsavel);
                this.MyReport.SetParameterValue("@IDUsuarioSolicitante", IDUsuarioSolicitante);
                this.MyReport.SetParameterValue("@DataInicial", DataInicial);
                this.MyReport.SetParameterValue("@DataFinal", DataFinal);
                this.MyReport.SetParameterValue("@Sistema", Sistema);

                this.GeraRelatorioPDF();
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }
        }

        public string GeraRelatorioSACTickets(int IDEmpresa, int IDTicket, int IDSituacao, string AberturaInicial, string AberturaFinal,
        string FechamentoInicial, string FechamentoFinal, string Cliente, int IDSolucao, string Ticket, string TipoArquivo, string NomeRelatorio)
        {
            try
            {

                HttpContext.Current.Session["reportDocument"] = null;

                this.MyReport.Load(HttpContext.Current.Server.MapPath("~/relatorios/RelatorioTicketSAC.rpt"));
                this.MyReport.Refresh();

                //Sistema CRM/SAP
                this.Sistema = "CRM";
                //Nome do relatório gerado
                this.NomeRelatorio = NomeRelatorio;

                //Parâmetros da Procedure quando existir
                this.MyReport.SetParameterValue("@Tela", "");
                this.MyReport.SetParameterValue("@IDEmpresa", IDEmpresa);
                this.MyReport.SetParameterValue("@IDTicket", IDTicket);
                this.MyReport.SetParameterValue("@IDSituacao", IDSituacao);
                this.MyReport.SetParameterValue("@Cliente", Cliente);
                this.MyReport.SetParameterValue("@AberturaInicial", AberturaInicial);
                this.MyReport.SetParameterValue("@AberturaFinal", AberturaFinal);
                this.MyReport.SetParameterValue("@FechamentoInicial", FechamentoInicial);
                this.MyReport.SetParameterValue("@FechamentoFinal", FechamentoFinal);
                this.MyReport.SetParameterValue("@IDSolucao", IDSolucao);
                this.MyReport.SetParameterValue("@Ticket", Ticket);

                //Verifica se vai gerar em EF(Excel Formatado), ED(Excel Dados) ou PD(PDF).
                switch (TipoArquivo)
                {
                    case "EF":
                        this.GeraRelatorioEXCELFORMATADO();
                        break;
                    case "ED":
                        this.GeraRelatorioEXCELDADOS();
                        break;
                    case "PD":
                        this.GeraRelatorioPDF();
                        break;
                    default:
                        this.GeraRelatorioPDF();
                        break;
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return erro;
        }

        public void GeraRelatorioClientesAtivos(string CodigoCliente, string NomeCliente,
            string Ativo, string CodigoUsuario, int IDVendedor, ref CrystalReportViewer OBJCrystalReportViewer)
        {
            this.MyReport.Load(HttpContext.Current.Server.MapPath("~/relatorios/RelatorioClientesAtivos.rpt"));
            this.MyReport.Refresh();

            //Sistema CRM/SAP
            this.Sistema = "CRM";

            //Parâmetros da Procedure quando existir
            this.MyReport.SetParameterValue("@CodigoCliente", CodigoCliente);
            this.MyReport.SetParameterValue("@NomeCliente", NomeCliente);
            this.MyReport.SetParameterValue("@Ativo", Ativo);
            this.MyReport.SetParameterValue("@CodigoUsuario", CodigoUsuario);
            this.MyReport.SetParameterValue("@IDVendedor", IDVendedor);

            this.InicializaDadosConexao();

            OBJCrystalReportViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;

            OBJCrystalReportViewer.AllowedExportFormats = (int)(ViewerExportFormats.ExcelRecordFormat | ViewerExportFormats.PdfFormat);
            OBJCrystalReportViewer.ReportSource = this.MyReport;
            OBJCrystalReportViewer.DataBind();
        }

        public void GeraRelatorioAtendimentos(string CodigoClienteSAP, DateTime PeriodoInicial,
        DateTime PeriodoFinal, string IDStatus, string IDVendedores, ref CrystalReportViewer OBJCrystalReportViewer)
        {
            this.MyReport.Load(HttpContext.Current.Server.MapPath("~/relatorios/RelatorioAtendimentosVendas.rpt"));
            this.MyReport.Refresh();

            //Sistema CRM/SAP
            this.Sistema = "CRM";

            //Parâmetros da Procedure quando existir
            this.MyReport.SetParameterValue("@CodigoClienteSAP", CodigoClienteSAP);
            this.MyReport.SetParameterValue("@PeriodoInicial", PeriodoInicial);
            this.MyReport.SetParameterValue("@PeriodoFinal", PeriodoFinal);
            this.MyReport.SetParameterValue("@IDStatus", IDStatus);
            this.MyReport.SetParameterValue("@IDVendedores", IDVendedores);

            this.InicializaDadosConexao();

            OBJCrystalReportViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;

            OBJCrystalReportViewer.AllowedExportFormats = (int)(ViewerExportFormats.ExcelRecordFormat | ViewerExportFormats.PdfFormat);
            OBJCrystalReportViewer.ReportSource = this.MyReport;
            OBJCrystalReportViewer.DataBind();
        }

    }
}