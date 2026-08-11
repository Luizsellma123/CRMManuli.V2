using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.WEBServiceSAP;

namespace VendasWeb.usercontrol
{
    public partial class CadastroUsuarioWebUserControl : System.Web.UI.UserControl
    {
        usuario OBJUsuario = new usuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Recupera objeto usuário da sessao do usuário
            if (Session["AdministrcaoUsuario"] != null)
            {
                OBJUsuario = (usuario)Session["AdministrcaoUsuario"];
            }

            if (!IsPostBack)
            {
                TrataAcessos();
            }
        }

        public void TrataAcessos()
        {
            if (OBJUsuario.Operacao == "inclusao")
            {
                VendedoresLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-user-plus fa-3x disabled";
                SetoresLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-list fa-3x disabled";
                TiposVendedorLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-exchange fa-3x disabled";
                GruposLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x disabled";
                MenusLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x disabled";
                EmpresasLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x disabled";
                SAPLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x disabled";
            }
            else
            {
                VendedoresLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-user-plus fa-3x";
                SetoresLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-list fa-3x";
                TiposVendedorLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-exchange fa-3x";
                GruposLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x";
                MenusLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x";
                EmpresasLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x";
                SAPLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-bars fa-3x";
            }

        }

        protected void EmpresasLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/CadastroUsuarioEmpresasWebForm.aspx?indmnu=2");
        }

        protected void VendedoresLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/CadastroUsuarioVendedoresWebForm.aspx?indmnu=2");
        }

        protected void PrincipalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/CadastroUsuarioWebForm.aspx?indmnu=2");
        }

        protected void SetoresLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/CadastroUsuarioSetoresWebForm.aspx?indmnu=2");
        }

        protected void TiposVendedorLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/CadastroUsuarioTipoVendedorWebForm.aspx?indmnu=2");
        }

        protected void GruposLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/ListaGruposUsuariosWebForm.aspx?indmnu=2");
        }

        protected void MenusLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/ListaMenusUsuariosWebForm.aspx?indmnu=2");
        }

        protected void SAPLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AdministracaoSistema/CadastroUsuariosSAPWebForm.aspx?indmnu=2");
        }

        protected void AtualizarLinkButton_Click(object sender, EventArgs e)
        {
            ClienteClasse OBJCliente = new ClienteClasse();
            OBJCliente.AtualizacaoGeral();
            Response.Redirect("../AdministracaoSistema/CadastroUsuarioWebForm.aspx?indmnu=2");
        }
    }
}