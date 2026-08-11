using System;
using System.Web.UI;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI.WebControls;

namespace VendasWeb.Logistica_New
{
    public partial class CadastroTransportadorWebForm : System.Web.UI.Page
    {
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass objSessao = new SessionClass();
        LogisticaClass objLogistica = new LogisticaClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                BuscarLinkButton_Click(null, null);
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void NovoLinkButton_Click(object sender, EventArgs e)
        {
            objLogistica.Operacao = "Inclusao";

            Session["CadastroTransportador"] = objLogistica;

            Response.Redirect("~/Logistica_New/CadastroTransportadorDetalheWebForm.aspx?indmnu=5");
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                objLogistica.Filtro = Convert.ToInt32(TransportadorTextBox.Text).ToString();

                objLogistica.TipoFiltro = "Codigo";
            }
            catch
            {
                objLogistica.Filtro = TransportadorTextBox.Text;

                objLogistica.TipoFiltro = "Nome";
            }

            objLogistica.Ativo = Convert.ToInt32(StatusDropDownList.SelectedValue);

            GridView.DataSource = objLogistica.RetornaListaTransportador();

            GridView.DataBind();

            MultiView.Visible = true;
        }

        protected void SelecionarGridViewLinkButton(object sender, EventArgs e)
        {
            objLogistica.Operacao = "Alteracao";

            objLogistica.IDTransportador = Convert.ToInt32(((Label)((Control)sender).FindControl("CodigoGridViewLabel")).Text);

            Session["CadastroTransportador"] = objLogistica;

            Response.Redirect("~/Logistica_New/CadastroTransportadorDetalheWebForm.aspx?indmnu=5");
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;

            BuscarLinkButton_Click(null, null);
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/HomeWebForm.aspx?indmnu=5");
        }
    }
}