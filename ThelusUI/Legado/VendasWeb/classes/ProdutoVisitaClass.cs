using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.GerencialVendas
{
    public class ProdutoVisitaClass : clsConexao
    {

        public int PRODUTO_VISITA_ID { get; set; }
        public int AGENDA_VISITA_ID { get; set; }
        public string ProdCodEstr { get; set; }
        public string ProdNome { get; set; }
        public string ClasseQtd { get; set; }
        public string PrazoPotencialMesCorrente { get; set; }
        public string PrazoPotencialMes1 { get; set; }
        public string PrazoPotencialMes3 { get; set; }
        public string PrazoPotencialMesSuperior { get; set; }
        public string TipoOperacao { get; set; }
        public string USERLINHAPRODUTOLISTA { get; set; }



        public string INSERE_PRODUTO_AGENDA()
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

                    dbCommand = new SqlCommand("USER_SP_INSERE_PRODUTO_AGENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@AGENDA_VISITA_ID", SqlDbType.Int, 0, "AGENDA_VISITA_ID"));
                    dbCommand.Parameters.Add(new SqlParameter("@ProdCodEstr", SqlDbType.VarChar, 150, "ProdCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@ClasseQtd", SqlDbType.VarChar, 5, "ClasseQtd"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoPotencialMesCorrente", SqlDbType.VarChar, 5, "PrazoPotencialMesCorrente"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoPotencialMes1", SqlDbType.VarChar, 5, "PrazoPotencialMes1"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoPotencialMes3", SqlDbType.VarChar, 5, "PrazoPotencialMes3"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoPotencialMesSuperior", SqlDbType.VarChar, 5, "PrazoPotencialMesSuperior"));
                    


                    dbCommand.Parameters["@AGENDA_VISITA_ID"].Value = AGENDA_VISITA_ID;
                    dbCommand.Parameters["@ProdCodEstr"].Value = ProdCodEstr;
                    dbCommand.Parameters["@ClasseQtd"].Value = ClasseQtd;
                    dbCommand.Parameters["@PrazoPotencialMesCorrente"].Value = PrazoPotencialMesCorrente;
                    dbCommand.Parameters["@PrazoPotencialMes1"].Value = PrazoPotencialMes1;
                    dbCommand.Parameters["@PrazoPotencialMes3"].Value = PrazoPotencialMes3;
                    dbCommand.Parameters["@PrazoPotencialMesSuperior"].Value = PrazoPotencialMesSuperior;

          
                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {

                            Retorno = row["msg"].ToString();


                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao INSERE_PRODUTO_AGENDA";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao INSERE_PRODUTO_AGENDA. Contactar o Suporte!";
            }




            return Retorno;

        }


        public string ALTERA_PRODUTO_AGENDA()
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

                    dbCommand = new SqlCommand("USER_SP_ALTERA_PRODUTO_AGENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@PRODUTO_VISITA_ID", SqlDbType.Int, 0, "PRODUTO_VISITA_ID"));
                    dbCommand.Parameters.Add(new SqlParameter("@ProdCodEstr", SqlDbType.VarChar, 150, "ProdCodEstr"));
                    dbCommand.Parameters.Add(new SqlParameter("@ClasseQtd", SqlDbType.VarChar, 5, "ClasseQtd"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoPotencialMesCorrente", SqlDbType.VarChar, 5, "PrazoPotencialMesCorrente"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoPotencialMes1", SqlDbType.VarChar, 5, "PrazoPotencialMes1"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoPotencialMes3", SqlDbType.VarChar, 5, "PrazoPotencialMes3"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrazoPotencialMesSuperior", SqlDbType.VarChar, 5, "PrazoPotencialMesSuperior"));



                    dbCommand.Parameters["@PRODUTO_VISITA_ID"].Value = PRODUTO_VISITA_ID;
                    dbCommand.Parameters["@ProdCodEstr"].Value = ProdCodEstr;
                    dbCommand.Parameters["@ClasseQtd"].Value = ClasseQtd;
                    dbCommand.Parameters["@PrazoPotencialMesCorrente"].Value = PrazoPotencialMesCorrente;
                    dbCommand.Parameters["@PrazoPotencialMes1"].Value = PrazoPotencialMes1;
                    dbCommand.Parameters["@PrazoPotencialMes3"].Value = PrazoPotencialMes3;
                    dbCommand.Parameters["@PrazoPotencialMesSuperior"].Value = PrazoPotencialMesSuperior;





                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {

                            Retorno = row["msg"].ToString();


                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao ALTERA_PRODUTO_AGENDA";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao ALTERA_PRODUTO_AGENDA. Contactar o Suporte!";
            }




            return Retorno;

        }


        public string DELETA_PRODUTO_AGENDA()
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

                    dbCommand = new SqlCommand("USER_SP_DELETA_PRODUTO_AGENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@PRODUTO_VISITA_ID", SqlDbType.Int, 0, "PRODUTO_VISITA_ID"));



                    dbCommand.Parameters["@PRODUTO_VISITA_ID"].Value = PRODUTO_VISITA_ID;
                    



                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {

                            Retorno = row["msg"].ToString();


                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao DELETA_PRODUTO_AGENDA";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao DELETA_PRODUTO_AGENDA. Contactar o Suporte!";
            }




            return Retorno;

        }


        public string MOSTRA_PRODUTO_AGENDA()
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

                    dbCommand = new SqlCommand("USER_SP_MOSTRA_PRODUTO_AGENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@PRODUTO_VISITA_ID", SqlDbType.Int, 0, "PRODUTO_VISITA_ID"));

                    dbCommand.Parameters["@PRODUTO_VISITA_ID"].Value = PRODUTO_VISITA_ID;



                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {

                            ProdCodEstr = row["ProdCodEstr"].ToString();
                            ClasseQtd = row["ClasseQtd"].ToString();
                            PrazoPotencialMesCorrente = row["PrazoPotencialMesCorrente"].ToString();
                            PrazoPotencialMes1 = row["PrazoPotencialMes1"].ToString();
                            PrazoPotencialMes3 = row["PrazoPotencialMes3"].ToString();
                            PrazoPotencialMesSuperior = row["PrazoPotencialMesSuperior"].ToString();
                            USERLINHAPRODUTOLISTA = row["USERLINHAPRODUTOLISTA"].ToString();

                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao MOSTRA_PRODUTO_AGENDA";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao MOSTRA_PRODUTO_AGENDA. Contactar o Suporte!";
            }




            return Retorno;

        }


        public DataTable MOSTRA_PRODUTO_VISITA_AGENDA_VISITA_ID()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_MOSTRA_PRODUTO_VISITA_AGENDA_VISITA_ID", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@AGENDA_VISITA_ID", SqlDbType.Int, 0, "AGENDA_VISITA_ID"));


                    dbCommand.Parameters["@AGENDA_VISITA_ID"].Value = AGENDA_VISITA_ID;
                    

                    dbCommand.CommandTimeout = 9999999;

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

    }
}