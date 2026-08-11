using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class PortalClienteMenuWebUserControl : System.Web.UI.UserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            GerencialVendas.UsuarioPortalClass OBJusuario = new GerencialVendas.UsuarioPortalClass();

            OBJusuario = (GerencialVendas.UsuarioPortalClass)Session["usuarioPortal"];

            Labelnome.Text = OBJusuario.nome.ToString();
        }
    }
}