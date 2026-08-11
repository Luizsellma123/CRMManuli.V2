using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class CadastroGrupoWebUserControl : System.Web.UI.UserControl
    {
        grupos objGrupo = new grupos();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Recupera objeto grupo da sessao do usuário
            if (Session["AdministracaoGrupo"] != null)
            {
                objGrupo = (grupos)Session["AdministracaoGrupo"];
            }

            if (!IsPostBack)
            {
                TrataAcessos();
            }
        }

        public void TrataAcessos()
        {
            if (objGrupo.Operacao == "inclusao")
            {
                UsuariosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x disabled";
                MenusLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x disabled";
            }
            else
            {
                UsuariosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x";
                MenusLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x";

            }
        }

        protected void PrincipalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/CadastroGrupoWebForm.aspx?indmnu=2");
        }

        protected void UsuariosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/ListaUsuariosGruposWebForm.aspx?indmnu=2");
        }

        protected void MenusLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/ListaMenusGruposWebForm.aspx?indmnu=2");
        }
    }
}