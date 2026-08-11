using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.cadastros
{
    public partial class cadPedidoArte : System.Web.UI.Page
    {
        funcoes mdlfuncsMan = new funcoes();
        criptografia mdlcriptografia = new criptografia();
        
        //Instancia classe pedido
        pedido novoPedido = new pedido();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica se usuário esta logado
            int varmenu = Convert.ToInt32(Request.QueryString["indmnu"]);
            if (Session["usuario"] == null && varmenu != 0 && varmenu < 99)
            {
                //Redireciona para tela de login
                Response.Redirect("~/Default.aspx");
            }            
            
            
            //Recupera objeto pedido da sessao do usuário
            novoPedido = (pedido)Session["pedidoNovo"];

            if (!IsPostBack)
            {
                TextBox1.Text = "0";
                TextBox2.Text = "0";
                TextBox3.Text = "0";

                LinkButton1.Visible = false;
                LinkButton2.Visible = false;

                ltlListaProdutos.Text = gerarLista(14);
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ltlListaProdutos.Text = gerarLista(-14);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            ltlListaProdutos.Text = gerarLista(14);
        }

        public string gerarLista(int quant)
        {
            string strSQL = "";
            string codEmp = novoPedido.codigoEmpresa;
            int tipoConsulta = Convert.ToInt32(drpProdutos.SelectedItem.Value);
            string valorConsulta = txtFiltroProd.Text;

            int indexPage = 0;
            int fimPage = 0;
            int numPad = 0;

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

            if (tipoConsulta == 1)
            {
                strSQL += "select count(*) as CNT From  Produto P INNER JOIN FOTO_PROD FP ON P.ProdCodEstr=FP.ProdCodEstr WHERE (P.ProdStat != 'Desativado' or P.ProdDataValidFim > getdate())  and P.ProdNome like '%" + valorConsulta.ToString() + "%'";
            }
            else
            {
                strSQL += "select count(*) as CNT From Produto P INNER JOIN FOTO_PROD FP ON P.ProdCodEstr=FP.ProdCodEstr WHERE (P.ProdStat != 'Desativado' or P.ProdDataValidFim > getdate())  and P.ProdCodEstr like '%" + valorConsulta.ToString() + "%'";
            }

            using (SqlConnection dbConnection = new SqlConnection(mdlfuncsMan.getString().ToString()))
            {
                SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection);

                dbConnection.Open();

                SqlDataReader drProduto = dbCommand.ExecuteReader();

                if (drProduto.Read())
                {
                    numPad = Convert.ToInt32(drProduto["CNT"]);
                }

                if (fimPage >= numPad)
                {
                    LinkButton2.Visible = false;
                }
                else
                {
                    LinkButton2.Visible = true;
                }

                drProduto.Close();
            }

            return linhasProduto(indexPage, fimPage);
        }

        public string linhasProduto(int indexPage, int fimPage)
        {
            string strSQL="";
            string codEmp = novoPedido.codigoEmpresa;
            string descLinhas = "";
            int tipoConsulta = Convert.ToInt32(drpProdutos.SelectedItem.Value);
            string valorConsulta = txtFiltroProd.Text;

            strSQL += "select ProdCodEstr, ProdNome, reg FROM (";
            
            if (tipoConsulta == 1)
            {
                strSQL += "select P.ProdCodEstr, P.ProdNome, ROW_NUMBER() OVER(ORDER BY P.ProdCodEstr) as reg From  Produto P INNER JOIN FOTO_PROD FP ON P.ProdCodEstr=FP.ProdCodEstr WHERE (P.ProdStat != 'Desativado' or P.ProdDataValidFim > getdate())  and P.ProdNome like '%" + valorConsulta.ToString() + "%'";
            }
            else {
                strSQL += "select P.ProdCodEstr, P.ProdNome, ROW_NUMBER() OVER(ORDER BY P.ProdCodEstr) as reg From Produto P INNER JOIN FOTO_PROD FP ON P.ProdCodEstr=FP.ProdCodEstr WHERE (P.ProdStat != 'Desativado' or P.ProdDataValidFim > getdate())  and P.ProdCodEstr like '%" + valorConsulta.ToString() + "%'";
            }
            
            strSQL += ") a where reg BETWEEN '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";


            using (SqlConnection dbConnection = new SqlConnection(mdlfuncsMan.getString().ToString()))
            {
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection);

                SqlDataReader drProduto = dbCommand.ExecuteReader();

                if (drProduto.HasRows)
                {
                    //Inicio da tabela
                    descLinhas += "<table class=\"lstTabela\">";

                    //cabeçario da tabela
                    descLinhas += "<tr class=\"tabLstCab\">";
                    descLinhas += "<td>Selecine:</td>";
                    //descLinhas += "<td>Tabela:</td>";
                    descLinhas += "<td>Código</td>";
                    descLinhas += "<td class=\"extend\">Nome:</td>";
                    //descLinhas += "<td>Unidade:</td>";
                    //descLinhas += "<td align=\"right\">Valor:</td>";
                    //descLinhas += "<td>Fantasia:</td>";
                    //descLinhas += "<td>CNPJ/CPF:</td>";
                    descLinhas += "<td>Foto:</td>";
                    descLinhas += "</tr>";

                    //TabPVCod, ProdCodEstr, ProdNome, ProdUnidMedCod, ItTabPVUnMedVal

                    while (drProduto.Read())
                    {
                        descLinhas += "<td class=\"edicao\"><a href=\"../cadastros/cadPedidoListaArte.aspx?indmnu=2&idComposicao=" + mdlcriptografia.Criptografar(drProduto["ProdCodEstr"].ToString(), "#!$a36?@") + "&idexItem=" + mdlcriptografia.Criptografar(Session["idexItem"].ToString(), "#!$a36?@") + "&codEmp=" + mdlcriptografia.Criptografar(codEmp.ToString(), "#!$a36?@") + "&idProd=" + mdlcriptografia.Criptografar(drProduto["ProdCodEstr"].ToString(), "#!$a36?@") + "&idOpe=" + mdlcriptografia.Criptografar("inclusao", "#!$a36?@") + " \" class=\"imgedit\"><img src=\"../imagens/edit.png\" alt=\"Alteração\" border=\"0\" /></a></td>";
                        //descLinhas += "<td>" + drProduto["TabPVCod"] + "</td>";
                        descLinhas += "<td class=\"grande\">" + drProduto["ProdCodEstr"] + "</td>";
                        descLinhas += "<td>" + drProduto["ProdNome"] + "</td>";
                        //descLinhas += "<td>" + drProduto["ProdUnidMedCod"] + "</td>";
                        //descLinhas += "<td align=\"right\">" + Math.Round(Convert.ToDecimal(drProduto["ItTabPVUnMedVal"]), 2).ToString() + "</td>";
                        descLinhas += "<td class=\"edicao\"><a href=\"#\" class=\"imgedit\"><img src=\"../imagens/search.png\" alt=\"Consulta\" border=\"0\" onclick=\"javascript: return abrirArte('" + drProduto["ProdCodEstr"].ToString() + "')\" /></a></td>";
                        descLinhas += "</tr>";
                    }
                    //Fim tabela
                    descLinhas += "</table>";
                }
                drProduto.Close();
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
    }
}