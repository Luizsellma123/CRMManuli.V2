using System;
using System.Web;
using System.Data;
using System.Linq;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Collections.Generic;

namespace VendasWeb.SAC
{
    public partial class SituacoesAtividadesDetalheWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        SACClass ObjSAC = new SACClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

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

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            if (Session["SituacaoAtividadesDetalhe"] != null)
            {
                ObjSAC = (SACClass)Session["SituacaoAtividadesDetalhe"];
            }

            if (ObjSAC.Operacao == "Alteracao")
            {

                ObjSAC.Filtro = ObjSAC.IDSituacao.ToString();
                CodigoTextBox.Text = ObjSAC.IDSituacao.ToString();

                DataTable SACDataTable = new DataTable();

                ObjSAC.Tela = "Detalhe";
                SACDataTable = ObjSAC.RetornaListaSituacaoAtividades();

                if (SACDataTable.Rows.Count > 0)
                {
                    foreach (DataRow Row in SACDataTable.Rows)
                    {
                        if (Row["Bloqueado"].ToString() == "Sim")
                        {
                            BloqueadoDropDownList.SelectedValue = "1";
                        }
                        else
                        {
                            BloqueadoDropDownList.SelectedValue = "0";
                        }

                        if (Row["Ativo"].ToString() == "True")
                        {
                            AtivoDropDownList.SelectedValue = "1";
                        }
                        else
                        {
                            AtivoDropDownList.SelectedValue = "0";
                        }

                        DescricaoTextBox.Text = Row["Descricao"].ToString();
                    }
                }
            }
        }

        protected string CarregaDadosDaTela()
        {
            string erro = "";

            if (Session["SituacaoAtividadesDetalhe"] != null)
            {
                ObjSAC = (SACClass)Session["SituacaoAtividadesDetalhe"];
            }

            if (BloqueadoDropDownList.SelectedValue == "" || BloqueadoDropDownList.SelectedValue == null)
            {
                erro = "Escolha se está bloqueado ou não";
            }
            else if (DescricaoTextBox.Text == "" || DescricaoTextBox.Text == null)
            {
                erro = "Informe uma descrição.";
            }
            else if (AtivoDropDownList.SelectedValue == "" || AtivoDropDownList.SelectedValue == null)
            {
                erro = "Escolha se está ativo ou não";
            }

            if (erro == "")
            {
                if (ObjSAC.Operacao == "Inclusao")
                {
                    ObjSAC.IDSituacao = 0;
                }
                else
                {
                    ObjSAC.IDSituacao = Convert.ToInt32(CodigoTextBox.Text);
                }

                if (BloqueadoDropDownList.SelectedValue == "1")
                {
                    ObjSAC.Bloqueado = true;
                }
                else
                {
                    ObjSAC.Bloqueado = false;
                }

                ObjSAC.Descricao = DescricaoTextBox.Text;

                if (AtivoDropDownList.SelectedValue == "1")
                {
                    ObjSAC.Ativo = true;
                }
                else
                {
                    ObjSAC.Ativo = false;
                }

                if (Session["IDUsuario"] != null)
                {
                    ObjSAC.IDUsuario = Convert.ToInt32(Session["IDUsuario"].ToString());
                }

            }

            return erro;
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            erro = CarregaDadosDaTela();

            if (erro == "")
            {
                erro = ObjSAC.GravaSituacaoAtividades();
            }

            if (erro != "")
            {
                ApresentaMensagem(erro);
            }
            else
            {
                CarregaDadosNaTela();
                ApresentaMensagem("");
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
            Response.Redirect("~/SAC/SituacoesAtividadesWebForm.aspx?indmnu=3");
        }
    }
}