using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class InfraestruturaWebUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Infraestrutura/InfraestruturaHomeWebForm.aspx?indmnu=5");
        }

        protected void PainelLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Infraestrutura/PainelWebForm.aspx?indmnu=5");
        }        
    }
}