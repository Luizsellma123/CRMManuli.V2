using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace VendasWeb.AdministracaoVendas
{
    public partial class ClassificacaoComercialWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();        
        ClienteClasse objClienteClasse = new ClienteClasse();

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

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            DateTime primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DataInicialTextBox.Text = primeiroDiaMes.ToString("yyyy-MM-dd");
            DataFinalTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

            CarregaCombos();
            BuscarButton_Click(null, null);
        }

        protected void CarregaCombos()
        {
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            VendedorDropDownList.DataSource = ObjUsuario.RetornaListaVendedores();
            VendedorDropDownList.DataTextField = "NomeVendedor";
            VendedorDropDownList.DataValueField = "IDVendedor";
            VendedorDropDownList.DataBind();

            VendedorDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

            SituacaoDropDownList.DataSource = objClienteClasse.Carrega_Solicitacao_Classificacao_Comercial_Situacao();
            SituacaoDropDownList.DataTextField = "Descricao";
            SituacaoDropDownList.DataValueField = "IDSituacao";
            SituacaoDropDownList.DataBind();

            SituacaoDropDownList.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (DataInicialTextBox.Text != null)
                objClienteClasse.DataInicial = Convert.ToDateTime(DataInicialTextBox.Text).ToString("yyyy-MM-dd");
            else
                objClienteClasse.DataInicial = "";

            if (DataFinalTextBox.Text != null)
                objClienteClasse.DataFinal = Convert.ToDateTime(DataFinalTextBox.Text).ToString("yyyy-MM-dd");
            else
                objClienteClasse.DataFinal = "";

            objClienteClasse.Cliente = ClienteTextBox.Text;

            objClienteClasse.IDVendedor = Convert.ToInt32(VendedorDropDownList.SelectedValue);

            objClienteClasse.IDSituacao = Convert.ToInt32(SituacaoDropDownList.SelectedValue);

            if (erro == "")
            {
                ClassificacaoComercialGridView.DataSource = objClienteClasse.Carrega_Solicitacao_Classificacao_Comercial();
                ClassificacaoComercialGridView.DataBind();
                ClassificacaoComercialMultiView.Visible = true;
            }
            else
            {
                ApresentaMensagem(erro);
            }
        }

        protected void SelecionarLinkButton_Click(object sender, EventArgs e)
        {
            ClienteClasse objClienteClasseAux = new ClienteClasse();

            objClienteClasseAux.IDCliente = Convert.ToInt32(((Label)((Control)sender).FindControl("IDClienteGridViewLabel")).Text);
            objClienteClasseAux.IDSolicitacao = Convert.ToInt32(((Label)((Control)sender).FindControl("IDSolicitacaoGridViewLabel")).Text);           

            Session["ClassificacaoComercialWebForm"] = objClienteClasseAux;

            Response.Redirect("~/AdministracaoVendas/ClassificacaoComercialDetalheWebForm.aspx?indmnu=3");
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoVendas/HomeWebForm.aspx?indmnu=3");
        }

        protected void ClassificacaoComercialGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ClassificacaoComercialGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
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
                Session["Msg"] = "Operação realizada com sucesso.";
                VoltarButton_Click(null, null);
            }
        }
    }
}