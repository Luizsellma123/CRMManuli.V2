using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ComunicacaoServiceLayerCamposSAPClass
    {
        /*Dados Acesso ao servidor*/
        public string SLDServer { get; set; }
        public string Server { get; set; }
        public string DbUserName { get; set; }
        public string DbPassword { get; set; }
        public string CompanyDB { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        /*Dados para query diretamente no banco de dados*/
        public string ConexaoPrincipalSAP = System.Configuration.ConfigurationManager.AppSettings["StringConexaoBDSAP"];

        /*Variavel par comunicação*/
        public string JSONEnvio = "";
        public string JSONRetorno = "";

        /*Dados comunicação SAP*/
        public string URLServiceLayerSAP { get; set; }
        public DateTime DataAcesso { get; set; }
        public DateTime ValidoAte { get; set; }
        public ComunicacaoServiceLayerLoginSAPClass OBJComunicacaoServiceLayerLogin { get; set; }
        public ComunicacaoServiceLayerLoginRetornoSAPClass OBJComunicacaoServiceLayerLoginRetorno { get; set; }
        public ComunicacaoServiceLayerRetornoSAPClass OBJComunicacaoServiceLayerRetorno { get; set; }
        public ComunicacaoEspecificaListaServiceLayerSAPClass OBJComunicacaoEspecificaListaServiceLayer { get; } = new ComunicacaoEspecificaListaServiceLayerSAPClass();

        /*Dados Clientes*/
        public string CodigoClienteSAP { get; set; }
        public string CodigoClienteTipoContato { get; set; }
        public int CodigoClienteLinha { get; set; }
        public string CodigoClientePrimeiroNome { get; set; }
        public string CodigoClienteUltimoNome { get; set; }
        public string CodigoClienteEmail { get; set; }
        public string CodigoClienteTelefone1 { get; set; }

        /*Aprovação Documentos*/
        public int AprovacaoNumero { get; set; }
        public string AprovacaoUsuario { get; set; }
        public string AprovacaoUsuarioSenha { get; set; }
        public string AprovacaoHistorico { get; set; }
        public string AprovacaoDecisao { get; set; }
        public string AprovacaoEsbocoStatusSAP { get; set; }

        /*Chave de Esboço SAP*/
        public int EsbocoChaveSAP { get; set; }

        /*Dados Para Adicionar Pedido*/
        public string EsbocoNovoPedidoSAP { get; set; }
        public DateTime DataLancamentoPedido { get; set; }
        public DateTime DataEntregaPedido { get; set; }
        public DateTime DataCancelamentoPedido { get; set; }
        public string LiberadoClicheProducaoPedido { get; set; }

        /*Dados Para Adicionar Nota*/
        public string EsbocoNovaNotaSAP { get; set; }

        /*Atualiza dados pedidos SAP*/
        public int NumeroPedidoSAP { get; set; }
        public string HistoricoPedidoSAP { get; set; }
        public string HistoricoAnteriorPedidoSAP { get; set; }

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
        //private string ConexaoPrincipalSAP = System.Configuration.ConfigurationManager.AppSettings["StringConexaoBDSAP"];

        /*Dados para atualizar Liberação produção*/
        public string LiberarProducaoLiberado { get; set; }

        /*Dados classificação comercial*/
        public string ClassificacaoComercialSAP { get; set; }

        /*Dados Contato Parceiro Negocio*/
        public int InternalCode { get; set; }

        /*Pedido de Venda*/
        public ComunicacaoServiceLayerPedidoSAPClass OBJPedidoVenda { get; set; }

        /*Cadastro Cliente*/
        public ComunicacaoServiceLayerClienteClass OBJCliente { get; set; }

        public ComunicacaoServiceLayerCamposSAPClass()
        {
            //Inicializa Pedidos de Venda
            this.OBJPedidoVenda = new ComunicacaoServiceLayerPedidoSAPClass();
            this.OBJPedidoVenda.OBJPedidoLinhas = new List<ComunicacaoServiceLayerPedidoLinhasSAPClass>();
            this.OBJPedidoVenda.OBJPedidoDespesasAdicionais = new List<ComunicacaoServiceLayerPedidoDespesasAdicionaisSAPClass>();
            this.OBJPedidoVenda.OBJPedidoExtensaoImpostos = new List<ComunicacaoServiceLayerPedidoExtensaoImpostosSAPClass>();

            //Inicializa Clientes
            this.OBJCliente = new ComunicacaoServiceLayerClienteClass();
            this.OBJCliente.ContactEmployees = new List<ComunicacaoServiceLayerClienteContatoClass>();
            this.OBJCliente.BPAddresses = new List<ComunicacaoServiceLayerClienteEnderecoClass>();
            this.OBJCliente.BPPaymentMethods = new List<ComunicacaoServiceLayerClientePagamentoClass>();
            this.OBJCliente.BPFiscalTaxIDCollection = new List<ComunicacaoServiceLayerClienteFiscalClass>();
        }

        /*Classe para Debug*/
        public DebugClass OBJDebug = new DebugClass();
    }
}