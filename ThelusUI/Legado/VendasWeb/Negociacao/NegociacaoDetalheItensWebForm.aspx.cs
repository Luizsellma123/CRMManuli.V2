using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Negociacao
{
    public partial class NegociacaoDetalheItensWebForm : System.Web.UI.Page
    {
        #region Instâncias e Variáveis Globais
        SessionClass OBJSessao = new SessionClass();
        NegociacaoClasse OBJNegociacao = new NegociacaoClasse();
        UtilClass ObjUtilClass = new UtilClass();
        enviarEmail OBJMail = new enviarEmail();
        pedido OBJPedido = new pedido();
        funcoes mdlfuncoes = new funcoes();
        #endregion

        #region Eventos do Ciclo de Vida
        protected void Page_Load(object sender, EventArgs e)
        {
            OBJSessao.ValidaAcesso();
            ExibirMensagensSessao();

            // Sempre recupera a instância ativa da sessão (tanto no Load inicial quanto no PostBack)
            if (Session["OBJNegociacao"] != null)
            {
                OBJNegociacao = (NegociacaoClasse)Session["OBJNegociacao"];
            }

            if (!IsPostBack)
            {
                // Inicializa combos e dados da tela no primeiro carregamento
                InicializarPagina();
            }
        }
        #endregion

        #region Inicialização e Cargas
        private void InicializarPagina()
        {
            // 1. Carrega os combos da tela
            CarregarEmpresas();
            CarregarSituacoes();
            CarregarProdutos();

            // 2. Preenche os dados do cabeçalho da negociação
            CarregarCabecalhoNegociacao();

            // 3. Carrega a grid com os itens gravados
            CarregarGridItens();
        }

        private void ExibirMensagensSessao()
        {
            Label lblMensagem = (Label)Master.Master.FindControl("MenssagemMasterLabel");
            if (lblMensagem != null)
            {
                lblMensagem.Text = "";

                if (Session["Msg"] != null)
                {
                    lblMensagem.Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                    lblMensagem.Visible = true;
                    lblMensagem.Focus();
                    Session.Remove("Msg");
                }
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

        private void CarregarProdutos()
        {
            // Ajuste o nome do método na NegociacaoClasse se for diferente na sua estrutura
            if (drpProduto != null)
            {
                drpProduto.DataSource = OBJNegociacao.RetornaProdutos();
                drpProduto.DataTextField = "DescricaoProduto";
                drpProduto.DataValueField = "IDProduto";
                drpProduto.DataBind();
                drpProduto.Items.Insert(0, new ListItem("Selecione um produto...", ""));
            }
        }

        private void CarregarCabecalhoNegociacao()
        {
            if (OBJNegociacao != null && OBJNegociacao.IDNegociacao > 0)
            {
                txtNegociacao.Text = OBJNegociacao.IDNegociacao.ToString();

                if (OBJNegociacao.IDEmpresa.HasValue)
                {
                    drpEmpresa.SelectedValue = OBJNegociacao.IDEmpresa.Value.ToString();

                    // Regra de Rótulo por Empresa: Se Empresa == 3, altera para "Preço Final:"
                    if (OBJNegociacao.IDEmpresa.Value == 3)
                    {
                        lblExSimulador.Text = "Preço Final:";
                    }
                    else
                    {
                        lblExSimulador.Text = "Ex. Simulador:";
                    }
                }

                if (OBJNegociacao.IDStatus.HasValue)
                    drpSituacao.SelectedValue = OBJNegociacao.IDStatus.Value.ToString();
            }
        }

        private void CarregarGridItens()
        {
            if (OBJNegociacao != null && OBJNegociacao.IDEmpresa.HasValue && OBJNegociacao.IDNegociacao.HasValue)
            {
                gridItensNegociacao.DataSource = OBJNegociacao.RetornaItensNegociacao();
                gridItensNegociacao.DataBind();
            }
        }
        #endregion

        #region Eventos de Ação dos Botões
        protected void btnAdicionarItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(drpProduto.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertaProduto", "alert('Selecione um produto antes de adicionar.');", true);
                    return;
                }

                // Mapeamento dos atributos na instância
                OBJNegociacao.IDItem = 0;
                OBJNegociacao.IDProduto = Convert.ToInt32(drpProduto.SelectedValue);

                decimal quantidade = 0, exSimulador = 0, solicitado = 0, desconto = 0;
                decimal.TryParse(txtQuantidade.Text, out quantidade);
                decimal.TryParse(txtExSimulador.Text.Replace(".", "").Replace(",", "."), out exSimulador);
                decimal.TryParse(txtSolicitado.Text.Replace(".", "").Replace(",", "."), out solicitado);

                if (exSimulador > 0 && solicitado > 0)
                {
                    desconto = (1 - (solicitado / exSimulador)) * 100;
                    if (desconto < 0) desconto = 0;
                }

                OBJNegociacao.Quantidade = quantidade;
                OBJNegociacao.ValorSimulador = exSimulador;
                OBJNegociacao.ValorSolicitado = solicitado;
                OBJNegociacao.PercentualDesconto = desconto;

                // Executa a gravação
                DataTable dtRes = OBJNegociacao.GravarItem();

                if (dtRes != null && dtRes.Rows.Count > 0 && Convert.ToInt32(dtRes.Rows[0]["Sucesso"]) == 1)
                {
                    // 1. Limpa os campos e reseta o combo visual
                    LimparCamposItem();

                    // 2. Recarrega a GridView com o novo item
                    CarregarGridItens();
                    updGridItens.Update();
                }
            }
            catch (Exception ex)
            {
                Label lblMensagem = (Label)Master.Master.FindControl("MenssagemMasterLabel");
                if (lblMensagem != null)
                {
                    lblMensagem.Text = ObjUtilClass.MenssagemErro("Erro ao adicionar item: " + ex.Message, true);
                    lblMensagem.Visible = true;
                }
            }
        }

        protected void gridItensNegociacao_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                // 1. Obtém o IDItem a partir da chave DataKeys da linha selecionada
                int idItem = Convert.ToInt32(gridItensNegociacao.DataKeys[e.RowIndex].Value);

                // 2. Carrega a propriedade no objeto ativo da sessão
                OBJNegociacao.IDItem = idItem;

                // 3. Executa a exclusão via Stored Procedure encapsulada
                DataTable dtResultado = OBJNegociacao.ExcluirItem();

                if (dtResultado != null && dtResultado.Rows.Count > 0)
                {
                    DataRow dr = dtResultado.Rows[0];

                    if (dr["Sucesso"] != DBNull.Value && Convert.ToInt32(dr["Sucesso"]) == 1)
                    {
                        // 4. Recarrega a GridView com a lista atualizada
                        CarregarGridItens();

                        // 5. Atualiza o UpdatePanel da grid para refletir a remoção na tela sem F5
                        updGridItens.Update();

                        // Exibe mensagem opcional de sucesso via MasterPage
                        Label lblMensagem = (Label)Master.Master.FindControl("MenssagemMasterLabel");
                        if (lblMensagem != null)
                        {
                            lblMensagem.Text = ObjUtilClass.MenssagemSucesso("Item excluído com sucesso!", true);
                            lblMensagem.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Label lblMensagem = (Label)Master.Master.FindControl("MenssagemMasterLabel");
                if (lblMensagem != null)
                {
                    lblMensagem.Text = ObjUtilClass.MenssagemErro("Erro ao excluir item: " + ex.Message, true);
                    lblMensagem.Visible = true;
                    lblMensagem.Focus();
                }
            }
        }

        private void LimparCamposItem()
        {
            // Reseta o DropDown de Produto
            drpProduto.SelectedIndex = -1;

            // Limpa os campos de texto
            txtQuantidade.Text = string.Empty;
            txtExSimulador.Text = string.Empty;
            txtSolicitado.Text = string.Empty;
            txtDesconto.Text = string.Empty;

            // Atualiza o UpdatePanel do formulário para refletir a limpeza dos controles
            updFormulario.Update();

            // Script JavaScript essencial para re-renderizar o Bootstrap SelectPicker e limpar o texto visual do combo
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "resetSelectPicker",
                "$('.selectpicker').val('').selectpicker('refresh');",
                true
            );
        }
        #endregion

        #region Navegação
        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            // Mantém a Session viva e retorna para a tela principal de detalhes da negociação
            Response.Redirect("~/Negociacao/NegociacaoDetalheWebForm.aspx?indmnu=3", false);
            Context.ApplicationInstance.CompleteRequest();
        }
        #endregion
    }
}