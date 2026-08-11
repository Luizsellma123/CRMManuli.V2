using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaNotasFiscaisPedidoModel
    {
        public string NumeroPedidoSAP { get; set; }

        List<NotasFiscaisPedidoClass> NotasFiscaisPedido = new List<NotasFiscaisPedidoClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaNotasFiscaisPedido()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select distinct ");
                stringSQL.AppendLine("INV1.Basetype Basetype, ");
                stringSQL.AppendLine("ORDR.DocEntry PedidoVenda, ");
                stringSQL.AppendLine("OINV.Serial NotaFiscal, ");
                stringSQL.AppendLine("OINV.DocDate DataEmissao, ");
                stringSQL.AppendLine("OINV.SeqCode SeqCode, ");
                stringSQL.AppendLine("OINV.DocEntry NumeroPrimarioNota ");

                stringSQL.AppendLine("FROM OINV ");
                stringSQL.AppendLine("INNER JOIN INV1 ");
                stringSQL.AppendLine("  ON OINV.docEntry = INV1.DocEntry ");
                stringSQL.AppendLine("INNER JOIN ORDR ");
                stringSQL.AppendLine("  ON(ORDR.DocEntry = INV1.BaseEntry) ");
                stringSQL.AppendLine("  OR(OINV.U_IB_CRM_CodPed = ORDR.U_IB_CRM_CodPed ");
                stringSQL.AppendLine("      and INV1.BaseType = -1) ");

                stringSQL.AppendLine("where(INV1.Basetype = '17' OR INV1.BaseType = -1) ");
                stringSQL.AppendLine("and ORDR.DocEntry = " + NumeroPedidoSAP);

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                NotasFiscaisPedido = objUtilClass.ConvertDataTable<NotasFiscaisPedidoClass>(ConsultaSAP);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar as notas fiscais do SAP.");
            }
        }

        public string AtualizaNotasFiscaisPedido()
        {
            string erro = "";

            try
            {
                CarregaNotasFiscaisPedido();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (NotasFiscaisPedidoClass NotaFiscal in NotasFiscaisPedido)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
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

                        dbCommand.Parameters["@NumeroPedidoSAP"].Value = NotaFiscal.PedidoVenda;
                        dbCommand.Parameters["@NumeroNotaFiscal"].Value = NotaFiscal.NotaFiscal;
                        dbCommand.Parameters["@DataEmissao"].Value = NotaFiscal.DataEmissao;
                        dbCommand.Parameters["@SeqCode"].Value = NotaFiscal.SeqCode;
                        dbCommand.Parameters["@NumeroPrimarioNota"].Value = NotaFiscal.NumeroPrimarioNota;

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;
                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação das notas fiscais.";
            }

            return erro;
        }
    }
}