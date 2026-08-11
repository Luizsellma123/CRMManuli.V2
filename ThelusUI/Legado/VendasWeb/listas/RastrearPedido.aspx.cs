using System;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.listas
{
    public partial class RastrearPedido : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        PedidoClass objPedidoClass = new PedidoClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

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

                HitoricoLiteral.Text = objPedidoClass.RetornaHistoricoRastreio();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            EmpresaDropDownList.Enabled = false;
            NotaFiscalTextBox.Enabled = false;
            ClienteTextBox.Enabled = false;
            PrevisaoTextBox.Enabled = false;

            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();

            if (Session["PedidoRastrear"] != null)
                objPedidoClass = (PedidoClass)Session["PedidoRastrear"];

            EmpresaDropDownList.SelectedValue = objPedidoClass.EmpCod;

            NotaFiscalTextBox.Text = objPedidoClass.NumeroNotaFiscal;

            ClienteTextBox.Text = objPedidoClass.CarregaCliente();

            PrevisaoTextBox.Text = objPedidoClass.CarregaPrevisaoEntrega();            
        }

        protected void AtualizarButton_Click(object sender, EventArgs e)
        {
            if (Session["PedidoRastrear"] != null)
                objPedidoClass = (PedidoClass)Session["PedidoRastrear"];

            string erro = objPedidoClass.AtualizaHistoricoRastreio();

            if (erro == "")
                HitoricoLiteral.Text = objPedidoClass.RetornaHistoricoRastreio();

            if (erro != "")
                ApresentaMensagem(erro);
            else
                CarregaDadosNaTela();
        }

        protected void ApresentaMensagem(string erro = "")
        {
            if (erro != "")
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
            }
            else
            {
                erro = "Operação realizada com sucesso.";
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
            }

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/listas/FrmListaPedidos.aspx?indmnu=5");
        }

    }
}