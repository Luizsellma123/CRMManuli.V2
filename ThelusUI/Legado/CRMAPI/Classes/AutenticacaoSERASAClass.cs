using System;
using System.Web;
using RestSharp;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using VendasWeb.WEBServiceSAP.ClassesWEBService;

namespace CRMAPI.Classes
{
    public class AutenticacaoSERASAClass : VendasWeb.GerencialVendas.clsConexao
    {
        private ProdutoSerasaAutenticacaoClass OBJAutenticacao = new ProdutoSerasaAutenticacaoClass();
        
        string Autenticacao = ""
           , credentials = ""
           , base64Credentials = ""
           , clientID = ""
           , clientSecret = ""
           , URLAUTENTICACAO = ""
           , reportName = ""
           , OptionalFeatures = ""
           , URLEXECUCAO = ""
           , ParametroCNPJCPF = "";

        public string CarregaCamposAutenticacaoSerasa()
        {
            string erro = "";

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CONSULTA_PRODUTO_SERASA_ENVIO_RELATOAPI", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            switch (row["NomeCampo"].ToString())
                            {
                                case "clientID":
                                    clientID = row["Valor"].ToString();
                                    break;
                                case "clientSecret":
                                    clientSecret = row["Valor"].ToString();
                                    break;
                                case "URLATENTICACAO":
                                    URLAUTENTICACAO = row["Valor"].ToString();
                                    break;
                                case "URLEXECUCAO":
                                    URLEXECUCAO = row["Valor"].ToString();
                                    break;
                                case "reportName":
                                    reportName = row["Valor"].ToString();
                                    break;
                                case "optionalFeatures":
                                    OptionalFeatures = row["Valor"].ToString();
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               erro = ex.ToString();
            }

            return erro;
        }

        public string AutenticacaoSERASA()
        {
            string erro = "";

            CarregaAppnProdSerasaAut();

            // Verifica se já existe um token válido 
            if (OBJAutenticacao == null || OBJAutenticacao.DataExpiracao <= DateTime.Now)
            {
                CarregaCamposAutenticacaoSerasa();

                // Configuração da requisição HTTP para autenticação 
                var client = new RestClient(URLAUTENTICACAO);
                client.Timeout = -1;

                // Gera a string de autenticação em Base64 
                credentials = clientID + ":" + clientSecret;
                base64Credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(credentials));
                Autenticacao = "Basic " + base64Credentials;

                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", Autenticacao);
                request.AddHeader("Content-Type", "application/json");

                // Define protocolos de segurança compatíveis 
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 |
                                                                    System.Net.SecurityProtocolType.Tls11 |
                                                                    System.Net.SecurityProtocolType.Tls;

                IRestResponse response = client.Execute(request);

                // Verifica se a autenticação foi bem-sucedida 
                if (response.StatusCode.ToString() == "Created")
                {
                    OBJAutenticacao = JsonConvert.DeserializeObject<ProdutoSerasaAutenticacaoClass>(response.Content);
                    OBJAutenticacao.SetClientID(clientID);
                    OBJAutenticacao.SetSecretID(clientSecret);
                    OBJAutenticacao.SetOptionalFeatures(OptionalFeatures);
                    OBJAutenticacao.SetReportName(reportName);
                    OBJAutenticacao.SetURLAutenticacao(URLAUTENTICACAO);
                    OBJAutenticacao.SetURLExecucao(URLEXECUCAO);
                }
                else
                {
                    erro = "Ocorreu um problema ao autenticar no SERASA. ERRO: " + response.ErrorMessage;
                }

                // Armazena a autenticação na aplicação global, se não houver erro 
                if (erro == "")
                {
                    HttpContext.Current.Application.Lock();
                    HttpContext.Current.Application["ApplicationProdutoSerasaAutenticacao"] = OBJAutenticacao;
                    HttpContext.Current.Application.UnLock();
                }
            }

            return erro;
        }

        public void CarregaAppnProdSerasaAut()
        {
            // Recupera os dados de autenticação armazenados na aplicação global 
            if (HttpContext.Current.Application["ApplicationProdutoSerasaAutenticacao"] != null)
            {
                OBJAutenticacao = (ProdutoSerasaAutenticacaoClass)HttpContext.Current.Application["ApplicationProdutoSerasaAutenticacao"];
            }
        }

        public ProdutoSerasaAutenticacaoClass GetOBJAutenticacao()
        {
            return OBJAutenticacao;
        }
    }
}