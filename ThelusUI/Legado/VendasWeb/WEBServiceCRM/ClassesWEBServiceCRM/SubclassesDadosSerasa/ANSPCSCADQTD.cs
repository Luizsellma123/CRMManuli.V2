using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class ANSPCSCADQTD : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string SEQ { get; set; }
        public string PESS { get; set; }
        public string DOC { get; set; }
        public string FIL { get; set; }
        public string DIG { get; set; }

       //[JsonProperty("SEQ-SOC")]
        public string SEQSOC { get; set; }
        public string VINC { get; set; }
        public string NOME { get; set; }
        public string QTANOT { get; set; }
        public string VRTOT { get; set; }
        public string DTRECE { get; set; }
        public string SITUAC { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);               

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_ANOT_SOC_ADM", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@SEQ", SqlDbType.VarChar, 8000, "SEQ"));
                    dbCommand.Parameters.Add(new SqlParameter("@PESS", SqlDbType.VarChar, 8000, "PESS"));
                    dbCommand.Parameters.Add(new SqlParameter("@DOC", SqlDbType.VarChar, 8000, "DOC"));
                    dbCommand.Parameters.Add(new SqlParameter("@FIL", SqlDbType.VarChar, 8000, "FIL"));
                    dbCommand.Parameters.Add(new SqlParameter("@DIG", SqlDbType.VarChar, 8000, "DIG"));
                    dbCommand.Parameters.Add(new SqlParameter("@SEQSOC", SqlDbType.VarChar, 8000, "SEQ-SOC"));
                    dbCommand.Parameters.Add(new SqlParameter("@VINC", SqlDbType.VarChar, 8000, "VINC"));
                    dbCommand.Parameters.Add(new SqlParameter("@NOME", SqlDbType.VarChar, 8000, "NOME"));
                    dbCommand.Parameters.Add(new SqlParameter("@QTANOT", SqlDbType.VarChar, 8000, "QTANOT"));
                    dbCommand.Parameters.Add(new SqlParameter("@VRTOT", SqlDbType.VarChar, 8000, "VRTOT"));
                    dbCommand.Parameters.Add(new SqlParameter("@DTRECE", SqlDbType.VarChar, 8000, "DTRECE"));
                    dbCommand.Parameters.Add(new SqlParameter("@SITUAC", SqlDbType.VarChar, 8000, "SITUAC"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@SEQ"].Value = SEQ ?? "";
                    dbCommand.Parameters["@PESS"].Value = PESS ?? "";
                    dbCommand.Parameters["@DOC"].Value = DOC ?? "";
                    dbCommand.Parameters["@FIL"].Value = FIL ?? "";
                    dbCommand.Parameters["@DIG"].Value = DIG ?? "";
                    dbCommand.Parameters["@SEQSOC"].Value = SEQSOC ?? "";
                    dbCommand.Parameters["@VINC"].Value = VINC ?? "";
                    dbCommand.Parameters["@NOME"].Value = NOME ?? "";
                    dbCommand.Parameters["@QTANOT"].Value = QTANOT ?? "";
                    dbCommand.Parameters["@VRTOT"].Value = VRTOT ?? "";
                    dbCommand.Parameters["@DTRECE"].Value = DTRECE ?? "";
                    dbCommand.Parameters["@SITUAC"].Value = SITUAC ?? "";

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