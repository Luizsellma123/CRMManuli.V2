using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Data;

namespace VendasWeb.GerencialVendas
{
    public partial class FrmCadExpClassDestalhes : System.Web.UI.Page
    {
        clsCadastroExpectativa objExpClassDetalhes = new clsCadastroExpectativa();
        VendedorClass ObjVendedorClass = new VendedorClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                Atualiza_Select_Vendedores();//Carregando combo do Vendedor

                NomeLinhaDropDown.DataSource = objExpClassDetalhes.Linha_Lista();
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
            Response.Redirect("FrmCadastroExpClasses.aspx?indmnu=2");
        }

        protected void BuscarLinkButton_Click1(object sender, EventArgs e)
        {

        }
    }
}