using System;
using System.Collections.Generic;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.logistica
{
    public partial class detalhePedido : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();
        }
    }
}