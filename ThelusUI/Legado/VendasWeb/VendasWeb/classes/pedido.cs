using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb
{
    public class pedido
    {
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
        public string  ENTVALLIMCRED {get;set;}
        public string  ENTDESDEDATA {get;set;}
        public string EntDataCad { get; set; }
        public string VendCadastrado { get; set; }
        public string PedVendaNumPedEnt { get; set; }
        public string ClasFiscal { get; set; }
        public string TribACod { get; set; }
        public string Tributacao { get; set; }
        public string NatOperacao { get; set; }
        public string IPIInclusoICMS { get; set; }

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
                itemProduto.valorTabela, itemProduto.valorItem, itemProduto.unidade, itemProduto.quantidade, this.codigoEmpresa, itemProduto.descricaoProduto, itemProduto.CompdescricaoProduto, itemProduto.numSeq, itemProduto.ItPedVendaNumSeq, itemProduto.valorOriginal);

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
            string[] retDados=new string[2];
            string retErro = "";
            this.tipoVendaCod = "0000002";
            this.valorTotal = 0;

            if (this.tipoOperacao == "inclusao")
            {
                this.statusPedio = "13";
                this.descricaoStatus = "Orçamento";
            }    
            
            this.quantidadeTotal = 0;
                        
            //Declara objeto conexao
            this.conexao();
            
            //Carrega a regiao do cliente
            this.regiao = this.buscaRegiao();
            
            //Busca Sigla Pais e UF da Empresa
            retDados = this.buscaSiglaEmpresa();
            this.empresaPaisSigla = retDados[0];
            this.empresaPaisUfSigla = retDados[1];
            
            //Busca Sigla Pais e UF da entidade
            retDados = this.buscaSiglaEntidade();
            this.entidadePaisSigla = retDados[0];
            this.entidadeUfSigla = retDados[1];
            

            //Busca nome condicao de pagamento
            this.nomeCondicao = this.buscaNomeCondicao();
            
            this.tipo = this.buscaTipoPedido();
            
            //Busca numero de pedido
            if (this.numeroPedido == null || this.numeroPedido == "0")
            {
                this.numeroPedido = this.buscaNumeroPedido();
            }

            //Busca tabela principal
            this.tabelaPrincipal = this.buscaTabela();

            //Grava Pedido no banco
            retErro =                                       
                this.mdlfuncoesBD.gravaPedido(this.codigoEmpresa, this.numeroPedido, this.tipo, this.dataEmissao,
                this.dataEntrega, this.codigoEntidade, this.tipoVendaCod, this.regiao, this.buscaNome(), this.usuario,
                this.observacao, this.historico, this.tipoFrete, this.transportadora, this.consultaNatureza(), this.tabelaPrincipal,
                this.operacao, this.especie, this.tipoOperacao, this.statusPedio, this.descricaoStatus, this.QuantidadeVolumes,
                this.EspecieVolume, this.PesoLiquido, this.PesoBruto, this.PedVendaNumPedEnt);

            if (retErro == "")
            {
                retErro = gravaComplementoPedido();
            }

            return retErro;
        }

        public string gravaItemPedido() 
        {
            string retErro = "";
            int quant = 0;
            int cont = 0;
            int numseq = 0;
            int achou = 0;
            float auxValorFrete =(float)Math.Round(this.valorFrete, 2);

            if (this.itemPedidoList != null)
            {
                quant = this.numeroItens();
            }

            while (cont < quant && quant > 0 && retErro =="")
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
                    retErro = this.itemPedidoList[cont].gravaItemPedido(this.codigoEmpresa, this.numeroPedido, this.vendedor,
                        this.codigoEntidade, this.consultaNatureza(), this.empresaPaisSigla, this.empresaPaisUfSigla, this.entidadePaisSigla,
                        this.entidadeUfSigla, this.condicao, this.operacao, this.especie, numseq, this.usuario, auxValorFrete, this.dataEmissao,
                        this.tipoOperacao, this.PedVendaNumPedEnt, this.IPIInclusoICMS);
                }
                else 
                {
                    //Chama funcao para grava item
                    retErro = this.itemPedidoList[cont].gravaItemPedido(this.codigoEmpresa, this.numeroPedido, this.vendedor,
                        this.codigoEntidade, this.consultaNatureza(), this.empresaPaisSigla, this.empresaPaisUfSigla, this.entidadePaisSigla,
                        this.entidadeUfSigla, this.condicao, this.operacao, this.especie, numseq, this.usuario, auxValorFrete,
                        this.dataEmissao, "inclusao", this.PedVendaNumPedEnt, this.IPIInclusoICMS);
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

            while (cont < quant && quant > 0 && erro=="")
            {
                achou = 0;

                if(this.itemPedidoList.Find(item => (item.codigoProduto == this.itemPedidoListAnterior[cont].codigoProduto && item.numSeq==this.itemPedidoListAnterior[cont].numSeq)) != null)
                {
                    achou = 1;                        
                }

                if (achou == 0) {
                    erro = this.mdlfuncoesBD.excluiItens(this.codigoEmpresa, this.numeroPedido, this.itemPedidoListAnterior[cont].codigoProduto, this.itemPedidoListAnterior[cont].numSeq);  
                }

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
            string[] retDados= new string[2];

            //Recupera Sigla Pais e UF da Entidae
            strSQL = "select cid.PaisSigla PaisSigla, cid.UfSigla UfSigla from ENTIDADE ent, CIDADE cid where EntCod='" + this.codigoEntidade.ToString() + "' and cid.CidCod=ent.CidCod";

            this.conexao();
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "buscaSiglaEntidade - pedido.cs");

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
        public string buscaNomeCondicao() {

            DataTable dadosTable = new DataTable();

            string strSQL = "";
            string retDados = "";

            //Recupera condicao pagamento
            strSQL = "select CondPagNome from COND_PAG where CondPagCod='" + this.condicao.ToString() + "'";

            this.conexao();
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "buscaNomeCondicao - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    retDados = (string)Convert.ToString(row["CondPagNome"]);
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
            string retNumero ="";
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
        public void carregaDadosPedido(string empresa, string numPedido)
        {
            string strSQL = "";
            DataTable dadosTable = new DataTable();
            DataTable compDadosTable = new DataTable();
            DataTable BloqueadosDadosTable = new DataTable();

            strSQL = "select coalesce(PedVendaTranspEntCod, EntCod) as PedVendaTranspEntCod, coalesce(PedVendaStatFrete, '') as PedVendaStatFrete, PedVendaEntNat, PedVendaTipo, PedVendaStatDescr, EntCod, convert(varchar(10),PedVendaData,103) as dataEmissao, convert(varchar(10),PedVendaDataEntrega,103) as dataEntrega, PedVendaValIcms, PedVendaValIpiCalc, PedVendaValTotal, PedVendaValMerc, TipoVendaCod, RegCodEstr, PedVendaStatFrete, PedVendaValFrete, TabPVCod, StatPedVendaCod, PedVendaValMerc, PedVendaValIpiCalc, PedVendaValIcms, PedVendaValTotal, PedVendaStatDescr, PedVendaQtdVol, PedVendaPesoLiq, PedVendaPesoBruto, PedVendaEspecVol,isnull(PedVendaNumPedEnt,'') as PedVendaNumPedEnt from PED_VENDA where PedVendaNum='" + numPedido + "' and EmpCod='" + empresa + "'";

            //Instancia objetos de consulta
            this.conexao();

            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    this.codigoEmpresa = empresa.ToString();
                    this.numeroPedido = numPedido.ToString();
                    this.codigoEntidade = (string)row["EntCod"].ToString();
                    this.tipoVendaCod = (string)row["TipoVendaCod"].ToString();
                    this.tipo = (string)row["PedVendaTipo"].ToString();
                    this.dataEmissao = (string)row["dataEmissao"].ToString();
                    this.dataEntrega = (string)row["dataEntrega"].ToString();

                    if ((string)row["PedVendaEntNat"].ToString() == "Consumidor" || (string)row["PedVendaEntNat"].ToString() == "Consumidor Contribuinte")
                    {
                        this.consumo = "Sim";
                    }
                    else 
                    {
                        this.consumo = "Nao";
                    }

                    this.regiao = (string)row["RegCodEstr"].ToString();
                    this.tipoFrete = (string)row["PedVendaStatFrete"].ToString();
                    this.valorFrete = (float)Convert.ToDecimal(row["PedVendaValFrete"]);
                    this.transportadora = (string)row["PedVendaTranspEntCod"].ToString();
                    this.tabelaPrincipal = (string)row["TabPVCod"].ToString();
                    this.statusPedio = (string)row["StatPedVendaCod"].ToString();
                    this.descricaoStatus = (string)row["PedVendaStatDescr"].ToString();
                    this.valorMercadoria = (float)Convert.ToDecimal(row["PedVendaValMerc"]);
                    this.valorIPI = (float)Convert.ToDecimal(row["PedVendaValIpiCalc"]);
                    this.valorICMS = (float)Convert.ToDecimal(row["PedVendaValIcms"]);
                    this.valorTotal = (float)Convert.ToDecimal(row["PedVendaValTotal"]);
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

                    this.PedVendaNumPedEnt = (string)row["PedVendaNumPedEnt"].ToString();
                }
            }

            //Limpa dados DataTable
            dadosTable.Clear();

            //Dados Complementares do Pedido
            strSQL = "select EmbarqueImediato from User_tb_ped_venda_complemento where PedVendaNum='" + this.numeroPedido + "' and EmpCod='" + this.codigoEmpresa + "'";

            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    this.embarqueImediato = (string)row["EmbarqueImediato"].ToString();
                }
            }
            dadosTable.Clear();



            strSQL = "select EntNome from ENTIDADE where EntCod ='" + this.transportadora.ToString() + "'";

            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    this.descricaoTransportadora = (string)row["EntNome"].ToString();
                }
            }
            dadosTable.Clear();
            
            strSQL = "select PedVendaOperacao, PedVendaEspecie from PED_VENDA1 where PedVendaNum='" + numeroPedido + "' and EmpCod='" + empresa + "'";
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    this.operacao = (string)row["PedVendaOperacao"].ToString();
                    this.especie = (string)row["PedVendaEspecie"].ToString();
                }
            }
            dadosTable.Clear();

            strSQL = "select coalesce(CondPagPedVendaNome, '') as CondPagPedVendaNome, CondPagCod from COND_PAG_PED_VENDA where PedVendaNum='" + numeroPedido + "' and EmpCod='" + empresa + "'";
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    this.condicao = (string)row["CondPagCod"].ToString();
                    this.nomeCondicao = (string)row["CondPagPedVendaNome"].ToString();
                }
            }
            dadosTable.Clear();

            strSQL = "select PedVendaTexto, PedVendaTextoHist from TEXTO_PED_VENDA where PedVendaNum='" + numeroPedido + "' and EmpCod='" + empresa + "'";
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    this.observacao = (string)row["PedVendaTexto"].ToString().Replace("#", "").Replace("&", "");
                    this.historicoAntigo = (string)row["PedVendaTextoHist"].ToString().Replace("#", "").Replace("&", "");
                }
            }
            dadosTable.Clear();

            strSQL = "select VendCod from VEND_PED_VENDA where EmpCod='" + empresa + "' and PedVendaNum='"+ numeroPedido +"'";
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    this.vendedor = (string)row["VendCod"].ToString();
                }
            }
            dadosTable.Clear();

            strSQL = "select tabpvcod, tabpvnome from crk_WebRep_TabPreco_Vendedor('" + this.vendedor.ToString() + "','" + this.codigoEmpresa.ToString() + "')";
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

            int tstCont = 0;
            this.tabela = "";

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    if (tstCont == 0)
                    {
                        this.tabela += "('" + row["tabpvcod"].ToString() + "'";
                        tstCont++;
                    }
                    else
                    {
                        this.tabela += ", '" + row["tabpvcod"].ToString() + "'";
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
            if (this.empresaPaisSigla == null)
            {
                retDados = this.buscaSiglaEmpresa();
                this.empresaPaisSigla = retDados[0];
                this.empresaPaisUfSigla = retDados[1];
            }

            //Busca Sigla Pais e UF da entidade
            if (this.entidadePaisSigla == null)
            {
                retDados = this.buscaSiglaEntidade();
                this.entidadePaisSigla = retDados[0];
                this.entidadeUfSigla = retDados[1];
            }

            strSQL = "select ProdCodEstr, ItPedVendaTexto, ItPedVendaUnidMedCod, ItPedVendaQtd, TabPvCod, ItPedVendaValUnit, ItPedVendaValTabPV, ItPedVendaSeq,ItPedVendaNumSeq from ITEM_PED_VENDA WHERE EmpCod='" + empresa.ToString() + "' and PedVendaNum='" + numPedido.ToString() + "'";
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "carregaDadosPedido - pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                produto novoProduto = new produto();
                produto compProduto = new produto();
                
                tstCont = 0;

                foreach (DataRow row in dadosTable.Rows)
                {
                    novoProduto.codigoProduto = row["ProdCodEstr"].ToString();
                    novoProduto.codigoTabela = row["TabPvCod"].ToString();
                    novoProduto.descProduto = row["ItPedVendaTexto"].ToString();
                    novoProduto.descricaoProduto = row["ItPedVendaTexto"].ToString();
                    novoProduto.quantidade = (double)Convert.ToDouble(row["ItPedVendaQtd"]);
                    novoProduto.revenda = "0";
                    novoProduto.unidade = row["ItPedVendaUnidMedCod"].ToString();
                    novoProduto.valorItem = (float)Convert.ToDecimal(row["ItPedVendaValUnit"]);
                    novoProduto.valorTabela = (float)Convert.ToDecimal(row["ItPedVendaValTabPV"]);
                    novoProduto.numSeq = (int)Convert.ToInt16(row["ItPedVendaSeq"]);
                    novoProduto.ItPedVendaNumSeq = (int)Convert.ToInt16(row["ItPedVendaNumSeq"]);

                    //Carrega valor tabela original
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


                    this.incluiItem(novoProduto);

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

                    tstCont++;
                }

                dadosTable.Clear();

                //Carrega lista anterior
                this.carregaDadosListaAnterior();
            }
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

            strSQL += "select ve.UsuCod,E.EntCod, E.EntNome, E.EntNomeFant, E.EntCpfCgc, E.EntNat, E.EntTranspCod, E.EntTipoFJ, E.EntRgIe, E.ENTVALLIMCRED,CONVERT(nvarchar(10), E.ENTDESDEDATA, 103) as ENTDESDEDATA,CONVERT(nvarchar(10), E.EntDataCad, 103) as EntDataCad from ENTIDADE E";
            strSQL += " left join VEND_ENT vend ON vend.EntCod = E.EntCod ";
            strSQL += " left join VENDEDOR ve ON ve.VendCod = vend.VendCod ";
            strSQL += " where E.EntCod =" + CodigoEntidade.ToString() + ";";

            this.conexao();
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "consultaEntidade pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    EntNome = row["EntNome"].ToString();
                    EntNomeFant = row["EntNomeFant"].ToString();
                    EntCpfCgc = row["EntCpfCgc"].ToString();
                    EntNat = row["EntNat"].ToString();
                    EntTranspCod = row["EntTranspCod"].ToString();
                    tipoEntidade = row["EntTipoFJ"].ToString();
                    EntRgIe = row["EntRgIe"].ToString();

                    this.ENTVALLIMCRED = row["ENTVALLIMCRED"].ToString();
                    this.ENTDESDEDATA = row["ENTDESDEDATA"].ToString();
                    this.EntDataCad = row["EntDataCad"].ToString();
                    
                    this.VendCadastrado = row["UsuCod"].ToString();
                }
            }
        }

        //Método para buscar descrição da Empresa
        public string consultaDescrEmpresa(string CodigoEmpresa, string usuario)
        {
            DataTable dadosTable = new DataTable();

            string strSQL = "";
            string descricaoEmpresa="";

            strSQL = "select EU.EmpCod, EU.EmpCod +' - '+EF.EmpNomeFant as EmpNome from EMP_FIL_USUARIO EU, EMPRESA_FILIAL EF where ";
            strSQL += "EU.EmpCod=EF.EmpCod and UsuCod = '" + usuario + "' and EF.EmpCod='" + CodigoEmpresa + "'";

            this.conexao();
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "consultaDescrEmpresa pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    descricaoEmpresa = row["EmpNome"].ToString();
                }
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
            string vendCod = "";
            DataTable dadosTable = new DataTable();
            strSQL="select VendCod from VEND_ENT where EntCod='" + codigoEntidade+ "'";

            this.conexao();
            dadosTable = this.mdlfuncoes.Executa_DataTable(strSQL, "consultaVendedor pedido.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    vendCod = row["VendCod"].ToString();
                }
            }

            return vendCod;
        }

        public string gravaComplementoPedido() {
            string retErro = "";

            retErro = this.mdlfuncoesBD.gravaComplemento(this.codigoEmpresa, this.numeroPedido, this.embarqueImediato);

            return retErro;
        }

        public string cancelaOrcamento()
        {
            string retErro = "";

            retErro = this.mdlfuncoesBD.alteraSatusPedido(this.codigoEmpresa, this.numeroPedido, this.usuario, this.codigoEntidade, "13", "Cancelado");
            
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
                                             out double RetValorUnitario, out double RetValorImpostos, out string RetErro, string tabelaPreco)
        {
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

            siglasEmpresa = this.buscaSiglaEmpresa();
            siglasEntidade = this.buscaSiglaEntidade();

            this.ClasFiscal = this.buscaInfoProduto(produto);
            buscaCFOPDiferimento(this.codigoEntidade);

            if (NatOperacao == "" || NatOperacao == null)
            {
                NatOperacao = "5.101";
            }

            if (NatOperacao == "5.101.011")
            {
                this.ClasFiscal = "0000258";
            }

            this.mdlfuncoesBD.recuperaValorHexadecimal(this.codigoEmpresa, this.codigoEntidade, produto, unidadeMedida, this.ClasFiscal, this.NatOperacao, this.especie, this.operacao, natureza, siglasEmpresa[0], siglasEntidade[0], siglasEmpresa[1], siglasEntidade[1], valorHexadecimal, IpiInclusoBase, this.dataEmissao, out valorUnitario, out valorImpostos, out erro, out TribBCod, out AliqDiferimento, tabelaPreco);

            RetValorUnitario = valorUnitario;
            RetValorImpostos = valorImpostos;
            this.Tributacao = TribBCod;
            this.Diferimento = AliqDiferimento;
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
                         cont++;
                     }
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
    }
} 