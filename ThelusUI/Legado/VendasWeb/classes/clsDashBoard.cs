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
        public string UsuCodSupervisor { get; set; }
        public string EmpCod { get; set; }
        public string VendCod { get; set; }
        public string VendClasseCod { get; set; }
        public string Regionais { get; set; }
        public string TodosCodigos { get; set; }
              


        //Busca DashBoard Por Vendedor
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
                    //dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;
                    dbCommand.Parameters["@EmpCod"].Value = "Todas";
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


    public DataTable Lista_Classes()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("USER_SP_CRM_CLASSES", dbConnection);

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


    //Busca DashBoard Por Classe
    public DataTable Lista_Dashboard_Classes()
    {
        DataTable outputTable = new DataTable();

        try
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand("USER_SP_CRM_DASHBOARD_CLASSES", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 20, "DataInicial"));
                dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 20, "DataFinal"));
                dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                dbCommand.Parameters.Add(new SqlParameter("@VendClasseCod", SqlDbType.VarChar, 8000, "VendClasseCod"));
                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 30, "UsuCod"));

                dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;
                //dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;
                dbCommand.Parameters["@EmpCod"].Value = "Todas";
                dbCommand.Parameters["@VendClasseCod"].Value = this.VendClasseCod;
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

    public DataTable Lista_Supervisor()
    {
        DataTable outputTable = new DataTable();

        try
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand("user_sp_CRM_Supervisor", dbConnection);

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

    //Busca DashBoard Por Supervisor
    public DataTable Lista_Dashboard_Supervisor()
    {
        DataTable outputTable = new DataTable();

        try
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand("USER_SP_CRM_DASHBOARD_SUPERVISOR", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 20, "DataInicial"));
                dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 20, "DataFinal"));
                dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 30, "UsuCod"));
                dbCommand.Parameters.Add(new SqlParameter("@UsuCodLogado", SqlDbType.VarChar, 30, "@UsuCodLogado"));

                dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;
                //dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;
                dbCommand.Parameters["@EmpCod"].Value = "Todas";
                dbCommand.Parameters["@UsuCod"].Value = this.UsuCodSupervisor;
                dbCommand.Parameters["@UsuCodLogado"].Value = this.UsuCod;

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

    public DataTable Lista_Regionais()
    {
        DataTable outputTable = new DataTable();

        try
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand("USER_SP_CRM_REGIONAIS", dbConnection);

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

    //Busca DashBoard Por Classe
    public DataTable Lista_Dashboard_Regional()
    {
        DataTable outputTable = new DataTable();

        try
        {
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand("USER_SP_CRM_DASHBOARD_REGIONAL", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 20, "DataInicial"));
                dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 20, "DataFinal"));
                dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                dbCommand.Parameters.Add(new SqlParameter("@Regional", SqlDbType.VarChar, 8000, "Regional"));
                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 30, "UsuCod"));

                dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;
                //dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;
                dbCommand.Parameters["@EmpCod"].Value = "Todas";
                dbCommand.Parameters["@Regional"].Value = this.Regionais;
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
      

   
  

    
    
     
  
        

    

