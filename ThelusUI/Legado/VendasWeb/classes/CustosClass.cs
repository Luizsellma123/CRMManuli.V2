using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class CustosClass : clsConexao
    {
        public string FiltroProduto { get; set; }
        public string Operacao { get; set; }

        /******DADOS PARA MANUTENÇÂO*******/
        public int Empresa { get; set; }
        public int IDCusto { get; set; }
        public DataTable Dados { get; set; }
        public string CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public string TipoMaterial { get; set; }
        public string Comprimento { get; set; }
        public decimal Largura { get; set; }
        public decimal FC { get; set; }
        public decimal FCConvertido { get; set; }
        public decimal Custo { get; set; }
        public decimal PercentualMargem { get; set; }
        public decimal PercentualPrazoProducao { get; set; }
        public int PrazoProducao { get; set; }
        public decimal DISTRIBUIDOR { get; set; }
        public decimal INDUSTRIA { get; set; }
        public decimal REVENDA { get; set; }

        public string gravaDadosPrincipais()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_DADOS_CUSTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.Int, 0, "Empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Custos", SqlDbType.Structured, 0, "Custos"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@Empresa"].Value = this.Empresa;
                    dbCommand.Parameters["@Custos"].Value = this.Dados;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao dos custos.";
                }
            }

            return erro;
        }

        public string gravaClassificacaoComercial()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_DADOS_CUSTOS_CLASSIFICACAO_COMERCIAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.Int, 0, "Empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Custos", SqlDbType.Structured, 0, "Custos"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@Empresa"].Value = this.Empresa;
                    dbCommand.Parameters["@Custos"].Value = this.Dados;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao dos custos.";
                }
            }

            return erro;
        }

        public DataTable CarregaCustos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CUSTOS_PRODUTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@ConsultaProduto", SqlDbType.VarChar, 8000, "ConsultaProduto"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.Empresa;
                    dbCommand.Parameters["@ConsultaProduto"].Value = this.FiltroProduto;

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

        public DataTable CarregaTiposMateriais()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_TIPO_MATERIAL_SIMULADOR", dbConnection);

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

        public DataTable CarregaDadosPrincipaisMaterial()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PRODUTO_CUSTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoProduto", SqlDbType.VarChar, 8000, "CodigoProduto"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.Empresa;
                    dbCommand.Parameters["@CodigoProduto"].Value = this.CodigoProduto;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.IDCusto = Convert.ToInt32(row["IDCusto"]);
                                this.CodigoProduto = Convert.ToString(row["CodigoProduto"]);
                                this.DescricaoProduto = Convert.ToString(row["NomeProduto"]);
                                this.Comprimento = Convert.ToString(row["Comprimento"]);
                                this.Largura = Convert.ToDecimal(row["Largura"]);
                                this.FC = Convert.ToDecimal(row["FC"]);
                                this.FCConvertido = Convert.ToDecimal(row["FCConvertido"]);
                                this.Custo = Convert.ToDecimal(row["Custo"]);
                                this.TipoMaterial = Convert.ToString(row["Material"]);
                                this.PercentualMargem = Convert.ToDecimal(row["Percentual"]);
                                this.PrazoProducao = Convert.ToInt32(row["PrazoProducao"]);

                                switch (row["ClassificacaoComercial"].ToString())
                                {
                                    case "DISTRIBUIDOR":
                                        this.DISTRIBUIDOR = Convert.ToDecimal(row["Margem"]);
                                        break;

                                    case "INDÚSTRIA":
                                        this.INDUSTRIA = Convert.ToDecimal(row["Margem"]);
                                        break;

                                    case "REVENDA":
                                        this.REVENDA = Convert.ToDecimal(row["Margem"]);
                                        break;
                                }


                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;

        }

        public string GravaDadosProdutoCusto()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_PRODUTO_CUSTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.Int, 0, "Empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoProduto", SqlDbType.VarChar, 8000, "CodigoProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeProduto", SqlDbType.VarChar, 8000, "NomeProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@Comprimento", SqlDbType.VarChar, 8000, "Comprimento"));
                    dbCommand.Parameters.Add(new SqlParameter("@Largura", SqlDbType.Decimal, 0, "Largura"));
                    dbCommand.Parameters.Add(new SqlParameter("@FC", SqlDbType.Decimal, 0, "FC"));
                    dbCommand.Parameters.Add(new SqlParameter("@FCConvertido", SqlDbType.Decimal, 0, "@FCConvertido"));
                    dbCommand.Parameters.Add(new SqlParameter("@Custo", SqlDbType.Decimal, 0, "@Custo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Material", SqlDbType.VarChar, 8000, "@Material"));
                    dbCommand.Parameters.Add(new SqlParameter("@Percentual", SqlDbType.Decimal, 0, "@Percentual"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoProducao", SqlDbType.Int, 0, "@PrazoProducao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters.Add(new SqlParameter("@DISTRIBUIDOR", SqlDbType.Decimal, 0, "@DISTRIBUIDOR"));
                    dbCommand.Parameters.Add(new SqlParameter("@INDUSTRIA", SqlDbType.Decimal, 0, "@INDUSTRIA"));
                    dbCommand.Parameters.Add(new SqlParameter("@REVENDA", SqlDbType.Decimal, 0, "@REVENDA"));

                    dbCommand.Parameters["@Empresa"].Value = this.Empresa;
                    dbCommand.Parameters["@CodigoProduto"].Value = this.CodigoProduto;
                    dbCommand.Parameters["@NomeProduto"].Value = this.DescricaoProduto;
                    dbCommand.Parameters["@Comprimento"].Value = this.Comprimento;
                    dbCommand.Parameters["@Largura"].Value = this.Largura;
                    dbCommand.Parameters["@FC"].Value = this.FC;
                    dbCommand.Parameters["@FCConvertido"].Value = this.FCConvertido;
                    dbCommand.Parameters["@Custo"].Value = this.Custo;
                    dbCommand.Parameters["@Material"].Value = this.TipoMaterial;
                    dbCommand.Parameters["@Percentual"].Value = this.PercentualMargem;
                    dbCommand.Parameters["@PrazoProducao"].Value = this.PrazoProducao;

                    dbCommand.Parameters["@DISTRIBUIDOR"].Value = this.DISTRIBUIDOR;
                    dbCommand.Parameters["@INDUSTRIA"].Value = this.INDUSTRIA;
                    dbCommand.Parameters["@REVENDA"].Value = this.REVENDA;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao dos custos.";
                }
            }

            return erro;
        }

        public string ExcluiDadosProdutoCusto()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_PRODUTO_CUSTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.Int, 0, "Empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoProduto", SqlDbType.VarChar, 8000, "CodigoProduto"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@Empresa"].Value = this.Empresa;
                    dbCommand.Parameters["@CodigoProduto"].Value = this.CodigoProduto;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na exclusão dos custos.";
                }
            }

            return erro;
        }

    }

}