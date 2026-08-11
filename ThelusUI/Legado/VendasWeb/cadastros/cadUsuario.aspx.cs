using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;

namespace VendasWeb.cadastros
{
    public partial class cadUsuario : System.Web.UI.Page
    {
        usuario novoUsuario = new usuario();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                lblError.Visible = false;

                btnSalvar.Attributes.Add("onclick", "javascript:return validaUsuario();");
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            string erro = "";
            erro = novoUsuario.consultaUsuario(txtUsuario.Text.ToString());

            if (erro == "")
            {
                novoUsuario.nome = txtUsuario.Text.ToString();
                novoUsuario.senha = txtSenha.Text.ToString();
                novoUsuario.email = txtEmail.Text.ToString();

                erro = novoUsuario.gravaUsuario();
                if (erro == "")
                {
                    Response.Write("<script>alert(\"Usuario : " + novoUsuario.nome.ToString() + " gravado com sucesso\");</script>");
                    Response.Write("<script>window.location=\"../login.aspx?indmnu=0\";</script>");
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = erro;
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = erro;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location=\"../login.aspx?indmnu=0\";</script>");
        }
    }
}