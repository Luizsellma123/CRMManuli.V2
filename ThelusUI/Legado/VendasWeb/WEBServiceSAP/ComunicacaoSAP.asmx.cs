using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using VendasWeb.classes;
using VendasWeb.WEBServiceCRM;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using VendasWeb.WEBServiceSAP.ClassesWEBService;


namespace VendasWeb.WEBServiceSAP
{
    /// <summary>
    /// Summary description for ComunicacaoSAP
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ComunicacaoSAP : WebService
    {
        JsonConversao jsonconv = new JsonConversao();
        //Aqui deverá ser gravado a hash para consumir web service
        //string hash = "56fd784c2311e1ebfd1db871beeef8a9";
        string hash = System.Configuration.ConfigurationManager.AppSettings["HashHUB"];
        //string urlPadraoSAP = "http://192.168.0.15:90/";
        string urlPadraoSAP = System.Configuration.ConfigurationManager.AppSettings["AcessoURLHUB"];

        string urlPadraoAPICRM = System.Configuration.ConfigurationManager.AppSettings["AcessoURLCRMAPI"];

        WSRetornoClass OBJRetorno = new WSRetornoClass();

        public class AtualizaModel
        {
            public string Codigo { get; set; }

            public AtualizaModel(string Codigo)
            {
                this.Codigo = Codigo;
            }
        }

        AtualizaModel objAtualizaModel;

        string json;

        string retorno = "";

        public ComunicacaoSAP()
        {
            objAtualizaModel = new AtualizaModel("");

            json = jsonconv.ConverteObjectParaJSon<AtualizaModel>(objAtualizaModel);
        }

        [WebMethod]
        public string Retorna_Pais()
        {
            string URI = "";
            HttpClient client = new HttpClient();
            URI = urlPadraoSAP + "api/Gets?consulta=SAP_PAIS&hash=" + hash;

            client.BaseAddress = new Uri(URI);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/json"));

            string JSONRetorno = client.GetStringAsync(URI).Result;

            return JSONRetorno;
        }

        [WebMethod]
        public string Atualiza_Pais()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaPais");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Estados()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaEstados");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Municipios()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaMunicipios");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Codigos_CNAE()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaCodigosCNAE");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Classe_Vendedores()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaClassesVendedores");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Vendedores()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaVendedor");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Tipo_Vendedor()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaTipoVendedor");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Natureza_Juridica()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaNaturezasJuridicas");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Grupo_Economico()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaGrupoEconomico");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Condicao_Pagamento()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaCondicoesPagamento");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Grupo_Clientes()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaGruposCliente");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Clientes()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaClientes");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Endereco_Clientes()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaClientesEndereco");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Clientes_Endereco_IDFiscais()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaClienteIdentificacaoFiscal");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Clientes_Contatos()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaClientesContato");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Clientes_Anexos()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaClientesAnexo");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Utilizacao()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaUtilizacao");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Grupos_Produtos()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaGruposProduto");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Produtos()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaProdutos");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Naturezas_Destinacao()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaNaturezaDestinacao");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Fretes_Inconterms()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaFreteInconterms");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Empresa_Filial()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaEmpresasFiliais");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        public class ImpostosPedido
        {
            public string NumeroPedido { get; set; }

            public ImpostosPedido(string NumeroPedido)
            {
                this.NumeroPedido = NumeroPedido;
            }
        }

        [WebMethod]
        public string Atualiza_Impostos_Pedido(string NumeroPedido)
        {
            ImpostosPedido objImpostosPedido = new ImpostosPedido(NumeroPedido);

            string jsonImpostosPedido = jsonconv.ConverteObjectParaJSon<ImpostosPedido>(objImpostosPedido);

            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaImpostosPedido");

            HttpContent c = new StringContent(jsonImpostosPedido, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Impostos_Rascunho_Pedido(string NumeroRascunho)
        {
            WSClassePedidosImpostosPrincipal OBJItemPedidos = new WSClassePedidosImpostosPrincipal();

            string URI = "";
            string erro = "";
            HttpClient client = new HttpClient();
            URI = urlPadraoSAP + "api/Gets?consulta=Recupera_Imposto_Rascunho&hash=" + hash + "&filtro=ODRF.DocEntry%3D'" + NumeroRascunho.ToString() + "'";

            client.BaseAddress = new Uri(URI);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/json"));

            string JSONRetorno = client.GetStringAsync(URI).Result;

            OBJItemPedidos.ListaProdutosImpostos = jsonconv.ConverteJSonParaObject<List<WSClassePedidoImpostos>>(JSONRetorno);

            erro = OBJItemPedidos.AtualizaImpostosRascunho();

            return erro;

        }

        [WebMethod]
        public string Insere_Pedido_SAP(string JSONPedido)
        {
            //WSClassePedidoInclusao OBJPedidoInclusao = new WSClassePedidoInclusao();
            WSClassePedidoInclusaoRetorno OBJRetorno = new WSClassePedidoInclusaoRetorno();

            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(urlPadraoSAP + "api/Posts/Post?consulta=cria_esboco_pedido&hash=" + hash);

            HttpContent c = new StringContent(JSONPedido, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            //OBJRetorno = jsonconv.ConverteJSonParaObject<WSClassePedidoInclusaoRetorno>(t.Result.ToString());
            retorno = t.Result.ToString();

            return retorno;
        }

        [WebMethod]
        public string Atualiza_Pedido_SAP(string JSONPedido)
        {
            //WSClassePedidoInclusao OBJPedidoInclusao = new WSClassePedidoInclusao();
            WSClassePedidoInclusaoRetorno OBJRetorno = new WSClassePedidoInclusaoRetorno();

            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(urlPadraoSAP + "api/Posts/Post?consulta=atualizar_esboco_pedido&hash=" + hash);

            HttpContent c = new StringContent(JSONPedido, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            //OBJRetorno = jsonconv.ConverteJSonParaObject<WSClassePedidoInclusaoRetorno>(t.Result.ToString());
            retorno = t.Result.ToString();

            return retorno;
        }

        [WebMethod]
        public string Transforma_Esboco_Pedido_SAP(string JSONPedido, string userDIServer)
        {
            //WSClassePedidoInclusao OBJPedidoInclusao = new WSClassePedidoInclusao();
            WSClassePedidoInclusaoRetorno OBJRetorno = new WSClassePedidoInclusaoRetorno();

            string retorno = "";

            HttpClient client = new HttpClient();
            Uri u = new Uri(urlPadraoSAP + "api/Posts/PostComRetornoStatus?consulta=efetiva_esboco_pedido&hash=" + hash + "&userDiServer=" + userDIServer);

            HttpContent c = new StringContent(JSONPedido, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            //OBJRetorno = jsonconv.ConverteJSonParaObject<WSClassePedidoInclusaoRetorno>(t.Result.ToString());
            retorno = t.Result.ToString();

            return retorno;
        }

        public async Task<string> PostURI(Uri u, HttpContent c)
        {
            var response = string.Empty;

            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.PostAsync(u, c);
                if (result.IsSuccessStatusCode)
                {
                    //response = result.StatusCode.ToString();
                    var retorno = result.Content.ReadAsStringAsync();
                    response = retorno.Result.ToString();
                }
            }
            return response;
        }

        [WebMethod]
        public string Atualiza_Numero_Pedido_Esboco(string NumeroEsboco)
        {
            WSClasseRetornoDadosPrincipal OBJRetornoDadosPrincipal = new WSClasseRetornoDadosPrincipal();

            string URI = "";
            string erro = "";
            HttpClient client = new HttpClient();
            //URI = urlPadraoSAP + "api/Gets?consulta=RETORNA_PEDIDO_ESBOCO&hash=" + hash + "&filtro=DRAFTKEY%3D'" + NumeroEsboco.ToString() + "'";
            URI = urlPadraoSAP + "api/Gets?consulta=RETORNA_PEDIDO_ESBOCO_V2&hash=" + hash + "&filtro=DocEntry%3D'" + NumeroEsboco.ToString() + "'";

            client.BaseAddress = new Uri(URI);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/json"));

            string JSONRetorno = client.GetStringAsync(URI).Result;

            OBJRetornoDadosPrincipal.ListaRetornoDados = jsonconv.ConverteJSonParaObject<List<WSClasseRetornoDados>>(JSONRetorno);

            if (OBJRetornoDadosPrincipal.ListaRetornoDados.Count > 0)
            {
                //Atribui o número de esboço ao objeto
                OBJRetornoDadosPrincipal.ListaRetornoDados[0].NumeroEsbocoSAP = NumeroEsboco.ToString();
                erro = OBJRetornoDadosPrincipal.ListaRetornoDados[0].AtualizaPedidoSAP();
            }

            return erro;

        }

        [WebMethod]
        public string Atualiza_Producao_Pedido(string NumeroPedido)
        {
            WSClasseRetornoDadosPrincipal OBJRetornoDadosPrincipal = new WSClasseRetornoDadosPrincipal();

            string URI = "";
            string erro = "";
            HttpClient client = new HttpClient();
            URI = urlPadraoSAP + "api/Gets?consulta=CONSULTA_PEDIDO_PRODUCAO&hash=" + hash + "&filtro=Documento%3D'" + NumeroPedido.ToString() + "'";

            client.BaseAddress = new Uri(URI);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/json"));

            string JSONRetorno = client.GetStringAsync(URI).Result;

            OBJRetornoDadosPrincipal.ListaRetornoDados = jsonconv.ConverteJSonParaObject<List<WSClasseRetornoDados>>(JSONRetorno);

            if (OBJRetornoDadosPrincipal.ListaRetornoDados.Count > 0)
            {
                //Atribui o número de esboço ao objeto
                erro = OBJRetornoDadosPrincipal.ListaRetornoDados[0].AtualizaProducaoSAP();
            }
            else
            {
                WSClasseRetornoDados OBJRetornoDados = new WSClasseRetornoDados();

                OBJRetornoDados.Documento = NumeroPedido;
                OBJRetornoDados.QuantidadePendente = "0";
                OBJRetornoDados.AtualizaProducaoSAP();
            }

            return erro;

        }

        public class Notas_Fiscais_Pedido
        {
            public string NumeroPedido { get; set; }

            public Notas_Fiscais_Pedido(string NumeroPedido)
            {
                this.NumeroPedido = NumeroPedido;
            }
        }

        [WebMethod]
        public string Atualiza_Notas_Fiscais_Pedido(string NumeroPedido)
        {
            Notas_Fiscais_Pedido obj_Notas_Fiscais_Pedido = new Notas_Fiscais_Pedido(NumeroPedido);

            string json_Notas_Fiscais_Pedido = jsonconv.ConverteObjectParaJSon<Notas_Fiscais_Pedido>(obj_Notas_Fiscais_Pedido);

            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaNotasFiscaisPedido");

            HttpContent c = new StringContent(json_Notas_Fiscais_Pedido, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Atualiza_Depositos()
        {
            HttpClient client = new HttpClient();
            Uri u = new Uri(this.urlPadraoAPICRM + "api/AtualizaDepositosMaterial");

            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");
            var t = PostURI(u, c);
            t.Wait();

            retorno = t.Result.ToString();
            OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);

            return OBJRetorno.MsgRetorno;
        }

        [WebMethod]
        public string Valida_Regras_Aprovacao(string NumeroEsbocoSAP)
        {
            WSClassePedidoAprovacaoPrincipal OBJPedidoAprovacao = new WSClassePedidoAprovacaoPrincipal();

            string URI = "";
            string retorno = "";
            HttpClient client = new HttpClient();

            URI = urlPadraoSAP + "api/Gets?consulta=validaRegraAprovacao&hash=" + hash + "&filtro=DocEntry%3D'" + NumeroEsbocoSAP.ToString() + "'";

            client.BaseAddress = new Uri(URI);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/json"));

            string JSONRetorno = client.GetStringAsync(URI).Result;

            OBJPedidoAprovacao.ListaPedidoAprovacao = jsonconv.ConverteJSonParaObject<List<WSClassePedidoAprovacao>>(JSONRetorno);


            if (OBJPedidoAprovacao.ListaPedidoAprovacao.Count > 0)
            {
                retorno = OBJPedidoAprovacao.ListaPedidoAprovacao[0].UserDiServer;
            }
            else
            {
                retorno = "DiServer";
            }

            return retorno;
        }

        [WebMethod]
        public string Atualiza_Status_Pedido_Finalizado(string NumeroPedido)
        {
            WSClassePedidoStatusPrincipal OBJPedidoStatus = new WSClassePedidoStatusPrincipal();

            string URI = "";
            string erro = "";
            HttpClient client = new HttpClient();

            URI = urlPadraoSAP + "api/Gets?consulta=RETORNA_STATUS_PEDIDO&hash=" + hash + "&filtro=DocNum%3D'" + NumeroPedido + "'";

            client.BaseAddress = new Uri(URI);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/json"));

            string JSONRetorno = client.GetStringAsync(URI).Result;

            OBJPedidoStatus.ListaPedidosStatus = jsonconv.ConverteJSonParaObject<List<WSClassePedidoStatus>>(JSONRetorno);

            erro = OBJPedidoStatus.AtualizaStatusPedido();

            return erro;

        }

        [WebMethod]
        public string Salva_Pedido_SAP_EXTERNO(string empresa, string NumeroPedido, string usuarioCRM)
        {
            string erro = "";

            //Instancia classe pedido
            pedido novoPedido = new pedido();
            GerencialVendas.PedidoClass PedidoClass = new GerencialVendas.PedidoClass();
            funcoesBD mdlFuncoesBD = new funcoesBD();
            novoPedido.carregaDadosPedido(empresa, NumeroPedido);
            erro = novoPedido.EnviaPedidoSAP();

            if (erro == "")
            {
                //Recupera impostos do pedido
                PedidoClass.PedVendaNum = empresa;
                PedidoClass.EmpCod = NumeroPedido;
                PedidoClass.NumeroPedidoSAP = Convert.ToInt32(novoPedido.NumeroPedidoSAP ?? "0");
                PedidoClass.NumeroEsbocoSAP = Convert.ToInt32(novoPedido.NumeroEsbocoSAP ?? "0");
                PedidoClass.Consulta_Pedido();

                erro = mdlFuncoesBD.aprovaPedido(novoPedido.codigoEmpresa, novoPedido.numeroPedido, usuarioCRM, novoPedido.codigoEntidade.ToString());

                if (erro == "")
                {
                    //Atualiza o historico de acordo com historico CRM
                    erro = novoPedido.AtualizarHistoricoPedidoSAPAPI();

                    //Se conseguiu gravar historico corretamente transforma pedido
                    if (erro == "")
                    {
                        erro = novoPedido.TransformaEsbocoPedido();
                    }
                }
            }
            return erro;
        }

        [WebMethod]
        public string AtualizacaoGeral()
        {
            string erro = "";

            erro = Atualiza_Pais();
            erro = Atualiza_Estados();
            erro = Atualiza_Municipios();
            erro = Atualiza_Codigos_CNAE();
            erro = Atualiza_Vendedores();
            erro = Atualiza_Natureza_Juridica();
            erro = Atualiza_Grupo_Economico();
            erro = Atualiza_Condicao_Pagamento();
            erro = Atualiza_Grupo_Clientes();
            erro = Atualiza_Clientes();
            erro = Atualiza_Endereco_Clientes();
            erro = Atualiza_Clientes_Endereco_IDFiscais();
            erro = Atualiza_Clientes_Contatos();
            erro = Atualiza_Clientes_Anexos();
            erro = Atualiza_Utilizacao();
            erro = Atualiza_Grupos_Produtos();
            erro = Atualiza_Produtos();
            erro = Atualiza_Naturezas_Destinacao();
            erro = Atualiza_Fretes_Inconterms();
            erro = Atualiza_Empresa_Filial();
            erro = Atualiza_Depositos();
            erro = Atualiza_Lancamento_ContabilHistoricoAT();

            return erro;
        }

        #region HUB Cliente

        [WebMethod]
        public string PostCliente(int _IDCliente, string _Operacao)
        {
            FuncoesAPIClass OBJApi = new FuncoesAPIClass();
            jsonconv = new JsonConversao();

            //string _Retorno = "";
            //string _RetornoAlteraCodSap = "";
            string _JSON = "";
            //string _URI = "";
            string erro = "";

            HttpClient client = new HttpClient();
            ClienteClasse OBJCliente = new ClienteClasse();
            WsHubClienteClass ObjWsHubClienteClass = new WsHubClienteClass();
            WsHubClienteResponseClass ObjWsHubClienteResponseClass = new WsHubClienteResponseClass();

            //Cria URL
            //if (_Operacao == "Inclusão")
            //{
            //    _URI = urlPadraoSAP + "api/Posts/Post?consulta=criar_pn_lead&hash=" + hash;
            //}
            //else
            //{
            //    _URI = urlPadraoSAP + "api/Posts/Post?consulta=atualizar_cliente&hash=" + hash;
            //}

            //Consulta Dados para Enviar para o HUB
            ObjWsHubClienteClass.ExportaDadosCliente(_IDCliente, _Operacao);


            //Converte Classe em JSON
            //_JSON = jsonconv.ConverteObjectParaJSon<WsHubClienteClass>(ObjWsHubClienteClass);
            //_JSON = _JSON.Replace("\"data_Carta_IPI\":\"\",", "");

            _JSON = JsonConvert.SerializeObject(ObjWsHubClienteClass);
            if (_Operacao == "Inclusão")
            {
                erro = OBJApi.InclusaoClienteAPI(_JSON);
            }
            else
            {
                erro = OBJApi.AtualizacaoClienteAPI(_JSON);
            }

            //Chama API HUB
            //Uri u = new Uri(_URI);
            //HttpContent c = new StringContent(_JSON, Encoding.UTF8, "application/json");
            //var t = PostURI(u, c);
            //t.Wait();

            //Descarrega Dados
            //string retorno = t.Result.ToString();
            //ObjWsHubClienteResponseClass = jsonconv.ConverteJSonParaObject<WsHubClienteResponseClass>(t.Result.ToString());

            //if (ObjWsHubClienteResponseClass.resultPositivo == "true")
            //{

            //    if (_Operacao == "Inclusão")
            //    {
            //        //Atualiza Cliene com ID SAP
            //        OBJCliente.IDCliente = _IDCliente;
            //        OBJCliente.CodigoCliente = ObjWsHubClienteResponseClass.Codigo;

            //        _RetornoAlteraCodSap = OBJCliente.AlteraClienteCodigoSAP();

            //        if (_RetornoAlteraCodSap != "")
            //        {
            //            _Retorno += _RetornoAlteraCodSap;
            //        }

            //    }
            //}


            //Retorna Msg 

            //_Retorno += ObjWsHubClienteResponseClass.msg;
            //_Retorno = "resultPositivo:" + ObjWsHubClienteResponseClass.resultPositivo + " <br> " + _Retorno;

            return erro;

        }

        #endregion

        #region Lancamento Contabil

        [WebMethod]
        public string Atualiza_Lancamento_ContabilHistoricoAT()
        {
            string erro = "";
            LancamentoContabilClass OBJLancamentoContabil = new LancamentoContabilClass();

            erro = OBJLancamentoContabil.AtualizaHistoricosLancamentosContabeisAT();

            return erro;
        }

        [WebMethod]
        public string Cancela_Pedidos_Periodo()
        {
            string erro = "";

            pedido OBJPedido = new pedido();

            erro = OBJPedido.CancelaPedidosForaPeriodo();

            return erro;
        }

        #endregion


    }
}

