using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Collections.Generic;

namespace VendasWeb.Controladoria
{
    public partial class EmpenhoEstoqueDetalheWebForm : System.Web.UI.Page
    {
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();
        producao objProducao = new producao();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
                BuscarButton_Click(null, null);
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            if (Session["EmpenhoEstoque"] != null)
                objProducao = (producao)Session["EmpenhoEstoque"];

            ProdutoTextBox.Text = objProducao.Produto;

            usuario ObjUsuario = new usuario();
            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.Items.Insert(0, new ListItem("Todas", "0"));

            StatusDropDownList.DataSource = objProducao.RetornaListaStatusEmpenhoEstoque();
            StatusDropDownList.DataTextField = "Descricao";
            StatusDropDownList.DataValueField = "IDStatus";
            StatusDropDownList.DataBind();
            StatusDropDownList.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void CarregaDadosDaTela()
        {
            objProducao.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue == "" ? "0" : EmpresaDropDownList.SelectedValue);
            objProducao.IDStatus = Convert.ToInt32(StatusDropDownList.SelectedValue == "" ? "0" : StatusDropDownList.SelectedValue);
            objProducao.Cliente = ClienteTextBox.Text;
            objProducao.NumeroPedidoCRM = Convert.ToInt32(PedidoCRMTextBox.Text == "" ? "0" : PedidoCRMTextBox.Text);
            objProducao.NumeroPedidoSAP = Convert.ToInt32(PedidoSAPTextBox.Text == "" ? "0" : PedidoSAPTextBox.Text);
            objProducao.DataInicial = DataInicioTextBox.Text == "" ? "" : Convert.ToDateTime(DataInicioTextBox.Text).ToString("yyyy-MM-dd");
            objProducao.DataFinal = DataFimTextBox.Text == "" ? "" : Convert.ToDateTime(DataFimTextBox.Text).ToString("yyyy-MM-dd");
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            if (Session["EmpenhoEstoque"] != null)
                objProducao = (producao)Session["EmpenhoEstoque"];

            CarregaDadosDaTela();

            ControladoriaGridView.DataSource = objProducao.RetornaListaEmpenhoEstoquePedidos();
            ControladoriaGridView.DataBind();
            ControladoriaMultiView.Visible = true;
        }

        protected void CancelarLinkButton_Click(object sender, EventArgs e)
        {
            objProducao.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaLabel")).Text);
            objProducao.IDPedido = Convert.ToInt32(((Label)((Control)sender).FindControl("IDPedidoLabel")).Text);
            objProducao.IDProduto = Convert.ToInt32(((Label)((Control)sender).FindControl("IDProdutoLabel")).Text);
            objProducao.IDEmpenho = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpenhoLabel")).Text);
            objProducao.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            ApresentaMensagem(objProducao.CancelaEmpenhoEstoquePedido());

            BuscarButton_Click(null, null);
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
                erro = "Operação realizada com sucesso.";
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/EmpenhoEstoqueWebForm.aspx?indmnu=5");
        }

        protected void ControladoriaGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ControladoriaGridView.PageIndex = e.NewPageIndex;

            BuscarButton_Click(null, null);
        }
    }
}