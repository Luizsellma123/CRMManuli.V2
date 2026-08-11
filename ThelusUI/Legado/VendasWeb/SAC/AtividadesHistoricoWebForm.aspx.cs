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
    public partial class AtividadesHistoricoWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        SACClass ObjSAC = new SACClass();
        setor ObjSetor = new setor();

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
            SetorDropDownList.Enabled = false;
            AtividadeTextBox.Enabled = false;

            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.Items.Insert(0, new ListItem("Todas", "0"));

            ObjSetor.Filtro = "";
            ObjSetor.Status = "";
            SetorDropDownList.DataSource = ObjSetor.ListaSetores();
            SetorDropDownList.DataTextField = "Descricao";
            SetorDropDownList.DataValueField = "IDSetor";
            SetorDropDownList.DataBind();

            ObjSAC.TipoHistorico = "Atividade";
            EventoDropDownList.DataSource = ObjSAC.RetornaListaTicketsEvento();
            EventoDropDownList.DataTextField = "Descricao";
            EventoDropDownList.DataValueField = "IDEvento";
            EventoDropDownList.DataBind();

            EventoDropDownList_SelectedIndexChanged(null, null);

            if (Session["AtividadesDetalhe"] != null) ObjSAC = (SACClass)Session["AtividadesDetalhe"];

            EmpresaDropDownList.SelectedValue = ObjSAC.IDEmpresa.ToString();
            ClienteTextBox.Text = ObjSAC.Cliente;
            TicketTextBox.Text = ObjSAC.IDTicket.ToString();
            SetorDropDownList.SelectedValue = ObjSAC.IDSetor.ToString();
            AtividadeTextBox.Text = ObjSAC.IDAtividade.ToString();

            HistoricoTextBox.Text = "";

            //CARREGA O HTML DAS ANIMAÇÕES DO HISTORICO
            //ObjSAC.IDChamado
            ObjSAC.RetornaTicketHistorico();
            HitoricoLiteral.Text = ObjSAC.Historico;
        }

        protected void AdicionarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["AtividadesDetalhe"] != null) ObjSAC = (SACClass)Session["AtividadesDetalhe"];

            ObjSAC.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
            //ObjSAC.IDEmpresa
            //ObjSAC.IDTicket
            //ObjSAC.IDAtividade
            ObjSAC.IDSetor = Convert.ToInt32(SetorDropDownList.SelectedValue ?? "0");
            ObjSAC.IDEvento = Convert.ToInt32(EventoDropDownList.SelectedValue ?? "0");
            ObjSAC.IDCategoria = Convert.ToInt32(CategoriaEventoDropDownList.SelectedValue ?? "0");
            ObjSAC.Historico = HistoricoTextBox.Text ?? "";
            ObjSAC.TipoHistorico = "Historico Detalhe";

            ObjSAC.EmailOperacao = ObjSAC.Operacao;

            erro = ObjSAC.IDSetor == 0 ? "Escolha um setor." : "";
            if (erro == "") erro = ObjSAC.IDEvento == 0 ? "Escolha um evento." : "";
            if (erro == "") erro = ObjSAC.IDEvento == 0 ? "Escolha uma categoria." : "";
            if (erro == "") erro = ObjSAC.Historico == "" ? "Digite um histórico." : "";

            if (erro == "") ObjSAC.GravaTicketHistorico();

            if (erro == "")
            {
                FormataEmail();

                erro = ObjSAC.EnviaEmailTicket();

                if (ObjSAC.IDUsuario != ObjSAC.IDResponsavel && ObjSAC.IDResponsavel != 0)
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
            ObjSAC.CabecalhoEmail += " - Atividade - " + ObjSAC.IDAtividade.ToString();

            ObjSAC.TituloEmail = ObjSAC.EmailOperacao + " de Histórico";

            ObjSAC.DetalheEmail += "Empresa: " + this.EmpresaDropDownList.SelectedItem.Text + "<br>";
            ObjSAC.DetalheEmail += "Cliente: " + this.ClienteTextBox.Text + "<br>";
            ObjSAC.DetalheEmail += "Setor: " + this.SetorDropDownList.SelectedItem.Text + "<br>";
            ObjSAC.DetalheEmail += "Evento: " + this.EventoDropDownList.SelectedItem.Text + "<br>";
            ObjSAC.DetalheEmail += "Categoria: " + this.CategoriaEventoDropDownList.SelectedItem.Text + "<br>";
            ObjSAC.DetalheEmail += "Novo Histórico: " + this.HistoricoTextBox.Text + "<br>";
        }

        protected void EventoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjSAC.IDEvento = Convert.ToInt32(EventoDropDownList.SelectedValue ?? "0");
            ObjSAC.TipoHistorico = "Atividade";

            CategoriaEventoDropDownList.DataSource = ObjSAC.RetornaListaTicketsCategoria();
            CategoriaEventoDropDownList.DataTextField = "Descricao";
            CategoriaEventoDropDownList.DataValueField = "IDCategoria";
            CategoriaEventoDropDownList.DataBind();
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

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/AtividadesDetalheWebForm.aspx?indmnu=3");
        }

    }
}