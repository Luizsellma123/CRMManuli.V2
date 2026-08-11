using System;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;
using System.Web.UI;

namespace VendasWeb.Logistica_New
{
    public partial class CadastroTransportadorFornecedorWebForm : System.Web.UI.Page
    {
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass objSessao = new SessionClass();
        LogisticaClass objLogistica = new LogisticaClass();
        ClienteClasse objCliente = new ClienteClasse();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                CarregaDadosNaTela();

                CarregaCombos();

                CarregaGridView();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaCombos()
        {
            ClienteDropDownList.DataSource = objCliente.RetornaListaClienteFornecedor();
            ClienteDropDownList.DataValueField = "IDCliente";
            ClienteDropDownList.DataTextField = "Cliente";
            ClienteDropDownList.DataBind();
        }

        protected void CarregaDadosNaTela()
        {
            if (Session["CadastroTransportador"] != null)
                objLogistica = (LogisticaClass)Session["CadastroTransportador"];

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

        protected void CarregaGridView()
        {
            objLogistica.IDTransportador = Convert.ToInt32(IDTransportadorHiddenField.Value);

            objLogistica.IDCliente = 0;

            GridView.DataSource = objLogistica.RetornaListaTransportadorFornecedor();
            GridView.DataBind();
            MultiView.Visible = true;
        }

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            objLogistica.IDTransportador = Convert.ToInt32(IDTransportadorHiddenField.Value);

            objLogistica.IDCliente = Convert.ToInt32(ClienteDropDownList.SelectedValue);

            if (Session["IDUsuario"] != null) objLogistica.IDUsuarioAlteracao = Convert.ToInt32(Session["IDUsuario"].ToString());

            string erro = objLogistica.GravaTransportadorFornecedor();

            if (erro == "") CarregaGridView();

            ApresentaMensagem(erro);
        }

        protected void ExcluirGridViewLinkButton_Click(object sender, EventArgs e)
        {
            objLogistica.IDTransportador = Convert.ToInt32(IDTransportadorHiddenField.Value);

            objLogistica.IDCliente = Convert.ToInt32(((Label)((Control)sender).FindControl("IDClienteGridViewLabel")).Text);

            string erro = objLogistica.ExcluiTransportadorFornecedor();

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
            Response.Redirect("~/Logistica_New/CadastroTransportadorDetalheWebForm.aspx?indmnu=5");
        }
    }
}