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
    public partial class LiberaPedidoProducaoWebForm : System.Web.UI.Page
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

        protected void CarregaCombos()
        {
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();

            VendedorDropDownList.DataSource = ObjUsuario.RetornaListaVendedores();
            VendedorDropDownList.DataTextField = "NomeVendedor";
            VendedorDropDownList.DataValueField = "IDVendedor";
            VendedorDropDownList.DataBind();
            VendedorDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

            LiberacaoDropDownList.Items.Insert(0, new ListItem("Todos", "2"));

            LiberacaoDropDownList.SelectedValue = "0";

            StatusDropDownList.DataSource = objAdmVendas.RetornaListaStatusPedidos();
            StatusDropDownList.DataTextField = "DescricaoStatus";
            StatusDropDownList.DataValueField = "IDStatus";
            StatusDropDownList.DataBind();

            StatusDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

            StatusDropDownList.SelectedValue = "11";
        }

        protected void CarregaDadosNaTela()
        {
            DateTime primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            DataInicialTextBox.Text = primeiroDiaMes.ToString("yyyy-MM-dd");
            DataFinalTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

            CarregaCombos();
            BuscarButton_Click(null, null);
        }

        protected void CarregaDadosDaTela()
        {
            objAdmVendas.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
            objAdmVendas.DataInicial = Convert.ToDateTime(DataInicialTextBox.Text).ToString("yyyy-MM-dd");
            objAdmVendas.DataFinal = Convert.ToDateTime(DataFinalTextBox.Text).ToString("yyyy-MM-dd");
            objAdmVendas.NumeroPedidoCRM = Convert.ToInt32(PedidoCRMTextBox.Text == "" ? "0" : PedidoCRMTextBox.Text);
            objAdmVendas.NumeroPedidoSAP = Convert.ToInt32(PedidoSAPTextBox.Text == "" ? "0" : PedidoSAPTextBox.Text);
            objAdmVendas.IDVendedor = Convert.ToInt32(VendedorDropDownList.SelectedValue);
            objAdmVendas.Liberacao = Convert.ToInt32(LiberacaoDropDownList.SelectedValue);
            objAdmVendas.Cliente = ClienteTextBox.Text;
            objAdmVendas.Status = StatusDropDownList.SelectedValue;
            objAdmVendas.IDStatus = Convert.ToInt32(StatusDropDownList.SelectedValue);
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();

            if (Convert.ToDateTime(objAdmVendas.DataInicial) > Convert.ToDateTime(objAdmVendas.DataFinal))
            {
                ApresentaMensagem("A data inicial não pode ser maior que a final.");
            }
            else
            {
                GridView.DataSource = objAdmVendas.RetornaListaPedidoLiberacaoProducao();
                GridView.DataBind();
                MultiView.Visible = true;
            }
        }

        protected void SelecionarLinkButton_Click(object sender, EventArgs e)
        {
            objAdmVendas.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaGridViewLabel")).Text);
            objAdmVendas.NumeroPedidoSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("PedidoSAPGridViewLabel")).Text);
            objAdmVendas.NumeroPedidoCRM = Convert.ToInt32(((Label)((Control)sender).FindControl("PedidoCRMGridViewLabel")).Text);

            Session["objAdmVendas"] = objAdmVendas;

            Response.Redirect("~/AdministracaoVendas/LiberaPedidoProducaoDetalheWebForm.aspx?indmnu=3");
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoVendas/HomeWebForm.aspx?indmnu=3");
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