using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Data;
using VendasWeb.classes;

namespace VendasWeb.GerencialVendas
{
    public partial class FrmCadastroExpectativas : System.Web.UI.Page
    {
        clsCadastroExpectativa objExpectativas = new clsCadastroExpectativa();
        VendedorClass ObjVendedorClass = new VendedorClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                Atualiza_Select_Vendedores();//Carregando combo do Vendedor

                NomeLinhaDropDown.DataSource = objExpectativas.Lista_Expectativa();
                NomeLinhaDropDown.DataTextField = "LinhaProduto";
                NomeLinhaDropDown.DataValueField = "LinhaProduto";
                NomeLinhaDropDown.DataBind();


                /*Tratar Abrir e fechar Div*/
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";




                this.ControlPainel.refreshVendedor();
            }

        }


        protected void Atualiza_Select_Vendedores()
        {
            DataTable Resultado = new DataTable();

            ObjVendedorClass.UsuCod = Session["usuario"].ToString();
            ObjVendedorClass.TodosCodigos = "S";
            Resultado = ObjVendedorClass.Consulta_Vendedor();



            VendedoresSelect.DataSource = Resultado;
            VendedoresSelect.DataTextField = "VendNome";
            VendedoresSelect.DataValueField = "VendCod";
            VendedoresSelect.DataBind();

        }



        protected void ExpectativaGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            ExpectativaGridView.PageIndex = e.NewPageIndex;
            BuscarLinkButton_Click1(null, null);
        }

        protected void BuscarLinkButton_Click1(object sender, EventArgs e)
        {

            objExpectativas = new clsCadastroExpectativa();


            objExpectativas.Mes = MesDropDown.SelectedValue;
            objExpectativas.VendCod = VendedoresSelect.Value;
            objExpectativas.LinhaProduto = NomeLinhaDropDown.SelectedValue;
            objExpectativas.Ano = AnoDropDown.SelectedValue;


            ExpectativaGridView.DataSource = objExpectativas.Linha_Lista();
            ExpectativaGridView.DataBind();
            ExpectativaMultiView.Visible = true;
        }






        protected void btnDeletar_Click(object sender, EventArgs e)
        {
            //Vamos tratar esse cara aqui blz ?
            objExpectativas = new clsCadastroExpectativa();
            objExpectativas.ID_Expectativa = Convert.ToInt32(((Label)((Control)sender).FindControl("ID_ExpectativaLabel")).Text);
            objExpectativas.Linha_Deleta();


            BuscarLinkButton_Click1(null, null);

        }





        protected void NovaLinkButton_Click1(object sender, EventArgs e)
        {
            Response.Redirect("FrmCadastroExpDetalhe.aspx?indmnu=2");
        }



        protected void VendedorDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
