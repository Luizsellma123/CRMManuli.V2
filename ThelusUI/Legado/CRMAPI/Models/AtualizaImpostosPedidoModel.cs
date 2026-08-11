using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaImpostosPedidoModel
    {
        public string NumeroPedidoSAP { get; set; }

        List<ImpostosPedidoClass> ImpostosPedidos = new List<ImpostosPedidoClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaImpostosPedido()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select CONVERT(VARCHAR(MAX),ORDR.DocNum) DocNum, ORDR.DocTotal, ");
                stringSQL.AppendLine("RDR1.ItemCode, OSTT.Name, sum(TaxSum) as Imposto, ");
                stringSQL.AppendLine("((sum(TaxSum)/(case when RDR1.LineTotal=0 then 1 else RDR1.LineTotal end)) * 100) ");
                stringSQL.AppendLine("PercentualImpostos ");

                stringSQL.AppendLine("from ORDR ");
                stringSQL.AppendLine("INNER JOIN RDR1 ");
                stringSQL.AppendLine("	ON ORDR.DocEntry=RDR1.DocEntry ");
                stringSQL.AppendLine("INNER JOIN RDR4 ");
                stringSQL.AppendLine("	ON RDR4.DocEntry=RDR1.DocEntry and RDR4.LineNum=RDR1.LineNum ");
                stringSQL.AppendLine("INNER JOIN OSTC ");
                stringSQL.AppendLine("	ON OSTC.Code=RDR4.StcCode ");
                stringSQL.AppendLine("INNER JOIN OTFC ");
                stringSQL.AppendLine("	ON OSTC.TfcId=OTFC.AbsId ");
                stringSQL.AppendLine("INNER JOIN TFC1 ");
                stringSQL.AppendLine("	ON TFC1.TfcId=OTFC.AbsId ");
                stringSQL.AppendLine("INNER JOIN OSTT ");
                stringSQL.AppendLine("	ON OSTT.AbsId=TFC1.TypeId ");
                stringSQL.AppendLine("INNER JOIN STC1 ");
                stringSQL.AppendLine("	ON STC1.STCCode=OSTC.Code and ");
                stringSQL.AppendLine("	TFC1.FmlId=STC1.FmlId and ");
                stringSQL.AppendLine("	RDR4.StcCode=STC1.STCCode and ");
                stringSQL.AppendLine("	RDR4.StaCode=STC1.STACODE ");

                stringSQL.AppendLine("where (ORDR.DocEntry = '" + NumeroPedidoSAP + "' or '' = '" + NumeroPedidoSAP + "')");

                stringSQL.AppendLine("group by ORDR.DocNum, ORDR.DocTotal, ");
                stringSQL.AppendLine("RDR1.LineTotal, RDR1.ItemCode, OSTT.Name ");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                ImpostosPedidos = objUtilClass.ConvertDataTable<ImpostosPedidoClass>(ConsultaSAP);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                throw new Exception("Erro ao carregar os impostos dos pedidos do SAP.");
            }
        }

        public string AtualizaImpostosPedido()
        {
            string erro = "";

            try
            {
                CarregaImpostosPedido();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (ImpostosPedidoClass ImpostosPedido in ImpostosPedidos)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
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

                        dbCommand.Parameters["@NumeroPedidoSAP"].Value = Convert.ToInt32(ImpostosPedido.DocNum);
                        dbCommand.Parameters["@NumeroEsbocoSAP"].Value = 0;
                        dbCommand.Parameters["@TotalPedidoImpostos"].Value = ImpostosPedido.DocTotal;
                        dbCommand.Parameters["@ValorImposto"].Value = ImpostosPedido.Imposto;
                        dbCommand.Parameters["@PercentualImposto"].Value = ImpostosPedido.PercentualImpostos;
                        dbCommand.Parameters["@Imposto"].Value = ImpostosPedido.Name ?? "";
                        dbCommand.Parameters["@CodigoProdutoSAP"].Value = ImpostosPedido.ItemCode ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                        //if (erro != "") erro = erro;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = ex.Message;

                erro = "Erro na atualização dos impostos.";
            }

            return erro;
        }
    }
}