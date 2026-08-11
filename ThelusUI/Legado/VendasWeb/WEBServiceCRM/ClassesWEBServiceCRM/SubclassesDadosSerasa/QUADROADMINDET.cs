using System;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM.SubclassesDadosSerasa
{
    public class QUADROADMINDET : SuperClasseDadosSerasa
    {
        public int IDProduto { get; set; }
        public int IDConfiguracao { get; set; }
        public string LINHA { get; set; }
        public string IDINF { get; set; }
        public string BCFIC { get; set; }
        public string TPINF { get; set; }

        //[JsonProperty("IDENT-ADM")]
        public string IDENTADM { get; set; }

        //[JsonProperty("CNPJ-CPF-ADM")]
        public string CNPJCPFADM { get; set; }

        //[JsonProperty("CNPJ-SEQ-ADM")]
        public string CNPJSEQADM { get; set; }

        //[JsonProperty("DIG-CPF-ADM")]
        public string DIGCPFADM { get; set; }

        //[JsonProperty("NOME-ADM")]
        public string NOMEADM { get; set; }

        //[JsonProperty("CARGO-ADM")]
        public string CARGOADM { get; set; }

        //[JsonProperty("NACIONAL-ADM")]
        public string NACIONALADM { get; set; }

        //[JsonProperty("EST-CIVIL-ADM")]
        public string ESTCIVILADM { get; set; }

        //[JsonProperty("DATA-INI-MANDATO-ADM")]
        public string DATAINIMANDATOADM { get; set; }

        //[JsonProperty("DATA-FIM-MANDATO-ADM")]
        public string DATAFIMMANDATOADM { get; set; }

        //[JsonProperty("RESTRI-ADMI")]
        public string RESTRIADMI { get; set; }

        //[JsonProperty("CARGO-ADMI")]
        public string CARGOADMI { get; set; }

        public string CDSITRF { get; set; }

        //[JsonProperty("DATA-ENTRA-ADM")]
        public string DATAENTRAADM { get; set; }

        //[JsonProperty("SITUAC-ADM")]
        public string SITUACADM { get; set; }

        public override string GravaDados(int IDCliente, int IDAnalise)
        {
            GerencialVendas.UtilClass objUtilClass = new GerencialVendas.UtilClass();

            try
            {
                base.GeraPREFIXO(LINHA, IDINF, BCFIC, TPINF);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_ANALISE_SERASA_DETALHES_ADMINISTRADORES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnalise", SqlDbType.Int, 0, "IDAnalise"));
                    dbCommand.Parameters.Add(new SqlParameter("@PREFIXO", SqlDbType.VarChar, 8000, "PREFIXO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDINF", SqlDbType.VarChar, 8000, "IDINF"));
                    dbCommand.Parameters.Add(new SqlParameter("@BCFIC", SqlDbType.VarChar, 8000, "BCFIC"));
                    dbCommand.Parameters.Add(new SqlParameter("@TPINF", SqlDbType.VarChar, 8000, "TPINF"));

                    dbCommand.Parameters.Add(new SqlParameter("@IDENTADM", SqlDbType.VarChar, 8000, "IDENTADM"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJCPFADM", SqlDbType.VarChar, 8000, "CNPJCPFADM"));
                    dbCommand.Parameters.Add(new SqlParameter("@CNPJSEQADM", SqlDbType.VarChar, 8000, "CNPJSEQADM"));
                    dbCommand.Parameters.Add(new SqlParameter("@DIGCPFADM", SqlDbType.VarChar, 8000, "DIGCPFADM"));
                    dbCommand.Parameters.Add(new SqlParameter("@NOMEADM", SqlDbType.VarChar, 8000, "NOMEADM"));
                    dbCommand.Parameters.Add(new SqlParameter("@CARGOADM", SqlDbType.VarChar, 8000, "CARGOADM"));
                    dbCommand.Parameters.Add(new SqlParameter("@NACIONALADM", SqlDbType.VarChar, 8000, "NACIONALADM"));
                    dbCommand.Parameters.Add(new SqlParameter("@ESTCIVILADM", SqlDbType.VarChar, 8000, "ESTCIVILADM"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAINIMANDATOADM", SqlDbType.VarChar, 8000, "DATAINIMANDATOADM"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAFIMMANDATOADM", SqlDbType.VarChar, 8000, "DATAFIMMANDATOADM"));
                    dbCommand.Parameters.Add(new SqlParameter("@RESTRIADMI", SqlDbType.VarChar, 8000, "RESTRIADMI"));
                    dbCommand.Parameters.Add(new SqlParameter("@CARGOADMI", SqlDbType.VarChar, 8000, "CARGOADMI"));
                    dbCommand.Parameters.Add(new SqlParameter("@CDSITRF", SqlDbType.VarChar, 8000, "CDSITRF"));
                    dbCommand.Parameters.Add(new SqlParameter("@DATAENTRAADM", SqlDbType.VarChar, 8000, "DATAENTRAADM"));
                    dbCommand.Parameters.Add(new SqlParameter("@SITUACADM", SqlDbType.VarChar, 8000, "SITUACADM"));

                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@IDAnalise"].Value = IDAnalise;
                    dbCommand.Parameters["@PREFIXO"].Value = PREFIXO ?? "";
                    dbCommand.Parameters["@IDINF"].Value = IDINF ?? "";
                    dbCommand.Parameters["@BCFIC"].Value = BCFIC ?? "";
                    dbCommand.Parameters["@TPINF"].Value = TPINF ?? "";

                    dbCommand.Parameters["@IDENTADM"].Value = IDENTADM ?? "";
                    dbCommand.Parameters["@CNPJCPFADM"].Value = CNPJCPFADM ?? "";
                    dbCommand.Parameters["@CNPJSEQADM"].Value = CNPJSEQADM ?? "";
                    dbCommand.Parameters["@DIGCPFADM"].Value = DIGCPFADM ?? "";
                    dbCommand.Parameters["@NOMEADM"].Value = NOMEADM ?? "";
                    dbCommand.Parameters["@CARGOADM"].Value = CARGOADM ?? "";
                    dbCommand.Parameters["@NACIONALADM"].Value = NACIONALADM ?? "";
                    dbCommand.Parameters["@ESTCIVILADM"].Value = ESTCIVILADM ?? "";
                    dbCommand.Parameters["@DATAINIMANDATOADM"].Value = DATAINIMANDATOADM ?? "";
                    dbCommand.Parameters["@DATAFIMMANDATOADM"].Value = DATAFIMMANDATOADM ?? "";
                    dbCommand.Parameters["@RESTRIADMI"].Value = RESTRIADMI ?? "";
                    dbCommand.Parameters["@CARGOADMI"].Value = CARGOADMI ?? "";
                    dbCommand.Parameters["@CDSITRF"].Value = CDSITRF ?? "";
                    dbCommand.Parameters["@DATAENTRAADM"].Value = DATAENTRAADM ?? "";
                    dbCommand.Parameters["@SITUACADM"].Value = SITUACADM ?? "";

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