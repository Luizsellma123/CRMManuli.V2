using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class AdmVendasWebUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoVendas/HomeWebForm.aspx?indmnu=3");
        }

        protected void LiberaPedidoProducaoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoVendas/LiberaPedidoProducaoWebForm.aspx?indmnu=3");
        }

        protected void TabelaPrecoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TabelaDePreco/TabelaDePrecoWebForm.aspx?indmnu=3");
        }

        protected void ClassificacaoComercialLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoVendas/ClassificacaoComercialWebForm.aspx?indmnu=3");
        }

        protected void PrazosProduçãoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoVendas/PrazosProducaoWebForm.aspx?indmnu=3");
        }

    }
}