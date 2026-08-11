using System;
using System.Data;
using System.Text;
using VendasWeb.classes;
using System.Data.SqlClient;
using VendasWeb.WEBServiceCRM;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using VendasWeb.WEBServiceSAP.ClassesWEBService;

namespace VendasWeb
{
    public class AdmVendas : GerencialVendas.clsConexao
    {
        #region CAMPOS

        public int IDEmpresa { get; set; }

        public string Empresa { get; set; }

        public string Utilizacao { get; set; }

        public string DataInicial { get; set; }

        public string DataFinal { get; set; }

        public int NumeroPedidoSAP { get; set; }

        public int NumeroPedidoCRM { get; set; }

        public int IDVendedor { get; set; }

        public string Vendedor { get; set; }

        public int Liberacao { get; set; }

        public string LiberadoProducao { get; set; }

        public string Cliente { get; set; }

        public string Status { get; set; }

        public int IDStatus { get; set; }

        public string DataEmissao { get; set; }

        public string DataEntrega { get; set; }

        public string EmbarqueImediato { get; set; }

        public string Comentarios { get; set; }

        public int IDUsuarioOperacao { get; set; }

        public string Cliche { get; set; }

        public string CodigoItemSAP { get; set; }
        
        #endregion

        ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();

        JsonConversao jsonconv = new JsonConversao();

        FuncoesAPIClass OBJApi = new FuncoesAPIClass();

        #region Métodos antigos

        public DataTable RecuperaListaLiberacaoPedidoSAP()
        {
            DataTable OBJDataTable = new DataTable();

            StringBuilder StringSQL = new StringBuilder();

            StringSQL.AppendLine(" DECLARE @IDEmpresa INT = '" + this.IDEmpresa.ToString() + "' ");
            StringSQL.AppendLine(" ,@DataInicial DATE = '" + this.DataInicial + "' ");
            StringSQL.AppendLine(" ,@DataFinal DATE = '" + this.DataFinal + "' ");
            StringSQL.AppendLine(" ,@NumeroPedidoSAP INT = " + this.NumeroPedidoSAP.ToString());
            StringSQL.AppendLine(" ,@NumeroPedidoCRM INT = " + this.NumeroPedidoCRM.ToString());
            StringSQL.AppendLine(" ,@IDVendedor INT = " + this.IDVendedor.ToString());
            StringSQL.AppendLine(" ,@Liberacao INT = " + this.Liberacao.ToString());
            StringSQL.AppendLine(" ,@Cliente VARCHAR(MAX) = '" + this.Cliente + "' ");
            StringSQL.AppendLine(" ,@Status VARCHAR(MAX) = '" + this.Status + "' ");

            StringSQL.AppendLine("select distinct ");
            StringSQL.AppendLine("ORDR.BPLId as IDEmpresa, ");
            StringSQL.AppendLine("ORDR.BPLName AS Empresa, ");
            StringSQL.AppendLine("ORDR.DocEntry as PedidoSAP,  ");
            StringSQL.AppendLine("ORDR.U_IB_CRM_CodPed AS PedidoCRM, ");
            StringSQL.AppendLine("ORDR.CardCode + ' - ' + ORDR.CardName AS Cliente, ");
            StringSQL.AppendLine("OUSG.Usage AS Utilizacao, ");
            StringSQL.AppendLine("(CASE WHEN isnull(ORDR.U_MF_ApProd, 1) = 1 THEN 'Sim' ");
            StringSQL.AppendLine("ELSE 'Não' END) as Liberado,  ");
            StringSQL.AppendLine("convert(varchar(10), ORDR.DocDate, 103) as DataEmissao,  ");
            StringSQL.AppendLine("convert(varchar(10), ORDR.DocDueDate, 103) as DataEntrega,  ");
            StringSQL.AppendLine("ORDR.NumAtCard as EmbarqueImediato, ");
            StringSQL.AppendLine("OSLP.SlpName AS Vendedor, ");
            StringSQL.AppendLine("(CASE WHEN ORDR.CANCELED = 'Y' THEN 'Cancelado' ");
            StringSQL.AppendLine("ELSE(CASE WHEN ORDR.DocStatus = 'C' ");
            StringSQL.AppendLine("THEN 'Fechado' ELSE 'Aberto' END) END) as [Status] ");

            StringSQL.AppendLine("from ORDR ");

            StringSQL.AppendLine("INNER JOIN RDR1 ");
            StringSQL.AppendLine("  ON ORDR.DocEntry = RDR1.DocEntry ");
            StringSQL.AppendLine("INNER JOIN OITM ");
            StringSQL.AppendLine("  ON RDR1.ItemCode = OITM.ItemCode ");
            StringSQL.AppendLine("INNER JOIN OITB ");
            StringSQL.AppendLine("  ON OITM.ItmsGrpCod = OITB.ItmsGrpCod ");
            StringSQL.AppendLine("INNER JOIN OUSG ");
            StringSQL.AppendLine("  ON RDR1.Usage = OUSG.ID ");
            StringSQL.AppendLine("INNER JOIN OSLP ");
            StringSQL.AppendLine("  ON ORDR.SlpCode = OSLP.SlpCode ");

            StringSQL.AppendLine("WHERE ORDR.BPLId = @IDEmpresa ");
            StringSQL.AppendLine("AND CONVERT(DATE, ORDR.DocDate) BETWEEN @DataInicial AND @DataFinal ");
            StringSQL.AppendLine("AND (ORDR.DocEntry = @NumeroPedidoSAP OR 0 = @NumeroPedidoSAP) ");
            StringSQL.AppendLine("AND (ORDR.U_IB_CRM_CodPed = @NumeroPedidoCRM OR 0 = @NumeroPedidoCRM) ");
            StringSQL.AppendLine("AND (ORDR.SlpCode = @IDVendedor OR 0 = @IDVendedor) ");
            StringSQL.AppendLine("AND (ISNULL(ORDR.U_MF_ApProd,1) = @Liberacao OR 2 = @Liberacao) ");
            StringSQL.AppendLine("AND (ORDR.CardCode LIKE '%' + @Cliente + '%' ");
            StringSQL.AppendLine("  OR ORDR.CardName LIKE '%' + @Cliente + '%') ");

            if (this.Status == "Y")
                StringSQL.AppendLine("AND ORDR.CANCELED = @Status ");
            else
                StringSQL.AppendLine("AND (ORDR.DocStatus = @Status OR 'Todos' = @Status) ");

            StringSQL.AppendLine("AND((RDR1.Usage = (select Code from[@MF_UT_BLOQ_PROD]) ");
            StringSQL.AppendLine("OR OITB.ItmsGrpNam = 'PROD - FITA IMP')) ");

            StringSQL.AppendLine("ORDER BY DataEmissao DESC ");

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL.ToString());

            return OBJDataTable;
        }

        public DataTable RecuperaListaLiberacaoPedidoDetalheSAP()
        {
            DataTable OBJDataTable = new DataTable();

            string StringSQL = "";

            StringSQL += "DECLARE @IDEmpresa INT = " + this.IDEmpresa.ToString() + " ";
            StringSQL += " ,@NumeroPedidoSAP INT = " + this.NumeroPedidoSAP.ToString();
            StringSQL += " ,@NumeroPedidoCRM INT = " + this.NumeroPedidoCRM.ToString() + " ";

            StringSQL += "select ";
            //StringSQL += "select distinct ";
            StringSQL += "ORDR.BPLName AS Empresa, ";
            StringSQL += "ORDR.CardCode + ' - ' + ORDR.CardName AS Cliente, ";
            StringSQL += "OUSG.Usage AS Utilizacao, ";
            StringSQL += "(CASE WHEN isnull(ORDR.U_MF_ApProd, 1) = 1 THEN 'Sim' ";
            StringSQL += "ELSE 'Não' END) as Liberado,  ";
            StringSQL += "convert(varchar(10), ORDR.DocDate, 103) as DataEmissao,  ";
            StringSQL += "convert(varchar(10), ORDR.DocDueDate, 103) as DataEntrega,  ";
            StringSQL += "isnull(ORDR.NumAtCard,'') as EmbarqueImediato, ";
            StringSQL += "OSLP.SlpName AS Vendedor, ";
            StringSQL += "isnull(ORDR.U_IB_HistPedido,'') AS Comentarios ";

            StringSQL += "from ORDR ";

            StringSQL += "INNER JOIN RDR1 ";
            StringSQL += "  ON ORDR.DocEntry = RDR1.DocEntry ";
            StringSQL += "INNER JOIN OITM ";
            StringSQL += "  ON RDR1.ItemCode = OITM.ItemCode ";
            StringSQL += "INNER JOIN OITB ";
            StringSQL += "  ON OITM.ItmsGrpCod = OITB.ItmsGrpCod ";
            StringSQL += "INNER JOIN OUSG ";
            StringSQL += "  ON RDR1.Usage = OUSG.ID ";
            StringSQL += "INNER JOIN OSLP ";
            StringSQL += "  ON ORDR.SlpCode = OSLP.SlpCode ";

            StringSQL += "WHERE ORDR.BPLId = @IDEmpresa ";
            StringSQL += "AND (ORDR.DocEntry = @NumeroPedidoSAP OR 0 = @NumeroPedidoSAP) ";
            StringSQL += "AND (ORDR.U_IB_CRM_CodPed = @NumeroPedidoCRM OR 0 = @NumeroPedidoCRM) ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;
        }

        public DataTable RecuperaListaLiberacaoPedidoProdutosSAP()
        {
            DataTable OBJDataTable = new DataTable();

            StringBuilder StringSQL = new StringBuilder();

            StringSQL.AppendLine("DECLARE @IDEmpresa INT = " + this.IDEmpresa.ToString());
            StringSQL.AppendLine(" ,@NumeroPedidoSAP INT = " + this.NumeroPedidoSAP.ToString());
            StringSQL.AppendLine(" ,@NumeroPedidoCRM INT = " + this.NumeroPedidoCRM.ToString());

            StringSQL.AppendLine("select ORDR.BPLId as IDEmpresa, ");
            StringSQL.AppendLine("ORDR.DocEntry as NumeroPedidoSAP, ");
            StringSQL.AppendLine("isnull(ORDR.U_IB_CRM_CodPed, 0) as NumeroPedidoCRM, ");
            StringSQL.AppendLine("OITM.ItemCode as CodigoItemSAP, ");
            StringSQL.AppendLine("OITM.ItemCode + ' - ' + OITM.ItemName as Produto, ");
            StringSQL.AppendLine("convert(int, RDR1.Quantity) as Quantidade, ");
            StringSQL.AppendLine("0 as IDOrdemServico,0 as IDITemSAP, ");
            StringSQL.AppendLine("isnull(RDR1.U_IB_Cliche, '') as Cliche ");

            StringSQL.AppendLine("from ORDR ");

            StringSQL.AppendLine("INNER JOIN RDR1 ");
            StringSQL.AppendLine("  ON ORDR.DocEntry = RDR1.DocEntry ");
            StringSQL.AppendLine("INNER JOIN OITM ");
            StringSQL.AppendLine("  ON RDR1.ItemCode = OITM.ItemCode ");

            StringSQL.AppendLine("WHERE ORDR.BPLId = @IDEmpresa ");
            StringSQL.AppendLine("AND (ORDR.DocEntry = @NumeroPedidoSAP OR 0 = @NumeroPedidoSAP) ");
            StringSQL.AppendLine("AND (ORDR.U_IB_CRM_CodPed = @NumeroPedidoCRM OR 0 = @NumeroPedidoCRM) ");

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL.ToString());

            return OBJDataTable;
        }

        public DataTable RecuperaListaLiberacaoPedidoDetalheSAPModal
            (int IDEmpresa, int NumeroPedidoSAP, int NumeroPedidoCRM, string CodigoItemSAP, string Cliche)
        {
            DataTable OBJDataTable = new DataTable();

            string StringSQL = "";

            StringSQL += "DECLARE @IDEmpresa INT = " + IDEmpresa.ToString();
            StringSQL += " ,@NumeroPedidoSAP INT = " + NumeroPedidoSAP.ToString();
            StringSQL += " ,@NumeroPedidoCRM INT = " + NumeroPedidoCRM.ToString() + " ";
            StringSQL += " ,@Cliche VARCHAR(MAX) = '" + Cliche.ToString() + "' ";
            StringSQL += " ,@CodigoItemSAP VARCHAR(MAX) = '" + CodigoItemSAP.ToString() + "' ";

            StringSQL += "select distinct ";
            StringSQL += "ORDR.CardCode + ' - ' + ORDR.CardName AS Cliente, ";
            StringSQL += "ORDR.BPLName AS Empresa, ";
            StringSQL += "ORDR.DocEntry as PedidoSAP, ";
            StringSQL += "(CASE WHEN(ORDR.DocStatus = 'O' AND ORDR.CANCELED = 'N') THEN 'Aberto' ";
            StringSQL += "WHEN(ORDR.DocStatus = 'C' AND ORDR.CANCELED = 'N') THEN 'Fechado' ";
            StringSQL += "WHEN ORDR.CANCELED = 'Y' THEN 'Cancelado' END) StatusPedidoSAP, ";
            StringSQL += "ISNULL(ORDR.U_IB_CRM_CodPed, 0) as PedidoCRM,  ";
            StringSQL += "convert(varchar(10), ORDR.DocDate, 103) as DataEmissao,  ";
            StringSQL += "convert(varchar(10), ORDR.DocDueDate, 103) as DataEntrega, ";
            StringSQL += "isnull(ORDR.NumAtCard, '') as EmbarqueImediato, ";
            StringSQL += "OSLP.SlpName AS Vendedor, ";
            StringSQL += "OITM.ItemCode + ' - ' + OITM.ItemName as Produto, ";
            StringSQL += "isnull(RDR1.U_IB_Cliche, '') + ' -  ' + ISNULL(OITM_2.ItemName,'') AS Cliche, ";
            StringSQL += "ISNULL(OITM_2.PicturName, '') AS ImagemCliche ";

            StringSQL += "from ORDR ";

            StringSQL += "INNER JOIN RDR1 ";
            StringSQL += "  ON ORDR.DocEntry = RDR1.DocEntry ";
            StringSQL += "INNER JOIN OITM ";
            StringSQL += "  ON RDR1.ItemCode = OITM.ItemCode ";
            StringSQL += "INNER JOIN OITB ";
            StringSQL += "  ON OITM.ItmsGrpCod = OITB.ItmsGrpCod ";
            StringSQL += "INNER JOIN OUSG ";
            StringSQL += "  ON RDR1.Usage = OUSG.ID ";
            StringSQL += "INNER JOIN OSLP ";
            StringSQL += "  ON ORDR.SlpCode = OSLP.SlpCode ";
            StringSQL += "INNER JOIN OITM OITM_2 ";
            StringSQL += "  ON RDR1.U_IB_Cliche = OITM_2.ItemCode ";

            StringSQL += "WHERE ORDR.BPLId = @IDEmpresa ";
            StringSQL += "AND ORDR.DocEntry = @NumeroPedidoSAP ";
            StringSQL += "AND(ORDR.U_IB_CRM_CodPed = @NumeroPedidoCRM OR 0 = @NumeroPedidoCRM) ";
            StringSQL += "AND OITM.ItemCode = @CodigoItemSAP ";
            StringSQL += "AND RDR1.U_IB_Cliche = @Cliche ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;
        }

        #endregion

        public string LiberacaoPedidoProducao()
        {
            string retorno = "";
            string JSONLiberacaoPedidoProducao = "";

            WSLiberacaoPedidoProducao ObjLiberacaoPedidoProducao = new WSLiberacaoPedidoProducao();

            //Carrega Objeto para enviar
            ObjLiberacaoPedidoProducao.NumeroPrimario = this.NumeroPedidoSAP;
            ObjLiberacaoPedidoProducao.LiberaPedido = this.Liberacao;
            ObjLiberacaoPedidoProducao.HistoricoPedido = this.Comentarios;

            //Transforma em JSON para enviar para o WEB SERVICE
            JSONLiberacaoPedidoProducao = jsonconv.ConverteObjectParaJSon<WSLiberacaoPedidoProducao>(ObjLiberacaoPedidoProducao);

            retorno = OBJApi.LiberaPedidoProducaoSAPCRMAPI(JSONLiberacaoPedidoProducao);

            return retorno;
        }

        public string GravaPedidoLiberacaoProducao()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_PEDIDO_LIBERACAO_PROD", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@Comentarios", SqlDbType.VarChar, 8000, "Comentarios"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Liberacao", SqlDbType.Int, 0, "Liberacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.NumeroPedidoCRM;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.NumeroPedidoSAP;
                    dbCommand.Parameters["@Comentarios"].Value = this.Comentarios;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuarioOperacao;
                    dbCommand.Parameters["@Liberacao"].Value = this.Liberacao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção do cliente";
                }
            }

            return erro;
        }

        #region Métodos novos

        public DataTable RetornaListaStatusPedidos()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_STATUS_PEDIDOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable RetornaListaPedidoLiberacaoProducao()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PEDIDO_LIBERACAO_PROD", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.VarChar, 8000, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.VarChar, 8000, "DataFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoCRM", SqlDbType.Int, 0, "NumeroPedidoCRM"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDVendedor", SqlDbType.Int, 0, "IDVendedor"));
                    dbCommand.Parameters.Add(new SqlParameter("@Liberacao", SqlDbType.Int, 0, "Liberacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Cliente", SqlDbType.VarChar, 8000, "Cliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                    dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;
                    dbCommand.Parameters["@NumeroPedidoCRM"].Value = this.NumeroPedidoCRM;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.NumeroPedidoSAP;
                    dbCommand.Parameters["@IDVendedor"].Value = this.IDVendedor;
                    dbCommand.Parameters["@Liberacao"].Value = this.Liberacao;
                    dbCommand.Parameters["@Cliente"].Value = this.Cliente;
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable RetornaListaPedidoLiberacaoProducaoDetalhe()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PEDIDO_LIBERACAO_PROD_DETALHE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoCRM", SqlDbType.Int, 0, "NumeroPedidoCRM"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@NumeroPedidoCRM"].Value = this.NumeroPedidoCRM;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.NumeroPedidoSAP;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable RetornaListaPedidoLiberacaoProducaoDetalheProdutos()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PEDIDO_LIBERACAO_PROD_DETALHE_PRODUTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoCRM", SqlDbType.Int, 0, "NumeroPedidoCRM"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@NumeroPedidoCRM"].Value = this.NumeroPedidoCRM;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.NumeroPedidoSAP;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable RetornaListaPedidoLiberacaoProducaoDetalheProdutosModal()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PEDIDO_LIBERACAO_PROD_DETALHE_PRODUTOS_MODAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoCRM", SqlDbType.Int, 0, "NumeroPedidoCRM"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@Cliche", SqlDbType.VarChar, 8000, "Cliche"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoItemSAP", SqlDbType.VarChar, 8000, "CodigoItemSAP"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@NumeroPedidoCRM"].Value = this.NumeroPedidoCRM;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.NumeroPedidoSAP;
                    dbCommand.Parameters["@Cliche"].Value = this.Cliche;
                    dbCommand.Parameters["@CodigoItemSAP"].Value = this.CodigoItemSAP;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return outputTable;
        }

        #endregion

    }
}