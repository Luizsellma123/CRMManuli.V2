using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;
using VendasWeb.WEBServiceSAP.ClassesWEBService;
using System.Text.RegularExpressions;
using VendasWeb.classes;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using VendasWeb.WEBServiceCRM;
using Newtonsoft.Json;

namespace VendasWeb
{
    public class pedido : clsConexao
    {
        public string CodigoUsuario { get; set; }
        public string tipoOperacao { get; set; }
        public string codigoEmpresa { get; set; }
        public string codigoEntidade { get; set; }
        public string tipoVendaCod { get; set; }
        public string numeroPedido { get; set; }
        public string tipo { get; set; }
        public string dataEmissao { get; set; }
        public string dataEntrega { get; set; }
        public string operacao { get; set; }
        public string especie { get; set; }
        public string natureza { get; set; }
        public string consumo { get; set; }
        public string regiao { get; set; }
        public string tipoFrete { get; set; }
        public float valorFrete { get; set; }
        public string transportadora { get; set; }
        public string descricaoTransportadora { get; set; }
        public string condicao { get; set; }
        public string nomeCondicao { get; set; }
        public string historico { get; set; }
        public string historicoAntigo { get; set; }
        public string observacao { get; set; }
        public string tabela { get; set; }
        public string tabelaPrincipal { get; set; }
        public string vendedor { get; set; }
        public string usuario { get; set; }
        public string empresaPaisSigla { get; set; }
        public string empresaPaisUfSigla { get; set; }
        public string entidadePaisSigla { get; set; }
        public string entidadeUfSigla { get; set; }
        public string tipoEntidade { get; set; }
        public string EntRgIe { get; set; }
        public float quantidadeTotal { get; set; }
        public string statusPedio { get; set; }
        public string descricaoStatus { get; set; }
        public float valorMercadoria { get; set; }
        public float valorIPI { get; set; }
        public float valorICMS { get; set; }
        public float valorTotal { get; set; }
        public string embarqueImediato { get; set; }
        public int ultimoSequencial { get; set; }
        public string ENTVALLIMCRED { get; set; }
        public string ENTDESDEDATA { get; set; }
        public string EntDataCad { get; set; }
        public string VendCadastrado { get; set; }
        public string PedVendaNumPedEnt { get; set; }
        public string ClasFiscal { get; set; }
        public string TribACod { get; set; }
        public string Tributacao { get; set; }
        public string NatOperacao { get; set; }
        public string IPIInclusoICMS { get; set; }
        public string VendCod { get; set; }
        public int IDPedido { get; set; }
        public int DiasCancelamento { get; set; }
        public string AprovadoProducaoCliche { get; set; }

        /*Campos utilizados para atualização na logisitica*/
        public double QuantidadeVolumes { get; set; }
        public string EspecieVolume { get; set; }
        public double PesoLiquido { get; set; }
        public double PesoBruto { get; set; }
        public double Diferimento { get; set; }

        public List<itemPedido> itemPedidoListAnterior { get; set; }
        public List<itemPedido> itemPedidoList { get; set; }
        public funcoesBD mdlfuncoesBD { get; set; }
        public funcoes mdlfuncoes { get; set; }

        /*Campo utilizado para setar para qual tela voltar se CRM ou Lista de Pedidos*/
        public string veioCRM { get; set; }

        public string CodigoEmpresaSAP { get; set; }
        public string CodigoVendedorSAP { get; set; }
        public string CodigoCondicaoPagamentoSAP { get; set; }
        public string CodigoClienteSAP { get; set; }
        public string NumeroEsbocoSAP { get; set; }
        public string NumeroPedidoSAP { get; set; }
        public int IDStatus { get; set; }

        public string HistoricoAtualizado { get; set; }


        JsonConversao jsonconv = new JsonConversao();
        FuncoesAPIClass OBJApi = new FuncoesAPIClass();

        //Construtor da classe pedido
        public pedido()
        {
            this.numeroPedido = "0";
            this.historico = "";
            this.historicoAntigo = "";
            this.observacao = "";

            //Campos utilizados na atualização da logística
            this.QuantidadeVolumes = 0;
            this.EspecieVolume = "";
            this.PesoLiquido = 0;
            this.PesoBruto = 0;
            this.embarqueImediato = "Sim";

            //Verifica se esta instanciado
            if (this.itemPedidoListAnterior == null)
            {
                this.itemPedidoListAnterior = new List<itemPedido>();
            }

            //Verifica se esta instanciado
            if (this.itemPedidoList == null)
            {
                this.itemPedidoList = new List<itemPedido>();
            }
        }

        //Metodo para inserir dados items do pedido
        public void incluiItem(produto itemProduto)
        {
            itemPedido novoItem = new itemPedido(itemProduto.codigoProduto, itemProduto.codigoTabela,
                itemProduto.valorTabela, itemProduto.valorItem, itemProduto.unidade, itemProduto.quantidade, this.codigoEmpresa, itemProduto.descricaoProduto, itemProduto.CompdescricaoProduto, itemProduto.numSeq, itemProduto.ItPedVendaNumSeq, itemProduto.valorOriginal, itemProduto.CodigoProdutoCliche, itemProduto.CodigoProdutoArruela,
                itemProduto.xPed, itemProduto.nItem);

            //Verifica se esta instanciado
            if (this.itemPedidoList == null)
            {
                this.itemPedidoList = new List<itemPedido>();
            }
            this.itemPedidoList.Add(novoItem);
        }

        //Retorna número de itens
        public int numeroItens()
        {
            return this.itemPedidoList.Count;
        }

        //Remove item da lista
        public void removeItem(int indexItem)
        {
            //Verifica se esta instanciado
            if (this.itemPedidoList != null)
            {
                this.itemPedidoList.RemoveAt(indexItem);
            }
        }

        //Função para gravar pedido na base
        public string gravaPedido()
        {
            string[] retDados = new string[2];
            string retErro = "";
            this.tipoVendaCod = "0000002";
            this.valorTotal = 0;

            if (this.tipoOperacao == "inclusao")
            {
                this.statusPedio = "1";
                this.descricaoStatus = "Orçamento";
            }

            this.quantidadeTotal = 0;

            //Declara objeto conexao
            this.conexao();

            //Carrega a regiao do cliente
            //this.regiao = this.buscaRegiao();

            //Busca Sigla Pais e UF da Empresa
            //retDados = this.buscaSiglaEmpresa();
            //this.empresaPaisSigla = retDados[0];
            //this.empresaPaisUfSigla = retDados[1];

            //Busca Sigla Pais e UF da entidade
            retDados = this.buscaSiglaEntidade();
            this.entidadePaisSigla = retDados[0];
            this.entidadeUfSigla = retDados[1];


            //Busca nome condicao de pagamento
            this.nomeCondicao = this.buscaNomeCondicao();

            //this.tipo = this.buscaTipoPedido();

            //Busca numero de pedido
            //if (this.numeroPedido == null || this.numeroPedido == "0")
            //{
            //    this.numeroPedido = this.buscaNumeroPedido();
            //}

            //Busca tabela principal
            this.tabelaPrincipal = this.buscaTabela();

            //Grava Pedido no banco
            retErro =
                this.mdlfuncoesBD.gravaPedido(this.codigoEmpresa, this.numeroPedido, this.tipo, this.dataEmissao,
                this.dataEntrega, this.codigoEntidade, this.tipoVendaCod, this.regiao, this.buscaNome(), this.usuario,
                this.observacao, this.historico, this.tipoFrete, this.transportadora, this.consultaNatureza(), this.tabelaPrincipal,
                this.operacao, this.especie, this.tipoOperacao, this.statusPedio, this.descricaoStatus, this.QuantidadeVolumes,
                this.EspecieVolume, this.PesoLiquido, this.PesoBruto, this.PedVendaNumPedEnt, this.consumo);

            if (retErro == "")
            {
                retErro = gravaComplementoPedido();
            }

            return retErro;
        }

        public string gravaPedidoCRM()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CRM_PEDIDO_VENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoOperacao", SqlDbType.NVarChar, 100, "TipoOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoCliente", SqlDbType.NVarChar, 100, "NumeroPedidoCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmbarqueImediato", SqlDbType.VarChar, 10, "EmbarqueImediato"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 10, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataEntrega", SqlDbType.DateTime, 10, "DataEntrega"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataLancamento", SqlDbType.DateTime, 10, "DataLancamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDVendedor", SqlDbType.Int, 0, "IDVendedor"));
                    dbCommand.Parameters.Add(new SqlParameter("@ObservacaoPedido", SqlDbType.NText, 0, "ObservacaoPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@ObservacaoNotaFiscal", SqlDbType.NText, 0, "ObservacaoNotaFiscal"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCondPag", SqlDbType.Int, 0, "IDCondPag"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoFrete", SqlDbType.Int, 0, "IDTipoFrete"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoTransportadoraSAP", SqlDbType.VarChar, 100, "CodigoTransportadoraSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTabela", SqlDbType.Int, 0, "IDTabela"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDOperacao", SqlDbType.Int, 0, "IDOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@ValorFrete", SqlDbType.Decimal, 0, "ValorFrete"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, ParameterDirection.InputOutput, false, 0, 0, "IDPedido", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.codigoEmpresa;
                    dbCommand.Parameters["@TipoOperacao"].Value = this.tipoOperacao;
                    dbCommand.Parameters["@NumeroPedidoCliente"].Value = this.PedVendaNumPedEnt;
                    dbCommand.Parameters["@EmbarqueImediato"].Value = this.embarqueImediato;
                    dbCommand.Parameters["@IDCliente"].Value = this.codigoEntidade;
                    dbCommand.Parameters["@DataEntrega"].Value = Convert.ToDateTime(this.dataEntrega);
                    dbCommand.Parameters["@DataLancamento"].Value = Convert.ToDateTime(this.dataEmissao);
                    dbCommand.Parameters["@IDVendedor"].Value = this.VendCod;
                    dbCommand.Parameters["@IDStatus"].Value = 1;
                    dbCommand.Parameters["@IDPedido"].Value = this.numeroPedido;
                    dbCommand.Parameters["@IDCondPag"].Value = this.condicao;
                    dbCommand.Parameters["@IDTipoFrete"].Value = this.tipoFrete;
                    dbCommand.Parameters["@CodigoTransportadoraSAP"].Value = this.transportadora;
                    dbCommand.Parameters["@IDTabela"].Value = Convert.ToInt32(this.buscaTabela() ?? "1");
                    dbCommand.Parameters["@IDOperacao"].Value = this.operacao;
                    dbCommand.Parameters["@ValorFrete"].Value = this.valorFrete;
                    dbCommand.Parameters["@ObservacaoNotaFiscal"].Value = this.observacao;

                    /******************Rotina Para Tratar Historico**************************/
                    if (this.historico == "")
                    {
                        this.historico = "Pedido Atualizado.";
                    }

                    if (this.historicoAntigo != "")
                    {
                        dbCommand.Parameters["@ObservacaoPedido"].Value = this.historicoAntigo + "\n\n" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + ' ' + this.CodigoUsuario + ":" + this.historico;
                    }
                    else
                    {
                        dbCommand.Parameters["@ObservacaoPedido"].Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + ' ' + this.CodigoUsuario + ":" + this.historico;
                    }
                    /***********************************************************************/

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                    this.IDPedido = Convert.ToInt32(dbCommand.Parameters["@IDPedido"].Value);
                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção do pedido";
                }
            }

            return erro;
        }

        public string gravaItemPedido()
        {
            string retErro = "";
            int quant = 0;
            int cont = 0;
            int numseq = 0;
            int achou = 0;
            float auxValorFrete = (float)Math.Round(this.valorFrete, 2);

            if (this.itemPedidoList != null)
            {
                quant = this.numeroItens();
            }

            while (cont < quant && quant > 0 && retErro == "")
            {
                if (cont != 0)
                {
                    auxValorFrete = 0;
                }

                //Verifica se o sequencial esta carregado, se não estiver busca o sequencial(deveria sempre chegar carregado sequencial)
                if (this.itemPedidoList[cont].numSeq != 0)
                {
                    numseq = this.itemPedidoList[cont].numSeq;
                }
                else
                {
                    numseq = this.buscaSequencial();
                    this.itemPedidoList[cont].numSeq = numseq;
                }

                achou = 0;
                if (this.itemPedidoListAnterior.Find(item => (item.codigoProduto == this.itemPedidoList[cont].codigoProduto && item.numSeq == this.itemPedidoList[cont].numSeq)) != null)
                {
                    achou = 1;
                }

                //Na opeção de inclusao o campo this.tipoOperacao vai estar setado como inclusão por isto sem problemas entrar nesta regra.
                if (achou == 1)
                {
                    //Chama funcao para grava item
                    /*
                    retErro = this.itemPedidoList[cont].gravaItemPedido(this.codigoEmpresa, this.numeroPedido, this.vendedor,
                        this.codigoEntidade, this.consultaNatureza(), this.empresaPaisSigla, this.empresaPaisUfSigla, this.entidadePaisSigla,
                        this.entidadeUfSigla, this.condicao, this.operacao, this.especie, numseq, this.usuario, auxValorFrete, this.dataEmissao,
                        this.tipoOperacao, this.PedVendaNumPedEnt, this.IPIInclusoICMS);
                    */
                    retErro = this.itemPedidoList[cont].gravaItemPedidoCRM(this.codigoEmpresa, this.IDPedido, this.operacao, this.tipoOperacao, this.consumo);
                }
                else
                {
                    //Chama funcao para grava item
                    /*
                    retErro = this.itemPedidoList[cont].gravaItemPedido(this.codigoEmpresa, this.numeroPedido, this.vendedor,
                        this.codigoEntidade, this.consultaNatureza(), this.empresaPaisSigla, this.empresaPaisUfSigla, this.entidadePaisSigla,
                        this.entidadeUfSigla, this.condicao, this.operacao, this.especie, numseq, this.usuario, auxValorFrete,
                        this.dataEmissao, "inclusao", this.PedVendaNumPedEnt, this.IPIInclusoICMS);
                        */
                    retErro = this.itemPedidoList[cont].gravaItemPedidoCRM(this.codigoEmpresa, this.IDPedido, this.operacao, this.tipoOperacao, this.consumo);
                }


                cont++;
            }

            return retErro;
        }

        public string testaRegraItens()
        {
            string retErro = "";
            int quant = 0;
            int cont = 0;
            float auxValorFrete = (float)Math.Round(this.valorFrete, 2);

            if (this.itemPedidoList != null)
            {
                quant = this.numeroItens();
            }

            while (cont < quant && quant > 0 && retErro == "")
            {
                if (cont != 0)
                {
                    auxValorFrete = 0;
                }

                //Chama funcao para grava item
                retErro = this.itemPedidoList[cont].testaRegraItemPedido(this.codigoEmpresa, this.numeroPedido, this.vendedor,
                    this.codigoEntidade, this.consultaNatureza(), this.empresaPaisSigla, this.empresaPaisUfSigla, this.entidadePaisSigla,
                    this.entidadeUfSigla, this.condicao, this.operacao, this.especie, cont, this.usuario, auxValorFrete, this.dataEmissao);

                cont++;
            }

            return retErro;
        }

        //Função para excluir itens
        public string excluiItens()
        {
            string erro = "";
            int cont = 0;
            int quant = 0;
            int achou = 0;

            quant = this.itemPedidoListAnterior.Count;

            while (cont < quant && quant > 0 && erro == "")
            {

                erro = this.mdlfuncoesBD.excluiItens(this.codigoEmpresa, this.numeroPedido, this.itemPedidoListAnterior[cont].codigoProduto, this.itemPedidoListAnterior[cont].numSeq);

                cont++;
            }

            return erro;
        }

        //Função para alterar itens
        public string salvaItens()
        {
            string erro = "";

            if (this.tipoOperacao == "alteracao")
            {
                erro = this.excluiItens();
            }

            if (erro == "")
            {
                erro = this.gravaItemPedido();
            }
            return erro;
        }


        //Atualiza dados pedidos
        public string gravaFinalizaPedido()
        {
            string retErro = "";

            retErro = this.mdlfuncoesBD.finalizaPedido(this.codigoEmpresa, this.numeroPedido, this.condicao, this.nomeCondicao,
                this.vendedor, this.dataEmissao, this.usuario, this.codigoEntidade, this.itemPedidoList[0].classeRecDesp, this.operacao, this.tipoOperacao);

            return retErro;
        }

        //Busca regiao do cliente
        public string buscaRegiao()
        {
            string strSQL = "";
            string retRegiao = "";
            DataTable dadosTable = new DataTable();

            //Recupera regiao do cliente
            strSQL = "select RegCodEstr from ENTIDADE where EntCod like '" + this.codigoEntidade.ToString() + "' ";

            this.conexao();
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "buscaRegiao - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    retRegiao = (string)Convert.ToString(row["RegCodEstr"]);
                }
            }
            return retRegiao;
        }

        //Busca Sigla Pais e UF da Entidade
        public string[] buscaSiglaEntidade()
        {
            DataTable dadosTable = new DataTable();

            string strSQL = "";
            string[] retDados = new string[3];

            try
            {

                //Recupera Sigla Pais e UF da Entidae
                strSQL = "select CP.CodigoSAP as PaisSigla,CE.CodigoEstadoSAP as UfSigla  from CRM_CLIENTE CC ";
                strSQL += "INNER JOIN CRM_CLIENTE_ENDERECO CCE ON CC.IDCliente = CCE.IDCliente and CCE.DescricaoEndereco = 'ENTREGA' ";
                strSQL += "INNER JOIN CRM_PAIS CP ON CCE.IDPais = CP.IDPais ";
                strSQL += "INNER JOIN CRM_ESTADO CE ON CP.IDPais = CE.IDPais and CCE.IDEstado = CE.IDEstado ";
                strSQL += "where CC.IDCliente = '" + this.codigoEntidade.ToString() + "' ";

                this.conexao();
                dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "buscaSiglaEntidade - pedido.cs");

                if (dadosTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dadosTable.Rows)
                    {
                        retDados[0] = (string)Convert.ToString(row["PaisSigla"]);
                        retDados[1] = (string)Convert.ToString(row["UfSigla"]);
                        retDados[2] = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string AnaliDados = "";
                AnaliDados += "|@strSQL:" + strSQL;
                AnaliDados += "|JSONPedido:" + LogAuditoria.ClassesAuditoria.LogErroClass.jsonconv.ConverteObjectParaJSon<pedido>(this);
                LogAuditoria.ClassesAuditoria.LogErroClass.GravaLOGErroStatic(0, "buscaSiglaEntidade", ex, AnaliDados);

                retDados[2] = "Não foi possivel recuperar dados de localização cliente.";
            }
            return retDados;
        }

        //Busca Sigla Pais e UF da Empresa
        public string[] buscaSiglaEmpresa()
        {
            DataTable dadosTable = new DataTable();

            string strSQL = "";
            string[] retDados = new string[2];

            //Recupera Sigla Pais e UF da Empresa
            strSQL = "select cid.PaisSigla, cid.UfSigla from EMPRESA_FILIAL emp, CIDADE cid where emp.EmpCod='" + this.codigoEmpresa.ToString() + "' and emp.CidCod=cid.CidCod";

            this.conexao();
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "buscaSiglaEmpresa - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    retDados[0] = (string)Convert.ToString(row["PaisSigla"]);
                    retDados[1] = (string)Convert.ToString(row["UfSigla"]);
                }
            }
            return retDados;
        }

        //Busca uf entidade e nat operação
        public string buscaInfoProduto(string produto)
        {
            DataTable dadosTable = new DataTable();

            string strSQL = "";
            string retDados = "";

            //Recupera Sigla Pais e UF da Empresa
            strSQL = "select Pro.ProdCodEstr, Pro.TribACod, Pro.TribBCod, Pro.ClasFiscCod from PRODUTO Pro where Pro.ProdCodEstr = '" + produto + "'";

            this.conexao();
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "buscaInfoProduto - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    retDados = (string)Convert.ToString(row["ClasFiscCod"]);
                    ClasFiscal = (string)Convert.ToString(row["ClasFiscCod"]);
                    TribACod = (string)Convert.ToString(row["TribACod"]);
                }
            }
            return retDados;
        }

        public void buscaCFOPDiferimento(string entidade)
        {
            DataTable dadosTable = new DataTable();

            string strSQL = "";

            //Recupera Sigla Pais e UF da Empresa          
            strSQL = "select TipoFatNatOpInternaProd from TIPO_FATURAMENTO where TipoFatCod in(select TipoFatCod from TIPO_FAT_ENTIDADE where EntCod ='" + entidade + "')";

            this.conexao();
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "buscaCFOPDiferimento - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    NatOperacao = (string)Convert.ToString(row["TipoFatNatOpInternaProd"]);
                }
            }
        }

        //Busca nome condicao pagamento
        public string buscaNomeCondicao()
        {

            DataTable dadosTable = new DataTable();

            string strSQL = "";
            string retDados = "";

            //Recupera condicao pagamento
            strSQL = "select NomeCondicao from CRM_CONDICAO_PAGAMENTO where IDCondPag='" + this.condicao.ToString() + "'";

            this.conexao();
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "buscaNomeCondicao - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    retDados = (string)Convert.ToString(row["NomeCondicao"]);
                }
            }

            return retDados;
        }

        //Declare objeto da conexao
        public void conexao()
        {
            if (this.mdlfuncoesBD == null)
            {
                this.mdlfuncoesBD = new funcoesBD();
            }

            if (this.mdlfuncoes == null)
            {
                this.mdlfuncoes = new funcoes();
            }
        }

        //Calculo Tipo do pedido
        public string buscaTipoPedido()
        {
            return "Total";
        }

        public string buscaNumeroPedido()
        {
            string retNumero = "";
            retNumero = this.mdlfuncoesBD.recuperaNumeroPedido(this.codigoEmpresa);
            return retNumero;
        }

        //Busca Nome Entidade
        public string buscaNome()
        {
            DataTable dadosTable = new DataTable();

            string strSQL = "";
            string retNome = "";

            //Recupera regiao do cliente
            strSQL = "select EntNome from ENTIDADE where EntCod like '" + this.codigoEntidade.ToString() + "' ";

            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "buscaNome - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    retNome = (string)Convert.ToString(row["EntNome"]);
                }
            }

            return retNome;
        }

        //Busca Tabela principal
        public string buscaTabela()
        {
            string retTabela = "";

            retTabela = this.itemPedidoList[0].codigoTabela;

            return retTabela;
        }

        //Carrega dados pedido
        public string carregaDadosPedido(string empresa, string numPedido)
        {
            string strSQL = "";
            string erro = "";

            DataTable dadosTable = new DataTable();
            DataTable compDadosTable = new DataTable();
            DataTable BloqueadosDadosTable = new DataTable();

            //strSQL = "select coalesce(PedVendaTranspEntCod, EntCod) as PedVendaTranspEntCod, coalesce(PedVendaStatFrete, '') as PedVendaStatFrete, PedVendaEntNat, PedVendaTipo, PedVendaStatDescr, EntCod, convert(varchar(10),PedVendaData,103) as dataEmissao, convert(varchar(10),PedVendaDataEntrega,103) as dataEntrega, PedVendaValIcms, PedVendaValIpiCalc, PedVendaValTotal, PedVendaValMerc, TipoVendaCod, RegCodEstr, PedVendaStatFrete, PedVendaValFrete, TabPVCod, StatPedVendaCod, PedVendaValMerc, PedVendaValIpiCalc, PedVendaValIcms, PedVendaValTotal, PedVendaStatDescr, PedVendaQtdVol, PedVendaPesoLiq, PedVendaPesoBruto, PedVendaEspecVol,isnull(PedVendaNumPedEnt,'') as PedVendaNumPedEnt from PED_VENDA where PedVendaNum='" + numPedido + "' and EmpCod='" + empresa + "'";
            strSQL = "select isnull(CC2.CodigoClienteSAP,'0') as TRCodigoClienteSAP, CPV.IDTipoFrete, coalesce(CFI.Descricao, '') as DescricaoFrete, ";
            strSQL += "isnull(CNJ.Nome, '') NaturezaJuridica, CSP.DescricaoStatus, CC.IDCliente, ";
            strSQL += "convert(varchar(10), CPV.DataLancamento, 103) as DataLancamento, ";
            strSQL += "convert(varchar(10), CPV.DataEntrega, 103) as DataEntrega, CPV.IDTabela, CPV.IDStatus, CSP.DescricaoStatus, CPV.NumeroPedidoCliente, CPV.EmbarqueImediato, CPV.IDOperacao, ";
            strSQL += " CPV.ValorFrete, isnull(CPV.NumeroEsbocoSAP,0) NumeroEsbocoSAP, ISNULL(CPV.DiasCancelamento,3) as DiasCancelamento, ";
            strSQL += "isnull(CPV.NumeroPedidoSAP,0) NumeroPedidoSAP ";
            strSQL += "from CRM_PEDIDO_VENDA CPV INNER JOIN CRM_FRETE_INCOTERMS CFI ON CPV.IDTipoFrete = CFI.IDTipoFrete ";
            strSQL += "INNER JOIN CRM_STATUS_PEDIDOS CSP ON CSP.IDStatus = CPV.IDStatus ";
            strSQL += "INNER JOIN CRM_CLIENTE CC ON CPV.IDCliente = CC.IDCliente ";
            strSQL += "LEFT JOIN CRM_CLIENTE CC2 ON CPV.IDTransportadora = CC2.IDCliente ";
            strSQL += "LEFT JOIN CRM_NATUREZA_JURIDICA CNJ ON CNJ.IDNatureza = CC.IDNatureza ";
            strSQL += "where CPV.IDEmpresa = '" + empresa + "' and CPV.IDpedido = '" + numPedido + "' ";


            //Instancia objetos de consulta
            this.conexao();

            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    this.IDPedido = Convert.ToInt32(numPedido.ToString() ?? "0");
                    this.codigoEmpresa = empresa.ToString();
                    this.numeroPedido = numPedido.ToString();
                    this.NumeroEsbocoSAP = (string)row["NumeroEsbocoSAP"].ToString();
                    this.NumeroPedidoSAP = (string)row["NumeroPedidoSAP"].ToString();
                    this.codigoEntidade = (string)row["IDCliente"].ToString();
                    //this.tipoVendaCod = (string)row["TipoVendaCod"].ToString();
                    //this.tipo = (string)row["PedVendaTipo"].ToString();
                    this.dataEmissao = (string)row["DataLancamento"].ToString();
                    this.dataEntrega = (string)row["DataEntrega"].ToString();

                    if ((string)row["NaturezaJuridica"].ToString() == "Consumidor" || (string)row["NaturezaJuridica"].ToString() == "Consumidor Contribuinte")
                    {
                        this.consumo = "Sim";
                    }
                    else
                    {
                        this.consumo = "Nao";
                    }

                    //this.regiao = (string)row["RegCodEstr"].ToString();
                    //this.tipoFrete = (string)row["DescricaoFrete"].ToString();
                    this.tipoFrete = (string)row["IDTipoFrete"].ToString();
                    this.valorFrete = (float)Convert.ToDecimal(row["ValorFrete"]);
                    this.transportadora = (string)row["TRCodigoClienteSAP"].ToString();
                    this.tabelaPrincipal = (string)row["IDTabela"].ToString();
                    this.statusPedio = (string)row["IDStatus"].ToString();
                    this.descricaoStatus = (string)row["DescricaoStatus"].ToString();
                    //this.valorMercadoria = (float)Convert.ToDecimal(row["PedVendaValMerc"]);
                    //this.valorIPI = (float)Convert.ToDecimal(row["PedVendaValIpiCalc"]);
                    //this.valorICMS = (float)Convert.ToDecimal(row["PedVendaValIcms"]);
                    //this.valorTotal = (float)Convert.ToDecimal(row["PedVendaValTotal"]);
                    /*
                    if (row["PedVendaQtdVol"] is Nullable || row["PedVendaQtdVol"].ToString() == "")
                    {
                        this.QuantidadeVolumes = 0;
                    }
                    else
                    {
                        this.QuantidadeVolumes = (double)Convert.ToDouble(row["PedVendaQtdVol"]);
                    }
                    
                    this.EspecieVolume = (string)row["PedVendaEspecVol"].ToString();

                    if (row["PedVendaPesoLiq"] is Nullable || row["PedVendaPesoLiq"].ToString() == "")
                    {
                        this.PesoLiquido = 0;
                    }
                    else
                    {
                        this.PesoLiquido = (double)Convert.ToDouble(row["PedVendaPesoLiq"]);
                    }

                    if (row["PedVendaPesoBruto"] is Nullable || row["PedVendaPesoBruto"].ToString() == "")
                    {
                        this.PesoBruto = 0;
                    }
                    else
                    {
                        this.PesoBruto = (double)Convert.ToDouble(row["PedVendaPesoBruto"]);
                    }
                    */
                    this.PedVendaNumPedEnt = (string)row["NumeroPedidoCliente"].ToString();
                    this.embarqueImediato = (string)row["EmbarqueImediato"].ToString();
                    this.operacao = (string)row["IDOperacao"].ToString();
                    this.DiasCancelamento = Convert.ToInt32(row["DiasCancelamento"]);
                }
            }

            //Limpa dados DataTable
            dadosTable.Clear();


            if (this.transportadora != null)
            {
                //strSQL = "select EntNome from ENTIDADE where EntCod ='" + this.transportadora.ToString() + "'";
                strSQL = "select NomeCliente from CRM_CLIENTE where CodigoClienteSAP = '" + this.transportadora.ToString() + "'";

                dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

                if (dadosTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dadosTable.Rows)
                    {
                        this.descricaoTransportadora = (string)row["NomeCliente"].ToString();
                    }
                }
                dadosTable.Clear();
            }


            //strSQL = "select coalesce(CondPagPedVendaNome, '') as CondPagPedVendaNome, CondPagCod from COND_PAG_PED_VENDA where PedVendaNum='" + numeroPedido + "' and EmpCod='" + empresa + "'";
            strSQL = "select CCP.IDCondPag, CCP.NomeCondicao from CRM_PEDIDO_VENDA CPV INNER JOIN CRM_CONDICAO_PAGAMENTO CCP ON CCP.IDCondPag = CPV.IDCondPag ";
            strSQL += "where CPV.IDEmpresa = '" + empresa + "' and CPV.IDpedido = '" + numeroPedido + "' ";
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    this.condicao = (string)row["IDCondPag"].ToString();
                    this.nomeCondicao = (string)row["NomeCondicao"].ToString();
                }
            }
            dadosTable.Clear();

            //strSQL = "select PedVendaTexto, PedVendaTextoHist from TEXTO_PED_VENDA where PedVendaNum='" + numeroPedido + "' and EmpCod='" + empresa + "'";
            strSQL = "select CPV.ObservacaoPedido, CPV.ObservacaoNotaFiscal from CRM_PEDIDO_VENDA CPV where CPV.IDEmpresa = '" + empresa + "' and CPV.IDpedido = '" + numeroPedido + "' ";
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    //this.observacao = (string)row["PedVendaTexto"].ToString().Replace("#", "").Replace("&", "");
                    this.historicoAntigo = (string)row["ObservacaoPedido"].ToString().Replace("#", "").Replace("&", "");
                    this.observacao = (string)row["ObservacaoNotaFiscal"].ToString().Replace("#", "").Replace("&", "");
                }
            }
            dadosTable.Clear();

            //strSQL = "select VendCod from VEND_PED_VENDA where EmpCod='" + empresa + "' and PedVendaNum='" + numeroPedido + "'";
            strSQL = "select CC.IDVendedor, CV.NomeVendedor from CRM_PEDIDO_VENDA CPV INNER JOIN CRM_CLIENTE CC ON CC.IDCliente = CPV.IDCliente ";
            strSQL += "INNER JOIN CRM_VENDEDOR CV ON CV.IDVendedor = CC.IDVendedor ";
            strSQL += "where CPV.IDEmpresa = '" + empresa + "' and CPV.IDpedido = '" + numeroPedido + "' ";
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    this.vendedor = (string)row["IDVendedor"].ToString();
                }
            }
            dadosTable.Clear();

            strSQL = "select CTP.IDTabela, CTP.Nome from CRM_TABELA_PRECO CTP INNER JOIN CRM_TABELA_EMPRESA CTE ON CTP.IDTabela=CTE.IDTabela WHERE CTE.IDEmpresa='" + empresa + "'";
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

            int tstCont = 0;
            this.tabela = "";

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    if (tstCont == 0)
                    {
                        this.tabela += "('" + row["IDTabela"].ToString() + "'";
                        tstCont++;
                    }
                    else
                    {
                        this.tabela += ", '" + row["IDTabela"].ToString() + "'";
                    }
                }

                this.tabela += ")";
            }
            else
            {
                this.tabela = "('')";
            }

            dadosTable.Clear();

            string[] retDados = new string[2];

            //Busca Sigla Pais e UF da Empresa
            /*
            if (this.empresaPaisSigla == null)
            {
                retDados = this.buscaSiglaEmpresa();
                this.empresaPaisSigla = retDados[0];
                this.empresaPaisUfSigla = retDados[1];
            }
            */

            //Busca Sigla Pais e UF da entidade
            if (this.entidadePaisSigla == null)
            {
                retDados = this.buscaSiglaEntidade();
                this.entidadePaisSigla = retDados[0];
                this.entidadeUfSigla = retDados[1];
                erro = retDados[2];
            }

            //strSQL = "select ProdCodEstr, ItPedVendaTexto, ItPedVendaUnidMedCod, ItPedVendaQtd, TabPvCod, ItPedVendaValUnit, ItPedVendaValTabPV, ItPedVendaSeq,ItPedVendaNumSeq from ITEM_PED_VENDA WHERE EmpCod='" + empresa.ToString() + "' and PedVendaNum='" + numPedido.ToString() + "'";
            strSQL = "select CPI.IDNaturezaDestinacao, isnull(CP3.CodigoProdutoSAP,'') ProdutoArruela, isnull(CP2.CodigoProdutoSAP,'') ProdutoCliche, CP.CodigoProdutoSAP, CP.Nome NomeProduto, CP.UnidadeVenda, ";
            strSQL += "CPI.xPed, CPI.nItem, ";
            strSQL += "CPI.Quantidade, CPV.IDTabela, CPI.PrecoUnitario, CPI.nItem AS Sequencia ";
            strSQL += " from ";
            strSQL += "CRM_PEDIDO_VENDA CPV INNER JOIN CRM_PEDIDO_ITENS CPI ON CPV.IDEmpresa = CPI.IDEmpresa and CPV.IDPedido = CPI.IDPedido ";
            strSQL += "INNER JOIN CRM_PRODUTO CP ON CPI.IDProduto = CP.IDProduto ";
            strSQL += "LEFT JOIN CRM_PRODUTO CP2 ON CPI.IDProdutoCliche = CP2.IDProduto ";
            strSQL += "LEFT JOIN CRM_PRODUTO CP3 ON CPI.IDProdutoArruela = CP3.IDProduto ";
            strSQL += "where CPV.IDPedido = '" + numPedido.ToString() + "' and CPV.IDEmpresa = '" + empresa.ToString() + "' ";
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                produto novoProduto = new produto();
                produto compProduto = new produto();

                tstCont = 0;

                foreach (DataRow row in dadosTable.Rows)
                {
                    novoProduto.codigoProduto = row["CodigoProdutoSAP"].ToString();
                    novoProduto.codigoTabela = row["IDTabela"].ToString();
                    novoProduto.descProduto = row["NomeProduto"].ToString();
                    novoProduto.descricaoProduto = row["NomeProduto"].ToString();
                    novoProduto.quantidade = (double)Convert.ToDouble(row["Quantidade"]);
                    novoProduto.revenda = "0";
                    novoProduto.unidade = row["UnidadeVenda"].ToString();
                    novoProduto.valorItem = (float)Convert.ToDecimal(row["PrecoUnitario"]);
                    novoProduto.valorTabela = (float)Convert.ToDecimal(row["PrecoUnitario"]);
                    novoProduto.numSeq = (int)Convert.ToInt64(row["Sequencia"]);
                    novoProduto.ItPedVendaNumSeq = (int)Convert.ToInt64(row["Sequencia"]);
                    novoProduto.CodigoProdutoCliche = row["ProdutoCliche"].ToString();
                    novoProduto.CodigoProdutoArruela = row["ProdutoArruela"].ToString();
                    novoProduto.xPed = row["xPed"].ToString();
                    novoProduto.nItem = row["nItem"].ToString();

                    this.consumo = row["IDNaturezaDestinacao"].ToString();
                    //Carrega valor tabela original
                    /*
                    strSQL = "SELECT ValorTabPv FROM User_TB_Pedido_Bloqueado_Itens WHERE EmpCod='" + empresa.ToString() + "' and PedVendaNum='" + numPedido.ToString() + "' and Prodcodestr ='" + row["ProdCodEstr"].ToString() + "' and Itpedvendaseq = '" + row["ItPedVendaSeq"].ToString() + "'";
                    BloqueadosDadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");
                    if (BloqueadosDadosTable.Rows.Count > 0)
                    {
                        foreach (DataRow bloqueadoRow in BloqueadosDadosTable.Rows)
                        {
                            novoProduto.valorOriginal = (float)Convert.ToDecimal(bloqueadoRow["ValorTabPv"]);

                        }
                        compDadosTable.Clear();
                    }
                    */

                    this.incluiItem(novoProduto);
                    /*
                    strSQL = "SELECT * FROM COMP_ITEM_PED_VENDA WHERE EmpCod='" + empresa.ToString() + "' and PedVendaNum='" + numPedido.ToString() + "' and CompItPedVendaProdCodEstrIt ='" + row["ProdCodEstr"].ToString() + "' and ItPedVendaSeq = '" + row["ItPedVendaSeq"].ToString() + "'";

                    compDadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");
                    if (compDadosTable.Rows.Count > 0)
                    {
                        foreach (DataRow compRow in compDadosTable.Rows)
                        {
                            compProduto.codigoProduto = compRow["ProdCodEstr"].ToString();
                            compProduto.codigoTabela = row["TabPvCod"].ToString();
                            compProduto.descProduto = mdlfuncoes.Consulta_CodNome_Produto(compRow["ProdCodEstr"].ToString());
                            compProduto.descricaoProduto = mdlfuncoes.Consulta_CodNome_Produto(compRow["ProdCodEstr"].ToString());
                            compProduto.quantidade = 1;
                            compProduto.revenda = "0";
                            compProduto.unidade = mdlfuncoes.Consulta_Unidade_Medida(compRow["ProdCodEstr"].ToString());
                            compProduto.valorItem = 0;
                            compProduto.valorTabela = 0;

                            this.itemPedidoList[tstCont].incluiItem(compProduto);
                        }
                        compDadosTable.Clear();
                    }
                    */
                    tstCont++;
                }

                dadosTable.Clear();

                //Carrega lista anterior
                this.carregaDadosListaAnterior();
            }

            return erro;
        }

        //Método para buscar dados da entidade do Pedido
        public void consultaEntidade(string CodigoEntidade, out string EntNome, out string EntNomeFant,
            out string EntCpfCgc, out string EntNat, out string EntTranspCod, out string tipoEntidade, out string EntRgIe)
        {
            string strSQL = "";
            DataTable dadosTable = new DataTable();

            EntNome = "";
            EntNomeFant = "";
            EntCpfCgc = "";
            EntNat = "";
            EntTranspCod = "";
            tipoEntidade = "";
            EntRgIe = "";

            try
            {

                strSQL += "select CCU.CodigoUsuario, CC.IDCliente, CC.CodigoCLienteSAP, CC.NomeCliente, CC.NomeFantasia, CIF.CNPJ, ";
                strSQL += "CC.IDNatureza, CNJ.Nome, CIF.InscricaoEstadual ";
                strSQL += "from CRM_CLIENTE CC ";
                strSQL += "INNER JOIN CRM_VENDEDOR CV ON CC.IDVendedor = CV.IDVendedor ";
                strSQL += "LEFT JOIN CRM_CLIENTE_ENDERECO CCE ON CCE.IDCliente = CC.IDCliente and CCE.DescricaoEndereco = 'ENTREGA' ";
                strSQL += "LEFT JOIN CRM_IDENTIFICACAO_FISCAL CIF ON CIF.IDCliente = CC.IDCliente and CIF.IDEndereco=CCE.IDEndereco ";
                strSQL += "LEFT JOIN CRM_NATUREZA_JURIDICA CNJ ON CNJ.IDNatureza = CC.IDNatureza ";
                strSQL += "LEFT JOIN CRM_CADASTRO_USUARIO CCU ON CV.IDUsuario=CCU.IDUsuario ";
                strSQL += "WHERE CC.IDCliente = '" + CodigoEntidade.ToString() + "'";

                this.conexao();
                dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "consultaEntidade pedido.cs");

                if (dadosTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dadosTable.Rows)
                    {
                        EntNome = row["NomeCliente"].ToString();
                        EntNomeFant = row["NomeFantasia"].ToString();
                        EntCpfCgc = row["CNPJ"].ToString();
                        EntNat = row["Nome"].ToString();
                        //SAP Não possui transportadora padrão
                        EntTranspCod = "";
                        tipoEntidade = "";
                        EntRgIe = row["InscricaoEstadual"].ToString();

                        this.ENTVALLIMCRED = "";
                        this.ENTDESDEDATA = "";
                        this.EntDataCad = "";

                        this.VendCadastrado = row["CodigoUsuario"].ToString() ?? "Nenhum Vendedor";

                        if (this.VendCadastrado == "")
                        {
                            this.VendCadastrado = "Nenhum Vendedor";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string AnaliDados = "";
                AnaliDados += "|@strSQL:" + strSQL;
                AnaliDados += "|JSONPedido:" + LogAuditoria.ClassesAuditoria.LogErroClass.jsonconv.ConverteObjectParaJSon<pedido>(this);
                LogAuditoria.ClassesAuditoria.LogErroClass.GravaLOGErroStatic(0, "consultaEntidade", ex, AnaliDados);
            }
        }

        //Método para buscar descrição da Empresa
        public string consultaDescrEmpresa(string CodigoEmpresa, string usuario)
        {
            DataTable dadosTable = new DataTable();

            string strSQL = "";
            string descricaoEmpresa = "";

            try
            {

                strSQL = "select NomeEmpresa from CRM_EMPRESA_FILIAL where IDEmpresa='" + CodigoEmpresa + "'";

                this.conexao();
                dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "consultaDescrEmpresa pedido.cs");

                if (dadosTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dadosTable.Rows)
                    {
                        descricaoEmpresa = row["NomeEmpresa"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                string AnaliDados = "";
                AnaliDados += "|@strSQL:" + strSQL;
                AnaliDados += "|JSONPedido:" + LogAuditoria.ClassesAuditoria.LogErroClass.jsonconv.ConverteObjectParaJSon<pedido>(this);
                LogAuditoria.ClassesAuditoria.LogErroClass.GravaLOGErroStatic(0, "consultaDescrEmpresa", ex, AnaliDados);
            }

            return descricaoEmpresa;
        }

        //Método para buscar ICMS devido
        public double consultaICMSDevido(string EmpCod, string PedVendaNum)
        {
            DataTable dadosTable = new DataTable();

            string strSQL = "";
            double Valor = 0;

            strSQL = "select sum(ItPedVendaValICMSDevido) as IcmsDevido from ITEM_PED_VENDA where ";
            strSQL += "PedVendaNum = '" + PedVendaNum + "' and EmpCod='" + EmpCod + "'";

            this.conexao();
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "consultaICMSDevido pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    Valor = (double)Math.Round((decimal)row["IcmsDevido"], 2);
                }
            }

            return Valor;
        }

        //Método para buscar valor diferimento
        public double consultaICMSDiferido(string EmpCod, string PedVendaNum)
        {
            DataTable dadosTable = new DataTable();

            string strSQL = "";
            double Valor = 0;

            strSQL = "select sum(ItPedVendaValDiferimICMS) as IcmsDiferido from ITEM_PED_VENDA where ";
            strSQL += "PedVendaNum = '" + PedVendaNum + "' and EmpCod='" + EmpCod + "'";

            this.conexao();
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "consultaICMSDiferido pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    Valor = (double)Math.Round((decimal)row["IcmsDiferido"], 2);
                }
            }

            return Valor;
        }

        //Método para buscar vendedor
        public string consultaVendedor(string codigoEntidade)
        {

            string strSQL = "";
            DataTable dadosTable = new DataTable();

            try
            {
                strSQL = "select IDVendedor from CRM_CLIENTE where IDCliente='" + codigoEntidade + "'";

                this.conexao();
                dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "consultaVendedor pedido.cs");

                if (dadosTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dadosTable.Rows)
                    {
                        this.VendCod = row["IDVendedor"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                string AnaliDados = "";
                AnaliDados += "|@strSQL:" + strSQL;
                AnaliDados += "|JSONPedido:" + LogAuditoria.ClassesAuditoria.LogErroClass.jsonconv.ConverteObjectParaJSon<pedido>(this);
                LogAuditoria.ClassesAuditoria.LogErroClass.GravaLOGErroStatic(0, "consultaEntidade", ex, AnaliDados);
            }

            return this.VendCod;
        }

        public string gravaComplementoPedido()
        {
            string retErro = "";

            retErro = this.mdlfuncoesBD.gravaComplemento(this.codigoEmpresa, this.numeroPedido, this.embarqueImediato);

            return retErro;
        }

        public string cancelaOrcamento()
        {
            string retErro = "";

            retErro = this.mdlfuncoesBD.alteraSatusPedido(this.codigoEmpresa, this.numeroPedido, this.usuario, this.codigoEntidade, "7", "Cancelado");

            return retErro;
        }

        public string AlteraStatusFaturar()
        {
            string retErro = "";

            retErro = this.mdlfuncoesBD.alteraSatusPedido(this.codigoEmpresa, this.numeroPedido, this.usuario, this.codigoEntidade, "07", "Faturar");

            return retErro;
        }

        //Método para calcular custo do produto para tabela HEXADECIMAL 
        //OBS.: Colocado na Classe pedido devido aos cálculos serem antes de instanciar o objeto item
        public void calculaCustoHexadecimal(double valorHexadecimal, string produto, string unidadeMedida,
                                             out double RetValorUnitario, out double RetValorImpostos, out string RetErro, string tabelaPreco, int IDUsuario
            , int IDClassificacaoComercial)
        {
            DataTable outputTable = new DataTable();
            double valorUnitario = 0;
            double valorImpostos = 0;
            double AliqDiferimento = 0;
            string TribBCod = "";

            string erro = "";
            string[] siglasEmpresa = new string[2];
            string[] siglasEntidade = new string[2];

            string IpiInclusoBase = "Não";

            string natureza = this.consultaNatureza();

            if (natureza == "Consumidor Contribuinte" || natureza == "Consumidor")
            {
                IpiInclusoBase = "Sim";
                this.IPIInclusoICMS = IpiInclusoBase;
            }

            //siglasEmpresa = this.buscaSiglaEmpresa();
            siglasEntidade = this.buscaSiglaEntidade();

            /*
            this.ClasFiscal = this.buscaInfoProduto(produto);
            buscaCFOPDiferimento(this.codigoEntidade);

            if (NatOperacao == "" || NatOperacao == null)
            {
                NatOperacao = "5.102";
            }

            if (NatOperacao == "5.101.011")
            {
                this.ClasFiscal = "0000258";
            }
            */

            //this.mdlfuncoesBD.recuperaValorHexadecimal(this.codigoEmpresa, this.codigoEntidade, produto, unidadeMedida, this.ClasFiscal, this.NatOperacao, this.especie, this.operacao, natureza, siglasEmpresa[0], siglasEntidade[0], siglasEmpresa[1], siglasEntidade[1], valorHexadecimal, IpiInclusoBase, this.dataEmissao, out valorUnitario, out valorImpostos, out erro, out TribBCod, out AliqDiferimento, tabelaPreco);
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_SIMULACAO_PRECO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.VarChar, 10, "Empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Estado", SqlDbType.VarChar, 20, "Estado"));
                    dbCommand.Parameters.Add(new SqlParameter("@Produto", SqlDbType.VarChar, 30, "Produto"));
                    dbCommand.Parameters.Add(new SqlParameter("@LocalFaturamento", SqlDbType.VarChar, 200, "LocalFaturamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@NivelVendedor", SqlDbType.VarChar, 30, "@NivelVendedor"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrecoExIcm", SqlDbType.Decimal, 0, "PrecoExIcm"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDClassificacaoComercial", SqlDbType.Int, 0, "IDClassificacaoComercial"));

                    dbCommand.Parameters["@Empresa"].Value = this.codigoEmpresa;
                    dbCommand.Parameters["@Estado"].Value = siglasEntidade[1].ToString();
                    dbCommand.Parameters["@Produto"].Value = produto;
                    dbCommand.Parameters["@LocalFaturamento"].Value = "";
                    dbCommand.Parameters["@NivelVendedor"].Value = "Representante";
                    dbCommand.Parameters["@PrecoExIcm"].Value = valorHexadecimal;
                    dbCommand.Parameters["@IDUsuario"].Value = IDUsuario;
                    dbCommand.Parameters["@IDClassificacaoComercial"].Value = IDClassificacaoComercial;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            valorImpostos = Convert.ToDouble(row["PrecoBase"]);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }




            RetValorUnitario = 0;
            RetValorImpostos = valorImpostos;
            //this.Tributacao = TribBCod;
            //this.Diferimento = AliqDiferimento;
            RetErro = erro;
        }

        //Retorna natureza Pedido conforme regra
        public string consultaNatureza()
        {
            string retNatureza = "";

            if (this.consumo == "Sim")
            {
                if (this.EntRgIe == "")
                {
                    retNatureza = "Consumidor";
                }
                else
                {
                    retNatureza = "Consumidor Contribuinte";
                }
            }
            else
            {
                retNatureza = this.natureza;
            }

            return retNatureza;
        }

        //Carrega dados lista anterior pedido
        public void carregaDadosListaAnterior()
        {
            if (this.itemPedidoList != null)
            {
                this.itemPedidoListAnterior = new List<itemPedido>(this.itemPedidoList);
            }
        }

        //Metodo para buscar numero sequencial
        public int buscaSequencial()
        {
            int numeroSequencial = 0;
            int quant = 0;
            int cont = 0;

            //Verifica se lista de pedido contem itens
            if (this.itemPedidoList != null)
            {
                quant = this.numeroItens();
            }

            //Se lista contem itens busca o sequencial
            if (quant != 0)
            {
                while (cont < quant && quant > 0)
                {
                    //Verifica se o sequencial anterior é maior que o atual, busca o maior sequencial
                    if (this.itemPedidoList[cont].numSeq > numeroSequencial)
                    {
                        numeroSequencial = this.itemPedidoList[cont].numSeq;
                    }

                    cont++;
                }
            }
            else
            {
                numeroSequencial = 0;
            }

            //Incrementa sequencial
            numeroSequencial = numeroSequencial + 1;

            //retorna sequencial
            return numeroSequencial;
        }

        public void RetornaPosicaoUnidadeMedida(string produto, out int Posicao)
        {
            Posicao = this.mdlfuncoesBD.BuscaPosicaoUnidadeMedida(produto);
        }

        public string verificaPeriodoPeriodo()
        {
            string retorno = "";


            return retorno;
        }

        public string EnviaPedidoSAP()
        {
            string pattern = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\s]";
            string replacement = "";
            Regex rgx = new Regex(pattern);

            string erro = "";
            string retorno = "";
            string JSONPedido = "";
            WSClassePedidoInclusao OBJPedidoInclusao = new WSClassePedidoInclusao();
            //WSClassePedidoInclusaoRetorno OBJRetorno = new WSClassePedidoInclusaoRetorno();
            WSRetornoClass OBJRetorno = new WSRetornoClass();
            //ServicoComunicacaoSAP.ComunicacaoSAPSoapClient WSComunicacaoSAP = new ServicoComunicacaoSAP.ComunicacaoSAPSoapClient();

            try
            {
                buscaEmpresaInformacoesSAP();

                //this.NumeroEsbocoSAP = "32";

                OBJPedidoInclusao.cod_esboco = this.NumeroEsbocoSAP;
                //OBJPedidoInclusao.DocObjectCode = "17";
                OBJPedidoInclusao.cod_cliente = this.CodigoClienteSAP;
                OBJPedidoInclusao.BPL_IDAssignedToInvoice = Convert.ToInt32(this.CodigoEmpresaSAP);
                OBJPedidoInclusao.data_entrega = Convert.ToDateTime(this.dataEntrega);
                OBJPedidoInclusao.cod_vendedor = Convert.ToInt32(this.CodigoVendedorSAP);
                OBJPedidoInclusao.cond_pag = Convert.ToInt32(this.CodigoCondicaoPagamentoSAP);
                OBJPedidoInclusao.data_lancamento = Convert.ToDateTime(this.dataEmissao);
                OBJPedidoInclusao.ped_cliente = this.PedVendaNumPedEnt;
                OBJPedidoInclusao.descricao = rgx.Replace(this.historico, replacement);
                OBJPedidoInclusao.num_ref_cliente = this.embarqueImediato;
                OBJPedidoInclusao.crm_cod_pedido = this.IDPedido;
                OBJPedidoInclusao.obs_nf = rgx.Replace(this.observacao, replacement);

                OBJPedidoInclusao.IDEmpresa = Convert.ToInt32(this.codigoEmpresa);
                OBJPedidoInclusao.IDPedido = this.IDPedido;

                OBJPedidoInclusao.incluiTransportadora(this.tipoFrete, this.transportadora);
                OBJPedidoInclusao.incluiDespesas("9", this.valorFrete.ToString().Replace(',', '.'));

                foreach (itemPedido PedidoItem in itemPedidoList)
                {
                    PedidoItem.RecuperaDadosItensSAP(this.IDPedido, Convert.ToInt32(this.codigoEmpresa));

                    OBJPedidoInclusao.incluiItens(PedidoItem.codigoProduto, Convert.ToDecimal(PedidoItem.quantidade), Convert.ToDecimal(PedidoItem.valorItem), PedidoItem.CodigoUtilizacaoSAP, PedidoItem.CodigoUnidadeMedidaSAP, PedidoItem.NomeUnidadeMedidaSAP, PedidoItem.ComplementoNomeProduto, PedidoItem.CodigoNaturezaDestinacaoSAP, PedidoItem.CodigoProdutoSAPCliche, PedidoItem.CodigoProdutoSAPArruela,
                        PedidoItem.xPed, PedidoItem.nItem, PedidoItem.CodigoDepositoSAP);
                }

                //Transforma em JSON para enviar para o WEB SERVICE
                //JSONPedido = jsonconv.ConverteObjectParaJSon<WSClassePedidoInclusao>(OBJPedidoInclusao);
                JSONPedido = JsonConvert.SerializeObject(OBJPedidoInclusao);


                //IF Criado para atualização de pedidos, porém não esta sendo usado
                //por enquanto
                if (this.NumeroEsbocoSAP == "" || this.NumeroEsbocoSAP == null || this.NumeroEsbocoSAP == "0")
                {
                    retorno = OBJApi.InclusaoEsbocoPedidoAPI(JSONPedido);
                    OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);
                    erro = OBJRetorno.MsgRetorno;

                    //retorno = WSComunicacaoSAP.Insere_Pedido_SAP(JSONPedido);
                    //OBJRetorno = jsonconv.ConverteJSonParaObject<WSClassePedidoInclusaoRetorno>(retorno);
                    //if (OBJRetorno.resultPositivo == "true")
                    //{
                    //    this.NumeroEsbocoSAP = OBJRetorno.Codigo;
                    //    AtualizaNumeroEsbocoSAP();
                    //}
                }
                else
                {
                    retorno = OBJApi.InclusaoEsbocoPedidoAPI(JSONPedido);
                    OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);
                    erro = OBJRetorno.MsgRetorno;

                    //retorno = WSComunicacaoSAP.Insere_Pedido_SAP(JSONPedido);
                    //OBJRetorno = jsonconv.ConverteJSonParaObject<WSClassePedidoInclusaoRetorno>(retorno);
                    //if (OBJRetorno.resultPositivo == "true")
                    //{
                    //    this.NumeroEsbocoSAP = OBJRetorno.Codigo;
                    //    AtualizaNumeroEsbocoSAP();
                    //}
                }
            }
            catch (Exception ex)
            {
                erro = "Erro ao enviar dados para o SAP.";
            }


            return erro;
        }

        public void buscaEmpresaInformacoesSAP()
        {
            DataTable outputTable = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PEDIDOS_DADOS_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));

                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.codigoEmpresa;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.CodigoEmpresaSAP = row["CodigoEmpresaSAP"].ToString();
                                this.CodigoVendedorSAP = row["CodigoVendedorSAP"].ToString();
                                this.CodigoCondicaoPagamentoSAP = row["CodigoCondicaoPagamentoSAP"].ToString();
                                this.CodigoClienteSAP = row["CodigoClienteSAP"].ToString();
                                this.NumeroPedidoSAP = row["NumeroPedidoSAP"].ToString();
                                this.NumeroEsbocoSAP = row["NumeroEsbocoSAP"].ToString();
                                this.IDStatus = Convert.ToInt32(row["IDStatus"].ToString() ?? "1");
                                this.observacao = row["ObservacaoNotaFiscal"].ToString();
                                this.historico = row["ObservacaoPedido"].ToString();
                                this.dataEntrega = row["DataEntrega"].ToString();
                                this.dataEmissao = row["DataLancamento"].ToString();
                                this.AprovadoProducaoCliche = Convert.ToInt16(row["LiberadoProducao"]).ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public string AtualizaNumeroEsbocoSAP()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_PEDIDO_VENDA_DADOS_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroEsbocoSAP", SqlDbType.Int, 0, "NumeroEsbocoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.codigoEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@NumeroEsbocoSAP"].Value = this.NumeroEsbocoSAP;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = 0;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                    this.IDPedido = Convert.ToInt32(dbCommand.Parameters["@IDPedido"].Value);
                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção do pedido";
                }


                return erro;
            }
        }

        public string TransformaEsbocoPedido()
        {
            string erro = "";
            string retorno = "";
            string JSONPedido = "";
            WSClassePedidoInclusaoTransformaPV OBJPedido = new WSClassePedidoInclusaoTransformaPV();
            //WSClassePedidoInclusaoRetorno OBJRetorno = new WSClassePedidoInclusaoRetorno();
            WSRetornoClass OBJRetorno = new WSRetornoClass();
            WSClassePedidoAtualizacaoAprovacao OBJPedidoAtualizacao = new WSClassePedidoAtualizacaoAprovacao();

            buscaEmpresaInformacoesSAP();

            //Se Status Aprovado efetiva o pedido no SAP
            if (this.IDStatus == 3)
            {
                OBJPedidoAtualizacao.cod_esboco = this.NumeroEsbocoSAP;
                OBJPedidoAtualizacao.data_entrega = Convert.ToDateTime(this.dataEntrega);
                OBJPedidoAtualizacao.data_lancamento = Convert.ToDateTime(this.dataEmissao);
                OBJPedidoAtualizacao.IDEmpresa = Convert.ToInt32(this.codigoEmpresa);
                OBJPedidoAtualizacao.IDPedido = this.IDPedido;
                OBJPedidoAtualizacao.LiberadoProducaoClicheCRM = this.AprovadoProducaoCliche;

                JSONPedido = JsonConvert.SerializeObject(OBJPedidoAtualizacao);

                retorno = OBJApi.PedidoTransformaEsbocoPedidoAPI(JSONPedido);
                OBJRetorno = jsonconv.ConverteJSonParaObject<WSRetornoClass>(retorno);
                erro = OBJRetorno.MsgRetorno;

                //Transforma em JSON para enviar para o WEB SERVICE
                //JSONPedido = jsonconv.ConverteObjectParaJSon<WSClassePedidoAtualizacaoAprovacao>(OBJPedidoAtualizacao);
                //retorno = WSComunicacaoSAP.Atualiza_Pedido_SAP(JSONPedido);
                //OBJRetorno = jsonconv.ConverteJSonParaObject<WSClassePedidoInclusaoRetorno>(retorno);
                //if (OBJRetorno.resultPositivo == "true")
                //{
                //Zera dados para utilizar novamente
                //retorno = "";
                //OBJRetorno = null;

                //OBJPedido.DocEntry = this.NumeroEsbocoSAP;

                //Chama WEB Service para tratar regras de aprovação 
                //userDIServer = WSComunicacaoSAP.Valida_Regras_Aprovacao(this.NumeroEsbocoSAP);

                //Transforma em JSON para enviar para o WEB SERVICE
                //JSONPedido = jsonconv.ConverteObjectParaJSon<WSClassePedidoInclusaoTransformaPV>(OBJPedido);

                //retorno = WSComunicacaoSAP.Transforma_Esboco_Pedido_SAP(JSONPedido, userDIServer);
                //OBJRetorno = jsonconv.ConverteJSonParaObject<WSClassePedidoInclusaoRetorno>(retorno);

                //if (OBJRetorno.resultPositivo == "true")
                //{
                //    if (OBJRetorno.lista[0].Tipo == "17")
                //    {
                //        this.NumeroPedidoSAP = OBJRetorno.lista[0].Codigo;
                //        erro = AtualizaNumeroPedidoSAP();
                //    }
                //    else
                //    {
                //        if (OBJRetorno.lista[0].Tipo == "112")
                //        {
                //            this.NumeroEsbocoSAP = OBJRetorno.lista[0].Codigo;
                //            this.IDStatus = 4;
                //            erro = AtualizaSituacaoPedidoSAP();
                //        }
                //    }
                //}
                //}
            }
            return erro;
        }

        public string AtualizaNumeroPedidoSAP()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_PEDIDO_VENDA_DADOS_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroEsbocoSAP", SqlDbType.Int, 0, "NumeroEsbocoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroPedidoSAP", SqlDbType.Int, 0, "NumeroPedidoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.codigoEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@NumeroEsbocoSAP"].Value = 0;
                    dbCommand.Parameters["@NumeroPedidoSAP"].Value = this.NumeroPedidoSAP;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                    this.IDPedido = Convert.ToInt32(dbCommand.Parameters["@IDPedido"].Value);
                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção do pedido";
                }


                return erro;
            }
        }

        public string AtualizaSituacaoPedidoSAP()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_SITUACAO_PEDIDO_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "NumeroEsbocoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.codigoEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                    this.IDPedido = Convert.ToInt32(dbCommand.Parameters["@IDPedido"].Value);
                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção do pedido";
                }


                return erro;
            }
        }

        public string AtualizaHistoricoPedidoSAP()
        {
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_HISTORICO_PEDIDO_VENDA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@ObservacaoPedido", SqlDbType.VarChar, 8000, "ObservacaoPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.codigoEmpresa;
                    dbCommand.Parameters["@IDPedido"].Value = this.IDPedido;

                    /******************Rotina Para Tratar Historico**************************/
                    if (this.historico == "")
                    {
                        this.historico = "Pedido Atualizado.";
                        this.HistoricoAtualizado = "Pedido Atualizado.";
                    }

                    if (this.historicoAntigo != "")
                    {
                        dbCommand.Parameters["@ObservacaoPedido"].Value = this.historicoAntigo + "\n\n" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + ' ' + this.CodigoUsuario + ":" + this.historico;
                        this.HistoricoAtualizado = this.historicoAntigo + "\n\n" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + ' ' + this.CodigoUsuario + ":" + this.historico;
                    }
                    else
                    {
                        dbCommand.Parameters["@ObservacaoPedido"].Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + ' ' + this.CodigoUsuario + ":" + this.historico;
                        this.HistoricoAtualizado = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + ' ' + this.CodigoUsuario + ":" + this.historico;
                    }
                    /***********************************************************************/

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção do histórico do pedido";
                }


                return erro;
            }
        }

        public string CancelaPedidosForaPeriodo()
        {
            string erro = "";
            DataTable outputTable = new DataTable();
            enviarEmail OBJMail = new enviarEmail();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_PEDIDOS_FORA_PERIODO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            FinanceiroClass OBJFinanceiro = new FinanceiroClass();

                            foreach (DataRow row in outputTable.Rows)
                            {
                                if (erro == "")
                                {
                                    //Recupera dados do pedido CRM
                                    this.codigoEmpresa = row["IDEmpresa"].ToString();
                                    this.IDPedido = Convert.ToInt32(row["IDPedido"]);
                                    carregaDadosPedido(this.codigoEmpresa, this.IDPedido.ToString());

                                    //Cancela esboço no SAP
                                    OBJFinanceiro.NumeroEsbocoSAP = row["NumeroEsbocoSAP"].ToString();
                                    OBJFinanceiro.UsuarioAprovacao = row["CodigoUsuarioSAP"].ToString();
                                    OBJFinanceiro.UsuarioAprovacaoSenha = row["SenhaUsuarioSAP"].ToString();
                                    OBJFinanceiro.AnalisePedido = "Recusado";
                                    OBJFinanceiro.Historico = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " - Cancelado automático, tempo limite atingido.";
                                    erro = OBJFinanceiro.AtualizaAnalisarEsboco();

                                    //Atualiza situação pedido SAP
                                    if (erro == "")
                                    {
                                        this.IDStatus = 7;
                                        erro = this.AtualizaSituacaoPedidoSAP();
                                    }

                                    //Atualiza histórico CRM
                                    if (erro == "")
                                    {
                                        this.CodigoUsuario = "Sistema";
                                        this.historico = "Pedido cancelado devido a tempo limite em aberto excedido.";
                                        erro = AtualizaHistoricoPedidoSAP();
                                    }

                                    //Dispara e-mail para o vendedor informando cancelamento automatico
                                    if (erro == "")
                                    {
                                        OBJMail.CodigoEmpresa = row["NomeEmpresa"].ToString();
                                        OBJMail.NumeroPedidoCRM = this.IDPedido.ToString();
                                        OBJMail.NomeCliente = row["NomeCliente"].ToString();
                                        OBJMail.DataAlteracao = DateTime.Today.ToString("dd/MM/yyyy");
                                        OBJMail.Situacao = "Cancelado Automático";
                                        OBJMail.Status = "Cancelado Automático.";
                                        OBJMail.Historico = this.historico;
                                        OBJMail.TituloEmail = "Cancelamento Automatico Pedido " + this.IDPedido.ToString() + ".";
                                        OBJMail.UsuarioCRM = "Sistema.";
                                        OBJMail.FormataTexto();

                                        //OBJMail.RecuperaEmailDestinatario();
                                        OBJMail.EmailDestinatario = OBJMail.RecuperaEmailAlteracaoFinanceiro();
                                        //OBJMail.EmailDestinatario = "luiz.carlos@manulifitasa.com.br";

                                        //OBJMail.enviaEmailFormatado();
                                        OBJMail.enviaEmailFormatadoAnexo();
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                erro = "Erro ao efetuar cancelamentos.";
            }

            return erro;
        }

        public string AtualizarHistoricoPedidoSAPAPI()
        {
            string erro = "";
            string JSONPedido = "";
            WSPedidoClass OBJPedidoWS = new WSPedidoClass();

            OBJPedidoWS.IDEmpresa = Convert.ToInt32(codigoEmpresa);
            OBJPedidoWS.IDPedido = IDPedido;

            //Transforma em JSON para enviar para o WEB SERVICE
            JSONPedido = jsonconv.ConverteObjectParaJSon<WSPedidoClass>(OBJPedidoWS);

            erro = OBJApi.AtualizarHistoricoPedidoSAPAPI(JSONPedido);

            return erro;
        }

    }
}
