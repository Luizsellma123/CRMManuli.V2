using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class INFADSOCNQUSOC : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        public string CNPJ { get; set; }
        public string FILIAL { get; set; }
        public string DGCNPJ { get; set; }
        public string DTFUND { get; set; }
        public string DTATU { get; set; }
        public string RAZAO { get; set; }
        public string NMFT { get; set; }
        public string VINCULO { get; set; }
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

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_INF_ADIC_SOC", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.VarChar, 8000, "CNPJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@FILIAL", SqlDbType.VarChar, 8000, "FILIAL"));
                    dbCommand.Parameters.Add(new SqlParameter("@DGCNPJ", SqlDbType.VarChar, 8000, "DGCNPJ"));
                    dbCommand.Parameters.Add(new SqlParameter("@DTFUND", SqlDbType.VarChar, 8000, "DTFUND"));
                    dbCommand.Parameters.Add(new SqlParameter("@DTATU", SqlDbType.VarChar, 8000, "DTATU"));
                    dbCommand.Parameters.Add(new SqlParameter("@RAZAO", SqlDbType.VarChar, 8000, "RAZAO"));
                    dbCommand.Parameters.Add(new SqlParameter("@NMFT", SqlDbType.VarChar, 8000, "NMFT"));
                    dbCommand.Parameters.Add(new SqlParameter("@VINCULO", SqlDbType.VarChar, 8000, "VINCULO"));
                    dbCommand.Parameters.Add(new SqlParameter("@SITUAC", SqlDbType.VarChar, 8000, "SITUAC"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@CNPJ"].Value = CNPJ ?? "";
                    dbCommand.Parameters["@FILIAL"].Value = FILIAL ?? "";
                    dbCommand.Parameters["@DGCNPJ"].Value = DGCNPJ ?? "";
                    dbCommand.Parameters["@DTFUND"].Value = DTFUND ?? "";
                    dbCommand.Parameters["@DTATU"].Value = DTATU ?? "";
                    dbCommand.Parameters["@RAZAO"].Value = RAZAO ?? "";
                    dbCommand.Parameters["@NMFT"].Value = NMFT ?? "";
                    dbCommand.Parameters["@VINCULO"].Value = VINCULO ?? "";
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