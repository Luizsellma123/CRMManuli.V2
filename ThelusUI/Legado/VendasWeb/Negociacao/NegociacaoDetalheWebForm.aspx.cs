using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.Negociacao
{
    public partial class NegociacaoDetalheWebForm : System.Web.UI.Page
    {
        #region Instâncias e Variáveis Globais
        SessionClass OBJSessao = new SessionClass();
        NegociacaoClasse OBJNegociacao = new NegociacaoClasse();
        UtilClass ObjUtilClass = new UtilClass();
        enviarEmail OBJMail = new enviarEmail();
        pedido OBJPedido = new pedido();
        funcoes mdlfuncoes = new funcoes(); // Adicionado para carregar empresas
        #endregion

        #region Eventos do Ciclo de Vida
        protected void Page_Load(object sender, EventArgs e)
        {
            OBJSessao.ValidaAcesso();
            ExibirMensagensSessao();

            if (!IsPostBack)
            {
                // 1. Recupera o objeto gravado na Session (módulo de Lista) para a variável global da página
                if (Session["OBJNegociacao"] != null)
                {
                    OBJNegociacao = (NegociacaoClasse)Session["OBJNegociacao"];
                }

                // 2. Inicializa as listas e descarrega os dados na tela
                InicializarPagina();
            }
        }
        #endregion

        #region Inicialização e Cargas
        private void InicializarPagina()
        {
            // Carregar todos os componentes da tela
            CarregarEmpresas();
            CarregarSituacoes();
            CarregarSolicitantes();
            CarregarFretes();
            CarregarEstados();
            CarregarMunicipios();

            SincronizarCidadeTexto();

            CarregarRegimes();
            CarregarVendedores();
            CarregarClassificacaoComercial();
            CarregarValidades();
            CarregarSolicitantes();

            // Configura o comportamento dos campos baseado se é Novo ou Edição
            ConfigurarCamposInclusaoEdicao();

            Atualiza_Grid();
        }

        private void ExibirMensagensSessao()
        {
            Label lblMensagem = (Label)Master.Master.FindControl("MenssagemMasterLabel");
            lblMensagem.Text = "";

            if (Session["Msg"] != null)
            {
                lblMensagem.Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                lblMensagem.Visible = true;
                lblMensagem.Focus();
                Session.Remove("Msg");
            }
        }

        private void CarregarEmpresas()
        {
            drpEmpresa.DataSource = mdlfuncoes.Consultar_Empresas();
            drpEmpresa.DataTextField = "NomeEmpresa";
            drpEmpresa.DataValueField = "IDEmpresa";
            drpEmpresa.DataBind();
        }

        private void CarregarSituacoes()
        {
            drpSituacao.DataSource = OBJNegociacao.RetornaStatus();
            drpSituacao.DataTextField = "Descricao";
            drpSituacao.DataValueField = "Id";
            drpSituacao.DataBind();
        }

        private void CarregarSolicitantes()
        {
            drpSolicitante.DataSource = OBJNegociacao.RetornaUsuarios();
            drpSolicitante.DataTextField = "Descricao";
            drpSolicitante.DataValueField = "Id";
            drpSolicitante.DataBind();
        }

        private void CarregarFretes()
        {
            drpFrete.DataSource = OBJNegociacao.RetornaFretes();
            drpFrete.DataTextField = "Descricao";
            drpFrete.DataValueField = "Id";
            drpFrete.DataBind();
        }

        private void CarregarEstados()
        {
            drpEstado.DataSource = OBJNegociacao.RetornaEstados();
            drpEstado.DataTextField = "Nome";
            drpEstado.DataValueField = "IDEstado";
            drpEstado.DataBind();
        }

        private void CarregarMunicipios()
        {
            if (!string.IsNullOrEmpty(drpEstado.SelectedValue))
            {
                // Certifique-se de que o método RetornaCidades exista na sua NegociacaoClasse
                drpMunicipio.DataSource = OBJNegociacao.RetornaMunicipios(drpEstado.SelectedValue);
                drpMunicipio.DataTextField = "NomeMunicipio"; // Verifique o nome da coluna no seu banco
                drpMunicipio.DataValueField = "IDMunicipio";
                drpMunicipio.DataBind();
            }
        }

        private void CarregarRegimes()
        {
            drpRegime.DataSource = OBJNegociacao.RetornaRegimes();
            drpRegime.DataTextField = "Descricao";
            drpRegime.DataValueField = "Id";
            drpRegime.DataBind();
        }

        private void CarregarVendedores()
        {
            drpVendedor.DataSource = OBJNegociacao.RetornaVendedores();
            drpVendedor.DataTextField = "Descricao";
            drpVendedor.DataValueField = "Id";
            drpVendedor.DataBind();
        }

        private void CarregarClassificacaoComercial()
        {
            drpClasComercial.DataSource = OBJNegociacao.RetornaClassificacaoComercial();
            drpClasComercial.DataTextField = "Descricao";
            drpClasComercial.DataValueField = "Id";
            drpClasComercial.DataBind();
        }

        private void CarregarValidades()
        {
            drpValidade.DataSource = OBJNegociacao.RetornaValidades();
            drpValidade.DataTextField = "Descricao";
            drpValidade.DataValueField = "Id";
            drpValidade.DataBind();
        }

        private void CarregarDadosCliente(string IDCliente)
        {
            try
            {
                // ARMAZENA O IDCLIENTE NA HIDDENFIELD DA TELA
                hfIdCliente.Value = IDCliente;

                // Busca os dados do cliente
                DataTable dtCliente = OBJNegociacao.RetornaDadosCliente(IDCliente, 1, 1);

                if (dtCliente != null && dtCliente.Rows.Count > 0)
                {
                    DataRow dr = dtCliente.Rows[0];

                    // 1. Preenche o campo de texto do cliente (Cód SAP - Nome - CNPJ)
                    txtCliente.Text = dr["Cliente"] != DBNull.Value ? dr["Cliente"].ToString() : string.Empty;

                    // 2. Seleciona o Vendedor associado (Tratamento para '0', nulos ou IDs inexistentes)
                    string idVendedor = dr["IDVendedor"] != DBNull.Value ? dr["IDVendedor"].ToString() : string.Empty;
                    ListItem itemVendedor = drpVendedor.Items.FindByValue(idVendedor);

                    if (itemVendedor != null && idVendedor != "0" && !string.IsNullOrEmpty(idVendedor))
                    {
                        drpVendedor.SelectedValue = idVendedor;
                    }
                    else
                    {
                        drpVendedor.SelectedIndex = 0; // Volta para o item padrão/inicial caso venha 0 ou inválido
                    }

                    // 3. Seleciona a Classificação Comercial associada (Tratamento para '0', nulos ou IDs inexistentes)
                    string idClasComercial = dr["IDClassificacaoComercial"] != DBNull.Value ? dr["IDClassificacaoComercial"].ToString() : string.Empty;
                    ListItem itemClasComercial = drpClasComercial.Items.FindByValue(idClasComercial);

                    if (itemClasComercial != null && idClasComercial != "0" && !string.IsNullOrEmpty(idClasComercial))
                    {
                        drpClasComercial.SelectedValue = idClasComercial;
                    }
                    else
                    {
                        drpClasComercial.SelectedIndex = 0; // Volta para o item padrão/inicial caso venha 0 ou inválido
                    }
                }
            }
            catch (Exception ex)
            {
                // Exibe mensagem de erro genérica ou registra log
                ScriptManager.RegisterStartupScript(this, this.GetType(), "erroCliente", $"alert('Erro ao carregar dados do cliente: {ex.Message}');", true);
            }
        }

        private void SincronizarCidadeTexto()
        {
            // Se houver uma opção válida selecionada, joga o texto para o campo
            if (drpMunicipio.SelectedIndex > 0)
            {
                txtCidade.Text = drpMunicipio.SelectedItem.Text;
            }
            else
            {
                txtCidade.Text = "";
            }
        }

        private void ConfigurarCamposInclusaoEdicao()
        {
            // Verifica se é uma nova inclusão checando a chave principal da classe
            bool isNovo = (OBJNegociacao?.IDNegociacao ?? 0) == 0;

            if (isNovo)
            {
                #region Regras Exclusivas para INCLUSÃO

                try
                {
                    if (Session["IdUsuario"] != null)
                    {
                        drpSolicitante.SelectedValue = Session["IdUsuario"].ToString();
                    }
                }
                catch
                {
                    // Evita exceção caso o usuário logado não esteja cadastrado na lista de solicitantes
                }

                // Define a data atual no formato aceito pelo TextMode="Date" (yyyy-MM-dd)
                txtData.Text = DateTime.Now.ToString("yyyy-MM-dd");

                // Habilitação padrão para novo registro
                HabilitarCamposFormulario(true);

                #endregion
            }
            else
            {
                #region Regras Exclusivas para EDIÇÃO

                // Chave da Negociação
                txtNegociacao.Text = OBJNegociacao.IDNegociacao.ToString();

                // Combos Obrigatórios de Cabeçalho
                if (OBJNegociacao.IDEmpresa.HasValue)
                    drpEmpresa.SelectedValue = OBJNegociacao.IDEmpresa.Value.ToString();

                if (OBJNegociacao.IDStatus.HasValue)
                    drpSituacao.SelectedValue = OBJNegociacao.IDStatus.Value.ToString();

                if (OBJNegociacao.IDUsuarioSolicitante.HasValue)
                    drpSolicitante.SelectedValue = OBJNegociacao.IDUsuarioSolicitante.Value.ToString();

                // Data da Solicitação
                if (OBJNegociacao.DataSolicitacao.HasValue)
                    txtData.Text = OBJNegociacao.DataSolicitacao.Value.ToString("yyyy-MM-dd");

                // Regra do Cliente
                chkNovo.Checked = OBJNegociacao.ClienteNovo;
                txtCliente.Text = OBJNegociacao.NomeCliente;

                if (OBJNegociacao.IDCliente.HasValue)
                {
                    hfIdCliente.Value = OBJNegociacao.IDCliente.Value.ToString();
                }
                else
                {
                    hfIdCliente.Value = string.Empty;
                }

                // Localização (Estado -> Município -> Cidade Texto)
                if (OBJNegociacao.IDEstado.HasValue)
                {
                    drpEstado.SelectedValue = OBJNegociacao.IDEstado.Value.ToString();

                    // Recarrega as cidades com base no estado selecionado
                    CarregarMunicipios();

                    if (OBJNegociacao.IDMunicipio.HasValue)
                    {
                        drpMunicipio.SelectedValue = OBJNegociacao.IDMunicipio.Value.ToString();
                    }
                }

                txtCidade.Text = OBJNegociacao.Cidade;
                txtFormaPagamento.Text = OBJNegociacao.CondicaoPagamento;

                // Combos Opcionais / Detalhes Comerciais
                if (OBJNegociacao.IDRegime.HasValue)
                    drpRegime.SelectedValue = OBJNegociacao.IDRegime.Value.ToString();

                if (OBJNegociacao.IDVendedor.HasValue)
                    drpVendedor.SelectedValue = OBJNegociacao.IDVendedor.Value.ToString();

                if (OBJNegociacao.IDClassificacaoComercial.HasValue)
                    drpClasComercial.SelectedValue = OBJNegociacao.IDClassificacaoComercial.Value.ToString();

                if (OBJNegociacao.IDFreteNegociacao.HasValue)
                    drpFrete.SelectedValue = OBJNegociacao.IDFreteNegociacao.Value.ToString();

                if (OBJNegociacao.IDValidadeNegociacao.HasValue)
                    drpValidade.SelectedValue = OBJNegociacao.IDValidadeNegociacao.Value.ToString();

                // Dentro de ConfigurarCamposInclusaoEdicao() -> bloco de EDIÇÃO:
                if (OBJNegociacao.IDEmpresa.HasValue && OBJNegociacao.IDNegociacao.HasValue)
                {
                    // Preenche o campo de texto acumulado do histórico
                    txtHistorico.Text = OBJNegociacao.ObterHistoricoFormatadoTexto(OBJNegociacao.IDEmpresa.Value, OBJNegociacao.IDNegociacao.Value);
                }

                // Regra de Permissão por Status (Permite edição apenas em Status 1 ou 5)
                int statusAtual = OBJNegociacao.IDStatus ?? 0;
                bool permiteEdicao = (statusAtual == 1 || statusAtual == 5);

                HabilitarCamposFormulario(permiteEdicao);

                #endregion
            }

            #region Trava Permanente de Campos de Controle

            // Campos imutáveis em qualquer situação de edição ou salvamento
            drpEmpresa.Enabled = isNovo; // Trava a Empresa na edição
            drpSolicitante.Enabled = false;
            txtData.Enabled = false;
            drpSituacao.Enabled = false;
            txtNegociacao.Enabled = false;
            txtHistorico.Enabled = false;

            #endregion
        }

        /// <summary>
        /// Controla a habilitação dos campos do formulário com base no Status da Negociação.
        /// </summary>
        private void HabilitarCamposFormulario(bool habilitar)
        {
            // Campos de Localização e Pagamento
            drpEstado.Enabled = habilitar;
            drpMunicipio.Enabled = habilitar;
            txtCidade.Enabled = habilitar;
            txtFormaPagamento.Enabled = habilitar;

            // Regra do Cliente
            chkNovo.Enabled = habilitar;
            txtCliente.ReadOnly = !chkNovo.Checked || !habilitar;
            btnProcurarCliente.Enabled = !chkNovo.Checked && habilitar;

            // Combos Comerciais
            drpRegime.Enabled = habilitar;
            drpVendedor.Enabled = habilitar;
            drpClasComercial.Enabled = habilitar;
            drpFrete.Enabled = habilitar;
            drpValidade.Enabled = habilitar;

            // O campo de Observação/Histórico e botões de ação continuam sempre disponíveis
            if (txtObservacao != null) txtObservacao.Enabled = true;
            if (txtHistorico != null) txtHistorico.Enabled = true;
        }
        #endregion

        #region Eventos de Controles
        protected void drpEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpMunicipio.Items.Clear();
            CarregarMunicipios();

            // Limpa o campo de texto se mudar o estado
            txtCidade.Text = "";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "refreshSelectPicker",
                "$('.selectpicker').selectpicker('refresh');", true);
        }

        // Evento ao mudar o município: preenche o campo de texto
        protected void drpMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Usa o novo método helper
            SincronizarCidadeTexto();

            // Mantém o SelectPicker funcional após o UpdatePanel
            ScriptManager.RegisterStartupScript(this, this.GetType(), "refreshSelectPicker", "$('.selectpicker').selectpicker('refresh');", true);
        }

        // Checkbox de Cliente Novo
        protected void chkNovo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNovo.Checked)
            {
                txtCliente.ReadOnly = false;
                txtCliente.Text = string.Empty;
                txtCliente.Focus();
                btnProcurarCliente.Enabled = false;
            }
            else
            {
                txtCliente.ReadOnly = true;
                btnProcurarCliente.Enabled = true;
            }

            // CORREÇÃO: Garante que o bloqueio/desbloqueio reflita na tela
            updFormulario.Update();

            // REAPLICA O SELECTPICKER APÓS O POSTBACK DO CHECKBOX
            ScriptManager.RegisterStartupScript(this, this.GetType(), "refreshSelectPicker", "$('.selectpicker').selectpicker('refresh');", true);
        }
        #endregion

        #region Pesquisa de Clientes (Modal)
        // 1. Quando o usuário clica no botão "Procurar" na tela principal
        protected void btnProcurarCliente_Click(object sender, EventArgs e)
        {
            CarregarClientesModal(string.Empty);

            // CORREÇÃO: Força o modal a receber o HTML da Grid preenchida no 1º clique
            updModalCliente.Update();

            // Comando JavaScript do Bootstrap para exibir o modal na tela
            ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModalCliente", "$('#modalCliente').modal('show'); $('.selectpicker').selectpicker('refresh');", true);
        }

        // Método auxiliar para buscar os clientes no banco
        private void CarregarClientesModal(string filtro)
        {
            // O GridView do ASP.NET gerencia as páginas a partir do índice 0, 
            // por isso somamos 1 para alinhar com o parâmetro @Pagina da Stored Procedure.
            int paginaAtual = gridClientesModal.PageIndex + 1;
            int linhasPorPagina = gridClientesModal.PageSize;

            // Chama o método na classe de negócio passando o filtro e a paginação
            DataTable dt = OBJNegociacao.RetornaClientesPaginado(filtro, paginaAtual, linhasPorPagina);

            gridClientesModal.DataSource = dt;
            gridClientesModal.DataBind();
        }

        // 2. Botão de pesquisa interno do modal
        protected void btnFiltrarModal_Click(object sender, EventArgs e)
        {
            CarregarClientesModal(txtFiltroCliente.Text);

            // Mantém o modal aberto após o PostBack do UpdatePanel
            ScriptManager.RegisterStartupScript(this, this.GetType(), "manterModalCliente", "$('#modalCliente').modal('show'); $('.selectpicker').selectpicker('refresh');", true);
        }

        // 3. Quando o usuário clica em "Selecionar" em uma linha do GridView do modal
        protected void gridClientesModal_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Recupera o IDCliente selecionado na grid do modal
            string idClienteSelecionado = gridClientesModal.SelectedDataKey.Value.ToString();

            // 2. Carrega os dados completos do banco (preenche txtCliente, drpVendedor e drpClasComercial)
            CarregarDadosCliente(idClienteSelecionado);

            // CORREÇÃO: Força a tela principal a se atualizar e exibir os dados puxados acima
            updFormulario.Update();

            // 3. Fecha o modal e agenda o refresh do SelectPicker com um pequeno delay 
            string script = @"
            $('#modalCliente').modal('hide');
            setTimeout(function() {
                $('.selectpicker').selectpicker('refresh');
            }, 100);";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "fecharEAtualizarCliente", script, true);
        }

        // Evento de troca de página do GridView do modal
        protected void gridClientesModal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Define a nova página selecionada pelo usuário
            gridClientesModal.PageIndex = e.NewPageIndex;

            // Recarrega os dados do grid aplicando o filtro que estava digitado
            CarregarClientesModal(txtFiltroCliente.Text);

            // IMPORTANTE: Mantém o modal aberto após o PostBack da paginação
            ScriptManager.RegisterStartupScript(this, this.GetType(), "manterModalCliente", "$('#modalCliente').modal('show');", true);
        }
        #endregion

        #region Ações de Negócio
        public void Atualiza_Grid()
        {
            // Mantendo a lógica original
            //DataTable OBJDataTable = OBJFinanceiro.RecuperaAutorizacoesEsbocoSAP();
            //AprovacoesGridView.DataSource = OBJDataTable;
            //AprovacoesGridView.DataBind();
            //AprovacoesMultiView.Visible = true;
        }

        public void CarregaDadosNaTela()
        {
            // ... (Sua lógica original de CarregaDadosNaTela permanece aqui)
        }

        protected void AprovarLinkButton_Click(object sender, EventArgs e)
        {
            // ... (Sua lógica original de Aprovar permanece aqui)
        }

        protected void ReprovarLinkButton_Click(object sender, EventArgs e)
        {
            // ... (Sua lógica original de Reprovar permanece aqui)
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validações da Interface de Usuário
                string mensagemErroValidacao;
                if (!ValidarCamposTela(out mensagemErroValidacao))
                {
                    Label lblMensagem = (Label)Master.Master.FindControl("MenssagemMasterLabel");
                    lblMensagem.Text = ObjUtilClass.MenssagemErro(mensagemErroValidacao, true);
                    lblMensagem.Visible = true;
                    lblMensagem.Focus();
                    return;
                }

                // Identifica se é uma nova inclusão ANTES de gravar
                bool isNovo = (OBJNegociacao?.IDNegociacao ?? 0) == 0;

                // 2. Mapeia os dados da tela para o objeto
                PreencherObjetoNegociacao();

                // 3. Executa a persistência da Negociação no banco
                DataTable dtResultado = OBJNegociacao.Gravar();

                if (dtResultado != null && dtResultado.Rows.Count > 0)
                {
                    DataRow dr = dtResultado.Rows[0];
                    int idNegociacaoGerado = Convert.ToInt32(dr["IDNegociacao"]);

                    txtNegociacao.Text = idNegociacaoGerado.ToString();

                    // 4. GRAVAÇÃO DE HISTÓRICO

                    // A) Grava a observação principal do usuário ou o texto padrão
                    if (string.IsNullOrWhiteSpace(OBJNegociacao.Historico))
                    {
                        OBJNegociacao.Historico = "Solicitação para autorizar negociação.";
                    }

                    OBJNegociacao.GravarHistorico(idNegociacaoGerado);

                    // B) REGRA EXCLUSIVA PARA INCLUSÃO: Grava o Histórico de Faturamento do SAP
                    if (isNovo)
                    {
                        string historicoSAP = string.Empty;

                        if (!OBJNegociacao.ClienteNovo && OBJNegociacao.IDCliente.HasValue)
                        {
                            // Busca o CardCode (Código SAP) do cliente a partir do IDCliente
                            string IDCliente = hfIdCliente.Value; // Ou recupere o código SAP correspondente
                            historicoSAP = OBJNegociacao.ObterHistoricoFaturamentoSAP(IDCliente);
                        }
                        else
                        {
                            // Cliente novo não possui histórico no SAP
                            historicoSAP = "Cliente sem Histórico de faturamento.";
                        }

                        // Configura os parâmetros do histórico para o lançamento do SAP
                        OBJNegociacao.Historico = historicoSAP;
                        OBJNegociacao.IDTipoHistorico = 10;
                        OBJNegociacao.IDEventoHistorico = 1;
                        OBJNegociacao.IDCategoriaHistorico = 1;

                        OBJNegociacao.GravarHistorico(idNegociacaoGerado);
                    }

                    // Limpa os campos de observação da tela após salvar
                    if (txtObservacao != null) txtObservacao.Text = string.Empty;
                    if (txtHistorico != null) txtHistorico.Text = string.Empty;

                    Session["Msg"] = dr["Mensagem"].ToString();

                    ExibirMensagensSessao();

                    updFormulario.Update();
                    TesteUpdatePanel.Update();
                }
            }
            catch (Exception ex)
            {
                Label lblMensagem = (Label)Master.Master.FindControl("MenssagemMasterLabel");
                if (lblMensagem != null)
                {
                    lblMensagem.Text = ObjUtilClass.MenssagemErro("Erro ao salvar negociação: " + ex.Message, true);
                    lblMensagem.Visible = true;
                    lblMensagem.Focus();
                }
            }
        }

        protected void RetornarVendedorLinkButton_Click(object sender, EventArgs e)
        {
            // ... (Sua lógica original de RetornarVendedor permanece aqui)
        }

        protected void PerderVendaLinkButton_Click(object sender, EventArgs e)
        {
            // Lógica para marcar a venda como perdida
        }
        #endregion

        #region Navegação
        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Session.Remove("OBJNegociacao");
            Response.Redirect("~/Negociacao/ListaNegociacaoWebForm.aspx?indmnu=3", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void RetornarNegociacaoLinkButton_Click(object sender, EventArgs e)
        {

        }

        protected void ContaCorrenteLinkButton_Click(object sender, EventArgs e)
        {
            // ... (Sua lógica original de ContaCorrente permanece aqui)
        }
        #endregion

        #region Auxiliares de Gravação

        private bool ValidarCamposTela(out string mensagemErro)
        {
            mensagemErro = string.Empty;

            if (string.IsNullOrEmpty(drpEmpresa.SelectedValue) || drpEmpresa.SelectedValue == "0")
            {
                mensagemErro = "Selecione uma Empresa válida.";
                return false;
            }

            if (string.IsNullOrEmpty(drpSituacao.SelectedValue) || drpSituacao.SelectedValue == "0")
            {
                mensagemErro = "Selecione uma Situação válida.";
                return false;
            }

            if (chkNovo.Checked && string.IsNullOrWhiteSpace(txtCliente.Text))
            {
                mensagemErro = "Informe o Nome do Cliente para novos cadastros.";
                return false;
            }

            if (!chkNovo.Checked && string.IsNullOrWhiteSpace(txtCliente.Text))
            {
                mensagemErro = "Selecione um Cliente cadastrado.";
                return false;
            }

            return true;
        }

        private void PreencherObjetoNegociacao()
        {
            int idNegociacao = 0;
            int.TryParse(txtNegociacao.Text, out idNegociacao);
            OBJNegociacao.IDNegociacao = idNegociacao;

            OBJNegociacao.IDEmpresa = Convert.ToInt32(drpEmpresa.SelectedValue);
            OBJNegociacao.IDStatus = Convert.ToInt32(drpSituacao.SelectedValue);
            OBJNegociacao.IDUsuarioSolicitante = Convert.ToInt32(drpSolicitante.SelectedValue);

            if (!string.IsNullOrEmpty(txtData.Text))
                OBJNegociacao.DataSolicitacao = Convert.ToDateTime(txtData.Text);

            // Localização
            OBJNegociacao.IDEstado = (!string.IsNullOrEmpty(drpEstado.SelectedValue) && drpEstado.SelectedValue != "0")
                ? Convert.ToInt32(drpEstado.SelectedValue) : (int?)null;

            OBJNegociacao.IDMunicipio = (!string.IsNullOrEmpty(drpMunicipio.SelectedValue) && drpMunicipio.SelectedValue != "0")
                ? Convert.ToInt32(drpMunicipio.SelectedValue) : (int?)null;

            OBJNegociacao.Cidade = txtCidade.Text.Trim();
            OBJNegociacao.CondicaoPagamento = txtFormaPagamento.Text.Trim();

            // Regra do Cliente
            OBJNegociacao.ClienteNovo = chkNovo.Checked;
            OBJNegociacao.NomeCliente = txtCliente.Text.Trim();

            if (!OBJNegociacao.ClienteNovo && hfIdCliente != null && !string.IsNullOrEmpty(hfIdCliente.Value))
            {
                OBJNegociacao.IDCliente = Convert.ToInt32(hfIdCliente.Value);
            }
            else
            {
                OBJNegociacao.IDCliente = null;
            }

            // Combos Opcionais
            OBJNegociacao.IDRegime = (!string.IsNullOrEmpty(drpRegime.SelectedValue) && drpRegime.SelectedValue != "0")
                ? Convert.ToInt32(drpRegime.SelectedValue) : (int?)null;

            OBJNegociacao.IDVendedor = (!string.IsNullOrEmpty(drpVendedor.SelectedValue) && drpVendedor.SelectedValue != "0")
                ? Convert.ToInt32(drpVendedor.SelectedValue) : (int?)null;

            OBJNegociacao.IDClassificacaoComercial = (!string.IsNullOrEmpty(drpClasComercial.SelectedValue) && drpClasComercial.SelectedValue != "0")
                ? Convert.ToInt32(drpClasComercial.SelectedValue) : (int?)null;

            OBJNegociacao.IDFreteNegociacao = (!string.IsNullOrEmpty(drpFrete.SelectedValue) && drpFrete.SelectedValue != "0")
                ? Convert.ToInt32(drpFrete.SelectedValue) : (int?)null;

            OBJNegociacao.IDValidadeNegociacao = (!string.IsNullOrEmpty(drpValidade.SelectedValue) && drpValidade.SelectedValue != "0")
                ? Convert.ToInt32(drpValidade.SelectedValue) : (int?)null;

            // Texto do Histórico
            OBJNegociacao.Historico = txtObservacao.Text.Trim();
        }

        #endregion
    }
}