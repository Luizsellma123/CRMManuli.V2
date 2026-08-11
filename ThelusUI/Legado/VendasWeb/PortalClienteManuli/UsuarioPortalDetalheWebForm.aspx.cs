using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.PortalClienteManuli
{
    public partial class UsuarioPortalDetalheWebForm : System.Web.UI.Page
    {
        UsuarioPortalClass OBJusuario = new UsuarioPortalClass();
        UtilClass ObjUtilClass = new UtilClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica se tem usuário logado no Portal
            if (Session["usuarioPortal"] == null)
            {
                //Redireciona para tela de login
                Response.Redirect("LoginPortal.aspx");
            }

            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = "";
            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {

                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }


            if (!IsPostBack)
            {
                //Chama função para carregar dados na tela
                carregadadostela();
            }
        }

        public void carregadadostela() {

            OBJusuario = (UsuarioPortalClass)Session["usuarioPortal"];

            EmailTextBox.Text = OBJusuario.email.ToString();
            TelefoneTextBox.Text = OBJusuario.Telefone.ToString();
        }

        protected void SalvarButton_Click(object sender, EventArgs e)
        {
            string retorno = "";
            OBJusuario = (UsuarioPortalClass)Session["usuarioPortal"];

            OBJusuario.email = EmailTextBox.Text;
            OBJusuario.Telefone = TelefoneTextBox.Text;
            OBJusuario.senha = SenhaTextBox.Text;

            retorno = OBJusuario.Atualiza_Usuario();

            if (retorno == "")
            {
                string FaltaValores = "Dados atualizados com sucesso !";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(FaltaValores, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }else
            {
                string FaltaValores = "Erro na atualização, favor contatar o suporte!";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }

        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            //Redireciona para tela de login
            Response.Redirect("~/PortalClienteManuli/HomePortal.aspx");
        }
    }
}