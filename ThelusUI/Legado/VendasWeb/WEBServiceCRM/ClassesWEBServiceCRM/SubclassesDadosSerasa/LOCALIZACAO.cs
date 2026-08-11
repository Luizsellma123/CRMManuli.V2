using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class LOCALIZACAO : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string CIDADE { get; set; }

        public string UF { get; set; }

        public string CEP { get; set; }

        //[JsonProperty("CD-DDD")]
        public string CDDDD { get; set; }

        //[JsonProperty("NR-TEL1")]
        public string NRTEL1 { get; set; }

        //[JsonProperty("NR-FAX1")]
        public string NRFAX1 { get; set; }

        public string CDEB1 { get; set; }

        public string HOME { get; set; }

        public string EMAIL { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_LOCALIZACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@CIDADE", SqlDbType.VarChar, 8000, "CIDADE"));
                    dbCommand.Parameters.Add(new SqlParameter("@UF", SqlDbType.VarChar, 8000, "UF"));
                    dbCommand.Parameters.Add(new SqlParameter("@CEP", SqlDbType.VarChar, 8000, "CEP"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDDDD", SqlDbType.VarChar, 8000, "CDDDD"));
                    dbCommand.Parameters.Add(new SqlParameter("@NRTEL1", SqlDbType.VarChar, 8000, "NRTEL1"));
                    dbCommand.Parameters.Add(new SqlParameter("@NRFAX1", SqlDbType.VarChar, 8000, "NRFAX1"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDEB1", SqlDbType.VarChar, 8000, "CDEB1"));
                    dbCommand.Parameters.Add(new SqlParameter("@HOME", SqlDbType.VarChar, 8000, "HOME"));
                    dbCommand.Parameters.Add(new SqlParameter("@EMAIL", SqlDbType.VarChar, 8000, "EMAIL"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@CIDADE"].Value = CIDADE ?? "";
                    dbCommand.Parameters["@UF"].Value = UF ?? "";
                    dbCommand.Parameters["@CEP"].Value = CEP ?? "";
                    dbCommand.Parameters["@CDDDD"].Value = CDDDD ?? "";
                    dbCommand.Parameters["@NRTEL1"].Value = NRTEL1 ?? "";
                    dbCommand.Parameters["@NRFAX1"].Value = NRFAX1 ?? "";
                    dbCommand.Parameters["@CDEB1"].Value = CDEB1 ?? "";
                    dbCommand.Parameters["@HOME"].Value = HOME ?? "";
                    dbCommand.Parameters["@EMAIL"].Value = EMAIL ?? "";

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