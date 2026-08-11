using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Chamados
{
    public partial class ListaProjetosWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        ChamadoClass OBJChamado = new ChamadoClass();
        usuario OBJUsuario = new usuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                CarregaDatas();

                //Carrega vendedores conforme autorização
                CarregaCombos();

                //Carrega dados na tela conforme filtros
                CarregaDadosGrid();

                //Trata acessos
                TrataAcessos();
            }
        }

        protected void CarregaDatas()
        {
            DateTime hoje = DateTime.Today;

            DateTime primeiroDiaDoAno = new DateTime(hoje.Year, 1, 1);

            DataInicialTextBox.Text = primeiroDiaDoAno.ToString("yyyy-MM-dd");

            DataFinalTextBox.Text = hoje.ToString("yyyy-MM-dd");
        }

        public void TrataAcessos()
        {
            usuario ObjusuarioAux = new usuario();
            ObjusuarioAux.CodigoUsuario = Session["usuario"].ToString();
            ObjusuarioAux.ConsultaGrupos("Ativo");

            //Somente libera botão recalculo se usuario for ADM do grupo
            if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 11).Count() <= 0)
            {
                RecalcularDatasLinkButton.Visible = false;
            }
            else
            {
                RecalcularDatasLinkButton.Visible = true;
            }
        }

        protected void ChamadosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ChamadosGridView.PageIndex = e.NewPageIndex;
            CarregaDadosGrid();
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosGrid();
        }

        public void CarregaDadosGrid()
        {
            OBJChamado.Chamado = ChamadoTextBox.Text;
            OBJChamado.IDStatus = Convert.ToInt32(StatusDropDownList.SelectedValue);
            OBJChamado.IDUsuarioSolicitante = Convert.ToInt32(SolicitanteDropDownList.SelectedValue);
            OBJChamado.IDUsuarioResponsavel = Convert.ToInt32(ResponsavelDropDownList.SelectedValue);
            OBJChamado.IDSetor = Convert.ToInt32(SetorDropDownList.SelectedValue);
            OBJChamado.IDPrioridadeProjeto = Convert.ToInt32(PrioridadeDropDownList.SelectedValue);
            OBJChamado.Ordenacao = Convert.ToString(OrdenacaoDropDownList.SelectedValue) + " " + Convert.ToString(TipoOdenacaoDropDownList.SelectedValue);

            //Se data não for preenchida pega a primeira data do sistema
            if (DataInicialTextBox.Text == "")
            {
                OBJChamado.DataInicial = Convert.ToDateTime("01-01-1900");
            }
            else
            {
                OBJChamado.DataInicial = Convert.ToDateTime(DataInicialTextBox.Text);
            }

            //Se data final não for preenchida pega data atual
            if (DataFinalTextBox.Text == "")
            {
                OBJChamado.DataFinal = DateTime.Now;
            }
            else
            {
                OBJChamado.DataFinal = Convert.ToDateTime(DataFinalTextBox.Text);
            }

            ChamadosGridView.DataSource = OBJChamado.CarregaListaChamadosProjetos();
            ChamadosGridView.DataBind();
            ChamadosMultiView.Visible = true;
        }

        public void CarregaCombos()
        {
            DataTable Resultado = new DataTable();

            Resultado = OBJChamado.CarregaUsuarios();
            SolicitanteDropDownList.DataSource = Resultado;
            SolicitanteDropDownList.DataValueField = "IDUsuario";
            SolicitanteDropDownList.DataTextField = "Nome";
            SolicitanteDropDownList.DataBind();
            SolicitanteDropDownList.Items.Insert(0, new ListItem("Todos", "0"));


            Resultado = OBJChamado.CarregaUsuariosSuporte();
            ResponsavelDropDownList.DataSource = Resultado;
            ResponsavelDropDownList.DataValueField = "IDUsuario";
            ResponsavelDropDownList.DataTextField = "CodigoUsuario";
            ResponsavelDropDownList.DataBind();
            ResponsavelDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

            Resultado = OBJUsuario.Consulta_Setores();
            SetorDropDownList.DataSource = Resultado;
            SetorDropDownList.DataValueField = "IDSetor";
            SetorDropDownList.DataTextField = "Descricao";
            SetorDropDownList.DataBind();
            SetorDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

            Resultado = OBJChamado.CarregaStatus();
            StatusDropDownList.DataSource = Resultado;
            StatusDropDownList.DataValueField = "IDStatus";
            StatusDropDownList.DataTextField = "Descricao";
            StatusDropDownList.DataBind();
            StatusDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

            //Resultado = OBJChamado.CarregaClassificacoes();
            //ClassificacaoDropDownList.DataSource = Resultado;
            //ClassificacaoDropDownList.DataValueField = "IDClassificacao";
            //ClassificacaoDropDownList.DataTextField = "Descricao";
            //ClassificacaoDropDownList.DataBind();

            Resultado = OBJChamado.CarregaPrioridadesProjeto();
            PrioridadeDropDownList.DataSource = Resultado;
            PrioridadeDropDownList.DataValueField = "IDPrioridadeProjeto";
            PrioridadeDropDownList.DataTextField = "Descricao";
            PrioridadeDropDownList.DataBind();
            PrioridadeDropDownList.Items.Insert(0, new ListItem("Todas", "0"));
        }

        protected void AcessarLinkButton_Click(object sender, EventArgs e)
        {
            OBJChamado.NumeroChamado = Convert.ToInt32(((Label)((Control)sender).FindControl("IDChamadoLabel")).Text ?? "0");
            Session["OBJChamado"] = OBJChamado;

            Response.Redirect("~/Projetos/ProjetoHistoricoWebForm.aspx?indmnu=5");
        }

        protected void RecalcularDatasLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            OBJChamado.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"]);
            OBJChamado.DataRecalculoPrevisao = DateTime.Now;
            erro = OBJChamado.GravaRecalculoProjeto();

            if (erro == "")
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Recalculo efetuado com sucesso!", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                CarregaDadosGrid();
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void GraficoLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosGrid();

            Session["GraficoProjetos"] = OBJChamado;

            Response.Redirect("~/Projetos/GraficoProjetosWebForm.aspx?indmnu=5");
        }

        protected void AnexoLinkButton_Click(object sender, EventArgs e)
        {
            OBJChamado.NumeroChamado = Convert.ToInt32(((Label)((Control)sender).FindControl("IDChamadoLabel")).Text ?? "0");
            Session["OBJChamado"] = OBJChamado;

            Response.Redirect("~/Projetos/ProjetoAnexosWebForm.aspx?indmnu=5");
        }
    }
}