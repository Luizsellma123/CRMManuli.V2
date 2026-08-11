using System;
using VendasWeb.classes;

namespace VendasWeb.usercontrol
{
    public partial class CadastroTransportadorWebUserControl : System.Web.UI.UserControl
    {
        SessionClass objSessao = new SessionClass();
        grupos objGrupo = new grupos();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();
        }

        public void LiberaMenus(bool enabled)
        {
            FornecedorLinkButton.Enabled = enabled;

            RegiaoLinkButton.Enabled = enabled;

            ParametrosLinkButton.Enabled = enabled;
        }

        protected void PrincipalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/CadastroTransportadorDetalheWebForm.aspx?indmnu=3");
        }

        protected void FornecedorLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/CadastroTransportadorFornecedorWebForm.aspx?indmnu=3");
        }

        protected void RegiaoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/CadastroTransportadorRegiaoWebForm.aspx?indmnu=3");
        }

        protected void ParametrosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/CadastroTransportadorParametrosWebForm.aspx?indmnu=3");
        }
    }
}