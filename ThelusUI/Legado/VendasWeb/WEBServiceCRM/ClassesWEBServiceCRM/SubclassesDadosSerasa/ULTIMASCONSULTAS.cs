using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class ULTIMASCONSULTAS : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

       //[JsonProperty("DATA-CONS")]
        public string DATACONS { get; set; }

       //[JsonProperty("NM-CONS")]
        public string NMCONS { get; set; }

       //[JsonProperty("QT-CONS")]
        public string QTCONS { get; set; }

       //[JsonProperty("CNPJ–CONS")]
        public string CNPJCONS { get; set; }
        public string RESERVADO { get; set; }

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

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_ULTIMAS_CONSULTAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DATACONS", SqlDbType.VarChar, 8000, "DATACONS"));
                    dbCommand.Parameters.Add(new SqlParameter("@NMCONS", SqlDbType.VarChar, 8000, "NMCONS"));
                    dbCommand.Parameters.Add(new SqlParameter("@QTCONS", SqlDbType.VarChar, 8000, "QTCONS"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJCONS", SqlDbType.VarChar, 8000, "CNPJ –CONS"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADO", SqlDbType.VarChar, 8000, "RESERVADO"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADO2", SqlDbType.VarChar, 8000, "RESERVADO2"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@DATACONS"].Value = DATACONS ?? "";
                    dbCommand.Parameters["@NMCONS"].Value = NMCONS ?? "";
                    dbCommand.Parameters["@QTCONS"].Value = QTCONS ?? "";
                    dbCommand.Parameters["@CNPJCONS"].Value = CNPJCONS ?? "";
                    dbCommand.Parameters["@RESERVADO"].Value = RESERVADO ?? "";
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