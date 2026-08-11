using System;
using System.Data;
using System.Web.UI;
using VendasWeb.classes;
using VendasWeb.WEBServiceCRM;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.WEBServiceSAP.ClassesWEBService;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;

namespace VendasWeb.Clientes
{
    public partial class AnaliseCreditoWebForm : System.Web.UI.Page
    {
        ClienteClasse ObjCliente = new ClienteClasse();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass ObjSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            ObjSessao.ValidaAcesso();

            if (Session["clienteClasse"] != null)
            {
                ObjCliente = (ClienteClasse)Session["clienteClasse"];

                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            ObjCliente.carregaDadosPrincipais();

            if (ObjCliente.CodigoCliente != "") CodigoClienteTextBox.Text = ObjCliente.CodigoCliente;
            else CodigoClienteTextBox.Text = ObjCliente.IDCliente.ToString();

            NomeClienteTextBox.Text = ObjCliente.NomeCliente;

            BuscarButton_Click(null, null);
        }

        protected void CarregaDadosDaTela()
        {
            if (Session["clienteClasse"] != null)
                ObjCliente = (ClienteClasse)Session["clienteClasse"];

            ObjCliente.DataInicial = PeriodoInicialTextBox.Text == "" ? ""
                : Convert.ToDateTime(PeriodoInicialTextBox.Text).ToString("yyyy-MM-dd");
            ObjCliente.DataFinal = PeriodoFinalTextBox.Text == "" ? ""
                : Convert.ToDateTime(PeriodoFinalTextBox.Text).ToString("yyyy-MM-dd");

            ObjCliente.IDAnalise = NumeroAnaliseTextBox.Text == "" ? 0 : Convert.ToInt32(NumeroAnaliseTextBox.Text);
        }

        protected void NovoAnaliseLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            WSRecuperaDadosSerasa objWSRecuperaDadosSerasa = new WSRecuperaDadosSerasa();

            JsonConversao jsonconv = new JsonConversao();

            FuncoesAPIClass OBJApi = new FuncoesAPIClass();

            WSRetornoJSONClass objWSRetornoJSONClass = new WSRetornoJSONClass();

            string JSON = "";            

            try
            {
                if (Session["clienteClasse"] != null)
                    objWSRecuperaDadosSerasa.IDCliente = ((ClienteClasse)Session["clienteClasse"]).IDCliente;

                objWSRecuperaDadosSerasa.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

                if (ObjUtilClass.RetornaApenasNumeros(ObjCliente.CNPJCliente).Length <= 11)
                    objWSRecuperaDadosSerasa.TipoConsulta = "PF";
                else
                    objWSRecuperaDadosSerasa.TipoConsulta = "PJ";

                objWSRecuperaDadosSerasa.NumeroDocumento = ObjUtilClass.RetornaApenasNumeros(ObjCliente.CNPJCliente).ToString();
                
                JSON = jsonconv.ConverteObjectParaJSon<WSRecuperaDadosSerasa>(objWSRecuperaDadosSerasa);

                objWSRetornoJSONClass = OBJApi.GravaDadosSerasaCRMAPI(JSON);

                erro = objWSRetornoJSONClass.MsgRetorno;

                if (erro == "") BuscarButton_Click(null, null);
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            ApresentaMensagem(erro);
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();

            ClienteGridView.DataSource = ObjCliente.CarregaAnaliseCredito();
            ClienteGridView.DataBind();
            ClienteMultiView.Visible = true;
        }

        protected void DetalhesLinkButton_Click(object sender, EventArgs e)
        {
            if (Session["clienteClasse"] != null) ObjCliente = (ClienteClasse)Session["clienteClasse"];

            ObjCliente.Operacao = "Alteracao";
            ObjCliente.IDAnalise = Convert.ToInt32(((Label)((Control)sender).FindControl("IDAnaliseLabel")).Text);
            Session["AnaliseCredito"] = ObjCliente;
            Response.Redirect("~/Clientes/AnaliseCreditoDetalheWebForm.aspx?indmnu=5");
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Clientes/CadastroClienteWebForm.aspx?indmnu=5");
        }

        protected void ClienteGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ClienteGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
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

        protected void ContaCorrenteLinkButton_Click(object sender, EventArgs e)
        {
            ClienteClasse OBJClienteClasse = new ClienteClasse();

            OBJClienteClasse.CodigoCliente = CodigoClienteTextBox.Text;

            DataTable OBJDataTable = OBJClienteClasse.RecuperaContaCorrenteClienteSAP();

            foreach (DataRow row in OBJDataTable.Rows)
            {
                Session["ContaCorrente"] = null;
                Session["ContaCorrenteDetalhe"] = null;
                Session["ContaCorrenteReturn"] = "~/Clientes/AnaliseCreditoWebForm.aspx?indmnu=5";

                OBJClienteClasse.VendedorCliente = row["Vendedor"].ToString();
                OBJClienteClasse.CodigoCliente = row["CardCode"].ToString();
                OBJClienteClasse.CodigoAux = row["CardCode"].ToString();
                OBJClienteClasse.NomeCliente = row["CardName"].ToString();
                OBJClienteClasse.CNPJCliente = row["CNPJ"].ToString();
                OBJClienteClasse.LimiteCredito = Convert.ToDecimal(row["LimiteCredito"]);

                Session["ContaCorrente"] = OBJClienteClasse;
            }

            if (Session["ContaCorrente"] != null)
                Response.Redirect("~/financeiro/ContaCorrenteDetalheWebForm.aspx?indmnu=5");
        }
    }
}