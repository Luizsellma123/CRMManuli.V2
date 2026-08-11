using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.financeiro
{
    public partial class DevolucoesWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        ClienteClasse OBJClienteClasse = new ClienteClasse();

        protected void Page_Load(object sender, EventArgs e)
        {
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

            if (Session["ContaCorrenteDetalhe"] != null)
            {
                OBJClienteClasse = (ClienteClasse)Session["ContaCorrenteDetalhe"];
            }

            if (!IsPostBack)
            {
                CarregaDadosNaTela(sender, e);
                CarregaCombos();
                BuscarButton_Click(sender, e);
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela(object sender, EventArgs e)
        {
            CodigoTextoLabel.Text = OBJClienteClasse.CodigoAux.ToString();
            NomeTextoLabel.Text = OBJClienteClasse.NomeCliente.ToString();
            CNPJTextoLabel.Text = OBJClienteClasse.CNPJCliente.ToString();
            VendedorTextoLabel.Text = OBJClienteClasse.VendedorCliente.ToString();
            APagarTextoLabel.Text = OBJClienteClasse.ValorAPagarDev.ToString("C");
            PagoTextoLabel.Text = OBJClienteClasse.ValorPagoDev.ToString("C");
        }

        public void CarregaCombos()
        {
            OBJClienteClasse.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = OBJClienteClasse.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

            OrdenarDropDownList.Items.Insert(0, new ListItem("Selecionar", "0"));
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            DataTable OBJDataTable = new DataTable();

            OBJClienteClasse.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
            OBJClienteClasse.Status = StatusDropDownList.SelectedValue;
            OBJClienteClasse.Ordenar = OrdenarDropDownList.SelectedValue;
            OBJClienteClasse.Tipo = TipoDropDownList.SelectedValue;

            if (VencimentoInicialTextBox.Text != null)
            {
                OBJClienteClasse.VencimentoInicial = VencimentoInicialTextBox.Text;
            }
            if (VencimentoFinalTextBox.Text != null)
            {
                OBJClienteClasse.VencimentoFinal = VencimentoFinalTextBox.Text;
            }

            if (NotaTextBox.Text != null && NotaTextBox.Text != "")
            {
                OBJClienteClasse.NotaFiscal = Convert.ToInt32(NotaTextBox.Text);
            }
            if (ValorTextBox.Text != null && ValorTextBox.Text != "")
            {
                OBJClienteClasse.Valor = Convert.ToInt32(ValorTextBox.Text);
            }

            OBJDataTable = OBJClienteClasse.RecuperaCCDevolucoes();
            CCDevolucoesGridView.DataSource = OBJDataTable;
            CCDevolucoesGridView.DataBind();
            CCDevolucoesMultiView.Visible = true;
        }

        protected void DetalhesLinkButton_Click(object sender, EventArgs e)
        {

        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/financeiro/ContaCorrenteDetalheWebForm.aspx?indmnu=5");
        }

        protected void CCDevolucoesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CCDevolucoesGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }
    }
}