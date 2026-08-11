using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using CRMAPI.Classes.ClassesOperacao;

namespace CRMAPI.Classes
{
    public class ComunicacaoServiceLayerSAPClass : ComunicacaoServiceLayerCamposSAPClass
    {

        private string PatchFunction(string url)
        {
            string erro = "";

            var client = new RestClient(String.Format("{0}", this.URLServiceLayerSAP + url));
            client.Timeout = -1;

            var request = new RestRequest(Method.PATCH);
            request.AddHeader("Content-Type", "application/json");
            request.AddCookie("B1SESSION", this.OBJComunicacaoServiceLayerLoginRetorno.SessionId);
            request.AddJsonBody(JSONEnvio);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                OBJComunicacaoServiceLayerRetorno = JsonConvert.DeserializeObject<ComunicacaoServiceLayerRetornoSAPClass>(response.Content);
                erro = "(" + OBJComunicacaoServiceLayerRetorno.error.code + ") " + OBJComunicacaoServiceLayerRetorno.error.message.value + ".";
            }

            return erro;
        }

        private string PostFunction(string url)
        {
            string erro = "";

            var client = new RestClient(String.Format("{0}", this.URLServiceLayerSAP + url));
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddCookie("B1SESSION", this.OBJComunicacaoServiceLayerLoginRetorno.SessionId);
            request.AddJsonBody(JSONEnvio);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                OBJComunicacaoServiceLayerRetorno = JsonConvert.DeserializeObject<ComunicacaoServiceLayerRetornoSAPClass>(response.Content);
                erro = "(" + OBJComunicacaoServiceLayerRetorno.error.code + ") " + OBJComunicacaoServiceLayerRetorno.error.message.value + ".";
            }
            else
            {
                JSONRetorno = response.Content;
            }

            return erro;
        }

        private string DeleteFunction(string url)
        {
            string erro = "";

            var client = new RestClient(String.Format("{0}", this.URLServiceLayerSAP + url));
            client.Timeout = -1;

            var request = new RestRequest(Method.DELETE);
            //request.AddHeader("Content-Type", "application/json");
            request.AddCookie("B1SESSION", this.OBJComunicacaoServiceLayerLoginRetorno.SessionId);
            //request.AddJsonBody(JSONEnvio);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                OBJComunicacaoServiceLayerRetorno = JsonConvert.DeserializeObject<ComunicacaoServiceLayerRetornoSAPClass>(response.Content);
                erro = "(" + OBJComunicacaoServiceLayerRetorno.error.code + ") " + OBJComunicacaoServiceLayerRetorno.error.message.value + ".";
            }

            return erro;
        }

        private string GetFunction(string url)
        {
            string erro = "";

            var client = new RestClient(String.Format("{0}", this.URLServiceLayerSAP + url));
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddCookie("B1SESSION", this.OBJComunicacaoServiceLayerLoginRetorno.SessionId);
            //request.AddJsonBody(JSONEnvio);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                //OBJComunicacaoServiceLayerRetorno = JsonConvert.DeserializeObject<ComunicacaoServiceLayerRetornoSAPClass>(response.Content);
                //erro = "(" + OBJComunicacaoServiceLayerRetorno.error.code + ") " + OBJComunicacaoServiceLayerRetorno.error.message.value + ".";
            }
            else
            {
                JSONRetorno = response.Content;
            }

            return erro;
        }

        public string RetornaInformacaoGET()
        {
            string erro = "";
            string Funcao = "";

            erro = this.conectarSAP();

            Funcao = "/BusinessPartners('CLI0018609')";
            erro = GetFunction(Funcao);

            return erro;
        }

        public virtual string conectarSAP()
        {
            string erro = "";
            JSONEnvio = "";

            if ((this.ValidoAte != null && this.ValidoAte <= DateTime.Now) || this.OBJComunicacaoServiceLayerLoginRetorno == null)
            {

                /*Atribuição de dados para conexão*/
                this.URLServiceLayerSAP = System.Configuration.ConfigurationManager.AppSettings["URLServiceLayerSAP"];
                this.CompanyDB = System.Configuration.ConfigurationManager.AppSettings["BancoDadosSAP"];
                this.UserName = System.Configuration.ConfigurationManager.AppSettings["UsuarioAcessoSAP"];
                this.Password = System.Configuration.ConfigurationManager.AppSettings["SenhaUsuarioAcessoSAP"];

                this.OBJComunicacaoServiceLayerLogin = new ComunicacaoServiceLayerLoginSAPClass();
                this.OBJComunicacaoServiceLayerLogin.CompanyDB = this.CompanyDB;
                this.OBJComunicacaoServiceLayerLogin.UserName = this.UserName;
                this.OBJComunicacaoServiceLayerLogin.Password = this.Password;
                JSONEnvio = JsonConvert.SerializeObject(OBJComunicacaoServiceLayerLogin);


                var client = new RestClient(String.Format("{0}", this.URLServiceLayerSAP + "/Login"));
                client.Timeout = -1;

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(JSONEnvio);

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() == "OK")
                {
                    OBJComunicacaoServiceLayerLoginRetorno = JsonConvert.DeserializeObject<ComunicacaoServiceLayerLoginRetornoSAPClass>(response.Content);
                    this.DataAcesso = DateTime.Now;
                    this.ValidoAte = DateTime.Now.AddMinutes(OBJComunicacaoServiceLayerLoginRetorno.SessionTimeout - 1);
                }
            }

            //Atribui Classe para Application
            if (erro == "")
            {
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application["ApplicationComunicacaoServiceLayerSAP"] = this;
                HttpContext.Current.Application.UnLock();
            }

            return erro;
        }

        public string RetornaNumeroPedidoSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string NumeroPedidoSAP = "";
            string stringSQL = "";

            stringSQL = "DECLARE @DraftKey VARCHAR(50) ";
            stringSQL += "DECLARE @DocNum VARCHAR(50) ";
            stringSQL += "DECLARE @PedidoCRM VARCHAR(50) ";
            stringSQL += "DECLARE @Empresa int ";

            stringSQL += "SET @DocNum = '' ";
            stringSQL += "SET @Empresa = 0 ";

            stringSQL += "SELECT @PedidoCRM=U_IB_CRM_CodPed, @Empresa=ODRF.BPLId, @DraftKey=DocEntry FROM dbo.ODRF WHERE DocEntry='" + this.EsbocoChaveSAP.ToString() + "' ";
            stringSQL += "SELECT @DocNum=DocNum FROM ORDR WHERE draftKey=@DraftKey ";

            stringSQL += "IF(@DocNum='') ";
            stringSQL += "BEGIN ";

            stringSQL += "SELECT @DocNum=DocNum, @DraftKey=ORDR.DraftKey FROM ORDR WHERE ORDR.U_IB_CRM_CodPed=@PedidoCRM AND ORDR.BPLId=@Empresa ";

            stringSQL += "END ";

            stringSQL += "SELECT @DocNum AS DocNum, @DraftKey as DraftKey ";

            OBJDataTable = this.RetornaDadosConsultaSAP(stringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    NumeroPedidoSAP = Convert.ToString(row["DocNum"]);
                }
            }

            if (OBJDebug.GetGeraDebug())
            {
                this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - RetornaNumeroPedidoSAP() - Passo 1");
                OBJDebug.SetDescricao("SQL: " + stringSQL);
                OBJDebug.GerarDadosDebug();
            }

            return NumeroPedidoSAP;
        }

        public string RetornaNumeroEsbocoSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string erro = "";
            string stringSQL = "";

            try
            {
                stringSQL = "DECLARE @DraftKey VARCHAR(50) ";
                stringSQL += "DECLARE @DocNum VARCHAR(50) ";
                stringSQL += "DECLARE @PedidoCRM VARCHAR(50) ";
                stringSQL += "DECLARE @Empresa int ";

                stringSQL += "SET @DocNum = '' ";
                stringSQL += "SET @Empresa = 0 ";

                stringSQL += "SELECT @DraftKey=DocEntry FROM dbo.ODRF WHERE ";
                stringSQL += "U_IB_CRM_CodPed='" + this.OBJPedidoVenda.NumeroPedidoCRM + "' and ODRF.BPLId='" + this.OBJPedidoVenda.CodigoEmpresaSAP + "'";

                stringSQL += "SELECT @DraftKey as DraftKey ";

                OBJDataTable = this.RetornaDadosConsultaSAP(stringSQL);

                if (OBJDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in OBJDataTable.Rows)
                    {
                        EsbocoChaveSAP = Convert.ToInt32(row["DraftKey"]);
                    }
                }

            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return erro;
        }

        public string RetornaStatusFinanceiroEsbocoSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string erro = "";
            string stringSQL = "";
            this.AprovacaoEsbocoStatusSAP = "";

            try
            {
                stringSQL = "select WddStatus from ODRF WHERE DocEntry='" + this.EsbocoChaveSAP + "'";

                OBJDataTable = this.RetornaDadosConsultaSAP(stringSQL);

                if (OBJDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in OBJDataTable.Rows)
                    {
                        AprovacaoEsbocoStatusSAP = Convert.ToString(row["WddStatus"]);
                    }
                }

            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return erro;
        }

        public string RetornaHistoricoPedidoSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string NumeroPedidoSAP = "";
            string stringSQL = "";

            stringSQL = "SELECT U_MF_ApProd, U_IB_HistPedido, DocEntry  from ORDR WHERE DocEntry='" + this.NumeroPedidoSAP + "'";


            OBJDataTable = this.RetornaDadosConsultaSAP(stringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    HistoricoAnteriorPedidoSAP = Convert.ToString(row["U_IB_HistPedido"]);
                }
            }

            return NumeroPedidoSAP;
        }

        public string RetornaHistoricoEsbocoSAP()
        {
            DataTable OBJDataTable = new DataTable();
            string NumeroPedidoSAP = "";
            string stringSQL = "";

            stringSQL = "SELECT U_MF_ApProd, U_IB_HistPedido, DocEntry  from ODRF WHERE DocEntry='" + this.EsbocoChaveSAP + "'";


            OBJDataTable = this.RetornaDadosConsultaSAP(stringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    HistoricoAnteriorPedidoSAP = Convert.ToString(row["U_IB_HistPedido"]);
                }
            }

            return NumeroPedidoSAP;
        }

        public DataTable RetornaDadosConsultaSAP(string StringSQL)
        {
            DataTable OBJDataTable = new DataTable();
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(this.ConexaoPrincipalSAP))
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

        public string ZeraLimiteClientes(DataTable OBJDataTable)
        {
            string erro = "";
            string HistoricoZeramento = "";
            JSONEnvio = "";
            string Funcao = "";
            decimal LimiteAnterior = 0;


            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                OCRD_ZerarLimitesClass OBJZerarLimitesClass = new OCRD_ZerarLimitesClass();

                if (OBJDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in OBJDataTable.Rows)
                    {
                        if (erro == "")
                        {
                            this.CodigoClienteSAP = Convert.ToString(row["CardCode"]);
                            LimiteAnterior = Convert.ToDecimal(row["CreditLine"]);
                            HistoricoZeramento = Convert.ToString(row["FreeText"]);

                            CultureInfo cult = new CultureInfo("pt-BR");
                            string dta = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", cult);

                            HistoricoZeramento += "\n\n";
                            HistoricoZeramento += "Limite de crédito zerado por inatividade em " + dta + ". ";
                            HistoricoZeramento += "Valor anterior " + LimiteAnterior.ToString("C2") + ". ";

                            OBJZerarLimitesClass.CreditLimit = 0;
                            OBJZerarLimitesClass.MaxCommitment = 0;
                            OBJZerarLimitesClass.FreeText = HistoricoZeramento;

                            JSONEnvio = JsonConvert.SerializeObject(OBJZerarLimitesClass);

                            Funcao = "/BusinessPartners('" + this.CodigoClienteSAP + "')";
                            erro = this.PatchFunction(Funcao);
                        }
                    }
                }
            }
            return erro;
        }

        public string GravarOrdemProducaoSAP(DataTable OBJDataTable)
        {
            string erro = "";
            JSONEnvio = "";
            JSONRetorno = "";
            string Funcao = "";
            string CodigoItemEstrutura = "";
            double QuantidadeEstrutura = 0;

            if (OBJDebug.GetGeraDebug())
            {
                this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - GravarOrdemProducaoSAP() - Passo 1");
                OBJDebug.SetDescricao("ComunicacaoServiceLayerSAPClass: " + OBJDebug.SerializarObjeto(this));
                OBJDebug.GerarDadosDebug();
            }

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                OWOR_InserirOrdemProducaoClass OBJOrdemProducao = new OWOR_InserirOrdemProducaoClass();
                OWOR_InserirOrdemProducaoRetornoClass OBJOrdemProducaoRetorno = new OWOR_InserirOrdemProducaoRetornoClass();

                OBJOrdemProducao.ProductionOrderType = this.OrdemProducaoTipoOrdem;
                OBJOrdemProducao.ProductionOrderStatus = this.OrdemProducaoStatus;

                //Cabeçalho
                OBJOrdemProducao.ItemNo = this.OrdemProducaoCodigoProdutoSAP;
                OBJOrdemProducao.PlannedQuantity = this.OrdemProducaoQuantidadePlanejada;
                OBJOrdemProducao.Warehouse = this.OrdemProducaoCodigoDepositoSAP;
                OBJOrdemProducao.Priority = this.OrdemProducaoPrioridade;
                OBJOrdemProducao.PostingDate = this.OrdemProducaoDataEmissao;
                OBJOrdemProducao.StartDate = this.OrdemProducaoDataInicio;
                OBJOrdemProducao.DueDate = this.OrdemProducaoDataVencimento;
                OBJOrdemProducao.ProductionOrderOriginEntry = this.OrdemProducaoNumeroPedidoSAP;
                OBJOrdemProducao.U_IB_SeqPedido = this.OrdemProducaoU_IB_SeqPedido;
                OBJOrdemProducao.U_MF_NUMOS = this.OrdemProducaoU_MF_NUMOS;

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

                if (OBJDebug.GetGeraDebug())
                {
                    this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - GravarOrdemProducaoSAP() - Passo 2");
                    OBJDebug.SetDescricao("OBJOrdemProducao: " + OBJDebug.SerializarObjeto(OBJOrdemProducao));
                    OBJDebug.GerarDadosDebug();
                }

                //Estrutura 
                if (OBJDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in OBJDataTable.Rows)
                    {
                        WOR1_InserirOrdemProducaoLinhasClass OBJOrdemProducaoLinhas = new WOR1_InserirOrdemProducaoLinhasClass();

                        CodigoItemEstrutura = row["CodigoItemEstrutura"].ToString();
                        QuantidadeEstrutura = Convert.ToDouble(row["QuantidadeBase"]);
                        OBJOrdemProducaoLinhas.Warehouse = Convert.ToString(row["DepositoInsumo"]);

                        OBJOrdemProducaoLinhas.ItemNo = CodigoItemEstrutura;
                        OBJOrdemProducaoLinhas.BaseQuantity = QuantidadeEstrutura;
                        OBJOrdemProducaoLinhas.PlannedQuantity = QuantidadeEstrutura * this.OrdemProducaoQuantidadePlanejada;
                        OBJOrdemProducaoLinhas.ItemType = Convert.ToInt16(row["TipoItem"]);

                        //Verifica se esta instanciado
                        if (OBJOrdemProducao.ProductionOrderLines == null)
                        {
                            OBJOrdemProducao.ProductionOrderLines = new List<WOR1_InserirOrdemProducaoLinhasClass>();
                        }
                        OBJOrdemProducao.ProductionOrderLines.Add(OBJOrdemProducaoLinhas);
                    }
                }

                if (OBJDebug.GetGeraDebug())
                {
                    this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - GravarOrdemProducaoSAP() - Passo 3");
                    OBJDebug.SetDescricao("OBJOrdemProducaoLinhas: " + OBJDebug.SerializarObjeto(OBJOrdemProducao));
                    OBJDebug.GerarDadosDebug();
                }

                JSONEnvio = JsonConvert.SerializeObject(OBJOrdemProducao);

                Funcao = "/ProductionOrders";
                erro = PostFunction(Funcao);

                //Adiciona Ordem de Produção e recupera numero da OP
                if (erro == "")
                {
                    OBJOrdemProducaoRetorno = JsonConvert.DeserializeObject<OWOR_InserirOrdemProducaoRetornoClass>(JSONRetorno);
                    OrdemProducaoNovoNumero = OBJOrdemProducaoRetorno.AbsoluteEntry.ToString();
                }

            }

            return erro;
        }

        public string LiberaOrdemProducaoSAP()
        {
            string erro = "";
            JSONEnvio = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                OWOR_LiberarOrdemProducaoClass OBJOrdemProducao = new OWOR_LiberarOrdemProducaoClass();

                OBJOrdemProducao.ProductionOrderStatus = this.OrdemProducaoStatus;

                //Seta Data e Hora da Liberação
                OBJOrdemProducao.U_IB_DtLiberacao = DateTime.Now;
                OBJOrdemProducao.U_IB_HoraLiberacao = DateTime.Now.ToString("HHmm");

                OBJOrdemProducao.Remarks = "Usuário: " + this.CodigoUsuarioCRM + " " + this.OrdemProducaoObservacao;

                JSONEnvio = JsonConvert.SerializeObject(OBJOrdemProducao);

                Funcao = "/ProductionOrders(" + this.OrdemProducaoNumeroPrimarioSAP.ToString() + ")";
                erro = this.PatchFunction(Funcao);

            }

            return erro;
        }

        public string AtualizaHistoricoNotaSAP()
        {
            string erro = "";
            JSONEnvio = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                OINV_AtualizarHistoricoClass OBJNota = new OINV_AtualizarHistoricoClass();

                OBJNota.U_IB_HistPedido += this.HistoricoNotaSAP;

                JSONEnvio = JsonConvert.SerializeObject(OBJNota);
                Funcao = "/Invoices(" + this.NumeroPrimarioNotaSAP.ToString() + ")";
                erro = this.PatchFunction(Funcao);
            }
            return erro;
        }

        public string AtualizaHistoricoPedidoSAP()
        {
            string erro = "";
            JSONEnvio = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                if (OBJDebug.GetGeraDebug())
                {
                    this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - AtualizaHistoricoPedidoSAP() - Passo 1");
                    OBJDebug.SetDescricao("Iniciando Atualizacao Historico Pedido");
                    OBJDebug.GerarDadosDebug();

                    OBJDebug.SetDescricao("ComunicacaoServiceLayerSAPClass: " + OBJDebug.SerializarObjeto(this));
                    OBJDebug.GerarDadosDebug();
                }

                if (this.NumeroPedidoSAP == 0)
                {
                    if (this.EsbocoChaveSAP != 0)
                    {
                        ODRF_AtualizarHistoricoClass OBJEsboco = new ODRF_AtualizarHistoricoClass();

                        OBJEsboco.U_IB_HistPedido = this.HistoricoPedidoSAP;

                        JSONEnvio = JsonConvert.SerializeObject(OBJEsboco);

                        if (OBJDebug.GetGeraDebug())
                        {
                            this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - AtualizaHistoricoPedidoSAP() - Passo 2");
                            OBJDebug.SetDescricao("OBJEsboco: " + OBJDebug.SerializarObjeto(OBJEsboco));
                            OBJDebug.GerarDadosDebug();
                        }

                        Funcao = "/Drafts(" + this.EsbocoChaveSAP.ToString() + ")";
                        erro = this.PatchFunction(Funcao);

                    }
                }
                else
                {
                    ORDR_AtualizarHistoricoClass OBJPedido = new ORDR_AtualizarHistoricoClass();

                    OBJPedido.U_IB_HistPedido = this.HistoricoPedidoSAP;

                    JSONEnvio = JsonConvert.SerializeObject(OBJPedido);

                    if (OBJDebug.GetGeraDebug())
                    {
                        this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - AtualizaHistoricoPedidoSAP() - Passo 3");
                        OBJDebug.SetDescricao("OBJEsboco: " + OBJDebug.SerializarObjeto(OBJPedido));
                        OBJDebug.GerarDadosDebug();
                    }

                    Funcao = "/Orders(" + this.NumeroPedidoSAP.ToString() + ")";
                    erro = this.PatchFunction(Funcao);
                }
            }
            return erro;
        }

        public string GravaDadosFaturaNotaSAP()
        {
            string erro = "";
            JSONEnvio = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                OPCH_FechamentoFaturaClass OBJNota = new OPCH_FechamentoFaturaClass();
                PCH6_FechamentoFaturaClass OBJNotaParcela = new PCH6_FechamentoFaturaClass();

                OBJNota.U_IB_NumeroFaturaDDA = NumeroFatura;
                OBJNota.DocDueDate = DataVencimentoFatura;

                OBJNotaParcela.DueDate = DataVencimentoFatura;
                OBJNotaParcela.U_MF_NumFat = NumeroFatura;

                //Verifica se esta instanciado
                if (OBJNota.DocumentInstallments == null)
                {
                    OBJNota.DocumentInstallments = new List<PCH6_FechamentoFaturaClass>();
                }

                OBJNota.DocumentInstallments.Add(OBJNotaParcela);

                JSONEnvio = JsonConvert.SerializeObject(OBJNota);

                Funcao = "/PurchaseInvoices(" + this.NumeroPrimarioNotaSAP.ToString() + ")";
                erro = this.PatchFunction(Funcao);

            }
            return erro;
        }

        public string AprovarAutorizacao()
        {
            string erro = "";
            JSONEnvio = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                OWDD_AprovarAutorizacaoClass OBJApprovalRequest = new OWDD_AprovarAutorizacaoClass();
                WDD1_AprovarAutorizacaoClass OBJApprovalRequestDecision = new WDD1_AprovarAutorizacaoClass();

                if (OBJDebug.GetGeraDebug())
                {
                    this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - AprovarAutorizacao() - Passo 1");
                    OBJDebug.SetDescricao("ComunicacaoServiceLayerSAPClass: " + OBJDebug.SerializarObjeto(this));
                    OBJDebug.GerarDadosDebug();
                }

                //Verifica se esta instanciado
                if (OBJApprovalRequest.ApprovalRequestDecisions == null)
                {
                    OBJApprovalRequest.ApprovalRequestDecisions = new List<WDD1_AprovarAutorizacaoClass>();
                }

                //Verifica o tipo da decisão
                switch (AprovacaoDecisao)
                {
                    case "Aprovado":
                        OBJApprovalRequestDecision.Status = "ardApproved";
                        break;
                    case "Reprovado":
                        OBJApprovalRequestDecision.Status = "ardNotApproved";
                        break;
                    case "Pendente":
                        OBJApprovalRequestDecision.Status = "ardPending";
                        break;
                    default:
                        OBJApprovalRequestDecision.Status = "ardPending";
                        break;
                }

                OBJApprovalRequestDecision.ApproverUserName = this.AprovacaoUsuario;
                OBJApprovalRequestDecision.ApproverPassword = this.AprovacaoUsuarioSenha;
                OBJApprovalRequestDecision.Remarks = this.AprovacaoHistorico;

                OBJApprovalRequest.ApprovalRequestDecisions.Add(OBJApprovalRequestDecision);

                JSONEnvio = JsonConvert.SerializeObject(OBJApprovalRequest);

                if (OBJDebug.GetGeraDebug())
                {
                    this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - AprovarAutorizacao() - Passo 2");
                    OBJDebug.SetDescricao("ComunicacaoServiceLayerSAPClass: " + OBJDebug.SerializarObjeto(this));
                    OBJDebug.GerarDadosDebug();

                    this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - AprovarAutorizacao() - Passo 3");
                    OBJDebug.SetDescricao("OBJApprovalRequest: " + OBJDebug.SerializarObjeto(OBJApprovalRequest));
                    OBJDebug.GerarDadosDebug();

                    this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - AprovarAutorizacao() - Passo 4");
                    OBJDebug.SetDescricao("OBJApprovalRequestDecision: " + OBJDebug.SerializarObjeto(OBJApprovalRequestDecision));
                    OBJDebug.GerarDadosDebug();
                }

                Funcao = "/ApprovalRequests(" + this.AprovacaoNumero.ToString() + ")";
                erro = this.PatchFunction(Funcao);

            }
            return erro;
        }

        public string AdicionaPedido()
        {
            if (OBJDebug.GetGeraDebug())
            {
                this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - AdicionaPedido() - Passo 1");
                OBJDebug.SetDescricao("Iniciando Adicionar Pedido");
                OBJDebug.GerarDadosDebug();

                OBJDebug.SetDescricao("ComunicacaoServiceLayerSAPClass: " + OBJDebug.SerializarObjeto(this));
                OBJDebug.GerarDadosDebug();
            }

            string erro = "";
            JSONEnvio = "";
            JSONRetorno = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            ODRF_AdicionarClass OBJEsboco = new ODRF_AdicionarClass();
            ODRF_AdicionarDocumentoClass OBJEsbocoDocumento = new ODRF_AdicionarDocumentoClass();


            OBJEsbocoDocumento.DocEntry = this.EsbocoChaveSAP;
            //OBJEsbocoDocumento.DocDueDate = this.DataEntregaPedido;

            if (OBJDebug.GetGeraDebug())
            {
                this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - AdicionaPedido() - Passo 2");
                OBJDebug.SetDescricao("OBJEsbocoDocumento: " + OBJDebug.SerializarObjeto(OBJEsbocoDocumento));
                OBJDebug.GerarDadosDebug();
            }

            erro = this.AtualizaEsbocoPedido();

            if (erro == "")
            {

                OBJEsboco.Document = OBJEsbocoDocumento;

                JSONEnvio = JsonConvert.SerializeObject(OBJEsboco);

                if (OBJDebug.GetGeraDebug())
                {
                    this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - AdicionaPedido() - Passo 3");
                    OBJDebug.SetDescricao("OBJEsboco: " + OBJDebug.SerializarObjeto(OBJEsboco));
                    OBJDebug.GerarDadosDebug();
                }

                Funcao = "/DraftsService_SaveDraftToDocument";
                erro = PostFunction(Funcao);

                //Salva Esboço Como Documento
                if (erro == "")
                {
                    this.EsbocoNovoPedidoSAP = RetornaNumeroPedidoSAP();

                    if (OBJDebug.GetGeraDebug())
                    {
                        this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - AdicionaPedido() - Passo 4");
                        OBJDebug.SetDescricao("EsbocoNovoPedidoSAP: " + this.EsbocoNovoPedidoSAP);
                        OBJDebug.GerarDadosDebug();
                    }
                }
            }

            return erro;
        }

        public string AdicionarContato()
        {
            string erro = "";
            JSONEnvio = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {

                OCRD_AdicionarContatosClass OBJParceiro = new OCRD_AdicionarContatosClass();
                OCPR_AdicionarContatosClass OBJParceiroContato = new OCPR_AdicionarContatosClass();

                OBJParceiroContato.Name = this.CodigoClienteTipoContato;
                OBJParceiroContato.FirstName = this.CodigoClientePrimeiroNome;
                OBJParceiroContato.LastName = this.CodigoClienteUltimoNome;
                OBJParceiroContato.E_Mail = this.CodigoClienteEmail;
                OBJParceiroContato.Phone1 = this.CodigoClienteTelefone1;

                //Verifica se esta instanciado
                if (OBJParceiro.ContactEmployees == null)
                {
                    OBJParceiro.ContactEmployees = new List<OCPR_AdicionarContatosClass>();
                }
                OBJParceiro.ContactEmployees.Add(OBJParceiroContato);

                JSONEnvio = JsonConvert.SerializeObject(OBJParceiro);

                Funcao = "/BusinessPartners('" + this.CodigoClienteSAP.ToString() + "')";
                erro = this.PatchFunction(Funcao);

            }

            return erro;
        }

        public string AtualizaContato()
        {
            string erro = "";
            JSONEnvio = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {

                OCRD_AtualizarContatosClass OBJParceiro = new OCRD_AtualizarContatosClass();
                OCPR_AtualizarContatosClass OBJParceiroContato = new OCPR_AtualizarContatosClass();

                OBJParceiroContato.InternalCode = this.InternalCode;
                OBJParceiroContato.Name = this.CodigoClienteTipoContato;
                OBJParceiroContato.FirstName = this.CodigoClientePrimeiroNome;
                OBJParceiroContato.LastName = this.CodigoClienteUltimoNome;
                OBJParceiroContato.E_Mail = this.CodigoClienteEmail;
                OBJParceiroContato.Phone1 = this.CodigoClienteTelefone1;


                //Verifica se esta instanciado
                if (OBJParceiro.ContactEmployees == null)
                {
                    OBJParceiro.ContactEmployees = new List<OCPR_AtualizarContatosClass>();
                }
                OBJParceiro.ContactEmployees.Add(OBJParceiroContato);

                JSONEnvio = JsonConvert.SerializeObject(OBJParceiro);

                Funcao = "/BusinessPartners('" + this.CodigoClienteSAP.ToString() + "')";
                erro = this.PatchFunction(Funcao);

            }

            return erro;
        }

        public string ExcluirContato()
        {
            string erro = "";
            JSONEnvio = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                erro = GetFunction("/BusinessPartners('" + this.CodigoClienteSAP + "')");

                if (erro == "")
                {
                    dynamic dynamicObject = JsonConvert.DeserializeObject<System.Dynamic.ExpandoObject>(this.JSONRetorno);

                    // Acesse a propriedade "ContactEmployees"
                    var contactEmployees = (dynamicObject.ContactEmployees as IEnumerable<dynamic>).ToList();

                    // Filtrar os itens com InternalCode diferente de 3448
                    var filteredList = contactEmployees.Where(item => Convert.ToInt32(item.InternalCode) != this.InternalCode).ToList();

                    // Atribuir a lista filtrada à propriedade "ContactEmployees"
                    dynamicObject.ContactEmployees = filteredList;

                    this.JSONEnvio = JsonConvert.SerializeObject(dynamicObject, new Newtonsoft.Json.Converters.ExpandoObjectConverter());

                    Funcao = "/BusinessPartners('" + this.CodigoClienteSAP.ToString() + "')";
                    erro = this.PatchFunction(Funcao);
                }
            }

            return erro;
        }

        public string AtualizaAprovacaoPedidoProducao()
        {
            string erro = "";
            JSONEnvio = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                ORDR_AtualizaAprovacaoPedidoProducaoClass OBJPedido = new ORDR_AtualizaAprovacaoPedidoProducaoClass();

                //Carrega Histórico antigo SAP
                this.RetornaHistoricoPedidoSAP();

                OBJPedido.U_MF_ApProd = this.LiberarProducaoLiberado;
                OBJPedido.U_IB_HistPedido += (this.HistoricoAnteriorPedidoSAP + " " + this.HistoricoPedidoSAP) ?? "";

                JSONEnvio = JsonConvert.SerializeObject(OBJPedido);

                Funcao = "/Orders(" + this.NumeroPedidoSAP + ")";
                erro = this.PatchFunction(Funcao);
            }

            return erro;
        }

        public string AtualizaAprovacaoEsbocoProducao()
        {
            string erro = "";
            JSONEnvio = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                ODRF_AtualizaAprovacaoPedidoProducaoClass OBJEsboco = new ODRF_AtualizaAprovacaoPedidoProducaoClass();

                //Carrega Histórico antigo SAP
                this.RetornaHistoricoPedidoSAP();

                OBJEsboco.U_MF_ApProd = this.LiberarProducaoLiberado;
                OBJEsboco.U_IB_HistPedido += (this.HistoricoAnteriorPedidoSAP + " " + this.HistoricoPedidoSAP) ?? "";

                JSONEnvio = JsonConvert.SerializeObject(OBJEsboco);

                Funcao = "/Orders(" + this.EsbocoChaveSAP + ")";
                erro = this.PatchFunction(Funcao);
            }

            return erro;
        }

        public string GravarEsbocoPedidoVendaSAP()
        {
            string erro = "";
            JSONEnvio = "";
            JSONRetorno = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                ORDR_InclusaoPedidoClass OBJPedidoVenda = new ORDR_InclusaoPedidoClass();
                ORDR_InclusaoPedidoRetornoClass OBJPedidoVendaRetorno = new ORDR_InclusaoPedidoRetornoClass();

                if (OBJDebug.GetGeraDebug())
                {
                    this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - GravarEsbocoPedidoVendaSAP() - Passo 1");
                    OBJDebug.SetDescricao("Iniciando Gravacao Pedido");
                    OBJDebug.GerarDadosDebug();

                    OBJDebug.SetDescricao("ComunicacaoServiceLayerSAPClass: " + OBJDebug.SerializarObjeto(this));
                    OBJDebug.GerarDadosDebug();
                }

                OBJPedidoVenda.BPL_IDAssignedToInvoice = this.OBJPedidoVenda.CodigoEmpresaSAP;
                OBJPedidoVenda.CardCode = this.OBJPedidoVenda.CodigoClienteSAP;
                OBJPedidoVenda.DocDate = this.OBJPedidoVenda.DataLancamento;
                OBJPedidoVenda.DocDueDate = this.OBJPedidoVenda.DataEntrega;
                OBJPedidoVenda.NumAtCard = this.OBJPedidoVenda.NumeroReferenciaCliente;
                OBJPedidoVenda.OpeningRemarks = this.OBJPedidoVenda.ObservacaoNotaFiscal;
                OBJPedidoVenda.PaymentGroupCode = this.OBJPedidoVenda.CondicaoPagamentoSAP;
                OBJPedidoVenda.SalesPersonCode = this.OBJPedidoVenda.CodigoVendedorSAP;
                OBJPedidoVenda.U_IB_CRM_CodPed = this.OBJPedidoVenda.NumeroPedidoCRM;
                OBJPedidoVenda.U_IB_HistPedido = this.OBJPedidoVenda.HistoricoPedido;
                OBJPedidoVenda.U_IB_Pedido_Cliente = this.OBJPedidoVenda.PedidoCliente;

                //Adiciona Linhas do pedido
                if (this.OBJPedidoVenda.OBJPedidoLinhas.Count > 0)
                {
                    foreach (ComunicacaoServiceLayerPedidoLinhasSAPClass OBJComunicacaoServiceLayerPedidoLinhas in this.OBJPedidoVenda.OBJPedidoLinhas)
                    {
                        RDR1_InclusaoPedidoClass OBJPedidoVendaLinha = new RDR1_InclusaoPedidoClass();

                        OBJPedidoVendaLinha.ItemCode = OBJComunicacaoServiceLayerPedidoLinhas.CodigoItem;
                        OBJPedidoVendaLinha.Price = OBJComunicacaoServiceLayerPedidoLinhas.Valorunitario;
                        OBJPedidoVendaLinha.UnitPrice = OBJComunicacaoServiceLayerPedidoLinhas.Valorunitario;
                        OBJPedidoVendaLinha.Quantity = OBJComunicacaoServiceLayerPedidoLinhas.Quantidade;
                        OBJPedidoVendaLinha.UomCode = OBJComunicacaoServiceLayerPedidoLinhas.CodigoUnidadeMedida;
                        OBJPedidoVendaLinha.Usage = OBJComunicacaoServiceLayerPedidoLinhas.Utilizacao;
                        OBJPedidoVendaLinha.U_IB_Arruela = OBJComunicacaoServiceLayerPedidoLinhas.CodigoArruela;
                        OBJPedidoVendaLinha.U_IB_Cliche = OBJComunicacaoServiceLayerPedidoLinhas.CodigoCliche;
                        OBJPedidoVendaLinha.U_IB_NAT_DESTINACAO = OBJComunicacaoServiceLayerPedidoLinhas.NaturezaDestinacao;
                        OBJPedidoVendaLinha.U_nItem = OBJComunicacaoServiceLayerPedidoLinhas.PosicaoItem;
                        OBJPedidoVendaLinha.U_xPed = OBJComunicacaoServiceLayerPedidoLinhas.NumeroPedidoCliente;
                        OBJPedidoVendaLinha.WarehouseCode = OBJComunicacaoServiceLayerPedidoLinhas.CodigoDeposito;
                        OBJPedidoVendaLinha.FreeText = OBJComunicacaoServiceLayerPedidoLinhas.ObservacaoItem;
                        OBJPedidoVendaLinha.MeasureUnit = OBJComunicacaoServiceLayerPedidoLinhas.NomeUnidadeDeMedida;

                        OBJPedidoVenda.DocumentLines.Add(OBJPedidoVendaLinha);
                    }
                }

                //Adicionar Despesas Adicionais
                if (this.OBJPedidoVenda.OBJPedidoDespesasAdicionais.Count > 0)
                {
                    foreach (ComunicacaoServiceLayerPedidoDespesasAdicionaisSAPClass OBJComunicacaoServiceLayerPedidoDespesasAdicionais in this.OBJPedidoVenda.OBJPedidoDespesasAdicionais)
                    {
                        RDR3_InclusaoPedidoClass OBJPedidoVendaDadosDespesasAdicionais = new RDR3_InclusaoPedidoClass();

                        OBJPedidoVendaDadosDespesasAdicionais.ExpenseCode = OBJComunicacaoServiceLayerPedidoDespesasAdicionais.CodigoDespesa;
                        OBJPedidoVendaDadosDespesasAdicionais.LineTotal = OBJComunicacaoServiceLayerPedidoDespesasAdicionais.ValorDespesa;

                        OBJPedidoVenda.DocumentAdditionalExpenses.Add(OBJPedidoVendaDadosDespesasAdicionais);
                    }
                }

                //Adicionar Extensão Fiscal
                if (this.OBJPedidoVenda.OBJPedidoExtensaoImpostos.Count > 0)
                {
                    foreach (ComunicacaoServiceLayerPedidoExtensaoImpostosSAPClass OBJComunicacaoServiceLayerPedidoExtensaoImpostos in this.OBJPedidoVenda.OBJPedidoExtensaoImpostos)
                    {
                        OBJPedidoVenda.TaxExtension.Incoterms = OBJComunicacaoServiceLayerPedidoExtensaoImpostos.TipoFrete;
                        OBJPedidoVenda.TaxExtension.Carrier = OBJComunicacaoServiceLayerPedidoExtensaoImpostos.CodigoTransportadora;
                    }
                }

                if (OBJDebug.GetGeraDebug())
                {
                    this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - GravarEsbocoPedidoVendaSAP() - Passo 2");
                    OBJDebug.SetDescricao("OBJPedidoVenda: " + OBJDebug.SerializarObjeto(OBJPedidoVenda));
                    OBJDebug.GerarDadosDebug();
                }

                JSONEnvio = JsonConvert.SerializeObject(OBJPedidoVenda);

                Funcao = "/Drafts";
                erro = PostFunction(Funcao);

                //Adiciona Pedido como esboço
                if (erro == "")
                {
                    OBJPedidoVendaRetorno = JsonConvert.DeserializeObject<ORDR_InclusaoPedidoRetornoClass>(JSONRetorno);
                    this.EsbocoChaveSAP = OBJPedidoVendaRetorno.DocEntry;
                }

                if (OBJDebug.GetGeraDebug())
                {
                    this.OBJDebug.SetOperacao("ComunicacaoServiceLayerSAPClass - GravarEsbocoPedidoVendaSAP() - Passo 3");
                    OBJDebug.SetDescricao("OBJPedidoVendaRetorno: " + OBJDebug.SerializarObjeto(OBJPedidoVendaRetorno));
                    OBJDebug.GerarDadosDebug();
                }
            }

            return erro;
        }

        public string AtualizaEsbocoPedido()
        {
            string erro = "";
            JSONEnvio = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {

                ODRF_AtualizaEsbocoClass OBJEsboco = new ODRF_AtualizaEsbocoClass();

                if (this.DataLancamentoPedido != DateTime.MinValue)
                    OBJEsboco.DocDate = this.DataLancamentoPedido;

                if (this.DataCancelamentoPedido != DateTime.MinValue)
                    OBJEsboco.CancelDate = this.DataCancelamentoPedido;

                if (this.DataEntregaPedido != DateTime.MinValue)
                    OBJEsboco.DocDueDate = this.DataEntregaPedido;

                if (!string.IsNullOrEmpty(this.LiberadoClicheProducaoPedido))
                    OBJEsboco.U_MF_ApProd = this.LiberadoClicheProducaoPedido;

                JSONEnvio = JsonConvert.SerializeObject(OBJEsboco);

                Funcao = "/Drafts(" + this.EsbocoChaveSAP.ToString() + ")";
                erro = this.PatchFunction(Funcao);
            }

            return erro;
        }

        public string GravarClienteSAP()
        {
            string erro = "";
            JSONEnvio = "";
            JSONRetorno = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                OCRD_IncluirClienteClass OBJCliente = new OCRD_IncluirClienteClass();
                OCRD_InclusaoClienteRetornoClass OBJClienteRetorno = new OCRD_InclusaoClienteRetornoClass();

                OBJCliente.CardName = this.OBJCliente.CardName;
                OBJCliente.CardType = this.OBJCliente.CardType;
                OBJCliente.Phone1 = this.OBJCliente.Phone1;
                OBJCliente.Fax = this.OBJCliente.Fax;
                OBJCliente.EmailAddress = this.OBJCliente.EmailAddress;
                OBJCliente.Notes = this.OBJCliente.Notes;
                OBJCliente.SalesPersonCode = this.OBJCliente.SalesPersonCode;
                OBJCliente.AliasName = this.OBJCliente.AliasName;
                OBJCliente.U_IB_NAT_JURIDICA = this.OBJCliente.U_IB_NAT_JURIDICA;
                OBJCliente.U_TX_IndIEDest = this.OBJCliente.U_TX_IndIEDest;
                OBJCliente.U_TX_IndNat = this.OBJCliente.U_TX_IndNat;
                OBJCliente.U_TX_IndFinal = this.OBJCliente.U_TX_IndFinal;
                OBJCliente.U_IB_Enquadr_Trib = this.OBJCliente.U_IB_Enquadr_Trib;
                OBJCliente.U_IB_CartaIPI = this.OBJCliente.U_IB_CartaIPI;
                OBJCliente.U_TX_SN = this.OBJCliente.U_TX_SN;
                OBJCliente.U_TX_ProdRural = this.OBJCliente.U_TX_ProdRural;
                OBJCliente.U_IB_CPOM = this.OBJCliente.U_IB_CPOM;
                OBJCliente.PayTermsGrpCode = this.OBJCliente.PayTermsGrpCode;
                OBJCliente.FreeText = this.OBJCliente.FreeText;
                OBJCliente.SinglePayment = this.OBJCliente.SinglePayment;
                OBJCliente.CollectionAuthorization = this.OBJCliente.CollectionAuthorization;
                OBJCliente.CreditLimit = this.OBJCliente.CreditLimit;
                OBJCliente.U_IB_DataCartaIPI = this.OBJCliente.U_IB_DataCartaIPI;
                OBJCliente.Properties1 = this.OBJCliente.Properties1;
                OBJCliente.Properties2 = this.OBJCliente.Properties2;
                OBJCliente.Properties3 = this.OBJCliente.Properties3;
                OBJCliente.Properties4 = this.OBJCliente.Properties4;
                OBJCliente.Properties5 = this.OBJCliente.Properties5;
                OBJCliente.Properties6 = this.OBJCliente.Properties6;
                OBJCliente.Properties7 = this.OBJCliente.Properties7;
                OBJCliente.Properties8 = this.OBJCliente.Properties8;
                OBJCliente.Properties9 = this.OBJCliente.Properties9;
                OBJCliente.Properties10 = this.OBJCliente.Properties10;
                OBJCliente.Properties11 = this.OBJCliente.Properties11;
                OBJCliente.Properties12 = this.OBJCliente.Properties12;
                OBJCliente.Properties13 = this.OBJCliente.Properties13;
                OBJCliente.Properties14 = this.OBJCliente.Properties14;
                OBJCliente.Properties15 = this.OBJCliente.Properties15;
                OBJCliente.Properties16 = this.OBJCliente.Properties16;
                OBJCliente.Properties17 = this.OBJCliente.Properties17;
                OBJCliente.Properties18 = this.OBJCliente.Properties18;
                OBJCliente.Properties19 = this.OBJCliente.Properties19;
                OBJCliente.Properties20 = this.OBJCliente.Properties20;
                OBJCliente.Properties21 = this.OBJCliente.Properties21;
                OBJCliente.Properties22 = this.OBJCliente.Properties22;
                OBJCliente.Properties23 = this.OBJCliente.Properties23;
                OBJCliente.Properties24 = this.OBJCliente.Properties24;
                OBJCliente.Properties25 = this.OBJCliente.Properties25;
                OBJCliente.Properties26 = this.OBJCliente.Properties26;
                OBJCliente.Properties27 = this.OBJCliente.Properties27;
                OBJCliente.Properties28 = this.OBJCliente.Properties28;
                OBJCliente.Properties29 = this.OBJCliente.Properties29;
                OBJCliente.Properties30 = this.OBJCliente.Properties30;
                OBJCliente.Properties31 = this.OBJCliente.Properties31;
                OBJCliente.Properties32 = this.OBJCliente.Properties32;
                OBJCliente.Properties33 = this.OBJCliente.Properties33;
                OBJCliente.Properties34 = this.OBJCliente.Properties34;
                OBJCliente.Properties35 = this.OBJCliente.Properties35;
                OBJCliente.Properties36 = this.OBJCliente.Properties36;
                OBJCliente.Properties37 = this.OBJCliente.Properties37;
                OBJCliente.Properties38 = this.OBJCliente.Properties38;
                OBJCliente.Properties39 = this.OBJCliente.Properties39;
                OBJCliente.Properties40 = this.OBJCliente.Properties40;
                OBJCliente.Properties41 = this.OBJCliente.Properties41;
                OBJCliente.Properties42 = this.OBJCliente.Properties42;
                OBJCliente.Properties43 = this.OBJCliente.Properties43;
                OBJCliente.Properties44 = this.OBJCliente.Properties44;
                OBJCliente.Properties45 = this.OBJCliente.Properties45;
                OBJCliente.Properties46 = this.OBJCliente.Properties46;
                OBJCliente.Properties47 = this.OBJCliente.Properties47;
                OBJCliente.Properties48 = this.OBJCliente.Properties48;
                OBJCliente.Properties49 = this.OBJCliente.Properties49;
                OBJCliente.Properties50 = this.OBJCliente.Properties50;
                OBJCliente.Properties51 = this.OBJCliente.Properties51;
                OBJCliente.Properties52 = this.OBJCliente.Properties52;
                OBJCliente.Properties53 = this.OBJCliente.Properties53;
                OBJCliente.Properties54 = this.OBJCliente.Properties54;
                OBJCliente.Properties55 = this.OBJCliente.Properties55;
                OBJCliente.Properties56 = this.OBJCliente.Properties56;
                OBJCliente.Properties57 = this.OBJCliente.Properties57;
                OBJCliente.Properties58 = this.OBJCliente.Properties58;
                OBJCliente.Properties59 = this.OBJCliente.Properties59;
                OBJCliente.Properties60 = this.OBJCliente.Properties60;
                OBJCliente.Properties61 = this.OBJCliente.Properties61;
                OBJCliente.Properties62 = this.OBJCliente.Properties62;
                OBJCliente.Properties63 = this.OBJCliente.Properties63;
                OBJCliente.Properties64 = this.OBJCliente.Properties64;


                //Adiciona Linhas do pedido
                if (this.OBJCliente.BPAddresses.Count > 0)
                {
                    foreach (ComunicacaoServiceLayerClienteEnderecoClass OBJComunicacaoServiceLayerClienteEndereco in this.OBJCliente.BPAddresses)
                    {
                        CRD1_IncluirClienteEnderecoClass OBJClienteEndereco = new CRD1_IncluirClienteEnderecoClass();

                        OBJClienteEndereco.AddressName = OBJComunicacaoServiceLayerClienteEndereco.AddressName;
                        OBJClienteEndereco.AddressType = OBJComunicacaoServiceLayerClienteEndereco.AddressType;
                        OBJClienteEndereco.Street = OBJComunicacaoServiceLayerClienteEndereco.Street;
                        OBJClienteEndereco.StreetNo = OBJComunicacaoServiceLayerClienteEndereco.StreetNo;
                        OBJClienteEndereco.BuildingFloorRoom = OBJComunicacaoServiceLayerClienteEndereco.BuildingFloorRoom;
                        OBJClienteEndereco.ZipCode = OBJComunicacaoServiceLayerClienteEndereco.ZipCode;
                        OBJClienteEndereco.Block = OBJComunicacaoServiceLayerClienteEndereco.Block;
                        OBJClienteEndereco.City = OBJComunicacaoServiceLayerClienteEndereco.City;
                        OBJClienteEndereco.State = OBJComunicacaoServiceLayerClienteEndereco.State;
                        OBJClienteEndereco.County = OBJComunicacaoServiceLayerClienteEndereco.County;
                        OBJClienteEndereco.Country = OBJComunicacaoServiceLayerClienteEndereco.Country;
                        OBJClienteEndereco.TypeOfAddress = OBJComunicacaoServiceLayerClienteEndereco.TypeOfAddress;

                        OBJCliente.BPAddresses.Add(OBJClienteEndereco);
                    }
                }

                //Adicionar Despesas Adicionais
                if (this.OBJCliente.ContactEmployees.Count > 0)
                {
                    foreach (ComunicacaoServiceLayerClienteContatoClass OBJComunicacaoServiceLayerClienteContato in this.OBJCliente.ContactEmployees)
                    {
                        OCPR_IncluirClienteContatoClass OBJClienteContatos = new OCPR_IncluirClienteContatoClass();

                        OBJClienteContatos.Name = OBJComunicacaoServiceLayerClienteContato.Name;
                        OBJClienteContatos.FirstName = OBJComunicacaoServiceLayerClienteContato.FirstName;
                        OBJClienteContatos.Phone1 = OBJComunicacaoServiceLayerClienteContato.Phone1;
                        OBJClienteContatos.E_Mail = OBJComunicacaoServiceLayerClienteContato.E_Mail;

                        OBJCliente.ContactEmployees.Add(OBJClienteContatos);
                    }
                }

                //Adicionar Extensão Fiscal
                if (this.OBJCliente.BPPaymentMethods.Count > 0)
                {
                    foreach (ComunicacaoServiceLayerClientePagamentoClass ComunicacaoServiceLayerClientePagamento in this.OBJCliente.BPPaymentMethods)
                    {
                        CRD2_IncluirClienteFormasPagamentoClass OBJClienteFormasPagamento = new CRD2_IncluirClienteFormasPagamentoClass();

                        OBJClienteFormasPagamento.PaymentMethodCode = ComunicacaoServiceLayerClientePagamento.PaymentMethodCode;

                        OBJCliente.BPPaymentMethods.Add(OBJClienteFormasPagamento);
                    }
                }

                //Adicionar Extensão Fiscal
                if (this.OBJCliente.BPFiscalTaxIDCollection.Count > 0)
                {
                    foreach (ComunicacaoServiceLayerClienteFiscalClass ComunicacaoServiceLayerClienteFiscal in this.OBJCliente.BPFiscalTaxIDCollection)
                    {
                        CRD7_IncluirClienteFiscalClass OBJClienteFiscal = new CRD7_IncluirClienteFiscalClass();

                        OBJClienteFiscal.TaxId0 = ComunicacaoServiceLayerClienteFiscal.TaxId0;
                        OBJClienteFiscal.Address = ComunicacaoServiceLayerClienteFiscal.Address;
                        OBJClienteFiscal.TaxId1 = ComunicacaoServiceLayerClienteFiscal.TaxId1;
                        OBJClienteFiscal.CNAECode = ComunicacaoServiceLayerClienteFiscal.CNAECode;
                        OBJClienteFiscal.TaxId8 = ComunicacaoServiceLayerClienteFiscal.TaxId8;
                        OBJClienteFiscal.AddrType = ComunicacaoServiceLayerClienteFiscal.AddrType;

                        OBJCliente.BPFiscalTaxIDCollection.Add(OBJClienteFiscal);
                    }
                }

                JSONEnvio = JsonConvert.SerializeObject(OBJCliente);

                Funcao = "/BusinessPartners";
                erro = PostFunction(Funcao);

                //Adiciona Pedido como esboço
                if (erro == "")
                {
                    OBJClienteRetorno = JsonConvert.DeserializeObject<OCRD_InclusaoClienteRetornoClass>(JSONRetorno);
                    this.CodigoClienteSAP = OBJClienteRetorno.CardCode;
                }
            }

            return erro;
        }

        public string AtualizaClienteSAP()
        {
            string erro = "";
            JSONEnvio = "";
            JSONRetorno = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                OCRD_AtualizarClienteClass OBJCliente = new OCRD_AtualizarClienteClass();

                OBJCliente.CardCode = this.CodigoClienteSAP.ToString();
                OBJCliente.CardName = this.OBJCliente.CardName;
                OBJCliente.CardType = this.OBJCliente.CardType;
                OBJCliente.Phone1 = this.OBJCliente.Phone1;
                OBJCliente.Fax = this.OBJCliente.Fax;
                OBJCliente.EmailAddress = this.OBJCliente.EmailAddress;
                OBJCliente.Notes = this.OBJCliente.Notes;
                OBJCliente.SalesPersonCode = this.OBJCliente.SalesPersonCode;
                OBJCliente.AliasName = this.OBJCliente.AliasName;
                OBJCliente.U_IB_NAT_JURIDICA = this.OBJCliente.U_IB_NAT_JURIDICA;
                OBJCliente.U_TX_IndIEDest = this.OBJCliente.U_TX_IndIEDest;
                OBJCliente.U_TX_IndNat = this.OBJCliente.U_TX_IndNat;
                OBJCliente.U_TX_IndFinal = this.OBJCliente.U_TX_IndFinal;
                OBJCliente.U_IB_Enquadr_Trib = this.OBJCliente.U_IB_Enquadr_Trib;
                OBJCliente.U_IB_CartaIPI = this.OBJCliente.U_IB_CartaIPI;
                OBJCliente.U_TX_SN = this.OBJCliente.U_TX_SN;
                OBJCliente.U_TX_ProdRural = this.OBJCliente.U_TX_ProdRural;
                OBJCliente.U_IB_CPOM = this.OBJCliente.U_IB_CPOM;
                OBJCliente.PayTermsGrpCode = this.OBJCliente.PayTermsGrpCode;
                OBJCliente.FreeText = this.OBJCliente.FreeText;
                OBJCliente.SinglePayment = this.OBJCliente.SinglePayment;
                OBJCliente.CollectionAuthorization = this.OBJCliente.CollectionAuthorization;
                OBJCliente.CreditLimit = this.OBJCliente.CreditLimit;
                OBJCliente.U_IB_DataCartaIPI = this.OBJCliente.U_IB_DataCartaIPI;
                OBJCliente.Properties1 = this.OBJCliente.Properties1;
                OBJCliente.Properties2 = this.OBJCliente.Properties2;
                OBJCliente.Properties3 = this.OBJCliente.Properties3;
                OBJCliente.Properties4 = this.OBJCliente.Properties4;
                OBJCliente.Properties5 = this.OBJCliente.Properties5;
                OBJCliente.Properties6 = this.OBJCliente.Properties6;
                OBJCliente.Properties7 = this.OBJCliente.Properties7;
                OBJCliente.Properties8 = this.OBJCliente.Properties8;
                OBJCliente.Properties9 = this.OBJCliente.Properties9;
                OBJCliente.Properties10 = this.OBJCliente.Properties10;
                OBJCliente.Properties11 = this.OBJCliente.Properties11;
                OBJCliente.Properties12 = this.OBJCliente.Properties12;
                OBJCliente.Properties13 = this.OBJCliente.Properties13;
                OBJCliente.Properties14 = this.OBJCliente.Properties14;
                OBJCliente.Properties15 = this.OBJCliente.Properties15;
                OBJCliente.Properties16 = this.OBJCliente.Properties16;
                OBJCliente.Properties17 = this.OBJCliente.Properties17;
                OBJCliente.Properties18 = this.OBJCliente.Properties18;
                OBJCliente.Properties19 = this.OBJCliente.Properties19;
                OBJCliente.Properties20 = this.OBJCliente.Properties20;
                OBJCliente.Properties21 = this.OBJCliente.Properties21;
                OBJCliente.Properties22 = this.OBJCliente.Properties22;
                OBJCliente.Properties23 = this.OBJCliente.Properties23;
                OBJCliente.Properties24 = this.OBJCliente.Properties24;
                OBJCliente.Properties25 = this.OBJCliente.Properties25;
                OBJCliente.Properties26 = this.OBJCliente.Properties26;
                OBJCliente.Properties27 = this.OBJCliente.Properties27;
                OBJCliente.Properties28 = this.OBJCliente.Properties28;
                OBJCliente.Properties29 = this.OBJCliente.Properties29;
                OBJCliente.Properties30 = this.OBJCliente.Properties30;
                OBJCliente.Properties31 = this.OBJCliente.Properties31;
                OBJCliente.Properties32 = this.OBJCliente.Properties32;
                OBJCliente.Properties33 = this.OBJCliente.Properties33;
                OBJCliente.Properties34 = this.OBJCliente.Properties34;
                OBJCliente.Properties35 = this.OBJCliente.Properties35;
                OBJCliente.Properties36 = this.OBJCliente.Properties36;
                OBJCliente.Properties37 = this.OBJCliente.Properties37;
                OBJCliente.Properties38 = this.OBJCliente.Properties38;
                OBJCliente.Properties39 = this.OBJCliente.Properties39;
                OBJCliente.Properties40 = this.OBJCliente.Properties40;
                OBJCliente.Properties41 = this.OBJCliente.Properties41;
                OBJCliente.Properties42 = this.OBJCliente.Properties42;
                OBJCliente.Properties43 = this.OBJCliente.Properties43;
                OBJCliente.Properties44 = this.OBJCliente.Properties44;
                OBJCliente.Properties45 = this.OBJCliente.Properties45;
                OBJCliente.Properties46 = this.OBJCliente.Properties46;
                OBJCliente.Properties47 = this.OBJCliente.Properties47;
                OBJCliente.Properties48 = this.OBJCliente.Properties48;
                OBJCliente.Properties49 = this.OBJCliente.Properties49;
                OBJCliente.Properties50 = this.OBJCliente.Properties50;
                OBJCliente.Properties51 = this.OBJCliente.Properties51;
                OBJCliente.Properties52 = this.OBJCliente.Properties52;
                OBJCliente.Properties53 = this.OBJCliente.Properties53;
                OBJCliente.Properties54 = this.OBJCliente.Properties54;
                OBJCliente.Properties55 = this.OBJCliente.Properties55;
                OBJCliente.Properties56 = this.OBJCliente.Properties56;
                OBJCliente.Properties57 = this.OBJCliente.Properties57;
                OBJCliente.Properties58 = this.OBJCliente.Properties58;
                OBJCliente.Properties59 = this.OBJCliente.Properties59;
                OBJCliente.Properties60 = this.OBJCliente.Properties60;
                OBJCliente.Properties61 = this.OBJCliente.Properties61;
                OBJCliente.Properties62 = this.OBJCliente.Properties62;
                OBJCliente.Properties63 = this.OBJCliente.Properties63;
                OBJCliente.Properties64 = this.OBJCliente.Properties64;

                JSONEnvio = JsonConvert.SerializeObject(OBJCliente);

                Funcao = "/BusinessPartners('" + this.CodigoClienteSAP.ToString() + "')";
                erro = this.PatchFunction(Funcao);
            }

            return erro;
        }

        public string AtualizaClienteVendedorSAP()
        {
            string erro = "";
            JSONEnvio = "";
            JSONRetorno = "";
            string Funcao = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {
                OCRD_AtualizarClienteVendedorClass OBJCliente = new OCRD_AtualizarClienteVendedorClass();

                OBJCliente.CardCode = this.CodigoClienteSAP.ToString();
                OBJCliente.SalesPersonCode = this.OBJCliente.SalesPersonCode;

                JSONEnvio = JsonConvert.SerializeObject(OBJCliente);

                Funcao = "/BusinessPartners('" + this.CodigoClienteSAP.ToString() + "')";
                erro = this.PatchFunction(Funcao);
            }

            return erro;
        }

        public void LimparCampos()
        {
            this.JSONEnvio = string.Empty;
            this.JSONRetorno = string.Empty;
            this.CodigoClienteSAP = string.Empty;
            this.CodigoClienteTipoContato = string.Empty;
            this.CodigoClienteLinha = 0;
            this.CodigoClientePrimeiroNome = string.Empty;
            this.CodigoClienteUltimoNome = string.Empty;
            this.CodigoClienteEmail = string.Empty;
            this.CodigoClienteTelefone1 = string.Empty;
            this.AprovacaoNumero = 0;
            this.AprovacaoUsuario = string.Empty;
            this.AprovacaoUsuarioSenha = string.Empty;
            this.AprovacaoHistorico = string.Empty;
            this.AprovacaoDecisao = string.Empty;
            this.EsbocoChaveSAP = 0;
            this.EsbocoNovoPedidoSAP = string.Empty;
            this.DataLancamentoPedido = DateTime.MinValue;
            this.DataEntregaPedido = DateTime.MinValue;
            this.DataCancelamentoPedido = DateTime.MinValue;
            this.EsbocoNovaNotaSAP = string.Empty;
            this.NumeroPedidoSAP = 0;
            this.HistoricoPedidoSAP = string.Empty;
            this.HistoricoAnteriorPedidoSAP = string.Empty;
            this.NumeroPrimarioNotaSAP = 0;
            this.HistoricoNotaSAP = "";
            this.EsbocoChaveSAP = 0;
            this.EsbocoNovoPedidoSAP = string.Empty;
            this.DataLancamentoPedido = DateTime.MinValue;
            this.DataEntregaPedido = DateTime.MinValue;
            this.DataCancelamentoPedido = DateTime.MinValue;
            this.EsbocoNovaNotaSAP = string.Empty;
            this.NumeroPedidoSAP = 0;
            this.HistoricoPedidoSAP = string.Empty;
            this.LiberadoClicheProducaoPedido = string.Empty;

            //Verifica se existe usuários específicos logados para limpar os campos
            if (this.OBJComunicacaoEspecificaListaServiceLayer != null)
            {
                this.OBJComunicacaoEspecificaListaServiceLayer.LimparDados();
            }
        }
    }
}