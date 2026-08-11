using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClassePedidosImpostosPrincipal : clsConexao
    {
        public List<WSClassePedidoImpostos> ListaProdutosImpostos { get; set; }

        //Importa dados de países do SAP
        public string AtualizaImpostos()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClassePedidoImpostos Impostos in ListaProdutosImpostos)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();


                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_IMPOSTOS_PEDIDO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroEsbocoSAP", SqlDbType.Int, 0, "NumeroEsbocoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@TotalPedidoImpostos", SqlDbType.Decimal, 0, "TotalPedidoImpostos"));
                        dbCommand.Parameters.Add(new SqlParameter("@ValorImposto", SqlDbType.Decimal, 0, "ValorImposto"));
                        dbCommand.Parameters.Add(new SqlParameter("@PercentualImposto", SqlDbType.Decimal, 0, "PercentualImposto"));
                        dbCommand.Parameters.Add(new SqlParameter("@Imposto", SqlDbType.VarChar, 50, "Imposto"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoProdutoSAP", SqlDbType.VarChar, 50, "CodigoProdutoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@NumeroPedidoSAP"].Value = Convert.ToInt32(Impostos.DocNum);
                        dbCommand.Parameters["@NumeroEsbocoSAP"].Value = 0;
                        dbCommand.Parameters["@TotalPedidoImpostos"].Value = Impostos.DocTotal;
                        dbCommand.Parameters["@ValorImposto"].Value = Impostos.Imposto;
                        dbCommand.Parameters["@PercentualImposto"].Value = Impostos.PercentualImpostos;
                        dbCommand.Parameters["@Imposto"].Value = Impostos.Name ?? "";
                        dbCommand.Parameters["@CodigoProdutoSAP"].Value = Impostos.ItemCode ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na atualização dos impostos.";
            }

            return erro;
        }

        //Importa dados de países do SAP
        public string AtualizaImpostosRascunho()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClassePedidoImpostos Impostos in ListaProdutosImpostos)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();


                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_IMPOSTOS_PEDIDO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroEsbocoSAP", SqlDbType.Int, 0, "NumeroEsbocoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@TotalPedidoImpostos", SqlDbType.Decimal, 0, "TotalPedidoImpostos"));
                        dbCommand.Parameters.Add(new SqlParameter("@ValorImposto", SqlDbType.Decimal, 0, "ValorImposto"));
                        dbCommand.Parameters.Add(new SqlParameter("@PercentualImposto", SqlDbType.Decimal, 0, "PercentualImposto"));
                        dbCommand.Parameters.Add(new SqlParameter("@Imposto", SqlDbType.VarChar, 50, "Imposto"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoProdutoSAP", SqlDbType.VarChar, 50, "CodigoProdutoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@NumeroPedidoSAP"].Value = 0;
                        dbCommand.Parameters["@NumeroEsbocoSAP"].Value = Convert.ToInt32(Impostos.DocNum);
                        dbCommand.Parameters["@TotalPedidoImpostos"].Value = Impostos.DocTotal;
                        dbCommand.Parameters["@ValorImposto"].Value = Impostos.Imposto;
                        dbCommand.Parameters["@PercentualImposto"].Value = Impostos.PercentualImpostos;
                        dbCommand.Parameters["@Imposto"].Value = Impostos.Name ?? "";
                        dbCommand.Parameters["@CodigoProdutoSAP"].Value = Impostos.ItemCode ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na atualização dos impostos.";
            }

            return erro;
        }

    }
}