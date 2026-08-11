using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;

namespace VendasWeb.usercontrol
{
    public partial class TicketAtividadeWebUserControl : System.Web.UI.UserControl
    {
        SessionClass objSessao = new SessionClass();
        grupos objGrupo = new grupos();
        SACClass ObjSAC = new SACClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (Session["TicketsDetalhe"] != null)
            {
                ObjSAC = (SACClass)Session["TicketsDetalhe"];
            }

            if (ObjSAC.Operacao != "InclusaoAtividade")
            {
                DesbloqueiaButtons();
            }
        }

        public void DesbloqueiaButtons()
        {
            PrincipalLinkButton.Enabled = true;
            HistoricoLinkButton.Enabled = true;
            AnexoLinkButton.Enabled = true;
        }

        protected void PrincipalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/TicketsAtividadesDetalheWebForm.aspx?indmnu=3");
        }

        protected void HistoricoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/TicketsAtividadesHistoricoWebForm.aspx?indmnu=3");
        }

        protected void AnexoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/TicketsAtividadesAnexoWebForm.aspx?indmnu=3");
        }
    }
}