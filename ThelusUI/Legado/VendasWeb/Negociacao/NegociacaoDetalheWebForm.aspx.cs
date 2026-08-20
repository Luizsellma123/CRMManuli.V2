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
            // Verifica se é uma nova inclusão checando o ID na classe de negócio
            bool isNovo = (OBJNegociacao?.IDNegociacao ?? 0) == 0;

            if (isNovo)
            {
                // 1. Regras exclusivas para INCLUSÃO
                try
                {
                    if (Session["IdUsuario"] != null)
                    {
                        drpSolicitante.SelectedValue = Session["IdUsuario"].ToString();
                    }
                }
                catch
                {
                    // Evita exceção caso o usuário logado não esteja no combo
                }

                // Define a data de hoje para novos registros 
                // (Obrigatório o formato yyyy-MM-dd para TextBox com TextMode="Date")
                txtData.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                // 2. Regras exclusivas para EDIÇÃO
                // (O OBJNegociacao já trará os dados carregados do banco para o txtData)
            }

            // 3. Regras gerais (travamentos de campos que não devem ser alterados)
            txtCliente.ReadOnly = true;
            drpSolicitante.Enabled = false;
            txtData.Enabled = false;
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

        protected void chkNovo_CheckedChanged(object sender, EventArgs e)
        {
            // Quando marcado, libera o campo para digitação manual e desativa o botão de procurar
            if (chkNovo.Checked)
            {
                txtCliente.ReadOnly = false;
                txtCliente.Text = string.Empty;
                txtCliente.Focus();
                btnProcurarCliente.Enabled = false;
            }
            else
            {
                // Quando desmarcado, bloqueia a digitação manual e ativa o botão de procurar
                txtCliente.ReadOnly = true;
                btnProcurarCliente.Enabled = true;
            }

            // REAPLICA O SELECTPICKER APÓS O POSTBACK DO CHECKBOX
            ScriptManager.RegisterStartupScript(this, this.GetType(), "refreshSelectPicker", "$('.selectpicker').selectpicker('refresh');", true);
        }
        #endregion

        #region Pesquisa de Clientes (Modal)
        // 1. Quando o usuário clica no botão "Procurar" na tela principal
        protected void btnProcurarCliente_Click(object sender, EventArgs e)
        {
            CarregarClientesModal(string.Empty);

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
            // Recupera o IDCliente definido no DataKeyNames acima
            string idClienteSelecionado = gridClientesModal.SelectedDataKey.Value.ToString();

            // Recupera o NomeCliente (exemplo de célula caso queira exibir na tela)
            // Usamos Cells[2] porque a coluna 0 é o Botão, 1 é CodigoSAP, 2 é NomeCliente
            string nomeCliente = gridClientesModal.SelectedRow.Cells[2].Text;

            // Ação: Preencher a tela principal
            txtCliente.Text = nomeCliente;

            // Ação: Se você tiver um campo oculto (HiddenField) para armazenar o ID do cliente selecionado, use-o aqui:
            // hfIdClienteSelecionado.Value = idClienteSelecionado;

            // Opcional: Se precisar carregar os dados completos do cliente ao selecionar:
            // CarregarDadosCliente(idClienteSelecionado);

            // Fecha o modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fecharModal", "$('#modalCliente').modal('hide'); $('.selectpicker').selectpicker('refresh');", true);
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
            // ... (Sua lógica original de Salvar permanece aqui)
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
            Response.Redirect("ListaNegociacaoWebForm.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void RetornarNegociacaoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("LiberacaoNegociacaoWebForm.aspx?indmnu=3", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void ContaCorrenteLinkButton_Click(object sender, EventArgs e)
        {
            // ... (Sua lógica original de ContaCorrente permanece aqui)
        }
        #endregion
    }
}