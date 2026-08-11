using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;

namespace VendasWeb
{
    public class modulo : GerencialVendas.clsConexao
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Filtro { get; set; }

        public DataTable RetornaModulos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_MODULOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));

                    dbCommand.Parameters["@Filtro"].Value = this.Filtro;

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

        public string AdicionaModulo()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_INCLUI_MODULOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 0, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@VErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "VErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@Nome"].Value = this.Nome;

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

        public string ExcluiModulo()
        {
            string erro = "";

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_EXCLUI_MODULO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.Int, 0, "Codigo"));
                    dbCommand.Parameters.Add(new SqlParameter("@VErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "VErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@Codigo"].Value = this.Codigo;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@VErro"].Value;
                }

            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return erro;
        }

    }

}
