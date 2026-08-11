using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;
using SAPbobsCOM;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.classes
{
    public class ComunicacaoSAPClass
    {
        public string SLDServer { get; set; }
        public string Server { get; set; }
        public string DbUserName { get; set; }
        public string DbPassword { get; set; }
        public string CompanyDB { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        /*Aprovação Documentos*/
        public int AprovacaoNumero { get; set; }
        public string AprovacaoUsuario { get; set; }
        public string AprovacaoUsuarioSenha { get; set; }
        public string AprovacaoHistorico { get; set; }
        public string AprovacaoDecisao { get; set; }

        /*Adicionar Pedido*/
        public int AdicionarNumeroEsboco { get; set; }

        /*Cliente Fornecedor*/
        public string ParceiroCodigoClienteSAP { get; set; }
        public int ParceiroCodigoVendedor { get; set; }
        public int ParceiroCodigoVendedorNovo { get; set; }
        DataTable ParceiroDTVendedores { get; set; }

        /*Variavel comunicação SAP*/
        Company OBJCompany = new Company();
        Recordset OBJRecordSet = null;

        /*Dados Para Adicionar Pedido*/
        public int EsbocoChaveSAP { get; set; }
        public string EsbocoNovoPedidoSAP { get; set; }

        /*Lançamento Contabil*/
        public int NumeroTransacaoSAP { get; set; }

        /*Dados para query diretamente no banco de dados*/
        private string ConexaoPrincipalSAP = System.Configuration.ConfigurationManager.AppSettings["StringConexaoBDSAP"];

        public string conectarSAP()
        {
            string erro = "";

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

            return erro;
        }

        public void desconectarSAP()
        {
            OBJCompany.Disconnect();
        }

        /*Função retorna Datatable de recordset versão 1*/
        public DataTable RsTODataTablaV2(ref SAPbobsCOM.Recordset _rs)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < _rs.Fields.Count; i++)
                dt.Columns.Add(_rs.Fields.Item(i).Description);
            while (!_rs.EoF)
            {
                DataRow row = dt.NewRow();
                for (int i = 0; i < _rs.Fields.Count; i++)
                    row[i] = _rs.Fields.Item(i).Value;
                dt.Rows.Add(row.ItemArray);
                _rs.MoveNext();
            }
            return dt;
        }

        /*Função retorna Datatable de recordset versão 2*/
        public DataTable RsTODataTabla(ref SAPbobsCOM.Recordset _rs)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < _rs.Fields.Count; i++)
                dt.Columns.Add(_rs.Fields.Item(i).Description);
            while (!_rs.EoF)
            {
                object[] array = new object[_rs.Fields.Count];
                for (int i = 0; i < _rs.Fields.Count; i++)
                    array[i] = _rs.Fields.Item(i).Value;
                dt.Rows.Add(array);
                _rs.MoveNext();
            }
            return dt;
        }

        public DataTable RetornaDadosConsulta(string StringSQL)
        {
            DataTable OBJDataTable = new DataTable();
            string erro = "";

            erro = this.conectarSAP();


            if (erro == "")
            {
                OBJRecordSet = (SAPbobsCOM.Recordset)OBJCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                OBJRecordSet.DoQuery(StringSQL);

                OBJDataTable = RsTODataTablaV2(ref OBJRecordSet);

            }

            this.desconectarSAP();

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
                this.desconectarSAP();
            }
            return erro;
        }

        public string AtualizaVendedor()
        {
            string erro = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {

                //Instancia Objeto Cliente/Fornecedor
                BusinessPartners OBJCliente = null;
                OBJCliente = (SAPbobsCOM.BusinessPartners)OBJCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

                OBJCliente.GetByKey(this.ParceiroCodigoClienteSAP);
                OBJCliente.SalesPersonCode = this.ParceiroCodigoVendedor;
                OBJCliente.Update();

                erro = OBJCompany.GetLastErrorDescription();

                //Desconectar do SAP
                this.desconectarSAP();
            }
            return erro;
        }

        public string AtualizaCarteirasVendedor()
        {
            string erro = "";
            string StringSQL = "";

            if (erro == "")
            {
                StringSQL = "select CardCode FROM OCRD WHERE slpcode='" + this.ParceiroCodigoVendedor + "'";

                ParceiroDTVendedores = this.RetornaDadosConsulta(StringSQL);

                if (ParceiroDTVendedores.Rows.Count > 0)
                {
                    //Conectar no SAP
                    erro = this.conectarSAP();

                    //Instancia Objeto Cliente/Fornecedor
                    BusinessPartners OBJCliente = null;
                    OBJCliente = (SAPbobsCOM.BusinessPartners)OBJCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

                    foreach (DataRow row in ParceiroDTVendedores.Rows)
                    {
                        if (erro == "")
                        {
                            OBJCliente.GetByKey(row["CardCode"].ToString());
                            OBJCliente.SalesPersonCode = this.ParceiroCodigoVendedorNovo;
                            OBJCliente.Update();

                            erro = OBJCompany.GetLastErrorDescription();
                        }
                    }

                    //Desconectar do SAP
                    this.desconectarSAP();
                }

            }
            return erro;
        }

        public string AdicionaPedido()
        {
            string erro = "";

            //Conectar no SAP
            erro = this.conectarSAP();

            if (erro == "")
            {

                //Instancia Objeto Esboço
                Documents OBJEsboco = null;
                OBJEsboco = (SAPbobsCOM.Documents)OBJCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);

                OBJEsboco.GetByKey(this.EsbocoChaveSAP);
                if (OBJEsboco.SaveDraftToDocument() == 0)
                {
                    this.EsbocoNovoPedidoSAP = OBJCompany.GetNewObjectKey();
                }

                erro = OBJCompany.GetLastErrorDescription();

                //Desconectar do SAP
                this.desconectarSAP();
            }
            return erro;
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

        public string AtualizaLancamentoContabilHistoricoTA(string StringSQL)
        {
            string erro = "";
            DataTable RetornoDados = new DataTable();

            RetornoDados = RetornaDadosConsultaSAP(StringSQL);

            if (RetornoDados.Rows.Count > 0)
            {

                //Conectar no SAP
                erro = this.conectarSAP();

                if (erro == "")
                {
                    //Instancia Objeto Cliente/Fornecedor
                    JournalEntries OBJLancamentoContabil = null;
                    OBJLancamentoContabil = (SAPbobsCOM.JournalEntries)OBJCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                    

                    foreach (DataRow row in RetornoDados.Rows)
                    {
                        if (erro == "")
                        {
                            OBJLancamentoContabil.GetByKey(Convert.ToInt32(row["TransId"]));
                            OBJLancamentoContabil.Memo = row["Historico"].ToString();
                            OBJLancamentoContabil.Update();

                            erro = OBJCompany.GetLastErrorDescription();
                        }
                    }

                    //Desconectar do SAP
                    this.desconectarSAP();
                }
            }
            return erro;

        }
    }
}