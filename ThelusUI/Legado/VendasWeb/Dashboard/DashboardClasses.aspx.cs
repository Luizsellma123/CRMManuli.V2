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
    public partial class DashboardClasses : System.Web.UI.Page
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

                ClassesSelect.Value = "0000000";

                //Atualiza Grid Com as Classes
                Atualiza_Select_Classes();

                //Classes = "Todas";
                ClassesSelect.Value = "0000000";

                TextBoxDataInicial.Text = Convert.ToDateTime((DateTime.Now.Year + "-" + DateTime.Now.Month + "-01")).ToString("yyyy-MM-dd");
                TextBoxDataFinal.Text = DateTime.Now.ToString("yyyy-MM-dd");

                Gerar_Lista();
            }
        }

        protected void AnaliseGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            AnaliseGridView.PageIndex = e.NewPageIndex;
            LinkButtonClasses_Click(null, null);
        }

        protected void Atualiza_Select_Classes()
        {
            DataTable Resultado = new DataTable();

            objPrincipal.UsuCod = Session["usuario"].ToString();
            objPrincipal.TodosCodigos = "S";
            Resultado = objPrincipal.Lista_Classes();



            ClassesSelect.DataSource = Resultado;
            ClassesSelect.DataTextField = "VendClasseDescr";
            ClassesSelect.DataValueField = "VendClasseCod";
            ClassesSelect.DataBind();
        }

        protected void LinkButtonClasses_Click(object sender, EventArgs e)
        {
            Gerar_Lista();
        }

        protected void RecuperaDados_Select()
        {

            objPrincipal.VendClasseCod = "";

            for (int i = 0; i < ClassesSelect.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (ClassesSelect.Items[i].Selected == true)
                {
                    objPrincipal.VendClasseCod += ClassesSelect.Items[i].Value + ",";
                }
            }


        }

        protected void LinkButtonVendedor_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashboardVendedor.aspx?indmnu=2");
        }

        private void Gerar_Lista()
        {
            objPrincipal = new clsDashBoard();

            objPrincipal.DataInicial = TextBoxDataInicial.Text.ToString();
            objPrincipal.DataFinal = TextBoxDataFinal.Text.ToString();
            //objPrincipal.EmpCod = EmpresaDropDown.SelectedValue;
            objPrincipal.UsuCod = Session["usuario"].ToString();

            RecuperaDados_Select();

            AnaliseGridView.DataSource = objPrincipal.Lista_Dashboard_Classes();
            AnaliseGridView.DataBind();
            AnalisesMultiView.Visible = true;
        }

        protected void LinkButtonSupervisor_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashboardSupervisor.aspx?indmnu=2");
        }

        protected void LinkButtonRegional_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashboardRegional.aspx?indmnu=2");
        }
    }
}