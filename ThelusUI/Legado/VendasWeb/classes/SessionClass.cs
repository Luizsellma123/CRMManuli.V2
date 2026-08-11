using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.classes
{
    public class SessionClass
    {

        public int UserId
        {
            set
            {
                HttpContext.Current.Session.Add("id_user_s", value);
            }
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["id_user_s"]);
            }
        }

        public void ValidaAcesso()
        {
            if (HttpContext.Current.Session["usuario"] == null/* && varmenu != 0 && varmenu < 99*/)
            {
                this.ReadCookies();

                if (HttpContext.Current.Session["usuario"] == null)
                {
                    //Redireciona para tela de login
                    HttpContext.Current.Response.Redirect("~/Default.aspx");
                }
            }

            //Verifica a integridade da senha do usuário
            {
                usuario objUsuario = new usuario();

                objUsuario.CodigoUsuario = HttpContext.Current.Session["usuario"].ToString();

                objUsuario.CarregaDadosPrincipais();

                senha objSenha = new senha();

                if (objSenha.ValidaIntegridadeSenhaUsuario(objUsuario.Senha) != "")
                {
                    HttpContext.Current.Session["Msg"] = "A senha é muito vulnerável, precisa ser trocada.";

                    if (HttpContext.Current.Session["TelaAtualUsuarioPerfilWebForm"] == null ||
                        HttpContext.Current.Session["TelaAtualUsuarioPerfilWebForm"].ToString() != "Sim")
                    {
                        HttpContext.Current.Session["TelaAtualUsuarioPerfilWebForm"] = "Não";

                        HttpContext.Current.Response.Redirect("~/Usuarios/UsuarioPerfilWebForm.aspx?indmnu=3");
                    }
                }
            }
        }

        public void ReadCookies()
        {
            foreach (var cookie in HttpContext.Current.Request.Cookies)
            {
                //Recupera usuario
                if (cookie.Equals("usuario"))
                {
                    HttpContext.Current.Session["usuario"] = HttpContext.Current.Request.Cookies[cookie.ToString()].Value;
                }

                //Recupera IDusuario
                if (cookie.Equals("IDUsuario"))
                {
                    HttpContext.Current.Session["IDUsuario"] = HttpContext.Current.Request.Cookies[cookie.ToString()].Value;
                }

                //Recupera idLogin
                if (cookie.Equals("idLogin"))
                {
                    HttpContext.Current.Session["idLogin"] = HttpContext.Current.Request.Cookies[cookie.ToString()].Value;
                }
            }
        }
    }
}
