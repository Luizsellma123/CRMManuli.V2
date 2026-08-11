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
    public partial class CadastroUsuarioVendedoresWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        usuario OBJUsuario = new usuario();

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
                CarregaCombos();

                //Carrega dados na tela
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

        }

        public void CarregaDadosNaTela()
        {
            OBJUsuario.CarregaDadosPrincipais();

            CodigoUsuarioTextBox.Text = OBJUsuario.CodigoUsuario;
            StatusDropDownList.SelectedValue = OBJUsuario.Status;

            //Carrega vendedores
            UsuariosVendedoresGridView.DataSource = OBJUsuario.ConsultaVendedoresUsuario();
            UsuariosVendedoresGridView.DataBind();
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
            Response.Redirect("CadastroUsuarioWebForm.aspx?indmnu=2");
        }

        protected void AdicionaVendedorLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            OBJUsuario.IDVendedor = Convert.ToInt16(VendedorDropDownList.SelectedValue.ToString());
            OBJUsuario.OperacaoDois = "inclusao";
            erro = OBJUsuario.GravaDadosVendedorUsuario();

            if (erro == "") {
                CarregaDadosNaTela();
            }else
            {
                DisparaMensagemTela(erro);
            }

        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            OBJUsuario.IDVendedor = Convert.ToInt32(((Label)((Control)sender).FindControl("IDVendedorLabel")).Text);
            OBJUsuario.OperacaoDois = "exclusao";
            erro = OBJUsuario.GravaDadosVendedorUsuario();

            if (erro == "")
            {
                CarregaDadosNaTela();
            }
            else
            {
                DisparaMensagemTela(erro);
            }
        }

        public void DisparaMensagemTela(string erro)
        {
            ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
        }
    }
}