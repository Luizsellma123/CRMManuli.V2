using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class IDENTIFICACAO : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string RAZAO { get; set; }

        public string CDCGCR { get; set; }

        //[JsonProperty("NOME FANTASIA")]
        public string NOMEFANTASIA { get; set; }

        public string NIRE { get; set; }

        //[JsonProperty("TP SOC")]
        public string TPSOC { get; set; }

        //[JsonProperty("OPCAO-TRIBUTÁRIA")]
        public string OPCAOTRIBUTARIA { get; set; }

        public string CDTPSC { get; set; }
       
        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_IDENTIFICACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@RAZAO", SqlDbType.VarChar, 8000, "RAZAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDCGCR", SqlDbType.VarChar, 8000, "CDCGCR"));
                    dbCommand.Parameters.Add(new SqlParameter("@NOMEFANTASIA", SqlDbType.VarChar, 8000, "NOMEFANTASIA"));
                    dbCommand.Parameters.Add(new SqlParameter("@NIRE", SqlDbType.VarChar, 8000, "NIRE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPSOC", SqlDbType.VarChar, 8000, "TPSOC"));
                    dbCommand.Parameters.Add(new SqlParameter("@OPCAOTRIBUTARIA", SqlDbType.VarChar, 8000, "OPCAOTRIBUTARIA"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDTPSC", SqlDbType.VarChar, 8000, "CDTPSC"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";
                    dbCommand.Parameters["@RAZAO"].Value = RAZAO ?? "";
                    dbCommand.Parameters["@CDCGCR"].Value = CDCGCR ?? "";
                    dbCommand.Parameters["@NOMEFANTASIA"].Value = NOMEFANTASIA ?? "";
                    dbCommand.Parameters["@NIRE"].Value = NIRE ?? "";
                    dbCommand.Parameters["@TPSOC"].Value = TPSOC ?? "";
                    dbCommand.Parameters["@OPCAOTRIBUTARIA"].Value = OPCAOTRIBUTARIA ?? "";
                    dbCommand.Parameters["@CDTPSC"].Value = CDTPSC ?? "";

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