using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.GerencialVendas;
using System.Web.UI.WebControls;
using VendasWeb.classes;

namespace VendasWeb.AdministracaoSistema
{
    public partial class CadastroGrupoWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        grupos objGrupo = new grupos();

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
            if (Session["AdministracaoGrupo"] != null)
            {
                objGrupo = (grupos)Session["AdministracaoGrupo"];
            }

            if (!IsPostBack)
            {
                //Carrega dados na tela
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        public void CarregaDadosNaTela()
        {
            if (objGrupo.Operacao == "inclusao")
            {
                IDGrupoHiddenField.Value = "0";
            }
            else
            {
                //Protege código usuário
                CodigoGrupoTextBox.Enabled = false;

                //Carrega dados dos usuários
                objGrupo.CarregaDadosPrincipais();

                CodigoGrupoTextBox.Text = objGrupo.IDGrupo.ToString();
                StatusDropDownList.SelectedValue = objGrupo.Status;
                NomeGrupoTextBox.Text = objGrupo.Nome;
                IDGrupoHiddenField.Value = objGrupo.IDGrupo.ToString();
            }
        }

        protected void voltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoSistema/ListaCadastroGruposWebForm.aspx?indmnu=5");
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (objGrupo.Operacao != "inclusao")
            {
                objGrupo.CodigoGrupo = CodigoGrupoTextBox.Text;
                objGrupo.CarregaDadosPrincipais();
            }

            objGrupo.Nome = NomeGrupoTextBox.Text;

            if (objGrupo.Nome == "" || objGrupo.Nome == null)
            {
                erro = "Informe nome do grupo";
            }

            if (erro == "")
            {
                objGrupo.IDGrupo = Convert.ToInt32(IDGrupoHiddenField.Value);
                objGrupo.CodigoGrupo = CodigoGrupoTextBox.Text;
                objGrupo.Status = StatusDropDownList.SelectedValue;

                erro = objGrupo.GravaDadosPrincipaisGrupo();
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
                Session["Msg"] = "Dados atualizados com sucesso.";
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                CodigoGrupoTextBox.Enabled = false;

                this.CadastroGrupoWebUserControl.TrataAcessos();
            }
        }

        protected void CodigoGrupoTextBox_TextChanged(object sender, EventArgs e)
        {
            if (objGrupo.Operacao == "inclusao")
            {
                objGrupo.CodigoGrupo = CodigoGrupoTextBox.Text;
                objGrupo.CarregaDadosPrincipais();

                if (objGrupo.Nome != "" && objGrupo.Nome != null)
                {
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Código de usuário já existe.", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                    objGrupo.Nome = "";
                }
            }
        }
    }
}