using System;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.GerencialVendas
{
    public partial class ListaClienteSimuladorVendedorForm : System.Web.UI.Page
    {
        UtilClass ObjUtilClass = new UtilClass();
        funcoes mdlfuncoes = new funcoes();
        SimuladorClass simulador = new SimuladorClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
            BuscarButton_Click(null, null);

        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            simulador.EntidadeBusca = ClienteInput.Text;
            simulador.CodigoUsuario = Session["usuario"].ToString();
            EntidadeGridView.DataSource = simulador.Consulta_Entidade_Vendedor();
            EntidadeGridView.DataBind();
            EntidadeMultiView.Visible = true;
        }

        protected void EntidadeGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EntidadeGridView.PageIndex = e.NewPageIndex;

            BuscarButton_Click(null, null);
        }

        protected void AcessarButton_Click(object sender, EventArgs e)
        {
            Session["ClienteSim"] = ((Label)((Control)sender).FindControl("EntidadeGrid")).Text;

            Session["UfSigla"] = ((Label)((Control)sender).FindControl("EstadoGrid")).Text;

            Session["CodigoClienteSimulador"] = ((Label)((Control)sender).FindControl("CodigoClienteGridViewLabel")).Text;
            
            Response.Redirect("FrmSimuladorVendedor.aspx?indmnu=3");
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmSimuladorVendedor.aspx?indmnu=3");
        }
    }
}