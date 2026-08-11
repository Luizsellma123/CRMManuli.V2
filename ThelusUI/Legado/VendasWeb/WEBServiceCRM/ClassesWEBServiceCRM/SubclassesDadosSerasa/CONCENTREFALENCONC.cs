using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class CONCENTREFALENCONC : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        //[JsonProperty("OCOR-FAC")]
        public string OCORFAC { get; set; }

        //[JsonProperty("DATA-FAC")]
        public string DATAFAC { get; set; }

        //[JsonProperty("TIPO-FAC")]
        public string TIPOFAC { get; set; }

        //[JsonProperty("ORIGEM-FAC")]
        public string ORIGEMFAC { get; set; }

        //[JsonProperty("VARA-FAC")]
        public string VARAFAC { get; set; }

        //[JsonProperty("CIDA-FAC")]
        public string CIDAFAC { get; set; }

        //[JsonProperty("UF-FAC")]
        public string UFFAC { get; set; }

        //[JsonProperty("CDNATU-FAC")]
        public string CDNATUFAC { get; set; }

        //[JsonProperty("RESERVADO-SERASA")]
        public string RESERVADOSERASA { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_FALENCIA_CONCORDATA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@OCORFAC", SqlDbType.VarChar, 8000, "OCORFAC"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAFAC", SqlDbType.VarChar, 8000, "DATAFAC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TIPOFAC", SqlDbType.VarChar, 8000, "TIPOFAC"));
                    dbCommand.Parameters.Add(new SqlParameter("@ORIGEMFAC", SqlDbType.VarChar, 8000, "ORIGEMFAC"));
                    dbCommand.Parameters.Add(new SqlParameter("@VARAFAC", SqlDbType.VarChar, 8000, "VARAFAC"));
                    dbCommand.Parameters.Add(new SqlParameter("@CIDAFAC", SqlDbType.VarChar, 8000, "CIDAFAC"));
                    dbCommand.Parameters.Add(new SqlParameter("@UFFAC", SqlDbType.VarChar, 8000, "UFFAC"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDNATUFAC", SqlDbType.VarChar, 8000, "CDNATUFAC"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@OCORFAC"].Value = OCORFAC ?? "";
                    dbCommand.Parameters["@DATAFAC"].Value = DATAFAC ?? "";
                    dbCommand.Parameters["@TIPOFAC"].Value = TIPOFAC ?? "";
                    dbCommand.Parameters["@ORIGEMFAC"].Value = ORIGEMFAC ?? "";
                    dbCommand.Parameters["@VARAFAC"].Value = VARAFAC ?? "";
                    dbCommand.Parameters["@CIDAFAC"].Value = CIDAFAC ?? "";
                    dbCommand.Parameters["@UFFAC"].Value = UFFAC ?? "";
                    dbCommand.Parameters["@CDNATUFAC"].Value = CDNATUFAC ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA"].Value = RESERVADOSERASA ?? "";

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