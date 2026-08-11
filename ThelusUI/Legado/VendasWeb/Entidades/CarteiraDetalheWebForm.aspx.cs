using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;

namespace VendasWeb.Entidades
{
    public partial class CarteiraDetalheWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        CarteiraClasse OBJCarteira = new CarteiraClasse();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            int IDCliente = Convert.ToInt32(Request.QueryString["IDCliente"].ToString());

        }


    }
}