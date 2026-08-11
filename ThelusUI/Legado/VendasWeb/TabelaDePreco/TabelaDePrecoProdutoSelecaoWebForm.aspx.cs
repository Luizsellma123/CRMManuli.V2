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
    public partial class TabelaDePrecoProdutoSelecaoWebForm : System.Web.UI.Page
    {
        UtilClass ObjUtilClass = new UtilClass();
        CrmTabelaPrecoClass ObjCrmTabelaPrecoClass = new CrmTabelaPrecoClass();
        SessionClass OBJSessao = new SessionClass();

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

            }

        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            DataTable retornoDados = new DataTable();
            CrmProdutoClass ObjCrmProdutoClassAux = new CrmProdutoClass();

            switch (PesquisarPorDropDownList.SelectedValue)
            {
                case "IdProduto":
                    if (PesquisarPorTextBox.Text != "")
                    {
                        try
                        {
                            ObjCrmProdutoClassAux.IDProduto = Convert.ToInt32(PesquisarPorTextBox.Text);
                        }
                        catch
                        {
                            erro = "O IdProduto precisa ser apenas um número (sem letras, pontos ou caracteres especiais)";
                        }
                    }
                    break;

                case "Codigo SAP":
                    ObjCrmProdutoClassAux.CodigoProdutoSAP = PesquisarPorTextBox.Text;
                    break;

                case "Nome":
                    ObjCrmProdutoClassAux.Nome = PesquisarPorTextBox.Text;
                    break;
            }

            if (erro == "")
            {
                retornoDados = ObjCrmProdutoClassAux.RetornaProduto();

                ProdutoGridView.DataSource = retornoDados;
                ProdutoGridView.DataBind();
                ProdutoMultiView.Visible = true;
            }
            else
            {
                ApresentaMensagem(erro);
            }
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("TabelaDePrecoProdutoWebForm.aspx?indmnu=2");
        }

        protected void Selecionar_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["ObjCrmTabelaPrecoClass"] != null)
            {

                ObjCrmTabelaPrecoClass = (CrmTabelaPrecoClass)Session["ObjCrmTabelaPrecoClass"];

                CrmTabelaPrecoProdutoClass ObjCrmTabelaPrecoProdutoClass = new CrmTabelaPrecoProdutoClass();


                ObjCrmTabelaPrecoProdutoClass.CodigoUsuario = Session["usuario"].ToString();
                ObjCrmTabelaPrecoProdutoClass.IDTabela = ObjCrmTabelaPrecoClass.IDTabela;
                ObjCrmTabelaPrecoProdutoClass.IDProduto = Convert.ToInt32(((Label)((Control)sender).FindControl("IDProdutoLabel")).Text);
                ObjCrmTabelaPrecoProdutoClass.ValorUnitario = 0;
                ObjCrmTabelaPrecoProdutoClass.Status = "Ativo";


                erro = ObjCrmTabelaPrecoProdutoClass.GravaTabelaPrecoProd();



            }
            else
            {
                erro = "Session foi finalizada antes da conclusão da Operação, favor sair e tentar novamente";
            }


            if (erro == "")
            {

                Session["Msg"] = "Produto " + ((Label)((Control)sender).FindControl("NomeLabel")).Text + " Adicionado com Sucesso!";
                RetornarButton_Click(null, null);



            }
            else
            {
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

            }

        }

        protected void ProdutoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProdutoGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }

        protected void ApresentaMensagem(string erro = "")
        {
            if (erro != "")
            {
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

    }
}