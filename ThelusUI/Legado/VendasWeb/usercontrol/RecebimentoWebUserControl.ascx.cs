using System;
using VendasWeb.classes;

namespace VendasWeb.usercontrol
{
    public partial class RecebimentoWebUserControl : System.Web.UI.UserControl
    {
        SessionClass objSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();
        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Recebimento/HomeWebForm.aspx?indmnu=3");
        }

        protected void NovoLinkButton_Click(object sender, EventArgs e)
        {
            Session["objRecebimento"] = null;

            Response.Redirect("~/Recebimento/DetalheWebForm.aspx?indmnu=3");
        }

        protected void ListaLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Recebimento/ListaWebForm.aspx?indmnu=3");
        }
    }
}