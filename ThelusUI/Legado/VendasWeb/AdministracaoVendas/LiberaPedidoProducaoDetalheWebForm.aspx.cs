using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace VendasWeb.AdministracaoVendas
{
    public partial class LiberaPedidoProducaoDetalheWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        AdmVendas objAdmVendas = new AdmVendas();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = false;
            }

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            objAdmVendas = (AdmVendas)Session["objAdmVendas"];

            DataTable ListaLiberaPedidoProducao = objAdmVendas.RetornaListaPedidoLiberacaoProducaoDetalhe();

            if (ListaLiberaPedidoProducao.Rows.Count > 0)
            {
                foreach (DataRow row in ListaLiberaPedidoProducao.Rows)
                {
                    EmpresaTextBox.Text = row["Empresa"].ToString();
                    PedidoSAPTextBox.Text = objAdmVendas.NumeroPedidoSAP.ToString();
                    PedidoCRMTextBox.Text = objAdmVendas.NumeroPedidoCRM.ToString();
                    ClienteTextBox.Text = row["Cliente"].ToString();
                    UtilizacaoTextBox.Text = row["Utilizacao"].ToString();
                    LiberadoProducaoTextBox.Text = row["Liberado"].ToString();
                    DataLancamentoTextBox.Text = row["DataEmissao"].ToString();
                    DataEntregaTextBox.Text = row["DataEntrega"].ToString();
                    EmbarqueImediatoTextBox.Text = row["EmbarqueImediato"].ToString();
                    VendedorTextBox.Text = row["Vendedor"].ToString();
                    HistoricoPedidoTextBox.Text = row["Comentarios"].ToString();
                    IDClienteHiddenField.Value = row["IDCliente"].ToString();
                    IDEmpresaHiddenField.Value = objAdmVendas.IDEmpresa.ToString();
                }
            }

            CarregaGridView();
        }

        protected void CarregaGridView()
        {
            LiberaPedidoGridView.DataSource = objAdmVendas.RetornaListaPedidoLiberacaoProducaoDetalheProdutos();
            LiberaPedidoGridView.DataBind();
            MultiView.Visible = true;
        }

        protected void CarregaDadosDaTela()
        {
            objAdmVendas.IDEmpresa = Convert.ToInt32(IDEmpresaHiddenField.Value);
            objAdmVendas.NumeroPedidoCRM = Convert.ToInt32(PedidoCRMTextBox.Text);
            objAdmVendas.NumeroPedidoSAP = Convert.ToInt32(PedidoSAPTextBox.Text);
            objAdmVendas.Comentarios = NovoHistoricoTextBox.Text;
            objAdmVendas.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"].ToString());
        }

        protected void AprovarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();

            objAdmVendas.Liberacao = 1;

            string erro = objAdmVendas.GravaPedidoLiberacaoProducao();

            if (erro == "")
            {
                funcoesBD mdlFuncoesBD = new funcoesBD();

                erro = mdlFuncoesBD.aprovaPedido
                    (objAdmVendas.IDEmpresa.ToString(), objAdmVendas.NumeroPedidoCRM.ToString(),
                    Session["usuario"].ToString(), IDClienteHiddenField.Value);
            }

            if (erro == "")
            {
                pedido novoPedido = new pedido();

                novoPedido.carregaDadosPedido(objAdmVendas.IDEmpresa.ToString(), objAdmVendas.NumeroPedidoCRM.ToString());

                erro = novoPedido.TransformaEsbocoPedido();

                if (novoPedido.NumeroPedidoSAP != "" && novoPedido.NumeroPedidoSAP != null && novoPedido.NumeroPedidoSAP != "0")
                {
                    erro = novoPedido.AtualizarHistoricoPedidoSAPAPI();
                }
            }

            ApresentaMensagem();
        }

        protected void ReprovarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();

            objAdmVendas.Liberacao = 0;

            ApresentaMensagem(objAdmVendas.GravaPedidoLiberacaoProducao());
        }

        protected void RetornarVendedorLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();

            objAdmVendas.Liberacao = 2;

            ApresentaMensagem(objAdmVendas.GravaPedidoLiberacaoProducao());
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LiberaPedidoGridView.PageIndex = e.NewPageIndex;
            CarregaGridView();
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoVendas/LiberaPedidoProducaoWebForm.aspx?indmnu=3");
        }

        protected void ApresentaMensagem(string erro = "")
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
                Session["Msg"] = "Operação realizada com sucesso.";
                VoltarButton_Click(null, null);
            }
        }
    }
}