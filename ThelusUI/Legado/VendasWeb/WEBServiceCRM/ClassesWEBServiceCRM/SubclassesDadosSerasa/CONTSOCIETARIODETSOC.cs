using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class CONTSOCIETARIODETSOC : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        //[JsonProperty("IDENT-CS")]
        public string IDENTCS { get; set; }

        //[JsonProperty("CNPJ-CPF-CS")]
        public string CNPJCPFCS { get; set; }

        //[JsonProperty("CNPJ-SEQ-CS")]
        public string CNPJSEQCS { get; set; }

        //[JsonProperty("DIG-CPF-CS")]
        public string DIGCPFCS { get; set; }

        //[JsonProperty("NOME-SOCIO-CS")]
        public string NOMESOCIOCS { get; set; }

        //[JsonProperty("NACIONAL-CS")]
        public string NACIONALCS { get; set; }

        //[JsonProperty("PERCAP-CS")]
        public string PERCAPCS { get; set; }

        //[JsonProperty("DATA-ENTRA-CS")]
        public string DATAENTRACS { get; set; }

        //[JsonProperty("RESTRI-SOCIO")]
        public string RESTRISOCIO { get; set; }

        //[JsonProperty("PERVOT-CS")]
        public string PERVOTCS { get; set; }

        public string CDSITRF { get; set; }

        //[JsonProperty("CDSA-SOCIO")]
        public string CDSASOCIO { get; set; }

        //[JsonProperty("SITUAC-CS")]
        public string SITUACCS { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_DETALHES_SOCIOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@IDENTCS", SqlDbType.VarChar, 8000, "IDENTCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJCPFCS", SqlDbType.VarChar, 8000, "CNPJCPFCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJSEQCS", SqlDbType.VarChar, 8000, "CNPJSEQCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@DIGCPFCS", SqlDbType.VarChar, 8000, "DIGCPFCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@NOMESOCIOCS", SqlDbType.VarChar, 8000, "NOMESOCIOCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@NACIONALCS", SqlDbType.VarChar, 8000, "NACIONALCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERCAPCS", SqlDbType.VarChar, 8000, "PERCAPCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAENTRACS", SqlDbType.VarChar, 8000, "DATAENTRACS"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESTRISOCIO", SqlDbType.VarChar, 8000, "RESTRISOCIO"));
                    dbCommand.Parameters.Add(new SqlParameter("@PERVOTCS", SqlDbType.VarChar, 8000, "PERVOTCS"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDSITRF", SqlDbType.VarChar, 8000, "CDSITRF"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDSASOCIO", SqlDbType.VarChar, 8000, "CDSASOCIO"));
                    dbCommand.Parameters.Add(new SqlParameter("@SITUACCS", SqlDbType.VarChar, 8000, "SITUACCS"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@IDENTCS"].Value = IDENTCS ?? "";
                    dbCommand.Parameters["@CNPJCPFCS"].Value = CNPJCPFCS ?? "";
                    dbCommand.Parameters["@CNPJSEQCS"].Value = CNPJSEQCS ?? "";
                    dbCommand.Parameters["@DIGCPFCS"].Value = DIGCPFCS ?? "";
                    dbCommand.Parameters["@NOMESOCIOCS"].Value = NOMESOCIOCS ?? "";
                    dbCommand.Parameters["@NACIONALCS"].Value = NACIONALCS ?? "";
                    dbCommand.Parameters["@PERCAPCS"].Value = PERCAPCS ?? "";
                    dbCommand.Parameters["@DATAENTRACS"].Value = DATAENTRACS ?? "";
                    dbCommand.Parameters["@RESTRISOCIO"].Value = RESTRISOCIO ?? "";
                    dbCommand.Parameters["@PERVOTCS"].Value = PERVOTCS ?? "";
                    dbCommand.Parameters["@CDSITRF"].Value = CDSITRF ?? "";
                    dbCommand.Parameters["@CDSASOCIO"].Value = CDSASOCIO ?? "";
                    dbCommand.Parameters["@SITUACCS"].Value = SITUACCS ?? "";

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