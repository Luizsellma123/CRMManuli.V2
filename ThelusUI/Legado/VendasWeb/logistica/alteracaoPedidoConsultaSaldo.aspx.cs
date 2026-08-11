using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace VendasWeb.logistica
{
    public partial class alteracaoPedidoConsultaSaldo : System.Web.UI.Page
    {
        criptografia mdlCriptografia = new criptografia();
        DataTable dadosTable = new DataTable();
        SessionClass OBJSessao = new SessionClass();

        //Instancia Objeto do tipo pedido
        pedido novoPedido = new pedido();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            novoPedido = (pedido)Session["pedidoNovo"];
            
            if (!IsPostBack)
            {
                string codigoProduto = "";

                codigoProduto = mdlCriptografia.Descriptografar(Request.QueryString["idProd"], "#!$a36?@");

                carregaDados(codigoProduto);
            }
        }

        public void carregaDados(string codigoProduto) {

            produto itemProduto = new produto();
            string descLinhas = "";

            dadosTable = itemProduto.verificaEstoque(novoPedido.codigoEmpresa, codigoProduto);

            if (dadosTable.Rows.Count > 0)
            {
                //Inicio da tabela
                descLinhas += "<table class=\"lstTabela\">";

                //cabeçario da tabela
                descLinhas += "<tr class=\"tabLstCab\">";
                descLinhas += "<td>Local Armazenagem:</td>";
                descLinhas += "<td>Quantidade:</td>";
                descLinhas += "</tr>";
                
                foreach (DataRow row in dadosTable.Rows)
                {
                    descLinhas += "<td>" + row["LocArmazCodEstr"] + "</td>";
                    descLinhas += "<td>" + row["EstqLocArmazQtd"] + "</td>";
                    descLinhas += "</tr>";
                }
            }

            ltlListaProdutos.Text = descLinhas;
        }
    }
}