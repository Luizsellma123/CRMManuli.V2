using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class CONTSOCIETARIOATUCAP : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        //[JsonProperty("DATA-ULTAT-CS")]
        public string DATAULTATCS { get; set; }

        //[JsonProperty("VR-CAPSOC-CS")]
        public string VRCAPSOCCS { get; set; }

        //[JsonProperty("VR-CAPREA-CS")]
        public string VRCAPREACS { get; set; }

        //[JsonProperty("VR-CAPAUT-CS")]
        public string VRCAPAUTCS { get; set; }

        //[JsonProperty("DES-CDNA-CS")]
        public string DESCDNACS { get; set; }

        //[JsonProperty("DES-CDCRAO-CS")]
        public string DESCDCRAOCS { get; set; }

        //[JsonProperty("DES-CPAR-CS")]
        public string DESCPARCS { get; set; }

        //[JsonProperty("TIPRET-CS")]
        public string TIPRETCS { get; set; }

        //[JsonProperty("SITUAC-CAPTOTAL")]
        public string SITUACCAPTOTAL { get; set; }

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

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONT_SOC_ULTATU_CAPSOCl", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DATAULTATCS", SqlDbType.VarChar, 8000, "DATAULTATCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@VRCAPSOCCS", SqlDbType.VarChar, 8000, "VRCAPSOCCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@VRCAPREACS", SqlDbType.VarChar, 8000, "VRCAPREACS"));
                    dbCommand.Parameters.Add(new SqlParameter("@VRCAPAUTCS", SqlDbType.VarChar, 8000, "VRCAPAUTCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@DESCDNACS", SqlDbType.VarChar, 8000, "DESCDNACS"));
                    dbCommand.Parameters.Add(new SqlParameter("@DESCDCRAOCS", SqlDbType.VarChar, 8000, "DESCDCRAOCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@DESCPARCS", SqlDbType.VarChar, 8000, "DESCPARCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@TIPRETCS", SqlDbType.VarChar, 8000, "TIPRETCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@SITUACCAPTOTAL", SqlDbType.VarChar, 8000, "SITUACCAPTOTAL"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@DATAULTATCS"].Value = DATAULTATCS ?? "";
                    dbCommand.Parameters["@VRCAPSOCCS"].Value = VRCAPSOCCS ?? "0";
                    dbCommand.Parameters["@VRCAPREACS"].Value = VRCAPREACS ?? "0";
                    dbCommand.Parameters["@VRCAPAUTCS"].Value = VRCAPAUTCS ?? "0";
                    dbCommand.Parameters["@DESCDNACS"].Value = DESCDNACS ?? "";
                    dbCommand.Parameters["@DESCDCRAOCS"].Value = DESCDCRAOCS ?? "";
                    dbCommand.Parameters["@DESCPARCS"].Value = DESCPARCS ?? "";
                    dbCommand.Parameters["@TIPRETCS"].Value = TIPRETCS ?? "";
                    dbCommand.Parameters["@SITUACCAPTOTAL"].Value = SITUACCAPTOTAL ?? "";

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