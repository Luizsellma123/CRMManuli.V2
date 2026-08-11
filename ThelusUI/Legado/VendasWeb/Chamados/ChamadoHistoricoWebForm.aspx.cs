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
    public partial class ChamadoHistoricoWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        HistoricosClass OBJHistorico = new HistoricosClass();
        UtilClass ObjUtilClass = new UtilClass();
        ClienteClasse OBJCliente = new ClienteClasse();
        ChamadoClass objChamado = new ChamadoClass();
        ParametroGeral objParametroGeral = new ParametroGeral();
        CrmGrupoUsuarioClass GruposUsuario = new CrmGrupoUsuarioClass();
        usuario Objusuario = new usuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (Session["objChamado"] != null)
                objChamado = (ChamadoClass)Session["objChamado"];

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"false\">";

                CarregaCombos();

                CarregaDadosNaTela();

                CarregaHistoricoLiteral();

                TrataBotoesDisponiveis();
            }
        }

        public void CarregaCombos()
        {
            DataTable Resultado = new DataTable();

            Resultado = objChamado.CarregaUsuarios();
            SolicitanteDropDownList.DataSource = Resultado;
            SolicitanteDropDownList.DataValueField = "IDUsuario";
            SolicitanteDropDownList.DataTextField = "Nome";
            SolicitanteDropDownList.DataBind();

            ParametroGeral objParametroGeral = new ParametroGeral();

            //Seta tipo de Historico
            OBJHistorico.IDTipoHistorico = objParametroGeral.RetornaValorNumericoParametro("TIPOPADRAOCHAMADOS");

            EventoHistoricoDropDownList.DataSource = OBJHistorico.RetornaEventos();
            EventoHistoricoDropDownList.DataValueField = "IDEvento";
            EventoHistoricoDropDownList.DataTextField = "Descricao";
            EventoHistoricoDropDownList.DataBind();

            //Carrega Combo Evento Categoria
            EventoHistoricoDropDownList_SelectedIndexChanged(null, null);
        }

        public void CarregaDadosNaTela()
        {
            SolicitanteDropDownList.SelectedValue = objChamado.IDUsuarioSolicitante.ToString();
            NumeroChamadoTextBox.Text = objChamado.NumeroChamado.ToString();
        }

        public void CarregaHistoricoLiteral()
        {
            OBJHistorico.IDChamado = objChamado.NumeroChamado;

            OBJHistorico.RetornaHistoricosChamados();

            HitoricoLiteral.Text = OBJHistorico.Historico;
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Chamados/ChamadoPrincipalWebForm.aspx?indmnu=5");
        }

        protected void EventoHistoricoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            OBJHistorico.IDTipoHistorico = objParametroGeral.RetornaValorNumericoParametro("TIPOPADRAOCHAMADOS");

            OBJHistorico.IDEvento = Convert.ToInt32(EventoHistoricoDropDownList.SelectedValue);

            CategoriaEventoDropDownList.DataSource = OBJHistorico.RetornaEventosCategorias();
            CategoriaEventoDropDownList.DataValueField = "IDCategoria";
            CategoriaEventoDropDownList.DataTextField = "Descricao";
            CategoriaEventoDropDownList.DataBind();
        }

        protected void GravarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            OBJHistorico.IDTipoHistorico = objParametroGeral.RetornaValorNumericoParametro("TIPOPADRAOCHAMADOS");
            OBJHistorico.IDEvento = Convert.ToInt32(EventoHistoricoDropDownList.SelectedValue);
            OBJHistorico.IDCategoria = Convert.ToInt32(CategoriaEventoDropDownList.SelectedValue);
            OBJHistorico.Historico = HistoricoTextBox.Text.Replace("\n"," <br> ");
            OBJHistorico.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
            OBJHistorico.IDChamado = Convert.ToInt32(NumeroChamadoTextBox.Text);

            if (OBJHistorico.Historico == "") erro = "Informe o histórico.";

            if (erro == "") erro = OBJHistorico.GravaHistoricoChamado();

            if (erro == "")
            {
                objChamado = new ChamadoClass();

                objChamado.NumeroChamado = OBJHistorico.IDChamado;

                objChamado.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"]);

                objChamado.Assunto = "Histórico adicionado - " + Session["usuario"].ToString();

                objChamado.descricao = OBJHistorico.Historico;

                erro = objChamado.EnviaEmailAposGravacao();

                CarregaHistoricoLiteral();

                HistoricoTextBox.Text = "";
            }

            ApresentaMensagem(erro);
        }

        public void ApresentaMensagem(string erro = "")
        {
            if (erro == "")
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Operação realizada com sucesso", true);
            else
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
        }

        public void ConsultaGruposUsuarioSuporte()
        {
            //Consulta grupos do usuário logado

            if (Session["usuario"] != null)
                Objusuario.CodigoUsuario = Session["usuario"].ToString();

            //Grupo de Suporte
            GruposUsuario = Objusuario.ConsultaGrupos("Ativo", objParametroGeral.RetornaValorNumericoParametro("GRUPOCHAMADOSSUPORTE"));
        }

        public void TrataBotoesDisponiveis()
        {
            string descricaoStatus = "";

            //Pega a descricao do status
            {
                DataTable Status = objChamado.CarregaStatus();

                foreach (DataRow row in Status.Rows)
                {
                    if (objChamado.IDStatus.ToString() == row["IDStatus"].ToString())
                    {
                        descricaoStatus = row["Descricao"].ToString();

                        break;
                    }
                }
            }

            bool suporte = false;

            //Verifica se o usuário logado esta no grupo de suporte
            {
                ConsultaGruposUsuarioSuporte();

                suporte = (GruposUsuario != null);
            }

            if (descricaoStatus == "Finalizado" && !suporte) GravarButton.Enabled = false;
        }
    }
}