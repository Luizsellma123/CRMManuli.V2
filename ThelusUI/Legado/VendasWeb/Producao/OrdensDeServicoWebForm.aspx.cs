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
    public partial class OrdensDeServicoWebForm : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                CarregaCombos();
                CarregaDadosTela();
            }
            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        public void CarregaCombos()
        {
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.Items.Insert(0, new ListItem("Todos", ""));

            ObjProducao.Status = StatusDropDownList.SelectedValue.ToString();

            ObjProducao.Tela = "OrdensDeServico";
            StatusDropDownList.DataSource = ObjProducao.ListaStatus();
            StatusDropDownList.DataTextField = "Descricao";
            StatusDropDownList.DataValueField = "IDStatus";
            StatusDropDownList.DataBind();
            StatusDropDownList.Items.Insert(0, new ListItem("Todos", ""));
        }

        public void CarregaDadosTela()
        {
            if (EmpresaDropDownList.SelectedValue != null && EmpresaDropDownList.SelectedValue != "")
            {
                ObjProducao.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
            }
            else
            {
                ObjProducao.IDEmpresa = 0;
            }

            if (DataInicialTextBox.Text != "" && DataInicialTextBox.Text != null)
            {
                DateTime DataInicialAux = Convert.ToDateTime(DataInicialTextBox.Text);
                ObjProducao.DataInicial = DataInicialAux.ToString("yyyy-MM-dd");
            }
            else
            {
                ObjProducao.DataInicial = "";
            }

            if (DataFinalTextBox.Text != "" && DataFinalTextBox.Text != null)
            {
                DateTime DataFinalAux = Convert.ToDateTime(DataFinalTextBox.Text);
                ObjProducao.DataFinal = DataFinalAux.ToString("yyyy-MM-dd");
            }
            else
            {
                ObjProducao.DataFinal = "";
            }

            if (OrdemServicoTextBox.Text == null || OrdemServicoTextBox.Text.ToString() == "" || OrdemServicoTextBox.Text.ToString() == null)
            {
                ObjProducao.OrdemServico = 0;
            }
            else
            {
                ObjProducao.OrdemServico = Convert.ToInt32(OrdemServicoTextBox.Text.ToString());
            }

            if (StatusDropDownList.SelectedValue != "" && StatusDropDownList.SelectedValue != null)
            {
                ObjProducao.IDStatus = Convert.ToInt32(StatusDropDownList.SelectedValue);
            }
            else
            {
                ObjProducao.IDStatus = 0;
            }

            DataTable OBJDataTable = new DataTable();

            OBJDataTable = ObjProducao.ListaOrdensServico();
            OrdensServicoGridView.DataSource = OBJDataTable;
            OrdensServicoGridView.DataBind();
            OrdensServicoMultiView.Visible = true;
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/HomeProducaoWebForm.aspx?indmnu=3");
        }

        protected void NovoLinkButton_Click(object sender, EventArgs e)
        {
            ObjProducao.Operacao = "inclusao";

            Session["OrdensDeServico"] = ObjProducao;

            Response.Redirect("~/Producao/OrdensDeServicoPrincipalWebForm.aspx?indmnu=3");
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosTela();
        }

        protected void SelecionarLinkButton_Click(object sender, EventArgs e)
        {
            ObjProducao.Operacao = "alteracao";
            ObjProducao.Empresa = ((Label)((Control)sender).FindControl("EmpresaLabel")).Text;
            ObjProducao.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaLabel")).Text);
            ObjProducao.Emissor = ((Label)((Control)sender).FindControl("EmissorLabel")).Text;
            ObjProducao.OrdemServico = Convert.ToInt32(((Label)((Control)sender).FindControl("OrdemServicoLabel")).Text);
            ObjProducao.DataEmissao = ((Label)((Control)sender).FindControl("DataEmissaoLabel")).Text;
            ObjProducao.Status = ((Label)((Control)sender).FindControl("StatusLabel")).Text;
            ObjProducao.IDStatus = Convert.ToInt32(((Label)((Control)sender).FindControl("IDStatusLabel")).Text);
            ObjProducao.StatusPrioridade = ((Label)((Control)sender).FindControl("StatusPrioridadeLabel")).Text;
            ObjProducao.OrdemServico = Convert.ToInt32(((Label)((Control)sender).FindControl("OrdemServicoLabel")).Text);
            ObjProducao.IDPrioridade = Convert.ToInt32(((Label)((Control)sender).FindControl("IDPrioridadeLabel")).Text);

            Session["OrdensDeServico"] = ObjProducao;

            Response.Redirect("~/Producao/OrdensDeServicoPrincipalWebForm.aspx?indmnu=3");
        }

        protected void OrdensServicoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OrdensServicoGridView.PageIndex = e.NewPageIndex;
            CarregaDadosTela();
        }

    }
}