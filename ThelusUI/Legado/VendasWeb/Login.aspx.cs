using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;

namespace VendasWeb
{
    public partial class Login : System.Web.UI.Page
    {

        funcoes mdlfuncoes = new funcoes();
        clsConexao ObjConexao = new clsConexao();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                //Verificando se a Conexao esta Ativa
                if (ObjConexao.getString() == "")
                {
                    //Se nao tiver redireciona para a Tela de Aceso OFF
                    Response.Redirect("cloudoff.aspx");
                }
                else
                {
                    string url = HttpContext.Current.Request.Url.AbsoluteUri;
                    /*
                    if (
                        ((url.ToUpper().Contains("177") == true) || (url.ToUpper().Contains("192") == true)) && (url.ToUpper().Contains("CRMMANULIDESENVOLVIMENTO") == false) && (url.ToUpper().Contains("CRMMANULIAPOLO") == false && (url.ToUpper().Contains("CRM_SAP_TESTE") == false) && (url.ToUpper().Contains("crmmanuli") == false))
                     )
                    {
                        Response.Redirect("acesso.aspx");
                    }
                    */
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
                    string controleAcesso = ValidaUsuario();

                    string controleSenha = ValidaIntegridadeSenhaUsuario();

                    if (controleAcesso != "")
                    {
                        lblError.Visible = true;
                        lblError.Text = controleAcesso;
                        Session["idLogin"] = 0;
                    }
                    else
                    {
                        Session["idLogin"] = 1;

                        //Grava cookies
                        WriteCookies();

                        if (controleSenha != "")
                        {
                            Session["Msg"] = controleSenha;

                            Response.Redirect("~/Usuarios/UsuarioPerfilWebForm.aspx?indmnu=3");
                        }
                        else
                        {
                            Response.Redirect("Home.aspx?indmnu=1");
                        }
                    }
                }
            }
        }

        public string ValidaUsuario()
        {
            string msgErro = "";
            int retUsuario;
            string aux = this.GetType().FullName;



            string sql = "Select count(*) as CNT from CRM_CADASTRO_USUARIO where CodigoUsuario ='" + UsuarioTextBox.Text.Trim() + "' and Senha='" + SenhaTextBox.Text.ToString() + "' and Status like 'Ativ%'";

            retUsuario = int.Parse(mdlfuncoes.ExecutaSqlReader(sql, "ValidaUsuario").ToString());

            if (retUsuario == 0)
            {
                msgErro = validaStatus();
            }
            else
            {
                msgErro = validaStatus();
            }

            if (retUsuario > 0)
            {
                /*
                sql = "select sum(CNT) as cont from (select COUNT(*) as CNT from GRP_X_USUARIO where GrpUsuCod ";
                sql += "like '%Vendas_GER%' and GrpUsuSuperv='T' and UsuCod = '" + UsuarioTextBox.Text.Trim() + "' ";
                sql += "union select COUNT(*) as CNT from USUARIO where UsuCod = '" + UsuarioTextBox.Text.Trim() + "' and UsuAdmin = 'T' ) as a";

                Session["nivel"] = mdlfuncoes.ExecutaSqlReader(sql, "ValidaUsuario").ToString();

                if ((string)Session["nivel"].ToString() == "" || (string)Session["nivel"].ToString() == "0" || Session["nivel"] == null)
                {
                    Session["nivel"] = mdlfuncoes.ExecutaSqlReader(sql, "ValidaUsuario").ToString();
                }
                */

                msgErro = "";
            }
            return msgErro;
        }

        public string validaStatus()
        {
            string sql = "";
            string UsuStat = "";

            sql = "Select Status from CRM_CADASTRO_USUARIO where CodigoUsuario ='" + UsuarioTextBox.Text.Trim() + "' and Senha='" + SenhaTextBox.Text.ToString() + "'";
            UsuStat = mdlfuncoes.ExecutaSqlReader(sql, "validaStatus").ToString();

            if (UsuStat == "Ativo")
            {
                sql = "Select CodigoUsuario from CRM_CADASTRO_USUARIO where CodigoUsuario ='" + UsuarioTextBox.Text.Trim() + "'";
                Session["usuario"] = mdlfuncoes.ExecutaSqlReader(sql, "validaStatus").ToString();

                sql = "Select IDUsuario from CRM_CADASTRO_USUARIO where CodigoUsuario ='" + UsuarioTextBox.Text.Trim() + "'";
                Session["IDUsuario"] = mdlfuncoes.ExecutaSqlReader(sql, "validaStatus").ToString();

                return "";
            }
            else
            {
                if (UsuStat == "Desligado")
                {
                    return "Usuario Desativado";
                }
                else
                {
                    return "Usuario ou Senha Invalida";
                }
            }
        }

        private void WriteCookies()
        {
            var CookieUsuario = new HttpCookie("usuario");
            CookieUsuario.Value = Session["usuario"].ToString();
            CookieUsuario.Expires = DateTime.Now.AddMinutes(480); //Expira em 480 minutos.
            Response.Cookies.Add(CookieUsuario);

            var CookieIDUsuario = new HttpCookie("IDUsuario");
            CookieIDUsuario.Value = Session["IDUsuario"].ToString();
            CookieIDUsuario.Expires = DateTime.Now.AddMinutes(480); //Expira em 480 minutos.
            Response.Cookies.Add(CookieIDUsuario);

            var CookieidLogin = new HttpCookie("idLogin");
            CookieidLogin.Value = Session["idLogin"].ToString();
            CookieidLogin.Expires = DateTime.Now.AddMinutes(480); //Expira em 480 minutos.
            Response.Cookies.Add(CookieidLogin);
        }

        protected string ValidaIntegridadeSenhaUsuario()
        {
            senha objSenha = new senha();

            if (objSenha.ValidaIntegridadeSenhaUsuario(SenhaTextBox.Text) != "")
                return "A senha é muito vulnerável, precisa ser trocada.";
            else
                return "";
        }
    }
}