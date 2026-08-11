using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.GerencialVendas
{
    public class DashBoardClass : clsConexao
    {

        public string UsuCod { get; set; }
        public string UsuCodAux { get; set; }
        public string VendClasseCod { get; set; }
        public string VendCod { get; set; }
        public string Acesso { get; set; }
        public string TipoPedidoStatus { get; set; }


        public DataTable Consulta_Gestores()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_GESTORES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    


                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch
            {


            }

            return outputTable;

        }

        public DataTable Consulta_Classes_Gestores()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_CLASSES_GESTORES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 50, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCodAux", SqlDbType.VarChar, 8000, "UsuCodAux"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@UsuCodAux"].Value = UsuCodAux;



                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch
            {


            }

            return outputTable;

        }

        public DataTable Consulta_Vendedor_Classes()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_VENDEDOR_CLASSES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCodAux", SqlDbType.VarChar, 8000, "UsuCodAux"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendClasseCod", SqlDbType.VarChar, 8000, "VendClasseCod"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@UsuCodAux"].Value = UsuCodAux;
                    dbCommand.Parameters["@VendClasseCod"].Value = VendClasseCod;



                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch
            {


            }

            return outputTable;

        }


        public DataTable Consulta_Posicao_Carteira()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_CONSULTA_POSICAO_CARTEIRA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;



                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCodAux", SqlDbType.VarChar, 8000, "UsuCodAux"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendClasseCod", SqlDbType.VarChar, 8000, "VendClasseCod"));

                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 8000, "VendCod"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@UsuCodAux"].Value = UsuCodAux;
                    dbCommand.Parameters["@VendClasseCod"].Value = VendClasseCod;
                    dbCommand.Parameters["@VendCod"].Value = VendCod;


                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch
            {


            }

            return outputTable;

        }

        public DataTable Consulta_Pedidos_Status()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("user_sp_Pedidos_Por_Status", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;



                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCodAux", SqlDbType.VarChar, 8000, "UsuCodAux"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendClasseCod", SqlDbType.VarChar, 8000, "VendClasseCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@VendCod", SqlDbType.VarChar, 8000, "VendCod"));

                    dbCommand.Parameters.Add(new SqlParameter("@TipoPedidoStatus", SqlDbType.VarChar, 100, "TipoPedidoStatus"));

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    dbCommand.Parameters["@UsuCodAux"].Value = UsuCodAux;
                    dbCommand.Parameters["@VendClasseCod"].Value = VendClasseCod;
                    dbCommand.Parameters["@VendCod"].Value = VendCod;

                    dbCommand.Parameters["@TipoPedidoStatus"].Value = TipoPedidoStatus;


                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch
            {


            }

            return outputTable;

        }


        public string Consulta_Filtro_Gestor()
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

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_FILTRO_GESTOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                    

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                    



                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            Acesso = row["Acesso"].ToString();
                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Consulta_Filtro_Gestor";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao Consulta_Filtro_Gestor. Contactar o Suporte!";
            }




            return Retorno;

        }



    }
}