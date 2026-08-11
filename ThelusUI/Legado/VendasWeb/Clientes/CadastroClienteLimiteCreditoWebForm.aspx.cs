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
    public partial class CadastroClienteLimiteCreditoWebForm : System.Web.UI.Page
    {
        ClienteClasse OBJCliente = new ClienteClasse();
        GerarGraficoClass OBJGrafico = new GerarGraficoClass();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                //CarregaCombo();

                if (Session["clienteClasse"] != null)
                {
                    //Descarega a session da Entidade
                    OBJCliente = (ClienteClasse)Session["clienteClasse"];

                    //Carrega dados
                    CarregaDadosNaTela();

                    //TrataAcesso();
                }

            }
        }

        public void CarregaDadosNaTela()
        {
            DataTable RetornoDados = new DataTable();
            string TituloLegenda = "Personalizada";
            double totalFaturamento = 0;
            int cont = 0;

            //recuperar faturamento Limite Crédito
            RetornoDados = OBJCliente.LimiteCreditoTomado();
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

                    if (row["limite"].ToString() == "Disponível" && Convert.ToDecimal(row["total"])<=0)
                    {
                        valorLimites[cont] = "0";
                    }else
                    {
                        valorLimites[cont] = Convert.ToDecimal(row["total"]).ToString().Replace(",", ".");
                    }

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

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroClienteWebForm.aspx?indmnu=2");
        }
    }
}
