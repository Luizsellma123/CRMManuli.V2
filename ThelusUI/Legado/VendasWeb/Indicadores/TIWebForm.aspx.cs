using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;

namespace VendasWeb.Indicadores
{
    public partial class TIWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        IndicadoresClass objIndicadores = new IndicadoresClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
                BuscarLinkButton_Click(null, null);
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            DateTime primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            DataInicialTextBox.Text = primeiroDiaMes.ToString("yyyy-MM-dd");
            DataFinalTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

            CarregaCombos();
        }

        protected void CarregaCombos()
        {
            ChamadoClass objChamado = new ChamadoClass();

            SolicitanteDropDownList.DataSource = objChamado.CarregaUsuarios();
            SolicitanteDropDownList.DataValueField = "IDUsuario";
            SolicitanteDropDownList.DataTextField = "Nome";
            SolicitanteDropDownList.DataBind();
            SolicitanteDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

            ResponsavelDropDownList.DataSource = objChamado.CarregaUsuariosSuporte();
            ResponsavelDropDownList.DataValueField = "IDUsuario";
            ResponsavelDropDownList.DataTextField = "CodigoUsuario";
            ResponsavelDropDownList.DataBind();
            ResponsavelDropDownList.Items.Insert(0, new ListItem("Todos", "0"));

            SistemaDropDownList.DataSource = objChamado.CarregaSistemas();
            SistemaDropDownList.DataValueField = "IDSistema";
            SistemaDropDownList.DataTextField = "Descricao";
            SistemaDropDownList.DataBind();
            SistemaDropDownList.Items.Insert(0, new ListItem("Todos", ""));
        }

        protected void CarregaDadosDaTela()
        {
            objIndicadores.IDUsuarioResponsavel = Convert.ToInt32(ResponsavelDropDownList.SelectedValue);
            objIndicadores.IDUsuarioSolicitante = Convert.ToInt32(SolicitanteDropDownList.SelectedValue);
            objIndicadores.DataInicial = Convert.ToDateTime(DataInicialTextBox.Text).ToString("yyyy-MM-dd");
            objIndicadores.DataFinal = Convert.ToDateTime(DataFinalTextBox.Text).ToString("yyyy-MM-dd");
            objIndicadores.Sistema = SistemaDropDownList.SelectedItem.Text == "Todos" ? "" : SistemaDropDownList.SelectedItem.Text;
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();

            GridView.DataSource = objIndicadores.RetornaListaIndicadoresTI();
            GridView.DataBind();
            MultiView.Visible = true;
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            CarregaDadosNaTela();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Indicadores/HomeWebForm.aspx?indmnu=5");
        }

        protected void RelatorioPDFLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                CarregaDadosDaTela();

                RelatorioCrystalClass objRelatorioCrystalClass = new RelatorioCrystalClass();

                objRelatorioCrystalClass.GeraRelatorioIndicadoresTI(objIndicadores.IDUsuarioResponsavel,
                      objIndicadores.IDUsuarioSolicitante,
                      objIndicadores.DataInicial, objIndicadores.DataFinal,
                      objIndicadores.Sistema, "IndicadoresTI");
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
        }
    }
}