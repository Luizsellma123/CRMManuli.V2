using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using CRMAPI.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using System.Data.SqlClient;
using RestSharp;

namespace CRMAPI.Models
{
    public class DadosSerasaModel : ConexaoClass
    {
        public string TipoConsulta { get; set; }
        public string NumeroDocumento { get; set; }
        public string Produto { get; set; }

        private dynamic ProdutoSerasa = new System.Dynamic.ExpandoObject();
        private List<ProdutoSerasaEnvioClass> ProdutoSerasaEnvio = new List<ProdutoSerasaEnvioClass>();
        private List<ProdutoSerasaRetornoClass> ProdutoSerasaRetorno = new List<ProdutoSerasaRetornoClass>();
        private string ProdutoDescricao { get; set; }

        private string SERASAURL = System.Configuration.ConfigurationManager.AppSettings["URLSERASA"];

        private string JSONRetorno { get; set; }

        public string ConsultaDadosSerasa()
        {
            string envio = "";
            string erro = "";
            string Produto = "";
            string ProdutoFilho = "";
            string CNPJCPF = "";
            int Tamanho = 0;
            int EspacosBrancos = 0;
            if (this.Produto == "" || this.Produto == null)
                this.Produto = "RELATO";
            this.RecuperaConfiguracaoRetornoProduto();
            this.RecuperaConfiguracaoEnvioProduto();

            //Recupera URL de Envio
            envio = this.SERASAURL;
            try
            {
                foreach (ProdutoSerasaEnvioClass OBJProdutoSerasaEnvio in ProdutoSerasaEnvio)
                {
                    foreach (ProdutoSerasaEnvioFilhoClass OBJProdutoSerasaEnvioFilho in OBJProdutoSerasaEnvio.ProdutoSerasaEnvioFilho)
                    {
                        Tamanho = OBJProdutoSerasaEnvioFilho.Valor.Length;

                        Produto = this.Produto;
                        ProdutoFilho = OBJProdutoSerasaEnvioFilho.NomeCampo;

                        //Verifica se deve pegar o valor da configuração ou se deve pegar dos parâmetros
                        if (!OBJProdutoSerasaEnvioFilho.RecuperaValorProduto)
                        {
                            OBJProdutoSerasaEnvioFilho.Valor = TrataValor(OBJProdutoSerasaEnvioFilho.NomeCampo, OBJProdutoSerasaEnvioFilho.Valor);
                        }


                        if (Tamanho == OBJProdutoSerasaEnvioFilho.Tamanho)
                        {
                            envio += OBJProdutoSerasaEnvioFilho.Valor;
                        }
                        else
                        {
                            EspacosBrancos = OBJProdutoSerasaEnvioFilho.Tamanho - Tamanho;

                            envio += OBJProdutoSerasaEnvioFilho.Valor.PadRight(EspacosBrancos);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = "Erro no envio produto " + Produto + " atributo " + ProdutoFilho + ". Erro :" + ex.Message;
            }

            string Retorno = "";

            var client = new RestClient(envio);
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                Retorno = response.Content;
            }

            // Dividir a string em linhas
            string[] lines = Retorno.Split(new[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
            bool VerificaProduto = false;
            bool MudaCPFCNPJ = false;
            dynamic dynamicObject = new System.Dynamic.ExpandoObject();
            string newline = "";

            try
            {
                // Percorrer as linhas
                foreach (string line in lines)
                {
                    //Atribui o CNPJ no final da linha
                    newline = line + CNPJCPF;

                    VerificaProduto = ProdutoSerasaRetorno.Exists(x => x.Prefixo == newline.Substring((x.PosicaoInicial - 1), x.Tamanho));

                    if (VerificaProduto == true)
                    {

                        dynamic dynamicObjectitem = new System.Dynamic.ExpandoObject();
                        ProdutoSerasaRetornoClass produtoEncontrado = ProdutoSerasaRetorno.Find(x => x.Prefixo == newline.Substring((x.PosicaoInicial - 1), x.Tamanho));

                        MudaCPFCNPJ = PrefixoCNPJ().Contains(produtoEncontrado.Prefixo);

                        //Atribui valores para poder roder DEBUG depois
                        Produto = this.Produto;
                        ProdutoFilho = produtoEncontrado.NomeCampo;
                        int TamanhoString = 0;

                        //Atribui o cabeçalho do produto
                        if (!HasAttribute(dynamicObject, "IDProduto"))
                        {
                            AddAttribute(dynamicObject, "Produto", this.Produto);
                            AddAttribute(dynamicObject, "IDProduto", produtoEncontrado.IDProduto);
                            AddAttribute(dynamicObject, "IDConfiguracao", produtoEncontrado.IDConfiguracao);
                            AddAttribute(dynamicObject, "NomeCampo", produtoEncontrado.NomeCampo);
                            AddAttribute(dynamicObject, "PosicaoInicial", produtoEncontrado.PosicaoInicial);
                            AddAttribute(dynamicObject, "PosicaoFinal", produtoEncontrado.PosicaoFinal);
                            AddAttribute(dynamicObject, "Prefixo", produtoEncontrado.Prefixo);
                            AddAttribute(dynamicObject, "Tamanho", produtoEncontrado.Tamanho);
                            AddAttribute(dynamicObject, "Descricao", this.ProdutoDescricao);
                        }

                        if (!HasAttribute(dynamicObject, produtoEncontrado.NomeCampo))
                        {
                            AddAttribute(dynamicObject, produtoEncontrado.NomeCampo, new List<System.Dynamic.ExpandoObject>());
                        }

                        AddAttribute(dynamicObjectitem, "IDProduto", produtoEncontrado.IDProduto);
                        AddAttribute(dynamicObjectitem, "IDConfiguracao", produtoEncontrado.IDConfiguracao);

                        foreach (ProdutoSerasaRetornoFilhoClass OBJProdutoSerasaRetornoFilho in produtoEncontrado.ProdutoSerasaRetornoFilho)
                        {

                            TamanhoString = Math.Min(OBJProdutoSerasaRetornoFilho.Tamanho, (newline.Length - (OBJProdutoSerasaRetornoFilho.PosicaoInicial - 1)));
                            AddAttribute(dynamicObjectitem, OBJProdutoSerasaRetornoFilho.NomeCampo, newline.Substring((OBJProdutoSerasaRetornoFilho.PosicaoInicial - 1), TamanhoString));
                        }

                        if (MudaCPFCNPJ == true)
                        {
                            switch (produtoEncontrado.Prefixo)
                            {

                                case "L010117":
                                    CNPJCPF = (string)((IDictionary<string, object>)dynamicObjectitem)["CPF"];
                                    break;

                                case "L010119":
                                    CNPJCPF = (string)((IDictionary<string, object>)dynamicObjectitem)["CNPJ"];
                                    break;

                                case "L010102":
                                    CNPJCPF = dynamicObjectitem.CDCGCR;
                                    break;
                            }
                        }

                        //var dynamicList = (List<System.Dynamic.ExpandoObject>)dynamicObject[produtoEncontrado.NomeCampo];
                        var dynamicList = (List<System.Dynamic.ExpandoObject>)((IDictionary<string, object>)dynamicObject)[produtoEncontrado.NomeCampo];
                        dynamicList.Add(dynamicObjectitem);
                    }
                }

                this.JSONRetorno = JsonConvert.SerializeObject(dynamicObject, new ExpandoObjectConverter());
            }
            catch (Exception ex)
            {
                erro = "Erro na atualização do produto " + Produto + " atributo " + ProdutoFilho + ". Erro :" + ex.Message;
            }

            return erro;
        }

        private static void AddAttribute(dynamic obj, string attributeName, object attributeValue)
        {
            var dictionary = (IDictionary<string, object>)obj;
            dictionary.Add(attributeName, attributeValue);
        }

        private static void AddAttribute(dynamic obj, string attributeName, System.Dynamic.ExpandoObject attributeValue)
        {
            var dictionary = (IDictionary<string, object>)obj;
            dictionary.Add(attributeName, attributeValue);
        }

        private static void AddAttribute(dynamic obj, string attributeName, List<object> attributeValue)
        {
            var dictionary = (IDictionary<string, object>)obj;
            dictionary.Add(attributeName, attributeValue);
        }

        private static void AddAttribute(dynamic obj, string attributeName, List<System.Dynamic.ExpandoObject> attributeValue)
        {
            var dictionary = (IDictionary<string, object>)obj;
            dictionary.Add(attributeName, attributeValue);
        }

        private static bool HasAttribute(dynamic obj, string attributeName)
        {
            var dictionary = (IDictionary<string, object>)obj;
            return dictionary.ContainsKey(attributeName);
        }

        public string RecuperaConfiguracaoRetornoProduto()
        {
            DataTable outputTable = new DataTable();
            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_API_SERASA_CONFIG_PROD_RETORNO", dbConnection);

                    dbCommand.Parameters.Add(new SqlParameter("@NomeProduto", SqlDbType.VarChar, 0, "NomeProduto"));

                    dbCommand.Parameters["@NomeProduto"].Value = this.Produto;

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                if (erro == "")
                                {
                                    ProdutoSerasaRetornoClass OBJProdutoSerasaRetorno = new ProdutoSerasaRetornoClass();

                                    OBJProdutoSerasaRetorno.IDProduto = Convert.ToInt32(row["IDProduto"]);
                                    OBJProdutoSerasaRetorno.IDConfiguracao = Convert.ToInt32(row["IDConfiguracao"]);
                                    OBJProdutoSerasaRetorno.NomeCampo = Convert.ToString(row["NomeCampo"]);
                                    OBJProdutoSerasaRetorno.Descricao = Convert.ToString(row["Descricao"]);
                                    OBJProdutoSerasaRetorno.PosicaoInicial = Convert.ToInt32(row["PosicaoInicial"]);
                                    OBJProdutoSerasaRetorno.PosicaoFinal = Convert.ToInt32(row["PosicaoFinal"]);
                                    OBJProdutoSerasaRetorno.Tamanho = Convert.ToInt32(row["Tamanho"]);
                                    OBJProdutoSerasaRetorno.Prefixo = Convert.ToString(row["Prefixo"]);
                                    this.ProdutoDescricao = Convert.ToString(row["DescricaoProduto"]);

                                    OBJProdutoSerasaRetorno.RecuperaConfiguracaoRetornoProdutoFilho();

                                    ProdutoSerasaRetorno.Add(OBJProdutoSerasaRetorno);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = "erro ao recuperar configuração produto SERASA.";
            }

            return erro;
        }

        public string RecuperaConfiguracaoEnvioProduto()
        {
            DataTable outputTable = new DataTable();
            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_API_SERASA_CONFIG_PROD_ENVIO", dbConnection);

                    dbCommand.Parameters.Add(new SqlParameter("@NomeProduto", SqlDbType.VarChar, 0, "NomeProduto"));

                    dbCommand.Parameters["@NomeProduto"].Value = this.Produto;

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                if (erro == "")
                                {
                                    ProdutoSerasaEnvioClass OBJProdutoSerasaEnvio = new ProdutoSerasaEnvioClass();

                                    OBJProdutoSerasaEnvio.IDProduto = Convert.ToInt32(row["IDProduto"]);
                                    OBJProdutoSerasaEnvio.IDConfiguracao = Convert.ToInt32(row["IDConfiguracao"]);
                                    OBJProdutoSerasaEnvio.NomeCampo = Convert.ToString(row["NomeCampo"]);
                                    OBJProdutoSerasaEnvio.Descricao = Convert.ToString(row["Descricao"]);
                                    OBJProdutoSerasaEnvio.PosicaoInicial = Convert.ToInt32(row["PosicaoInicial"]);
                                    OBJProdutoSerasaEnvio.PosicaoFinal = Convert.ToInt32(row["PosicaoFinal"]);
                                    OBJProdutoSerasaEnvio.Tamanho = Convert.ToInt32(row["Tamanho"]);
                                    this.ProdutoDescricao = Convert.ToString(row["DescricaoProduto"]);

                                    OBJProdutoSerasaEnvio.RecuperaConfiguracaoEnvioProdutoFilho();

                                    ProdutoSerasaEnvio.Add(OBJProdutoSerasaEnvio);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = "erro ao recuperar configuração produto SERASA.";
            }

            return erro;
        }

        public string TrataValor(string Campo, string valor)
        {
            string Retorno = "";
            int Tamanho = Math.Min(8, (NumeroDocumento.Replace(".", "").Replace("/", "").Replace("-", "")).Length);

            switch (Campo)
            {
                case "CNPJ":
                    Retorno = "00000000".Substring(0, (9 - Tamanho)) + NumeroDocumento.Replace(".", "").Replace("/", "").Replace("-", "").Substring(0, Tamanho);
                    break;
                default:
                    Retorno = valor;
                    break;
            }

            return Retorno;
        }

        public string GetJSONRetorno()
        {
            return this.JSONRetorno;
        }

        // Função retorna Prefixo a serem verificados que retorna uma lista de valores a serem verificados
        List<string> PrefixoCNPJ()
        {
            return new List<string> { "L010117", "L010119", "L010102" };
        }

        public string ConsultaDadosSerasaAPI()
        {
            string erro = "";

            AutenticacaoSERASAClass objAutenticacaoSERASAClass = new AutenticacaoSERASAClass();

            ProdutoSerasaAutenticacaoClass OBJAutenticacao = new ProdutoSerasaAutenticacaoClass();

            erro = objAutenticacaoSERASAClass.AutenticacaoSERASA();

            if (erro == "") OBJAutenticacao = objAutenticacaoSERASAClass.GetOBJAutenticacao();

            if (erro != "") throw new Exception(erro);

            // Montagem da URL com parâmetros diretamente
            string url = $"{OBJAutenticacao.GetURLExecucao()}?reportName={Uri.EscapeDataString(OBJAutenticacao.GetReportName())}&optionalFeatures={Uri.EscapeDataString(OBJAutenticacao.GetOptionalFeatures())}&reportParameters=ew0KCSJyZXBvcnRQYXJhbWV0ZXJzIjogWw0KCQl7DQoJCQkibmFtZSI6ICJzZWdtZW50Q29kZSIsDQoJCQkidmFsdWUiOiAiMDA1Ig0KCQl9DQoJXQ0KfQ";

            var client = new RestClient(url);
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + OBJAutenticacao.accessToken);
            request.AddHeader("X-Document-id", this.NumeroDocumento);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                return response.Content;
            }
            else
            {
                string message = null;

                //Expressão regular para extrair o conteúdo do campo "message"
                Match match = Regex.Match(response.Content, @"""message"":""(.*?)""");

                if (match.Success) message = match.Groups[1].Value;

                throw new Exception("Ocorreu um problema ao autenticar no SERASA. " + (message ?? ""));
            }            
        }
    }
}