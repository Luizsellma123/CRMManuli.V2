using CRMAPI.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRMAPI.Models
{
    public class PedidoModelClass : ConexaoClass
    {
        //ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();
        ComunicacaoServiceLayerSAPClass OBJComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        public int IDEmpresa { get; set; }
        public int IDPedido { get; set; }
        public string NumeroPedidoSAP { get; set; }

        private int IDStatusCRM { get; set; }

        private DebugClass OBJDebug = new DebugClass();

        public string AtualizaHistoricoPedidoSAP()
        {
            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("PedidoModelClass - AtualizaHistoricoPedidoSAP() - Passo 1");
                OBJDebug.SetDescricao("Iniciando Atualização Pedido");
                OBJDebug.GerarDadosDebug();

                OBJDebug.SetDescricao("PedidoModelClass: " + OBJDebug.SerializarObjeto(this));
                OBJDebug.GerarDadosDebug();
            }

            this.CarregaApplication();

            //Limpar campos para evitar lixo
            OBJComunicacaoServiceLayerSAP.LimparCampos();

            string erro = "";
            VendasWeb.pedido OBJPedido = new VendasWeb.pedido();

            //Carrega informações do pedido
            OBJPedido.carregaDadosPedido(IDEmpresa.ToString(), IDPedido.ToString());

            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("PedidoModelClass - AtualizaHistoricoPedidoSAP() - Passo 2");
                OBJDebug.SetDescricao("Iniciando Atualização Pedido");
                OBJDebug.GerarDadosDebug();

                OBJDebug.SetDescricao("OBJPedido: " + OBJDebug.SerializarObjeto(OBJPedido));
                OBJDebug.GerarDadosDebug();
            }

            OBJComunicacaoServiceLayerSAP.NumeroPedidoSAP = Convert.ToInt32(OBJPedido.NumeroPedidoSAP);
            OBJComunicacaoServiceLayerSAP.EsbocoChaveSAP = Convert.ToInt32(OBJPedido.NumeroEsbocoSAP);
            OBJComunicacaoServiceLayerSAP.HistoricoPedidoSAP = OBJPedido.historicoAntigo;
            

            erro = OBJComunicacaoServiceLayerSAP.AtualizaHistoricoPedidoSAP();

            return erro;
        }

        public string AtualizaStatusPedidoSAP()
        {
            if (OBJDebug.GetGeraDebug())
            {
                OBJDebug.SetOperacao("PedidoModelClass - AtualizaHistoricoPedidoSAP() - Passo 1");
                OBJDebug.SetDescricao("Iniciando Atualização Pedido");
                OBJDebug.GerarDadosDebug();

                OBJDebug.SetDescricao("PedidoModelClass: " + OBJDebug.SerializarObjeto(this));
                OBJDebug.GerarDadosDebug();
            }

            this.CarregaApplication();

            //Limpar campos para evitar lixo
            OBJComunicacaoServiceLayerSAP.LimparCampos();

            string erro = "";

            erro = RetornaStatusPedidoSAP();

            if(erro == "")
            {
                erro = AtualizaStatusPedidoCRM();
            }            

            return erro;
        }

        public string RetornaStatusPedidoSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string strSql = "";
            string erro = "";

            try
            {
                // Declaração das variáveis em SQL
                strSql = "DECLARE @DocEntry BIGINT ";
                strSql += "DECLARE @StatusPedido VARCHAR(30) ";
                strSql += "DECLARE @StatusProducao VARCHAR(30) ";

                // Definição dos valores das variáveis em SQL
                strSql += "SET @DocEntry = '" + this.NumeroPedidoSAP + "' ";
                strSql += "SET @StatusPedido = '' ";
                strSql += "SET @StatusProducao = '' ";

                // Consulta SQL
                strSql += "SELECT @StatusPedido = CSTP.IDStatus ";
                strSql += "FROM ORDR ";
                strSql += "LEFT JOIN CRM_MANULI..CRM_STATUS_PEDIDOS CSTP ON CSTP.IDStatus = (CASE WHEN ORDR.CANCELED='Y' THEN 7 WHEN ORDR.DocStatus='O' THEN 3 WHEN ORDR.DocStatus='C' THEN 6 END) ";
                strSql += "WHERE ORDR.DocEntry=@DocEntry ";

                strSql += "SELECT @StatusProducao = ( ";
                strSql += "SELECT STRING_AGG(Status, ', ') AS ConcatenatedStatus ";
                strSql += "FROM ( ";
                strSql += "SELECT ";
                strSql += "CASE ";
                strSql += "WHEN ISNULL(PKL1.PickStatus,'')='R' THEN 'Liberado' ";
                strSql += "WHEN ISNULL(PKL1.PickStatus,'')='Y' THEN 'Efetuado' ";
                strSql += "WHEN ISNULL(PKL1.PickStatus,'')='C' THEN 'Faturado' ";
                strSql += "WHEN ISNULL(PKL1.PickStatus,'')='P' THEN 'Parcial' ";
                strSql += "WHEN ISNULL(PKL1.PickStatus,'')='D' THEN 'Parcial' ";
                strSql += "WHEN ISNULL(PKL1.PickStatus,'')='' THEN ";
                strSql += "CASE WHEN ISNULL(OWOR.DocNum,'0')<>'0' THEN 'Producao' ELSE 'Liberado' END ";
                strSql += "END AS Status ";
                strSql += "FROM RDR1 ";
                strSql += "LEFT JOIN OWOR ON OWOR.OriginNum=RDR1.DocEntry AND OWOR.ItemCode=RDR1.ItemCode AND OWOR.U_IB_SeqPedido=RDR1.LineNum AND OWOR.[Status] NOT IN ('C','L') ";
                strSql += "LEFT JOIN PKL1 ON RDR1.DocEntry=PKL1.OrderEntry ";
                strSql += "WHERE RDR1.DocEntry=@DocEntry AND RDR1.ObjType='17' AND ISNULL(PKL1.OrderLine,RDR1.LineNum)=RDR1.LineNum AND RDR1.LineStatus='O' ";
                strSql += ") AS Subquery ";
                strSql += ") ";

                strSql += "IF CHARINDEX('Producao', @StatusProducao) > 0 ";
                strSql += "BEGIN ";
                strSql += "SET @StatusPedido = '5'; ";
                strSql += "END ";
                strSql += "ELSE ";
                strSql += "BEGIN ";
                strSql += "IF(@StatusPedido='3') ";
                strSql += "BEGIN ";

                //Se Faturado
                strSql += "SELECT @StatusPedido=";
                strSql += "(CASE ";
                strSql += "WHEN COUNT(CASE WHEN RDR1.LineStatus <> 'C' THEN 1 END) = 0 THEN '6' ";
                strSql += "WHEN COUNT(CASE WHEN ORDR.DocStatus <> 'C' THEN 1 END) = 0 THEN '6' ";
                strSql += "ELSE '3' ";
                strSql += "END) ";
                strSql += "FROM ORDR ";
                strSql += "INNER JOIN RDR1 ON RDR1.DocEntry = ORDR.DocEntry ";
                strSql += "WHERE ORDR.CANCELED = 'N' AND ORDR.DocEntry = @DocEntry ";

                //Se Cancelado
                strSql += "SELECT @StatusPedido='7' ";
                strSql += "FROM ORDR ";
                strSql += "WHERE ORDR.CANCELED='Y' AND ORDR.DocEntry = @DocEntry ";

                strSql += "END ";
                strSql += "END; ";

                strSql += "SELECT @StatusPedido IDStatusCRM; ";

                if (OBJDebug.GetGeraDebug())
                {
                    OBJDebug.SetOperacao("PedidoModelClass - RetornaStatusPedidoSAP() - Passo 1");
                    OBJDebug.SetDescricao("Iniciando Recuperação Status SAP");
                    OBJDebug.GerarDadosDebug();

                    OBJDebug.SetDescricao("strSql: " + strSql);
                    OBJDebug.GerarDadosDebug();
                }


                OBJDataTable = OBJComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(strSql);

                if (OBJDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in OBJDataTable.Rows)
                    {
                        this.IDStatusCRM = Convert.ToInt32(row["IDStatusCRM"]);
                    }
                }
            }catch(Exception ex)
            {
                erro = ex.Message;
            }

            return erro;
        }

        public void CarregaApplication()
        {
            //Atribui variavel Global para local Service Layer
            if (HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"] != null)
            {
                OBJComunicacaoServiceLayerSAP = (ComunicacaoServiceLayerSAPClass)HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"];
            }
        }

        public string AtualizaStatusPedidoCRM()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_STATUS_PEDIDO_VENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatusCRM;


                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Atualiza Status Pedido. ERRO: "+ ex.Message;
                }

                return erro;
            }
        }
    }
}