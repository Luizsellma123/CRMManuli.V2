using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class AtividadeWebUserControl : System.Web.UI.UserControl
    {
        SessionClass objSessao = new SessionClass();
        grupos objGrupo = new grupos();
        SACClass ObjSAC = new SACClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (Session["AtividadesDetalhe"] != null)
            {
                ObjSAC = (SACClass)Session["AtividadesDetalhe"];
            }

            if (ObjSAC.Operacao != "Inclusao")
            {
                PrincipalLinkButton.Enabled = true;
                HistoricoLinkButton.Enabled = true;
                AnexoLinkButton.Enabled = true;
            }
        }

        protected void PrincipalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/AtividadesDetalheWebForm.aspx?indmnu=3");
        }

        protected void HistoricoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/AtividadesHistoricoWebForm.aspx?indmnu=3");
        }

        protected void AnexoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/AtividadesAnexoWebForm.aspx?indmnu=3");
        }
    }
}