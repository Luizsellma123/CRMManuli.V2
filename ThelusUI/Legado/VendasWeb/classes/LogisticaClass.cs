using System;
using System.Data.SqlClient;
using System.Data;
using VendasWeb.classes;
using VendasWeb.WEBServiceCRM;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using VendasWeb.WEBServiceSAP.ClassesWEBService;

namespace VendasWeb
{
    public class LogisticaClass : GerencialVendas.clsConexao
    {
        #region Campos

        public int IDEmpresa { get; set; }

        public string DataInicial { get; set; }

        public string DataFinal { get; set; }

        public string Operacao { get; set; }

        public int IDStatus { get; set; }

        public string Descricao { get; set; }

        public int Bloqueado { get; set; }

        public int Ativo { get; set; }

        public string Filtro { get; set; }

        public string TipoFiltro { get; set; }

        public int IDUsuario { get; set; }

        public int IDUsuarioAlteracao { get; set; }

        public Int64 NumeroFatura { get; set; }

        public int NumeroNota { get; set; }

        public string Parceiro { get; set; }

        public int Fechamento { get; set; }

        public string CNPJ { get; set; }

        public string Data { get; set; }

        public string Vencimento { get; set; }

        public decimal ValorFatura { get; set; }

        public int IDNota { get; set; }

        public string CodigoUsuario { get; set; }

        public int Identificado { get; set; }

        public int Importado { get; set; }

        public decimal ValorNota { get; set; }

        public int PrimarioNotaSAP { get; set; }

        public string CodigoClienteSAP { get; set; }

        public int IDTransportador { get; set; }

        public int IDCliente { get; set; }

        public int IDRegiao { get; set; }

        public string CodigoRegiao { get; set; }

        public string DescricaoRegiao { get; set; }

        public int IDPais { get; set; }

        public int IDEstado { get; set; }

        public int IDMunicipio { get; set; }

        public int IDParametro { get; set; }

        public string Nome { get; set; }

        public string ValorString { get; set; }

        public decimal ValorNumerico { get; set; }

        public decimal PesoNota { get; set; }

        public string ValorFrete { get; set; }

        public string PrevisaoEntrega { get; set; }

        #endregion

        ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();
        JsonConversao jsonconv = new JsonConversao();
        FuncoesAPIClass OBJApi = new FuncoesAPIClass();
        SQLUtilClass objSQLUtilClass = new SQLUtilClass();

        public DataTable RetornaListaStatusFechamentoFatura()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_STATUS_FECHAMENTO_FATURA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoFiltro", SqlDbType.VarChar, 8000, "TipoFiltro"));

                    dbCommand.Parameters["@Filtro"].Value = this.Filtro;
                    dbCommand.Parameters["@TipoFiltro"].Value = this.TipoFiltro;

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

        public string GravaStatusFechamentoFatura()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_STATUS_FECHAMENTO_FATURA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));
                    dbCommand.Parameters.Add(new SqlParameter("@Bloqueado", SqlDbType.Bit, 0, "Bloqueado"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Bit, 0, "Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;
                    dbCommand.Parameters["@Bloqueado"].Value = this.Bloqueado;
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao;
                    dbCommand.Parameters["@Ativo"].Value = this.Ativo;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            this.IDStatus = Convert.ToInt32(row["IDStatus"].ToString());
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public DataTable RetornaListaFechamentoFatura()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_FECHAMENTO_FATURA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 8000, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 8000, "DataFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroFatura", SqlDbType.BigInt, 0, "NumeroFatura"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));
                    dbCommand.Parameters.Add(new SqlParameter("@Parceiro", SqlDbType.VarChar, 8000, "Parceiro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Fechamento", SqlDbType.Int, 0, "Fechamento"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                    dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;
                    dbCommand.Parameters["@NumeroFatura"].Value = this.NumeroFatura;
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;
                    dbCommand.Parameters["@Parceiro"].Value = this.Parceiro;
                    dbCommand.Parameters["@Fechamento"].Value = this.Fechamento;

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

        public DataTable RetornaListaFechamentoFaturaDetalhe()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_FECHAMENTO_FATURA_DETALHE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Fechamento", SqlDbType.Int, 0, "Fechamento"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@Fechamento"].Value = this.Fechamento;

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

        public DataTable RetornaListaFechamentoFaturaNotas()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_FECHAMENTO_FATURA_NOTAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Fechamento", SqlDbType.Int, 0, "Fechamento"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@Fechamento"].Value = this.Fechamento;

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

        public string GravaFechamentoFatura()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_FECHAMENTO_FATURA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));
                    dbCommand.Parameters.Add(new SqlParameter("@Fechamento", SqlDbType.Int, 0, "Fechamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.VarChar, 8000, "CNPJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@Vencimento", SqlDbType.VarChar, 8000, "Vencimento"));
                    dbCommand.Parameters.Add(new SqlParameter("@Data", SqlDbType.VarChar, 8000, "Data"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorFatura", SqlDbType.Decimal, 0, "ValorFatura"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroFatura", SqlDbType.BigInt, 0, "NumeroFatura"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;
                    dbCommand.Parameters["@Fechamento"].Value = this.Fechamento;
                    dbCommand.Parameters["@CNPJ"].Value = this.CNPJ;
                    dbCommand.Parameters["@Vencimento"].Value = this.Vencimento;
                    dbCommand.Parameters["@Data"].Value = this.Data;
                    dbCommand.Parameters["@ValorFatura"].Value = this.ValorFatura;
                    dbCommand.Parameters["@NumeroFatura"].Value = this.NumeroFatura;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            this.IDEmpresa = Convert.ToInt32(row["IDEmpresa"].ToString());
                            this.Fechamento = Convert.ToInt32(row["Fechamento"].ToString());
                            if (row["Erro"].ToString() != "") return row["Erro"].ToString();
                        }
                    }

                    dataReader.Close();

                    this.Operacao = "Alteracao";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public string CancelaFechamentoFatura()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CANCELA_FECHAMENTO_FATURA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Fechamento", SqlDbType.Int, 0, "Fechamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@Fechamento"].Value = this.Fechamento;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public string ExcluiFechamentoFaturaNotas()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_LIMPA_DADOS_FECHAMENTO_FATURA_NOTAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Fechamento", SqlDbType.Int, 0, "Fechamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDNota", SqlDbType.Int, 0, "IDNota"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@Fechamento"].Value = this.Fechamento;
                    dbCommand.Parameters["@IDNota"].Value = this.IDNota;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Erro"].ToString();
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public string ImportaFechamentoFatura()
        {
            string retorno = "";
            string JSONFechamentoFatura = "";
            ImportaFechamentoFaturaClass OBJFechamentoFatura = new ImportaFechamentoFaturaClass();

            //Carrega Objeto para enviar
            OBJFechamentoFatura.IDEmpresa = this.IDEmpresa;
            OBJFechamentoFatura.IDFechamentoFatura = this.Fechamento;
            OBJFechamentoFatura.CodigoUsuarioCRM = this.CodigoUsuario.ToString();

            //Transforma em JSON para enviar para o WEB SERVICE
            JSONFechamentoFatura = jsonconv.ConverteObjectParaJSon<ImportaFechamentoFaturaClass>(OBJFechamentoFatura);

            retorno = OBJApi.ImportaFechamentoFaturaSAPCRMAPI(JSONFechamentoFatura);

            return retorno;
        }

        public string AtualizaImportadoSAPFechamentoFaturaNotas()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_IMPORTADOSAP_FECHAMENTO_FATURA_NOTAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Fechamento", SqlDbType.Int, 0, "Fechamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@Fechamento"].Value = this.Fechamento;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public string GravaFechamentoFaturaNotas()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_FECHAMENTO_FATURA_NOTAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Fechamento", SqlDbType.Int, 0, "Fechamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroNota", SqlDbType.Int, 0, "NumeroNota"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.VarChar, 8000, "CodigoClienteSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrimarioNotaSAP", SqlDbType.Int, 0, "PrimarioNotaSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorNota", SqlDbType.Decimal, 0, "ValorNota"));
                    dbCommand.Parameters.Add(new SqlParameter("@IdentificadoSAP", SqlDbType.Int, 0, "IdentificadoSAP"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@Fechamento"].Value = this.Fechamento;
                    dbCommand.Parameters["@NumeroNota"].Value = this.NumeroNota;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    dbCommand.Parameters["@CodigoClienteSAP"].Value = this.CodigoClienteSAP;
                    dbCommand.Parameters["@PrimarioNotaSAP"].Value = this.PrimarioNotaSAP;
                    dbCommand.Parameters["@ValorNota"].Value = this.ValorNota;
                    dbCommand.Parameters["@IdentificadoSAP"].Value = this.Identificado;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public DataTable RetornaDadosSAPFechamentoFaturaNotas()
        {
            string StringSQL = "";

            StringSQL = "SELECT DISTINCT OCRD.CardCode, OCRD.CardName, OPCH.DocEntry, PCH6.InsTotal ";
            StringSQL += "FROM OPCH INNER JOIN PCH6 ON OPCH.DocEntry=PCH6.DocEntry AND PCH6.InstlmntID=1 ";
            StringSQL += "INNER JOIN OCRD ON OPCH.CardCode=OCRD.CardCode ";
            StringSQL += "INNER JOIN CRD7 ON OCRD.CardCode=CRD7.CardCode ";
            StringSQL += "WHERE OPCH.CANCELED='N' and OPCH.DocStatus='O' and OCRD.CardType='S' and ";
            StringSQL += "(LEFT(replace(replace(replace(CRD7.TaxId0,'.',''),'/',''),'-',''),8)= ";
            StringSQL += "LEFT(replace(replace(replace('" + this.CNPJ + "','.',''),'/',''),'-',''),8) OR ";
            StringSQL += "LEFT(replace(replace(replace(CRD7.TaxId4,'.',''),'/',''),'-',''),8)= ";
            StringSQL += "LEFT(replace(replace(replace('" + this.CNPJ + "','.',''),'/',''),'-',''),8))and ";
            StringSQL += "OPCH.Serial=" + this.NumeroNota + " ";

            DataTable ConsultaSAP = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            return ConsultaSAP;
        }

        public DataTable RetornaListaTransportador()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_TRANSPORTADOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoFiltro", SqlDbType.VarChar, 8000, "TipoFiltro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Int, 0, "Ativo"));

                    dbCommand.Parameters["@Filtro"].Value = this.Filtro ?? "";
                    dbCommand.Parameters["@TipoFiltro"].Value = this.TipoFiltro ?? "";
                    dbCommand.Parameters["@Ativo"].Value = this.Ativo;

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

        public string GravaTransportador()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_TRANSPORTADOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Bit, 0, "Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao;
                    dbCommand.Parameters["@Ativo"].Value = this.Ativo;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            this.IDTransportador = Convert.ToInt32(row["IDTransportador"].ToString());
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public DataTable RetornaListaTransportadorFornecedor()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_TRANSPORTADOR_FORNECEDOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
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

        public string GravaTransportadorFornecedor()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_TRANSPORTADOR_FORNECEDOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Erro"].ToString();
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public string ExcluiTransportadorFornecedor()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_TRANSPORTADOR_FORNECEDOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Erro"].ToString();
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public DataTable RetornaListaTransportadorRegiao()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_TRANSPORTADOR_REGIAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRegiao", SqlDbType.Int, 0, "IDRegiao"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoRegiao", SqlDbType.VarChar, 8000, "CodigoRegiao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDRegiao"].Value = this.IDRegiao;
                    dbCommand.Parameters["@CodigoRegiao"].Value = this.CodigoRegiao;
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao;

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

        public string GravaTransportadorRegiao()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_TRANSPORTADOR_REGIAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoRegiao", SqlDbType.VarChar, 8000, "CodigoRegiao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@CodigoRegiao"].Value = this.CodigoRegiao;
                    dbCommand.Parameters["@Descricao"].Value = this.DescricaoRegiao;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Erro"].ToString();
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public string ExcluiTransportadorRegiao()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_TRANSPORTADOR_REGIAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRegiao", SqlDbType.Int, 0, "IDRegiao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDRegiao"].Value = this.IDRegiao;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Erro"].ToString();
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public DataTable RetornaListaTransportadorRegiaoMunicipio()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_TRANSPORTADOR_REGIAO_MUNICIPIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRegiao", SqlDbType.Int, 0, "IDRegiao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPais", SqlDbType.Int, 0, "IDPais"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEstado", SqlDbType.Int, 0, "IDEstado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMunicipio", SqlDbType.Int, 0, "IDMunicipio"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDRegiao"].Value = this.IDRegiao;
                    dbCommand.Parameters["@IDPais"].Value = this.IDPais;
                    dbCommand.Parameters["@IDEstado"].Value = this.IDEstado;
                    dbCommand.Parameters["@IDMunicipio"].Value = this.IDMunicipio;

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

        public string GravaTransportadorRegiaoMunicipio()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_TRANSPORTADOR_REGIAO_MUNICIPIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRegiao", SqlDbType.Int, 0, "IDRegiao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPais", SqlDbType.Int, 0, "IDPais"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEstado", SqlDbType.Int, 0, "IDEstado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMunicipio", SqlDbType.Int, 0, "IDMunicipio"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDRegiao"].Value = this.IDRegiao;
                    dbCommand.Parameters["@IDPais"].Value = this.IDPais;
                    dbCommand.Parameters["@IDEstado"].Value = this.IDEstado;
                    dbCommand.Parameters["@IDMunicipio"].Value = this.IDMunicipio;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Erro"].ToString();
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public string ExcluiTransportadorRegiaoMunicipio()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_TRANSPORTADOR_REGIAO_MUNICIPIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRegiao", SqlDbType.Int, 0, "IDRegiao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPais", SqlDbType.Int, 0, "IDPais"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEstado", SqlDbType.Int, 0, "IDEstado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMunicipio", SqlDbType.Int, 0, "IDMunicipio"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDRegiao"].Value = this.IDRegiao;
                    dbCommand.Parameters["@IDPais"].Value = this.IDPais;
                    dbCommand.Parameters["@IDEstado"].Value = this.IDEstado;
                    dbCommand.Parameters["@IDMunicipio"].Value = this.IDMunicipio;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Erro"].ToString();
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public DataTable RetornaListaTransportadorRegiaoParametros()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_TRANSPORTADOR_REGIAO_PARAMETROS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRegiao", SqlDbType.Int, 0, "IDRegiao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDParametro", SqlDbType.Int, 0, "IDParametro"));

                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 8000, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDRegiao"].Value = this.IDRegiao;
                    dbCommand.Parameters["@IDParametro"].Value = this.IDParametro;

                    dbCommand.Parameters["@Nome"].Value = this.Nome ?? "";
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao ?? "";

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

        public string GravaTransportadorRegiaoParametros()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_TRANSPORTADOR_REGIAO_PARAMETROS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRegiao", SqlDbType.Int, 0, "IDRegiao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDParametro", SqlDbType.Int, 0, "IDParametro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 8000, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorString", SqlDbType.VarChar, 8000, "ValorString"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorNumerico", SqlDbType.Decimal, 0, "ValorNumerico"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDRegiao"].Value = this.IDRegiao;
                    dbCommand.Parameters["@IDParametro"].Value = this.IDParametro;
                    dbCommand.Parameters["@Nome"].Value = this.Nome ?? "";
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao ?? "";
                    dbCommand.Parameters["@ValorString"].Value = this.ValorString ?? "";
                    dbCommand.Parameters["@ValorNumerico"].Value = this.ValorNumerico;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Erro"].ToString();
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public string ExcluiTransportadorRegiaoParametros()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_TRANSPORTADOR_REGIAO_PARAMETROS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRegiao", SqlDbType.Int, 0, "IDRegiao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDParametro", SqlDbType.Int, 0, "IDParametro"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDRegiao"].Value = this.IDRegiao;
                    dbCommand.Parameters["@IDParametro"].Value = this.IDParametro;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Erro"].ToString();
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public string AlteraTransportadorRegiaoParametros()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ALTERA_TRANSPORTADOR_REGIAO_PARAMETROS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRegiao", SqlDbType.Int, 0, "IDRegiao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDParametro", SqlDbType.Int, 0, "IDParametro"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorString", SqlDbType.VarChar, 8000, "ValorString"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorNumerico", SqlDbType.VarChar, 8000, "ValorNumerico"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDRegiao"].Value = this.IDRegiao;
                    dbCommand.Parameters["@IDParametro"].Value = this.IDParametro;
                    dbCommand.Parameters["@ValorString"].Value = this.ValorString ?? "";
                    dbCommand.Parameters["@ValorNumerico"].Value = this.ValorNumerico.ToString().Replace(",", ".");
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    string exec = objSQLUtilClass.MontarComandoExec(dbCommand);

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Erro"].ToString();
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public DataTable RetornaListaTransportadorParametros()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_TRANSPORTADOR_PARAMETROS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDParametro", SqlDbType.Int, 0, "IDParametro"));

                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 8000, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorString", SqlDbType.VarChar, 8000, "ValorString"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorNumerico", SqlDbType.Decimal, 0, "ValorNumerico"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDParametro"].Value = this.IDParametro;

                    dbCommand.Parameters["@Nome"].Value = this.Nome ?? "";
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao ?? "";
                    dbCommand.Parameters["@ValorString"].Value = this.ValorString ?? "";
                    dbCommand.Parameters["@ValorNumerico"].Value = this.ValorNumerico;

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

        public string GravaTransportadorParametros()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_TRANSPORTADOR_PARAMETROS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDParametro", SqlDbType.Int, 0, "IDParametro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 8000, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorString", SqlDbType.VarChar, 8000, "ValorString"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorNumerico", SqlDbType.Decimal, 0, "ValorNumerico"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDParametro"].Value = this.IDParametro;
                    dbCommand.Parameters["@Nome"].Value = this.Nome ?? "";
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao ?? "";
                    dbCommand.Parameters["@ValorString"].Value = this.ValorString ?? "";
                    dbCommand.Parameters["@ValorNumerico"].Value = this.ValorNumerico;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Erro"].ToString();
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public string ExcluiTransportadorParametros()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_TRANSPORTADOR_PARAMETROS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDParametro", SqlDbType.Int, 0, "IDParametro"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDParametro"].Value = this.IDParametro;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Erro"].ToString();
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public string AlteraTransportadorParametros()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ALTERA_TRANSPORTADOR_PARAMETROS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDParametro", SqlDbType.Int, 0, "IDParametro"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorString", SqlDbType.VarChar, 8000, "ValorString"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorNumerico", SqlDbType.Decimal, 0, "ValorNumerico"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioAlteracao", SqlDbType.Int, 0, "IDUsuarioAlteracao"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDParametro"].Value = this.IDParametro;
                    dbCommand.Parameters["@ValorString"].Value = this.ValorString ?? "";
                    dbCommand.Parameters["@ValorNumerico"].Value = this.ValorNumerico;
                    dbCommand.Parameters["@IDUsuarioAlteracao"].Value = this.IDUsuarioAlteracao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["Erro"].ToString();
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public DataTable RetornaListaTransportadorRegiaoMunicipio_Transportador()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_TRANSPORTADOR_REGIAO_MUNICIPIO_Transportador", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPais", SqlDbType.Int, 0, "IDPais"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEstado", SqlDbType.Int, 0, "IDEstado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMunicipio", SqlDbType.Int, 0, "IDMunicipio"));

                    dbCommand.Parameters["@IDPais"].Value = this.IDPais;
                    dbCommand.Parameters["@IDEstado"].Value = this.IDEstado;
                    dbCommand.Parameters["@IDMunicipio"].Value = this.IDMunicipio;

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

        public string SimulaFrete()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_LOGISTICA_SIMULA_FRETE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRegiao", SqlDbType.Int, 0, "IDRegiao"));
                    dbCommand.Parameters.Add(new SqlParameter("@PesoNota", SqlDbType.Decimal, 0, "PesoNota"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorNota", SqlDbType.Decimal, 0, "ValorNota"));

                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDRegiao"].Value = this.IDRegiao;
                    dbCommand.Parameters["@PesoNota"].Value = this.PesoNota;
                    dbCommand.Parameters["@ValorNota"].Value = this.ValorNota;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            ValorFrete = row["ValorFrete"].ToString();

                            PrevisaoEntrega = row["PrevisaoEntrega"].ToString();
                        }
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public DataTable RetornaFretes()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LOGISTICA_SIMULACOES_FRETE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPais", SqlDbType.Int, 0, "IDPais"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEstado", SqlDbType.Int, 0, "IDEstado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMunicipio", SqlDbType.Int, 0, "IDMunicipio"));
                    dbCommand.Parameters.Add(new SqlParameter("@PesoNota", SqlDbType.Decimal, 0, "PesoNota"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorNota", SqlDbType.Decimal, 0, "ValorNota"));

                    dbCommand.Parameters["@IDPais"].Value = this.IDPais;
                    dbCommand.Parameters["@IDEstado"].Value = this.IDEstado;
                    dbCommand.Parameters["@IDMunicipio"].Value = this.IDMunicipio;
                    dbCommand.Parameters["@PesoNota"].Value = this.PesoNota;
                    dbCommand.Parameters["@ValorNota"].Value = this.ValorNota;

                    string comando = objSQLUtilClass.MontarComandoExec(dbCommand);

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        return outputTable;
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return null;
        }
    }
}