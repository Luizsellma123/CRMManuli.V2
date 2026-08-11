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
    public partial class PedidosWebForm : System.Web.UI.Page
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
            //PedidoSAPTextoLabel.Text = OBJClienteClasse.ValorAReceber.ToString("C");
            //PedidoCRMTextoLabel.Text = OBJClienteClasse.ValorRecebido.ToString("C");
        }

        public void CarregaCombos()
        {
            OBJClienteClasse.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = OBJClienteClasse.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            DataTable OBJDataTable = new DataTable();

            OBJClienteClasse.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);

            if (PedidoSAPTextBox.Text != null && PedidoSAPTextBox.Text != "")
            {
                OBJClienteClasse.PedidoSAP = Convert.ToInt32(PedidoSAPTextBox.Text);
            }
            else
            {
                OBJClienteClasse.PedidoSAP = 0;
            }

            if (PedidoCRMTextBox.Text != null && PedidoCRMTextBox.Text != "")
            {
                OBJClienteClasse.PedidoCRM = Convert.ToInt32(PedidoCRMTextBox.Text);
            }
            else
            {
                OBJClienteClasse.PedidoCRM = 0;
            }

            OBJDataTable = OBJClienteClasse.RecuperaCCPedidos();
            CCPedidosGridView.DataSource = OBJDataTable;
            CCPedidosGridView.DataBind();
            CCPedidosMultiView.Visible = true;
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/financeiro/ContaCorrenteDetalheWebForm.aspx?indmnu=5");
        }

        protected void CCPedidosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CCPedidosGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }
    }
}