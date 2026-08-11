using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class INFADSOCNQUSOCCMP : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string NMLG { get; set; }
        public string DSBR { get; set; }
        public string CDEBHG { get; set; }
        public string CDUF { get; set; }
        public string CDCE { get; set; }
        public string DDD { get; set; }
        public string FONE { get; set; }
        public string RAMAL { get; set; }
        public string RAMO { get; set; }
        public string CNPJCPF { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_INF_ADIC_SOC_COMP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@NMLG", SqlDbType.VarChar, 8000, "NMLG"));
                    dbCommand.Parameters.Add(new SqlParameter("@DSBR", SqlDbType.VarChar, 8000, "DSBR"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDEBHG", SqlDbType.VarChar, 8000, "CDEBHG"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDUF", SqlDbType.VarChar, 8000, "CDUF"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDCE", SqlDbType.VarChar, 8000, "CDCE"));
                    dbCommand.Parameters.Add(new SqlParameter("@DDD", SqlDbType.VarChar, 8000, "DDD"));
                    dbCommand.Parameters.Add(new SqlParameter("@FONE", SqlDbType.VarChar, 8000, "FONE"));
                    dbCommand.Parameters.Add(new SqlParameter("@RAMAL", SqlDbType.VarChar, 8000, "RAMAL"));
                    dbCommand.Parameters.Add(new SqlParameter("@RAMO", SqlDbType.VarChar, 8000, "RAMO"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJCPF", SqlDbType.VarChar, 8000, "CNPJCPF"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@NMLG"].Value = NMLG ?? "";
                    dbCommand.Parameters["@DSBR"].Value = DSBR ?? "";
                    dbCommand.Parameters["@CDEBHG"].Value = CDEBHG ?? "";
                    dbCommand.Parameters["@CDUF"].Value = CDUF ?? "";
                    dbCommand.Parameters["@CDCE"].Value = CDCE ?? "";
                    dbCommand.Parameters["@DDD"].Value = DDD ?? "";
                    dbCommand.Parameters["@FONE"].Value = FONE ?? "";
                    dbCommand.Parameters["@RAMAL"].Value = RAMAL ?? "";
                    dbCommand.Parameters["@RAMO"].Value = RAMO ?? "";
                    dbCommand.Parameters["@CNPJCPF"].Value = CNPJCPF ?? "";
                    
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