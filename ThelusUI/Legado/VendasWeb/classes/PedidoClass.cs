using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.WEBServiceSAP;
using VendasWeb.WEBServiceSAP.ClassesWEBService;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using VendasWeb.classes;
using VendasWeb.WEBServiceCRM;

namespace VendasWeb.GerencialVendas
{
    public class PedidoClass : clsConexao
    {
        #region Campos

        public string EmpCod { get; set; }
        public string UsuCod { get; set; }
        public string PedVendaTipo { get; set; }
        public string PedVendaStatDescr { get; set; }
        public string DescricaoStatus { get; set; }
        public int Nivel { get; set; }
        public string valorConsulta { get; set; }
        public string PedVendaNum { get; set; }
        public string PedVendaNumSAP { get; set; }
        public string PedVendaNumCopia { get; set; }
        public int NumeroPedidoSAP { get; set; }
        public int NumeroEsbocoSAP { get; set; }

        public string EntCod { get; set; }
        public string CodigoClienteSAP { get; set; }
        public string CodigoProdutoSAP { get; set; }
        public string EntNome { get; set; }
        public string NfNum { get; set; }

        public string EmpNome { get; set; }
        public string EntCpfCgc { get; set; }
        public string PedVendaData { get; set; }
        public string NFHoraSaida { get; set; }
        public string EntEnderCompleto { get; set; }
        public string EntBair { get; set; }
        public string CidNome { get; set; }
        public string UfSigla { get; set; }
        public string EntCep { get; set; }
        public string CondPagCod { get; set; }
        public string CondPagPedVendaNome { get; set; }
        public string PedVendaNatOpProd { get; set; }
        public string NatOpNome { get; set; }
        public string VendCod { get; set; }
        public string VendNome { get; set; }
        public string PedVendaValMerc { get; set; }
        public string PedVendaValIpiCalc { get; set; }
        public string PedVendaValIcms { get; set; }
        public string IcmsDiferido { get; set; }
        public string IcmsDevido { get; set; }
        public string PedVendaValTotal { get; set; }
        public string EntCodTransp { get; set; }
        public string EntNomeTransp { get; set; }
        public string PedVendaStatFrete { get; set; }
        public string PedVendaTexto { get; set; }
        public string PedVendaTextoHist { get; set; }
        public string HistoricoLiberacoes { get; set; }
        public string ItensFormatados { get; set; }
        public string ClicheFormatados { get; set; }
        public string NFETransChvAcesso { get; set; }
        public int IDStatusCliente { get; set; }

        //Consulta Produtos
        public string ProdCodEstr { get; set; }
        public string ProdNome { get; set; }
        public string Produtos { get; set; }
        public string UsuCodCopia { get; set; }
        public string NumeroNotaFiscal { get; set; }

        public string PrevisaoEntrega { get; set; }

        public int IDEmpresa { get; set; }
        public int IDPedido { get; set; }
        public int IDTipo { get; set; }
        public int IDEvento { get; set; }
        public int IDCategoria { get; set; }
        public int IDUsuario { get; set; }
        public string Historico { get; set; }

        public string DataInicial { get; set; }
        public string DataFinal { get; set; }

        #endregion

        FuncoesAPIClass OBJApi = new FuncoesAPIClass();

        SQLUtilClass objSQLUtilClass = new SQLUtilClass();

        public DataTable Lista_Pedidos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CRM_PEDIDO_VENDA", dbConnection);

                    dbCommand.CommandTimeout = 999999;

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.VarChar, 100, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.VarChar, 100, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 100, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeCliente", SqlDbType.VarChar, 100, "NomeCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.VarChar, 100, "CodigoClienteSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigosStatus", SqlDbType.VarChar, 31, "valorConsulta"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroNotaFiscal", SqlDbType.VarChar, 100, "NumeroNotaFiscal"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoProdutoSAP", SqlDbType.VarChar, 100, "CodigoProdutoSAP"));

                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 100, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 100, "DataFinal"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.EmpCod;
                    dbCommand.Parameters["@IDPedido"].Value = this.PedVendaNum ?? "";
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.PedVendaNumSAP ?? "";
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.UsuCod;
                    dbCommand.Parameters["@CodigosStatus"].Value = this.PedVendaStatDescr ?? "";
                    dbCommand.Parameters["@NumeroNotaFiscal"].Value = this.NumeroNotaFiscal ?? "";
                    dbCommand.Parameters["@CodigoClienteSAP"].Value = this.EntCod ?? "";
                    dbCommand.Parameters["@NomeCliente"].Value = this.EntNome ?? "";
                    dbCommand.Parameters["@CodigoProdutoSAP"].Value = this.CodigoProdutoSAP ?? "";

                    dbCommand.Parameters["@DataInicial"].Value = this.DataInicial ?? "";
                    dbCommand.Parameters["@DataFinal"].Value = this.DataFinal ?? "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable Consulta_Pedido()
        {
            //Chama web service para atualizar impostos conforme SAP
            Atualiza_Valores_Impostos();

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CONSULTA_PEDIDO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));

                    dbCommand.Parameters["@IDPedido"].Value = this.PedVendaNum;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.EmpCod;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                        HistoricoLiberacoes = "";

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                EmpCod = row["IDEmpresa"].ToString();

                                PedVendaTipo = "";
                                PedVendaStatDescr = row["DescricaoStatus"].ToString();
                                //PedVendaNum = row["PedVendaNum"].ToString();

                                EntCod = row["IDCliente"].ToString();
                                EntNome = row["NomeCliente"].ToString();
                                EmpNome = row["NomeEmpresa"].ToString();
                                EntCpfCgc = row["CNPJ"].ToString();
                                PedVendaData = row["DataLancamento"].ToString();
                                NFHoraSaida = row["DataEntrega"].ToString();
                                EntEnderCompleto = row["Endereco"].ToString();
                                EntBair = row["Bairro"].ToString();
                                CidNome = row["Cidade"].ToString();
                                UfSigla = row["CodigoEstadoSAP"].ToString();
                                EntCep = row["CEP"].ToString();
                                CondPagCod = row["IDCondPag"].ToString();
                                CondPagPedVendaNome = row["NomeCondicao"].ToString();
                                CodigoClienteSAP = row["CodigoClienteSAP"].ToString() ?? "";
                                //Recuperar codigo do Imposto do SAP
                                //PedVendaNatOpProd = row["PedVendaNatOpProd"].ToString();
                                //NatOpNome = row["NatOpNome"].ToString();
                                VendCod = row["IDVendedor"].ToString();
                                VendNome = row["NomeVendedor"].ToString();
                                NumeroPedidoSAP = Convert.ToInt32(row["NumeroPedidoSAP"] ?? "0");
                                NumeroEsbocoSAP = Convert.ToInt32(row["NumeroEsbocoSAP"] ?? "0");

                                //Campos a serem recuperados no SAP
                                PedVendaValMerc = string.Format("{0:N}", row["ValorTotalItens"] ?? "0,00");
                                PedVendaValIpiCalc = string.Format("{0:N}", row["ValorIPI"] ?? "0,00");
                                PedVendaValIcms = string.Format("{0:N}", row["ValorICMS"] ?? "0,00");
                                IcmsDiferido = string.Format("{0:N}", row["IcmsDiferido"] ?? "0,00");
                                IcmsDevido = string.Format("{0:N}", row["IcmsDevido"] ?? "0,00");
                                PedVendaValTotal = string.Format("{0:N}", row["TotalPedidoImpostos"] ?? "");
                                //Fim recuperação SAP

                                EntCodTransp = row["TRCodigoClienteSAP"].ToString();
                                EntNomeTransp = row["NomeTransportadora"].ToString();
                                PedVendaStatFrete = row["DescricaoFrete"].ToString();
                                PedVendaTexto = row["ObservacaoNotaFiscal"].ToString();
                                PedVendaTextoHist = row["ObservacaoPedido"].ToString();
                                ItensFormatados = row["ItensFormatados"].ToString();
                                ClicheFormatados = row["ClicheFormatados"].ToString();
                                this.IDStatusCliente = Convert.ToInt32(row["IDStatus"]);
                                //NFETransChvAcesso = row["NFETransChvAcesso"].ToString();

                                /*
                                if (HistoricoLiberacoes != "")
                                {
                                    HistoricoLiberacoes = HistoricoLiberacoes + ' ' + row["HistoricoLiberacoes"].ToString();
                                }
                                else
                                {
                                    HistoricoLiberacoes = row["HistoricoLiberacoes"].ToString();
                                }
                                */
                            }
                        }
                    }

                    //Chama função para atualizar valores dos impostos
                    //Atualiza_Valores_Impostos();


                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable Lista_Item_Pedido()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("user_sp_Webvendas_Listar_Item_Pedido", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 5, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@PedVendaNum", SqlDbType.VarChar, 7, "PedVendaNum"));

                    dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;
                    dbCommand.Parameters["@PedVendaNum"].Value = this.PedVendaNum;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

            }

            return outputTable;
        }

        public DataTable Lista_Item_Pedido_Portal()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_PORTAL_PEDIDOS_ITENS_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 5, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaNum", SqlDbType.VarChar, 7, "vPedVendaNum"));

                    dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;
                    dbCommand.Parameters["@vPedVendaNum"].Value = this.PedVendaNum;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable Lista_Produtos_Ativos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_CRM_CONSULTA_PRODUTOS_ATIVOS", dbConnection);
                    dbCommand.Parameters.Add(new SqlParameter("@ProdCodEstr", SqlDbType.VarChar, 30, "ProdCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@ProdNome", SqlDbType.VarChar, 30, "ProdNome"));

                    dbCommand.Parameters["@ProdCodEstr"].Value = this.ProdCodEstr;
                    dbCommand.Parameters["@ProdNome"].Value = this.ProdNome;

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

            }

            return outputTable;
        }

        public string Gera_Copia()
        {
            string erro = "";

            //Instancia classe pedido
            pedido novoPedido = new pedido();

            //Carrega dados do pedido atual
            novoPedido.carregaDadosPedido(this.EmpCod, this.PedVendaNum);

            //Seta pedido atual para inclusão
            novoPedido.tipoOperacao = "inclusao";
            novoPedido.VendCod = novoPedido.vendedor;
            novoPedido.numeroPedido = "0";
            novoPedido.historico = "";
            novoPedido.historicoAntigo = "";
            novoPedido.observacao = "";
            novoPedido.dataEmissao = DateTime.Now.ToString("yyyy-MM-dd");

            erro = novoPedido.gravaPedidoCRM();

            if (erro == "")
            {
                erro = novoPedido.salvaItens();
            }

            //Seta mensagens de erro da gravação dentro do CRM
            if (erro != "")
            {
                //Seta mensagem de erro.
                erro = "Erro na gravacao do Pedido: " + novoPedido.IDPedido.ToString() + ".";
            }

            if (erro == "")
            {
                //Rotina para enviar pedido para o SAP
                erro = novoPedido.EnviaPedidoSAP();
                this.PedVendaNumCopia = novoPedido.IDPedido.ToString();
            }

            return erro;
        }

        public void Consulta_Copia_Pedido()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_DIREITO_COPIA_PEDIDO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add(new SqlParameter("@USUCOD", SqlDbType.VarChar, 31, "USUCOD"));

                dbCommand.Parameters["@USUCOD"].Value = this.UsuCod;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {
                        if (Convert.ToInt32(row["CONT"]) == 0)
                        {
                            this.UsuCodCopia = "false";
                        }
                        else
                        {
                            this.UsuCodCopia = "true";
                        }
                    }
                }
            }
        }

        public void Atualiza_Valores_Impostos()
        {
            string erro = "";

            if (this.NumeroPedidoSAP != 0)
            {
                //erro = WSComunicaoSAP.Atualiza_Impostos_Pedido(this.NumeroPedidoSAP.ToString());

                erro = Atualiza_Valores_Impostos_API(this.NumeroPedidoSAP.ToString());
            }
            else if (this.NumeroEsbocoSAP != 0)
            {
                //erro = WSComunicaoSAP.Atualiza_Impostos_Rascunho_Pedido(this.NumeroEsbocoSAP.ToString());

                erro = Atualiza_Valores_Impostos_Rascunho_API(this.NumeroEsbocoSAP.ToString());
            }
        }

        #region Impostos Pedido API               

        string urlPadraoAPICRM = System.Configuration.ConfigurationManager.AppSettings["AcessoURLCRMAPI"];

        string retorno = "";

        WSRetornoClass OBJRetorno = new WSRetornoClass();

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

        JsonConversao jsonconv = new JsonConversao();

        public string Atualiza_Valores_Impostos_API(string NumeroPedidoSAP)
        {
            ImpostosPedido objImpostosPedido = new ImpostosPedido(NumeroPedidoSAP);

            string jsonImpostosPedido = jsonconv.ConverteObjectParaJSon<ImpostosPedido>(objImpostosPedido);

            return OBJApi.AtualizaValoresImpostosPedidoAPI(jsonImpostosPedido);

            //HttpClient client = new HttpClient();
            //Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaImpostosPedido");

            //HttpContent c = new StringContent(jsonImpostosPedido, Encoding.UTF8, "application/json");
            //var t = PostURI(u, c);
            //t.Wait();

            //retorno = t.Result.ToString();
            //OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            //return OBJRetorno.MsgRetorno;
        }

        public string Atualiza_Valores_Impostos_Rascunho_API(string NumeroEsbocoSAP)
        {
            ImpostosPedidoRascunho objImpostosPedidoRascunho = new ImpostosPedidoRascunho(NumeroEsbocoSAP);

            string jsonImpostosPedidoRascunho = jsonconv.ConverteObjectParaJSon<ImpostosPedidoRascunho>(objImpostosPedidoRascunho);

            return OBJApi.AtualizaValoresImpostosRascunhoAPI(jsonImpostosPedidoRascunho);

            //HttpClient client = new HttpClient();
            //Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaImpostosPedidoRascunho");

            //HttpContent c = new StringContent(jsonImpostosPedidoRascunho, Encoding.UTF8, "application/json");
            //var t = PostURI(u, c);
            //t.Wait();

            //retorno = t.Result.ToString();
            //OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            //return OBJRetorno.MsgRetorno;
        }

        #endregion

        public string Atualiza_Dados_Pedido_SAP()
        {
            string JSONEnvio = "";
            WSClassePedidoInclusao OBJPedidoInclusao = new WSClassePedidoInclusao();


            OBJPedidoInclusao.IDEmpresa = Convert.ToInt32(this.EmpCod);
            OBJPedidoInclusao.IDPedido = Convert.ToInt32(this.PedVendaNum);
            OBJPedidoInclusao.CodigoUsuarioCRM = HttpContext.Current.Session["usuario"].ToString();

            JSONEnvio = jsonconv.ConverteObjectParaJSon<WSClassePedidoInclusao>(OBJPedidoInclusao);

            if (!string.IsNullOrEmpty(this.NumeroEsbocoSAP.ToString()))
                OBJPedidoInclusao.cod_esboco = this.NumeroEsbocoSAP.ToString();

            return OBJApi.AtualizacaoIntegracaoPedidoAPI(JSONEnvio);

            //if (this.NumeroEsbocoSAP != 0)
            //{

            //    //Atualiza Número de pedido do SAP
            //    if (this.NumeroPedidoSAP == 0)
            //    {
            //        if (this.DescricaoStatus != "Aprovado")
            //        {
            //            //OBJApi.AtualizaNumeroPedidoEsboco();
            //            //WSComunicaoSAP.Atualiza_Numero_Pedido_Esboco(this.NumeroEsbocoSAP.ToString());
            //        }
            //        else
            //        {
            //            //Tenta gravar novamente pedido caso pedido esteja aprovado e não salvou no SAP
            //            if (this.DescricaoStatus == "Aprovado")
            //            {
            //                OBJPedidoInclusao.IDEmpresa = Convert.ToInt32(this.EmpCod);
            //                OBJPedidoInclusao.IDPedido = Convert.ToInt32(this.PedVendaNum);
            //                OBJPedidoInclusao.CodigoUsuarioCRM = HttpContext.Current.Session["usuario"].ToString();

            //                JSONEnvio = jsonconv.ConverteObjectParaJSon<WSClassePedidoInclusao>(OBJPedidoInclusao);

            //                if (!string.IsNullOrEmpty(this.NumeroEsbocoSAP.ToString()))
            //                    OBJPedidoInclusao.cod_esboco = this.NumeroEsbocoSAP.ToString();

            //                OBJApi.AtualizacaoIntegracaoPedidoAPI(JSONEnvio);
            //                //WSComunicaoSAP.Salva_Pedido_SAP_EXTERNO(this.EmpCod, this.PedVendaNum, HttpContext.Current.Session["usuario"].ToString());
            //            }
            //        }
            //    }

            //    if (this.NumeroPedidoSAP != 0)
            //    {
            //        //Atualiza Status de produção
            //        //WSComunicaoSAP.Atualiza_Producao_Pedido(this.NumeroPedidoSAP.ToString());

            //        //Atualiza Notas Fiscais do Pedido
            //        //WSComunicaoSAP.Atualiza_Notas_Fiscais_Pedido(this.NumeroPedidoSAP.ToString());
            //    }

            //}
            //else
            //{
            //    if (this.DescricaoStatus == "Aprovado")
            //    {
            //        //Caso tenha dado algum problema na geração do Esboço tenta gravar novamente.
            //        //WSComunicaoSAP.Salva_Pedido_SAP_EXTERNO(this.EmpCod, this.PedVendaNum, HttpContext.Current.Session["usuario"].ToString());
            //    }
            //}
        }

        public string CarregaPrevisaoEntrega()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PEDIDO_VENDA_PrevisaoEntrega", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoEmpresa", SqlDbType.Int, 0, "CodigoEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroNotaFiscal", SqlDbType.Int, 0, "NumeroNotaFiscal"));

                    dbCommand.Parameters["@CodigoEmpresa"].Value = EmpCod;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = NumeroPedidoSAP;
                    dbCommand.Parameters["@NumeroNotaFiscal"].Value = NumeroNotaFiscal;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["PrevisaoEntrega"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return "";
        }

        public string CarregaCliente()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PEDIDO_VENDA_Cliente", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoEmpresa", SqlDbType.Int, 0, "CodigoEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));

                    dbCommand.Parameters["@CodigoEmpresa"].Value = EmpCod;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = NumeroPedidoSAP;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Cliente"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return "";
        }

        public string RetornaHistoricoRastreio()
        {
            //Limpa para não trazer lixo
            StringBuilder Historico = new StringBuilder();

            DataTable OBJData = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_HISTORICO_RASTREIO_PEDIDOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroNotaFiscal", SqlDbType.Int, 0, "NumeroNotaFiscal"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.EmpCod;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.NumeroPedidoSAP;
                    dbCommand.Parameters["@NumeroNotaFiscal"].Value = this.NumeroNotaFiscal;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        OBJData.Load(dataReader);

                        if (OBJData.Rows.Count > 0)
                        {
                            foreach (DataRow row in OBJData.Rows)
                            {
                                Historico.AppendLine("<div class=\"timeline-entry\">");

                                Historico.AppendLine("  <div class=\"timeline-stat\"> ");

                                Historico.AppendLine("      <div class=\"" + row["TimeLineButonClass"].ToString() + "\"> ");

                                Historico.AppendLine("          <i class=\"" + row["TimeLineIconClass"].ToString() + "\"></i> ");

                                Historico.AppendLine("      </div>");

                                Historico.AppendLine("      <div class=\"timeline-time\">");

                                Historico.AppendLine("          <b>" + row["DataHistorico"].ToString() + "</b>");

                                Historico.AppendLine("      </div> ");

                                Historico.AppendLine("  </div> ");

                                Historico.AppendLine("  <div class=\"timeline-label\"> ");

                                Historico.AppendLine("      <p class=\"mar-no pad-btm\">");

                                Historico.AppendLine("          <span class=\"" + row["TimeLineTituloClass"].ToString() + "\">");

                                Historico.AppendLine("              " + row["DescricaoEvento"].ToString() + " - " + row["DescricaoCategoria"].ToString());

                                Historico.AppendLine("          </span>");

                                Historico.AppendLine("          por ");

                                Historico.AppendLine("          <a href=\"#\" class=\"btn-link btn-md text-semibold\"> ");

                                Historico.AppendLine("              " + row["CodigoUsuario"].ToString());

                                Historico.AppendLine("          </a>");

                                Historico.AppendLine("      </p>");

                                Historico.AppendLine("      <div class=\"well well-xs mar-no\"> ");

                                Historico.AppendLine("          " + row["Historico"].ToString());

                                Historico.AppendLine("      </div>");

                                Historico.AppendLine("  </div>");

                                Historico.AppendLine("</div>");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return Historico.ToString();
        }

        public string AtualizaHistoricoRastreio()
        {
            try
            {
                RastrearPedidoJsonModel objRastrearPedidoJsonModel = new RastrearPedidoJsonModel();

                objRastrearPedidoJsonModel.IDEmpresa = this.EmpCod;
                objRastrearPedidoJsonModel.NumeroPedidoSAP = this.NumeroPedidoSAP.ToString();
                objRastrearPedidoJsonModel.NumeroNotaFiscal = this.NumeroNotaFiscal;

                string Json = jsonconv.ConverteObjectParaJSon(objRastrearPedidoJsonModel);

                FuncoesAPIClass objFuncoesAPIClass = new FuncoesAPIClass();

                WSRetornoJSONClass objWSRetornoJSONClass = objFuncoesAPIClass.AtualizaRastreioPedido(Json);

                return objWSRetornoJSONClass.MsgRetorno;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string RetornaCodigoENomeCliente(int IDCliente)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CRM_CLIENTE_Codigo_E_Nome_Cliente", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Cliente"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return "";
        }

        public string RetornaIDCliente(string CodigoClienteSAP)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CRM_CLIENTE_IDCliente", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.VarChar, 8000, "CodigoClienteSAP"));

                    dbCommand.Parameters["@CodigoClienteSAP"].Value = CodigoClienteSAP;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["IDCliente"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return "";
        }

        public string RetornaIDPedido(string IDCliente, string NOTA_FISCAL)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CRM_PEDIDO_VENDA_IDPedido", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@NOTA_FISCAL", SqlDbType.Int, 0, "NOTA_FISCAL"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@NOTA_FISCAL"].Value = NOTA_FISCAL;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["IDPedido"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return "";
        }

        public string GravaHistoricoPedidosImportacao()
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_HISTORICO_RASTREIO_PEDIDOS_Importacao", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroNotaFiscal", SqlDbType.Int, 0, "NumeroNotaFiscal"));

                    dbCommand.Parameters.Add(new SqlParameter("@IDTipo", SqlDbType.Int, 0, "IDTipo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEvento", SqlDbType.Int, 0, "IDEvento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCategoria", SqlDbType.Int, 0, "IDCategoria"));

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataHistorico", SqlDbType.VarChar, 8000, "DataHistorico"));
                    dbCommand.Parameters.Add(new SqlParameter("@Historico", SqlDbType.VarChar, 8000, "Historico"));
                    dbCommand.Parameters.Add(new SqlParameter("@Tipo", SqlDbType.VarChar, 8000, "Tipo"));

                    dbCommand.Parameters.Add(new SqlParameter("@PrevisaoEntrega", SqlDbType.VarChar, 8000, "PrevisaoEntrega"));

                    dbCommand.Parameters["@IDEmpresa"].Value = IDEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = IDPedido;
                    dbCommand.Parameters["@NumeroNotaFiscal"].Value = NumeroNotaFiscal;

                    dbCommand.Parameters["@IDTipo"].Value = IDTipo;
                    dbCommand.Parameters["@IDEvento"].Value = IDEvento;
                    dbCommand.Parameters["@IDCategoria"].Value = IDCategoria;

                    dbCommand.Parameters["@IDUsuario"].Value = IDUsuario;
                    dbCommand.Parameters["@DataHistorico"].Value = DateTime.Today.ToString("yyyy-MM-dd");
                    dbCommand.Parameters["@Historico"].Value = Historico;
                    dbCommand.Parameters["@Tipo"].Value = "M";

                    dbCommand.Parameters["@PrevisaoEntrega"].Value = PrevisaoEntrega;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return "Erro na gravação do rastreio: " + ex.Message;
            }

            return "";
        }
    }
}