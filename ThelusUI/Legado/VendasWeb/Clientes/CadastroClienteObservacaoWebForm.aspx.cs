using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Clientes
{
    public partial class CadastroClienteObservacaoWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        ClienteClasse OBJCliente = new ClienteClasse();
        VendedorClass ObjVendedorClass = new VendedorClass();
        UtilClass ObjUtilClass = new UtilClass();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

                //Verificando se deve mandar alerta
                if (Session["Msg"] != null)
                {

                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                    Session.Remove("Msg");
                }



                if (Session["clienteClasse"] != null)
                {
                    //Descarega a session da Entidade
                    OBJCliente = (ClienteClasse)Session["clienteClasse"];

                    //Carrega dados
                    CarregaDadosNaTela();

                    TrataAcesso();
                }
               
            }
        }

        public void CarregaDadosNaTela()
        {
            //recupera dados principais da tela
            OBJCliente.carregaDadosPrincipais();

            IDCliente.Value = OBJCliente.IDCliente.ToString();
            if (OBJCliente.CodigoCliente != "")
            {
                CodigoClienteTextBox.Text = OBJCliente.CodigoCliente;
            }
            else
            {
                CodigoClienteTextBox.Text = OBJCliente.IDCliente.ToString();
            }
            NomeClienteTextBox.Text = OBJCliente.NomeCliente;
            ObservacaoCompletaTextBox.Text = OBJCliente.ObservacaoCompleta;


        }


        public void CarregaDadosDaTela()
        {
            OBJCliente.CodigoUsuario = Session["usuario"].ToString();
            OBJCliente.ObservacaoCompleta = ObservacaoCompletaTextBox.Text;


        }

        protected void GravarButton_Click(object sender, EventArgs e)
        {


            string erro = "";

            if (Session["clienteClasse"] != null)
            {
                //Descarega a session da Entidade
                OBJCliente = (ClienteClasse)Session["clienteClasse"];

                CarregaDadosDaTela();

                if (erro == "")
                {
                    erro = OBJCliente.gravaDadosClienteObservacaoCompleta();

                }

            }
            else
            {
                erro = "Session foi finalizada antes da conclusão da Operação, favor sair e tentar novamente";
            }


            if (erro == "")
            {

               
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Observação Atualizada com Sucesso!", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
               

            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

            }


        }


        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroClienteWebForm.aspx?indmnu=2");

        }


        public void TrataAcesso()
        {
            usuario ObjusuarioAux = new usuario();

            ObjusuarioAux = new usuario();
            ObjusuarioAux.CodigoUsuario = Session["usuario"].ToString();
            ObjusuarioAux.ConsultaGrupos("Ativo");

            switch (OBJCliente.IDStatus)
            {
                case 0: //Novo Cadastro
                    GravarButton.Visible = true;
                    break;

                case 1: //Status Cliente Prospectivo
                    GravarButton.Visible = true;
                    break;

                case 2: //Status Cliente Ativo
                case 3: //Status Cliente Inativo
                    GravarButton.Visible = true;
                    break;

                case 4: //Status Cliente Análise Financeira
                        //Verifica se esta no Grupo Análise Financeira
                    if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 7).Count() > 0)
                    {
                        GravarButton.Visible = true;
                    }
                    else
                    {
                        GravarButton.Visible = false;
                    }

                    break;

                case 5: //Status Cliente Análise Fiscal

                    //Verifica se esta no Grupo Análise Fiscal
                    if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 8).Count() > 0)
                    {
                        GravarButton.Visible = true;
                    }
                    else
                    {
                        GravarButton.Visible = false;
                    }

                    break;

                default:
                    GravarButton.Visible = false;
                    break;
            }
        }

    }
}