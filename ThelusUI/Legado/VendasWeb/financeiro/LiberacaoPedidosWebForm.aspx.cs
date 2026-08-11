using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;

namespace VendasWeb.financeiro
{
    public partial class LiberacaoPedidosWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        FiltroClass ObjFiltroClass = new FiltroClass();
        UtilClass ObjUtilClass = new UtilClass();
        funcoes mdlFuncoes = new funcoes();
        FinanceiroClass OBJFinanceiro = new FinanceiroClass();

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
                CarregaDatas();

                //Inserindo datasource para dropdown empresa
                EmpresaDropDownList.DataSource = mdlFuncoes.Consulta_Empresa(Session["usuario"].ToString());
                EmpresaDropDownList.DataValueField = "IDEmpresa";
                EmpresaDropDownList.DataTextField = "NomeEmpresa";
                EmpresaDropDownList.DataBind();

                //Inserindo valor padrão de dropdown empresa
                EmpresaDropDownList.Items.Insert(0, new ListItem("Todas", ""));
                EmpresaDropDownList.Focus();

                if (Session["ObjFiltroClass"] != null)
                {
                    ObjFiltroClass = (FiltroClass)Session["ObjFiltroClass"];
                    EmpresaDropDownList.SelectedValue = ObjFiltroClass.FinanceiroEmpresa;
                    SituacaoDropDownList.SelectedValue = ObjFiltroClass.FinanceiroSituacao;
                    PedidoCRMTextBox.Text = ObjFiltroClass.FinanceiroPedidoCRM;
                    PedidoSAPTextBox.Text = ObjFiltroClass.FinanceiroPedidoSAP;
                    NumeroEsbocoTextBox.Text = ObjFiltroClass.FinanceiroEsbocoSAP;

                    Session["ObjFiltroClass"] = null;

                    BuscarButton_Click(sender, e);

                }

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

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            OBJFinanceiro.CodigoEmpresa = EmpresaDropDownList.SelectedValue;
            OBJFinanceiro.NumeroPedidoCRM = PedidoCRMTextBox.Text;
            OBJFinanceiro.NumeroPedidoSAP = PedidoSAPTextBox.Text;
            OBJFinanceiro.NumeroEsbocoSAP = NumeroEsbocoTextBox.Text;
            OBJFinanceiro.StatusPedidos = SituacaoDropDownList.SelectedValue;
            OBJFinanceiro.ConsultaCliente = ClienteTextBox.Text;

            OBJFinanceiro.DataInicial = DataInicialTextBox.Text == "" ? "" : Convert.ToDateTime(DataInicialTextBox.Text).ToString("yyyy-MM-dd");
            OBJFinanceiro.DataFinal = DataFinalTextBox.Text == "" ? "" : Convert.ToDateTime(DataFinalTextBox.Text).ToString("yyyy-MM-dd");

            if (OBJFinanceiro.DataInicial == "")
            {
                ApresentaMensagem("Informe a data inicial");
            }
            else if (OBJFinanceiro.DataFinal == "")
            {
                ApresentaMensagem("Informe a data final");
            }
            else
            {
                DataTable OBJDataTable = new DataTable();
                OBJDataTable = OBJFinanceiro.RecuperaPedidosSAP();
                PedidosGridView.DataSource = OBJDataTable;
                PedidosGridView.DataBind();
                PedidosMultiView.Visible = true;
            }
        }

        protected void AcessarButton_Click(object sender, EventArgs e)
        {
            OBJFinanceiro.NumeroEsbocoSAP = ((Label)((Control)sender).FindControl("ChaveEsbocoLabel")).Text;
            OBJFinanceiro.SituacaoPedido = ((Label)((Control)sender).FindControl("SituacaoLabel")).Text;
            OBJFinanceiro.NumeroPedidoSAP = ((Label)((Control)sender).FindControl("PedidoSAPLabel")).Text;

            //Grava filtros em memória
            ObjFiltroClass.FinanceiroEmpresa = EmpresaDropDownList.SelectedValue;
            ObjFiltroClass.FinanceiroSituacao = SituacaoDropDownList.SelectedValue;
            ObjFiltroClass.FinanceiroPedidoCRM = PedidoCRMTextBox.Text;
            ObjFiltroClass.FinanceiroPedidoSAP = PedidoSAPTextBox.Text;
            ObjFiltroClass.FinanceiroEsbocoSAP = NumeroEsbocoTextBox.Text;
            Session["ObjFiltroClass"] = ObjFiltroClass;

            //Grava Objeto em memória
            Session["OBJFinanceiro"] = OBJFinanceiro;

            Response.Redirect("LiberacaoPedidosDetalheWebForm.aspx?indmnu=2");
        }

        protected void PedidosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PedidosGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
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
    }
}