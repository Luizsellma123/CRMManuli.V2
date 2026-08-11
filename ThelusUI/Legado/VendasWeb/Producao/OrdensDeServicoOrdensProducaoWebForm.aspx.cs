using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace VendasWeb.Producao
{
    public partial class OrdensDeServicoOrdensProducaoWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        producao ObjProducao = new producao();

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

            ValidaDadosSessao();

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
                CarregaGrid();
            }
            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        public void CarregaDadosNaTela()
        {
            //EMPRESA
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.SelectedValue = ObjProducao.IDEmpresa.ToString();
            EmpresaDropDownList.Enabled = false;

            OrdemServicoTextBox.Text = ObjProducao.OrdemServico.ToString();
            OrdemServicoTextBox.Enabled = false;

            StatusDropDownList.Items.Insert(0, new ListItem("Todos", ""));
        }

        public void CarregaDadosDaTela()
        {
            if (EmpresaDropDownList.SelectedValue != "" && EmpresaDropDownList.SelectedValue != null)
            {
                ObjProducao.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
            }
            else
            {
                ObjProducao.IDEmpresa = 0;
            }

            if (DataInicialTextBox.Text != "" && DataInicialTextBox.Text != null)
            {
                ObjProducao.DataInicial = DataInicialTextBox.Text.ToString();
            }
            else
            {
                ObjProducao.DataInicial = "";
            }

            if (DataFinalTextBox.Text != "" && DataFinalTextBox.Text != null)
            {
                ObjProducao.DataFinal = DataFinalTextBox.Text.ToString();
            }
            else
            {
                ObjProducao.DataFinal = "";
            }

            if (PedidoSAPTextBox.Text != "" && PedidoSAPTextBox.Text != null)
            {
                ObjProducao.NumeroPedidoSAP = Convert.ToInt32(PedidoSAPTextBox.Text);
            }
            else
            {
                ObjProducao.NumeroPedidoSAP = 0;
            }

            if (PedidoCRMTextBox.Text != "" && PedidoCRMTextBox.Text != null)
            {
                ObjProducao.NumeroPedidoCRM = Convert.ToInt32(PedidoCRMTextBox.Text);
            }
            else
            {
                ObjProducao.NumeroPedidoCRM = 0;
            }

            if (ProdutoTextBox.Text != "" && ProdutoTextBox.Text != null)
            {
                ObjProducao.Produto = ProdutoTextBox.Text.ToString();
            }
            else
            {
                ObjProducao.Produto = "";
            }

            if (OrdemTextBox.Text != "" && OrdemTextBox.Text != null)
            {
                ObjProducao.Ordem = Convert.ToInt32(OrdemTextBox.Text);
            }
            else
            {
                ObjProducao.Ordem = 0;
            }

            if (StatusOPDropDownList.SelectedValue != "" && StatusOPDropDownList.SelectedValue != null)
            {
                ObjProducao.StatusOP = StatusOPDropDownList.SelectedValue.ToString();
            }
            else
            {
                ObjProducao.StatusOP = "";
            }

            if (StatusDropDownList.SelectedValue != "" && StatusDropDownList.SelectedValue != null)
            {
                ObjProducao.Status = StatusDropDownList.SelectedValue.ToString();
            }
            else
            {
                ObjProducao.Status = "";
            }
        }

        public void CarregaGrid()
        {
            CarregaDadosDaTela();

            DataTable OBJDataTable = new DataTable();

            OBJDataTable = ObjProducao.RecuperaListaOrdensServicoOrdensProducao();
            OrdensServicoGridView.DataSource = OBJDataTable;
            OrdensServicoGridView.DataBind();
            OrdensServicoMultiView.Visible = true;
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/OrdensDeServicoPrincipalWebForm.aspx?indmnu=3");
        }

        protected void OrdensServicoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OrdensServicoGridView.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void ValidaDadosSessao()
        {
            if (Session["OrdensDeServico"] != null)
            {
                ObjProducao = (producao)Session["OrdensDeServico"];
            }
            else
            {
                Session["Msg"] = "A sua sessão expirou.";

                Response.Redirect("~/Producao/OrdensDeServicoWebForm.aspx?indmnu=3");
            }
        }

    }
}