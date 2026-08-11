using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace VendasWeb.GerencialVendas
{


    public class EnderecoEntregaClass : clsConexao
    {


        public string EntCod { get; set; }
        public int EnderEntSeq { get; set; }
        public string EnderEntEntrega { get; set; }
        public string EnderEntNome { get; set; }
        public string EnderEnt { get; set; }
        public string EnderEntNo { get; set; }
        public string EnderEntNoPI { get; set; }
        public string EnderEntComp { get; set; }
        public string EnderEntBair { get; set; }
        public string CidCod { get; set; }
        public string EnderEntCep { get; set; }
        public string EnderEntEMail { get; set; }
        public string EnderEntContato { get; set; }

        public string EnderEntTipoFJ { get; set; }
        public string EnderEntCpfCgc { get; set; }
        public int EnderEntFoneSeq { get; set; }
        public string EnderEntFoneTipo { get; set; }
        public string EnderEntFoneDDD { get; set; }
        public string EnderEntFoneNum { get; set; }
        public string EnderEntFoneRamalBip { get; set; }
        public string EnderEntFoneRamalBipNum { get; set; }

        public string UsuCod { get; set; }





        public string Incluir_Endereco_Entrega()
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
                    dbCommand = new SqlCommand("User_SP_INSERE_ENDERECO_ENTREGA", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 10, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntSeq", SqlDbType.Int, 0, "EnderEntSeq"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntEntrega", SqlDbType.VarChar, 10, "EnderEntEntrega"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntNome", SqlDbType.VarChar, 500, "EnderEntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEnt", SqlDbType.VarChar, 500, "EnderEnt"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntNo", SqlDbType.VarChar, 50, "EnderEntNo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntNoPI", SqlDbType.VarChar, 30, "EnderEntNoPI"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntComp", SqlDbType.VarChar, 500, "EnderEntComp"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntBair", SqlDbType.VarChar, 30, "EnderEntBair"));
                    dbCommand.Parameters.Add(new SqlParameter("@CidCod", SqlDbType.VarChar, 10, "CidCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntCep", SqlDbType.VarChar, 30, "EnderEntCep"));

                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntTipoFJ", SqlDbType.VarChar, 30, "EnderEntTipoFJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntCpfCgc", SqlDbType.VarChar, 30, "EnderEntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntFoneSeq", SqlDbType.Int, 0, "EnderEntFoneSeq"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntFoneTipo", SqlDbType.VarChar, 50, "EnderEntFoneTipo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntFoneDDD", SqlDbType.VarChar, 10, "EnderEntFoneDDD"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntFoneNum", SqlDbType.VarChar, 30, "EnderEntFoneNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntFoneRamalBip", SqlDbType.VarChar, 50, "EnderEntFoneRamalBip"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntFoneRamalBipNum", SqlDbType.VarChar, 50, "EnderEntFoneRamalBipNum"));

                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntEMail", SqlDbType.VarChar, 250, "EnderEntEMail"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntContato", SqlDbType.VarChar, 200, "EnderEntContato"));

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 200, "UsuCod"));

                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@EnderEntSeq"].Value = EnderEntSeq;
                    dbCommand.Parameters["@EnderEntEntrega"].Value = EnderEntEntrega;
                    dbCommand.Parameters["@EnderEntNome"].Value = EnderEntNome;
                    dbCommand.Parameters["@EnderEnt"].Value = EnderEnt;
                    dbCommand.Parameters["@EnderEntNo"].Value = EnderEntNo;
                    dbCommand.Parameters["@EnderEntNoPI"].Value = EnderEntNoPI;
                    dbCommand.Parameters["@EnderEntComp"].Value = EnderEntComp;
                    dbCommand.Parameters["@EnderEntBair"].Value = EnderEntBair;
                    dbCommand.Parameters["@CidCod"].Value = CidCod;
                    dbCommand.Parameters["@EnderEntCep"].Value = EnderEntCep;

                    dbCommand.Parameters["@EnderEntTipoFJ"].Value = EnderEntTipoFJ;
                    dbCommand.Parameters["@EnderEntCpfCgc"].Value = EnderEntCpfCgc;

                    dbCommand.Parameters["@EnderEntFoneSeq"].Value = EnderEntFoneSeq;
                    dbCommand.Parameters["@EnderEntFoneTipo"].Value = EnderEntFoneTipo;
                    dbCommand.Parameters["@EnderEntFoneDDD"].Value = EnderEntFoneDDD;
                    dbCommand.Parameters["@EnderEntFoneNum"].Value = EnderEntFoneNum;
                    dbCommand.Parameters["@EnderEntFoneRamalBip"].Value = EnderEntFoneRamalBip;
                    dbCommand.Parameters["@EnderEntFoneRamalBipNum"].Value = EnderEntFoneRamalBipNum;

                    dbCommand.Parameters["@EnderEntEMail"].Value = EnderEntEMail;
                    dbCommand.Parameters["@EnderEntContato"].Value = EnderEntContato;

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;

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
                        Retorno = "Erro na Funcao Incluir_Endereco_Entrega";
                    }*/


                }
            }
            catch
            {

                Retorno = "Erro na Funcao Incluir_Endereco_Entrega. Contactar o Suporte!";
            }




            return Retorno;

        }


        public string Aletar_Endereco_Entrega()
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
                    dbCommand = new SqlCommand("User_SP_ALTERA_ENDERECO_ENTREGA", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 10, "EntCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntSeq", SqlDbType.Int, 0, "EnderEntSeq"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntEntrega", SqlDbType.VarChar, 10, "EnderEntEntrega"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntNome", SqlDbType.VarChar, 500, "EnderEntNome"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEnt", SqlDbType.VarChar, 500, "EnderEnt"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntNo", SqlDbType.VarChar, 50, "EnderEntNo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntNoPI", SqlDbType.VarChar, 30, "EnderEntNoPI"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntComp", SqlDbType.VarChar, 500, "EnderEntComp"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntBair", SqlDbType.VarChar, 30, "EnderEntBair"));
                    dbCommand.Parameters.Add(new SqlParameter("@CidCod", SqlDbType.VarChar, 10, "CidCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntCep", SqlDbType.VarChar, 30, "EnderEntCep"));

                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntTipoFJ", SqlDbType.VarChar, 30, "EnderEntTipoFJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntCpfCgc", SqlDbType.VarChar, 30, "EnderEntCpfCgc"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntFoneSeq", SqlDbType.Int, 0, "EnderEntFoneSeq"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntFoneTipo", SqlDbType.VarChar, 50, "EnderEntFoneTipo"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntFoneDDD", SqlDbType.VarChar, 10, "EnderEntFoneDDD"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntFoneNum", SqlDbType.VarChar, 30, "EnderEntFoneNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntFoneRamalBip", SqlDbType.VarChar, 50, "EnderEntFoneRamalBip"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntFoneRamalBipNum", SqlDbType.VarChar, 50, "EnderEntFoneRamalBipNum"));

                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntEMail", SqlDbType.VarChar, 250, "EnderEntEMail"));
                    dbCommand.Parameters.Add(new SqlParameter("@EnderEntContato", SqlDbType.VarChar, 200, "EnderEntContato"));

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 200, "UsuCod"));


                    dbCommand.Parameters["@EntCod"].Value = EntCod;
                    dbCommand.Parameters["@EnderEntSeq"].Value = EnderEntSeq;
                    dbCommand.Parameters["@EnderEntEntrega"].Value = EnderEntEntrega;
                    dbCommand.Parameters["@EnderEntNome"].Value = EnderEntNome;
                    dbCommand.Parameters["@EnderEnt"].Value = EnderEnt;
                    dbCommand.Parameters["@EnderEntNo"].Value = EnderEntNo;
                    dbCommand.Parameters["@EnderEntNoPI"].Value = EnderEntNoPI;
                    dbCommand.Parameters["@EnderEntComp"].Value = EnderEntComp;
                    dbCommand.Parameters["@EnderEntBair"].Value = EnderEntBair;
                    dbCommand.Parameters["@CidCod"].Value = CidCod;
                    dbCommand.Parameters["@EnderEntCep"].Value = EnderEntCep;

                    dbCommand.Parameters["@EnderEntTipoFJ"].Value = EnderEntTipoFJ;
                    dbCommand.Parameters["@EnderEntCpfCgc"].Value = EnderEntCpfCgc;

                    dbCommand.Parameters["@EnderEntFoneSeq"].Value = EnderEntFoneSeq;
                    dbCommand.Parameters["@EnderEntFoneTipo"].Value = EnderEntFoneTipo;
                    dbCommand.Parameters["@EnderEntFoneDDD"].Value = EnderEntFoneDDD;
                    dbCommand.Parameters["@EnderEntFoneNum"].Value = EnderEntFoneNum;
                    dbCommand.Parameters["@EnderEntFoneRamalBip"].Value = EnderEntFoneRamalBip;
                    dbCommand.Parameters["@EnderEntFoneRamalBipNum"].Value = EnderEntFoneRamalBipNum;

                    dbCommand.Parameters["@EnderEntEMail"].Value = EnderEntEMail;
                    dbCommand.Parameters["@EnderEntContato"].Value = EnderEntContato;

                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;

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
                        Retorno = "Erro na Funcao Incluir_Endereco_Entrega";
                    }


                }
            }
            catch
            {

                Retorno = "Erro na Funcao Incluir_Endereco_Entrega. Contactar o Suporte!";
            }




            return Retorno;

        }



        public DataTable Consulta_EnderecoEntrega_EntCod()
        {
            DataTable outputTable = new DataTable();


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("USER_SP_CONSULTA_ENDERECO_ENTREGA", dbConnection);

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