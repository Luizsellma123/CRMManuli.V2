using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.AprovarOrcamento
{
    public partial class frmOrcamentoDetalheLogistica : System.Web.UI.Page
    {
        clsOrcamento objOrcamento = new clsOrcamento();
        VendedorClass ObjVendedorClass = new VendedorClass();
        funcoes mdlfuncoes = new funcoes();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {

                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }

            if (!IsPostBack)
            {

                //Valida Acesso
                OBJSessao.ValidaAcesso();

                /*Tratar Abrir e fechar Div*/
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";


                if (Session["objOrcamento"] != null)
                {
                    objOrcamento = (clsOrcamento)Session["objOrcamento"];
                    objOrcamento.UsuCod = Session["usuario"].ToString();
                    objOrcamento.Mostra_Liberacoes_Orcamento_Logistica();
                    CarregaDadosNaTela();
                    Atualiza_Grid();
                }
            }
        }

        public void CarregaDadosNaTela()
        {
            DataTable Resultado = new DataTable();
            EmpresaLabel.Text = objOrcamento.EmpCod + " - " + objOrcamento.EmpNome;
            HiddenFieldEmpCod.Value = objOrcamento.EmpCod;

            if (objOrcamento.Situacao == "Cancelado")
            {
                SituacaoLabel.Text = "Não Aprovado";
            }
            else
            {
                SituacaoLabel.Text = objOrcamento.Situacao;
            }
            StatusLabel.Text = objOrcamento.PedVendaStatDescr;
            EntidadeLabel.Text = objOrcamento.EntCod + " - " + objOrcamento.EntNome;
            NaturezaLabel.Text = objOrcamento.EntNat;
            PreisaoLabel.Text = objOrcamento.DataPrevisao;
            EstadoLabel.Text = objOrcamento.UfSigla;
            AprovacaoLabel.Text = objOrcamento.AprovadoPrincipal;
            AlcadaPrincipalLabel.Text = objOrcamento.AlcadaPrincipal;
            ConcluidoLabel.Text = objOrcamento.Concluido;
            VendedorLabel.Text = objOrcamento.VendCod + " - " + objOrcamento.VendNome;
            PedVendaNumLabel.Text = objOrcamento.PedVendaNum;
            LabelTextoLogistica.Text = objOrcamento.StatusLogisitica;
            LabelTextoTotal.Text = objOrcamento.TotalPedido.ToString("R$ #,###.00");
            HiddenFieldTotal.Value = objOrcamento.TotalPedido.ToString();
            LabelPagadorFrete.Text = objOrcamento.PagadorFrete;


            if (objOrcamento.AprovadoSupervisor == "Sim")
            {
                AlcadaSupervisorCheckBox.Checked = true;
            }

            /*
            if (objOrcamento.AprovadoRegional == "Sim")
            {
                AlcadaRegionalCheckBox.Checked = true;
            }
             */

            if (objOrcamento.AprovadoControladoria == "Sim")
            {
                AlcadaControladoriaCheckBox.Checked = true;
            }

            /*
            if (objOrcamento.AprovadoDiretoria == "Sim")
            {
                AlcadaDiretoriaCheckBox.Checked = true;
            }
             */

            Resultado = objOrcamento.Consulta_Transportadoras();

            transportadoraSelect.DataSource = Resultado;
            transportadoraSelect.DataTextField = "EntNome";
            transportadoraSelect.DataValueField = "EntCod";
            transportadoraSelect.DataBind();

            //Seta transportadora se existir
            if (objOrcamento.transportadora != null && objOrcamento.transportadora != "")
            {
                transportadoraSelect.Items.FindByValue(objOrcamento.transportadora.ToString()).Selected = true;
            }

            //Seta local de embarque se existir
            if (objOrcamento.LocalEmbarque != null && objOrcamento.LocalEmbarque != "")
            {
                RadioButtonListLocalEmbarque.Items.FindByValue(objOrcamento.LocalEmbarque.ToString()).Selected = true;
            }

            //Seta quantidade de volumes se existir
            if (objOrcamento.quantidadeVolumes != null && objOrcamento.quantidadeVolumes != "")
            {
                textoQuantidadeProdutos.Text = objOrcamento.quantidadeVolumes.ToString();
            }

            //Seta peso bruto se existir
            if (objOrcamento.pesoBruto != 0)
            {
                TextBoxPesoBruto.Text = objOrcamento.pesoBruto.ToString();
            }

            //Seta Valor Frete
            if (objOrcamento.ValorFrete != 0)
            {
                TextBoxValorFrete.Text = objOrcamento.ValorFrete.ToString();
            }

            //Seta Percentual Frete
            if (objOrcamento.PercentualFrete != 0)
            {
                TextBoxPercentualFrete.Text = objOrcamento.PercentualFrete.ToString();
            }
        }

        public void Atualiza_Grid()
        {

            ItemGridView.DataSource = objOrcamento.Consulta_Itens_Orcamento();
            ItemGridView.DataBind();

            MultiView.Visible = true;

        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmOrcamentoLogistica.aspx?indmnu=2");
        }

        protected void ItemGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            ItemGridView.PageIndex = e.NewPageIndex;

            if (Session["objOrcamento"] != null)
            {
                objOrcamento = (clsOrcamento)Session["objOrcamento"];
            }

            Atualiza_Grid();
        }

        protected void AprovarLinkButton_Click(object sender, EventArgs e)
        {
            string Retorno = "";
            string transportadora;
            string quantidadeVolumes;
            decimal pesoBruto;
            string empresa;
            string pedido;
            string erro;
            string LocalEmbarque;


            if (Session["objOrcamento"] != null)
            {
                objOrcamento = (clsOrcamento)Session["objOrcamento"];
                objOrcamento.Historico = NovoHistoricoTextBox.Text;
                objOrcamento.AprovadoPrincipal = "Sim";
                objOrcamento.RetornaVendedor = "nao";
                objOrcamento.ValorFrete = Convert.ToDecimal(TextBoxValorFrete.Text.ToString());
                objOrcamento.PercentualFrete = Convert.ToDecimal(TextBoxPercentualFrete.Text.ToString());

                transportadora = transportadoraSelect.Items[transportadoraSelect.SelectedIndex].Value;
                quantidadeVolumes = textoQuantidadeProdutos.Text.ToString();

                if (TextBoxPesoBruto.Text.ToString() == "" || TextBoxPesoBruto.Text.ToString() == null)
                {
                    pesoBruto = 0;
                }
                else
                {
                    pesoBruto = Convert.ToDecimal(TextBoxPesoBruto.Text.ToString());
                }

                empresa = HiddenFieldEmpCod.Value.ToString();
                pedido = PedVendaNumLabel.Text.ToString();
                LocalEmbarque = RadioButtonListLocalEmbarque.SelectedValue.Trim();

                objOrcamento.EmpCod = empresa;
                objOrcamento.PedVendaNum = pedido;
                objOrcamento.pesoBruto = pesoBruto;
                objOrcamento.quantidadeVolumes = quantidadeVolumes;
                objOrcamento.transportadora = transportadora;
                objOrcamento.LocalEmbarque = LocalEmbarque;
                objOrcamento.UsuCod = Session["usuario"].ToString();

                Retorno = objOrcamento.Atualiza_Fretes_Logistica();

                if (Retorno != "")
                {
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(Retorno, true);
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                }
                else
                {
                    Session["Msg"] = "Pedido, " + objOrcamento.PedVendaNum.ToString() + ", Aprovado com Sucesso!";
                    RetornarLinkButton_Click(null, null);
                }

            }
        }

        protected void LinkButtonSolicitarCotacao_Click(object sender, EventArgs e)
        {
            string transportadora;
            string quantidadeVolumes;
            decimal pesoBruto;
            string empresa;
            string pedido;
            string erro;
            string LocalEmbarque;

            transportadora = transportadoraSelect.Items[transportadoraSelect.SelectedIndex].Value;
            quantidadeVolumes = textoQuantidadeProdutos.Text.ToString();
            pesoBruto = Convert.ToDecimal(TextBoxPesoBruto.Text.ToString());
            empresa = HiddenFieldEmpCod.Value.ToString();
            pedido = PedVendaNumLabel.Text.ToString();
            LocalEmbarque = RadioButtonListLocalEmbarque.SelectedValue.Trim();

            objOrcamento.EmpCod = empresa;
            objOrcamento.PedVendaNum = pedido;
            objOrcamento.pesoBruto = pesoBruto;
            objOrcamento.quantidadeVolumes = quantidadeVolumes;
            objOrcamento.transportadora = transportadora;
            objOrcamento.LocalEmbarque = LocalEmbarque;
            objOrcamento.UsuCod = Session["usuario"].ToString();

            erro = objOrcamento.Gera_Cotacao_Logistica();

            if (erro != "")
            {
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

            }
        }

        protected void TextBoxValorFrete_TextChanged(object sender, EventArgs e)
        {
            TextBoxPercentualFrete.Text = Math.Round(((Convert.ToDecimal(TextBoxValorFrete.Text) / Convert.ToDecimal(HiddenFieldTotal.Value)) * 100),2).ToString("#0.00");
        }

        protected void TextBoxPercentualFrete_TextChanged(object sender, EventArgs e)
        {
            TextBoxValorFrete.Text = Math.Round((Convert.ToDecimal(HiddenFieldTotal.Value) * (Convert.ToDecimal(TextBoxPercentualFrete.Text)/100)),2).ToString("#0.00");
        }
    }
}