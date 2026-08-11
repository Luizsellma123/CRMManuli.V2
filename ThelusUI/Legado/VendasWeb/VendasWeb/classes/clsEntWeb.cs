using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VendasWeb.GerencialVendas
{

    public class clsEntWeb : clsConexao
    {


        #region Tabela ENT_WEB

        public string EntCod { get; set; }
        public int EntWebSeq { get; set; }
        public string EntWebTipo { get; set; }
        public string EntWebWWW { get; set; }
        public string EntWebEMail { get; set; }
        public string EntWebEMailPrinc { get; set; }
        public string EntWebEMailPedComp { get; set; }
        public string EntWebRecebeEmailOcor { get; set; }
        public string EntWebDisparaEmailAgenda { get; set; }
        public string EntWebEmailNFe { get; set; }
        public string EntWebEmailNFSe { get; set; }

        #endregion

        public string Incluir_Email()
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

                    dbCommand = new SqlCommand("USER_SP_INSERE_ENT_WEB", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebSeq", SqlDbType.Int, 0, "EntWebSeq"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebTipo", SqlDbType.VarChar, 10, "EntWebTipo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebWWW", SqlDbType.VarChar, 255, "EntWebWWW"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebEMail", SqlDbType.VarChar, 50, "EntWebEMail"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebEMailPrinc", SqlDbType.VarChar, 5, "EntWebEMailPrinc"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebEMailPedComp", SqlDbType.VarChar, 5, "EntWebEMailPedComp"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebRecebeEmailOcor", SqlDbType.VarChar, 5, "EntWebRecebeEmailOcor"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebDisparaEmailAgenda", SqlDbType.VarChar, 5, "EntWebDisparaEmailAgenda"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebEmailNFe", SqlDbType.VarChar, 5, "EntWebEmailNFe"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebEmailNFSe", SqlDbType.VarChar, 5, "EntWebEmailNFSe"));


                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@EntWebSeq"].Value = EntWebSeq;
                    dbCommand.Parameters["@EntWebTipo"].Value = EntWebTipo;
                    dbCommand.Parameters["@EntWebWWW"].Value = EntWebWWW;
                    dbCommand.Parameters["@EntWebEMail"].Value = EntWebEMail;
                    dbCommand.Parameters["@EntWebEMailPrinc"].Value = EntWebEMailPrinc;
                    dbCommand.Parameters["@EntWebEMailPedComp"].Value = EntWebEMailPedComp;
                    dbCommand.Parameters["@EntWebRecebeEmailOcor"].Value = EntWebRecebeEmailOcor;
                    dbCommand.Parameters["@EntWebDisparaEmailAgenda"].Value = EntWebDisparaEmailAgenda;
                    dbCommand.Parameters["@EntWebEmailNFe"].Value = EntWebEmailNFe;
                    dbCommand.Parameters["@EntWebEmailNFSe"].Value = EntWebEmailNFSe;

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
                        Retorno = "Erro na Funcao Incluir_Email";
                    }*/


                }
            }
            catch
            {

                Retorno = "Erro na Funcao Incluir_Email. Contactar o Suporte!";
            }




            return Retorno;

        }

        public string Altera_Email()
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

                    dbCommand = new SqlCommand("USER_SP_ALTERA_ENT_WEB", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebSeq", SqlDbType.Int, 0, "EntWebSeq"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebTipo", SqlDbType.VarChar, 10, "EntWebTipo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebWWW", SqlDbType.VarChar, 255, "EntWebWWW"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebEMail", SqlDbType.VarChar, 50, "EntWebEMail"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebEMailPrinc", SqlDbType.VarChar, 5, "EntWebEMailPrinc"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebEMailPedComp", SqlDbType.VarChar, 5, "EntWebEMailPedComp"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebRecebeEmailOcor", SqlDbType.VarChar, 5, "EntWebRecebeEmailOcor"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebDisparaEmailAgenda", SqlDbType.VarChar, 5, "EntWebDisparaEmailAgenda"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebEmailNFe", SqlDbType.VarChar, 5, "EntWebEmailNFe"));
                    dbCommand.Parameters.Add(new SqlParameter("@EntWebEmailNFSe", SqlDbType.VarChar, 5, "EntWebEmailNFSe"));


                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@EntWebSeq"].Value = EntWebSeq;
                    dbCommand.Parameters["@EntWebTipo"].Value = EntWebTipo;
                    dbCommand.Parameters["@EntWebWWW"].Value = EntWebWWW;
                    dbCommand.Parameters["@EntWebEMail"].Value = EntWebEMail;
                    dbCommand.Parameters["@EntWebEMailPrinc"].Value = EntWebEMailPrinc;
                    dbCommand.Parameters["@EntWebEMailPedComp"].Value = EntWebEMailPedComp;
                    dbCommand.Parameters["@EntWebRecebeEmailOcor"].Value = EntWebRecebeEmailOcor;
                    dbCommand.Parameters["@EntWebDisparaEmailAgenda"].Value = EntWebDisparaEmailAgenda;
                    dbCommand.Parameters["@EntWebEmailNFe"].Value = EntWebEmailNFe;
                    dbCommand.Parameters["@EntWebEmailNFSe"].Value = EntWebEmailNFSe;


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
                        Retorno = "Erro na Funcao Incluir_Email";
                    }*/


                }
            }
            catch
            {

                Retorno = "Erro na Funcao Incluir_Email. Contactar o Suporte!";
            }




            return Retorno;

        }

        public DataTable Consulta_EntWeb_EntCod()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("USER_SP_CONSULTA_ENT_WEB", dbConnection);

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