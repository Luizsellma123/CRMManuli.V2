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
using System.Data;

namespace VendasWeb.Controladoria
{
    public partial class RelatorioAtendimentosWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        funcoes mdlFuncoes = new funcoes();
        ReportDocument MyReport = new ReportDocument();
        VendedorClass ObjVendedorClass = new VendedorClass();
        ClienteClasse OBJCliente = new ClienteClasse();

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
                //Carrega Combos
                CarregaCombos();
            }

        }

        public void CarregaCombos()
        {
            DataTable Resultado = new DataTable();

            ObjVendedorClass.UsuCod = Session["usuario"].ToString();
            ObjVendedorClass.TodosCodigos = "S";
            Resultado = ObjVendedorClass.Consulta_Vendedor();

            VendedoresSelect.DataSource = Resultado;
            VendedoresSelect.DataTextField = "NomeVendedor";
            VendedoresSelect.DataValueField = "IDVendedor";
            VendedoresSelect.DataBind();

            StatusDropDownList.DataSource = OBJCliente.CarregaStatusCliente();
            StatusDropDownList.DataTextField = "DescricaoStatus";
            StatusDropDownList.DataValueField = "IDStatus";
            StatusDropDownList.DataBind();

            StatusDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

        }

        protected void RelatorioPassoButton_Click(object sender, EventArgs e)
        {
            RecuperaDados_Select();

            Session["reportDocument"] = null;
                    
            RelatorioCrystalClass OBJRelatorioCrystal = new RelatorioCrystalClass();

            OBJRelatorioCrystal.GeraRelatorioAtendimentos(ClienteTextBox.Text.ToString(),
                Convert.ToDateTime(DataInicialTextBox.Text),
                Convert.ToDateTime(DataFinalTextBox.Text),
                StatusDropDownList.SelectedValue.ToString(),
                OBJCliente.IDVendedores, ref CrystalReportViewer1);

            Session["reportDocument"] = MyReport;
        }

        protected void RecuperaDados_Select()
        {

            OBJCliente.IDVendedores = "";

            for (int i = 0; i < VendedoresSelect.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (VendedoresSelect.Items[i].Selected == true)
                {
                    if (i == 0)
                    {
                        OBJCliente.IDVendedores = VendedoresSelect.Items[i].Value;
                    }
                    else
                    {
                        OBJCliente.IDVendedores += "," + VendedoresSelect.Items[i].Value;
                    }
                }
            }


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