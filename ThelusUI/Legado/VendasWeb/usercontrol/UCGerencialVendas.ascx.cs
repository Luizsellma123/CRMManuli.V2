using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;

namespace VendasWeb.usercontrol
{
    public partial class UCGerencialVendas : System.Web.UI.UserControl
    {
        ClienteClasse OBJCliente = new ClienteClasse();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/GerencialVendas/HomeGerencialWebForm.aspx?indmnu=2");
        }

        protected void SimuladorPrecosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/GerencialVendas/FrmListaSimuladorForm.aspx?indmnu=3");
        }

        protected void AcompanhamentoPedidoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Entidades/AcompanhamentoVendasWebForm.aspx?indmnu=3");
        }

        protected void TabelaPrecoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Entidades/TabelaPrecoWebForm.aspx?indmnu=3");
        }

        protected void TrocaCarteiraLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/GerencialVendas/AlterarCarteiraWebForm.aspx?indmnu=3");
        }

        protected void LinkButtonAtualizar_Click(object sender, EventArgs e)
        {
            OBJCliente.AtualizacaoGeral();
        }
    }
}