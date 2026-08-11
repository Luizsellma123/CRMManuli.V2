using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace VendasWeb.GerencialVendas
{
    public class EntRelacionamentoClass : clsConexao
    {
        public int Codigo { get; set; }
        public string EntCod { get; set; }
        public string Descricao { get; set; }
        public string Data { get; set; }

        public string Inserir_Relacionamento()
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

                    dbCommand = new SqlCommand("user_sp_User_Tb_Ent_Relacionamento_Inserir", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.NVarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Data", SqlDbType.DateTime, 19, "Data"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@Descricao"].Value = Descricao;
                    if (Data == "")
                        dbCommand.Parameters["@Data"].Value = DBNull.Value;
                    else
                        dbCommand.Parameters["@Data"].Value = Data;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Relacionamento_Inserir. Contactar o Suporte!";
            }

            return Retorno;
        }


        public string Alterar_Relacionamento()
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

                    dbCommand = new SqlCommand("user_sp_User_Tb_Ent_Relacionamento_Alterar", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.Int, 0, "Codigo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.NVarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Data", SqlDbType.DateTime, 19, "Data"));

                    dbCommand.Parameters["@Codigo"].Value = Codigo;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@Descricao"].Value = Descricao;
                    if (Data == "")
                        dbCommand.Parameters["@Data"].Value = DBNull.Value;
                    else
                        dbCommand.Parameters["@Data"].Value = Data;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch
            {

                Retorno = "Erro na Funcao Alterar_Relacionamento. Contactar o Suporte!";
            }




            return Retorno;

        }

        public string Remove_Relacionamento()
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

                    dbCommand = new SqlCommand("user_sp_User_Tb_Ent_Relacionamento_Remove", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.Int, 0, "Codigo"));

                    dbCommand.Parameters["@Codigo"].Value = Codigo;
                   
                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();



                }
            }
            catch
            {

                Retorno = "Erro na Funcao Remove_Relacionamento. Contactar o Suporte!";
            }




            return Retorno;

        }

        public DataTable Consulta_Relacionamento_EntCod()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_User_Tb_Ent_Relacionamento_Consulta", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));


                dbCommand.Parameters["@EntCod"].Value = EntCod;



                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }

        }
        public string Relacionamento_Excluir_Todos()
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

                    dbCommand = new SqlCommand("User_sp_Tb_Ent_Relacionamento_Excluir_Todos", dbConnection);

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
                Retorno = "Erro na Funcao Relacionamento_Inserir. Contactar o Suporte!";
            }

            return Retorno;
        }

    }
}