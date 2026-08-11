using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Data;

namespace VendasWeb.Entidades
{
    public partial class frmExpectativaPedidosVendedor : System.Web.UI.Page
    {
        VendedorClass ObjVendedorClass = new VendedorClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                VendNomeLabel.Text = Session["VendNome"].ToString();

                atualizarGrid();
            }
        }

        protected void atualizarGrid()
        {
            ExpectativaMultiView.Visible = true;

            ObjVendedorClass.VendCod = Session["VendCod"].ToString();
            ObjVendedorClass.AnoPesquisa = "2016"; 

            ListaExpectativaGridView.DataSource = ObjVendedorClass.Listar_Expectativas();
            ListaExpectativaGridView.DataBind();
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            ExpectativaMultiView.Visible = true;

            ObjVendedorClass.CodExpectativa = 0;
            ObjVendedorClass.VendCod = Session["VendCod"].ToString();
            /*ObjVendedorClass.Mes = MesTextBox.Text;
            ObjVendedorClass.Ano = AnoTextBox.Text;
            ObjVendedorClass.UserLinhaProdutoLista = FamiliaDropDownList.SelectedValue;
            ObjVendedorClass.QtdExpectativa = Convert.ToDecimal(QuantidadeTextBox.Text);*/

            ObjVendedorClass.Alterar_Expectativa();

            atualizarGrid();
        }

        protected void AlterarLinkButton_Click(object sender, EventArgs e)
        {
            //ExpectativaMultiView.Visible = true;

            ObjVendedorClass.CodExpectativa = Convert.ToInt32(((Label)((Control)sender).FindControl("CodigoLabel")).Text);
            ObjVendedorClass.QtdJaneiro = Convert.ToDouble(((TextBox)((Control)sender).FindControl("QtdJaneiroTextBox")).Text);
            ObjVendedorClass.QtdFevereiro = Convert.ToDouble(((TextBox)((Control)sender).FindControl("QtdFevereiroTextBox")).Text);
            ObjVendedorClass.QtdMarco = Convert.ToDouble(((TextBox)((Control)sender).FindControl("QtdMarcoTextBox")).Text);
            ObjVendedorClass.QtdAbril = Convert.ToDouble(((TextBox)((Control)sender).FindControl("QtdAbrilTextBox")).Text);
            ObjVendedorClass.QtdMaio = Convert.ToDouble(((TextBox)((Control)sender).FindControl("QtdMaioTextBox")).Text);
            ObjVendedorClass.QtdJunho = Convert.ToDouble(((TextBox)((Control)sender).FindControl("QtdJunhoTextBox")).Text);
            ObjVendedorClass.QtdJulho = Convert.ToDouble(((TextBox)((Control)sender).FindControl("QtdJulhoTextBox")).Text);
            ObjVendedorClass.QtdAgosto = Convert.ToDouble(((TextBox)((Control)sender).FindControl("QtdAgostoTextBox")).Text);
            ObjVendedorClass.QtdSetembro = Convert.ToDouble(((TextBox)((Control)sender).FindControl("QtdSetembroTextBox")).Text);
            ObjVendedorClass.QtdOutubro = Convert.ToDouble(((TextBox)((Control)sender).FindControl("QtdOutubroTextBox")).Text);
            ObjVendedorClass.QtdNovembro = Convert.ToDouble(((TextBox)((Control)sender).FindControl("QtdNovembroTextBox")).Text);
            ObjVendedorClass.QtdDezembro = Convert.ToDouble(((TextBox)((Control)sender).FindControl("QtdDezembroTextBox")).Text);

            ObjVendedorClass.Alterar_Expectativa();

            atualizarGrid();
        }

        protected void ListarLinkButton_Click(object sender, EventArgs e)
        {
            atualizarGrid();
        }

        protected void ExcluirButton_Click(object sender, EventArgs e)
        {
            ObjVendedorClass.CodExpectativa = Convert.ToInt32(((Label)((Control)sender).FindControl("CodigoLabel")).Text);

            ObjVendedorClass.Excluir_Expectativa();

            atualizarGrid();
        }

        protected void ListaExpectativaGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            ListaExpectativaGridView.PageIndex = e.NewPageIndex;
            atualizarGrid();
        }
    }
}