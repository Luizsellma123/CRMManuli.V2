using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.AdministracaoSistema;

namespace VendasWeb.usercontrol
{
    public partial class WebUserControlAdministracaoSistema : System.Web.UI.UserControl
    {
        ClienteClasse OBJCliente = new ClienteClasse();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void HomeLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoSistema/AdministracaoHomeWebform.aspx?indmnu=5");
        }

        protected void CadastroUsuarioLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoSistema/ListaCadastroUsuarioWebForm.aspx?indmnu=5");
        }

        protected void LinkButtonAtualizar_Click(object sender, EventArgs e)
        {
            OBJCliente.AtualizacaoGeral();
            Response.Redirect("../AdministracaoSistema/AdministracaoHomeWebForm.aspx?indmnu=2");
        }

        protected void RestartPoolCRMAPILinkButton_Click(object sender, EventArgs e)
        {
            WEBServiceCRM.FuncoesAPIClass OBJApi = new WEBServiceCRM.FuncoesAPIClass();
            OBJApi.ReiniciarCRMAPI();
        }

        protected void CadatroGruposLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoSistema/ListaCadastroGruposWebForm.aspx?indmnu=5");
        }

        protected void CadastroMenusLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoSistema/ListaCadastroMenusWebForm.aspx?indmnu=5");
        }

        protected void CadastroSetoresLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoSistema/ListaCadastroSetoresWebForm.aspx?indmnu=5");
        }

        protected void ModulosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoSistema/ListaCadastroModulosWebForm.aspx?indmnu=5");
        }

        protected void ParametrosGeraisLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoSistema/ParametrosGeraisWebForm.aspx?indmnu=5");
        }

        protected void AtualizarStoredProceduresLinkButton_Click(object sender, EventArgs e)
        {
            SQL.Atualizar objAtualizar = new SQL.Atualizar();
            string erro = objAtualizar.AtualizarStoredProcedures();
            Session["Msg"] = erro == "" ? null : erro;
            HomeLinkButton_Click(null, null);
        }
    }
}