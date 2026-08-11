using System;
using System.Data;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Text;

namespace VendasWeb.Clientes
{
    public partial class AnaliseCreditoEvolucaoCompromissosWebForm : System.Web.UI.Page
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

                CarregaEvolucaoCompromissos();
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