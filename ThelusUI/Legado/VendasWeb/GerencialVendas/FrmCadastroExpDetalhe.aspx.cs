using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.GerencialVendas
{
    public partial class FrmCadastroExpDetalhe : System.Web.UI.Page
    {

        SessionClass OBJSessao = new SessionClass();
        clsCadastroExpectativa ObjExpDetalhes = new clsCadastroExpectativa();
        VendedorClass ObjVendedorClass = new VendedorClass();


        protected void Page_Load(object sender, EventArgs e)
        {

            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                Atualiza_Select_Vendedores();//Carregando combo do Vendedor


                NomeLinhaDropDown.DataSource = ObjExpDetalhes.Linha_Lista();
                NomeLinhaDropDown.DataTextField = "LinhaProduto";
                NomeLinhaDropDown.DataValueField = "LinhaProduto";
                NomeLinhaDropDown.DataBind();



                /*Tratar Abrir e fechar Div*/
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";




                this.ControlPainel.refreshVendedor();
            }
        }

        private void Atualiza_Select_Vendedores()
        {
            DataTable Resultado = new DataTable();

            ObjVendedorClass.UsuCod = Session["usuario"].ToString();
            ObjVendedorClass.TodosCodigos = " S";
            Resultado = ObjVendedorClass.Consulta_Vendedor();
        }


        protected void RetornaLinkButton_Click1(object sender, EventArgs e)
        {
            Response.Redirect("FrmCadastroExpectativas.aspx?indmnu=2");
        }

        protected void BuscarLinkButton_Click1(object sender, EventArgs e)
        {

        }


    }
}