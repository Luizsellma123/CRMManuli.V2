using System;
using System.Data.SqlClient;
using System.Data;

namespace VendasWeb
{
    public class ParametroGeral : GerencialVendas.clsConexao
    {
        public int IDEmpresa { get; set; }
        public string NomeEmpresa { get; set; }
        public int IDParametro { get; set; }
        public string NomeParametro { get; set; }
        public int IDModulo { get; set; }
        public string NomeModulo { get; set; }
        public string DescricaoParametro { get; set; }
        public string ValorTexto { get; set; }
        public decimal ValorNumerico { get; set; }
        public string Filtro { get; set; }
        public string Operacao { get; set; }

        public ParametroGeral()
        {
        }

        public ParametroGeral(string NomeParametro)
        {
            this.NomeParametro = NomeParametro;
        }

        public DataTable RetornaListaParametrosGerais()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_PARAMETROS_GERAIS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));

                    dbCommand.Parameters["@Filtro"].Value = this.Filtro;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;

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

        public string AdicionaParametroGeral()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_GRAVA_PARAMETROS_GERAIS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDParametro", SqlDbType.Int, 0, "IDParametro"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeParametro", SqlDbType.VarChar, 8000, "NomeParametro"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDModulo", SqlDbType.Int, 0, "IDModulo"));
                    dbCommand.Parameters.Add(new SqlParameter("@DescricaoParametro", SqlDbType.VarChar, 8000, "DescricaoParametro"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorTexto", SqlDbType.VarChar, 8000, "ValorTexto"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorNumerico", SqlDbType.Decimal, 0, "ValorNumerico"));
                    dbCommand.Parameters.Add(new SqlParameter("@VErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "VErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDParametro"].Value = this.IDParametro;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@NomeParametro"].Value = this.NomeParametro;
                    dbCommand.Parameters["@IDModulo"].Value = this.IDModulo;
                    dbCommand.Parameters["@DescricaoParametro"].Value = this.DescricaoParametro;
                    dbCommand.Parameters["@ValorTexto"].Value = this.ValorTexto;
                    dbCommand.Parameters["@ValorNumerico"].Value = this.ValorNumerico;
                    dbCommand.Parameters["@VErro"].Value = "";

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@VErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }

                return erro;
            }

        }

        public string RetornaValorStringParametro(string Parametro)
        {
            DataTable outputTable = new DataTable();

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

                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {
                        return row["ValorString"].ToString();
                    }
                }
            }

            return "";
        }

        public int RetornaValorNumericoParametro(string Parametro)
        {
            DataTable outputTable = new DataTable();

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

                string valorNumerico = "";

                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {
                        valorNumerico = row["ValorNumerico"].ToString();
                        break;
                    }

                    valorNumerico = valorNumerico.Substring(0, valorNumerico.LastIndexOf(","));
                }

                return Convert.ToInt32(valorNumerico);
            }
        }

    }
}