using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class HPCHISTPAG : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

       //[JsonProperty("DES-HIS")]
        public string DESHIS { get; set; }

       //[JsonProperty("TOT-COD-HIS")]
        public string TOTCODHIS { get; set; }

       //[JsonProperty("TOT-DESCR-HIS")]
        public string TOTDESCRHIS { get; set; }

       //[JsonProperty("TOT-QTD-HIS-DE")]
        public string TOTQTDHISDE { get; set; }

       //[JsonProperty("TOT-QTD-HIS-ATE")]
        public string TOTQTDHISATE { get; set; }

       //[JsonProperty("PERC-HIS-DE")]
        public string PERCHISDE { get; set; }

       //[JsonProperty("PERC-HIS-ATE")]
        public string PERCHISATE { get; set; }

       //[JsonProperty("SEG-INFO")]
        public string SEGINFO { get; set; }

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

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_HIST_PAG_QTDTIT", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DESHIS", SqlDbType.VarChar, 8000, "DESHIS"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTCODHIS", SqlDbType.VarChar, 8000, "TOTCODHIS"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTDESCRHIS", SqlDbType.VarChar, 8000, "TOTDESCRHIS"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTQTDHISDE", SqlDbType.VarChar, 8000, "TOTQTDHISDE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTQTDHISATE", SqlDbType.VarChar, 8000, "TOTQTDHISATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCHISDE", SqlDbType.VarChar, 8000, "PERCHISDE"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCHISATE", SqlDbType.VarChar, 8000, "PERCHISATE"));
                    dbCommand.Parameters.Add(new SqlParameter("@SEGINFO", SqlDbType.VarChar, 8000, "SEGINFO"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@DESHIS"].Value = DESHIS ?? "";
                    dbCommand.Parameters["@TOTCODHIS"].Value = TOTCODHIS ?? "";
                    dbCommand.Parameters["@TOTDESCRHIS"].Value = TOTDESCRHIS ?? "";
                    dbCommand.Parameters["@TOTQTDHISDE"].Value = TOTQTDHISDE ?? "";
                    dbCommand.Parameters["@TOTQTDHISATE"].Value = TOTQTDHISATE ?? "";
                    dbCommand.Parameters["@PERCHISDE"].Value = PERCHISDE ?? "";
                    dbCommand.Parameters["@PERCHISATE"].Value = PERCHISATE ?? "";
                    dbCommand.Parameters["@SEGINFO"].Value = SEGINFO ?? "";
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