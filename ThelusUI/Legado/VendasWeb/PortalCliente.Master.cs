using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb
{
    public partial class PortalCliente : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica se tem usuário logado no Portal
            if (Session["usuarioPortal"] == null)
            {
                //Redireciona para tela de login
                Response.Redirect(ResolveUrl("~/PortalClienteManuli/LoginPortal.aspx"));
            }

        }
    }
}