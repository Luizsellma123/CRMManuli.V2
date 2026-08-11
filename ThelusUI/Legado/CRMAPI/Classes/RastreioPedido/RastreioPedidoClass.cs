using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CRMAPI.Models;

namespace CRMAPI.Classes.RastreioPedido
{
    public class RastreioPedidoClass : RastreioPedido
    {
        public RastreioPedidoClass(RastreiaPedidoModel objRastreiaPedidoModel) : base(objRastreiaPedidoModel)
        {

        }

        public string RetornaIDTransportador()
        {
            return CarregaCodigoFornecedorSAP();
        }

        private string CarregaCodigoFornecedorSAP()
        {
            try
            {                
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("Select OINV.BPLId, OINV.CardCode, ");
                stringSQL.AppendLine("OINV.DocEntry, INV12.Carrier ");
                stringSQL.AppendLine("from OINV ");
                stringSQL.AppendLine("INNER JOIN ORDR ON OINV.CardCode=ORDR.CardCode ");
                stringSQL.AppendLine("INNER JOIN INV12 ON INV12.DocEntry=OINV.DocEntry ");
                //OINV.Serial = Recebe o Número Serial da NFE
                stringSQL.AppendLine("where OINV.Serial='" + NumeroNotaFiscal + "' ");
                //ORDR.DocEntry = Recebe o Número do Pedido SAP
                stringSQL.AppendLine("and ORDR.DocEntry=" + NumeroPedidoSAP + " ");
                //ORDR.BPLId = Recebe o código da empresa
                stringSQL.AppendLine("and ORDR.BPLId=" + IDEmpresa + " ");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                if (ConsultaSAP.Rows.Count > 0)
                {
                    foreach (DataRow row in ConsultaSAP.Rows)
                    {
                        return Consulta_TRANSPORTADORA_FORNECEDOR(row["Carrier"].ToString());
                    }
                }               
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar o código do fornecedor do SAP.");
            }

            return "0";
        }

        private string Consulta_TRANSPORTADORA_FORNECEDOR(string CodigoClienteSAP)
        {
            try
            {
                DataTable outputTable = new DataTable();

                using (SqlConnection dbConnection = new SqlConnection(objComunicacaoCRM.getString()))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_TRANSPORTADORA_FORNECEDOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.VarChar, 8000, "CodigoClienteSAP"));

                    dbCommand.Parameters["@CodigoClienteSAP"].Value = CodigoClienteSAP;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            return row["IDTransportador"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

                throw new Exception("Erro Consulta_TRANSPORTADORA_FORNECEDOR.");
            }

            return "0";
        }
    }
}