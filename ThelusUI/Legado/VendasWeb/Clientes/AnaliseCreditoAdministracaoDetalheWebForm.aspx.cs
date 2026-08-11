using System;
using System.Data;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Clientes
{
    public partial class AnaliseCreditoAdministracaoDetalheWebForm : System.Web.UI.Page
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
                if (Session["AnaliseCreditoAdministracao"] != null)
                {
                    ObjCliente = (ClienteClasse)Session["AnaliseCreditoAdministracao"];

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

                CarregaInformacoesAdministracao();

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

        protected void CarregaInformacoesAdministracao()
        {
            DataTable Dados = ObjCliente.CarregaAdministracaoDetalhe();

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    AdministradorTextBox.Text = row["Administracao"].ToString();
                    CPFTextBox.Text = row["CPFCNPJ"].ToString();
                    IdentidadeTextBox.Text = row["Identidade"].ToString();
                    EstadoCivilTextBox.Text = row["EstadoCivil"].ToString();
                    NaturalidadeTextBox.Text = row["Naturalidade"].ToString();
                    NacionalidadeTextBox.Text = row["Nacionalidade"].ToString();
                    VinculoTextBox.Text = row["Vinculo"].ToString();
                    EntradaTextBox.Text = row["Entrada"].ToString();
                    MandatoTextBox.Text = row["Mandato"].ToString();
                    TelefoneTextBox.Text = row["Telefone"].ToString();
                    CargoTextBox.Text = row["Cargo"].ToString();
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
            Response.Redirect("~/Clientes/AnaliseCreditoAdministracaoWebForm.aspx?indmnu=5");
        }
    }
}