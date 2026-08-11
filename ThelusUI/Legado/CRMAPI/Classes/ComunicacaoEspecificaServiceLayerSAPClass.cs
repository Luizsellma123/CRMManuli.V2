using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ComunicacaoEspecificaServiceLayerSAPClass : ComunicacaoServiceLayerSAPClass
    {
        public string UsuarioAcessoSAP { get; set; }
        public string SenhaUsuarioAcessoSAP { get; set; }

        public override string conectarSAP()
        {
            string erro = "";
            JSONEnvio = "";

            if (UsuarioAcessoSAP != "" && SenhaUsuarioAcessoSAP != null)
            {
                if ((this.ValidoAte != null && this.ValidoAte <= DateTime.Now) || this.OBJComunicacaoServiceLayerLoginRetorno == null)
                {

                    /*Atribuição de dados para conexão*/
                    this.URLServiceLayerSAP = System.Configuration.ConfigurationManager.AppSettings["URLServiceLayerSAP"];
                    this.CompanyDB = System.Configuration.ConfigurationManager.AppSettings["BancoDadosSAP"];
                    this.UserName = this.UsuarioAcessoSAP;
                    this.Password = this.SenhaUsuarioAcessoSAP;

                    this.OBJComunicacaoServiceLayerLogin = new ComunicacaoServiceLayerLoginSAPClass();
                    this.OBJComunicacaoServiceLayerLogin.CompanyDB = this.CompanyDB;
                    this.OBJComunicacaoServiceLayerLogin.UserName = this.UserName;
                    this.OBJComunicacaoServiceLayerLogin.Password = this.Password;
                    JSONEnvio = JsonConvert.SerializeObject(OBJComunicacaoServiceLayerLogin);


                    var client = new RestClient(String.Format("{0}", this.URLServiceLayerSAP + "/Login"));
                    client.Timeout = -1;

                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddJsonBody(JSONEnvio);

                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

                    IRestResponse response = client.Execute(request);

                    if (response.StatusCode.ToString() == "OK")
                    {
                        OBJComunicacaoServiceLayerLoginRetorno = JsonConvert.DeserializeObject<ComunicacaoServiceLayerLoginRetornoSAPClass>(response.Content);
                        this.DataAcesso = DateTime.Now;
                        this.ValidoAte = DateTime.Now.AddMinutes(OBJComunicacaoServiceLayerLoginRetorno.SessionTimeout - 1);
                    }
                }
            }else
            {
                erro = "Necessário informar usuário e senha.";
            }

            return erro;
        }

    }
}