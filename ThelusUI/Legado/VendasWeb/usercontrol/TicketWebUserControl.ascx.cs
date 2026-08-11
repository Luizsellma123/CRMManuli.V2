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
    public partial class TicketWebUserControl : System.Web.UI.UserControl
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

            if (ObjSAC.Operacao == "Inclusao")
            {
                BloqueiaButtons();
            }
            else
            {
                LiberaButtons();
            }
        }

        protected void PrincipalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/TicketsDetalheWebForm.aspx?indmnu=3");
        }

        protected void ContatosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/TicketsContatosWebForm.aspx?indmnu=3");
        }

        protected void AtividadesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/TicketsAtividadesWebForm.aspx?indmnu=3");
        }

        protected void HistoricoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/TicketsHistoricoWebForm.aspx?indmnu=3");
        }

        protected void AnexoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/TicketsAnexoWebForm.aspx?indmnu=3");
        }

        protected void NotasFiscaisLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/NotasFiscaisWebForm.aspx?indmnu=3");
        }

        public void LiberaButtons()
        {
            PrincipalLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-home fa-3x";
            ContatosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x";
            AtividadesLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-list-ul fa-3x";
            HistoricoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-edit fa-3x";
            AnexoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x";
            NotasFiscaisLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x";
        }

        public void BloqueiaButtons()
        {
            PrincipalLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-home fa-3x disabled";
            ContatosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x disabled";
            AtividadesLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-list-ul fa-3x disabled";
            HistoricoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-edit fa-3x disabled";
            AnexoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x disabled";
            NotasFiscaisLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x disabled";
        }

    }
}