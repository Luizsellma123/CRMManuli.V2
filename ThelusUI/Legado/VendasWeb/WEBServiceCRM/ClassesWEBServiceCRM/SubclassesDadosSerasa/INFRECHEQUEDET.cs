using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class INFRECHEQUEDET : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string DATA { get; set; }

        //[JsonProperty("BANCO-RECH")]
        public string BANCORECH { get; set; }

        //[JsonProperty("AGENC-RECH")]
        public string AGENCRECH { get; set; }

        public string CONTA { get; set; }
        public string DGCON { get; set; }
        public string CHINI { get; set; }
        public string CHFIN { get; set; }
        public string MOTIVO { get; set; }

        //[JsonProperty("CONTA 12")]
        public string CONTA12 { get; set; }

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

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_RECHEQUE_DETALHES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DATA", SqlDbType.VarChar, 8000, "DATA"));
                    dbCommand.Parameters.Add(new SqlParameter("@BANCORECH", SqlDbType.VarChar, 8000, "BANCORECH"));
                    dbCommand.Parameters.Add(new SqlParameter("@AGENCRECH", SqlDbType.VarChar, 8000, "AGENCRECH"));
                    dbCommand.Parameters.Add(new SqlParameter("@CONTA", SqlDbType.VarChar, 8000, "CONTA"));
                    dbCommand.Parameters.Add(new SqlParameter("@DGCON", SqlDbType.VarChar, 8000, "DGCON"));
                    dbCommand.Parameters.Add(new SqlParameter("@CHINI", SqlDbType.VarChar, 8000, "CHINI"));
                    dbCommand.Parameters.Add(new SqlParameter("@CHFIN", SqlDbType.VarChar, 8000, "CHFIN"));
                    dbCommand.Parameters.Add(new SqlParameter("@MOTIVO", SqlDbType.VarChar, 8000, "MOTIVO"));
                    dbCommand.Parameters.Add(new SqlParameter("@CONTA12", SqlDbType.VarChar, 8000, "CONTA12"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJCPF", SqlDbType.VarChar, 8000, "CNPJCPF"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@DATA"].Value = DATA ?? "";
                    dbCommand.Parameters["@BANCORECH"].Value = BANCORECH ?? "";
                    dbCommand.Parameters["@AGENCRECH"].Value = AGENCRECH ?? "";
                    dbCommand.Parameters["@CONTA"].Value = CONTA ?? "";
                    dbCommand.Parameters["@DGCON"].Value = DGCON ?? "";
                    dbCommand.Parameters["@CHINI"].Value = CHINI ?? "";
                    dbCommand.Parameters["@CHFIN"].Value = CHFIN ?? "";
                    dbCommand.Parameters["@MOTIVO"].Value = MOTIVO ?? "";
                    dbCommand.Parameters["@CONTA12"].Value = CONTA12 ?? "";
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