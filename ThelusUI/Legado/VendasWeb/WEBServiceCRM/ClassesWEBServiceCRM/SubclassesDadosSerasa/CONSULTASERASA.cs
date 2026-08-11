using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class CONSULTASERASA : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

       //[JsonProperty("ANO-CONS")]
        public string ANOCONS { get; set; }

       //[JsonProperty("MES-CONS")]
        public string MESCONS { get; set; }

       //[JsonProperty("MES-DES-COM")]
        public string MESDESCOM { get; set; }

       //[JsonProperty("QTD-CONS")]
        public string QTDCONS { get; set; }

       //[JsonProperty("QTD-BCO-CONS")]
        public string QTDBCOCONS { get; set; }

       //[JsonProperty("IND-BCO-EMP")]
        public string INDBCOEMP { get; set; }
        public string RESERVADO { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONSULTAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@ANOCONS", SqlDbType.VarChar, 8000, "ANO-CONS"));
                    dbCommand.Parameters.Add(new SqlParameter("@MESCONS", SqlDbType.VarChar, 8000, "MES-CONS"));
                    dbCommand.Parameters.Add(new SqlParameter("@MESDESCOM", SqlDbType.VarChar, 8000, "MES-DES-COM"));
                    dbCommand.Parameters.Add(new SqlParameter("@QTDCONS", SqlDbType.VarChar, 8000, "QTD-CONS"));
                    dbCommand.Parameters.Add(new SqlParameter("@QTDBCOCONS", SqlDbType.VarChar, 8000, "QTD-BCO-CONS"));
                    dbCommand.Parameters.Add(new SqlParameter("@INDBCOEMP", SqlDbType.VarChar, 8000, "IND-BCO-EMP"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADO", SqlDbType.VarChar, 8000, "RESERVADO"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@ANOCONS"].Value = ANOCONS ?? "";
                    dbCommand.Parameters["@MESCONS"].Value = MESCONS ?? "";
                    dbCommand.Parameters["@MESDESCOM"].Value = MESDESCOM ?? "";
                    dbCommand.Parameters["@QTDCONS"].Value = QTDCONS ?? "";
                    dbCommand.Parameters["@QTDBCOCONS"].Value = QTDBCOCONS ?? "";
                    dbCommand.Parameters["@INDBCOEMP"].Value = INDBCOEMP ?? "";
                    dbCommand.Parameters["@RESERVADO"].Value = RESERVADO ?? "";

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