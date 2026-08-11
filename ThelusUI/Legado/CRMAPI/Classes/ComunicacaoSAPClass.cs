using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAPbobsCOM;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace CRMAPI.Classes
{
    public class ComunicacaoSAPClass : ComunicacaoSAPClienteClass
    {
        public string SLDServer { get; set; }
        public string Server { get; set; }
        public string DbUserName { get; set; }
        public string DbPassword { get; set; }
        public string CompanyDB { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        /*Variavel comunicação SAP*/
        Company OBJCompany = new Company();

        /*Aprovação Documentos*/
        public int AprovacaoNumero { get; set; }
        public string AprovacaoUsuario { get; set; }
        public string AprovacaoUsuarioSenha { get; set; }
        public string AprovacaoHistorico { get; set; }
        public string AprovacaoDecisao { get; set; }

        /*Chave de Esboço SAP*/
        public int EsbocoChaveSAP { get; set; }

        /*Dados Para Adicionar Pedido*/
        public string EsbocoNovoPedidoSAP { get; set; }
        public DateTime DataEntregaPedido { get; set; }

        /*Dados Para Adicionar Nota*/
        public string EsbocoNovaNotaSAP { get; set; }

        /*Atualiza dados pedidos SAP*/
        public int NumeroPedidoSAP { get; set; }
        public string HistoricoPedidoSAP { get; set; }

        /*Atualiza dados Nota SAP*/
        public int NumeroPrimarioNotaSAP { get; set; }
        public string HistoricoNotaSAP { get; set; }

        /*Atualização cliente*/
        //public string CodigoClienteSAP { get; set; }

        /*Ordens de Produção*/
        public string OrdemProducaoTipoOrdem { get; set; } //P=Padrão E=Especial D=Desmontagem
        public string OrdemProducaoCodigoProdutoSAP { get; set; }
        public string OrdemProducaoCodigoProdutoOrigemSAP { get; set; }
        public string OrdemProducaoStatus { get; set; } //C=Cancelado, F=Fechado, P=Planejado, L=Liberado 
        public double OrdemProducaoQuantidadePlanejada { get; set; }
        public string OrdemProducaoCodigoDepositoSAP { get; set; }
        public string OrdemProducaoUsuarioSAP { get; set; }
        public int OrdemProducaoPrioridade { get; set; }
        public DateTime OrdemProducaoDataEmissao { get; set; }
        public DateTime OrdemProducaoDataInicio { get; set; }
        public DateTime OrdemProducaoDataVencimento { get; set; } //Data de Saída Pedido SAP
        public int OrdemProducaoNumeroPedidoSAP { get; set; }
        public int OrdemProducaoU_IB_SeqPedido { get; set; }
        public int OrdemProducaoU_MF_NUMOS { get; set; }
        public int OrdemProducaoNumeroPrimarioSAP { get; set; }
        public string OrdemProducaoNovoNumero { get; set; }
        public string OrdemProducaoObservacao { get; set; }
        public string OrdemProducaoTipoEmbarque { get; set; }

        public string CodigoUsuarioCRM { get; set; }

        /*Fechamento de Fatura*/
        public string NumeroFatura { get; set; }
        public DateTime DataVencimentoFatura { get; set; }

        /*Dados para query diretamente no banco de dados*/
        private string ConexaoPrincipalSAP = System.Configuration.ConfigurationManager.AppSettings["StringConexaoBDSAP"];

        private string ConexaoContingenciaSAP = System.Configuration.ConfigurationManager.AppSettings["StringConexaoBDSAP"];

        /*Dados para atualizar Liberação produção*/
        public string LiberarProducaoLiberado { get; set; }

        /*Dados classificação comercial*/
        public string ClassificacaoComercialSAP { get; set; }

        public static string strConec { get; set; }

        public string conectarSAP()
        {
            string erro = "";

            if (OBJCompany.Connected == false)
            {

                /*Atribuição de dados para conexão*/
                this.SLDServer = System.Configuration.ConfigurationManager.AppSettings["ServerSLDSAP"];
                this.Server = System.Configuration.ConfigurationManager.AppSettings["ServerSAP"];
                this.DbUserName = System.Configuration.ConfigurationManager.AppSettings["UsuarioBancoSAP"];
                this.DbPassword = System.Configuration.ConfigurationManager.AppSettings["SenhaBancoSAP"];
                this.CompanyDB = System.Configuration.ConfigurationManager.AppSettings["BancoDadosSAP"];
                this.UserName = System.Configuration.ConfigurationManager.AppSettings["UsuarioAcessoSAP"];
                this.Password = System.Configuration.ConfigurationManager.AppSettings["SenhaUsuarioAcessoSAP"];

                OBJCompany.SLDServer = this.SLDServer;

                //OBJCompany.LicenseServer = "WIN-31TPRDV86IS:30000";
                OBJCompany.Server = this.Server;
                OBJCompany.language = BoSuppLangs.ln_Portuguese_Br;

                OBJCompany.DbServerType = BoDataServerTypes.dst_MSSQL2019;
                OBJCompany.DbUserName = this.DbUserName;
                OBJCompany.DbPassword = this.DbPassword;
                OBJCompany.CompanyDB = this.CompanyDB;

                OBJCompany.UserName = this.UserName;
                OBJCompany.Password = this.Password;

                int con = OBJCompany.Connect();

                if (con != 0)//sucesso
                {
                    erro = OBJCompany.GetLastErrorDescription();
                }
            }

            //Atribui Classe para Application
            if (erro == "")
            {
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application["ApplicationComunicacaoSAP"] = this;
                HttpContext.Current.Application.UnLock();
            }

            return erro;
        }

        public void desconectarSAP()
        {
            OBJCompany.Disconnect();
        }

        public DataTable RetornaDadosConsultaSAP(string StringSQL)
        {
            DataTable OBJDataTable = new DataTable();
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(ConexaoPrincipalSAP))
            {
                try
                {
                    using (SqlCommand dbCommand = new SqlCommand(StringSQL, dbConnection))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        //Deixa o Timeout da consulta com cerca de 4 minutos
                        dbCommand.CommandTimeout = 340;

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            OBJDataTable.Load(dataReader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return OBJDataTable;
        }

        public string AprovarAutorizacao()
        {
            string erro = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                ApprovalRequestsService OBJApprovalRequestsService = null;
                ApprovalRequestParams OBJApprovalRequestParams = null;
                ApprovalRequest OBJApprovalRequest = null;
                ApprovalRequestDecision OBJApprovalRequestDecision = null;

                CompanyService OBJCompanyService = (CompanyService)OBJCompany.GetCompanyService();
                OBJApprovalRequestsService = (ApprovalRequestsService)OBJCompanyService.GetBusinessService(ServiceTypes.ApprovalRequestsService);
                OBJApprovalRequestParams = (ApprovalRequestParams)OBJApprovalRequestsService.GetDataInterface(ApprovalRequestsServiceDataInterfaces.arsApprovalRequestParams);

                OBJApprovalRequestParams.Code = this.AprovacaoNumero;
                OBJApprovalRequest = OBJApprovalRequestsService.GetApprovalRequest(OBJApprovalRequestParams);
                OBJApprovalRequestDecision = OBJApprovalRequest.ApprovalRequestDecisions.Add();

                //Verifica o tipo da decisão
                switch (AprovacaoDecisao)
                {
                    case "Aprovado":
                        OBJApprovalRequestDecision.Status = BoApprovalRequestDecisionEnum.ardApproved;
                        break;
                    case "Reprovado":
                        OBJApprovalRequestDecision.Status = BoApprovalRequestDecisionEnum.ardNotApproved;
                        break;
                    case "Pendente":
                        OBJApprovalRequestDecision.Status = BoApprovalRequestDecisionEnum.ardPending;
                        break;
                    default:
                        OBJApprovalRequestDecision.Status = BoApprovalRequestDecisionEnum.ardPending;
                        break;
                }

                OBJApprovalRequestDecision.ApproverUserName = this.AprovacaoUsuario;
                OBJApprovalRequestDecision.ApproverPassword = this.AprovacaoUsuarioSenha;
                OBJApprovalRequestDecision.Remarks = this.AprovacaoHistorico;
                OBJApprovalRequestsService.UpdateRequest(OBJApprovalRequest);

                erro = OBJCompany.GetLastErrorDescription();

                //Desconectar do SAP
                //this.desconectarSAP();
            }
            return erro;
        }

        public string AdicionaPedido()
        {
            string erro = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            Documents OBJEsboco = null;
            OBJEsboco = (SAPbobsCOM.Documents)OBJCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);

            if (OBJEsboco.GetByKey(this.EsbocoChaveSAP) == true)
            {
                OBJEsboco.DocDueDate = this.DataEntregaPedido;
                //OBJEsboco.UserFields.Fields.Item("U_IB_HistPedido").Value = this.HistoricoPedidoSAP;

                if (OBJEsboco.SaveDraftToDocument() == 0)
                {
                    this.EsbocoNovoPedidoSAP = OBJCompany.GetNewObjectKey();
                }

                erro = OBJCompany.GetLastErrorDescription();
            }


            //this.desconectarSAP();

            return erro;
        }

        public string AdicionaNota()
        {
            string erro = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            Documents OBJEsboco = null;
            OBJEsboco = (SAPbobsCOM.Documents)OBJCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);

            if (OBJEsboco.GetByKey(this.EsbocoChaveSAP) == true)
            {
                if (OBJEsboco.SaveDraftToDocument() == 0)
                {
                    this.EsbocoNovaNotaSAP = OBJCompany.GetNewObjectKey();
                }

                erro = OBJCompany.GetLastErrorDescription();
            }

            return erro;
        }

        public string ZeraLimiteClientes(DataTable OBJDataTable)
        {
            string erro = "";
            string HistoricoZeramento = "";
            decimal LimiteAnterior = 0;

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                BusinessPartners OBJParceiro = null;
                OBJParceiro = (SAPbobsCOM.BusinessPartners)OBJCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

                if (OBJDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in OBJDataTable.Rows)
                    {
                        if (erro == "")
                        {
                            this.CodigoClienteSAP = Convert.ToString(row["CardCode"]);
                            LimiteAnterior = Convert.ToDecimal(row["CreditLine"]);

                            if (OBJParceiro.GetByKey(this.CodigoClienteSAP) == true)
                            {
                                CultureInfo cult = new CultureInfo("pt-BR");
                                string dta = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", cult);

                                HistoricoZeramento = OBJParceiro.FreeText;
                                HistoricoZeramento += "\n\n";
                                HistoricoZeramento += "Limite de crédito zerado por inatividade em " + dta + ". ";
                                HistoricoZeramento += "Valor anterior " + LimiteAnterior.ToString("C2") + ". ";

                                OBJParceiro.CreditLimit = 0;
                                OBJParceiro.MaxCommitment = 0;
                                OBJParceiro.FreeText = HistoricoZeramento;
                                OBJParceiro.Update();
                            }

                            erro = OBJCompany.GetLastErrorDescription();
                        }
                    }
                }
                //Desconectar do SAP
                //this.desconectarSAP();
            }
            return erro;
        }

        public string AtualizaHistoricoPedidoSAP()
        {
            string erro = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                Documents OBJEsboco = null;
                Documents OBJPedido = null;

                if (this.NumeroPedidoSAP == 0)
                {
                    if (this.EsbocoChaveSAP != 0)
                    {
                        OBJEsboco = (SAPbobsCOM.Documents)OBJCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);

                        if (OBJEsboco.GetByKey(this.EsbocoChaveSAP) == true)
                        {
                            OBJEsboco.UserFields.Fields.Item("U_IB_HistPedido").Value = this.HistoricoPedidoSAP;

                            OBJEsboco.Update();
                        }

                    }
                }
                else
                {
                    OBJPedido = (SAPbobsCOM.Documents)OBJCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);

                    if (OBJPedido.GetByKey(this.NumeroPedidoSAP))
                    {
                        OBJPedido.UserFields.Fields.Item("U_IB_HistPedido").Value = this.HistoricoPedidoSAP;

                        OBJPedido.Update();
                    }

                }

                erro = OBJCompany.GetLastErrorDescription();

                //Desconectar do SAP
                //this.desconectarSAP();
            }
            return erro;
        }

        public string AtualizaHistoricoNotaSAP()
        {
            string erro = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                Documents OBJNota = null;

                OBJNota = (SAPbobsCOM.Documents)OBJCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);

                if (OBJNota.GetByKey(this.NumeroPrimarioNotaSAP))
                {
                    OBJNota.UserFields.Fields.Item("U_IB_HistPedido").Value += this.HistoricoNotaSAP;

                    OBJNota.Update();
                }

                erro = OBJCompany.GetLastErrorDescription();
            }
            return erro;
        }

        public string GravarOrdemProducaoSAP(DataTable OBJDataTable)
        {
            string erro = "";
            string CodigoItemEstrutura = "";
            double QuantidadeEstrutura = 0;

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                ProductionOrders OBJOrdemProducao = null;
                OBJOrdemProducao = (SAPbobsCOM.ProductionOrders)OBJCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oProductionOrders);

                //Trata tipo da Ordem de produção
                switch (this.OrdemProducaoTipoOrdem)
                {
                    case "S":
                        OBJOrdemProducao.ProductionOrderType = BoProductionOrderTypeEnum.bopotStandard;
                        break;
                    case "P":
                        OBJOrdemProducao.ProductionOrderType = BoProductionOrderTypeEnum.bopotSpecial;
                        break;
                    case "D":
                        OBJOrdemProducao.ProductionOrderType = BoProductionOrderTypeEnum.bopotDisassembly;
                        break;
                    default:
                        OBJOrdemProducao.ProductionOrderType = BoProductionOrderTypeEnum.bopotStandard;
                        break;
                }

                //Trata status Ordem Produção
                switch (this.OrdemProducaoStatus)
                {
                    case "C":
                        OBJOrdemProducao.ProductionOrderStatus = BoProductionOrderStatusEnum.boposCancelled;
                        break;
                    case "L":
                        OBJOrdemProducao.ProductionOrderStatus = BoProductionOrderStatusEnum.boposClosed;
                        break;
                    case "P":
                        OBJOrdemProducao.ProductionOrderStatus = BoProductionOrderStatusEnum.boposPlanned;
                        break;
                    case "R":
                        OBJOrdemProducao.ProductionOrderStatus = BoProductionOrderStatusEnum.boposReleased;
                        break;
                    default:
                        OBJOrdemProducao.ProductionOrderStatus = BoProductionOrderStatusEnum.boposPlanned;
                        break;
                }

                //Cabeçalho
                OBJOrdemProducao.ItemNo = this.OrdemProducaoCodigoProdutoSAP;
                OBJOrdemProducao.PlannedQuantity = this.OrdemProducaoQuantidadePlanejada;
                OBJOrdemProducao.Warehouse = this.OrdemProducaoCodigoDepositoSAP;
                OBJOrdemProducao.Priority = this.OrdemProducaoPrioridade;
                OBJOrdemProducao.PostingDate = this.OrdemProducaoDataEmissao;
                OBJOrdemProducao.StartDate = this.OrdemProducaoDataInicio;
                OBJOrdemProducao.DueDate = this.OrdemProducaoDataVencimento;
                OBJOrdemProducao.ProductionOrderOriginEntry = this.OrdemProducaoNumeroPedidoSAP;
                OBJOrdemProducao.UserFields.Fields.Item("U_IB_SeqPedido").Value = this.OrdemProducaoU_IB_SeqPedido;
                OBJOrdemProducao.UserFields.Fields.Item("U_MF_NUMOS").Value = this.OrdemProducaoU_MF_NUMOS;

                //Grava Produto Relacional quando existir
                if (this.OrdemProducaoCodigoProdutoOrigemSAP != this.OrdemProducaoCodigoProdutoSAP)
                {
                    this.OrdemProducaoObservacao = "Codigo Origem: " + this.OrdemProducaoCodigoProdutoOrigemSAP;
                    OBJOrdemProducao.Remarks = this.OrdemProducaoObservacao;
                }

                //Grava o Tipo de Embarque
                if (this.OrdemProducaoTipoEmbarque != "")
                {
                    OBJOrdemProducao.Remarks += "\n Embarque Imediato: " + this.OrdemProducaoTipoEmbarque;
                }

                //Estrutura 
                if (OBJDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in OBJDataTable.Rows)
                    {
                        CodigoItemEstrutura = row["CodigoItemEstrutura"].ToString();
                        QuantidadeEstrutura = Convert.ToDouble(row["QuantidadeBase"]);
                        OBJOrdemProducao.Lines.Warehouse = Convert.ToString(row["DepositoInsumo"]);

                        OBJOrdemProducao.Lines.ItemNo = CodigoItemEstrutura;
                        OBJOrdemProducao.Lines.BaseQuantity = QuantidadeEstrutura;
                        OBJOrdemProducao.Lines.PlannedQuantity = QuantidadeEstrutura * this.OrdemProducaoQuantidadePlanejada;

                        switch (Convert.ToInt16(row["TipoItem"]))
                        {
                            case 4:
                                OBJOrdemProducao.Lines.ItemType = ProductionItemType.pit_Item;
                                break;
                            case 290:
                                OBJOrdemProducao.Lines.ItemType = ProductionItemType.pit_Resource;
                                break;
                            default:
                                OBJOrdemProducao.Lines.ItemType = ProductionItemType.pit_Item;
                                break;
                        }

                        //Adiciona Linha OP
                        OBJOrdemProducao.Lines.Add();
                    }
                }

                //Adiciona Ordem de Produção e recupera numero da OP
                if (OBJOrdemProducao.Add() != 0)
                {
                    erro = OBJCompany.GetLastErrorDescription();
                }
                else
                {
                    OrdemProducaoNovoNumero = OBJCompany.GetNewObjectKey();
                }

            }


            return erro;
        }

        public string LiberaOrdemProducaoSAP()
        {
            string erro = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                ProductionOrders OBJOrdemProducao = null;
                OBJOrdemProducao = (SAPbobsCOM.ProductionOrders)OBJCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oProductionOrders);

                if (OBJOrdemProducao.GetByKey(this.OrdemProducaoNumeroPrimarioSAP))
                {

                    //Trata status Ordem Produção
                    switch (this.OrdemProducaoStatus)
                    {
                        case "C":
                            OBJOrdemProducao.ProductionOrderStatus = BoProductionOrderStatusEnum.boposCancelled;
                            break;
                        case "L":
                            OBJOrdemProducao.ProductionOrderStatus = BoProductionOrderStatusEnum.boposClosed;
                            break;
                        case "P":
                            OBJOrdemProducao.ProductionOrderStatus = BoProductionOrderStatusEnum.boposPlanned;
                            break;
                        case "R":
                            OBJOrdemProducao.ProductionOrderStatus = BoProductionOrderStatusEnum.boposReleased;
                            break;
                        default:
                            OBJOrdemProducao.ProductionOrderStatus = BoProductionOrderStatusEnum.boposPlanned;
                            break;
                    }

                    //Seta Data e Hora da Liberação
                    OBJOrdemProducao.UserFields.Fields.Item("U_IB_DtLiberacao").Value = DateTime.Now;
                    OBJOrdemProducao.UserFields.Fields.Item("U_IB_HoraLiberacao").Value = DateTime.Now.ToString("HHmm");

                    OBJOrdemProducao.Remarks = "Usuário: " + this.CodigoUsuarioCRM + " " + OBJOrdemProducao.Remarks;

                    //Adiciona Ordem de Produção e recupera numero da OP
                    if (OBJOrdemProducao.Update() != 0)
                    {
                        erro = OBJCompany.GetLastErrorDescription();
                    }
                }
            }

            return erro;
        }

        public string GravaDadosFaturaNotaSAP()
        {
            string erro = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                Documents OBJNota = null;

                OBJNota = (SAPbobsCOM.Documents)OBJCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseInvoices);

                if (OBJNota.GetByKey(this.NumeroPrimarioNotaSAP))
                {
                    OBJNota.DocDueDate = DataVencimentoFatura;
                    OBJNota.UserFields.Fields.Item("U_IB_NumeroFaturaDDA").Value = NumeroFatura;
                    OBJNota.Installments.SetCurrentLine(0);
                    OBJNota.Installments.DueDate = DataVencimentoFatura;
                    OBJNota.Installments.UserFields.Fields.Item("U_MF_NumFat").Value = NumeroFatura;

                    OBJNota.Update();
                }

                erro = OBJCompany.GetLastErrorDescription();
            }
            return erro;
        }

        public string AtualizaAprovacaoPedidoProducao()
        {
            string erro = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                Documents OBJPedido = null;

                OBJPedido = (SAPbobsCOM.Documents)OBJCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);

                if (OBJPedido.GetByKey(this.NumeroPedidoSAP))
                {
                    OBJPedido.UserFields.Fields.Item("U_MF_ApProd").Value = this.LiberarProducaoLiberado;
                    OBJPedido.UserFields.Fields.Item("U_IB_HistPedido").Value += this.HistoricoPedidoSAP ?? "";

                    OBJPedido.Update();
                }

                erro = OBJCompany.GetLastErrorDescription();

            }

            return erro;
        }

        public string ExcluirContato()
        {
            string erro = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                //Contacts Contato = null;
                BusinessPartners ParceiroNegocio = null;

                ParceiroNegocio = (BusinessPartners)OBJCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);

                if (ParceiroNegocio.GetByKey(this.CodigoClienteSAP))
                {
                    ParceiroNegocio.ContactEmployees.SetCurrentLine(this.CodigoClienteLinha);
                    ParceiroNegocio.ContactEmployees.Delete();

                    ParceiroNegocio.Update();
                }

                erro = OBJCompany.GetLastErrorDescription();
            }

            return erro;
        }

        public string AdicionarContato()
        {
            string erro = "";
            int idContato = 0;

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                //Contacts Contato = null;
                BusinessPartners ParceiroNegocio = null;

                ParceiroNegocio = (BusinessPartners)OBJCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);
                //ContactEmployees Contatos = ParceiroNegocio.ContactEmployees;


                if (ParceiroNegocio.GetByKey(this.CodigoClienteSAP))
                {
                    idContato = ParceiroNegocio.ContactEmployees.Count;

                    ParceiroNegocio.ContactEmployees.Add();
                    ParceiroNegocio.ContactEmployees.SetCurrentLine(idContato);

                    ParceiroNegocio.ContactEmployees.Name = this.CodigoClienteTipoContato;
                    ParceiroNegocio.ContactEmployees.FirstName = this.CodigoClientePrimeiroNome;
                    ParceiroNegocio.ContactEmployees.LastName = this.CodigoClienteUltimoNome;
                    ParceiroNegocio.ContactEmployees.E_Mail = this.CodigoClienteEmail;
                    ParceiroNegocio.ContactEmployees.Phone1 = this.CodigoClienteTelefone1;

                    ParceiroNegocio.Update();
                }

                erro = OBJCompany.GetLastErrorDescription();
            }

            return erro;
        }

        public string AtualizaContato()
        {
            string erro = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                //Contacts Contato = null;
                BusinessPartners ParceiroNegocio = null;

                ParceiroNegocio = (BusinessPartners)OBJCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);

                if (ParceiroNegocio.GetByKey(this.CodigoClienteSAP))
                {
                    ParceiroNegocio.ContactEmployees.SetCurrentLine(this.CodigoClienteLinha);
                    ParceiroNegocio.ContactEmployees.Name = this.CodigoClienteTipoContato;
                    ParceiroNegocio.ContactEmployees.FirstName = this.CodigoClientePrimeiroNome;
                    ParceiroNegocio.ContactEmployees.LastName = this.CodigoClienteUltimoNome;
                    ParceiroNegocio.ContactEmployees.E_Mail = this.CodigoClienteEmail;
                    ParceiroNegocio.ContactEmployees.Phone1 = this.CodigoClienteTelefone1;

                    ParceiroNegocio.Update();
                }

                erro = OBJCompany.GetLastErrorDescription();
            }

            return erro;
        }

        public string AtualizarClassificacaoComercialCliente()
        {
            string erro = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                BusinessPartners ParceiroNegocio = null;

                ParceiroNegocio = (BusinessPartners)OBJCompany.GetBusinessObject(BoObjectTypes.oBusinessPartners);

                if (ParceiroNegocio.GetByKey(this.CodigoClienteSAP))
                {
                    ParceiroNegocio.UserFields.Fields.Item("U_MF_CLS_COM").Value = this.ClassificacaoComercialSAP;

                    ParceiroNegocio.Update();
                }

                erro = OBJCompany.GetLastErrorDescription();
            }

            return erro;
        }

        public ComunicacaoSAPClass()
        {

            try
            {
                #region Testando Conexao Principal
                using (SqlConnection dbConnection = new SqlConnection(ConexaoPrincipalSAP))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    //Fecha Conexao
                    dbConnection.Close();

                    strConec = ConexaoPrincipalSAP;

                }
                #endregion

            }
            catch (Exception)
            {

                try
                {
                    #region Testando Conexao Contingencia
                    using (SqlConnection dbConnection = new SqlConnection(ConexaoContingenciaSAP))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        //Fecha Conexao
                        dbConnection.Close();

                        strConec = ConexaoContingenciaSAP;

                    }
                    #endregion
                }
                catch (Exception)
                {
                    strConec = "";//Se nao acessar na De Contingencia nao retornar nada   
                }

            }
        }

        public string getString()
        {
            return strConec;
        }
    }
}