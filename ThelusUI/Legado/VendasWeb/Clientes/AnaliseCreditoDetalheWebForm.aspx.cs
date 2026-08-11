using System;
using System.Text;
using System.Data;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Clientes
{
    public partial class AnaliseCreditoDetalheWebForm : System.Web.UI.Page
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

                CarregaScoreSerasaLimiteCredito();

                CarregaAnotacoesNegativasDaEmpresa();

                CarregaAnotacoesNegativasSociosAdm();

                CarregaQuadroSocial();

                CarregaInformacoesSobreConsultas();

                CarregaUltimasConsultasRealizadas();

                CarregaHistoricoDePagamentos();

                CarregaEvolucaoCompromissos();

                CarregaReferenciasDeNegocios();

                CarregaRelacionamentoComFornecedores();
            }
            catch (Exception ex)
            {
                ApresentaMensagem(ex.Message);
            }
        }

        #region Detalhes principais

        protected void CarregaDetalhesPrincipais()
        {
            DataTable Dados = ObjCliente.CarregaAnaliseCreditoDetalhe();

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    AnaliseTextBox.Text = row["IDAnalise"].ToString();
                    DataTextBox.Text = row["DataAnalise"].ToString();
                    CodigoTextBox.Text = ObjCliente.CodigoCliente == "" ? ObjCliente.IDCliente.ToString() : ObjCliente.CodigoCliente;
                    NomeTextBox.Text = row["RAZAO"].ToString();

                    try { CNPJTextBox.Text = ObjUtilClass.FormatCNPJ(ObjCliente.CNPJCliente); }
                    catch { CNPJTextBox.Text = ObjCliente.CNPJCliente; }

                    FantasiaTextBox.Text = row["NOMEFANTASIA"].ToString();
                    SituacaoCNPJTextBox.Text = row["DSSITUNOV"].ToString();
                    EnderecoCompletoTextBox.Text = row["EnderecoCompleto"].ToString();
                    TelefoneTextBox.Text = row["Telefone"].ToString();
                    SiteTextBox.Text = row["HOME"].ToString();
                    TipoSociedadeTextBox.Text = row["TPSOC"].ToString();
                    RegistroTextBox.Text = row["NRUTRG"].ToString();
                    RealizadoTextBox.Text = row["DTUTRG"].ToString();
                    NIRETextBox.Text = row["NIRE"].ToString();
                    AntecessoraTextBox.Text = row["Antecessora"].ToString();
                    FundacaoTextBox.Text = row["DATAFUND"].ToString();
                    InscricaoEstadualTextBox.Text = row["INSCRICAOESTADUAL"].ToString();
                    OpcaoTributariaTextBox.Text = row["OPCAOTRIBUTARIA"].ToString();
                    RamoAtividadeTextBox.Text = row["RAMOATV"].ToString();
                    AtividadeSerasaTextBox.Text = row["AtividadeSerasa"].ToString();
                    ImportacaoTextBox.Text = row["PCCOMPRA"].ToString();
                    ExportacaoTextBox.Text = row["PCVENDAS"].ToString();
                    ScoreTextBox.Text = row["FATORRISKSCORING"].ToString();
                    CNAETextBox.Text = row["CNAE"].ToString();
                    FiliaisTextBox.Text = row["QTFIL"].ToString();
                    InterpretacaoTextBox.Text = row["Interpretacao"].ToString();
                }
            }

            CarregaGrafiasSemelhantes();

            CarregaFraseAlerta();
        }

        protected void CarregaGrafiasSemelhantes()
        {
            DataTable Dados = ObjCliente.CarregaGrafiasSemelhantes();

            GrafiasSemelhantesLinkButton.Text = Dados.Rows.Count + " Variações Encontradas";

            GrafiasSemelhantesGridView.CssClass = CssClassGridView;

            GrafiasSemelhantesGridView.DataSource = Dados;
            GrafiasSemelhantesGridView.DataBind();
        }

        protected void CarregaFraseAlerta()
        {
            FraseAlertaLabel.Text = ObjCliente.CarregaFraseAlerta();

            if (FraseAlertaLabel.Text.Trim() != "") FraseAlertaDiv.Visible = true;
        }

        #endregion

        #region Score Serasa e Limite de Crédito

        protected void CarregaScoreSerasaLimiteCredito()
        {
            CarregaScoreSerasa();

            CarregaLimiteDeCredito();
        }

        protected void CarregaScoreSerasa()
        {
            GerarGraficoClass ObjGrafico = new GerarGraficoClass();

            DataTable RetornoDados = new DataTable();
            string TituloLegenda = "Personalizada";
            double totalScore = 0;
            int cont = 0;

            //recuperar Score Serasa
            RetornoDados = ObjCliente.CarregaScoreSerasa();
            totalScore = 0;

            if (RetornoDados.Rows.Count > 0)
            {
                string[] limites = new string[RetornoDados.Rows.Count];
                string[] valorLimites = new string[RetornoDados.Rows.Count];
                string[] background = new string[2];
                cont = 0;

                foreach (DataRow row in RetornoDados.Rows)
                {
                    limites[cont] = row["limite"].ToString();

                    if (row["limite"].ToString() == "Restante" && Convert.ToDecimal(row["total"]) <= 0)
                    {
                        valorLimites[cont] = "0";
                    }
                    else
                    {
                        valorLimites[cont] = Convert.ToDecimal(row["total"]).ToString().Replace(",", ".");
                    }

                    totalScore = Convert.ToInt32(row["EntVal"]);

                    cont++;
                }

                background[0] = "'#3da5f4'";
                background[1] = "'#f1536e'";

                //Verifica se existem dados para serem limpos
                if (ObjGrafico.itemDataFaturamentoSetList != null)
                {
                    ObjGrafico.itemDataFaturamentoSetList.Clear();
                }
                ObjGrafico.NomeVariaveis = limites;
                ObjGrafico.TotalScoreSerasa = totalScore.ToString();
                ObjGrafico.incluiDataSetFaturamento(valorLimites, TituloLegenda, background);

                ObjGrafico.GraficoScoreSerasa();
                LiteralGraficoScoreSerasa.Text = ObjGrafico.grafico.ToString();
            }
        }

        protected void CarregaLimiteDeCredito()
        {
            if (ObjCliente.CodigoCliente == "") ObjCliente.CodigoCliente = CodigoTextBox.Text;

            GerarGraficoClass ObjGrafico = new GerarGraficoClass();

            DataTable RetornoDados = new DataTable();
            string TituloLegenda = "Personalizada";
            double totalFaturamento = 0;
            int cont = 0;

            //recuperar faturamento Limite Crédito
            RetornoDados = ObjCliente.LimiteCreditoTomado();
            totalFaturamento = 0;

            string[] limites;
            string[] valorLimites;
            string[] background = new string[2];

            if (RetornoDados.Rows.Count > 0)
            {
                limites = new string[RetornoDados.Rows.Count];
                valorLimites = new string[RetornoDados.Rows.Count];
                cont = 0;

                foreach (DataRow row in RetornoDados.Rows)
                {
                    limites[cont] = row["limite"].ToString();

                    if (row["limite"].ToString() == "Disponível" && Convert.ToDecimal(row["total"]) <= 0)
                    {
                        valorLimites[cont] = "0";
                    }
                    else
                    {
                        valorLimites[cont] = Convert.ToDecimal(row["total"]).ToString().Replace(",", ".");
                    }

                    totalFaturamento = Convert.ToDouble(row["EntValLimCred"]);

                    cont++;
                }
            }
            else
            {
                limites = new string[2];
                valorLimites = new string[2];

                limites[0] = "Disponível";
                valorLimites[0] = "0";

                limites[1] = "Utilizado";
                valorLimites[1] = "0";

                totalFaturamento = 0;
            }

            background[0] = "'#3da5f4'";
            background[1] = "'#f1536e'";

            //Verifica se existem dados para serem limpos
            if (ObjGrafico.itemDataFaturamentoSetList != null)
            {
                ObjGrafico.itemDataFaturamentoSetList.Clear();
            }
            ObjGrafico.NomeVariaveis = limites;
            ObjGrafico.TotalFaturamento = totalFaturamento.ToString("C");
            ObjGrafico.incluiDataSetFaturamento(valorLimites, TituloLegenda, background);

            ObjGrafico.GraficoLimiteCredito();
            LiteralGraficoLimiteCredito.Text = ObjGrafico.grafico.ToString();
        }

        #endregion

        #region Informações sobre anotações negativas da empresa

        protected void CarregaAnotacoesNegativasDaEmpresa()
        {
            CarregaTextoAnotacoesNegativasDaEmpresa();

            CarregaGridResumoAnotacoesNegativasDaEmpresa();

            CarregaGridPefinAnotacoesNegativasDaEmpresa();

            CarregaGridProtestoAnotacoesNegativasDaEmpresa();

            CarregaGridChequesAnotacoesNegativasDaEmpresa();

            CarregaGridParticipacaoFalenciaAnotacoesNegativasDaEmpresa();

            CarregaGridRefinAnotacoesNegativasDaEmpresa();

            CarregaGridAcaoJudicialAnotacoesNegativasDaEmpresa();

            CarregaGridRechequeAnotacoesNegativasDaEmpresa();

            CarregaGridDividaVencidaAnotacoesNegativasDaEmpresa();
        }

        protected void CarregaTextoAnotacoesNegativasDaEmpresa()
        {
            DataTable Dados = ObjCliente.CarregaTextoAnotacoesNegativasDaEmpresa();

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    AnotacoesNegativasEmpresaPefinLinkButton.Text = row["Pefin"].ToString();
                    AnotacoesNegativasEmpresaPefinLinkButton.CssClass = row["PefinButtonCssClass"].ToString();

                    AnotacoesNegativasEmpresaRefinLinkButton.Text = row["Refin"].ToString();
                    AnotacoesNegativasEmpresaRefinLinkButton.CssClass = row["RefinButtonCssClass"].ToString();

                    AnotacoesNegativasEmpresaProtestoLinkButton.Text = row["Protesto"].ToString();
                    AnotacoesNegativasEmpresaProtestoLinkButton.CssClass = row["ProtestoButtonCssClass"].ToString();

                    AnotacoesNegativasEmpresaAcaoJudicialLinkButton.Text = row["AcaoJudicial"].ToString();
                    AnotacoesNegativasEmpresaAcaoJudicialLinkButton.CssClass = row["AcaoJudicialButtonCssClass"].ToString();

                    AnotacoesNegativasEmpresaChequesLinkButton.Text = row["Cheques"].ToString();
                    AnotacoesNegativasEmpresaChequesLinkButton.CssClass = row["ChequesButtonCssClass"].ToString();

                    AnotacoesNegativasEmpresaRechequeLinkButton.Text = row["Recheque"].ToString();
                    AnotacoesNegativasEmpresaRechequeLinkButton.CssClass = row["RechequeButtonCssClass"].ToString();

                    AnotacoesNegativasEmpresaParticipacaoFalenciaLinkButton.Text = row["ParticipacaoFalencia"].ToString();
                    AnotacoesNegativasEmpresaParticipacaoFalenciaLinkButton.CssClass = row["ParticipacaoFalenciaButtonCssClass"].ToString();

                    AnotacoesNegativasEmpresaDividaVencidaLinkButton.Text = row["DividaVencida"].ToString();
                    AnotacoesNegativasEmpresaDividaVencidaLinkButton.CssClass = row["DividaVencidaButtonCssClass"].ToString();

                    AnotacoesNegativasEmpresaTotalPendenciasTextBox.Text = row["TotalPendencias"].ToString();

                    AnotacoesNegativasEmpresaQuantidadeTextBox.Text = row["Quantidade"].ToString();
                }
            }
        }

        protected void CarregaGridResumoAnotacoesNegativasDaEmpresa()
        {
            ConcetreResumoEmpresaGridView.CssClass = CssClassGridView;

            ConcetreResumoEmpresaGridView.DataSource = ObjCliente.CarregaConcetreResumoEmpresa();
            ConcetreResumoEmpresaGridView.DataBind();

            ConcetreResumoEmpresaGridViewRows.Text = ConcetreResumoEmpresaGridView.Rows.Count.ToString();
        }

        protected void CarregaGridPefinAnotacoesNegativasDaEmpresa()
        {
            PefinEmpresaGridView.CssClass = CssClassGridView;

            PefinEmpresaGridView.DataSource = ObjCliente.CarregaPefinEmpresa();
            PefinEmpresaGridView.DataBind();

            PefinEmpresaGridViewRows.Text = PefinEmpresaGridView.Rows.Count.ToString();
        }

        protected void CarregaGridProtestoAnotacoesNegativasDaEmpresa()
        {
            ProtestoEmpresaGridView.CssClass = CssClassGridView;

            ProtestoEmpresaGridView.DataSource = ObjCliente.CarregaProtestoEmpresa();
            ProtestoEmpresaGridView.DataBind();

            ProtestoEmpresaGridViewRows.Text = ProtestoEmpresaGridView.Rows.Count.ToString();
        }

        protected void CarregaGridChequesAnotacoesNegativasDaEmpresa()
        {
            ChequesEmpresaGridView.CssClass = CssClassGridView;

            ChequesEmpresaGridView.DataSource = ObjCliente.CarregaChequesEmpresa();
            ChequesEmpresaGridView.DataBind();

            ChequesEmpresaGridViewRows.Text = ChequesEmpresaGridView.Rows.Count.ToString();
        }

        protected void CarregaGridParticipacaoFalenciaAnotacoesNegativasDaEmpresa()
        {
            ParticipacaoFalenciaEmpresaGridView.CssClass = CssClassGridView;

            ParticipacaoFalenciaEmpresaGridView.DataSource = ObjCliente.CarregaParticipacaoFalenciaEmpresa();
            ParticipacaoFalenciaEmpresaGridView.DataBind();

            ParticipacaoFalenciaEmpresaGridViewRows.Text = ParticipacaoFalenciaEmpresaGridView.Rows.Count.ToString();
        }

        protected void CarregaGridRefinAnotacoesNegativasDaEmpresa()
        {
            RefinEmpresaGridView.CssClass = CssClassGridView;

            RefinEmpresaGridView.DataSource = ObjCliente.CarregaRefinEmpresa();
            RefinEmpresaGridView.DataBind();

            RefinEmpresaGridViewRows.Text = RefinEmpresaGridView.Rows.Count.ToString();
        }

        protected void CarregaGridAcaoJudicialAnotacoesNegativasDaEmpresa()
        {
            AcaoJudicialEmpresaGridView.CssClass = CssClassGridView;

            AcaoJudicialEmpresaGridView.DataSource = ObjCliente.CarregaAcaoJudicialEmpresa();
            AcaoJudicialEmpresaGridView.DataBind();

            AcaoJudicialEmpresaGridViewRows.Text = AcaoJudicialEmpresaGridView.Rows.Count.ToString();
        }

        protected void CarregaGridRechequeAnotacoesNegativasDaEmpresa()
        {
            RechequeEmpresaGridView.CssClass = CssClassGridView;

            RechequeEmpresaGridView.DataSource = ObjCliente.CarregaRechequeEmpresa();
            RechequeEmpresaGridView.DataBind();

            RechequeEmpresaGridViewRows.Text = RechequeEmpresaGridView.Rows.Count.ToString();
        }

        protected void CarregaGridDividaVencidaAnotacoesNegativasDaEmpresa()
        {
            DividaVencidaEmpresaGridView.CssClass = CssClassGridView;

            DividaVencidaEmpresaGridView.DataSource = ObjCliente.CarregaDividaVencidaEmpresa();
            DividaVencidaEmpresaGridView.DataBind();

            DividaVencidaEmpresaGridViewRows.Text = DividaVencidaEmpresaGridView.Rows.Count.ToString();
        }

        #endregion

        #region Informações sobre anotações negativas dos socios e/ou administradores

        protected void CarregaAnotacoesNegativasSociosAdm()
        {
            CarregaTextoAnotacoesNegativasSociosAdm();

            CarregaGridAnotacoesNegativasSociosAdm();
        }

        protected void CarregaTextoAnotacoesNegativasSociosAdm()
        {
            DataTable Dados = ObjCliente.CarregaTextoAnotacoesNegativasSociosAdm();

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    AnotacoesNegativasSociosAdmPefinLinkButton.Text = row["Pefin"].ToString();
                    AnotacoesNegativasSociosAdmPefinLinkButton.CssClass = row["PefinButtonCssClass"].ToString();

                    AnotacoesNegativasSociosAdmRefinLinkButton.Text = row["Refin"].ToString();
                    AnotacoesNegativasSociosAdmRefinLinkButton.CssClass = row["RefinButtonCssClass"].ToString();

                    AnotacoesNegativasSociosAdmProtestoLinkButton.Text = row["Protesto"].ToString();
                    AnotacoesNegativasSociosAdmProtestoLinkButton.CssClass = row["ProtestoButtonCssClass"].ToString();

                    AnotacoesNegativasSociosAdmAcaoJudicialLinkButton.Text = row["AcaoJudicial"].ToString();
                    AnotacoesNegativasSociosAdmAcaoJudicialLinkButton.CssClass = row["AcaoJudicialButtonCssClass"].ToString();

                    AnotacoesNegativasSociosAdmChequeSustadoCanceladoLinkButton.Text = row["ChequeSustadoCancelado"].ToString();
                    AnotacoesNegativasSociosAdmChequeSustadoCanceladoLinkButton.CssClass = row["ChequeSustadoCanceladoButtonCssClass"].ToString();

                    AnotacoesNegativasSociosAdmChequeSemFundoLinkButton.Text = row["ChequeSemFundo"].ToString();
                    AnotacoesNegativasSociosAdmChequeSemFundoLinkButton.CssClass = row["ChequeSemFundoButtonCssClass"].ToString();

                    AnotacoesNegativasSociosAdmParticipacaoFalenciaLinkButton.Text = row["ParticipacaoFalencia"].ToString();
                    AnotacoesNegativasSociosAdmParticipacaoFalenciaLinkButton.CssClass = row["ParticipacaoFalenciaButtonCssClass"].ToString();

                    AnotacoesNegativasSociosAdmDividaVencidaLinkButton.Text = row["DividaVencida"].ToString();
                    AnotacoesNegativasSociosAdmDividaVencidaLinkButton.CssClass = row["DividaVencidaButtonCssClass"].ToString();

                    AnotacoesNegativasSociosAdmTotalPendenciasTextBox.Text = row["TotalPendencias"].ToString();

                    AnotacoesNegativasSociosAdmQuantidadeTextBox.Text = row["Quantidade"].ToString();
                }
            }
        }

        protected void CarregaGridAnotacoesNegativasSociosAdm()
        {
            AnotacoesNegativasSociosAdmGridView.CssClass = CssClassGridView;

            AnotacoesNegativasSociosAdmGridView.DataSource = ObjCliente.CarregaAnotacoesNegativasSociosAdm();
            AnotacoesNegativasSociosAdmGridView.DataBind();
        }

        protected void AnotacoesNegativasSociosAdmGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            AnotacoesNegativasSociosAdmGridView.PageIndex = e.NewPageIndex;
            CarregaGridAnotacoesNegativasSociosAdm();
        }

        protected void RedirecionaAnotacoesNegativasSociosAdmGridView(object sender, EventArgs e)
        {
            ObjCliente.IDCliente = Convert.ToInt32(IDClienteHiddenField.Value);
            ObjCliente.IDAnalise = Convert.ToInt32(IDAnaliseHiddenField.Value);

            char Vinculo = Convert.ToChar(((Label)((Control)sender).FindControl("VinculoAnotacoesNegativasSociosAdmGridViewLabel")).Text);

            Session["AnaliseCredito"] = ObjCliente;

            if (Vinculo == 'S' || Vinculo == 'D')
                Response.Redirect("~/Clientes/AnaliseCreditoQuadroSocialWebForm.aspx?indmnu=5");
            else
                Response.Redirect("~/Clientes/AnaliseCreditoAdministracaoWebForm.aspx?indmnu=5");
        }

        #endregion

        #region Quadro Social

        protected void CarregaQuadroSocial()
        {
            DataTable Dados = ObjCliente.CarregaQuadroSocial();

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    QuadroSocialCapitalSocialLinkButton.Text =
                        Convert.ToDecimal(row["CapitalSocial"]).ToString("C") + "<br> Capital Social";
                    QuadroSocialRealizadoLinkButton.Text =
                        Convert.ToDecimal(row["Realizado"]).ToString("C") + "<br> Realizado";
                    QuadroSocialOrigemLinkButton.Text =
                        row["Origem"].ToString() + "<br> Origem";
                    QuadroSocialControleLinkButton.Text =
                        row["Controle"].ToString() + "<br> Controle";
                    QuadroSocialNaturezaLinkButton.Text =
                        row["Natureza"].ToString() + "<br> Natureza";
                }
            }
        }

        #endregion

        #region Informações sobre consultas

        protected void CarregaInformacoesSobreConsultas()
        {
            DataTable Dados = ObjCliente.CarregaGraficoInfSobCon();

            if (Dados.Rows.Count > 0)
            {
                GraficoInfSobConColunasLiteral.Text = MontaGraficoInfSobConColunas(Dados);

                GraficoInfSobConMesAnoLiteral.Text = MontaGraficoInfSobConMesAno(Dados);
            }
            else
            {
                InformacoesSobreConsultasDiv.Visible = false;
            }
        }

        protected string MontaGraficoInfSobConColunas(DataTable Dados)
        {
            StringBuilder GraficoInfSobConColunas = new StringBuilder();

            GraficoInfSobConColunas.AppendLine("<table class=\"GraficoColunas\">");
            GraficoInfSobConColunas.AppendLine("<tr>");

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    GraficoInfSobConColunas.AppendLine("<td>");

                    GraficoInfSobConColunas.AppendLine("<div class=\"Coluna\">");

                    GraficoInfSobConColunas.AppendLine("<div style=\"top: " + row["top"].ToString() + "%; ");

                    GraficoInfSobConColunas.AppendLine("height: " + row["height"].ToString() + "%; ");

                    GraficoInfSobConColunas.AppendLine("background-color: " + row["backgroundcolor"].ToString() + ";\" ");

                    GraficoInfSobConColunas.AppendLine("class=\"Porcentagem\">" + row["valor"].ToString() + "</div>");

                    GraficoInfSobConColunas.AppendLine("</div>");

                    GraficoInfSobConColunas.AppendLine("</td>");
                }
            }

            GraficoInfSobConColunas.AppendLine("</tr>");
            GraficoInfSobConColunas.AppendLine("</table>");

            return GraficoInfSobConColunas.ToString();
        }

        protected string MontaGraficoInfSobConMesAno(DataTable Dados)
        {
            StringBuilder GraficoInfSobConMesAno = new StringBuilder();

            GraficoInfSobConMesAno.AppendLine("<table class=\"GraficoColunasMesAno\">");
            GraficoInfSobConMesAno.AppendLine("<tr>");

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    GraficoInfSobConMesAno.AppendLine("<td>");

                    GraficoInfSobConMesAno.AppendLine("<div class=\"MesAno\">" + row["MesAno"].ToString() + "</div>");

                    GraficoInfSobConMesAno.AppendLine("</td>");

                }
            }

            GraficoInfSobConMesAno.AppendLine("</tr>");
            GraficoInfSobConMesAno.AppendLine("</table>");

            return GraficoInfSobConMesAno.ToString();
        }

        #endregion

        #region Últimas 5 consultas Realizadas

        protected void CarregaUltimasConsultasRealizadas()
        {
            UltimasConsultasRealizadasGridView.CssClass = CssClassGridView;

            UltimasConsultasRealizadasGridView.DataSource = ObjCliente.CarregaUltimasConsultasRealizadas();
            UltimasConsultasRealizadasGridView.DataBind();

            if (UltimasConsultasRealizadasGridView.Rows.Count == 0)
                UltimasConsultasRealizadasDiv.Visible = false;
        }

        #endregion

        #region Histórico de pagamentos

        protected void CarregaHistoricoDePagamentos()
        {
            CarregaQuantidadeDeTitulos();

            CarregaMercadoValoresEmReais();

            if (QuantidadeDeTitulosRowsCountLabel.Text == "0")
                QuantidadeDeTitulosDiv.Visible = false;

            if (MercadoValoresEmReaisRowsCountLabel.Text == "0")
                MercadoValoresEmReaisDiv.Visible = false;

            if (QuantidadeDeTitulosRowsCountLabel.Text == "0" && MercadoValoresEmReaisRowsCountLabel.Text == "0")
                HistoricoDePagamentosDiv.Visible = false;
        }

        protected void CarregaQuantidadeDeTitulos()
        {
            DataTable Dados = ObjCliente.CarregaQuantidadeDeTitulos();

            QuantidadeDeTitulosRowsCountLabel.Text = Dados.Rows.Count.ToString();

            if (Dados.Rows.Count > 0)
            {
                int count = 1;

                foreach (DataRow row in Dados.Rows)
                {
                    switch (count)
                    {
                        case 1:
                            QuantidadeDeTitulosLinkButton1.Text = row["TextoButton"].ToString();
                            QuantidadeDeTitulosLinkButton1.CssClass = row["ButtonCssClass"].ToString();
                            break;

                        case 2:
                            QuantidadeDeTitulosLinkButton2.Text = row["TextoButton"].ToString();
                            QuantidadeDeTitulosLinkButton2.CssClass = row["ButtonCssClass"].ToString();
                            break;

                        case 3:
                            QuantidadeDeTitulosLinkButton3.Text = row["TextoButton"].ToString();
                            QuantidadeDeTitulosLinkButton3.CssClass = row["ButtonCssClass"].ToString();
                            break;

                        case 4:
                            QuantidadeDeTitulosLinkButton4.Text = row["TextoButton"].ToString();
                            QuantidadeDeTitulosLinkButton4.CssClass = row["ButtonCssClass"].ToString();
                            break;

                        case 5:
                            QuantidadeDeTitulosLinkButton5.Text = row["TextoButton"].ToString();
                            QuantidadeDeTitulosLinkButton5.CssClass = row["ButtonCssClass"].ToString();
                            break;

                        case 6:
                            QuantidadeDeTitulosLinkButton6.Text = row["TextoButton"].ToString();
                            QuantidadeDeTitulosLinkButton6.CssClass = row["ButtonCssClass"].ToString();
                            break;
                    }

                    count++;

                }
            }

        }

        protected void CarregaMercadoValoresEmReais()
        {
            DataTable Dados = ObjCliente.CarregaMercadoValoresEmReais();

            MercadoValoresEmReaisRowsCountLabel.Text = Dados.Rows.Count.ToString();

            if (Dados.Rows.Count > 0)
            {
                int count = 1;

                foreach (DataRow row in Dados.Rows)
                {
                    switch (count)
                    {
                        case 1:
                            MercadoLinkButton1.Text = row["TextoButton"].ToString();
                            MercadoLinkButton1.CssClass = row["ButtonCssClass"].ToString();
                            break;

                        case 2:
                            MercadoLinkButton2.Text = row["TextoButton"].ToString();
                            MercadoLinkButton2.CssClass = row["ButtonCssClass"].ToString();
                            break;

                        case 3:
                            MercadoLinkButton3.Text = row["TextoButton"].ToString();
                            MercadoLinkButton3.CssClass = row["ButtonCssClass"].ToString();
                            break;

                        case 4:
                            MercadoLinkButton4.Text = row["TextoButton"].ToString();
                            MercadoLinkButton4.CssClass = row["ButtonCssClass"].ToString();
                            break;

                        case 5:
                            MercadoLinkButton5.Text = row["TextoButton"].ToString();
                            MercadoLinkButton5.CssClass = row["ButtonCssClass"].ToString();
                            break;

                        case 6:
                            MercadoLinkButton6.Text = row["TextoButton"].ToString();
                            MercadoLinkButton6.CssClass = row["ButtonCssClass"].ToString();
                            break;
                    }

                    count++;

                }
            }

        }

        #endregion

        #region Evolução de Compromissos

        protected void CarregaEvolucaoCompromissos()
        {
            DataTable Dados = ObjCliente.CarregaEvolucaoCompromissos();

            if (Dados.Rows.Count > 0)
            {
                GraficoEvolucaoCompromissoColunasLiteral.Text = MontaGraficoEvolucaoCompromissoColunas(Dados);

                GraficoEvolucaoCompromissoMesAnoLiteral.Text = MontaGraficoEvolucaoCompromissoMesAno(Dados);

                GraficoEvolucaoCompromissoDescricaoLiteral.Text = MontaEvolucaoCompromissoDescricaoLiteral(Dados);
            }
            else
            {
                EvolucaoDeCompromissosDiv.Visible = false;
            }
        }

        protected string MontaGraficoEvolucaoCompromissoColunas(DataTable Dados)
        {
            StringBuilder GraficoInfSobConColunas = new StringBuilder();

            GraficoInfSobConColunas.AppendLine("<table class=\"GraficoColunas\">");
            GraficoInfSobConColunas.AppendLine("<tr>");

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    GraficoInfSobConColunas.AppendLine("<td>");

                    GraficoInfSobConColunas.AppendLine("<div class=\"Coluna\">");

                    GraficoInfSobConColunas.AppendLine("<div style=\"top: " + row["top"].ToString() + "%; ");

                    GraficoInfSobConColunas.AppendLine("height: " + row["height"].ToString() + "%; ");

                    GraficoInfSobConColunas.AppendLine("background-color: " + row["backgroundcolor"].ToString() + ";\" ");

                    GraficoInfSobConColunas.AppendLine("class=\"Porcentagem\">" + row["Codigo"].ToString() + "</div>");

                    GraficoInfSobConColunas.AppendLine("</div>");

                    GraficoInfSobConColunas.AppendLine("</td>");
                }
            }

            GraficoInfSobConColunas.AppendLine("</tr>");
            GraficoInfSobConColunas.AppendLine("</table>");

            return GraficoInfSobConColunas.ToString();
        }

        protected string MontaGraficoEvolucaoCompromissoMesAno(DataTable Dados)
        {
            StringBuilder GraficoInfSobConMesAno = new StringBuilder();

            GraficoInfSobConMesAno.AppendLine("<table class=\"GraficoColunasMesAno\">");
            GraficoInfSobConMesAno.AppendLine("<tr>");

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    GraficoInfSobConMesAno.AppendLine("<td>");

                    GraficoInfSobConMesAno.AppendLine("<div class=\"MesAno\">" + row["MesAno"].ToString() + "</div>");

                    GraficoInfSobConMesAno.AppendLine("</td>");

                }
            }

            GraficoInfSobConMesAno.AppendLine("</tr>");
            GraficoInfSobConMesAno.AppendLine("</table>");

            return GraficoInfSobConMesAno.ToString();
        }

        protected string MontaEvolucaoCompromissoDescricaoLiteral(DataTable Dados)
        {
            StringBuilder GraficoInfSobConMesAno = new StringBuilder();

            GraficoInfSobConMesAno.AppendLine("<table class=\"GraficoColunasMesAno\">");
            GraficoInfSobConMesAno.AppendLine("<tr>");

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    GraficoInfSobConMesAno.AppendLine("<td>");

                    GraficoInfSobConMesAno.AppendLine("<div class=\"Descricao\">" + row["Descricao"].ToString() + "</div>");

                    GraficoInfSobConMesAno.AppendLine("</td>");

                }
            }

            GraficoInfSobConMesAno.AppendLine("</tr>");
            GraficoInfSobConMesAno.AppendLine("</table>");

            return GraficoInfSobConMesAno.ToString();
        }

        #endregion

        #region Referenciais de negócios (valores em reais)

        protected void CarregaReferenciasDeNegocios()
        {
            ReferenciasDeNegociosGridView.CssClass = CssClassGridView;

            DataTable DadosInput = ObjCliente.CarregaReferenciasDeNegocios();

            DataTable DadosOutput = new DataTable();

            DadosOutput.Columns.Add("Tipo", typeof(string));

            if (DadosInput.Rows.Count > 0)
            {
                //Adiciona as colunas
                foreach (DataRow row in DadosInput.Rows)
                {
                    DadosOutput.Columns.Add(row["POTENC"].ToString(), typeof(string));
                }

                //Adiciona as linhas
                for (int i = 0; i < DadosOutput.Columns.Count - 1; i++)
                {
                    DadosOutput.Rows.Add();

                    int j = 0;
                    string rowNome = "";

                    switch (i)
                    {
                        case 0:
                            DadosOutput.Rows[i][j] = "Data";
                            rowNome = "AAAAMM";
                            break;

                        case 1:
                            DadosOutput.Rows[i][j] = "Valor";
                            rowNome = "DESCRFAIXAPOT";
                            break;

                        case 2:
                            DadosOutput.Rows[i][j] = "Média";
                            rowNome = "DESCRFAIXAMED";
                            break;

                    }

                    foreach (DataRow row in DadosInput.Rows)
                    {
                        j++;

                        DadosOutput.Rows[i][j] = row[rowNome].ToString();
                    }
                }
            }

            if (DadosOutput.Rows.Count > 0)
            {
                ReferenciasDeNegociosGridView.DataSource = DadosOutput;
                ReferenciasDeNegociosGridView.DataBind();
            }
            else
            {
                ReferenciaisDeNegociosValoresEmReiasDiv.Visible = false;
            }
        }

        #endregion

        #region Relacionamento com fornecedores

        protected void CarregaRelacionamentoComFornecedores()
        {
            DataTable Dados = ObjCliente.CarregaRelacionamentoComFornecedores();

            if (Dados.Rows.Count > 0)
            {
                int count = 1;

                foreach (DataRow row in Dados.Rows)
                {
                    switch (count)
                    {
                        case 1:
                            RelacionamentoComFornecedoresLinkButton1.Text = row["Text"].ToString();
                            break;

                        case 2:
                            RelacionamentoComFornecedoresLinkButton2.Text = row["Text"].ToString();
                            break;

                        case 3:
                            RelacionamentoComFornecedoresLinkButton3.Text = row["Text"].ToString();
                            break;

                        case 4:
                            RelacionamentoComFornecedoresLinkButton4.Text = row["Text"].ToString();
                            break;

                        case 5:
                            RelacionamentoComFornecedoresLinkButton5.Text = row["Text"].ToString();
                            break;

                        case 6:
                            RelacionamentoComFornecedoresLinkButton6.Text = row["Text"].ToString();
                            break;

                        case 7:
                            RelacionamentoComFornecedoresLinkButton7.Text = row["Text"].ToString();
                            break;
                    }

                    count++;
                }
            }
            else
            {
                RelacionamentoComFornecedoresDiv.Visible = false;
            }
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
            Response.Redirect("~/Clientes/AnaliseCreditoWebForm.aspx?indmnu=5");
        }

    }
}