using System;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Logistica_New
{
    public partial class RastreamentoWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        PedidoClass objPedidoClass = new PedidoClass();
        HistoricosClass objHistoricosClass = new HistoricosClass();

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

                CarregaHistorico(false);
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            EmpresaDropDownList.Enabled = false;
            NotaFiscalTextBox.Enabled = false;
            ClienteTextBox.Enabled = false;
            PrevisaoTextBox.Enabled = true;

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

            CarregaPrevisaoEntrega();

            objHistoricosClass.IDTipoHistorico = 6;

            EventoDropDownList.DataSource = objHistoricosClass.RetornaEventos();
            EventoDropDownList.DataTextField = "Descricao";
            EventoDropDownList.DataValueField = "IDEvento";
            EventoDropDownList.DataBind();

            objHistoricosClass.IDEvento = Convert.ToInt32(EventoDropDownList.SelectedValue);

            CategoriaDropDownList.DataSource = objHistoricosClass.RetornaEventosCategorias();
            CategoriaDropDownList.DataTextField = "Descricao";
            CategoriaDropDownList.DataValueField = "IDCategoria";
            CategoriaDropDownList.DataBind();
        }

        protected void CarregaPrevisaoEntrega()
        {
            string previsaoEntrega = objPedidoClass.CarregaPrevisaoEntrega();

            if (previsaoEntrega != "")
                previsaoEntrega = Convert.ToDateTime(previsaoEntrega).ToString("yyyy-MM-dd");

            PrevisaoTextBox.Text = previsaoEntrega;
        }

        protected void AtualizarButton_Click(object sender, EventArgs e)
        {
            if (Session["PedidoRastrear"] != null)
                objPedidoClass = (PedidoClass)Session["PedidoRastrear"];

            string erro = objPedidoClass.AtualizaHistoricoRastreio();

            if (erro == "") CarregaHistorico(false);

            if (erro != "") ApresentaMensagem(erro);
            else CarregaDadosNaTela();
        }

        protected void CarregaHistorico(bool carregaSession)
        {
            if (carregaSession)
                if (Session["PedidoRastrear"] != null)
                    objPedidoClass = (PedidoClass)Session["PedidoRastrear"];

            HitoricoLiteral.Text = objPedidoClass.RetornaHistoricoRastreio();
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

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            int IDEmpresa = Convert.ToInt32(((PedidoClass)Session["PedidoRastrear"]).EmpCod),
                NumeroPedidoSAP = ((PedidoClass)Session["PedidoRastrear"]).NumeroPedidoSAP,
                NumeroNotaFiscal = Convert.ToInt32(((PedidoClass)Session["PedidoRastrear"]).NumeroNotaFiscal),
                IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
            string PrevisaoEntrega = "", Tipo = "";

            if (PrevisaoTextBox.Text != null && PrevisaoTextBox.Text != "")
                PrevisaoEntrega = Convert.ToDateTime(PrevisaoTextBox.Text).ToString("yyyy-MM-dd");

            objHistoricosClass.IDTipoHistorico = 6;
            objHistoricosClass.IDEvento = Convert.ToInt32(EventoDropDownList.SelectedValue);
            objHistoricosClass.IDCategoria = Convert.ToInt32(CategoriaDropDownList.SelectedValue);

            objHistoricosClass.Historico = NovoHistoricoTextBox.Text;

            string erro = "";

            if (((System.Web.UI.Control)sender).ID == "AdicionarLinkButton")
            {
                Tipo = "M";

                if (objHistoricosClass.Historico == "")
                    erro = "Informe o histórico.";
            }
            else
            {
                Tipo = "APE"; //Atualiza Previsao Entrega
            }

            if (erro == "") erro = objHistoricosClass.GRAVA_HISTORICO_RASTREIO_PEDIDOS
            (IDEmpresa, NumeroPedidoSAP, NumeroNotaFiscal, IDUsuario, PrevisaoEntrega, Tipo);

            if (erro != "") ApresentaMensagem(erro);

            if (erro == "")
            {
                CarregaHistorico(true);
                CarregaPrevisaoEntrega();
            }
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/RastreioPedidosWebForm.aspx?indmnu=5");
        }
    }
}