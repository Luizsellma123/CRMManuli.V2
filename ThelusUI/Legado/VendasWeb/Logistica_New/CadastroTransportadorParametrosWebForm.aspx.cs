using System;
using System.Data;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Web.UI;

namespace VendasWeb.Logistica_New
{
    public partial class CadastroTransportadorParametrosWebForm : System.Web.UI.Page
    {
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass objSessao = new SessionClass();
        LogisticaClass objLogistica = new LogisticaClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                CarregaDadosNaTela();

                CarregaGridView();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            if (Session["CadastroTransportador"] != null)
                objLogistica = (LogisticaClass)Session["CadastroTransportador"];

            if (objLogistica.Operacao == "Alteracao" || objLogistica.IDTransportador != 0)
            {
                objLogistica.Filtro = objLogistica.IDTransportador.ToString();

                objLogistica.TipoFiltro = "Detalhe";

                DataTable ListaTransportador = objLogistica.RetornaListaTransportador();

                if (ListaTransportador.Rows.Count > 0)
                {
                    foreach (DataRow row in ListaTransportador.Rows)
                    {
                        IDTransportadorHiddenField.Value = row["IDTransportador"].ToString();

                        TransportadorTextBox.Text = row["Descricao"].ToString();
                    }
                }
            }
            else
            {
                this.CadastroTransportadorWebUserControl.LiberaMenus(false);
            }
        }

        protected void CarregaIDsHiddenField()
        {
            objLogistica.IDTransportador = Convert.ToInt32(IDTransportadorHiddenField.Value);
        }

        protected void CarregaGridView()
        {
            objLogistica = new LogisticaClass();

            CarregaIDsHiddenField();

            GridView.DataSource = objLogistica.RetornaListaTransportadorParametros();
            GridView.DataBind();
            MultiView.Visible = true;
        }

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            CarregaIDsHiddenField();

            if (NomeTextBox.Text == "")
                erro = "Informe o nome.";
            else
                objLogistica.Nome = NomeTextBox.Text;

            if (erro == "" && NomeTextBox.Text == "")
                erro = "Informe a descrição.";
            else
                objLogistica.Descricao = DescricaoTextBox.Text;

            if (erro == "" && TextoTextBox.Text == "" && (ValorTextBox.Text == "" || Convert.ToDecimal(ValorTextBox.Text) == 0))
                erro = "Informe o texto ou o valor";

            objLogistica.ValorString = TextoTextBox.Text;

            objLogistica.ValorNumerico = Convert.ToDecimal(ValorTextBox.Text == "" ? "0" : ValorTextBox.Text);

            if (Session["IDUsuario"] != null) objLogistica.IDUsuarioAlteracao = Convert.ToInt32(Session["IDUsuario"].ToString());

            if (erro == "") erro = objLogistica.GravaTransportadorParametros();

            if (erro == "") CarregaGridView();

            ApresentaMensagem(erro);
        }

        protected void CarregaDadosGridView(object sender, EventArgs e)
        {
            CarregaIDsHiddenField();

            objLogistica.IDParametro = Convert.ToInt32(((Label)((Control)sender).FindControl("IDParametroGridViewLabel")).Text);

            objLogistica.ValorString = ((TextBox)((Control)sender).FindControl("ValorStringGridViewTextBox")).Text;

            objLogistica.ValorNumerico = Convert.ToDecimal(((TextBox)((Control)sender).FindControl("ValorNumericoGridViewTextBox")).Text);

            if (Session["IDUsuario"] != null) objLogistica.IDUsuarioAlteracao = Convert.ToInt32(Session["IDUsuario"].ToString());
        }

        protected void ExcluirGridViewLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosGridView(sender, e);

            string erro = objLogistica.ExcluiTransportadorParametros();

            if (erro == "") CarregaGridView();

            ApresentaMensagem(erro);
        }

        protected void ValorStringGridViewTextBox_TextChanged(object sender, EventArgs e)
        {
            CarregaDadosGridView(sender, e);

            string erro = objLogistica.AlteraTransportadorParametros();

            if (erro == "") CarregaGridView();

            ApresentaMensagem(erro);
        }

        protected void ValorNumericoGridViewTextBox_TextChanged(object sender, EventArgs e)
        {
            CarregaDadosGridView(sender, e);

            string erro = objLogistica.AlteraTransportadorParametros();

            if (erro == "") CarregaGridView();

            ApresentaMensagem(erro);
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;

            CarregaGridView();
        }

        protected void ApresentaMensagem(string erro)
        {
            if (erro != "")
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                erro = "Operação realizada com sucesso.";
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/CadastroTransportadorWebForm.aspx?indmnu=5");
        }
    }
}