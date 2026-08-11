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
    public partial class CadastroClienteFinanceiroWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        ClienteClasse OBJCliente = new ClienteClasse();
        usuario Objusuario = new usuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                if (Session["clienteClasse"] != null)
                {
                    //Descarega a session da Entidade
                    OBJCliente = (ClienteClasse)Session["clienteClasse"];

                    //Carrega dados
                    CarregaDadosNaTela();

                    CarregaCombo();

                    TrataAcesso();

                }
                else
                {

                    CarregaCombo();
                }

            }
        }


        public void CarregaCombo()
        {
            DataTable RetornoDados = new DataTable();
            ClienteClasse OBJClienteAux = new ClienteClasse();

            OBJClienteAux.CodigoUsuario = Session["usuario"].ToString();
            OBJClienteAux.IDCliente = OBJCliente.IDCliente;
            RetornoDados = OBJClienteAux.CarregaCondicoesPagamento();
            CondicaoPagamentoDropDownList.DataSource = RetornoDados;
            CondicaoPagamentoDropDownList.DataValueField = "IDCondPag";
            CondicaoPagamentoDropDownList.DataTextField = "NomeCondicao";
            CondicaoPagamentoDropDownList.DataBind();
            CondicaoPagamentoDropDownList.Items.Insert(0, new ListItem("Selecione para Adicionar uma Nova Condição", ""));
        }


        public void CarregaDadosNaTela()
        {

            //recupera dados principais da tela
            OBJCliente.carregaDadosPrincipais();

            //Recupera dados do usuario
            Objusuario = new usuario();
            Objusuario.CodigoUsuario = Session["usuario"].ToString();
            Objusuario.ConsultaGrupos("Ativo");

            IDCliente.Value = OBJCliente.IDCliente.ToString();
            if (OBJCliente.CodigoCliente != "")
            {
                CodigoClienteTextBox.Text = OBJCliente.CodigoCliente;

                if (Objusuario.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 7).Count() > 0)
                {
                    LimiteCreditoTextBox.Enabled = true;
                }
                else
                {
                    LimiteCreditoTextBox.Enabled = false;
                }
            }
            else
            {
                CodigoClienteTextBox.Text = OBJCliente.IDCliente.ToString();
            }
            NomeClienteTextBox.Text = OBJCliente.NomeCliente;

            LimiteCreditoTextBox.Text = string.Format("{0:N}", OBJCliente.LimiteCredito);
            PagamentoUnicoDropDownList.SelectedValue = OBJCliente.PagamentoUnico.ToString();
            AutorizacaoCobrancaDropDownList.SelectedValue = OBJCliente.AutorizacaoCobranca.ToString();


            //Atualiza dados do GRID
            AtualizaGrid();

        }


        public void CarregaDadosDaTela()
        {

            OBJCliente.CodigoUsuario = Session["usuario"].ToString();
            OBJCliente.IDCondPag = CondicaoPagamentoDropDownList.SelectedItem.Value;
            OBJCliente.LimiteCredito = Convert.ToDecimal(LimiteCreditoTextBox.Text);
            OBJCliente.PagamentoUnico = PagamentoUnicoDropDownList.SelectedItem.Value;
            OBJCliente.AutorizacaoCobranca = AutorizacaoCobrancaDropDownList.SelectedItem.Value;

        }

        public void AtualizaGrid()
        {
            DataTable retornoDados = new DataTable();

            retornoDados = OBJCliente.CarregaClienteCondicaoPagamento();

            ClienteCondicaoPagamentoGridView.DataSource = retornoDados;
            ClienteCondicaoPagamentoGridView.DataBind();
            ClientesCondicaoPagamentoMultiView.Visible = true;
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            if (Session["clienteClasse"] != null)
            {
                //Descarega a session da Entidade
                OBJCliente = (ClienteClasse)Session["clienteClasse"];

                CarregaDadosDaTela();

                AtualizaGrid();
            }
        }

        protected void GravarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["clienteClasse"] != null)
            {
                //Descarega a session da Entidade
                OBJCliente = (ClienteClasse)Session["clienteClasse"];

                CarregaDadosDaTela();

                erro = OBJCliente.gravaDadosClienteFinanceiro();

            }
            else
            {
                erro = "Session foi finalizada antes da conclusão da Operação, favor sair e tentar novamente";
            }


            if (erro == "")
            {
                TrataAlteracaoStatusAnalise();

                if (OBJCliente.IDCondPag != "")
                {
                    LimpaCampos();
                    CarregaDadosDaTela();
                    AtualizaGrid();

                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Dados Atualizados e Condição Incluida com Sucesso!", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                }
                else
                {
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Dados Atualizados com Sucesso!", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                }

            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (Session["clienteClasse"] != null)
            {
                //Descarega a session da Entidade
                OBJCliente = (ClienteClasse)Session["clienteClasse"];
                OBJCliente.CodigoUsuario = Session["usuario"].ToString();
                OBJCliente.IDCondPag = ((Label)((Control)sender).FindControl("IDCondPagLabel")).Text;

                erro = OBJCliente.ExcluiDadosClienteCondicoesPagamento();
            }
            else
            {
                erro = "Session foi finalizada antes da conclusão da Operação, favor sair e tentar novamente";
            }


            if (erro == "")
            {
                TrataAlteracaoStatusAnalise();
                LimpaCampos();
                CarregaDadosDaTela();
                AtualizaGrid();

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Condição Deletada com Sucesso!", true);
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

        public void LimpaCampos()
        {
            CondicaoPagamentoDropDownList.SelectedValue = "";
        }


        public void TrataAcesso()
        {
            usuario ObjusuarioAux = new usuario();

            ObjusuarioAux = new usuario();
            ObjusuarioAux.CodigoUsuario = Session["usuario"].ToString();
            ObjusuarioAux.ConsultaGrupos("Ativo");

            BuscarButton.Visible = true;

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
                    ClienteCondicaoPagamentoGridView.Columns[0].Visible = true;

                    //Somente libera condição de pagamento para quem for do grupo Financeiro
                    if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 6).Count() <= 0)
                    {
                        CondicaoPagamentoDropDownList.CssClass = "form-control";
                        CondicaoPagamentoDropDownList.Enabled = false;
                    }

                    break;
                case 4: //Status Cliente Análise Financeira
                        //Verifica se esta no Grupo Análise Financeira
                    if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 7).Count() > 0)
                    {
                        ClienteCondicaoPagamentoGridView.Columns[0].Visible = true;
                        GravarButton.Visible = true;
                    }
                    else
                    {
                        ClienteCondicaoPagamentoGridView.Columns[0].Visible = false;
                        GravarButton.Visible = false;
                    }

                    //Somente libera condição de pagamento para quem for do grupo Financeiro
                    if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 6).Count() <= 0)
                    {
                        CondicaoPagamentoDropDownList.CssClass = "form-control";
                        CondicaoPagamentoDropDownList.Enabled = false;
                    }

                    break;

                case 5: //Status Cliente Análise Fiscal
                    ClienteCondicaoPagamentoGridView.Columns[0].Visible = false;
                    GravarButton.Visible = false;

                    //Somente libera condição de pagamento para quem for do grupo Financeiro
                    if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 6).Count() <= 0)
                    {
                        CondicaoPagamentoDropDownList.Enabled = false;
                    }


                    break;

                default:
                    GravarButton.Visible = false;
                    ClienteCondicaoPagamentoGridView.Columns[0].Visible = false;
                    break;
            }
        }


        public void TrataAlteracaoStatusAnalise()
        {

            OBJCliente.carregaDadosPrincipais();

            switch (OBJCliente.IDStatus)
            {
                case 0: //Novo Cadastro
                    break;
                case 1: //Status Cliente Prospectivo
                    break;
                case 4: //Status Cliente Análise Financeira
                    break;

                case 2: //Status Cliente Ativo
                case 3: //Status Cliente Inativo
                case 5: //Status Cliente Análise Fiscal

                    //Enviar Cliente para Analise Financeiro
                    OBJCliente.CodigoUsuario = Session["usuario"].ToString();
                    OBJCliente.IDStatus = 4; //Analise Financeiro
                    OBJCliente.AlteraStatusCliente();
                    break;

                default:
                    break;



            }
        }
    }
}
