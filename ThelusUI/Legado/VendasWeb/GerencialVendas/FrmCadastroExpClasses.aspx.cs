using System;
using System.Collections.Generic;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Data;


namespace VendasWeb.GerencialVendas
{
    public partial class FrmCadastroExpClasses : System.Web.UI.Page
    {
        clsCadastroExpectativa objExpClasses = new clsCadastroExpectativa();
        VendedorClass ObjVendedorClass = new VendedorClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {

            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                Atualiza_Select_Vendedores();//Carregando combo do Vendedor

                NomeLinhaDropDown.DataSource = objExpClasses.Linha_Lista();
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


        protected void ExpectativaGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            // ExpectativaGridView.PageIndex = e.NewPageIndex;
            BuscarLinkButton_Click1(null, null);
        }

        protected void BuscarLinkButton_Click1(object sender, EventArgs e)
        {
            objExpClasses.Mes = MesDropDown.SelectedValue;
            objExpClasses.VendClasseCod = VendedoresSelect.Value;
            objExpClasses.LinhaProduto = NomeLinhaDropDown.SelectedValue;
            objExpClasses.Ano = AnoDropDown.SelectedValue;

        }



        protected void btnDeletar_Click(object sender, EventArgs e)
        {
            //Vamos tratar esse cara aqui blz ?
            objExpClasses = new clsCadastroExpectativa();
            objExpClasses.ID_Expectativa = Convert.ToInt32(((Label)((Control)sender).FindControl("ID_ExpectativaLabel")).Text);
            objExpClasses.Linha_Deleta();


            BuscarLinkButton_Click1(null, null);

        }



        protected void NovaLinkButton_Click1(object sender, EventArgs e)
        {
            Response.Redirect("FrmCadExpClassDestalhes.aspx?indmnu=2");
        }



        protected void VendedorDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ExpectativaMultiView_ActiveViewChanged(object sender, EventArgs e)
        {

        }





    }
}

