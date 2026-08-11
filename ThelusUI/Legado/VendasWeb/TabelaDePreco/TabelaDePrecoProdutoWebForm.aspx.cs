using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;


namespace VendasWeb.TabelaDePreco
{
    public partial class TabelaDePrecoProdutoWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        CrmTabelaPrecoClass ObjCrmTabelaPrecoClass = new CrmTabelaPrecoClass();
        CrmTabelaPrecoProdutoClass ObjCrmTabelaPrecoProdutoClass = new CrmTabelaPrecoProdutoClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

            if (Session["Msg"] != null)
            {

                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;

            }


            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"false\">";

                Session["ObjCrmTabelaPrecoProdutoClass"] = null;

                if (Session["ObjCrmTabelaPrecoClass"] != null)
                {

                    ObjCrmTabelaPrecoClass = (CrmTabelaPrecoClass)Session["ObjCrmTabelaPrecoClass"];

                    if (ObjCrmTabelaPrecoClass.IDTabela > 0)
                    {
                        ObjCrmTabelaPrecoClass.ManutencaoTabelaPreco();

                        CarregaDadosNaTela();
                    }

                }

            }


        }


        public void CarregaDadosNaTela()
        {

            IDTabelaTextBox.Text = ObjCrmTabelaPrecoClass.IDTabela.ToString();
            NomeTextBox.Text = ObjCrmTabelaPrecoClass.Nome;

            AtualizaGrid();
        }


        public void AtualizaGrid()
        {
            DataTable retornoDados = new DataTable();

            ObjCrmTabelaPrecoProdutoClass = new CrmTabelaPrecoProdutoClass();
            ObjCrmTabelaPrecoProdutoClass.IDTabela = ((CrmTabelaPrecoClass)Session["ObjCrmTabelaPrecoClass"]).IDTabela;


            

            switch (PesquisarPorDropDownList.SelectedValue)
            {
                case "IdProduto":
                    if (PesquisarPorTextBox.Text != "")
                    {
                        ObjCrmTabelaPrecoProdutoClass.IDProduto = Convert.ToInt32(PesquisarPorTextBox.Text);
                    }
                    break;

                case "Codigo SAP":
                    ObjCrmTabelaPrecoProdutoClass.CodigoProdutoSAP = PesquisarPorTextBox.Text;
                    break;

                case "Nome":
                    ObjCrmTabelaPrecoProdutoClass.NomeProduto = PesquisarPorTextBox.Text;
                    break;
            }


            retornoDados = ObjCrmTabelaPrecoProdutoClass.RetornaTabelaPrecoProd();

            ProdutoGridView.DataSource = retornoDados;
            ProdutoGridView.DataBind();
            ProdutoMultiView.Visible = true;
        }


        protected void RetornarButton_Click(object sender, EventArgs e)
        {

            Response.Redirect("TabelaDePrecoDetalheWebForm.aspx?indmnu=2");

        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {

            string erro = "";

            if (Session["ObjCrmTabelaPrecoClass"] != null)
            {
                ObjCrmTabelaPrecoClass = (CrmTabelaPrecoClass)Session["ObjCrmTabelaPrecoClass"];

                ObjCrmTabelaPrecoProdutoClass = new CrmTabelaPrecoProdutoClass();

                ObjCrmTabelaPrecoProdutoClass.CodigoUsuario = Session["usuario"].ToString();
                ObjCrmTabelaPrecoProdutoClass.IDTabela = Convert.ToInt32(((Label)((Control)sender).FindControl("IDTabelaLabel")).Text);
                ObjCrmTabelaPrecoProdutoClass.IDProduto = Convert.ToInt32(((Label)((Control)sender).FindControl("IDProdutoLabel")).Text);
                

                erro = ObjCrmTabelaPrecoProdutoClass.ExcluiTabelaPrecoProd();

            }
            else
            {
                erro = "Session foi finalizada antes da conclusão da Operação, favor sair e tentar novamente";
            }

            if (erro == "")
            {

                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Produto " + ((Label)((Control)sender).FindControl("CodigoProdutoSAPLabel")).Text + " Excluido com Sucesso!", true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                AtualizaGrid();

            }
            else
            {
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

            }

        }


        protected void ProdutoGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            ProdutoGridView.PageIndex = e.NewPageIndex;
            AtualizaGrid();
        }

        protected void NovoProdutoButton_Click(object sender, EventArgs e)
        {
            if (Session["ObjCrmTabelaPrecoClass"] != null)
            {

                ObjCrmTabelaPrecoClass = (CrmTabelaPrecoClass)Session["ObjCrmTabelaPrecoClass"];
                Session["ObjCrmTabelaPrecoClass"] = ObjCrmTabelaPrecoClass;

                Response.Redirect("TabelaDePrecoProdutoSelecaoWebForm.aspx?indmnu=2");
            }
            else
            {
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro("Session foi finalizada antes da conclusão da Operação, favor sair e tentar novamente", true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void ValorUnitarioTextBox_TextChanged(object sender, EventArgs e)
        {
            Atualiza_Dados_Produto_Grid(sender, e);
        }

        public void Atualiza_Dados_Produto_Grid(object sender, EventArgs e)
        {
            //Atualiza Produto a Produto
            string erro = "";

            if (Session["ObjCrmTabelaPrecoClass"] != null)
            {
                ObjCrmTabelaPrecoClass = (CrmTabelaPrecoClass)Session["ObjCrmTabelaPrecoClass"];

                ObjCrmTabelaPrecoProdutoClass = new CrmTabelaPrecoProdutoClass();

                ObjCrmTabelaPrecoProdutoClass.CodigoUsuario = Session["usuario"].ToString();
                ObjCrmTabelaPrecoProdutoClass.IDTabela = Convert.ToInt32(((Label)((Control)sender).FindControl("IDTabelaLabel")).Text);
                ObjCrmTabelaPrecoProdutoClass.IDProduto = Convert.ToInt32(((Label)((Control)sender).FindControl("IDProdutoLabel")).Text);
                ObjCrmTabelaPrecoProdutoClass.ValorUnitario = Convert.ToDecimal(((TextBox)((Control)sender).FindControl("ValorUnitarioTextBox")).Text);
                ObjCrmTabelaPrecoProdutoClass.Status = ((DropDownList)((Control)sender).FindControl("StatusDropDownList")).SelectedValue;


                erro = ObjCrmTabelaPrecoProdutoClass.GravaTabelaPrecoProd();

            }
            else
            {
                erro = "Session foi finalizada antes da conclusão da Operação, favor sair e tentar novamente";
            }

            if (erro == "")
            {

                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Dados do Produto " + ((Label)((Control)sender).FindControl("CodigoProdutoSAPLabel")).Text + " com Sucesso!", true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                AtualizaGrid();

            }
            else
            {
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

            }

        }

        protected void StatusDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Atualiza_Dados_Produto_Grid(sender, e);
        }


        protected void ProdutoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //novoPedido = new  pedido();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                DropDownList ddlCategories = e.Row.FindControl("StatusDropDownList") as DropDownList;
                if (ddlCategories != null)
                {
                    
                    ddlCategories.SelectedValue = drv["Status"].ToString();
                }
            }

        }

        protected void LogButton_Click(object sender, EventArgs e)
        {
            ObjCrmTabelaPrecoProdutoClass = new CrmTabelaPrecoProdutoClass();

            ObjCrmTabelaPrecoProdutoClass.CodigoUsuario = Session["usuario"].ToString();
            ObjCrmTabelaPrecoProdutoClass.IDTabela = Convert.ToInt32(((Label)((Control)sender).FindControl("IDTabelaLabel")).Text);
            ObjCrmTabelaPrecoProdutoClass.IDProduto = Convert.ToInt32(((Label)((Control)sender).FindControl("IDProdutoLabel")).Text);

            Session["ObjCrmTabelaPrecoProdutoClass"] = ObjCrmTabelaPrecoProdutoClass;

            Response.Redirect("TabelaDePrecoProdutoLogWebForm.aspx?indmnu=2");

        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            AtualizaGrid();
        }
    }
}