using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Text;
using VendasWeb.GerencialVendas;
using VendasWeb.WEBServiceCRM;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using VendasWeb.WEBServiceSAP.ClassesWEBService;

namespace VendasWeb.classes
{
    public class ClienteClasse : clsConexao
    {
        #region CAMPOS

        #region Campos Principais
        public string CodigoUsuario { get; set; }
        public int IDUsuario { get; set; }
        public int IDUsuarioOperacao { get; set; }
        public int IDCliente { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoFornecedor { get; set; }
        public string CodigoAux { get; set; }
        public string NomeCliente { get; set; }
        public string Cliente { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJCliente { get; set; }
        public string EmailCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public string VendedorCliente { get; set; }
        public string ObservacaoBreveCliente { get; set; }
        public string ObservacaoCompleta { get; set; }
        public DateTime DataCadastroCliente { get; set; }
        public string DataUltimaCompraCliente { get; set; }
        public int IDVendedor { get; set; }
        public int IdSimulacao { get; set; }
        #endregion

        #region Campos Para grava Endereços
        public string DescricaoEndereco { get; set; }
        public string TipoLogradouro { get; set; }
        public string Rua { get; set; }
        public string NumeroRua { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string IDEstado { get; set; }
        public string IDMunicipio { get; set; }
        public string IDPais { get; set; }
        public string IDEndereco { get; set; }
        #endregion

        #region Campos para contato do cliente
        public string TipoContato { get; set; }
        public string NomeContato { get; set; }
        public string TelefoneContato { get; set; }
        public string EmailContato { get; set; }
        public string IDContato { get; set; }
        #endregion

        #region Campos financeiro
        public decimal LimiteCredito { get; set; }
        public decimal LimiteDisponivel { get; set; }
        public decimal PedidosAbertos { get; set; }
        public decimal PedidosFaturados { get; set; }
        public decimal ValorRecebido { get; set; }
        public decimal ValorAReceber { get; set; }
        public int QuantidadeDiasAtraso { get; set; }
        public string PagamentoUnico { get; set; }
        public string AutorizacaoCobranca { get; set; }
        public string IDCondPag { get; set; }
        public int QuantidadeDiasFaturamento { get; set; }

        public decimal ValorRecebidoCuritiba { get; set; }
        public decimal ValorAReceberCuritiba { get; set; }
        public int QuantidadeDiasAtrasoCuritiba { get; set; }
        public int QuantidadeDiasFaturamentoCuritiba { get; set; }

        public decimal ValorRecebidoManaus { get; set; }
        public decimal ValorAReceberManaus { get; set; }
        public int QuantidadeDiasAtrasoManaus { get; set; }
        public int QuantidadeDiasFaturamentoManaus { get; set; }

        public decimal ValorPago { get; set; }
        public decimal ValorAPagar { get; set; }
        public int QuantidadeDiasAtrasoAP { get; set; }
        public int QuantidadeDiasFaturamentoAP { get; set; }

        public decimal ValorPagoDev { get; set; }
        public decimal ValorAPagarDev { get; set; }
        public int QuantidadeDiasAtrasoDev { get; set; }
        public int QuantidadeDiasFaturamentoDev { get; set; }

        public int IDEmpresa { get; set; }
        public string Status { get; set; }
        public int QuantidadeDias { get; set; }
        public string VencimentoInicial { get; set; }
        public string VencimentoFinal { get; set; }
        public string Ordenar { get; set; }
        public string Tipo { get; set; }
        public int NotaFiscal { get; set; }
        public decimal Valor { get; set; }
        public string DataEmissao { get; set; }
        public string DataPagamento { get; set; }
        public string DataPagmento { get; set; }
        public string DataVencimento { get; set; }
        public int PedidoSAP { get; set; }
        public int PedidoCRM { get; set; }

        #endregion

        #region Campos relatório atendimento
        public string IDVendedores { get; set; }
        #endregion

        #region Campos filtro setores
        public int IDTipoHistorico { get; set; }
        public int IDEvento { get; set; }
        public int IDCategoria { get; set; }
        #endregion

        #region Campos Fiscal
        public int IDNatureza { get; set; }
        public string IndicadorIndIEDest { get; set; }
        public string IndicadorOpConsumidor { get; set; }
        public string IndicadorNatureza { get; set; }
        public string EnquadramentoTributario { get; set; }
        public string SimplesNacional { get; set; }
        public string CartaIPI { get; set; }
        public DateTime? DataRecebimentoCartaIPI { get; set; }
        public string ProdutorRural { get; set; }
        public string CPOM { get; set; }
        public int IDCNAE { get; set; }
        public string CNPJ { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Isento { get; set; }
        public string Suframa { get; set; }
        public int IDStatus { get; set; }


        public List<CrmClienteNaturezaDestinacaoClass> ListaCrmClienteNaturezaDestinacaoClass { get; set; }

        ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();

        #endregion

        #region Campos E-Mail
        public int EmailTipoSolicitacao { get; set; }
        public string EmailDescricaoTipoSolicitacao { get; set; }
        public string EmailDescricao { get; set; }
        public Attachment EmailAnexo { get; set; }
        #endregion

        #region Campos Análise Crédito

        public int IDAnalise { get; set; }

        public string DataInicial { get; set; }

        public string DataFinal { get; set; }

        public string Tela { get; set; }

        public string CPFCNPJ { get; set; }

        public string Operacao { get; set; }

        #endregion

        #region Classificação Comercial       

        public int IDSituacao { get; set; }

        public int IDSolicitacao { get; set; }

        public int IDClassificacaoComercial { get; set; }

        public string DataSolicitacao { get; set; }

        public string DataClassificacao { get; set; }

        public int CodigoSAP { get; set; }

        public string CodigoClienteSAP { get; set; }

        public int IDHistorico { get; set; }

        #endregion

        #region CENPROT

        public string PKCS12CERT { get; set; }

        public string PKCS12PASS { get; set; }

        public string PKCS12VALID { get; set; }

        #region CENPROT CARTORIOS

        public string Codigo { get; set; }

        public string Cartorio { get; set; }

        public string TelefoneCartorio { get; set; }

        public string Endereco { get; set; }

        public string Uf { get; set; }

        public string CidadeCodigo { get; set; }

        public string CodigoIBGE { get; set; }

        public string Municipio { get; set; }

        //public string Bairro { get; set; }

        public string AtualizacaoData { get; set; }

        public string Quantidade { get; set; }

        public string PeriodoPesquisa { get; set; }

        #endregion

        #region CENPROT CARTORIOS PROTESTOS

        //public string CPFCNPJ { get; set; }

        public string Data { get; set; }

        public string DataProtesto { get; set; }

        public string DataProtestoString { get; set; }

        //public string DataVencimento { get; set; }

        public string DataVencimentoString { get; set; }

        //public string Valor { get; set; }

        public string ValorString { get; set; }

        public string Chave { get; set; }

        public string NomeApresentante { get; set; }

        public string NomeCedente { get; set; }

        public string TemAnuencia { get; set; }

        public int IDCartorio { get; set; }

        #endregion

        #endregion

        JsonConversao jsonconv = new JsonConversao();
        FuncoesAPIClass OBJApi = new FuncoesAPIClass();

        #endregion

        #region Métodos Principais

        public string gravaDadosPrincipais()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CLIENTE_PRINCIPAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeCliente", SqlDbType.NVarChar, 100, "NomeCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeFantasia", SqlDbType.NText, 0, "NomeFantasia"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.VarChar, 20, "CNPJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.NVarChar, 20, "Telefone"));
                    dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100, "Email"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoCliente", SqlDbType.NVarChar, 30, "TipoCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@ObservacaoSimples", SqlDbType.NText, 0, "ObersvacaoSimples"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDVendedor", SqlDbType.Int, 0, "IDVendedor"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, ParameterDirection.InputOutput, false, 0, 0, "IDCliente", DataRowVersion.Default, null));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@NomeCliente"].Value = this.NomeCliente;
                    dbCommand.Parameters["@NomeFantasia"].Value = this.NomeFantasia;
                    dbCommand.Parameters["@CNPJ"].Value = this.CNPJCliente;
                    dbCommand.Parameters["@Telefone"].Value = this.TelefoneCliente;
                    dbCommand.Parameters["@Email"].Value = this.EmailCliente;
                    dbCommand.Parameters["@TipoCliente"].Value = "C";
                    dbCommand.Parameters["@ObservacaoSimples"].Value = this.ObservacaoBreveCliente;
                    dbCommand.Parameters["@IDVendedor"].Value = this.VendedorCliente;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                    this.IDCliente = (int)dbCommand.Parameters["@IDCliente"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção do cliente";
                }
            }

            return erro;
        }

        public void carregaDadosPrincipais()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_MANUTENCAO_CLIENTE_PRINCIPAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));

                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.IDCliente = Convert.ToInt32(row["IDCliente"]);
                                this.CodigoCliente = row["CodigoClienteSAP"].ToString();
                                this.NomeCliente = row["NomeCliente"].ToString();
                                this.NomeFantasia = row["NomeFantasia"].ToString();
                                this.CNPJCliente = row["CNPJ"].ToString();
                                this.EmailCliente = row["Email"].ToString();
                                this.TelefoneCliente = row["Telefone"].ToString();
                                this.VendedorCliente = row["IDVendedor"].ToString();
                                this.ObservacaoBreveCliente = row["ObservacaoSimples"].ToString();
                                this.ObservacaoCompleta = row["ObservacaoCompleta"].ToString();
                                this.LimiteCredito = Convert.ToDecimal(row["LimiteCredito"]);
                                this.PagamentoUnico = row["PagamentoUnico"].ToString();
                                this.AutorizacaoCobranca = row["AutorizacaoCobranca"].ToString();

                                if (row["IDNatureza"].ToString() != "")
                                {
                                    this.IDNatureza = Convert.ToInt32(row["IDNatureza"].ToString());
                                }

                                this.IndicadorIndIEDest = row["IndicadorIndIEDest"].ToString();
                                this.IndicadorOpConsumidor = row["IndicadorOpConsumidor"].ToString();
                                this.IndicadorNatureza = row["IndicadorNatureza"].ToString();
                                this.EnquadramentoTributario = row["EnquadramentoTributario"].ToString();
                                this.SimplesNacional = row["SimplesNacional"].ToString();
                                this.CartaIPI = row["CartaIPI"].ToString();

                                if (row["DataRecebimentoCartaIPI"].ToString() != "")
                                {
                                    this.DataRecebimentoCartaIPI = Convert.ToDateTime(row["DataRecebimentoCartaIPI"].ToString());
                                }
                                this.ProdutorRural = row["ProdutorRural"].ToString();
                                this.CPOM = row["CPOM"].ToString();


                                this.IDStatus = Convert.ToInt32(row["IDStatus"].ToString());

                                carregaDadosClienteNaturezaDestinacao();

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

        }

        private void carregaDadosClienteNaturezaDestinacao()
        {
            DataTable outputTable = new DataTable();
            this.ListaCrmClienteNaturezaDestinacaoClass = new List<CrmClienteNaturezaDestinacaoClass>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CARREGA_CLIENTE_NATUREZA_DESTINACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));

                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                CrmClienteNaturezaDestinacaoClass ObjCrmClienteNaturezaDestinacaoClassAux = new CrmClienteNaturezaDestinacaoClass();
                                ObjCrmClienteNaturezaDestinacaoClassAux.IDCliente = Convert.ToInt32(row["IDCliente"]);
                                ObjCrmClienteNaturezaDestinacaoClassAux.IDNaturezaDestinacao = Convert.ToInt32(row["IDNaturezaDestinacao"]);

                                this.ListaCrmClienteNaturezaDestinacaoClass.Add(ObjCrmClienteNaturezaDestinacaoClassAux);
                            }
                        }
                    }



                }
            }
            catch (Exception ex)
            {

            }

        }

        public DataTable CarregaEstados()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ESTADOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDPais", SqlDbType.VarChar, 10, "IDPais"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEstado", SqlDbType.VarChar, 10, "IDEstado"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 100, "Nome"));

                    //Fixo ID do Brasil
                    dbCommand.Parameters["@IDPais"].Value = 30;
                    dbCommand.Parameters["@IDEstado"].Value = IDEstado;
                    dbCommand.Parameters["@Nome"].Value = "";

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

        public DataTable CarregaTiposSolicitacao()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_TIPOS_SOLICITACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

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

        public DataTable CarregaCondicoesPagamento()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CONDICAO_PAGAMENTO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDCondPag", SqlDbType.VarChar, 10, "IDCondPag"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 100, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuarioSAP", SqlDbType.VarChar, 100, "CodigoUsuarioSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));

                    //Fixo ID do Brasil
                    dbCommand.Parameters["@IDCondPag"].Value = "";
                    dbCommand.Parameters["@Nome"].Value = "";
                    dbCommand.Parameters["@CodigoUsuarioSAP"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;

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

        public DataTable CarregaMunicipios()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_MUNICIPIOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDPais", SqlDbType.VarChar, 10, "IDPais"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEstado", SqlDbType.VarChar, 10, "IDEstado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMunicipio", SqlDbType.VarChar, 10, "IDMunicipio"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeMunicipio", SqlDbType.VarChar, 100, "NomeMunicipio"));

                    //Fixo ID do Brasil
                    dbCommand.Parameters["@IDPais"].Value = 30;
                    dbCommand.Parameters["@IDEstado"].Value = IDEstado;
                    dbCommand.Parameters["@IDMunicipio"].Value = IDMunicipio;
                    dbCommand.Parameters["@NomeMunicipio"].Value = "";

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

        public DataTable CarregaEnderecosCliente()
        {
            DataTable outputTable = new DataTable();



            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CLIENTE_ENDERECOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@DescricaoEndereco", SqlDbType.VarChar, 50, "DescricaoEndereco"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoLogradouro", SqlDbType.VarChar, 100, "TipoLogradouro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Rua", SqlDbType.VarChar, 100, "Rua"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroRua", SqlDbType.VarChar, 100, "NumeroRua"));
                    dbCommand.Parameters.Add(new SqlParameter("@Complemento", SqlDbType.VarChar, 100, "Complemento"));
                    dbCommand.Parameters.Add(new SqlParameter("@CEP", SqlDbType.VarChar, 20, "CEP"));
                    dbCommand.Parameters.Add(new SqlParameter("@Bairro", SqlDbType.VarChar, 100, "Bairro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Cidade", SqlDbType.VarChar, 100, "Cidade"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEstado", SqlDbType.VarChar, 100, "IDEstado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMunicipio", SqlDbType.VarChar, 100, "IDMunicipio"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPais", SqlDbType.VarChar, 100, "IDPais"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));

                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@DescricaoEndereco"].Value = this.DescricaoEndereco;
                    dbCommand.Parameters["@TipoLogradouro"].Value = this.TipoLogradouro;
                    dbCommand.Parameters["@Rua"].Value = this.Rua;
                    dbCommand.Parameters["@NumeroRua"].Value = this.NumeroRua;
                    dbCommand.Parameters["@Complemento"].Value = this.Complemento;
                    dbCommand.Parameters["@CEP"].Value = this.CEP;
                    dbCommand.Parameters["@Bairro"].Value = this.Bairro;
                    dbCommand.Parameters["@Cidade"].Value = this.Cidade;
                    dbCommand.Parameters["@IDEstado"].Value = this.IDEstado;
                    dbCommand.Parameters["@IDMunicipio"].Value = this.IDMunicipio;
                    dbCommand.Parameters["@IDPais"].Value = "30"; //Fixo BRasil


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

        public string gravaDadosClienteEnderecos()
        {
            string erro = "";

            foreach (string Logradouro in this.DescricaoEndereco.Split(new char[] { '|' }))
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    dbConnection.Open();
                    try
                    {
                        SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CLIENTE_ENDERECOS", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                        dbCommand.Parameters.Add(new SqlParameter("@DescricaoEndereco", SqlDbType.VarChar, 50, "DescricaoEndereco"));
                        dbCommand.Parameters.Add(new SqlParameter("@TipoLogradouro", SqlDbType.VarChar, 100, "TipoLogradouro"));
                        dbCommand.Parameters.Add(new SqlParameter("@Rua", SqlDbType.VarChar, 100, "Rua"));
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroRua", SqlDbType.VarChar, 100, "NumeroRua"));
                        dbCommand.Parameters.Add(new SqlParameter("@Complemento", SqlDbType.VarChar, 100, "Complemento"));
                        dbCommand.Parameters.Add(new SqlParameter("@CEP", SqlDbType.VarChar, 20, "CEP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Bairro", SqlDbType.VarChar, 100, "Bairro"));
                        dbCommand.Parameters.Add(new SqlParameter("@Cidade", SqlDbType.VarChar, 100, "Cidade"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDEstado", SqlDbType.VarChar, 100, "IDEstado"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDMunicipio", SqlDbType.VarChar, 100, "IDMunicipio"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDPais", SqlDbType.VarChar, 100, "IDPais"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                        //dbCommand.Parameters["@DescricaoEndereco"].Value = this.DescricaoEndereco;
                        dbCommand.Parameters["@DescricaoEndereco"].Value = Logradouro;
                        dbCommand.Parameters["@TipoLogradouro"].Value = this.TipoLogradouro;
                        dbCommand.Parameters["@Rua"].Value = this.Rua;
                        dbCommand.Parameters["@NumeroRua"].Value = this.NumeroRua;
                        dbCommand.Parameters["@Complemento"].Value = this.Complemento;
                        dbCommand.Parameters["@CEP"].Value = this.CEP;
                        dbCommand.Parameters["@Bairro"].Value = this.Bairro;
                        dbCommand.Parameters["@Cidade"].Value = this.Cidade;
                        dbCommand.Parameters["@IDEstado"].Value = this.IDEstado;
                        dbCommand.Parameters["@IDMunicipio"].Value = this.IDMunicipio;
                        dbCommand.Parameters["@IDPais"].Value = "30"; //Fixo BRasil
                        dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                    catch (Exception ex)
                    {
                        erro = "Erro na inserção do cliente";
                    }
                }
            }

            return erro;
        }

        public string ExcluiDadosClienteEnderecos()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_CLIENTE_ENDERECOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEndereco", SqlDbType.Int, 0, "IDEndereco"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@IDEndereco"].Value = Convert.ToInt32(this.IDEndereco);

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na exclusão do endereço.";
                }
            }

            return erro;
        }

        public DataTable CarregaContatosCliente()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CLIENTE_CONTATOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoContato", SqlDbType.VarChar, 50, "TipoContato"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeContato", SqlDbType.VarChar, 50, "NomeContato"));
                    dbCommand.Parameters.Add(new SqlParameter("@TelefoneContato", SqlDbType.VarChar, 20, "TelefoneContato"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmailContato", SqlDbType.VarChar, 100, "EmailContato"));

                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@TipoContato"].Value = this.TipoContato;
                    dbCommand.Parameters["@NomeContato"].Value = this.NomeContato;
                    dbCommand.Parameters["@TelefoneContato"].Value = this.TelefoneContato;
                    dbCommand.Parameters["@EmailContato"].Value = this.EmailContato;


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

        public string gravaDadosClienteContatos()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CLIENTE_CONTATO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoContato", SqlDbType.VarChar, 50, "TipoContato"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeContato", SqlDbType.VarChar, 50, "NomeContato"));
                    dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.VarChar, 20, "Telefone"));
                    dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100, "Email"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@TipoContato"].Value = this.TipoContato;
                    dbCommand.Parameters["@NomeContato"].Value = this.NomeContato;
                    dbCommand.Parameters["@Telefone"].Value = this.TelefoneContato;
                    dbCommand.Parameters["@Email"].Value = this.EmailContato;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção do contato do cliente";
                }
            }

            return erro;
        }

        public string ExcluiDadosClienteContatos()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_CLIENTE_CONTATOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDContato", SqlDbType.Int, 0, "IDEndereco"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@IDContato"].Value = Convert.ToInt32(this.IDContato);

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na exclusão do contato do cliente.";
                }
            }

            return erro;
        }

        public DataTable CarregaClienteCondicaoPagamento()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CLIENTE_CONDICAO_PAGAMENTO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCondPag", SqlDbType.VarChar, 100, "IDCondPag"));

                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@IDCondPag"].Value = this.IDCondPag;

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

        public DataTable CarregaClienteCNAE()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CLIENTE_CNAE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCNAE", SqlDbType.Int, 0, "IDCNAE"));

                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@IDCNAE"].Value = this.IDCNAE;


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

        public string gravaDadosClienteFinanceiro()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CLIENTE_CONDICAO_PAGAMENTO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCondicao", SqlDbType.VarChar, 50, "IDCondicao"));
                    dbCommand.Parameters.Add(new SqlParameter("@LimiteCredito", SqlDbType.Decimal, 0, "LimiteCredito"));
                    dbCommand.Parameters.Add(new SqlParameter("@PagamentoUnico", SqlDbType.VarChar, 10, "PagamentoUnico"));
                    dbCommand.Parameters.Add(new SqlParameter("@AutorizacaoCobranca", SqlDbType.VarChar, 10, "AutorizacaoCobranca"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@IDCondicao"].Value = this.IDCondPag;
                    dbCommand.Parameters["@LimiteCredito"].Value = this.LimiteCredito;
                    dbCommand.Parameters["@PagamentoUnico"].Value = this.PagamentoUnico;
                    dbCommand.Parameters["@AutorizacaoCobranca"].Value = this.AutorizacaoCobranca;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção dos dados financeiros do cliente";
                }
            }

            return erro;
        }

        public string gravaDadosClienteFiscal()
        {
            string erro = "";
            string _IDNaturezaDestinacaoAux = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CLIENTE_Fiscal", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDNatureza", SqlDbType.Int, 0, "IDNatureza"));
                    dbCommand.Parameters.Add(new SqlParameter("@IndicadorIndIEDest", SqlDbType.VarChar, 800, "IndicadorIndIEDest"));
                    dbCommand.Parameters.Add(new SqlParameter("@IndicadorOpConsumidor", SqlDbType.VarChar, 800, "IndicadorOpConsumidor"));
                    dbCommand.Parameters.Add(new SqlParameter("@IndicadorNatureza", SqlDbType.VarChar, 800, "IndicadorNatureza"));

                    dbCommand.Parameters.Add(new SqlParameter("@EnquadramentoTributario", SqlDbType.VarChar, 800, "EnquadramentoTributario"));
                    dbCommand.Parameters.Add(new SqlParameter("@SimplesNacional", SqlDbType.VarChar, 800, "SimplesNacional"));
                    dbCommand.Parameters.Add(new SqlParameter("@CartaIPI", SqlDbType.VarChar, 800, "CartaIPI"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataRecebimentoCartaIPI", SqlDbType.DateTime, 0, "DataRecebimentoCartaIPI"));
                    dbCommand.Parameters.Add(new SqlParameter("@ProdutorRural", SqlDbType.VarChar, 800, "ProdutorRural"));
                    dbCommand.Parameters.Add(new SqlParameter("@CPOM", SqlDbType.VarChar, 800, "CPOM"));

                    dbCommand.Parameters.Add(new SqlParameter("@IDCNAE", SqlDbType.Int, 0, "IDCNAE"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.VarChar, 800, "CNPJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@InscricaoEstadual", SqlDbType.VarChar, 800, "InscricaoEstadual"));
                    dbCommand.Parameters.Add(new SqlParameter("@Suframa", SqlDbType.VarChar, 800, "Suframa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDNaturezaDestinacao", SqlDbType.VarChar, 800, "IDNaturezaDestinacao"));



                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@IDNatureza"].Value = this.IDNatureza;
                    dbCommand.Parameters["@IndicadorIndIEDest"].Value = this.IndicadorIndIEDest;
                    dbCommand.Parameters["@IndicadorOpConsumidor"].Value = this.IndicadorOpConsumidor;
                    dbCommand.Parameters["@IndicadorNatureza"].Value = this.IndicadorNatureza;

                    dbCommand.Parameters["@EnquadramentoTributario"].Value = this.EnquadramentoTributario;
                    dbCommand.Parameters["@SimplesNacional"].Value = this.SimplesNacional;
                    dbCommand.Parameters["@CartaIPI"].Value = this.CartaIPI;
                    dbCommand.Parameters["@DataRecebimentoCartaIPI"].Value = this.DataRecebimentoCartaIPI;
                    dbCommand.Parameters["@ProdutorRural"].Value = this.ProdutorRural;
                    dbCommand.Parameters["@CPOM"].Value = this.CPOM;

                    dbCommand.Parameters["@IDCNAE"].Value = this.IDCNAE;
                    dbCommand.Parameters["@CNPJ"].Value = this.CNPJ;
                    dbCommand.Parameters["@InscricaoEstadual"].Value = this.InscricaoEstadual;
                    dbCommand.Parameters["@Suframa"].Value = this.Suframa;


                    if (this.ListaCrmClienteNaturezaDestinacaoClass != null)
                    {

                        foreach (CrmClienteNaturezaDestinacaoClass CND in this.ListaCrmClienteNaturezaDestinacaoClass)
                        {
                            _IDNaturezaDestinacaoAux += CND.IDNaturezaDestinacao.ToString() + ",";
                        }

                    }
                    dbCommand.Parameters["@IDNaturezaDestinacao"].Value = _IDNaturezaDestinacaoAux;


                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção dos dados fiscal do cliente";
                }
            }

            return erro;
        }

        public string gravaDadosNaturezaDestinacao()
        {
            string erro = "";
            string _IDNaturezaDestinacaoAux = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CLIENTE_NATUREZA_DESTINACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDNaturezaDestinacao", SqlDbType.VarChar, 800, "IDNaturezaDestinacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;


                    if (this.ListaCrmClienteNaturezaDestinacaoClass != null)
                    {

                        foreach (CrmClienteNaturezaDestinacaoClass CND in this.ListaCrmClienteNaturezaDestinacaoClass)
                        {
                            _IDNaturezaDestinacaoAux += CND.IDNaturezaDestinacao.ToString() + ",";
                        }

                    }
                    dbCommand.Parameters["@IDNaturezaDestinacao"].Value = _IDNaturezaDestinacaoAux;


                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualização da Natureza de Destinção.";
                }
            }

            return erro;
        }

        public string gravaDadosClienteObservacaoCompleta()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CLIENTE_OBSERVACAO_COMPLETA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));

                    dbCommand.Parameters.Add(new SqlParameter("@ObservacaoCompleta", SqlDbType.VarChar, 80000, "ObservacaoCompleta"));



                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;

                    dbCommand.Parameters["@ObservacaoCompleta"].Value = this.ObservacaoCompleta;


                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção da Observação Completa do cliente. " + ex.Message;
                }
            }

            return erro;
        }

        public string ExcluiDadosClienteCondicoesPagamento()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_CLIENTE_CONDICAO_PAGAMENTO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCondicao", SqlDbType.Int, 0, "IDCondicao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@IDCondicao"].Value = Convert.ToInt32(this.IDCondPag);

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na exclusão do contato do cliente.";
                }
            }

            return erro;
        }

        public string ExcluiDadosClienteCNAE()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_CLIENTE_CNAE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCNAE", SqlDbType.Int, 0, "IDCNAE"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@IDCNAE"].Value = this.IDCNAE;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na exclusão do CNAE do cliente.";
                }
            }

            return erro;
        }

        public DataTable CarregaNaturezasJuridicas()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_NATUREZAS_JURIDICAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDNatureza", SqlDbType.VarChar, 10, "IDNatureza"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 100, "Nome"));

                    //Fixo ID do Brasil
                    dbCommand.Parameters["@IDNatureza"].Value = "";
                    dbCommand.Parameters["@Nome"].Value = "";

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

        public DataTable CarregaCodigosCNAE()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CODIGOS_CNAE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDCNAE", SqlDbType.VarChar, 10, "IDCNAE"));
                    dbCommand.Parameters.Add(new SqlParameter("@DescricaoCNAE", SqlDbType.VarChar, 8000, "DescricaoCNAE"));

                    //Fixo ID do Brasil
                    dbCommand.Parameters["@IDCNAE"].Value = "";
                    dbCommand.Parameters["@DescricaoCNAE"].Value = "";

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

        public DataTable CarregaNaturezaDestinacao()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_NATUREZA_DESTINACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


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

        public DataTable CarregaEnquadramentoTributario()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ENQUADRAMENTO_TRIBUTARIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


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

        public string ValidaCadastroAnaliseCliente(int _IDCliente, string _Operacao)
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_VALIDA_CADASTRO_ANALISE_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 800, "Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDCliente"].Value = _IDCliente;
                    dbCommand.Parameters["@Operacao"].Value = _Operacao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro ValidaAnaliseCliente. " + ex.Message;
                }
            }

            return erro;
        }

        public string AlteraStatusCliente()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ALTERA_STATUS_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na Alteração do Status do cliente.";
                }
            }

            return erro;
        }

        public DataTable CarregaStatusAnaliseCliente()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_STATUS_ANALISE_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 250, "CodigoUsuario"));


                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;


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

        public DataTable CarregaStatusCliente()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_STATUS_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

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

        public string AlteraClienteCodigoSAP()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ALTERA_CLIENTE_CODIGO_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.VarChar, 8000, "CodigoClienteSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@CodigoClienteSAP"].Value = this.CodigoCliente;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro AlteraClienteCodigoSAP." + ex.Message;
                }
            }

            return erro;
        }

        public string AtualizacaoGeral()
        {
            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_API_IMPORTACAO_SAP_CRM", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    dbCommand.ExecuteNonQuery();

                }
            }
            catch
            {
                Retorno = "Erro na Funcao Altera_CondPagCod_Entidade. Contactar o Suporte!";
            }

            return Retorno;
        }

        public DataTable LimiteCreditoTomado()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select CreditLine EntValLimCred, 'Disponível' as limite,  ";
            StringSQL += "(CreditLine-(Balance + OrdersBal)) total  from OCRD  ";
            StringSQL += "where CardCode='" + CodigoCliente.ToString() + "' ";
            StringSQL += "UNION ";
            StringSQL += "select CreditLine EntValLimCred, 'Utilizado' as limite,   ";
            StringSQL += "(CASE WHEN (Balance + OrdersBal)=0 THEN 0.01 ELSE   ";
            StringSQL += "(Balance + OrdersBal) END) total  from OCRD where CardCode='" + CodigoCliente.ToString() + "' ";

            //OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsulta(StringSQL);
            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;

        }

        public string Consulta_CRM_CLIENTE_CNPJ_OU_CPF()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand("CRM_SP_CONSULTA_CRM_CLIENTE_CNPJ_OU_CPF", dbConnection);

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
                        return row["Tipo"].ToString();
                    }
                }
            }

            return "";
        }

        public string Consulta_CRM_CLIENTE_CNPJCPF()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand("CRM_SP_CONSULTA_CRM_CLIENTE_CNPJ_OU_CPF", dbConnection);

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
                        return row["CNPJCPF"].ToString();
                    }
                }
            }

            return "";
        }

        public DataTable RetornaListaCliente()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));

                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable RetornaListaClienteFornecedor()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CLIENTE_FORNECEDOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));

                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable RetornaListaEstados()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_ESTADOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPais", SqlDbType.Int, 0, "IDPais"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEstado", SqlDbType.Int, 0, "IDEstado"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoEstadoSAP", SqlDbType.VarChar, 8000, "CodigoEstadoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 8000, "Nome"));

                    dbCommand.Parameters["@IDPais"].Value = IDPais;
                    dbCommand.Parameters["@IDEstado"].Value = IDEstado;
                    dbCommand.Parameters["@CodigoEstadoSAP"].Value = "";
                    dbCommand.Parameters["@Nome"].Value = "";

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

        public DataTable RetornaListaMunicipios()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_MUNICIPIOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPais", SqlDbType.Int, 0, "IDPais"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEstado", SqlDbType.Int, 0, "IDEstado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMunicipio", SqlDbType.Int, 0, "IDMunicipio"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeMunicipio", SqlDbType.VarChar, 8000, "NomeMunicipio"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoIBGE", SqlDbType.VarChar, 8000, "CodigoIBGE"));
                    dbCommand.Parameters.Add(new SqlParameter("@AbsIdSAP", SqlDbType.Int, 0, "AbsIdSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@MunicipioSAP", SqlDbType.VarChar, 8000, "MunicipioSAP"));

                    dbCommand.Parameters["@IDPais"].Value = IDPais;
                    dbCommand.Parameters["@IDEstado"].Value = IDEstado;
                    dbCommand.Parameters["@IDMunicipio"].Value = IDMunicipio;
                    dbCommand.Parameters["@NomeMunicipio"].Value = "";
                    dbCommand.Parameters["@CodigoIBGE"].Value = "";
                    dbCommand.Parameters["@AbsIdSAP"].Value = 0;
                    dbCommand.Parameters["@MunicipioSAP"].Value = "";

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

        #endregion

        #region Email's

        public string EnviaEmail()
        {
            string erro = "";

            enviarEmail OBJMail = new enviarEmail();
            try
            {
                OBJMail.DataAlteracao = DateTime.Today.ToString("dd/MM/yyyy");
                OBJMail.NomeCliente = this.CodigoCliente != "" ? this.CodigoCliente + " - " + this.NomeCliente : this.IDCliente + " - " + this.NomeCliente;
                OBJMail.TipoSolicitacao = this.EmailDescricaoTipoSolicitacao;
                OBJMail.TituloEmail = this.EmailDescricaoTipoSolicitacao;
                OBJMail.Historico = this.EmailDescricao.Replace("\n", "<br />");
                OBJMail.Anexo = this.EmailAnexo;
                OBJMail.EmailDestinatario = this.RecuperaEmailSolicitacao();

                OBJMail.FormataTextoSolicitacaoCliente();
                OBJMail.enviaEmailFormatadoAnexo();

            }
            catch (Exception ex)
            {
                erro = "Erro ao enviar solicitação de alteração.";
            }

            return erro;
        }

        public string RecuperaEmailSolicitacao()
        {
            string Emails = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_EMAILS_SOLICITACAO_ALTERACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoSolicitacao", SqlDbType.Int, 0, "IDTipoSolicitacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Emails", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "Emails", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDTipoSolicitacao"].Value = this.EmailTipoSolicitacao;
                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    Emails = (string)dbCommand.Parameters["@Emails"].Value;

                }
                catch (Exception ex)
                {
                    Emails = "";
                }
            }

            return Emails;
        }

        public string EnviaEmailVendedor()
        {
            string erro = "";

            enviarEmail OBJMail = new enviarEmail();
            try
            {
                OBJMail.DataAlteracao = DateTime.Today.ToString("dd/MM/yyyy");
                OBJMail.NomeCliente = this.CodigoCliente != "" ? this.CodigoCliente + " - " + this.NomeCliente : this.IDCliente + " - " + this.NomeCliente;
                OBJMail.TipoSolicitacao = this.EmailDescricaoTipoSolicitacao;
                OBJMail.TituloEmail = this.EmailDescricaoTipoSolicitacao;
                OBJMail.Historico = this.EmailDescricao;
                OBJMail.EmailDestinatario = this.RecuperaEmailVendedor();
                //OBJMail.EmailDestinatario = "luiz.carlos@manulifitasa.com.br";

                OBJMail.FormataTextoSolicitacaoCliente();
                OBJMail.enviaEmailFormatadoAnexo();

            }
            catch (Exception ex)
            {
                erro = "Erro ao enviar solicitação de alteração.";
            }

            return erro;
        }

        public string EnviaEmailVendedorHistorico()
        {
            string erro = "";

            enviarEmail OBJMail = new enviarEmail();
            try
            {
                OBJMail.DataAlteracao = DateTime.Today.ToString("dd/MM/yyyy");
                OBJMail.NomeCliente = this.CodigoCliente != "" ? this.CodigoCliente + " - " + this.NomeCliente : this.IDCliente + " - " + this.NomeCliente;
                OBJMail.TipoSolicitacao = this.EmailDescricaoTipoSolicitacao;
                OBJMail.TituloEmail = this.CodigoCliente != "" ? this.CodigoCliente + " - " + this.NomeCliente : this.IDCliente + " - " + this.NomeCliente;
                OBJMail.Historico = this.EmailDescricao;
                OBJMail.EmailDestinatario = this.RecuperaEmailVendedor();
                OBJMail.UsuarioCRM = this.CodigoUsuario;
                //OBJMail.EmailDestinatario = "luiz.carlos@manulifitasa.com.br";

                OBJMail.FormataTextoHistoricoCliente();
                OBJMail.enviaEmailFormatadoAnexo();

            }
            catch (Exception ex)
            {
                erro = "Erro ao enviar solicitação de alteração.";
            }

            return erro;
        }

        public string EnviaEmailSetoresHistorico()
        {
            string erro = "";

            enviarEmail OBJMail = new enviarEmail();
            try
            {
                OBJMail.DataAlteracao = DateTime.Today.ToString("dd/MM/yyyy");
                OBJMail.NomeCliente = this.CodigoCliente != "" ? this.CodigoCliente + " - " + this.NomeCliente : this.IDCliente + " - " + this.NomeCliente;
                OBJMail.TipoSolicitacao = this.EmailDescricaoTipoSolicitacao;
                OBJMail.TituloEmail = this.CodigoCliente != "" ? this.CodigoCliente + " - " + this.NomeCliente : this.IDCliente + " - " + this.NomeCliente;
                OBJMail.Historico = this.EmailDescricao;
                OBJMail.EmailDestinatario = this.RecuperaEmailSolicitacaoEvento();
                OBJMail.UsuarioCRM = this.CodigoUsuario;
                //OBJMail.EmailDestinatario = "luiz.carlos@manulifitasa.com.br";

                OBJMail.FormataTextoHistoricoCliente();
                OBJMail.enviaEmailFormatadoAnexo();

            }
            catch (Exception ex)
            {
                erro = "Erro ao enviar solicitação de alteração.";
            }

            return erro;
        }

        public string RecuperaEmailSolicitacaoEvento()
        {
            string Emails = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_EMAILS_SOLICITACAO_ALTERACAO_EVENTO_CATEGORIA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTipo", SqlDbType.Int, 0, "IDTipo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEvento", SqlDbType.Int, 0, "IDEvento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCategoria", SqlDbType.Int, 0, "IDCategoria"));
                    dbCommand.Parameters.Add(new SqlParameter("@Emails", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "Emails", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDTipo"].Value = this.IDTipoHistorico;
                    dbCommand.Parameters["@IDEvento"].Value = this.IDEvento;
                    dbCommand.Parameters["@IDCategoria"].Value = this.IDCategoria;
                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    Emails = (string)dbCommand.Parameters["@Emails"].Value;

                }
                catch (Exception ex)
                {
                    Emails = "";
                }
            }

            return Emails;
        }

        public string RecuperaEmailVendedor()
        {
            string Emails = "";

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_EMAIL_VENDEDOR_ID", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@IDVendedor", SqlDbType.Int, 0, "IDVendedor"));

                    dbCommand.Parameters["@IDVendedor"].Value = this.VendedorCliente;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                Emails = row["Email"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Emails;
        }

        #endregion

        #region CONTA CORRENTE 

        public DataTable RecuperaContaCorrenteClienteSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            /*Recupera Pedidos Pendentes de autorização*/
            StringSQL += "select OCRD.CardCode, OCRD.CardName, OrdersBal, ";
            StringSQL += "OCRD.Fax as CNPJ, CRD1.City as Cidade, ";
            StringSQL += "isnull(OCRD.CreditLine, 0) as LimiteCredito,  ";
            StringSQL += "OrdersBal, isnull(SUM(OINV.DocTotal),0) PedidosFaturados,  ";
            StringSQL += "CONVERT(VARCHAR(10),isnull(OSLP.SlpCode,''))+' - '+ OSLP.SlpName Vendedor  ";

            StringSQL += "FROM OCRD ";
            StringSQL += "LEFT JOIN CRD1 ON CRD1.CardCode=OCRD.CardCode ";
            StringSQL += "and CRD1.[Address] = 'ENTREGA'  ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode=OCRD.SlpCode ";
            StringSQL += "LEFT JOIN OINV ON OINV.CardCode=OCRD.CardCode ";
            StringSQL += "and OINV.CANCELED = 'N'  ";
            StringSQL += "and OINV.DocStatus = 'C' ";

            StringSQL += "Where  ";
            StringSQL += "OCRD.CardCode like '%" + this.CodigoCliente + "%'  ";
            StringSQL += "and isnull(OCRD.CardName, '') like '%" + this.RazaoSocial + "%'  ";
            StringSQL += "and isnull(OCRD.AliasName, '') like '%" + this.NomeFantasia + "%'  ";
            StringSQL += "and isnull(OCRD.Fax, '') like '%" + this.CNPJCliente + "%' ";

            StringSQL += "group by OCRD.CardCode, OCRD.CardName, OCRD.Fax, ";
            StringSQL += "CRD1.City, OCRD.CreditLine, OCRD.OrdersBal, ";
            StringSQL += "OSLP.SlpCode, OSLP.SlpName ";

            StringSQL += "Order by OCRD.CardCode ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;
        }

        #region Métodos do Financeiro - Conta Corrente - Detalhe

        #region RECUPERA DADOS GERAIS

        public void RecuperaCadastroClienteSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            //Recupera data de cadastro do cliente
            StringSQL += "select CreateDate from OCRD ";
            StringSQL += "where CardCode= '" + this.CodigoCliente + "' ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.DataCadastroCliente = Convert.ToDateTime(row["CreateDate"]);
                }
            }

        }

        public void RecuperaPedidosAbertosClienteSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            //Recupera pedidos abertos do cliente
            StringSQL += "select OrdersBal from OCRD ";
            StringSQL += "where CardCode= '" + this.CodigoCliente + "' ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.PedidosAbertos = Convert.ToDecimal(row["OrdersBal"]);
                }
            }

        }

        public void RecuperaUltimaCompraClienteSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select isnull(isnull(convert(varchar(10),(select MAX(OINV.DocDate) ";
            StringSQL += "from OINV where OINV.CANCELED = 'N' ";
            StringSQL += "and OINV.DocStatus = 'C' and OINV.CardCode = '" + this.CodigoCliente + "'),103),  ";
            StringSQL += "convert(varchar(10), (select MAX(OPCH.DocDate) from OPCH ";
            StringSQL += "where OPCH.CANCELED = 'N' and OPCH.DocStatus = 'C' ";
            StringSQL += "and OPCH.CardCode = '" + this.CodigoCliente + "'),103)),'') UltimaDataFaturamento ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    if (row["UltimaDataFaturamento"] != null && row["UltimaDataFaturamento"].ToString() != "")
                    {
                        this.DataUltimaCompraCliente = Convert.ToString(row["UltimaDataFaturamento"]);
                    }
                    else
                    {
                        this.DataUltimaCompraCliente = "";
                    }
                }
            }

        }

        public void RecuperaPedidosFaturadosClienteSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select SUM(OINV.DocTotal) PedidosFaturados from OINV ";
            StringSQL += "where OINV.CANCELED = 'N' ";
            StringSQL += "and OINV.DocStatus = 'C' ";
            StringSQL += "and OINV.CardCode = '" + this.CodigoCliente + "' ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    if (row["PedidosFaturados"] != null && row["PedidosFaturados"].ToString() != "")
                    {
                        this.PedidosFaturados = Convert.ToDecimal(row["PedidosFaturados"]);
                    }
                }
            }

        }

        #endregion

        #region RECUPERA RECEBER 

        public void RecuperaCodigoClienteSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select (T2.CardCode) CodigoCliente from CRD7 ";
            StringSQL += "INNER JOIN CRD7 T2 ON T2.TaxId0 = CRD7.TaxId0 ";
            StringSQL += "INNER JOIn OCRD T3 ON T3.CardCode = T2.CardCode and T3.CardType = 'C' ";
            StringSQL += " where CRD7.CardCode = '" + this.CodigoCliente + "' and ";
            StringSQL += " CRD7.[Address] = '' and T2.TaxId0 <> '' ";
            StringSQL += "group by T2.CardCode ";


            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.CodigoCliente = Convert.ToString(row["CodigoCliente"]);
                }
            }

        }

        public void RecuperaValorAReceberSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select sum(Debit) as ValorAReceber ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN OINV ON JDT1.BaseRef = OINV.DocEntry and TransType = '13' ";
            StringSQL += "INNER JOIN OINV T1 ON T1.DocEntry = OINV.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OINV.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OINV.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN INV6 ON OINV.DocEntry=INV6.DocEntry and INV6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=OINV.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where (isnull(JDT1.MthDate,'')='' OR ((isnull(JDT1.BalDueDeb,0)>0 OR isnull(JDT1.BalDueCred,0)>0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoCliente + "' and OINV.CANCELED='N' ";
            StringSQL += "group by T0.CardCode UNION ";
            StringSQL += "select sum(Debit) as ValorAReceber from JDT1 ";
            StringSQL += "INNER JOIN ODPI ON JDT1.BaseRef=ODPI.DocEntry and TransType= '203' ";
            StringSQL += "INNER JOIN ODPI T1 ON T1.DocEntry= ODPI.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON ODPI.BPLId= OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON ODPI.CardCode= OCRD.CardCode and JDT1.ShortName= OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode= OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode= OCRD.CardCode and CRD1.AdresType= 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode= OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode= OCRD.CardCode and CRD7.[Address]= '' ";
            StringSQL += "LEFT JOIN DPI6 ON ODPI.DocEntry= DPI6.DocEntry and DPI6.InstLmntID= JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod= ODPI.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where (isnull(JDT1.MthDate,'')<>'' AND((isnull(JDT1.BalDueDeb,0)=0 AND isnull(JDT1.BalDueCred,0)=0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoCliente + "' and ODPI.CANCELED='N' ";
            StringSQL += "group by T0.CardCode ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.ValorAReceber = Convert.ToDecimal(row["ValorAReceber"]);
                }
            }

        }

        public void RecuperaValorRecebidoSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select sum(Debit) as ValorRecebido ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN OINV ON JDT1.BaseRef = OINV.DocEntry and TransType = '13' ";
            StringSQL += "INNER JOIN OINV T1 ON T1.DocEntry = OINV.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OINV.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OINV.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN INV6 ON OINV.DocEntry=INV6.DocEntry and INV6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=OINV.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where (isnull(JDT1.MthDate,'')<>'' AND((isnull(JDT1.BalDueDeb,0)=0 AND isnull(JDT1.BalDueCred,0)=0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoCliente + "' and OINV.CANCELED='N' ";
            StringSQL += "group by T0.CardCode UNION ";
            StringSQL += "select sum(Debit) as ValorRecebido from JDT1 ";
            StringSQL += "INNER JOIN ODPI ON JDT1.BaseRef=ODPI.DocEntry and TransType= '203' ";
            StringSQL += "INNER JOIN ODPI T1 ON T1.DocEntry= ODPI.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON ODPI.BPLId= OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON ODPI.CardCode= OCRD.CardCode and JDT1.ShortName= OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode= OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode= OCRD.CardCode and CRD1.AdresType= 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode= OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode= OCRD.CardCode and CRD7.[Address]= '' ";
            StringSQL += "LEFT JOIN DPI6 ON ODPI.DocEntry= DPI6.DocEntry and DPI6.InstLmntID= JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod= ODPI.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where (isnull(JDT1.MthDate,'')<>'' AND((isnull(JDT1.BalDueDeb,0)=0 AND isnull(JDT1.BalDueCred,0)=0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoCliente + "' and ODPI.CANCELED='N' ";
            StringSQL += "group by T0.CardCode ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.ValorRecebido = Convert.ToDecimal(row["ValorRecebido"]);
                }
            }

        }

        public void RecuperaQuantidadeDiasAtrasoSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select Ceiling(sum(Convert(numeric(18,6),(CASE WHEN DiasAtraso<0 THEN 0 ELSE DiasAtraso END)))/count(*)) ";
            StringSQL += "DiasAtrazo ";
            StringSQL += "from( ";
            StringSQL += "select DATEDIFF(day, (CASE WHEN DATEPART(weekday, JDT1.DueDate) = 7 THEN DATEADD(day, 2, JDT1.DueDate) ";
            StringSQL += "WHEN DATEPART(weekday, JDT1.DueDate) = 1 THEN DATEADD(day, 1, JDT1.DueDate) ";
            StringSQL += "ELSE JDT1.DueDate END), JDT1.MthDate) DiasAtraso ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN OINV ON JDT1.BaseRef = OINV.DocEntry and TransType = '13' ";
            StringSQL += "INNER JOIN OINV T1 ON T1.DocEntry = OINV.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OINV.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OINV.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN INV6 ON OINV.DocEntry=INV6.DocEntry and INV6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=OINV.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where OINV.CardCode= '" + this.CodigoCliente + "'  and OINV.Canceled= 'N' ";
            StringSQL += ") TabAux ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    if (row["DiasAtrazo"].ToString() != "")
                    {
                        this.QuantidadeDiasAtraso = Convert.ToInt32(row["DiasAtrazo"]);
                    }
                    else
                    {
                        this.QuantidadeDiasAtraso = 0;
                    }
                }
            }

        }

        public void RecuperaQuantidadeDiasFaturamentoSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select Ceiling(sum(Convert(numeric(18,6),(CASE WHEN DiasFaturamento<0 THEN 0 ELSE DiasFaturamento END)))/count(*)) ";
            StringSQL += "DiasFaturamento ";
            StringSQL += "from( ";
            StringSQL += "select DATEDIFF(day, (CASE WHEN DATEPART(weekday, JDT1.DueDate) = 7 THEN DATEADD(day, 2, JDT1.DueDate) ";
            StringSQL += "WHEN DATEPART(weekday, JDT1.DueDate) = 1 THEN DATEADD(day, 1, JDT1.DueDate) ";
            StringSQL += "ELSE JDT1.DueDate END), JDT1.RefDate) DiasFaturamento ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN OINV ON JDT1.BaseRef = OINV.DocEntry and TransType = '13' ";
            StringSQL += "INNER JOIN OINV T1 ON T1.DocEntry = OINV.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OINV.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OINV.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN INV6 ON OINV.DocEntry=INV6.DocEntry and INV6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=OINV.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where OINV.CardCode= '" + this.CodigoCliente + "'  and OINV.Canceled= 'N' ";
            StringSQL += ") TabAux ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    if (row["DiasFaturamento"].ToString() != "")
                    {
                        this.QuantidadeDiasFaturamento = Convert.ToInt32(row["DiasFaturamento"]);
                    }
                    else
                    {
                        this.QuantidadeDiasFaturamento = 0;
                    }
                }
            }

        }

        #endregion

        #region RECUPERA RECEBER CURITIBA

        public void RecuperaValorAReceberCuritibaSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select sum(Debit) as ValorRecebido from JDT1 ";
            StringSQL += "INNER JOIN OINV ON JDT1.BaseRef = OINV.DocEntry and TransType = '13' ";
            StringSQL += "INNER JOIN OINV T1 ON T1.DocEntry = OINV.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OINV.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OINV.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN INV6 ON OINV.DocEntry=INV6.DocEntry and INV6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=OINV.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where (isnull(JDT1.MthDate,'')='' OR((isnull(JDT1.BalDueDeb,0)>0 OR isnull(JDT1.BalDueCred,0)>0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoCliente + "' and JDT1.BPLId= 1 and OINV.CANCELED='N' ";
            StringSQL += "group by T0.CardCode UNION ";
            StringSQL += "select sum(Debit) as ValorRecebido from JDT1 ";
            StringSQL += "INNER JOIN ODPI ON JDT1.BaseRef=ODPI.DocEntry and TransType= '203' ";
            StringSQL += "INNER JOIN ODPI T1 ON T1.DocEntry= ODPI.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON ODPI.BPLId= OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON ODPI.CardCode= OCRD.CardCode and JDT1.ShortName= OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode= OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode= OCRD.CardCode and CRD1.AdresType= 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode= OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode= OCRD.CardCode and CRD7.[Address]= '' ";
            StringSQL += "LEFT JOIN DPI6 ON ODPI.DocEntry= DPI6.DocEntry and DPI6.InstLmntID= JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod= ODPI.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where (isnull(JDT1.MthDate,'')='' OR((isnull(JDT1.BalDueDeb,0)>0 OR isnull(JDT1.BalDueCred,0)>0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoCliente + "' and JDT1.BPLId= 1 and ODPI.CANCELED='N' ";
            StringSQL += "group by T0.CardCode ";


            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.ValorAReceberCuritiba = Convert.ToDecimal(row["ValorRecebido"]);
                }
            }

        }

        public void RecuperaValorRecebidoCuritibaSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select sum(Debit) as ValorRecebido from JDT1 ";
            StringSQL += "INNER JOIN OINV ON JDT1.BaseRef = OINV.DocEntry and TransType = '13' ";
            StringSQL += "INNER JOIN OINV T1 ON T1.DocEntry = OINV.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OINV.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OINV.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN INV6 ON OINV.DocEntry=INV6.DocEntry and INV6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=OINV.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where (isnull(JDT1.MthDate,'')<>'' AND((isnull(JDT1.BalDueDeb,0)=0 AND isnull(JDT1.BalDueCred,0)=0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoCliente + "' and JDT1.BPLId= 1 and OINV.CANCELED='N' ";
            StringSQL += "group by T0.CardCode UNION ";
            StringSQL += "select sum(Debit) as ValorRecebido from JDT1 ";
            StringSQL += "INNER JOIN ODPI ON JDT1.BaseRef=ODPI.DocEntry and TransType= '203' ";
            StringSQL += "INNER JOIN ODPI T1 ON T1.DocEntry= ODPI.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON ODPI.BPLId= OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON ODPI.CardCode= OCRD.CardCode and JDT1.ShortName= OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode= OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode= OCRD.CardCode and CRD1.AdresType= 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode= OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode= OCRD.CardCode and CRD7.[Address]= '' ";
            StringSQL += "LEFT JOIN DPI6 ON ODPI.DocEntry= DPI6.DocEntry and DPI6.InstLmntID= JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod= ODPI.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where (isnull(JDT1.MthDate,'')<>'' AND((isnull(JDT1.BalDueDeb,0)=0 AND isnull(JDT1.BalDueCred,0)=0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoCliente + "' and JDT1.BPLId= 1 and ODPI.CANCELED='N' ";
            StringSQL += "group by T0.CardCode ";


            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.ValorRecebidoCuritiba = Convert.ToDecimal(row["ValorRecebido"]);
                }
            }

        }

        public void RecuperaQuantidadeDiasAtrasoCuritibaSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select Ceiling(sum(Convert(numeric(18,6),(CASE WHEN DiasAtraso<0 THEN 0 ELSE DiasAtraso END)))/count(*)) ";
            StringSQL += "DiasAtrazo ";
            StringSQL += "from( ";
            StringSQL += "select DATEDIFF(day, (CASE WHEN DATEPART(weekday, JDT1.DueDate) = 7 THEN DATEADD(day, 2, JDT1.DueDate) ";
            StringSQL += "WHEN DATEPART(weekday, JDT1.DueDate) = 1 THEN DATEADD(day, 1, JDT1.DueDate) ";
            StringSQL += "ELSE JDT1.DueDate END), JDT1.MthDate) DiasAtraso ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN OINV ON JDT1.BaseRef = OINV.DocEntry and TransType = '13' ";
            StringSQL += "INNER JOIN OINV T1 ON T1.DocEntry = OINV.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OINV.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OINV.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN INV6 ON OINV.DocEntry=INV6.DocEntry and INV6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=OINV.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where OINV.CardCode= '" + this.CodigoCliente + "' and OINV.Canceled= 'N' and OINV.BPLId= 1 ";
            StringSQL += ") TabAux ";


            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    if (row["DiasAtrazo"].ToString() != "")
                    {
                        this.QuantidadeDiasAtrasoCuritiba = Convert.ToInt32(row["DiasAtrazo"]);
                    }
                    else
                    {
                        this.QuantidadeDiasAtrasoCuritiba = 0;
                    }
                }
            }

        }

        public void RecuperaQuantidadeDiasFaturamentoCuritibaSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select Ceiling(sum(Convert(numeric(18,6),(CASE WHEN DiasFaturamento<0 THEN 0 ELSE DiasFaturamento END)))/count(*)) ";
            StringSQL += "DiasFaturamento ";
            StringSQL += "from( ";
            StringSQL += "select DATEDIFF(day, (CASE WHEN DATEPART(weekday, JDT1.DueDate) = 7 THEN DATEADD(day, 2, JDT1.DueDate) ";
            StringSQL += "WHEN DATEPART(weekday, JDT1.DueDate) = 1 THEN DATEADD(day, 1, JDT1.DueDate) ";
            StringSQL += "ELSE JDT1.DueDate END), JDT1.RefDate) DiasFaturamento ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN OINV ON JDT1.BaseRef = OINV.DocEntry and TransType = '13' ";
            StringSQL += "INNER JOIN OINV T1 ON T1.DocEntry = OINV.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OINV.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OINV.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN INV6 ON OINV.DocEntry=INV6.DocEntry and INV6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=OINV.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where OINV.CardCode= '" + this.CodigoCliente + "' and OINV.Canceled= 'N' and OINV.BPLId= 1 ";
            StringSQL += ") TabAux ";


            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    if (row["DiasFaturamento"].ToString() != "")
                    {
                        this.QuantidadeDiasFaturamentoCuritiba = Convert.ToInt32(row["DiasFaturamento"]);
                    }
                    else
                    {
                        this.QuantidadeDiasFaturamentoCuritiba = 0;
                    }
                }
            }

        }

        #endregion

        #region RECUPERA RECEBER MANAUS

        public void RecuperaValorAReceberManausSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += " select sum(Debit) as ValorRecebido from JDT1 ";
            StringSQL += "INNER JOIN OINV ON JDT1.BaseRef = OINV.DocEntry and TransType = '13' ";
            StringSQL += "INNER JOIN OINV T1 ON T1.DocEntry = OINV.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OINV.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OINV.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN INV6 ON OINV.DocEntry=INV6.DocEntry and INV6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=OINV.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where (isnull(JDT1.MthDate,'')='' OR((isnull(JDT1.BalDueDeb,0)>0 OR isnull(JDT1.BalDueCred,0)>0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoCliente + "' and JDT1.BPLId= 3 and OINV.CANCELED='N' ";
            StringSQL += "group by T0.CardCode UNION ";
            StringSQL += "select sum(Debit) as ValorRecebido from JDT1 ";
            StringSQL += "INNER JOIN ODPI ON JDT1.BaseRef=ODPI.DocEntry and TransType= '203' ";
            StringSQL += "INNER JOIN ODPI T1 ON T1.DocEntry= ODPI.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON ODPI.BPLId= OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON ODPI.CardCode= OCRD.CardCode and JDT1.ShortName= OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode= OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode= OCRD.CardCode and CRD1.AdresType= 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode= OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode= OCRD.CardCode and CRD7.[Address]= '' ";
            StringSQL += "LEFT JOIN DPI6 ON ODPI.DocEntry= DPI6.DocEntry and DPI6.InstLmntID= JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod= ODPI.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where (isnull(JDT1.MthDate,'')='' OR((isnull(JDT1.BalDueDeb,0)>0 OR isnull(JDT1.BalDueCred,0)>0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoCliente + "' and JDT1.BPLId= 3 and ODPI.CANCELED='N'";
            StringSQL += "group by T0.CardCode ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.ValorAReceberManaus = Convert.ToDecimal(row["ValorRecebido"]);
                }
            }

        }

        public void RecuperaValorRecebidoManausSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select sum(Debit) as ValorRecebido from JDT1 ";
            StringSQL += "INNER JOIN OINV ON JDT1.BaseRef = OINV.DocEntry and TransType = '13' ";
            StringSQL += "INNER JOIN OINV T1 ON T1.DocEntry = OINV.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OINV.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OINV.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN INV6 ON OINV.DocEntry=INV6.DocEntry and INV6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=OINV.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where (isnull(JDT1.MthDate,'')<>'' AND((isnull(JDT1.BalDueDeb,0)=0 AND isnull(JDT1.BalDueCred,0)=0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoCliente + "' and JDT1.BPLId= 3 and OINV.CANCELED='N' ";
            StringSQL += "group by T0.CardCode UNION ";
            StringSQL += "select sum(Debit) as ValorRecebido from JDT1 ";
            StringSQL += "INNER JOIN ODPI ON JDT1.BaseRef=ODPI.DocEntry and TransType= '203' ";
            StringSQL += "INNER JOIN ODPI T1 ON T1.DocEntry= ODPI.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON ODPI.BPLId= OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON ODPI.CardCode= OCRD.CardCode and JDT1.ShortName= OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode= OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode= OCRD.CardCode and CRD1.AdresType= 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode= OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode= OCRD.CardCode and CRD7.[Address]= '' ";
            StringSQL += "LEFT JOIN DPI6 ON ODPI.DocEntry= DPI6.DocEntry and DPI6.InstLmntID= JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod= ODPI.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where (isnull(JDT1.MthDate,'')<>'' AND((isnull(JDT1.BalDueDeb,0)=0 AND isnull(JDT1.BalDueCred,0)=0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoCliente + "' and JDT1.BPLId= 3 and ODPI.CANCELED='N' ";
            StringSQL += "group by T0.CardCode ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.ValorRecebidoManaus = Convert.ToDecimal(row["ValorRecebido"]);
                }
            }

        }

        public void RecuperaQuantidadeDiasAtrasoManausSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select Ceiling(sum(Convert(numeric(18,6),(CASE WHEN DiasAtraso<0 THEN 0 ELSE DiasAtraso END)))/count(*)) ";
            StringSQL += "DiasAtrazo ";
            StringSQL += "from( ";
            StringSQL += "select DATEDIFF(day, (CASE WHEN DATEPART(weekday, JDT1.DueDate) = 7 THEN DATEADD(day, 2, JDT1.DueDate) ";
            StringSQL += "WHEN DATEPART(weekday, JDT1.DueDate) = 1 THEN DATEADD(day, 1, JDT1.DueDate) ";
            StringSQL += "ELSE JDT1.DueDate END), JDT1.MthDate) DiasAtraso ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN OINV ON JDT1.BaseRef = OINV.DocEntry and TransType = '13' ";
            StringSQL += "INNER JOIN OINV T1 ON T1.DocEntry = OINV.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OINV.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OINV.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN INV6 ON OINV.DocEntry=INV6.DocEntry and INV6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=OINV.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where OINV.CardCode= '" + this.CodigoCliente + "' and OINV.Canceled= 'N' and OINV.BPLId= 3 ";
            StringSQL += ") TabAux ";


            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    if (row["DiasAtrazo"].ToString() != "")
                    {
                        this.QuantidadeDiasAtrasoManaus = Convert.ToInt32(row["DiasAtrazo"]);
                    }
                    else
                    {
                        this.QuantidadeDiasAtrasoManaus = 0;
                    }
                }
            }

        }

        public void RecuperaQuantidadeDiasFaturamentoManausSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            //StringSQL += "select Ceiling(sum(Convert(numeric(18,6),(CASE WHEN DiasFaturamento<0 THEN 0 ELSE DiasFaturamento END)))/count(*)) ";
            StringSQL += "select isnull(Ceiling(sum(Convert(numeric(18,6),(CASE WHEN DiasFaturamento<0 THEN 0 ELSE DiasFaturamento END)))/count(*)),0) ";
            StringSQL += "DiasFaturamento ";
            StringSQL += "from( ";
            StringSQL += "select DATEDIFF(day, (CASE WHEN DATEPART(weekday, JDT1.DueDate) = 7 THEN DATEADD(day, 2, JDT1.DueDate) ";
            StringSQL += "WHEN DATEPART(weekday, JDT1.DueDate) = 1 THEN DATEADD(day, 1, JDT1.DueDate) ";
            StringSQL += "ELSE JDT1.DueDate END), JDT1.RefDate) DiasFaturamento ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN OINV ON JDT1.BaseRef = OINV.DocEntry and TransType = '13' ";
            StringSQL += "INNER JOIN OINV T1 ON T1.DocEntry = OINV.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OINV.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OINV.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN INV6 ON OINV.DocEntry=INV6.DocEntry and INV6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=OINV.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where OINV.CardCode= '" + this.CodigoCliente + "' and OINV.Canceled= 'N' and OINV.BPLId= 3 ";
            StringSQL += ") TabAux ";


            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    if (row["DiasFaturamento"].ToString() != "")
                    {
                        this.QuantidadeDiasFaturamentoManaus = Convert.ToInt32(row["DiasFaturamento"]);
                    }
                    else
                    {
                        this.QuantidadeDiasFaturamentoManaus = 0;
                    }
                }
            }

        }

        #endregion

        #region RECUPERA PAGAR

        public void RecuperaCodigoFornecedorSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select (T2.CardCode) CodigoFornecedor from CRD7 ";
            StringSQL += "INNER JOIN CRD7 T2 ON T2.TaxId0 = CRD7.TaxId0 ";
            StringSQL += "INNER JOIn OCRD T3 ON T3.CardCode = T2.CardCode and T3.CardType = 'S' ";
            StringSQL += "where CRD7.CardCode = '" + this.CodigoCliente + "' and ";
            StringSQL += "CRD7.[Address] = '' and T2.TaxId0 <> '' ";
            StringSQL += "group by T2.CardCode ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.CodigoFornecedor = Convert.ToString(row["CodigoFornecedor"]);
                }
            }

        }

        public void RecuperaValorAPagarSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select sum(Credit) as TotalPagamentoAberto from ( ";
            StringSQL += "select JDT1.Credit from JDT1 ";
            StringSQL += "INNER JOIN OPCH ON JDT1.BaseRef = OPCH.DocEntry and TransType = '18' ";
            StringSQL += "INNER JOIN OPCH T0 ON T0.DocEntry = OPCH.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OPCH.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OPCH.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T1 ON OCRD.CardCode = T1.CardCode ";
            StringSQL += "LEFT JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN PCH6 ON OPCH.DocEntry=PCH6.DocEntry and PCH6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN PCH6 T2 ON OPCH.DocEntry=T2.DocEntry and T2.InstLmntID=JDT1.SourceLine ";
            StringSQL += "where(isnull(JDT1.MthDate,'')='' OR((isnull(JDT1.BalDueDeb,0)<>0 OR isnull(JDT1.BalDueCred,0)<>0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoFornecedor + "' and OPCH.CANCELED='N' UNION ";
            StringSQL += "select JDT1.Credit from JDT1 ";
            StringSQL += "INNER JOIN OVPM ON JDT1.BaseRef= OVPM.DocNum ";
            StringSQL += "INNER JOIN OVPM T0 ON JDT1.BaseRef= T0.DocNum ";
            StringSQL += "INNER JOIN OBPL ON OVPM.BPLId= OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OVPM.CardCode= OCRD.CardCode and JDT1.ShortName= OCRD.CardCode ";
            StringSQL += "LEFT JOIN OCRD T1 ON OCRD.CardCode= T1.CardCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode= OCRD.CardCode and CRD7.[Address]= '' ";
            StringSQL += "where (isnull(JDT1.MthDate,'')='' OR((isnull(JDT1.BalDueDeb,0)<>0 OR isnull(JDT1.BalDueCred,0)<>0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoFornecedor + "' and OVPM.Canceled='N' ";
            StringSQL += ") JDT1_AUX ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0 && this.CodigoFornecedor != null)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    if (row["TotalPagamentoAberto"] != null && row["TotalPagamentoAberto"].ToString() != "")
                    {
                        this.ValorAPagar = Convert.ToDecimal(row["TotalPagamentoAberto"]);
                    }
                }
            }
            else if (this.CodigoFornecedor == null || this.CodigoFornecedor == "0")
            {
                this.ValorAPagar = 0;
            }

        }

        public void RecuperaValorPagoSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select sum(Credit) as TotalPagamento from ( ";
            StringSQL += "select JDT1.Credit from JDT1 ";
            StringSQL += "INNER JOIN OPCH ON JDT1.BaseRef = OPCH.DocEntry and TransType = '18' ";
            StringSQL += "INNER JOIN OPCH T0 ON T0.DocEntry = OPCH.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OPCH.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OPCH.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T1 ON OCRD.CardCode = T1.CardCode ";
            StringSQL += "LEFT JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN PCH6 ON OPCH.DocEntry=PCH6.DocEntry and PCH6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN PCH6 T2 ON OPCH.DocEntry=T2.DocEntry and T2.InstLmntID=JDT1.SourceLine ";
            StringSQL += "where(isnull(JDT1.MthDate,'')<>'' AND((isnull(JDT1.BalDueDeb,0)=0 AND isnull(JDT1.BalDueCred,0)=0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoFornecedor + "' and OPCH.CANCELED='N' UNION ";
            StringSQL += "select JDT1.Credit from JDT1 ";
            StringSQL += "INNER JOIN OVPM ON JDT1.BaseRef= OVPM.DocNum ";
            StringSQL += "INNER JOIN OVPM T0 ON JDT1.BaseRef= T0.DocNum ";
            StringSQL += "INNER JOIN OBPL ON OVPM.BPLId= OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OVPM.CardCode= OCRD.CardCode and JDT1.ShortName= OCRD.CardCode ";
            StringSQL += "LEFT JOIN OCRD T1 ON OCRD.CardCode= T1.CardCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode= OCRD.CardCode and CRD7.[Address]= '' ";
            StringSQL += "where (isnull(JDT1.MthDate,'')<>'' AND((isnull(JDT1.BalDueDeb,0)=0 AND isnull(JDT1.BalDueCred,0)=0))) ";
            StringSQL += "and T0.CardCode='" + this.CodigoFornecedor + "' and OVPM.Canceled='N' ";
            StringSQL += ") JDT1_AUX ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0 && this.CodigoFornecedor != null)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    if (row["TotalPagamento"] != null && row["TotalPagamento"].ToString() != "")
                    {
                        this.ValorPago = Convert.ToDecimal(row["TotalPagamento"]);
                    }
                }
            }
            else if (this.CodigoFornecedor == null || this.CodigoFornecedor == "0")
            {
                this.ValorPago = 0;
            }

        }

        public void RecuperaQuantidadeDiasAtrasoAPSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select ";
            StringSQL += "Ceiling(sum(Convert(numeric(18, 6), (CASE WHEN DiasAtraso < 0 THEN 0 ELSE DiasAtraso END))) / count(*)) ";
            StringSQL += "DiasAtraso ";
            StringSQL += "from( ";
            StringSQL += "select DATEDIFF(day, (CASE WHEN DATEPART(weekday, JDT1.DueDate) = 7 THEN DATEADD(day, 2, JDT1.DueDate) ";
            StringSQL += "WHEN DATEPART(weekday, JDT1.DueDate) = 1 THEN DATEADD(day, 1, JDT1.DueDate) ";
            StringSQL += "ELSE JDT1.DueDate END), JDT1.MthDate) DiasAtraso ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN OPCH ON JDT1.BaseRef = OPCH.DocEntry and TransType = '18' ";
            StringSQL += "INNER JOIN OPCH T1 ON T1.DocEntry = OPCH.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OPCH.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OPCH.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "LEFT JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN PCH6 ON OPCH.DocEntry=PCH6.DocEntry and PCH6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "where OPCH.CardCode='" + this.CodigoFornecedor + "' ";
            StringSQL += "and OPCH.CANCELED='N' ) TabAux ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0 && this.CodigoFornecedor != null)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    if (row["DiasAtraso"] != null && row["DiasAtraso"].ToString() != "")
                    {
                        this.QuantidadeDiasAtrasoAP = Convert.ToInt32(row["DiasAtraso"]);
                    }
                }
            }
            else if (this.CodigoFornecedor == null || this.CodigoFornecedor == "0")
            {
                this.QuantidadeDiasAtrasoAP = 0;
            }

        }

        public void RecuperaQuantidadeDiasFaturamentoAPSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select ";
            //StringSQL += "Ceiling(sum(Convert(numeric(18, 6), (CASE WHEN DiasAtraso < 0 THEN 0 ELSE DiasAtraso END))) / count(*)) ";
            StringSQL += "isnull(Ceiling(sum(Convert(numeric(18, 6), (CASE WHEN DiasFaturamento < 0 THEN 0 ELSE DiasFaturamento END))) / count(*)),0) ";
            StringSQL += "DiasFaturamento ";
            StringSQL += "from( ";
            StringSQL += "select DATEDIFF(day, (CASE WHEN DATEPART(weekday, JDT1.DueDate) = 7 THEN DATEADD(day, 2, JDT1.DueDate) ";
            StringSQL += "WHEN DATEPART(weekday, JDT1.DueDate) = 1 THEN DATEADD(day, 1, JDT1.DueDate) ";
            StringSQL += "ELSE JDT1.DueDate END), JDT1.RefDate) DiasFaturamento ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN OPCH ON JDT1.BaseRef = OPCH.DocEntry and TransType = '18' ";
            StringSQL += "INNER JOIN OPCH T1 ON T1.DocEntry = OPCH.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OPCH.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OPCH.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "LEFT JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN PCH6 ON OPCH.DocEntry=PCH6.DocEntry and PCH6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "where OPCH.CardCode='" + this.CodigoFornecedor + "' ";
            StringSQL += "and OPCH.CANCELED='N' ) TabAux ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0 && this.CodigoFornecedor != null)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    if (row["DiasFaturamento"] != null && row["DiasFaturamento"].ToString() != "")
                    {
                        this.QuantidadeDiasFaturamentoAP = Convert.ToInt32(row["DiasFaturamento"]);
                    }
                }
            }
            else if (this.CodigoFornecedor == null || this.CodigoFornecedor == "0")
            {
                this.QuantidadeDiasFaturamentoAP = 0;
            }

        }

        #endregion

        #region DEVOLUÇÕES

        public void RecuperaCodigoClienteDevSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select (T2.CardCode) CodigoCliente from CRD7 ";
            StringSQL += "INNER JOIN CRD7 T2 ON T2.TaxId0 = CRD7.TaxId0 ";
            StringSQL += "INNER JOIn OCRD T3 ON T3.CardCode = T2.CardCode and T3.CardType = 'C' ";
            StringSQL += "where CRD7.CardCode = '" + this.CodigoFornecedor + "' and ";
            StringSQL += "CRD7.[Address] = '' and T2.TaxId0 <> '' ";
            StringSQL += "group by T2.CardCode ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.CodigoCliente = Convert.ToString(row["CodigoCliente"]);
                }
            }

        }

        public void RecuperaValorAPagarDevSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select T0.CardCode, sum(Credit) as ValorDevolucoesAberto ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN ORIN ON JDT1.BaseRef = ORIN.DocEntry and TransType = '14' ";
            StringSQL += "INNER JOIN ORIN T1 ON T1.DocEntry = ORIN.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON ORIN.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON ORIN.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN RIN6 ON ORIN.DocEntry=RIN6.DocEntry and RIN6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=ORIN.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where isnull(JDT1.MthDate,'')='' ";
            StringSQL += "and T0.CardCode='" + this.CodigoCliente + "' and ORIN.CANCELED='N' ";
            StringSQL += "group by T0.CardCode ";


            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.ValorAPagarDev = Convert.ToDecimal(row["ValorDevolucoesAberto"]);
                }
            }

        }

        public void RecuperaValorPagoDevSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select T0.CardCode, sum(credit) as ValorDevolucaoPaga ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN ORIN ON JDT1.BaseRef = ORIN.DocEntry and TransType = '14' ";
            StringSQL += "INNER JOIN ORIN T1 ON T1.DocEntry = ORIN.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON ORIN.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON ORIN.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN RIN6 ON ORIN.DocEntry=RIN6.DocEntry and RIN6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=ORIN.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";
            StringSQL += "where isnull(JDT1.MthDate,'')<>'' ";
            StringSQL += "and T0.CardCode='" + this.CodigoCliente + "' and ORIN.CANCELED='N' ";
            StringSQL += "group by T0.CardCode ";


            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.ValorPagoDev = Convert.ToDecimal(row["ValorDevolucaoPaga"]);
                }
            }
            else
            {
                this.ValorPagoDev = 0;
            }

        }

        #endregion

        #endregion

        #region Métodos do Financeiro - Conta Corrente

        public DataTable ListaEmpresasUsuario()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_EMPRESAS_USUARIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.VarChar, 8000, "IDUsuario"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable ListaDiasAtraso()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_DIAS_ATRASO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return outputTable;
        }

        #endregion

        #region Métodos do Financeiro - Conta Corrente - Contas Receber

        public DataTable RecuperaCCCReceber()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL_AUX_1 = "";
            string StringSQL_AUX_2 = "";
            string StringSQL_AUX = "";
            string StringSQL = "";

            #region if´s do StringSQL

            if (this.IDEmpresa != 0)
            {
                StringSQL_AUX_1 += " AND JDT1.BPLId='" + this.IDEmpresa + "' ";
                StringSQL_AUX_2 += " AND JDT1.BPLId='" + this.IDEmpresa + "' ";
            }

            if (this.Status != "0" && this.Status != null)
            {
                if (this.Status == "1")
                {
                    StringSQL_AUX_1 += "AND (isnull(JDT1.MthDate, '') <> '' AND ";
                    StringSQL_AUX_1 += " ((isnull(JDT1.BalDueDeb, 0) = 0 AND ";
                    StringSQL_AUX_1 += " isnull(JDT1.BalDueCred, 0) = 0))) ";

                    StringSQL_AUX_2 += "AND (isnull(JDT1.MthDate, '') <> '' AND ";
                    StringSQL_AUX_2 += " ((isnull(JDT1.BalDueDeb, 0) = 0 AND ";
                    StringSQL_AUX_2 += " isnull(JDT1.BalDueCred, 0) = 0))) ";

                }
                else
                {
                    StringSQL_AUX_1 += "AND (isnull(JDT1.MthDate, '') = '' OR ";
                    StringSQL_AUX_1 += " ((isnull(JDT1.BalDueDeb, 0) > 0 OR ";
                    StringSQL_AUX_1 += " isnull(JDT1.BalDueCred, 0) > 0))) ";

                    StringSQL_AUX_2 += "AND (isnull(JDT1.MthDate, '') = '' OR ";
                    StringSQL_AUX_2 += " ((isnull(JDT1.BalDueDeb, 0) > 0 OR ";
                    StringSQL_AUX_2 += " isnull(JDT1.BalDueCred, 0) > 0))) ";
                }
            }

            if (this.QuantidadeDias > 0)
            {
                StringSQL_AUX_1 += "AND DATEDIFF(day,JDT1.DueDate, isnull(JDT1.MthDate, GETDATE()))> " + this.QuantidadeDias.ToString() + " ";
                StringSQL_AUX_2 += "AND DATEDIFF(day,JDT1.DueDate, isnull(JDT1.MthDate, GETDATE()))> " + this.QuantidadeDias.ToString() + " ";
            }

            if (this.VencimentoInicial != null && this.VencimentoInicial != "")
            {
                StringSQL_AUX_1 += "AND JDT1.DueDate>='" + this.VencimentoInicial.ToString() + "' ";
                StringSQL_AUX_2 += "AND JDT1.DueDate>='" + this.VencimentoInicial.ToString() + "' ";
            }

            if (this.VencimentoFinal != null && this.VencimentoFinal != "")
            {
                StringSQL_AUX_1 += "AND JDT1.DueDate<='" + this.VencimentoFinal.ToString() + "' ";
                StringSQL_AUX_2 += "AND JDT1.DueDate<='" + this.VencimentoFinal.ToString() + "' ";
            }

            if (this.NotaFiscal > 0)
            {
                StringSQL_AUX_1 += "AND OINV.Serial = '" + this.NotaFiscal.ToString() + "' ";
                StringSQL_AUX_2 += "AND ODPI.Serial = '" + this.NotaFiscal.ToString() + "' ";
            }

            if (this.Valor > 0)
            {
                StringSQL_AUX_1 += "AND JDT1.Debit = '" + this.Valor.ToString() + "' ";
                StringSQL_AUX_2 += "AND JDT1.Debit = '" + this.Valor.ToString() + "' ";
            }

            if (this.Ordenar != "0")
            {
                if (this.Ordenar == "1")
                {
                    StringSQL_AUX += "Order By TabAux.DataEmissao ";
                }
                else if (this.Ordenar == "2")
                {
                    StringSQL_AUX += "Order By TabAux.DataVencimento ";
                }
                else if (this.Ordenar == "3")
                {
                    StringSQL_AUX += "Order By TabAux.DataPagamento ";
                }
            }

            if (this.Tipo == "2")
            {
                StringSQL_AUX += " Desc ";
            }

            #endregion

            #region StringSQL

            StringSQL += "select Empresa, DocEntry, ObjType, NotaFiscal, DataEmissao, isnull(convert(varchar(10), DataPagamento,103),'') as DataPagamento, DataVencimento, ";
            StringSQL += "(CASE WHEN DiasAtraso < 0 THEN 0 ELSE DiasAtraso END) as DiasAtraso, ValorReceber from ( ";
            StringSQL += "select OINV.BPLId Empresa, OINV.DocEntry, OINV.ObjType, OINV.Serial NotaFiscal, ";
            StringSQL += "OINV.DocDate DataEmissao, JDT1.MthDate DataPagamento, JDT1.DueDate DataVencimento, ";
            StringSQL += "DATEDIFF(day, (CASE WHEN DATEPART(weekday, JDT1.DueDate) = 7 THEN DATEADD(day, 2, JDT1.DueDate) ";
            StringSQL += "WHEN DATEPART(weekday, JDT1.DueDate) = 1 THEN DATEADD(day, 1, JDT1.DueDate) ";
            StringSQL += "ELSE JDT1.DueDate END), JDT1.MthDate) DiasAtraso, ";
            StringSQL += "Debit ValorReceber ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN OINV ON JDT1.BaseRef = OINV.DocEntry and TransType = '13' ";
            StringSQL += "INNER JOIN OINV T1 ON T1.DocEntry = OINV.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OINV.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OINV.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN INV6 ON OINV.DocEntry=INV6.DocEntry and INV6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=OINV.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";

            StringSQL += "where T0.CardCode= '" + this.CodigoCliente + "' and OINV.CANCELED='N' ";

            //StringSQL += "where T0.CardCode= 'CLI0000020' ";

            StringSQL += StringSQL_AUX_1;

            StringSQL += " UNION ";
            StringSQL += "select ODPI.BPLId, ODPI.DocEntry, ODPI.ObjType, ODPI.Serial, ODPI.DocDate, JDT1.MthDate DataPagamento, JDT1.DueDate, ";
            StringSQL += "DATEDIFF(day,(CASE WHEN DATEPART(weekday, JDT1.DueDate)=7 THEN DATEADD(day,2, JDT1.DueDate) ";
            StringSQL += "WHEN DATEPART(weekday, JDT1.DueDate)=1 THEN DATEADD(day,1, JDT1.DueDate) ";
            StringSQL += "ELSE JDT1.DueDate END),JDT1.MthDate) DiasAtraso, Debit ValorReceber ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN ODPI ON JDT1.BaseRef=ODPI.DocEntry and TransType='203' ";
            StringSQL += "INNER JOIN ODPI T1 ON T1.DocEntry=ODPI.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON ODPI.BPLId= OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON ODPI.CardCode= OCRD.CardCode and JDT1.ShortName= OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode= OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode= OCRD.CardCode and CRD1.AdresType= 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode= OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode= OCRD.CardCode and CRD7.[Address]= '' ";
            StringSQL += "LEFT JOIN DPI6 ON ODPI.DocEntry= DPI6.DocEntry and DPI6.InstLmntID= JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod= ODPI.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";

            StringSQL += "where T0.CardCode= '" + this.CodigoCliente + "' and ODPI.CANCELED='N' ";

            //StringSQL += "where T0.CardCode= 'CLI0009117' ";

            StringSQL += StringSQL_AUX_2;

            StringSQL += ") TabAux ";

            StringSQL += StringSQL_AUX;

            #endregion

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;
        }

        #endregion

        #region Métodos do Financeiro - Conta Corrente - Contas Pagar

        public DataTable RecuperaCCCPagar()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL_AUX_1 = "";
            string StringSQL_AUX_2 = "";
            string StringSQL_AUX = "";
            string StringSQL = "";

            #region if´s do StringSQL

            if (this.IDEmpresa != 0)
            {
                StringSQL_AUX_1 += " AND JDT1.BPLId='" + this.IDEmpresa + "' ";
                StringSQL_AUX_2 += " AND JDT1.BPLId='" + this.IDEmpresa + "' ";
            }

            if (this.Status != "0")
            {
                if (this.Status == "1")
                {
                    StringSQL_AUX_1 += "AND (isnull(JDT1.MthDate, '') <> '' AND ";
                    StringSQL_AUX_1 += " ((isnull(JDT1.BalDueDeb, 0) = 0 AND ";
                    StringSQL_AUX_1 += " isnull(JDT1.BalDueCred, 0) = 0))) ";

                    StringSQL_AUX_2 += "AND (isnull(JDT1.MthDate, '') <> '' AND ";
                    StringSQL_AUX_2 += " ((isnull(JDT1.BalDueDeb, 0) = 0 AND ";
                    StringSQL_AUX_2 += " isnull(JDT1.BalDueCred, 0) = 0))) ";
                }
                else
                {
                    StringSQL_AUX_1 += "AND (isnull(JDT1.MthDate, '') = '' OR ";
                    StringSQL_AUX_1 += " ((isnull(JDT1.BalDueDeb, 0) > 0 OR ";
                    StringSQL_AUX_1 += " isnull(JDT1.BalDueCred, 0) > 0))) ";

                    StringSQL_AUX_2 += "AND (isnull(JDT1.MthDate, '') = '' OR ";
                    StringSQL_AUX_2 += " ((isnull(JDT1.BalDueDeb, 0) > 0 OR ";
                    StringSQL_AUX_2 += " isnull(JDT1.BalDueCred, 0) > 0))) ";
                }
            }

            if (this.QuantidadeDias > 0)
            {
                StringSQL_AUX_1 += "AND DATEDIFF(day,JDT1.DueDate, isnull(JDT1.MthDate, GETDATE()))> 0 ";
                StringSQL_AUX_2 += "AND DATEDIFF(day,JDT1.DueDate, isnull(JDT1.MthDate, GETDATE()))> 0 ";
            }

            if (this.VencimentoInicial != null && this.VencimentoInicial != "")
            {
                StringSQL_AUX_1 += "AND JDT1.DueDate>='" + this.VencimentoInicial.ToString() + "' ";
                StringSQL_AUX_2 += "AND JDT1.DueDate>='" + this.VencimentoInicial.ToString() + "' ";
            }

            if (this.VencimentoFinal != null && this.VencimentoFinal != "")
            {
                StringSQL_AUX_1 += "AND JDT1.DueDate<='" + this.VencimentoFinal.ToString() + "' ";
                StringSQL_AUX_2 += "AND JDT1.DueDate<='" + this.VencimentoFinal.ToString() + "' ";
            }

            if (this.NotaFiscal > 0)
            {
                StringSQL_AUX_1 += "AND OPCH.Serial = '" + this.NotaFiscal.ToString() + "' ";
                StringSQL_AUX_2 += "AND OVPM.Serial = '" + this.NotaFiscal.ToString() + "' ";
            }

            if (this.Valor > 0)
            {
                StringSQL_AUX_1 += "AND JDT1.Credit = '" + this.Valor.ToString() + "' ";
                StringSQL_AUX_2 += "AND JDT1.Credit = '" + this.Valor.ToString() + "' ";
            }

            if (this.Ordenar != "0")
            {
                if (this.Ordenar == "1")
                {
                    StringSQL_AUX += "Order By TabAux.DataEmissao ";
                }
                else if (this.Ordenar == "2")
                {
                    StringSQL_AUX += "Order By TabAux.DataVencimento ";
                }
                else if (this.Ordenar == "3")
                {
                    StringSQL_AUX += "Order By TabAux.DataPagamento ";
                }
            }

            if (this.Tipo == "2")
            {
                StringSQL_AUX += " Desc ";
            }

            #endregion

            #region StringSQL

            StringSQL += "select Empresa, DocEntry, DataVencimento, ObjType, NotaFiscal, DataEmissao, DataPagamento, ValorPagar from( ";
            StringSQL += "select OPCH.BPLId Empresa, OPCH.DocEntry, JDT1.DueDate DataVencimento, ";
            StringSQL += "OPCH.ObjType, OPCH.Serial NotaFiscal, ";
            StringSQL += "OPCH.DocDate DataEmissao, isnull(convert(varchar(10), JDT1.MthDate,103),'') as DataPagamento, JDT1.Credit ValorPagar ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN OPCH ON JDT1.BaseRef = OPCH.DocEntry and TransType = '18' ";
            StringSQL += "INNER JOIN OPCH T0 ON T0.DocEntry = OPCH.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OPCH.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OPCH.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T1 ON OCRD.CardCode = T1.CardCode ";
            StringSQL += "LEFT JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN PCH6 ON OPCH.DocEntry=PCH6.DocEntry and PCH6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN PCH6 T2 ON OPCH.DocEntry=T2.DocEntry and T2.InstLmntID=JDT1.SourceLine ";

            StringSQL += "where T0.CardCode= '" + this.CodigoFornecedor + "' and OPCH.CANCELED='N' ";

            //StringSQL += "where T0.CardCode= 'FOR0000884' ";

            StringSQL += StringSQL_AUX_1;

            StringSQL += " UNION ";
            StringSQL += "select OVPM.BPLId Empresa, OVPM.DocEntry,  JDT1.DueDate DataVencimento, ";
            StringSQL += "OVPM.ObjType, OVPM.Serial NotaFiscal,";
            StringSQL += "OVPM.DocDate DataEmissao, isnull(convert(varchar(10), JDT1.MthDate,103),'') as DataPagamento, JDT1.Credit ValorPagar ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN OVPM ON JDT1.BaseRef = OVPM.DocNum ";
            StringSQL += "INNER JOIN OVPM T0 ON JDT1.BaseRef = T0.DocNum ";
            StringSQL += "INNER JOIN OBPL ON OVPM.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON OVPM.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "LEFT JOIN OCRD T1 ON OCRD.CardCode = T1.CardCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";

            StringSQL += "where T0.CardCode= '" + this.CodigoFornecedor + "' and OVPM.Canceled='N' ";

            //StringSQL += "where T0.CardCode= 'FOR0000884' ";

            StringSQL += StringSQL_AUX_2;

            StringSQL += ") TabAux ";

            StringSQL += StringSQL_AUX;

            #endregion

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;
        }

        #endregion

        #region Métodos do Financeiro - Conta Corrente - Devoluções

        public DataTable RecuperaCCDevolucoes()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL_AUX_1 = "";
            string StringSQL_AUX = "";
            string StringSQL = "";

            #region if´s do StringSQL

            if (this.IDEmpresa != 0)
            {
                StringSQL_AUX_1 += " AND JDT1.BPLId='" + this.IDEmpresa + "' ";
            }

            if (this.Status != "0")
            {
                if (this.Status == "1")
                {
                    StringSQL_AUX_1 += "AND (isnull(JDT1.MthDate, '') <> '' AND ";
                    StringSQL_AUX_1 += " ((isnull(JDT1.BalDueDeb, 0) = 0 AND ";
                    StringSQL_AUX_1 += " isnull(JDT1.BalDueCred, 0) = 0))) ";
                }
                else
                {
                    StringSQL_AUX_1 += "AND (isnull(JDT1.MthDate, '') = '' OR ";
                    StringSQL_AUX_1 += " ((isnull(JDT1.BalDueDeb, 0) > 0 OR ";
                    StringSQL_AUX_1 += " isnull(JDT1.BalDueCred, 0) > 0))) ";
                }
            }

            if (this.QuantidadeDias > 0)
            {
                StringSQL_AUX_1 += "AND DATEDIFF(day,JDT1.DueDate, isnull(JDT1.MthDate, GETDATE()))> 0 ";
            }

            if (this.VencimentoInicial != null && this.VencimentoInicial != "")
            {
                StringSQL_AUX_1 += "AND JDT1.DueDate>='" + this.VencimentoInicial.ToString() + "' ";
            }

            if (this.VencimentoFinal != null && this.VencimentoFinal != "")
            {
                StringSQL_AUX_1 += "AND JDT1.DueDate<='" + this.VencimentoFinal.ToString() + "' ";
            }

            if (this.NotaFiscal > 0)
            {
                StringSQL_AUX_1 += "AND ORIN.Serial = '" + this.NotaFiscal.ToString() + "' ";
            }

            if (this.Valor > 0)
            {
                StringSQL_AUX_1 += "AND JDT1.Credit = '" + this.Valor.ToString() + "' ";
            }

            if (this.Ordenar != "0")
            {
                if (this.Ordenar == "1")
                {
                    StringSQL_AUX += "Order By TabAux.DataEmissao ";
                }
                else if (this.Ordenar == "2")
                {
                    StringSQL_AUX += "Order By TabAux.DataVencimento ";
                }
                else if (this.Ordenar == "3")
                {
                    StringSQL_AUX += "Order By TabAux.DataPagamentoAux ";
                }
            }

            if (this.Tipo == "2")
            {
                StringSQL_AUX += " Desc ";
            }

            #endregion

            #region StringSQL

            StringSQL += "select Empresa, DocEntry, ObjType, NotaFiscal, DataVencimento, DataEmissao, DataPagamento, ValorPagar from ( ";
            StringSQL += "select ORIN.BPLId Empresa, ORIN.DocEntry, ";
            StringSQL += "ORIN.ObjType, ORIN.Serial NotaFiscal, JDT1.DueDate DataVencimento, ";
            StringSQL += "ORIN.DocDate DataEmissao, isnull(convert(varchar(10), JDT1.MthDate,103),'') as DataPagamento, JDT1.MthDate as DataPagamentoAux , JDT1.Credit ValorPagar ";
            StringSQL += "from JDT1 ";
            StringSQL += "INNER JOIN ORIN ON JDT1.BaseRef = ORIN.DocEntry and TransType = '14' ";
            StringSQL += "INNER JOIN ORIN T1 ON T1.DocEntry = ORIN.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON ORIN.BPLId = OBPL.BPLId ";
            StringSQL += "INNER JOIN OCRD ON ORIN.CardCode = OCRD.CardCode and JDT1.ShortName = OCRD.CardCode ";
            StringSQL += "INNER JOIN OCRD T0 ON T0.CardCode = OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD1 ON CRD1.CardCode = OCRD.CardCode and CRD1.AdresType = 'S' ";
            StringSQL += "LEFT JOIN OSLP ON OSLP.SlpCode = OCRD.SlpCode ";
            StringSQL += "LEFT JOIN CRD7 ON CRD7.CardCode = OCRD.CardCode and CRD7.[Address]='' ";
            StringSQL += "LEFT JOIN RIN6 ON ORIN.DocEntry=RIN6.DocEntry and RIN6.InstLmntID=JDT1.SourceLine ";
            StringSQL += "LEFT JOIN OPYM ON OPYM.PayMethCod=ORIN.PeyMethod ";
            StringSQL += "LEFT JOIN ODSC ON OPYM.BankCountr= ODSC.CountryCod and OPYM.BnkDflt= ODSC.BankCode ";

            StringSQL += "where T0.CardCode= '" + this.CodigoCliente + "' and ORIN.CANCELED='N' ";

            //PRA TESTE USE ESTE
            //StringSQL += "where T0.CardCode= 'CLI0001352' ";

            StringSQL += StringSQL_AUX_1;

            StringSQL += ") TabAux ";

            StringSQL += StringSQL_AUX;

            #endregion

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;
        }

        #endregion

        #region Métodos do Financeiro - Conta Corrente - Pedidos

        public DataTable RecuperaCCPedidos()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL_AUX_1 = "";
            string StringSQL = "";

            #region if´s do StringSQL

            if (this.IDEmpresa != 0)
            {
                StringSQL_AUX_1 += " AND ORDR.BPLId='" + this.IDEmpresa + "' ";
            }

            if (this.PedidoSAP != 0)
            {
                StringSQL_AUX_1 += " AND ORDR.DocEntry='" + this.PedidoSAP + "' ";
            }

            if (this.PedidoCRM != 0)
            {
                StringSQL_AUX_1 += " AND ORDR.U_IB_CRM_CodPed='" + this.PedidoCRM + "' ";
            }

            #endregion

            #region StringSQL

            StringSQL += "select ORDR.BPLId Empresa, ORDR.DocEntry PedidoSAP, ";
            StringSQL += "isnull(ORDR.U_IB_CRM_CodPed, '0') PedidoCRM, ORDR.DocDate DataEmissao, ";
            StringSQL += "ORDR.DocTotal TotalPedido from ";
            StringSQL += "ORDR ";

            //PRA TESTE USE ESTE
            //StringSQL += "where ORDR.CardCode = 'CLI0009117' ";

            StringSQL += "where ORDR.CardCode = '" + this.CodigoCliente + "' ";

            StringSQL += "and ORDR.CANCELED = 'N' and ORDR.DocStatus = 'O' ";

            StringSQL += StringSQL_AUX_1;

            #endregion

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;
        }

        #endregion

        #endregion

        #region Métodos da Integração com o Sefaz

        public bool RetornaBloqueiaCamposConsultaSefaz()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_BLOQUEIA_CAMPOS_CONSULTA_SEFAZ", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            if (row["Bloqueia"].ToString() == "Sim")
                                return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return false;
        }

        public String GravaClienteSefaz(WSSaidaDadosReceita objWSSaidaDadosReceita)
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CLIENTE_SEFAZ", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeCliente", SqlDbType.VarChar, 8000, "NomeCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeFantasia", SqlDbType.VarChar, 8000, "NomeFantasia"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.VarChar, 8000, "CNPJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.VarChar, 8000, "Telefone"));
                    dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 8000, "Email"));
                    dbCommand.Parameters.Add(new SqlParameter("@ObservacaoSimples", SqlDbType.VarChar, 8000, "ObservacaoSimples"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDVendedor", SqlDbType.Int, 0, "IDVendedor"));
                    dbCommand.Parameters.Add(new SqlParameter("@SimplesNacional", SqlDbType.VarChar, 8000, "SimplesNacional"));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;

                    if (objWSSaidaDadosReceita.SintegraWSDadosSintegra != null)
                        dbCommand.Parameters["@NomeCliente"].Value = objWSSaidaDadosReceita.SintegraWSDadosSintegra.nome_empresarial;
                    else
                        dbCommand.Parameters["@NomeCliente"].Value = objWSSaidaDadosReceita.nome.ToUpper();

                    dbCommand.Parameters["@NomeFantasia"].Value = objWSSaidaDadosReceita.fantasia.ToUpper();

                    dbCommand.Parameters["@CNPJ"].Value = objWSSaidaDadosReceita.cnpj;
                    dbCommand.Parameters["@Telefone"].Value = objWSSaidaDadosReceita.telefone;
                    dbCommand.Parameters["@Email"].Value = objWSSaidaDadosReceita.email;
                    dbCommand.Parameters["@ObservacaoSimples"].Value = this.ObservacaoBreveCliente;
                    dbCommand.Parameters["@IDVendedor"].Value = this.IDVendedor;

                    if (objWSSaidaDadosReceita.PossuiSimplesNacional == "Não")
                    {
                        dbCommand.Parameters["@SimplesNacional"].Value = " Não Optante";
                    }
                    else if (objWSSaidaDadosReceita.PossuiSimplesNacional == "Sim")
                    {
                        if (objWSSaidaDadosReceita.SintegraWSDadosSimplesNacional.situacao_simei == "Enquadrado")
                            dbCommand.Parameters["@SimplesNacional"].Value = "Optante MEI";
                        else if (objWSSaidaDadosReceita.SintegraWSDadosSimplesNacional.situacao_simples_nacional.Substring(0, 7)
                            == "Optante")
                            dbCommand.Parameters["@SimplesNacional"].Value = "Optante ME/EPP";
                        else
                            dbCommand.Parameters["@SimplesNacional"].Value = "Não Optante";
                    }
                    else
                    {
                        dbCommand.Parameters["@SimplesNacional"].Value = "Não Optante";
                    }

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            erro = row["Erro"].ToString();
                            this.IDCliente = Convert.ToInt32(row["IDCliente"]);
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

        public String GravaClienteEnderecoSefaz(WSSaidaDadosReceita objWSSaidaDadosReceita)
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CLIENTE_ENDERECO_SEFAZ", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoLogradouro", SqlDbType.VarChar, 8000, "TipoLogradouro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Rua", SqlDbType.VarChar, 8000, "Rua"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroRua", SqlDbType.VarChar, 8000, "NumeroRua"));
                    dbCommand.Parameters.Add(new SqlParameter("@Complemento", SqlDbType.VarChar, 8000, "Complemento"));
                    dbCommand.Parameters.Add(new SqlParameter("@CEP", SqlDbType.VarChar, 8000, "CEP"));
                    dbCommand.Parameters.Add(new SqlParameter("@Bairro", SqlDbType.VarChar, 8000, "Bairro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Cidade", SqlDbType.VarChar, 8000, "Cidade"));
                    dbCommand.Parameters.Add(new SqlParameter("@Estado", SqlDbType.VarChar, 8000, "Estado"));
                    dbCommand.Parameters.Add(new SqlParameter("@Municipio", SqlDbType.VarChar, 8000, "Municipio"));
                    dbCommand.Parameters.Add(new SqlParameter("@Pais", SqlDbType.VarChar, 8000, "Pais"));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;

                    string logradouro = "";

                    if (objWSSaidaDadosReceita.SintegraWSDadosSintegra != null &&
                            (objWSSaidaDadosReceita.SintegraWSDadosSintegra.logradouro != null
                          || objWSSaidaDadosReceita.SintegraWSDadosSintegra.numero != null
                          || objWSSaidaDadosReceita.SintegraWSDadosSintegra.complemento != null
                          || objWSSaidaDadosReceita.SintegraWSDadosSintegra.cep != null
                          || objWSSaidaDadosReceita.SintegraWSDadosSintegra.bairro != null
                          || objWSSaidaDadosReceita.SintegraWSDadosSintegra.municipio != null))
                    {
                        logradouro = objWSSaidaDadosReceita.SintegraWSDadosSintegra.logradouro ?? "";

                        dbCommand.Parameters["@NumeroRua"].Value = objWSSaidaDadosReceita.SintegraWSDadosSintegra.numero ?? "";

                        dbCommand.Parameters["@Complemento"].Value = objWSSaidaDadosReceita.SintegraWSDadosSintegra.complemento ?? "";

                        dbCommand.Parameters["@CEP"].Value = objWSSaidaDadosReceita.SintegraWSDadosSintegra.cep ?? "";

                        dbCommand.Parameters["@Bairro"].Value = objWSSaidaDadosReceita.SintegraWSDadosSintegra.bairro ?? "";

                        dbCommand.Parameters["@Cidade"].Value = objWSSaidaDadosReceita.SintegraWSDadosSintegra.municipio ?? "";

                        dbCommand.Parameters["@Estado"].Value = objWSSaidaDadosReceita.SintegraWSDadosSintegra.uf ?? "";

                        dbCommand.Parameters["@Municipio"].Value = objWSSaidaDadosReceita.SintegraWSDadosSintegra.municipio ?? "";

                        dbCommand.Parameters["@Pais"].Value = "Brasil";
                    }
                    else
                    {
                        logradouro = objWSSaidaDadosReceita.logradouro;

                        dbCommand.Parameters["@NumeroRua"].Value = objWSSaidaDadosReceita.numero ?? "";

                        dbCommand.Parameters["@Complemento"].Value = objWSSaidaDadosReceita.complemento ?? "";

                        dbCommand.Parameters["@CEP"].Value = objWSSaidaDadosReceita.cep ?? "";

                        dbCommand.Parameters["@Bairro"].Value = objWSSaidaDadosReceita.bairro ?? "";

                        dbCommand.Parameters["@Cidade"].Value = objWSSaidaDadosReceita.municipio ?? "";

                        dbCommand.Parameters["@Estado"].Value = objWSSaidaDadosReceita.uf ?? "";

                        dbCommand.Parameters["@Municipio"].Value = objWSSaidaDadosReceita.municipio ?? "";

                        dbCommand.Parameters["@Pais"].Value = "Brasil";
                    }

                    //Pega a primeira palavra antes do primeiro espaço
                    string primeiraPalavraLogradouro = logradouro.Split(' ')[0];
                    dbCommand.Parameters["@TipoLogradouro"].Value = primeiraPalavraLogradouro;

                    //Pega as palavras depois do primeiro espaço
                    int indicePrimeiroEspaco = logradouro.IndexOf(' ');
                    string restoDaString = logradouro.Substring(indicePrimeiroEspaco + 1);
                    dbCommand.Parameters["@Rua"].Value = restoDaString;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            erro = row["Erro"].ToString();
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

        public String GravaClienteFiscalSefaz(WSSaidaDadosReceita objWSSaidaDadosReceita)
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            if (objWSSaidaDadosReceita == null)
            {
                return "Objeto WSSaidaDadosReceita é nulo.";
            }

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CLIENTE_FISCAL_SEFAZ", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNAE", SqlDbType.VarChar, 8000, "CNAE"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.VarChar, 8000, "CNPJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@InscricaoEstadual", SqlDbType.VarChar, 8000, "InscricaoEstadual"));
                    dbCommand.Parameters.Add(new SqlParameter("@Suframa", SqlDbType.VarChar, 8000, "Suframa"));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;

                    string CNAE = "";

                    // 1. Garante que o objeto principal não é nulo antes de prosseguir
                    if (objWSSaidaDadosReceita != null)
                    {
                        string cnaeSintegra = null;

                        // Extrai o código do CNAE de forma segura (passo a passo para C# antigo)
                        if (objWSSaidaDadosReceita.SintegraWSDadosSintegra != null &&
                            objWSSaidaDadosReceita.SintegraWSDadosSintegra.cnae_principal != null)
                        {
                            cnaeSintegra = objWSSaidaDadosReceita.SintegraWSDadosSintegra.cnae_principal.code;
                        }

                        // Se o CNAE do Sintegra for nulo ou "Não informado", busca na lista de atividades principais
                        if (string.IsNullOrEmpty(cnaeSintegra) || cnaeSintegra == "Não informado")
                        {
                            // Se a lista existir e tiver elementos
                            if (objWSSaidaDadosReceita.atividade_principal != null && objWSSaidaDadosReceita.atividade_principal.Count > 0)
                            {
                                // Pega o último elemento usando o tamanho da lista menos 1 (Compatível com C# antigo)
                                int ultimoIndice = objWSSaidaDadosReceita.atividade_principal.Count - 1;
                                var ultimaAtividade = objWSSaidaDadosReceita.atividade_principal[ultimoIndice];

                                if (ultimaAtividade != null)
                                {
                                    CNAE = ultimaAtividade.code;
                                }
                            }
                        }
                        else
                        {
                            // Se o CNAE do Sintegra for válido, usa ele
                            CNAE = cnaeSintegra;
                        }

                        // 2. Gravação segura da Inscrição Estadual
                        if (objWSSaidaDadosReceita.IsentoIE == "Não" &&
                            dbCommand != null &&
                            dbCommand.Parameters["@InscricaoEstadual"] != null)
                        {
                            if (objWSSaidaDadosReceita.SintegraWSDadosSintegra != null)
                            {
                                dbCommand.Parameters["@InscricaoEstadual"].Value = objWSSaidaDadosReceita.SintegraWSDadosSintegra.inscricao_estadual;
                            }
                        }
                    }

                    #region Formata CNAE

                    CNAE = CNAE ?? "";
                    CNAE = CNAE.Replace("C", "");
                    CNAE = CNAE.Replace(".", "");
                    CNAE = CNAE.Replace("-", "");

                    if (CNAE.Length >= 7)
                    {
                        StringBuilder sb = new StringBuilder(CNAE);
                        if (sb.Length >= 4) sb.Insert(4, "-");
                        if (sb.Length >= 6) sb.Insert(6, "/");
                        CNAE = sb.ToString();

                        if (CNAE.Length > 9)
                            CNAE = CNAE.Substring(0, 9);
                    }

                    #endregion

                    dbCommand.Parameters["@CNAE"].Value = CNAE;
                    dbCommand.Parameters["@CNPJ"].Value = objWSSaidaDadosReceita.cnpj;

                    // 1. Verifica de forma segura se o comando e o parâmetro existem
                    if (dbCommand != null && dbCommand.Parameters["@InscricaoEstadual"] != null)
                    {
                        // Obtém o valor atual de forma segura. Se for nulo, vira string vazia.
                        string inscricaoEstadual = dbCommand.Parameters["@InscricaoEstadual"].Value != null
                            ? dbCommand.Parameters["@InscricaoEstadual"].Value.ToString()
                            : "";

                        // 2. Se estiver vazio, define com base no objeto de receita (com proteção para nulos)
                        if (string.IsNullOrEmpty(inscricaoEstadual))
                        {
                            if (objWSSaidaDadosReceita != null && objWSSaidaDadosReceita.IsentoIE == "Sim")
                            {
                                dbCommand.Parameters["@InscricaoEstadual"].Value = "ISENTO";
                            }
                            else
                            {
                                dbCommand.Parameters["@InscricaoEstadual"].Value = "";
                            }

                            // Atualiza a variável local para refletir a mudança
                            inscricaoEstadual = dbCommand.Parameters["@InscricaoEstadual"].Value.ToString();
                        }

                        // 3. Formatação segura (Compatível com C# antigo)
                        if (!string.IsNullOrEmpty(inscricaoEstadual) && inscricaoEstadual != "ISENTO")
                        {
                            ulong ieNumerica; // <-- Declarada aqui fora para funcionar em qualquer versão do C#

                            // Tenta converter para número. Se conseguir, entra no IF e formata.
                            if (ulong.TryParse(inscricaoEstadual, out ieNumerica))
                            {
                                string ieFormatado = ieNumerica.ToString(@"000\.000\.000\.000");
                                dbCommand.Parameters["@InscricaoEstadual"].Value = ieFormatado;
                            }
                        }
                    }

                    if (objWSSaidaDadosReceita.SintegraWSDadosSuframa != null && objWSSaidaDadosReceita.PossuiSuframa == "Sim")
                        dbCommand.Parameters["@Suframa"].Value = objWSSaidaDadosReceita.SintegraWSDadosSuframa.inscricao_suframa;
                    else //if (objWSSaidaDadosReceita.PossuiSuframa == "Não")
                        dbCommand.Parameters["@Suframa"].Value = "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            erro = row["Erro"]?.ToString() ?? "";
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

        #endregion

        #region Classificação Comercial

        public void CarregaClienteTipoSolicitacaoStatus(string TipoSolicitacao)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CLIENTE_TIPOS_SOLICITACAO_STATUS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@TipoSolicitacao", SqlDbType.VarChar, 8000, "TipoSolicitacao"));

                    dbCommand.Parameters["@TipoSolicitacao"].Value = TipoSolicitacao;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            this.IDStatus = Convert.ToInt32(row["IDStatus"]);
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public DataTable CarregaClassificacaoComercial()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CLIENTE_CLASSIFICACAO_COMERCIAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoCliente", SqlDbType.VarChar, 8000, "CodigoCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSimulacao", SqlDbType.Int, 0, "IDSimulacao"));

                    dbCommand.Parameters["@CodigoCliente"].Value = this.CodigoCliente ?? "";
                    dbCommand.Parameters["@IDSimulacao"].Value = this.IdSimulacao;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;

        }

        public DataTable Carrega_Solicitacao_Classificacao_Comercial_Situacao()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CLIENTE_SOL_CLAS_COM_SITUACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;

        }

        public DataTable Carrega_Solicitacao_Classificacao_Comercial()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CLIENTE_SOL_CLASSIFICACAO_COMERCIAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSolicitacao", SqlDbType.Int, 0, "IDSolicitacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 8000, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 8000, "DataFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@Cliente", SqlDbType.VarChar, 8000, "Cliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDVendedor", SqlDbType.Int, 0, "IDVendedor"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSituacao", SqlDbType.Int, 0, "IDSituacao"));

                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@IDSolicitacao"].Value = this.IDSolicitacao;
                    dbCommand.Parameters["@DataInicial"].Value = this.DataInicial ?? "";
                    dbCommand.Parameters["@DataFinal"].Value = this.DataFinal ?? "";
                    dbCommand.Parameters["@Cliente"].Value = this.Cliente ?? "";
                    dbCommand.Parameters["@IDVendedor"].Value = this.IDVendedor;
                    dbCommand.Parameters["@IDSituacao"].Value = this.IDSituacao;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;

        }

        public string AtualizaClassificacaoComercial()
        {
            string retorno = "";
            string JsonCliente = "";

            WSAtualizaClassificacaoComercial objWSAtualizaClassificacaoComercial =
                new WSAtualizaClassificacaoComercial();

            objWSAtualizaClassificacaoComercial.CodigoClienteSAP = this.CodigoClienteSAP;

            objWSAtualizaClassificacaoComercial.ClassificacaoComercialSAP = this.CodigoSAP.ToString();

            JsonCliente = jsonconv.ConverteObjectParaJSon(objWSAtualizaClassificacaoComercial);

            retorno = OBJApi.AtualizaClassificacaoComercial(JsonCliente);

            WSRetornoJSONClass objWSRetornoJSONClass = new WSRetornoJSONClass();

            objWSRetornoJSONClass = jsonconv.ConverteJSonParaObject<WSRetornoJSONClass>(retorno);

            return objWSRetornoJSONClass.MsgRetorno;
        }

        public string Grava_Solicitacao_Classificacao_Comercial()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CLIENTE_SOL_CLASSIFICACAO_COMERCIAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSolicitacao", SqlDbType.Int, 0, "IDSolicitacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDClassificacaoComercial", SqlDbType.Int, 0, "IDClassificacaoComercial"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDHistorico", SqlDbType.Int, 0, "IDHistorico"));

                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@IDSolicitacao"].Value = this.IDSolicitacao;
                    dbCommand.Parameters["@IDClassificacaoComercial"].Value = this.IDClassificacaoComercial;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuarioOperacao;
                    dbCommand.Parameters["@IDHistorico"].Value = this.IDHistorico;

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

            return erro;
        }

        #endregion

        #region Métodos da Análise de Credito

        public DataTable CarregaAnaliseCredito()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_CREDITO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 8000, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 8000, "DataFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@DataInicial"].Value = DataInicial;
                    dbCommand.Parameters["@DataFinal"].Value = DataFinal;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaAnaliseCreditoDetalhe()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_CREDITO_DETALHE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaGrafiasSemelhantes()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_CONCENTRE_GRAFIAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@Tela", SqlDbType.VarChar, 8000, "Tela"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@Tela"].Value = Tela ?? "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public string CarregaFraseAlerta()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_FRASE_ALERTA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["FraseAlerta"].ToString();
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

        public DataTable CarregaScoreSerasa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_RISKSCORING_PRINAD", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaScoreSerasaInterpretacao()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_FAIXAS_INTERPRETACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable RetornaValorParametro(string Parametro)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PARAMETROS_GERAIS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Parametro", SqlDbType.VarChar, 8000, "Parametro"));

                    dbCommand.Parameters["@Parametro"].Value = Parametro;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public string GravaJsonSerasa(string JSON)
        {
            try
            {
                ParametroGeral objParametroGeral = new ParametroGeral();

                string Parametro = objParametroGeral.RetornaValorStringParametro("GRAVAJSONSERASA");

                if (Parametro == "Sim" || Parametro == "\tSim")
                {

                    // Obter o caminho para a pasta base
                    string pastaBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Clientes\\AnaliseCreditoJSONs");

                    // Verificar se a pasta base não existe e, se for o caso, criá-la
                    if (!Directory.Exists(pastaBase))
                    {
                        Directory.CreateDirectory(pastaBase);
                    }

                    // Criar uma pasta com o ID do cliente dentro da pasta base
                    string pastaCliente = Path.Combine(pastaBase, this.IDCliente.ToString());

                    // Verificar se a pasta do cliente não existe e, se for o caso, criá-la
                    if (!Directory.Exists(pastaCliente))
                    {
                        Directory.CreateDirectory(pastaCliente);
                    }

                    // Criar um nome de arquivo baseado na data atual
                    string nomeArquivo = $"Json_Serasa_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.txt";

                    // Caminho completo do arquivo dentro da pasta do cliente
                    string caminhoCompletoArquivo = Path.Combine(pastaCliente, nomeArquivo);

                    // Escrever o conteúdo no arquivo
                    using (StreamWriter writer = new StreamWriter(caminhoCompletoArquivo))
                    {
                        // Escrever o cabeçalho com ID do cliente e a data
                        writer.WriteLine($"ID do Cliente: {this.IDCliente.ToString()}");
                        writer.WriteLine($"Data: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                        writer.WriteLine(); // Espaço em branco entre cabeçalho e conteúdo JSON

                        // Escrever o conteúdo JSON
                        writer.WriteLine(JSON);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }

        #region Anotacoes Negativas Da Empresa

        public DataTable CarregaTextoAnotacoesNegativasDaEmpresa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_Texto_Anotacoes_Negativas_Da_Empresa", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaConcetreResumoEmpresa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_CONCENTRE_RESUMO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaPefinEmpresa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_PEFIN", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaRefinEmpresa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_REFIN", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaProtestoEmpresa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_PROTESTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaAcaoJudicialEmpresa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_ACAO_JUDICIAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaChequesEmpresa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_CHEQUE_SEM_FUNDO_E_CCF", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaRechequeEmpresa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_RECHEQUE_DETALHES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaParticipacaoFalenciaEmpresa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_FALENCIA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaDividaVencidaEmpresa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_DIVIDAS_VENCIDAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        #endregion

        #region Anotacoes Negativas Dos Socios/Administradores

        public DataTable CarregaTextoAnotacoesNegativasSociosAdm()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_Texto_Anotacoes_Negativas_Socios_Adm", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@CPFCNPJ", SqlDbType.VarChar, 8000, "CPFCNPJ"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@CPFCNPJ"].Value = CPFCNPJ ?? "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaAnotacoesNegativasSociosAdm()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_Anotacoes_Negativas_Socios_Adm", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@CPFCNPJ", SqlDbType.VarChar, 8000, "CPFCNPJ"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@CPFCNPJ"].Value = CPFCNPJ ?? "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        #endregion

        #region Quadro Social

        public DataTable CarregaQuadroSocial()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_CONT_SOC_ULTATU_CAPSOCl", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaQuadroSocialGridView()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_Quadro_Social_Grid_View", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaQuadroSocialDetalhe()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_Quadro_Social_Detalhe", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@CPFCNPJ", SqlDbType.VarChar, 8000, "CPFCNPJ"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@CPFCNPJ"].Value = CPFCNPJ;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        #endregion

        #region Administracao

        public DataTable CarregaAdministracaoGridView()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_Administracao_Grid_View", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaAdministracaoDetalhe()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_Administracao_Detalhe", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@CPFCNPJ", SqlDbType.VarChar, 8000, "CPFCNPJ"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@CPFCNPJ"].Value = CPFCNPJ;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        #endregion

        #region Informação sobre consultas

        public DataTable CarregaGraficoInfSobCon()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_CONSULTAS_grafico", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        #endregion

        #region Últimas 5 consultas Realizadas

        public DataTable CarregaUltimasConsultasRealizadas()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_ULTIMAS_CONSULTAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaUltimasConsultasRealizadasTodas()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_ULTIMAS_CONSULTAS_Todas", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        #endregion

        #region Histórico de pagamentos

        public DataTable CarregaQuantidadeDeTitulos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_HIST_PAG_QTDTIT", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable CarregaMercadoValoresEmReais()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_HIST_PAGAMENTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        #endregion

        #region Evolução de Compromissos

        public DataTable CarregaEvolucaoCompromissos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_EVOL_COMPROMISSO_FOR_grafico", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        #endregion

        #region Referenciais de negócios (valores em reais)

        public DataTable CarregaReferenciasDeNegocios()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_REFERENCIAIS_NEGOCIOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        #endregion

        #region Relacionamento com fornecedores

        public DataTable CarregaRelacionamentoComFornecedores()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ANALISE_SERASA_REL_FORNECEDOR_PERIODO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        #endregion

        #region CENPROT

        public CENPROTClass Consulta_CENPROT_PARAMETROS()
        {
            DataTable outputTable = new DataTable();

            CENPROTClass objCENPROTClass = new CENPROTClass();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CONSULTA_CRM_CENPROT_PARAMETROS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            string ValorString = row["ValorString"].ToString();

                            switch (row["NomeParametro"].ToString())
                            {
                                case "TOKEN":
                                    objCENPROTClass.TOKEN = ValorString;
                                    break;

                                //case "LOGINCPF":
                                //    objCENPROTClass.LOGINCPF = ValorString;
                                //    break;

                                //case "LOGINSENHA":
                                //    objCENPROTClass.LOGINSENHA = ValorString;
                                //    break;

                                //case "PKCS12CERT":
                                //    objCENPROTClass.PKCS12CERT = ValorString;
                                //    break;

                                //case "PKCS12PASS":
                                //    objCENPROTClass.PKCS12PASS = ValorString;
                                //    break;

                                //case "PKCS12VALID":
                                //    objCENPROTClass.PKCS12VALID = ValorString;
                                //    break;

                                //case "TIPOAUTENTICAO":
                                //    objCENPROTClass.TIPOAUTENTICAO = ValorString;
                                //    break;

                                //case "URLCHAMADA":
                                //    objCENPROTClass.URLCHAMADA = ValorString;
                                //    break;

                                //case "LOGINCNPJ":
                                //    objCENPROTClass.LOGINCNPJ = ValorString;
                                //    break;

                                //case "CHAVECRIPTOGRAFIA":
                                //    objCENPROTClass.CHAVECRIPTOGRAFIA = ValorString;
                                //    break;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar os parametros do CENPROT do banco de dados: " + ex.Message);
            }

            return objCENPROTClass;
        }

        public string Consulta_CENPROT_CRMAPI()
        {
            string retorno = "";
            string Json = "";

            WSConsultaCENPROT objWSConsultaCENPROT = new WSConsultaCENPROT();

            objWSConsultaCENPROT.IDCliente = this.IDCliente;

            objWSConsultaCENPROT.IDAnalise = this.IDAnalise;

            Json = jsonconv.ConverteObjectParaJSon(objWSConsultaCENPROT);

            retorno = OBJApi.Consulta_CENPROT_CRMAPI(Json);

            WSRetornoJSONClass objWSRetornoJSONClass = new WSRetornoJSONClass();

            if (retorno != "")
            {
                objWSRetornoJSONClass = jsonconv.ConverteJSonParaObject<WSRetornoJSONClass>(retorno);

                return objWSRetornoJSONClass.MsgRetorno;
            }

            return "";
        }

        public DataTable Carrega_CENPROT_GridView()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CONSULTA_CENPROT_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public string Grava_CRM_CENPROT_PARAMETROS()
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CENPROT_PARAMETROS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@PKCS12CERT", SqlDbType.VarChar, 8000, "PKCS12CERT"));
                    dbCommand.Parameters.Add(new SqlParameter("@PKCS12PASS", SqlDbType.VarChar, 8000, "PKCS12PASS"));
                    dbCommand.Parameters.Add(new SqlParameter("@PKCS12VALID", SqlDbType.VarChar, 8000, "PKCS12VALID"));

                    dbCommand.Parameters["@PKCS12CERT"].Value = PKCS12CERT;
                    dbCommand.Parameters["@PKCS12PASS"].Value = PKCS12PASS;
                    dbCommand.Parameters["@PKCS12VALID"].Value = PKCS12VALID;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }

        public string Grava_CRM_CENPROT_CLIENTE()
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CENPROT_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataCenprot", SqlDbType.DateTime, 8000, "DataCenprot"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@DataCenprot"].Value = DateTime.Now;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }

        public int Grava_CRM_CENPROT_CLIENTE_CARTORIOS()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CENPROT_CLIENTE_CARTORIOS", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                dbCommand.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.VarChar, 8000, "Codigo"));
                dbCommand.Parameters.Add(new SqlParameter("@Cartorio", SqlDbType.VarChar, 8000, "Cartorio"));
                dbCommand.Parameters.Add(new SqlParameter("@TelefoneCartorio", SqlDbType.VarChar, 8000, "TelefoneCartorio"));
                dbCommand.Parameters.Add(new SqlParameter("@Endereco", SqlDbType.VarChar, 8000, "Endereco"));
                dbCommand.Parameters.Add(new SqlParameter("@Uf", SqlDbType.VarChar, 8000, "Uf"));
                dbCommand.Parameters.Add(new SqlParameter("@CidadeCodigo", SqlDbType.VarChar, 8000, "CidadeCodigo"));
                dbCommand.Parameters.Add(new SqlParameter("@CodigoIBGE", SqlDbType.VarChar, 8000, "CodigoIBGE"));
                dbCommand.Parameters.Add(new SqlParameter("@Municipio", SqlDbType.VarChar, 8000, "Municipio"));
                dbCommand.Parameters.Add(new SqlParameter("@Bairro", SqlDbType.VarChar, 8000, "Bairro"));
                dbCommand.Parameters.Add(new SqlParameter("@AtualizacaoData", SqlDbType.VarChar, 8000, "AtualizacaoData"));
                dbCommand.Parameters.Add(new SqlParameter("@Quantidade", SqlDbType.VarChar, 8000, "Quantidade"));
                dbCommand.Parameters.Add(new SqlParameter("@PeriodoPesquisa", SqlDbType.VarChar, 8000, "PeriodoPesquisa"));

                dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                dbCommand.Parameters["@Codigo"].Value = Codigo;
                dbCommand.Parameters["@Cartorio"].Value = Cartorio;
                dbCommand.Parameters["@TelefoneCartorio"].Value = TelefoneCartorio;
                dbCommand.Parameters["@Endereco"].Value = Endereco;
                dbCommand.Parameters["@Uf"].Value = Uf;
                dbCommand.Parameters["@CidadeCodigo"].Value = CidadeCodigo;
                dbCommand.Parameters["@CodigoIBGE"].Value = CodigoIBGE;
                dbCommand.Parameters["@Municipio"].Value = Municipio;
                dbCommand.Parameters["@Bairro"].Value = Bairro;
                dbCommand.Parameters["@AtualizacaoData"].Value = AtualizacaoData;
                dbCommand.Parameters["@Quantidade"].Value = Quantidade;
                dbCommand.Parameters["@PeriodoPesquisa"].Value = PeriodoPesquisa;

                using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                {
                    outputTable.Load(dataReader);
                }

                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {
                        return Convert.ToInt32(row["IDCartorio"]);
                    }
                }
            }

            return 0;
        }

        public string Grava_CRM_CENPROT_CLIENTE_CARTORIOS_PROTESTOS()
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CENPROT_CLIENTE_CARTORIOS_PROTESTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCartorio", SqlDbType.Int, 0, "IDCartorio"));
                    dbCommand.Parameters.Add(new SqlParameter("@CPFCNPJ", SqlDbType.VarChar, 8000, "CPFCNPJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@Data", SqlDbType.VarChar, 8000, "Data"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataProtesto", SqlDbType.VarChar, 8000, "DataProtesto"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataProtestoString", SqlDbType.VarChar, 8000, "DataProtestoString"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataVencimento", SqlDbType.VarChar, 8000, "DataVencimento"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataVencimentoString", SqlDbType.VarChar, 8000, "DataVencimentoString"));
                    dbCommand.Parameters.Add(new SqlParameter("@Valor", SqlDbType.Decimal, 0, "Valor"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorString", SqlDbType.VarChar, 8000, "ValorString"));
                    dbCommand.Parameters.Add(new SqlParameter("@Chave", SqlDbType.VarChar, 8000, "Chave"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeApresentante", SqlDbType.VarChar, 8000, "NomeApresentante"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeCedente", SqlDbType.VarChar, 8000, "NomeCedente"));
                    dbCommand.Parameters.Add(new SqlParameter("@TemAnuencia", SqlDbType.VarChar, 8000, "TemAnuencia"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@IDCartorio"].Value = IDCartorio;
                    dbCommand.Parameters["@CPFCNPJ"].Value = CPFCNPJ;
                    dbCommand.Parameters["@Data"].Value = Data;
                    dbCommand.Parameters["@DataProtesto"].Value = DataProtesto;
                    dbCommand.Parameters["@DataProtestoString"].Value = DataProtestoString;
                    dbCommand.Parameters["@DataVencimento"].Value = DataVencimento;
                    dbCommand.Parameters["@DataVencimentoString"].Value = DataVencimentoString;
                    dbCommand.Parameters["@Valor"].Value = Valor;
                    dbCommand.Parameters["@ValorString"].Value = ValorString;
                    dbCommand.Parameters["@Chave"].Value = Chave;
                    dbCommand.Parameters["@NomeApresentante"].Value = NomeApresentante;
                    dbCommand.Parameters["@NomeCedente"].Value = NomeCedente;
                    dbCommand.Parameters["@TemAnuencia"].Value = TemAnuencia;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }

        public DataTable Consulta_CRM_CENPROT_CLIENTE_CARTORIOS()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CONSULTA_CENPROT_CLIENTE_CARTORIOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCartorio", SqlDbType.Int, 0, "IDCartorio"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@IDCartorio"].Value = IDCartorio;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable Consulta_CRM_CENPROT_CLIENTE_CARTORIOS_PROTESTOS()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CONSULTA_CENPROT_CLIENTE_CARTORIOS_PROTESTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCartorio", SqlDbType.Int, 0, "IDCartorio"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@IDCartorio"].Value = IDCartorio;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public string CriptografaCertificado(string caminhoAbsolutoArquivo)
        {
            CENPROTClass objCENPROTClass = new CENPROTClass();

            objCENPROTClass = Consulta_CENPROT_PARAMETROS();

            AES256Class AES = new AES256Class();

            string CriptogramaCertificado = AES.Encrypt_V2(File.ReadAllBytes(caminhoAbsolutoArquivo), objCENPROTClass.CHAVECRIPTOGRAFIA);

            return CriptogramaCertificado;
        }

        public string CriptografaSenhaCertificado(string senhaCertificado)
        {
            CENPROTClass objCENPROTClass = new CENPROTClass();

            objCENPROTClass = Consulta_CENPROT_PARAMETROS();

            AES256Class AES = new AES256Class();

            string CriptogramaSenha = AES.Encrypt_V2(Encoding.UTF8.GetBytes(senhaCertificado), objCENPROTClass.CHAVECRIPTOGRAFIA);

            return CriptogramaSenha;
        }

        public string DescriptografaSenhaCertificado()
        {
            CENPROTClass objCENPROTClass = new CENPROTClass();

            objCENPROTClass = Consulta_CENPROT_PARAMETROS();

            AES256Class AES = new AES256Class();

            return AES.Decrypt(objCENPROTClass.PKCS12PASS, objCENPROTClass.CHAVECRIPTOGRAFIA);
        }

        public string DescriptografaCertificado()
        {
            CENPROTClass objCENPROTClass = new CENPROTClass();

            objCENPROTClass = Consulta_CENPROT_PARAMETROS();

            AES256Class AES = new AES256Class();

            byte[] certificadoDesencriptado = AES.DecryptToBytes_V2(objCENPROTClass.PKCS12CERT, objCENPROTClass.CHAVECRIPTOGRAFIA);

            string RecuperaDiretorioBase = AppDomain.CurrentDomain.BaseDirectory;

            string caminhoTemp = Path.Combine(RecuperaDiretorioBase, "~/Temp/");

            if (!Directory.Exists(caminhoTemp)) Directory.CreateDirectory(caminhoTemp);

            string caminhoAbsolutoArquivo = Path.Combine(caminhoTemp, "certificado.pfx");

            File.WriteAllBytes(caminhoAbsolutoArquivo, certificadoDesencriptado);

            return caminhoAbsolutoArquivo;
        }

        public string DescriptografaCertificado_API()
        {
            CENPROTClass objCENPROTClass = new CENPROTClass();

            objCENPROTClass = Consulta_CENPROT_PARAMETROS();

            AES256Class AES = new AES256Class();

            byte[] certificadoDesencriptado = AES.DecryptToBytes_V2(objCENPROTClass.PKCS12CERT, objCENPROTClass.CHAVECRIPTOGRAFIA);

            string RecuperaDiretorioBase = AppDomain.CurrentDomain.BaseDirectory;

            string caminhoTemp = Path.Combine(RecuperaDiretorioBase, "~/Temp/");

            if (!Directory.Exists(caminhoTemp)) Directory.CreateDirectory(caminhoTemp);

            string caminhoAbsolutoArquivo = Path.Combine(caminhoTemp, "certificado.pfx");

            File.WriteAllBytes(caminhoAbsolutoArquivo, certificadoDesencriptado);

            return caminhoAbsolutoArquivo;
        }

        #endregion

        #endregion

        public string PostCliente(int _IDCliente, string _Operacao)
        {
            FuncoesAPIClass OBJApi = new FuncoesAPIClass();
            jsonconv = new JsonConversao();

            //string _Retorno = "";
            //string _RetornoAlteraCodSap = "";
            string _JSON = "";
            //string _URI = "";
            string erro = "";

            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            ClienteClasse OBJCliente = new ClienteClasse();
            WsHubClienteClass ObjWsHubClienteClass = new WsHubClienteClass();
            WsHubClienteResponseClass ObjWsHubClienteResponseClass = new WsHubClienteResponseClass();

            //Cria URL
            //if (_Operacao == "Inclusão")
            //{
            //    _URI = urlPadraoSAP + "api/Posts/Post?consulta=criar_pn_lead&hash=" + hash;
            //}
            //else
            //{
            //    _URI = urlPadraoSAP + "api/Posts/Post?consulta=atualizar_cliente&hash=" + hash;
            //}

            //Consulta Dados para Enviar para o HUB
            ObjWsHubClienteClass.ExportaDadosCliente(_IDCliente, _Operacao);


            //Converte Classe em JSON
            //_JSON = jsonconv.ConverteObjectParaJSon<WsHubClienteClass>(ObjWsHubClienteClass);
            //_JSON = _JSON.Replace("\"data_Carta_IPI\":\"\",", "");

            _JSON = JsonConvert.SerializeObject(ObjWsHubClienteClass);
            if (_Operacao == "Inclusão")
            {
                erro = OBJApi.InclusaoClienteAPI(_JSON);
            }
            else
            {
                erro = OBJApi.AtualizacaoClienteAPI(_JSON);
            }

            //Chama API HUB
            //Uri u = new Uri(_URI);
            //HttpContent c = new StringContent(_JSON, Encoding.UTF8, "application/json");
            //var t = PostURI(u, c);
            //t.Wait();

            //Descarrega Dados
            //string retorno = t.Result.ToString();
            //ObjWsHubClienteResponseClass = jsonconv.ConverteJSonParaObject<WsHubClienteResponseClass>(t.Result.ToString());

            //if (ObjWsHubClienteResponseClass.resultPositivo == "true")
            //{

            //    if (_Operacao == "Inclusão")
            //    {
            //        //Atualiza Cliene com ID SAP
            //        OBJCliente.IDCliente = _IDCliente;
            //        OBJCliente.CodigoCliente = ObjWsHubClienteResponseClass.Codigo;

            //        _RetornoAlteraCodSap = OBJCliente.AlteraClienteCodigoSAP();

            //        if (_RetornoAlteraCodSap != "")
            //        {
            //            _Retorno += _RetornoAlteraCodSap;
            //        }

            //    }
            //}


            //Retorna Msg 

            //_Retorno += ObjWsHubClienteResponseClass.msg;
            //_Retorno = "resultPositivo:" + ObjWsHubClienteResponseClass.resultPositivo + " <br> " + _Retorno;

            return erro;

        }
    }
}
