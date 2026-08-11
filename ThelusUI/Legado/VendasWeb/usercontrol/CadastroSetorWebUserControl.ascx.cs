using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class CadastroSetorWebUserControl : System.Web.UI.UserControl
    {
        setor objSetor = new setor();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Recupera objeto grupo da sessao do usuário
            if (Session["AdministracaoSetor"] != null)
            {
                objSetor = (setor)Session["AdministracaoSetor"];
            }

            if (!IsPostBack)
            {
                TrataAcessos();
            }
        }

        public void TrataAcessos()
        {
            if (objSetor.Operacao == "inclusao")
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
            Response.Redirect("../AdministracaoSistema/CadastroSetorWebForm.aspx?indmnu=2");
        }

        protected void UsuariosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/ListaUsuariosSetorWebForm.aspx?indmnu=2");
        }

        protected void GruposLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/ListaGruposSetorWebForm.aspx?indmnu=2");
        }

    }
}