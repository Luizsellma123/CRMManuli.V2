using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class CONTABILIZACAO : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string CICSUSER { get; set; }

        //[JsonProperty("DATA-EMIS")]
        public string DATAEMIS { get; set; }

        //[JsonProperty("HORA-EMIS")]
        public string HORAEMIS { get; set; }

        public string RESERVADO { get; set; }

        //[JsonProperty("CNPJ-EDITADO")]
        public string CNPJEDITADO { get; set; }

        //[JsonProperty("DATA-ULTAT-CONT")]
        public string DATAULTATCONT { get; set; }

        //[JsonProperty("ORIGEM-DADOS")]
        public string ORIGEMDADOS { get; set; }

        public string NRUTRG { get; set; }

        public string DTUTRG { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            UtilClass objUtilClass = new UtilClass();

            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONTABILIZACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@CICSUSER", SqlDbType.VarChar, 8000, "CICSUSER"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAEMIS", SqlDbType.VarChar, 8000, "DATA-EMIS"));
                    dbCommand.Parameters.Add(new SqlParameter("@HORAEMIS", SqlDbType.VarChar, 8000, "HORA-EMIS"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADO", SqlDbType.VarChar, 8000, "RESERVADO"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJEDITADO", SqlDbType.VarChar, 8000, "CNPJ-EDITADO"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAULTATCONT", SqlDbType.VarChar, 8000, "DATA-ULTAT-CONT"));
                    dbCommand.Parameters.Add(new SqlParameter("@ORIGEMDADOS", SqlDbType.VarChar, 8000, "ORIGEM-DADOS"));
                    dbCommand.Parameters.Add(new SqlParameter("@NRUTRG", SqlDbType.VarChar, 8000, "NRUTRG"));
                    dbCommand.Parameters.Add(new SqlParameter("@DTUTRG", SqlDbType.VarChar, 8000, "DTUTRG"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@CICSUSER"].Value = CICSUSER ?? "";
                    dbCommand.Parameters["@DATAEMIS"].Value = DATAEMIS ?? "";
                    dbCommand.Parameters["@HORAEMIS"].Value = HORAEMIS ?? "";
                    dbCommand.Parameters["@RESERVADO"].Value = RESERVADO ?? "";
                    dbCommand.Parameters["@CNPJEDITADO"].Value = CNPJEDITADO ?? "";
                    dbCommand.Parameters["@DATAULTATCONT"].Value = DATAULTATCONT ?? "";
                    dbCommand.Parameters["@ORIGEMDADOS"].Value = ORIGEMDADOS ?? "";
                    dbCommand.Parameters["@NRUTRG"].Value = NRUTRG ?? "";
                    dbCommand.Parameters["@DTUTRG"].Value = DTUTRG ?? "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    foreach (DataRow row in outputTable.Rows)
                    {
                        erro = row["Erro"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return erro;
        }
    }
}