using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseClienteEnderecoPrincipal : clsConexao
    {

        public List<WSClasseClienteEndereco> ListaClientesEnderecos { get; set; }

        //Importa dados de países do SAP
        public string AtualizaClientesEnderecos()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseClienteEndereco ClienteEndereco in ListaClientesEnderecos)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_CLIENTE_ENDERECO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.NVarChar, 15, "CodigoClienteSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@DescricaoEndereco", SqlDbType.NVarChar, 50, "DescricaoEndereco"));
                        dbCommand.Parameters.Add(new SqlParameter("@TipoLogradouro", SqlDbType.NVarChar, 100, "TipoLogradouro"));
                        dbCommand.Parameters.Add(new SqlParameter("@Rua", SqlDbType.NVarChar, 100, "Rua"));
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroRua", SqlDbType.NVarChar, 100, "NumeroRua"));
                        dbCommand.Parameters.Add(new SqlParameter("@Complemento", SqlDbType.NText, 0, "Complemento"));
                        dbCommand.Parameters.Add(new SqlParameter("@CEP", SqlDbType.VarChar, 20, "CEP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Bairro", SqlDbType.NVarChar, 100, "Bairro"));
                        dbCommand.Parameters.Add(new SqlParameter("@Cidade", SqlDbType.NVarChar, 100, "Cidade"));
                        dbCommand.Parameters.Add(new SqlParameter("@PaisSAP", SqlDbType.NVarChar, 3, "PaisSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@EstadoSAP", SqlDbType.NVarChar, 3, "EstadoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@MunicipioSAP", SqlDbType.NVarChar, 100, "MunicipioSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoClienteSAP"].Value = ClienteEndereco.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@DescricaoEndereco"].Value = ClienteEndereco.DescricaoEndereco ?? "";
                        dbCommand.Parameters["@TipoLogradouro"].Value = ClienteEndereco.TipoLogradouro ?? "";
                        dbCommand.Parameters["@Rua"].Value = ClienteEndereco.Rua ?? "";
                        dbCommand.Parameters["@NumeroRua"].Value = ClienteEndereco.NumeroRua ?? "";
                        dbCommand.Parameters["@Complemento"].Value = ClienteEndereco.Complemento ?? "";
                        dbCommand.Parameters["@CEP"].Value = ClienteEndereco.CEP ?? "";
                        dbCommand.Parameters["@Bairro"].Value = ClienteEndereco.Bairro ?? "";
                        dbCommand.Parameters["@Cidade"].Value = ClienteEndereco.Cidade ?? "";
                        dbCommand.Parameters["@PaisSAP"].Value = ClienteEndereco.PaisSAP ?? "";
                        dbCommand.Parameters["@EstadoSAP"].Value = ClienteEndereco.EstadoSAP ?? "";
                        dbCommand.Parameters["@MunicipioSAP"].Value = ClienteEndereco.MunicipioSAP ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação dos edereços dos Clientes.";
            }

            return erro;
        }
    }
}