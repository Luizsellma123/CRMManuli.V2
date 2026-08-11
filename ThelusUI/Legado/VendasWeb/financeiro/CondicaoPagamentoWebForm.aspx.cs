using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.financeiro
{
    public partial class CondicaoPagamentoWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        FinanceiroClass OBJFinanceiro = new FinanceiroClass();

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
                Atualizar_Grid();
            }

            /*Tratar Abrir e fechar Div*/
            PainelFiltrosLiteral.Text = "<div id=\"filtros\" class=\"collapse in\" runat=\"server\">";

        }

        public void Atualizar_Grid()
        {
            DataTable OBJDataTable = new DataTable();
            OBJFinanceiro.ValorConsulta = CondicaoPagamentoTextBox.Text;
            
            OBJDataTable = OBJFinanceiro.RetornaCondicoesPagamentoConfiguracao();
            CondicoesGridView.DataSource = OBJDataTable;
            CondicoesGridView.DataBind();
            CondicoesMultiView.Visible = true;
        }

        protected void CondicoesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CondicoesGridView.PageIndex = e.NewPageIndex;
            Atualizar_Grid();
        }

        protected void LiberadoPoliticaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Atualiza Dados do usuário
            AtualizaDadosUsuario(sender, e);

            GridViewRow row = ((Control)sender).Parent.Parent.Parent.Parent as GridViewRow;

            //Recarrega Tela
            Atualizar_Grid();

            CondicoesGridView.Rows[row.RowIndex].Focus(); 
        }

        protected void CondicaoAVistaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Atualiza Dados do usuário
            AtualizaDadosUsuario(sender, e);

            GridViewRow row = ((Control)sender).Parent.Parent.Parent.Parent as GridViewRow;

            //Recarrega Tela
            Atualizar_Grid();

            CondicoesGridView.Rows[row.RowIndex].Focus();
        }

        public void AtualizaDadosUsuario(object sender, EventArgs e)
        {
            OBJFinanceiro.IDCondicaoPagamento = Convert.ToInt32(((Label)((Control)sender).FindControl("IDCondPagLabel")).Text);
            OBJFinanceiro.LiberadoPolitica = ((CheckBox)((Control)sender).FindControl("LiberadoPoliticaCheckBox")).Checked;
            OBJFinanceiro.CondicaoAVista = ((CheckBox)((Control)sender).FindControl("CondicaoAVistaCheckBox")).Checked;

            OBJFinanceiro.GravaConfiguracaoCondicaoPagamento();

        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            //Recarrega Tela
            Atualizar_Grid();
        }
    }
}