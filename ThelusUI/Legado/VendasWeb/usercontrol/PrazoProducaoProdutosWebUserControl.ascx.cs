using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class PrazoProducaoProdutosWebUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void PrincipalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/PrazoProducaoProdutosWebForm.aspx?indmnu=3");
        }

        protected void CarregaPrazosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/CarregaPrazoProducaoProdutosWebForm.aspx?indmnu=3");
        }
    }
}