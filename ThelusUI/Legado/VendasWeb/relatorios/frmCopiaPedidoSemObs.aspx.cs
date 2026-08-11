using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;


namespace VendasWeb.relatorios
{
    public partial class frmCopiaPedidoSemObs : System.Web.UI.Page
    {
        criptografia mdlCriptografia = new criptografia();
        clasRelatorios mdlRelatorios = new clasRelatorios();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            /*string empCod = mdlCriptografia.Descriptografar(Request.QueryString["idEmp"], "#!$a36?@");
            string pedVendaNum = mdlCriptografia.Descriptografar(Request.QueryString["idPed"], "#!$a36?@");
            string codOperacao = mdlCriptografia.Descriptografar(Request.QueryString["idOpe"], "#!$a36?@");*/

            string empCod = Session["EmpCod"].ToString();
            string pedVendaNum = Session["PedVendaNum"].ToString();
            string codOperacao = Session["Tipo"].ToString();

            /*pedido.Value = pedVendaNum;
            empresa.Value = empCod;
            operacao.Value = codOperacao;*/

            if (!IsPostBack)
            {
                ReportDataSource rptDados = new ReportDataSource();
                ReportDataSource rptItemDados = new ReportDataSource();
                ReportDataSource rptItemDadosFotos = new ReportDataSource();

                DataTable dadosTable = new DataTable();
                DataTable dadosItemTable = new DataTable();
                DataTable dadosItemTableFotos = new DataTable();


                mdlRelatorios.copiaPedido(empCod, pedVendaNum, out dadosTable, out dadosItemTable, out dadosItemTableFotos);

                //Preenche ReportDataSource Cabecario
                rptDados.Name = "dtCopiaPedidoCabecario";
                rptDados.DataMember = "USER_CopiaPedido";
                rptDados.Value = dadosTable;

                //Preenche ReportDataSource Items
                rptItemDados.Name = "dtItemCopiaPedido";
                rptItemDados.DataMember = "USER_dtItemCopiaPedido";
                rptItemDados.Value = dadosItemTable;

                //Preenche ReportDataSource Items
                rptItemDadosFotos.Name = "dtItemPedido";
                rptItemDadosFotos.DataMember = "dtItemPedido";
                rptItemDadosFotos.Value = dadosItemTableFotos;

                //Atribui ReportDataSource aos dados do sistema
                this.rptCopiaPedidoSemObs.LocalReport.DataSources.Add(rptDados);
                this.rptCopiaPedidoSemObs.LocalReport.DataSources.Add(rptItemDados);
                this.rptCopiaPedidoSemObs.LocalReport.DataSources.Add(rptItemDadosFotos);
                this.rptCopiaPedidoSemObs.DataBind();



                Warning[] warn = null;
                string[] streaminds = null;
                string mimeType = "application/pdf";
                string encoding = string.Empty;
                string extension = string.Empty;
                byte[] byteViewer = null;

                //Carrega o Report Viewer sem preview
                byteViewer = this.rptCopiaPedidoSemObs.LocalReport.Render("pdf", null, out mimeType, out encoding, out extension, out streaminds, out warn);
                /*Response.Buffer = true;
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "inline; filename=copia_pedido_" + pedVendaNum.ToString() + ".pdf");
                Response.BinaryWrite(byteViewer);
                Response.Flush();
                Response.End();   */



                //Grava Relatorio em Byte para Envio por Email
                Session["byteViewer"] = byteViewer;
                Session["pedVendaNum"] = pedVendaNum;

            }

        }

        protected void EnviarPorEmailButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../WebVendas/Geral/FrmEnviaEmail.aspx?indmnu=10");
        }
    }
}