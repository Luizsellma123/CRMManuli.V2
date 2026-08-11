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
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;

namespace VendasWeb.SAC
{
    public partial class TicketsDetalheWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        SACClass ObjSAC = new SACClass();
        usuario ObjUsuario = new usuario();
        grupos objGrupo = new grupos();
        EmailTemplateClass OBJEmail = new EmailTemplateClass();
        TicketWebUserControl ObjTicketWebUserControl = new TicketWebUserControl();

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
            TicketTextBox.Enabled = false;
            ClienteTextBox.Enabled = false;

            DataTable SACDataTable = new DataTable();

            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            #region Carrega DropDownList

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();

            objGrupo.IDGrupo = 18;
            ResponsavelDropDownList.DataSource = objGrupo.RetornaUsuariosGrupo();
            ResponsavelDropDownList.DataTextField = "Nome";
            ResponsavelDropDownList.DataValueField = "IDUsuario";
            ResponsavelDropDownList.DataBind();

            ObjSAC.Tela = "ClassificacaoDropDownList";
            ObjSAC.Filtro = "";
            ClassificacaoDropDownList.DataSource = ObjSAC.RetornaListaClassificacao();
            ClassificacaoDropDownList.DataTextField = "Descricao";
            ClassificacaoDropDownList.DataValueField = "IDClassificacao";
            ClassificacaoDropDownList.DataBind();

            ObjSAC.Tela = "SituacaoDropDownList";
            //ObjSAC.Filtro = "";
            SituacaoDropDownList.DataSource = ObjSAC.RetornaListaSituacaoTickets();
            SituacaoDropDownList.DataTextField = "Descricao";
            SituacaoDropDownList.DataValueField = "IDSituacao";
            SituacaoDropDownList.DataBind();

            ObjSAC.Tela = "PrioridadeDropDownList";
            //ObjSAC.Filtro = "";
            PrioridadeDropDownList.DataSource = ObjSAC.RetornaListaPrioridades();
            PrioridadeDropDownList.DataTextField = "Descricao";
            PrioridadeDropDownList.DataValueField = "IDPrioridade";
            PrioridadeDropDownList.DataBind();

            ObjSAC.Solucao = "SolucaoDropDownList";
            SolucaoDropDownList.DataSource = ObjSAC.RetornaListaSolucoes();
            SolucaoDropDownList.DataTextField = "Descricao";
            SolucaoDropDownList.DataValueField = "IDSolucao";
            SolucaoDropDownList.DataBind();
            SolucaoDropDownList.Items.Insert(0, new ListItem("", "0"));

            ObjSAC.TipoOcorrencia = "OcorrenciaDropDownList";
            OcorrenciaDropDownList.DataSource = ObjSAC.RetornaListaTipoOcorrencia();
            OcorrenciaDropDownList.DataTextField = "Descricao";
            OcorrenciaDropDownList.DataValueField = "IDTipoOcorrencia";
            OcorrenciaDropDownList.DataBind();

            VendedorDropDownList.DataSource = ObjUsuario.RetornaListaVendedores();
            VendedorDropDownList.DataTextField = "NomeVendedor";
            VendedorDropDownList.DataValueField = "IDVendedor";
            VendedorDropDownList.DataBind();

            ObjSAC.Motivo = "MotivoDropDownList";
            MotivoDropDownList.DataSource = ObjSAC.RetornaListaMotivo();
            MotivoDropDownList.DataTextField = "Descricao";
            MotivoDropDownList.DataValueField = "IDMotivo";
            MotivoDropDownList.DataBind();

            #endregion

            if (Session["TicketsDetalhe"] != null)
                ObjSAC = (SACClass)Session["TicketsDetalhe"];

            if (ObjSAC.Operacao == "Inclusao")
            {
                //Atribui o cliente escolhido na tela escolha

                EmpresaDropDownList.SelectedValue = ObjSAC.IDEmpresa.ToString() ?? "";
                ClienteTextBox.Text = ObjSAC.Cliente.ToString() ?? "";
                TicketTextBox.Text = "0";
                SolicitanteTextBox.Text = ObjSAC.Solicitante ?? "";
                ResponsavelDropDownList.SelectedValue = ObjSAC.IDResponsavel.ToString() ?? "0";
                ClassificacaoDropDownList.SelectedValue = ObjSAC.IDClassificacao.ToString() ?? "0";
                SituacaoDropDownList.SelectedValue = ObjSAC.IDSituacao.ToString() ?? "0";
                AberturaTextBox.Text = Convert.ToDateTime(ObjSAC.DataSolicitacao ?? DateTime.Now.ToString()).ToString("yyyy-MM-dd");
                //FechamentoTextBox.Enabled = false;
                PrioridadeDropDownList.SelectedValue = ObjSAC.IDPrioridade.ToString() ?? "0";
                SolucaoDropDownList.SelectedValue = ObjSAC.IDSolucao.ToString() ?? "0";
                OcorrenciaDropDownList.SelectedValue = ObjSAC.IDTipoOcorrencia.ToString() ?? "0";
                VendedorDropDownList.SelectedValue = ObjUsuario.RetornaVendedorDoCliente(ObjSAC.CodigoCliente);
                MotivoDropDownList.SelectedValue = ObjSAC.IDMotivo.ToString() ?? "0";
                DescricaoTextBox.Text = ObjSAC.Descricao ?? "";
            }
            else if (ObjSAC.Operacao == "Alteracao")
            {
                EmpresaDropDownList.Enabled = false;
                //ClienteTextBox.Enabled = false;
                //TicketTextBox.Enabled = false;
                //SolicitanteTextBox.Enabled = false;
                //ResponsavelDropDownList.Enabled = false
                ClassificacaoDropDownList.Enabled = false;
                //SituacaoDropDownList.Enabled = false;
                AberturaTextBox.Enabled = false;
                //FechamentoTextBox.Enabled = false;
                //PrioridadeDropDownList.Enabled = false;
                //SolucaoDropDownList.Enabled = false;
                OcorrenciaDropDownList.Enabled = false;
                //VendedorDropDownList.Enabled = false;
                //MotivoDropDownList.Enabled = false
                //DescricaoTextBox.Enabled = false;

                TrocarClienteLinkButton.Visible = false;

                ObjSAC.Tela = "Detalhe";
                //ObjSAC.IDEmpresa vem da sessao
                //ObjSAC.IDTicket vem da sessao
                ObjSAC.IDSituacao = 0;
                ObjSAC.Cliente = "";
                SACDataTable = ObjSAC.RetornaListaTickets();

                if (SACDataTable.Rows.Count > 0)
                {
                    foreach (DataRow Row in SACDataTable.Rows)
                    {
                        EmpresaDropDownList.SelectedValue = ObjSAC.IDEmpresa.ToString();
                        ClienteTextBox.Text = Row["Cliente"].ToString();
                        TicketTextBox.Text = ObjSAC.IDTicket.ToString();
                        SolicitanteTextBox.Text = Row["Solicitante"].ToString();
                        ResponsavelDropDownList.SelectedValue = Row["IDResponsavel"].ToString();
                        ClassificacaoDropDownList.SelectedValue = Row["IDClassificacao"].ToString();
                        SituacaoDropDownList.SelectedValue = Row["IDSituacao"].ToString();
                        AberturaTextBox.Text = Convert.ToDateTime(Row["DataSolicitacao"].ToString()).ToString("yyyy-MM-dd");
                        if (Row["DataFechamento"].ToString() != "" &&
                            Convert.ToDateTime(Row["DataFechamento"]) > Convert.ToDateTime("01-01-2000"))
                            FechamentoTextBox.Text = Convert.ToDateTime(Row["DataFechamento"].ToString()).ToString("yyyy-MM-dd");
                        PrioridadeDropDownList.SelectedValue = Row["IDPrioridade"].ToString();
                        SolucaoDropDownList.SelectedValue = Row["IDSolucao"].ToString() == "" ? "0" : Row["IDSolucao"].ToString();
                        OcorrenciaDropDownList.SelectedValue = Row["IDTipoOcorrencia"].ToString();
                        VendedorDropDownList.SelectedValue = Row["IDVendedor"].ToString();
                        MotivoDropDownList.SelectedValue = Row["IDMotivo"].ToString();
                        DescricaoTextBox.Text = Row["Descricao"].ToString();
                        ObjSAC.CodigoCliente = Row["CodigoCliente"].ToString();
                    }
                }

                CarregaDadosDaTela("");

                Session["TicketsDetalhe"] = ObjSAC;

            }

        }

        protected string CarregaDadosDaTela(string Button)
        {
            ObjSAC.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue ?? "0");
            ObjSAC.Cliente = ClienteTextBox.Text ?? "";
            ObjSAC.Solicitante = SolicitanteTextBox.Text ?? "";
            ObjSAC.IDResponsavel = Convert.ToInt32(ResponsavelDropDownList.SelectedValue ?? "0");
            ObjSAC.IDClassificacao = Convert.ToInt32(ClassificacaoDropDownList.SelectedValue ?? "0");
            ObjSAC.IDSituacao = Convert.ToInt32(SituacaoDropDownList.SelectedValue ?? "0");
            ObjSAC.DataSolicitacao = AberturaTextBox.Text ?? "";
            ObjSAC.DataFechamento = FechamentoTextBox.Text ?? "";
            ObjSAC.IDPrioridade = Convert.ToInt32(PrioridadeDropDownList.SelectedValue ?? "0");
            ObjSAC.IDSolucao = Convert.ToInt32(SolucaoDropDownList.SelectedValue == "" ? "0" : SolucaoDropDownList.SelectedValue);
            ObjSAC.IDTipoOcorrencia = Convert.ToInt32(OcorrenciaDropDownList.SelectedValue == "" ? "0" : OcorrenciaDropDownList.SelectedValue);
            ObjSAC.IDVendedor = Convert.ToInt32(VendedorDropDownList.SelectedValue == "" ? "0" : VendedorDropDownList.SelectedValue);
            ObjSAC.IDMotivo = Convert.ToInt32(MotivoDropDownList.SelectedValue == "" ? "0" : MotivoDropDownList.SelectedValue);
            ObjSAC.Descricao = DescricaoTextBox.Text ?? "";

            if (Button == "SalvarLinkButton_Click")
            {
                ObjSAC.IDTicket = ObjSAC.Operacao == "Inclusao" ? 0 : Convert.ToInt32(TicketTextBox.Text);

                if (ObjSAC.IDEmpresa == 0) return "Escolha uma empresa.";
                if (ObjSAC.Cliente == "") return "Informe o nome do cliente.";
                //if (ObjSAC.Solicitante == "") return "Informe o nome do solicitante.";
                if (ObjSAC.IDResponsavel == 0) return "Escolha um responsável.";
                if (ObjSAC.IDClassificacao == 0) return "Escolha uma classificação.";
                if (ObjSAC.IDSituacao == 0) return "Escolha uma situação.";
                if (ObjSAC.DataSolicitacao == "") return "Informe a data de abertura.";
                if (ObjSAC.DataFechamento == "" && (SituacaoDropDownList.SelectedItem.Text == "Finalizado"
                                                 || SituacaoDropDownList.SelectedItem.Text == "finalizado"))
                    return "Informe a data de fechamento.";
                if (ObjSAC.IDPrioridade == 0) return "Escolha a prioridade.";
                if (ObjSAC.IDSolucao == 0 && (SituacaoDropDownList.SelectedItem.Text == "Finalizado"
                                           || SituacaoDropDownList.SelectedItem.Text == "finalizado"))
                    return "Escolha uma solução.";
                if (ObjSAC.IDTipoOcorrencia == 0) return "Escolha um tipo de ocorrencia.";
                if (ObjSAC.IDVendedor == 0) return "Escolha um vendedor.";
                if (ObjSAC.IDMotivo == 0) return "Escolha um motivo.";
                if (ObjSAC.Descricao == "") return "Informe uma descrição.";
            }

            return "";
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["TicketsDetalhe"] != null) ObjSAC = (SACClass)Session["TicketsDetalhe"];

            if (Session["IDUsuario"] != null) ObjSAC.IDUsuario = Convert.ToInt32(Session["IDUsuario"].ToString());

            erro = CarregaDadosDaTela("SalvarLinkButton_Click");

            ObjSAC.EmailOperacao = ObjSAC.Operacao;

            if (erro == "") erro = ObjSAC.GravaTickets();

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
                ApresentaMensagem("");
                this.TicketWebUserControl.LiberaButtons();
            }
        }

        protected void TrocarClienteLinkButton_Click(object sender, EventArgs e)
        {
            if (Session["TicketsDetalhe"] != null)
            {
                ObjSAC = (SACClass)Session["TicketsDetalhe"];
            }

            CarregaDadosDaTela("TrocarClienteLinkButton_Click");

            Session["TicketsDetalhe"] = ObjSAC;

            Response.Redirect("~/SAC/TicketEscolhaClienteWebForm.aspx?indmnu=3");
        }

        protected void FormataEmail()
        {
            WSTicketDetalhe OBJTicketDetalhe = new WSTicketDetalhe();

            OBJTicketDetalhe.IDEmpresa = ObjSAC.IDEmpresa;
            OBJTicketDetalhe.IDTicket = ObjSAC.IDTicket;
            OBJTicketDetalhe.RetornaListaTicketsDetalhe();

            ObjSAC.NomeUsuario = Session["usuario"].ToString();

            ObjSAC.CabecalhoEmail = "Ticket - " + ObjSAC.IDTicket.ToString();

            ObjSAC.TituloEmail = ObjSAC.EmailOperacao + " de Ticket";

            ObjSAC.DetalheEmail += "<b>Empresa: </b> " + OBJTicketDetalhe.Empresa + "<br>";
            ObjSAC.DetalheEmail += "<b>Cliente: </b> " + OBJTicketDetalhe.Cliente + "<br>";
            ObjSAC.DetalheEmail += "<b>Solicitante: </b> " + OBJTicketDetalhe.Solicitante + "<br>";
            ObjSAC.DetalheEmail += "<b>Responsável: </b> " + OBJTicketDetalhe.Responsavel + "<br>";
            ObjSAC.DetalheEmail += "<b>Tratativa: </b> " + OBJTicketDetalhe.Tratativa + "<br>";
            ObjSAC.DetalheEmail += "<b>Situação: </b> " + OBJTicketDetalhe.Situacao + "<br>";
            ObjSAC.DetalheEmail += "<b>Prioridade: </b> " + OBJTicketDetalhe.Prioridade + "<br>";
            ObjSAC.DetalheEmail += "<b>Abertura: </b> " + OBJTicketDetalhe.Abertura + "<br>";
            ObjSAC.DetalheEmail += "<b>Fechamento: </b> " + OBJTicketDetalhe.Fechamento + "<br>";
            ObjSAC.DetalheEmail += "<b>Solução: </b> " + OBJTicketDetalhe.Solucao + "<br>";
            ObjSAC.DetalheEmail += "<b>Ocorrência: </b> " + OBJTicketDetalhe.Ocorrencia + "<br>";
            ObjSAC.DetalheEmail += "<b>Vendedor: </b> " + OBJTicketDetalhe.Vendedor + "<br>";
            ObjSAC.DetalheEmail += "<b>Motivo: </b> " + OBJTicketDetalhe.Motivo + "<br>";
            ObjSAC.DetalheEmail += "<b>Descrição: </b> " + OBJTicketDetalhe.Descricao + "<br>";
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
            Session["TicketsDetalhe"] = null;
            Response.Redirect("~/SAC/TicketsWebForm.aspx?indmnu=3");
        }
    }
}