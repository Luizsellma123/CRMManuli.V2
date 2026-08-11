using System;
using System.Data;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Clientes
{
    public partial class AnaliseCreditoReferencialNegociosWebForm : System.Web.UI.Page
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

                CarregaRelacionamentoComFornecedores();

                CarregaReferenciasDeNegocios();
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

        protected void CarregaReferenciasDeNegocios()
        {
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

            ReferencialNegociosGridView.DataSource = DadosOutput;
            ReferencialNegociosGridView.DataBind();
            ReferencialNegociosMultiView.Visible = true;
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

        protected void ReferencialNegociosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ReferencialNegociosGridView.PageIndex = e.NewPageIndex;
            CarregaReferenciasDeNegocios();
        }
    }
}