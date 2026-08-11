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
    public partial class TabelaDePrecoProdutoLogWebForm : System.Web.UI.Page
    {
        UtilClass ObjUtilClass = new UtilClass();
        CrmTabelaPrecoClass ObjCrmTabelaPrecoClass = new CrmTabelaPrecoClass();
        CrmTabelaPrecoProdutoClass ObjCrmTabelaPrecoProdutoClass = new CrmTabelaPrecoProdutoClass();
        CrmProdutoClass ObjCrmProdutoClass = new CrmProdutoClass();
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


                

                if (Session["ObjCrmTabelaPrecoClass"] != null && Session["ObjCrmTabelaPrecoProdutoClass"] != null)
                {

                    ObjCrmTabelaPrecoClass = (CrmTabelaPrecoClass)Session["ObjCrmTabelaPrecoClass"];

                    if (ObjCrmTabelaPrecoClass.IDTabela > 0)
                    {
                        ObjCrmTabelaPrecoClass.ManutencaoTabelaPreco();

                        ObjCrmTabelaPrecoProdutoClass = (CrmTabelaPrecoProdutoClass)Session["ObjCrmTabelaPrecoProdutoClass"];
                        ObjCrmProdutoClass = new CrmProdutoClass();
                        ObjCrmProdutoClass.IDProduto = ObjCrmTabelaPrecoProdutoClass.IDProduto;


                        ObjCrmProdutoClass.ManutencaoProduto();


                        CarregaDadosNaTela();
                    }

                }

            }

        }


        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("TabelaDePrecoProdutoWebForm.aspx?indmnu=2");
        }



        public void CarregaDadosNaTela()
        {

            IDTabelaTextBox.Text = ObjCrmTabelaPrecoClass.IDTabela.ToString();
            NomeTextBox.Text = ObjCrmTabelaPrecoClass.Nome;

            
            CodigoProdutoSAPTextBox.Text = ObjCrmProdutoClass.CodigoProdutoSAP;
            NomeProdutoTextBox.Text = ObjCrmProdutoClass.Nome;


            AtualizaGrid();
        }


        public void AtualizaGrid()
        {
            DataTable retornoDados = new DataTable();

            retornoDados = ObjCrmTabelaPrecoProdutoClass.RetornaTabelaPrecoProdLog();

            LogProdutoGridView.DataSource = retornoDados;
            LogProdutoGridView.DataBind();
            
        }

    }
}