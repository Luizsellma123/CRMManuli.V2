using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.cadastros
{
    public partial class cadPedidoListaArte : System.Web.UI.Page
    {
        criptografia mdlCriptografia = new criptografia();
        funcoes mdlfuncoes = new funcoes();

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

                Session["Origem"] = "Cliche";
                int idexItem = 0;
                if (Request.QueryString["idexItem"] != null)
                {
                    idexItem = (int)Convert.ToInt16(mdlCriptografia.Descriptografar(Request.QueryString["idexItem"].ToString(), "#!$a36?@"));

                    Session["idexItem"] = idexItem;
                    carregaCabecario();

                    if (Request.QueryString["idComposicao"] != null)
                    {
                        lblDescProduto.Visible = false;
                        lblDescUnidade.Visible = false;
                        drpRevenda.Visible = false;
                        txtQuantidade.Visible = false;
                        drpTabela.Visible = false;
                        txtValor.Visible = false;
                        btnSalvar.Visible = false;

                        salvaItem(mdlCriptografia.Descriptografar(Request.QueryString["idComposicao"].ToString(), "#!$a36?@").ToString());
                    }
                    else
                    {
                        lblDescProduto.Visible = false;
                        lblDescUnidade.Visible = false;
                        drpRevenda.Visible = false;
                        txtQuantidade.Visible = false;
                        drpTabela.Visible = false;
                        txtValor.Visible = false;
                        btnSalvar.Visible = false;
                    }
                    carregaItems();

                    if (novoPedido.tipoOperacao == "inclusao" || novoPedido.tipoOperacao == "alteracao")
                    {
                        btnIncluir.Visible = true;
                    }
                    else
                    {
                        btnIncluir.Visible = false;
                    }
                }
                else
                {
                    Response.Write("<script>history.go(-1)</script>"); 
                }
            }
            else
            {
                string idItem = Page.Request["idItem"];

                if (idItem != null && idItem != "")
                {
                    detetaItem(idItem);
                }
            }
        }

        public void carregaCabecario()
        {
            string strSQL = "";
            string vendCod = "";
            string codEnt = novoPedido.codigoEntidade.ToString();
            string codEmp = novoPedido.codigoEmpresa.ToString();
            int idexItem =(int)Convert.ToInt16(Session["idexItem"]);

            lblCabDescProduto.Text = mdlfuncoes.Consulta_CodNome_Produto(novoPedido.itemPedidoList[idexItem].codigoProduto.ToString());

            vendCod = mdlfuncoes.Consulta_Vendedor_Entidade(novoPedido.codigoEntidade.ToString()).ToString();
            if (vendCod == "0" || vendCod == null)
            {
                vendCod = "0000009";
            }

            drpTabela.DataSource = mdlfuncoes.Consulta_Tab_PV_Vendedor(novoPedido.codigoEmpresa.ToString(), vendCod.ToString());
            drpTabela.DataTextField = "tabpvnome";
            drpTabela.DataValueField = "tabpvcod";
            drpTabela.DataBind();
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location=\"../cadastros/cadPedidoArte.aspx?indmnu=2&idProduto=" + mdlCriptografia.Criptografar(Session["idexItem"].ToString(), "#!$a36?@") + "\";</script>");
        }

        public void carregaItems()
        {
            int idexItem = (int)Convert.ToInt16(Session["idexItem"]);
            int quant = 0;
            int cont = 0;
            ltlItems.Text = "";

            if (novoPedido.itemPedidoList[idexItem].compItemPedidoList != null)
            {
                quant = novoPedido.itemPedidoList[idexItem].numeroItens();
            }

            while (cont < quant && quant > 0)
            {
                ltlItems.Text += "<tr>";

                if (novoPedido.tipoOperacao == "inclusao" || novoPedido.tipoOperacao =="alteracao")
                {
                    ltlItems.Text += "<td align=\"center\"><a href=\"#\"><img src=\"../imagens/delete.png\" alt=\"delete\" border=\"0\" onclick=\"javascript: return fdelete('" + cont.ToString() + "')\" /></a></td>";
                }
                else
                {
                    ltlItems.Text += "<td></td>";
                }

                ltlItems.Text += "<td class=\"grande\">" + novoPedido.itemPedidoList[idexItem].compItemPedidoList[cont].descProduto.ToString() + "</td>";
                ltlItems.Text += "<td class=\"texto\">" + novoPedido.itemPedidoList[idexItem].compItemPedidoList[cont].unidade.ToString() + "</td>";
                ltlItems.Text += "<td class=\"edicao\"><a href=\"#\" class=\"imgedit\"><img src=\"../imagens/search.png\" alt=\"Consulta\" border=\"0\" onclick=\"javascript: return abrirArte('" + novoPedido.itemPedidoList[idexItem].compItemPedidoList[cont].codigoProdutoComposicao.ToString() + "')\" /></a></td>";
                
                ltlItems.Text += "</tr>";

                cont++;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int idexItem = (int)Convert.ToInt16(Session["idexItem"]);
            
            //Instancia objeto do tipo produto para incluir nos itens
            produto novoProduto = new produto();

            novoProduto.descProduto = lblDescProduto.Text;
            novoProduto.unidade = lblDescUnidade.Text;
            novoProduto.revenda = "0";
            novoProduto.quantidade = 1;
            novoProduto.codigoTabela = novoPedido.itemPedidoList[idexItem].codigoTabela;
            novoProduto.valorItem = 0;
            novoProduto.codigoProduto = lblProdutoAux.Text;

            //Inclui composicão do produto
            novoPedido.itemPedidoList[idexItem].incluiItem(novoProduto);
            
            //Limpa Variaveis
            lblDescProduto.Text = "";
            lblDescUnidade.Text = "";
            lblProdutoAux.Text = "";

            lblDescProduto.Visible = false;
            lblDescUnidade.Visible = false;
            btnSalvar.Visible = false;

            carregaItems();
        }

        public void carregaDados(string compProduto) 
        {
            string codEmp = novoPedido.codigoEmpresa;
            compProduto = mdlCriptografia.Descriptografar(compProduto, "#!$a36?@");

             if (Request.QueryString["idProd"] != null)
            {
                lblDescProduto.Visible = true;
                lblDescUnidade.Visible = true;
                btnSalvar.Visible = true;
            }
            else
            {
                lblDescProduto.Visible = false;
                lblDescUnidade.Visible = false;
                btnSalvar.Visible = false;
            }

             lblDescProduto.Text = mdlfuncoes.Consulta_CodNome_Produto(compProduto).ToString();

            lblProdutoAux.Text = compProduto;
            lblDescUnidade.Text = mdlfuncoes.Consulta_Unidade_Medida(compProduto).ToString();

            carregaItems();
        }

        public void detetaItem(string idItem)
        {
            int auxItem =(int)Convert.ToInt16(Session["idexItem"]);
            int idRemove = (int)Convert.ToInt16(idItem);

            novoPedido.itemPedidoList[auxItem].removeItem(idRemove);

            carregaItems();
        }

        public void salvaItem(string compProduto)
        {
            int idexItem = (int)Convert.ToInt16(Session["idexItem"]);

            //Instancia objeto do tipo produto para incluir nos itens
            produto novoProduto = new produto();

            novoProduto.descProduto = mdlfuncoes.Consulta_CodNome_Produto(compProduto).ToString();

            novoProduto.unidade = mdlfuncoes.Consulta_Unidade_Medida(compProduto).ToString();
            novoProduto.revenda = "0";
            novoProduto.quantidade = 1;
            novoProduto.codigoTabela = novoPedido.itemPedidoList[idexItem].codigoTabela;
            novoProduto.valorItem = 0;
            novoProduto.codigoProduto = compProduto;

            //Inclui composicão do produto
            novoPedido.itemPedidoList[idexItem].incluiItem(novoProduto);

            //Limpa Variaveis
            lblDescProduto.Text = "";
            lblDescUnidade.Text = "";
            lblProdutoAux.Text = "";

            lblDescProduto.Visible = false;
            lblDescUnidade.Visible = false;
            btnSalvar.Visible = false;

            carregaItems();
        }
    }
}