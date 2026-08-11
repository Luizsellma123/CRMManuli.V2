using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;
using VendasWeb.usercontrol;

namespace VendasWeb.SAC
{
    public partial class TicketsAtividadesDetalheWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        SACClass ObjSAC = new SACClass();
        setor ObjSetor = new setor();
        usuario ObjUsuario = new usuario();
        grupos objGrupo = new grupos();

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
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaCombos()
        {
            if (Session["TicketsDetalhe"] != null) ObjSAC = (SACClass)Session["TicketsDetalhe"];

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
            //SetorDropDownList.Items.Insert(0, new ListItem("Todas", "0"));

            ObjSAC.Tela = "Lista";
            ObjSAC.Filtro = "";
            SituacaoDropDownList.DataSource = ObjSAC.RetornaListaSituacaoAtividades();
            SituacaoDropDownList.DataTextField = "Descricao";
            SituacaoDropDownList.DataValueField = "Codigo";
            SituacaoDropDownList.DataBind();
            //SituacaoDropDownList.Items.Insert(0, new ListItem("Todas", "0"));

            SetorDropDownList_SelectedIndexChanged(null, null);
        }

        protected void CarregaDadosNaTela()
        {
            if (Session["TicketsDetalhe"] != null) ObjSAC = (SACClass)Session["TicketsDetalhe"];

            EmpresaDropDownList.Enabled = false;
            TicketTextBox.Enabled = false;
            ClienteTextBox.Enabled = false;
            AtividadeTextBox.Enabled = false;

            EmpresaDropDownList.SelectedValue = ObjSAC.IDEmpresa.ToString();
            TicketTextBox.Text = ObjSAC.IDTicket.ToString();
            ClienteTextBox.Text = ObjSAC.Cliente;
            AtividadeTextBox.Text = ObjSAC.IDAtividade.ToString();

            DataAtividadeTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

            if (ObjSAC.Operacao == "Alteracao")
            {
                AssuntoTextBox.Enabled = false;
                DescricaoTextBox.Enabled = false;
                DataAtividadeTextBox.Enabled = false;
                SetorDropDownList.Enabled = false;
                ResponsavelDropDownList.Enabled = false;

                ObjSAC.Tela = "Detalhe";
                ObjSAC.Atividade = ObjSAC.IDAtividade.ToString();
                DataTable ObjDataTable = new DataTable();
                ObjDataTable = ObjSAC.RetornaListaTicketsAtividades();

                if (ObjDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in ObjDataTable.Rows)
                    {
                        ObjSAC.IDSetor = Convert.ToInt32(row["IDSetor"].ToString());
                        ObjSAC.IDResponsavel = Convert.ToInt32(row["IDResponsavel"].ToString());
                        ObjSAC.IDSituacao = Convert.ToInt32(row["IDSituacao"].ToString());
                        ObjSAC.AssuntoAtividade = row["AssuntoAtividade"].ToString();
                        ObjSAC.DescricaoAtividade = row["DescricaoAtividade"].ToString();
                        ObjSAC.DataAtividade = Convert.ToDateTime(row["Data"].ToString()).ToString("yyyy-MM-dd");
                    }
                }

                SetorDropDownList.SelectedValue = ObjSAC.IDSetor.ToString();
                DataAtividadeTextBox.Text = Convert.ToDateTime(ObjSAC.DataAtividade).ToString("yyyy-MM-dd");
                ResponsavelDropDownList.SelectedValue = ObjSAC.IDResponsavel.ToString();
                SituacaoDropDownList.SelectedValue = ObjSAC.IDSituacao.ToString();
                AssuntoTextBox.Text = ObjSAC.AssuntoAtividade;
                DescricaoTextBox.Text = ObjSAC.DescricaoAtividade;
            }

        }

        protected string CarregaDadosDaTela()
        {
            string erro = "";

            if (Session["TicketsDetalhe"] != null) ObjSAC = (SACClass)Session["TicketsDetalhe"];

            //ObjSAC.IDEmpresa e ObjSAC.IDTicket e ObjSAC.Cliente e ObjSAC.IDAtividade vem da sessao
            ObjSAC.IDAtividade = Convert.ToInt32(AtividadeTextBox.Text ?? "0");
            ObjSAC.IDSetor = Convert.ToInt32(SetorDropDownList.SelectedValue ?? "0");
            ObjSAC.DataAtividade = (Convert.ToDateTime(DataAtividadeTextBox.Text).ToString("yyyy-MM-dd")) ?? "";
            ObjSAC.IDResponsavel = Convert.ToInt32(ResponsavelDropDownList.SelectedValue ?? "0");
            ObjSAC.IDSituacao = Convert.ToInt32(SituacaoDropDownList.SelectedValue ?? "0");
            ObjSAC.AssuntoAtividade = AssuntoTextBox.Text ?? "";
            ObjSAC.DescricaoAtividade = DescricaoTextBox.Text ?? "";

            erro = ObjSAC.IDSetor == 0 ? "Escolha um empresa" : "";
            if (erro == "") erro = ObjSAC.DataAtividade == "" ? "Escolha uma data" : "";
            if (ObjSAC.Operacao == "InclusaoAtividade" && erro == "")
                erro = Convert.ToDateTime(ObjSAC.DataAtividade).Date < DateTime.Now.Date ? "Escolha uma data maior ou igual a hoje" : "";
            if (erro == "") erro = ObjSAC.IDResponsavel == 0 ? "Escolha um responsável" : "";
            if (erro == "") erro = ObjSAC.IDSituacao == 0 ? "Escolha uma situação" : "";
            if (erro == "") erro = ObjSAC.AssuntoAtividade == "" ? "Digite um assunto" : "";
            if (erro == "") erro = ObjSAC.DescricaoAtividade == "" ? "Digite uma descrição" : "";

            return erro;
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            erro = CarregaDadosDaTela();

            ObjSAC.EmailOperacao = ObjSAC.Operacao;
            if (ObjSAC.Operacao == "InclusaoAtividade") ObjSAC.EmailOperacao = "Inclusao";

            if (Session["IDUsuario"] != null) ObjSAC.IDUsuario = Convert.ToInt32(Session["IDUsuario"].ToString());

            if (erro == "") erro = ObjSAC.GravaTicketAtividades();

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

            if (erro != "")
            {
                ApresentaMensagem(erro);
            }
            else
            {
                Session["TicketsDetalhe"] = ObjSAC;
                CarregaDadosNaTela();
                CarregaCombos();
                ApresentaMensagem("");
                this.TicketAtividadeWebUserControl.DesbloqueiaButtons();
            }

        }

        protected void FormataEmail()
        {
            string Data = DataAtividadeTextBox.Text;
            Data = Convert.ToDateTime(Data).ToString("dd-MM-yyyy");
            Data = Data.Replace("-", "/");

            ObjSAC.NomeUsuario = Session["usuario"].ToString();

            ObjSAC.CabecalhoEmail = "Ticket - " + ObjSAC.IDTicket.ToString();

            //if (ObjSAC.IDAtividade != 0)
            ObjSAC.CabecalhoEmail += " - Atividade - " + ObjSAC.IDAtividade.ToString();

            ObjSAC.TituloEmail = ObjSAC.EmailOperacao + " de Atividade";

            ObjSAC.DetalheEmail += "Empresa: " + this.EmpresaDropDownList.SelectedItem.Text + "<br>";
            ObjSAC.DetalheEmail += "Cliente: " + this.ClienteTextBox.Text + "<br>";
            ObjSAC.DetalheEmail += "Setor: " + this.SetorDropDownList.SelectedItem.Text + "<br>";
            ObjSAC.DetalheEmail += "Data Atividade: " + Data + "<br>";
            ObjSAC.DetalheEmail += "Responsavel: " + this.ResponsavelDropDownList.SelectedItem.Text + "<br>";
            ObjSAC.DetalheEmail += "Situação: " + this.SituacaoDropDownList.SelectedItem.Text + "<br>";
            ObjSAC.DetalheEmail += "Assunto: " + this.AssuntoTextBox.Text + "<br>";
            ObjSAC.DetalheEmail += "Descrição: " + this.DescricaoTextBox.Text + "<br>";
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
            if (Session["TicketsDetalhe"] != null) ObjSAC = (SACClass)Session["TicketsDetalhe"];
            ObjSAC.Operacao = "Alteracao";
            Session["TicketsDetalhe"] = ObjSAC;
            Response.Redirect("~/SAC/TicketsAtividadesWebForm.aspx?indmnu=3");
        }

        protected void SetorDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["TicketsDetalhe"] != null) ObjSAC = (SACClass)Session["TicketsDetalhe"];

            if (ObjSAC.Operacao != "Alteracao") ObjSetor.IDSetor = Convert.ToInt32(SetorDropDownList.SelectedValue);
            else ObjSetor.IDSetor = 0;

            DataTable ObjDataTable = new DataTable();
            ObjDataTable = ObjSetor.RetornaUsuariosSetor();

            if (ObjDataTable.Rows.Count > 0)
            {
                ResponsavelDropDownList.DataSource = ObjDataTable;
                ResponsavelDropDownList.DataTextField = "Nome";
                ResponsavelDropDownList.DataValueField = "IDUsuario";
                ResponsavelDropDownList.DataBind();
            }
            else
            {
                ApresentaMensagem("Não há usuários cadastrados neste setor");
            }

        }
    }
}