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
    public partial class TicketsHistoricoWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        SACClass ObjSAC = new SACClass();

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
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            EmpresaDropDownList.Enabled = false;
            ClienteTextBox.Enabled = false;
            TicketTextBox.Enabled = false;

            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.Items.Insert(0, new ListItem("Todas", "0"));

            ObjSAC.TipoHistorico = "Ticket";
            EventoDropDownList.DataSource = ObjSAC.RetornaListaTicketsEvento();
            EventoDropDownList.DataTextField = "Descricao";
            EventoDropDownList.DataValueField = "IDEvento";
            EventoDropDownList.DataBind();

            EventoDropDownList_SelectedIndexChanged(null, null);

            if (Session["TicketsDetalhe"] != null) ObjSAC = (SACClass)Session["TicketsDetalhe"];

            EmpresaDropDownList.SelectedValue = ObjSAC.IDEmpresa.ToString();
            ClienteTextBox.Text = ObjSAC.Cliente;
            TicketTextBox.Text = ObjSAC.IDTicket.ToString();

            HistoricoTextBox.Text = "";

            ObjSAC.IDAtividade = 0;
            ObjSAC.IDSetor = 0;

            //CARREGA O HTML DAS ANIMAÇÕES DO HISTORICO
            //ObjSAC.IDChamado
            ObjSAC.RetornaTicketHistorico();
            HitoricoLiteral.Text = ObjSAC.Historico;
        }

        protected void EventoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjSAC.IDEvento = Convert.ToInt32(EventoDropDownList.SelectedValue ?? "0");
            ObjSAC.TipoHistorico = "Ticket";

            CategoriaDropDownList.DataSource = ObjSAC.RetornaListaTicketsCategoria();
            CategoriaDropDownList.DataTextField = "Descricao";
            CategoriaDropDownList.DataValueField = "IDCategoria";
            CategoriaDropDownList.DataBind();
        }

        protected void AdicionarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["TicketsDetalhe"] != null) ObjSAC = (SACClass)Session["TicketsDetalhe"];

            ObjSAC.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
            //ObjSAC.IDEmpresa
            //ObjSAC.IDTicket
            ObjSAC.IDEvento = Convert.ToInt32(EventoDropDownList.SelectedValue ?? "0");
            ObjSAC.Evento = EventoDropDownList.SelectedItem.Text;
            ObjSAC.IDCategoria = Convert.ToInt32(CategoriaDropDownList.SelectedValue ?? "0");
            ObjSAC.Categoria = CategoriaDropDownList.SelectedItem.Text;
            ObjSAC.Historico = HistoricoTextBox.Text ?? "";
            ObjSAC.TipoHistorico = "Historico Detalhe";
            ObjSAC.IDAtividade = 0;
            ObjSAC.IDSetor = 0;

            ObjSAC.EmailOperacao = ObjSAC.Operacao;

            erro = ObjSAC.IDEvento == 0 ? "Escolha um evento." : "";
            if (erro == "") erro = ObjSAC.IDEvento == 0 ? "Escolha uma categoria." : "";
            if (erro == "") erro = ObjSAC.Historico == "" ? "Digite um historico." : "";

            if (erro == "") ObjSAC.GravaTicketHistorico();

            if (erro == "")
            {
                FormataEmail();

                erro = ObjSAC.EnviaEmailTicket();

                if (ObjSAC.IDUsuario != ObjSAC.IDResponsavel)
                {
                    ObjSAC.IDUsuario = ObjSAC.IDResponsavel;
                    FormataEmail();
                    erro = ObjSAC.EnviaEmailTicket();
                }
            }

            if (erro != "") ApresentaMensagem(erro);
            else
            {
                ApresentaMensagem("");
                CarregaDadosNaTela();
            }

        }

        protected void FormataEmail()
        {
            ObjSAC.NomeUsuario = Session["usuario"].ToString();

            ObjSAC.CabecalhoEmail = "Ticket - " + ObjSAC.IDTicket.ToString();

            ObjSAC.TituloEmail = ObjSAC.EmailOperacao + " de Histórico";

            ObjSAC.DetalheEmail += "Empresa: " + this.EmpresaDropDownList.SelectedItem.Text + "<br>";
            ObjSAC.DetalheEmail += "Cliente: " + this.ClienteTextBox.Text + "<br>";
            ObjSAC.DetalheEmail += "Evento: " + this.EventoDropDownList.SelectedItem.Text + "<br>";
            ObjSAC.DetalheEmail += "Categoria: " + this.CategoriaDropDownList.SelectedItem.Text + "<br>";
            ObjSAC.DetalheEmail += "Novo Histórico: " + this.HistoricoTextBox.Text + "<br>";
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/TicketsDetalheWebForm.aspx?indmnu=5");
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
                erro = "Operação realizada com sucesso.";
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

    }
}