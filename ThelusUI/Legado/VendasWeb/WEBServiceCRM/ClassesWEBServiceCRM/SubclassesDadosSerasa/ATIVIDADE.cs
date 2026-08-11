using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class ATIVIDADE : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        //[JsonProperty("DATA-FUND")]
        public string DATAFUND { get; set; }

        //[JsonProperty("DATA-CNPJ")]
        public string DATACNPJ { get; set; }

        //[JsonProperty("RAMO-ATV")]
        public string RAMOATV { get; set; }
        public string CDSA { get; set; }

        //[JsonProperty("NR-EMP")]
        public string NREMP { get; set; }

        //[JsonProperty("PC-COMPRA")]
        public string PCCOMPRA { get; set; }

        //[JsonProperty("PC-VENDAS")]
        public string PCVENDAS { get; set; }

        //[JsonProperty("NR-FIL")]
        public string NRFIL { get; set; }

        //[JsonProperty("QT-FIL")]
        public string QTFIL { get; set; }

        public string CNAE { get; set; }

        public string DTINDOPER { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_ATIVIDADE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DATAFUND", SqlDbType.VarChar, 8000, "DATAFUND"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATACNPJ", SqlDbType.VarChar, 8000, "DATACNPJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@RAMOATV", SqlDbType.VarChar, 8000, "RAMOATV"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDSA", SqlDbType.VarChar, 8000, "CDSA"));
                    dbCommand.Parameters.Add(new SqlParameter("@NREMP", SqlDbType.VarChar, 8000, "NREMP"));
                    dbCommand.Parameters.Add(new SqlParameter("@PCCOMPRA", SqlDbType.VarChar, 8000, "PCCOMPRA"));
                    dbCommand.Parameters.Add(new SqlParameter("@PCVENDAS", SqlDbType.VarChar, 8000, "PCVENDAS"));
                    dbCommand.Parameters.Add(new SqlParameter("@NRFIL", SqlDbType.VarChar, 8000, "NRFIL"));
                    dbCommand.Parameters.Add(new SqlParameter("@QTFIL", SqlDbType.VarChar, 8000, "QTFIL"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNAE", SqlDbType.VarChar, 8000, "CNAE"));
                    dbCommand.Parameters.Add(new SqlParameter("@DTINDOPER", SqlDbType.VarChar, 8000, "DTINDOPER"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@DATAFUND"].Value = DATAFUND ?? "";
                    dbCommand.Parameters["@DATACNPJ"].Value = DATACNPJ ?? "";
                    dbCommand.Parameters["@RAMOATV"].Value = RAMOATV ?? "";
                    dbCommand.Parameters["@CDSA"].Value = CDSA ?? "";
                    dbCommand.Parameters["@NREMP"].Value = NREMP ?? "";
                    dbCommand.Parameters["@PCCOMPRA"].Value = PCCOMPRA ?? "";
                    dbCommand.Parameters["@PCVENDAS"].Value = PCVENDAS ?? "";
                    dbCommand.Parameters["@NRFIL"].Value = NRFIL ?? "";
                    dbCommand.Parameters["@QTFIL"].Value = QTFIL ?? "";
                    dbCommand.Parameters["@CNAE"].Value = CNAE ?? "";
                    dbCommand.Parameters["@DTINDOPER"].Value = DTINDOPER ?? "";

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