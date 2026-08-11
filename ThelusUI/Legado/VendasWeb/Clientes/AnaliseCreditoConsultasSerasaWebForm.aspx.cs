using System;
using System.Data;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Text;

namespace VendasWeb.Clientes
{
    public partial class AnaliseCreditoConsultasSerasaWebForm : System.Web.UI.Page
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

                CarregaInformacoesSobreConsultas();

                CarregaUltimasConsultasRealizadas();
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

        protected void CarregaInformacoesSobreConsultas()
        {
            DataTable Dados = ObjCliente.CarregaGraficoInfSobCon();

            GraficoInfSobConColunasLiteral.Text = MontaGraficoInfSobConColunas(Dados);

            GraficoInfSobConMesAnoLiteral.Text = MontaGraficoInfSobConMesAno(Dados);
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

                    ConsultasTextBox.Text = row["valor"].ToString() + " no mês atual";
                }
            }
            else
            {
                GraficoDiv.Visible = false;
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

        protected void CarregaUltimasConsultasRealizadas()
        {
            UltimasConsultasRealizadasGridView.DataSource = ObjCliente.CarregaUltimasConsultasRealizadasTodas();
            UltimasConsultasRealizadasGridView.DataBind();
            UltimasConsultasRealizadasMultiView.Visible = true;
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

        protected void UltimasConsultasRealizadasGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UltimasConsultasRealizadasGridView.PageIndex = e.NewPageIndex;
            CarregaUltimasConsultasRealizadas();
        }
    }
}