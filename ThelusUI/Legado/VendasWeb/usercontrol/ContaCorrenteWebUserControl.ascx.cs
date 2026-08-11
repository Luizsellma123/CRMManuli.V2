using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class ContaCorrenteWebUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                //Redireciona para tela de login
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void PrincipalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/financeiro/ContaCorrenteDetalheWebForm.aspx?indmnu=3");
        }

        protected void ContasReceberLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/financeiro/ContasReceberWebForm.aspx?indmnu=3");

        }

        protected void ContasPagarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/financeiro/ContasPagarWebForm.aspx?indmnu=3");
        }

        protected void DevolucoesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/financeiro/DevolucoesWebForm.aspx?indmnu=3");
        }

        protected void PedidosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/financeiro/PedidosWebForm.aspx?indmnu=3");
        }
    }
}