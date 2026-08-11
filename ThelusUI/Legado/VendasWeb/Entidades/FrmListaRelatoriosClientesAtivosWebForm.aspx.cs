using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace VendasWeb.Entidades
{
    public partial class FrmListaRelatoriosClientesAtivosWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        funcoes mdlFuncoes = new funcoes();
        RelatorioCrystalClass OBJRelatorioCrystal = new RelatorioCrystalClass();
        //ReportDocument MyReport = new ReportDocument();
        UtilClass ObjUtilClass = new UtilClass();
        usuario ObjUsuarioClass = new usuario();

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
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = false;
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

            if (!IsPostBack)
            {
                CarregaCombos();
            }
        }

        public void CarregaCombos()
        {
            //ObjUsuarioClass.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            //EmpresaDropDownList.DataSource = ObjUsuarioClass.ListaEmpresasUsuario();
            //EmpresaDropDownList.DataTextField = "NomeEmpresa";
            //EmpresaDropDownList.DataValueField = "IDEmpresa";
            //EmpresaDropDownList.DataBind();
            //EmpresaDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

            ObjUsuarioClass.CodigoUsuario = Session["usuario"].ToString();

            VendedorDropDownList.DataSource = ObjUsuarioClass.ListaVendedores();
            VendedorDropDownList.DataTextField = "NomeVendedor";
            VendedorDropDownList.DataValueField = "IDVendedor";
            VendedorDropDownList.DataBind();
            VendedorDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmCarteira.aspx?indmnu=2");
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            Session["reportDocument"] = null;

            string CodigoCliente = "", NomeCliente = "", Ativo = "", CodigoUsuario = "";
            int IDVendedor = 0;

            if (ClientesTextBox.Text.ToString() != "" && ClientesTextBox.Text.ToString() != null)
            {
                string aux = ClientesTextBox.Text.Substring(0, 3);

                if (aux == "CLI" || aux == "FOR")
                    CodigoCliente = ClientesTextBox.Text.ToString();
                else
                    NomeCliente = ClientesTextBox.Text.ToString();
            }

            Ativo = StatusDropDownList.SelectedValue.ToString();
            CodigoUsuario = Session["usuario"].ToString();
            IDVendedor = Convert.ToInt32(VendedorDropDownList.SelectedValue);

            OBJRelatorioCrystal.GeraRelatorioClientesAtivos(CodigoCliente, NomeCliente,
             Ativo, CodigoUsuario, IDVendedor, ref CrystalReportViewer1);

            Session["reportDocument"] = OBJRelatorioCrystal.MyReport;
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