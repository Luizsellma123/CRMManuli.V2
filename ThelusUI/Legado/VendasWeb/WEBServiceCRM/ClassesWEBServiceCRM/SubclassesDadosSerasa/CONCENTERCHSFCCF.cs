using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class CONCENTERCHSFCCF : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string OCOR { get; set; }

       //[JsonProperty("DATA-CCF")]
        public string DATACCF { get; set; }
        public string CHEQUE { get; set; }
        public string QTDE { get; set; }

       //[JsonProperty("BANCO-CCF")]
        public string BANCOCCF { get; set; }

       //[JsonProperty("AGENC-CCF")]
        public string AGENCCCF { get; set; }

       //[JsonProperty("CIDA-CCF")]
        public string CIDACCF { get; set; }

       //[JsonProperty("UF-CCF")]
        public string UFCCF { get; set; }

       //[JsonProperty("CDNATU-ACHEI")]
        public string CDNATUACHEI { get; set; }

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

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CHEQUE_SEM_FUNDO_CCF", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@OCOR", SqlDbType.VarChar, 8000, "OCOR"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATACCF", SqlDbType.VarChar, 8000, "DATACCF"));
                    dbCommand.Parameters.Add(new SqlParameter("@CHEQUE", SqlDbType.VarChar, 8000, "CHEQUE"));
                    dbCommand.Parameters.Add(new SqlParameter("@QTDE", SqlDbType.VarChar, 8000, "QTDE"));
                    dbCommand.Parameters.Add(new SqlParameter("@BANCOCCF", SqlDbType.VarChar, 8000, "BANCOCCF"));
                    dbCommand.Parameters.Add(new SqlParameter("@AGENCCCF", SqlDbType.VarChar, 8000, "AGENCCCF"));
                    dbCommand.Parameters.Add(new SqlParameter("@CIDACCF", SqlDbType.VarChar, 8000, "CIDACCF"));
                    dbCommand.Parameters.Add(new SqlParameter("@UFCCF", SqlDbType.VarChar, 8000, "UFCCF"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDNATUACHEI", SqlDbType.VarChar, 8000, "CDNATUACHEI"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@OCOR"].Value = OCOR ?? "";
                    dbCommand.Parameters["@DATACCF"].Value = DATACCF ?? "";
                    dbCommand.Parameters["@CHEQUE"].Value = CHEQUE ?? "";
                    dbCommand.Parameters["@QTDE"].Value = QTDE ?? "";
                    dbCommand.Parameters["@BANCOCCF"].Value = BANCOCCF ?? "";
                    dbCommand.Parameters["@AGENCCCF"].Value = AGENCCCF ?? "";
                    dbCommand.Parameters["@CIDACCF"].Value = CIDACCF ?? "";
                    dbCommand.Parameters["@UFCCF"].Value = UFCCF ?? "";
                    dbCommand.Parameters["@CDNATUACHEI"].Value = CDNATUACHEI ?? "";
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