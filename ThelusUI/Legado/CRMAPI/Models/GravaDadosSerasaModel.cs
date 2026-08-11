using System;
using VendasWeb;
using System.IO;
using CRMAPI.Classes;
using VendasWeb.classes;
using VendasWeb.WEBServiceCRM;
using VendasWeb.WEBServiceSAP.ClassesWEBService;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using Newtonsoft.Json.Linq;

namespace CRMAPI.Models
{
    public class GravaDadosSerasaModel
    {
        ClienteClasse ObjCliente = new ClienteClasse();
        VendasWeb.GerencialVendas.UtilClass ObjUtilClass = new VendasWeb.GerencialVendas.UtilClass();
        JsonConversao jsonconv = new JsonConversao();
        FuncoesAPIClass OBJApi = new FuncoesAPIClass();
        WSSaidaDadosSerasa objWSSaidaDadosSerasa = new WSSaidaDadosSerasa();

        bool teste = false;

        public string GravaDadosSerasa(WSRecuperaDadosSerasa objWSRecuperaDadosSerasa)
        {
            string erro = "";

            try
            {
                ObjCliente.IDCliente = objWSRecuperaDadosSerasa.IDCliente;

                ObjCliente.IDUsuario = objWSRecuperaDadosSerasa.IDUsuario;

                ParametroGeral objParametroGeral = new ParametroGeral();

                objWSRecuperaDadosSerasa.Produto = objParametroGeral.RetornaValorStringParametro("PRODUTOANALISECREDITOSERASA");

                objWSRecuperaDadosSerasa.Produto = teste ? "RELATOAPI" : objWSRecuperaDadosSerasa.Produto;

                if (objWSRecuperaDadosSerasa.Produto == "RELATO")
                {
                    erro = GravaDadosSerasaRELATO(objWSRecuperaDadosSerasa);
                }
                else if (objWSRecuperaDadosSerasa.Produto == "RELATOAPI")
                {
                    erro = GravaDadosSerasaRELATOAPI(objWSRecuperaDadosSerasa);
                }
                else
                {
                    erro = "Nenhum Produto Relacionado Encontrado.";
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return erro;
        }

        public string GravaDadosSerasaRELATO(WSRecuperaDadosSerasa objWSRecuperaDadosSerasa)
        {
            string erro = "", JSON = "", jsonNomesDosCamposDesformatados = "";

            JSON = jsonconv.ConverteObjectParaJSon<WSRecuperaDadosSerasa>(objWSRecuperaDadosSerasa);

            DadosSerasaModel OBJDadosSerasa = jsonconv.ConverteJSonParaObject<DadosSerasaModel>(JSON);

            erro = OBJDadosSerasa.ConsultaDadosSerasa();

            if (erro == "")
            {
                jsonNomesDosCamposDesformatados = ObjUtilClass.RemoveTraçosEspacosAcentosDosNomesCamposJson(OBJDadosSerasa.GetJSONRetorno());

                erro = ObjCliente.GravaJsonSerasa(jsonNomesDosCamposDesformatados);
            }

            if (erro == "") objWSSaidaDadosSerasa = jsonconv.ConverteJSonParaObject<WSSaidaDadosSerasa>(jsonNomesDosCamposDesformatados);

            if (erro == "") erro = objWSSaidaDadosSerasa.GravaAnaliseSerasa(ObjCliente);

            return erro;
        }

        public string GravaDadosSerasaRELATOAPI(WSRecuperaDadosSerasa objWSRecuperaDadosSerasa)
        {
            string erro = "";

            DadosSerasaModel objDadosSerasa = new DadosSerasaModel();

            objDadosSerasa.NumeroDocumento = objWSRecuperaDadosSerasa.NumeroDocumento;

            string json = teste ? RetornaJsonTeste() : objDadosSerasa.ConsultaDadosSerasaAPI();

            erro = ObjCliente.GravaJsonSerasa(json);

            if (erro == "")
            {
                JsonSerasaRELATOAPIClass objJsonSerasaRELATOAPIClass = jsonconv.ConverteJSonParaObject<JsonSerasaRELATOAPIClass>(json);

                DadosSerasaRELATOAPIClass objDadosSerasaRELATOAPIClass =
                    new DadosSerasaRELATOAPIClass(ObjCliente.IDCliente, ObjCliente.IDUsuario, objJsonSerasaRELATOAPIClass);

                erro = objDadosSerasaRELATOAPIClass.GravaAnalise();
            }

            return erro;
        }

        protected void CalculaTamanhoCamposJson(string json = "")
        {
            try
            {
                // Caminhos dos arquivos
                string jsonFilePath = @"C:\Meus arquivos\Projetos\Arquivos dos projetos\Especi. Pende. e Consul. sql\Projeto CRM Manuli\IntegracaoSerasaAPI\SERASAJASONV2.json";
                string outputFilePath = @"C:\Meus arquivos\Projetos\Arquivos dos projetos\Especi. Pende. e Consul. sql\Projeto CRM Manuli\IntegracaoSerasaAPI\TamanhoCamposjson2.txt";

                // Lê o JSON do arquivo
                if (json == "") json = File.ReadAllText(jsonFilePath);
                JObject jsonObject = JObject.Parse(json);

                using (StreamWriter writer = new StreamWriter(outputFilePath, false)) // 'false' sobrescreve o arquivo
                {
                    writer.WriteLine("Campo\t\tTamanho");
                    writer.WriteLine("----------------------");

                    // Chama o método recursivo para percorrer o JSON
                    PercorrerJson(jsonObject, "", writer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao processar JSON: " + ex.Message);
            }
        }

        private void PercorrerJson(JToken token, string prefixo, StreamWriter writer)
        {
            if (token.Type == JTokenType.Object)
            {
                // Se for um JObject, percorre as propriedades
                JObject obj = (JObject)token;

                foreach (var property in obj.Properties())
                {
                    PercorrerJson(property.Value, $"{prefixo}{property.Name}.", writer);
                }
            }
            else if (token.Type == JTokenType.Array)
            {
                // Se for um JArray, percorre cada item do array
                JArray array = (JArray)token;

                for (int i = 0; i < array.Count; i++)
                {
                    PercorrerJson(array[i], $"{prefixo}[{i}].", writer);
                }
            }
            else
            {
                // Se for um valor simples, escreve o campo e o tamanho
                string campo = prefixo.TrimEnd('.');
                string valor = token.ToString();
                int tamanho = valor.Length;
                writer.WriteLine($"{campo}\t\t{tamanho}");
            }
        }

        protected string RetornaJsonTeste()
        {
            string json =
            @"
{
	""reports"": [
		{
			""reportName"": ""RELATORIO_AVANCADO_PJ"",
			""identificationReport"": {
				""updateDate"": ""2025-05-17"",
				""documentNumber"": ""31096068000140"",
				""statusRegistration"": ""SITUACAO DO CNPJ EM 10/05/2025: ATIVA"",
				""statusCode"": ""2"",
				""statusCodeDescription"": ""ATIVA"",
				""companyName"": ""MULTITERMINAIS ALFANDEGADOS DO BRASIL S/A"",
				""address"": {
					""addressLine"": ""R ANIBAL DE MENDONCA 132 4 ANDAR"",
					""zipCode"": ""22410050"",
					""district"": ""IPANEMA"",
					""city"": ""RIO DE JANEIRO"",
					""state"": ""RJ""
				},
				""phone"": {
					""areaCode"": ""21"",
					""phoneNumber"": ""25808654""
				},
				""companyUrl"": ""WWW.MULTITERMINAIS.COM.BR"",
				""partnership"": ""SOCIEDADE ANONIMA FECHADA"",
				""companyRegister"": ""5397518"",
				""companyRegisterDate"": ""2023-03-30"",
				""companyFoundation"": ""1986-08-08"",
				""numberEmployees"": 455,
				""taxOption"": ""LUCRO REAL"",
				""stateRegistration"": ""85915967"",
				""economicActivity"": ""ARMAZEM GERAL E TRAPICHE"",
				""importPurchases"": 0.0,
				""exportSales"": 0.0,
				""cnae"": ""5211799"",
				""serasaActiveCode"": ""S020500"",
				""branchOffices"": ""21"",
				""nireNumber"": ""33300320369"",
				""predecessorList"": [],
				""reorganizations"": [
					{
						""relatedCompany"": ""08026280000119"",
						""code"": ""5"",
						""description"": ""INC"",
						""date"": ""2007-03-29"",
						""lastChangeDate"": ""2008-11-26"",
						""updateDate"": ""2024-11-10""
					}
				],
				""legalNatureCode"": ""205""
			},
			""negativeData"": {
				""pefin"": {
					""pefinResponse"": [
						{
							""occurrenceDate"": ""2024-09-10"",
							""legalNatureId"": ""VM"",
							""legalNature"": ""VENDA MERCAD"",
							""contractId"": ""152181901"",
							""creditorName"": ""VERISURE BRASIL MONITORAMENTO"",
							""amount"": 334.62,
							""principal"": true,
							""dispute"": {
								""disputeIndicativeFlag"": false
							},
							""cadus"": ""C404958025""
						},
						{
							""occurrenceDate"": ""2024-09-10"",
							""legalNatureId"": ""VM"",
							""legalNature"": ""VENDA MERCAD"",
							""contractId"": ""152186001"",
							""creditorName"": ""VERISURE BRASIL MONITORAMENTO"",
							""amount"": 401.12,
							""principal"": true,
							""dispute"": {
								""disputeIndicativeFlag"": false
							},
							""cadus"": ""C404958024""
						}
					],
					""summary"": {
						""firstOccurrence"": ""2024-09-10"",
						""lastOccurrence"": ""2024-09-10"",
						""count"": 2,
						""balance"": 735.74
					}
				},
				""refin"": {
					""summary"": {
						""count"": 0,
						""balance"": 0.0
					}
				},
				""collectionRecords"": {
					""summary"": {
						""count"": 0,
						""balance"": 0.0
					}
				},
				""check"": {
					""summary"": {
						""count"": 0,
						""balance"": 0.0
					}
				},
				""notary"": {
					""summary"": {
						""count"": 0,
						""balance"": 0.0
					}
				}
			},
			""facts"": {
				""judgementFilings"": {
					""summary"": {
						""count"": 0,
						""balance"": 0.0
					}
				},
				""bankrupts"": {
					""summary"": {
						""count"": 0,
						""balance"": 0.0
					}
				},
				""inquiryCompanyResponse"": {
					""results"": [
						{
							""occurrenceDate"": ""2025-07-04"",
							""companyName"": ""KOVR SEGURADORA S A"",
							""companyDocumentId"": ""42366302000128"",
							""companyAlias"": """",
							""daysQuantity"": 1
						},
						{
							""occurrenceDate"": ""2025-07-03"",
							""companyName"": ""MULTIPLIKE FUNDO DE INVESTIMENTO EM DIREITOS CREDITORIOS - RESPONSABILIDADE LIMITADA"",
							""companyDocumentId"": ""29469420000101"",
							""companyAlias"": """",
							""daysQuantity"": 1
						},
						{
							""occurrenceDate"": ""2025-07-02"",
							""companyName"": ""RTT INFORMATICA E TELECOMUNICACOES LTDA - EPP"",
							""companyDocumentId"": ""31978612000187"",
							""companyAlias"": ""RTT TELECOM"",
							""daysQuantity"": 1
						},
						{
							""occurrenceDate"": ""2025-07-01"",
							""companyName"": ""AMPLA CONSULTORIA E CORRETORA DE SEGUROS LTDA"",
							""companyDocumentId"": ""02443207000166"",
							""companyAlias"": """",
							""daysQuantity"": 1
						},
						{
							""occurrenceDate"": ""2025-07-01"",
							""companyName"": ""BANCO SANTANDER (BRASIL) S.A."",
							""companyDocumentId"": ""90400888000142"",
							""companyAlias"": """",
							""daysQuantity"": 1
						}
					],
					""quantity"": {
						""actual"": 5,
						""historical"": [
							{
								""inquiryDate"": ""2025-06"",
								""occurrences"": 13
							},
							{
								""inquiryDate"": ""2025-05"",
								""occurrences"": 12
							},
							{
								""inquiryDate"": ""2025-04"",
								""occurrences"": 9
							},
							{
								""inquiryDate"": ""2025-03"",
								""occurrences"": 13
							},
							{
								""inquiryDate"": ""2025-02"",
								""occurrences"": 14
							},
							{
								""inquiryDate"": ""2025-01"",
								""occurrences"": 6
							},
							{
								""inquiryDate"": ""2024-12"",
								""occurrences"": 4
							},
							{
								""inquiryDate"": ""2024-11"",
								""occurrences"": 8
							},
							{
								""inquiryDate"": ""2024-10"",
								""occurrences"": 5
							},
							{
								""inquiryDate"": ""2024-09"",
								""occurrences"": 8
							},
							{
								""inquiryDate"": ""2024-08"",
								""occurrences"": 7
							},
							{
								""inquiryDate"": ""2024-07"",
								""occurrences"": 3
							},
							{
								""inquiryDate"": ""2024-06"",
								""occurrences"": 4
							}
						]
					}
				}
			},
			""negativeSummary"": {},
			""checkFilingsHistorical"": {
				""checkFilingsHistoricalResponse"": []
			}
		}
	],
	""optionalFeatures"": {
		""qsaCompleteReport"": {
			""shareCapital"": {},
			""partners"": [],
			""administrators"": [
				{
					""kindPerson"": ""F"",
					""document"": ""610928196"",
					""documentDigit"": ""49"",
					""documentSequence"": ""0000"",
					""name"": ""ANTONIO RIBEIRO GUIMARAES NETO"",
					""office"": ""DIRETOR"",
					""nationality"": ""BRASIL"",
					""maritalStatus"": ""CASADO"",
					""startDateTerm"": ""2016-06-28"",
					""entryDate"": ""2016-06-28"",
					""restrictionIndicator"": false,
					""birthDate"": ""1966-02-08"",
					""relationship"": ""A"",
					""address"": {
						""addressLine"": ""AV PE LEONEL FRANCA 120 UN 1001"",
						""zipCode"": ""22451000"",
						""district"": ""GAVEA"",
						""city"": ""RIO DE JANEIRO"",
						""state"": ""RJ""
					},
					""phone"": {
						""areaCode"": ""21"",
						""phoneNumber"": ""23306285""
					},
					""debts"": [
						{
							""debtType"": ""BANKRUPTSPATICIPATION"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""CHECKCCF"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""COLLECTIONRECORDS"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""FINANCIAL"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""JUDGEMENTFILINGS"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""MARKET"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""NOTARY"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						}
					]
				},
				{
					""kindPerson"": ""F"",
					""document"": ""466409677"",
					""documentDigit"": ""15"",
					""documentSequence"": ""0000"",
					""name"": ""RICARDO AURELIO MARIO VEGA ORELLANA"",
					""office"": ""DIRETOR"",
					""nationality"": ""BRASIL"",
					""maritalStatus"": ""CASADO"",
					""startDateTerm"": ""2016-06-28"",
					""entryDate"": ""2016-06-28"",
					""restrictionIndicator"": false,
					""birthDate"": ""1954-03-26"",
					""relationship"": ""A"",
					""address"": {
						""addressLine"": ""R ENG FONSECA COSTA 153 SL 404"",
						""zipCode"": ""22641160"",
						""district"": ""ITANHANGA"",
						""city"": ""RIO DE JANEIRO"",
						""state"": ""RJ""
					},
					""phone"": {
						""areaCode"": ""21"",
						""phoneNumber"": ""31541926""
					},
					""debts"": [
						{
							""debtType"": ""BANKRUPTSPATICIPATION"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""CHECKCCF"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""COLLECTIONRECORDS"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""FINANCIAL"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""JUDGEMENTFILINGS"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""MARKET"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""NOTARY"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						}
					]
				},
				{
					""kindPerson"": ""F"",
					""document"": ""375663507"",
					""documentDigit"": ""49"",
					""documentSequence"": ""0000"",
					""name"": ""THOMAS KLIEN"",
					""office"": ""DIRETOR"",
					""nationality"": ""BRASIL"",
					""maritalStatus"": ""CASADO"",
					""startDateTerm"": ""2016-06-28"",
					""entryDate"": ""2016-06-28"",
					""restrictionIndicator"": true,
					""birthDate"": ""1956-12-01"",
					""relationship"": ""A"",
					""address"": {
						""addressLine"": ""R ANIBAL DE MENDONCA 132 AND 4"",
						""zipCode"": ""22410050"",
						""district"": ""IPANEMA"",
						""city"": ""RIO DE JANEIRO"",
						""state"": ""RJ""
					},
					""phone"": {
						""areaCode"": ""21"",
						""phoneNumber"": ""24343401""
					},
					""debts"": [
						{
							""debtType"": ""BANKRUPTSPATICIPATION"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""CHECKCCF"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""COLLECTIONRECORDS"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""FINANCIAL"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""JUDGEMENTFILINGS"",
							""summary"": {
								""count"": 0,
								""balance"": 0.0
							}
						},
						{
							""debtType"": ""MARKET"",
							""summary"": {
								""lastOccurrence"": ""2024-07-05"",
								""count"": 1,
								""balance"": 337
							}
						},
						{
							""debtType"": ""NOTARY"",
							""summary"": {
								""lastOccurrence"": ""2024-04-26"",
								""count"": 1,
								""balance"": 2205
							}
						}
					]
				}
			]
		},
		""advancedCommercialPaymentHistory"": {
			""paymentHistory"": {
				""titlesQuantity"": [
					{
						""rangeCode"": ""A5"",
						""name"": ""PONTUAL"",
						""range"": ""15 A 20"",
						""rangeValueFrom"": 15,
						""rangeValueTo"": 20,
						""percentage"": ""93.0% e 95.0%"",
						""percentageFrom"": 9300.0,
						""percentageTo"": 9500.0
					},
					{
						""rangeCode"": """",
						""name"": ""8-15"",
						""range"": ""-"",
						""rangeValueFrom"": 0,
						""rangeValueTo"": 0,
						""percentage"": ""0.0% e 0.0%"",
						""percentageFrom"": 0.0,
						""percentageTo"": 0.0
					},
					{
						""rangeCode"": ""A1"",
						""name"": ""16-30"",
						""range"": ""1 A 3"",
						""rangeValueFrom"": 1,
						""rangeValueTo"": 3,
						""percentage"": ""5.0% e 7.0%"",
						""percentageFrom"": 500.0,
						""percentageTo"": 700.0
					},
					{
						""rangeCode"": """",
						""name"": ""31-60"",
						""range"": ""-"",
						""rangeValueFrom"": 0,
						""rangeValueTo"": 0,
						""percentage"": ""0.0% e 0.0%"",
						""percentageFrom"": 0.0,
						""percentageTo"": 0.0
					},
					{
						""rangeCode"": """",
						""name"": ""+60"",
						""range"": ""-"",
						""rangeValueFrom"": 0,
						""rangeValueTo"": 0,
						""percentage"": ""0.0% e 0.0%"",
						""percentageFrom"": 0.0,
						""percentageTo"": 0.0
					},
					{
						""rangeCode"": ""A1"",
						""name"": ""A VISTA"",
						""range"": ""1 A 3"",
						""rangeValueFrom"": 1,
						""rangeValueTo"": 3,
						""percentage"": ""0.0% e 0.0%"",
						""percentageFrom"": 0.0,
						""percentageTo"": 0.0
					}
				],
				""monthDetail"": {
					""months"": [
						{
							""month"": ""JUN/24"",
							""periodList"": [
								{
									""rangeCode"": ""B6"",
									""name"": ""PONTUAL"",
									""range"": ""4 MIL A 4,5 MIL"",
									""rangeValueFrom"": 4000,
									""rangeValueTo"": 4500,
									""percentage"": ""97.0% e 100.0%"",
									""percentageFrom"": 9700.0,
									""percentageTo"": 10000.0
								},
								{
									""rangeCode"": """",
									""name"": ""8-15"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""16-30"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""31-60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""+60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": ""A10"",
									""name"": ""A VISTA"",
									""range"": ""300 A 350"",
									""rangeValueFrom"": 300,
									""rangeValueTo"": 350,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": ""B6"",
									""name"": ""TOTAL MES"",
									""range"": ""4 MIL A 4,5 MIL"",
									""rangeValueFrom"": 4000,
									""rangeValueTo"": 4500,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								}
							]
						},
						{
							""month"": ""JUL/24"",
							""periodList"": [
								{
									""rangeCode"": ""B12"",
									""name"": ""PONTUAL"",
									""range"": ""7 MIL A 7,5 MIL"",
									""rangeValueFrom"": 7000,
									""rangeValueTo"": 7500,
									""percentage"": ""97.0% e 100.0%"",
									""percentageFrom"": 9700.0,
									""percentageTo"": 10000.0
								},
								{
									""rangeCode"": """",
									""name"": ""8-15"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""16-30"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""31-60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""+60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""A VISTA"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": ""B12"",
									""name"": ""TOTAL MES"",
									""range"": ""7 MIL A 7,5 MIL"",
									""rangeValueFrom"": 7000,
									""rangeValueTo"": 7500,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								}
							]
						},
						{
							""month"": ""AGO/24"",
							""periodList"": [
								{
									""rangeCode"": ""B3"",
									""name"": ""PONTUAL"",
									""range"": ""2,5 MIL A 3 MIL"",
									""rangeValueFrom"": 2500,
									""rangeValueTo"": 3000,
									""percentage"": ""97.0% e 100.0%"",
									""percentageFrom"": 9700.0,
									""percentageTo"": 10000.0
								},
								{
									""rangeCode"": """",
									""name"": ""8-15"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""16-30"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""31-60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""+60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": ""A7"",
									""name"": ""A VISTA"",
									""range"": ""150 A 200"",
									""rangeValueFrom"": 150,
									""rangeValueTo"": 200,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": ""B3"",
									""name"": ""TOTAL MES"",
									""range"": ""2,5 MIL A 3 MIL"",
									""rangeValueFrom"": 2500,
									""rangeValueTo"": 3000,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								}
							]
						},
						{
							""month"": ""SET/24"",
							""periodList"": [
								{
									""rangeCode"": ""A9"",
									""name"": ""PONTUAL"",
									""range"": ""250 A 300"",
									""rangeValueFrom"": 250,
									""rangeValueTo"": 300,
									""percentage"": ""97.0% e 100.0%"",
									""percentageFrom"": 9700.0,
									""percentageTo"": 10000.0
								},
								{
									""rangeCode"": """",
									""name"": ""8-15"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""16-30"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""31-60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""+60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""A VISTA"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": ""A9"",
									""name"": ""TOTAL MES"",
									""range"": ""250 A 300"",
									""rangeValueFrom"": 250,
									""rangeValueTo"": 300,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								}
							]
						},
						{
							""month"": ""NOV/24"",
							""periodList"": [
								{
									""rangeCode"": ""B6"",
									""name"": ""PONTUAL"",
									""range"": ""4 MIL A 4,5 MIL"",
									""rangeValueFrom"": 4000,
									""rangeValueTo"": 4500,
									""percentage"": ""97.0% e 100.0%"",
									""percentageFrom"": 9700.0,
									""percentageTo"": 10000.0
								},
								{
									""rangeCode"": """",
									""name"": ""8-15"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""16-30"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""31-60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""+60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""A VISTA"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": ""B6"",
									""name"": ""TOTAL MES"",
									""range"": ""4 MIL A 4,5 MIL"",
									""rangeValueFrom"": 4000,
									""rangeValueTo"": 4500,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								}
							]
						},
						{
							""month"": ""JAN/25"",
							""periodList"": [
								{
									""rangeCode"": """",
									""name"": ""PONTUAL"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""8-15"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""16-30"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""31-60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""+60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": ""A3"",
									""name"": ""A VISTA"",
									""range"": ""30 A 50"",
									""rangeValueFrom"": 30,
									""rangeValueTo"": 50,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": ""A3"",
									""name"": ""TOTAL MES"",
									""range"": ""30 A 50"",
									""rangeValueFrom"": 30,
									""rangeValueTo"": 50,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								}
							]
						},
						{
							""month"": ""MAR/25"",
							""periodList"": [
								{
									""rangeCode"": ""B11"",
									""name"": ""PONTUAL"",
									""range"": ""6,5 MIL A 7 MIL"",
									""rangeValueFrom"": 6500,
									""rangeValueTo"": 7000,
									""percentage"": ""97.0% e 100.0%"",
									""percentageFrom"": 9700.0,
									""percentageTo"": 10000.0
								},
								{
									""rangeCode"": """",
									""name"": ""8-15"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""16-30"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""31-60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""+60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""A VISTA"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": ""B11"",
									""name"": ""TOTAL MES"",
									""range"": ""6,5 MIL A 7 MIL"",
									""rangeValueFrom"": 6500,
									""rangeValueTo"": 7000,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								}
							]
						},
						{
							""month"": ""ABR/25"",
							""periodList"": [
								{
									""rangeCode"": ""B13"",
									""name"": ""PONTUAL"",
									""range"": ""7,5 MIL A 8 MIL"",
									""rangeValueFrom"": 7500,
									""rangeValueTo"": 8000,
									""percentage"": ""87.0% e 89.0%"",
									""percentageFrom"": 8700.0,
									""percentageTo"": 8900.0
								},
								{
									""rangeCode"": """",
									""name"": ""8-15"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": ""A24"",
									""name"": ""16-30"",
									""range"": ""1 MIL A 1,5 MIL"",
									""rangeValueFrom"": 1000,
									""rangeValueTo"": 1500,
									""percentage"": ""11.0% e 13.0%"",
									""percentageFrom"": 1100.0,
									""percentageTo"": 1300.0
								},
								{
									""rangeCode"": """",
									""name"": ""31-60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""+60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""A VISTA"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": ""B15"",
									""name"": ""TOTAL MES"",
									""range"": ""8,5 MIL A 9 MIL"",
									""rangeValueFrom"": 8500,
									""rangeValueTo"": 9000,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								}
							]
						},
						{
							""month"": ""MAI/25"",
							""periodList"": [
								{
									""rangeCode"": ""B6"",
									""name"": ""PONTUAL"",
									""range"": ""4 MIL A 4,5 MIL"",
									""rangeValueFrom"": 4000,
									""rangeValueTo"": 4500,
									""percentage"": ""97.0% e 100.0%"",
									""percentageFrom"": 9700.0,
									""percentageTo"": 10000.0
								},
								{
									""rangeCode"": """",
									""name"": ""8-15"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""16-30"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""31-60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""+60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""A VISTA"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": ""B6"",
									""name"": ""TOTAL MES"",
									""range"": ""4 MIL A 4,5 MIL"",
									""rangeValueFrom"": 4000,
									""rangeValueTo"": 4500,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								}
							]
						},
						{
							""month"": ""JUL/25"",
							""periodList"": [
								{
									""rangeCode"": ""B19"",
									""name"": ""PONTUAL"",
									""range"": ""13 MIL A 15 MIL"",
									""rangeValueFrom"": 13000,
									""rangeValueTo"": 15000,
									""percentage"": ""97.0% e 100.0%"",
									""percentageFrom"": 9700.0,
									""percentageTo"": 10000.0
								},
								{
									""rangeCode"": """",
									""name"": ""8-15"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""16-30"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""31-60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""+60"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": """",
									""name"": ""A VISTA"",
									""range"": ""-"",
									""rangeValueFrom"": 0,
									""rangeValueTo"": 0,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								},
								{
									""rangeCode"": ""B19"",
									""name"": ""TOTAL MES"",
									""range"": ""13 MIL A 15 MIL"",
									""rangeValueFrom"": 13000,
									""rangeValueTo"": 15000,
									""percentage"": ""0.0% e 0.0%"",
									""percentageFrom"": 0.0,
									""percentageTo"": 0.0
								}
							]
						}
					],
					""summary"": {
						""punctual"": {
							""periodDescription"": ""PONTUAL"",
							""totalValueRangeCode"": ""C9"",
							""totalValueRangeDescription"": ""50 MIL A 70 MIL"",
							""totalValueFrom"": 50000,
							""totalValueTo"": 70000,
							""averageValueRangeCode"": ""B8"",
							""averageValueRangeDescription"": ""5 MIL A 5,5 MIL"",
							""percentageValueFrom"": 97.0,
							""percentageValueTo"": 100.0,
							""averagePaymentDelayPeriodRangeValueFrom"": 0,
							""averagePaymentDelayPeriodRangeValueTo"": 0,
							""historicalAverageRangeFrom"": 5000,
							""historicalAverageRangeTo"": 5500,
							""originCode"": 0
						},
						""period8To15"": {
							""periodDescription"": ""8-15"",
							""totalValueRangeCode"": """",
							""totalValueRangeDescription"": ""-"",
							""totalValueFrom"": 0,
							""totalValueTo"": 0,
							""averageValueRangeCode"": """",
							""averageValueRangeDescription"": ""-"",
							""percentageValueFrom"": 0.0,
							""percentageValueTo"": 0.0,
							""averagePaymentDelayPeriodRangeValueFrom"": 0,
							""averagePaymentDelayPeriodRangeValueTo"": 0,
							""historicalAverageRangeFrom"": 0,
							""historicalAverageRangeTo"": 0,
							""originCode"": 0
						},
						""period16To30"": {
							""periodDescription"": ""16-30"",
							""totalValueRangeCode"": ""A24"",
							""totalValueRangeDescription"": ""1 MIL A 1,5 MIL"",
							""totalValueFrom"": 1000,
							""totalValueTo"": 1500,
							""averageValueRangeCode"": ""A6"",
							""averageValueRangeDescription"": ""100 A 150"",
							""percentageValueFrom"": 1.0,
							""percentageValueTo"": 3.0,
							""averagePaymentDelayPeriodRangeValueFrom"": 0,
							""averagePaymentDelayPeriodRangeValueTo"": 0,
							""historicalAverageRangeFrom"": 100,
							""historicalAverageRangeTo"": 150,
							""originCode"": 0
						},
						""period31To60"": {
							""periodDescription"": ""31-60"",
							""totalValueRangeCode"": """",
							""totalValueRangeDescription"": ""-"",
							""totalValueFrom"": 0,
							""totalValueTo"": 0,
							""averageValueRangeCode"": """",
							""averageValueRangeDescription"": ""-"",
							""percentageValueFrom"": 0.0,
							""percentageValueTo"": 0.0,
							""averagePaymentDelayPeriodRangeValueFrom"": 0,
							""averagePaymentDelayPeriodRangeValueTo"": 0,
							""historicalAverageRangeFrom"": 0,
							""historicalAverageRangeTo"": 0,
							""originCode"": 0
						},
						""periodGT60"": {
							""periodDescription"": ""+60"",
							""totalValueRangeCode"": """",
							""totalValueRangeDescription"": ""-"",
							""totalValueFrom"": 0,
							""totalValueTo"": 0,
							""averageValueRangeCode"": """",
							""averageValueRangeDescription"": ""-"",
							""percentageValueFrom"": 0.0,
							""percentageValueTo"": 0.0,
							""averagePaymentDelayPeriodRangeValueFrom"": 0,
							""averagePaymentDelayPeriodRangeValueTo"": 0,
							""historicalAverageRangeFrom"": 0,
							""historicalAverageRangeTo"": 0,
							""originCode"": 0
						},
						""spotPayment"": {
							""periodDescription"": ""A VISTA"",
							""totalValueRangeCode"": ""A15"",
							""totalValueRangeDescription"": ""550 A 600"",
							""totalValueFrom"": 550,
							""totalValueTo"": 600,
							""averageValueRangeCode"": ""A4"",
							""averageValueRangeDescription"": ""50 A 70"",
							""percentageValueFrom"": 0.0,
							""percentageValueTo"": 0.0,
							""averagePaymentDelayPeriodRangeValueFrom"": 0,
							""averagePaymentDelayPeriodRangeValueTo"": 0,
							""historicalAverageRangeFrom"": 50,
							""historicalAverageRangeTo"": 70,
							""originCode"": 0
						},
						""total"": {
							""periodDescription"": ""TOTAL MES"",
							""totalValueRangeCode"": ""C9"",
							""totalValueRangeDescription"": ""50 MIL A 70 MIL"",
							""totalValueFrom"": 50000,
							""totalValueTo"": 70000,
							""averageValueRangeCode"": ""B8"",
							""averageValueRangeDescription"": ""5 MIL A 5,5 MIL"",
							""percentageValueFrom"": 0.0,
							""percentageValueTo"": 0.0,
							""averagePaymentDelayPeriodRangeValueFrom"": 0,
							""averagePaymentDelayPeriodRangeValueTo"": 0,
							""historicalAverageRangeFrom"": 5000,
							""historicalAverageRangeTo"": 5500,
							""originCode"": 0
						}
					}
				},
				""averageDelayPeriod"": {
					""periodList"": [
						{
							""period"": ""JUN/24"",
							""averageDelayDaysFrom"": 0,
							""averageDelayDaysTo"": 0
						},
						{
							""period"": ""JUL/24"",
							""averageDelayDaysFrom"": 0,
							""averageDelayDaysTo"": 0
						},
						{
							""period"": ""AGO/24"",
							""averageDelayDaysFrom"": 0,
							""averageDelayDaysTo"": 0
						},
						{
							""period"": ""SET/24"",
							""averageDelayDaysFrom"": 0,
							""averageDelayDaysTo"": 0
						},
						{
							""period"": ""NOV/24"",
							""averageDelayDaysFrom"": 0,
							""averageDelayDaysTo"": 0
						},
						{
							""period"": ""JAN/25"",
							""averageDelayDaysFrom"": 0,
							""averageDelayDaysTo"": 0
						},
						{
							""period"": ""MAR/25"",
							""averageDelayDaysFrom"": 0,
							""averageDelayDaysTo"": 0
						},
						{
							""period"": ""ABR/25"",
							""averageDelayDaysFrom"": 1,
							""averageDelayDaysTo"": 3
						},
						{
							""period"": ""MAI/25"",
							""averageDelayDaysFrom"": 0,
							""averageDelayDaysTo"": 0
						},
						{
							""period"": ""JUL/25"",
							""averageDelayDaysFrom"": 0,
							""averageDelayDaysTo"": 0
						}
					],
					""summary"": {
						""averageDelayDaysFrom"": 0,
						""averageDelayDaysTo"": 0
					}
				}
			},
			""mainSuppliers"": {
				""lastUpdateDate"": ""2025-07-03"",
				""mainSuppliersList"": [
					{
						""supplierName"": ""SCANSOURCE BRASIL DISTRIBUIDORA DE TECNOLOGIAS LTDA"",
						""supplierDocument"": ""05607657000135""
					},
					{
						""supplierName"": ""ATACADAO PAPELEX LTDA"",
						""supplierDocument"": ""16731862000124""
					},
					{
						""supplierName"": ""REFRIGELO CLIMATIZACAO DE AMBIENTES S/A"",
						""supplierDocument"": ""61502324000112""
					},
					{
						""supplierName"": ""TELCABOS TELECOMUNICACOES E INFORMATICA LTDA"",
						""supplierDocument"": ""71680193000117""
					},
					{
						""supplierName"": ""FRIGELAR COMERCIO E INDUSTRIA LTDA"",
						""supplierDocument"": ""92660406000119""
					}
				]
			},
			""relationshipSuppliersPeriods"": {
				""lastUpdateDate"": ""2025-07-09"",
				""relationshipSuppliersPeriodList"": [
					{
						""relationshipPeriodDescription"": ""0-6 MESES:"",
						""relationshipSourceQuantity"": 3
					},
					{
						""relationshipPeriodDescription"": ""6MES-1ANO:"",
						""relationshipSourceQuantity"": 0
					},
					{
						""relationshipPeriodDescription"": ""1-3ANOS:"",
						""relationshipSourceQuantity"": 0
					},
					{
						""relationshipPeriodDescription"": ""3-5ANOS:"",
						""relationshipSourceQuantity"": 1
					},
					{
						""relationshipPeriodDescription"": ""5-10ANOS:"",
						""relationshipSourceQuantity"": 3
					},
					{
						""relationshipPeriodDescription"": ""+10ANOS:"",
						""relationshipSourceQuantity"": 7
					},
					{
						""relationshipPeriodDescription"": ""INAT.:"",
						""relationshipSourceQuantity"": 0
					}
				],
				""summary"": {
					""sourcesTotal"": 14,
					""paymentHistorySources"": 8,
					""paymentHistoryValuesSources"": 0,
					""evolutionCommitmentsSources"": 0,
					""businessReferencesSources"": 0,
					""spotPaymentBusinessReferencesSources"": 0
				}
			},
			""evolutionCommitmentsSuppliers"": {
				""lastUpdateDate"": ""2025-07-09"",
				""evolutionCommitmentsSuppliersList"": [
					{
						""yearCommitment"": ""25"",
						""monthCommitment"": ""7"",
						""descriptionMonthCommitment"": ""JUL"",
						""trackCodeToExpire"": ""B3"",
						""trackDescriptionToExpire"": ""2,5 MIL A 3 MIL"",
						""valueCommitmentsDueFrom"": ""2500"",
						""valueCommitmentsDueTo"": ""3000"",
						""totalMonthRangeCode"": ""B3"",
						""totalMonthRangeDescription"": ""2,5 MIL A 3 MIL"",
						""totalMonthlyRangeValueFrom"": ""2500"",
						""totalMonthlyRangeValueTo"": ""3000"",
						""segmentInformation"": ""000""
					},
					{
						""yearCommitment"": ""25"",
						""monthCommitment"": ""6"",
						""descriptionMonthCommitment"": ""JUN"",
						""trackCodeToExpire"": ""B3"",
						""trackDescriptionToExpire"": ""2,5 MIL A 3 MIL"",
						""valueCommitmentsDueFrom"": ""2500"",
						""valueCommitmentsDueTo"": ""3000"",
						""totalMonthRangeCode"": ""B3"",
						""totalMonthRangeDescription"": ""2,5 MIL A 3 MIL"",
						""totalMonthlyRangeValueFrom"": ""2500"",
						""totalMonthlyRangeValueTo"": ""3000"",
						""segmentInformation"": ""000""
					},
					{
						""yearCommitment"": ""25"",
						""monthCommitment"": ""5"",
						""descriptionMonthCommitment"": ""MAI"",
						""trackCodeToExpire"": """",
						""trackDescriptionToExpire"": ""-"",
						""valueCommitmentsDueFrom"": ""0"",
						""valueCommitmentsDueTo"": ""0"",
						""totalMonthRangeCode"": """",
						""totalMonthRangeDescription"": ""-"",
						""totalMonthlyRangeValueFrom"": ""0"",
						""totalMonthlyRangeValueTo"": ""0"",
						""segmentInformation"": ""000""
					},
					{
						""yearCommitment"": ""25"",
						""monthCommitment"": ""4"",
						""descriptionMonthCommitment"": ""ABR"",
						""trackCodeToExpire"": ""B6"",
						""trackDescriptionToExpire"": ""4 MIL A 4,5 MIL"",
						""valueCommitmentsDueFrom"": ""4000"",
						""valueCommitmentsDueTo"": ""4500"",
						""totalMonthRangeCode"": ""B6"",
						""totalMonthRangeDescription"": ""4 MIL A 4,5 MIL"",
						""totalMonthlyRangeValueFrom"": ""4000"",
						""totalMonthlyRangeValueTo"": ""4500"",
						""segmentInformation"": ""000""
					},
					{
						""yearCommitment"": ""25"",
						""monthCommitment"": ""3"",
						""descriptionMonthCommitment"": ""MAR"",
						""trackCodeToExpire"": ""A24"",
						""trackDescriptionToExpire"": ""1 MIL A 1,5 MIL"",
						""valueCommitmentsDueFrom"": ""1000"",
						""valueCommitmentsDueTo"": ""1500"",
						""totalMonthRangeCode"": ""A24"",
						""totalMonthRangeDescription"": ""1 MIL A 1,5 MIL"",
						""totalMonthlyRangeValueFrom"": ""1000"",
						""totalMonthlyRangeValueTo"": ""1500"",
						""segmentInformation"": ""000""
					},
					{
						""yearCommitment"": ""25"",
						""monthCommitment"": ""2"",
						""descriptionMonthCommitment"": ""FEV"",
						""trackCodeToExpire"": ""B13"",
						""trackDescriptionToExpire"": ""7,5 MIL A 8 MIL"",
						""valueCommitmentsDueFrom"": ""7500"",
						""valueCommitmentsDueTo"": ""8000"",
						""totalMonthRangeCode"": ""B13"",
						""totalMonthRangeDescription"": ""7,5 MIL A 8 MIL"",
						""totalMonthlyRangeValueFrom"": ""7500"",
						""totalMonthlyRangeValueTo"": ""8000"",
						""segmentInformation"": ""000""
					},
					{
						""yearCommitment"": ""25"",
						""monthCommitment"": ""1"",
						""descriptionMonthCommitment"": ""JAN"",
						""trackCodeToExpire"": """",
						""trackDescriptionToExpire"": ""-"",
						""valueCommitmentsDueFrom"": ""0"",
						""valueCommitmentsDueTo"": ""0"",
						""totalMonthRangeCode"": """",
						""totalMonthRangeDescription"": ""-"",
						""totalMonthlyRangeValueFrom"": ""0"",
						""totalMonthlyRangeValueTo"": ""0"",
						""segmentInformation"": ""000""
					},
					{
						""yearCommitment"": ""24"",
						""monthCommitment"": ""12"",
						""descriptionMonthCommitment"": ""DEZ"",
						""trackCodeToExpire"": """",
						""trackDescriptionToExpire"": ""-"",
						""valueCommitmentsDueFrom"": ""0"",
						""valueCommitmentsDueTo"": ""0"",
						""totalMonthRangeCode"": """",
						""totalMonthRangeDescription"": ""-"",
						""totalMonthlyRangeValueFrom"": ""0"",
						""totalMonthlyRangeValueTo"": ""0"",
						""segmentInformation"": ""000""
					},
					{
						""yearCommitment"": ""24"",
						""monthCommitment"": ""11"",
						""descriptionMonthCommitment"": ""NOV"",
						""trackCodeToExpire"": """",
						""trackDescriptionToExpire"": ""-"",
						""valueCommitmentsDueFrom"": ""0"",
						""valueCommitmentsDueTo"": ""0"",
						""totalMonthRangeCode"": """",
						""totalMonthRangeDescription"": ""-"",
						""totalMonthlyRangeValueFrom"": ""0"",
						""totalMonthlyRangeValueTo"": ""0"",
						""segmentInformation"": ""000""
					},
					{
						""yearCommitment"": ""24"",
						""monthCommitment"": ""10"",
						""descriptionMonthCommitment"": ""OUT"",
						""trackCodeToExpire"": ""B6"",
						""trackDescriptionToExpire"": ""4 MIL A 4,5 MIL"",
						""valueCommitmentsDueFrom"": ""4000"",
						""valueCommitmentsDueTo"": ""4500"",
						""totalMonthRangeCode"": ""B6"",
						""totalMonthRangeDescription"": ""4 MIL A 4,5 MIL"",
						""totalMonthlyRangeValueFrom"": ""4000"",
						""totalMonthlyRangeValueTo"": ""4500"",
						""segmentInformation"": ""000""
					},
					{
						""yearCommitment"": ""24"",
						""monthCommitment"": ""9"",
						""descriptionMonthCommitment"": ""SET"",
						""trackCodeToExpire"": """",
						""trackDescriptionToExpire"": ""-"",
						""valueCommitmentsDueFrom"": ""0"",
						""valueCommitmentsDueTo"": ""0"",
						""totalMonthRangeCode"": """",
						""totalMonthRangeDescription"": ""-"",
						""totalMonthlyRangeValueFrom"": ""0"",
						""totalMonthlyRangeValueTo"": ""0"",
						""segmentInformation"": ""000""
					},
					{
						""yearCommitment"": ""24"",
						""monthCommitment"": ""8"",
						""descriptionMonthCommitment"": ""AGO"",
						""trackCodeToExpire"": ""A9"",
						""trackDescriptionToExpire"": ""250 A 300"",
						""valueCommitmentsDueFrom"": ""250"",
						""valueCommitmentsDueTo"": ""300"",
						""totalMonthRangeCode"": ""A9"",
						""totalMonthRangeDescription"": ""250 A 300"",
						""totalMonthlyRangeValueFrom"": ""250"",
						""totalMonthlyRangeValueTo"": ""300"",
						""segmentInformation"": ""000""
					},
					{
						""yearCommitment"": ""24"",
						""monthCommitment"": ""7"",
						""descriptionMonthCommitment"": ""JUL"",
						""trackCodeToExpire"": ""B12"",
						""trackDescriptionToExpire"": ""7 MIL A 7,5 MIL"",
						""valueCommitmentsDueFrom"": ""7000"",
						""valueCommitmentsDueTo"": ""7500"",
						""totalMonthRangeCode"": ""B12"",
						""totalMonthRangeDescription"": ""7 MIL A 7,5 MIL"",
						""totalMonthlyRangeValueFrom"": ""7000"",
						""totalMonthlyRangeValueTo"": ""7500"",
						""segmentInformation"": ""000""
					},
					{
						""yearCommitment"": ""24"",
						""monthCommitment"": ""6"",
						""descriptionMonthCommitment"": ""JUN"",
						""trackCodeToExpire"": ""B12"",
						""trackDescriptionToExpire"": ""7 MIL A 7,5 MIL"",
						""valueCommitmentsDueFrom"": ""7000"",
						""valueCommitmentsDueTo"": ""7500"",
						""totalMonthRangeCode"": ""B12"",
						""totalMonthRangeDescription"": ""7 MIL A 7,5 MIL"",
						""totalMonthlyRangeValueFrom"": ""7000"",
						""totalMonthlyRangeValueTo"": ""7500"",
						""segmentInformation"": ""000""
					}
				],
				""summary"": {
					""total"": {
						""periodDescription"": ""TOTAL"",
						""overdueTotalRangeCode"": """",
						""overdueTotalRangeDescription"": ""-"",
						""overdueTotalFrom"": 0,
						""overdueTotalTo"": 0,
						""upcomingValueRangeCode"": ""C4"",
						""upcomingValueRangeDescription"": ""37 MIL A 40 MIL"",
						""upcomingValueFrom"": 37000,
						""upcomingValueTo"": 40000
					}
				}
			},
			""businessReferences"": {
				""lastUpdateDate"": ""2025-07-09"",
				""businessReferencesList"": [
					{
						""businessDescription"": ""ULTIMA COMPRA"",
						""yearPotentialDate"": ""2025"",
						""monthPotentialDate"": ""6"",
						""potentialValueRangeCode"": ""B3"",
						""potentialValueRangeDescription"": ""2,5 MIL A 3 MIL"",
						""potentialValueFrom"": ""2500"",
						""potentialValueTo"": ""3000"",
						""potentialMidrangeCode"": ""B3"",
						""potentialMidrangeDescription"": ""2,5 MIL A 3 MIL"",
						""potentialMidrangeValueFrom"": ""2500"",
						""potentialMidrangeValueTo"": ""3000""
					},
					{
						""businessDescription"": ""MAIOR FATURA"",
						""yearPotentialDate"": ""2025"",
						""monthPotentialDate"": ""6"",
						""potentialValueRangeCode"": ""B19"",
						""potentialValueRangeDescription"": ""13 MIL A 15 MIL"",
						""potentialValueFrom"": ""13000"",
						""potentialValueTo"": ""15000"",
						""potentialMidrangeCode"": ""B9"",
						""potentialMidrangeDescription"": ""5,5 MIL A 6 MIL"",
						""potentialMidrangeValueFrom"": ""5500"",
						""potentialMidrangeValueTo"": ""6000""
					},
					{
						""businessDescription"": ""MAIOR ACUMULO"",
						""yearPotentialDate"": ""2025"",
						""monthPotentialDate"": ""6"",
						""potentialValueRangeCode"": ""B23"",
						""potentialValueRangeDescription"": ""23 MIL A 25 MIL"",
						""potentialValueFrom"": ""23000"",
						""potentialValueTo"": ""25000"",
						""potentialMidrangeCode"": ""B12"",
						""potentialMidrangeDescription"": ""7 MIL A 7,5 MIL"",
						""potentialMidrangeValueFrom"": ""7000"",
						""potentialMidrangeValueTo"": ""7500""
					}
				]
			}
		}
	}
}
            ";

            return json;
        }
    }
}