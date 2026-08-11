using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class CONCENTREPROTESTOS : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        //[JsonProperty("OCOR-PROT ")]
        public string OCORPROT { get; set; }

        //[JsonProperty("DATA-PROT")]
        public string DATAPROT { get; set; }

        //[JsonProperty("MOED-PROT")]
        public string MOEDPROT { get; set; }

        //[JsonProperty("VALO-PROT")]
        public string VALOPROT { get; set; }
        public string CART { get; set; }

        //[JsonProperty("CIDA-PROT")]
        public string CIDAPROT { get; set; }

        //[JsonProperty("UF-PROT")]
        public string UFPROT { get; set; }

        //[JsonProperty("PRACA-PRO")]
        public string PRACAPRO { get; set; }

       //[JsonProperty("DISTR-PRO")]
        public string DISTRPRO { get; set; }

       //[JsonProperty("VARA-PRO")]
        public string VARAPRO { get; set; }

       //[JsonProperty("DATA-PRO")]
        public string DATAPRO { get; set; }

       //[JsonProperty("PROC-PRO")]
        public string PROCPRO { get; set; }

       //[JsonProperty("CDNATU-PRO")]
        public string CDNATUPRO { get; set; }

       //[JsonProperty("RESERVADO-SERASA")]
        public string RESERVADOSERASA { get; set; }

       //[JsonProperty("TPANUE-PROT")]
        public string TPANUEPROT { get; set; }

       //[JsonProperty("DTANUE-PROT")]
        public string DTANUEPROT { get; set; }

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

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_PROTESTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@OCORPROT", SqlDbType.VarChar, 8000, "OCORPROT"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAPROT", SqlDbType.VarChar, 8000, "DATAPROT"));
                    dbCommand.Parameters.Add(new SqlParameter("@MOEDPROT", SqlDbType.VarChar, 8000, "MOEDPROT"));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOPROT", SqlDbType.VarChar, 8000, "VALOPROT"));
                    dbCommand.Parameters.Add(new SqlParameter("@CART", SqlDbType.VarChar, 8000, "CART"));
                    dbCommand.Parameters.Add(new SqlParameter("@CIDAPROT", SqlDbType.VarChar, 8000, "CIDAPROT"));
                    dbCommand.Parameters.Add(new SqlParameter("@UFPROT", SqlDbType.VarChar, 8000, "UFPROT"));
                    dbCommand.Parameters.Add(new SqlParameter("@PRACAPRO", SqlDbType.VarChar, 8000, "PRACAPRO"));
                    dbCommand.Parameters.Add(new SqlParameter("@DISTRPRO", SqlDbType.VarChar, 8000, "DISTRPRO"));
                    dbCommand.Parameters.Add(new SqlParameter("@VARAPRO", SqlDbType.VarChar, 8000, "VARAPRO"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAPRO", SqlDbType.VarChar, 8000, "DATAPRO"));
                    dbCommand.Parameters.Add(new SqlParameter("@PROCPRO", SqlDbType.VarChar, 8000, "PROCPRO"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDNATUPRO", SqlDbType.VarChar, 8000, "CDNATUPRO"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPANUEPROT", SqlDbType.VarChar, 8000, "TPANUEPROT"));
                    dbCommand.Parameters.Add(new SqlParameter("@DTANUEPROT", SqlDbType.VarChar, 8000, "DTANUEPROT"));
                    dbCommand.Parameters.Add(new SqlParameter("@MSGSUBJUD", SqlDbType.VarChar, 8000, "MSGSUBJUD"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA2", SqlDbType.VarChar, 8000, "RESERVADOSERASA2"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@OCORPROT"].Value = OCORPROT ?? "";
                    dbCommand.Parameters["@DATAPROT"].Value = DATAPROT ?? "";
                    dbCommand.Parameters["@MOEDPROT"].Value = MOEDPROT ?? "";
                    dbCommand.Parameters["@VALOPROT"].Value = VALOPROT ?? "";
                    dbCommand.Parameters["@CART"].Value = CART ?? "";
                    dbCommand.Parameters["@CIDAPROT"].Value = CIDAPROT ?? "";
                    dbCommand.Parameters["@UFPROT"].Value = UFPROT ?? "";
                    dbCommand.Parameters["@PRACAPRO"].Value = PRACAPRO ?? "";
                    dbCommand.Parameters["@DISTRPRO"].Value = DISTRPRO ?? "";
                    dbCommand.Parameters["@VARAPRO"].Value = VARAPRO ?? "";
                    dbCommand.Parameters["@DATAPRO"].Value = DATAPRO ?? "";
                    dbCommand.Parameters["@PROCPRO"].Value = PROCPRO ?? "";
                    dbCommand.Parameters["@CDNATUPRO"].Value = CDNATUPRO ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA"].Value = RESERVADOSERASA ?? "";
                    dbCommand.Parameters["@TPANUEPROT"].Value = TPANUEPROT ?? "";
                    dbCommand.Parameters["@DTANUEPROT"].Value = DTANUEPROT ?? "";
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