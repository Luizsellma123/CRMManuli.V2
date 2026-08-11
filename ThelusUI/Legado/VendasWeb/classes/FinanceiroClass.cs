using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;
using VendasWeb.WEBServiceCRM;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using VendasWeb.WEBServiceSAP.ClassesWEBService;

namespace VendasWeb.classes
{
    public class FinanceiroClass : clsConexao
    {
        ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();
        JsonConversao jsonconv = new JsonConversao();
        FuncoesAPIClass OBJApi = new FuncoesAPIClass();

        public string CodigoEmpresa { get; set; }
        public string NumeroPedidoCRM { get; set; }
        public string NumeroPedidoSAP { get; set; }
        public string NumeroEsbocoSAP { get; set; }
        public string CodigoClienteSAP { get; set; }
        public string ConsultaCliente { get; set; }
        public string StatusPedidos { get; set; }
        public string SituacaoPedido { get; set; }

        /*Variavel análise Esboco*/
        public int IDUsuarioSAP { get; set; }
        public string AnalisePedido { get; set; }
        public string HistoricoDetalhado { get; set; }
        public string Historico { get; set; } //Campo deve ser limitado a 254 caracteres
        public string HistoricoPedido { get; set; }
        public int IDMotivo { get; set; } //Campo deve ser limitado a 254 caracteres
        public int IDUsuarioCRM { get; set; }
        public int IDEmpresa { get; set; }
        public int IDPedido { get; set; }
        public int IDStatus { get; set; }
        public int IDCliente { get; set; }
        public int DiasCancelamento { get; set; }
        public string DataHistorico { get; set; }
        public string UsuarioAprovacao { get; set; }
        public string UsuarioAprovacaoSenha { get; set; }

        /*Dados Bancarios*/
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string ContaCorrente { get; set; }
        public string NumeroRemessa { get; set; }
        public int IDBanco { get; set; }
        public int IDAgencia { get; set; }
        public int IDContaCorrente { get; set; }
        public int IDStatusRemessa { get; set; }
        public DateTime DataRemessa { get; set; }
        public string DropOpcaoFiltro { get; set; } //Opcao do Combo disponivel na tela
        public string TextoFiltro { get; set; } //Valor digitado 

        /*Dados Pesquisa Condições*/
        public string ValorConsulta { get; set; }
        public bool LiberadoPolitica { get; set; }
        public bool CondicaoAVista { get; set; }
        public int IDCondicaoPagamento { get; set; }

        public string DataInicial { get; set; }
        public string DataFinal { get; set; }

        public DataTable RecuperaPedidosSAP()
        {
            DataTable OBJDataTable = new DataTable();

            StringBuilder StringSQL = new StringBuilder("");

            string ConsultaStatus = "";

            if (this.StatusPedidos == "Pendentes")
            {
                ConsultaStatus = "'C','N','Y'";
            }

            if (this.StatusPedidos == "Autorizados")
            {
                ConsultaStatus = "'C','N','W'";
            }

            if (this.StatusPedidos == "Recusados")
            {
                ConsultaStatus = "'C','Y','W','A','P'";
            }

            if (this.StatusPedidos == "Todos")
            {
                ConsultaStatus = "''";
            }

            #region Primeira versão

            //string StringSQLOld = "";

            //StringSQLOld += "SELECT convert(varchar(10),OWDD.DocDate,103) as DataAutorizacao, ";
            //StringSQLOld += "(case	when OWDD.[Status]='C' then 'Cancelado' when OWDD.[Status]='N' then 'Rejeitado' when OWDD.[Status]='W' then 'Pendente' when OWDD.[Status]='Y' then 'Aprovado' end) as Situacao, ";
            //StringSQLOld += "T0.CardCode + ' - ' + T1.CardName NomeCliente, T0.BPLId Empresa, isnull(T0.U_IB_CRM_CodPed,'') ";
            //StringSQLOld += "PedidoCRM, T0.DocEntry ChaveEsboco, isnull(ORDR.DocEntry,'') PedidoSAP, ";
            //StringSQLOld += "isnull(ORDR.DocTotal,ODRF.DocTotal) - ";
            //StringSQLOld += "isnull((select sum(OINV.DocTotal) from OINV ";
            //StringSQLOld += "INNER JOIN INV1 ON OINV.DocEntry = INV1.DocEntry ";
            //StringSQLOld += "where INV1.BaseType = '17' and INV1.BaseEntry = ORDR.DocEntry ";
            //StringSQLOld += "and OINV.CANCELED = 'N'),0) ";
            //StringSQLOld += "TotalPedido, ";
            //StringSQLOld += "COALESCE((SELECT WDD1.Remarks ";
            //StringSQLOld += "FROM OWDD O ";
            //StringSQLOld += "INNER JOIN WDD1 ON O.WddCode = Wdd1.WddCode ";
            //StringSQLOld += "where O.DraftEntry = T0.DocEntry ";
            //StringSQLOld += "group by WDD1.Remarks ";
            //StringSQLOld += "FOR XML PATH(''), TYPE).value('.[1]', 'VARCHAR(MAX)'), '') Historico ";
            //StringSQLOld += "from OWDD INNER JOIN ODRF T0 ON T0.DocEntry=OWDD.DraftEntry INNER JOIN OCRD T1 ON T0.CardCode=T1.CardCode ";
            //StringSQLOld += "LEFT JOIN ORDR ON ORDR.DocEntry=OWDD.DocEntry ";
            //StringSQLOld += "LEFT JOIN ODRF ON ODRF.DocEntry=OWDD.DraftEntry ";
            //StringSQLOld += "LEFT JOIN OCRD ON OCRD.CardCode=isnull(ORDR.CardCode,ODRF.CardCode) ";
            //StringSQLOld += "where OWDD.ObjType='17'  AND OWDD.ProcesStat NOT IN ('C') AND not exists ";
            //StringSQLOld += "( ";
            //StringSQLOld += "select * from OWDD T2 where T2.DraftEntry=OWDD.DraftEntry and T2.ObjType='17' and T2.[Status] in (" + ConsultaStatus + ") ";
            //StringSQLOld += ") ";
            //StringSQLOld += "and isnull(ORDR.DocEntry,'') like '%" + this.NumeroPedidoSAP + "%' and isnull(T0.U_IB_CRM_CodPed,'') like '%" + this.NumeroPedidoCRM + "%' and T0.DocEntry like '%" + this.NumeroEsbocoSAP + "%' ";
            //StringSQLOld += "and (isnull(OCRD.CardCode,'') like '%" + this.ConsultaCliente + "%' OR isnull(OCRD.CardName,'') like '%" + this.ConsultaCliente + "%') ";
            //StringSQLOld += "and isnull(ORDR.BPLId,ODRF.BPLId) like '%" + this.CodigoEmpresa + "%' ";
            //StringSQLOld += "group by OWDD.DocDate, T0.CardCode, T1.CardName, T0.BPLId, T0.U_IB_CRM_CodPed, T0.DocEntry, ORDR.DocEntry, OWDD.[Status], ";
            //StringSQLOld += "ORDR.DocTotal, ODRF.DocTotal ";
            //StringSQLOld += "order by NomeCliente ";

            #endregion

            #region Primeira versão otimizada

            StringSQL.AppendLine("SELECT convert(varchar(10),OWDD.DocDate,103) as DataAutorizacao, ");
            StringSQL.AppendLine("(case	");
            StringSQL.AppendLine("  when OWDD.[Status]='C' then 'Cancelado' ");
            StringSQL.AppendLine("  when OWDD.[Status]='N' then 'Rejeitado' ");
            StringSQL.AppendLine("  when OWDD.[Status]='W' then 'Pendente' ");
            StringSQL.AppendLine("  when OWDD.[Status]='Y' then 'Aprovado' ");
            StringSQL.AppendLine("end) as Situacao, ");
            StringSQL.AppendLine("T0.CardCode + ' - ' + T1.CardName NomeCliente, ");
            StringSQL.AppendLine("T0.BPLId Empresa, ");
            StringSQL.AppendLine("isnull(T0.U_IB_CRM_CodPed,'') PedidoCRM, ");
            StringSQL.AppendLine("T0.DocEntry ChaveEsboco, ");
            StringSQL.AppendLine("isnull(ORDR.DocEntry,'') PedidoSAP, ");
            StringSQL.AppendLine("isnull(ORDR.DocTotal,ODRF.DocTotal) ");
            StringSQL.AppendLine(" - ");
            StringSQL.AppendLine("isnull( ");
            StringSQL.AppendLine("  (select sum(OINV.DocTotal) from OINV ");
            StringSQL.AppendLine("  INNER JOIN INV1 ");
            StringSQL.AppendLine("      ON OINV.DocEntry = INV1.DocEntry ");
            StringSQL.AppendLine("  where INV1.BaseType = '17' ");
            StringSQL.AppendLine("  and INV1.BaseEntry = ORDR.DocEntry ");
            StringSQL.AppendLine("  and OINV.CANCELED = 'N') ");
            StringSQL.AppendLine(",0) ");            
            StringSQL.AppendLine("TotalPedido, ");
            StringSQL.AppendLine("COALESCE( ");
            StringSQL.AppendLine("  (SELECT WDD1.Remarks ");
            StringSQL.AppendLine("  FROM OWDD O ");
            StringSQL.AppendLine("  INNER JOIN WDD1 ");
            StringSQL.AppendLine("      ON O.WddCode = Wdd1.WddCode ");
            StringSQL.AppendLine("  where O.DraftEntry = T0.DocEntry ");
            StringSQL.AppendLine("  group by WDD1.Remarks ");
            StringSQL.AppendLine("  FOR XML PATH(''), TYPE).value('.[1]', 'VARCHAR(MAX)'), '') Historico ");
            StringSQL.AppendLine(" ");
            StringSQL.AppendLine("from OWDD ");
            StringSQL.AppendLine("INNER JOIN ODRF T0  ");
            StringSQL.AppendLine("  ON T0.DocEntry=OWDD.DraftEntry ");
            StringSQL.AppendLine("INNER JOIN OCRD T1 ");
            StringSQL.AppendLine("  ON T0.CardCode=T1.CardCode ");
            StringSQL.AppendLine("LEFT JOIN ORDR ");
            StringSQL.AppendLine("  ON ORDR.DocEntry=OWDD.DocEntry ");
            StringSQL.AppendLine("LEFT JOIN ODRF ");
            StringSQL.AppendLine("  ON ODRF.DocEntry=OWDD.DraftEntry ");
            StringSQL.AppendLine("LEFT JOIN OCRD ");
            StringSQL.AppendLine("  ON OCRD.CardCode=isnull(ORDR.CardCode,ODRF.CardCode) ");
            StringSQL.AppendLine(" ");
            StringSQL.AppendLine("where OWDD.ObjType='17' ");
            StringSQL.AppendLine("AND OWDD.ProcesStat NOT IN ('C') AND not exists ");
            StringSQL.AppendLine("  ( ");
            StringSQL.AppendLine("  select * from OWDD T2 ");
            StringSQL.AppendLine("  where T2.DraftEntry=OWDD.DraftEntry");
            StringSQL.AppendLine("  and T2.ObjType='17' ");
            StringSQL.AppendLine("  and T2.[Status] in (" + ConsultaStatus + ") ");
            StringSQL.AppendLine("  ) ");
            StringSQL.AppendLine("and isnull(ORDR.DocEntry,'') like '%" + this.NumeroPedidoSAP + "%' ");
            StringSQL.AppendLine("and isnull(T0.U_IB_CRM_CodPed,'') like '%" + this.NumeroPedidoCRM + "%' ");
            StringSQL.AppendLine("and T0.DocEntry like '%" + this.NumeroEsbocoSAP + "%' ");
            StringSQL.AppendLine("and (isnull(OCRD.CardCode,'') like '%" + this.ConsultaCliente + "%'  ");
            StringSQL.AppendLine("  OR isnull(OCRD.CardName,'') like '%" + this.ConsultaCliente + "%') ");
            StringSQL.AppendLine("and isnull(ORDR.BPLId,ODRF.BPLId) like '%" + this.CodigoEmpresa + "%' ");
            StringSQL.AppendLine("and convert(date, isnull(ORDR.DocDate,ODRF.DocDate)) between convert(date,'" + this.DataInicial + "') ");
            StringSQL.AppendLine("                                                         and convert(date,'" + this.DataFinal + "')");
            StringSQL.AppendLine(" ");
            StringSQL.AppendLine("group by OWDD.DocDate, ");
            StringSQL.AppendLine("  T0.CardCode, ");
            StringSQL.AppendLine("  T1.CardName, ");
            StringSQL.AppendLine("  T0.BPLId, ");
            StringSQL.AppendLine("  T0.U_IB_CRM_CodPed,");
            StringSQL.AppendLine("  T0.DocEntry, ");
            StringSQL.AppendLine("  ORDR.DocEntry, ");
            StringSQL.AppendLine("  OWDD.[Status], ");
            StringSQL.AppendLine("  ORDR.DocTotal, ");
            StringSQL.AppendLine("  ODRF.DocTotal ");
            StringSQL.AppendLine(" ");
            StringSQL.AppendLine("order by NomeCliente ");

            #endregion

            #region Segunda versão otimizada

            //StringSQL.AppendLine
            //(@"
            //SELECT 
            //    CONVERT(VARCHAR(10), OWDD.DocDate, 103) AS DataAutorizacao,
            //    CASE 
            //        WHEN OWDD.[Status] = 'C' THEN 'Cancelado' 
            //        WHEN OWDD.[Status] = 'N' THEN 'Rejeitado' 
            //        WHEN OWDD.[Status] = 'W' THEN 'Pendente' 
            //        WHEN OWDD.[Status] = 'Y' THEN 'Aprovado' 
            //        ELSE ''
            //    END AS Situacao,
            //    T0.CardCode + ' - ' + T1.CardName AS NomeCliente,
            //    T0.BPLId AS Empresa,
            //    ISNULL(T0.U_IB_CRM_CodPed, '') AS PedidoCRM,
            //    T0.DocEntry AS ChaveEsboco,
            //    ISNULL(ORDR.DocEntry, '') AS PedidoSAP,
            //    ISNULL(SUM(OINV.DocTotal), 0) AS TotalPedido,
            //    COALESCE(MAX(WDD1.Remarks), '') AS Historico

            //FROM 
            //    OWDD
            //    INNER JOIN ODRF T0 
		          //  ON T0.DocEntry = OWDD.DraftEntry
            //    INNER JOIN OCRD T1 
		          //  ON T0.CardCode = T1.CardCode
            //    LEFT JOIN ORDR 
		          //  ON ORDR.DocEntry = OWDD.DocEntry
            //    LEFT JOIN ODRF 
		          //  ON ODRF.DocEntry = OWDD.DraftEntry
            //    LEFT JOIN OCRD 
		          //  ON OCRD.CardCode = ISNULL(ORDR.CardCode, ODRF.CardCode)
            //    LEFT JOIN WDD1 
		          //  ON OWDD.WddCode = WDD1.WddCode
            //    LEFT JOIN INV1 
		          //  ON INV1.BaseEntry = ORDR.DocEntry 
		          //  AND INV1.BaseType = '17'
	           // LEFT JOIN OINV 
		          //  ON OINV.DocEntry=INV1.DocEntry 
		          //  AND OINV.CANCELED = 'N'
            //");

            //StringSQL.AppendLine("WHERE ");
            //StringSQL.AppendLine("    OWDD.ObjType='17' ");
            //StringSQL.AppendLine("AND OWDD.ProcesStat NOT IN ('C') AND not exists ");
            //StringSQL.AppendLine("  ( ");
            //StringSQL.AppendLine("  select * from OWDD T2 ");
            //StringSQL.AppendLine("  where T2.DraftEntry=OWDD.DraftEntry ");
            //StringSQL.AppendLine("  and T2.ObjType='17' ");
            //StringSQL.AppendLine("  and T2.[Status] in (" + ConsultaStatus + ") ");
            //StringSQL.AppendLine("  ) ");
            //StringSQL.AppendLine("and isnull(ORDR.DocEntry,'') like '%" + this.NumeroPedidoSAP + "%' ");
            //StringSQL.AppendLine("and isnull(T0.U_IB_CRM_CodPed,'') like '%" + this.NumeroPedidoCRM + "%' ");
            //StringSQL.AppendLine("and T0.DocEntry like '%" + this.NumeroEsbocoSAP + "%' ");
            //StringSQL.AppendLine("and (isnull(OCRD.CardCode,'') like '%" + this.ConsultaCliente + "%'  ");
            //StringSQL.AppendLine("  OR isnull(OCRD.CardName,'') like '%" + this.ConsultaCliente + "%') ");
            //StringSQL.AppendLine("and isnull(ORDR.BPLId,ODRF.BPLId) like '%" + this.CodigoEmpresa + "%' ");
            //StringSQL.AppendLine("and convert(date,ISNULL(ORDR.DocDate,ODRF.DocDate)) ");            
            //StringSQL.AppendLine("    between  ");
            //StringSQL.AppendLine("        convert(date,'" + this.DataInicial + "') ");
            //StringSQL.AppendLine("        and  ");
            //StringSQL.AppendLine("        convert(date,'" + this.DataFinal + "') ");

            //StringSQL.AppendLine
            //(@"
            //GROUP BY 
            //    OWDD.DocDate, 
	           // T0.CardCode, 
	           // T1.CardName, 
	           // T0.BPLId, 
	           // T0.U_IB_CRM_CodPed, 
	           // T0.DocEntry, 
	           // ORDR.DocEntry, 
	           // OWDD.[Status], 
	           // ORDR.DocTotal, 
	           // ODRF.DocTotal

            //ORDER BY 
            //    NomeCliente;
            //");

            #endregion

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL.ToString());

            return OBJDataTable;
        }

        public DataTable RecuperaAutorizacoesEsbocoSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "select WddCode Codigo, OWTM.Name Nome, OWTM.Remarks Descricao from ";
            StringSQL += "OWDD INNER JOIN OWTM ON OWDD.WtmCode = OWTM.WtmCode ";
            StringSQL += "where OWDD.ObjType = '17' and OWDD.[Status]='W' and OWDD.DraftEntry = '" + this.NumeroEsbocoSAP + "'";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;
        }

        public string AtualizaAnalisarEsboco()
        {
            string erro = "";

            DataTable OBJDataTable = new DataTable();

            OBJDataTable = this.RecuperaAutorizacoesEsbocoSAP();

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    OBJComunicacaoSAP.AprovacaoNumero = Convert.ToInt32(row["Codigo"]);
                    OBJComunicacaoSAP.AprovacaoUsuario = this.UsuarioAprovacao;
                    OBJComunicacaoSAP.AprovacaoUsuarioSenha = this.UsuarioAprovacaoSenha;
                    OBJComunicacaoSAP.AprovacaoHistorico = this.Historico; //Campo deve ser limitado a 254 caracteres
                    OBJComunicacaoSAP.AprovacaoDecisao = this.AnalisePedido; //Aprovado, Recusado ou Pendente

                    erro = OBJComunicacaoSAP.AprovarAutorizacao();
                }
            }

            return erro;
        }

        public string AtualizaAnalisarEsbocoAPI()
        {
            string retorno = "";
            string JSONFinanceiro = "";
            WSFinanceiroClass OBJFinanceiroAtualizar = new WSFinanceiroClass();

            //Carrega Objeto para enviar
            OBJFinanceiroAtualizar.CodigoEmpresa = this.CodigoEmpresa ?? "";
            OBJFinanceiroAtualizar.NumeroPedidoCRM = this.NumeroPedidoCRM ?? "";
            OBJFinanceiroAtualizar.NumeroPedidoSAP = this.NumeroPedidoSAP ?? "";
            OBJFinanceiroAtualizar.NumeroEsbocoSAP = this.NumeroEsbocoSAP ?? "";
            OBJFinanceiroAtualizar.ConsultaCliente = this.ConsultaCliente ?? "";
            OBJFinanceiroAtualizar.StatusPedidos = this.StatusPedidos ?? "";
            OBJFinanceiroAtualizar.SituacaoPedido = this.SituacaoPedido ?? "";
            OBJFinanceiroAtualizar.IDUsuarioSAP = this.IDUsuarioSAP;
            OBJFinanceiroAtualizar.AnalisePedido = this.AnalisePedido ?? "";
            OBJFinanceiroAtualizar.HistoricoDetalhado = this.HistoricoDetalhado ?? "";
            OBJFinanceiroAtualizar.Historico = this.Historico ?? "";
            OBJFinanceiroAtualizar.HistoricoPedido = this.HistoricoPedido ?? "";
            OBJFinanceiroAtualizar.IDMotivo = this.IDMotivo;
            OBJFinanceiroAtualizar.IDEmpresa = this.IDEmpresa;
            OBJFinanceiroAtualizar.IDPedido = this.IDPedido;
            OBJFinanceiroAtualizar.IDStatus = this.IDStatus;
            OBJFinanceiroAtualizar.IDCliente = this.IDCliente;
            OBJFinanceiroAtualizar.DataHistorico = this.DataHistorico ?? "";
            OBJFinanceiroAtualizar.IDUsuarioCRM = this.IDUsuarioCRM;
            OBJFinanceiroAtualizar.UsuarioAprovacao = this.UsuarioAprovacao ?? "";
            OBJFinanceiroAtualizar.UsuarioAprovacaoSenha = this.UsuarioAprovacaoSenha ?? "";


            //Transforma em JSON para enviar para o WEB SERVICE
            JSONFinanceiro = jsonconv.ConverteObjectParaJSon<WSFinanceiroClass>(OBJFinanceiroAtualizar);

            retorno = OBJApi.AtualizaAnalisarEsbocoAPI(JSONFinanceiro);

            return retorno;
        }

        public string AdicionaEsbocoPedido()
        {
            string erro = "";

            OBJComunicacaoSAP.EsbocoChaveSAP = Convert.ToInt32(this.NumeroEsbocoSAP);
            erro = OBJComunicacaoSAP.AdicionaPedido();

            if (erro == "")
            {
                this.NumeroPedidoSAP = OBJComunicacaoSAP.EsbocoNovoPedidoSAP;

                //Atualiza numero do pedido SAP no CRM
                erro = this.AtualizaPedidoSAPCRM();
            }

            return erro;
        }

        public string AdicionaEsbocoPedidoAPI()
        {
            string retorno = "";
            string JSONFinanceiro = "";
            WSFinanceiroClass OBJFinanceiroAtualizar = new WSFinanceiroClass();

            //Carrega Objeto para enviar
            //Carrega Objeto para enviar
            OBJFinanceiroAtualizar.CodigoEmpresa = this.CodigoEmpresa ?? "";
            OBJFinanceiroAtualizar.NumeroPedidoCRM = this.NumeroPedidoCRM ?? "";
            OBJFinanceiroAtualizar.NumeroPedidoSAP = this.NumeroPedidoSAP ?? "";
            OBJFinanceiroAtualizar.NumeroEsbocoSAP = this.NumeroEsbocoSAP ?? "";
            OBJFinanceiroAtualizar.ConsultaCliente = this.ConsultaCliente ?? "";
            OBJFinanceiroAtualizar.StatusPedidos = this.StatusPedidos ?? "";
            OBJFinanceiroAtualizar.SituacaoPedido = this.SituacaoPedido ?? "";
            OBJFinanceiroAtualizar.IDUsuarioSAP = this.IDUsuarioSAP;
            OBJFinanceiroAtualizar.AnalisePedido = this.AnalisePedido ?? "";
            OBJFinanceiroAtualizar.HistoricoDetalhado = this.HistoricoDetalhado ?? "";
            OBJFinanceiroAtualizar.Historico = this.Historico ?? "";
            OBJFinanceiroAtualizar.HistoricoPedido = this.HistoricoPedido ?? "";
            OBJFinanceiroAtualizar.IDMotivo = this.IDMotivo;
            OBJFinanceiroAtualizar.IDEmpresa = this.IDEmpresa;
            OBJFinanceiroAtualizar.IDPedido = this.IDPedido;
            OBJFinanceiroAtualizar.IDStatus = this.IDStatus;
            OBJFinanceiroAtualizar.IDCliente = this.IDCliente;
            OBJFinanceiroAtualizar.DataHistorico = this.DataHistorico ?? "";
            OBJFinanceiroAtualizar.IDUsuarioCRM = this.IDUsuarioCRM;
            OBJFinanceiroAtualizar.UsuarioAprovacao = this.UsuarioAprovacao ?? "";
            OBJFinanceiroAtualizar.UsuarioAprovacaoSenha = this.UsuarioAprovacaoSenha ?? "";


            //Transforma em JSON para enviar para o WEB SERVICE
            JSONFinanceiro = jsonconv.ConverteObjectParaJSon<WSFinanceiroClass>(OBJFinanceiroAtualizar);

            retorno = OBJApi.AdicionaEsbocoPedidoAPI(JSONFinanceiro);

            return retorno;
        }

        public void RetornaUsuarioSenhaSAP()
        {
            DataTable outputTable = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_USUARIO_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuarioCRM;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.UsuarioAprovacao = row["CodigoUsuarioSAP"].ToString();
                                this.UsuarioAprovacaoSenha = row["SenhaUsuarioSAP"].ToString();
                                this.IDUsuarioSAP = Convert.ToInt32(row["IDUsuarioSAP"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public DataTable RecuperaPedidosDetalheSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL += "Select Top 1 convert(varchar(2), OBPL.BPLID) +' - ' + OBPL.BPLName NomeEmpresa, ";
            StringSQL += "ODRF.U_IB_CRM_CodPed PedidoCRM, isnull(ORDR.DocNum, '') PedidoSAP, ODRF.DocEntry NumeroEsboco, ";
            StringSQL += "OCRD.CardCode + ' - ' + OCRD.CardName Cliente, ";
            StringSQL += "isnull(ORDR.DocTotal,ODRF.DocTotal) - ";
            StringSQL += "isnull((select sum(OINV.DocTotal) from OINV ";
            StringSQL += "INNER JOIN INV1 ON OINV.DocEntry = INV1.DocEntry ";
            StringSQL += "where INV1.BaseType = '17' and INV1.BaseEntry = ORDR.DocEntry ";
            StringSQL += "and OINV.CANCELED = 'N'),0) ";
            StringSQL += "TotalPedido, ";
            StringSQL += "isnull(OCTG.PymntGroup, '') CondicaoPagamento, ";
            StringSQL += "ISNULL(ORDR.DocDate,ODRF.DocDate) DataLancamento, ISNULL(ORDR.DocDueDate,ODRF.DocDueDate) DataEntrega, ";
            StringSQL += "ISNULL(ORDR.TaxDate,ODRF.TaxDate) DataDocumento, OUSG.Usage Utilizacao, ";
            StringSQL += "OCRD.CardCode ";
            StringSQL += "from ODRF ";
            StringSQL += "INNER JOIN DRF1 ON DRF1.DocEntry = ODRF.DocEntry ";
            StringSQL += "INNER JOIN OBPL ON OBPL.BPLID = ODRF.BPLId ";
            StringSQL += "LEFT JOIN ORDR ON ORDR.DraftKey = ODRF.Docentry ";
            StringSQL += "LEFT JOIN RDR1 ON RDR1.DocEntry=ORDR.DocEntry ";
            StringSQL += "LEFT JOIN OCRD ON OCRD.CardCode = isnull(ORDR.CardCode, ODRF.CardCode) ";
            StringSQL += "LEFT JOIN OCTG ON OCTG.GroupNum = isnull(ORDR.GroupNum, ODRF.GroupNum) ";
            StringSQL += "LEFT JOIN OUSG ON OUSG.ID=ISNULL(RDR1.Usage,DRF1.Usage) ";
            StringSQL += "where ODRF.DocEntry = '" + this.NumeroEsbocoSAP + "' ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;
        }

        public DataTable RecuperaPedidosDetalheHistoricoSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string StringSQL = "";

            StringSQL = "DECLARE @Decricao VARCHAR(MAX) ";
            StringSQL += "select @Decricao = COALESCE(@Decricao + ',', '') + isnull(WDD1.Remarks, '') from OWDD INNER JOIN WDD1 ON OWDD.WddCode = Wdd1.WddCode ";
            StringSQL += "where ObjType = '17' and DraftEntry = '" + this.NumeroEsbocoSAP + "' and UserID = '" + this.IDUsuarioSAP + "' ";
            StringSQL += "group by WDD1.Remarks ";
            StringSQL += "SELECT @Decricao as Historico ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            return OBJDataTable;
        }

        public DataTable RecuperaMotivos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_MOTIVOS_LIBERACOES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable RecuperaDiasCancelmaneto()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_DIAS_CANCELAMENTO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public string AtualizaHistoricoPedidoCRM()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_HISTORICO_PEDIDO_VENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMotivo", SqlDbType.Int, 0, "IDMotivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataHistorico", SqlDbType.DateTime, 0, "DataHistorico"));
                    dbCommand.Parameters.Add(new SqlParameter("@HistoricoDetalhado", SqlDbType.VarChar, 8000, "HistoricoDetalhado"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@IDMotivo"].Value = this.IDMotivo;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuarioCRM;
                    dbCommand.Parameters["@DataHistorico"].Value = Convert.ToDateTime(this.DataHistorico);
                    dbCommand.Parameters["@HistoricoDetalhado"].Value = this.HistoricoDetalhado;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualização do histórico.";
                }
            }

            return erro;
        }

        public string AtualizaDiasCancelamentoPedidoCRM()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_DIAS_CANCELAMENTO_PEDIDO_VENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@DiasCancelamento", SqlDbType.Int, 0, "DiasCancelamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@DiasCancelamento"].Value = this.DiasCancelamento;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualização data do cancelamento.";
                }
            }

            return erro;
        }

        public void RecuperaHistoricoPedidoCRM()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RECUPERA_OBSERVACAO_PEDIDO_VENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));


                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.HistoricoPedido = row["ObservacaoPedido"].ToString();
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }

        public string RetornaPedidoVendedorCRM()
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
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;


                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro no retorno do pedido ao vendedor.";
                }
            }

            return erro;
        }

        public string AtualizaPedidoSAPCRM()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_PEDIDO_APROVADO_FINANCEIRO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoCRM", SqlDbType.Int, 0, "NumeroPedidoCRM"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "@NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@NumeroPedidoCRM"].Value = this.IDPedido;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.NumeroPedidoSAP;


                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro no aprovação do pedido ao vendedor.";
                }
            }

            return erro;
        }

        public DataTable RecuperaBancosAgenciaContas()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_BANCOS_AGENCIAS_CONTAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Banco", SqlDbType.VarChar, 0, "Banco"));
                    dbCommand.Parameters.Add(new SqlParameter("@Agencia", SqlDbType.VarChar, 0, "Agencia"));
                    dbCommand.Parameters.Add(new SqlParameter("@Conta", SqlDbType.VarChar, 0, "@Conta"));

                    dbCommand.Parameters["@Banco"].Value = this.Banco;
                    dbCommand.Parameters["@Agencia"].Value = this.Banco;
                    dbCommand.Parameters["@Conta"].Value = this.Banco;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())

                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable RecuperaBancos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_BANCOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())

                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable RecuperaBancoAgencias()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_BANCO_AGENCIAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDBanco", SqlDbType.Int, 0, "IDBanco"));

                    dbCommand.Parameters["@IDBanco"].Value = this.IDBanco;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())

                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable RecuperaBancoAgenciaContas()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_BANCO_AGENCIA_CONTAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDBanco", SqlDbType.Int, 0, "IDBanco"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAgencia", SqlDbType.Int, 0, "IDAgencia"));

                    dbCommand.Parameters["@IDBanco"].Value = this.IDBanco;
                    dbCommand.Parameters["@IDAgencia"].Value = this.IDAgencia;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())

                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public DataTable RecuperaStatusRemessas()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_STATUS_REMESSAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())

                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public string GravaDadosPrincipaisRemessa()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_REMESSA_PRINCIPAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDBanco", SqlDbType.Int, 0, "IDBanco"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAgencia", SqlDbType.Int, 0, "IDAgencia"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDConta", SqlDbType.Int, 0, "IDConta"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));
                    dbCommand.Parameters.Add(new SqlParameter("@Data", SqlDbType.DateTime, 0, "Data"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroRemessa", SqlDbType.VarChar, 8000, ParameterDirection.InputOutput, false, 0, 0, "NumeroRemessa", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDBanco"].Value = this.IDBanco;
                    dbCommand.Parameters["@IDAgencia"].Value = this.IDAgencia;
                    dbCommand.Parameters["@IDConta"].Value = this.IDContaCorrente;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuarioCRM;
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatusRemessa;
                    dbCommand.Parameters["@Data"].Value = this.DataRemessa;
                    dbCommand.Parameters["@NumeroRemessa"].Value = this.NumeroRemessa ?? "";

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                    this.NumeroRemessa = (string)dbCommand.Parameters["@NumeroRemessa"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção da remessa ";
                }
            }

            return erro;
        }

        public DataTable RetornaCondicoesPagamentoConfiguracao()
        {
            DataTable outputTable = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CONDICOES_PAGAMENTO_CONFIGURACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@ValorConsulta", SqlDbType.VarChar, 8000, "ValorConsulta"));

                    dbCommand.Parameters["@ValorConsulta"].Value = this.ValorConsulta;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string AnaliDados = "";
                AnaliDados += "|@ValorConsulta:" + this.ValorConsulta;
                AnaliDados += "|JSONPedido:" + LogAuditoria.ClassesAuditoria.LogErroClass.jsonconv.ConverteObjectParaJSon<FinanceiroClass>(this);
                LogAuditoria.ClassesAuditoria.LogErroClass.GravaLOGErroStatic(0, "RetornaCondicoesPagamentoConfiguracao", ex, AnaliDados);
            }

            return outputTable;
        }

        public string GravaConfiguracaoCondicaoPagamento()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";


            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CONFIGURACAO_CONDICAO_PAG", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDCondicao", SqlDbType.Int, 0, "IDCondicao"));
                    dbCommand.Parameters.Add(new SqlParameter("@LiberadoPolitica", SqlDbType.Int, 0, "LiberadoPolitica"));
                    dbCommand.Parameters.Add(new SqlParameter("@CondicaoAVista", SqlDbType.Int, 0, "CondicaoAVista"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDCondicao"].Value = this.IDCondicaoPagamento;
                    dbCommand.Parameters["@LiberadoPolitica"].Value = Convert.ToInt32(this.LiberadoPolitica);
                    dbCommand.Parameters["@CondicaoAVista"].Value = Convert.ToInt32(this.CondicaoAVista);

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao dos grupos do usuário.";

                    string AnaliDados = "";
                    AnaliDados += "|@IDCondicaoPagamento:" + this.IDCondicaoPagamento;
                    AnaliDados += "|@LiberadoPolitica:" + this.LiberadoPolitica;
                    AnaliDados += "|@CondicaoAVista:" + this.CondicaoAVista;
                    AnaliDados += "|JSONFinanceiroClass:" + LogAuditoria.ClassesAuditoria.LogErroClass.jsonconv.ConverteObjectParaJSon<FinanceiroClass>(this);
                    LogAuditoria.ClassesAuditoria.LogErroClass.GravaLOGErroStatic(0, "GravaConfiguracaoCondicaoPagamento", ex, AnaliDados);
                }
            }

            return erro;
        }
    }
}