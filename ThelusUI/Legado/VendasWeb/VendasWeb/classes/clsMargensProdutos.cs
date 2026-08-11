using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes.GerencialVendas
{
    public class clsMargensProdutos : clsConexao
    {

        public string UsuCod { get; set; }
        public string ProdCodEstr { get; set; }
        public string ProdNome { get; set; }
        public string DataVigencia { get; set; }
        public float CustoProduto { get; set; }
        public string Empresa { get; set; }
        public string Busca { get; set; }


       
        public DataTable Lista_Empresa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_EMPRESA_FILIAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar,100, "UsuCod"));


                    dbCommand.Parameters["@UsuCod"].Value = this.UsuCod;
                   
                    



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


        public DataTable Retorna_Empresa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_EMPRESA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

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


        public DataTable Lista_Produtos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_MARGENS_PRODUTO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@ProdCodEstr", SqlDbType.VarChar,100, "ProdCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@ProdNome", SqlDbType.VarChar,100, "ProdNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataVigencia", SqlDbType.VarChar,100, "DataVigencia"));
                    dbCommand.Parameters.Add(new SqlParameter("@CustoProduto", SqlDbType.VarChar, 100, "CustoProduto"));
                

                    dbCommand.Parameters["@ProdCodEstr"].Value = this.ProdCodEstr;
                    dbCommand.Parameters["@ProdNome"].Value = this.ProdNome;
                    dbCommand.Parameters["@DataVigencia"].Value = this.DataVigencia;
                    dbCommand.Parameters["@CustoProduto"].Value = this.CustoProduto;



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

        public DataTable Retorna_Produto()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_PRODUTO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

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
        





    
