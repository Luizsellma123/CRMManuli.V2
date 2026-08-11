using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;

namespace VendasWeb
{
    public class itemPedido : clsConexao
    {
        public string codigoProduto { get; set; }
        public string descProduto { get; set; }
        public string ProdutoGrupo { get; set; }
        public string nomeProduto { get; set; }
        public string ComplementoNomeProduto { get; set; }
        public string revenda { get; set; }
        public string codigoTabela { get; set; }
        public string descricaoTabela { get; set; }
        public string classeRecDesp { get; set; }
        public float valorTabela { get; set; }
        public float valorItem { get; set; }
        public double valorTotal { get; set; }
        public string unidade { get; set; }
        public double quantidade { get; set; }
        public int UnidadePos { get; set; }
        public string obrigaCliche { get; set; }
        public int numSeq { get; set; }
        public int ItPedVendaNumSeq { get; set; }
        public float valorOriginal { get; set; }
        public List<composicaoItem> compItemPedidoList { get; set; }

        public string CodigoProdutoSAPCliche { get; set; }
        public string CodigoProdutoSAPArruela { get; set; }
        public string CodigoDepositoSAP { get; set; }

        //Campos SAP
        public string CodigoUtilizacaoSAP { get; set; }
        public string CodigoUnidadeMedidaSAP { get; set; }
        public string NomeUnidadeMedidaSAP { get; set; }
        public string CodigoNaturezaDestinacaoSAP { get; set; }

        public string xPed { get; set; }
        public string nItem { get; set; }


        //Construtor da classe item pedido
        public itemPedido(string novoCodigo, string novoTabela, float novoVlrTabela, float novoVlrItem,
            string novoUnidade, double novoQuantidade, string empresa, string nomeProduto, string compItemProduto, int numSeq, int ItPedVendaNumSeq, float valorOriginal, string SRCodigoProdutoSAPCliche, string SRCodigoProdutoSAPArruela,
            string xPedPedido, string nItemPedido)
        {
            //campos para NFE
            this.xPed = xPedPedido ?? "";
            this.nItem = nItemPedido ?? "";

            this.codigoProduto = novoCodigo;
            this.nomeProduto = nomeProduto + ' ' + compItemProduto;
            this.ComplementoNomeProduto = compItemProduto;
            this.descProduto = this.buscaDescricao(novoCodigo);
            this.revenda = "0";
            this.numSeq = numSeq;
            this.codigoTabela = novoTabela;
            this.valorTabela = novoVlrTabela;
            this.valorItem = novoVlrItem;
            this.unidade = novoUnidade;
            this.quantidade = novoQuantidade;
            this.valorTotal = calculaTotal(novoQuantidade, novoVlrItem);
            this.obrigaCliche = this.buscaObrigatoriedadeCliche(novoCodigo);

            this.descricaoTabela = this.buscaDescriaoTabela(empresa, novoTabela);
            this.ItPedVendaNumSeq = ItPedVendaNumSeq;
            this.valorOriginal = valorOriginal;

            this.CodigoProdutoSAPCliche = SRCodigoProdutoSAPCliche;
            this.CodigoProdutoSAPArruela = SRCodigoProdutoSAPArruela;

            if(this.CodigoProdutoSAPCliche != "" || this.CodigoProdutoSAPArruela != "")
            {
                this.InsereAruelaCliche();
            }
        }

        //Calcula total do item
        public double calculaTotal(double vlrItem, double quantidade)
        {
            return vlrItem * quantidade;
        }

        //Metodo para inserir dados composicao itens do pedido
        public void incluiItem(produto itemProduto)
        {
            composicaoItem novoItem = new composicaoItem(itemProduto.codigoProduto, itemProduto.codigoTabela,
                itemProduto.valorTabela, itemProduto.valorItem, itemProduto.unidade, itemProduto.quantidade);

            //Verifica se esta instanciado
            if (compItemPedidoList == null)
            {
                compItemPedidoList = new List<composicaoItem>();
            }

            this.compItemPedidoList.Add(novoItem);
        }

        //Metodo para remover dados composicao itens do pedido
        //Remove item da lista
        public void removeItem(int indexItem)
        {
            //Verifica se esta instanciado
            if (this.compItemPedidoList != null)
            {
                this.compItemPedidoList.RemoveAt(indexItem);
            }
        }

        //Retorna número de itens
        public int numeroItens()
        {
            return this.compItemPedidoList.Count;
        }

        //Funcao para grava pedido no banco
        public string gravaItemPedido(string empresa, string PedVendaNum, string vendedor, string entidade,
            string natureza, string empPaisOrig, string empUfOrig, string entPais, string entUf, string condicao,
            string operacao, string especie, int numseq, string usuario, float valorFrete, string dataPedido,
            string descOpercao, string PedVendaNumPedEnt, string IPIInclusoICMS)
        {

            string[] retErro = new string[2];
            string auxUnd = "";
            string situacao = "";

            funcoesBD mdlfuncoesBD = new funcoesBD();

            retErro = mdlfuncoesBD.gravaBancoItemPedido(empresa, PedVendaNum, vendedor, entidade, natureza, this.revenda,
                    this.codigoProduto, empPaisOrig, empUfOrig, entPais, entUf, condicao, this.unidade, operacao,
                    especie, numseq, this.quantidade, this.valorItem, this.codigoTabela, valorFrete, this.nomeProduto, dataPedido,
                    descOpercao, PedVendaNumPedEnt, this.ItPedVendaNumSeq, IPIInclusoICMS, this.CodigoProdutoSAPCliche, this.CodigoProdutoSAPArruela);

            auxUnd = retErro[2];
            this.UnidadePos = (int)Convert.ToInt32(auxUnd);


            //Grava tabela USER_tb_Pedido_Bloqueado_ITens
            if (retErro[0] == "")
            {

                if (this.valorItem < this.valorOriginal)
                {
                    situacao = "abaixo";
                }
                else
                {
                    situacao = "acima";
                }
                retErro[0] = this.gravaPedidoBloqueado(empresa, PedVendaNum, numseq, descOpercao, situacao);
            }

            this.classeRecDesp = retErro[1];

            return retErro[0];
        }

        public string gravaItemPedidoCRM(string empresa, int IDPedido, string OperacaoVenda, string TipoOperacao, string consumo)
        {
            //Recupera Cliche e Arruela
            this.RecuperaClicheArruela();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CRM_PEDIDO_ITENS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoOperacao", SqlDbType.VarChar, 100, "TipoOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoProdutoSAP", SqlDbType.VarChar, 50, "CodigoProdutoSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrecoUnitario", SqlDbType.Decimal, 0, "PrecoUnitario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Quantidade", SqlDbType.Decimal, 0, "Quantidade"));
                    //dbCommand.Parameters.Add(new SqlParameter("@NaturezaDestinacao", SqlDbType.NVarChar, 100, "NaturezaDestinacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDNaturezaDestinacao", SqlDbType.Int, 0, "IDNaturezaDestinacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoProdutoSAPCliche", SqlDbType.NVarChar, 50, "CodigoProdutoSAPCliche"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoProdutoSAPArruela", SqlDbType.NVarChar, 50, "CodigoProdutoSAPArruela"));
                    dbCommand.Parameters.Add(new SqlParameter("@TextoLivre", SqlDbType.NVarChar, 100, "TextoLivre"));
                    dbCommand.Parameters.Add(new SqlParameter("@xPed", SqlDbType.NVarChar, 10, "xPed"));
                    dbCommand.Parameters.Add(new SqlParameter("@nItem", SqlDbType.NVarChar, 10, "nItem"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@IDEmpresa"].Value = empresa;
                    dbCommand.Parameters["@IDPedido"].Value = IDPedido;
                    dbCommand.Parameters["@TipoOperacao"].Value = TipoOperacao;
                    dbCommand.Parameters["@CodigoProdutoSAP"].Value = this.codigoProduto;
                    dbCommand.Parameters["@PrecoUnitario"].Value = this.valorItem;
                    dbCommand.Parameters["@Quantidade"].Value = this.quantidade;
                    dbCommand.Parameters["@IDNaturezaDestinacao"].Value = consumo ?? "0";
                    dbCommand.Parameters["@CodigoProdutoSAPCliche"].Value = this.CodigoProdutoSAPCliche ?? "";
                    dbCommand.Parameters["@CodigoProdutoSAPArruela"].Value = this.CodigoProdutoSAPArruela ?? "";
                    dbCommand.Parameters["@TextoLivre"].Value = this.ComplementoNomeProduto ?? "";
                    dbCommand.Parameters["@xPed"].Value = this.xPed ?? "";
                    dbCommand.Parameters["@nItem"].Value = this.nItem ?? "";

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na inserção do item do pedido";
                }
            }

            return erro;
        }

        public string testaRegraItemPedido(string empresa, string PedVendaNum, string vendedor, string entidade,
            string natureza, string empPaisOrig, string empUfOrig, string entPais, string entUf, string condicao,
            string operacao, string especie, int cont, string usuario, float valorFrete, string dataPedido)
        {
            string retErro = "";

            funcoesBD mdlfuncoesBD = new funcoesBD();

            retErro = mdlfuncoesBD.testaRegrasItens(empresa, PedVendaNum, vendedor, entidade, natureza, this.revenda,
                this.codigoProduto, empPaisOrig, empUfOrig, entPais, entUf, condicao, this.unidade, operacao,
                especie, cont, this.quantidade, this.valorItem, this.codigoTabela, valorFrete, this.nomeProduto, dataPedido);

            return retErro;
        }

        //Funcao para gravar composicao do item
        public string gravaComposicaoItem(string empresa, int PedVendaNum, string usuario, int seqPrincipal, string descOpercao)
        {
            string retErro = "";

            int quant = 0;
            int cont = 0;

            if (this.compItemPedidoList != null)
            {
                quant = this.numeroItens();
            }

            while (cont < quant && quant > 0 && retErro == "")
            {
                //Chama funcao para grava item
                retErro = this.compItemPedidoList[cont].gravaComposicao(empresa, PedVendaNum, usuario, seqPrincipal, this.codigoProduto, this.unidade, this.UnidadePos, descOpercao);

                cont++;
            }

            return retErro;
        }

        //Funcao para buscar descricao do item
        public string buscaDescricao(string codigoProduto)
        {

            DataTable dadosTable = new DataTable();
            funcoes mdlfuncoes = new funcoes();

            string descricaoProduto = codigoProduto + " - ";
            string strSQL = "select ProdNome from PRODUTO where ProdCodEstr = '" + codigoProduto + "'";

            dadosTable = mdlfuncoes.Executa_DataTable(strSQL, "buscaDescricao - itemPedido.cs");
            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    descricaoProduto += row["ProdNome"].ToString();
                }
            }
            return descricaoProduto;
        }

        //Método buscar descrição da tabela
        public string buscaDescriaoTabela(string empresa, string codigo)
        {
            DataTable dadosTable = new DataTable();
            funcoes mdlfuncoes = new funcoes();

            string descricao = "";
            string strSQL = "";
            strSQL = "select TabPVNome from TAB_PV where EmpCod='" + empresa + "' and TabPVCod='" + codigo + "'";

            dadosTable = mdlfuncoes.Executa_DataTable(strSQL, "buscaDescriaoTabela - itemPedido.cs");
            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    descricao += row["TabPVNome"].ToString();
                }
            }
            return descricao;
        }

        //Método buscar quantidade em estoque
        public double buscaEstoque(string empresa, string pedido, string produto)
        {
            funcoesBD mdlfuncoesBD = new funcoesBD();

            double quantidadeEstoque = 0;
            string LocArmazCodEstr = "";

            string strSQL = "select LocArmazCodEstr from ITEM_PED_VENDA IPV INNER JOIN LOC_ARMAZ_ITEM_PED_VENDA LIPV ";
            strSQL += "ON IPV.EmpCod=LIPV.EmpCod and IPV.PedVendaNum=LIPV.PedVendaNum and IPV.ItPedVendaSeq=LIPV.ItPedVendaSeq ";
            strSQL += "where IPV.EmpCod='" + empresa.ToString() + "' and IPV.PedVendaNum='" + pedido.ToString() + "' and IPV.ProdCodEstr='" + produto.ToString() + "' ";

            LocArmazCodEstr = mdlfuncoesBD.ExecutaSqlReader(strSQL);

            strSQL = "select coalesce(EstqLocArmazQtd, 0) from ESTQ_LOC_ARMAZ ";
            strSQL += "where EmpCod='" + empresa + "' and ProdCodEstr='" + produto + "' and LocArmazCodEstr='" + LocArmazCodEstr + "'";

            quantidadeEstoque = (double)Convert.ToDouble(mdlfuncoesBD.ExecutaSqlReader(strSQL));

            return quantidadeEstoque;
        }

        //Método para verificar se item é obrigatorio CLiche
        public string buscaObrigatoriedadeCliche(string produto)
        {
            string obrigaCliche = "Nao";
            string strSQL = "";
            int cont = 0;
            //strSQL = "select count(*) as CNT from PRODUTO Pro join PRODUTO1 Pro1 on Pro.ProdCodEstr = Pro1.ProdCodEstr where Pro.ProdGrupo='F' and Pro1.USERLINHAPRODUTO='FITA IMP' and Pro.ProdCodEstr='" + produto.ToString() + "'";

            strSQL = "select count(*) as CNT from CRM_PRODUTO CP INNER JOIN CRM_PRODUTO_GRUPO CPG ON CP.IDGrupo = CPG.IDGrupo ";
            strSQL += "where CPG.Nome = 'PROD - FITA IMP' and CP.CodigoProdutoSAP = '" + produto.ToString() + "' ";

            funcoes mdlfuncoes = new funcoes();

            cont = Convert.ToInt32(mdlfuncoes.ExecutaSqlReader(strSQL, "buscaObrigatoriedadeCliche - ItemPedido.cs"));

            if (cont > 0)
            {
                obrigaCliche = "SIM";
            }
            return obrigaCliche;
        }

        //Método para gravar USER_TB_PEDIDOS_BLOQUEADOS
        public string gravaPedidoBloqueado(string empresa, string PedVendaNum, int numseq, string operacao, string situacao)
        {
            string erro = "";
            funcoesBD mdlfuncoesBD = new funcoesBD();

            erro = mdlfuncoesBD.gravaPedidoBloqueadoItem(empresa, PedVendaNum, this.codigoProduto, this.valorOriginal, numseq, operacao, situacao);

            return erro;
        }

        public void RecuperaClicheArruela()
        {
            string retErro = "";

            int quant = 0;
            int cont = 0;

            if (this.compItemPedidoList != null)
            {
                quant = this.numeroItens();
            }

            while (cont < quant && quant > 0 && retErro == "")
            {
                if(this.compItemPedidoList[cont].GrupoProduto == "14")
                {
                    this.CodigoProdutoSAPCliche = this.compItemPedidoList[cont].codigoProdutoComposicao;
                }else
                {
                    this.CodigoProdutoSAPArruela = this.compItemPedidoList[cont].codigoProdutoComposicao;
                }

                cont++;
            }
        }

        public void InsereAruelaCliche()
        {
            produto compProduto = new produto();
            funcoes mdlfuncoes= new funcoes();

            if (this.CodigoProdutoSAPCliche != "" && this.CodigoProdutoSAPCliche !=null)
            {
                compProduto.codigoProduto = this.CodigoProdutoSAPCliche;
                compProduto.codigoTabela = "1";
                compProduto.descProduto = mdlfuncoes.Consulta_CodNome_Produto(this.CodigoProdutoSAPCliche);
                compProduto.descricaoProduto = mdlfuncoes.Consulta_CodNome_Produto(this.CodigoProdutoSAPCliche);
                compProduto.quantidade = 1;
                compProduto.revenda = "0";
                compProduto.unidade = mdlfuncoes.Consulta_Unidade_Medida(this.CodigoProdutoSAPCliche);
                compProduto.valorItem = 0;
                compProduto.valorTabela = 0;

                this.incluiItem(compProduto);
            }

            if (this.CodigoProdutoSAPArruela != "" && this.CodigoProdutoSAPArruela !=null)
            {
                compProduto.codigoProduto = this.CodigoProdutoSAPArruela;
                compProduto.codigoTabela = "1";
                compProduto.descProduto = mdlfuncoes.Consulta_CodNome_Produto(this.CodigoProdutoSAPArruela);
                compProduto.descricaoProduto = mdlfuncoes.Consulta_CodNome_Produto(this.CodigoProdutoSAPArruela);
                compProduto.quantidade = 1;
                compProduto.revenda = "0";
                compProduto.unidade = mdlfuncoes.Consulta_Unidade_Medida(this.CodigoProdutoSAPArruela);
                compProduto.valorItem = 0;
                compProduto.valorTabela = 0;

                this.incluiItem(compProduto);
            }

        }

        public void RecuperaDadosItensSAP(int IDPedido, int IDEmpresa)
        {
            DataTable outputTable = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PEDIDOS_ITENS_DADOS_SAP", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPedido", SqlDbType.Int, 0, "IDPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoProdutoSAP", SqlDbType.VarChar, 100, "CodigoProdutoSAP"));

                    dbCommand.Parameters["@IDPedido"].Value = IDPedido;
                    dbCommand.Parameters["@IDEmpresa"].Value = IDEmpresa;
                    dbCommand.Parameters["@CodigoProdutoSAP"].Value = codigoProduto;


                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.CodigoUtilizacaoSAP = row["CodigoUtilizacaoSAP"].ToString();
                                this.CodigoUnidadeMedidaSAP = row["CodigoUnidadeMedida"].ToString();
                                this.NomeUnidadeMedidaSAP = row["UnidadeVenda"].ToString();
                                this.CodigoNaturezaDestinacaoSAP = row["CodigoNaturezaDestinacaoSAP"].ToString();
                                this.CodigoDepositoSAP = row["CodigoDepositoSAP"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}