using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.Infraestrutura
{
    public partial class InfraestruturaHomeWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            OBJSessao.ValidaAcesso();
        }
    }
}