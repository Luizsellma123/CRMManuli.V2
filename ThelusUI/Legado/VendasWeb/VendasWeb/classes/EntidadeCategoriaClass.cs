using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace VendasWeb.GerencialVendas
{
    public class EntidadeCategoriaClass : clsConexao
    {
        public int Codigo { get; set; }
        public string EntCod { get; set; }
        public string CategCodEstr { get; set; }
        public string CategNome { get; set; }
        public string Categoria { get; set; }

        public string Incluir_Categoria()
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

                    dbCommand = new SqlCommand("user_sp_Insere_Ent_Categoria", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@CategCodEstr", SqlDbType.VarChar, 100, "CategCodEstr"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@CategCodEstr"].Value = CategCodEstr;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Incluir_Categoria. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string Excluir_Categoria()
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

                    dbCommand = new SqlCommand("user_sp_Exclui_Ent_Categoria", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));                    
                    dbCommand.Parameters.Add(new SqlParameter("@CategCodEstr", SqlDbType.VarChar, 100, "CategCodEstr"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@CategCodEstr"].Value = CategCodEstr;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Excluir_Categoria. Contactar o Suporte!";
            }

            return Retorno;
        }


        public DataTable Consulta_Categora()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_Consulta_Categoria", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));


                dbCommand.Parameters["@EntCod"].Value = EntCod;



                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }
        }

        public string Excluir_Categoria_Todas()
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

                    dbCommand = new SqlCommand("user_sp_Exclui_Categoria_Todas", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Excluir_Categoria_Todas. Contactar o Suporte!";
            }

            return Retorno;
        }

    }
}