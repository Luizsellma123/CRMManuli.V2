using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.GerencialVendas
{
    public class EntPerfilDeConsumoClass : clsConexao
    {
        public int Codigo { get; set; }
        public string EntCod { get; set; }
        public string Linha { get; set; }
        public double Quantidade { get; set; }
        public string Descricao { get; set; }

        public string Inserir_Perfil_Consumo()
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

                    dbCommand = new SqlCommand("User_sp_CRM_Perfil_Consumo_Cliente_Inserir", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Linha", SqlDbType.VarChar, 50, "Linha"));
                    dbCommand.Parameters.Add(new SqlParameter("@Quantidade", SqlDbType.Decimal, 0, "Quantidade"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@Linha"].Value = Linha;
                    dbCommand.Parameters["@Quantidade"].Value = Quantidade;
                    dbCommand.Parameters["@Descricao"].Value = Descricao;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Perfil_Consumo_Inserir. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Alterar_Perfil_Consumo()
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

                    dbCommand = new SqlCommand("User_sp_CRM_Perfil_Consumo_Cliente_Altera", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.Int, 0, "Codigo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Linha", SqlDbType.VarChar, 50, "Linha"));
                    dbCommand.Parameters.Add(new SqlParameter("@Quantidade", SqlDbType.Decimal, 0, "Quantidade"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));

                    dbCommand.Parameters["@Codigo"].Value = Codigo;
                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@Linha"].Value = Linha;
                    dbCommand.Parameters["@Quantidade"].Value = Quantidade;
                    dbCommand.Parameters["@Descricao"].Value = Descricao;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Alterar_Perfil_Consumo. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Remove_Perfil_Consumo()
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

                    dbCommand = new SqlCommand("User_sp_CRM_Perfil_Consumo_Cliente_Remove", dbConnection);

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

                Retorno = "Erro na Funcao Remove_PerfilDeConsumo. Contactar o Suporte!";
            }

            return Retorno;

        }

        public DataTable Consulta_Perfil_Consumo_EntCod()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("User_sp_CRM_Perfil_Consumo_Cliente_Consulta", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));

                dbCommand.Parameters["@EntCod"].Value = EntCod;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }

        }

        public string Perfil_Consumo_Excluir_Todos()
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

                    dbCommand = new SqlCommand("User_sp_CRM_Perfil_Consumo_Cliente_Excluir_Todos", dbConnection);

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
                Retorno = "Erro na Funcao Perfil_Consumo_Excluir_Todos. Contactar o Suporte!";
            }

            return Retorno;
        }
    }
}