using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Controladoria
{
    public partial class IncluiProdutoCustoWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        CustosClass OBJCustos = new CustosClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }

            if (Session["OBJCustos"] != null)
            {
                OBJCustos = (CustosClass)Session["OBJCustos"];
            }

            if (!IsPostBack)
            {
                /*Tratar Abrir e fechar Div*/
                PainelFiltrosLiteral.Text = "<div id=\"filtros\" class=\"collapse in\" runat=\"server\">";

                //Carrega Combos
                CarregaCombos();

                CarregaDadosNatela();
            }
        }

        public void CarregaCombos()
        {
            DataTable Resultado = new DataTable();

            Resultado = OBJCustos.CarregaTiposMateriais();
            MaterialDropDownList.DataSource = Resultado;
            MaterialDropDownList.DataValueField = "CodigoMaterial";
            MaterialDropDownList.DataTextField = "Material";
            MaterialDropDownList.DataBind();
        }

        public void CarregaDadosNatela()
        {
            if (OBJCustos.Operacao == "alteracao")
            {
                OBJCustos.CarregaDadosPrincipaisMaterial();

                EmpresaDropDown.SelectedValue = OBJCustos.Empresa.ToString();
                CodigoProdutoTextBox.Text = OBJCustos.CodigoProduto;
                DescricaoTextBox.Text = OBJCustos.DescricaoProduto;
                ComprimentoTextBox.Text = OBJCustos.Comprimento.ToString();
                LarguraTextBox.Text = OBJCustos.Largura.ToString();
                FCTextBox.Text = OBJCustos.FC.ToString();
                FCConvertidoTextBox.Text = OBJCustos.FCConvertido.ToString();
                CustoTextBox.Text = OBJCustos.Custo.ToString();
                MaterialDropDownList.SelectedValue = OBJCustos.TipoMaterial;
                MargemTextBox.Text = OBJCustos.PercentualMargem.ToString();
                PrazoProducaoTextBox.Text = OBJCustos.PrazoProducao.ToString();

                DistribuidorTextBox.Text = OBJCustos.DISTRIBUIDOR.ToString("00.00");
                IndustriaTextBox.Text = OBJCustos.INDUSTRIA.ToString("0.00");
                RevendaTextBox.Text = OBJCustos.REVENDA.ToString("0.00");

                //Bloqueio código material
                EmpresaDropDown.Enabled = false;
                CodigoProdutoTextBox.Enabled = false;

            }
            else
            {
                ExcluirLinkButton.Visible = false;
            }
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/ConsultaCustosWebForm.aspx?indmnu=5");
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            OBJCustos.Empresa = Convert.ToInt32(EmpresaDropDown.SelectedValue);
            OBJCustos.CodigoProduto = CodigoProdutoTextBox.Text;
            OBJCustos.DescricaoProduto = DescricaoTextBox.Text;
            OBJCustos.Comprimento = ComprimentoTextBox.Text;
            OBJCustos.Largura = Convert.ToDecimal(LarguraTextBox.Text);
            OBJCustos.FC = Convert.ToDecimal(FCTextBox.Text);
            OBJCustos.FCConvertido = Convert.ToDecimal(FCConvertidoTextBox.Text);
            OBJCustos.Custo = Convert.ToDecimal(CustoTextBox.Text);
            OBJCustos.TipoMaterial = MaterialDropDownList.SelectedValue;
            OBJCustos.PercentualMargem = Convert.ToDecimal(MargemTextBox.Text);
            OBJCustos.PrazoProducao = Convert.ToInt32(PrazoProducaoTextBox.Text);

            OBJCustos.DISTRIBUIDOR = Convert.ToDecimal(DistribuidorTextBox.Text);
            OBJCustos.INDUSTRIA = Convert.ToDecimal(IndustriaTextBox.Text);
            OBJCustos.REVENDA = Convert.ToDecimal(RevendaTextBox.Text);

            erro = OBJCustos.GravaDadosProdutoCusto();

            if (erro == "")
            {
                Session["Msg"] = "Produto " + OBJCustos.CodigoProduto + " gravado com sucesso!";
                RetornarLinkButton_Click(null, null);
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void ExcluirLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            OBJCustos.Empresa = Convert.ToInt32(EmpresaDropDown.SelectedValue);
            OBJCustos.CodigoProduto = CodigoProdutoTextBox.Text;

            erro = OBJCustos.ExcluiDadosProdutoCusto();

            if (erro == "")
            {
                Session["Msg"] = "Produto " + OBJCustos.CodigoProduto + " excluído com sucesso!";
                RetornarLinkButton_Click(null, null);
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }
    }
}