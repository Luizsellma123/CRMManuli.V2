using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class CONCENTREACAOJUD : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        //[JsonProperty("OCOR-ACAO")]
        public string OCORACAO { get; set; }

        //[JsonProperty("DATA-ACAO")]
        public string DATAACAO { get; set; }

        public string NATU { get; set; }

        //[JsonProperty("AVAL-ACAO")]
        public string AVALACAO { get; set; }

        //[JsonProperty("MOED-ACAO")]
        public string MOEDACAO { get; set; }

        //[JsonProperty("VALO-ACAO")]
        public string VALOACAO { get; set; }

        public string DIST { get; set; }

        //[JsonProperty("VARA-ACAO")]
        public string VARAACAO { get; set; }

        //[JsonProperty("CIDA-ACAO")]
        public string CIDAACAO { get; set; }

        //[JsonProperty("UF-ACAO")]
        public string UFACAO { get; set; }

        //[JsonProperty("PRACA-ACO")]
        public string PRACAACO { get; set; }

        //[JsonProperty("DISTR-ACO")]
        public string DISTRACO { get; set; }

        //[JsonProperty("VARA-ACO")]
        public string VARAACO { get; set; }

        //[JsonProperty("DATA-ACO")]
        public string DATAACO { get; set; }

        //[JsonProperty("PROC-ACO")]
        public string PROCACO { get; set; }

        //[JsonProperty("CDNATU-ACO")]
        public string CDNATUACO { get; set; }

        //[JsonProperty("RESERVADO-SERASA")]
        public string RESERVADOSERASA { get; set; }

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

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_ACAO_JUDICIAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@OCORACAO", SqlDbType.VarChar, 8000, "OCORACAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAACAO", SqlDbType.VarChar, 8000, "DATAACAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@NATU", SqlDbType.VarChar, 8000, "NATU"));
                    dbCommand.Parameters.Add(new SqlParameter("@AVALACAO", SqlDbType.VarChar, 8000, "AVALACAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@MOEDACAO", SqlDbType.VarChar, 8000, "MOEDACAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@VALOACAO", SqlDbType.VarChar, 8000, "VALOACAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@DIST", SqlDbType.VarChar, 8000, "DIST"));
                    dbCommand.Parameters.Add(new SqlParameter("@VARAACAO", SqlDbType.VarChar, 8000, "VARAACAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@CIDAACAO", SqlDbType.VarChar, 8000, "CIDAACAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@UFACAO", SqlDbType.VarChar, 8000, "UFACAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@PRACAACO", SqlDbType.VarChar, 8000, "PRACAACO"));
                    dbCommand.Parameters.Add(new SqlParameter("@DISTRACO", SqlDbType.VarChar, 8000, "DISTRACO"));
                    dbCommand.Parameters.Add(new SqlParameter("@VARAACO", SqlDbType.VarChar, 8000, "VARAACO"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAACO", SqlDbType.VarChar, 8000, "DATAACO"));
                    dbCommand.Parameters.Add(new SqlParameter("@PROCACO", SqlDbType.VarChar, 8000, "PROCACO"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDNATUACO", SqlDbType.VarChar, 8000, "CDNATUACO"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                    dbCommand.Parameters.Add(new SqlParameter("@MSGSUBJUD", SqlDbType.VarChar, 8000, "MSGSUBJUD"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA2", SqlDbType.VarChar, 8000, "RESERVADOSERASA2"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@OCORACAO"].Value = OCORACAO ?? "";
                    dbCommand.Parameters["@DATAACAO"].Value = DATAACAO ?? "";
                    dbCommand.Parameters["@NATU"].Value = NATU ?? "";
                    dbCommand.Parameters["@AVALACAO"].Value = AVALACAO ?? "";
                    dbCommand.Parameters["@MOEDACAO"].Value = MOEDACAO ?? "";
                    dbCommand.Parameters["@VALOACAO"].Value = VALOACAO ?? "";
                    dbCommand.Parameters["@DIST"].Value = DIST ?? "";
                    dbCommand.Parameters["@VARAACAO"].Value = VARAACAO ?? "";
                    dbCommand.Parameters["@CIDAACAO"].Value = CIDAACAO ?? "";
                    dbCommand.Parameters["@UFACAO"].Value = UFACAO ?? "";
                    dbCommand.Parameters["@PRACAACO"].Value = PRACAACO ?? "";
                    dbCommand.Parameters["@DISTRACO"].Value = DISTRACO ?? "";
                    dbCommand.Parameters["@VARAACO"].Value = VARAACO ?? "";
                    dbCommand.Parameters["@DATAACO"].Value = DATAACO ?? "";
                    dbCommand.Parameters["@PROCACO"].Value = PROCACO ?? "";
                    dbCommand.Parameters["@CDNATUACO"].Value = CDNATUACO ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA"].Value = RESERVADOSERASA ?? "";
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