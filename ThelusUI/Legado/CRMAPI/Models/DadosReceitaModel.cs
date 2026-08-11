using CRMAPI.Classes;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Models
{
    public class DadosReceitaModel
    {
        public string TipoConsulta { get; set; }
        public string NumeroDocumento { get; set; }

        private JsonConversaoClass jsonConversaoClass = new JsonConversaoClass();
        private DadosReceitaClass OBJDadosReceita = new DadosReceitaClass();
        private DadosReceitaRetornoClass OBJRetorno = new DadosReceitaRetornoClass();
        private string ReceitaCNPJWS = System.Configuration.ConfigurationManager.AppSettings["ReceitaCNPJ"];
        private string ReceitaIEWS = System.Configuration.ConfigurationManager.AppSettings["ReceitaIE"];
        private string ReceitaSUFRAMA = System.Configuration.ConfigurationManager.AppSettings["ReceitaSUFRAMA"];
        private string TokenCNPJA = System.Configuration.ConfigurationManager.AppSettings["TokenCNPJA"];
        private string SintegraWS = System.Configuration.ConfigurationManager.AppSettings["SintegraWS"];
        private string SintegraWSToken = System.Configuration.ConfigurationManager.AppSettings["SintegraWSToken"];

        public string ConsultaDocumentos()
        {
            string erro = "";

            if (this.TipoConsulta == "PJ")
            {
                erro = ConsultaCNPJ();
            }
            else
            {

            }

            return erro;
        }

        public string ConsultaCNPJ()
        {
            string erro = "";

            var client = new RestClient(String.Format("{0}{1}", this.ReceitaCNPJWS, this.NumeroDocumento));
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {

                OBJDadosReceita = jsonConversaoClass.ConverteJSonParaObject<DadosReceitaClass>(response.Content);

                if (OBJDadosReceita.status != "ERROR")
                {
                    //ConsultaInscricaoEstadual();

                    erro = ConsultaSintegraWSDadosSintegra();

                    //Se não der erro recupera dados do Sintegra
                    if (erro == "")
                    {
                        //Consulta Suframa para os estados AC, AM, AP, RO, RR
                        if (OBJDadosReceita.SintegraWSDadosSintegra.uf.Contains("AC") ||
                            OBJDadosReceita.SintegraWSDadosSintegra.uf.Contains("AM") ||
                            OBJDadosReceita.SintegraWSDadosSintegra.uf.Contains("AP") ||
                            OBJDadosReceita.SintegraWSDadosSintegra.uf.Contains("RO") ||
                            OBJDadosReceita.SintegraWSDadosSintegra.uf.Contains("RR"))
                        {
                            erro = ConsultaSintegraWSDadosSuframa();
                        }else
                        {
                            OBJDadosReceita.PossuiSuframa = "Não";
                        }
                    }

                    //Se não der erro recupera Simples Nacional
                    if (erro == "")
                    {
                        erro = ConsultaSintegraWSDadosSimplesNacional();
                    }
                }
                else
                {
                    erro = OBJDadosReceita.message;
                }
            }
            else
            {
                erro = "Não conseguiu recuperar dados.";
            }

            return erro;
        }

        public string ConsultaInscricaoEstadual()
        {
            string erro = "";

            var client = new RestClient(String.Format("{0}?taxId={1}&states={2}", this.ReceitaIEWS, this.NumeroDocumento, OBJDadosReceita.uf));
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", this.TokenCNPJA);
            //request.AddHeader("maxAge", "30D"); //Por Default ele busca 30 dias
            //request.AddHeader("maxStale", "30D"); //Por Default ele busca 30 dias

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                OBJDadosReceita.CadastroContribuiente = jsonConversaoClass.ConverteJSonParaObject<DadosReceitaCadastroContribuinteClass>(response.Content);
                erro = "";
            }
            else
            {
                OBJRetorno = jsonConversaoClass.ConverteJSonParaObject<DadosReceitaRetornoClass>(response.Content);
                erro = OBJRetorno.message;
            }

            return erro;
        }

        public string ConsultaSintegraWSDadosSintegra()
        {
            string erro = "";

            var client = new RestClient(String.Format("{0}?token={1}&cnpj={2}&plugin={3}", this.SintegraWS, this.SintegraWSToken, this.NumeroDocumento, "ST"));
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            //request.AddHeader("Authorization", this.TokenCNPJA);
            //request.AddHeader("maxAge", "30D"); //Por Default ele busca 30 dias
            //request.AddHeader("maxStale", "30D"); //Por Default ele busca 30 dias

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                OBJDadosReceita.SintegraWSDadosSintegra = jsonConversaoClass.ConverteJSonParaObject<SintegraWSDadosSintegraClass>(response.Content);
                if (OBJDadosReceita.SintegraWSDadosSintegra.status == "ERROR")
                {
                    switch (OBJDadosReceita.SintegraWSDadosSintegra.code)
                    {
                        case "2":
                            erro = "CNPJ inválido.";
                            break;
                        case "3":
                            erro = "Token inválido.";
                            break;
                        case "4":
                            erro = "Usuário não contratou nenhum pacote de créditos.";
                            break;
                        case "5":
                            erro = "Os créditos contratados acabaram.";
                            break;
                        case "6":
                            erro = "Plugin não existe.";
                            break;
                        case "7":
                            erro = "Site do Sintegra esta com instabilidade.";
                            break;
                        case "8":
                            erro = "Ocorreu um erro interno, por favor contatar o nosso suporte.";
                            break;
                        default:
                            erro = "Erro não identificado.";
                            break;
                    }
                }
                else
                {
                    if (OBJDadosReceita.SintegraWSDadosSintegra.code == "1")
                    {
                        OBJDadosReceita.IsentoIE = "Sim";
                    }else
                    {
                        OBJDadosReceita.IsentoIE = "Não";
                    }
                }
            }
            else
            {
                OBJRetorno = jsonConversaoClass.ConverteJSonParaObject<DadosReceitaRetornoClass>(response.Content);
                erro = OBJRetorno.message;
            }

            return erro;
        }

        public string ConsultaSintegraWSDadosSuframa()
        {
            string erro = "";

            var client = new RestClient(String.Format("{0}?token={1}&cnpj={2}&plugin={3}", this.SintegraWS, this.SintegraWSToken, this.NumeroDocumento, "SF"));
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            //request.AddHeader("Authorization", this.TokenCNPJA);
            //request.AddHeader("maxAge", "30D"); //Por Default ele busca 30 dias
            //request.AddHeader("maxStale", "30D"); //Por Default ele busca 30 dias

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                OBJDadosReceita.SintegraWSDadosSuframa = jsonConversaoClass.ConverteJSonParaObject<SintegraWSDadosSuframaClass>(response.Content);
                if (OBJDadosReceita.SintegraWSDadosSuframa.status == "ERROR")
                {
                    switch (OBJDadosReceita.SintegraWSDadosSuframa.code)
                    {
                        case "2":
                            erro = "CNPJ inválido.";
                            break;
                        case "3":
                            erro = "Token inválido.";
                            break;
                        case "4":
                            erro = "Usuário não contratou nenhum pacote de créditos.";
                            break;
                        case "5":
                            erro = "Os créditos contratados acabaram.";
                            break;
                        case "6":
                            erro = "Plugin não existe.";
                            break;
                        case "7":
                            erro = "Site do Suframa esta com instabilidade.";
                            break;
                        case "8":
                            erro = "Ocorreu um erro interno, por favor contatar o nosso suporte.";
                            break;
                        default:
                            erro = "Erro não identificado.";
                            break;
                    }
                }
                else
                {
                    if (OBJDadosReceita.SintegraWSDadosSintegra.code == "1")
                    {
                        OBJDadosReceita.PossuiSuframa = "Não";
                    }else
                    {
                        OBJDadosReceita.PossuiSuframa = "Sim";
                    }
                }
            }
            else
            {
                OBJRetorno = jsonConversaoClass.ConverteJSonParaObject<DadosReceitaRetornoClass>(response.Content);
                erro = OBJRetorno.message;
            }

            return erro;
        }

        public string ConsultaSintegraWSDadosSimplesNacional()
        {
            string erro = "";

            var client = new RestClient(String.Format("{0}?token={1}&cnpj={2}&plugin={3}", this.SintegraWS, this.SintegraWSToken, this.NumeroDocumento, "SN"));
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            //request.AddHeader("Authorization", this.TokenCNPJA);
            //request.AddHeader("maxAge", "30D"); //Por Default ele busca 30 dias
            //request.AddHeader("maxStale", "30D"); //Por Default ele busca 30 dias

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                OBJDadosReceita.SintegraWSDadosSimplesNacional = jsonConversaoClass.ConverteJSonParaObject<SintegraWSDadosSimplesNacionalClass>(response.Content);
                if (OBJDadosReceita.SintegraWSDadosSimplesNacional.status == "ERROR")
                {
                    switch (OBJDadosReceita.SintegraWSDadosSimplesNacional.code)
                    {
                        case "2":
                            erro = "CNPJ inválido.";
                            break;
                        case "3":
                            erro = "Token inválido.";
                            break;
                        case "4":
                            erro = "Usuário não contratou nenhum pacote de créditos.";
                            break;
                        case "5":
                            erro = "Os créditos contratados acabaram.";
                            break;
                        case "6":
                            erro = "Plugin não existe.";
                            break;
                        case "7":
                            erro = "Site do simples nacional esta com instabilidade.";
                            break;
                        case "8":
                            erro = "Ocorreu um erro interno, por favor contatar o nosso suporte.";
                            break;
                        default:
                            erro = "Erro não identificado.";
                            break;
                    }
                }
                else
                {
                    if (OBJDadosReceita.SintegraWSDadosSimplesNacional.code == "1")
                    {
                        OBJDadosReceita.PossuiSimplesNacional = "Não";
                        OBJDadosReceita.PossuiSimplesNacionalMEI = "Não";
                    }else
                    {
                        if (OBJDadosReceita.SintegraWSDadosSimplesNacional.situacao_simples_nacional.Contains("Não"))
                        {
                            OBJDadosReceita.PossuiSimplesNacional = "Não";
                        }
                        else
                        {
                            OBJDadosReceita.PossuiSimplesNacional = "Sim";
                        }

                        if (OBJDadosReceita.SintegraWSDadosSimplesNacional.situacao_simei.Contains("Não"))
                        {
                            OBJDadosReceita.PossuiSimplesNacionalMEI = "Não";
                        }else
                        {
                            OBJDadosReceita.PossuiSimplesNacionalMEI = "Sim";
                        }
                    }
                }
            }
            else
            {
                OBJRetorno = jsonConversaoClass.ConverteJSonParaObject<DadosReceitaRetornoClass>(response.Content);
                erro = OBJRetorno.message;
            }

            return erro;
        }

        public string GetDadosReceita()
        {
            string retorno = "";

            retorno = this.jsonConversaoClass.ConverteObjectParaJSon<DadosReceitaClass>(OBJDadosReceita);

            return retorno;
        }
    }
}