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
    public partial class SACWebUserControl : System.Web.UI.UserControl
    {
        SessionClass objSessao = new SessionClass();
        grupos objGrupo = new grupos();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (RetornaAdministrador("Administração SAC") == 0)
            {
                SituacoesTicketsLinkButton.Enabled = false;
                SituacoesAtividadesLinkButton.Enabled = false;
                ClassificacaoLinkButton.Enabled = false;
                PrioridadeLinkButton.Enabled = false;
                CadastroSolucaoLinkButton.Enabled = false;
                CadastroTipoOcorrenciaLinkButton.Enabled = false;
                CadastroMotivoLinkButton.Enabled = false;

                if (RetornaAdministrador("Atendimento Cliente") == 0)
                    TicketsLinkButton.Enabled = false;
            }
        }

        protected int RetornaAdministrador(string NomeGrupo)
        {
            objGrupo.Status = "";
            objGrupo.Filtro = NomeGrupo;

            DataTable ValidaAcessoDataTable = new DataTable();

            ValidaAcessoDataTable = objGrupo.ListaGrupos();

            if (ValidaAcessoDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in ValidaAcessoDataTable.Rows)
                {
                    objGrupo.IDGrupo = Convert.ToInt32(row["IDGrupo"].ToString());
                }
            }

            objGrupo.Filtro = Session["IDUsuario"].ToString();

            ValidaAcessoDataTable = objGrupo.ListaUsuariosGrupos();

            if (ValidaAcessoDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in ValidaAcessoDataTable.Rows)
                {
                    if (NomeGrupo == "Administração SAC")
                        return 1;
                    else
                        return Convert.ToInt32(row["Administrador"]);
                }
            }

            return 0;
        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/HomeSACWebForm.aspx?indmnu=3");
        }

        protected void AtividadesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/AtividadesWebForm.aspx?indmnu=3");
        }

        protected void TicketsLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/TicketsWebForm.aspx?indmnu=3");
        }

        protected void SituacoesTicketsLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/TicketsSACWebForm.aspx?indmnu=3");
        }

        protected void SituacoesAtividadesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/SituacoesAtividadesWebForm.aspx?indmnu=3");
        }

        protected void ClassificacaoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/ClassificacaoWebForm.aspx?indmnu=3");
        }

        protected void PrioridadeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/PrioridadeWebForm.aspx?indmnu=3");
        }

        protected void CadastroSolucaoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/CadastroSolucaoWebForm.aspx?indmnu=3");
        }

        protected void CadastroTipoOcorrenciaLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/CadastroTipoOcorrenciaWebForm.aspx?indmnu=3");
        }

        protected void CadastroMotivoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/CadastroMotivoWebForm.aspx?indmnu=3");
        }
    }
}