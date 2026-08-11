using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.GerencialVendas
{
    public class AgendaClass : clsConexao
    {
        public int Codigo { get; set; }
        public string Data { get; set; }
        public string UsuCod { get; set; }
        public string Historico { get; set; }
        public string EntCod { get; set; }

        public string Agenda_Inserir()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    //Chama procedure para buscar número do pedido
                    SqlCommand dbCommand = new SqlCommand("user_sp_Agenda_Inserir", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.Int, 0, "Codigo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Data", SqlDbType.DateTime, 19, "Data"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 31, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Historico", SqlDbType.VarChar, 8000, "Historico"));

                    dbCommand.Parameters["@Codigo"].Value = Codigo;
                    dbCommand.Parameters["@Data"].Value = Data;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@Historico"].Value = Historico;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();
                }
                catch
                {
                    erro = "Erro ao inserir agenda!";
                }
            }

            return erro;
        }

        public DataTable Agenda_Listar()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("user_sp_Agenda_Listar", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;

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
    }
}