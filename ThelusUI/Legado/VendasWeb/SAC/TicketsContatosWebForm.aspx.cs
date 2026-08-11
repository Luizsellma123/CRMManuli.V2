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
    public partial class TicketsContatosWebForm : System.Web.UI.Page
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

            if (Session["TicketsDetalhe"] != null)
            {
                ObjSAC = (SACClass)Session["TicketsDetalhe"];
            }

            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();
            EmpresaDropDownList.Items.Insert(0, new ListItem("Todas", "0"));

            EmpresaDropDownList.SelectedValue = ObjSAC.IDEmpresa.ToString();
            ClienteTextBox.Text = ObjSAC.Cliente;
            TicketTextBox.Text = ObjSAC.IDTicket.ToString();

            BuscarButton_Click(null, null);
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            ObjSAC.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
            ObjSAC.IDTicket = Convert.ToInt32(TicketTextBox.Text);
            ObjSAC.Pessoa = PessoaTextBox.Text ?? ""; //se for nulo atribui zero
            ObjSAC.Email = EmailTextBox.Text ?? ""; //se for nulo atribui zero
            ObjSAC.Telefone = TelefoneTextBox.Text ?? ""; //se for nulo atribui zero

            ObjSAC.Tela = "Lista";
            SACGridView.DataSource = ObjSAC.RetornaListaTicketsContatos();
            SACGridView.DataBind();
            SACMultiView.Visible = true;
        }

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["TicketsDetalhe"] != null)
            {
                ObjSAC = (SACClass)Session["TicketsDetalhe"];
            }

            //ObjSAC.IDEmpresa
            //ObjSAC.IDTicket
            ObjSAC.Operacao = "Adicionar";
            ObjSAC.Pessoa = PessoaTextBox.Text ?? "";
            ObjSAC.Email = EmailTextBox.Text ?? "";
            ObjSAC.Telefone = TelefoneTextBox.Text ?? "";

            erro = ObjSAC.Pessoa == "" ? "Digite o nome do contato." : "";
            if (erro == "") erro = ObjSAC.Email == "" ? "Digite o e-mail do contato." : "";
            if (erro == "") erro = ValidaEmail(ObjSAC.Email) != "" ? "E-mail inválido." : "";
            if (erro == "") erro = ObjSAC.Telefone == "" ? "Digite o telefone do contato." : "";
            int count = Enumerable.Count(ObjSAC.Telefone);
            if (erro == "") erro = count > 15 ? "Telefone com números sobrando." : "";
            if (erro == "") erro = count < 14 ? "Telefone com números faltando." : "";

            if (erro == "")
            {
                ObjSAC.GravaExcluiTicketContato();
            }

            if (erro != "")
            {
                ApresentaMensagem(erro);
            }
            else
            {
                PessoaTextBox.Text = "";
                EmailTextBox.Text = "";
                TelefoneTextBox.Text = "";
                CarregaDadosNaTela();
                ApresentaMensagem("");
            }
        }

        protected void ExcluirLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["TicketsDetalhe"] != null)
            {
                ObjSAC = (SACClass)Session["TicketsDetalhe"];
            }

            ObjSAC.Operacao = "Excluir";
            //ObjSAC.IDEmpresa
            //ObjSAC.IDTicket
            ObjSAC.IDContato = Convert.ToInt32(((Label)((Control)sender).FindControl("IDContatoLabel")).Text);
            ObjSAC.Pessoa = "";
            ObjSAC.Email = "";
            ObjSAC.Telefone = "";

            if (erro == "")
            {
                ObjSAC.GravaExcluiTicketContato();
            }

            if (erro != "")
            {
                ApresentaMensagem(erro);
            }
            else
            {
                PessoaTextBox.Text = "";
                EmailTextBox.Text = "";
                TelefoneTextBox.Text = "";
                CarregaDadosNaTela();
                ApresentaMensagem("");
            }
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

        protected void SACGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SACGridView.PageIndex = e.NewPageIndex;
            CarregaDadosNaTela();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/TicketsDetalheWebForm.aspx?indmnu=3");
        }

        protected string ValidaEmail(string email)
        {
            string erro = "";

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return erro;
        }

    }
}