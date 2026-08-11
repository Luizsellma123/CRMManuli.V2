using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.Email;

namespace VendasWeb.Projetos
{
    public partial class GraficoProjetosWebForm : System.Web.UI.Page
    {
        GraficoProjetosClass OBJGrafico = new GraficoProjetosClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                CarregaDados();
            }
        }

        public void CarregaDados()
        {
            if (Session["GraficoProjetos"] != null)
            {
                ChamadoClass OBJChamado = (ChamadoClass)Session["GraficoProjetos"];

                OBJGrafico.Chamado = OBJChamado.Chamado;
                OBJGrafico.IDStatus = OBJChamado.IDStatus;                                
                OBJGrafico.DataInicial = OBJChamado.DataInicial;
                OBJGrafico.DataFinal = OBJChamado.DataFinal;
                OBJGrafico.IDUsuarioSolicitante = OBJChamado.IDUsuarioSolicitante;
                OBJGrafico.IDUsuarioResponsavel = OBJChamado.IDUsuarioResponsavel;
                OBJGrafico.IDSetor = OBJChamado.IDSetor;
                OBJGrafico.IDPrioridadeProjeto = OBJChamado.IDPrioridadeProjeto;
            }

            TesteLiteral.Text = OBJGrafico.GeraGrafico();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Projetos/ListaProjetosWebForm.aspx?indmnu=5");
        }
    }
}