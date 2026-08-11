using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.PortalClienteManuli
{
    public partial class HomePortal : System.Web.UI.Page
    {
        GerarGraficoClass OBJGrafico = new GerarGraficoClass();
        PortalClass OBJPortal = new PortalClass();
        UsuarioPortalClass OBJusuario = new UsuarioPortalClass();
        PedidoClass PedidoClass = new PedidoClass();

        protected void Page_Load(object sender, EventArgs e)
        {

            //Verifica se tem usuário logado no Portal
            if (Session["usuarioPortal"] == null)
            {
                //Redireciona para tela de login
                Response.Redirect("LoginPortal.aspx");
            }

            if (!IsPostBack)
            {
                //Carrega Dados na Tela
                carregaDadosTela();

                //Chama função para gerar gráfico faturamento
                GerarGraficoFaturamento();

                //Recupera dados pedidos
                carregaPedidos();
            }
        }

        public void GerarGraficoFaturamento()
        {
            OBJusuario = (UsuarioPortalClass)Session["usuarioPortal"];

            DataTable RetornoDados = new DataTable();
            string TituloLegenda = "Personalizada";
            double totalFaturamento = 0;
            int cont = 0;

            //recuperar faturamento da entidade
            OBJPortal.Entcod = OBJusuario.EntCod;
            RetornoDados = OBJPortal.FaturamentoEntidade();
            if (RetornoDados.Rows.Count > 0)
            {
                string[] linhas = new string[RetornoDados.Rows.Count];
                string[] valor = new string[RetornoDados.Rows.Count];
                string[] background = new string[3];
                foreach (DataRow row in RetornoDados.Rows)
                {
                    linhas[cont] = row["LinhaProduto"].ToString();
                    valor[cont] = row["total"].ToString().Replace(",", ".");

                    totalFaturamento += Convert.ToDouble(row["total"]);

                    cont++;
                }

                background[0] = "'#3da5f4'";
                background[1] = "'#f1536e'";
                background[2] = "'#fda006'";

                OBJGrafico.NomeVariaveis = linhas;
                OBJGrafico.TotalFaturamento = totalFaturamento.ToString("C");
                OBJGrafico.incluiDataSetFaturamento(valor, TituloLegenda, background);

                OBJGrafico.GraficoFaturamento();
                LiteralGraficoFaturamento.Text = OBJGrafico.grafico.ToString();
            }

            //recuperar faturamento mes a mes
            RetornoDados = OBJPortal.FaturamentoMesAMes();
            totalFaturamento = 0;

            if (RetornoDados.Rows.Count > 0)
            {
                string[] meses = new string[RetornoDados.Rows.Count];
                string[] valorMeses = new string[RetornoDados.Rows.Count];
                cont = 0;

                foreach (DataRow row in RetornoDados.Rows)
                {
                    meses[cont] = row["MesAno"].ToString();
                    valorMeses[cont] = row["total"].ToString().Replace(",", ".");

                    totalFaturamento += Convert.ToDouble(row["total"]);

                    cont++;
                }

                //Seta total de faturamento
                LabelTotalFaturamento.Text = totalFaturamento.ToString("C");

                OBJGrafico.NomeVariaveis = meses;
                OBJGrafico.ValoresVariaveis = valorMeses;
                OBJGrafico.GraficoFaturamentoAnual();
                LiteralGraficoFaturamentoAnual.Text = OBJGrafico.grafico.ToString();
            }

            //recuperar faturamento Limite Crédito
            RetornoDados = OBJPortal.LimiteCredito();
            totalFaturamento = 0;

            if (RetornoDados.Rows.Count > 0)
            {
                string[] limites = new string[RetornoDados.Rows.Count];
                string[] valorLimites = new string[RetornoDados.Rows.Count];
                string[] background = new string[2];
                cont = 0;

                foreach (DataRow row in RetornoDados.Rows)
                {
                    limites[cont] = row["limite"].ToString();
                    valorLimites[cont] = row["total"].ToString().Replace(",", ".");
                    totalFaturamento = Convert.ToDouble(row["EntValLimCred"]);

                    cont++;
                }

                background[0] = "'#3da5f4'";
                background[1] = "'#f1536e'";

                //Verifica se existem dados para serem limpos
                if (OBJGrafico.itemDataFaturamentoSetList != null)
                {
                    OBJGrafico.itemDataFaturamentoSetList.Clear();
                }
                OBJGrafico.NomeVariaveis = limites;
                OBJGrafico.TotalFaturamento = totalFaturamento.ToString("C");
                OBJGrafico.incluiDataSetFaturamento(valorLimites, TituloLegenda, background);

                OBJGrafico.GraficoLimiteCredito();
                LiteralGraficoLimiteCredito.Text = OBJGrafico.grafico.ToString();
            }

        }

        public void carregaPedidos()
        {
            OBJusuario = (UsuarioPortalClass)Session["usuarioPortal"];

            DataTable RetornoDados = new DataTable();

            //recupera pedidos pendentes
            OBJPortal.Entcod = OBJusuario.EntCod;
            RetornoDados = OBJPortal.PedidosPendentes();

            GridViewPedidosPendentes.DataSource = RetornoDados;
            GridViewPedidosPendentes.DataBind();
        }

        protected void GridViewPedidosPendentes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPedidosPendentes.PageIndex = e.NewPageIndex;
            carregaPedidos();
        }

        protected void PendentesLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pedidos/ListaPedidosWebForm.aspx");
        }

        protected void LinkButtonConsulta_Click(object sender, EventArgs e)
        {
            PedidoClass = new PedidoClass();
            PedidoClass.PedVendaNum = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;
            PedidoClass.EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            PedidoClass.Consulta_Pedido();
            PedidoClass.PedVendaStatDescr = ((Label)((Control)sender).FindControl("pedvendastatdescrLabel")).Text;
            Session["PedidoClass"] = PedidoClass;

            Response.Redirect("~/PortalClienteManuli/Pedidos/DetalhesPedidosWebForm.aspx");
        }

        public void carregaDadosTela()
        {
            //Recupera usuario da sessão
            OBJusuario = (UsuarioPortalClass)Session["usuarioPortal"];

            //Carrega Razão Social Cliente
            RazaoSocialDropDownList.DataSource = OBJusuario.Entidades_Usuario();
            RazaoSocialDropDownList.DataTextField = "EntNome";
            RazaoSocialDropDownList.DataValueField = "EntCod";
            RazaoSocialDropDownList.DataBind();
        }
    }
}
