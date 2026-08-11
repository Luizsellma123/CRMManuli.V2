using System;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Web.UI;

namespace VendasWeb.Logistica_New
{
    public partial class RastreioPedidosWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass objUtilClass = new UtilClass();
        funcoes objFuncoes = new funcoes();
        PedidoClass objPedidoClass = new PedidoClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                CarregaDatas();

                CarregaDadosNaTela();

                BuscarLinkButton_Click(null, null);
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDatas()
        {
            DateTime hoje = DateTime.Today;

            DateTime primeiroDiaDoAno = new DateTime(hoje.Year, 1, 1);

            DataInicialTextBox.Text = primeiroDiaDoAno.ToString("yyyy-MM-dd");

            DataFinalTextBox.Text = hoje.ToString("yyyy-MM-dd");
        }

        protected void CarregaDadosNaTela()
        {
            EmpresaDropDownList.DataSource = objFuncoes.Consultar_Empresas();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();

            StatusDropDownList.DataSource = objFuncoes.Consulta_ListaStatus_Ped_Venda();
            StatusDropDownList.DataTextField = "DescricaoStatus";
            StatusDropDownList.DataValueField = "IDStatus";
            StatusDropDownList.DataBind();
            StatusDropDownList.Items.Insert(0, "Todos");
            StatusDropDownList.SelectedIndex = 6;
        }

        protected void CarregaDadosDaTela()
        {
            objPedidoClass = new PedidoClass();

            objPedidoClass.EmpCod = EmpresaDropDownList.SelectedItem.Value;

            objPedidoClass.PedVendaStatDescr = StatusDropDownList.SelectedValue.ToString();

            objPedidoClass.UsuCod = Session["usuario"].ToString();

            objPedidoClass.valorConsulta = FiltroTextBox.Text;

            objPedidoClass.DataInicial = DataInicialTextBox.Text == "" ? "" : Convert.ToDateTime(DataInicialTextBox.Text).ToString("yyyy-MM-dd");
            objPedidoClass.DataFinal = DataFinalTextBox.Text == "" ? "" : Convert.ToDateTime(DataFinalTextBox.Text).ToString("yyyy-MM-dd");

            switch (FiltroDropDownList.SelectedValue.ToString())
            {
                case "1":
                    objPedidoClass.EntCod = FiltroTextBox.Text;
                    break;

                case "2":
                    objPedidoClass.EntNome = FiltroTextBox.Text;
                    break;

                case "3":
                    objPedidoClass.PedVendaNum = FiltroTextBox.Text;
                    break;

                case "4":
                    objPedidoClass.NumeroNotaFiscal = FiltroTextBox.Text;
                    break;

                case "5":
                    objPedidoClass.CodigoProdutoSAP = FiltroTextBox.Text;
                    break;
            }
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();

            objPedidoClass.Consulta_Copia_Pedido();

            RastreioPedidosGridView.DataSource = objPedidoClass.Lista_Pedidos();

            RastreioPedidosGridView.DataBind();

            RastreioPedidosMultiView.Visible = true;
        }

        protected void RastreioPedidosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RastreioPedidosGridView.PageIndex = e.NewPageIndex;
            BuscarLinkButton_Click(null, null);
        }

        protected void RastrearLinkButton_Click(object sender, EventArgs e)
        {
            PedidoClass objPedidoClass = new PedidoClass();

            try
            {
                objPedidoClass.EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;

                objPedidoClass.NumeroPedidoSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("NumeroPedidoSAPLabel")).Text ?? "0");

                objPedidoClass.NumeroNotaFiscal = ((Label)((Control)sender).FindControl("NotaFiscalLabel")).Text ?? "0";

                if (objPedidoClass.NumeroPedidoSAP == 0)
                {
                    ApresentaMensagem("O pedido não pode ser rastreado sem ter gerado Numero Pedido SAP");
                }
                else if (objPedidoClass.NumeroNotaFiscal == "0")
                {
                    ApresentaMensagem("O pedido não pode ser rastreado sem ter Numero da Nota Fiscal");
                }
                else
                {
                    Session["PedidoRastrear"] = objPedidoClass;

                    Response.Redirect("~/Logistica_New/RastreamentoWebForm.aspx?indmnu=5");
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }
        }

        protected void ApresentaMensagem(string erro = "")
        {
            if (erro != "")
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = objUtilClass.MenssagemErro(erro, true);
            }
            else
            {
                erro = "Operação realizada com sucesso.";
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = objUtilClass.MenssagemSucesso(erro, true);
            }

           ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
        }

        protected void ImportacaoRastreioLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/ImportacaoRastreioWebForm.aspx?indmnu=5");
        }
    }
}