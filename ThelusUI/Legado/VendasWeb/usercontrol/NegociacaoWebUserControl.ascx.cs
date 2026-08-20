using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class NegociacaoWebUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                //Redireciona para tela de login
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/financeiro/HomeFinanceiroWebForm.aspx?indmnu=3");
        }

        //protected void Simulacao_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/financeiro/LiberacaoPedidosWebForm.aspx?indmnu=3");
        //}

        protected void NegociacaoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Negociacao/NegociacaoDetalheWebForm.aspx?indmnu=3");
        }
    }
}