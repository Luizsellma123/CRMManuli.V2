using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;

namespace VendasWeb.financeiro
{
    public partial class ContaCorrenteDetalheWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        ClienteClasse OBJClienteClasse = new ClienteClasse();

        protected void Page_Load(object sender, EventArgs e)
        {
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = false;
            }

            if (Session["ContaCorrenteDetalhe"] != null)
            {
                OBJClienteClasse = (ClienteClasse)Session["ContaCorrenteDetalhe"];
                CarregaDadosDaSessaoNaTela(sender, e);
            }
            else
            {
                CarregaDadosNaTela(sender, e);
            }

            //if (!IsPostBack)
            //{
            //    CarregaDadosNaTela(sender, e);
            //}

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

        }

        protected void CarregaDadosDaSessaoNaTela(object sender, EventArgs e)
        {
            CodigoTextoLabel.Text = OBJClienteClasse.CodigoAux.ToString();
            NomeTextoLabel.Text = OBJClienteClasse.NomeCliente.ToString();
            CNPJTextoLabel.Text = OBJClienteClasse.CNPJCliente.ToString();
            VendedorTextoLabel.Text = OBJClienteClasse.VendedorCliente.ToString();


            LimiteCreditoTextoLabel.Text = OBJClienteClasse.LimiteCredito.ToString("C");
            limiteDisponivelTextoLabel.Text = OBJClienteClasse.LimiteDisponivel.ToString("C");
            CadastroTextoLabel.Text = OBJClienteClasse.DataCadastroCliente.ToString("dd/MM/yyyy");
            PedidosAbertosTextoLabel.Text = OBJClienteClasse.PedidosAbertos.ToString("C");
            UltimaCompraTextoLabel.Text = OBJClienteClasse.DataUltimaCompraCliente.ToString();
            PedidosFaturadosTextoLabel.Text = OBJClienteClasse.PedidosFaturados.ToString("C");


            AReceberTextoLabel.Text = OBJClienteClasse.ValorAReceber.ToString("C");
            RecebidoTextoLabel.Text = OBJClienteClasse.ValorRecebido.ToString("C");
            MediaAtrasoTextoLabel.Text = OBJClienteClasse.QuantidadeDiasAtraso.ToString();
            MediaFaturamentoTextoLabel.Text = OBJClienteClasse.QuantidadeDiasFaturamento.ToString();


            AReceberCuritibaTextoLabel.Text = OBJClienteClasse.ValorAReceberCuritiba.ToString("C");
            RecebidoCuritibaTextoLabel.Text = OBJClienteClasse.ValorRecebidoCuritiba.ToString("C");
            MediaAtrasoCuritibaTextoLabel.Text = OBJClienteClasse.QuantidadeDiasAtrasoCuritiba.ToString();
            MediaFaturamentoCuritibaTextoLabel.Text = OBJClienteClasse.QuantidadeDiasFaturamentoCuritiba.ToString();


            AReceberManausTextoLabel.Text = OBJClienteClasse.ValorAReceberManaus.ToString("C");
            RecebidoManausTextoLabel.Text = OBJClienteClasse.ValorRecebidoManaus.ToString("C");
            MediaAtrasoManausTextoLabel.Text = OBJClienteClasse.QuantidadeDiasAtrasoManaus.ToString();
            MediaFaturamentoManausTextoLabel.Text = OBJClienteClasse.QuantidadeDiasFaturamentoManaus.ToString();


            APagarTextoLabel.Text = OBJClienteClasse.ValorAPagar.ToString("C");
            APagarPagoTextoLabel.Text = OBJClienteClasse.ValorPago.ToString("C");
            APMediaAtrasoTextoLabel.Text = OBJClienteClasse.QuantidadeDiasAtrasoAP.ToString();
            APMediaFaturamentoTextoLabel.Text = OBJClienteClasse.QuantidadeDiasFaturamentoAP.ToString();


            DevAPagarTextoLabel.Text = OBJClienteClasse.ValorAPagarDev.ToString("C");
            DevPagoTextoLabel.Text = OBJClienteClasse.ValorPagoDev.ToString("C");
        }

        protected void CarregaDadosNaTela(object sender, EventArgs e)
        {
            if (Session["ContaCorrente"] != null)
            {
                OBJClienteClasse = (ClienteClasse)Session["ContaCorrente"];
            }

            CodigoTextoLabel.Text = OBJClienteClasse.CodigoAux.ToString();
            NomeTextoLabel.Text = OBJClienteClasse.NomeCliente.ToString();
            CNPJTextoLabel.Text = OBJClienteClasse.CNPJCliente.ToString();
            VendedorTextoLabel.Text = OBJClienteClasse.VendedorCliente.ToString();

            CarregaDadosGerais(sender, e);
            CarregaContasReceber(sender, e);
            CarregaContasReceberCuritiba(sender, e);
            CarregaContasReceberManaus(sender, e);
            CarregaContasPagar(sender, e);
            CarregaDevolucoes(sender, e);

            Session["ContaCorrenteDetalhe"] = OBJClienteClasse;
        }

        protected void CarregaDadosGerais(object sender, EventArgs e)
        {
            LimiteCreditoTextoLabel.Text = OBJClienteClasse.LimiteCredito.ToString("C");

            //recuperar faturamento Limite Crédito
            DataTable RetornoDados = new DataTable();
            RetornoDados = OBJClienteClasse.LimiteCreditoTomado();

            #region ifs

            if (RetornoDados.Rows.Count > 0)
            {
                foreach (DataRow row in RetornoDados.Rows)
                {
                    if (Convert.ToString(row["limite"]) == "Disponível")
                    {
                        limiteDisponivelTextoLabel.Text = (Convert.ToDecimal(row["total"])).ToString("C");
                        OBJClienteClasse.LimiteDisponivel = Convert.ToDecimal(row["total"]);
                    }
                    else if (OBJClienteClasse.LimiteDisponivel == 0 || limiteDisponivelTextoLabel.Text == null || limiteDisponivelTextoLabel.Text == "")
                    {
                        limiteDisponivelTextoLabel.Text = "0";
                        OBJClienteClasse.LimiteDisponivel = 0;
                    }
                }
            }

            #endregion

            OBJClienteClasse.RecuperaCadastroClienteSAP();
            CadastroTextoLabel.Text = OBJClienteClasse.DataCadastroCliente.ToString("dd/MM/yyyy");

            OBJClienteClasse.RecuperaPedidosAbertosClienteSAP();
            PedidosAbertosTextoLabel.Text = OBJClienteClasse.PedidosAbertos.ToString("C");

            OBJClienteClasse.RecuperaUltimaCompraClienteSAP();
            UltimaCompraTextoLabel.Text = OBJClienteClasse.DataUltimaCompraCliente.ToString();

            OBJClienteClasse.RecuperaPedidosFaturadosClienteSAP();
            PedidosFaturadosTextoLabel.Text = OBJClienteClasse.PedidosFaturados.ToString("C");
        }

        protected void CarregaContasReceber(object sender, EventArgs e)
        {
            OBJClienteClasse.RecuperaCodigoClienteSAP();

            OBJClienteClasse.RecuperaValorAReceberSAP();
            AReceberTextoLabel.Text = OBJClienteClasse.ValorAReceber.ToString("C");

            OBJClienteClasse.RecuperaValorRecebidoSAP();
            RecebidoTextoLabel.Text = OBJClienteClasse.ValorRecebido.ToString("C");

            OBJClienteClasse.RecuperaQuantidadeDiasAtrasoSAP();
            MediaAtrasoTextoLabel.Text = OBJClienteClasse.QuantidadeDiasAtraso.ToString();

            OBJClienteClasse.RecuperaQuantidadeDiasFaturamentoSAP();
            MediaFaturamentoTextoLabel.Text = OBJClienteClasse.QuantidadeDiasFaturamento.ToString();
        }

        protected void CarregaContasReceberCuritiba(object sender, EventArgs e)
        {
            OBJClienteClasse.RecuperaCodigoClienteSAP();

            OBJClienteClasse.RecuperaValorAReceberCuritibaSAP();
            AReceberCuritibaTextoLabel.Text = OBJClienteClasse.ValorAReceberCuritiba.ToString("C");

            OBJClienteClasse.RecuperaValorRecebidoCuritibaSAP();
            RecebidoCuritibaTextoLabel.Text = OBJClienteClasse.ValorRecebidoCuritiba.ToString("C");

            OBJClienteClasse.RecuperaQuantidadeDiasAtrasoCuritibaSAP();
            MediaAtrasoCuritibaTextoLabel.Text = OBJClienteClasse.QuantidadeDiasAtrasoCuritiba.ToString();

            OBJClienteClasse.RecuperaQuantidadeDiasFaturamentoCuritibaSAP();
            MediaFaturamentoCuritibaTextoLabel.Text = OBJClienteClasse.QuantidadeDiasFaturamentoCuritiba.ToString();
        }

        protected void CarregaContasReceberManaus(object sender, EventArgs e)
        {
            OBJClienteClasse.RecuperaCodigoClienteSAP();

            OBJClienteClasse.RecuperaValorAReceberManausSAP();
            AReceberManausTextoLabel.Text = OBJClienteClasse.ValorAReceberManaus.ToString("C");

            OBJClienteClasse.RecuperaValorRecebidoManausSAP();
            RecebidoManausTextoLabel.Text = OBJClienteClasse.ValorRecebidoManaus.ToString("C");

            OBJClienteClasse.RecuperaQuantidadeDiasAtrasoManausSAP();
            MediaAtrasoManausTextoLabel.Text = OBJClienteClasse.QuantidadeDiasAtrasoManaus.ToString();

            OBJClienteClasse.RecuperaQuantidadeDiasFaturamentoManausSAP();
            MediaFaturamentoManausTextoLabel.Text = OBJClienteClasse.QuantidadeDiasFaturamentoManaus.ToString();
        }

        protected void CarregaContasPagar(object sender, EventArgs e)
        {
            OBJClienteClasse.RecuperaCodigoFornecedorSAP();

            OBJClienteClasse.RecuperaValorAPagarSAP();
            APagarTextoLabel.Text = OBJClienteClasse.ValorAPagar.ToString("C");

            OBJClienteClasse.RecuperaValorPagoSAP();
            APagarPagoTextoLabel.Text = OBJClienteClasse.ValorPago.ToString("C");

            OBJClienteClasse.RecuperaQuantidadeDiasAtrasoAPSAP();
            APMediaAtrasoTextoLabel.Text = OBJClienteClasse.QuantidadeDiasAtrasoAP.ToString();

            OBJClienteClasse.RecuperaQuantidadeDiasFaturamentoAPSAP();
            APMediaFaturamentoTextoLabel.Text = OBJClienteClasse.QuantidadeDiasFaturamentoAP.ToString();
        }

        protected void CarregaDevolucoes(object sender, EventArgs e)
        {
            OBJClienteClasse.RecuperaCodigoClienteSAP();

            OBJClienteClasse.RecuperaValorAPagarDevSAP();
            DevAPagarTextoLabel.Text = OBJClienteClasse.ValorAPagarDev.ToString("C");

            OBJClienteClasse.RecuperaValorPagoDevSAP();
            DevPagoTextoLabel.Text = OBJClienteClasse.ValorPagoDev.ToString("C");
        }

        protected void voltarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect((string)Session["ContaCorrenteReturn"]);

            //Response.Redirect("~/financeiro/ContaCorrenteWebForm.aspx?indmnu=5");
        }
    }
}