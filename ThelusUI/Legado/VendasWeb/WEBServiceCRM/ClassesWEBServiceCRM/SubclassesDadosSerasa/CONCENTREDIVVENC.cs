using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class CONCENTREDIVVENC : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

       //[JsonProperty("OCOR-DIV")]
        public string OCORDIV { get; set; }

       //[JsonProperty("DATA-DIV")]
        public string DATADIV { get; set; }
        public string MODALI { get; set; }

       //[JsonProperty("MOED-DIV")]
        public string MOEDDIV { get; set; }

       //[JsonProperty("VALO-DIV")]
        public string VALODIV { get; set; }

       //[JsonProperty("TÍTULO-DIV")]
        public string TTULODIV { get; set; }
        public string INSTFI { get; set; }

       //[JsonProperty("LOCAL-DIV")]
        public string LOCALDIV { get; set; }

       //[JsonProperty("CDNATU-DIV")]
        public string CDNATUDIV { get; set; }

       //[JsonProperty("RESERVADO-SERASA")]
        public string RESERVADOSERASA { get; set; }

       //[JsonProperty("PRACA-DIV")]
        public string PRACADIV { get; set; }

       //[JsonProperty("DISTR- DIV")]
        public string DISTRDIV { get; set; }

       //[JsonProperty("VARA- DIV")]
        public string VARADIV { get; set; }

       //[JsonProperty("DATASUB- DIV")]
        public string DATASUBDIV { get; set; }

       //[JsonProperty("PROC- DIV")]
        public string PROCDIV { get; set; }

       //[JsonProperty("MSG-SUBJUD")]
        public string MSGSUBJUD { get; set; }

       //[JsonProperty("RESERVADO-SERASA2")]
        public string RESERVADOSERASA2 { get; set; }
       
        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_DIVIDAS_VENCIDAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@OCORDIV", SqlDbType.VarChar, 8000, "OCORDIV"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATADIV", SqlDbType.VarChar, 8000, "DATADIV"));
                    dbCommand.Parameters.Add(new SqlParameter("@MODALI", SqlDbType.VarChar, 8000, "MODALI"));
                    dbCommand.Parameters.Add(new SqlParameter("@MOEDDIV", SqlDbType.VarChar, 8000, "MOEDDIV"));
                    dbCommand.Parameters.Add(new SqlParameter("@VALODIV", SqlDbType.VarChar, 8000, "VALODIV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TITULODIV", SqlDbType.VarChar, 8000, "TITULODIV"));
                    dbCommand.Parameters.Add(new SqlParameter("@INSTFI", SqlDbType.VarChar, 8000, "INSTFI"));
                    dbCommand.Parameters.Add(new SqlParameter("@LOCALDIV", SqlDbType.VarChar, 8000, "LOCALDIV"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDNATUDIV", SqlDbType.VarChar, 8000, "CDNATUDIV"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                    dbCommand.Parameters.Add(new SqlParameter("@PRACADIV", SqlDbType.VarChar, 8000, "PRACADIV"));
                    dbCommand.Parameters.Add(new SqlParameter("@DISTRDIV", SqlDbType.VarChar, 8000, "DISTRDIV"));
                    dbCommand.Parameters.Add(new SqlParameter("@VARADIV", SqlDbType.VarChar, 8000, "VARADIV"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATASUBDIV", SqlDbType.VarChar, 8000, "DATASUBDIV"));
                    dbCommand.Parameters.Add(new SqlParameter("@PROCDIV", SqlDbType.VarChar, 8000, "PROCDIV"));
                    dbCommand.Parameters.Add(new SqlParameter("@MSGSUBJUD", SqlDbType.VarChar, 8000, "MSGSUBJUD"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA2", SqlDbType.VarChar, 8000, "RESERVADOSERASA2"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@OCORDIV"].Value = OCORDIV ?? "";
                    dbCommand.Parameters["@DATADIV"].Value = DATADIV ?? "";
                    dbCommand.Parameters["@MODALI"].Value = MODALI ?? "";
                    dbCommand.Parameters["@MOEDDIV"].Value = MOEDDIV ?? "";
                    dbCommand.Parameters["@VALODIV"].Value = VALODIV ?? "";
                    dbCommand.Parameters["@TITULODIV"].Value = TTULODIV ?? "";
                    dbCommand.Parameters["@INSTFI"].Value = INSTFI ?? "";
                    dbCommand.Parameters["@LOCALDIV"].Value = LOCALDIV ?? "";
                    dbCommand.Parameters["@CDNATUDIV"].Value = CDNATUDIV ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA"].Value = RESERVADOSERASA ?? "";
                    dbCommand.Parameters["@PRACADIV"].Value = PRACADIV ?? "";
                    dbCommand.Parameters["@DISTRDIV"].Value = DISTRDIV ?? "";
                    dbCommand.Parameters["@VARADIV"].Value = VARADIV ?? "";
                    dbCommand.Parameters["@DATASUBDIV"].Value = DATASUBDIV ?? "";
                    dbCommand.Parameters["@PROCDIV"].Value = PROCDIV ?? "";
                    dbCommand.Parameters["@MSGSUBJUD"].Value = MSGSUBJUD ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA2"].Value = RESERVADOSERASA2 ?? "";

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