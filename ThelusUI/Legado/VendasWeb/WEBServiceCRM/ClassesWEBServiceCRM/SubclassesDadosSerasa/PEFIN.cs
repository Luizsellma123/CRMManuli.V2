using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class PEFIN : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string QTDEOCOR { get; set; }

        public string ULTOCOR { get; set; }

        //[JsonProperty("DATA-PEF")]
        public string DATAPEF { get; set; }

        //[JsonProperty("TÍTULO-PEF")]
        public string TITULOPEF { get; set; }

        //[JsonProperty("AVAL-PEF")]
        public string AVALPEF { get; set; }

        public string VALOR { get; set; }

        public string CONTRA { get; set; }

        public string ORIGEM { get; set; }

        public string FILIAL { get; set; }

        //[JsonProperty("PRACA-PEF")]
        public string PRACAPEF { get; set; }

        //[JsonProperty("DISTR-PEF")]
        public string DISTRPEF { get; set; }

        //[JsonProperty("VARA-PEF")]
        public string VARAPEF { get; set; }

        //[JsonProperty("DATA-SUB-PEF")]
        public string DATASUBPEF { get; set; }

        //[JsonProperty("PROC-PEF")]
        public string PROCPEF { get; set; }

        //[JsonProperty("CDNATU-PEF")]
        public string CDNATUPEF { get; set; }

        //[JsonProperty("RESERVADO-SERASA")]
        public string RESERVADOSERASA { get; set; }

        //[JsonProperty("MSG-SUBJUD")]
        public string MSGSUBJUD { get; set; }

        public string QTDEVALO { get; set; }

        //[JsonProperty("RESERVADO-SERASA2")]
        public string RESERVADOSERASA2 { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            GerencialVendas.UtilClass objUtilClass = new GerencialVendas.UtilClass();

            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_PEFIN", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@QTDEOCOR", SqlDbType.VarChar, 8000, "QTDEOCOR"));
                    dbCommand.Parameters.Add(new SqlParameter("@ULTOCOR", SqlDbType.VarChar, 8000, "ULTOCOR"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAPEF", SqlDbType.VarChar, 8000, "DATAPEF"));
                    dbCommand.Parameters.Add(new SqlParameter("@TITULOPEF", SqlDbType.VarChar, 8000, "TITULOPEF"));
                    dbCommand.Parameters.Add(new SqlParameter("@AVALPEF", SqlDbType.VarChar, 8000, "AVALPEF"));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR", SqlDbType.VarChar, 8000, "VALOR"));
                    dbCommand.Parameters.Add(new SqlParameter("@CONTRA", SqlDbType.VarChar, 8000, "CONTRA"));
                    dbCommand.Parameters.Add(new SqlParameter("@ORIGEM", SqlDbType.VarChar, 8000, "ORIGEM"));
                    dbCommand.Parameters.Add(new SqlParameter("@FILIAL", SqlDbType.VarChar, 8000, "FILIAL"));
                    dbCommand.Parameters.Add(new SqlParameter("@PRACAPEF", SqlDbType.VarChar, 8000, "PRACAPEF"));
                    dbCommand.Parameters.Add(new SqlParameter("@DISTRPEF", SqlDbType.VarChar, 8000, "DISTRPEF"));
                    dbCommand.Parameters.Add(new SqlParameter("@VARAPEF", SqlDbType.VarChar, 8000, "VARAPEF"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATASUBPEF", SqlDbType.VarChar, 8000, "DATASUBPEF"));
                    dbCommand.Parameters.Add(new SqlParameter("@PROCPEF", SqlDbType.VarChar, 8000, "PROCPEF"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDNATUPEF", SqlDbType.VarChar, 8000, "CDNATUPEF"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                    dbCommand.Parameters.Add(new SqlParameter("@MSGSUBJUD", SqlDbType.VarChar, 8000, "MSGSUBJUD"));
                    dbCommand.Parameters.Add(new SqlParameter("@QTDEVALO", SqlDbType.VarChar, 8000, "QTDEVALO"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA2", SqlDbType.VarChar, 8000, "RESERVADOSERASA2"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@QTDEOCOR"].Value = QTDEOCOR ?? "";
                    dbCommand.Parameters["@ULTOCOR"].Value = ULTOCOR ?? "";
                    dbCommand.Parameters["@DATAPEF"].Value = DATAPEF ?? "";
                    dbCommand.Parameters["@TITULOPEF"].Value = TITULOPEF ?? "";
                    dbCommand.Parameters["@AVALPEF"].Value = AVALPEF ?? "";
                    dbCommand.Parameters["@VALOR"].Value = VALOR ?? "";
                    dbCommand.Parameters["@CONTRA"].Value = CONTRA ?? "";
                    dbCommand.Parameters["@ORIGEM"].Value = ORIGEM ?? "";
                    dbCommand.Parameters["@FILIAL"].Value = FILIAL ?? "";
                    dbCommand.Parameters["@PRACAPEF"].Value = PRACAPEF ?? "";
                    dbCommand.Parameters["@DISTRPEF"].Value = DISTRPEF ?? "";
                    dbCommand.Parameters["@VARAPEF"].Value = VARAPEF ?? "";
                    dbCommand.Parameters["@DATASUBPEF"].Value = DATASUBPEF ?? "";
                    dbCommand.Parameters["@PROCPEF"].Value = PROCPEF ?? "";
                    dbCommand.Parameters["@CDNATUPEF"].Value = CDNATUPEF ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA"].Value = RESERVADOSERASA ?? "";
                    dbCommand.Parameters["@MSGSUBJUD"].Value = MSGSUBJUD ?? "";
                    dbCommand.Parameters["@QTDEVALO"].Value = QTDEVALO ?? "";
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