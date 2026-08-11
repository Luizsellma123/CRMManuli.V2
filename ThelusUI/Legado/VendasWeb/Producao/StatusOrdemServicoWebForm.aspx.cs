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
    public partial class StatusOrdemServicoWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        producao ObjProducao = new producao();

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
                CarregaDadosTela();
            }
            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosTela();
        }

        public void CarregaDadosTela()
        {
            ObjProducao.Tela = "StatusOrdemServico";
            ObjProducao.Status = StatusTextBox.Text;

            DataTable OBJDataTable = new DataTable();

            OBJDataTable = ObjProducao.ListaStatus();
            StatusGridView.DataSource = OBJDataTable;
            StatusGridView.DataBind();
            StatusMultiView.Visible = true;
        }

        protected void EditarLinkButton_Click(object sender, EventArgs e)
        {
            ObjProducao.IDStatus = Convert.ToInt32(((Label)((Control)sender).FindControl("IDStatusLabel")).Text);
            ObjProducao.Operacao = "alteracao";
            Session["StatusOrdemServico"] = ObjProducao;
            Response.Redirect("~/Producao/OrdensDeServicoDetalheWebForm.aspx?indmnu=3");
        }

        protected void NovoLinkButton_Click(object sender, EventArgs e)
        {
            ObjProducao.Operacao = "inclusao";
            Session["StatusOrdemServico"] = ObjProducao;
            Response.Redirect("~/Producao/OrdensDeServicoDetalheWebForm.aspx?indmnu=3");
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/HomeProducaoWebForm.aspx?indmnu=3");
        }

        protected void StatusGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            StatusGridView.PageIndex = e.NewPageIndex;
            CarregaDadosTela();
        }
        
    }
}