using System.Web;
using CRMAPI.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Linq;
using RestSharp;

namespace CRMAPI.Models
{
    public class DadosClienteChatBotModel : ConexaoClass
    {
        public string Consulta { get; set; }
        public string CodigoCliente { get; set; }
        public string Historico { get; set; }

        private ChatBotClienteRetornoClass OBJChatBotClienteRetorno = new ChatBotClienteRetornoClass();

        public string RecuperaDadosCliente()
        {
            DataTable outputTable = new DataTable();
            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_API_CHATBOT_CLIENTE", dbConnection);

                    dbCommand.Parameters.Add(new SqlParameter("@Consulta", SqlDbType.VarChar, 0, "Consulta"));

                    dbCommand.Parameters["@Consulta"].Value = this.Consulta;

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
                                    OBJChatBotClienteRetorno.CodigoClienteSAP = row["CodigoClienteSAP"].ToString();
                                    OBJChatBotClienteRetorno.CodigoClienteCRM = row["CodigoClienteCRM"].ToString();
                                    OBJChatBotClienteRetorno.NomeCliente = row["NomeCliente"].ToString();
                                    OBJChatBotClienteRetorno.NomeVendedor = row["NomeVendedor"].ToString();
                                    OBJChatBotClienteRetorno.Encontrado = row["Encontrado"].ToString();
                                }
                            }
                        }
                        else
                        {
                            erro = CadastraCliente();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = "Erro ao recuperar dados do cliente. Erro:" + ex.Message;
            }

            return erro;
        }

        public ChatBotClienteRetornoClass GetDadosCliente()
        {
            //Se cliente tem vendedor relacionado verifica se está disponível
            if (OBJChatBotClienteRetorno.NomeVendedor != "")
            {
                VerificaDisponibilidade();
            }

            return OBJChatBotClienteRetorno;
        }

        public string IntegraHistoricoCliente()
        {
            string retorno = "";
            VendasWeb.classes.ClienteClasse OBJCliente = new VendasWeb.classes.ClienteClasse();

            OBJCliente.IDCliente = Convert.ToInt32(this.CodigoCliente);

            retorno = GravaHistorico(OBJCliente);

            if (retorno == "")
            {
                OBJChatBotClienteRetorno.NomeCliente = "";
                OBJChatBotClienteRetorno.CodigoClienteCRM = this.CodigoCliente;
                OBJChatBotClienteRetorno.CodigoClienteSAP = "";
                OBJChatBotClienteRetorno.NomeVendedor = "ClienteNovo";
                OBJChatBotClienteRetorno.Encontrado = "Nao";
            }

            return retorno;
        }

        public string CadastraCliente()
        {
            string retorno = "";
            string MsgRetorno = "";
            this.Historico = "Cliente foi cadastrado com sucesso utilizando integração com Sintegra e ChatBot.";


            VendasWeb.classes.ClienteClasse OBJCliente = new VendasWeb.classes.ClienteClasse();
            VendasWeb.GerencialVendas.UtilClass ObjUtilClass = new VendasWeb.GerencialVendas.UtilClass();
            VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.WSSaidaDadosReceita objWSSaidaDadosReceita = new VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.WSSaidaDadosReceita();
            VendasWeb.WEBServiceSAP.ClassesWEBService.JsonConversao jsonconv = new VendasWeb.WEBServiceSAP.ClassesWEBService.JsonConversao();
            DadosReceitaModel OBJDadosReceita = new DadosReceitaModel();

            OBJCliente.CodigoUsuario = "LUIZ.CARLOS";

            OBJCliente.IDVendedor = 48;

            OBJCliente.ObservacaoBreveCliente = "Cadastrado via ChatBot.";

            OBJCliente.CNPJCliente = this.Consulta;

            string ValidacaoCpfCnpj = ObjUtilClass.Valida_CPF_CNPJ_CRM(OBJCliente.CNPJCliente, OBJCliente.IDCliente, "C");

            if (ValidacaoCpfCnpj != "Valido")
            {
                if (ValidacaoCpfCnpj == "Invalido")
                    retorno = "CNPJ/CPF inválido.";
                else
                    retorno = "CNPJ/CPF " + ValidacaoCpfCnpj;
            }


            if (retorno == "")
            {
                OBJDadosReceita.TipoConsulta = "PJ";
                OBJDadosReceita.NumeroDocumento = System.Text.RegularExpressions.Regex.Replace(this.Consulta, @"[^0-9]", "");

                MsgRetorno = OBJDadosReceita.ConsultaDocumentos();

                if (MsgRetorno == "")
                    objWSSaidaDadosReceita = jsonconv.ConverteJSonParaObject<VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.WSSaidaDadosReceita>(OBJDadosReceita.GetDadosReceita());
                else if (retorno == "")
                    retorno = MsgRetorno;
            }

            #region VERIFICA SE A PESQUISA FOI REALIZADA COM SUCESSO

            if (retorno == "" && objWSSaidaDadosReceita.message != null && objWSSaidaDadosReceita.message != "null")
            {
                retorno = objWSSaidaDadosReceita.message;
            }

            if (retorno == "" && objWSSaidaDadosReceita.SintegraWSDadosSimplesNacional.message != "Pesquisa realizada com sucesso.")
            {
                retorno = objWSSaidaDadosReceita.SintegraWSDadosSimplesNacional.message;
            }

            if (retorno == "" && objWSSaidaDadosReceita.SintegraWSDadosSintegra.message != "Pesquisa realizada com sucesso.")
            {
                objWSSaidaDadosReceita.SintegraWSDadosSintegra = null;
                //erro = objWSSaidaDadosReceita.SintegraWSDadosSintegra.message;
            }

            if (retorno == "" && objWSSaidaDadosReceita.SintegraWSDadosSuframa != null
            && objWSSaidaDadosReceita.SintegraWSDadosSuframa.message != "Pesquisa realizada com sucesso.")
            {
                objWSSaidaDadosReceita.SintegraWSDadosSuframa = null;
                //erro = objWSSaidaDadosReceita.SintegraWSDadosSuframa.message;
            }

            #endregion

            if (retorno == "") retorno = OBJCliente.GravaClienteSefaz(objWSSaidaDadosReceita);

            if (retorno == "") retorno = OBJCliente.GravaClienteEnderecoSefaz(objWSSaidaDadosReceita);

            if (retorno == "") retorno = OBJCliente.GravaClienteFiscalSefaz(objWSSaidaDadosReceita);

            if (retorno == "") retorno = GravaHistorico(OBJCliente);

            //Cria retorno para o chatbot
            if(retorno == "")
            {
                if (objWSSaidaDadosReceita.SintegraWSDadosSintegra != null)
                    OBJChatBotClienteRetorno.NomeCliente = objWSSaidaDadosReceita.SintegraWSDadosSintegra.nome_empresarial;
                else
                    OBJChatBotClienteRetorno.NomeCliente = objWSSaidaDadosReceita.nome.ToUpper();
                OBJChatBotClienteRetorno.CodigoClienteCRM = OBJCliente.IDCliente.ToString();
                OBJChatBotClienteRetorno.CodigoClienteSAP = "";
                OBJChatBotClienteRetorno.NomeVendedor = "ClienteNovo";
            }else
            {
                OBJChatBotClienteRetorno.NomeCliente = "";
                OBJChatBotClienteRetorno.CodigoClienteCRM = "";
                OBJChatBotClienteRetorno.CodigoClienteSAP = "";
                OBJChatBotClienteRetorno.NomeVendedor = "ClienteNovo";
                OBJChatBotClienteRetorno.Encontrado = "Nao";
            }


            return retorno;
        }

        protected string GravaHistorico(VendasWeb.classes.ClienteClasse objClienteClasse)
        {
            VendasWeb.classes.HistoricosClass objHistorico = new VendasWeb.classes.HistoricosClass();

            objHistorico.IDCliente = objClienteClasse.IDCliente;
            objHistorico.IDTipoHistorico = 1;
            objHistorico.IDEvento = 7;
            objHistorico.IDCategoria = 4;
            objHistorico.IDUsuario = Convert.ToInt32(182);
            objHistorico.Historico = this.Historico;

            return objHistorico.GravaHistoricoCliente();
        }

        public void VerificaDisponibilidade()
        {
            ChatBotConsultaAgentesOnlineClass OBJChatBotConsultaAgentesOnline = new ChatBotConsultaAgentesOnlineClass();
            string URLBlip = "https://manulifitasa.http.msging.net/commands";

            string JSONEnvio = "{ ";
            JSONEnvio +="\"id\": \"{{$guid}}\", ";
            JSONEnvio +="\"to\": \"postmaster@desk.msging.net\", ";
            JSONEnvio +="\"method\": \"get\", ";
            JSONEnvio +="\"uri\": \"/teams/agents-online\" ";
            JSONEnvio +="}";

            var client = new RestClient(String.Format("{0}", URLBlip));
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Key Y29tZXJjaWFsMTgxOjRaTVlGRzJPZ2xpR0FtU0VkODdn");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(JSONEnvio);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                OBJChatBotConsultaAgentesOnline = JsonConvert.DeserializeObject<ChatBotConsultaAgentesOnlineClass>(response.Content);
            }

            if (OBJChatBotConsultaAgentesOnline.resource.items.Any(item => item.name == OBJChatBotClienteRetorno.NomeVendedor && item.agentsOnline <= 0))
            {
                OBJChatBotClienteRetorno.NomeVendedor = "GerencialVendas";
            }
        }

    }
}