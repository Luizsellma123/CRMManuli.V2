using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.GerencialVendas;
using System.Web.UI.WebControls;
using VendasWeb.classes;

namespace VendasWeb.Producao
{
    public partial class PrioridadeOrdensServicoDetalheWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        producao ObjProducao = new producao();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = false;
            }

            //Recupera objeto grupo da sessao do usuário
            if (Session["PrioridadeOrdemServico"] != null)
            {
                ObjProducao = (producao)Session["PrioridadeOrdemServico"];
            }

            if (!IsPostBack)
            {
                CarregaDadosTela();
            }
            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        public void CarregaDadosTela()
        {
            if (ObjProducao.Operacao == "inclusao")
            {
                IDSPrioridadeHiddenField.Value = "0";
                CodigoTextBox.Text = "";
            }
            else
            {
                //Protege código usuário
                CodigoTextBox.Enabled = false;

                //Carrega dados do status
                ObjProducao.CarregaDadosPrincipaisPrioridades();

                CodigoTextBox.Text = ObjProducao.IDPrioridade.ToString();
                if (ObjProducao.Descricao == null || ObjProducao.Valor == null)
                {
                    DescricaoTextBox.Text = "";
                    ValorTextBox.Text = "";
                }
                else
                {
                    DescricaoTextBox.Text = ObjProducao.Descricao.ToString();
                    ValorTextBox.Text = ObjProducao.Valor.ToString();
                }

                AtivoDropDownList.SelectedValue = ObjProducao.Ativo.ToString();
                PadraoDropDownList.SelectedValue = ObjProducao.PadraoPrioridade.ToString();
                IDSPrioridadeHiddenField.Value = ObjProducao.IDStatus.ToString();
            }
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (ObjProducao.Operacao != "inclusao")
            {
                ObjProducao.IDPrioridade = Convert.ToInt32(CodigoTextBox.Text);
                ObjProducao.CarregaDadosPrincipaisPrioridades();
            }

            #region validação de campos preenchidos

            if (DescricaoTextBox.Text == "" || DescricaoTextBox.Text == null)
            {
                erro = "Informe descrição";
            }

            if (ValorTextBox.Text == "" || ValorTextBox.Text == null)
            {
                erro = "Informe valor";
            }

            if (ObjProducao.Operacao != "inclusao")
            {
                Session["Msg"] = "Dados atualizados com sucesso.";
            }
            else
            {
                Session["Msg"] = "Dados incluidos com sucesso.";
            }

            #endregion

            if (ObjProducao.Operacao != "inclusao")
            {
                ObjProducao.IDPrioridade = Convert.ToInt32(CodigoTextBox.Text);
            }

            if (erro == "")
            {
                ObjProducao.Descricao = DescricaoTextBox.Text;
                ObjProducao.Valor = ValorTextBox.Text;
                ObjProducao.Ativo = Convert.ToInt32(AtivoDropDownList.SelectedValue.ToString());
                ObjProducao.PadraoPrioridade = Convert.ToInt32(PadraoDropDownList.SelectedValue.ToString());

                erro = ObjProducao.VerificaValor();
            }

            if (erro == "")
            {
                erro = ObjProducao.GravaDadosPrincipaisPrioridades();
            }

            if (erro != "")
            {
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
                CodigoTextBox.Enabled = false;

                //this.ProducaoWebUserControl.TrataAcessos();
            }
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/PrioridadeOrdensServicoWebForm.aspx?indmnu=3");
        }

    }
}