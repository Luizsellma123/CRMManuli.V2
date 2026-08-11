using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb
{
    public class composicaoItem
    {
        public string codigoProduto { get; set; }
        public string codigoProdutoComposicao { get; set; }
        public string descProduto { get; set; }
        public string revenda { get; set; }
        public string codigoTabela { get; set; }
        public float valorTabela { get; set; }
        public float valorItem { get; set; }
        public double valorTotal { get; set; }
        public string unidade { get; set; }
        public double quantidade { get; set; }
        public string GrupoProduto { get; set; }

        //Construtor da classe composicao item pedido
        public composicaoItem(string novoCodigo, string novoTabela, float novoVlrTabela, float novoVlrItem,
            string novoUnidade, double novoQuantidade)
        {
            this.codigoProdutoComposicao = novoCodigo;
            this.descProduto = this.buscaDescricao(novoCodigo);
            this.GrupoProduto = this.BuscaGrupo(novoCodigo);
            this.revenda = "0";
            this.codigoTabela = novoTabela;
            this.valorTabela = novoVlrTabela;
            this.valorItem = novoVlrItem;
            this.unidade = novoUnidade;
            this.quantidade = novoQuantidade;
            this.valorTotal = calculaTotal(novoQuantidade, novoVlrItem);
        }

        //Calcula total do item
        public double calculaTotal(double vlrItem, double quantidade)
        {
            return vlrItem * quantidade;
        }

        //Funcao para grava composicao
        public string gravaComposicao(string empresa, int PedVendaNum, string usuario, int cont, string codigoProdutoPrincipal, string unidadePrincipal,
            int UnidadePos, string tipoOpercao)
        {
            string retErro = "";
            funcoesBD mdlFuncoes = new funcoesBD();

            //retErro = mdlFuncoes.gravaComposicaoPedido(empresa, PedVendaNum, usuario, cont, codigoProdutoPrincipal, this.codigoProdutoComposicao, this.buscaAlternativo(),
            //    this.unidade, this.quantidade, UnidadePos, tipoOpercao);

            return retErro;
        }

        //funcao para buscar nome alternativo produto
        public string buscaAlternativo()
        {
            DataTable dadosTable = new DataTable();

            string retAlternativo = "";
            string strSQL = "";

            funcoes mdlfuncoes = new funcoes();

            strSQL = "select ProdCodAlt from produto where ProdCodEstr like '%" + this.codigoProdutoComposicao + "%'";

            dadosTable = mdlfuncoes.Executa_DataTable(strSQL, "buscaAlternativo - composicaoItem.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    retAlternativo = (string)Convert.ToString(row["ProdCodAlt"]);
                }
            }
            return retAlternativo;
        }

        //Funcao para buscar descricao do item
        public string buscaDescricao(string codigoProduto)
        {
            DataTable dadosTable = new DataTable();

            string descricaoProduto = codigoProduto + " - ";
            //string strSQL = "select ProdNome from PRODUTO where ProdCodEstr = '" + codigoProduto + "'";
            string strSQL = "select CP.Nome ProdNome from CRM_PRODUTO CP where CP.CodigoProdutoSAP = '" + codigoProduto + "'";

            funcoes mdlfuncoes = new funcoes();

            dadosTable = mdlfuncoes.Executa_DataTable(strSQL, "buscaDescricao composicaoItem.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    descricaoProduto += row["ProdNome"].ToString();
                }
            }
            return descricaoProduto;
        }

        public string BuscaGrupo(string codigoProduto)
        {
            DataTable dadosTable = new DataTable();

            string strSQL = "select CP.IDGrupo from CRM_PRODUTO CP where CP.CodigoProdutoSAP = '" + codigoProduto + "'";

            funcoes mdlfuncoes = new funcoes();

            dadosTable = mdlfuncoes.Executa_DataTable(strSQL, "buscaDescricao composicaoItem.cs");

            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    GrupoProduto = row["IDGrupo"].ToString();
                }
            }
            return GrupoProduto;

        }
    }
}