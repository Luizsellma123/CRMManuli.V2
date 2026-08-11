using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;

namespace VendasWeb.Usuarios
{
    public partial class UsuarioPerfilWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        usuario OBJUsuario = new usuario();
        UtilClass ObjUtilClass = new UtilClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["TelaAtualUsuarioPerfilWebForm"] = "Sim";

            //Valida Acesso
            OBJSessao.ValidaAcesso();

            HttpContext.Current.Session["TelaAtualUsuarioPerfilWebForm"] = "Não";

            //Oculta mensagem
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = false;
            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {
                if (Session["Msg"].ToString().Contains("senha"))
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(Session["Msg"].ToString(), true);
                else
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }

            if (!IsPostBack)
            {
                //Carrega dados
                CarregaDadosNaTela();
            }
        }

        public void CarregaDadosNaTela()
        {
            //Recupera usuário da Session
            OBJUsuario.CodigoUsuario = Session["usuario"].ToString();
            OBJUsuario.CarregaDadosPrincipais();

            CodigoUsuarioTextBox.Text = OBJUsuario.CodigoUsuario;
            StatusTextBox.Text = OBJUsuario.Status;
            NomeUsuarioTextBox.Text = OBJUsuario.nome;
            EmailTextBox.Text = OBJUsuario.email;
            TelefoneTextBox.Text = OBJUsuario.telefone;
            IDUsuarioHiddenField.Value = OBJUsuario.IDUsuario.ToString();
        }

        protected void GravarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (SenhaNovaTextBox.Text != SenhaNovaRepetirTextBox.Text)
            {
                erro = "Senhas não coincidem.";
            }

            if (erro == "")
            {
                senha objSenha = new senha();

                erro = objSenha.ValidaIntegridadeSenhaUsuario(SenhaNovaTextBox.Text);
            }

            if (erro == "")
            {
                OBJUsuario.IDUsuario = Convert.ToInt32(IDUsuarioHiddenField.Value);
                OBJUsuario.CodigoUsuario = CodigoUsuarioTextBox.Text;
                OBJUsuario.Status = StatusTextBox.Text;
                OBJUsuario.nome = NomeUsuarioTextBox.Text;
                OBJUsuario.email = EmailTextBox.Text;
                OBJUsuario.telefone = TelefoneTextBox.Text;
                OBJUsuario.senha = SenhaNovaTextBox.Text;

                erro = OBJUsuario.AtualizaDadosUsuario();
            }

            if (erro != "")
            {
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                Response.Redirect("../Home.aspx?indmnu=1");
            }
        }
    }
}