using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class HPCRELFOR : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

       //[JsonProperty("FT-CONSULT")]
        public string FTCONSULT { get; set; }

       //[JsonProperty("FT-CONSULT-PERF")]
        public string FTCONSULTPERF { get; set; }

       //[JsonProperty("FT-CONSULT-EVOL")]
        public string FTCONSULTEVOL { get; set; }

       //[JsonProperty("FT-CONSUL-POTN")]
        public string FTCONSULPOTN { get; set; }

       //[JsonProperty("FT-CONSUL-POTV")]
        public string FTCONSULPOTV { get; set; }
        public string RESERVADO { get; set; }

       //[JsonProperty("FT-CONSULT-HIST")]
        public string FTCONSULTHIST { get; set; }
        public string RESERVADO2 { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_RELACIONAMENTO_FORNECEDOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@FTCONSULT", SqlDbType.VarChar, 8000, "FT-CONSULT"));
                    dbCommand.Parameters.Add(new SqlParameter("@FTCONSULTPERF", SqlDbType.VarChar, 8000, "FT-CONSULT-PERF"));
                    dbCommand.Parameters.Add(new SqlParameter("@FTCONSULTEVOL", SqlDbType.VarChar, 8000, "FT-CONSULT-EVOL"));
                    dbCommand.Parameters.Add(new SqlParameter("@FTCONSULPOTN", SqlDbType.VarChar, 8000, "FT-CONSUL-POTN"));
                    dbCommand.Parameters.Add(new SqlParameter("@FTCONSULPOTV", SqlDbType.VarChar, 8000, "FT-CONSUL-POTV"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADO", SqlDbType.VarChar, 8000, "RESERVADO"));
                    dbCommand.Parameters.Add(new SqlParameter("@FTCONSULTHIST", SqlDbType.VarChar, 8000, "FT-CONSULT-HIST"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADO2", SqlDbType.VarChar, 8000, "RESERVADO2"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@FTCONSULT"].Value = FTCONSULT ?? "";
                    dbCommand.Parameters["@FTCONSULTPERF"].Value = FTCONSULTPERF ?? "";
                    dbCommand.Parameters["@FTCONSULTEVOL"].Value = FTCONSULTEVOL ?? "";
                    dbCommand.Parameters["@FTCONSULPOTN"].Value = FTCONSULPOTN ?? "";
                    dbCommand.Parameters["@FTCONSULPOTV"].Value = FTCONSULPOTV ?? "";
                    dbCommand.Parameters["@RESERVADO"].Value = RESERVADO ?? "";
                    dbCommand.Parameters["@FTCONSULTHIST"].Value = FTCONSULTHIST ?? "";
                    dbCommand.Parameters["@RESERVADO2"].Value = RESERVADO2 ?? "";

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