using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;

namespace VendasWeb.GerencialVendas
{
    public partial class FrmFretesCidades : System.Web.UI.Page
    {
        clsFretesEstado objFretes = new clsFretesEstado();
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
            }

        }


        protected void CidadeGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            CidadeGridView.PageIndex = e.NewPageIndex;
            BuscarLinkButton_Click1(null, null);
        }

        protected void BuscarLinkButton_Click1(object sender, EventArgs e)
        {

            objFretes = new clsFretesEstado();

            if (drpFiltro.SelectedValue == "2")
            {
                objFretes.CodigoCidade = txtFiltro.Text.ToString();
                objFretes.NomeCidade = "";
            }
            else
            {

                objFretes.CodigoCidade = "";
                objFretes.NomeCidade = txtFiltro.Text.ToString();
            }

            CidadeGridView.DataSource = objFretes.Lista_Cidade();
            CidadeGridView.DataBind();
            CidadeMultiView.Visible = true;
        }


        protected void PercentualTextBox_TextChanged(object sender, EventArgs e)
        {


            objFretes = new clsFretesEstado();
            objFretes.CodigoCidade = ((Label)((Control)sender).FindControl("CidCodLabel")).Text;
            // objFretes.PercentualFretes = Convert.ToInt32 (((Label)((Control)sender).FindControl("PercentualTextBox")).Text);

            if (((TextBox)((Control)sender).FindControl("PercentualTextBox")).Text != "")
            {
                objFretes.PercentualFretes = Convert.ToInt32(((TextBox)((Control)sender).FindControl("PercentualTextBox")).Text);

            }
            else
            {
                objFretes.PercentualFretes = 0;


            }

            objFretes.Altera_Percentual_Cidade();

        }

        protected void drpFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}