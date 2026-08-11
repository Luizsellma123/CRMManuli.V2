using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class WebUserControlControladoria : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                //Redireciona para tela de login
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void PerfilComercialLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/PeriodoPedidosWebForm.aspx?indmnu=3");
        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/HomeControladoriaWebForm.aspx?indmnu=3");
        }

        protected void CadastroPSIULinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/CadastroPSIU.aspx?indmnu=3");
        }

        protected void SimuladorParametros_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/SimuladorParametrosWebForm.aspx?indmnu=3");
        }

        protected void FreteLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/FreteWebForm.aspx?indmnu=3");
        }

        protected void ListaSimulador_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/ListaSimuladorControladoria.aspx?indmnu=3");
        }

        protected void Simulacao_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/SimuladorConsultaControladoriaWebForm.aspx?indmnu=3");
        }

        protected void AtualizacaoCustosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/AtualizacaoCustosWebForm.aspx?indmnu=3");
        }

        protected void ConsultaCustosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/ConsultaCustosWebForm.aspx?indmnu=3");
        }

        protected void PeriodoSimulacaoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/PeriodoSimulacaoWebForm.aspx?indmnu=3");
        }

        protected void RelatorioAtendimentoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/RelatorioAtendimentosWebForm.aspx?indmnu=3");
        }

        protected void EmpenhoEstoqueLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/EmpenhoEstoqueWebForm.aspx?indmnu=3");
        }

        protected void PosicaoFinanceiraLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/PosicaoFinanceiraWebForm.aspx?indmnu=3");
        }

        protected void SimuladorFreteLinkButton_Click(object sender, EventArgs e)
        {
            Session["VemTelaControladoriaSimuladorFrete"] = "Sim";

            Response.Redirect("~/Logistica_New/SimuladorFreteWebForm.aspx?indmnu=3");            
        }
    }
}