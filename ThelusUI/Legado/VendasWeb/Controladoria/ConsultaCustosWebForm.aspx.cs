using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Data;

namespace VendasWeb.Controladoria
{
    public partial class ConsultaCustosWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        CustosClass OBJCustos = new CustosClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }

            if (!IsPostBack)
            {
                /*Tratar Abrir e fechar Div*/
                PainelFiltrosLiteral.Text = "<div id=\"filtros\" class=\"collapse in\" runat=\"server\">";

                AtualizaGrid();
            }

        }

        public void AtualizaGrid()
        {
            DataTable Dados = new DataTable();

            OBJCustos.Empresa = Convert.ToInt32(EmpresaDropDown.SelectedValue);
            OBJCustos.FiltroProduto = ProdutoTextBox.Text;

            Dados = OBJCustos.CarregaCustos();
            
            CustosGridView.DataSource = Dados;
            CustosGridView.DataBind();
            CustosMultiView.Visible = true;

        }

        protected void CustosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CustosGridView.PageIndex = e.NewPageIndex;
            AtualizaGrid();
        }

        protected void ListarLinkButton_Click(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        protected void NovoProdutoLinkButton_Click(object sender, EventArgs e)
        {
            OBJCustos.Operacao = "inclusao";
            Session["OBJCustos"] = OBJCustos;
            Response.Redirect("IncluiProdutoCustoWebForm.aspx?indmnu=3");
        }

        protected void AcessarLinkButton_Click(object sender, EventArgs e)
        {
            OBJCustos.Empresa = Convert.ToInt32(EmpresaDropDown.SelectedValue);
            OBJCustos.CodigoProduto =((Label)((Control)sender).FindControl("CodigoProdutoGrid")).Text;
            OBJCustos.Operacao = "alteracao";
            Session["OBJCustos"] = OBJCustos;
            Response.Redirect("IncluiProdutoCustoWebForm.aspx?indmnu=3");
        }
    }
}