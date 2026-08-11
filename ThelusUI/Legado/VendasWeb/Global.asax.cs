using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using VendasWeb.WEBServiceCRM;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using VendasWeb.WEBServiceSAP.ClassesWEBService;

namespace VendasWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // dispara uma nova thread para executar
            ThreadStart tsTarefa = new ThreadStart(TarefaLoop);
            Thread MinhaTarefa = new Thread(tsTarefa);
            MinhaTarefa.Start();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //Aumenta tempo da sessão para 60 minutos
            Session.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TempoDaSessao"]);
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
                try
                {

                    Exception ex = new Exception();
                    ex = Server.GetLastError();

                    enviarEmail ObjEnviarEmail = new enviarEmail();
                    ObjEnviarEmail.EmailDestinatario = "luiz@thelus.curitiba.br";
                    ObjEnviarEmail.EmailRemetente = "luiz@thelus.curitiba.br";
                    ObjEnviarEmail.Remetente = "Manuli";
                    ObjEnviarEmail.EmailSenha = "raiden@!1";
                    ObjEnviarEmail.Descricao = "Exception Error Vendas Web Manuli";
                    ObjEnviarEmail.Texto = "Um erro não tratado ocorreu no sistema <br/ > no servidor: " + Server.MachineName + "<br/>";

                    //Caso a pagina não exista, não será possivel enviar o caminho
                    ObjEnviarEmail.Texto = ObjEnviarEmail.Texto + "Pagina: " + Request.CurrentExecutionFilePath + "<br/>";

                    if (Session["usuario"] != null)
                    {
                        //Caso a sessão tenha expirado, não será possível enviar o usuário.
                        ObjEnviarEmail.Texto = ObjEnviarEmail.Texto + "USUARIO: " + Session["usuario"].ToString() + "<br/><br/><br/>";
                    }

                    ObjEnviarEmail.Texto = ObjEnviarEmail.Texto + "Segue abaixo detalhes do erro: <br /><br/>";
                    ObjEnviarEmail.Texto = ObjEnviarEmail.Texto + ex.InnerException + "<br/> <br/> <br/>";
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
                    LogAuditoria.ClassesAuditoria.LogErroClass OBJLog = new LogAuditoria.ClassesAuditoria.LogErroClass();
                    OBJLog.IDusuario = 0;

                    OBJLog.OperacaoAcao = Request.CurrentExecutionFilePath;
                    OBJLog.LogErro(ex, Request.CurrentExecutionFilePath);

                }
                catch (Exception exception)
                {
                    string erro = exception.ToString();
                }
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void TarefaLoop()
        {
            int cont = 0;
            //irá verificar se deve ou não executar o método a cada 1 hora
            while (true)
            {
                if (DateTime.Now.Hour == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["HorarioParaThread"])) //Se for 1 horas da manhã
                {
                    TarefaAgendada();
                }

                //tarefa de importação ira rodar conforme o parametro do webconfig(IntervaloParaThread)
                if (cont == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["IntervaloParaThread"]))
                {
                    TarefaImportaDepositos();
                    TarefaImportaDepositosPadrao();
                    cont = 0;
                }

                cont++;

                System.Threading.Thread.Sleep(TimeSpan.FromHours(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TempoParaThread"])));
            }

        }

        protected void TarefaAgendada()
        {
            FuncoesAPIClass OBJApi = new FuncoesAPIClass();
            OBJApi.ZeraLimitesCliente();
        }

        protected void TarefaImportaDepositos()
        {
            FuncoesAPIClass ObjAPI = new FuncoesAPIClass();
            JsonConversao jsonconv = new JsonConversao();
            WSImportacaoDepositoClass ObjImportacaoDeposito = new WSImportacaoDepositoClass();
            ObjImportacaoDeposito.CodigoDepositoSAP = "";
            ObjImportacaoDeposito.ImportaTodos = false;
            string JsonImportacaoDeposito = "";

            JsonImportacaoDeposito = jsonconv.ConverteObjectParaJSon(ObjImportacaoDeposito);

            ObjAPI.ImportacaoDepositoSAPCRMAPI(JsonImportacaoDeposito);
        }

        protected void TarefaImportaDepositosPadrao()
        {
            FuncoesAPIClass ObjAPI = new FuncoesAPIClass();
            JsonConversao jsonconv = new JsonConversao();
            WSImportacaoDepositoPadraoClass ObjImportacaoDepositoPadrao = new WSImportacaoDepositoPadraoClass();
            ObjImportacaoDepositoPadrao.CodigoProdutoSAP = "";
            ObjImportacaoDepositoPadrao.ImportaTodos = false;
            string JsonImportacaoDepositoPadrao = "";

            JsonImportacaoDepositoPadrao = jsonconv.ConverteObjectParaJSon(ObjImportacaoDepositoPadrao);

            ObjAPI.ImportacaoDepositoPadraoSAPCRMAPI(JsonImportacaoDepositoPadrao);
        }


    }
}