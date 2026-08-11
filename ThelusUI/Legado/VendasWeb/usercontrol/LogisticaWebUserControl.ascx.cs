using System;
using System.Data;
using VendasWeb.classes;

namespace VendasWeb.usercontrol
{
    public partial class LogisticaWebUserControl : System.Web.UI.UserControl
    {
        SessionClass objSessao = new SessionClass();
        ClienteClasse OBJCliente = new ClienteClasse();
        grupos objGrupo = new grupos();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            RastreioPedidosLinkButton.Enabled = true;

            int AdministradorLogistica = RetornaAdministrador();

            if (AdministradorLogistica == 1 || AdministradorLogistica == 0)
            {
                FechamentoFaturaLinkButton.Enabled = true;

                if (AdministradorLogistica == 1)
                {
                    StatusFechamentoFaturaLinkButton.Enabled = true;

                    CadastroTransportadorLinkButton.Enabled = true;
                }
            }

            SimuladorFreteLinkButton.Enabled = true;

            VerificaSeVemTelaControladoria();
        }

        protected void VerificaSeVemTelaControladoria()
        {
            if (Session["VemTelaControladoriaSimuladorFrete"] != null)
            {
                if (Session["VemTelaControladoriaSimuladorFrete"].ToString() == "Sim")
                {
                    HomeLinkButton.Enabled = false;

                    FechamentoFaturaLinkButton.Enabled = false;

                    StatusFechamentoFaturaLinkButton.Enabled = false;

                    RastreioPedidosLinkButton.Enabled = false;

                    CadastroTransportadorLinkButton.Enabled = false;
                }
            }
        }

        protected int RetornaAdministrador()
        {
            DataTable ValidaAcessoDataTable = new DataTable();

            objGrupo.Filtro = Session["IDUsuario"].ToString();

            ValidaAcessoDataTable = objGrupo.ListaUsuariosGrupos();

            if (ValidaAcessoDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in ValidaAcessoDataTable.Rows)
                {
                    if (row["NomeGrupo"].ToString() == "Administração Logística")
                        return 1;
                }

                foreach (DataRow row in ValidaAcessoDataTable.Rows)
                {
                    if (row["NomeGrupo"].ToString() == "Logística")
                        return 0;
                }
            }

            return 2;
        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/HomeWebForm.aspx?indmnu=3");
        }

        protected void LinkButtonAtualizar_Click(object sender, EventArgs e)
        {
            OBJCliente.AtualizacaoGeral();
            Response.Redirect("~/Logistica_New/HomeWebForm.aspx?indmnu=3");
        }

        protected void FechamentoFaturaLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/FechamentoFaturaWebForm.aspx?indmnu=3");
        }

        protected void StatusFechamentoFaturaLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/StatusFechamentoFaturaWebForm.aspx?indmnu=3");
        }

        protected void RastreioPedidosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/RastreioPedidosWebForm.aspx?indmnu=3");
        }

        protected void CadastroTransportadorLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/CadastroTransportadorWebForm.aspx?indmnu=3");
        }

        protected void SimuladorFreteLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/SimuladorFreteWebForm.aspx?indmnu=3");
        }
    }
}