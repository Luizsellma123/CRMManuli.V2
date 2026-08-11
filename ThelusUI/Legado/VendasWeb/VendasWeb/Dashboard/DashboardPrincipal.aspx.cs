using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using VendasWeb.classes.GerencialVendas;
using VendasWeb.GerencialVendas;
using System.Data;

namespace VendasWeb.Dashboard
{
    public partial class DashboardPrincipal : System.Web.UI.Page
    {

        clsDashBoard objPrincipal = new clsDashBoard();
        VendedorClass ObjVendedorClass = new VendedorClass();

        protected void Page_Load(object sender, EventArgs e)
        {
             int varmenu = Convert.ToInt32(Request.QueryString["indmnu"]);
            if (Session["usuario"] == null && varmenu != 0 && varmenu < 99)
            {
                //Redireciona para tela de login
                Response.Redirect("~/Default.aspx");
            }

            if (!IsPostBack)
            {


                objPrincipal.UsuCod = Session["usuario"].ToString();

                EmpresaDropDown.DataSource = objPrincipal.Lista_Empresa();
                EmpresaDropDown.DataTextField = "EmpNome";
                EmpresaDropDown.DataValueField = "EmpCod";
                EmpresaDropDown.DataBind();



                //this.ControlPainel.refreshVendedor();

               

                /*Tratar Abrir e fechar Div*/
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                //Atualiza Grid Com os Vendedores
                Atualiza_Select_Vendedores();

                EmpresaDropDown.SelectedValue = "Todas";
                VendedoresSelect.Value = "0000000";


                txtData1.Text = Convert.ToDateTime((DateTime.Now.Year + "-" + DateTime.Now.Month + "-01")).ToString("yyyy-MM-dd");
                txtData2.Text = DateTime.Now.ToString("yyyy-MM-dd");

                Gerar_Lista();
            }

            



        
        }

        protected void AnaliseGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            AnaliseGridView.PageIndex = e.NewPageIndex;
            GerarLinkButton_Click1(null, null);
        }
        protected void GerarLinkButton_Click1(object sender, EventArgs e)
        {

            Gerar_Lista();

        }

        private void Gerar_Lista()
        {
            objPrincipal = new clsDashBoard();

            objPrincipal.DataInicial = txtData1.Text.ToString();
            objPrincipal.DataFinal = txtData2.Text.ToString();
            objPrincipal.EmpCod = EmpresaDropDown.SelectedValue;
            objPrincipal.UsuCod = Session["usuario"].ToString();

            RecuperaDados_Select();

            AnaliseGridView.DataSource = objPrincipal.Lista_Principal();
            AnaliseGridView.DataBind();
            AnalisesMultiView.Visible = true;
        }

        protected void txtData1_TextChanged(object sender, EventArgs e)
        {

        }


        protected void Atualiza_Select_Vendedores()
        {
            DataTable Resultado = new DataTable();

            ObjVendedorClass.UsuCod = Session["usuario"].ToString();
            ObjVendedorClass.TodosCodigos = "S";
            Resultado = ObjVendedorClass.Consulta_Vendedor();



            VendedoresSelect.DataSource = Resultado;
            VendedoresSelect.DataTextField = "VendNome";
            VendedoresSelect.DataValueField = "VendCod";
            VendedoresSelect.DataBind();

        }

        protected void RecuperaDados_Select()
        {

            objPrincipal.VendCod = "";

            for (int i = 0; i < VendedoresSelect.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (VendedoresSelect.Items[i].Selected == true)
                {
                    objPrincipal.VendCod += VendedoresSelect.Items[i].Value + ",";
                }
            }


        }

       
    }
}