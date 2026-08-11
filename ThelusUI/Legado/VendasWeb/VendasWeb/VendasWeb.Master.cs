using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb
{
    public partial class VendasWeb : System.Web.UI.MasterPage
    {
        string prefix = "";
        string Caminho = "";
        protected void Page_Load(object sender, EventArgs e)
        {       
            
            int varmenu = Convert.ToInt32(Request.QueryString["indmnu"]);
            if (Session["usuario"] == null && varmenu != 0 && varmenu < 99)
            {
                //Redireciona para tela de login
                Response.Redirect("~/Default.aspx"); 
            }


            if (varmenu > 1)
                prefix = "../";

            if (varmenu == 10)
                prefix = "../../../";

            if (varmenu == 11)
                prefix = "../../";

            Caminho = prefix + "img/Logo.png ";
            ltlLogo.Text = "<img src=" + Caminho + "height=\"60\" width=\"200\" alt=\"logo\"/>";
                                            
        }
    }
}