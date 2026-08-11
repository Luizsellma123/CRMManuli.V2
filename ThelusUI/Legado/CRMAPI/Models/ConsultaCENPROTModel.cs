using CRMAPI.Classes;
using RestSharp;
using System;
using System.Collections.Generic;
using VendasWeb.classes;
using static CRMAPI.Classes.CENPROTRetornoClass;

namespace CRMAPI.Models
{
    public class ConsultaCENPROTModel
    {
        public int IDCliente { get; set; }

        public int IDAnalise { get; set; }

        int IDCartorio = 0;

        ClienteClasse ObjCliente = new ClienteClasse();

        CENPROTClass objCENPROTClass = new CENPROTClass();

        JsonConversaoClass objJsonConversaoClass = new JsonConversaoClass();

        CENPROTRetornoClass objCENPROTRetornoClass = new CENPROTRetornoClass();

        UtilClass objUtilClass = new UtilClass();

        string jsonRetornoAPI = "";

        public string ConsultaCENPROT()
        {
            ObjCliente.IDCliente = IDCliente;

            ObjCliente.IDAnalise = IDAnalise;

            bool teste = false;

            try
            {
                objCENPROTClass = ObjCliente.Consulta_CENPROT_PARAMETROS();

                if (teste)
                    CarregaJsonTeste();
                else
                    jsonRetornoAPI = ChamaApiDirectData();

                objCENPROTRetornoClass = objJsonConversaoClass.ConverteJSonParaObject<CENPROTRetornoClass>(jsonRetornoAPI);

                if (objCENPROTRetornoClass.metaDados.resultado == "Sucesso")
                    GravaCliente();
                else
                    return objCENPROTRetornoClass.metaDados.mensagem;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }

        public string ChamaApiDirectData()
        {
            string Cnpj = "0", Cpf = "0", erro = "";

            string tipoCliente = ObjCliente.Consulta_CRM_CLIENTE_CNPJ_OU_CPF();

            string CNPJCPF = ObjCliente.Consulta_CRM_CLIENTE_CNPJCPF();

            if (tipoCliente == "CNPJ")
                Cnpj = CNPJCPF;
            else
                Cpf = CNPJCPF;

            string token = objCENPROTClass.TOKEN;

            string url = "https://apiv3.directd.com.br/api/ProtestosOnline?";

            if (objUtilClass.ValidarCNPJ(Cnpj))
            {
                url += "CNPJ=" + objUtilClass.RetornaApenasDigitos(Cnpj) + "&";
            }

            if (objUtilClass.ValidarCPF(Cpf))
            {
                url += "CPF=" + objUtilClass.RetornaApenasDigitos(Cpf) + "&";
            }

            url += "TOKEN=" + token;

            //Faz Comunicação
            var client = new RestClient(url);

            client.Timeout = -1;

            //Adiciona Parametros
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            erro = response.Content;

            return erro;
        }

        public string ConsultaCENPROTTESTE()
        {
            string erro = "";
            string resposta = "";
            string token = "FF101FC1-83F2-4ACF-B548-8B78940A3357";

            //Dados a serem consultados
            string Cnpj = "34.448.971/0001-10", Cpf = "0";

            //"https://apiv3.directd.com.br/api/ProtestosOnline?Cnpj=00000000000191&Cpf=12345678901&TOKEN=SEU_TOKEN"

            string url = "https://apiv3.directd.com.br/api/ProtestosOnline?";

            if (objUtilClass.ValidarCNPJ(Cnpj))
            {
                url += "CNPJ=" + objUtilClass.RetornaApenasDigitos(Cnpj) + "&";
            }

            if (objUtilClass.ValidarCPF(Cpf))
            {
                url += "CPF=" + objUtilClass.RetornaApenasDigitos(Cpf) + "&";
            }

            url += "TOKEN=" + token;

            //Faz Comunicação
            var client = new RestClient(url);

            client.Timeout = -1;

            //Adiciona Parametros
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                erro = response.Content;
            }
            else
            {
                resposta = response.Content;
            }

            return erro;
        }

        protected string GravaCliente()
        {
            string erro = ObjCliente.Grava_CRM_CENPROT_CLIENTE();

            if (erro == "") erro = GravaCartorios();

            return erro;
        }

        protected string GravaCartorios()
        {
            try
            {
                if (objCENPROTRetornoClass.retorno != null)
                {
                    if (objCENPROTRetornoClass.retorno.numeroTotalProtestos > 0)
                    {
                        foreach (ProtestoUf protesto in objCENPROTRetornoClass.retorno.protestos)
                        {
                            foreach (Cartorio cartorio in protesto.cartorios)
                            {
                                GravaCartorioUF(protesto, cartorio);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Erro ao gravar cartórios: " + ex.Message;
            }

            return "";
        }

        protected void GravaCartorioUF(ProtestoUf protesto, Cartorio cartorio)
        {
            try
            {
                ObjCliente.Codigo = "";

                ObjCliente.Cartorio = (protesto.estado + " - " + cartorio.cidade) ?? "";

                ObjCliente.TelefoneCartorio = "";

                ObjCliente.Endereco = "";

                ObjCliente.Uf = protesto.estado ?? "";

                ObjCliente.CidadeCodigo = cartorio.codigoCidade ?? "";

                ObjCliente.CodigoIBGE = "";

                ObjCliente.Municipio = cartorio.cidade ?? "";

                ObjCliente.Bairro = "";

                ObjCliente.Cidade = cartorio.cidade ?? "";

                ObjCliente.AtualizacaoData = "";

                ObjCliente.Quantidade = cartorio.numeroProtestos.ToString() ?? "";

                ObjCliente.PeriodoPesquisa = "";

                IDCartorio = ObjCliente.Grava_CRM_CENPROT_CLIENTE_CARTORIOS();

                GravaProtestos(cartorio.titulos);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar cartório: " + ex.Message);
            }
        }

        protected void GravaProtestos(List<TituloProtestado> titulos)
        {
            string erro = "";

            try
            {
                if (titulos.Count > 0)
                {
                    foreach (TituloProtestado tituloProtestado in titulos)
                    {
                        ObjCliente.CPFCNPJ = objUtilClass.RetornaApenasDigitos(tituloProtestado.documento) ?? "";

                        ObjCliente.Data = objUtilClass.FormataDataSQL(tituloProtestado.dataProtesto) ?? "";

                        ObjCliente.DataProtesto = objUtilClass.FormataDataSQL(tituloProtestado.dataProtesto) ?? "";

                        ObjCliente.DataProtestoString = objUtilClass.FormataDataSQL(tituloProtestado.dataProtesto) ?? "";

                        ObjCliente.DataVencimento = "";

                        ObjCliente.DataVencimentoString = "";

                        ObjCliente.Valor = Convert.ToDecimal(tituloProtestado.valorProtestado.Replace("R$", ""));

                        ObjCliente.ValorString = tituloProtestado.valorProtestado.Replace("R$", "") ?? "";

                        ObjCliente.Chave = "";

                        ObjCliente.NomeApresentante = "";

                        ObjCliente.NomeCedente = "";

                        ObjCliente.TemAnuencia = "";

                        ObjCliente.IDCartorio = IDCartorio;

                        erro = ObjCliente.Grava_CRM_CENPROT_CLIENTE_CARTORIOS_PROTESTOS();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar protestos: " + ex.Message + " - " + erro);
            }
        }

        protected void CarregaJsonTeste()
        {
            CarregaJsonCerto();

            return;
        }

        protected void CarregaJsonCerto()
        {
            jsonRetornoAPI =
            @"
                {
                  ""metaDados"": {
                    ""consultaNome"": ""Protestos Nacional - IEPTB Online"",
                    ""consultaUid"": ""direct-f62f0f92-e5ee-4703-8c5e-e6390118b19e"",
                    ""chave"": ""CNPJ=34448971000110;"",
                    ""usuario"": ""Luiz Sellma"",
                    ""mensagem"": ""Sucesso"",
                    ""ip"": ""189.45.159.2"",
                    ""resultadoId"": 1,
                    ""resultado"": ""Sucesso"",
                    ""apiVersao"": ""v3"",
                    ""enviarCallback"": false,
                    ""gerarComprovante"": false,
                    ""urlComprovante"": null,
                    ""assincrono"": false,
                    ""data"": ""09/07/2026 09:31:41"",
                    ""tempoExecucaoMs"": 813
                  },
                  ""retorno"": {
                    ""documentoConsultado"": ""34.448.971/0001-10"",
                    ""constamProtestos"": true,
                    ""numeroTotalProtestos"": 15,
                    ""valorTotalProtestos"": ""R$ 20.724,20"",
                    ""observacoes"": ""A entidade possui protestos"",
                    ""protestos"": [
                      {
                        ""estado"": ""PR"",
                        ""numeroTotalProtestosUF"": 15,
                        ""valorTotalProtestosEstado"": ""R$ 20.724,20"",
                        ""cartorios"": [
                          {
                            ""codigoCidade"": ""4106902"",
                            ""cidade"": ""CURITIBA"",
                            ""numeroProtestos"": 3,
                            ""valorTotalProtestosCartorio"": ""R$ 6.438,87"",
                            ""titulos"": [
                              {
                                ""dataProtesto"": ""13/06/2025"",
                                ""valorProtestado"": ""R$ 2.838,03"",
                                ""documento"": ""34.448.971/0001-10""
                              },
                              {
                                ""dataProtesto"": ""15/10/2024"",
                                ""valorProtestado"": ""R$ 1.800,42"",
                                ""documento"": ""34.448.971/0001-10""
                              },
                              {
                                ""dataProtesto"": ""29/10/2024"",
                                ""valorProtestado"": ""R$ 1.800,42"",
                                ""documento"": ""34.448.971/0001-10""
                              }
                            ]
                          },
                          {
                            ""codigoCidade"": ""4106902"",
                            ""cidade"": ""CURITIBA"",
                            ""numeroProtestos"": 3,
                            ""valorTotalProtestosCartorio"": ""R$ 5.992,86"",
                            ""titulos"": [
                              {
                                ""dataProtesto"": ""05/11/2024"",
                                ""valorProtestado"": ""R$ 2.853,50"",
                                ""documento"": ""34.448.971/0001-10""
                              },
                              {
                                ""dataProtesto"": ""27/01/2025"",
                                ""valorProtestado"": ""R$ 285,86"",
                                ""documento"": ""34.448.971/0001-10""
                              },
                              {
                                ""dataProtesto"": ""30/10/2024"",
                                ""valorProtestado"": ""R$ 2.853,50"",
                                ""documento"": ""34.448.971/0001-10""
                              }
                            ]
                          },
                          {
                            ""codigoCidade"": ""4106902"",
                            ""cidade"": ""CURITIBA"",
                            ""numeroProtestos"": 3,
                            ""valorTotalProtestosCartorio"": ""R$ 4.939,72"",
                            ""titulos"": [
                              {
                                ""dataProtesto"": ""12/11/2024"",
                                ""valorProtestado"": ""R$ 2.853,50"",
                                ""documento"": ""34.448.971/0001-10""
                              },
                              {
                                ""dataProtesto"": ""22/10/2024"",
                                ""valorProtestado"": ""R$ 1.800,42"",
                                ""documento"": ""34.448.971/0001-10""
                              },
                              {
                                ""dataProtesto"": ""13/01/2025"",
                                ""valorProtestado"": ""R$ 285,80"",
                                ""documento"": ""34.448.971/0001-10""
                              }
                            ]
                          },
                          {
                            ""codigoCidade"": ""4106902"",
                            ""cidade"": ""CURITIBA"",
                            ""numeroProtestos"": 1,
                            ""valorTotalProtestosCartorio"": ""R$ 355,60"",
                            ""titulos"": [
                              {
                                ""dataProtesto"": ""23/01/2025"",
                                ""valorProtestado"": ""R$ 355,60"",
                                ""documento"": ""34.448.971/0001-10""
                              }
                            ]
                          },
                          {
                            ""codigoCidade"": ""4106902"",
                            ""cidade"": ""CURITIBA"",
                            ""numeroProtestos"": 2,
                            ""valorTotalProtestosCartorio"": ""R$ 1.290,60"",
                            ""titulos"": [
                              {
                                ""dataProtesto"": ""17/01/2025"",
                                ""valorProtestado"": ""R$ 935,00"",
                                ""documento"": ""34.448.971/0001-10""
                              },
                              {
                                ""dataProtesto"": ""23/01/2025"",
                                ""valorProtestado"": ""R$ 355,60"",
                                ""documento"": ""34.448.971/0001-10""
                              }
                            ]
                          },
                          {
                            ""codigoCidade"": ""4106902"",
                            ""cidade"": ""CURITIBA"",
                            ""numeroProtestos"": 3,
                            ""valorTotalProtestosCartorio"": ""R$ 1.706,55"",
                            ""titulos"": [
                              {
                                ""dataProtesto"": ""21/01/2025"",
                                ""valorProtestado"": ""R$ 285,80"",
                                ""documento"": ""34.448.971/0001-10""
                              },
                              {
                                ""dataProtesto"": ""07/11/2024"",
                                ""valorProtestado"": ""R$ 502,75"",
                                ""documento"": ""34.448.971/0001-10""
                              },
                              {
                                ""dataProtesto"": ""30/10/2024"",
                                ""valorProtestado"": ""R$ 918,00"",
                                ""documento"": ""34.448.971/0001-10""
                              }
                            ]
                          }
                        ]
                      }
                    ]
                  }
                }                
            ";

            return;
        }

        protected void CarregaJson_SemSaldo()
        {
            jsonRetornoAPI =
                @"
                    {
	                    ""metaDados"": {
		                    ""consultaNome"": ""Protestos Nacional - IEPTB Online"",
		                    ""consultaUid"": ""direct-c86efbce-36f1-4966-9fbb-dc5aa1cb77a8"",
		                    ""chave"": ""CNPJ=34448971000110;"",
		                    ""usuario"": ""Luiz Sellma"",
		                    ""mensagem"": ""Aviso: Saldo Insuficiente Para Realizar Esta Consulta, A Consulta Não Esta Disponível Para Clientes em Teste"",
		                    ""ip"": ""189.45.159.2"",
		                    ""resultadoId"": 29,
		                    ""resultado"": ""Saldo Insuficiente Para Realizar Esta Consulta"",
		                    ""apiVersao"": ""v3"",
		                    ""enviarCallback"": false,
		                    ""gerarComprovante"": false,
		                    ""urlComprovante"": null,
		                    ""assincrono"": false,
		                    ""data"": ""08/07/2026 11:38:55"",
		                    ""tempoExecucaoMs"": 1033
	                    },
	                    ""retorno"": null
                    }
                ";
        }

        protected void CarregaJson_ParametrosErrados()
        {
            jsonRetornoAPI =
                @"
                    {
	                    ""metaDados"": {
		                    ""consultaNome"": ""Protestos Nacional - IEPTB Online"",
		                    ""consultaUid"": ""direct-9ee62d07-7baa-48f9-b693-8f51d3f26c63"",
		                    ""chave"": """",
		                    ""usuario"": ""Luiz Sellma"",
		                    ""mensagem"": ""Parâmetros não suportados."",
		                    ""ip"": ""189.45.159.2"",
		                    ""resultadoId"": 8,
		                    ""resultado"": ""Parametros Não Suportados"",
		                    ""apiVersao"": ""v3"",
		                    ""enviarCallback"": false,
		                    ""gerarComprovante"": false,
		                    ""urlComprovante"": null,
		                    ""assincrono"": false,
		                    ""data"": ""08/07/2026 11:48:34"",
		                    ""tempoExecucaoMs"": 4
	                    },
	                    ""retorno"": null
                    }
                ";
        }

        protected void CarregaJson_TokenInvalido()
        {
            jsonRetornoAPI =
                @"
                    {
	                    ""metaDados"": {
		                    ""consultaNome"": ""Protestos Nacional - IEPTB Online"",
		                    ""consultaUid"": ""direct-3e50b427-a3cd-40e6-8bca-a53d4697d38d"",
		                    ""chave"": """",
		                    ""usuario"": null,
		                    ""mensagem"": ""IP ou Token inválido. Verifique."",
		                    ""ip"": ""189.45.159.2"",
		                    ""resultadoId"": 3,
		                    ""resultado"": ""Não Autorizado"",
		                    ""apiVersao"": ""v3"",
		                    ""enviarCallback"": false,
		                    ""gerarComprovante"": false,
		                    ""urlComprovante"": null,
		                    ""assincrono"": false,
		                    ""data"": ""08/07/2026 11:48:51"",
		                    ""tempoExecucaoMs"": 0
	                    },
	                    ""retorno"": null
                    }
                ";
        }
    }
}

/*         
               
          
        protected void GravaCartorios()
        {
            if (objCENPROTRetornoClass.data.Count > 0)
            {
                foreach (CENPROTRetornoClass.Data data in objCENPROTRetornoClass.data)
                {
                    Type type = typeof(CENPROTRetornoClass.Data.Cartorios);

                    PropertyInfo[] properties = type.GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        string uf = property.Name;

                        switch (uf)
                        {
                            case "AC":
                                GravaCartorioUF(data.cartorios.AC, uf);
                                break;

                            case "AL":
                                GravaCartorioUF(data.cartorios.AL, uf);
                                break;

                            case "AP":
                                GravaCartorioUF(data.cartorios.AP, uf);
                                break;

                            case "AM":
                                GravaCartorioUF(data.cartorios.AM, uf);
                                break;

                            case "BA":
                                GravaCartorioUF(data.cartorios.BA, uf);
                                break;

                            case "CE":
                                GravaCartorioUF(data.cartorios.CE, uf);
                                break;

                            case "DF":
                                GravaCartorioUF(data.cartorios.DF, uf);
                                break;

                            case "ES":
                                GravaCartorioUF(data.cartorios.ES, uf);
                                break;

                            case "GO":
                                GravaCartorioUF(data.cartorios.GO, uf);
                                break;

                            case "MA":
                                GravaCartorioUF(data.cartorios.MA, uf);
                                break;

                            case "MT":
                                GravaCartorioUF(data.cartorios.MT, uf);
                                break;

                            case "MS":
                                GravaCartorioUF(data.cartorios.MS, uf);
                                break;

                            case "MG":
                                GravaCartorioUF(data.cartorios.MG, uf);
                                break;

                            case "PA":
                                GravaCartorioUF(data.cartorios.PA, uf);
                                break;

                            case "PB":
                                GravaCartorioUF(data.cartorios.PB, uf);
                                break;

                            case "PR":
                                GravaCartorioUF(data.cartorios.PR, uf);
                                break;

                            case "PE":
                                GravaCartorioUF(data.cartorios.PE, uf);
                                break;

                            case "PI":
                                GravaCartorioUF(data.cartorios.PI, uf);
                                break;

                            case "RJ":
                                GravaCartorioUF(data.cartorios.RJ, uf);
                                break;

                            case "RN":
                                GravaCartorioUF(data.cartorios.RN, uf);
                                break;

                            case "RS":
                                GravaCartorioUF(data.cartorios.RS, uf);
                                break;

                            case "RO":
                                GravaCartorioUF(data.cartorios.RO, uf);
                                break;

                            case "RR":
                                GravaCartorioUF(data.cartorios.RR, uf);
                                break;

                            case "SC":
                                GravaCartorioUF(data.cartorios.SC, uf);
                                break;

                            case "SP":
                                GravaCartorioUF(data.cartorios.SP, uf);
                                break;

                            case "SE":
                                GravaCartorioUF(data.cartorios.SE, uf);
                                break;

                            case "TO":
                                GravaCartorioUF(data.cartorios.TO, uf);
                                break;
                        }
                    }
                }
            }
        }
               
        protected void GravaCartorioUF(List<CENPROTRetornoClass.Data.Cartorios.Cartorio> cartorioUFList, string uf)
        {
            if (cartorioUFList != null && cartorioUFList.Count > 0)
            {
                foreach (CENPROTRetornoClass.Data.Cartorios.Cartorio cartorio in cartorioUFList)
                {
                    ObjCliente.Codigo = cartorio.codigo ?? "";

                    ObjCliente.Cartorio = cartorio.nome ?? "";

                    ObjCliente.TelefoneCartorio = cartorio.telefone ?? "";

                    ObjCliente.Endereco = cartorio.endereco ?? "";

                    ObjCliente.Uf = uf;

                    ObjCliente.CidadeCodigo = cartorio.cidade_codigo ?? "";

                    ObjCliente.CodigoIBGE = cartorio.cidade_codigo_ibge ?? "";

                    ObjCliente.Municipio = cartorio.municipio ?? "";

                    ObjCliente.Bairro = cartorio.bairro ?? "";

                    ObjCliente.AtualizacaoData = cartorio.atualizacao_data ?? "";

                    ObjCliente.Quantidade = cartorio.quantidade ?? "";

                    ObjCliente.PeriodoPesquisa = cartorio.periodo_pesquisa ?? "";

                    IDCartorio = ObjCliente.Grava_CRM_CENPROT_CLIENTE_CARTORIOS();

                    GravaProtestos(cartorio.protestos);
                }
            }
        }

        protected void GravaProtestos(List<CENPROTRetornoClass.Data.Cartorios.Cartorio.Protesto> protestos)
        {
            string erro = "";

            if (protestos.Count > 0)
            {
                foreach (CENPROTRetornoClass.Data.Cartorios.Cartorio.Protesto protesto in protestos)
                {
                    ObjCliente.CPFCNPJ = protesto.cpf_cnpj ?? "";

                    ObjCliente.Data = protesto.data ?? "";

                    ObjCliente.DataProtesto = protesto.data_protesto ?? "";

                    ObjCliente.DataProtestoString = protesto.data_protesto_string ?? "";

                    ObjCliente.DataVencimento = protesto.data_vencimento ?? "";

                    ObjCliente.DataVencimentoString = protesto.data_vencimento_string ?? "";

                    ObjCliente.Valor = protesto.valor;

                    ObjCliente.ValorString = protesto.valor_string ?? "";

                    ObjCliente.Chave = protesto.chave ?? "";

                    ObjCliente.NomeApresentante = protesto.nome_apresentante ?? "";

                    ObjCliente.NomeCedente = protesto.nome_cedente ?? "";

                    ObjCliente.TemAnuencia = protesto.tem_anuencia ?? "";

                    ObjCliente.IDCartorio = IDCartorio;

                    erro = ObjCliente.Grava_CRM_CENPROT_CLIENTE_CARTORIOS_PROTESTOS();
                }
            }
        }
        */