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
    public partial class PrioridadeOrdensServicoWebForm : System.Web.UI.Page
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

            //Recupera objeto grupo da sessao do usuário
            if (Session["StatusOrdemServico"] != null)
            {
                ObjProducao = (producao)Session["StatusOrdemServico"];
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
            ObjProducao.Prioridade = PrioridadeTextBox.Text;

            DataTable OBJDataTable = new DataTable();

            OBJDataTable = ObjProducao.ListaPrioridades();
            PrioridadeGridView.DataSource = OBJDataTable;
            PrioridadeGridView.DataBind();
            PrioridadeMultiView.Visible = true;
        }

        protected void EditarLinkButton_Click(object sender, EventArgs e)
        {
            ObjProducao.IDPrioridade = Convert.ToInt32(((Label)((Control)sender).FindControl("IDPrioridadeLabel")).Text);
            ObjProducao.Operacao = "alteracao";
            Session["PrioridadeOrdemServico"] = ObjProducao;
            Response.Redirect("~/Producao/PrioridadeOrdensServicoDetalheWebForm.aspx?indmnu=3");
        }

        protected void NovoLinkButton_Click(object sender, EventArgs e)
        {
            ObjProducao.Operacao = "inclusao";
            Session["PrioridadeOrdemServico"] = ObjProducao;
            Response.Redirect("~/Producao/PrioridadeOrdensServicoDetalheWebForm.aspx?indmnu=3");
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/HomeProducaoWebForm.aspx?indmnu=3");
        }

        protected void PrioridadeGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PrioridadeGridView.PageIndex = e.NewPageIndex;
            CarregaDadosTela();
        }
    }
}