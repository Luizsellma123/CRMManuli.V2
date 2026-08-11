using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.financeiro
{
    public partial class LiberacaoPedidosDetalheWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        FinanceiroClass OBJFinanceiro = new FinanceiroClass();
        UtilClass ObjUtilClass = new UtilClass();
        enviarEmail OBJMail = new enviarEmail();
        pedido OBJPedido = new pedido();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }

            if (Session["OBJFinanceiro"] != null)
            {
                OBJFinanceiro = (FinanceiroClass)Session["OBJFinanceiro"];
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
                Atualiza_Grid();
            }

        }

        public void Atualiza_Grid()
        {
            DataTable OBJDataTable = new DataTable();
            OBJDataTable = OBJFinanceiro.RecuperaAutorizacoesEsbocoSAP();
            AprovacoesGridView.DataSource = OBJDataTable;
            AprovacoesGridView.DataBind();
            AprovacoesMultiView.Visible = true;
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            //Redireciona para tela de login
            Response.Redirect("LiberacaoPedidosWebForm.aspx?indmnu=2");
        }

        protected void AprovarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";
            Boolean PrimeiraAprovacao = false;

            //Recupera usuário do SAP para aprovação
            OBJFinanceiro.IDUsuarioCRM = Convert.ToInt32(Session["IDUsuario"]);
            OBJFinanceiro.RetornaUsuarioSenhaSAP();

            //Chama funação para efetuar aprovação
            //OBJFinanceiro.Historico = HistoricoTextBox.Text;
            OBJFinanceiro.HistoricoDetalhado = HistoricoTextBox.Text;
            OBJFinanceiro.Historico = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - " + MotivoDropDownList.SelectedItem.Text.ToString();
            OBJFinanceiro.IDMotivo = Convert.ToInt32(MotivoDropDownList.SelectedValue);
            OBJFinanceiro.AnalisePedido = "Aprovado";
            //erro = OBJFinanceiro.AtualizaAnalisarEsboco();
            erro = OBJFinanceiro.AtualizaAnalisarEsbocoAPI();



            //Adiciona esboco no sap e atualiza no CRM, valida se o pedido já não está no SAP
            if (erro == "" && OBJFinanceiro.NumeroEsbocoSAP != "" && (OBJFinanceiro.NumeroPedidoSAP == "" || OBJFinanceiro.NumeroPedidoSAP == null || OBJFinanceiro.NumeroPedidoSAP == "0"))
            {
                OBJFinanceiro.IDEmpresa = Convert.ToInt32(EmpresaLabel.Text.Substring(0, 1));
                OBJFinanceiro.IDPedido = Convert.ToInt32(PedidoCRMLabel.Text != "" ? PedidoCRMLabel.Text : "0");
                //erro = OBJFinanceiro.AdicionaEsbocoPedido();
                erro = OBJFinanceiro.AdicionaEsbocoPedidoAPI();

                PrimeiraAprovacao = true;
            }

            //Atualiza historico do pedido
            if (erro == "")
            {
                OBJFinanceiro.DataHistorico = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                OBJFinanceiro.IDEmpresa = Convert.ToInt32(EmpresaLabel.Text.Substring(0, 1));
                OBJFinanceiro.IDPedido = Convert.ToInt32(PedidoCRMLabel.Text != "" ? PedidoCRMLabel.Text : "0");
                erro = OBJFinanceiro.AtualizaHistoricoPedidoCRM();

                //Atualiza historico pedido SAP
                if (PrimeiraAprovacao == true)
                {
                    if (OBJPedido.codigoEmpresa == "" || OBJPedido.codigoEmpresa == null || OBJPedido.IDPedido == 0)
                    {
                        OBJPedido.codigoEmpresa = OBJFinanceiro.IDEmpresa.ToString();
                        OBJPedido.IDPedido = OBJFinanceiro.IDPedido;
                    }
                    //Atualiza Histórico do pedido no SAP
                    erro = OBJPedido.AtualizarHistoricoPedidoSAPAPI();
                }
            }

            //Se não der nenhum problema grava
            if (erro == "")
            {
                Session["Msg"] = "Pedido " + OBJFinanceiro.IDPedido.ToString() + " aprovado com Sucesso!";

                //Dispara E-mail para o vendedor
                try
                {
                    OBJMail.CodigoEmpresa = EmpresaLabel.Text;
                    OBJMail.NumeroPedidoCRM = PedidoCRMLabel.Text != "" ? PedidoCRMLabel.Text : "0";
                    OBJMail.NomeCliente = ClienteLabel.Text;
                    OBJMail.DataAlteracao = DateTime.Today.ToString("dd/MM/yyyy");
                    OBJMail.Situacao = "Aprovado";
                    OBJMail.Status = "Liberado Financeiro.";
                    OBJMail.Historico = OBJFinanceiro.Historico;
                    if (OBJFinanceiro.HistoricoDetalhado == "") { OBJMail.HistoricoDetalhado = "Pedido Analisado !"; } else { OBJMail.HistoricoDetalhado = OBJFinanceiro.HistoricoDetalhado; }
                    OBJMail.TituloEmail = "Análise Financeira Pedido " + PedidoCRMLabel.Text != "" ? PedidoCRMLabel.Text : "0" + ".";
                    OBJMail.UsuarioCRM = Session["usuario"].ToString();
                    OBJMail.FormataTexto();

                    //OBJMail.RecuperaEmailDestinatario();
                    OBJMail.EmailDestinatario = OBJMail.RecuperaEmailAlteracaoFinanceiro();
                    //OBJMail.EmailDestinatario = "luiz.carlos@manulifitasa.com.br";

                    //OBJMail.enviaEmailFormatado();
                    OBJMail.enviaEmailFormatadoAnexo();

                }
                catch (Exception ex)
                {
                    string novoerro = ex.ToString();
                }

                RetornarLinkButton_Click(null, null);
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }

        }

        public void CarregaDadosNaTela()
        {
            DataTable OBJDataTable = new DataTable();

            OBJDataTable = OBJFinanceiro.RecuperaPedidosDetalheSAP();
            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    EmpresaLabel.Text = row["NomeEmpresa"].ToString();
                    PedidoCRMLabel.Text = row["PedidoCRM"].ToString();
                    PedidoSAPLabel.Text = row["PedidoSAP"].ToString();
                    EsbocoLabel.Text = row["NumeroEsboco"].ToString();
                    ClienteLabel.Text = row["Cliente"].ToString();
                    UtilizacaoLabel.Text = row["Utilizacao"].ToString();
                    DataLancamentoLabel.Text = Convert.ToDateTime(row["DataLancamento"]).ToString("dd/MM/yyyy");
                    DataEntregaLabel.Text = Convert.ToDateTime(row["DataEntrega"]).ToString("dd/MM/yyyy");
                    DataDocumentoLabel.Text = Convert.ToDateTime(row["DataDocumento"]).ToString("dd/MM/yyyy");
                    CondicaoPagamentoLabel.Text = row["CondicaoPagamento"].ToString();
                    TotalPedidoLabel.Text = String.Format("{0:C}", Convert.ToDouble(row["TotalPedido"].ToString()));

                    //Carrega Dados do Histórico na tela de aprovações
                    OBJFinanceiro.CodigoClienteSAP = row["CardCode"].ToString();
                    OBJFinanceiro.IDEmpresa = Convert.ToInt32(EmpresaLabel.Text.Substring(0, 1));
                    OBJFinanceiro.IDPedido = Convert.ToInt32(PedidoCRMLabel.Text != "" ? PedidoCRMLabel.Text : "0");
                    OBJFinanceiro.RecuperaHistoricoPedidoCRM();
                    HistoricoPedidoTextBox.Text = OBJFinanceiro.HistoricoPedido;
                }

                //Carrega dados do pedido
                //Carrega codigo da Entidade, caso não exista pedido CRM, vai utilizar o código do SAP
                OBJPedido.codigoEntidade = OBJFinanceiro.CodigoClienteSAP;
                OBJPedido.carregaDadosPedido(OBJFinanceiro.IDEmpresa.ToString(), OBJFinanceiro.IDPedido.ToString());
            }

            //Limpa DataTable
            OBJDataTable.Clear();
            OBJFinanceiro.IDUsuarioCRM = Convert.ToInt32(Session["IDUsuario"]);
            OBJFinanceiro.RetornaUsuarioSenhaSAP();
            OBJDataTable = OBJFinanceiro.RecuperaPedidosDetalheHistoricoSAP();
            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    //HistoricoTextBox.Text = row["Historico"].ToString();
                }
            }


            if (OBJFinanceiro.SituacaoPedido != "Pendente")
            {
                AprovarLinkButton.Visible = false;
                ReprovarLinkButton.Visible = false;
                SalvarLinkButton.Visible = false;
                HistoricoTextBox.Enabled = false;
            }

            //Inserindo datasource para dropdown empresa
            MotivoDropDownList.DataSource = OBJFinanceiro.RecuperaMotivos();
            MotivoDropDownList.DataValueField = "IDMotivo";
            MotivoDropDownList.DataTextField = "Descricao";
            MotivoDropDownList.DataBind();

            //Inserindo quantidade dias cancelamento
            DiasCancelamentoDropDownList.DataSource = OBJFinanceiro.RecuperaDiasCancelmaneto();
            DiasCancelamentoDropDownList.DataValueField = "QuantidadeDias";
            DiasCancelamentoDropDownList.DataTextField = "DescricaoQuantidade";
            DiasCancelamentoDropDownList.DataBind();

            //Caso pedido já tenha dias definidos sistema traz para mostrar na tela
            if (OBJPedido.DiasCancelamento != 0)
            {
                DiasCancelamentoDropDownList.SelectedValue = OBJPedido.DiasCancelamento.ToString();
            }

        }

        protected void ReprovarLinkButton_Click(object sender, EventArgs e)
        {

            string erro = "";

            //Recupera usuário do SAP para aprovação
            OBJFinanceiro.IDUsuarioCRM = Convert.ToInt32(Session["IDUsuario"]);
            OBJFinanceiro.RetornaUsuarioSenhaSAP();

            //Chama funação para efetuar aprovação
            //OBJFinanceiro.Historico = HistoricoTextBox.Text;
            OBJFinanceiro.HistoricoDetalhado = HistoricoTextBox.Text;
            OBJFinanceiro.Historico = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - " + MotivoDropDownList.SelectedItem.Text.ToString();
            OBJFinanceiro.IDMotivo = Convert.ToInt32(MotivoDropDownList.SelectedValue);
            OBJFinanceiro.AnalisePedido = "Reprovado";

            //erro = OBJFinanceiro.AtualizaAnalisarEsboco();
            erro = OBJFinanceiro.AtualizaAnalisarEsbocoAPI();

            //Atualiza historico do pedido
            if (erro == "")
            {
                OBJFinanceiro.DataHistorico = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                OBJFinanceiro.IDEmpresa = Convert.ToInt32(EmpresaLabel.Text.Substring(0, 1));
                OBJFinanceiro.IDPedido = Convert.ToInt32(PedidoCRMLabel.Text);
                erro = OBJFinanceiro.AtualizaHistoricoPedidoCRM();
            }

            if (erro == "")
            {
                //Fixo Status 7 --Cancelado
                OBJFinanceiro.IDStatus = 7;
                erro = OBJFinanceiro.RetornaPedidoVendedorCRM();
            }

            if (erro == "")
            {
                Session["Msg"] = "Pedido " + OBJFinanceiro.IDPedido.ToString() + " reprovado com Sucesso!.";

                //Dispara E-mail para o vendedor
                try
                {
                    OBJMail.CodigoEmpresa = EmpresaLabel.Text;
                    OBJMail.NumeroPedidoCRM = PedidoCRMLabel.Text;
                    OBJMail.NomeCliente = ClienteLabel.Text;
                    OBJMail.DataAlteracao = DateTime.Today.ToString("dd/MM/yyyy");
                    OBJMail.Situacao = "Reprovado";
                    OBJMail.Status = "Reprovado Financeiro.";
                    OBJMail.Historico = OBJFinanceiro.Historico;
                    if (OBJFinanceiro.HistoricoDetalhado == "") { OBJMail.HistoricoDetalhado = "Pedido Analisado !"; } else { OBJMail.HistoricoDetalhado = OBJFinanceiro.HistoricoDetalhado; }
                    OBJMail.TituloEmail = "Análise Financeira Pedido " + PedidoCRMLabel.Text + ".";
                    OBJMail.UsuarioCRM = Session["usuario"].ToString();
                    OBJMail.FormataTexto();

                    //OBJMail.RecuperaEmailDestinatario();
                    OBJMail.EmailDestinatario = OBJMail.RecuperaEmailAlteracaoFinanceiro();
                    //OBJMail.EmailDestinatario = "luiz.carlos@manulifitasa.com.br";

                    //OBJMail.enviaEmailFormatado();
                    OBJMail.enviaEmailFormatadoAnexo();
                }
                catch (Exception ex)
                {

                }

                RetornarLinkButton_Click(null, null);
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }

        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            //Recupera dias de aprovação
            OBJFinanceiro.DiasCancelamento = Convert.ToInt32(DiasCancelamentoDropDownList.SelectedValue);

            //Recupera usuário do SAP para aprovação
            OBJFinanceiro.IDUsuarioCRM = Convert.ToInt32(Session["IDUsuario"]);
            OBJFinanceiro.RetornaUsuarioSenhaSAP();

            //Chama funação para efetuar aprovação
            //OBJFinanceiro.Historico = HistoricoTextBox.Text;
            OBJFinanceiro.HistoricoDetalhado = HistoricoTextBox.Text;
            OBJFinanceiro.Historico = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - " + MotivoDropDownList.SelectedItem.Text.ToString();
            OBJFinanceiro.IDMotivo = Convert.ToInt32(MotivoDropDownList.SelectedValue);
            OBJFinanceiro.AnalisePedido = "Pendente";
            //erro = OBJFinanceiro.AtualizaAnalisarEsboco();
            erro = OBJFinanceiro.AtualizaAnalisarEsbocoAPI();

            //Atualiza historico do pedido
            if (erro == "")
            {
                OBJFinanceiro.DataHistorico = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                OBJFinanceiro.IDEmpresa = Convert.ToInt32(EmpresaLabel.Text.Substring(0, 1));
                OBJFinanceiro.IDPedido = Convert.ToInt32(PedidoCRMLabel.Text);
                erro = OBJFinanceiro.AtualizaHistoricoPedidoCRM();
            }

            //Atualiza dias cancelamento pedido
            if (erro == "")
            {
                OBJFinanceiro.DiasCancelamento = Convert.ToInt32(DiasCancelamentoDropDownList.SelectedValue);
                erro = OBJFinanceiro.AtualizaDiasCancelamentoPedidoCRM();
            }

            if (erro == "")
            {
                //Fixo Status 10 --Analisado Financeiro
                OBJFinanceiro.IDStatus = 10;
                erro = OBJFinanceiro.RetornaPedidoVendedorCRM();
            }

            if (erro == "")
            {
                Session["Msg"] = "Pedido " + OBJFinanceiro.IDPedido.ToString() + " salvo com Sucesso!.";

                //Dispara E-mail para o vendedor
                try
                {
                    OBJMail.CodigoEmpresa = EmpresaLabel.Text;
                    OBJMail.NumeroPedidoCRM = PedidoCRMLabel.Text;
                    OBJMail.NomeCliente = ClienteLabel.Text;
                    OBJMail.DataAlteracao = DateTime.Today.ToString("dd/MM/yyyy");
                    OBJMail.Situacao = "Analisado";
                    //OBJMail.Status = "Aguardando Financeiro.";
                    OBJMail.Status = MotivoDropDownList.SelectedItem.ToString();
                    //if (OBJFinanceiro.Historico == "") { OBJMail.Historico = "Pedido Aguardando !"; } else { OBJMail.Historico = OBJFinanceiro.Historico; }
                    OBJMail.Historico = OBJFinanceiro.Historico;
                    if (OBJFinanceiro.HistoricoDetalhado == "") { OBJMail.HistoricoDetalhado = "Pedido Analisado !"; } else { OBJMail.HistoricoDetalhado = OBJFinanceiro.HistoricoDetalhado; }
                    OBJMail.TituloEmail = "Análise Financeira " + PedidoCRMLabel.Text + " aguardando.";
                    OBJMail.UsuarioCRM = Session["usuario"].ToString();
                    OBJMail.FormataTexto();

                    //OBJMail.RecuperaEmailDestinatario();
                    OBJMail.EmailDestinatario = OBJMail.RecuperaEmailAlteracaoFinanceiro();
                    //OBJMail.EmailDestinatario = "luiz.carlos@manulifitasa.com.br";

                    //OBJMail.enviaEmailFormatado();
                    OBJMail.enviaEmailFormatadoAnexo();
                }
                catch (Exception ex)
                {

                }

                RetornarLinkButton_Click(null, null);
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }

        }

        protected void RetornarVendedorLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            //Recupera usuário do SAP para aprovação
            OBJFinanceiro.IDUsuarioCRM = Convert.ToInt32(Session["IDUsuario"]);
            OBJFinanceiro.RetornaUsuarioSenhaSAP();

            //Chama funação para efetuar aprovação
            //OBJFinanceiro.Historico = HistoricoTextBox.Text;
            OBJFinanceiro.HistoricoDetalhado = HistoricoTextBox.Text;
            OBJFinanceiro.Historico = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - " + MotivoDropDownList.SelectedItem.Text.ToString();
            OBJFinanceiro.IDMotivo = Convert.ToInt32(MotivoDropDownList.SelectedValue);
            OBJFinanceiro.AnalisePedido = "Pendente";
            //erro = OBJFinanceiro.AtualizaAnalisarEsboco();
            erro = OBJFinanceiro.AtualizaAnalisarEsbocoAPI();

            //Atualiza historico do pedido
            if (erro == "")
            {
                OBJFinanceiro.DataHistorico = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                OBJFinanceiro.IDEmpresa = Convert.ToInt32(EmpresaLabel.Text.Substring(0, 1));
                OBJFinanceiro.IDPedido = Convert.ToInt32(PedidoCRMLabel.Text);
                erro = OBJFinanceiro.AtualizaHistoricoPedidoCRM();
            }

            if (erro == "")
            {
                //Fixo Status 9 --Retornando Vendedor
                OBJFinanceiro.IDStatus = 9;
                erro = OBJFinanceiro.RetornaPedidoVendedorCRM();
            }

            if (erro == "")
            {
                Session["Msg"] = "Pedido " + OBJFinanceiro.IDPedido.ToString() + " retornado ao vendedor com Sucesso!";

                //Dispara E-mail para o vendedor
                try
                {
                    OBJMail.CodigoEmpresa = EmpresaLabel.Text;
                    OBJMail.NumeroPedidoCRM = PedidoCRMLabel.Text;
                    OBJMail.NomeCliente = ClienteLabel.Text;
                    OBJMail.DataAlteracao = DateTime.Today.ToString("dd/MM/yyyy");
                    //OBJMail.Situacao = "Aguardando";
                    OBJMail.Situacao = "Analisado";
                    //OBJMail.Status = "Aguardando Financeiro.";
                    OBJMail.Status = MotivoDropDownList.SelectedItem.ToString();
                    //if (OBJFinanceiro.Historico == "") { OBJMail.Historico = "Pedido Aguardando !"; } else { OBJMail.Historico = OBJFinanceiro.Historico; }
                    OBJMail.Historico = OBJFinanceiro.Historico;
                    if (OBJFinanceiro.HistoricoDetalhado == "") { OBJMail.HistoricoDetalhado = "Pedido Retornado Para Vendedor!"; } else { OBJMail.HistoricoDetalhado = OBJFinanceiro.HistoricoDetalhado; }
                    OBJMail.TituloEmail = "Análise financeira pedido " + PedidoCRMLabel.Text + " retornado.";
                    OBJMail.UsuarioCRM = Session["usuario"].ToString();
                    OBJMail.FormataTexto();

                    //OBJMail.RecuperaEmailDestinatario();
                    OBJMail.EmailDestinatario = OBJMail.RecuperaEmailAlteracaoFinanceiro();
                    //OBJMail.EmailDestinatario = "luiz.carlos@manulifitasa.com.br";

                    //OBJMail.enviaEmailFormatado();
                    OBJMail.enviaEmailFormatadoAnexo();
                }
                catch (Exception ex)
                {

                }

                RetornarLinkButton_Click(null, null);
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void ContaCorrenteLinkButton_Click(object sender, EventArgs e)
        {
            ClienteClasse OBJClienteClasse = new ClienteClasse();

            OBJClienteClasse.CodigoCliente = ClienteLabel.Text.Substring(0, ClienteLabel.Text.IndexOf("-")).Trim();

            DataTable OBJDataTable = OBJClienteClasse.RecuperaContaCorrenteClienteSAP();

            foreach (DataRow row in OBJDataTable.Rows)
            {
                Session["ContaCorrente"] = null;
                Session["ContaCorrenteDetalhe"] = null;
                Session["ContaCorrenteReturn"] = "~/financeiro/LiberacaoPedidosDetalheWebForm.aspx?indmnu=5";

                OBJClienteClasse.VendedorCliente = row["Vendedor"].ToString();
                OBJClienteClasse.CodigoCliente = row["CardCode"].ToString();
                OBJClienteClasse.CodigoAux = row["CardCode"].ToString();
                OBJClienteClasse.NomeCliente = row["CardName"].ToString();
                OBJClienteClasse.CNPJCliente = row["CNPJ"].ToString();
                OBJClienteClasse.LimiteCredito = Convert.ToDecimal(row["LimiteCredito"]);

                Session["ContaCorrente"] = OBJClienteClasse;
            }

            if (Session["ContaCorrente"] != null)
                Response.Redirect("~/financeiro/ContaCorrenteDetalheWebForm.aspx?indmnu=5");
        }
    }
}