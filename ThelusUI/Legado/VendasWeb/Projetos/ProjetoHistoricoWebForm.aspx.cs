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
    public partial class ProjetoHistoricoWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        HistoricosClass OBJHistorico = new HistoricosClass();
        UtilClass ObjUtilClass = new UtilClass();
        ClienteClasse OBJCliente = new ClienteClasse();
        ChamadoClass OBJChamado = new ChamadoClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            //Carrega Historico da Session
            if (Session["SOBJHistorico"] != null)
            {
                //Descarega a session da Entidade
                OBJHistorico = (HistoricosClass)Session["SOBJHistorico"];
            }

            //Carrega Chamado da Session
            if (Session["OBJChamado"] != null)
            {
                //Descarega a session Financeiro
                OBJChamado = (ChamadoClass)Session["OBJChamado"];
            }

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"false\">";

                //Carrega Combo
                CarregaCombos();

                //Carrega dados
                CarregaDadosNaTela();

            }
        }

        public void CarregaDadosNaTela()
        {
            DataTable RetornoDados = new DataTable();

            //recupera dados principais da tela
            OBJChamado.RecuperaDadosPrincipais();

            SolicitanteDropDownList.SelectedValue = OBJChamado.IDUsuarioSolicitante.ToString();
            NumeroChamadoTextBox.Text = OBJChamado.NumeroChamado.ToString();

            //Recupera dados de Historico
            OBJHistorico.IDChamado = OBJChamado.NumeroChamado;
            OBJHistorico.RetornaHistoricosChamados();
            HitoricoLiteral.Text = OBJHistorico.Historico;

            //Seta tipo de Historico
            OBJHistorico.IDTipoHistorico = 2;

            //Carrega Eventos
            RetornoDados = OBJHistorico.RetornaEventos();
            EventoHistoricoDropDownList.DataSource = RetornoDados;
            EventoHistoricoDropDownList.DataValueField = "IDEvento";
            EventoHistoricoDropDownList.DataTextField = "Descricao";
            EventoHistoricoDropDownList.DataBind();

            //Carrega Combo Evento Categoria
            CarregaEventoCategoria();

            Session["SOBJHistorico"] = OBJHistorico;

        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Projetos/ListaProjetosWebForm.aspx?indmnu=5");
        }

        protected void EventoHistoricoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaEventoCategoria();
        }

        public void CarregaEventoCategoria()
        {
            DataTable RetornoDados = new DataTable();

            //Carrega Eventos Categoria
            OBJHistorico.IDTipoHistorico = 2;
            OBJHistorico.IDEvento = Convert.ToInt32(EventoHistoricoDropDownList.SelectedValue);
            RetornoDados = OBJHistorico.RetornaEventosCategorias();
            CategoriaEventoDropDownList.DataSource = RetornoDados;
            CategoriaEventoDropDownList.DataValueField = "IDCategoria";
            CategoriaEventoDropDownList.DataTextField = "Descricao";
            CategoriaEventoDropDownList.DataBind();
        }

        protected void GravarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            OBJChamado.Evento = Convert.ToInt32(EventoHistoricoDropDownList.SelectedValue);
            OBJChamado.Categoria = Convert.ToInt32(CategoriaEventoDropDownList.SelectedValue);
            OBJChamado.Historico = HistoricoTextBox.Text;
            OBJChamado.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"]);

            erro = OBJChamado.GravaHistorico();
            
            if (erro == "")
            {
                HistoricoTextBox.Text = "";
                CarregaDadosNaTela();
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        public void CarregaCombos()
        {
            DataTable Resultado = new DataTable();

            Resultado = OBJChamado.CarregaUsuarios();
            SolicitanteDropDownList.DataSource = Resultado;
            SolicitanteDropDownList.DataValueField = "IDUsuario";
            SolicitanteDropDownList.DataTextField = "Nome";
            SolicitanteDropDownList.DataBind();

            //Resultado = OBJChamado.CarregaPrioridadesProjeto();
            //PrioridadeProjetoDropDownList.DataSource = Resultado;
            //PrioridadeProjetoDropDownList.DataValueField = "IDPrioridadeProjeto";
            //PrioridadeProjetoDropDownList.DataTextField = "Descricao";
            //PrioridadeProjetoDropDownList.DataBind();

            //Resultado = OBJChamado.CarregaHorasDesenvolvimentoProjeto();
            //HorasDesenvolvimentoDropDownList.DataSource = Resultado;
            //HorasDesenvolvimentoDropDownList.DataValueField = "IDHoras";
            //HorasDesenvolvimentoDropDownList.DataTextField = "Descricao";
            //HorasDesenvolvimentoDropDownList.DataBind();

        }

    }
}