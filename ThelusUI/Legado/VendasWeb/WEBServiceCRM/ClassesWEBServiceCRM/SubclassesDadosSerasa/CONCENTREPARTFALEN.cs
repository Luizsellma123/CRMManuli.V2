using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class CONCENTREPARTFALEN : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

       //[JsonProperty("OCOR-PIE")]
        public string OCORPIE { get; set; }

       //[JsonProperty("DATA-PIE")]
        public string DATAPIE { get; set; }

       //[JsonProperty("TIPO-PIE")]
        public string TIPOPIE { get; set; }
        public string CNPJ { get; set; }

       //[JsonProperty("EMPRESA-PIE")]
        public string EMPRESAPIE { get; set; }

       //[JsonProperty("CDNATU-PIE")]
        public string CDNATUPIE { get; set; }

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

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_PARTICIPACAO_FALENCIA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@OCORPIE", SqlDbType.VarChar, 8000, "OCORPIE"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAPIE", SqlDbType.VarChar, 8000, "DATAPIE"));
                    dbCommand.Parameters.Add(new SqlParameter("@TIPOPIE", SqlDbType.VarChar, 8000, "TIPOPIE"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.VarChar, 8000, "CNPJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@EMPRESAPIE", SqlDbType.VarChar, 8000, "EMPRESAPIE"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDNATUPIE", SqlDbType.VarChar, 8000, "CDNATUPIE"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@OCORPIE"].Value = OCORPIE ?? "";
                    dbCommand.Parameters["@DATAPIE"].Value = DATAPIE ?? "";
                    dbCommand.Parameters["@TIPOPIE"].Value = TIPOPIE ?? "";
                    dbCommand.Parameters["@CNPJ"].Value = CNPJ ?? "";
                    dbCommand.Parameters["@EMPRESAPIE"].Value = EMPRESAPIE ?? "";
                    dbCommand.Parameters["@CDNATUPIE"].Value = CDNATUPIE ?? "";
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