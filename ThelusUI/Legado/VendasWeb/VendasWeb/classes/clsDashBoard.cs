using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace VendasWeb.GerencialVendas
{
    public class clsDashBoard : clsConexao
    {

        public string DataInicial { get; set; }
        public string DataFinal { get; set; }
        public string UsuCod { get; set; }
        public string EmpCod { get; set; }
        public string VendCod { get; set; }
        
      

        public DataTable Lista_Principal()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_CRM_DASHBOARD", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar,20, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar,20, "DataFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar,10, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 8000, "VendCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 30, "UsuCod"));

                    dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                    dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;
                    dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;
                    dbCommand.Parameters["@VendCod"].Value = this.VendCod;
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

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));


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




        }

        }
      

   
  

    
    
     
  
        

    

