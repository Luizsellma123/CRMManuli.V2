using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class InfraestruturaMaquinaWebUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void InformacoesGeraisLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Infraestrutura/InfoPCWebForm.aspx?indmnu=5");
        }

        protected void RAMLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Infraestrutura/RAMWebForm.aspx?indmnu=5");
        }

        protected void DiscosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Infraestrutura/DiscosWebForm.aspx?indmnu=5");
        }

        protected void ProcessosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Infraestrutura/ProcessosWebForm.aspx?indmnu=5");
        }

        protected void ProgramasLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Infraestrutura/ProgramasWebForm.aspx?indmnu=5");
        }

        protected void EmailLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Infraestrutura/EmailWebForm.aspx?indmnu=5");
        }       
    }
}