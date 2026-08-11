using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseClienteEnderecoIDFiscalPrincipal : clsConexao
    {
        public List<WSClasseClienteEnderecoIDFiscal> ListaClientesEndIDFiscal { get; set; }

        //Importa dados de países do SAP
        public string AtualizaClientesEnderecosIDFiscal()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseClienteEnderecoIDFiscal ClienteEndFiscal in ListaClientesEndIDFiscal)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_IDENTIFICACAO_FISCAL", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.NVarChar, 15, "CodigoClienteSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@DescricaoEndereco", SqlDbType.NVarChar, 50, "DescricaoEndereco"));
                        dbCommand.Parameters.Add(new SqlParameter("@CNAESAP", SqlDbType.Int, 0, "CNAESAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.NVarChar, 100, "CNPJ"));
                        dbCommand.Parameters.Add(new SqlParameter("@InscricaoEstadual", SqlDbType.NVarChar, 100, "InscricaoEstadual"));
                        dbCommand.Parameters.Add(new SqlParameter("@Suframa", SqlDbType.NVarChar, 100, "Suframa"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoClienteSAP"].Value = ClienteEndFiscal.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@DescricaoEndereco"].Value = ClienteEndFiscal.DescricaoEndereco ?? "";
                        dbCommand.Parameters["@CNAESAP"].Value = Convert.ToInt32(ClienteEndFiscal.IDCNAE ?? "0");
                        dbCommand.Parameters["@CNPJ"].Value = ClienteEndFiscal.CNPJ ?? "";
                        dbCommand.Parameters["@InscricaoEstadual"].Value = ClienteEndFiscal.InscricaoEstadual ?? "";
                        dbCommand.Parameters["@Suframa"].Value = ClienteEndFiscal.Suframa ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação das identificações fiscais dos Clientes.";
            }

            return erro;
        }
    }
}