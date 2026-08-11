using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.GerencialVendas
{
    public class BDClass : clsConexao
    {
        public string strSql { get; set; }
        public string Metodo { get; set; }

        public DataTable Executa_DataTable()
        {
            DataTable outputTable = new DataTable();
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand(strSql, dbConnection))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        //Deixa o Timeout da consulta com cerca de 4 minutos
                        dbCommand.CommandTimeout = 340;

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            outputTable.Load(dataReader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro no método " + Metodo);
                }
                return outputTable;
            }
        }

        public String Executa_DataTable_String()
        {
            string Campo = "";
            DataTable outputTable = new DataTable();
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand(strSql, dbConnection))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        //Deixa o Timeout da consulta com cerca de 4 minutos
                        dbCommand.CommandTimeout = 340;

                        Campo = Convert.ToString(dbCommand.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro no método " + Metodo);
                }

                return Campo;
            }
        }
    }
}