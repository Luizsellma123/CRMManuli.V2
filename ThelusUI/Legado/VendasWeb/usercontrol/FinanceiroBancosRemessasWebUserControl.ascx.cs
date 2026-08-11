using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class FinanceiroBancosRemessasWebUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LiberaNavegacao()
        {
            TitulosBancoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x";
            AdicionarTitulosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-calendar fa-3x";
        }
    }
}