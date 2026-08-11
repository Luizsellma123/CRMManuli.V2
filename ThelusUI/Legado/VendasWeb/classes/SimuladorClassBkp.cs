using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class SimuladorClassBkp : clsConexao
    {
        #region Campos

        public string codempresar { get; set; }
        public string codempresa { get; set; }
        public string codproduto { get; set; }
        public string empresa { get; set; }
        public string estado { get; set; }
        public string produto { get; set; }
        public string produtoNome { get; set; }
        public string LocalFaturamento { get; set; }
        public string NivelVendedor { get; set; }
        public decimal ICMS { get; set; }
        public string NomeCliente { get; set; }
        public string alcada { get; set; }
        public string tipomaterial { get; set; }
        public decimal ValorICMS { get; set; }
        public string usucod { get; set; }
        public string observacao { get; set; }
        public decimal margem { get; set; }
        public decimal MargemContribuicao { get; set; }
        public decimal quantidade { get; set; }
        public string NovoCliente { get; set; }
        public DateTime DataSimulacao { get; set; }
        public string EntidadeBusca { get; set; }
        public string SearchEmpresa { get; set; }
        public string SearchIdsim { get; set; }
        public string SearchNomeCliente { get; set; }
        public string SearchSituacao { get; set; }
        public string SearchVendedor { get; set; }
        public string IdSimulacao { get; set; }
        public int PaginaSalva { get; set; }
        public string situacao { get; set; }
        public string NumeroSimulacao { get; set; }
        public string CodigoUsuario { get; set; }
        public string TipoVendedor { get; set; }
        public int IDUsuario { get; set; }
        public int IDClassificacaoComercial { get; set; }
        public int IDTipoFrete { get; set; }
        public int AVista { get; set; }
        public decimal Quantidade { get; set; }
        public int IDPais { get; set; }
        public int IDEstado { get; set; }
        public int IDMunicipio { get; set; }
        public decimal ValorFrete { get; set; }
        public int IDSimulacao { get; set; }
        public string CodigoEstadoSAP { get; set; }
        public string PrevisaoEntrega { get; set; }
        public int IDTransportador { get; set; }
        public int IDRegiao { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorComDesconto { get; set; }

        #endregion

        public DataTable Consulta_Produto(int controladoria = 0)
        {
            DataTable outputTable = new DataTable();
            this.Arredonda_codempresa();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_SIMULADOR_PRODUTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@empresa", SqlDbType.VarChar, 300, "empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@controladoria", SqlDbType.Int, 0, "controladoria"));

                    dbCommand.Parameters["@empresa"].Value = this.codempresar;
                    dbCommand.Parameters["@controladoria"].Value = controladoria;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);


                }
            }

            catch
            {

            }
            return outputTable;

        }

        public DataTable Consulta_Local()
        {
            DataTable outputTable = new DataTable();
            this.Arredonda_codempresa();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_SIMULADOR_FATURAMENTO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@empresa", SqlDbType.VarChar, 300, "empresa"));

                    dbCommand.Parameters["@empresa"].Value = this.codempresar;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                }
            }

            catch
            {

            }
            return outputTable;

        }

        public DataTable Simulacao()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_SIMULACAO_PRECO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.VarChar, 10, "Empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Estado", SqlDbType.VarChar, 30, "Estado"));
                    dbCommand.Parameters.Add(new SqlParameter("@Produto", SqlDbType.VarChar, 30, "Produto"));
                    dbCommand.Parameters.Add(new SqlParameter("@LocalFaturamento", SqlDbType.VarChar, 200, "LocalFaturamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@NivelVendedor", SqlDbType.VarChar, 30, "NivelVendedor"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrecoExIcm", SqlDbType.Decimal, 0, "PrecoExIcm"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDClassificacaoComercial", SqlDbType.Int, 0, "IDClassificacaoComercial"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoFrete", SqlDbType.Int, 0, "IDTipoFrete"));
                    dbCommand.Parameters.Add(new SqlParameter("@AVista", SqlDbType.Int, 0, "AVista"));

                    dbCommand.Parameters["@Empresa"].Value = this.codempresa;
                    dbCommand.Parameters["@Estado"].Value = this.estado;
                    dbCommand.Parameters["@Produto"].Value = this.produto;
                    dbCommand.Parameters["@LocalFaturamento"].Value = this.LocalFaturamento;
                    dbCommand.Parameters["@NivelVendedor"].Value = this.NivelVendedor;
                    dbCommand.Parameters["@PrecoExIcm"].Value = this.ICMS;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@IDClassificacaoComercial"].Value = this.IDClassificacaoComercial;
                    dbCommand.Parameters["@IDTipoFrete"].Value = this.IDTipoFrete;
                    dbCommand.Parameters["@AVista"].Value = this.AVista;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                }
            }
            catch (Exception ex)
            {

            }
            return outputTable;

        }

        public DataTable SimulacaoVendedor()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_SIMULACAO_CALCULOLIBERACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.VarChar, 10, "Empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Produto", SqlDbType.VarChar, 30, "Produto"));
                    dbCommand.Parameters.Add(new SqlParameter("@LocalFaturamento", SqlDbType.VarChar, 200, "LocalFaturamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@NivelVendedor", SqlDbType.VarChar, 30, "NivelVendedor"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrecoExIcm", SqlDbType.Decimal, 0, "PrecoExIcm"));
                    dbCommand.Parameters.Add(new SqlParameter("@Quantidade", SqlDbType.Decimal, 0, "Quantidade"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDClassificacaoComercial", SqlDbType.Int, 0, "IDClassificacaoComercial"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoFrete", SqlDbType.Int, 0, "IDTipoFrete"));
                    dbCommand.Parameters.Add(new SqlParameter("@AVista", SqlDbType.Int, 0, "AVista"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPais", SqlDbType.Int, 0, "IDPais"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEstado", SqlDbType.Int, 0, "IDEstado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMunicipio", SqlDbType.Int, 0, "IDMunicipio"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorFrete", SqlDbType.Decimal, 0, "ValorFrete"));

                    dbCommand.Parameters["@Empresa"].Value = this.codempresa;
                    dbCommand.Parameters["@Produto"].Value = this.produto;
                    dbCommand.Parameters["@LocalFaturamento"].Value = this.LocalFaturamento;
                    dbCommand.Parameters["@NivelVendedor"].Value = this.NivelVendedor;
                    dbCommand.Parameters["@PrecoExIcm"].Value = this.ICMS;
                    dbCommand.Parameters["@Quantidade"].Value = this.Quantidade;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@IDClassificacaoComercial"].Value = this.IDClassificacaoComercial;
                    dbCommand.Parameters["@IDTipoFrete"].Value = this.IDTipoFrete;
                    dbCommand.Parameters["@AVista"].Value = this.AVista;
                    dbCommand.Parameters["@IDPais"].Value = this.IDPais;
                    dbCommand.Parameters["@IDEstado"].Value = this.IDEstado;
                    dbCommand.Parameters["@IDMunicipio"].Value = this.IDMunicipio;
                    dbCommand.Parameters["@ValorFrete"].Value = this.ValorFrete;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public string SimulaPreco()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_SIMULACAO_PRECO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.VarChar, 10, "Empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Estado", SqlDbType.VarChar, 30, "Estado"));
                    dbCommand.Parameters.Add(new SqlParameter("@Produto", SqlDbType.VarChar, 30, "Produto"));
                    dbCommand.Parameters.Add(new SqlParameter("@LocalFaturamento", SqlDbType.VarChar, 200, "LocalFaturamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@NivelVendedor", SqlDbType.VarChar, 30, "NivelVendedor"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrecoExIcm", SqlDbType.Decimal, 0, "PrecoExIcm"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDClassificacaoComercial", SqlDbType.Int, 0, "IDClassificacaoComercial"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoFrete", SqlDbType.Int, 0, "IDTipoFrete"));
                    dbCommand.Parameters.Add(new SqlParameter("@AVista", SqlDbType.Int, 0, "AVista"));

                    dbCommand.Parameters["@Empresa"].Value = this.codempresa;
                    dbCommand.Parameters["@Estado"].Value = this.estado;
                    dbCommand.Parameters["@Produto"].Value = this.produto;
                    dbCommand.Parameters["@LocalFaturamento"].Value = this.LocalFaturamento;
                    dbCommand.Parameters["@NivelVendedor"].Value = this.NivelVendedor;
                    dbCommand.Parameters["@PrecoExIcm"].Value = this.ICMS;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@IDClassificacaoComercial"].Value = this.IDClassificacaoComercial;
                    dbCommand.Parameters["@IDTipoFrete"].Value = this.IDTipoFrete;
                    dbCommand.Parameters["@AVista"].Value = this.AVista;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            alcada = row["Aprovacao"].ToString();
                            tipomaterial = row["TipoMaterial"].ToString();
                            ValorICMS = Convert.ToDecimal(row["ICMS"].ToString());
                            margem = Convert.ToDecimal(row["MargemSimulacao"].ToString());

                            //Executando cortes na Label produto para obter apenas o código
                            string produto = row["NomeProduto"].ToString();
                            string[] codproduto = produto.Split('-');
                            this.codproduto = codproduto[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }

        public string Arredonda_codempresa()
        {
            try
            {
                codempresar = codempresa.Substring(0, codempresa.IndexOf("."));
            }
            catch
            {
                codempresar = codempresa;
            }

            return (codempresar);
        }

        public decimal Transfere_ICMS(string valor)
        {
            decimal retorno = 0;
            try
            {
                valor = valor.Replace(".", ",");
                retorno = Convert.ToDecimal(valor);

            }

            catch
            {

            }

            return (retorno);

        }

        public DataTable Consulta_Entidade()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_BUSCA_ENTIDADE_SIMULADOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@busca", SqlDbType.VarChar, 3000, "busca"));

                    dbCommand.Parameters["@busca"].Value = this.EntidadeBusca;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                }


            }

            catch (Exception ex)
            {

            }
            return outputTable;
        }

        public DataTable Consulta_Entidade_Vendedor()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_BUSCA_ENTIDADE_SIMULADOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@busca", SqlDbType.VarChar, 3000, "busca"));

                    dbCommand.Parameters["@busca"].Value = this.EntidadeBusca;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                }


            }

            catch (Exception ex)
            {

            }
            return outputTable;
        }

        public DataTable Pesquisa_Simulacao(string usucod)
        {

            DataTable outputTable = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_BUSCA_SIMULACOES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@empresa", SqlDbType.VarChar, 30, "empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@idsim", SqlDbType.VarChar, 300, "idsim"));
                    dbCommand.Parameters.Add(new SqlParameter("@nomecliente", SqlDbType.VarChar, 300, "nomecliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@situacao", SqlDbType.VarChar, 300, "situacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@usucod", SqlDbType.VarChar, 300, "usucod"));

                    dbCommand.Parameters["@empresa"].Value = this.SearchEmpresa;
                    dbCommand.Parameters["@idsim"].Value = this.SearchIdsim;
                    dbCommand.Parameters["@nomecliente"].Value = this.SearchNomeCliente;
                    dbCommand.Parameters["@situacao"].Value = this.SearchSituacao;
                    dbCommand.Parameters["@usucod"].Value = usucod;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                }
            }
            catch (Exception ex)
            {

            }
            return outputTable;

        }

        public string RecuperaSimulacao()
        {
            string retorno;

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_ACESSA_SIMULACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IdSimulacao", SqlDbType.Int, 0, "IdSimulacao"));

                    dbCommand.Parameters["@IdSimulacao"].Value = this.IdSimulacao;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            empresa = row["EmpCod"].ToString();
                            estado = row["UF"].ToString();
                            produto = row["ProdCodEstr"].ToString();
                            produtoNome = row["ProdNome"].ToString();
                            quantidade = Convert.ToDecimal(row["Quantidade"].ToString());
                            NivelVendedor = row["NivelVendedor"].ToString();
                            ValorICMS = Convert.ToDecimal(row["ValorUnitario"]);
                            LocalFaturamento = row["Tabela"].ToString();
                            NomeCliente = row["NomeCliente"].ToString();
                            observacao = row["Observacao"].ToString();
                            situacao = row["Situacao"].ToString();
                            NovoCliente = row["NovoCliente"].ToString();
                            MargemContribuicao = Convert.ToDecimal(row["MargemContribuicao"].ToString());
                            IDClassificacaoComercial = Convert.ToInt32(row["IDClassificacaoComercial"].ToString());
                            IDTipoFrete = Convert.ToInt32(row["IDTipoFrete"].ToString());
                            AVista = Convert.ToInt32(row["AVista"].ToString());

                            IDPais = Convert.ToInt32(row["IDPais"]);
                            IDEstado = Convert.ToInt32(row["IDEstado"]);
                            IDMunicipio = Convert.ToInt32(row["IDMunicipio"]);
                            IDTransportador = Convert.ToInt32(row["IDTransportador"]);
                            IDRegiao = Convert.ToInt32(row["IDRegiao"]);
                            ValorFrete = Convert.ToDecimal(row["ValorFrete"]);
                            PrevisaoEntrega = row["PrevisaoEntrega"].ToString();

                            Desconto = Convert.ToDecimal(row["Desconto"]);
                            ValorComDesconto = Convert.ToDecimal(row["ValorComDesconto"]);
                        }
                    }

                    retorno = "sucesso";
                }
            }
            catch (Exception ex)
            {
                retorno = "erro";
            }
            return retorno;

        }

        public DataTable Pesquisa_Simulacao_Control()
        {
            DataTable outputTable = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_BUSCA_SIM_CONTROL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@empresa", SqlDbType.VarChar, 30, "empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@idsim", SqlDbType.VarChar, 300, "idsim"));
                    dbCommand.Parameters.Add(new SqlParameter("@nomecliente", SqlDbType.VarChar, 300, "nomecliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@situacao", SqlDbType.VarChar, 300, "situacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vendedor", SqlDbType.VarChar, 300, "vendedor"));

                    dbCommand.Parameters["@empresa"].Value = this.SearchEmpresa;
                    dbCommand.Parameters["@idsim"].Value = this.SearchIdsim;
                    dbCommand.Parameters["@nomecliente"].Value = this.SearchNomeCliente;
                    dbCommand.Parameters["@situacao"].Value = this.SearchSituacao;
                    dbCommand.Parameters["@vendedor"].Value = this.SearchVendedor;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                }
            }
            catch (Exception ex)
            {

            }
            return outputTable;
        }

        public string Atualiza_Simulacao()
        {
            string retorno;

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_ANALISA_SIMULACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@historico", SqlDbType.VarChar, 8000, "historico"));
                    dbCommand.Parameters.Add(new SqlParameter("@situacao", SqlDbType.VarChar, 30, "situacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@idsim", SqlDbType.Int, 0, "idsim"));
                    dbCommand.Parameters.Add(new SqlParameter("@usucod", SqlDbType.VarChar, 500, "usucod"));

                    dbCommand.Parameters["@historico"].Value = this.observacao;
                    dbCommand.Parameters["@situacao"].Value = this.situacao;
                    dbCommand.Parameters["@idsim"].Value = this.IdSimulacao;
                    dbCommand.Parameters["@usucod"].Value = this.usucod;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    retorno = "sucesso";
                }
            }
            catch (Exception ex)
            {
                retorno = "erro";
            }
            return retorno;
        }

        public void RecuperaNivelVendedor()
        {
            string retorno;

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_NIVEL_VENDEDOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            TipoVendedor = row["DescricaoTipo"].ToString();
                        }
                    }
                    retorno = "sucesso";
                }
            }
            catch (Exception ex)
            {
                retorno = ex.ToString();
            }

        }

        public string SalvaSimulacao()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_USER_TB_SIMULADOR_SIMULACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDSimulacao", SqlDbType.Int, 0, "IDSimulacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 30, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@ProdCodEstr", SqlDbType.VarChar, 100, "ProdCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@UF", SqlDbType.VarChar, 30, "UF"));
                    dbCommand.Parameters.Add(new SqlParameter("@NivelVendedor", SqlDbType.VarChar, 50, "NivelVendedor"));
                    dbCommand.Parameters.Add(new SqlParameter("@Tabela", SqlDbType.VarChar, 300, "Tabela"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorUnitario", SqlDbType.Decimal, 0, "ValorUnitario"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeCliente", SqlDbType.VarChar, 300, "NomeCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@Observacao", SqlDbType.VarChar, -1, "Observacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Alcada", SqlDbType.VarChar, 50, "Alcada"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoMaterial", SqlDbType.VarChar, 300, "TipoMaterial"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorICMS", SqlDbType.Decimal, 0, "ValorICMS"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 300, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@MargemContribuicao", SqlDbType.Decimal, 0, "MargemContribuicao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Quantidade", SqlDbType.Decimal, 0, "Quantidade"));
                    dbCommand.Parameters.Add(new SqlParameter("@NovoCliente", SqlDbType.VarChar, -1, "NovoCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@Situacao", SqlDbType.VarChar, 30, "Situacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataSimulacao", SqlDbType.DateTime, 0, "DataSimulacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDClassificacaoComercial", SqlDbType.Int, 0, "IDClassificacaoComercial"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoFrete", SqlDbType.Int, 0, "IDTipoFrete"));
                    dbCommand.Parameters.Add(new SqlParameter("@AVista", SqlDbType.Int, 0, "AVista"));

                    dbCommand.Parameters.Add(new SqlParameter("@IDPais", SqlDbType.Int, 0, "IDPais"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEstado", SqlDbType.Int, 0, "IDEstado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMunicipio", SqlDbType.Int, 0, "IDMunicipio"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTransportador", SqlDbType.Int, 0, "IDTransportador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDRegiao", SqlDbType.Int, 0, "IDRegiao"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorFrete", SqlDbType.Decimal, 0, "ValorFrete"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrevisaoEntrega", SqlDbType.VarChar, 20, "PrevisaoEntrega"));

                    dbCommand.Parameters.Add(new SqlParameter("@Desconto", SqlDbType.Decimal, 0, "Desconto"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorComDesconto", SqlDbType.Decimal, 0, "ValorComDesconto"));

                    dbCommand.Parameters["@IDSimulacao"].Value = this.IDSimulacao;
                    dbCommand.Parameters["@EmpCod"].Value = this.codempresa ?? "";
                    dbCommand.Parameters["@ProdCodEstr"].Value = this.produto ?? "";
                    dbCommand.Parameters["@UF"].Value = this.CodigoEstadoSAP;
                    dbCommand.Parameters["@NivelVendedor"].Value = this.NivelVendedor ?? "";
                    dbCommand.Parameters["@Tabela"].Value = this.LocalFaturamento;
                    dbCommand.Parameters["@ValorUnitario"].Value = this.ICMS;
                    dbCommand.Parameters["@NomeCliente"].Value = this.NomeCliente ?? "";
                    dbCommand.Parameters["@Observacao"].Value = this.observacao ?? "";
                    dbCommand.Parameters["@Alcada"].Value = this.alcada ?? "";
                    dbCommand.Parameters["@TipoMaterial"].Value = this.tipomaterial ?? "";
                    dbCommand.Parameters["@ValorICMS"].Value = this.ValorICMS;
                    dbCommand.Parameters["@UsuCod"].Value = this.usucod ?? "";
                    dbCommand.Parameters["@MargemContribuicao"].Value = this.margem;
                    dbCommand.Parameters["@Quantidade"].Value = this.Quantidade;
                    dbCommand.Parameters["@NovoCliente"].Value = this.NovoCliente ?? "";
                    dbCommand.Parameters["@Situacao"].Value = "Pendente";
                    dbCommand.Parameters["@DataSimulacao"].Value = this.DataSimulacao;
                    dbCommand.Parameters["@IDClassificacaoComercial"].Value = this.IDClassificacaoComercial;
                    dbCommand.Parameters["@IDTipoFrete"].Value = this.IDTipoFrete;
                    dbCommand.Parameters["@AVista"].Value = this.AVista;

                    dbCommand.Parameters["@IDPais"].Value = this.IDPais;
                    dbCommand.Parameters["@IDEstado"].Value = this.IDEstado;
                    dbCommand.Parameters["@IDMunicipio"].Value = this.IDMunicipio;
                    dbCommand.Parameters["@IDTransportador"].Value = this.IDTransportador;
                    dbCommand.Parameters["@IDRegiao"].Value = this.IDRegiao;
                    dbCommand.Parameters["@ValorFrete"].Value = this.ValorFrete;
                    dbCommand.Parameters["@PrevisaoEntrega"].Value = this.PrevisaoEntrega ?? "";

                    dbCommand.Parameters["@Desconto"].Value = this.Desconto;
                    dbCommand.Parameters["@ValorComDesconto"].Value = this.ValorComDesconto;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            IDSimulacao = Convert.ToInt32(row["IDSimulacao"]);
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

        public string SalvaSimulacao_old()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_SALVA_SIMULACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@empresa", SqlDbType.VarChar, 30, "empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@codprod", SqlDbType.VarChar, 100, "codprod"));
                    dbCommand.Parameters.Add(new SqlParameter("@uf", SqlDbType.VarChar, 30, "uf"));
                    dbCommand.Parameters.Add(new SqlParameter("@nivelvend", SqlDbType.VarChar, 50, "nivelvend"));
                    dbCommand.Parameters.Add(new SqlParameter("@tabela", SqlDbType.VarChar, 300, "tabela"));
                    dbCommand.Parameters.Add(new SqlParameter("@cliente", SqlDbType.VarChar, 300, "cliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@observacao", SqlDbType.VarChar, 8000, "observacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@alcada", SqlDbType.VarChar, 50, "alcada"));
                    dbCommand.Parameters.Add(new SqlParameter("@tipomaterial", SqlDbType.VarChar, 300, "tipomaterial"));
                    dbCommand.Parameters.Add(new SqlParameter("@valoricms", SqlDbType.Decimal, 0, "valoricms"));
                    dbCommand.Parameters.Add(new SqlParameter("@usucod", SqlDbType.VarChar, 300, "usucod"));
                    dbCommand.Parameters.Add(new SqlParameter("@valorfinal", SqlDbType.Decimal, 0, "valorfinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@margem", SqlDbType.Decimal, 0, "margem"));
                    dbCommand.Parameters.Add(new SqlParameter("@quantidade", SqlDbType.Decimal, 0, "quantidade"));
                    dbCommand.Parameters.Add(new SqlParameter("@NovoCliente", SqlDbType.VarChar, 0, "NovoCliente"));

                    dbCommand.Parameters.Add(new SqlParameter("@IDClassificacaoComercial", SqlDbType.Int, 0, "IDClassificacaoComercial"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoFrete", SqlDbType.Int, 0, "IDTipoFrete"));
                    dbCommand.Parameters.Add(new SqlParameter("@AVista", SqlDbType.Int, 0, "AVista"));

                    dbCommand.Parameters["@empresa"].Value = this.codempresa;
                    dbCommand.Parameters["@codprod"].Value = this.codproduto;
                    dbCommand.Parameters["@uf"].Value = this.estado;
                    dbCommand.Parameters["@nivelvend"].Value = this.NivelVendedor;
                    dbCommand.Parameters["@tabela"].Value = this.LocalFaturamento;
                    dbCommand.Parameters["@cliente"].Value = this.NomeCliente;
                    dbCommand.Parameters["@observacao"].Value = this.observacao;
                    dbCommand.Parameters["@alcada"].Value = this.alcada;
                    dbCommand.Parameters["@tipomaterial"].Value = this.tipomaterial;
                    dbCommand.Parameters["@valoricms"].Value = this.ValorICMS;
                    dbCommand.Parameters["@usucod"].Value = this.usucod;
                    dbCommand.Parameters["@valorfinal"].Value = this.ICMS; //Atributo era chamado de ICMS por ser o unico
                                                                           //valor atribuido pelo usuario 
                                                                           //na primeira versão da pagina
                    dbCommand.Parameters["@margem"].Value = this.margem;
                    dbCommand.Parameters["@quantidade"].Value = this.quantidade;
                    dbCommand.Parameters["@NovoCliente"].Value = this.NovoCliente;

                    dbCommand.Parameters["@IDClassificacaoComercial"].Value = this.IDClassificacaoComercial;
                    dbCommand.Parameters["@IDTipoFrete"].Value = this.IDTipoFrete;
                    dbCommand.Parameters["@AVista"].Value = this.AVista;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            NumeroSimulacao = row["IDSimulacao"].ToString();
                            IdSimulacao = row["IDSimulacao"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

            return "";
        }
    }
}