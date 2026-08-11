using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using VendasWeb.WEBServiceCRM;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using VendasWeb.WEBServiceSAP.ClassesWEBService;

namespace VendasWeb
{
    public class producao : GerencialVendas.clsConexao
    {
        #region CAMPOS

        public string Descricao { get; set; }
        public int IDEmpresa { get; set; }
        public int IDGrupo { get; set; }
        public string Empresa { get; set; }
        public string DataInicial { get; set; }
        public string DataFinal { get; set; }
        public string DataEmissao { get; set; }
        public string DataEntrega { get; set; }
        public string Valor { get; set; }
        public int BloqueadoAlteracao { get; set; }
        public int OrdemServico { get; set; }
        public string Emissor { get; set; }
        public int IDEmissor { get; set; }
        public string Operacao { get; set; }
        public int Ativo { get; set; }
        public string Status { get; set; }
        public string StatusOP { get; set; }
        public string Prioridade { get; set; }
        public string Validacao { get; set; }
        public string StatusPrioridade { get; set; }
        public string CodigoUsuario { get; set; }
        public string Produto { get; set; }
        public string ProdutoOrigem { get; set; }
        public string ProdutoRelacionado { get; set; }
        public int IDStatus { get; set; }
        public int IDPrioridade { get; set; }
        public int IDProduto { get; set; }
        public int IDProdutoOrigem { get; set; }
        public int IDProdutoRelacionado { get; set; }
        public bool Relacionado { get; set; }
        public string OrdensProducao { get; set; }
        public decimal QuantidadePedido { get; set; }
        public decimal Planejada { get; set; }
        public bool Estoque { get; set; }
        public int IDTipoOrdemServico { get; set; }
        public string Cliente { get; set; }
        public int NumeroPedidoSAP { get; set; }
        public int NumeroPedidoCRM { get; set; }
        public bool Selecionado { get; set; }
        public int IDITemSAP { get; set; }
        public int Ordem { get; set; }
        public int IDUsuario { get; set; }
        public int IDPedido { get; set; }
        public string Padrao { get; set; }
        public string OK { get; set; }
        public int PadraoPrioridade { get; set; }
        public string Tela { get; set; }
        public int PrazoProducao { get; set; }
        public int PrazoExpedicao { get; set; }
        public string CodigoProduto { get; set; }
        public decimal QuantidadeEstoque { get; set; }
        public int IDEmpenho { get; set; }

        ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();
        JsonConversao jsonconv = new JsonConversao();
        FuncoesAPIClass OBJApi = new FuncoesAPIClass();

        #endregion

        #region MÉTODOS STATUS

        public DataTable ListaStatus()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_STATUS_ORDEM_SERVICO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 8000, "Status"));
                    dbCommand.Parameters.Add(new SqlParameter("@Tela", SqlDbType.VarChar, 8000, "Tela"));

                    dbCommand.Parameters["@Status"].Value = this.Status;
                    dbCommand.Parameters["@Tela"].Value = this.Tela;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public void CarregaDadosPrincipaisStatus()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_DADOS_ORDEM_SERVICO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.VarChar, 8000, "IDStatus"));

                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.IDStatus = Convert.ToInt32(row["IDStatus"]);
                                this.BloqueadoAlteracao = Convert.ToInt32(row["BloqueadoAlteracao"]);
                                this.Ativo = Convert.ToInt32(row["Ativo"]);
                                this.Descricao = row["Descricao"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

        }

        public string GravaDadosPrincipaisStatus()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_DADOS_ORDEM_SERVICO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, ParameterDirection.InputOutput, false, 0, 0, "IDStatus", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "@Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@BloqueadoAlteracao", SqlDbType.Bit, 8000, "BloqueadoAlteracao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Bit, 8000, "Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "@Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao;
                    dbCommand.Parameters["@BloqueadoAlteracao"].Value = this.BloqueadoAlteracao;
                    dbCommand.Parameters["@Ativo"].Value = this.Ativo;
                    dbCommand.Parameters["@Operacao"].Value = this.Operacao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                    this.IDStatus = (int)dbCommand.Parameters["@IDStatus"].Value;
                    this.Operacao = "alteracao";

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao.";
                }
            }

            return erro;
        }

        #endregion

        #region MÉTODOS PRIORIDADES

        public DataTable ListaPrioridades()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_PRIORIDADES_ORDEM_SERVICO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Prioridade", SqlDbType.VarChar, 8000, "Prioridade"));

                    dbCommand.Parameters["@Prioridade"].Value = this.Prioridade;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public void CarregaDadosPrincipaisPrioridades()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_DADOS_PRIORIDADE_ORDEM_SERVICO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDPrioridade", SqlDbType.VarChar, 8000, "IDPrioridade"));

                    dbCommand.Parameters["@IDPrioridade"].Value = this.IDPrioridade;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.IDPrioridade = Convert.ToInt32(row["IDPrioridade"]);
                                this.Valor = row["Valor"].ToString();
                                this.Descricao = row["Descricao"].ToString();
                                this.Ativo = Convert.ToInt32(row["Ativo"]);
                                this.PadraoPrioridade = Convert.ToInt32(row["Padrao"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

        }

        public string GravaDadosPrincipaisPrioridades()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_DADOS_PRIORIDADE_ORDEM_SERVICO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDPrioridade", SqlDbType.Int, 0, ParameterDirection.InputOutput, false, 0, 0, "IDPrioridade", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "@Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Valor", SqlDbType.Int, 0, "Valor"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Bit, 0, "Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Padrao", SqlDbType.Bit, 0, "Padrao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "@Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDPrioridade"].Value = this.IDPrioridade;
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao;
                    dbCommand.Parameters["@Valor"].Value = this.Valor;
                    dbCommand.Parameters["@Ativo"].Value = this.Ativo;
                    dbCommand.Parameters["@Padrao"].Value = this.PadraoPrioridade;
                    dbCommand.Parameters["@Operacao"].Value = this.Operacao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                    this.IDPrioridade = (int)dbCommand.Parameters["@IDPrioridade"].Value;
                    this.Operacao = "alteracao";

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao.";
                }
            }

            return erro;
        }

        #endregion

        #region MÉTODOS PRODUTOS RELACIONAIS

        public DataTable ListaProdutosRelacionais()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_PRODUTOS_RELACIONAIS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Produto", SqlDbType.VarChar, 8000, "Produto"));

                    dbCommand.Parameters["@Produto"].Value = this.Produto;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public DataTable ListaProdutosRelacionaisRelacionamento()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_PRODUTOS_RELACIONAIS_RELACIONAMENTO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDProdutoOrigem", SqlDbType.Int, 0, "IDProdutoOrigem"));
                    dbCommand.Parameters.Add(new SqlParameter("@ProdutoRelacional", SqlDbType.VarChar, 8000, "ProdutoRelacional"));

                    dbCommand.Parameters["@IDProdutoOrigem"].Value = Convert.ToInt32(this.IDProdutoOrigem);
                    dbCommand.Parameters["@ProdutoRelacional"].Value = this.ProdutoRelacionado;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public string AtualizaListaProdutosRelacionais()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_LISTA_PRODUTOS_RELACIONAIS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDProdutoOrigem", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "IDProdutoOrigem", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@IDProdutoRelacionado", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "IDProdutoRelacionado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@Relacionado", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "Relacionado", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDProdutoOrigem"].Value = this.IDProdutoOrigem;
                    dbCommand.Parameters["@IDProdutoRelacionado"].Value = this.IDProdutoRelacionado;
                    dbCommand.Parameters["@Relacionado"].Value = this.Relacionado;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao.";
                }
            }

            return erro;
        }

        public string ValidaDepositoPadraoProdutosRelacionais()
        {
            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_VALIDA_DEPOSITO_PADRAO_PRODUTOS_RELACIONAIS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@Erro", SqlDbType.VarChar, 8000, ParameterDirection.Output, false, 0, 0, "Erro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;

                    dbCommand.ExecuteReader();

                    erro = (string)dbCommand.Parameters["@Erro"].Value;

                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return erro;
        }

        #endregion

        #region MÉTODOS ORDENS DE SERVIÇO

        public DataTable ListaOrdensServico()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_ORDENS_SERVICO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 8000, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 8000, "DataFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@OrdemServico", SqlDbType.Int, 0, "OrdemServico"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    if (this.DataInicial != null)
                    {
                        dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                    }
                    else
                    {
                        this.DataInicial = "";
                    }
                    if (this.DataFinal != null)
                    {
                        dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;
                    }
                    else
                    {
                        this.DataFinal = "";
                    }
                    dbCommand.Parameters["@OrdemServico"].Value = this.OrdemServico;
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public string RecuperaOrdensProducao()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_NUMERO_ORDEM_PRODUCAO_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "IDEmpresa", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "IDOrdemServico", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@OrdensProd", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "OrdensProd", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDOrdemServico"].Value = this.OrdemServico;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    OrdensProducao = (string)dbCommand.Parameters["@OrdensProd"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao.";
                }
            }

            return erro;
        }

        public string GravaOrdensServico()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ORDEM_SERVICO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, ParameterDirection.InputOutput, false, 0, 0, "IDOrdemServico", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmissor", SqlDbType.Int, 0, "IDEmissor"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoOrdemServico", SqlDbType.Int, 0, "IDTipoOrdemServico"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataEmissao", SqlDbType.VarChar, 8000, "DataEmissao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPrioridade", SqlDbType.Int, 0, "IDPrioridade"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDOrdemServico"].Value = this.OrdemServico;
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;
                    dbCommand.Parameters["@IDEmissor"].Value = this.IDEmissor;
                    dbCommand.Parameters["@IDTipoOrdemServico"].Value = this.IDTipoOrdemServico;
                    dbCommand.Parameters["@DataEmissao"].Value = Convert.ToDateTime(this.DataEmissao).ToString("yyyy-MM-dd");
                    dbCommand.Parameters["@IDPrioridade"].Value = this.IDPrioridade;
                    dbCommand.Parameters["@Operacao"].Value = this.Operacao;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                    if (erro == "")
                    {
                        this.OrdemServico = (int)dbCommand.Parameters["@IDOrdemServico"].Value;
                        this.Operacao = "alteracao";
                    }

                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        public string ValidaExistenciaProdutos()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_VALIDA_LISTA_PRODUTOS_PEDIDO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "IDOrdemServico", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@Validacao", SqlDbType.VarChar, 800, ParameterDirection.Output, false, 0, 0, "Validacao", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDOrdemServico"].Value = this.OrdemServico;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    Validacao = (string)dbCommand.Parameters["@Validacao"].Value;

                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return Validacao;
        }

        public string GeracaoOrdensProducao()
        {
            string retorno = "";
            string JSONProducao = "";
            WSGeracaoOrdensProducaoClass OBJProducao = new WSGeracaoOrdensProducaoClass();

            //Carrega Objeto para enviar
            OBJProducao.IDEmpresa = this.IDEmpresa;
            OBJProducao.IDOrdemServico = this.OrdemServico;
            OBJProducao.CodigoUsuarioCRM = this.CodigoUsuario.ToString();

            //Transforma em JSON para enviar para o WEB SERVICE
            JSONProducao = jsonconv.ConverteObjectParaJSon<WSGeracaoOrdensProducaoClass>(OBJProducao);

            retorno = OBJApi.GeracaoOrdensProducaoSAPCRMAPI(JSONProducao);

            if (retorno == "")
            {
                retorno = OBJApi.LiberacaoOrdensProducaoSAPCRMAPI(JSONProducao);
            }

            return retorno;
        }

        public string ExcluiProduto()
        {
            string erro = "";

            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_EXCLUI_PRODUTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, "@IDOrdemServico"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 0, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.VarChar, 0, "IDPedido"));

                    dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDOrdemServico"].Value = this.OrdemServico;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao.";
                }
            }
            return erro;
        }

        public DataTable ListaOrdensServicoProdutos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_ORDENS_SERVICO_PRODUTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@OrdemServico", SqlDbType.Int, 0, "OrdemServico"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@OrdemServico"].Value = this.OrdemServico;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public string AtualizaListaProdutosOrdemServico()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_LISTA_PRODUTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 0, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Planejada", SqlDbType.Decimal, 0, "Planejada"));
                    dbCommand.Parameters.Add(new SqlParameter("@Estoque", SqlDbType.Bit, 0, "Estoque"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, "IDOrdemServico"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.IDPedido;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@Planejada"].Value = this.Planejada;
                    dbCommand.Parameters["@Estoque"].Value = this.Estoque;
                    dbCommand.Parameters["@IDOrdemServico"].Value = this.OrdemServico;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao.";
                }
            }

            return erro;
        }

        public DataTable RecuperaListaOrdensServicoProdutos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RECUPERA_LISTA_ORDENS_SERVICO_PRODUTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 0, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 0, "DataFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoCRM", SqlDbType.Int, 0, "NumeroPedidoCRM"));
                    dbCommand.Parameters.Add(new SqlParameter("@Cliente", SqlDbType.VarChar, 0, "Cliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 0, "Status"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                    dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.NumeroPedidoSAP;
                    dbCommand.Parameters["@NumeroPedidoCRM"].Value = this.NumeroPedidoCRM;
                    dbCommand.Parameters["@Cliente"].Value = this.Cliente;
                    dbCommand.Parameters["@Status"].Value = this.Status;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;

        }

        public string GravaOrdensProducao()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ORDEM_SERVICO_PRODUTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDITemSAP", SqlDbType.Int, 0, "@IDITemSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@QuantidadePedido", SqlDbType.Decimal, 0, "QuantidadePedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@QuantidadePlanejada", SqlDbType.Decimal, 0, "QuantidadePlanejada"));
                    dbCommand.Parameters.Add(new SqlParameter("@Estoque", SqlDbType.Bit, 0, "Estoque"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataEntrega", SqlDbType.VarChar, 8000, "DataEntrega"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, "@IDOrdemServico"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.NumeroPedidoSAP;
                    dbCommand.Parameters["@IDITemSAP"].Value = this.IDITemSAP;
                    dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;
                    dbCommand.Parameters["@QuantidadePedido"].Value = this.QuantidadePedido;
                    dbCommand.Parameters["@QuantidadePlanejada"].Value = this.Planejada;
                    dbCommand.Parameters["@Estoque"].Value = this.Estoque;
                    dbCommand.Parameters["@DataEntrega"].Value = this.DataEntrega;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDOrdemServico"].Value = this.OrdemServico;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    //erro = "Erro na atualizacao.";
                }
            }

            return erro;
        }

        public DataTable RecuperaListaOrdensServicoEditarProdutos()
        {
            {
                DataTable outputTable = new DataTable();

                try
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_RECUPERA_LISTA_ORDENS_SERVICO_EDITAR_PRODUTOS", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                        dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 0, "DataInicial"));
                        dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 0, "DataFinal"));
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoCRM", SqlDbType.Int, 0, "NumeroPedidoCRM"));
                        dbCommand.Parameters.Add(new SqlParameter("@Cliente", SqlDbType.VarChar, 0, "Cliente"));
                        dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 0, "Status"));
                        dbCommand.Parameters.Add(new SqlParameter("@OrdemServico", SqlDbType.Int, 0, "OrdemServico"));

                        dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                        dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                        dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;
                        dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.IDPedido;
                        dbCommand.Parameters["@NumeroPedidoCRM"].Value = this.NumeroPedidoCRM;
                        dbCommand.Parameters["@Cliente"].Value = this.Cliente;
                        dbCommand.Parameters["@Status"].Value = this.Status;
                        dbCommand.Parameters["@OrdemServico"].Value = this.OrdemServico;

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string erro = ex.Message;
                }

                return outputTable;
            }
        }

        public DataTable RecuperaListaOrdensServicoOrdensProducao()
        {
            {
                DataTable outputTable = new DataTable();

                try
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_RECUPERA_LISTA_ORDENS_SERVICO_ORDENS_PRODUCAO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                        dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 8000, "DataInicial"));
                        dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 8000, "DataFinal"));
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoCRM", SqlDbType.Int, 0, "NumeroPedidoCRM"));
                        dbCommand.Parameters.Add(new SqlParameter("@Produto", SqlDbType.VarChar, 8000, "Produto"));
                        dbCommand.Parameters.Add(new SqlParameter("@Ordem", SqlDbType.Int, 0, "Ordem"));
                        dbCommand.Parameters.Add(new SqlParameter("@StatusOP", SqlDbType.VarChar, 800, "StatusOP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 800, "Status"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, "IDOrdemServico"));

                        dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                        dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                        dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;
                        dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.NumeroPedidoSAP;
                        dbCommand.Parameters["@NumeroPedidoCRM"].Value = this.NumeroPedidoCRM;
                        dbCommand.Parameters["@Produto"].Value = this.Produto;
                        dbCommand.Parameters["@Ordem"].Value = this.Ordem;
                        dbCommand.Parameters["@StatusOP"].Value = this.StatusOP;
                        dbCommand.Parameters["@Status"].Value = this.Status;
                        dbCommand.Parameters["@IDOrdemServico"].Value = this.OrdemServico;

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string erro = ex.Message;
                }

                return outputTable;
            }
        }

        public DataTable RetornaListaStatusOrdensServico()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_STATUS_ORDENS_SERVICO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 8000, "IDStatus"));

                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return outputTable;
        }

        public DataTable RetornaListaPrioridadesOrdensServico()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_PRIORIDADES_ORDENS_SERVICO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "@Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPrioridade", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "IDPrioridade", DataRowVersion.Default, null));
                    //dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, "IDOrdemServico"));

                    dbCommand.Parameters["@Operacao"].Value = this.Operacao;
                    //dbCommand.Parameters["@IDOrdemServico"].Value = this.OrdemServico;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (this.Operacao == "inclusao")
                    {
                        this.IDPrioridade = (int)dbCommand.Parameters["@IDPrioridade"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return outputTable;
        }

        public DataTable RetornaListaTiposOrdensServico()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_TIPOS_ORDENS_SERVICO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoOrdemServico", SqlDbType.Int, 0, "IDTipoOrdemServico"));

                    dbCommand.Parameters["@IDTipoOrdemServico"].Value = this.IDTipoOrdemServico;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return outputTable;
        }

        public string VerificaValor()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_VERIFICA_VALOR_PRIORIDADE_ORDEM_SERVICO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDPrioridade", SqlDbType.Int, 0, ParameterDirection.InputOutput, false, 0, 0, "IDPrioridade", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@Valor", SqlDbType.Int, 8000, "Valor"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "@Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDPrioridade"].Value = this.IDPrioridade;
                    dbCommand.Parameters["@Valor"].Value = this.Valor;
                    dbCommand.Parameters["@Operacao"].Value = this.Operacao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        public string VerificaExistenciaOrdensProducao()
        {
            string ExisteOP = "";
            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_VERIFICA_EXISTENCIA_ORDENS_PRODUCAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@ExisteOP", SqlDbType.VarChar, 800, ParameterDirection.Output, false, 0, 0, "ExisteOP", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, "IDOrdemServico"));

                    dbCommand.Parameters["@IDOrdemServico"].Value = this.OrdemServico;

                    dbCommand.ExecuteReader();

                    ExisteOP = (string)dbCommand.Parameters["@ExisteOP"].Value;

                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return ExisteOP;
        }

        public string CancelarOrdemServico()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CANCELA_ORDEM_SERVICO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    //dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 0, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    //dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));                    
                    //dbCommand.Parameters.Add(new SqlParameter("@Planejada", SqlDbType.Decimal, 0, "Planejada"));
                    //dbCommand.Parameters.Add(new SqlParameter("@Estoque", SqlDbType.Bit, 0, "Estoque"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, "IDOrdemServico"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    //dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    //dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.IDPedido;                    
                    //dbCommand.Parameters["@Planejada"].Value = this.Planejada;
                    //dbCommand.Parameters["@Estoque"].Value = this.Estoque;
                    dbCommand.Parameters["@IDOrdemServico"].Value = this.OrdemServico;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        public DataTable VerificaStatusOP()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_VERIFICA_STATUS_OP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, "IDOrdemServico"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));

                    dbCommand.Parameters["@IDOrdemServico"].Value = this.OrdemServico;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return outputTable;
        }

        #endregion

        #region MÉTODOS PRAZOS PRODUÇÃO GRUPOS

        public DataTable RetornaListaPrazosProducao()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_PRAZOS_PRODUCAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoProducao", SqlDbType.Int, 0, "PrazoProducao"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoExpedicao", SqlDbType.Int, 0, "PrazoExpedicao"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDGrupo"].Value = this.IDGrupo;
                    dbCommand.Parameters["@PrazoProducao"].Value = this.PrazoProducao;
                    dbCommand.Parameters["@PrazoExpedicao"].Value = this.PrazoExpedicao;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public DataTable RetornaListaGruposProdutos()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_GRUPOS_PRODUTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return outputTable;
        }

        public string GravaPrazoProducao()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_PRAZO_PRODUCAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoProducao", SqlDbType.Int, 0, "PrazoProducao"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoExpedicao", SqlDbType.Int, 0, "PrazoExpedicao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDGrupo"].Value = this.IDGrupo;
                    dbCommand.Parameters["@PrazoProducao"].Value = this.PrazoProducao;
                    dbCommand.Parameters["@PrazoExpedicao"].Value = this.PrazoExpedicao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        public string ExcluiPrazoProducao()
        {
            string erro = "";

            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_EXCLUI_PRAZO_PRODUCAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDGrupo"].Value = this.IDGrupo;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
                catch (Exception ex)
                {
                    erro = "Erro na exclusão.";
                }
            }
            return erro;
        }

        public string AtualizaPrazoProducao()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_PRAZO_PRODUCAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoProducao", SqlDbType.Int, 0, "PrazoProducao"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoExpedicao", SqlDbType.Int, 0, "PrazoExpedicao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDGrupo"].Value = this.IDGrupo;
                    dbCommand.Parameters["@PrazoProducao"].Value = this.PrazoProducao;
                    dbCommand.Parameters["@PrazoExpedicao"].Value = this.PrazoExpedicao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao.";
                }
            }

            return erro;
        }

        #endregion

        #region MÉTODOS PRAZOS PRODUÇÃO PRODUTOS

        public DataTable RetornaListaPrazoProducaoProdutos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_PRAZOS_PRODUCAO_PRODUTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Produto", SqlDbType.VarChar, 8000, "Produto"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@Produto"].Value = this.Produto;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public string ExcluiPrazoProducaoProdutos()
        {
            string erro = "";

            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_EXCLUI_PRAZO_PRODUCAO_PRODUTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
                catch (Exception ex)
                {
                    erro = "Erro na exclusão.";
                }
            }
            return erro;
        }

        public string AtualizaPrazoProducaoProdutos()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_PRAZO_PRODUCAO_PRODUTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoProducao", SqlDbType.Int, 0, "PrazoProducao"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoExpedicao", SqlDbType.Int, 0, "PrazoExpedicao"));
                    dbCommand.Parameters.Add(new SqlParameter("@QuantidadeEstoque", SqlDbType.Decimal, 0, "QuantidadeEstoque"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;
                    dbCommand.Parameters["@PrazoProducao"].Value = this.PrazoProducao;
                    dbCommand.Parameters["@PrazoExpedicao"].Value = this.PrazoExpedicao;
                    dbCommand.Parameters["@QuantidadeEstoque"].Value = this.QuantidadeEstoque;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao.";
                }
            }

            return erro;
        }

        public string GravaPrazoProducaoProdutos()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_PRAZO_PRODUCAO_PRODUTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoProduto", SqlDbType.VarChar, 8000, "CodigoProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoProducao", SqlDbType.Int, 0, "PrazoProducao"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoExpedicao", SqlDbType.Int, 0, "PrazoExpedicao"));
                    dbCommand.Parameters.Add(new SqlParameter("@QuantidadeEstoque", SqlDbType.Int, 0, "QuantidadeEstoque"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@CodigoProduto"].Value = this.CodigoProduto;
                    dbCommand.Parameters["@PrazoProducao"].Value = this.PrazoProducao;
                    dbCommand.Parameters["@PrazoExpedicao"].Value = this.PrazoExpedicao;
                    dbCommand.Parameters["@QuantidadeEstoque"].Value = this.QuantidadeEstoque;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        public string Exclui_PrazoProducaoProdutos()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_DELETE_PRODUTO_PRAZO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        public string GravaProdutoEstoqueEmpenho()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_PRODUTO_ESTOQUE_EMPENHO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));                    
                    dbCommand.Parameters.Add(new SqlParameter("@IDITem", SqlDbType.Int, 0, "IDITem"));
                    dbCommand.Parameters.Add(new SqlParameter("@Altera", SqlDbType.Bit, 0, "Altera"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = 0;
                    dbCommand.Parameters["@IDPedido"].Value = 0;
                    dbCommand.Parameters["@IDITem"].Value = 0;
                    dbCommand.Parameters["@Altera"].Value = 0;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@vErro"].Value = "";

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    object vErroValue = dbCommand.Parameters["@vErro"].Value;
                    erro = (vErroValue != DBNull.Value) ? (string)vErroValue : "";

                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        #endregion

        #region MÉTODOS EMPENHO ESTOQUE

        public DataTable RetornaListaEmpenhoEstoque()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_EMPENHO_ESTOQUE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Produto", SqlDbType.VarChar, 8000, "Produto"));

                    dbCommand.Parameters["@Produto"].Value = this.Produto;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public DataTable RetornaListaStatusEmpenhoEstoque()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_STATUS_EMPENHO_ESTOQUE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    //dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 8000, "IDStatus"));

                    //dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return outputTable;
        }

        public DataTable RetornaListaEmpenhoEstoquePedidos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_EMPENHO_ESTOQUE_PEDIDOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));
                    dbCommand.Parameters.Add(new SqlParameter("@Cliente", SqlDbType.VarChar, 8000, "Cliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoCRM", SqlDbType.Int, 0, "NumeroPedidoCRM"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 8000, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 8000, "DataFinal"));

                    dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;
                    dbCommand.Parameters["@Cliente"].Value = this.Cliente;
                    dbCommand.Parameters["@NumeroPedidoCRM"].Value = this.NumeroPedidoCRM;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.NumeroPedidoSAP;
                    dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                    dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public string CancelaEmpenhoEstoquePedido()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_EXCLUI_EMPENHO_ESTOQUE_PEDIDO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDProduto", SqlDbType.Int, 0, "IDProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpenho", SqlDbType.Int, 0, "IDEmpenho"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@IDProduto"].Value = this.IDProduto;
                    dbCommand.Parameters["@IDEmpenho"].Value = this.IDEmpenho;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();
                }
                catch //(Exception ex)
                {
                    return "Erro na exclusão.";
                }
            }

            return "";
        }

        #endregion
    }
}