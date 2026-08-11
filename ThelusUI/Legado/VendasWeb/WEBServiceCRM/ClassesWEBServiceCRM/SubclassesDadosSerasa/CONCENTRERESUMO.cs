using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class CONCENTRERESUMO : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

       //[JsonProperty("QTDE-RES")]
        public string QTDERES { get; set; }
        public string DISC { get; set; }

       //[JsonProperty("MESI-DES")]
        public string MESIDES { get; set; }
        public string MESI { get; set; }
        public string ANOI { get; set; }

       //[JsonProperty("MESF-DES")]
        public string MESFDES { get; set; }
        public string MESF { get; set; }
        public string ANOF { get; set; }
        public string MOED { get; set; }
        public string VALO { get; set; }
        public string ORIG { get; set; }
        public string AGPR { get; set; }

       //[JsonProperty("TOTAL-RES")]
        public string TOTALRES { get; set; }
        public string NATUREZA { get; set; }
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

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_CONCENTRE_RESUMO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@QTDERES", SqlDbType.VarChar, 8000, "QTDERES"));
                    dbCommand.Parameters.Add(new SqlParameter("@DISC", SqlDbType.VarChar, 8000, "DISC"));
                    dbCommand.Parameters.Add(new SqlParameter("@MESIDES", SqlDbType.VarChar, 8000, "MESIDES"));
                    dbCommand.Parameters.Add(new SqlParameter("@MESI", SqlDbType.VarChar, 8000, "MESI"));
                    dbCommand.Parameters.Add(new SqlParameter("@ANOI", SqlDbType.VarChar, 8000, "ANOI"));
                    dbCommand.Parameters.Add(new SqlParameter("@MESFDES", SqlDbType.VarChar, 8000, "MESFDES"));
                    dbCommand.Parameters.Add(new SqlParameter("@MESF", SqlDbType.VarChar, 8000, "MESF"));
                    dbCommand.Parameters.Add(new SqlParameter("@ANOF", SqlDbType.VarChar, 8000, "ANOF"));
                    dbCommand.Parameters.Add(new SqlParameter("@MOED", SqlDbType.VarChar, 8000, "MOED"));
                    dbCommand.Parameters.Add(new SqlParameter("@VALO", SqlDbType.VarChar, 8000, "VALO"));
                    dbCommand.Parameters.Add(new SqlParameter("@ORIG", SqlDbType.VarChar, 8000, "ORIG"));
                    dbCommand.Parameters.Add(new SqlParameter("@AGPR", SqlDbType.VarChar, 8000, "AGPR"));
                    dbCommand.Parameters.Add(new SqlParameter("@TOTALRES", SqlDbType.VarChar, 8000, "TOTALRES"));
                    dbCommand.Parameters.Add(new SqlParameter("@NATUREZA", SqlDbType.VarChar, 8000, "NATUREZA"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJCPF", SqlDbType.VarChar, 8000, "CNPJCPF"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@QTDERES"].Value = QTDERES ?? "";
                    dbCommand.Parameters["@DISC"].Value = DISC ?? "";
                    dbCommand.Parameters["@MESIDES"].Value = MESIDES ?? "";
                    dbCommand.Parameters["@MESI"].Value = MESI ?? "";
                    dbCommand.Parameters["@ANOI"].Value = ANOI ?? "";
                    dbCommand.Parameters["@MESFDES"].Value = MESFDES ?? "";
                    dbCommand.Parameters["@MESF"].Value = MESF ?? "";
                    dbCommand.Parameters["@ANOF"].Value = ANOF ?? "";
                    dbCommand.Parameters["@MOED"].Value = MOED ?? "";
                    dbCommand.Parameters["@VALO"].Value = VALO ?? "";
                    dbCommand.Parameters["@ORIG"].Value = ORIG ?? "";
                    dbCommand.Parameters["@AGPR"].Value = AGPR ?? "";
                    dbCommand.Parameters["@TOTALRES"].Value = TOTALRES ?? "";
                    dbCommand.Parameters["@NATUREZA"].Value = NATUREZA ?? "";
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