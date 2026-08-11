using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceCRM
{
    /// <summary>
    /// Summary description for ComunicacaoCRM
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ComunicacaoCRM : System.Web.Services.WebService
    {
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSClasseNotaFiscal RetornaDadosNota(int CodigoEmpresa, int NumeroNotaFiscal, int NumeroPedidoSAP)
        {
            WSClasseNotaFiscal OBJNotaFiscal = new WSClasseNotaFiscal();

            OBJNotaFiscal.CodigoEmpresa = CodigoEmpresa;
            OBJNotaFiscal.NumeroNotaFiscal = NumeroNotaFiscal;
            OBJNotaFiscal.RecuperaDadosCabecalhoNota();
            OBJNotaFiscal.RecuperaDadosItemsNota();

            PedidoClass objPedidoClass = new PedidoClass();

            objPedidoClass.EmpCod = CodigoEmpresa.ToString();
            objPedidoClass.NumeroPedidoSAP = NumeroPedidoSAP;
            objPedidoClass.NumeroNotaFiscal = NumeroNotaFiscal.ToString();

            OBJNotaFiscal.PrevisaoEntrega = objPedidoClass.CarregaPrevisaoEntrega();
            
            return OBJNotaFiscal;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSContasReceberNotas RetornaDadosCRNota(int DocEntry, int ObjType)
        {
            WSContasReceberNotas OBJCRNotaFiscal = new WSContasReceberNotas();

            OBJCRNotaFiscal.DocEntry = DocEntry;
            OBJCRNotaFiscal.ObjType = ObjType;
            OBJCRNotaFiscal.RecuperaNotasContasReceber();

            return OBJCRNotaFiscal;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSContasPagarNotas RetornaDadosCPNota(int DocEntry, int ObjType)
        {
            WSContasPagarNotas OBJNotaFiscal = new WSContasPagarNotas();

            OBJNotaFiscal.DocEntry = DocEntry;
            OBJNotaFiscal.ObjType = ObjType;
            OBJNotaFiscal.RecuperaNotasContasPagar();

            return OBJNotaFiscal;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSDevolucoes RecuperaNotasDevolucoes(int DocEntry, int ObjType)
        {
            WSDevolucoes OBJNotaFiscal = new WSDevolucoes();

            OBJNotaFiscal.DocEntry = DocEntry;
            OBJNotaFiscal.ObjType = ObjType;
            OBJNotaFiscal.RecuperaNotasDevolucoes();

            return OBJNotaFiscal;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSPedidos RecuperaPedidos(int DocEntry)
        {
            WSPedidos OBJPedidos = new WSPedidos();

            OBJPedidos.DocEntry = DocEntry;
            OBJPedidos.RecuperaPedidos();

            return OBJPedidos;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSClienteDetalhes RecuperaDetalheCliente(int IDCliente)
        {
            WSClienteDetalhes OBJClienteDetalhes = new WSClienteDetalhes();

            OBJClienteDetalhes.IDCliente = IDCliente;
            OBJClienteDetalhes.CarregaCarteiraDetalheCliente();
            OBJClienteDetalhes.CarregaCarteiraVendedorCliente();

            return OBJClienteDetalhes;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSOrdensServicoProdutosDetalhes RecuperaOrdensServicoProdutos(int IDEmpresa, int IDOrdemServico, int NumeroPedidoSAP, int IDITemSAP)
        {
            WSOrdensServicoProdutosDetalhes OBJOrdensServicoProdutos = new WSOrdensServicoProdutosDetalhes();

            OBJOrdensServicoProdutos.IDEmpresa = IDEmpresa;
            OBJOrdensServicoProdutos.IDOrdemServico = IDOrdemServico;
            OBJOrdensServicoProdutos.NumeroPedidoSAP = NumeroPedidoSAP;
            OBJOrdensServicoProdutos.IDITemSAP = IDITemSAP;

            OBJOrdensServicoProdutos.RecuperaOrdensServico();

            return OBJOrdensServicoProdutos;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSOrdensServicoProdutos RecuperaOrdensServicoOrdensProducao(int IDEmpresa, int IDOrdemServico, int NumeroPedidoSAP, int IDITemSAP, int DocEntry)
        {
            WSOrdensServicoProdutos OBJOrdensServicoProdutos = new WSOrdensServicoProdutos();

            OBJOrdensServicoProdutos.IDEmpresa = IDEmpresa;
            OBJOrdensServicoProdutos.IDOrdemServico = IDOrdemServico;
            OBJOrdensServicoProdutos.NumeroPedidoSAP = NumeroPedidoSAP;
            OBJOrdensServicoProdutos.IDITemSAP = IDITemSAP;
            OBJOrdensServicoProdutos.DocEntry = DocEntry;

            OBJOrdensServicoProdutos.RecuperaOrdensServicoProdutoCabecalho();
            OBJOrdensServicoProdutos.RecuperaOrdensServicoProdutoEstrutura();

            return OBJOrdensServicoProdutos;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSOrdensServicoIncluirProdutos RecuperaOrdensServicoIncluirProdutos(int IDITemSAP, int DocEntry)
        {
            WSOrdensServicoIncluirProdutos OBJOrdensServicoInlcuirProdutos = new WSOrdensServicoIncluirProdutos();

            OBJOrdensServicoInlcuirProdutos.IDITemSAP = IDITemSAP;
            OBJOrdensServicoInlcuirProdutos.DocEntry = DocEntry;

            OBJOrdensServicoInlcuirProdutos.RecuperaOrdensServicoIncluirProdutos();
            return OBJOrdensServicoInlcuirProdutos;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSTicketDetalhe RetornaTicketDetalhe(int IDEmpresa, int IDTicket)
        {
            WSTicketDetalhe OBJTicketDetalhe = new WSTicketDetalhe();

            OBJTicketDetalhe.IDEmpresa = IDEmpresa;
            OBJTicketDetalhe.IDTicket = IDTicket;
            OBJTicketDetalhe.RetornaListaTicketsDetalhe();

            return OBJTicketDetalhe;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSAtividadeDetalhe RetornaAtividadeDetalhe(int IDEmpresa, int IDTicket, int IDAtividade)
        {
            WSAtividadeDetalhe OBJAtividadeDetalhe = new WSAtividadeDetalhe();

            OBJAtividadeDetalhe.IDEmpresa = IDEmpresa;
            OBJAtividadeDetalhe.IDTicket = IDTicket;
            OBJAtividadeDetalhe.IDAtividade = IDAtividade;

            OBJAtividadeDetalhe.RetornaListaAtividadesDetalhe();

            return OBJAtividadeDetalhe;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSPedidoProdutoDetalhe RetornaPedidoProdutoDetalhe
            (int IDEmpresa, int NumeroPedidoSAP, int NumeroPedidoCRM, string CodigoItemSAP, string Cliche)
        {
            WSPedidoProdutoDetalhe objWSPedidoProdutoDetalhe = new WSPedidoProdutoDetalhe();

            objWSPedidoProdutoDetalhe.IDEmpresa = IDEmpresa;
            objWSPedidoProdutoDetalhe.NumeroPedidoSAP = NumeroPedidoSAP;
            objWSPedidoProdutoDetalhe.NumeroPedidoCRM = NumeroPedidoCRM;
            objWSPedidoProdutoDetalhe.CodigoItemSAP = CodigoItemSAP;
            objWSPedidoProdutoDetalhe.Cliche = Cliche;

            objWSPedidoProdutoDetalhe.RecuperaPedidoProdutoDetalhe();

            return objWSPedidoProdutoDetalhe;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSChamadoPrincipal RetornaChamadoPrincipal(int IDChamado)
        {
            WSChamadoPrincipal objWSChamadoPrincipal = new WSChamadoPrincipal();

            objWSChamadoPrincipal.IDChamado = IDChamado;                        

            return objWSChamadoPrincipal.RetornaChamadoPrincipal();
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSChamadoApontamentoHoras RetornaChamadoApontamentoHoras(int IDChamado, int IDUsuarioResponsavel, int IDApontamento)
        {
            WSChamadoApontamentoHoras objWSChamadoApontamentoHoras = new WSChamadoApontamentoHoras();

            objWSChamadoApontamentoHoras.IDChamado = IDChamado;
            objWSChamadoApontamentoHoras.IDUsuarioResponsavel = IDUsuarioResponsavel;
            objWSChamadoApontamentoHoras.IDApontamento = IDApontamento;

            return objWSChamadoApontamentoHoras.RetornaChamadoApontamentoHoras();
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSRecebimentoPrincipal RetornaRecebimentoPrincipal(int IDEmpresa, int IDRecebimento)
        {
            WSRecebimentoPrincipal objWSRecebimentoPrincipal = new WSRecebimentoPrincipal();

            objWSRecebimentoPrincipal.IDEmpresa = IDEmpresa;

            objWSRecebimentoPrincipal.IDRecebimento = IDRecebimento;

            return objWSRecebimentoPrincipal.RetornaRecebimentoPrincipal();
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public WSConsultaCENPROTProtestos ConsultaCENPROTProtestos(int IDCliente, int IDAnalise, int IDCartorio)
        {
            WSConsultaCENPROTProtestos objWSConsultaCENPROTProtestos = new WSConsultaCENPROTProtestos();

            objWSConsultaCENPROTProtestos.IDCliente = IDCliente;
            objWSConsultaCENPROTProtestos.IDAnalise = IDAnalise;
            objWSConsultaCENPROTProtestos.IDCartorio = IDCartorio;

            return objWSConsultaCENPROTProtestos.RetornaCENPROTProtestos();
        }

    }
}
