using System;
using System.Data;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Clientes
{
    public partial class AnaliseCreditoAnotacoesNegativasWebForm : System.Web.UI.Page
    {
        ClienteClasse ObjCliente = new ClienteClasse();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass ObjSessao = new SessionClass();
        string CssClassGridView = "table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            ObjSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                if (Session["clienteClasse"] != null)
                {
                    ObjCliente = (ClienteClasse)Session["clienteClasse"];

                    IDClienteHiddenField.Value = ObjCliente.IDCliente.ToString();

                    IDAnaliseHiddenField.Value = ObjCliente.IDAnalise.ToString();

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

                CarregaAnotacoesNegativasDa();
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
                    FantasiaTextBox.Text = row["NOMEFANTASIA"].ToString();
                }
            }
        }

        #region Informações sobre anotações negativas da 

        protected void CarregaAnotacoesNegativasDa()
        {
            CarregaTextoAnotacoesNegativasDa();

            CarregaGridResumoAnotacoesNegativasDa();

            CarregaGridPefinAnotacoesNegativasDa();

            CarregaGridProtestoAnotacoesNegativasDa();

            CarregaGridChequesAnotacoesNegativasDa();

            CarregaGridParticipacaoFalenciaAnotacoesNegativasDa();

            CarregaGridRefinAnotacoesNegativasDa();

            CarregaGridAcaoJudicialAnotacoesNegativasDa();

            CarregaGridRechequeAnotacoesNegativasDa();

            CarregaGridDividaVencidaAnotacoesNegativasDa();
        }

        protected void CarregaTextoAnotacoesNegativasDa()
        {
            DataTable Dados = ObjCliente.CarregaTextoAnotacoesNegativasDaEmpresa();

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    AnotacoesNegativasPefinLinkButton.Text = row["Pefin"].ToString();
                    AnotacoesNegativasPefinLinkButton.CssClass = row["PefinButtonCssClass"].ToString();

                    AnotacoesNegativasRefinLinkButton.Text = row["Refin"].ToString();
                    AnotacoesNegativasRefinLinkButton.CssClass = row["RefinButtonCssClass"].ToString();

                    AnotacoesNegativasProtestoLinkButton.Text = row["Protesto"].ToString();
                    AnotacoesNegativasProtestoLinkButton.CssClass = row["ProtestoButtonCssClass"].ToString();

                    AnotacoesNegativasAcaoJudicialLinkButton.Text = row["AcaoJudicial"].ToString();
                    AnotacoesNegativasAcaoJudicialLinkButton.CssClass = row["AcaoJudicialButtonCssClass"].ToString();

                    AnotacoesNegativasChequesLinkButton.Text = row["Cheques"].ToString();
                    AnotacoesNegativasChequesLinkButton.CssClass = row["ChequesButtonCssClass"].ToString();

                    AnotacoesNegativasRechequeLinkButton.Text = row["Recheque"].ToString();
                    AnotacoesNegativasRechequeLinkButton.CssClass = row["RechequeButtonCssClass"].ToString();

                    AnotacoesNegativasParticipacaoFalenciaLinkButton.Text = row["ParticipacaoFalencia"].ToString();
                    AnotacoesNegativasParticipacaoFalenciaLinkButton.CssClass = row["ParticipacaoFalenciaButtonCssClass"].ToString();

                    AnotacoesNegativasDividaVencidaLinkButton.Text = row["DividaVencida"].ToString();
                    AnotacoesNegativasDividaVencidaLinkButton.CssClass = row["DividaVencidaButtonCssClass"].ToString();
                }
            }
        }

        protected void CarregaGridResumoAnotacoesNegativasDa()
        {
            ConcetreResumoGridView.CssClass = CssClassGridView;

            ConcetreResumoGridView.DataSource = ObjCliente.CarregaConcetreResumoEmpresa();
            ConcetreResumoGridView.DataBind();

            if (ConcetreResumoGridView.Rows.Count > 0) ConcetreResumoMultiView.Visible = true;
        }

        protected void CarregaGridPefinAnotacoesNegativasDa()
        {
            PefinGridView.CssClass = CssClassGridView;

            PefinGridView.DataSource = ObjCliente.CarregaPefinEmpresa();
            PefinGridView.DataBind();
            if (PefinGridView.Rows.Count > 0) PefinMultiView.Visible = true;
        }

        protected void CarregaGridProtestoAnotacoesNegativasDa()
        {
            ProtestoGridView.CssClass = CssClassGridView;

            ProtestoGridView.DataSource = ObjCliente.CarregaProtestoEmpresa();
            ProtestoGridView.DataBind();
            if (ProtestoGridView.Rows.Count > 0) ProtestoMultiView.Visible = true;
        }

        protected void CarregaGridChequesAnotacoesNegativasDa()
        {
            ChequesGridView.CssClass = CssClassGridView;

            ChequesGridView.DataSource = ObjCliente.CarregaChequesEmpresa();
            ChequesGridView.DataBind();
            if (ChequesGridView.Rows.Count > 0) ChequesMultiView.Visible = true;
        }

        protected void CarregaGridParticipacaoFalenciaAnotacoesNegativasDa()
        {
            ParticipacaoFalenciaGridView.CssClass = CssClassGridView;

            ParticipacaoFalenciaGridView.DataSource = ObjCliente.CarregaParticipacaoFalenciaEmpresa();
            ParticipacaoFalenciaGridView.DataBind();
            if (ParticipacaoFalenciaGridView.Rows.Count > 0) ParticipacaoFalenciaMultiView.Visible = true;
        }

        protected void CarregaGridRefinAnotacoesNegativasDa()
        {
            RefinGridView.CssClass = CssClassGridView;

            RefinGridView.DataSource = ObjCliente.CarregaRefinEmpresa();
            RefinGridView.DataBind();
            if (RefinGridView.Rows.Count > 0) RefinMultiView.Visible = true;
        }

        protected void CarregaGridAcaoJudicialAnotacoesNegativasDa()
        {
            AcaoJudicialGridView.CssClass = CssClassGridView;

            AcaoJudicialGridView.DataSource = ObjCliente.CarregaAcaoJudicialEmpresa();
            AcaoJudicialGridView.DataBind();
            if (AcaoJudicialGridView.Rows.Count > 0) AcaoJudicialMultiView.Visible = true;
        }

        protected void CarregaGridRechequeAnotacoesNegativasDa()
        {
            RechequeGridView.CssClass = CssClassGridView;

            RechequeGridView.DataSource = ObjCliente.CarregaRechequeEmpresa();
            RechequeGridView.DataBind();
            if (RechequeGridView.Rows.Count > 0) RechequeMultiView.Visible = true;
        }

        protected void CarregaGridDividaVencidaAnotacoesNegativasDa()
        {
            DividaVencidaGridView.CssClass = CssClassGridView;

            DividaVencidaGridView.DataSource = ObjCliente.CarregaDividaVencidaEmpresa();
            DividaVencidaGridView.DataBind();
            if (DividaVencidaGridView.Rows.Count > 0) DividaVencidaMultiView.Visible = true;
        }

        #endregion

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
            Response.Redirect("~/Clientes/AnaliseCreditoDetalheWebForm.aspx?indmnu=5");
        }
    }
}