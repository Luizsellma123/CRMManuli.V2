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
    public partial class CadastroClienteHistoricoWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        HistoricosClass OBJHistorico = new HistoricosClass();
        UtilClass ObjUtilClass = new UtilClass();
        ClienteClasse OBJCliente = new ClienteClasse();

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

            //Carrega Cliente da Session
            if (Session["clienteClasse"] != null)
            {
                //Descarega a session da Entidade
                OBJCliente = (ClienteClasse)Session["clienteClasse"];
            }

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"false\">";

                //Carrega dados
                CarregaDadosNaTela();

            }
        }

        public void CarregaDadosNaTela()
        {
            DataTable RetornoDados = new DataTable();

            //recupera dados principais da tela
            OBJCliente.carregaDadosPrincipais();

            IDCliente.Value = OBJCliente.IDCliente.ToString();

            NomeClienteTextBox.Text = OBJCliente.NomeCliente;

            if (OBJCliente.CodigoCliente != "")
            {
                CodigoClienteTextBox.Text = OBJCliente.CodigoCliente;
            }
            else
            {
                CodigoClienteTextBox.Text = OBJCliente.IDCliente.ToString();
            }

            //Recupera dados de Historico
            OBJHistorico.IDCliente = OBJCliente.IDCliente;
            OBJHistorico.RetornaHistoricosCliente();
            HitoricoLiteral.Text = OBJHistorico.Historico;

            //Seta tipo de Historico
            OBJHistorico.IDTipoHistorico = 1;

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
            Response.Redirect("CadastroClienteWebForm.aspx?indmnu=2");
        }

        protected void EventoHistoricoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaEventoCategoria();
        }

        public void CarregaEventoCategoria()
        {
            DataTable RetornoDados = new DataTable();

            //Carrega Eventos Categoria
            OBJHistorico.IDTipoHistorico = 1;
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

            OBJHistorico.IDEvento = Convert.ToInt32(EventoHistoricoDropDownList.SelectedValue);
            OBJHistorico.IDCategoria = Convert.ToInt32(CategoriaEventoDropDownList.SelectedValue);
            OBJHistorico.Historico = HistoricoTextBox.Text;
            OBJHistorico.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
            OBJHistorico.IDCliente = Convert.ToInt32(IDCliente.Value);

            erro = OBJHistorico.GravaHistoricoCliente();

            if (erro == "")
            {
                //Envia e-mail 
                OBJCliente.EmailDescricaoTipoSolicitacao = EventoHistoricoDropDownList.SelectedItem.Text + " - " + CategoriaEventoDropDownList.SelectedItem.Text;
                OBJCliente.EmailDescricao = HistoricoTextBox.Text;
                OBJCliente.CodigoUsuario = Session["usuario"].ToString();

                //Envia e-mail Vendedor
                OBJCliente.EnviaEmailVendedorHistorico();

                //Envia e-mail para setor responsável
                OBJCliente.IDTipoHistorico = 1;
                OBJCliente.IDEvento = OBJHistorico.IDEvento;
                OBJCliente.IDCategoria = OBJHistorico.IDCategoria;
                OBJCliente.EnviaEmailSetoresHistorico();

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

    }
}