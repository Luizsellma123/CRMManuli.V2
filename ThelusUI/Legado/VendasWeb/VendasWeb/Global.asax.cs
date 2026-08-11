using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace VendasWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            
        }

        protected void Session_Start(object sender, EventArgs e)
        {
          
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (
                // (Server.MachineName != "JACKSON-LIZIER") &&
                (Request.CurrentExecutionFilePath != "/favicon.ico")
             )
            {
                Exception ex = new Exception();
                ex = Server.GetLastError();

                enviarEmail ObjEnviarEmail = new enviarEmail();
                ObjEnviarEmail.EmailDestinatario = "jackson@athelus.com.br";
                ObjEnviarEmail.EmailRemetente = "jackson@athelus.com.br";
                ObjEnviarEmail.Remetente = "Manuli";
                ObjEnviarEmail.EmailSenha = "thelus@!1";
                ObjEnviarEmail.Descricao = "Exception Error Vendas Web Manuli";
                ObjEnviarEmail.Texto = "Um erro não tratado ocorreu no sistema <br/ > no servidor: " + Server.MachineName + "<br/>";

                try
                {
                    //Caso a pagina não exista, não será possivel enviar o caminho
                    ObjEnviarEmail.Texto = ObjEnviarEmail.Texto + "Pagina: " + Request.CurrentExecutionFilePath + "<br/>";
                }
                catch
                {
                }

                try
                {
                    if (Session["usuario"] != null)
                    {
                        //Caso a sessão tenha expirado, não será possível enviar o usuário.
                        ObjEnviarEmail.Texto = ObjEnviarEmail.Texto + "USUARIO: " + Session["usuario"].ToString() + "<br/><br/><br/>";
                    }
                }
                catch
                {
                }

                ObjEnviarEmail.Texto = ObjEnviarEmail.Texto + "Segue abaixo detalhes do erro: <br /><br/>";
                ObjEnviarEmail.Texto = ObjEnviarEmail.Texto + ex.Message + "<br/> <br/> <br/>";
                ObjEnviarEmail.Texto = ObjEnviarEmail.Texto + ex.ToString() + "<br/> <br/>";

                //Enviar Email
                ObjEnviarEmail.enviarEmails();

                //Mandar Copia
               /* ObjEnviarEmail.EmailDestinatario = "mario.duarte@manulifitasa.com.br";
                //Enviar Email
                ObjEnviarEmail.enviarEmails();*/


                //Mandar Copia
                ObjEnviarEmail.EmailDestinatario = "luiz.carlos@manulifitasa.com.br";
                //Enviar Email
                ObjEnviarEmail.enviarEmails();

                //Mandar Copia
                //ObjEnviarEmail.EmailDestinatario = "marlon@athelus.com.br";
                //Enviar Email
                //ObjEnviarEmail.enviarEmails();

                //Response.Redirect("Error.aspx?indmnu=2");  <--Alterado para redirecionar no WebConfig(<customErrors mode="On" defaultRedirect="error.aspx?indmnu=2"/>)
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}