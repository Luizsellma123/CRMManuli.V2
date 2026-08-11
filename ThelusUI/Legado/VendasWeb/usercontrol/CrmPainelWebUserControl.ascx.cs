using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Data;
using VendasWeb.classes;

namespace VendasWeb.usercontrol
{
    public partial class CrmPainelWebUserControl : System.Web.UI.UserControl
    {


        clsEntidades ObjEntidadesClass = new clsEntidades();
        funcoes mdlFuncoes = new funcoes();
        criptografia mdlCriptografia = new criptografia();
        usuario ObjUsuarioClass = new usuario();
        VendedorClass ObjVendedorClass = new VendedorClass();
        produto ObjProduto = new produto();
        HistoricoCRMClass ObjHistoricoCRMClass = new HistoricoCRMClass();
        ClienteClasse OBJCliente = new ClienteClasse();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                //Redireciona para tela de login
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                ObjUsuarioClass.CodigoUsuario = Session["usuario"].ToString();
            }

            //Verifica se mostra botoes de cadastro de entidade
            if (ObjUsuarioClass.AcessoCadastroCliente() <= 0)
            {
                NovaEntidadeButton.Visible = false;
                CadastroDetalheLinkButton.Visible = false;
            }
            else
            {
                NovaEntidadeButton.Visible = true;
                CadastroDetalheLinkButton.Visible = true;
            }

            TrataBloqueios();


            if (!Page.IsPostBack)
            {

                if (Session["clsEntidades"] != null)
                {
                    refresh();
                }

            }



        }



        public void refresh()
        {
            //update controls's property and rebind data inside the usercontrol.

            if (Session["clsEntidades"] != null)
            {
                //Descarrega session
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

                //Valida acesso para Liberar menus
                ValidaAcesso(ObjEntidadesClass.IDCliente);
            }

        }

        public void Desabilitar_Botoes()
        {
            ExpectativaLinkButton.Visible = false;
            //CarteirasLinkButton.Visible = false;
            ClassificacaoLinkButton.Visible = false;
            RelatorioGerencialLinkButton.Visible = false;
            AtendimentoLinkButton.Visible = false;
            QuantidadeClientesLinkButton.Visible = false;
            //SimuladorLinkButton.Visible = false;
        }

        public void refreshVendedor()
        {
            //update controls's property and rebind data inside the usercontrol.

            /*if (Session["VendCod"] != null)
            { */
            //ExpectativaLinkButton.Visible = true;
            //CarteirasLinkButton.Visible = true;
            //AtendimentoLinkButton.Visible = true;
            //QuantidadeClientesLinkButton.Visible = true;
            //ClassificacaoLinkButton.Visible = true;
            //SimuladorLinkButton.Visible = true;

            ObjUsuarioClass.CodigoUsuario = Session["usuario"].ToString();
            //RelatorioGerencialLinkButton.Visible = true;
            if (ObjUsuarioClass.AcessoCadastroCliente() <= 0)
            {
                NovaEntidadeButton.Visible = false;
                CadastroDetalheLinkButton.Visible = false;
            }
            else
            {
                NovaEntidadeButton.Visible = true;
                CadastroDetalheLinkButton.Visible = true;
            }


            ContatoLinkButton.Visible = false;
            PerfilComercialLinkButton.Visible = false;
            AnexosLinkButton.Visible = false;
            CRMLinkButton.Visible = false;
            IncluirCarteiraLinkButton.Visible = false;
            ExcluirCarteiraLinkButton.Visible = false;
            PedidoLinkButton.Visible = false;
            ListaLinkButton.Visible = false;
            NotasLinkButton.Visible = false;
            LogisticaLinkButton.Visible = false;
            DuplicatasLinkButton.Visible = false;
            FiscalLinkButton.Visible = false;
            FinanceiroLinkButton.Visible = false;
            AnaliseLinkButton.Visible = false;
            CalendarioLinkButton.Visible = false;
            EstoqueLinkButton.Visible = false;

            ExpectativaLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-book fa-3x";
            CarteirasLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-book fa-3x";
            ClassificacaoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-book fa-3x";
            RelatorioGerencialLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-book fa-3x";

            FretesCidadesGerencialLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-book fa-3x";
            FretesEstadosGerencialLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-book fa-3x";
            CadastroExpClassesLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-book fa-3x";

            AtendimentoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-book fa-3x";
            QuantidadeClientesLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-book fa-3x";
            //}

        }



        #region Acessos



        public void ValidaAcesso(int IDCliente)
        {
            //ObjEntidadesClass = new clsEntidades();
            //Descarega a session da Entidade
            ObjEntidadesClass.IDCliente = IDCliente;
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

            //ObjEntidadesClass.Consulta_Entidade_Acessos();

            DataTable AcessoDataTable = new DataTable(); //DataTable para Retorno dos acessos
            AcessoDataTable = ObjEntidadesClass.Consulta_Entidade_Acessos();

            ObjUsuarioClass.CodigoUsuario = Session["usuario"].ToString();
            if (ObjUsuarioClass.AcessoCadastroCliente() <= 0)
            {
                NovaEntidadeButton.Visible = false;
                CadastroDetalheLinkButton.Visible = false;
            }
            else
            {
                NovaEntidadeButton.Visible = true;
                CadastroDetalheLinkButton.Visible = true;
            }

            //ContatoLinkButton.Visible = true;
            //PerfilComercialLinkButton.Visible = true;
            //AnexosLinkButton.Visible = true;
            //CRMLinkButton.Visible = true;
            IncluirCarteiraLinkButton.Visible = true;
            ExcluirCarteiraLinkButton.Visible = true;
            PedidoLinkButton.Visible = true;
            ListaLinkButton.Visible = true;
            //NotasLinkButton.Visible = true;
            //LogisticaLinkButton.Visible = true;
            //DuplicatasLinkButton.Visible = true;
            //FiscalLinkButton.Visible = true;
            //FinanceiroLinkButton.Visible = true;
            //AnaliseLinkButton.Visible = true;
            //CalendarioLinkButton.Visible = false;
            //EstoqueLinkButton.Visible = true;

            //ExpectativaLinkButton.Visible = false;
            //CarteirasLinkButton.Visible = false;
            //ClassificacaoLinkButton.Visible = false;
            //RelatorioGerencialLinkButton.Visible = false;
            //AtendimentoLinkButton.Visible = false;
            //QuantidadeClientesLinkButton.Visible = false;

            //Verifica se retornou alguma linha
            if (AcessoDataTable.Rows.Count >= 1)
            {
                //Percorre todas as linhas
                foreach (DataRow row in AcessoDataTable.Rows)
                {

                    string Acesso = row["Acesso"].ToString().ToUpper();
                    switch (Acesso)
                    {
                        case "ENTIDADE_VENDEDOR":
                            CadastroDetalheLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x";
                            ContatoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-phone-square fa-3x";
                            PerfilComercialLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x";
                            AnexosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x";
                            AnaliseLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x"; //Bloqueado ate Entidade OK
                            CRMLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x";//Liberado acesso para todos chamado Manuli: 11587 - Jackson Lizier 12/02/2016    
                            IncluirCarteiraLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-plus-circle fa-3x disabled";
                            ExcluirCarteiraLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-times-circle fa-3x";
                            PedidoLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-cart-plus fa-3x";
                            ListaLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-list-alt fa-3x";
                            NotasLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-file-text fa-3x";
                            LogisticaLinkButton.CssClass = "btn btn-lg btn-block btn-primary btn-labeled fa fa-truck fa-3x";
                            DuplicatasLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-list fa-3x";
                            FiscalLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-money fa-3x";
                            FinanceiroLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x";
                            ExpectativaLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x";
                            CarteirasLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x";
                            //ClassificacaoLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x";
                            RelatorioGerencialLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x";
                            break;


                        case "LIVRE":
                            CadastroDetalheLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x";
                            ContatoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-phone-square fa-3x";
                            PerfilComercialLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x";
                            AnexosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x";
                            AnaliseLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x"; //Bloqueado ate Entidade OK
                            CRMLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x";//Liberado acesso para todos chamado Manuli: 11587 - Jackson Lizier 12/02/2016    
                            IncluirCarteiraLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-plus-circle fa-3x";
                            ExcluirCarteiraLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-times-circle fa-3x disabled";
                            PedidoLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-cart-plus fa-3x disabled";
                            ListaLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-list-alt fa-3x disabled";
                            NotasLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-file-text fa-3x disabled";
                            LogisticaLinkButton.CssClass = "btn btn-lg btn-block btn-primary btn-labeled fa fa-truck fa-3x disabled";
                            DuplicatasLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-list fa-3x disabled";
                            FiscalLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-money fa-3x disabled";
                            FinanceiroLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled";
                            ExpectativaLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled";
                            CarteirasLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled";
                            //ClassificacaoLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled";
                            RelatorioGerencialLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled";
                            break;

                        case "OUTRO_VENDEDOR":
                            CadastroDetalheLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x disabled";
                            ContatoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-phone-square fa-3x disabled";
                            PerfilComercialLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x disabled";
                            AnexosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x disabled";
                            AnaliseLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x disabled";  //Bloqueado ate Entidade OK
                            CRMLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x";//Liberado acesso para todos chamado Manuli: 11587 - Jackson Lizier 12/02/2016    
                            IncluirCarteiraLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-plus-circle fa-3x disabled";
                            ExcluirCarteiraLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-times-circle fa-3x disabled";
                            PedidoLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-cart-plus fa-3x disabled";
                            ListaLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-list-alt fa-3x disabled";
                            NotasLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-file-text fa-3x disabled";
                            LogisticaLinkButton.CssClass = "btn btn-lg btn-block btn-primary btn-labeled fa fa-truck fa-3x disabled";
                            DuplicatasLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-list fa-3x disabled";
                            FiscalLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-money fa-3x disabled";
                            FinanceiroLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled";
                            ExpectativaLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled";
                            CarteirasLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled";
                            //ClassificacaoLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled";
                            RelatorioGerencialLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled";
                            break;

                        case "ADM":
                            CadastroDetalheLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x";
                            ContatoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-phone-square fa-3x";
                            PerfilComercialLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x";
                            AnexosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x";
                            AnaliseLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x";  //Bloqueado ate Entidade OK
                            CRMLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x";//Liberado acesso para todos chamado Manuli: 11587 - Jackson Lizier 12/02/2016    
                            IncluirCarteiraLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-plus-circle fa-3x";
                            ExcluirCarteiraLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-times-circle fa-3x";
                            PedidoLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-cart-plus fa-3x";
                            ListaLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-list-alt fa-3x";
                            NotasLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-file-text fa-3x";
                            LogisticaLinkButton.CssClass = "btn btn-lg btn-block btn-primary btn-labeled fa fa-truck fa-3x";
                            DuplicatasLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-list fa-3x";
                            FiscalLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-money fa-3x";
                            FinanceiroLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x";
                            ExpectativaLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x";
                            CarteirasLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x";
                            //ClassificacaoLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x";
                            RelatorioGerencialLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x";
                            break;


                        default://Algum problema
                            CadastroDetalheLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x disabled";
                            ContatoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-phone-square fa-3x disabled";
                            PerfilComercialLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x disabled";
                            AnexosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x disabled";
                            AnaliseLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x disabled";  //Bloqueado ate Entidade OK
                            CRMLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x disabled";
                            IncluirCarteiraLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-plus-circle fa-3x disabled";
                            ExcluirCarteiraLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-times-circle fa-3x disabled";
                            PedidoLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-cart-plus fa-3x disabled";
                            ListaLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-list-alt fa-3x disabled";
                            NotasLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-file-text fa-3x disabled";
                            DuplicatasLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-list fa-3x disabled";
                            FiscalLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-money fa-3x disabled";
                            FinanceiroLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled";
                            ExpectativaLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled";
                            CarteirasLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled";
                            //ClassificacaoLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled";
                            RelatorioGerencialLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-credit-card fa-3x disabled";
                            break;
                    }

                    //Se situação comercial for diferente de cliente ativo não permite incluir pedido
                    if (ObjEntidadesClass.SituacaoComercial != "Ativo")
                    {
                        PedidoLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-cart-plus fa-3x disabled";
                    }

                }
            }
        }

        public void BloqueiaAcesso()
        {
            Session["clsEntidades"] = null;
            CadastroDetalheLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-file-text-o fa-3x disabled";
            ContatoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-phone-square fa-3x disabled";
            PerfilComercialLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-users fa-3x disabled";
            AnexosLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x disabled";
            AnaliseLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x disabled";
            CRMLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x disabled";
            IncluirCarteiraLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-plus-circle fa-3x disabled";
            ExcluirCarteiraLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-times-circle fa-3x disabled";
            PedidoLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-cart-plus fa-3x disabled";
            ListaLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-list-alt fa-3x disabled";
            NotasLinkButton.CssClass = "btn btn-lg btn-block btn-success btn-labeled fa fa-file-text fa-3x disabled";
            DuplicatasLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-list fa-3x disabled";
            FiscalLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-money fa-3x disabled";
            ExpectativaLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-money fa-3x disabled";
            CarteirasLinkButton.CssClass = "btn btn-lg btn-block btn-warning btn-labeled fa fa-money fa-3x disabled";
            ExpectativaLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x disabled";
            //ClassificacaoLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x disabled";
            RelatorioGerencialLinkButton.CssClass = "btn btn-lg btn-block btn-danger btn-labeled fa fa-book fa-3x disabled";
        }




        #endregion


        protected void NovaEntidadeButton_Click(object sender, EventArgs e)
        {
            //Session["clsEntidades"] = null;
            Session["clienteClasse"] = null;

            Response.Redirect("../Clientes/CadastroClienteWebForm.aspx?indmnu=2");
            //Response.Redirect("../Entidade/FrmAbaPrincipal.aspx?indmnu=2");
            //Response.Redirect("../Entidades/FrmAbaPrincipal.aspx?indmnu=2");

        }

        protected void AnaliseLinkButton_Click(object sender, EventArgs e)
        {
            /*Pega o codigo da Entidade Selecionada*/
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = new clsEntidades();

                //Descarega a session da Entidade
                ObjEntidadesClass = (clsEntidades)Session["clsEntidades"];
                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

                ObjEntidadesClass.Mostra_Entidade();

                ObjEntidadesClass.TipoOperacao = "Alterar";
                ObjEntidadesClass.Origem = "Analise";
                /*Carrega em Session*/
                Session["clsEntidades"] = ObjEntidadesClass;

                /*Chama a proxima Tela*/
                Response.Redirect("../Entidade/FrmAbaPrincipal.aspx?indmnu=2");
            }


            //Response.Redirect("../Entidade/FrmAbaPrincipal.aspx?indmnu=2");

        }

        protected void CadastroDetalheLinkButton_Click(object sender, EventArgs e)
        {
            /*Pega o codigo da Entidade Selecionada*/
            if (Session["clienteClasse"] != null)
            {

                /*Chama a proxima Tela*/
                //Response.Redirect("../Entidade/FrmAbaPrincipal.aspx?indmnu=2");
                //Chama a proxima Tela
                Response.Redirect("../Clientes/CadastroClienteWebForm.aspx?indmnu=2");
            }
        }

        protected void ExpectativaLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../GerencialVendas/FrmCadastroExpectativas.aspx?indmnu=2");
        }

        protected void CarteirasLinkButton_Click(object sender, EventArgs e)
        {
            refreshVendedor();
            /*Pega o codigo do vendedor Selecionado*/
            if (Session["VendCod"] != null)
            {

                ObjVendedorClass.VendCod = Session["VendCod"].ToString();
                //ObjVendedorClass.Listar_Vendedores();

                /*Chama a proxima Tela*/
                //Response.Redirect("FrmGestoresClasses.aspx?indmnu=2");
            }
        }

        protected void ClassificacaoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Entidades/frmClassificacaoEntidade.aspx?indmnu=5");
        }

        protected void RelatorioGerencialLinkButton_Click(object sender, EventArgs e)
        {
            /*Pega o codigo do vendedor Selecionado*/
            if (Session["VendCod"] != null)
            {

                ObjVendedorClass.VendCod = Session["VendCod"].ToString();

                /*Chama a proxima Tela*/
                Response.Redirect("../Entidades/FrmRelatorioGerencial.aspx?indmnu=2");
            }
        }

        protected void ContatoLinkButton_Click(object sender, EventArgs e)
        {

            /*Pega o codigo da Entidade Selecionada*/
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = new clsEntidades();

                //Descarega a session da Entidade
                ObjEntidadesClass = (clsEntidades)Session["clsEntidades"];
                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

                ObjEntidadesClass.Mostra_Entidade();

                ObjEntidadesClass.Origem = "Outros";
                /*Carrega em Session*/
                Session["clsEntidades"] = ObjEntidadesClass;

                /*Chama a proxima Tela*/
                Response.Redirect("../Entidade/FrmAbaContatos.aspx?indmnu=2");
            }

        }

        protected void PerfilComercialLinkButton_Click(object sender, EventArgs e)
        {
            /*Pega o codigo da Entidade Selecionada*/
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = new clsEntidades();

                //Descarega a session da Entidade
                ObjEntidadesClass = (clsEntidades)Session["clsEntidades"];
                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

                ObjEntidadesClass.Mostra_Entidade();
                /*Carrega em Session*/
                Session["clsEntidades"] = ObjEntidadesClass;

                /*Chama a proxima Tela*/
                Response.Redirect("../Entidades/FrmPerfilComercial.aspx?indmnu=2");
            }
        }

        protected void AnexosLinkButton_Click(object sender, EventArgs e)
        {

            /*Pega o codigo da Entidade Selecionada*/
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = new clsEntidades();
                //Descarega a session da Entidade
                ObjEntidadesClass = (clsEntidades)Session["clsEntidades"];
                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

                ObjEntidadesClass.Mostra_Entidade();

                ObjEntidadesClass.Origem = "Outros";
                /*Carrega em Session*/
                Session["clsEntidades"] = ObjEntidadesClass;

                /*Chama a proxima Tela*/
                Response.Redirect("../Entidade/FrmAbaAnexo.aspx?indmnu=2");
            }

        }

        protected void CRMLinkButton_Click(object sender, EventArgs e)
        {
            Session["TelaHist"] = "";

            /*Pega o codigo da Entidade Selecionada*/
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = new clsEntidades();
                Session["Retornar"] = "FrmCarteira.aspx?indmnu=2";
                //Session["Retornar"] = ResolveUrl("~/Entidades/FrmCarteira.aspx?indmnu=2");

                /*Chama a proxima Tela*/
                Response.Redirect("../Entidades/FrmHistoricoCRM.aspx?indmnu=12");
            }
        }

        protected void IncluirCarteiraLinkButton_Click(object sender, EventArgs e)
        {
            Session["TelaHist"] = "Incluir";

            /*Pega o codigo da Entidade Selecionada*/
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = new clsEntidades();
                Session["Retornar"] = "FrmCarteira.aspx?indmnu=2";
                //Session["Retornar"] = ResolveUrl("~/Entidades/FrmCarteira.aspx?indmnu=2");

                /*Pega o codigo da Entidade Selecionada*/
                ObjEntidadesClass = (clsEntidades)Session["clsEntidades"];
                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                ObjEntidadesClass.TipoOperacao = "Incluir Carteira";
                /*Carrega em Session*/
                Session["clsEntidades"] = ObjEntidadesClass;

                /*Chama a proxima Tela*/
                Response.Redirect("../Entidades/FrmSelecaoVendedor.aspx?indmnu=2");
            }
        }

        protected void ExcluirCarteiraLinkButton_Click(object sender, EventArgs e)
        {
            Session["TelaHist"] = "Excluir";

            /*Pega o codigo da Entidade Selecionada*/
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = new clsEntidades();
                Session["Retornar"] = "FrmCarteira.aspx?indmnu=2";
                //Session["Retornar"] = ResolveUrl("~/Entidades/FrmCarteira.aspx?indmnu=2");

                /*Pega o codigo da Entidade Selecionada*/
                ObjEntidadesClass = (clsEntidades)Session["clsEntidades"];
                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                ObjEntidadesClass.TipoOperacao = "Excluir Carteira";

                /*Carrega em Session*/
                Session["clsEntidades"] = ObjEntidadesClass;

                /*Chama a proxima Tela*/
                Response.Redirect("../Entidades/FrmHistoricoCRM.aspx?indmnu=12");
            }
        }

        protected void PedidoLinkButton_Click(object sender, EventArgs e)
        {
            if (Session["clsEntidades"] != null)
            {
                //Descarrega session
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

                ClienteClasse objClienteClasse = new ClienteClasse();

                objClienteClasse.CodigoCliente = ObjEntidadesClass.CodigoClienteSAP;

                DataTable ClassificacaoComercialDataTable = objClienteClasse.CarregaClassificacaoComercial();

                int IDClassificacaoComercial = 0;

                if (ClassificacaoComercialDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in ClassificacaoComercialDataTable.Rows)
                    {
                        IDClassificacaoComercial = Convert.ToInt32(row["IDClassificacaoComercial"]);
                    }
                }

                if (IDClassificacaoComercial > 0)
                {
                    Session["Retornar"] = "../Entidades/FrmCarteira.aspx?indmnu=2";
                    //Session["Retornar"] = ResolveUrl("~/Entidades/FrmCarteira.aspx?indmnu=2");
                    Response.Redirect("../Entidades/FrmSelecaoEmpresa.aspx?indmnu=2");
                }
                else
                {
                    Session["Msg"] = "Cliente sem classificação comercial, entrar em contato com administração de vendas.";
                    Response.Redirect("../Entidades/FrmCarteira.aspx?indmnu=2");
                }
            }
        }

        protected void DuplicatasLinkButton_Click(object sender, EventArgs e)
        {

            /*Pega o codigo da Entidade Selecionada*/
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = new clsEntidades();
                //Descarega a session da Entidade
                ObjEntidadesClass = (clsEntidades)Session["clsEntidades"];
                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

                ObjEntidadesClass.Mostra_Entidade();
                /*Carrega em Session*/
                Session["clsEntidades"] = ObjEntidadesClass;

                /*Chama a proxima Tela*/
                Response.Redirect("FrmAbaDuplicata.aspx?indmnu=2");
            }

        }

        protected void NotasLinkButton_Click(object sender, EventArgs e)
        {

            /*Pega o codigo da Entidade Selecionada*/
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = new clsEntidades();
                //Descarega a session da Entidade
                ObjEntidadesClass = (clsEntidades)Session["clsEntidades"];
                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

                ObjEntidadesClass.Mostra_Entidade();
                /*Carrega em Session*/
                Session["clsEntidades"] = ObjEntidadesClass;

                /*Chama a proxima Tela*/
                //Session["Retornar"] = "FrmCarteira.aspx?indmnu=2";
                Session["Retornar"] = ResolveUrl("~/Entidades/FrmCarteira.aspx?indmnu=2");
                Response.Redirect("frmListaNotasFiscais.aspx?indmnu=2");
            }


        }

        protected void LogisticaLinkButton_Click(object sender, EventArgs e)
        {

            /*Pega o codigo da Entidade Selecionada*/
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = new clsEntidades();
                //Descarega a session da Entidade
                ObjEntidadesClass = (clsEntidades)Session["clsEntidades"];
                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

                ObjEntidadesClass.Mostra_Entidade();
                /*Carrega em Session*/
                Session["clsEntidades"] = ObjEntidadesClass;

                /*Chama a proxima Tela*/
                Response.Redirect("~/FrmAbaLogistica.aspx?indmnu=2");
            }

        }

        protected void ListaLinkButton_Click(object sender, EventArgs e)
        {
            /*Chama a proxima Tela*/
            Session["ObjFiltroClass"] = null;
            Response.Redirect("../listas/FrmListaPedidos.aspx?indmnu=2");
        }

        protected void FiscalLinkButton_Click(object sender, EventArgs e)
        {
            /*Pega o codigo da Entidade Selecionada*/
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = new clsEntidades();
                //Descarega a session da Entidade
                ObjEntidadesClass = (clsEntidades)Session["clsEntidades"];
                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

                ObjEntidadesClass.Mostra_Entidade();
                /*Carrega em Session*/
                Session["clsEntidades"] = ObjEntidadesClass;

                /*Chama a proxima Tela*/
                Response.Redirect("FrmAbaFiscal.aspx?indmnu=2");
            }
        }





        protected void FinanceiroLinkButton_Click(object sender, EventArgs e)
        {

            /*Pega o codigo da Entidade Selecionada*/
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = new clsEntidades();
                //Descarega a session da Entidade
                ObjEntidadesClass = (clsEntidades)Session["clsEntidades"];
                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

                ObjEntidadesClass.Mostra_Entidade();
                /*Carrega em Session*/
                Session["clsEntidades"] = ObjEntidadesClass;

                /*Chama a proxima Tela*/
                Response.Redirect("FrmAbaFinanceiro.aspx?indmnu=2");
            }

        }

        protected void QuantidadeClientesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Entidades/FrmQtdClienteVendedor.aspx?indmnu=5");
        }


        protected void GeolocalizacaoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Entidades/FrmMaps.aspx?indmnu=5");
        }



        protected void RoterizacaoPainelLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Entidades/FrmMapsListaRota.aspx?indmnu=5");
        }


        public void Libera_Geomapeamento()
        {
            GeolocalizacaoLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-map-marker fa-3x";
        }


        public void Libera_Roterizacao()
        {
            RoterizacaoPainelLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-car fa-3x";
        }

        public void FretesCidadesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../GerencialVendas/FrmFretesCidades.aspx?indmnu=5");
        }

        public void FretesEstadosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../GerencialVendas/FrmFretesEstados.aspx?indmnu=5");
        }

        public void MargensProdutoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../GerencialVendas/MargensProduto.aspx?indmnu=5");
        }

        public void CadastroExpClassesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../GerencialVendas/FrmCadastroExpClasses.aspx?indmnu=5");
        }

        protected void PedidoProdutosLinkButton_Click(object sender, EventArgs e)
        {
            //Limpa Session de Filtros
            Session["ObjFiltroClass"] = null;
            Response.Redirect("../Listas/FrmListaPedidosProdutos.aspx?indmnu=5");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../GerencialVendas/FrmListaSimuladorForm.aspx?indmnu=3");
        }

        protected void AcompanhamentoPedidoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Entidades/AcompanhamentoVendasWebForm.aspx?indmnu=3");
        }

        protected void TabelaPrecoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Entidades/TabelaPrecoWebForm.aspx?indmnu=3");
        }

        protected void SimuladorLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Entidades/FrmListaSimuladorVendedorForm.aspx?indmnu=3");
        }

        public void TrataBloqueios()
        {
            ObjUsuarioClass.CodigoUsuario = Session["usuario"].ToString();

            if (ObjUsuarioClass.ValidaPeriodos() <= 0)
            {
                SimuladorLinkButton.CssClass = "btn btn-lg btn-block btn-info btn-labeled fa fa-paperclip fa-3x disabled"; //Bloqueado ate Entidade OK
            }
        }

        protected void ClientesAtivosLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Entidades/FrmListaRelatoriosClientesAtivosWebForm.aspx?indmnu=3");
        }
    }
}