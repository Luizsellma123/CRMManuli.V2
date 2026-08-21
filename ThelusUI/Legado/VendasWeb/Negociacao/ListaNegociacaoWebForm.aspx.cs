using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using VendasWeb.GerencialVendas;
using System.Data;
using VendasWeb.classes;

namespace VendasWeb.Negociacao
{
    public partial class ListaNegociacaoWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        FiltroClass ObjFiltroClass = new FiltroClass();
        funcoes mdlfuncoes = new funcoes();
        NegociacaoClasse OBJNegociacao = new NegociacaoClasse();
        criptografia mdlCriptografia = new criptografia();
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        UtilClass ObjUtilClass = new UtilClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            OBJSessao.ValidaAcesso();

            ExibirMensagensSessao();

            if (!IsPostBack)
            {
                InicializarPagina();
            }
            else
            {
                ProcessarAcoesPostBack();
            }
        }

        #region Métodos Auxiliares de Inicialização

        private void InicializarPagina()
        {
            CarregaDatas();

            // HTML estático de layout
            collapseLiteral.Text = "<div id=\"filtros\" class=\"collapse in\" runat=\"server\">";
            Session["EmpCod"] = "";

            // Preenchimento dos componentes
            CarregarEmpresas();
            CarregarStatus();
            CarregarUsuarios();
            CarregarFretes();

            // Restauração do estado/filtros anteriores
            RestaurarFiltrosSessao();
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
        private void CarregarStatus()
        {
            drpListFiltroStat.DataSource = OBJNegociacao.RetornaStatus();
            drpListFiltroStat.DataTextField = "Descricao";
            drpListFiltroStat.DataValueField = "Id";
            drpListFiltroStat.DataBind();

            drpListFiltroStat.Items.Insert(0, new ListItem("Todos", "0"));
            drpListFiltroStat.SelectedIndex = 0;
        }
        private void CarregarUsuarios()
        {
            drpUsuario.DataSource = OBJNegociacao.RetornaUsuarios();
            drpUsuario.DataTextField = "Descricao";
            drpUsuario.DataValueField = "Id";
            drpUsuario.DataBind();

            drpUsuario.Items.Insert(0, new ListItem("Todos", "0"));
            drpUsuario.SelectedIndex = 0;
        }
        private void CarregarFretes()
        {
            drpFrete.DataSource = OBJNegociacao.RetornaFretes();
            drpFrete.DataTextField = "Descricao";
            drpFrete.DataValueField = "Id";
            drpFrete.DataBind();

            drpFrete.Items.Insert(0, new ListItem("Todos", "0"));
            drpFrete.SelectedIndex = 0;
        }
        private void RestaurarFiltrosSessao()
        {
            if (Session["ObjFiltroClass"] == null) return;

            ObjFiltroClass = (FiltroClass)Session["ObjFiltroClass"];
            Session["ObjFiltroClass"] = null; // Limpa para não re-aplicar indevidamente

            if (!string.IsNullOrEmpty(ObjFiltroClass.EmpCod))
            {
                drpEmpresa.SelectedValue = ObjFiltroClass.EmpCod;
            }

            if (drpListFiltroStat.Items.FindByText(ObjFiltroClass.PedVendaStatDescr) != null)
            {
                drpListFiltroStat.SelectedItem.Text = ObjFiltroClass.PedVendaStatDescr;
            }

            // Processa a lista de produtos selecionados no filtro
            if (ObjFiltroClass.itemProdutoList != null && ObjFiltroClass.itemProdutoList.Count > 0)
            {
                List<string> codigos = ObjFiltroClass.itemProdutoList
                    .Select(p => p.codigoProduto)
                    .ToList();

                HiddenFieldListaProdutos.Value = string.Join(",", codigos) + ",";
            }
        }

        #endregion

        #region Métodos Auxiliares de PostBack / Ações

        private void ProcessarAcoesPostBack()
        {
            string acao = TipoHiddenField.Value.Trim();
            if (string.IsNullOrEmpty(acao)) return;

            // Reseta o gatilho da ação
            TipoHiddenField.Value = " ";

            switch (acao)
            {
                case "Consulta":
                    AcaoConsultarPedido();
                    break;

                case "Imprimir":
                    AcaoImprimirPedido("../relatorios/frmCopiaPedido.aspx?indmnu=2");
                    break;

                case "ImprimirSemHist":
                    AcaoImprimirPedido("../relatorios/frmCopiaPedidoSemObs.aspx?indmnu=2");
                    break;
            }
        }
        private void AcaoConsultarPedido()
        {
            ArmazenarDadosSessaoPedido();
            Session["pedidoNovo"] = null;

            // Salva o estado dos filtros para poder retornar depois
            ObjFiltroClass = new FiltroClass
            {
                EmpCod = drpEmpresa.SelectedItem.Value,
                PedVendaStatDescr = drpListFiltroStat.SelectedItem.Text,
                TextoFiltro = txtFiltro.Text
            };

            Session["ObjFiltroClass"] = ObjFiltroClass;

            Response.Redirect("../cadastros/cadPedidoPrincipal.aspx?indmnu=2");
        }

        private void AcaoImprimirPedido(string urlRelatorio)
        {
            ArmazenarDadosSessaoPedido();

            string script = $"window.open('{urlRelatorio}');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect", script, true);
        }

        private void ArmazenarDadosSessaoPedido()
        {
            Session["EmpCod"] = EmpCodHiddenField.Value;
            Session["PedVendaNum"] = PedVendaNumHiddenField.Value;
            Session["Tipo"] = "Consulta";
        }

        #endregion

        protected void CarregaDatas()
        {
            DateTime hoje = DateTime.Today;

            DateTime primeiroDiaDoAno = new DateTime(hoje.Year, 1, 1);

            DataInicialTextBox.Text = primeiroDiaDoAno.ToString("yyyy-MM-dd");

            DataFinalTextBox.Text = hoje.ToString("yyyy-MM-dd");
        }

        protected void ListaNegociacaoGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            ListaNegociacoesGridView.PageIndex = e.NewPageIndex;
            Atualizar_Grid();
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            Atualizar_Grid();
        }


        public void Atualizar_Grid()
        {
            ObjFiltroClass = (FiltroClass)Session["ObjFiltroClass"];

            // 1. Carrega as propriedades do objeto com os valores do form
            ObterFiltrosDaTela();

            // 2. Consulta o banco
            DataTable dtResultado = OBJNegociacao.ConsultarNegociacoesGrid();

            // 2.1 SALVA EM MEMÓRIA: Adicione esta linha para o modal ter acesso aos dados
            ViewState["dtNegociacoes"] = dtResultado;

            // 2.2 Vincula no Grid
            ListaNegociacoesGridView.DataSource = dtResultado;
            ListaNegociacoesGridView.DataBind();

            // 3. Torna a visualização ativa
            NegociacaoMultiView.Visible = true;
            NegociacaoMultiView.ActiveViewIndex = 0;
        }

        // Método acionado ao clicar no botão 'Ver Detalhes' do GridView
        protected void btnVerDetalhe_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idNegociacao = Convert.ToInt32(btn.CommandArgument);

            DataTable dt = ViewState["dtNegociacoes"] as DataTable;

            if (dt != null)
            {
                // Localiza os dados da linha clicada no DataTable carregado
                DataRow[] rows = dt.Select($"IDNegociacao = {idNegociacao}");

                if (rows.Length > 0)
                {
                    DataRow row = rows[0];

                    // Preenche os Labels do Modal
                    lblModalIDNegociacao.Text = row["IDNegociacao"].ToString();
                    lblEmpresa.Text = $"{row["IDEmpresa"]} - {row["NomeEmpresa"]}";
                    lblSolicitante.Text = row["NomeSolicitante"] != DBNull.Value ? row["NomeSolicitante"].ToString() : "-";

                    string codSap = row["CodigoClienteSAP"] != DBNull.Value ? row["CodigoClienteSAP"].ToString() : "";
                    string nomeCli = row["NomeCliente"] != DBNull.Value ? row["NomeCliente"].ToString() : "";
                    lblCliente.Text = string.IsNullOrEmpty(codSap) ? nomeCli : $"{codSap} - {nomeCli}";

                    bool clienteNovo = row["ClienteNovo"] != DBNull.Value && Convert.ToBoolean(row["ClienteNovo"]);
                    lblClienteNovo.Text = clienteNovo ? "SIM" : "NÃO";

                    lblVendedor.Text = row["NomeVendedor"] != DBNull.Value ? row["NomeVendedor"].ToString() : "-";
                    lblCidadeUF.Text = $"{row["NomeCidade"]} / {row["NomeEstado"]}";
                    lblRegimeTributario.Text = row["DescricaoRegime"] != DBNull.Value ? row["DescricaoRegime"].ToString() : "-";

                    lblCondicaoPagamento.Text = row["CondicaoPagamento"] != DBNull.Value ? row["CondicaoPagamento"].ToString() : "-";
                    lblClassificacaoComercial.Text = row["DescricaoClassificacao"] != DBNull.Value ? row["DescricaoClassificacao"].ToString() : "-";
                    lblFrete.Text = row["DescricaoFrete"] != DBNull.Value ? row["DescricaoFrete"].ToString() : "-";
                    lblValidade.Text = row["DescricaoValidade"] != DBNull.Value ? row["DescricaoValidade"].ToString() : "-";

                    // Dispara o JavaScript para abrir o Modal no Front-End
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModalDetalhes", "$('#fullReservaModal').modal('show');", true);
                }
            }
        }

        private void ObterFiltrosDaTela()
        {
            // Empresa
            if (int.TryParse(drpEmpresa.SelectedValue, out int idEmpresa) && idEmpresa > 0)
                OBJNegociacao.IDEmpresa = idEmpresa;
            else
                OBJNegociacao.IDEmpresa = null;

            // Usuário Solicitante
            if (int.TryParse(drpUsuario.SelectedValue, out int idUsuario) && idUsuario > 0)
                OBJNegociacao.IDUsuarioSolicitante = idUsuario;
            else
                OBJNegociacao.IDUsuarioSolicitante = null;

            // Situação / Status
            if (int.TryParse(drpListFiltroStat.SelectedValue, out int idStatus) && idStatus > 0)
                OBJNegociacao.IDStatus = idStatus;
            else
                OBJNegociacao.IDStatus = null;

            // Frete
            if (int.TryParse(drpFrete.SelectedValue, out int idFrete) && idFrete > 0)
                OBJNegociacao.IDFreteNegociacao = idFrete;
            else
                OBJNegociacao.IDFreteNegociacao = null;

            // Número da Negociação
            if (int.TryParse(txtNegociacao.Text, out int idNegociacao) && idNegociacao > 0)
                OBJNegociacao.IDNegociacao = idNegociacao;
            else
                OBJNegociacao.IDNegociacao = null;

            // Data Início
            if (DateTime.TryParse(DataInicialTextBox.Text, out DateTime dataInicio))
                OBJNegociacao.DataInicio = dataInicio;
            else
                OBJNegociacao.DataInicio = null;

            // Data Fim
            if (DateTime.TryParse(DataFinalTextBox.Text, out DateTime dataFim))
                OBJNegociacao.DataFim = dataFim;
            else
                OBJNegociacao.DataFim = null;

            // Cliente (Nome ou Código)
            OBJNegociacao.Cliente = string.IsNullOrWhiteSpace(txtFiltro.Text) ? null : txtFiltro.Text.Trim();
        }

        protected void ListaPedidosGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label SituacaoLabel = e.Row.Cells[0].FindControl("SituaCaoLabel") as Label;

                if (SituacaoLabel.Text == "NVINCULADO")
                {
                    e.Row.BackColor = Color.White;
                    e.Row.ForeColor = Color.OrangeRed;
                }
            }
        }

        protected void IncluirProdutoLinkButton_Click(object sender, EventArgs e)
        {
            ObjFiltroClass = new FiltroClass();
            ObjFiltroClass.EmpCod = drpEmpresa.SelectedItem.Value;
            ObjFiltroClass.PedVendaStatDescr = drpListFiltroStat.SelectedItem.Text;
            //ObjFiltroClass.PedVendaTipo = drpListFiltroTipo.SelectedItem.Text;
            //ObjFiltroClass.DropOpcaoFiltro = drpListFiltroPri.SelectedValue;
            ObjFiltroClass.TextoFiltro = txtFiltro.Text;
            Session["ObjFiltroClass"] = ObjFiltroClass;

            OBJNegociacao = new NegociacaoClasse();

            Response.Redirect("~/listas/FrmListaPedidosProdutos.aspx?indmnu=5");
        }

        protected void CopiaLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";
            ObjFiltroClass = new FiltroClass();
            ObjFiltroClass.EmpCod = drpEmpresa.SelectedItem.Value;
            ObjFiltroClass.PedVendaStatDescr = drpListFiltroStat.SelectedItem.Text;
            //ObjFiltroClass.PedVendaTipo = drpListFiltroTipo.SelectedItem.Text;
            //ObjFiltroClass.DropOpcaoFiltro = drpListFiltroPri.SelectedValue;
            ObjFiltroClass.TextoFiltro = txtFiltro.Text;
            Session["ObjFiltroClass"] = ObjFiltroClass;

            OBJNegociacao = new NegociacaoClasse();
            //OBJNegociacao.PedVendaNum = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;
            //OBJNegociacao.EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            //OBJNegociacao.NumeroPedidoSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("NumeroPedidoSAPLabel")).Text ?? "0");
            //OBJNegociacao.NumeroEsbocoSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("NumeroEsbocoSAPLabel")).Text ?? "0");
            //OBJNegociacao.DescricaoStatus = ((Label)((Control)sender).FindControl("PedVendaStatDescrLabel")).Text;
            //erro = OBJNegociacao.Gera_Copia();

            Session["OBJNegociacao"] = OBJNegociacao;

            if(erro != "")
            {
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                //Retorna Mensagem de Geração
                //((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Novo pedido gerado com número "+ OBJNegociacao.PedVendaNumCopia +".", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

            }
        }

        protected void AtualizarLinkButton_Click(object sender, EventArgs e)
        {
            //OBJNegociacao.PedVendaNum = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;
            //OBJNegociacao.EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            //OBJNegociacao.NumeroPedidoSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("NumeroPedidoSAPLabel")).Text ?? "0");
            //OBJNegociacao.NumeroEsbocoSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("NumeroEsbocoSAPLabel")).Text ?? "0");
            //OBJNegociacao.DescricaoStatus = ((Label)((Control)sender).FindControl("PedVendaStatDescrLabel")).Text;

            //OBJNegociacao.Atualiza_Dados_Pedido_SAP();

            ////Zera número para não interferir no recarregamento da página
            //OBJNegociacao.PedVendaNum = "";
            Atualizar_Grid();
        }

        // Evento do botão "Sel." / "Editar"
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Recupera a chave composta diretamente do DataKeys da linha do Grid
            int idEmpresa = Convert.ToInt32(ListaNegociacoesGridView.DataKeys[row.RowIndex].Values["IDEmpresa"]);
            int idNegociacao = Convert.ToInt32(ListaNegociacoesGridView.DataKeys[row.RowIndex].Values["IDNegociacao"]);

            OBJNegociacao = new NegociacaoClasse();
            if (OBJNegociacao.CarregarNegociacaoPorID(idEmpresa, idNegociacao))
            {
                // Salva a instância completa e populada na Session
                Session["OBJNegociacao"] = OBJNegociacao;

                Response.Redirect("NegociacaoDetalheWebForm.aspx?indmnu=4", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}