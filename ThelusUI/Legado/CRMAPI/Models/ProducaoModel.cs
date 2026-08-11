using CRMAPI.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRMAPI.Models
{
    public class ProducaoModel : ConexaoClass
    {
        public int IDEmpresa { get; set; }
        public int IDOrdemServico { get; set; }
        public string CodigoUsuarioCRM { get; set; }

        //ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();
        ComunicacaoServiceLayerSAPClass OBJComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();
        private DebugClass OBJDebug = new DebugClass();

        public string GravaOrdemProducao()
        {
            string erro = "";
            DataTable OBJDataTableEstrutura = new DataTable();
            DataTable OBJDataTableOrdem = new DataTable();

            if (HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"] != null)
            {
                OBJComunicacaoServiceLayerSAP = (ComunicacaoServiceLayerSAPClass)HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"];
            }

            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("ProducaoModel - GravaOrdemProducao()");
                OBJDebug.SetDescricao("Iniciando Gravacao Ordem Produção");
                OBJDebug.GerarDadosDebug();

                OBJDebug.SetDescricao("ProducaoModel: " + OBJDebug.SerializarObjeto(this));
                OBJDebug.GerarDadosDebug();
            }

            OBJDataTableOrdem = this.RecuperaDadosOrdemServico();

            if (OBJDataTableOrdem.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTableOrdem.Rows)
                {
                    if (erro == "")
                    {
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoTipoOrdem = Convert.ToString(row["CodigoSAP"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoCodigoProdutoSAP = Convert.ToString(row["CodigoProdutoSAP"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoCodigoProdutoOrigemSAP = Convert.ToString(row["ProdutoOrigemSAP"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoStatus = Convert.ToString(row["StatusOrdemProducao"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoQuantidadePlanejada = Convert.ToDouble(row["QuantidadePlanejada"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoCodigoDepositoSAP = Convert.ToString(row["CodigoDepositoSAP"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoPrioridade = Convert.ToInt32(row["Prioridade"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoDataEmissao = Convert.ToDateTime(row["DataEmissao"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoDataInicio = Convert.ToDateTime(row["DataEmissao"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoDataVencimento = Convert.ToDateTime(row["DataVencimento"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoNumeroPedidoSAP = Convert.ToInt32(row["NumeroPedidoSAP"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoU_IB_SeqPedido = Convert.ToInt32(row["IDItemSAP"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoU_MF_NUMOS = this.IDOrdemServico;
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoTipoEmbarque = Convert.ToString(row["TipoEmbarque"]);

                        //Limpa para não trazer lixo do anterior
                        OBJDataTableEstrutura.Clear();
                        OBJDataTableEstrutura = OrdemProducaoRetornaEstrutura(OBJComunicacaoServiceLayerSAP.OrdemProducaoCodigoProdutoSAP, OBJComunicacaoServiceLayerSAP.OrdemProducaoCodigoDepositoSAP);

                        if (OBJDebug.GetGeraDebug())
                        {
                            OBJDebug.SetOperacao("ProducaoModel - GravaOrdemProducao() - Passo 1");
                            OBJDebug.SetDescricao("OBJDataTableEstrutura: " + OBJDebug.SerializarObjeto(OBJDataTableEstrutura));
                            OBJDebug.GerarDadosDebug();
                        }

                        erro = OBJComunicacaoServiceLayerSAP.GravarOrdemProducaoSAP(OBJDataTableEstrutura);

                        if(erro == "")
                        {
                            erro = AtualizaNumeroOrdemProducaoSAP(OBJComunicacaoServiceLayerSAP.OrdemProducaoNumeroPedidoSAP, OBJComunicacaoServiceLayerSAP.OrdemProducaoU_IB_SeqPedido, OBJComunicacaoServiceLayerSAP.OrdemProducaoNovoNumero);
                        }
                    }
                }
            }else
            {
                erro = AtualizaOrdemProducaoGeradoSAP();
            }

            return erro;
        }

        public string LiberaOrdemProducao()
        {
            string erro = "";
            DataTable OBJDataTableOrdem = new DataTable();

            if (HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"] != null)
            {
                OBJComunicacaoServiceLayerSAP = (ComunicacaoServiceLayerSAPClass)HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"];
            }

            OBJDataTableOrdem = this.RecuperaOrdemProducaoOrdemServico();

            if (OBJDataTableOrdem.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTableOrdem.Rows)
                {
                    if (erro == "")
                    {
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoNumeroPedidoSAP = Convert.ToInt32(row["NumeroPedidoSAP"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoU_IB_SeqPedido = Convert.ToInt32(row["IDItemSAP"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoNumeroPrimarioSAP = Convert.ToInt32(row["NumeroOrdemProducaoSAP"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoStatus = Convert.ToString(row["StatusOrdemProducao"]);
                        OBJComunicacaoServiceLayerSAP.OrdemProducaoObservacao = Convert.ToString(row["Observacao"]);
                        OBJComunicacaoServiceLayerSAP.CodigoUsuarioCRM = this.CodigoUsuarioCRM;

                        erro = OBJComunicacaoServiceLayerSAP.LiberaOrdemProducaoSAP();

                        if (erro == "")
                        {
                            erro = AtualizaNumeroOrdemProducaoLiberadoSAP(OBJComunicacaoServiceLayerSAP.OrdemProducaoNumeroPedidoSAP, OBJComunicacaoServiceLayerSAP.OrdemProducaoU_IB_SeqPedido, OBJComunicacaoServiceLayerSAP.OrdemProducaoNumeroPrimarioSAP.ToString());
                        }
                    }
                }
            }
            else
            {
                erro = AtualizaOrdemProducaoGeradoSAP();
            }

            return erro;
        }

        public DataTable OrdemProducaoRetornaEstrutura(string OrdemProducaoCodigoProdutoSAP, string OrdemProducaoCodigoDepositoSAP)
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            try
            {
                StringSQL += "select ITT1.Code CodigoItemEstrutura, ITT1.[Type] TipoItem, ITT1.Warehouse DepositoInsumo, ITT1.Quantity QuantidadeBase from OITT ";
                StringSQL += "INNER JOIN ITT1 ON OITT.Code = ITT1.Father ";
                StringSQL += "where OITT.Code = '" + OrdemProducaoCodigoProdutoSAP + "' and OITT.ToWH = '" + OrdemProducaoCodigoDepositoSAP + "' ";

                OBJDataTable = OBJComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(StringSQL);

            }
            catch (Exception ex)
            {
                VendasWeb.LogAuditoria.ClassesAuditoria.LogErroClass OBJLog = new VendasWeb.LogAuditoria.ClassesAuditoria.LogErroClass();
                OBJLog.IDusuario = 0;

                OBJLog.OperacaoAcao = "OrdemProducaoRetornaEstrutura()";
                OBJLog.LogErro(ex, "ProducaoModel");
            }

            return OBJDataTable;
        }

        public DataTable RecuperaDadosOrdemServico()
        {

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PEDIDOS_ORDENS_SERVICO_SAP", dbConnection);

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, "IDOrdemServico"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDOrdemServico"].Value = this.IDOrdemServico;

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                //erro = "erro ao recuperar Ordem Serviço";
            }


            return outputTable;
        }

        public DataTable RecuperaOrdemProducaoOrdemServico()
        {

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_ORDENS_SERVICO_ORDEM_PRODUCAO_SAP", dbConnection);

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, "IDOrdemServico"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDOrdemServico"].Value = this.IDOrdemServico;

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                //erro = "erro ao recuperar Ordem Serviço";
            }


            return outputTable;
        }

        public string AtualizaNumeroOrdemProducaoSAP(int NumeroPedidoSAP, int IDItemSAP, string NumeroOrdemProducaoSAP)
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_ORDEM_SERVICO_ORDEM_PRODUCAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, "IDOrdemServico"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDItemSAP", SqlDbType.Int, 0, "IDItemSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroOrdemProducaoSAP", SqlDbType.Int, 0, "NumeroOrdemProducaoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDOrdemServico"].Value = this.IDOrdemServico;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuarioCRM;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = NumeroPedidoSAP;
                    dbCommand.Parameters["@IDItemSAP"].Value = IDItemSAP;
                    dbCommand.Parameters["@NumeroOrdemProducaoSAP"].Value = Convert.ToInt32(NumeroOrdemProducaoSAP ?? "0");

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualização do número de Ordem de Produção.";
                }
            }

            return erro;
        }

        public string AtualizaNumeroOrdemProducaoLiberadoSAP(int NumeroPedidoSAP, int IDItemSAP, string NumeroOrdemProducaoSAP)
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_ORDEM_SERVICO_ORDEM_PRODUCAO_LIBERADO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, "IDOrdemServico"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDItemSAP", SqlDbType.Int, 0, "IDItemSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroOrdemProducaoSAP", SqlDbType.Int, 0, "NumeroOrdemProducaoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDOrdemServico"].Value = this.IDOrdemServico;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuarioCRM;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = NumeroPedidoSAP;
                    dbCommand.Parameters["@IDItemSAP"].Value = IDItemSAP;
                    dbCommand.Parameters["@NumeroOrdemProducaoSAP"].Value = Convert.ToInt32(NumeroOrdemProducaoSAP ?? "0");

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualização do número de Ordem de Produção.";
                }
            }

            return erro;
        }

        public string AtualizaOrdemProducaoGeradoSAP()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_ORDEM_SERVICO_GERADO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDOrdemServico", SqlDbType.Int, 0, "IDOrdemServico"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 8000, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDOrdemServico"].Value = this.IDOrdemServico;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuarioCRM;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualização do número de Ordem de Produção.";
                }
            }

            return erro;
        }
    }
}