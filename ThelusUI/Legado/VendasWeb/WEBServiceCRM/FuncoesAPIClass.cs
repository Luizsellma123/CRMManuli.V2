using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using VendasWeb.WEBServiceSAP.ClassesWEBService;
using RestSharp;
using Newtonsoft.Json;

namespace VendasWeb.WEBServiceCRM
{
    public class FuncoesAPIClass
    {
        string urlPadraoAPICRM = System.Configuration.ConfigurationManager.AppSettings["AcessoURLCRMAPI"];
        WSRetornoClass OBJRetorno = new WSRetornoClass();
        JsonConversao jsonconv = new JsonConversao();

        //Função async
        public async Task<string> PostURI(Uri u, HttpContent c)
        {
            var response = string.Empty;

            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.PostAsync(u, c);
                if (result.IsSuccessStatusCode)
                {
                    //response = result.StatusCode.ToString();
                    var retorno = result.Content.ReadAsStringAsync();
                    response = retorno.Result.ToString();
                }
            }
            return response;
        }

        public string AtualizaAnalisarEsbocoAPI(String JSONFinanceiro)
        {
            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/FinanceiroAtualizaAnalisarEsboco");

            HttpContent c = new StringContent(JSONFinanceiro, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        public string AdicionaEsbocoPedidoAPI(String JSONFinanceiro)
        {
            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/FinanceiroAdicionaEsbocoPedido");

            HttpContent c = new StringContent(JSONFinanceiro, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        public string ZeraLimitesCliente()
        {
            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/FinanceiroZeraLimites");

            HttpContent c = new StringContent("", Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        public string AtualizarHistoricoPedidoSAPAPI(String JSONPedido)
        {
            //string retorno = "";

            //HttpClient client = new HttpClient();
            //Uri u = new Uri(this.urlPadraoAPICRM + "api/PedidoAtualizarHistorico");

            //HttpContent c = new StringContent(JSONPedido, Encoding.UTF8, "application/json");
            //var t = PostURI(u, c);
            //t.Wait();

            //retorno = t.Result.ToString();
            //OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            //---------------------//
            var client = new RestClient(String.Format("{0}", this.urlPadraoAPICRM + "api/PedidoAtualizarHistorico"));
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(JSONPedido);

            // Verifica se a URL é HTTPS
            Uri uri = new Uri(this.urlPadraoAPICRM);
            if (uri.Scheme == Uri.UriSchemeHttps)
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
            }

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                OBJRetorno.MsgRetorno = "Erro na chamada.";
            }
            else
            {
                OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(response.Content);
            }

            return OBJRetorno.MsgRetorno;
        }

        public string ReiniciarCRMAPI()
        {
            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/RestartPool");

            HttpContent c = new StringContent("", Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        public string ImportacaoDepositoSAPCRMAPI(String JSONDeposito)
        {
            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/ImportacaoEstoqueDepositos");

            HttpContent c = new StringContent(JSONDeposito, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        public string ImportacaoDepositoPadraoSAPCRMAPI(String JSONDeposito)
        {
            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/ImportacaoDepositoPadraoProduto");

            HttpContent c = new StringContent(JSONDeposito, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        public string GeracaoOrdensProducaoSAPCRMAPI(String JSONGeracaoOrdens)
        {
            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/ProducaoGerarOrdemProducao");

            HttpContent c = new StringContent(JSONGeracaoOrdens, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        public string LiberacaoOrdensProducaoSAPCRMAPI(String JSONGeracaoOrdens)
        {
            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/ProducaoLiberarOrdemProducao");

            HttpContent c = new StringContent(JSONGeracaoOrdens, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        public string ImportaFechamentoFaturaSAPCRMAPI(String JSONImportaFechamentoFatura)
        {
            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/LogisticaFechamentoFatura");

            HttpContent c = new StringContent(JSONImportaFechamentoFatura, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();

            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        public string LiberaPedidoProducaoSAPCRMAPI(String JSONLiberacaoPedidoProducao)
        {
            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AdmVendasLiberaPedidoProducao");

            HttpContent c = new StringContent(JSONLiberacaoPedidoProducao, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        public string RecuperaDadosReceitaCRMAPI(String jsonRecuperaDadosReceitaCRMAPI)
        {
            string retorno = "";

            try
            {
                WSRetornoJSONClass objWSRetornoJSONClass = new WSRetornoJSONClass();

                HttpClient client = new HttpClient();

                Uri u = new Uri(this.urlPadraoAPICRM + "api/RecuperaDadosReceita");

                HttpContent c = new StringContent(jsonRecuperaDadosReceitaCRMAPI, Encoding.UTF8, "application/json");

                var t = PostURI(u, c);

                t.Wait();

                retorno = t.Result.ToString();

            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return retorno;
        }

        public string AtualizaClassificacaoComercial(String JsonAtualizaClassificacaoComercial)
        {
            string retorno = "";

            try
            {
                WSRetornoJSONClass objWSRetornoJSONClass = new WSRetornoJSONClass();

                HttpClient client = new HttpClient();

                Uri u = new Uri(this.urlPadraoAPICRM + "api/AdmVendasAtualizaClassificacaoComercial");

                HttpContent c = new StringContent(JsonAtualizaClassificacaoComercial, Encoding.UTF8, "application/json");

                var t = PostURI(u, c);

                t.Wait();

                retorno = t.Result.ToString();

            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return retorno;
        }

        public string InclusaoEsbocoPedidoAPI(String JSONPedido)
        {
            //string retorno = "";

            //HttpClient client = new HttpClient();
            //Uri u = new Uri(this.urlPadraoAPICRM + "api/PedidoInclusaoEsboco");

            //HttpContent c = new StringContent(JSONPedido, Encoding.UTF8, "application/json");
            //var t = PostURI(u, c);
            //t.Wait();

            //retorno = t.Result.ToString();
            //OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            //return retorno;

            //---------------//
            string retorno = "";

            var client = new RestClient(String.Format("{0}", this.urlPadraoAPICRM + "api/PedidoInclusaoEsboco"));
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(JSONPedido);

            // Verifica se a URL é HTTPS
            Uri uri = new Uri(this.urlPadraoAPICRM);
            if (uri.Scheme == Uri.UriSchemeHttps)
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
            }

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                retorno = "Erro na chamada.";
            }
            else
            {
                retorno = response.Content;
            }


            return retorno;
        }

        public string PedidoTransformaEsbocoPedidoAPI(String JSONPedido)
        {
            string retorno = "";

            //HttpClient client = new HttpClient();
            //Uri u = new Uri(this.urlPadraoAPICRM + "api/PedidoTransformaEsbocoPedido");

            //HttpContent c = new StringContent(JSONPedido, Encoding.UTF8, "application/json");
            //var t = PostURI(u, c);
            //t.Wait();

            //retorno = t.Result.ToString();
            //OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            //return retorno;

            var client = new RestClient(String.Format("{0}", this.urlPadraoAPICRM + "api/PedidoTransformaEsbocoPedido"));
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(JSONPedido);

            // Verifica se a URL é HTTPS
            Uri uri = new Uri(this.urlPadraoAPICRM);
            if (uri.Scheme == Uri.UriSchemeHttps)
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
            }

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                retorno = "Erro na chamada.";
            }
            else
            {
                retorno = response.Content;
            }


            return retorno;
        }

        public string AtualizacaoIntegracaoPedidoAPI(String JSONPedido)
        {
            //string retorno = "";

            //HttpClient client = new HttpClient();
            //Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizacaoIntegracaoPedido");

            //HttpContent c = new StringContent(JSONPedido, Encoding.UTF8, "application/json");
            //var t = PostURI(u, c);
            //t.Wait();

            //retorno = t.Result.ToString();
            //OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            //-----//
            var client = new RestClient(String.Format("{0}", this.urlPadraoAPICRM + "api/AtualizacaoIntegracaoPedido"));
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(JSONPedido);

            // Verifica se a URL é HTTPS
            Uri uri = new Uri(this.urlPadraoAPICRM);
            if (uri.Scheme == Uri.UriSchemeHttps)
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
            }

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                OBJRetorno.MsgRetorno = "Erro na chamada.";
            }
            else
            {
                OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(response.Content);
            }


            return OBJRetorno.MsgRetorno;
        }

        public string AtualizaValoresImpostosRascunhoAPI(String JSONPedido)
        {
            //string retorno = "";
            //HttpClient client = new HttpClient();
            //Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaImpostosPedidoRascunho");
            //HttpContent c = new StringContent(JSONPedido, Encoding.UTF8, "application/json");
            //var t = PostURI(u, c);
            //t.Wait();

            //---------------

            var client = new RestClient(String.Format("{0}", this.urlPadraoAPICRM + "api/AtualizaImpostosPedidoRascunho"));
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(JSONPedido);

            // Verifica se a URL é HTTPS
            Uri uri = new Uri(this.urlPadraoAPICRM);
            if (uri.Scheme == Uri.UriSchemeHttps)
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
            }

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                OBJRetorno.MsgRetorno = "Erro na chamada.";
            }
            else
            {
                OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(response.Content);
            }

            return OBJRetorno.MsgRetorno;
        }

        public string AtualizaValoresImpostosPedidoAPI(String JSONPedido)
        {
            //string retorno = "";

            //HttpClient client = new HttpClient();
            //Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaImpostosPedido");

            //HttpContent c = new StringContent(JSONPedido, Encoding.UTF8, "application/json");
            //var t = PostURI(u, c);
            //t.Wait();

            //retorno = t.Result.ToString();
            //OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            //return retorno;

            var client = new RestClient(String.Format("{0}", this.urlPadraoAPICRM + "api/AtualizaImpostosPedido"));
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(JSONPedido);

            // Verifica se a URL é HTTPS
            Uri uri = new Uri(this.urlPadraoAPICRM);
            if (uri.Scheme == Uri.UriSchemeHttps)
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
            }

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                OBJRetorno.MsgRetorno = "Erro na chamada.";
            }
            else
            {
                OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(response.Content);
            }


            return OBJRetorno.MsgRetorno;


        }

        public string InclusaoClienteAPI(String JSONPedido)
        {
            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/ClienteInclusao");

            HttpContent c = new StringContent(JSONPedido, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        public string AtualizacaoClienteAPI(String JSONPedido)
        {
            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/ClienteAtualizacao");

            HttpContent c = new StringContent(JSONPedido, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        public string AtualizacaoClienteVendedorAPI(String JSONPedido)
        {
            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/ClienteAtualizarVendedor");

            HttpContent c = new StringContent(JSONPedido, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }
       
        public WSRetornoJSONClass GravaDadosSerasaCRMAPI(String json)
        {
            HttpClient client = new HttpClient();

            Uri u = new Uri(this.urlPadraoAPICRM + "api/GravaDadosSerasa");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");

            var t = PostURI(u, c);

            t.Wait();

            WSRetornoJSONClass objWSRetornoJSONClass = jsonconv.ConverteJSonParaObject<WSRetornoJSONClass>(t.Result.ToString());

            return objWSRetornoJSONClass;
        }

        public WSRetornoJSONClass GeraPosicaoDiaria(String Json)
        {
            HttpClient client = new HttpClient();

            Uri u = new Uri(this.urlPadraoAPICRM + "api/GeraPosicaoDiaria");

            HttpContent c = new StringContent(Json, Encoding.UTF8, "application/json");

            var t = PostURI(u, c);

            t.Wait();

            WSRetornoJSONClass objWSRetornoJSONClass = jsonconv.ConverteJSonParaObject<WSRetornoJSONClass>(t.Result.ToString());

            return objWSRetornoJSONClass;
        }

        public WSRetornoJSONClass AtualizaRastreioPedido(String Json)
        {
            HttpClient client = new HttpClient();

            Uri u = new Uri(this.urlPadraoAPICRM + "api/RastreiaPedido");

            HttpContent c = new StringContent(Json, Encoding.UTF8, "application/json");

            var t = PostURI(u, c);

            t.Wait();

            WSRetornoJSONClass objWSRetornoJSONClass = jsonconv.ConverteJSonParaObject<WSRetornoJSONClass>(t.Result.ToString());

            return objWSRetornoJSONClass;
        }

        public string Consulta_CENPROT_CRMAPI(String Json)
        {
            try
            {
                HttpClient client = new HttpClient();

                Uri u = new Uri(this.urlPadraoAPICRM + "api/ConsultaCENPROT");

                HttpContent c = new StringContent(Json, Encoding.UTF8, "application/json");

                var t = PostURI(u, c);

                t.Wait();

                return t.Result.ToString();
            }
            catch
            {
                throw new Exception("Erro ao consultar a api ConsultaCENPROT.");
            }
        }

    }
}