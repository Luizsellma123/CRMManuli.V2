using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using VendasWeb.classes.GerencialVendas;

namespace VendasWeb.GerencialVendas
{
    public partial class FrmMargensProduto : System.Web.UI.Page
    {
        clsMargensProdutos objProduto = new clsMargensProdutos();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                this.ControlPainel.refreshVendedor();

                objProduto.UsuCod = Session["usuario"].ToString();

                EmpresaDropDown.DataSource = objProduto.Lista_Empresa();
                EmpresaDropDown.DataTextField = "EmpNome";
                EmpresaDropDown.DataValueField = "EmpCod";
                EmpresaDropDown.DataBind();

                /*Tratar Abrir e fechar Div*/
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

            }



        }


        protected void ProdutosGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            ProdutosGridView.PageIndex = e.NewPageIndex;
            BuscarLinkButton_Click1(null, null);
        }

        protected void BuscarLinkButton_Click1(object sender, EventArgs e)
        {
            objProduto = new clsMargensProdutos();
            string consulta = "";

            if (CheckBox1.Checked)
            {

                consulta = "Todos";
            }

            if (CheckBox2.Checked)
            {
                consulta = "Vigentes";
            }

            if (CheckBox3.Checked)
            {
                consulta = "ForadeVigencia";
            }

            if (CheckBox2.Checked || CheckBox3.Checked)
            {
                consulta = "Todos";
            }



            if (TipoDropDown.SelectedValue == "2")
            {
                objProduto.ProdCodEstr = txtValor.Text.ToString();
                objProduto.ProdNome = "";
            }
            else
            {

                objProduto.ProdNome = txtValor.Text.ToString();
                objProduto.ProdCodEstr = "";
            }



            objProduto.Empresa = EmpresaDropDown.ToString();
            objProduto.ProdNome = TipoDropDown.ToString();
            objProduto.DataVigencia = TipoDropDown.ToString();
            objProduto.Busca = consulta.ToString();

            ProdutosGridView.DataSource = objProduto.Lista_Produtos();
            ProdutosGridView.DataBind();
            ProdutosMultiView.Visible = true;

        }

        protected void NovoLinkButton_Click1(object sender, EventArgs e)
        {

        }

        protected void btnEncerrar_Click(object sender, EventArgs e)
        {

        }



    }
}