using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class CadastroMenuWebUserControl : System.Web.UI.UserControl
    {
        menu objMenu = new menu();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Recupera objeto grupo da sessao do usuário
            if (Session["AdministracaoMenu"] != null)
            {
                objMenu = (menu)Session["AdministracaoMenu"];
            }

            if (!IsPostBack)
            {
                TrataAcessos();
            }
        }

        public void TrataAcessos()
        {
            if (objMenu.Operacao == "inclusao")
            {
                UsuariosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x disabled";
                GruposLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x disabled";
            }
            else
            {
                UsuariosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x";
                GruposLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x";
            }
        }

        protected void PrincipalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/CadastroMenuWebForm.aspx?indmnu=2");
        }

        protected void UsuariosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/ListaUsuariosMenuWebForm.aspx?indmnu=2");
        }

        protected void GruposLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/ListaGruposMenuWebForm.aspx?indmnu=2");
        }
    }
}