using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class ProducaoWebUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/HomeProducaoWebForm.aspx?indmnu=3");
        }

        protected void OrdensDeServicoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/OrdensDeServicoWebForm.aspx?indmnu=3");
        }

        protected void ProdutosRelacionaisLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/ProdutosRelacionaisWebForm.aspx?indmnu=3");
        }

        protected void StatusOrdemServicoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/StatusOrdemServicoWebForm.aspx?indmnu=3");
        }

        protected void PrioridadeOrdensServicoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/PrioridadeOrdensServicoWebForm.aspx?indmnu=3");
        }

        protected void PrazoProducaoGruposLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/OrdensDeServicoPrazosWebForm.aspx?indmnu=3");
        }

        protected void PrazoProducaoProdutosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/PrazoProducaoProdutosWebForm.aspx?indmnu=3");
        }
    }
}