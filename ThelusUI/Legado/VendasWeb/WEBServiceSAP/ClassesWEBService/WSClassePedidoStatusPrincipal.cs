using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClassePedidoStatusPrincipal : clsConexao
    {
        public List<WSClassePedidoStatus> ListaPedidosStatus { get; set; }

        //Importa dados de países do SAP
        public string AtualizaStatusPedido()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClassePedidoStatus PedidosStatus in ListaPedidosStatus)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_FINALIZA_PEDIDO_VENDA_STATUS", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@StatusPedidoSAP", SqlDbType.NVarChar, 10, "StatusPedidoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@CanceladoPedidoSAP", SqlDbType.VarChar, 10, "CanceladoPedidoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@NumeroPedidoSAP"].Value = PedidosStatus.DocNum ?? "";
                        dbCommand.Parameters["@StatusPedidoSAP"].Value = PedidosStatus.DocStatus ?? "";
                        dbCommand.Parameters["@CanceladoPedidoSAP"].Value = PedidosStatus.CANCELED ?? "N";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro no cancelamento do pedido.";
            }

            return erro;
        }
    }
}