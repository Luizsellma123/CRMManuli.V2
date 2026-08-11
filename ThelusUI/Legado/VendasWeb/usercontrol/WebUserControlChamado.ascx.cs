using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.WEBServiceSAP;

namespace VendasWeb.usercontrol
{
    public partial class WebUserControlChamado : System.Web.UI.UserControl
    {
        ChamadoClass OBJChamado = new ChamadoClass();
        CrmGrupoUsuarioClass GruposUsuario = new CrmGrupoUsuarioClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) TrataAcessos();
        }

        public void TrataAcessos()
        {
            if (Session["OBJChamado"] != null)
            {
                OBJChamado = (ChamadoClass)Session["OBJChamado"];

                if (OBJChamado.NumeroChamado != 0)
                    LiberaNavegacao();
            }
        }

        public void LiberaNavegacao()
        {
            ParametroGeral objParametroGeral = new ParametroGeral();

            //Consulta Grupo de Suporte
            {
                ConsultaGruposUsuario(objParametroGeral.RetornaValorNumericoParametro("GRUPOCHAMADOSSUPORTE"));

                if (GruposUsuario != null)
                {
                    if (Convert.ToBoolean(GruposUsuario.Administrador))
                    {
                        ResponsaveisLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square fa-3x";

                        ApontamentoHorasLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-clock-o fa-3x";
                    }
                    else
                    {
                        ResponsaveisLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square fa-3x disabled";

                        ApontamentoHorasLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-clock-o fa-3x disabled";
                    }
                }
            }

            //Consulta Grupo de Projetos
            {
                ConsultaGruposUsuario(objParametroGeral.RetornaValorNumericoParametro("GRUPOPROJETOS"));

                if (GruposUsuario != null)
                {
                    if (Convert.ToBoolean(GruposUsuario.Administrador))
                        ProjetoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-trello fa-3x";
                    else
                        ProjetoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-trello fa-3x disabled";
                }
            }

            AnexosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-list fa-3x";

            HistoricoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-binoculars fa-3x";
        }

        public void BloqueiaNavegacao()
        {
            ResponsaveisLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-pencil-square fa-3x disabled";

            AnexosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-list fa-3x disabled";

            HistoricoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-binoculars fa-3x disabled";

            ProjetoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-trello fa-3x disabled";
        }

        public void ConsultaGruposUsuario(int IDGrupo)
        {
            usuario Objusuario = new usuario();

            if (Session["usuario"] != null)
                Objusuario.CodigoUsuario = Session["usuario"].ToString();

            GruposUsuario = Objusuario.ConsultaGrupos("Ativo", IDGrupo);
        }

        protected void PrincipalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Chamados/ChamadoPrincipalWebForm.aspx?indmnu=5");
        }

        protected void ProjetoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Chamados/ChamadoProjetoWebForm.aspx?indmnu=5");
        }

        protected void HistoricoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Chamados/ChamadoHistoricoWebForm.aspx?indmnu=5");
        }

        protected void AnexosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Chamados/ChamadoAnexosWebForm.aspx?indmnu=5");
        }

        protected void ResponsaveisLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Chamados/ChamadoResponsaveisWebForm.aspx?indmnu=5");
        }

        protected void ApontamentoHorasLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Chamados/ChamadoApontamentoHorasWebForm.aspx?indmnu=5");
        }
    }
}