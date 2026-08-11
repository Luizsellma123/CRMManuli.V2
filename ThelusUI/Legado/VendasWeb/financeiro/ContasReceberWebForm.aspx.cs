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
    public partial class ContasReceberWebForm : System.Web.UI.Page
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
            AReceberTextoLabel.Text = OBJClienteClasse.ValorAReceber.ToString("C");
            RecebidoTextoLabel.Text = OBJClienteClasse.ValorRecebido.ToString("C");
            MediaAtrasoTextoLabel.Text = OBJClienteClasse.QuantidadeDiasAtraso.ToString();
        }

        public void CarregaCombos()
        {
            OBJClienteClasse.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = OBJClienteClasse.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

            AtrasoAcimaDropDownList.DataSource = OBJClienteClasse.ListaDiasAtraso();
            AtrasoAcimaDropDownList.DataTextField = "QuantidadeDias";
            AtrasoAcimaDropDownList.DataValueField = "IDDias";
            AtrasoAcimaDropDownList.DataBind();
            AtrasoAcimaDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

            OrdenarDropDownList.Items.Insert(0, new ListItem("Selecionar", "0"));
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            DataTable OBJDataTable = new DataTable();

            OBJClienteClasse.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
            OBJClienteClasse.Status = StatusDropDownList.SelectedValue;
            OBJClienteClasse.QuantidadeDias = Convert.ToInt32(AtrasoAcimaDropDownList.SelectedValue);
            OBJClienteClasse.Ordenar = OrdenarDropDownList.SelectedValue;
            OBJClienteClasse.Tipo = TipoDropDownList.SelectedValue;

            if (VencimentoInicialTextBox.Text != null && VencimentoInicialTextBox.Text != "")
            {
                OBJClienteClasse.VencimentoInicial = VencimentoInicialTextBox.Text;
            }
            else
            {
                OBJClienteClasse.VencimentoInicial = null;
            }

            if (VencimentoFinalTextBox.Text != null && VencimentoFinalTextBox.Text != "")
            {
                OBJClienteClasse.VencimentoFinal = VencimentoFinalTextBox.Text;
            }
            else
            {
                OBJClienteClasse.VencimentoFinal = null;
            }

            if (NotaTextBox.Text != null && NotaTextBox.Text != "")
            {
                OBJClienteClasse.NotaFiscal = Convert.ToInt32(NotaTextBox.Text);
            }
            else
            {
                OBJClienteClasse.NotaFiscal = 0;
            }

            if (ValorTextBox.Text != null && ValorTextBox.Text != "")
            {
                OBJClienteClasse.Valor = Convert.ToDecimal(ValorTextBox.Text);
            }
            else
            {
                OBJClienteClasse.Valor = 0;
            }

            OBJDataTable = OBJClienteClasse.RecuperaCCCReceber();
            CCCReceberGridView.DataSource = OBJDataTable;
            CCCReceberGridView.DataBind();
            CCCReceberMultiView.Visible = true;
        }

        protected void DetalhesLinkButton_Click(object sender, EventArgs e)
        {

        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/financeiro/ContaCorrenteDetalheWebForm.aspx?indmnu=5");
        }

        protected void CCCReceberGridView_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            CCCReceberGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }
    }
}