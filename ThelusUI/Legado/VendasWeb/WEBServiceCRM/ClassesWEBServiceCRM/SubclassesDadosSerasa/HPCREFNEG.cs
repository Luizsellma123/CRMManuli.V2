using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class HPCREFNEG : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string POTENC { get; set; }
        public string AAAAMM { get; set; }

       //[JsonProperty("COD-FAIXA-POT")]
        public string CODFAIXAPOT { get; set; }

       //[JsonProperty("DESCR-FAIXA-POT")]
        public string DESCRFAIXAPOT { get; set; }

       //[JsonProperty("VLR-FAIXA-DE-POT")]
        public string VLRFAIXADEPOT { get; set; }

       //[JsonProperty("VLR-FAIXA-ATE-POT")]
        public string VLRFAIXAATEPOT { get; set; }

       //[JsonProperty("COD-FAIXA-MED")]
        public string CODFAIXAMED { get; set; }

       //[JsonProperty("DESCR-FAIXA-MED")]
        public string DESCRFAIXAMED { get; set; }

       //[JsonProperty("VLR-FAIXA-DE-MED")]
        public string VLRFAIXADEMED { get; set; }

       //[JsonProperty("VLR-FAIXA-ATÉ-MED")]
        public string VLRFAIXAATEMED { get; set; }
        public string SEG0INFO { get; set; }

       //[JsonProperty("RESERVADO-SERASA")]
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

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_REFERENCIAIS_NEGOCIOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@POTENC", SqlDbType.VarChar, 8000, "POTENC"));
                    dbCommand.Parameters.Add(new SqlParameter("@AAAAMM", SqlDbType.VarChar, 8000, "AAAAMM"));
                    dbCommand.Parameters.Add(new SqlParameter("@CODFAIXAPOT", SqlDbType.VarChar, 8000, "CODFAIXAPOT"));
                    dbCommand.Parameters.Add(new SqlParameter("@DESCRFAIXAPOT", SqlDbType.VarChar, 8000, "DESCRFAIXAPOT"));
                    dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXADEPOT", SqlDbType.VarChar, 8000, "VLRFAIXADEPOT"));
                    dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXAATEPOT", SqlDbType.VarChar, 8000, "VLRFAIXAATEPOT"));
                    dbCommand.Parameters.Add(new SqlParameter("@CODFAIXAMED", SqlDbType.VarChar, 8000, "CODFAIXAMED"));
                    dbCommand.Parameters.Add(new SqlParameter("@DESCRFAIXAMED", SqlDbType.VarChar, 8000, "DESCRFAIXAMED"));
                    dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXADEMED", SqlDbType.VarChar, 8000, "VLRFAIXADEMED"));
                    dbCommand.Parameters.Add(new SqlParameter("@VLRFAIXAATEMED", SqlDbType.VarChar, 8000, "VLRFAIXAATEMED"));
                    dbCommand.Parameters.Add(new SqlParameter("@SEG0INFO", SqlDbType.VarChar, 8000, "SEG0INFO"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADO-SERASA"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@POTENC"].Value = POTENC ?? "";
                    dbCommand.Parameters["@AAAAMM"].Value = AAAAMM ?? "";
                    dbCommand.Parameters["@CODFAIXAPOT"].Value = CODFAIXAPOT ?? "";
                    dbCommand.Parameters["@DESCRFAIXAPOT"].Value = DESCRFAIXAPOT ?? "";
                    dbCommand.Parameters["@VLRFAIXADEPOT"].Value = VLRFAIXADEPOT ?? "";
                    dbCommand.Parameters["@VLRFAIXAATEPOT"].Value = VLRFAIXAATEPOT ?? "";
                    dbCommand.Parameters["@CODFAIXAMED"].Value = CODFAIXAMED ?? "";
                    dbCommand.Parameters["@DESCRFAIXAMED"].Value = DESCRFAIXAMED ?? "";
                    dbCommand.Parameters["@VLRFAIXADEMED"].Value = VLRFAIXADEMED ?? "";
                    dbCommand.Parameters["@VLRFAIXAATEMED"].Value = VLRFAIXAATEMED ?? "";
                    dbCommand.Parameters["@SEG0INFO"].Value = SEG0INFO ?? "";
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