using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class INFADICSOCIOS : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string CPF { get; set; }
        public string SQCPF { get; set; }
        public string DGCPF { get; set; }
        public string DTATU { get; set; }
        public string NMPF { get; set; }
        public string NRRGGL { get; set; }
        public string DTNS { get; set; }

       //[JsonProperty("VÍNCULO")]
        public string VINCULO { get; set; }
        public string CDEBNSHG { get; set; }
        public string UFNS { get; set; }
        public string DDD { get; set; }
        public string FONE { get; set; }
        public string RAMAL { get; set; }
        public string NMLG { get; set; }
        public string DSBR { get; set; }
        public string CDEBHG { get; set; }
        public string CDUF { get; set; }
        public string CDCE { get; set; }

       //[JsonProperty("RESERVADO-SERASA")]
        public string RESERVADOSERASA { get; set; }
        public string SITUAC { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_INF_ADI_SOC", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@CPF", SqlDbType.VarChar, 8000, "CPF"));
                    dbCommand.Parameters.Add(new SqlParameter("@SQCPF", SqlDbType.VarChar, 8000, "SQCPF"));
                    dbCommand.Parameters.Add(new SqlParameter("@DGCPF", SqlDbType.VarChar, 8000, "DGCPF"));
                    dbCommand.Parameters.Add(new SqlParameter("@DTATU", SqlDbType.VarChar, 8000, "DTATU"));
                    dbCommand.Parameters.Add(new SqlParameter("@NMPF", SqlDbType.VarChar, 8000, "NMPF"));
                    dbCommand.Parameters.Add(new SqlParameter("@NRRGGL", SqlDbType.VarChar, 8000, "NRRGGL"));
                    dbCommand.Parameters.Add(new SqlParameter("@DTNS", SqlDbType.VarChar, 8000, "DTNS"));
                    dbCommand.Parameters.Add(new SqlParameter("@VINCULO", SqlDbType.VarChar, 8000, "VINCULO"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDEBNSHG", SqlDbType.VarChar, 8000, "CDEBNSHG"));
                    dbCommand.Parameters.Add(new SqlParameter("@UFNS", SqlDbType.VarChar, 8000, "UFNS"));
                    dbCommand.Parameters.Add(new SqlParameter("@DDD", SqlDbType.VarChar, 8000, "DDD"));
                    dbCommand.Parameters.Add(new SqlParameter("@FONE", SqlDbType.VarChar, 8000, "FONE"));
                    dbCommand.Parameters.Add(new SqlParameter("@RAMAL", SqlDbType.VarChar, 8000, "RAMAL"));
                    dbCommand.Parameters.Add(new SqlParameter("@NMLG", SqlDbType.VarChar, 8000, "NMLG"));
                    dbCommand.Parameters.Add(new SqlParameter("@DSBR", SqlDbType.VarChar, 8000, "DSBR"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDEBHG", SqlDbType.VarChar, 8000, "CDEBHG"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDUF", SqlDbType.VarChar, 8000, "CDUF"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDCE", SqlDbType.VarChar, 8000, "CDCE"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADOSERASA", SqlDbType.VarChar, 8000, "RESERVADOSERASA"));
                    dbCommand.Parameters.Add(new SqlParameter("@SITUAC", SqlDbType.VarChar, 8000, "SITUAC"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@CPF"].Value = CPF ?? "";
                    dbCommand.Parameters["@SQCPF"].Value = SQCPF ?? "";
                    dbCommand.Parameters["@DGCPF"].Value = DGCPF ?? "";
                    dbCommand.Parameters["@DTATU"].Value = DTATU ?? "";
                    dbCommand.Parameters["@NMPF"].Value = NMPF ?? "";
                    dbCommand.Parameters["@NRRGGL"].Value = NRRGGL ?? "";
                    dbCommand.Parameters["@DTNS"].Value = DTNS ?? "";
                    dbCommand.Parameters["@VINCULO"].Value = VINCULO ?? "";
                    dbCommand.Parameters["@CDEBNSHG"].Value = CDEBNSHG ?? "";
                    dbCommand.Parameters["@UFNS"].Value = UFNS ?? "";
                    dbCommand.Parameters["@DDD"].Value = DDD ?? "";
                    dbCommand.Parameters["@FONE"].Value = FONE ?? "";
                    dbCommand.Parameters["@RAMAL"].Value = RAMAL ?? "";
                    dbCommand.Parameters["@NMLG"].Value = NMLG ?? "";
                    dbCommand.Parameters["@DSBR"].Value = DSBR ?? "";
                    dbCommand.Parameters["@CDEBHG"].Value = CDEBHG ?? "";
                    dbCommand.Parameters["@CDUF"].Value = CDUF ?? "";
                    dbCommand.Parameters["@CDCE"].Value = CDCE ?? "";
                    dbCommand.Parameters["@RESERVADOSERASA"].Value = RESERVADOSERASA ?? "";
                    dbCommand.Parameters["@SITUAC"].Value = SITUAC ?? "";

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