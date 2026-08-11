using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace VendasWeb.GerencialVendas
{
    public partial class FrmFretesEstados : System.Web.UI.Page
    {
        clsEstadosFretes objEstado = new clsEstadosFretes();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {


                /*Tratar Abrir e fechar Div*/
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                this.ControlPainel.refreshVendedor();

                OrigemDropDown.DataSource = objEstado.Retorna_Estado();
                OrigemDropDown.DataTextField = "UfNome";
                OrigemDropDown.DataValueField = "UfSigla";
                OrigemDropDown.DataBind();


                OrigemDropDown.DataSource = objEstado.Retorna_Estado();
                OrigemDropDown.DataTextField = "UfNome";
                OrigemDropDown.DataValueField = "UfSigla";
                OrigemDropDown.DataBind();

            }

        }

        protected void EstadoGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            EstadosGridView.PageIndex = e.NewPageIndex;
            BuscarLinkButton_Click1(null, null);
        }

        protected void BuscarLinkButton_Click1(object sender, EventArgs e)
        {

            objEstado = new clsEstadosFretes();


            objEstado.EstadoOrigem = OrigemDropDown.SelectedValue.ToString();
            objEstado.EstadoDestino = OrigemDropDown.SelectedValue.ToString();


            EstadosGridView.DataSource = objEstado.Lista_Estados();
            EstadosGridView.DataBind();
            EstadosMultiView.Visible = true;
        }


        protected void PercentualTextBox_TextChanged(object sender, EventArgs e)
        {


            objEstado = new clsEstadosFretes();
            objEstado.EstadoOrigem = ((Label)((Control)sender).FindControl("OrigemCodCLabel")).Text;
            objEstado.EstadoDestino = ((Label)((Control)sender).FindControl("DestinoNomeLabel")).Text;

            // objFretes.PercentualFretes = Convert.ToInt32 (((Label)((Control)sender).FindControl("PercentualTextBox")).Text);

            if (((TextBox)((Control)sender).FindControl("PercentualTextBox")).Text != "")
            {
                objEstado.PercentualDesconto = Convert.ToInt32(((TextBox)((Control)sender).FindControl("PercentualTextBox")).Text);

            }
            else
            {
                objEstado.PercentualDesconto = 0;


            }

            objEstado.Altera_Percentual_Estado();

        }


        protected void drpFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }

}