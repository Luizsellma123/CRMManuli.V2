using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class EMPCONSULTA : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string CDSITRF { get; set; }

        public string DSSITRF { get; set; }

        public string CDCG { get; set; }

        public string INDFICHA { get; set; }

        //[JsonProperty("TRN-CONTAB")]
        public string TRNCONTAB { get; set; }

        //[JsonProperty("AREA RESERVADA")]
        public string AREARESERVADA { get; set; }

        //[JsonProperty("TRN-CONT02")]
        public string TRNCONT02 { get; set; }

        //[JsonProperty("TRN-CONT03")]
        public string TRNCONT03 { get; set; }

        //[JsonProperty("TRN-CONT04")]
        public string TRNCONT04 { get; set; }

        //[JsonProperty("TRN-CONT05")]
        public string TRNCONT05 { get; set; }

        //[JsonProperty("TRN-CONT06")]
        public string TRNCONT06 { get; set; }

        //[JsonProperty("TRN-CONT07")]
        public string TRNCONT07 { get; set; }

        //[JsonProperty("TRN-CONT08")]
        public string TRNCONT08 { get; set; }

        //[JsonProperty("TRN-CONT09")]
        public string TRNCONT09 { get; set; }

        //[JsonProperty("TRN-CONT10")]

        public string TRNCONT10 { get; set; }

        public string TIPRELATO { get; set; }

        public string TEMRECIPR { get; set; }

        public string TIPRELCOB { get; set; }

        public string DIASREST { get; set; }

        public string CDSITUNOV { get; set; }

        public string DSSITUNOV { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_DADOS_CONTROLE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@CDSITRF", SqlDbType.VarChar, 8000, "CDSITRF"));
                    dbCommand.Parameters.Add(new SqlParameter("@DSSITRF", SqlDbType.VarChar, 8000, "DSSITRF"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDCG", SqlDbType.VarChar, 8000, "CDCG"));
                    dbCommand.Parameters.Add(new SqlParameter("@INDFICHA", SqlDbType.VarChar, 8000, "INDFICHA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONTAB", SqlDbType.VarChar, 8000, "TRNCONTAB"));
                    dbCommand.Parameters.Add(new SqlParameter("@AREARESERVADA", SqlDbType.VarChar, 8000, "AREARESERVADA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT02", SqlDbType.VarChar, 8000, "TRNCONT02"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT03", SqlDbType.VarChar, 8000, "TRNCONT03"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT04", SqlDbType.VarChar, 8000, "TRNCONT04"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT05", SqlDbType.VarChar, 8000, "TRNCONT05"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT06", SqlDbType.VarChar, 8000, "TRNCONT06"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT07", SqlDbType.VarChar, 8000, "TRNCONT07"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT08", SqlDbType.VarChar, 8000, "TRNCONT08"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT09", SqlDbType.VarChar, 8000, "TRNCONT09"));
                    dbCommand.Parameters.Add(new SqlParameter("@TRNCONT10", SqlDbType.VarChar, 8000, "TRNCONT10"));
                    dbCommand.Parameters.Add(new SqlParameter("@TIPRELATO", SqlDbType.VarChar, 8000, "TIPRELATO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TEMRECIPR", SqlDbType.VarChar, 8000, "TEMRECIPR"));
                    dbCommand.Parameters.Add(new SqlParameter("@TIPRELCOB", SqlDbType.VarChar, 8000, "TIPRELCOB"));
                    dbCommand.Parameters.Add(new SqlParameter("@DIASREST", SqlDbType.VarChar, 8000, "DIASREST"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDSITUNOV", SqlDbType.VarChar, 8000, "CDSITUNOV"));
                    dbCommand.Parameters.Add(new SqlParameter("@DSSITUNOV", SqlDbType.VarChar, 8000, "DSSITUNOV"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@CDSITRF"].Value = CDSITRF ?? "";
                    dbCommand.Parameters["@DSSITRF"].Value = DSSITRF ?? "";
                    dbCommand.Parameters["@CDCG"].Value = CDCG ?? "";
                    dbCommand.Parameters["@INDFICHA"].Value = INDFICHA ?? "";
                    dbCommand.Parameters["@TRNCONTAB"].Value = TRNCONTAB ?? "";
                    dbCommand.Parameters["@AREARESERVADA"].Value = AREARESERVADA ?? "";
                    dbCommand.Parameters["@TRNCONT02"].Value = TRNCONT02 ?? "";
                    dbCommand.Parameters["@TRNCONT03"].Value = TRNCONT03 ?? "";
                    dbCommand.Parameters["@TRNCONT04"].Value = TRNCONT04 ?? "";
                    dbCommand.Parameters["@TRNCONT05"].Value = TRNCONT05 ?? "";
                    dbCommand.Parameters["@TRNCONT06"].Value = TRNCONT06 ?? "";
                    dbCommand.Parameters["@TRNCONT07"].Value = TRNCONT07 ?? "";
                    dbCommand.Parameters["@TRNCONT08"].Value = TRNCONT08 ?? "";
                    dbCommand.Parameters["@TRNCONT09"].Value = TRNCONT09 ?? "";
                    dbCommand.Parameters["@TRNCONT10"].Value = TRNCONT10 ?? "";
                    dbCommand.Parameters["@TIPRELATO"].Value = TIPRELATO ?? "";
                    dbCommand.Parameters["@TEMRECIPR"].Value = TEMRECIPR ?? "";
                    dbCommand.Parameters["@TIPRELCOB"].Value = TIPRELCOB ?? "";
                    dbCommand.Parameters["@DIASREST"].Value = DIASREST ?? "0";
                    dbCommand.Parameters["@CDSITUNOV"].Value = CDSITUNOV ?? "0";
                    dbCommand.Parameters["@DSSITUNOV"].Value = DSSITUNOV ?? "";

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