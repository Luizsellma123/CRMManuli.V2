using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class CONCENTERCHSF : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

       //[JsonProperty("OCOR-ACHEI")]
        public string OCORACHEI { get; set; }

       //[JsonProperty("DATA-ACHEI")]
        public string DATAACHEI { get; set; }

       //[JsonProperty("CHEQUE-ACHEI")]
        public string CHEQUEACHEI { get; set; }
        public string ALIN { get; set; }

       //[JsonProperty("QTDE-ACHEI")]
        public string QTDEACHEI { get; set; }

       //[JsonProperty("MOED-ACHEI")]
        public string MOEDACHEI { get; set; }

       //[JsonProperty("VALO-ACHEI")]
        public string VALOACHEI { get; set; }

       //[JsonProperty("BANCO-ACHEI")]
        public string BANCOACHEI { get; set; }

       //[JsonProperty("AGENC-ACHEI")]
        public string AGENCACHEI { get; set; }

       //[JsonProperty("CIDA-ACHEI")]
        public string CIDAACHEI { get; set; }

       //[JsonProperty("UF-ACHEI")]
        public string UFACHEI { get; set; }

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

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CHEQUE_SEM_FUNDO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@OCOR_ACHEI", SqlDbType.VarChar, 8000, "OCOR_ACHEI"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATA_ACHEI", SqlDbType.VarChar, 8000, "DATA_ACHEI"));
                    dbCommand.Parameters.Add(new SqlParameter("@CHEQUE_ACHEI", SqlDbType.VarChar, 8000, "CHEQUE_ACHEI"));
                    dbCommand.Parameters.Add(new SqlParameter("@ALIN", SqlDbType.VarChar, 8000, "ALIN"));
                    dbCommand.Parameters.Add(new SqlParameter("@QTDE_ACHEI", SqlDbType.VarChar, 8000, "QTDE_ACHEI"));
                    dbCommand.Parameters.Add(new SqlParameter("@MOED_ACHEI", SqlDbType.VarChar, 8000, "MOED_ACHEI"));
                    dbCommand.Parameters.Add(new SqlParameter("@VALO_ACHEI", SqlDbType.VarChar, 8000, "VALO_ACHEI"));
                    dbCommand.Parameters.Add(new SqlParameter("@BANCO_ACHEI", SqlDbType.VarChar, 8000, "BANCO_ACHEI"));
                    dbCommand.Parameters.Add(new SqlParameter("@AGENC_ACHEI", SqlDbType.VarChar, 8000, "AGENC_ACHEI"));
                    dbCommand.Parameters.Add(new SqlParameter("@CIDA_ACHEI", SqlDbType.VarChar, 8000, "CIDA_ACHEI"));
                    dbCommand.Parameters.Add(new SqlParameter("@UF_ACHEI", SqlDbType.VarChar, 8000, "UF_ACHEI"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDNATU_ACHEI", SqlDbType.VarChar, 8000, "CDNATU_ACHEI"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADO_SERASA", SqlDbType.VarChar, 8000, "RESERVADO_SERASA"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@OCOR_ACHEI"].Value = OCORACHEI ?? "";
                    dbCommand.Parameters["@DATA_ACHEI"].Value = DATAACHEI ?? "";
                    dbCommand.Parameters["@CHEQUE_ACHEI"].Value = CHEQUEACHEI ?? "";
                    dbCommand.Parameters["@ALIN"].Value = ALIN ?? "";
                    dbCommand.Parameters["@QTDE_ACHEI"].Value = QTDEACHEI ?? "";
                    dbCommand.Parameters["@MOED_ACHEI"].Value = MOEDACHEI ?? "";
                    dbCommand.Parameters["@VALO_ACHEI"].Value = VALOACHEI ?? "";
                    dbCommand.Parameters["@BANCO_ACHEI"].Value = BANCOACHEI ?? "";
                    dbCommand.Parameters["@AGENC_ACHEI"].Value = AGENCACHEI ?? "";
                    dbCommand.Parameters["@CIDA_ACHEI"].Value = CIDAACHEI ?? "";
                    dbCommand.Parameters["@UF_ACHEI"].Value = UFACHEI ?? "";
                    dbCommand.Parameters["@CDNATU_ACHEI"].Value = CDNATUACHEI ?? "";
                    dbCommand.Parameters["@RESERVADO_SERASA"].Value = RESERVADOSERASA ?? "";

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