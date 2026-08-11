using System;
using VendasWeb.classes;

namespace VendasWeb.usercontrol
{
    public partial class FechamentoFaturaWebUserControl : System.Web.UI.UserControl
    {
        SessionClass objSessao = new SessionClass();

        public void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            LogisticaClass objLogistica = new LogisticaClass();

            if (Session["Logistica"] != null)
                objLogistica = (LogisticaClass)Session["Logistica"];

            if (objLogistica.Operacao == "Alteracao")
                NotasFiscaisLinkButton.Enabled = true;
        }

        protected void PrincipalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/FechamentoFaturaDetalheWebForm.aspx?indmnu=3");
        }

        protected void NotasFiscaisLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/NotaFiscalWebForm.aspx?indmnu=3");
        }
    }
}