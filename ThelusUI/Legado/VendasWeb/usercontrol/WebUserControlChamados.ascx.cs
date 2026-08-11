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
    public partial class WebUserControlChamados : System.Web.UI.UserControl
    {
        ChamadoClass OBJChamado = new ChamadoClass();
        usuario Objusuario = new usuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Recupera usuario
            if (Session["usuario"] != null)
            {
                Objusuario.CodigoUsuario = Session["usuario"].ToString();
            }

            if (!IsPostBack)
            {
                TrataAcessos();
            }

        }

        protected void NovoChamadoLinkButton_Click(object sender, EventArgs e)
        {
            //Limpa Session no caso de um novo chamado
            Session["OBJChamado"] = null;
            Response.Redirect("~/Chamados/ChamadoPrincipalWebForm.aspx?indmnu=5");
        }

        public void TrataAcessos()
        {
            //Consulta grupos
            Objusuario.ConsultaGrupos("Ativo");

            //grupo Administracao Chamados	
            //Aba Gerenciar chamados somente liberada para Key User
            if (Objusuario.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 13).Count() > 0)
            {
                ImportarChamadosLinkButton.Visible = true;
                //ImportarChamadosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-cog fa-3x";
            }
            else
            {
                ImportarChamadosLinkButton.Visible = false;
                //ImportarChamadosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-cog fa-3x disabled";
            }

            //grupo Suporte Chamados
            //Aba Suporte somente liberado a usuários do suporte
            //if (Objusuario.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 14).Count() > 0)
            //{
            //    SuporteChamadosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-cog fa-3x";
            //}
            //else
            //{
            //    SuporteChamadosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-cog fa-3x disabled";
            //}
        }

        protected void ChamadosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Chamados/ListaChamadosWebForm.aspx?indmnu=5");
        }

        protected void ImportarChamadosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Chamados/ImportarChamadosWebForm.aspx?indmnu=5");
        }
    }
}