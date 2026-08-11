using System;
using System.IO;
using System.Data;
using System.Text;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI.WebControls;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using VendasWeb.WEBServiceSAP.ClassesWEBService;

namespace VendasWeb.Controladoria
{
    public partial class PosicaoFinanceiraResumoWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        ControladoriaClass objControladoriaClass = new ControladoriaClass();
        GerarGraficoClass objGerarGraficoClass = new GerarGraficoClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(Session["Msg"].ToString(), true);
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

                CarregaGraficos();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            try
            {
                if (Session["PosicaoFinanceiraResumo"] != null)
                {
                    objControladoriaClass = (ControladoriaClass)Session["PosicaoFinanceiraResumo"];

                    if (objControladoriaClass.PeriodoInicial.ToString("dd-MM-yyyy") != "01-01-0001")
                        PeriodoInicialModalTextBox.Text = objControladoriaClass.PeriodoInicial.ToString("yyyy-MM-dd");

                    if (objControladoriaClass.PeriodoFinal.ToString("dd-MM-yyyy") != "01-01-0001")
                        PeriodoFinalModalTextBox.Text = objControladoriaClass.PeriodoFinal.ToString("yyyy-MM-dd");
                }

                if (Session["PosicaoFinanceiraDetalhe"] != null)
                    objControladoriaClass = (ControladoriaClass)Session["PosicaoFinanceiraDetalhe"];

                DataTable ControladoriaDataTable = objControladoriaClass.Consulta_CRM_POSICAO_DIARIA();

                if (ControladoriaDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in ControladoriaDataTable.Rows)
                    {
                        PosicaoTextBox.Text = objControladoriaClass.IDPosicaoDiaria.ToString();
                        UsuarioTextBox.Text = row["Usuario"].ToString();
                        PeriodoInicialTextBox.Text = row["PeriodoInicial"].ToString();
                        PeriodoFinalTextBox.Text = row["PeriodoFinal"].ToString();
                        GeracaoTextBox.Text = row["Geracao"].ToString();
                    }
                }

                CarregaGraficos();
            }
            catch (Exception ex)
            {
                ApresentaMensagem(ex.Message);
            }
        }

        protected void CarregaIDPosicaoDiaria()
        {
            objControladoriaClass.IDPosicaoDiaria = Convert.ToInt32(PosicaoTextBox.Text);
        }

        protected void CarregaGraficos()
        {
            CarregaIDPosicaoDiaria();

            CarregaGraficoConsolidadoFaturamentoPendentes();

            CarregaGraficoConsolidadoFaturamento();

            CarregaGraficoCustoMedio();

            CarregaGraficoFaturamento();

            CarregaGraficoPendentes();

            CarregaGraficoDevolucoes();
        }

        #region Graficos       

        protected void CarregaGraficoConsolidadoFaturamentoPendentes()
        {
            StringBuilder ConsFatuPendScript = new StringBuilder();

            ConsFatuPendScript.AppendLine("<script>");

            ConsFatuPendScript.AppendLine("");

            ConsFatuPendScript.AppendLine(objGerarGraficoClass.MontaGraficoBarrasDinamico("GraficoConsFatuPendTotal",
                objControladoriaClass.Consulta_POSICAO_DIARIA_Consolidado_Faturamento_Pendentes("Total")));

            ConsFatuPendScript.AppendLine(objGerarGraficoClass.MontaGraficoBarrasDinamico("GraficoConsFatuPendQtdTotal",
                objControladoriaClass.Consulta_POSICAO_DIARIA_Consolidado_Faturamento_Pendentes("TotalQuantidade")));

            ConsFatuPendScript.AppendLine("");

            ConsFatuPendScript.AppendLine("</script>");

            ConsFatuPendScriptLiteral.Text = ConsFatuPendScript.ToString();
        }

        protected void CarregaGraficoConsolidadoFaturamento()
        {
            StringBuilder ConsFatuScript = new StringBuilder();

            ConsFatuScript.AppendLine("<script>");

            ConsFatuScript.AppendLine("");

            ConsFatuScript.AppendLine(objGerarGraficoClass.MontaGraficoBarrasDinamico("GraficoConsFatuTotal",
                objControladoriaClass.Consulta_POSICAO_DIARIA_Consolidado_Faturamento("Total")));

            ConsFatuScript.AppendLine(objGerarGraficoClass.MontaGraficoBarrasDinamico("GraficoConsFatuQtdTotal",
                objControladoriaClass.Consulta_POSICAO_DIARIA_Consolidado_Faturamento("TotalQuantidade")));

            ConsFatuScript.AppendLine("");

            ConsFatuScript.AppendLine("</script>");

            ConsFatuScriptLiteral.Text = ConsFatuScript.ToString();
        }

        protected void CarregaGraficoCustoMedio()
        {
            StringBuilder CustoMedioScript = new StringBuilder();

            CustoMedioScript.AppendLine("<script>");

            CustoMedioScript.AppendLine("");

            CustoMedioScript.AppendLine(objGerarGraficoClass.MontaGraficoBarrasDinamico("GraficoCustoMedio",
                 objControladoriaClass.Consulta_POSICAO_DIARIA_Consolidado_Custo_Medio(0)));

            CustoMedioScript.AppendLine(objGerarGraficoClass.MontaGraficoBarrasDinamico("GraficoConsCustoMedio",
            objControladoriaClass.Consulta_POSICAO_DIARIA_Consolidado_Custo_Medio(1)));

            CustoMedioScript.AppendLine("");

            CustoMedioScript.AppendLine("</script>");

            ValorMedioScriptLiteral.Text = CustoMedioScript.ToString();
        }

        protected void CarregaGraficoFaturamento()
        {
            StringBuilder FaturamentoScript = new StringBuilder();

            FaturamentoScript.AppendLine("<script>");

            FaturamentoScript.AppendLine("");

            FaturamentoScript.AppendLine(objGerarGraficoClass.MontaGraficoBarrasDinamico("GraficoFaturamento",
                 objControladoriaClass.Consulta_POSICAO_DIARIA_Faturamento("Total")));

            FaturamentoScript.AppendLine(objGerarGraficoClass.MontaGraficoBarrasDinamico("GraficoFaturamentoQtd",
            objControladoriaClass.Consulta_POSICAO_DIARIA_Faturamento("TotalQuantidade")));

            FaturamentoScript.AppendLine("");

            FaturamentoScript.AppendLine("</script>");

            FaturamentoScriptLiteral.Text = FaturamentoScript.ToString();
        }

        protected void CarregaGraficoPendentes()
        {
            StringBuilder PendentesScript = new StringBuilder();

            PendentesScript.AppendLine("<script>");

            PendentesScript.AppendLine("");

            PendentesScript.AppendLine(objGerarGraficoClass.MontaGraficoBarrasDinamico("GraficoPendentes",
                 objControladoriaClass.Consulta_POSICAO_DIARIA_Pendentes("Total")));

            PendentesScript.AppendLine(objGerarGraficoClass.MontaGraficoBarrasDinamico("GraficoPendentesQtd",
            objControladoriaClass.Consulta_POSICAO_DIARIA_Pendentes("TotalQuantidade")));

            PendentesScript.AppendLine("");

            PendentesScript.AppendLine("</script>");

            PendentesScriptLiteral.Text = PendentesScript.ToString();
        }

        protected void CarregaGraficoDevolucoes()
        {
            StringBuilder DevolucoesScript = new StringBuilder();

            DevolucoesScript.AppendLine("<script>");

            DevolucoesScript.AppendLine("");

            DevolucoesScript.AppendLine(objGerarGraficoClass.MontaGraficoBarrasDinamico("GraficoDevolucoes",
                 objControladoriaClass.Consulta_POSICAO_DIARIA_Devolucoes("Total")));

            DevolucoesScript.AppendLine(objGerarGraficoClass.MontaGraficoBarrasDinamico("GraficoDevolucoesQtd",
            objControladoriaClass.Consulta_POSICAO_DIARIA_Devolucoes("TotalQuantidade")));

            DevolucoesScript.AppendLine("");

            DevolucoesScript.AppendLine("</script>");

            DevolucoesScriptLiteral.Text = DevolucoesScript.ToString();
        }

        #endregion

        protected void BaixarLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                enviarEmail OBJMail = new enviarEmail();

                CarregaIDPosicaoDiaria();

                //DataTable Excel = objControladoriaClass.CarregaResumoTabelaExcel();

                string tabelaHTML = "";

                //colocar as bordas nas celulas
                //tabelaHTML += "<style>table{border-collapse:collapse;}th,td{border:1px solid black;padding:8px;}</style>";

                //tabelaHTML += OBJMail.FormataTextoPosicaoDiariaTabela(Excel);

                tabelaHTML += OBJMail.FormataHTMLPosicaoDiaria_Tabela(objControladoriaClass.IDPosicaoDiaria, true);

                SalvaTabelaHTMLComoExcel(tabelaHTML, "ConsolidadoGeral");
            }
            catch (Exception ex)
            {
                ApresentaMensagem(ex.Message);
            }
        }

        protected void EnviarEmailLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteClasse objClienteClasseAux = new ClienteClasse();

                usuario objUsuario = new usuario();

                DataTable PosicaoDiariaEmails = objControladoriaClass.Consulta_POSICAO_DIARIA_PARM_EMAIL();

                CarregaIDPosicaoDiaria();

                if (PosicaoDiariaEmails.Rows.Count > 0)
                {
                    foreach (DataRow row in PosicaoDiariaEmails.Rows)
                    {
                        enviarEmail OBJMail = new enviarEmail();
                      
                        OBJMail.FormataHTMLPosicaoDiaria(objControladoriaClass.IDPosicaoDiaria);
                      
                        OBJMail.EmailDestinatario = row["Email"].ToString();

                        OBJMail.enviaEmailPosicaoDiariaFormatadoComAnexos();
                    }
                }
            }
            catch (Exception ex)
            {
                ApresentaMensagem(ex.Message);
            }
        }

        protected void GerarModalLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (PeriodoInicialModalTextBox.Text == "" || PeriodoInicialModalTextBox.Text == null)
            {
                erro = "Escolha o período incial.";

                objControladoriaClass.PeriodoInicial = Convert.ToDateTime("01-01-0001");
            }
            else
                objControladoriaClass.PeriodoInicial = Convert.ToDateTime(PeriodoInicialModalTextBox.Text);

            if (erro == "")
            {
                if (PeriodoFinalModalTextBox.Text == "" || PeriodoFinalModalTextBox.Text == null)
                {
                    erro = "Escolha o período final.";

                    objControladoriaClass.PeriodoFinal = Convert.ToDateTime("01-01-0001");
                }
                else
                    objControladoriaClass.PeriodoFinal = Convert.ToDateTime(PeriodoFinalModalTextBox.Text);
            }

            if (erro == "")
            {
                if (objControladoriaClass.PeriodoFinal < objControladoriaClass.PeriodoInicial)
                    erro = "O Período final não pode ser menor que o inicial.";
            }

            IDPosicaoDiariaClass objIDPosicaoDiariaClass = new IDPosicaoDiariaClass();

            if (erro == "")
            {
                WSRetornoJSONClass objWSRetornoJSONClass = objControladoriaClass.Gera_Posicao_Diaria(Convert.ToInt32(Session["IDUsuario"]));

                JsonConversao jsonconv = new JsonConversao();

                objIDPosicaoDiariaClass = jsonconv.ConverteJSonParaObject<IDPosicaoDiariaClass>(objWSRetornoJSONClass.JSONRetorno);

                erro = objWSRetornoJSONClass.MsgRetorno;
            }

            if (erro == "")
            {
                objControladoriaClass.IDPosicaoDiaria = objIDPosicaoDiariaClass.IDPosicaoDiaria;

                Session["PosicaoFinanceiraDetalhe"] = objControladoriaClass;

                Session["PosicaoFinanceiraResumo"] = null;
            }
            else
            {
                Session["Msg"] = erro;

                CarregaIDPosicaoDiaria();

                Session["PosicaoFinanceiraResumo"] = objControladoriaClass;
            }

            Response.Redirect("~/Controladoria/PosicaoFinanceiraResumoWebForm.aspx?indmnu=5");
        }

        public class IDPosicaoDiariaClass
        {
            public int IDPosicaoDiaria { get; set; }
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/PosicaoFinanceiraWebForm.aspx?indmnu=3");
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
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void SalvaTabelaHTMLComoExcel(string tabelaHTML, string nome)
        {
            MemoryStream stream = new MemoryStream();

            // Converta a string HTML em um fluxo de memória
            byte[] byteArray = Encoding.UTF8.GetBytes(tabelaHTML);
            stream.Write(byteArray, 0, byteArray.Length);
            stream.Position = 0;

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=" + nome + ".xls");

            // Copie o conteúdo do fluxo de memória para o fluxo de resposta
            stream.WriteTo(Response.OutputStream);

            Response.End();
        }
    }
}