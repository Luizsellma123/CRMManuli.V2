using System;
using VendasWeb.Email;

namespace VendasWeb
{
    public partial class esqueci : System.Web.UI.Page
    {
        usuario objUsuario = new usuario();
        EmailTemplateClass OBJEmail = new EmailTemplateClass();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (txtUsuario.Text.Trim() == "") erro = "Digite o email.";

            string codigoUsuario = "", senha = "";

            if (erro == "")
            {
                objUsuario.email = txtUsuario.Text.Trim();

                codigoUsuario = objUsuario.RecuperaUsuarioPeloEmail();

                if (codigoUsuario == "")
                    erro = "Usuário não cadastrado no sistema.";
                else
                    senha = objUsuario.RecuperaSenhaPeloEmail();
            }

            if (erro == "")
            {
                string corpoEmail = "Codigo do usuário: " + codigoUsuario;

                corpoEmail += "<br>";

                corpoEmail += "Senha: " + senha;

                erro = EnviaEmail(corpoEmail, objUsuario.email);
            }

            if (erro == "") erro = "Senha enviada para o email " + objUsuario.email;

            ApresentaMensagem(erro);
        }        

        public string EnviaEmail(string corpoEmail, string EmailPara)
        {
            OBJEmail.cabecalho = "Recuperação de acesso";
            OBJEmail.titulo = "Recuperação de acesso";
            OBJEmail.detalhe = corpoEmail;
            OBJEmail.data = DateTime.Now.ToString("dd/MM/yyyy");
            OBJEmail.emailpara = EmailPara;

            return OBJEmail.EnviaEmailRecuperacaoAcesso();
        }

        protected void ApresentaMensagem(string texto = "")
        {
            lblError.Visible = true;
            lblError.Text = texto;
        }

    }
}