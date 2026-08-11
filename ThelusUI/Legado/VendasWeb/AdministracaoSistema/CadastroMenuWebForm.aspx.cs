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
    public partial class CadastroMenuWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        menu objMenu = new menu();

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
            if (Session["AdministracaoMenu"] != null)
            {
                objMenu = (menu)Session["AdministracaoMenu"];
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
            if (objMenu.Operacao == "inclusao")
            {
                IDMenuHiddenField.Value = "0";
            }
            else
            {
                //Protege código usuário
                CodigoMenuTextBox.Enabled = false;

                //Carrega dados dos usuários
                objMenu.CarregaDadosPrincipais();

                CodigoMenuTextBox.Text = objMenu.IDMenu.ToString();
                StatusDropDownList.SelectedValue = objMenu.Status;
                IconeTextBox.Text = objMenu.IconeCSS.ToString();
                EnderecoTextBox.Text = objMenu.Endereco.ToString();
                OrdemTextBox.Text = objMenu.Ordem.ToString();
                NomeMenuTextBox.Text = objMenu.Nome;
                IDMenuHiddenField.Value = objMenu.IDMenu.ToString();
            }
        }

        protected void voltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoSistema/ListaCadastroMenusWebForm.aspx?indmnu=5");
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (objMenu.Operacao != "inclusao")
            {
                objMenu.IDMenu = Convert.ToInt32(CodigoMenuTextBox.Text);
                objMenu.CarregaDadosPrincipais();
            }

            objMenu.Nome = NomeMenuTextBox.Text;
            objMenu.Status = StatusDropDownList.SelectedValue;
            objMenu.Endereco = EnderecoTextBox.Text;
            objMenu.IconeCSS = IconeTextBox.Text;
            objMenu.Ordem = OrdemTextBox.Text;

            erro = VerificaCampos();

            if (erro == "")
            {
                erro = objMenu.GravaDadosPrincipaisMenu();
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
                CodigoMenuTextBox.Enabled = false;

                this.CadastroMenuWebUserControl.TrataAcessos();
            }
        }

        protected void CodigoMenuTextBox_TextChanged(object sender, EventArgs e)
        {
            if (objMenu.Operacao == "inclusao")
            {
                objMenu.IDMenu = Convert.ToInt32(CodigoMenuTextBox.Text);
                objMenu.CarregaDadosPrincipais();

                if (objMenu.Nome != "" && objMenu.Nome != null)
                {
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Código de usuário já existe.", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                    objMenu.Nome = "";
                }
            }
        }

        protected string VerificaCampos()
        {
            string erro = "";

            if ((objMenu.Nome == "" || objMenu.Nome == null) &&
                (objMenu.Endereco == "" || objMenu.Endereco == null) &&
                (objMenu.IconeCSS == "" || objMenu.IconeCSS == null) &&
                (objMenu.Ordem == "" || objMenu.Ordem == null))
            {
                erro = "Preencha os Campos";
            }
            else
            {
                if (objMenu.Nome == "" || objMenu.Nome == null)
                {
                    erro = "Informe nome do menu";
                }

                else if (objMenu.Endereco == "" || objMenu.Endereco == null)
                {
                    erro = "Informe endereco do menu";
                }

                else if (objMenu.IconeCSS == "" || objMenu.IconeCSS == null)
                {
                    erro = "Informe IconeCSS do menu";
                }

                else if (objMenu.Ordem == "" || objMenu.Ordem == null)
                {
                    erro = "Informe ordem do menu";
                }
            }

            return erro;
        }
    }
}