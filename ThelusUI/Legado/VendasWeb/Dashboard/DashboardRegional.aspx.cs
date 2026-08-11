using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Dashboard
{
    public partial class DashboardRegional : System.Web.UI.Page
    {
        clsDashBoard objPrincipal = new clsDashBoard();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                /*Tratar Abrir e fechar Div*/
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                //Atualiza Grid Com as Classes
                Atualiza_Select_Regional();

                RegionalSelect.Value = "0";

                TextBoxDataInicial.Text = Convert.ToDateTime((DateTime.Now.Year + "-" + DateTime.Now.Month + "-01")).ToString("yyyy-MM-dd");
                TextBoxDataFinal.Text = DateTime.Now.ToString("yyyy-MM-dd");

                Gerar_Lista();
            }
        }

        protected void AnaliseGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            AnaliseGridView.PageIndex = e.NewPageIndex;
            LinkButtonRegional_Click(null, null);
        }

        protected void Atualiza_Select_Regional()
        {
            DataTable Resultado = new DataTable();

            objPrincipal.UsuCod = Session["usuario"].ToString();
            objPrincipal.TodosCodigos = "S";
            Resultado = objPrincipal.Lista_Regionais();


            RegionalSelect.DataSource = Resultado;
            RegionalSelect.DataTextField = "DescricaoRegional";
            RegionalSelect.DataValueField = "IDRegional";
            RegionalSelect.DataBind();
        }

        private void Gerar_Lista()
        {
            objPrincipal = new clsDashBoard();

            objPrincipal.DataInicial = TextBoxDataInicial.Text.ToString();
            objPrincipal.DataFinal = TextBoxDataFinal.Text.ToString();
            //objPrincipal.EmpCod = EmpresaDropDown.SelectedValue;
            objPrincipal.UsuCod = Session["usuario"].ToString();

            RecuperaDados_Select();

            AnaliseGridView.DataSource = objPrincipal.Lista_Dashboard_Regional();
            AnaliseGridView.DataBind();
            AnalisesMultiView.Visible = true;
        }

        protected void RecuperaDados_Select()
        {

            objPrincipal.Regionais = "";

            for (int i = 0; i < RegionalSelect.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (RegionalSelect.Items[i].Selected == true)
                {
                    //objPrincipal.Regionais += RegionalSelect.Items[i].Value + ",";
                    objPrincipal.Regionais += RegionalSelect.Items[i].Value;
                }
            }


        }

        protected void LinkButtonClasses_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashboardClasses.aspx?indmnu=2");
        }

        protected void LinkButtonVendedor_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashboardVendedor.aspx?indmnu=2");
        }

        protected void LinkButtonSupervisor_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashboardSupervisor.aspx?indmnu=2");
        }

        protected void LinkButtonRegional_Click(object sender, EventArgs e)
        {
            Gerar_Lista();
        }
    }
}