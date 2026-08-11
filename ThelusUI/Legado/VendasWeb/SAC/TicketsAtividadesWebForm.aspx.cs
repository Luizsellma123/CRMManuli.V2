using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.SAC
{
    public partial class TicketsAtividadesWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        SACClass ObjSAC = new SACClass();
        setor ObjSetor = new setor();
        usuario ObjUsuario = new usuario();

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
                CarregaCombos();
                CarregaDadosNaTela();
                BuscarButton_Click(null, null);
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaCombos()
        {
            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            //EmpresaDropDownList.Items.Insert(0, new ListItem("Todas", "0"));

            ObjSetor.Filtro = "";
            ObjSetor.Status = "";
            SetorDropDownList.DataSource = ObjSetor.ListaSetores();
            SetorDropDownList.DataTextField = "Descricao";
            SetorDropDownList.DataValueField = "IDSetor";
            SetorDropDownList.DataBind();
            SetorDropDownList.Items.Insert(0, new ListItem("Todas", "0"));

            ObjSAC.Tela = "Lista";
            ObjSAC.Filtro = "";
            SituacaoDropDownList.DataSource = ObjSAC.RetornaListaSituacaoAtividades();
            SituacaoDropDownList.DataTextField = "Descricao";
            SituacaoDropDownList.DataValueField = "Codigo";
            SituacaoDropDownList.DataBind();
            SituacaoDropDownList.Items.Insert(0, new ListItem("Todas", "0"));
        }

        protected void CarregaDadosNaTela()
        {
            EmpresaDropDownList.Enabled = false;
            ClienteTextBox.Enabled = false;
            TicketTextBox.Enabled = false;
            SolicitanteTextBox.Enabled = false;
            DataTextBox.Enabled = false;

            if (Session["TicketsDetalhe"] != null) ObjSAC = (SACClass)Session["TicketsDetalhe"];

            EmpresaDropDownList.SelectedValue = ObjSAC.IDEmpresa.ToString();
            ClienteTextBox.Text = ObjSAC.Cliente;
            TicketTextBox.Text = ObjSAC.IDTicket.ToString();
            SolicitanteTextBox.Text = ObjSAC.Solicitante;
            DataTextBox.Text = ObjSAC.DataSolicitacao;
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            if (Session["TicketsDetalhe"] != null) ObjSAC = (SACClass)Session["TicketsDetalhe"];

            ObjSAC.IDSetor = Convert.ToInt32(SetorDropDownList.SelectedValue ?? "0");
            ObjSAC.IDSituacao = Convert.ToInt32(SituacaoDropDownList.SelectedValue ?? "0");
            ObjSAC.DataInicio = DataInicioTextBox.Text == null || DataInicioTextBox.Text == "" ? "" :
                Convert.ToDateTime(DataInicioTextBox.Text).ToString("yyyy-MM-dd");
            ObjSAC.DataFim = DataFimTextBox.Text == null || DataFimTextBox.Text == "" ? "" :
                Convert.ToDateTime(DataFimTextBox.Text).ToString("yyyy-MM-dd");
            ObjSAC.Atividade = AtividadeTextBox.Text ?? "";

            ObjSAC.Tela = "Lista";
            SACGridView.DataSource = ObjSAC.RetornaListaTicketsAtividades();
            SACGridView.DataBind();
            SACMultiView.Visible = true;
        }

        protected void NovoLinkButton_Click(object sender, EventArgs e)
        {
            if (Session["TicketsDetalhe"] != null) ObjSAC = (SACClass)Session["TicketsDetalhe"];

            //ObjSAC.Operacao = "Inclusao";
            ObjSAC.Operacao = "InclusaoAtividade";
            ObjSAC.IDAtividade = 0;

            Session["TicketsDetalhe"] = ObjSAC;
            Response.Redirect("~/SAC/TicketsAtividadesDetalheWebForm.aspx?indmnu=5");
        }

        protected void SelLinkButton_Click(object sender, EventArgs e)
        {
            if (Session["TicketsDetalhe"] != null) ObjSAC = (SACClass)Session["TicketsDetalhe"];

            ObjSAC.Operacao = "Alteracao";
            //ObjSAC.Operacao = "AlteracaoAtividade";
            ObjSAC.IDAtividade = Convert.ToInt32(((Label)((Control)sender).FindControl("AtividadeLabel")).Text);

            Session["TicketsDetalhe"] = ObjSAC;
            Response.Redirect("~/SAC/TicketsAtividadesDetalheWebForm.aspx?indmnu=5");
        }

        protected void SACGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SACGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/TicketsDetalheWebForm.aspx?indmnu=5");
        }
    }
}