using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.Controladoria.SimuladorParametros
{
    public partial class SimuladorResponseWebForm : System.Web.UI.Page
    {
        SimuladorParametrosClass simulador = new SimuladorParametrosClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
                simulador = (SimuladorParametrosClass)Session["simulacao"];
                EmpresaInput.Value = (simulador.codempresa + " - " + simulador.nomeEmpresa);
                AlcadaInput.Value = simulador.alcada;

                DataTable outpout = new DataTable();
                outpout = simulador.Consulta_Parametros();
                SimuladorGridView.DataSource = outpout;
                SimuladorGridView.DataBind();
                SimuladorMultiView.Visible = true;

            }
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("SimuladorParametrosWebForm.aspx?indmnu=3");
        }

        protected void PercentBox_TextChanged(object sender, EventArgs e)
        {
            simulador.idparametro = ((Label)((Control)sender).FindControl("IDGrid")).Text;
            string valor = ((TextBox)((Control)sender).FindControl("PercentBox")).Text;
            valor = valor + "%";
            string[] vetor = valor.Split('%');
            simulador.Percentual = vetor[0];
            simulador.Atualiza_Porcentagem();

        }
    }
}