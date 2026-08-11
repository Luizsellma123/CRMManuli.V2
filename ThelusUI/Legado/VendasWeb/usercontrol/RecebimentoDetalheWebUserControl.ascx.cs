using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;

namespace VendasWeb.usercontrol
{
    public partial class RecebimentoDetalheWebUserControl : System.Web.UI.UserControl
    {
        RecebimentoClass objRecebimento = new RecebimentoClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) TrataAcessos();
        }

        public void TrataAcessos()
        {
            if (Session["objRecebimento"] != null)
                objRecebimento = (RecebimentoClass)Session["objRecebimento"];


            if (objRecebimento.IDRecebimento != 0)
                LiberaNavegacao();
            else
                BloqueiaNavegacao();
        }

        public void LiberaNavegacao()
        {
            AnexosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-list fa-3x";

            HistoricoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-binoculars fa-3x";
        }

        public void BloqueiaNavegacao()
        {
            AnexosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-list fa-3x disabled";

            HistoricoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-binoculars fa-3x disabled";
        }

        protected void PrincipalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Recebimento/DetalheWebForm.aspx?indmnu=5");
        }
        protected void HistoricoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Recebimento/HistoricoWebForm.aspx?indmnu=5");
        }

        protected void AnexosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Recebimento/AnexosWebForm.aspx?indmnu=5");
        }
    }
}