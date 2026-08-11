using System;
using System.Data;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Clientes
{
    public partial class AnaliseCreditoQuadroSocialWebForm : System.Web.UI.Page
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

                CarregaQuadroSocial();

                CarregaQuadroSocialGridView();
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

        protected void CarregaQuadroSocial()
        {
            DataTable Dados = ObjCliente.CarregaQuadroSocial();

            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow row in Dados.Rows)
                {
                    CapitalSocialTextBox.Text = Convert.ToDecimal(row["CapitalSocial"]).ToString("C");
                    RealizadoTextBox.Text = Convert.ToDecimal(row["Realizado"]).ToString("C");
                    OrigemTextBox.Text = row["Origem"].ToString();
                    ControleTextBox.Text = row["Controle"].ToString();
                    NaturezaTextBox.Text = row["Natureza"].ToString();
                }
            }
        }

        protected void CarregaQuadroSocialGridView()
        {
            DataTable Dados = ObjCliente.CarregaQuadroSocialGridView();

            QuadroSocialGridView.DataSource = Dados;
            QuadroSocialGridView.DataBind();
            QuadroSocialMultiView.Visible = true;
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

        protected void QuadroSocialGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            QuadroSocialGridView.PageIndex = e.NewPageIndex;
            CarregaQuadroSocial();
        }

        protected void DetalhesLinkButton_Click(object sender, EventArgs e)
        {
            ObjCliente = new ClienteClasse();

            ObjCliente.IDCliente = Convert.ToInt32(IDClienteHiddenField.Value);

            ObjCliente.IDAnalise = Convert.ToInt32(IDAnaliseHiddenField.Value);

            ObjCliente.CPFCNPJ = ObjUtilClass.RetornaApenasNumeros(((Label)((Control)sender).FindControl("CPFCNPJLabel")).Text);

            ObjCliente.CodigoCliente = CodigoTextBox.Text;

           Session["AnaliseCreditoQuadroSocial"] = ObjCliente;

            Response.Redirect("~/Clientes/AnaliseCreditoQuadroSocialDetalheWebForm.aspx?indmnu=5");
        }
    }
}