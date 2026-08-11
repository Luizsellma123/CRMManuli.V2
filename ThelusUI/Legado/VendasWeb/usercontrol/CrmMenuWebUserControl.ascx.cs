using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VendasWeb.classes;

namespace VendasWeb.usercontrol
{
    public partial class CrmMenuWebUserControl : System.Web.UI.UserControl
    {
        funcoes mdlFuncoes = new funcoes();
        GerencialVendas.clsAcessos objClsAcessos = new GerencialVendas.clsAcessos();
        usuario usuarioClass = new usuario();

        funcoes mdlfuncoes = new funcoes();
        string prefix = "";
        string[] strCss;


        public string DashBoardActive = "";
        public string HomeActive = "";
        public string CarteiraActive = "";
        public string IntranetActive = "";
        public string LogisticaActive = "";
        public string AprovaPedidosActive = "";
        public string LiberarPedidosActive = "";
        public string TIActive = "";
        public string ApontamentoProducaoActive = "";
        public string ConsultasActive = "";
        public string ManuaisActive = "";
        public string DocumentosActive = "";
        public string TabelaPrecoActive = "";
        public string AgendaVisitaActive = "";
        public string CalendarioActive = "";
        public string ControladoriaActive = "";
        public string HistoricoActive = "";



        protected void Page_Load(object sender, EventArgs e)
        {

            int varmenu = Convert.ToInt32(Request.QueryString["indmnu"]);
            int verLogin = Convert.ToInt32(Session["idLogin"]);

            if (varmenu == 0)
            {
                Session.RemoveAll();
                verLogin = 0;
            }

            if (verLogin > 0)
            {


                switch (Page.ToString().ToLower())
                {

                    case "asp.home_aspx":
                        HomeActive = "active-link";
                        break;

                    case "asp.entidades_frmcarteira_aspx":
                    case "asp.entidades_frmperfilcomercial_aspx":
                    case "asp.entidades_frmabafinanceiro_aspx":
                    case "asp.entidades_frmabaduplicata_aspx":
                        CarteiraActive = "active-link";
                        break;


                    case "asp.entidades_dashboard_aspx":
                        DashBoardActive = "active-link";
                        break;


                    case "asp.entidades_frmagendavisita_aspx":
                    case "asp.entidades_frmagendavisitadetalhe_aspx":
                        AgendaVisitaActive = "active-link";
                        break;


                    case "asp.entidades_frmcalendario_aspx":
                        CalendarioActive = "active-link";
                        break;





                }



                carregamenu(varmenu);
            }

        }




        public void carregamenu(int varmenu)
        {
            strCss = montaCss(varmenu);


            if (varmenu > 1)
                prefix = "../";

            if (varmenu == 10)
                prefix = "../../../";

            if (varmenu == 11)
                prefix = "../../";

            MenuLiteral.Text = "";

            MenuLiteral.Text = MontaMenus(prefix);

            /*
            MenuHome();
            MenuHistorico();
            MenuTrello();
            MenuDashBoard();
            //MenuAgendaVisita();

            MenuCarteira();
            MenuIntranet();

            MenuPortalCliente();

            #region Controle de Acesso

            //Validacao de Perfis
            DataTable GrupoUsuario = new DataTable(); //DataTable para Retorno dos acessos
            objClsAcessos.UsuCod = Session["usuario"].ToString();
            GrupoUsuario = objClsAcessos.Consulta_Acesso_Usuario();

            Session["keyuser"] = 0; //Deixa a session de KeyUser como Zero caso nao entre no If abaixo.
            Session["AcessoDiretoria"] = "Não";//Utilizada na Tela FrmSelecaoVendedor


            //Verifica se Tem algum aceso expecifico
            if (GrupoUsuario.Rows.Count >= 1)
            {
                DataTable outputTable = new DataTable();
                outputTable = mdlFuncoes.Consulta_Gestores_Classe();
                int Contador = 0;

                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row1 in outputTable.Rows)
                    {
                        if (row1["UsuCod"].ToString() == Session["usuario"].ToString())
                        {
                            MenuGerencialVendas();
                            Contador = 1;
                        }
                    }
                }

                foreach (DataRow row in GrupoUsuario.Rows)
                {
                    switch (row["GrpUsuCod"].ToString().ToUpper())
                    {

                        case "ADM_VENDAS":
                            if (Contador == 0)
                            {
                                MenuGerencialVendas();
                            }
                            break;

                        case "LOGISTICA":
                            MenuLogistica();
                            break;

                        case "APROVA_PEDIDO":
                            MenuAprovarPedidos();
                            break;

                        case "APROVA_LOGISTICA":
                            MenuLiberarPedidosLogistica();
                            break;

                        case "LIBERA_PEDIDO":
                            MenuLiberarPedidos();
                            break;


                        case "TI":
                            MenuTI();
                            break;

                        case "PRODUÇÃO(PPCP)":
                            MenuApontamentoProcucao();
                            break;


                        case "DIRETORIA":
                            Session["AcessoDiretoria"] = "Sim";
                            break;

                        case "CONTROLADORIA":
                            MenuControladoria();
                            break;

                    }


                }
            }

            #endregion


            MenuCalendario();

            MenuConsultas();
            MenuManuais();
            MenuDocumentos();
            MenuTabelaPreco();
            */
        }

        public string MontaMenus(string prefix)
        {
            string menus = "";
            usuarioClass.CodigoUsuario = Session["usuario"].ToString();
            usuarioClass.ConsultaMenus();

            foreach (ClasseMenus Menu in usuarioClass.ListaMenus)
            {
                menus += contatenaMenus(Menu.NomeMenu, Menu.Endereco, Menu.IconeCSS, prefix);
            }

            return menus;
        }

        public string contatenaMenus(string nome, string endereco, string IconeCSS, string prefix)
        {
            string Menus = "";

            
            Menus += "<li class=\"" + DashBoardActive + "\">";

            //Verifica se é um menu ou link
            if (endereco.Substring(0, 4) == "http")
            {
                Menus += "<a href=\"" + endereco + "\">";
            }else
            {
                Menus += "<a href=\"" + prefix + endereco + "\">";
            }

            Menus += "<i class=\"" + IconeCSS + "\"></i>";
            Menus += "<span class=\"menu-title\">";
            Menus += "<strong>" + nome.ToString() + "</strong>";
            //MenuLiteral.Text += "<span class=\"label label-success pull-right\">Novo</span>";
            Menus += "</span>";
            Menus += "</a>";
            Menus += "</li>";

            return Menus;

        }


        public string[] montaCss(int varmenu)
        {
            int i = 0;
            string[] auxCss = new string[50];

            for (i = 0; i <= 20; i++)
            {
                if (i == varmenu)
                {
                    auxCss[i] = "menudefault";
                }
                else
                {
                    auxCss[i] = "";
                }
            }

            return auxCss;
        }




        public void MenuDashBoard()
        {

            MenuLiteral.Text += "<li class=\"" + DashBoardActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "Dashboard/DashboardClasses.aspx?indmnu=5\">";
            MenuLiteral.Text += "<i class=\"fa fa-pie-chart\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>DashBoard</strong>";
            //MenuLiteral.Text += "<span class=\"label label-success pull-right\">Novo</span>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }

        public void MenuAgendaVisita()
        {

            MenuLiteral.Text += "<li class=\"" + AgendaVisitaActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "Entidades/FrmAgendaVisita.aspx?indmnu=5\">";
            MenuLiteral.Text += "<i class=\"fa fa-calendar\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Agenda de Visita</strong>";
            //MenuLiteral.Text += "<span class=\"label label-success pull-right\">Novo</span>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }



        public void MenuHome()
        {

            MenuLiteral.Text += "<li class=\"" + HomeActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "Home.aspx?indmnu=1\">";
            MenuLiteral.Text += "<i class=\"fa fa-home fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Home</strong>";
            //MenuLiteral.Text += "<span class=\"label label-success pull-right\">Novo</span>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }

        public void MenuCarteira()
        {

            MenuLiteral.Text += "<li class=\"" + CarteiraActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "Entidades/FrmCarteira.aspx?indmnu=5\">";
            MenuLiteral.Text += "<i class=\"fa fa-child fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Clientes</strong>";
            //MenuLiteral.Text += "<span class=\"label label-success pull-right\">Novo</span>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }

        public void MenuGerencialVendas()
        {
            MenuLiteral.Text += "<li class=\"" + LogisticaActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "Entidades/FrmVendedores.aspx?indmnu=5\">";
            MenuLiteral.Text += "<i class=\"fa fa-home fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Gerencial Vendas</strong>";
            MenuLiteral.Text += "<span class=\"label label-success pull-right\">Novo</span>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";


            MenuLiteral.Text += "<li class=\"" + LogisticaActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "Entidades/FrmRelatorioGerencial.aspx?indmnu=5\">";
            MenuLiteral.Text += "<i class=\"fa fa-home fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Relatório Gerencial</strong>";
            MenuLiteral.Text += "<span class=\"label label-success pull-right\">Novo</span>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";
        }


        public void MenuIntranet()
        {


            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            //Vareavel Para Tratamento
            string local = "";

            //Pego a URL Verifico se é Externo ou Interno
            //string Validaurl = url.Substring(7, 3);


            //switch (Validaurl)
            //{
            //    case "192":
            //        local = "http://192.168.0.240/intranet/login.aspx?indmnu=0";
            //        break;
            //    default:
            //        local = "http://intranet.manulifitasa.com.br/login.aspx?indmnu=0";
            //        break;
            //}



            if ((url.ToUpper().Contains("CRMINTERNO") == true))
            {
                local = "http://192.168.0.240/intranet/login.aspx?indmnu=0";
            }
            else
            {
                local = "http://intranet.manulifitasa.com.br/login.aspx?indmnu=0";
            }


            MenuLiteral.Text += "<li class=\"" + IntranetActive + "\">";
            MenuLiteral.Text += "<a href=\"#\" onclick=\"window.open('" + local + "')\" target\"_blank\">";
            MenuLiteral.Text += "<i class=\"fa fa-share-alt fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Intranet</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }


        public void MenuLogistica()
        {

            MenuLiteral.Text += "<li class=\"" + LogisticaActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "listas/lstPedidosLogistica.aspx?indmnu=3\">";
            MenuLiteral.Text += "<i class=\"fa fa-truck fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Logistica</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }





        public void MenuTI()
        {

            MenuLiteral.Text += "<li class=\"" + TIActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "documentos/FrmAdmDocumentos.aspx?indmnu=2\">";
            MenuLiteral.Text += "<i class=\"fa fa-list-ul fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Documentos Cadastrados</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";


            MenuLiteral.Text += "<li class=\"" + TIActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "documentos/FrmAdmAcessoDoc.aspx?indmnu=3\">";
            MenuLiteral.Text += "<i class=\"fa fa-key fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Acessos aos Documentos</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";


            MenuLiteral.Text += "<li class=\"" + TIActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "Banner/frmBanner.aspx?indmnu=3\">";
            MenuLiteral.Text += "<i class=\"fa fa-picture-o fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Banner</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";



        }


        public void MenuApontamentoProcucao()
        {

            MenuLiteral.Text += "<li class=\"" + ApontamentoProducaoActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "apontamento/apontamento.aspx?indmnu=3\">";
            MenuLiteral.Text += "<i class=\"fa fa-hand-o-right fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Apontamento da Produção</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }

        public void MenuConsultas()
        {

            MenuLiteral.Text += "<li class=\"" + ConsultasActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "listas/lstConsulta.aspx?indmnu=7\">";
            MenuLiteral.Text += "<i class=\"fa fa-search fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Consultas</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }


        public void MenuManuais()
        {

            MenuLiteral.Text += "<li class=\"" + ManuaisActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "listas/lstManuais.aspx?indmnu=5\">";
            MenuLiteral.Text += "<i class=\"fa fa-book fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Manuais</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }

        public void MenuDocumentos()
        {

            MenuLiteral.Text += "<li class=\"" + DocumentosActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "documentos/FrmDocumentos.aspx?indmnu=5\">";
            MenuLiteral.Text += "<i class=\"fa fa-cloud-download fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Documentos Download</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }

        public void MenuTabelaPreco()
        {
            MenuLiteral.Text += "<li class=\"" + TabelaPrecoActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "telasRelatorio/FrmRelTabelaExICMS.aspx?indmnu=5\">";
            MenuLiteral.Text += "<i class=\"fa fa-list-alt fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Tabela EX (DOC 50)</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

            MenuLiteral.Text += "<li class=\"" + TabelaPrecoActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "telasRelatorio/FrmRelTabelaManausLocal.aspx?indmnu=5\">";
            MenuLiteral.Text += "<i class=\"fa fa-list-alt fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Tabela Manaus Local</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";


            MenuLiteral.Text += "<li class=\"" + TabelaPrecoActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "telasRelatorio/FrmRelTabelaManausNacional.aspx?indmnu=5\">";
            MenuLiteral.Text += "<i class=\"fa fa-list-alt fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Tabela Manaus Nacional</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";
        }




        public void MenuCalendario()
        {

            MenuLiteral.Text += "<li class=\"" + CalendarioActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "Entidades/FrmCalendario.aspx?indmnu=3\">";
            MenuLiteral.Text += "<i class=\"fa fa-calendar fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Calendario</strong>";
            MenuLiteral.Text += "<span class=\"label label-success pull-right\">Novo</span>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }



        public void MenuLiberarPedidos()
        {

            MenuLiteral.Text += "<li class=\"" + LiberarPedidosActive + "\">";
            //MenuLiteral.Text += "<a href=\"" + prefix + "listas/lstLiberarPedido.aspx?indmnu=3\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "AprovarOrcamento/FrmOrcamento.aspx?indmnu=3\">";
            MenuLiteral.Text += "<i class=\"fa fa-check-square-o fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Liberar Pedidos</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }


        public void MenuAprovarPedidos()
        {

            MenuLiteral.Text += "<li class=\"" + AprovaPedidosActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "listas/lstAprovarPedidos.aspx?indmnu=3\">";
            MenuLiteral.Text += "<i class=\"fa fa-check-square-o fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Aprovar Pedidos</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";


        }

        public void MenuLiberarPedidosLogistica()
        {

            MenuLiteral.Text += "<li class=\"" + LiberarPedidosActive + "\">";
            //MenuLiteral.Text += "<a href=\"" + prefix + "listas/lstLiberarPedido.aspx?indmnu=3\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "AprovarOrcamento/FrmOrcamentoLogistica.aspx?indmnu=3\">";
            MenuLiteral.Text += "<i class=\"fa fa-truck fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Liberar Logistica</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }

        public void MenuControladoria()
        {

            MenuLiteral.Text += "<li class=\"" + LiberarPedidosActive + "\">";
            //MenuLiteral.Text += "<a href=\"" + prefix + "controladoria/HomeControladoriaWebForm.aspx?indmnu=3\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "controladoria/HomeControladoriaWebForm.aspx?indmnu=3\">";
            MenuLiteral.Text += "<i class=\"fa fa-users fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Controladoria</strong>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }


        public void MenuHistorico()
        {

            MenuLiteral.Text += "<li class=\"" + HistoricoActive + "\">";
            MenuLiteral.Text += "<a href=\"" + prefix + "FrmHistoricoPSIU.aspx?indmnu=1\">";
            MenuLiteral.Text += "<i class=\"fa fa-clock-o fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Historico PSIU</strong>";
            //MenuLiteral.Text += "<span class=\"label label-success pull-right\">Novo</span>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }

        public void MenuTrello()
        {

            MenuLiteral.Text += "<li class=\"" + HistoricoActive + "\">";
            MenuLiteral.Text += "<a href=\"https://trello.com\" target=\"_blank\">";
            MenuLiteral.Text += "<i class=\"fa fa-trello fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Projetos</strong>";
            //MenuLiteral.Text += "<span class=\"label label-success pull-right\">Novo</span>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }

        public void MenuPortalCliente()
        {

            string urlLogin = ResolveUrl("~/PortalClienteManuli/HomePortal.aspx");
            MenuLiteral.Text += "<li class=\"" + HistoricoActive + "\">";
            MenuLiteral.Text += "<a href=\"" + urlLogin + "\" >";
            MenuLiteral.Text += "<i class=\"fa fa-line-chart fa-lg\"></i>";
            MenuLiteral.Text += "<span class=\"menu-title\">";
            MenuLiteral.Text += "<strong>Portal Cliente</strong>";
            //MenuLiteral.Text += "<span class=\"label label-success pull-right\">Novo</span>";
            MenuLiteral.Text += "</span>";
            MenuLiteral.Text += "</a>";
            MenuLiteral.Text += "</li>";

        }


    }
}