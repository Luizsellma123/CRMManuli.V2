using System;
using System.Data;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Clientes
{
    public partial class AnaliseCreditoQuadroSocialDetalheWebForm : System.Web.UI.Page
    {
        ClienteClasse ObjCliente = new ClienteClasse();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass ObjSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            ObjSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                if (Session["AnaliseCreditoQuadroSocial"] != null)
                {
                    ObjCliente = (ClienteClasse)Session["AnaliseCreditoQuadroSocial"];

                    IDClienteHiddenField.Value = ObjCliente.IDCliente.ToString();

                    IDAnaliseHiddenField.Value = ObjCliente.IDAnalise.ToString();

                    CPFCNPJHiddenField.Value = ObjCliente.CPFCNPJ.ToString();

                    CarregaDadosNaTela();
                }

                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
            }
        }

        protected void CarregaDadosNaTela()
        {
            try
            {
                CarregaDetalhesPrincipais();

                CarregaInformacoesSocio();

                CarregaTextoAnotacoesNegativasSociosAdm();
            }
            catch (Exception ex)
            {
                ApresentaMensagem(ex.Message);
            }
        }

        protected void CarregaDetalhesPrincipais()
        {
            DataTable Dados = ObjCliente.CarregaAnaliseCreditoDetalhe();

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    AnaliseTextBox.Text = row["IDAnalise"].ToString();
                    DataTextBox.Text = row["DataAnalise"].ToString();
                    CodigoTextBox.Text = ObjCliente.CodigoCliente;
                    NomeTextBox.Text = row["RAZAO"].ToString();
                }
            }
        }

        protected void CarregaInformacoesSocio()
        {
            DataTable Dados = ObjCliente.CarregaQuadroSocialDetalhe();

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    SocioTextBox.Text = row["Socio"].ToString();
                    CNPJTextBox.Text = row["CPFCNPJ"].ToString();
                    EntradaTextBox.Text = row["Entrada"].ToString();
                    FundacaoTextBox.Text = row["Fundacao"].ToString();
                    TelefoneTextBox.Text = row["Telefone"].ToString();
                    VinculoTextBox.Text = row["Vinculo"].ToString();
                    CapitalVotanteTextBox.Text = row["CapitalVotante"].ToString();
                    TotalTextBox.Text = row["Total"].ToString();
                    EnderecoTextBox.Text = row["Endereco"].ToString();
                }
            }
        }

        protected void CarregaTextoAnotacoesNegativasSociosAdm()
        {
            DataTable Dados = ObjCliente.CarregaTextoAnotacoesNegativasSociosAdm();

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    PefinLinkButton.Text = row["Pefin"].ToString();
                    PefinLinkButton.CssClass = row["PefinButtonCssClass"].ToString();

                    RefinLinkButton.Text = row["Refin"].ToString();
                    RefinLinkButton.CssClass = row["RefinButtonCssClass"].ToString();

                    ProtestoLinkButton.Text = row["Protesto"].ToString();
                    ProtestoLinkButton.CssClass = row["ProtestoButtonCssClass"].ToString();

                    AcaoJudicialLinkButton.Text = row["AcaoJudicial"].ToString();
                    AcaoJudicialLinkButton.CssClass = row["AcaoJudicialButtonCssClass"].ToString();

                    ChequeSustadoCanceladoLinkButton.Text = row["ChequeSustadoCancelado"].ToString();
                    ChequeSustadoCanceladoLinkButton.CssClass = row["ChequeSustadoCanceladoButtonCssClass"].ToString();

                    ChequeSemFundoLinkButton.Text = row["ChequeSemFundo"].ToString();
                    ChequeSemFundoLinkButton.CssClass = row["ChequeSemFundoButtonCssClass"].ToString();

                    ParticipacaoFalenciaLinkButton.Text = row["ParticipacaoFalencia"].ToString();
                    ParticipacaoFalenciaLinkButton.CssClass = row["ParticipacaoFalenciaButtonCssClass"].ToString();

                    DividaVencidaLinkButton.Text = row["DividaVencida"].ToString();
                    DividaVencidaLinkButton.CssClass = row["DividaVencidaButtonCssClass"].ToString();
                }
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

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes/AnaliseCreditoQuadroSocialWebForm.aspx?indmnu=5");
        }

    }
}