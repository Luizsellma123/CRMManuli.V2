using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class FinanceiroBancosWebUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LiberaRemessasRetorno()
        {
            NovaRemessaBancariaLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x";
            //RetornoBancarioLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x";
        }

        protected void RemessasBancariasLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/financeiro/ListaRemessasWebForm.aspx?indmnu=3");
        }

        protected void NovaRemessaBancariaLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/financeiro/RemessaBancariaWebForm.aspx?indmnu=3");
        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/financeiro/HomeFinanceiroWebForm.aspx?indmnu=5");
        }
    }
}