using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseRetornoDadosPrincipal : clsConexao
    {
        public List<WSClasseRetornoDados> ListaRetornoDados { get; set; }
        public List<WSClasseRetornoDadosNotas> ListaRetornoDadosNotas { get; set; }

        //Importa dados de países do SAP
        public string AtualizaNotasFiscais()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseRetornoDadosNotas NotaFiscal in ListaRetornoDadosNotas)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();


                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_NOTAS_PEDIDO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroNotaFiscal", SqlDbType.Int, 0, "NumeroNotaFiscal"));
                        dbCommand.Parameters.Add(new SqlParameter("@DataEmissao", SqlDbType.DateTime, 0, "DataEmissao"));
                        dbCommand.Parameters.Add(new SqlParameter("@SeqCode", SqlDbType.Int, 0, "SeqCode"));
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroPrimarioNota", SqlDbType.BigInt, 0, "NumeroPrimarioNota"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@NumeroPedidoSAP"].Value = Convert.ToInt32(NotaFiscal.PedidoVenda);
                        dbCommand.Parameters["@NumeroNotaFiscal"].Value = Convert.ToInt32(NotaFiscal.NotaFiscal);
                        dbCommand.Parameters["@DataEmissao"].Value = Convert.ToDateTime(NotaFiscal.DataEmissao);
                        dbCommand.Parameters["@SeqCode"].Value = Convert.ToInt32(NotaFiscal.SeqCode);
                        dbCommand.Parameters["@NumeroPrimarioNota"].Value = Convert.ToInt32(NotaFiscal.NumeroPrimarioNota);

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na atualização das notas do pedido.";
            }

            return erro;
        }
    }
}