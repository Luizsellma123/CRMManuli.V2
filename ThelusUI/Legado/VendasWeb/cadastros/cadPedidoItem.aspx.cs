using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.classes;

namespace VendasWeb
{
    public partial class cadPedidoItem : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        funcoes mdlfuncoes = new funcoes();
        criptografia mdlcriptografia = new criptografia();

        //Instancia Objeto do tipo pedido
        pedido novoPedido = new pedido();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            novoPedido = (pedido)Session["pedidoNovo"];

            if (!IsPostBack)
            {

                Session["Origem"] = "Itens";

                TextBox1.Text = "0";
                TextBox2.Text = "0";
                TextBox3.Text = "0";

                LinkButton1.Visible = false;
                LinkButton2.Visible = false;
            }
            else
            {
                if (Request.QueryString["acao"] != null)
                {
                    int acao = Convert.ToInt32(Request.QueryString["acao"]);

                    if (acao == 1)
                    {
                        carregaCalculo();
                    }
                }
            }
        }

        public void carregaCalculo()
        {
            string codProduto;
            string codTabela;
            string valProduto;
            string codUnidade;

            //Recupera dados do produto a ser trabalhado
            codProduto = mdlcriptografia.Descriptografar(Request.QueryString["idProd"], "#!$a36?@");
            codTabela = mdlcriptografia.Descriptografar(Request.QueryString["idTab"], "#!$a36?@");
            valProduto = mdlcriptografia.Descriptografar(Request.QueryString["idVal"], "#!$a36?@");
            codUnidade = mdlcriptografia.Descriptografar(Request.QueryString["idUn"], "#!$a36?@");
        }

        public string gerarLista(int quant)
        {
            int indexPage = 0;
            int fimPage = 0;
            int numPad = 0;
            string auxemp = "1";
            string strSQL;

            if (novoPedido.codigoEmpresa != "99")
            {
                //Alteracao feita para sempre trazer tabela de preco da empresa 1 se nao for empresa 2
                if (novoPedido.codigoEmpresa.Substring(0, 1) != "2")
                {
                    auxemp = "1";
                }
                else
                {
                    auxemp = novoPedido.codigoEmpresa;
                }
            }
            else
            {
                auxemp = "1";
            }

            if (quant > 0)
            {
                indexPage = Convert.ToInt32(TextBox1.Text);
                fimPage = Convert.ToInt32(TextBox1.Text);
                fimPage = fimPage + 14;
                indexPage = indexPage + 1;
            }
            else
            {
                indexPage = Convert.ToInt32(TextBox2.Text);
                fimPage = Convert.ToInt32(TextBox2.Text);
                indexPage = indexPage - 14;
                fimPage = fimPage - 1;
            }

            if (indexPage <= 0 || indexPage == 1)
            {
                LinkButton1.Visible = false;
            }
            else
            {
                LinkButton1.Visible = true;
            }

            strSQL = "select count(*) as CNT from CRM_TABELA_PRECO_PROD CTPP INNER JOIN CRM_TABELA_EMPRESA CTE ON CTPP.IDtabela=CTE.IDTabela ";
            strSQL += "INNER JOIN CRM_PRODUTO CP ON CP.IDProduto = CTPP.IDProduto ";
            strSQL += "where CTE.IDEmpresa = '" + novoPedido.codigoEmpresa + "' and CTPP.[Status]='Ativo' ";

            if (txtFiltroProd.Text == "" || txtFiltroProd.Text == null)
            {
                numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader(strSQL, "gerarLista CadPedidoItem.apx.cs")).ToString());
            }
            else
            {
                int tipoConsulta = Convert.ToInt32(drpProdutos.SelectedItem.Value);
                string valorConsulta = txtFiltroProd.Text;
                if (tipoConsulta == 1)
                {
                    strSQL += " and CP.Nome like '" + valorConsulta.ToString() + "%'";
                    numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader(strSQL, "gerarLista CadPedidoItem.apx.cs")).ToString());
                }
                else
                {
                    strSQL += " and CP.CodigoProdutoSAP like '" + valorConsulta.ToString() + "%'";
                    numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader(strSQL, "gerarLista CadPedidoItem.apx.cs")).ToString());
                }
            }

            if (fimPage >= numPad)
            {
                LinkButton2.Visible = false;
            }
            else
            {
                LinkButton2.Visible = true;
            }

            return linhasProduto(indexPage, fimPage);
        }

        public string linhasProduto(int indexPage, int fimPage)
        {
            string descLinhas = "";
            string codEmp = novoPedido.codigoEmpresa.ToString();
            string strSQL = "";
            string auxemp = "1";
            double auxValorItem = 0;
            double auxValorImpostos = 0;
            string erro = "";
            string valorImpostos = "";
            int IDUsuario;
            //int Posicao;
            string strconec;

            IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            //novoPedido.RetornaPosicaoUnidadeMedida(txtFiltroProd.Text, out Posicao);

            if (codEmp != "99")
            {
                //Alteracao feita para sempre trazer tabela de preco da empresa 1 se nao for empresa 2
                if (codEmp.Substring(0, 1) != "2")
                {
                    auxemp = "1";
                    codEmp = "1";
                }
                else
                {
                    auxemp = codEmp;
                }
            }
            else
            {
                auxemp = "1";
            }

            strSQL = "select IDTabela, NomeTabela, CodigoProdutoSAP, NomeProduto, UnidadeVenda, ";
            strSQL += "ValorUnitario, reg from( ";
            strSQL += "select CTPP.IDTabela, CTP.Nome as NomeTabela, CP.CodigoProdutoSAP, CP.Nome as NomeProduto, CP.UnidadeVenda, ";
            strSQL += "CTPP.ValorUnitario, ROW_NUMBER() OVER(ORDER BY CP.CodigoProdutoSAP) as reg from ";
            strSQL += "CRM_TABELA_PRECO_PROD CTPP ";
            strSQL += "INNER JOIN CRM_TABELA_EMPRESA CTE ON CTPP.IDtabela = CTE.IDTabela ";
            strSQL += "INNER JOIN CRM_TABELA_PRECO CTP ON CTP.IDTabela = CTE.IDTabela ";
            strSQL += "INNER JOIN CRM_PRODUTO CP ON CP.IDProduto = CTPP.IDProduto ";
            strSQL += "where CTE.IDEmpresa = '" + novoPedido.codigoEmpresa + "' and CTPP.[Status] = 'Ativo' ";


            int tipoConsulta = Convert.ToInt32(drpProdutos.SelectedItem.Value);
            string valorConsulta = txtFiltroProd.Text;
            if (tipoConsulta == 1)
            {
                strSQL += " and CP.Nome like '" + valorConsulta.ToString() + "%'";
            }
            else
            {
                strSQL += " and CP.CodigoProdutoSAP like '" + valorConsulta.ToString() + "%'";
            }

            strSQL += ") a where reg BETWEEN '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";

            strconec = mdlfuncoes.getString().ToString();

            using (SqlConnection dbConnection = new SqlConnection(strconec))
            {
                using (SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection))
                {
                    dbConnection.Open();

                    using (SqlDataReader drProduto = dbCommand.ExecuteReader())
                    {

                        if (drProduto.HasRows)
                        {
                            //Inicio da tabela
                            descLinhas += "<table table class=\"table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed\"style=\"border-collapse:collapse;border-collapse: collapse; max-width: 100%\">";
                            //cabeçario da tabela
                            descLinhas += "<tr>";
                            descLinhas += "<th scope=\"col\">Selecione</th>";
                            descLinhas += "<th scope=\"col\">Tabela</th>";
                            descLinhas += "<th scope=\"col\">Código</th>";
                            descLinhas += "<th class=\"extend\">Nome</th>";
                            descLinhas += "<th scope=\"col\">Unidade</th>";
                            //descLinhas += "<th scope=\"col\">Tributação</th>";
                            descLinhas += "<th scope=\"col\">Valor Tab. </th>";
                            descLinhas += "<th scope=\"col\">Valor Venda</th>";
                            descLinhas += "</tr>";

                            while (drProduto.Read())
                            {
                                if (novoPedido.codigoEmpresa.ToString() != "2" && novoPedido.codigoEmpresa.ToString() != "2.1")
                                {
                                    int IDClassificacaoComercial = 0;

                                    {
                                        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();

                                        if (Session["clsEntidades"] != null)
                                            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

                                        ClienteClasse objClienteClasse = new ClienteClasse();

                                        objClienteClasse.CodigoCliente = ObjEntidadesClass.CodigoClienteSAP;

                                        DataTable ClassificacaoComercialDataTable = objClienteClasse.CarregaClassificacaoComercial();

                                        if (ClassificacaoComercialDataTable.Rows.Count > 0)
                                        {
                                            foreach (DataRow row in ClassificacaoComercialDataTable.Rows)
                                            {
                                                IDClassificacaoComercial = Convert.ToInt32(row["IDClassificacaoComercial"]);
                                            }
                                        }
                                    }

                                    novoPedido.calculaCustoHexadecimal((double)Math.Round(Convert.ToDecimal(drProduto["ValorUnitario"]), 2), drProduto["CodigoProdutoSAP"].ToString(), drProduto["UnidadeVenda"].ToString(), out auxValorItem, out auxValorImpostos, out erro, drProduto["IDTabela"].ToString(), IDUsuario, IDClassificacaoComercial);

                                    if (erro != "")
                                    {
                                        auxValorItem = (double)Math.Round(Convert.ToDecimal(drProduto["ValorUnitario"]), 2);
                                        auxValorImpostos = (double)Math.Round(Convert.ToDecimal(drProduto["ValorUnitario"]), 2);
                                    }
                                }
                                else
                                {
                                    auxValorItem = (double)Math.Round(Convert.ToDecimal(drProduto["ValorUnitario"]), 2);
                                    auxValorImpostos = (double)Math.Round(Convert.ToDecimal(drProduto["ValorUnitario"]), 2);
                                }

                                valorImpostos = String.Format("{0:0.00}", auxValorImpostos);

                                descLinhas += "<td align=\"center\"><a href=\"../cadastros/cadPedidoPrincipal.aspx?indmnu=2&idEmp=" + mdlcriptografia.Criptografar(codEmp.ToString(), "#!$a36?@") + "&idProd=" + mdlcriptografia.Criptografar(drProduto["CodigoProdutoSAP"].ToString(), "#!$a36?@") + "&idTab=" + mdlcriptografia.Criptografar(drProduto["IDTabela"].ToString(), "#!$a36?@") + "&idUn=" + mdlcriptografia.Criptografar(drProduto["UnidadeVenda"].ToString(), "#!$a36?@") + "&idVal=" + mdlcriptografia.Criptografar(String.Format("{0:0.00}", auxValorImpostos), "#!$a36?@") + "&idOpe=" + mdlcriptografia.Criptografar(novoPedido.tipoOperacao.ToString(), "#!$a36?@") + "&idPed=" + mdlcriptografia.Criptografar(novoPedido.numeroPedido.ToString(), "#!$a36?@") + " \" ><i class=\"fa fa-check-circle-o fa-2x\"></i></a></td>";
                                descLinhas += "<td>" + drProduto["NomeTabela"] + "</td>";
                                descLinhas += "<td>" + drProduto["CodigoProdutoSAP"] + "</td>";
                                descLinhas += "<td>" + drProduto["NomeProduto"] + "</td>";
                                descLinhas += "<td>" + drProduto["UnidadeVenda"] + "</td>";
                                //descLinhas += "<td>" + novoPedido.Tributacao + "</td>";
                                descLinhas += "<td align=\"right\">" + Math.Round(Convert.ToDecimal(drProduto["ValorUnitario"]), 2).ToString() + "</td>";
                                descLinhas += "<td align=\"right\">" + valorImpostos.ToString() + "</td>";
                                descLinhas += "</tr>";
                            }
                            //Fim tabela
                            descLinhas += "</table><br />";
                        }
                    }
                }
            }
            TextBox1.Text = fimPage.ToString();
            TextBox2.Text = indexPage.ToString();
            return descLinhas;
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "0";
            TextBox2.Text = "0";
            ltlListaProdutos.Text = gerarLista(14);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ltlListaProdutos.Text = gerarLista(-14);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            ltlListaProdutos.Text = gerarLista(14);
        }
    }
}