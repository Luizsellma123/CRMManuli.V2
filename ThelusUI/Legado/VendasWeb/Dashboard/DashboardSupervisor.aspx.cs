using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Dashboard
{
    public partial class DashboardSupervisor : System.Web.UI.Page
    {
        clsDashBoard objPrincipal = new clsDashBoard();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                objPrincipal.UsuCod = Session["usuario"].ToString();

                /*Tratar Abrir e fechar Div*/
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                //Atualiza Grid Com os Supervisores
                Atualiza_Select_Supervisor();
                
                //Supervisor = "Todas";
                SupervisorSelect.Value = "0000000";

                TextBoxDataInicial.Text = Convert.ToDateTime((DateTime.Now.Year + "-" + DateTime.Now.Month + "-01")).ToString("yyyy-MM-dd");
                TextBoxDataFinal.Text = DateTime.Now.ToString("yyyy-MM-dd");

                Gerar_Lista();
            }
        }

        protected void AnaliseGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            AnaliseGridView.PageIndex = e.NewPageIndex;
            LinkButtonSupervisor_Click(null, null);
        }

        protected void Atualiza_Select_Supervisor()
        {
            DataTable Resultado = new DataTable();

            objPrincipal.UsuCod = Session["usuario"].ToString();
            Resultado = objPrincipal.Lista_Supervisor();

            SupervisorSelect.DataSource = Resultado;
            SupervisorSelect.DataTextField = "UsuNome";
            SupervisorSelect.DataValueField = "UsuCod";
            SupervisorSelect.DataBind();

        }

        private void Gerar_Lista()
        {
            objPrincipal = new clsDashBoard();

            objPrincipal.DataInicial = TextBoxDataInicial.Text.ToString();
            objPrincipal.DataFinal = TextBoxDataFinal.Text.ToString();
            //objPrincipal.EmpCod = EmpresaDropDown.SelectedValue;
            objPrincipal.UsuCod = Session["usuario"].ToString();

            RecuperaDados_Select();

            AnaliseGridView.DataSource = objPrincipal.Lista_Dashboard_Supervisor();
            AnaliseGridView.DataBind();
            AnalisesMultiView.Visible = true;
        }

        protected void RecuperaDados_Select()
        {

            objPrincipal.UsuCodSupervisor = "";

            for (int i = 0; i < SupervisorSelect.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (SupervisorSelect.Items[i].Selected == true)
                {
                    objPrincipal.UsuCodSupervisor += SupervisorSelect.Items[i].Value + ",";
                }
            }


        }

        protected void LinkButtonClasses_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashboardClasses.aspx?indmnu=2");
        }

        protected void GerarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashboardVendedor.aspx?indmnu=2");
        }

        protected void LinkButtonSupervisor_Click(object sender, EventArgs e)
        {
            Gerar_Lista();
        }

        protected void LinkButtonRegional_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashboardRegional.aspx?indmnu=2");
        }

    }
}