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
    public partial class PosicaoDiariaWebUserControl : System.Web.UI.UserControl
    {
        SessionClass objSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        { 
            //Valida Acesso
            objSessao.ValidaAcesso();
        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/PosicaoFinanceiraResumoWebForm.aspx?indmnu=3");
        }

        protected void FaturadosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/PosicaoFinanceiraFaturadosWebForm.aspx?indmnu=3");
        }

        protected void PendentesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/PosicaoFinanceiraPendentesWebForm.aspx?indmnu=3");
        }

        protected void DevolucoesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/PosicaoFinanceiraDevolucoesWebForm.aspx?indmnu=3");
        }
    }
}