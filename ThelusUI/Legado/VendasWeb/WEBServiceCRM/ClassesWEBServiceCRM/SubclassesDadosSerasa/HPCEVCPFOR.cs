using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class HPCEVCPFOR : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

       //[JsonProperty("ANO-EVO")]
        public string ANOEVO { get; set; }

       //[JsonProperty("MES-EVO")]
        public string MESEVO { get; set; }

       //[JsonProperty("MES-DESE")]
        public string MESDESE { get; set; }

       //[JsonProperty("COD-FAIXA-VENC")]
        public string CODFAIXAVENC { get; set; }

       //[JsonProperty("DESCR-FAIXA-VENC")]
        public string DESCRFAIXAVENC { get; set; }

       //[JsonProperty("VLR-FAIXA-DE-VENC")]
        public string VLRFAIXADEVENC { get; set; }

       //[JsonProperty("VLR-FAIXA-ATE-VENC")]
        public string VLRFAIXAATEVENC { get; set; }

       //[JsonProperty("COD-FAIXA-AVEN")]
        public string CODFAIXAAVEN { get; set; }

       //[JsonProperty("DESCR-FAIXA-AVEN")]
        public string DESCRFAIXAAVEN { get; set; }

       //[JsonProperty("VLR-FAIXA-DE-AVEN")]
        public string VLRFAIXADEAVEN { get; set; }

       //[JsonProperty("VLR-FAIXA-ATE-AVEN")]
        public string VLRFAIXAATEAVEN { get; set; }

       //[JsonProperty("RESERVADO-SERASA    ")]
        public string RESERVADOSERASA { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_EVOL_COMPROMISSO_FOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@ANOEVO", SqlDbType.VarChar, 8000, "ANO-EVO"));
                    dbCommand.Parameters.Add(new SqlParameter("@MESEVO", SqlDbType.VarChar, 8000, "MES-EVO"));
                    dbCommand.Parameters.Add(new SqlParameter("@MESDESE", SqlDbType.VarChar, 8000, "MES-DESE"));
                    dbCommand.Parameters.Add(new SqlParameter("@CODFAIXAVENC", SqlDbType.VarChar, 8000, "COD-FAIXA-VENC"));
                    dbCommand.Parameters.Add(new SqlParameter("@DESCRFAIXAVENC", SqlDbType.VarChar, 8000, "DESCR-FAIXA-VENC"));
                    dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXADEVENC", SqlDbType.VarChar, 8000, "VLR-FAIXA-DE-VENC"));
                    dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXAATEVENC", SqlDbType.VarChar, 8000, "VLR-FAIXA-ATE-VENC"));
                    dbCommand.Parameters.Add(new SqlParameter("@CODFAIXAAVEN", SqlDbType.VarChar, 8000, "COD-FAIXA-AVEN"));
                    dbCommand.Parameters.Add(new SqlParameter("@DESCRFAIXAAVEN", SqlDbType.VarChar, 8000, "DESCR-FAIXA-AVEN"));
                    dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXADEAVEN", SqlDbType.VarChar, 8000, "VLR-FAIXA-DE-AVEN"));
                    dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXAATEAVEN", SqlDbType.VarChar, 8000, "VLR-FAIXA-ATE-AVEN"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADO-SERASA"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@ANOEVO"].Value = ANOEVO ?? "";
                    dbCommand.Parameters["@MESEVO"].Value = MESEVO ?? "";
                    dbCommand.Parameters["@MESDESE"].Value = MESDESE ?? "";
                    dbCommand.Parameters["@CODFAIXAVENC"].Value = CODFAIXAVENC ?? "";
                    dbCommand.Parameters["@DESCRFAIXAVENC"].Value = DESCRFAIXAVENC ?? "";
                    dbCommand.Parameters["@VLRFAIXADEVENC"].Value = VLRFAIXADEVENC ?? "";
                    dbCommand.Parameters["@VLRFAIXAATEVENC"].Value = VLRFAIXAATEVENC ?? "";
                    dbCommand.Parameters["@CODFAIXAAVEN"].Value = CODFAIXAAVEN ?? "";
                    dbCommand.Parameters["@DESCRFAIXAAVEN"].Value = DESCRFAIXAAVEN ?? "";
                    dbCommand.Parameters["@VLRFAIXADEAVEN"].Value = VLRFAIXADEAVEN ?? "";
                    dbCommand.Parameters["@VLRFAIXAATEAVEN"].Value = VLRFAIXAATEAVEN ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA"].Value = RESERVADOSERASA ?? "";

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