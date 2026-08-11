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
    public partial class CadastroUsuarioWebForm : System.Web.UI.Page
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
                //Carrega vendedores
                //CarregaCombos();

                //Carrega dados na tela
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        public void CarregaDadosNaTela()
        {
            if (OBJUsuario.Operacao == "inclusao")
            {
                IDUsuarioHiddenField.Value = "0";
            }
            else
            {
                //Protege código usuário
                CodigoUsuarioTextBox.Enabled = false;

                //Carrega dados dos usuários
                OBJUsuario.CarregaDadosPrincipais();

                CodigoUsuarioTextBox.Text = OBJUsuario.CodigoUsuario;
                StatusDropDownList.SelectedValue = OBJUsuario.Status;
                //VendedorDropDownList.SelectedValue = OBJUsuario.IDVendedor.ToString();
                NomeUsuarioTextBox.Text = OBJUsuario.nome;
                EmailTextBox.Text = OBJUsuario.email;
                TelefoneTextBox.Text = OBJUsuario.telefone;
                IDUsuarioHiddenField.Value = OBJUsuario.IDUsuario.ToString();

                //Carrega vendedores
                //CarregaVendedores();
            }
        }

        public void CarregaCombos()
        {
            DataTable Resultado = new DataTable();

            Resultado = OBJUsuario.Consulta_Vendedores();
            VendedorDropDownList.DataSource = Resultado;
            VendedorDropDownList.DataValueField = "IDVendedor";
            VendedorDropDownList.DataTextField = "NomeVendedor";
            VendedorDropDownList.DataBind();
        }

        protected void voltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoSistema/ListaCadastroUsuarioWebForm.aspx?indmnu=5");
        }

        protected void AdicionarVendedorLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            OBJVendedor.IDVendedorNovo = Convert.ToInt32(VendedorDropDownList.SelectedValue);
            OBJVendedor.VendNome = VendedorDropDownList.SelectedItem.Text;

            erro = OBJUsuario.INCLUI_Vendedor_Lista(OBJVendedor);

            CarregaVendedores();

            //Carrega dados na Session
            Session["AdministrcaoUsuario"] = OBJUsuario;

            if (erro != "")
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        public void CarregaVendedores()
        {
            //Carrega vendedores
            UsuariosVendedoresGridView.DataSource = ObjUtilClass.ConvertToDataTable(OBJUsuario.ListaVendedorClass);
            UsuariosVendedoresGridView.DataBind();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int IDVendedor = Convert.ToInt32(((Label)((Control)sender).FindControl("IDVendedorLabel")).Text);
            OBJUsuario.ListaVendedorClass.RemoveAll(x => x.IDVendedorNovo == IDVendedor);

            CarregaVendedores();

            //Carrega dados na Session
            Session["AdministrcaoUsuario"] = OBJUsuario;
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (SenhaNovaTextBox.Text != SenhaNovaRepetirTextBox.Text)
            {
                erro = "Senhas não coincidem.";
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("As senhas não coincidem.", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }

            if (OBJUsuario.Operacao == "inclusao")
            {
                OBJUsuario.CodigoUsuario = CodigoUsuarioTextBox.Text;
                OBJUsuario.CarregaDadosPrincipais();

                if (OBJUsuario.nome != "" && OBJUsuario.nome !=null)
                {
                    erro = "Código de usuário já existe";
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                    OBJUsuario.nome = "";
                }
            }

            if (erro == "")
            {
                OBJUsuario.IDUsuario = Convert.ToInt32(IDUsuarioHiddenField.Value);
                OBJUsuario.CodigoUsuario = CodigoUsuarioTextBox.Text;
                OBJUsuario.Status = StatusDropDownList.SelectedValue;
                OBJUsuario.nome = NomeUsuarioTextBox.Text;
                OBJUsuario.email = EmailTextBox.Text;
                OBJUsuario.telefone = TelefoneTextBox.Text;
                OBJUsuario.senha = SenhaNovaTextBox.Text;

                erro = OBJUsuario.GravaDadosPrincipaisUsuario();

                if (erro != "")
                {
                    //Retorna Mensagem de Erro
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Erro na atualização dos dados usuário.", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                }
                else
                {
                    Session["Msg"] = "Dados atualizados com sucesso.";
                    CodigoUsuarioTextBox.Enabled = false;

                    this.CadastroUsuarioWebUserControl.TrataAcessos();
                }
            }
        }

        protected void CodigoUsuarioTextBox_TextChanged(object sender, EventArgs e)
        {
            if (OBJUsuario.Operacao == "inclusao")
            {
                OBJUsuario.CodigoUsuario = CodigoUsuarioTextBox.Text;
                OBJUsuario.CarregaDadosPrincipais();

                if (OBJUsuario.nome != "" && OBJUsuario.nome != null)
                {
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Código de usuário já existe.", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                    OBJUsuario.nome = "";
                }
            }
        }
    }
}