using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class HPCTOTEVCMP1 : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string DESCRICAO { get; set; }

        //[JsonProperty("TOT-COD-FAIXA-VV")]
        public string TOTCODFAIXAVV { get; set; }

        //[JsonProperty("TOT-DESC-FAIXA-VV")]
        public string TOTDESCFAIXAVV { get; set; }

        //[JsonProperty("TOT-VLR-FAIXA-DE-VV")]
        public string TOTVLRFAIXADEVV { get; set; }

        //[JsonProperty("TOT-VLR-FAIXA-ATE-VV")]
        public string TOTVLRFAIXAATEVV { get; set; }

        //[JsonProperty("TOT-COD-FAIXA-AV")]
        public string TOTCODFAIXAAV { get; set; }

        //[JsonProperty("TOT-DESC-FAIXA-AV")]
        public string TOTDESCFAIXAAV { get; set; }

        //[JsonProperty("TOT-VLR-FAIXA-DE-AV")]
        public string TOTVLRFAIXADEAV { get; set; }

        //[JsonProperty("TOT-VLR-FAIXA-ATE-AV")]
        public string TOTVLRFAIXAATEAV { get; set; }

        //[JsonProperty("TOT-COD-FXA-TM")]
        public string TOTCODFXATM { get; set; }

        //[JsonProperty("TOT-DES-FXA-TM")]
        public string TOTDESFXATM { get; set; }

        //[JsonProperty("TOT-VLR-FXA-DE-TM")]
        public string TOTVLRFXADETM { get; set; }

        //[JsonProperty("TOT-VLR-FXA-ATE-TM")]
        public string TOTVLRFXAATETM { get; set; }

        //[JsonProperty("SEG-INFO")]
        public string SEGINFO { get; set; }

        //[JsonProperty("SUB-GRUPO")]
        public string SUBGRUPO { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_EVOL_COMPROMISSO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DESCRICAO", SqlDbType.VarChar, 8000, "DESCRICAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODFAIXAVV", SqlDbType.VarChar, 8000, "TOTCODFAIXAVV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCFAIXAVV", SqlDbType.VarChar, 8000, "TOTDESCFAIXAVV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXADEVV", SqlDbType.VarChar, 8000, "TOTVLRFAIXADEVV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXAATEVV", SqlDbType.VarChar, 8000, "TOTVLRFAIXAATEVV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODFAIXAAV", SqlDbType.VarChar, 8000, "TOTCODFAIXAAV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCFAIXAAV", SqlDbType.VarChar, 8000, "TOTDESCFAIXAAV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXADEAV", SqlDbType.VarChar, 8000, "TOTVLRFAIXADEAV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFAIXAATEAV", SqlDbType.VarChar, 8000, "TOTVLRFAIXAATEAV"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODFXATM", SqlDbType.VarChar, 8000, "TOTCODFXATM"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESFXATM", SqlDbType.VarChar, 8000, "TOTDESFXATM"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFXADETM", SqlDbType.VarChar, 8000, "TOTVLRFXADETM"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTVLRFXAATETM", SqlDbType.VarChar, 8000, "TOTVLRFXAATETM"));
                    dbCommand.Parameters.Add(new SqlParameter("@SEGINFO", SqlDbType.VarChar, 8000, "SEGINFO"));
                    dbCommand.Parameters.Add(new SqlParameter("@SUBGRUPO", SqlDbType.VarChar, 8000, "SUBGRUPO"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@DESCRICAO"].Value = DESCRICAO ?? "";
                    dbCommand.Parameters["@TOTCODFAIXAVV"].Value = TOTCODFAIXAVV ?? "";
                    dbCommand.Parameters["@TOTDESCFAIXAVV"].Value = TOTDESCFAIXAVV ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXADEVV"].Value = TOTVLRFAIXADEVV ?? "0";
                    dbCommand.Parameters["@TOTVLRFAIXAATEVV"].Value = TOTVLRFAIXAATEVV ?? "0";
                    dbCommand.Parameters["@TOTCODFAIXAAV"].Value = TOTCODFAIXAAV ?? "";
                    dbCommand.Parameters["@TOTDESCFAIXAAV"].Value = TOTDESCFAIXAAV ?? "";
                    dbCommand.Parameters["@TOTVLRFAIXADEAV"].Value = TOTVLRFAIXADEAV ?? "0";
                    dbCommand.Parameters["@TOTVLRFAIXAATEAV"].Value = TOTVLRFAIXAATEAV ?? "0";
                    dbCommand.Parameters["@TOTCODFXATM"].Value = TOTCODFXATM ?? "";
                    dbCommand.Parameters["@TOTDESFXATM"].Value = TOTDESFXATM ?? "";
                    dbCommand.Parameters["@TOTVLRFXADETM"].Value = TOTVLRFXADETM ?? "0";
                    dbCommand.Parameters["@TOTVLRFXAATETM"].Value = TOTVLRFXAATETM ?? "0";
                    dbCommand.Parameters["@SEGINFO"].Value = SEGINFO ?? "0";
                    dbCommand.Parameters["@SUBGRUPO"].Value = SUBGRUPO ?? "0";

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