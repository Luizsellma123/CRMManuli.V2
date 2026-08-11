using System;
using VendasWeb.classes;

namespace VendasWeb.usercontrol
{
    public partial class AnaliseCreditoDetalheWebUserControl : System.Web.UI.UserControl
    {
        SessionClass objSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();
        }

        protected void PrincipalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes/AnaliseCreditoDetalheWebForm.aspx?indmnu=5");
        }

        protected void ScoreSerasaLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes/AnaliseCreditoScoreWebForm.aspx?indmnu=5");
        }

        protected void GrafiasSemelhantesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes/AnaliseCreditoGrafiasSemelhantesWebForm.aspx?indmnu=5");
        }

        protected void QuadroSociosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes/AnaliseCreditoQuadroSocialWebForm.aspx?indmnu=5");
        }

        protected void AdministracaoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes/AnaliseCreditoAdministracaoWebForm.aspx?indmnu=5");
        }

        protected void ConsultaSerasaLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes/AnaliseCreditoConsultasSerasaWebForm.aspx?indmnu=5");
        }

        protected void HistoricoPagamentosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes/AnaliseCreditoHistoricoPagamentosWebForm.aspx?indmnu=5");
        }

        protected void EvolucaoCompromissosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes/AnaliseCreditoEvolucaoCompromissosWebForm.aspx?indmnu=5");
        }

        protected void ReferenciaisDeNegociosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes/AnaliseCreditoReferencialNegociosWebForm.aspx?indmnu=5");
        }

        protected void AnotacoesNegativasLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes/AnaliseCreditoAnotacoesNegativasWebForm.aspx?indmnu=5");
        }

        protected void AnaliseCreditoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes/AnaliseCreditoWebForm.aspx?indmnu=5");
        }

        protected void CENPROTLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes/AnaliseCreditoCENPROTWebForm.aspx?indmnu=5");
        }
    }
}