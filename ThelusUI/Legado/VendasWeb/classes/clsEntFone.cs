using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VendasWeb.GerencialVendas
{

    public class clsEntFone : clsConexao
    {



        public string EntCod { get; set; }
        public int EntFoneSeq { get; set; }
        public string EntFoneTipo { get; set; }
        public string EntFoneDDI { get; set; }
        public string EntFoneDDD { get; set; }
        public string EntFoneNum { get; set; }
        public string EntFoneRamalBip { get; set; }
        public string EntFoneRamalBipNum { get; set; }
        public string EntFonePrinc { get; set; }

        public string TipoOperacao { get; set; }



        public string Incluir_Telefone()
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

                    dbCommand = new SqlCommand("USER_SP_INSERE_ENT_FONE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneTipo", SqlDbType.VarChar, 15, "EntFoneTipo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneDDI", SqlDbType.VarChar, 5, "EntFoneDDI"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneDDD", SqlDbType.VarChar, 6, "EntFoneDDD"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneNum", SqlDbType.VarChar, 20, "EntFoneNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneRamalBip", SqlDbType.VarChar, 5, "EntFoneRamalBip"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneRamalBipNum", SqlDbType.VarChar, 10, "EntFoneRamalBipNum"));


                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@EntFoneTipo"].Value = EntFoneTipo;
                    dbCommand.Parameters["@EntFoneDDI"].Value = EntFoneDDI;
                    dbCommand.Parameters["@EntFoneDDD"].Value = EntFoneDDD;
                    dbCommand.Parameters["@EntFoneNum"].Value = EntFoneNum;
                    dbCommand.Parameters["@EntFoneRamalBip"].Value = EntFoneRamalBip;
                    dbCommand.Parameters["@EntFoneRamalBipNum"].Value = EntFoneRamalBipNum;


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
                        Retorno = "Erro na Funcao Incluir_Telefone";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao Incluir_Telefone. Contactar o Suporte!";
            }




            return Retorno;

        }


        public string Alterar_Telefone()
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

                    dbCommand = new SqlCommand("USER_SP_ALTERA_ENT_FONE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneTipo", SqlDbType.VarChar, 15, "EntFoneTipo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneDDI", SqlDbType.VarChar, 5, "EntFoneDDI"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneDDD", SqlDbType.VarChar, 6, "EntFoneDDD"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneNum", SqlDbType.VarChar, 20, "EntFoneNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneRamalBip", SqlDbType.VarChar, 5, "EntFoneRamalBip"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneRamalBipNum", SqlDbType.VarChar, 10, "EntFoneRamalBipNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneSeq", SqlDbType.Int, 0, "EntFoneSeq"));


                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@EntFoneTipo"].Value = EntFoneTipo;
                    dbCommand.Parameters["@EntFoneDDI"].Value = EntFoneDDI;
                    dbCommand.Parameters["@EntFoneDDD"].Value = EntFoneDDD;
                    dbCommand.Parameters["@EntFoneNum"].Value = EntFoneNum;
                    dbCommand.Parameters["@EntFoneRamalBip"].Value = EntFoneRamalBip;
                    dbCommand.Parameters["@EntFoneRamalBipNum"].Value = EntFoneRamalBipNum;
                    dbCommand.Parameters["@EntFoneSeq"].Value = EntFoneSeq;


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
                        Retorno = "Erro na Funcao Alterar_Telefone";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao Alterar_Telefone. Contactar o Suporte!";
            }




            return Retorno;

        }

        public string Deleta_Telefone()
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

                    dbCommand = new SqlCommand("USER_SP_DELETA_ENT_FONE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntFoneSeq", SqlDbType.Int, 0, "EntFoneSeq"));


                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@EntFoneSeq"].Value = EntFoneSeq;


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
                        Retorno = "Erro na Funcao Alterar_Telefone";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao Alterar_Telefone. Contactar o Suporte!";
            }




            return Retorno;

        }

        public DataTable Consulta_EntFone_EntCod()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("USER_SP_CONSULTA_ENT_FONE", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));


                dbCommand.Parameters["@EntCod"].Value = EntCod;



                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);


                return outputTable;
            }

        }

    }

}