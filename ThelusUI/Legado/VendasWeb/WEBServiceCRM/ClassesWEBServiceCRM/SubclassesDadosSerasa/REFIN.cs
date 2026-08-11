using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class REFIN : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string QTDEOCOR { get; set; }

        public string ULTOCOR { get; set; }

        //[JsonProperty("DATA-REF")]
        public string DATAREF { get; set; }

        //[JsonProperty("TÍTULO-REF")]
        public string TITULOREF { get; set; }

        //[JsonProperty("AVAL-REF")]
        public string AVALREF { get; set; }
        public string VALOR { get; set; }

        public string CONTRA { get; set; }

        public string ORIGEM { get; set; }

        public string FILIAL { get; set; }

        //[JsonProperty("PRACA-REF")]
        public string PRACAREF { get; set; }

        //[JsonProperty("DISTR-REF")]
        public string DISTRREF { get; set; }

        //[JsonProperty("VARA-REF")]
        public string VARAREF { get; set; }

        //[JsonProperty("DATA-SUB-REF")]
        public string DATASUBREF { get; set; }

        //[JsonProperty("PROC-REF")]
        public string PROCREF { get; set; }

        //[JsonProperty("CDNATU-REF")]
        public string CDNATUREF { get; set; }

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

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_REFIN", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@QTDEOCOR", SqlDbType.VarChar, 8000, "QTDEOCOR"));
                    dbCommand.Parameters.Add(new SqlParameter("@ULTOCOR", SqlDbType.VarChar, 8000, "ULTOCOR"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAREF", SqlDbType.VarChar, 8000, "DATAREF"));
                    dbCommand.Parameters.Add(new SqlParameter("@TITULOREF", SqlDbType.VarChar, 8000, "TITULOREF"));
                    dbCommand.Parameters.Add(new SqlParameter("@AVALREF", SqlDbType.VarChar, 8000, "AVALREF"));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOR", SqlDbType.VarChar, 8000, "VALOR"));
                    dbCommand.Parameters.Add(new SqlParameter("@CONTRA", SqlDbType.VarChar, 8000, "CONTRA"));
                    dbCommand.Parameters.Add(new SqlParameter("@ORIGEM", SqlDbType.VarChar, 8000, "ORIGEM"));
                    dbCommand.Parameters.Add(new SqlParameter("@FILIAL", SqlDbType.VarChar, 8000, "FILIAL"));
                    dbCommand.Parameters.Add(new SqlParameter("@PRACAREF", SqlDbType.VarChar, 8000, "PRACAREF"));
                    dbCommand.Parameters.Add(new SqlParameter("@DISTRREF", SqlDbType.VarChar, 8000, "DISTRREF"));
                    dbCommand.Parameters.Add(new SqlParameter("@VARAREF", SqlDbType.VarChar, 8000, "VARAREF"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATASUBREF", SqlDbType.VarChar, 8000, "DATASUBREF"));
                    dbCommand.Parameters.Add(new SqlParameter("@PROCREF", SqlDbType.VarChar, 8000, "PROCREF"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDNATUREF", SqlDbType.VarChar, 8000, "CDNATUREF"));
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
                    dbCommand.Parameters["@DATAREF"].Value = DATAREF ?? "";
                    dbCommand.Parameters["@TITULOREF"].Value = TITULOREF ?? "";
                    dbCommand.Parameters["@AVALREF"].Value = AVALREF ?? "";
                    dbCommand.Parameters["@VALOR"].Value = VALOR ?? "";
                    dbCommand.Parameters["@CONTRA"].Value = CONTRA ?? "";
                    dbCommand.Parameters["@ORIGEM"].Value = ORIGEM ?? "";
                    dbCommand.Parameters["@FILIAL"].Value = FILIAL ?? "";
                    dbCommand.Parameters["@PRACAREF"].Value = PRACAREF ?? "";
                    dbCommand.Parameters["@DISTRREF"].Value = DISTRREF ?? "";
                    dbCommand.Parameters["@VARAREF"].Value = VARAREF ?? "";
                    dbCommand.Parameters["@DATASUBREF"].Value = DATASUBREF ?? "";
                    dbCommand.Parameters["@PROCREF"].Value = PROCREF ?? "";
                    dbCommand.Parameters["@CDNATUREF"].Value = CDNATUREF ?? "";
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