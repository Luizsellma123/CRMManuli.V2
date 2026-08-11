using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.usercontrol
{
    public partial class userctlMenu : System.Web.UI.UserControl
    {
        funcoes mdlfuncoes = new funcoes();
        string prefix = "";
        string[] strCss;
        
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
                carregamenu(varmenu);            
        }

        public void carregamenu(int varmenu)
        {
            strCss= montaCss(varmenu);
            
            /**********************************************************/
            //Contadores
            /**********************************************************/

            int contTI = 0;
            int contVendas = 0;
            int contLogistica = 0;
            int contAprovarPedidos = 0;
            int contApontamento = 0;
            int contLiberarPedidos = 0;

            /**********************************************************/
            //Fim Contadores
            /**********************************************************/

            if (varmenu > 1)
                prefix = "../";

            if (varmenu == 10)
                prefix = "../../../";

            if (varmenu == 11)
                prefix = "../../";

            ltlMenu.Text = "";
            ltlMenu.Text += "<div id=\"menu\" style=\"z-index:5;\">";
            ltlMenu.Text += "<ul class=\"menuUl\">";
            ltlMenu.Text += "<li><a href=\"" + prefix + "Home.aspx?indmnu=1\" class=\"" + strCss[1] + "\">Home</a></li>";
            ltlMenu.Text += "<li><a href=\"" + prefix + "Entidades/FrmCarteira.aspx?indmnu=5\" class=\"" + strCss[12] + "\">Carteira</a></li>";


            /*************************************************************************************************************************************************************************************************************/
            //Menu de Pedidos
            /*************************************************************************************************************************************************************************************************************/
            //Abaixo Execultado todas as consultas que estaram no Menu Pedido
            contVendas = 1;
            //contVendas = (int)Convert.ToInt32(mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from GRP_X_USUARIO where UsuCod='" + Session["usuario"].ToString() + "' and GrpUsuCod='VENDAS_WEB'", "carregamenu"));
            //contLogistica = (int)Convert.ToInt32(mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from GRP_X_USUARIO where UsuCod='" + Session["usuario"].ToString() + "' and GrpUsuCod='LOGISTICA'", "carregamenu"));
            //contAprovarPedidos = (int)Convert.ToInt32(mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from GRP_X_USUARIO where UsuCod='" + Session["usuario"].ToString() + "' and GrpUsuCod='APROVA_PEDIDO'", "carregamenu"));
            //contLiberarPedidos = (int)Convert.ToInt32(mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from GRP_X_USUARIO where UsuCod='" + Session["usuario"].ToString() + "' and GrpUsuCod='LIBERA_PEDIDO'", "carregamenu"));
            //contTI = (int)Convert.ToInt32(mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from GRP_X_USUARIO where UsuCod='" + Session["usuario"].ToString() + "' and GrpUsuCod='TI'", "carregamenu"));
            //Session["pedidoLiberado"] = (int)Convert.ToInt32(mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from GRP_X_USUARIO where UsuCod='" + Session["usuario"].ToString() + "' and GrpUsuCod='LIBERA_PEDIDO' and GrpUsuSuperv='T'", "carregamenu"));
            Session["pedidoLiberado"] = 0;

            //Abaixo Execultado todas as consultas que estaram no Menu Pedido
            //contApontamento = (int)Convert.ToInt32(mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from GRP_X_USUARIO where UsuCod='" + Session["usuario"].ToString() + "' and GrpUsuCod='PRODUÇÃO(PPCP)'", "carregamenu"));
            contApontamento = 0;



            ltlMenu.Text += "<li><a href=\"" + prefix + "listas/lstManuais.aspx?indmnu=5\" class=\"" + strCss[12] + "\">Manuais</a></li>";

            

            ltlMenu.Text += "<li><a href=\"" + prefix + "\" class=\"" + strCss[21] + "\">Documentos</a></li>";

            


            //Monta cabecalho 
            ltlMenu.Text += " <li> <a href=\"#\">Tabela de Preço</a>";
            ltlMenu.Text += "<ul>";

            //Monta SubMenus
            MenuTabelaPreco();

            //Fim Cabecalho
            ltlMenu.Text += "</ul>";
            ltlMenu.Text += " </li>";


            ltlMenu.Text += "<li><a href=\"" + prefix + "login.aspx?indmnu=0\" class=\"" + strCss[13] + "\">Sair</a></li>";
            /*************************************************************************************************************************************************************************************************************/
            //Fim Menu Gerais
            /*************************************************************************************************************************************************************************************************************/




            //Menu Ti
            if (contTI > 0)
            {

                //Monta cabecalho 
                ltlMenu.Text += " <li> <a href=\"#\">TI</a>";
                ltlMenu.Text += "<ul>";

                //Monta SubMenus
                MenuTI();
                
                //Fim Cabecalho
                ltlMenu.Text += "</ul>";
                ltlMenu.Text += " </li>";

            }


            ltlMenu.Text += "</ul>";
            ltlMenu.Text += "</div>"; 
        }

        public string[] montaCss(int varmenu) 
        { 
            int i=0;
            string[] auxCss= new string[50];

            for(i=0; i<=20 ; i++)
            {                
                if(i == varmenu)
                {
                    auxCss[i] = "menudefault";
                }else
                {
                    auxCss[i] = "";
                }
            }

            return auxCss;
        }

        public void MenuVendas()
        {
           ltlMenu.Text += "<li><a href=\"" + prefix + "listas/FrmListaPedidos.aspx?indmnu=2\" class=\"" + strCss[2] + "\">Consultar Pedidos</a></li>";
        }
       
        public void MenuLogistica()
        {
            ltlMenu.Text += "<li><a href=\"" + prefix + "listas/lstPedidosLogistica.aspx?indmnu=3\" class=\"" + strCss[3] + "\">Logistica</a></li>";
        }

        public void MenuAprovarPedidos()
        {
            ltlMenu.Text += "<li><a href=\"" + prefix + "listas/lstAprovarPedidos.aspx?indmnu=3\" class=\"" + strCss[4] + "\">Aprovar Pedidos</a></li>";
        }
        
        public void MenuApontamentoProcucao()
        {
            ltlMenu.Text += "<li><a href=\"" + prefix + "apontamento/apontamento.aspx?indmnu=3\" class=\"" + strCss[5] + "\">Apontamento da Produção</a></li> ";
        }

        public void MenuLiberarPedidos()
        {
            ltlMenu.Text += "<li><a href=\"" + prefix + "listas/lstLiberarPedido.aspx?indmnu=3\" class=\"" + strCss[6] + "\">Liberar Pedidos</a></li>";
        }

        public void MenuEntidade()
        {
            ltlMenu.Text += "<li><a href=\"" + prefix + "Entidades/FrmListaEntidade.aspx?indmnu=2\" class=\"" + strCss[12] + "\">Consultar</a></li>";
            

        }

        public void MenuTI()
        {
            ltlMenu.Text += "<li><a href=\"" + prefix + "documentos/FrmAdmDocumentos.aspx?indmnu=2\" class=\"" + strCss[12] + "\">Documentos Cadastrados</a></li>";
            ltlMenu.Text += "<li><a href=\"" + prefix + "documentos/FrmAdmAcessoDoc.aspx?indmnu=3\" class=\"" + strCss[13] + "\">Acessos aos Documentos</a></li>";

            ltlMenu.Text += "<li><a href=\"" + prefix + "Banner/frmBanner.aspx?indmnu=3\" class=\"" + strCss[13] + "\">Banner</a></li>";
        }

        public void MenuTabelaPreco()
        {
            ltlMenu.Text += "<li><a href=\"" + prefix + "telasRelatorio/FrmRelTabelaExICMS.aspx?indmnu=5\" class=\"" + strCss[21] + "\">Tabela EX</a></li>";
            ltlMenu.Text += "<li><a href=\"" + prefix + "telasRelatorio/FrmRelTabelaManausLocal.aspx?indmnu=5\" class=\"" + strCss[21] + "\">Tabela Manaus Local</a></li>";
            ltlMenu.Text += "<li><a href=\"" + prefix + "telasRelatorio/FrmRelTabelaManausNacional.aspx?indmnu=5\" class=\"" + strCss[21] + "\">Tabela Manaus Nacional</a></li>";

        }

    }
}