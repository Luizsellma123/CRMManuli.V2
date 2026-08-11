using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaImpostosPedidoRascunhoModel
    {
        public string NumeroEsbocoSAP { get; set; }

        List<ImpostosPedidoRascunhoClass> ImpostosPedidosRascunho = new List<ImpostosPedidoRascunhoClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaImpostosPedido()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select ");
                stringSQL.AppendLine("ISNULL(CONVERT(VARCHAR(MAX),ODRF.DocEntry),'') as DocNum, ");
                stringSQL.AppendLine("ISNULL(DRF1.ItemCode,'') AS ItemCode, ");
                stringSQL.AppendLine("ISNULL(OSTT.[Name],'') AS [Name], ");
                stringSQL.AppendLine("sum(TaxSum) as Imposto, ");
                stringSQL.AppendLine("ODRF.DocTotal, ");
                stringSQL.AppendLine("((sum(TaxSum)/(case when DRF1.LineTotal=0 then 1 else DRF1.LineTotal end)) * 100) PercentualImpostos ");

                stringSQL.AppendLine("from ODRF ");
                stringSQL.AppendLine("INNER JOIN DRF1 ");
                stringSQL.AppendLine("	ON ODRF.DocEntry=DRF1.DocEntry ");
                stringSQL.AppendLine("INNER JOIN DRF4 ");
                stringSQL.AppendLine("	ON DRF4.DocEntry=DRF1.DocEntry and DRF4.LineNum=DRF1.LineNum ");
                stringSQL.AppendLine("INNER JOIN OSTC ");
                stringSQL.AppendLine("	ON OSTC.Code=DRF4.StcCode ");
                stringSQL.AppendLine("INNER JOIN OTFC ");
                stringSQL.AppendLine("	ON OSTC.TfcId=OTFC.AbsId ");
                stringSQL.AppendLine("INNER JOIN TFC1 ");
                stringSQL.AppendLine("	ON TFC1.TfcId=OTFC.AbsId ");
                stringSQL.AppendLine("INNER JOIN OSTT ");
                stringSQL.AppendLine("	ON OSTT.AbsId=TFC1.TypeId ");
                stringSQL.AppendLine("INNER JOIN STC1 ");
                stringSQL.AppendLine("	ON STC1.STCCode=OSTC.Code ");
                stringSQL.AppendLine("	and TFC1.FmlId=STC1.FmlId ");
                stringSQL.AppendLine("	and DRF4.StcCode=STC1.STCCode ");
                stringSQL.AppendLine("	and DRF4.StaCode=STC1.STACODE ");

                stringSQL.AppendLine("WHERE (CONVERT(VARCHAR(MAX),ODRF.DocEntry) =  '" + NumeroEsbocoSAP + "' ");
                stringSQL.AppendLine("or '' = '" + NumeroEsbocoSAP + "' )");

                stringSQL.AppendLine("GROUP BY ODRF.DocEntry, ODRF.DocTotal, ");
                stringSQL.AppendLine("DRF1.LineTotal, DRF1.ItemCode, OSTT.[Name] ");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                ImpostosPedidosRascunho = objUtilClass.ConvertDataTable<ImpostosPedidoRascunhoClass>(ConsultaSAP);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                throw new Exception("Erro ao carregar os impostos dos rascunhos do SAP.");
            }
        }

        public string AtualizaImpostosPedido()
        {
            string erro = "";

            try
            {
                CarregaImpostosPedido();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (ImpostosPedidoRascunhoClass ImpostosPedidoRascunho in ImpostosPedidosRascunho)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_IMPOSTOS_PEDIDO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

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
                        dbCommand.Parameters["@NumeroEsbocoSAP"].Value = Convert.ToInt32(ImpostosPedidoRascunho.DocNum);
                        dbCommand.Parameters["@TotalPedidoImpostos"].Value = ImpostosPedidoRascunho.DocTotal;
                        dbCommand.Parameters["@ValorImposto"].Value = ImpostosPedidoRascunho.Imposto;
                        dbCommand.Parameters["@PercentualImposto"].Value = ImpostosPedidoRascunho.PercentualImpostos;
                        dbCommand.Parameters["@Imposto"].Value = ImpostosPedidoRascunho.Name ?? "";
                        dbCommand.Parameters["@CodigoProdutoSAP"].Value = ImpostosPedidoRascunho.ItemCode ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                        if (erro != "")
                            erro = erro;

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