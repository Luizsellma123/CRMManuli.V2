using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.AdministracaoSistema
{
    public partial class CadastroUsuariosSAPWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        usuario OBJUsuario = new usuario();
        UsuarioVendedoresClass OBJVendedor = new UsuarioVendedoresClass();

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

            //Recupera objeto usuário da sessao do usuário
            if (Session["AdministrcaoUsuario"] != null)
            {
                OBJUsuario = (usuario)Session["AdministrcaoUsuario"];
            }

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

        }

        public void CarregaDadosNaTela()
        {
            CodigoUsuarioTextBox.Text = OBJUsuario.CodigoUsuario;
            StatusDropDownList.SelectedValue = OBJUsuario.Status;

            CodigoUsuarioTextBox.Enabled = false;
            StatusDropDownList.Enabled = false;


            OBJUsuario.CarregaDadosUsuarioSAP();

            if (OBJUsuario.IDUsuarioSAP != 0 && OBJUsuario.CodigoUsuarioSAP != "" && OBJUsuario.NomeUsuarioSAP != "")
            {
                OBJUsuario.OperacaoSAP = "alteracao";

                if (OBJUsuario.IDUsuarioSAP != 0)
                {
                    IDTextBox.Text = OBJUsuario.IDUsuarioSAP.ToString();
                }
                else
                {
                    IDTextBox.Text = "";
                }

                CodigoTextBox.Text = OBJUsuario.CodigoUsuarioSAP;
                NomeTextBox.Text = OBJUsuario.NomeUsuarioSAP;

                //if (IDTextBox.Text != "" && CodigoTextBox.Text != "")
                //{
                //    IDTextBox.Enabled = false;
                //    CodigoTextBox.Enabled = false;
                //}
            }
            else
            {
                OBJUsuario.OperacaoSAP = "inclusao";
            }
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (erro == "")
            {
                erro = VerificaCamposPreenchidos();

                if (erro == "")
                {
                    OBJUsuario.IDUsuarioSAP = Convert.ToInt32(IDTextBox.Text);
                    OBJUsuario.CodigoUsuarioSAP = CodigoTextBox.Text;
                    OBJUsuario.NomeUsuarioSAP = NomeTextBox.Text;
                    OBJUsuario.SenhaUsuarioSAP = SenhaTextBox.Text;

                    erro = OBJUsuario.GravaDadosUsuarioSAP();
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
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Dados gravados com sucesso.", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                    //IDTextBox.Enabled = false;
                    //CodigoTextBox.Enabled = false;

                    this.CadastroUsuarioWebUserControl.TrataAcessos();
                }
            }

        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoSistema/CadastroUsuarioWebForm.aspx?indmnu=5");
        }

        protected string VerificaCamposPreenchidos()
        {
            string erro = "";

            if (IDTextBox.Text == "" || IDTextBox.Text == null)
            {
                erro = "Informe um ID.";
            }
            else if (CodigoTextBox.Text == "" || CodigoTextBox.Text == null)
            {
                erro = "Informe um Código.";
            }
            else if (NomeTextBox.Text == "" || NomeTextBox.Text == null)
            {
                erro = "Informe um nome.";
            }
            else if (SenhaTextBox.Text == "" || SenhaTextBox.Text == null)
            {
                erro = "Informe uma senha.";
            }
            else if (SenhaTextBox.Text != RepitaSenhaTextBox.Text)
            {
                erro = "Senhas não coincidem.";
            }

            return erro;
        }
    }
}