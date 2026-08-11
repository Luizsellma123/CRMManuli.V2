using CRMAPI.Classes;
using Swashbuckle.Application;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CRMAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        // Variável estática para armazenar o valor único
        private static string _sessaoID;
        public static string SessaoID
        {
            get
            {
                lock (typeof(WebApiApplication))
                {
                    return _sessaoID;
                }
            }
            set
            {
                lock (typeof(WebApiApplication))
                {
                    _sessaoID = value;
                }
            }
        }

        protected void Application_Start()
        {
            //Criando Comunicacao Global DI API
            //if (Application["ApplicationComunicacaoSAP"] == null)
            //{
            //    ComunicacaoSAPClass OBJComunicacao = new ComunicacaoSAPClass();
            //    OBJComunicacao.conectarSAP();
            //    Application.Lock();
            //    Application["ApplicationComunicacaoSAP"] = OBJComunicacao;
            //    Application.UnLock();
            //}

            //Criando Comunicacao Global Service Layer
            if (Application["ApplicationComunicacaoServiceLayerSAP"] == null)
            {
                ComunicacaoServiceLayerSAPClass OBJComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();
                OBJComunicacaoServiceLayerSAP.conectarSAP();
                Application.Lock();
                Application["ApplicationComunicacaoServiceLayerSAP"] = OBJComunicacaoServiceLayerSAP;
                Application.UnLock();
            }

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            // Atualizar o valor único a cada solicitação
            AtualizarValorUnico();
        }


        private void AtualizarValorUnico()
        {
            // Lógica para atualizar o valor único
            // Aqui você pode gerar um novo valor único ou atualizá-lo de acordo com a lógica do seu aplicativo
            SessaoID = GerarNovoValorUnico();
        }

        // Método para gerar um novo valor único
        private string GerarNovoValorUnico()
        {
            Random random = new Random();
            int tamanho = random.Next(101, 201); // Gera um número aleatório entre 101 e 200

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, tamanho)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
