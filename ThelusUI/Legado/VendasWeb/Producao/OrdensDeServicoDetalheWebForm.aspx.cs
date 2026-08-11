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
    public partial class OrdensDeServicoDetalheWebForm : System.Web.UI.Page
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
            if (Session["StatusOrdemServico"] != null)
            {
                ObjProducao = (producao)Session["StatusOrdemServico"];
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
                IDStatusHiddenField.Value = "0";
                CodigoTextBox.Text = "";
                ObjProducao.Descricao = DescricaoTextBox.Text;
                ObjProducao.BloqueadoAlteracao = Convert.ToInt32(BloqueadoDropDownList.SelectedValue);
                ObjProducao.Ativo = Convert.ToInt32(AtivoDropDownList.SelectedValue);
            }
            else
            {
                //Protege código usuário
                CodigoTextBox.Enabled = false;

                //Carrega dados do status
                ObjProducao.CarregaDadosPrincipaisStatus();

                CodigoTextBox.Text = ObjProducao.IDStatus.ToString();
                if (ObjProducao.Descricao == null)
                {
                    DescricaoTextBox.Text = "";
                }
                else
                {
                    DescricaoTextBox.Text = ObjProducao.Descricao.ToString();
                }
                BloqueadoDropDownList.SelectedValue = ObjProducao.BloqueadoAlteracao.ToString();
                AtivoDropDownList.SelectedValue = ObjProducao.Ativo.ToString();
                IDStatusHiddenField.Value = ObjProducao.IDStatus.ToString();
            }
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (ObjProducao.Operacao != "inclusao")
            {
                ObjProducao.IDStatus = Convert.ToInt32(CodigoTextBox.Text);
                ObjProducao.CarregaDadosPrincipaisStatus();
            }

            if (DescricaoTextBox.Text == "" || ObjProducao.Descricao == null)
            {
                erro = "Informe descrição";
            }

            if (ObjProducao.Operacao != "inclusao")
            {
                Session["Msg"] = "Dados atualizados com sucesso.";
            }
            else
            {
                Session["Msg"] = "Dados incluidos com sucesso.";
            }

            if (erro == "")
            {
                if (ObjProducao.Operacao != "inclusao")
                {
                    ObjProducao.IDStatus = Convert.ToInt32(CodigoTextBox.Text);
                }

                ObjProducao.Descricao = DescricaoTextBox.Text;
                ObjProducao.BloqueadoAlteracao = Convert.ToInt32(BloqueadoDropDownList.SelectedValue.ToString());
                ObjProducao.Ativo = Convert.ToInt32(AtivoDropDownList.SelectedValue.ToString());

                erro = ObjProducao.GravaDadosPrincipaisStatus();
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
                CodigoTextBox.Enabled = false;

                //this.ProducaoWebUserControl.TrataAcessos();
            }
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Producao/StatusOrdemServicoWebForm.aspx?indmnu=3");
        }

    }
}