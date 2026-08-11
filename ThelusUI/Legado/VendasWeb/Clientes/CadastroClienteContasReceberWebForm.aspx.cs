using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.Clientes
{
    public partial class CadastroClienteContasReceberWebForm : System.Web.UI.Page
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

            if (Session["ClienteClasse"] != null)
            {
                OBJClienteClasse = (ClienteClasse)Session["ClienteClasse"];
            }

            if (!IsPostBack)
            {
                CarregaDadosNaTela(sender, e);
                CarregaCombos();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela(object sender, EventArgs e)
        {
            CodigoTextoLabel.Text = OBJClienteClasse.CodigoCliente.ToString();
            NomeTextoLabel.Text = OBJClienteClasse.NomeCliente.ToString();

            OBJClienteClasse.RecuperaCodigoClienteSAP();

            OBJClienteClasse.RecuperaValorAReceberSAP();
            AReceberTextoLabel.Text = OBJClienteClasse.ValorAReceber.ToString("C");

            OBJClienteClasse.RecuperaQuantidadeDiasAtrasoSAP();
            MediaAtrasoTextoLabel.Text = OBJClienteClasse.QuantidadeDiasAtraso.ToString();

            DataTable OBJDataTable = new DataTable();
            OBJDataTable = OBJClienteClasse.RecuperaContaCorrenteClienteSAP();

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    if (Convert.ToDecimal(row["LimiteCredito"]) > 0)
                    {
                        LimiteCreditoTextoLabel.Text = (Convert.ToDecimal(row["LimiteCredito"])).ToString("C");
                    }
                    else
                    {
                        LimiteCreditoTextoLabel.Text = "0";
                    }
                }
            }

            DataTable RetornoDados = new DataTable();
            RetornoDados = OBJClienteClasse.LimiteCreditoTomado();

            if (RetornoDados.Rows.Count > 0)
            {
                foreach (DataRow row in RetornoDados.Rows)
                {
                    if (Convert.ToDecimal(row["total"]) > 0)
                    {
                        limiteDisponivelTextoLabel.Text = (Convert.ToDecimal(row["total"])).ToString("C");
                    }
                    else
                    {
                        limiteDisponivelTextoLabel.Text = "0";
                    }
                }
            }
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
            OBJClienteClasse.QuantidadeDias = Convert.ToInt32(AtrasoAcimaDropDownList.SelectedValue);
            OBJClienteClasse.Ordenar = OrdenarDropDownList.SelectedValue;

            if (VencimentoInicialTextBox.Text != null && VencimentoInicialTextBox.Text != "")
            {
                OBJClienteClasse.VencimentoInicial = VencimentoInicialTextBox.Text;
            }
            if (VencimentoFinalTextBox.Text != null && VencimentoFinalTextBox.Text != "")
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

            OBJClienteClasse.Status = "2";
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
            Response.Redirect("~/financeiro/CadastroClienteWebForm.aspx?indmnu=5");
        }
    }
}