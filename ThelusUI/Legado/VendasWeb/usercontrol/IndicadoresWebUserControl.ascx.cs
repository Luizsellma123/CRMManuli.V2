using System;
using System.Collections.Generic;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class IndicadoresWebUserControl : System.Web.UI.UserControl
    {
        SessionClass objSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();
        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Indicadores/HomeWebForm.aspx?indmnu=3");
        }

        protected void TILinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Indicadores/TIWebForm.aspx?indmnu=3");
        }

        protected void SAMLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Indicadores/SAMWebForm.aspx?indmnu=3");
        }
    }
}