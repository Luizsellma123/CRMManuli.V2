using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class RISKSCORINGPRINAD1 : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string DATA { get; set; }
        public string HORA { get; set; }
        public string FATORRISKSCORING { get; set; }
        public string FATORPRINAD { get; set; }
        public string RESERVADO { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            UtilClass objUtilClass = new UtilClass();

            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_RISKSCORING_PRINAD", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@DATA", SqlDbType.VarChar, 8000, "DATA"));
                    dbCommand.Parameters.Add(new SqlParameter("@HORA", SqlDbType.VarChar, 8000, "HORA"));
                    dbCommand.Parameters.Add(new SqlParameter("@FATORRISKSCORING", SqlDbType.VarChar, 8000, "FATORRISKSCORING"));
                    dbCommand.Parameters.Add(new SqlParameter("@FATORPRINAD", SqlDbType.VarChar, 8000, "FATORPRINAD"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESERVADO", SqlDbType.VarChar, 8000, "RESERVADO"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@DATA"].Value = objUtilClass.RetornaDataFormatada(DATA, "dd-MM-yyyy");
                    dbCommand.Parameters["@HORA"].Value = HORA ?? "";
                    dbCommand.Parameters["@FATORRISKSCORING"].Value = FATORRISKSCORING ?? "0";
                    dbCommand.Parameters["@FATORPRINAD"].Value = FATORPRINAD ?? "0";
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