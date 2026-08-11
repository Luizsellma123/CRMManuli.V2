using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;

namespace VendasWeb.listas
{
    public partial class FrmListaPedidosProdutos : System.Web.UI.Page
    {
        GerencialVendas.PedidoClass PedidoClass = new GerencialVendas.PedidoClass();
        GerencialVendas.FiltroClass ObjFiltroClass = new GerencialVendas.FiltroClass();
        produto OBJProduto = new produto();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (Session["ObjFiltroClass"] != null)
            {
                ObjFiltroClass = (GerencialVendas.FiltroClass)Session["ObjFiltroClass"];
            }

            if (!IsPostBack)
            {
                if (Session["ObjFiltroClass"] != null)
                {
                    ObjFiltroClass = (GerencialVendas.FiltroClass)Session["ObjFiltroClass"];
                }
                /*Tratar Abrir e fechar Div*/
                collapseLiteral.Text = "<div id=\"filtros\" class=\"collapse in\" runat=\"server\">";
            }
        }

        protected void ListaPedidosGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            ListaProdutosGridView.PageIndex = e.NewPageIndex;
            buscar_produtos();

        }

        protected void ListarLinkButton_Click(object sender, EventArgs e)
        {
            //Chama função para buscar filtros
            buscar_produtos();

        }

        public void buscar_produtos()
        {

            if (TipoDropDownList.SelectedValue == "1")
            {
                PedidoClass.ProdCodEstr = FiltroTextBox.Text.ToString();
                PedidoClass.ProdNome = "";
            }
            else
            {
                PedidoClass.ProdNome = FiltroTextBox.Text.ToString();
                PedidoClass.ProdCodEstr = "";
            }
            
            ListaProdutosGridView.DataSource = PedidoClass.Lista_Produtos_Ativos();
            ListaProdutosGridView.DataBind();

            ProdutosMultiView.Visible = true;
        }

        protected void ProdutoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string campoCheck = "";
            campoCheck = ((CheckBox)((Control)sender).FindControl("ProdutoCheckBox")).Checked.ToString();
            OBJProduto.codigoProduto = ((Label)((Control)sender).FindControl("ProdCodEstrCodLabel")).Text.ToString();

            ObjFiltroClass.incluiItem(OBJProduto);

            Session["ObjFiltroClass"] = ObjFiltroClass;
        }

        protected void ListaProdutosGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            string campoCheck = "";
            campoCheck = ((CheckBox)((Control)sender).FindControl("ProdutoCheckBox")).Text;
        }

        protected void VoltarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/listas/FrmListaPedidos.aspx?indmnu=2");
        }
    }
}