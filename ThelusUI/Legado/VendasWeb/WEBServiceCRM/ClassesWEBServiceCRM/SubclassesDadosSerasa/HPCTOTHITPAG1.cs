using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class HPCTOTHITPAG1 : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string DESCRICAO { get; set; }

       //[JsonProperty("TOT-COD-FAIXA")]
        public string TOTCODFAIXA { get; set; }

       //[JsonProperty("TOT-DESC-FAIXA")]
        public string TOTDESCFAIXA { get; set; }

       //[JsonProperty("TOT-VLR-FAIXA-DE")]
        public string TOTVLRFAIXADE { get; set; }

       //[JsonProperty("TOT-VLR-FAIXA-ATE")]
        public string TOTVLRFAIXAATE { get; set; }

       //[JsonProperty("TOT-COD-MED-FAIXA")]
        public string TOTCODMEDFAIXA { get; set; }

       //[JsonProperty("TOT-DESC-MED-FAIXA")]
        public string TOTDESCMEDFAIXA { get; set; }

       //[JsonProperty("TOT-MED-FAIXA-DE")]
        public string TOTMEDFAIXADE { get; set; }

       //[JsonProperty("TOT-MED-FAIXA-ATE")]
        public string TOTMEDFAIXAATE { get; set; }

       //[JsonProperty("TOT-PERC-FAIXA-DE")]
        public string TOTPERCFAIXADE { get; set; }

       //[JsonProperty("TOT-PERC-FAIXA-ATE")]
        public string TOTPERCFAIXAATE { get; set; }

       //[JsonProperty("RESERVADO-SERASA")]
        public string RESERVADOSERASA { get; set; }

       //[JsonProperty("SEG-INFO")]
        public string SEGINFO { get; set; }

       //[JsonProperty("SUB-GRUPO")]
        public string SUBGRUPO { get; set; }

       //[JsonProperty("TOT-AVISTA")]
        public string TOTAVISTA { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_HIST_PAGAMENTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DESCRICAO", SqlDbType.VarChar, 8000, "DESCRICAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODFAIXA", SqlDbType.VarChar, 8000, "TOTCODFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCFAIXA", SqlDbType.VarChar, 8000, "TOTDESCFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXADE", SqlDbType.VarChar, 8000, "TOTVLRFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXAATE", SqlDbType.VarChar, 8000, "TOTVLRFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODMEDFAIXA", SqlDbType.VarChar, 8000, "TOTCODMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCMEDFAIXA", SqlDbType.VarChar, 8000, "TOTDESCMEDFAIXA"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXADE", SqlDbType.VarChar, 8000, "TOTMEDFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTMEDFAIXAATE", SqlDbType.VarChar, 8000, "TOTMEDFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXADE", SqlDbType.VarChar, 8000, "TOTPERCFAIXADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTPERCFAIXAATE", SqlDbType.VarChar, 8000, "TOTPERCFAIXAATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                    dbCommand.Parameters.Add(new SqlParameter("@SEGINFO", SqlDbType.VarChar, 8000, "SEGINFO"));
                    dbCommand.Parameters.Add(new SqlParameter("@SUBGRUPO", SqlDbType.VarChar, 8000, "SUBGRUPO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTAVISTA", SqlDbType.VarChar, 8000, "TOTAVISTA"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@DESCRICAO"].Value = DESCRICAO ?? "";
                    dbCommand.Parameters["@TOTCODFAIXA"].Value = TOTCODFAIXA ?? "";
                    dbCommand.Parameters["@TOTDESCFAIXA"].Value = TOTDESCFAIXA ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXADE"].Value = TOTVLRFAIXADE ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXAATE"].Value = TOTVLRFAIXAATE ?? "";
                    dbCommand.Parameters["@TOTCODMEDFAIXA"].Value = TOTCODMEDFAIXA ?? "";
                    dbCommand.Parameters["@TOTDESCMEDFAIXA"].Value = TOTDESCMEDFAIXA ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXADE"].Value = TOTMEDFAIXADE ?? "";
                    dbCommand.Parameters["@TOTMEDFAIXAATE"].Value = TOTMEDFAIXAATE ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXADE"].Value = TOTPERCFAIXADE ?? "";
                    dbCommand.Parameters["@TOTPERCFAIXAATE"].Value = TOTPERCFAIXAATE ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA"].Value = RESERVADOSERASA ?? "";
                    dbCommand.Parameters["@SEGINFO"].Value = SEGINFO ?? "";
                    dbCommand.Parameters["@SUBGRUPO"].Value = SUBGRUPO ?? "";
                    dbCommand.Parameters["@TOTAVISTA"].Value = TOTAVISTA ?? "";

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