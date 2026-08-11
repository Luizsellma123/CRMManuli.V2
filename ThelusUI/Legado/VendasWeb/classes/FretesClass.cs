using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class FretesClass : clsConexao
    {

        public string endereco { get; set; } 
        public string nome { get; set; }
        public string padrao { get; set; }
        public string empcod { get; set; }
        public string cidcod { get; set; }
        public decimal percentual { get; set; }
        public bool carregado { get; set; }
        public DataTable tabela { get; set; }

        public DataTable Consulta_Empresa(string usucod, string codempresa)
        {
            DataTable outputTable = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_FRETE_EMPRESAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@codempresa", SqlDbType.VarChar, 20, "codempresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@usucod", SqlDbType.VarChar, 30, "usucod"));

                    dbCommand.Parameters["@usucod"].Value = usucod;
                    dbCommand.Parameters["@codempresa"].Value = codempresa;

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

        public string Salva_Cenario_Cidade()
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

                    dbCommand = new SqlCommand("USER_SP_SALVA_CENARIO_CIDADE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@nomeCenario", SqlDbType.VarChar, 50, "nomeCenario"));

                    dbCommand.Parameters["@nomeCenario"].Value = this.nome;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                    retorno = "sucesso";
                }
            }
            catch (Exception ex)
            {
                retorno = "erro";
            }
            return retorno;

        }

        public string Salva_Cenario_Tabela_Cidade()
        {
            string retorno = "";
            int i = 0;

            DataTable outputTable = new DataTable();

            foreach (DataRow row in this.tabela.Rows)
            {
                try
                {
                    this.empcod = tabela.Rows[i]["Empresa"].ToString(); 
                    this.cidcod = tabela.Rows[i]["Cidade"].ToString();
                    this.percentual = Convert.ToDecimal(tabela.Rows[i]["ValorFrete"].ToString());

                    if(this.empcod == "" || this.cidcod == "")
                    {
                        throw new System.ArgumentException("Valor não pode ser nulo");
                    }

                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand();

                        dbCommand = new SqlCommand("USER_SP_SALVA_CENARIO_CIDADE_TAB", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@empcod", SqlDbType.VarChar, 30, "empcod"));
                        dbCommand.Parameters.Add(new SqlParameter("@cidcod", SqlDbType.VarChar, 60, "cidcod"));
                        dbCommand.Parameters.Add(new SqlParameter("@percentual", SqlDbType.Decimal, 1, "percentual"));

                        dbCommand.Parameters["@empcod"].Value = this.empcod;
                        dbCommand.Parameters["@cidcod"].Value = this.cidcod;
                        dbCommand.Parameters["@percentual"].Value = this.percentual;

                        SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                        SqlDataReader dataReader = dbCommand.ExecuteReader();
                        outputTable.Load(dataReader);

                        retorno = "sucesso";
                    }
                }
                catch (Exception ex)
                {
                    Apaga_Cenario_Cidade();
                    retorno = "erro";
                    break;
                }
                //Adicionando +1 ao contador para a próxima linha
                i++;
            }
            return retorno;

        }

        private string Apaga_Cenario_Cidade()
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

                    dbCommand = new SqlCommand("USER_SP_EXCLUI_CENARIO_CIDADE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

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

        public string Define_Padrao_Cidade()
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

                    dbCommand = new SqlCommand("USER_SP_DEFINE_CENARIO_PADRAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

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

        public DataTable CarregaFreteIncoterms()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_FRETE_INCOTERMS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    //dbCommand.Parameters.Add(new SqlParameter("@empcod", SqlDbType.VarChar, 30, "empcod"));

                    //dbCommand.Parameters["@empcod"].Value = this.empcod;

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
    }

}