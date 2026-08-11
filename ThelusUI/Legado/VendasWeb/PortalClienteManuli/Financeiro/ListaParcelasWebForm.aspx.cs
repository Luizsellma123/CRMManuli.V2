using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.PortalClienteManuli.Financeiro
{
    public partial class ListaParcelasWebForm : System.Web.UI.Page
    {
        UsuarioPortalClass OBJusuario = new UsuarioPortalClass();
        UtilClass ObjUtilClass = new UtilClass();
        enviarEmailClass OBJEmail = new enviarEmailClass();
        PortalClass OBJPortal = new PortalClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica se tem usuário logado no Portal
            if (Session["usuarioPortal"] == null)
            {
                //Redireciona para tela de login
                Response.Redirect("LoginPortal.aspx");

            }

            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

            if (!IsPostBack)
            {
                //Chama função para carregar dados na tela
                carregaDadosTela();
            }

        }

        public void carregaDadosTela()
        {
            //Recupera usuario da sessão
            OBJusuario = (UsuarioPortalClass)Session["usuarioPortal"];

            //Carrega Empresa
            EmpresaDropDownList.DataSource = OBJusuario.Empresas_Usuario();
            EmpresaDropDownList.DataTextField = "EmpNome";
            EmpresaDropDownList.DataValueField = "EmpCod";
            EmpresaDropDownList.DataBind();

            //Carrega Razão Social Cliente
            RazaoSocialDropDownList.DataSource = OBJusuario.Entidades_Usuario();
            RazaoSocialDropDownList.DataTextField = "EntNome";
            RazaoSocialDropDownList.DataValueField = "EntCod";
            RazaoSocialDropDownList.DataBind();

            //Carrega parelas na tela
            carregaParcelas();
        }

        public void carregaParcelas()
        {
            DataTable RetornoDados = new DataTable();

            //Recupera usuario da sessão
            OBJusuario = (UsuarioPortalClass)Session["usuarioPortal"];

            //recupera pedidos pendentes
            OBJusuario.EmpCod = EmpresaDropDownList.SelectedValue.ToString();
            OBJusuario.EntCod = RazaoSocialDropDownList.SelectedValue.ToString();
            OBJusuario.ParcDocNum = DocumentoTextBox.Text.ToString();
            OBJusuario.abertas = AbertasRadioButton.Checked;

            if (DataInicialTextBox.Text != "")
            {
                OBJusuario.DataInicial = Convert.ToDateTime(DataInicialTextBox.Text);
            }
            else
            {
                OBJusuario.DataInicial = DateTime.Now.AddYears(-1);
            }

            if (DataFinalTextBox.Text != "")
            {
                OBJusuario.DataFinal = Convert.ToDateTime(DataFinalTextBox.Text);
            }
            else
            {
                OBJusuario.DataFinal = DateTime.Now;
            }

            RetornoDados = OBJusuario.Parcelas_Entidade();

            GridViewParcelasClientes.DataSource = RetornoDados;
            GridViewParcelasClientes.DataBind();

        }

        protected void GridViewParcelasClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewParcelasClientes.PageIndex = e.NewPageIndex;
            carregaParcelas();
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            carregaParcelas();
        }

        protected void LinkButtonConsulta_Click(object sender, EventArgs e)
        {
            string parcela = "";
            string empresa = "";
            string cliente = "";

            //Carrega Email
            OBJPortal.NomeContato = "Boletos";
            OBJPortal.ContatoSetorPortal();

            try
            {
                parcela = ((Label)((Control)sender).FindControl("ParcDocFinDupNumLabel")).Text;
                empresa = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text + " - " + EmpresaDropDownList.SelectedItem.Text.ToString();
                cliente = RazaoSocialDropDownList.SelectedValue.ToString() + " - " + RazaoSocialDropDownList.SelectedItem.Text;

                OBJEmail.SolicitacaoBoletoEmail("Solicitação Boleto", parcela, empresa, cliente, OBJPortal.EmailSetor);

                string FaltaValores = "Solicitação encaminhada com sucesso !";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(FaltaValores, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            catch
            {
                string FaltaValores = "Ocorreu um problema na solicitação !";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }
    }
}