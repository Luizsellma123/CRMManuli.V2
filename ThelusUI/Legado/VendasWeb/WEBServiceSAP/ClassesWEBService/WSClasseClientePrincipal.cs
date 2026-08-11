using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseClientePrincipal : clsConexao
    {
        public List<WSClasseCliente> ListaClientes { get; set; }

        //Importa dados de países do SAP
        public string AtualizaClientes()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseCliente Cliente in ListaClientes)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_CLIENTE", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@NomeCliente", SqlDbType.NVarChar, 100, "@NomeCliente"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeFantasia", SqlDbType.NText, 0, "NomeFantasia"));
                        dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.NVarChar, 20, "CNPJ"));
                        dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.NVarChar, 20, "Telefone"));
                        dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100, "Email"));
                        dbCommand.Parameters.Add(new SqlParameter("@ObservacaoSimples", SqlDbType.NVarChar, 20, "ObservacaoSimples"));
                        dbCommand.Parameters.Add(new SqlParameter("@ObservacaoCompleta", SqlDbType.NText, 0, "ObservacaoCompleta"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoVendedorSAP", SqlDbType.Int, 0, "CodigoVendedorSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@TipoCliente", SqlDbType.NVarChar, 30, "TipoCliente"));
                        dbCommand.Parameters.Add(new SqlParameter("@NaturezaJuridica", SqlDbType.NVarChar, 50, "NaturezaJuridica"));
                        dbCommand.Parameters.Add(new SqlParameter("@IndicadorIndIEDest", SqlDbType.NVarChar, 2, "IndicadorIndIEDest"));
                        dbCommand.Parameters.Add(new SqlParameter("@IndicadorNatureza", SqlDbType.NVarChar, 2, "IndicadorNatureza"));
                        dbCommand.Parameters.Add(new SqlParameter("@IndicadorOpConsumidor", SqlDbType.NVarChar, 2, "IndicadorOpConsumidor"));
                        dbCommand.Parameters.Add(new SqlParameter("@EnquadramentoTributario", SqlDbType.NVarChar, 50, "EnquadramentoTributario"));
                        dbCommand.Parameters.Add(new SqlParameter("@CartaIPI", SqlDbType.NVarChar, 10, "CartaIPI"));
                        dbCommand.Parameters.Add(new SqlParameter("@DataCarta", SqlDbType.DateTime, 0, "DataCarta"));
                        dbCommand.Parameters.Add(new SqlParameter("@SimplesNacional", SqlDbType.NVarChar, 1, "SimplesNacional"));
                        dbCommand.Parameters.Add(new SqlParameter("@ProdutorRural", SqlDbType.NVarChar, 2, "ProdutorRural"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.NVarChar, 15, "CodigoClienteSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@GrupoEconomico", SqlDbType.NVarChar, 50, "GrupoEconomico"));
                        dbCommand.Parameters.Add(new SqlParameter("@GrupoClientes", SqlDbType.Int, 0, "GrupoClientes"));
                        dbCommand.Parameters.Add(new SqlParameter("@IdAnexoSAP", SqlDbType.Int, 0, "IdAnexoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@CPOM", SqlDbType.VarChar, 50, "CPOM"));
                        dbCommand.Parameters.Add(new SqlParameter("@LimiteCredito", SqlDbType.Decimal, 0, "LimiteCredito"));
                        dbCommand.Parameters.Add(new SqlParameter("@CondicaoPagamentoPadraoSAP", SqlDbType.Int, 0, "CondicaoPagamentoPadraoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@ClassificacaoComercial", SqlDbType.VarChar, 8000, "ClassificacaoComercial"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@NomeCliente"].Value = Cliente.NomeCliente ?? "";
                        dbCommand.Parameters["@NomeFantasia"].Value = Cliente.NomeFantasia ?? "";
                        dbCommand.Parameters["@CNPJ"].Value = Cliente.CNPJ ?? "";
                        dbCommand.Parameters["@Telefone"].Value = Cliente.Telefone ?? "";
                        dbCommand.Parameters["@Email"].Value = Cliente.Email ?? "";
                        dbCommand.Parameters["@ObservacaoSimples"].Value = Cliente.ObservacaoSimples ?? "";
                        dbCommand.Parameters["@ObservacaoCompleta"].Value = Cliente.ObservacaoCompleta ?? "";
                        dbCommand.Parameters["@CodigoVendedorSAP"].Value = Cliente.CodigoVendedorSAP;
                        dbCommand.Parameters["@TipoCliente"].Value = Cliente.TipoCliente ?? "";
                        dbCommand.Parameters["@NaturezaJuridica"].Value = Cliente.NaturezaJuridica ?? "";
                        dbCommand.Parameters["@IndicadorIndIEDest"].Value = Cliente.IndicadorIndIEDest ?? "";
                        dbCommand.Parameters["@IndicadorNatureza"].Value = Cliente.IndicadorNatureza ?? "";
                        dbCommand.Parameters["@IndicadorOpConsumidor"].Value = Cliente.IndicadorOpConsumidor ?? "";
                        dbCommand.Parameters["@EnquadramentoTributario"].Value = Cliente.EnquadramentoTributario ?? "";
                        dbCommand.Parameters["@CartaIPI"].Value = Cliente.CartaIPI ?? "";

                        if (Cliente.DataCarta != null) { dbCommand.Parameters["@DataCarta"].Value = Convert.ToDateTime(Cliente.DataCarta); } else { dbCommand.Parameters["@DataCarta"].Value = "1900-01-01"; }

                        dbCommand.Parameters["@SimplesNacional"].Value = Cliente.SimplesNacional ?? "";
                        dbCommand.Parameters["@ProdutorRural"].Value = Cliente.ProdutorRural ?? "";
                        dbCommand.Parameters["@CodigoClienteSAP"].Value = Cliente.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@GrupoEconomico"].Value = Cliente.GrupoEconomico ?? "";
                        dbCommand.Parameters["@GrupoClientes"].Value = Cliente.GrupoClientes;
                        dbCommand.Parameters["@IdAnexoSAP"].Value = Convert.ToInt32(Cliente.IdAnexoSAP ?? "0");
                        dbCommand.Parameters["@CPOM"].Value = Cliente.CPOM ?? "";
                        dbCommand.Parameters["@LimiteCredito"].Value = Cliente.LimiteCredito;
                        dbCommand.Parameters["@CondicaoPagamentoPadraoSAP"].Value = Convert.ToInt32(Cliente.CondicaoPagamentoPadraoSAP ?? "0");

                        dbCommand.Parameters["@ClassificacaoComercial"].Value = Cliente.ClassificacaoComercial;

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação dos Clientes.";
            }

            return erro;
        }

    }
}