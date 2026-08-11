using System;
using System.Web;
using System.Data;
using System.Linq;
using System.Web.UI;
using VendasWeb.Email;
using VendasWeb.classes;
using VendasWeb.usercontrol;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Collections.Generic;

namespace VendasWeb.SAC
{
    public partial class AtividadesDetalheWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        SACClass ObjSAC = new SACClass();
        usuario ObjUsuario = new usuario();
        setor ObjSetor = new setor();
        grupos objGrupo = new grupos();
        TicketWebUserControl ObjTicketWebUserControl = new TicketWebUserControl();
        EmailTemplateClass OBJEmail = new EmailTemplateClass();

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
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();

            CarregaSetor();

            ResponsavelDropDownList_SelectedIndexChanged(null, null);

            ObjSAC.Tela = "Lista";
            ObjSAC.Filtro = "";
            SituacaoDropDownList.DataSource = ObjSAC.RetornaListaSituacaoAtividades();
            SituacaoDropDownList.DataTextField = "Descricao";
            SituacaoDropDownList.DataValueField = "Codigo";
            SituacaoDropDownList.DataBind();
        }

        protected void CarregaDadosNaTela()
        {
            if (Session["AtividadesDetalhe"] != null) ObjSAC = (SACClass)Session["AtividadesDetalhe"];

            DataTable ObjDataTable = new DataTable();

            EmpresaDropDownList.Enabled = false;
            TicketTextBox.Enabled = false;
            ClienteTextBox.Enabled = false;
            AtividadeTextBox.Enabled = false;
            SetorDropDownList.Enabled = false;
            DataTextBox.Enabled = false;
            ResponsavelDropDownList.Enabled = false;
            AssuntoTextBox.Enabled = false;
            DescricaoTextBox.Enabled = false;
            AssuntoTicketTextBox.Enabled = false;
            DescricaoTicketTextBox.Enabled = false;

            ObjSAC.Tela = "WSAtividadeDetalhe";
            //ObjSAC.IDEmpresa
            ObjSAC.IDSetor = 0;
            //ObjSAC.IDTicket
            ObjSAC.Cliente = "";
            ObjSAC.Solicitante = "";
            //ObjSAC.IDAtividade
            ObjSAC.IDSituacao = 0;
            ObjSAC.DataInicio = "";
            ObjSAC.DataFim = "";
            ObjSAC.Administrador = "0";
            ObjSAC.IDUsuario = 0;

            ObjDataTable = ObjSAC.RetornaListaAtividades();

            if (ObjDataTable.Rows.Count > 0)
            {
                foreach (DataRow Row in ObjDataTable.Rows)
                {
                    EmpresaDropDownList.SelectedValue = ObjSAC.IDEmpresa.ToString();
                    TicketTextBox.Text = ObjSAC.IDTicket.ToString();
                    ClienteTextBox.Text = Row["Cliente"].ToString();
                    AtividadeTextBox.Text = ObjSAC.IDAtividade.ToString();
                    SetorDropDownList.SelectedValue = Row["IDSetor"].ToString();
                    DataTextBox.Text = Convert.ToDateTime(Row["Data"].ToString()).ToString("yyyy-MM-dd");
                    ResponsavelDropDownList.SelectedValue = Row["IDResponsavel"].ToString();
                    SituacaoDropDownList.SelectedValue = Row["IDSituacao"].ToString();
                    AssuntoTextBox.Text = Row["AssuntoAtividade"].ToString();
                    DescricaoTextBox.Text = Row["DescricaoAtividade"].ToString();
                    AssuntoTicketTextBox.Text = Row["Assunto"].ToString();
                    DescricaoTicketTextBox.Text = Row["Descricao"].ToString();
                }
            }

            //ObjSAC.IDEmpresa
            ObjSAC.Cliente = ClienteTextBox.Text;
            //ObjSAC.IDTicket
            ObjSAC.IDSetor = Convert.ToInt32(SetorDropDownList.SelectedValue);
            //ObjSAC.IDAtividade
            Session["AtividadesDetalhe"] = ObjSAC;
        }

        protected string CarregaDadosDaTela()
        {
            string erro = "";

            //ObjSAC.IDEmpresa
            //ObjSAC.IDTicket
            //ObjSAC.Cliente 
            ObjSAC.IDAtividade = Convert.ToInt32(AtividadeTextBox.Text);
            ObjSAC.IDSetor = Convert.ToInt32(SetorDropDownList.SelectedValue ?? "0");
            ObjSAC.DataAtividade = DataTextBox.Text != "" ? Convert.ToDateTime(DataTextBox.Text).ToString("yyyy-MM-dd") : "";
            ObjSAC.IDResponsavel = Convert.ToInt32(ResponsavelDropDownList.SelectedValue ?? "0");
            ObjSAC.IDSituacao = Convert.ToInt32(SituacaoDropDownList.SelectedValue ?? "0");
            ObjSAC.AssuntoAtividade = AssuntoTextBox.Text ?? "";
            ObjSAC.DescricaoAtividade = DescricaoTextBox.Text ?? "";

            if (erro == "") erro = ObjSAC.IDSituacao == 0 ? "Escolha uma situação" : "";

            return erro;
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["AtividadesDetalhe"] != null) ObjSAC = (SACClass)Session["AtividadesDetalhe"];

            if (Session["IDUsuario"] != null) ObjSAC.IDUsuario = Convert.ToInt32(Session["IDUsuario"].ToString());

            erro = CarregaDadosDaTela();

            ObjSAC.EmailOperacao = ObjSAC.Operacao;

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
                Session["AtividadesDetalhe"] = ObjSAC;
                CarregaDadosNaTela();
                ApresentaMensagem("");
            }
        }

        private void CarregaSetor()
        {
            setor ObjSetor = new setor();

            ObjSetor.Filtro = "";
            ObjSetor.Status = "";

            SetorDropDownList.DataSource = ObjSetor.ListaSetores();
            SetorDropDownList.DataTextField = "Descricao";
            SetorDropDownList.DataValueField = "IDSetor";
            SetorDropDownList.DataBind();

        }

        protected string VerificaAdministrador()
        {
            string Administrador = "";

            objGrupo.Status = "";
            objGrupo.Filtro = "Atendimento Cliente";

            DataTable ValidaAcessoDataTable = new DataTable();

            ValidaAcessoDataTable = objGrupo.ListaGrupos();

            if (ValidaAcessoDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in ValidaAcessoDataTable.Rows)
                {
                    objGrupo.IDGrupo = Convert.ToInt32(row["IDGrupo"].ToString());
                }
            }

            objGrupo.Filtro = Session["IDUsuario"].ToString();

            ValidaAcessoDataTable = objGrupo.ListaUsuariosGrupos();

            if (ValidaAcessoDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in ValidaAcessoDataTable.Rows)
                {
                    Administrador = row["Administrador"].ToString();
                }
            }

            return Administrador;
        }

        protected void ResponsavelDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjSetor.IDSetor = 0;

            ResponsavelDropDownList.DataSource = ObjSetor.RetornaUsuariosSetor();
            ResponsavelDropDownList.DataTextField = "Nome";
            ResponsavelDropDownList.DataValueField = "IDUsuario";
            ResponsavelDropDownList.DataBind();
        }

        protected void FormataEmail()
        {
            ObjSAC.NomeUsuario = Session["usuario"].ToString();

            ObjSAC.CabecalhoEmail = "Ticket - " + ObjSAC.IDTicket.ToString();

            //if (ObjSAC.IDAtividade != 0)
            ObjSAC.CabecalhoEmail += " - Atividade - " + ObjSAC.IDAtividade.ToString();

            ObjSAC.TituloEmail = ObjSAC.EmailOperacao + " de Atividade";

            ObjSAC.DetalheEmail += "Empresa: " + this.EmpresaDropDownList.SelectedItem.Text + "<br>";
            ObjSAC.DetalheEmail += "Cliente: " + this.ClienteTextBox.Text + "<br>";
            ObjSAC.DetalheEmail += "Setor: " + this.SetorDropDownList.SelectedItem.Text + "<br>";
            ObjSAC.DetalheEmail += "Data Atividade: " + this.DataTextBox.Text + "<br>";
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
            Session["AtividadesDetalhe"] = null;
            Response.Redirect("~/SAC/AtividadesWebForm.aspx?indmnu=3");
        }
    }
}