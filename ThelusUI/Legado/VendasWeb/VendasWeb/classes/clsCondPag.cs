using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VendasWeb.GerencialVendas
{

    public class clsCondPag : clsConexao
    {


        public string EntCod { get; set; }
        public string CondPagCod { get; set; }
        public string Condicao { get; set; }
        public int Codigo { get; set; }
        public decimal CondPagEntValAte { get; set; }
        public string UsuCod { get; set; }
        public string TipoOperacao { get; set; }
        public string CondPagNome { get; set; }
        public string NIVCOD { get; set; }

        public string Incluir_Cond_Pag_Ent()
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

                    dbCommand = new SqlCommand("USER_SP_INSERE_COND_PAG_ENT", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 300, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@CondPagCod", SqlDbType.VarChar, 300, "CondPagCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@CondPagEntValAte", SqlDbType.Decimal, 0, "CondPagEntValAte"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));


                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@CondPagCod"].Value = CondPagCod;
                    dbCommand.Parameters["@CondPagEntValAte"].Value = CondPagEntValAte;
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;


                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }


                    /*if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {

                            Retorno = row["msg"].ToString();


                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Incluir_Cond_Pag_Ent";
                    }*/


                }
            }
            catch
            {

                Retorno = "Erro na Funcao Incluir_Cond_Pag_Ent. Contactar o Suporte!";
            }




            return Retorno;

        }

        public string Altera_Cond_Pag_Ent()
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

                    dbCommand = new SqlCommand("USER_SP_ALTERA_COND_PAG_ENT", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 300, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@CondPagCod", SqlDbType.VarChar, 300, "CondPagCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@CondPagEntValAte", SqlDbType.Decimal, 0, "CondPagEntValAte"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));


                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@CondPagCod"].Value = CondPagCod;
                    dbCommand.Parameters["@CondPagEntValAte"].Value = CondPagEntValAte;
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

                            Retorno = row["msg"].ToString();


                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Altera_Cond_Pag_Ent";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao Altera_Cond_Pag_Ent. Contactar o Suporte!";
            }




            return Retorno;

        }

        public string Remove_Cond_Pag_Ent()
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

                    dbCommand = new SqlCommand("USER_SP_REMOVE_COND_PAG_ENT", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 300, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@CondPagCod", SqlDbType.VarChar, 300, "CondPagCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@CondPagCod"].Value = CondPagCod;
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
                            Retorno = row["msg"].ToString();
                        }
                    }
                    else
                    {
                        Retorno = "Erro na Funcao Remove_Cond_Pag_Ent";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Funcao Remove_Cond_Pag_Ent. Contactar o Suporte!";
            }

            return Retorno;
        }

        public DataTable Mostra_Cond_pag_Holding()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_MOSTRA_COND_PAG_HOLDING", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@NIVCOD", SqlDbType.VarChar, 100, "NIVCOD"));




                    dbCommand.Parameters["@EntCod"].Value = this.EntCod;
                    dbCommand.Parameters["@NIVCOD"].Value = this.NIVCOD;



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


        public DataTable Consulta_Cod_Pag_EntCod()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("USER_SP_CONSULTA_COND_PAG_ENT", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));


                dbCommand.Parameters["@EntCod"].Value = EntCod;



                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }

        }

        public DataTable Consulta_Cod_Pag_Pedidos_EntCod()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_Consulta_Cond_Pag_Ped_Venda_Ent", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));


                dbCommand.Parameters["@EntCod"].Value = EntCod;



                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }
        }

        public DataTable Consulta_Condicao_Recebimento_CRM()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_CONSULTA_CONDICOES_PAGAMENTO_RECEBIMENTO", dbConnection);

                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 100, "EntCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;

                    dbCommand.CommandType = CommandType.StoredProcedure;

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

        public string Remove_Cond_Pag_Todas()
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

                    dbCommand = new SqlCommand("USER_SP_REMOVE_COND_PAG_Todas", dbConnection);

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
                Retorno = "Erro na Funcao Remove_Cond_Pag_Todas. Contactar o Suporte!";
            }

            return Retorno;
        }

    }
}