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
    public partial class CadastroSetorWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        setor objSetor = new setor();

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
            if (Session["AdministracaoSetor"] != null)
            {
                objSetor = (setor)Session["AdministracaoSetor"];
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
            if (objSetor.Operacao == "inclusao")
            {
                IDSetorHiddenField.Value = "0";
            }
            else
            {
                //Protege código usuário
                IDSetorTextBox.Enabled = false;

                //Carrega dados dos usuários
                objSetor.CarregaDadosPrincipais();

                IDSetorTextBox.Text = objSetor.IDSetor.ToString();
                StatusDropDownList.SelectedValue = objSetor.Status;
                DescricaoTextBox.Text = objSetor.Nome;
                IDSetorHiddenField.Value = objSetor.IDSetor.ToString();
            }
        }

        protected void voltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoSistema/ListaCadastroSetoresWebForm.aspx?indmnu=5");
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (objSetor.Operacao != "inclusao")
            {
                objSetor.IDSetor = Convert.ToInt32(IDSetorTextBox.Text);
                objSetor.CarregaDadosPrincipais();
            }

            objSetor.Nome = DescricaoTextBox.Text;

            if (objSetor.Nome == "" || objSetor.Nome == null)
            {
                erro = "Informe descrição do setor";
            }

            if (erro == "")
            {
                objSetor.Status = StatusDropDownList.SelectedValue;

                erro = objSetor.GravaDadosPrincipaisSetor();
            }

            if (erro != "")
            {
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                Session["Msg"] = "Dados atualizados com sucesso.";
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                IDSetorTextBox.Enabled = false;

                this.CadastroSetorWebUserControl.TrataAcessos();
            }
        }

        protected void IDSetorTextBox_TextChanged(object sender, EventArgs e)
        {
            if (objSetor.Operacao == "inclusao")
            {
                objSetor.IDSetor = Convert.ToInt32(IDSetorTextBox.Text);
                objSetor.CarregaDadosPrincipais();

                if (objSetor.Nome != "" && objSetor.Nome != null)
                {
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Código de usuário já existe.", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                    objSetor.Nome = "";
                }
            }
        }
    }
}