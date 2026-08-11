using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb
{
    public partial class esqueci : System.Web.UI.Page
    {
        usuario novoUsuario = new usuario();
        enviarEmail mdlMail = new enviarEmail();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string erro = "";
            erro = novoUsuario.consultaValorUsuario(txtUsuario.Text.ToString());

            if (erro == "")
            {
                mdlMail.enviaEmail("Senha Acesso WEB", novoUsuario.consultaValorSenha(txtUsuario.Text.ToString()).ToString(), novoUsuario.consultaEmail(txtUsuario.Text.ToString()).ToString());
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = erro;
            }
        }
    }
}