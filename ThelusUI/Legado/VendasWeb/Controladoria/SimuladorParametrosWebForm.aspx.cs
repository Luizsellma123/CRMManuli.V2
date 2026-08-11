using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.Controladoria
{
    public partial class SimuladorParametrosWebForm : System.Web.UI.Page
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
                EmpresaDropDown.Items.Add("Escolha a empresa");
                EmpresaDropDown.Items.Add("1   - Manuli CTBA");
                EmpresaDropDown.Items.Add("1.3 - Manuli SP");
                EmpresaDropDown.Items.Add("2   - Manuli Manaus");
                EmpresaDropDown.DataBind();

                if (Session["FiltrosParametros"] != null)
                {
                    EmpresaDropDown.SelectedIndex = (int)Session["FiltrosParametros"];
                    BuscarButton_Click(null, null);
                }
            }
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            //Criando tabela para ser usada no grid
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Empresa");

            DataRow dr = dt.NewRow();
            DataRow dr1 = dt.NewRow();
            DataRow dr2 = dt.NewRow();

            //Criando linhas para a tabela de empresas
            string valorDrop = EmpresaDropDown.Text;
            switch (valorDrop)
            {
                case ("Escolha a empresa"):
                    dr["Empresa"] = "Manuli Curitiba";
                    dr["Codigo"] = "1";
                    dt.Rows.Add(dr);

                    dr1["Empresa"] = "Manuli São Paulo";
                    dr1["Codigo"] = "1.3";
                    dt.Rows.Add(dr1);

                    dr2["Empresa"] = "Manuli Manaus";
                    dr2["Codigo"] = "2";
                    dt.Rows.Add(dr2);

                    SimuladorGridView.DataSource = dt;
                    SimuladorGridView.DataBind();
                    SimuladorMultiView.Visible = true;
                    break;

                case ("1   - Manuli CTBA"):
                    dr["Empresa"] = "Manuli Curitiba";
                    dr["Codigo"] = "1";
                    dt.Rows.Add(dr);

                    SimuladorGridView.DataSource = dt;
                    SimuladorGridView.DataBind();
                    SimuladorMultiView.Visible = true;
                    break;

                case ("1.3 - Manuli SP"):
                    dr["Empresa"] = "Manuli São Paulo";
                    dr["Codigo"] = "1.3";
                    dt.Rows.Add(dr);

                    SimuladorGridView.DataSource = dt;
                    SimuladorGridView.DataBind();
                    SimuladorMultiView.Visible = true;
                    break;

                case ("2   - Manuli Manaus"):
                    dr["Empresa"] = "Manuli Manaus";
                    dr["Codigo"] = "2";
                    dt.Rows.Add(dr);

                    SimuladorGridView.DataSource = dt;
                    SimuladorGridView.DataBind();
                    SimuladorMultiView.Visible = true;
                    break;
            }

        }

        protected void VendedorButton_Click(object sender, EventArgs e)
        {
            simulador.codempresa = ((Label)((Control)sender).FindControl("CodigoGrid")).Text;
            simulador.nomeEmpresa = ((Label)((Control)sender).FindControl("EmpresaGrid")).Text;
            simulador.alcada = "Vendedor";
            Session["simulacao"] = simulador;
            Session["FiltrosParametros"] = EmpresaDropDown.SelectedIndex;
            Response.Redirect("SimuladorResponseWebForm.aspx?indmnu=3");
        }

        protected void SupervisorButton_Click(object sender, EventArgs e)
        {
            simulador.codempresa = ((Label)((Control)sender).FindControl("CodigoGrid")).Text;
            simulador.nomeEmpresa = ((Label)((Control)sender).FindControl("EmpresaGrid")).Text;
            simulador.alcada = "Supervisor";
            Session["simulacao"] = simulador;
            Session["FiltrosParametros"] = EmpresaDropDown.SelectedIndex;
            Response.Redirect("SimuladorResponseWebForm.aspx?indmnu=3");
        }

        protected void GerenteButton_Click(object sender, EventArgs e)
        {
            simulador.codempresa = ((Label)((Control)sender).FindControl("CodigoGrid")).Text;
            simulador.nomeEmpresa = ((Label)((Control)sender).FindControl("EmpresaGrid")).Text;
            simulador.alcada = "Gerente";
            Session["simulacao"] = simulador;
            Session["FiltrosParametros"] = EmpresaDropDown.SelectedIndex;
            Response.Redirect("SimuladorResponseWebForm.aspx?indmnu=3");
        }

        protected void ControladoriaButton_Click(object sender, EventArgs e)
        {
            simulador.codempresa = ((Label)((Control)sender).FindControl("CodigoGrid")).Text;
            simulador.nomeEmpresa = ((Label)((Control)sender).FindControl("EmpresaGrid")).Text;
            simulador.alcada = "Controladoria";
            Session["simulacao"] = simulador;
            Session["FiltrosParametros"] = EmpresaDropDown.SelectedIndex;
            Response.Redirect("SimuladorResponseWebForm.aspx?indmnu=3");
        }
    }
}