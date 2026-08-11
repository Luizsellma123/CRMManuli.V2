using System;
using System.Web.Management;
using RestSharp;
using static CRMAPI.Classes.CENPROTRetornoClass;
using System.Collections.Generic;

namespace CRMAPI.Classes
{
    public class ChamaAPIClass
    {
        public string EnderecoAPI { get; set; }

        public string Json { get; set; }

        public string AuthorizationKey { get; set; }

        public VendasWeb.WEBServiceSAP.ClassesWEBService.JsonConversao jsonconv = new VendasWeb.WEBServiceSAP.ClassesWEBService.JsonConversao();

        public string Chama_API()
        {
            var client = new RestClient(String.Format("{0}", this.EnderecoAPI));
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);

            System.Net.ServicePointManager.SecurityProtocol =
                  System.Net.SecurityProtocolType.Tls12
                | System.Net.SecurityProtocolType.Tls11
                | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
                return response.Content;

            RetornoJson objRetornoJson = jsonconv.ConverteJSonParaObject<RetornoJson>(response.Content);

            return "Erro: " + objRetornoJson.message;
        }

        public string Chama_API_POST_Request(RestRequest request)
        {
            RetornoJson objRetornoJson = new RetornoJson();

            var client = new RestClient(this.EnderecoAPI);

            client.Timeout = -1;

            System.Net.ServicePointManager.SecurityProtocol =
                  System.Net.SecurityProtocolType.Tls12
                | System.Net.SecurityProtocolType.Tls11
                | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            objRetornoJson = jsonconv.ConverteJSonParaObject<RetornoJson>(response.Content);

            if (!response.IsSuccessful)
            {
                throw new Exception("Erro: " + objRetornoJson.error);
            }
            else
            {
                if (objRetornoJson.errors.Count > 0)
                {
                    foreach (string erro in objRetornoJson.errors)
                    {
                        throw new Exception(erro);
                    }
                }

                if (objRetornoJson.code != "200")
                {
                    throw new Exception("Erro: " + objRetornoJson.code_message);
                }

                return response.Content;
            }
        }

        public string Chama_API_Json()
        {
            var client = new RestClient(String.Format("{0}", this.EnderecoAPI));
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(Json);

            System.Net.ServicePointManager.SecurityProtocol =
                  System.Net.SecurityProtocolType.Tls12
                | System.Net.SecurityProtocolType.Tls11
                | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
                return response.Content;

            RetornoJson objRetornoJson = jsonconv.ConverteJSonParaObject<RetornoJson>(response.Content);

            return "Erro: " + objRetornoJson.message;
        }

        public string Chama_API_GET_Json()
        {
            var client = new RestClient(String.Format("{0}", this.EnderecoAPI));
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(Json);

            System.Net.ServicePointManager.SecurityProtocol =
                  System.Net.SecurityProtocolType.Tls12
                | System.Net.SecurityProtocolType.Tls11
                | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
                return response.Content;

            RetornoJson objRetornoJson = jsonconv.ConverteJSonParaObject<RetornoJson>(response.Content);

            return "Erro: " + objRetornoJson.message;
        }

        public string Chama_API_GET_Com_Autenticacao()
        {
            var client = new RestClient(String.Format("{0}", this.EnderecoAPI));
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + AuthorizationKey);

            System.Net.ServicePointManager.SecurityProtocol =
                  System.Net.SecurityProtocolType.Tls12
                | System.Net.SecurityProtocolType.Tls11
                | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
                return response.Content;

            RetornoJson objRetornoJson = jsonconv.ConverteJSonParaObject<RetornoJson>(response.Content);

            return "Erro: " + objRetornoJson.message;
        }

        public string Chama_API_Json_Com_Autenticacao()
        {
            var client = new RestClient(String.Format("{0}", this.EnderecoAPI));
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(Json);
            request.AddHeader("Authorization", "Bearer " + AuthorizationKey);

            System.Net.ServicePointManager.SecurityProtocol =
                  System.Net.SecurityProtocolType.Tls12
                | System.Net.SecurityProtocolType.Tls11
                | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
                return response.Content;

            RetornoJson objRetornoJson;

            try
            {
                objRetornoJson = jsonconv.ConverteJSonParaObject<RetornoJson>(response.Content);
            }
            catch
            {
                return "Erro: " + response.Content;
            }

            return "Erro: " + objRetornoJson.message;
        }

        public class RetornoJson
        {
            public string message { get; set; }
            public string error { get; set; }
            public string status { get; set; }
            public string code { get; set; }
            public string code_message { get; set; }
            public List<string> errors { get; set; }
        }
    }
}