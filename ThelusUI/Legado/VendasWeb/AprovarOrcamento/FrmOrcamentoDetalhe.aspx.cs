using System;
using System.Collections.Generic;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Data;

namespace VendasWeb.AprovarOrcamento
{
    public partial class FrmOrcamentoDetalhe : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        clsOrcamento objOrcamento = new clsOrcamento();
        VendedorClass ObjVendedorClass = new VendedorClass();
        funcoes mdlfuncoes = new funcoes();
        UtilClass ObjUtilClass = new UtilClass();
        ClienteClasse OBJClienteClasse = new ClienteClasse();

        //Instancia classe pedido
        pedido novoPedido = new pedido();

        protected void Page_Load(object sender, EventArgs e)
        {

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

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
                    objOrcamento.Mostra_Liberacoes_Orcamento();
                    CarregaDadosNaTela();
                    Atualiza_Grid();
                }


            }

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

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmOrcamento.aspx?indmnu=2");
        }

        public void CarregaDadosNaTela()
        {
            EmpresaLabel.Text = objOrcamento.EmpCod + " - " + objOrcamento.EmpNome;


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
            LabelValorTotalPedido.Text = objOrcamento.TotalPedido.ToString("R$ #,###.00");
            NatOpLabel.Text = objOrcamento.NaturezaOperacao.ToString();
            CidadeLabel.Text = objOrcamento.Cidade ?? "";
            CondicaoPagamentoLabel.Text = objOrcamento.CondicaoPagamento ?? "";

            //Preenche o campo classificação comercial
            {
                ClienteClasse objClienteClasseAux = new ClienteClasse();
                objClienteClasseAux.CodigoCliente = objOrcamento.EntCod;

               DataTable ClienteDataTable =  objClienteClasseAux.CarregaClassificacaoComercial();

                if(ClienteDataTable.Rows.Count > 0)
                {
                    foreach(DataRow row in ClienteDataTable.Rows)
                    {
                        ClassificacaoComercialLabel.Text = row["Descricao"].ToString();
                    }
                }
            }

            //PrazoMedioLabel.Text = objOrcamento.PrazoMedio ?? "";
            LabelFreteTexto.Text = objOrcamento.PagadorFrete ?? "";
            HistoricoPedidoTextBox.Text = objOrcamento.HistoricoPedido ?? "";
            EnquadramentoTributarioLabel.Text = objOrcamento.EnquadramentoTirbutario ?? "";
            InscricaoEstadualLabel.Text = objOrcamento.InscricaoEstadual ?? "";

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


            HistoricoTextBox.Text = objOrcamento.Historico;

            //Verifica se usuario tem acesso para Aprovar ou reprovar o Orçamento
            if (objOrcamento.Valida_Acesso_Liberacoes_Orcamento() == true)
            {
                AprovarLinkButton.Visible = true;
                ReprovarLinkButton.Visible = true;
                RetornarVendedor.Visible = true;
                NovoHistoricoTextBox.Visible = true;
                NovoHistoricoLabel.Visible = true;

            }

            //Seta Valor Frete
            if (objOrcamento.ValorFrete != 0)
            {
                LabelTotalFrete.Text = objOrcamento.ValorFrete.ToString();
            }

            //Seta Percentual Frete
            if (objOrcamento.PercentualFrete != 0)
            {
                LabelPercentualValorFrete.Text = objOrcamento.PercentualFrete.ToString();
            }

            //Seta transportadora se existir
            if (objOrcamento.NomeTransportador != null && objOrcamento.NomeTransportador != "")
            {
                LabelTextoTransportadora.Text = objOrcamento.NomeTransportador;
            }

            //Seta local de embarque se existir
            if (objOrcamento.LocalEmbarque != null && objOrcamento.LocalEmbarque != "")
            {
                LabelTextoOrigem.Text = objOrcamento.LocalEmbarque;
            }

            //Seta quantidade de volumes se existir
            if (objOrcamento.quantidadeVolumes != null && objOrcamento.quantidadeVolumes != "")
            {
                LabelTextoQuantidadeVolumes.Text = objOrcamento.quantidadeVolumes.ToString();
            }

            //Seta peso bruto se existir
            if (objOrcamento.pesoBruto != 0)
            {
                LabelTextoPesoBruto.Text = objOrcamento.pesoBruto.ToString();
            }

            DataTable OBJDataTable = new DataTable();
            OBJClienteClasse.CodigoCliente = objOrcamento.EntCod.Substring(0, 10);
            OBJDataTable = OBJClienteClasse.RecuperaContaCorrenteClienteSAP();

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    LimiteCreditoLabel.Text = (Convert.ToDecimal(row["LimiteCredito"])).ToString("C");
                }
            }

            OBJDataTable = OBJClienteClasse.LimiteCreditoTomado();

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    if (Convert.ToString(row["limite"]) == "Disponível")
                    {
                        LimiteDisponivelLabel.Text = (Convert.ToDecimal(row["total"])).ToString("C");
                    }
                }
            }
        }

        public void Atualiza_Grid()
        {

            ItemGridView.DataSource = objOrcamento.Consulta_Itens_Orcamento();
            ItemGridView.DataBind();

            MultiView.Visible = true;

        }

        protected void AprovarButton_Click(object sender, EventArgs e)
        {
            string Retorno = "";

            if (Session["objOrcamento"] != null)
            {
                objOrcamento = (clsOrcamento)Session["objOrcamento"];
                objOrcamento.Historico = NovoHistoricoTextBox.Text;
                objOrcamento.AprovadoPrincipal = "Sim";
                objOrcamento.RetornaVendedor = "nao";

                Retorno = objOrcamento.Registra_Operacao_Orcamento();

                if (Retorno != "")
                {
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(Retorno, true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                }
                else
                {
                    novoPedido.NumeroEsbocoSAP = objOrcamento.NumeroEsbocoSAP;
                    novoPedido.codigoEmpresa = objOrcamento.EmpCod;
                    novoPedido.IDPedido = Convert.ToInt32(objOrcamento.PedVendaNum ?? "0");

                    if (novoPedido.NumeroEsbocoSAP == "" || novoPedido.NumeroEsbocoSAP == null || novoPedido.NumeroEsbocoSAP == "0")
                    {
                        if (objOrcamento.EmpCod != "" && objOrcamento.PedVendaNum != "")
                        {
                            novoPedido.carregaDadosPedido(objOrcamento.EmpCod, objOrcamento.PedVendaNum);
                            Retorno = novoPedido.EnviaPedidoSAP();
                        }
                    }

                    if (objOrcamento.AprovadoPrincipal == "Sim")
                    {
                        //Atualiza Histórico do pedido no SAP
                        Retorno = novoPedido.AtualizarHistoricoPedidoSAPAPI();

                        //Transforma esboço em pedido
                        if (Retorno == "")
                        {
                            Retorno = novoPedido.TransformaEsbocoPedido();
                        }

                        if (Retorno != "")
                        {
                            Session["Msg"] = "Pedido, " + objOrcamento.PedVendaNum.ToString() + ", Aprovado com Sucesso! Erro na integração com SAP.";
                            RetornarLinkButton_Click(null, null);
                        }
                        else
                        {
                            Session["Msg"] = "Pedido, " + objOrcamento.PedVendaNum.ToString() + ", Aprovado com Sucesso!";
                            RetornarLinkButton_Click(null, null);
                        }
                    }
                    else
                    {
                        if (Retorno == "")
                        {
                            Session["Msg"] = "Pedido, " + objOrcamento.PedVendaNum.ToString() + ", Aprovado com Sucesso!";
                            RetornarLinkButton_Click(null, null);
                        }
                    }
                }

            }
        }

        protected void ReprovarButton_Click(object sender, EventArgs e)
        {
            string Retorno = "";

            if (Session["objOrcamento"] != null)
            {
                objOrcamento = (clsOrcamento)Session["objOrcamento"];
                objOrcamento.Historico = NovoHistoricoTextBox.Text;
                objOrcamento.AprovadoPrincipal = "Não";
                objOrcamento.RetornaVendedor = "nao";


                Retorno = objOrcamento.Registra_Operacao_Orcamento();

                if (Retorno != "")
                {
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(Retorno, true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                }
                else
                {
                    Session["Msg"] = "Pedido, " + objOrcamento.PedVendaNum.ToString() + ", Reprovado com Sucesso!";
                    RetornarLinkButton_Click(null, null);
                }


            }
        }

        protected void RetornarVendedor_Click(object sender, EventArgs e)
        {
            string Retorno = "";

            if (Session["objOrcamento"] != null)
            {
                objOrcamento = (clsOrcamento)Session["objOrcamento"];
                objOrcamento.Historico = NovoHistoricoTextBox.Text;
                objOrcamento.AprovadoPrincipal = "";
                objOrcamento.RetornaVendedor = "sim";

                Retorno = objOrcamento.Registra_Operacao_Orcamento();

                if (Retorno != "")
                {
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(Retorno, true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                }
                else
                {
                    Session["Msg"] = "Pedido, " + objOrcamento.PedVendaNum.ToString() + ", Enviado para o Vendedor com Sucesso!";
                    RetornarLinkButton_Click(null, null);
                }

            }
        }
    }
}