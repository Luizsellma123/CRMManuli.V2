using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb
{
    public partial class LoginPortal : System.Web.UI.Page
    {

        UsuarioPortalClass OBJUsuario = new UsuarioPortalClass();
        clsConexao ObjConexao = new clsConexao();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //Limpa Session 
                Session.Remove("usuarioPortal");

                //Verificando se a Conexao esta Ativa
                if (ObjConexao.getString() == "")
                {
                    //Se nao tiver redireciona para a Tela de Aceso OFF
                    Response.Redirect("cloudoff.aspx");
                }
                else
                {
                    string url = HttpContext.Current.Request.Url.AbsoluteUri;

                    if (
                        ((url.ToUpper().Contains("177") == true) || (url.ToUpper().Contains("192") == true)) && (url.ToUpper().Contains("CRMMANULIDESENVOLVIMENTO") == false)
                     )
                    {
                        Response.Redirect("acesso.aspx");
                    }
                }
            }      

            
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (UsuarioTextBox.Text.Trim() != "")
            {
                if (Page.IsValid)
                {

                    //Response.Redirect("HomePortal.aspx");
                    OBJUsuario.codigo = UsuarioTextBox.Text.Trim();
                    OBJUsuario.senha = SenhaTextBox.Text.ToString();
                    string controleAcesso = OBJUsuario.Valida_Usuario();

                    if (controleAcesso != "")
                    {
                        lblError.Visible = true;
                        lblError.Text = controleAcesso;
                        Session.Remove("usuarioPortal");
                    }
                    else
                    {
                        Session["usuarioPortal"] = OBJUsuario;
                        Response.Redirect("HomePortal.aspx?indmnu=1");
                    }
                }
            }
        }
    }
}