using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseRetornoDados : clsConexao
    {
        public string DocNum { get; set; }
        public string DraftKey { get; set; }
        public string NumeroEsbocoSAP { get; set; }
        public string Documento { get; set; }
        public string QuantidadePendente { get; set; }

        public string AtualizaPedidoSAP()
        {
            string erro = "";

            if (this.DocNum != "" && this.DocNum != "0" && this.DocNum != null)
            {

                //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
                try
                {

                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_PEDIDO_VENDA_NUMERO_PEDIDO_ESBOCO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroEsbocoSAP", SqlDbType.Int, 0, "NumeroEsbocoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroEsbocoSAPNovo", SqlDbType.Int, 0, "NumeroEsbocoSAPNovo"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.DocNum;
                        dbCommand.Parameters["@NumeroEsbocoSAP"].Value = this.NumeroEsbocoSAP;
                        dbCommand.Parameters["@NumeroEsbocoSAPNovo"].Value = this.DraftKey;

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }


                }
                catch (Exception ex)
                {
                    erro = "Erro na importação da tabela de empresas.";
                }
            }
            return erro;
        }

        public string AtualizaProducaoSAP()
        {
            string erro = "";

            if (this.Documento != "" && this.Documento != "0" && this.Documento != null)
            {

                //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
                try
                {

                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();
                                                               
                        SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_PEDIDO_VENDA_NUMERO_PRODUCAO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@QuantidadePendente", SqlDbType.Decimal, 0, "QuantidadePendente"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@NumeroPedidoSAP"].Value = Convert.ToInt32(this.Documento);
                        dbCommand.Parameters["@QuantidadePendente"].Value = Convert.ToDecimal(this.QuantidadePendente.Replace(".",","));

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }


                }
                catch (Exception ex)
                {
                    erro = "Erro na importação da tabela de empresas.";
                }
            }
            return erro;
        }
    }
}