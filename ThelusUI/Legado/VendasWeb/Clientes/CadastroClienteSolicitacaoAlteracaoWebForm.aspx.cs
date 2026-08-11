using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Clientes
{
    public partial class CadastroClienteSolicitacaoAlteracaoWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        ClienteClasse OBJCliente = new ClienteClasse();
        UtilClass ObjUtilClass = new UtilClass();
        HistoricosClass OBJHistorico = new HistoricosClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

                //Verificando se deve mandar alerta
                if (Session["Msg"] != null)
                {

                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                    Session.Remove("Msg");
                }


                if (Session["clienteClasse"] != null)
                {
                    //Descarega a session da Entidade
                    OBJCliente = (ClienteClasse)Session["clienteClasse"];

                    //Carrega dados
                    CarregaDadosNaTela();

                }
            }
        }

        public void CarregaDadosNaTela()
        {
            //recupera dados principais da tela
            OBJCliente.carregaDadosPrincipais();

            IDCliente.Value = OBJCliente.IDCliente.ToString();
            if (OBJCliente.CodigoCliente != "")
            {
                CodigoClienteTextBox.Text = OBJCliente.CodigoCliente;
            }
            else
            {
                CodigoClienteTextBox.Text = OBJCliente.IDCliente.ToString();
            }
            NomeClienteTextBox.Text = OBJCliente.NomeCliente;


            DataTable RetornoDados = new DataTable();

            RetornoDados = OBJCliente.CarregaTiposSolicitacao();
            TipoSolicitacaoDropDownList.DataSource = RetornoDados;
            TipoSolicitacaoDropDownList.DataValueField = "IDTipoSolicitacao";
            TipoSolicitacaoDropDownList.DataTextField = "Descricao";
            TipoSolicitacaoDropDownList.DataBind();
            TipoSolicitacaoDropDownList.Items.Insert(0, new ListItem("Selecione um tipo de solicitação.", ""));

        }

        protected void EnviarEmailButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            string cli = CodigoClienteTextBox.Text.Substring(0, 3);

            if (cli != "CLI")
                erro = "O cliente é prospectivo ainda";

            if (erro == "")
            {
                erro = EnviaEmail();

                if (erro == "") erro = GravaHistoricoCliente();

                string TipoSolicitacao = TipoSolicitacaoDropDownList.SelectedItem.ToString();

                if (erro == "" && TipoSolicitacao == "Alteração Classificação Comercial")
                    erro = Grava_Solicitacao_Classificacao_Comercial();

                if (erro == "") erro = MudaStatusCliente(TipoSolicitacao);
            }

            ApresentaMensagem(erro);
        }

        protected string EnviaEmail()
        {
            try
            {
                //Verifica se tem arquivo anexo para enviar
                if (ArquivoFileUpload.HasFile == true)
                {
                    MemoryStream MSAnexo = new MemoryStream(ArquivoFileUpload.FileBytes);
                    OBJCliente.EmailAnexo = new System.Net.Mail.Attachment(MSAnexo, ArquivoFileUpload.FileName);
                }

                OBJCliente.EmailTipoSolicitacao = Convert.ToInt32(TipoSolicitacaoDropDownList.SelectedValue);
                OBJCliente.EmailDescricaoTipoSolicitacao = TipoSolicitacaoDropDownList.SelectedItem.ToString();
                OBJCliente.EmailDescricao = DescricaoObservacaoTextBox.Text;
                OBJCliente.NomeCliente = NomeClienteTextBox.Text;
                OBJCliente.CodigoCliente = CodigoClienteTextBox.Text;

                return OBJCliente.EnviaEmail();
            }
            catch
            {
                return "Erro ao enviar o email.";
            }
        }

        protected string GravaHistoricoCliente()
        {
            OBJHistorico.IDTipoHistorico = 1;
            OBJHistorico.IDEvento = 6;
            OBJHistorico.IDCategoria = 1;
            OBJHistorico.Historico = DescricaoObservacaoTextBox.Text;
            OBJHistorico.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
            OBJHistorico.IDCliente = Convert.ToInt32(IDCliente.Value);

            return OBJHistorico.GravaHistoricoCliente();
        }

        protected string Grava_Solicitacao_Classificacao_Comercial()
        {
            OBJCliente.IDCliente = Convert.ToInt32(IDCliente.Value);
            OBJCliente.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
            OBJCliente.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"]);
            OBJCliente.IDHistorico = OBJHistorico.IDHistorico;

            return OBJCliente.Grava_Solicitacao_Classificacao_Comercial();
        }

        protected string MudaStatusCliente(string TipoSolicitacao)
        {
            OBJCliente.CodigoUsuario = Session["usuario"].ToString();
            OBJCliente.IDCliente = Convert.ToInt32(IDCliente.Value);
            OBJCliente.CarregaClienteTipoSolicitacaoStatus(TipoSolicitacao);

            return OBJCliente.AlteraStatusCliente();
        }

        protected void ApresentaMensagem(string erro)
        {
            if (erro == "")
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Solicitação de alteração enviada com sucesso!", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroClienteWebForm.aspx?indmnu=2");
        }
    }
}