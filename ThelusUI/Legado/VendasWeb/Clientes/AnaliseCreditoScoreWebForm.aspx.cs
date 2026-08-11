using System;
using System.Data;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Clientes
{
    public partial class AnaliseCreditoScoreWebForm : System.Web.UI.Page
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
                    InterpretacaoTextBox.Text = row["Interpretacao"].ToString();
                }
            }

            CarregaScoreSerasaInterpretacao();
        }

        protected void CarregaScoreSerasaInterpretacao()
        {
            DataTable Dados = ObjCliente.CarregaScoreSerasaInterpretacao();

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    ProbInadimTextBox.Text = row["ProbInadim"].ToString();
                    RiscoTextBox.Text = row["Risco"].ToString();
                    PraticasTextBox.Text = row["Praticas"].ToString();
                }
            }
        }

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