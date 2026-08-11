using System;
using System.Data;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Clientes
{
    public partial class AnaliseCreditoHistoricoPagamentosWebForm : System.Web.UI.Page
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

                CarregaQuantidadeDeTitulos();

                CarregaMercadoValoresEmReais();
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

        protected void CarregaQuantidadeDeTitulos()
        {
            DataTable Dados = ObjCliente.CarregaQuantidadeDeTitulos();

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
            else
            {
                QuantidadeDeTitulosDiv.Visible = false;
            }

        }

        protected void CarregaMercadoValoresEmReais()
        {
            DataTable Dados = ObjCliente.CarregaMercadoValoresEmReais();

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
            else
            {
                MercadoValoresEmReaisDiv.Visible = false;
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
            Response.Redirect("~/Clientes/AnaliseCreditoDetalheWebForm.aspx?indmnu=5");
        }
    }
}