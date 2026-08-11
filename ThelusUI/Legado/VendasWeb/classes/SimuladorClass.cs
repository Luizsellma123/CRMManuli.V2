using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class SimuladorClass : clsConexao
    {

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

        public DataTable Consulta_Produto()
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
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }
            return outputTable;

        }

        public string PreparaEmail()
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
                    retorno = "sucesso";
                }
            }
            catch (Exception ex)
            {
                retorno = "erro";
            }
            return retorno;

        }

        public void EnviaEmail()
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

            }

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

        public string Armazena_Pesquisa()
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
    }

}