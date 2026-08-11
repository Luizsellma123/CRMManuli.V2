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
    public partial class frmCopiaPedido : System.Web.UI.Page
    {
        clasRelatorios mdlRelatorios = new clasRelatorios();
        criptografia mdlCriptografia = new criptografia();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            string empCod = Session["EmpCod"].ToString();
            string pedVendaNum = Session["PedVendaNum"].ToString();
            //string pedVendaNum = mdlCriptografia.Descriptografar(Request.QueryString["idPed"], "#!$a36?@");
            string codOperacao = Session["Tipo"].ToString();

            //Valida Acesso
            OBJSessao.ValidaAcesso();

            /* pedido.Value = pedVendaNum;
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
                this.rptCopiaPedidos.LocalReport.DataSources.Add(rptDados);
                this.rptCopiaPedidos.LocalReport.DataSources.Add(rptItemDados);
                this.rptCopiaPedidos.LocalReport.DataSources.Add(rptItemDadosFotos);
                this.rptCopiaPedidos.LocalReport.EnableExternalImages = true;
                this.rptCopiaPedidos.DataBind();


                Warning[] warn = null;
                string[] streaminds = null;
                string mimeType = "application/pdf";
                string encoding = string.Empty;
                string extension = string.Empty;
                byte[] byteViewer = null;

                //Carrega o Report Viewer sem preview
                byteViewer = this.rptCopiaPedidos.LocalReport.Render("pdf", null, out mimeType, out encoding, out extension, out streaminds, out warn);


                /*Response.Buffer = true;
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "inline; filename=copia_pedido_" + pedVendaNum.ToString() + ".pdf");
                Response.BinaryWrite(byteViewer);

                Response.Flush();
                Response.End();*/

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