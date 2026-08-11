using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using VendasWeb.LogAuditoria.ClassesAuditoria;

namespace VendasWeb.cadastros
{
    public partial class cadPedidoPrincipal : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        funcoes mdlfuncoes = new funcoes();
        funcoesBD mdlFuncoesBD = new funcoesBD();
        criptografia mdlCriptografia = new criptografia();
        enviarEmail mdlMail = new enviarEmail();
        tratamentoLog mdlLog = new tratamentoLog();
        UtilClass ObjUtilClass = new UtilClass();
        LogErroClass OBJLog = new LogErroClass();
        ControladoriaClass OBJControladoria = new ControladoriaClass();

        //Instancia classe pedido
        pedido novoPedido = new pedido();

        protected void Page_Load(object sender, EventArgs e)
        {

            //Valida Acesso
            OBJSessao.ValidaAcesso();

            //Recupera objeto pedido da sessao do usuário
            if (Session["pedidoNovo"] != null)
            {
                novoPedido = (pedido)Session["pedidoNovo"];
                novoPedido.CodigoUsuario = Session["usuario"].ToString();
            }

            //ContentPlaceHolder ph = (ContentPlaceHolder)this.Master.Master.Master.Master.FindControl("ContentPlaceHolder1");

            //Oculta mensagem
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = false;
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
                string codOperacao = "";

                if (Request.QueryString["idOpe"] != null)
                {
                    codOperacao = mdlCriptografia.Descriptografar(Request.QueryString["idOpe"], "#!$a36?@");

                }
                else
                {
                    if (Session["Tipo"] != null)
                        codOperacao = Session["Tipo"].ToString();
                }

                if (codOperacao == "inclusao")
                {
                    novoPedido.tipoOperacao = "inclusao";

                    if (Request.QueryString["idEnt"] != null)
                    {
                        txtDataEmissao.Text = DateTime.Today.ToString("dd/MM/yyyy");

                        lblDescProduto.Visible = false;
                        lblDescricaoProduto.Visible = false;
                        txtCompDescProduto.Visible = false;
                        lblDescUnidade.Visible = false;
                        txtQuantidade.Visible = false;
                        drpTabela.Visible = false;
                        txtValor.Visible = false;
                        btnSalvar.Visible = false;
                        btnGerarCopia.Visible = false;
                        Button1.Visible = false;
                        ltlTotais.Visible = false;
                        txtDataEntrega.Focus();


                        if (novoPedido.PedVendaNumPedEnt != null)
                        {
                            txtPedCliente.Text = novoPedido.PedVendaNumPedEnt.ToString();
                        }


                        //Campos usados na consulta
                        ltlNumPedido.Text = "";

                        //Recupera dados da url
                        novoPedido.codigoEntidade = mdlCriptografia.Descriptografar(Request.QueryString["idEnt"], "#!$a36?@");
                        novoPedido.codigoEmpresa = mdlCriptografia.Descriptografar(Request.QueryString["codEmp"], "#!$a36?@");
                        novoPedido.tipoOperacao = "inclusao";
                        novoPedido.numeroPedido = "0";

                        carregaCabecario();
                    }
                    else
                    {
                        carregaDados();
                    }

                    if (novoPedido.itemPedidoList != null)
                    {
                        carregaItems();
                    }

                    btnSalvar.Attributes.Add("onclick", "javascript:return validaItem();");
                    btnSalvarPedido.Attributes.Add("onclick", "javascript:return validaPedido();");
                }
                else
                {
                    carregaConsulta();
                }
            }
            else
            {
                string idItem = Page.Request["idItem"];

                if (idItem != null && idItem != "")
                {
                    detetaItem(idItem);
                }
            }
        }

        public void carregaCabecario()
        {
            novoPedido.usuario = Session["usuario"].ToString();

            //Parametros de saida Entidade não precisam ser setados
            string EntNome;
            string EntNomeFant;
            string EntCpfCgc;
            string EntNat;
            string EntTranspCod;
            string tipoEntidade;
            string EntRgIe;

            DataTable dadosTable = new DataTable();

            novoPedido.consultaEntidade(novoPedido.codigoEntidade, out EntNome, out EntNomeFant, out EntCpfCgc, out EntNat, out EntTranspCod, out tipoEntidade, out EntRgIe);

            //Tratativa para verifcar se entidade está completa
            if (EntNat == "" && EntNome == "" && EntCpfCgc == "")
            {
                //Seta mensagem de erro.
                Session["Msg"] = "Cliente " + novoPedido.CodigoClienteSAP + " com cadastro incompleto.";
                Response.Redirect("../Entidades/FrmCarteira.aspx?indmnu=2");
            }

            novoPedido.natureza = EntNat;
            lblDescNome.Text = EntNome;
            lblDescFantasia.Text = EntNomeFant;
            lblDescCnpj.Text = EntCpfCgc;
            txtIDEntidade.Text = novoPedido.codigoEntidade;
            txtTransportadora.Text = EntTranspCod;
            txtVendedorCadastrado.Text = novoPedido.VendCadastrado.ToString();
            lblEntRgIe.Text = EntRgIe;


            if (EntTranspCod != "")
            {
                lblDescTransp.Text = mdlfuncoes.Consulta_Nome_Transportadora(EntTranspCod);
            }
            else
            {
                lblDescTransp.Text = "";
            }

            //Descricao Empresa
            lblDescEmpresa.Text = novoPedido.consultaDescrEmpresa(novoPedido.codigoEmpresa, Session["usuario"].ToString());

            //Consulta Condicao de Pagamento
            drpCondPag.DataSource = mdlfuncoes.Consulta_Condicao_Pagamento(novoPedido.codigoEntidade.ToString(), novoPedido.codigoEmpresa);
            drpCondPag.DataTextField = "NomeCondicao";
            drpCondPag.DataValueField = "IDCondPag";
            drpCondPag.DataBind();

            //Carrega Tabela de Preço
            string vendCod = novoPedido.consultaVendedor(novoPedido.codigoEntidade);

            //Carregado datatable com tabelas de preços do vendedor
            dadosTable = mdlfuncoes.Consulta_Tab_PV_Vendedor(novoPedido.codigoEmpresa.ToString(), vendCod.ToString());

            // Passando a tabela para o combo
            drpTabela.DataSource = dadosTable;
            drpTabela.DataTextField = "Nome";
            drpTabela.DataValueField = "IDTabela";
            drpTabela.DataBind();

            Session["Tabela"] = "";
            int tstCont = 0;

            //Utilizando a tabela carregada anteriormente
            if (dadosTable.Rows.Count > 0)
            {
                foreach (DataRow row in dadosTable.Rows)
                {
                    if (tstCont == 0)
                    {
                        Session["Tabela"] += "('" + row["IDTabela"].ToString() + "'";
                        tstCont++;
                    }
                    else
                    {
                        Session["Tabela"] += ", '" + row["IDTabela"].ToString() + "'";
                    }
                }

                Session["Tabela"] += ")";
            }
            else
            {
                Session["Tabela"] = "('')";
            }

            novoPedido.tabela = Session["Tabela"].ToString();

            Session["vendCod"] = vendCod;

            novoPedido.vendedor = vendCod;

            //Alterar para tratar SAP

            drpOperacao1.DataSource = mdlfuncoes.Consulta_Operacao_Ped_Venda();
            drpOperacao1.DataTextField = "NomeOperacao";
            drpOperacao1.DataValueField = "IDOperacao";
            drpOperacao1.DataBind();
            /*
            drpEspecie.DataSource = mdlfuncoes.Consulta_Especie_Ped_Venda();
            drpEspecie.DataTextField = "TIPOFATESPECIE";
            drpEspecie.DataValueField = "TIPOFATESPECIE";
            drpEspecie.DataBind();
            */

            drpDestinacao.DataSource = mdlfuncoes.Consulta_Natureza_Destinacao(novoPedido.codigoEntidade);
            drpDestinacao.DataTextField = "Nome";
            drpDestinacao.DataValueField = "IDNaturezaDestinacao";
            drpDestinacao.DataBind();
            drpDestinacao.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione", ""));

            drpStatus.DataSource = mdlfuncoes.Consulta_ListaStatus_Ped_Venda();
            drpStatus.DataTextField = "DescricaoStatus";
            drpStatus.DataValueField = "IDStatus";
            drpStatus.DataBind();


            drpNatureza.SelectedValue = EntNat;
            /*
            if (novoPedido.codigoEmpresa != "99")
            {
                drpOperacao1.SelectedValue = "Venda";
                drpEspecie.SelectedValue = "Venda";
            }
            else
            {
                drpOperacao1.SelectedValue = "Venda";
                drpEspecie.SelectedValue = "Venda";
            }
            */

            drpStatus.SelectedValue = "1";
            drpStatus.Enabled = false;
            drpTipo.Enabled = false;
            drpNatureza.Enabled = false;
            novoPedido.tipoEntidade = tipoEntidade;
        }

        protected void btnAlteraEntidade_Click(object sender, EventArgs e)
        {
            //Remove objeto da memória pois será instanciado novo
            Session.Remove("pedidoNovo");

            //Response.Write("<script>window.location=\"../Entidades/FrmCarteira.aspx?indmnu=2\";</script>");
            Response.Redirect("../Entidades/FrmCarteira.aspx?indmnu=2");
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {

            novoPedido.tipo = drpTipo.SelectedItem.Value;
            novoPedido.dataEmissao = txtDataEmissao.Text;
            novoPedido.dataEntrega = txtDataEntrega.Text;
            novoPedido.tipoFrete = drpTipoFrete.SelectedItem.Value;
            novoPedido.transportadora = txtTransportadora.Text;
            novoPedido.descricaoTransportadora = lblDescTransp.Text;
            novoPedido.condicao = drpCondPag.SelectedItem.Value;
            novoPedido.operacao = drpOperacao1.SelectedItem.Value;
            //novoPedido.especie = drpEspecie.SelectedItem.Value;
            novoPedido.natureza = drpNatureza.SelectedItem.Value;
            novoPedido.embarqueImediato = drpEmbarque.SelectedItem.Value;
            novoPedido.consumo = drpDestinacao.SelectedItem.Value;
            novoPedido.PedVendaNumPedEnt = txtPedCliente.Text;

            if (txtValorFrete.Text != null && txtValorFrete.Text != "")
            {
                novoPedido.valorFrete = (float)Convert.ToDecimal(txtValorFrete.Text);
            }

            Session["pedidoNovo"] = novoPedido;

            //Response.Write("<script>window.location=\"../cadastros/cadPedidoItem.aspx?indmnu=2\";</script>");
            Response.Redirect("../cadastros/cadPedidoItem.aspx?indmnu=2");
            /*
            else
            {   
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Selecione se o pedido é para consumo.", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            */
        }

        public void carregaDados()
        {

            if (Request.QueryString["idProd"] != null)
            {
                lblDescProduto.Visible = true;
                lblDescricaoProduto.Visible = true;
                txtCompDescProduto.Visible = true;
                lblDescUnidade.Visible = true;
                txtQuantidade.Visible = true;
                drpTabela.Visible = true;
                txtValor.Visible = true;
                txtPosicao.Visible = true;
                btnSalvar.Visible = true;
                if (novoPedido.tipoOperacao == "inclusao")
                {
                    btnGerarCopia.Visible = false;
                    Button1.Visible = false;
                }
            }
            else
            {
                lblDescProduto.Visible = false;
                lblDescricaoProduto.Visible = false;
                txtCompDescProduto.Visible = false;
                lblDescUnidade.Visible = false;
                txtQuantidade.Visible = false;
                drpTabela.Visible = false;
                txtValor.Visible = false;
                txtPosicao.Visible = false;
                btnSalvar.Visible = false;
                if (novoPedido.tipoOperacao == "inclusao")
                {
                    btnGerarCopia.Visible = false;
                    Button1.Visible = false;
                }
            }

            string codProduto;
            string codTabela;
            string valProduto;
            string codUnidade;
            string codTransp;

            //Carrega o cabecario
            carregaCabecario();

            if (novoPedido.PedVendaNumPedEnt != null)
            {
                txtPedCliente.Text = novoPedido.PedVendaNumPedEnt.ToString();
            }

            //drpTipo.SelectedValue = novoPedido.tipo.ToString();
            if (novoPedido.dataEntrega != null && novoPedido.dataEntrega != "")
            {
                txtDataEntrega.Text = Convert.ToDateTime(novoPedido.dataEntrega).ToString("yyyy-MM-dd");
            }
            else
            {
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Escolha uma data de entrega", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            txtDataEmissao.Text = novoPedido.dataEmissao.ToString();
            //drpOperacao.SelectedValue = novoPedido.operacao.ToString();
            drpTipoFrete.SelectedValue = novoPedido.tipoFrete.ToString();
            txtTransportadora.Text = novoPedido.transportadora.ToString();
            lblDescTransp.Text = novoPedido.descricaoTransportadora ?? "";
            drpCondPag.SelectedValue = novoPedido.condicao.ToString();
            drpOperacao1.SelectedValue = novoPedido.operacao.ToString();
            //drpEspecie.SelectedValue = novoPedido.especie.ToString();
            drpNatureza.SelectedValue = novoPedido.natureza.ToString();
            drpEmbarque.SelectedValue = novoPedido.embarqueImediato.ToString();
            drpDestinacao.SelectedValue = novoPedido.consumo.ToString();

            if (novoPedido.valorFrete != 0)
            {
                txtValorFrete.Text = (string)Convert.ToString(novoPedido.valorFrete);
            }

            if (Request.QueryString["idProd"] != null)
            {
                //Recupera dados do produto a ser trabalhado
                codProduto = mdlCriptografia.Descriptografar(Request.QueryString["idProd"], "#!$a36?@");
                codTabela = mdlCriptografia.Descriptografar(Request.QueryString["idTab"], "#!$a36?@");
                valProduto = mdlCriptografia.Descriptografar(Request.QueryString["idVal"], "#!$a36?@");
                codUnidade = mdlCriptografia.Descriptografar(Request.QueryString["idUn"], "#!$a36?@");

                lblDescProduto.Text = mdlfuncoes.Consulta_CodNome_Produto(codProduto);
                lblDescricaoProduto.Text = mdlfuncoes.Consulta_Nome_Produto(codProduto);

                lblProdutoAux.Text = codProduto;
                lblDescUnidade.Text = codUnidade;
                drpTabela.SelectedValue = codTabela;
                txtValor.Text = valProduto;
                txtQuantidade.Focus();

                txtPedCliente.Text = novoPedido.PedVendaNumPedEnt.ToString();
                //txtPedCliente.Enabled = false;

                //Recupera valor do produto original para gravar tabela USER_TB_USER_tb_Pedido_Bloqueado_ITens
                idValorOriginal.Value = valProduto;

            }
            else
            {
                if (Request.QueryString["idTra"] != null)
                {
                    //Recupera dados da transportadora
                    codTransp = mdlCriptografia.Descriptografar(Request.QueryString["idTra"], "#!$a36?@");
                    txtTransportadora.Text = codTransp;

                    //Atribui código da transportadora ao objeto pedido
                    novoPedido.transportadora = codTransp;

                    lblDescTransp.Text = mdlfuncoes.Consulta_Nome_Transportadora(novoPedido.transportadora.ToString());
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            ParametroGeral objParametroGeral = new ParametroGeral();

            string OBRIGAITEMPEDIDOCLIENTE = objParametroGeral.RetornaValorStringParametro("OBRIGAITEMPEDIDOCLIENTE");

            if (txtPosicao.Text == "" && OBRIGAITEMPEDIDOCLIENTE == "Sim")
            {
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Obrigatório informar a posição do produto.", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                //Instancia objeto do tipo produto para incluir nos itens
                produto novoProduto = new produto();
                novoProduto.descProduto = lblDescProduto.Text;
                novoProduto.descricaoProduto = lblDescricaoProduto.Text;
                novoProduto.unidade = lblDescUnidade.Text;
                novoProduto.CompdescricaoProduto = txtCompDescProduto.Text;
                novoProduto.quantidade = (float)Convert.ToDecimal(txtQuantidade.Text);
                novoProduto.codigoTabela = drpTabela.SelectedItem.Value;
                novoProduto.valorItem = (float)Convert.ToDecimal(txtValor.Text);
                novoProduto.codigoProduto = lblProdutoAux.Text;
                novoProduto.numSeq = novoPedido.buscaSequencial();
                novoProduto.valorOriginal = (float)Convert.ToDecimal(idValorOriginal.Value);
                novoProduto.ItPedVendaNumSeq = Convert.ToInt32(txtPosicao.Text);
                novoProduto.xPed = txtPedCliente.Text.ToString();
                novoProduto.nItem = txtPosicao.Text.ToString();

                if (txtValorFrete.Text != null && txtValorFrete.Text != "")
                {
                    novoPedido.valorFrete = (float)Convert.ToDecimal(txtValorFrete.Text);
                }

                novoPedido.incluiItem(novoProduto);

                Session["tstItem"] = true;

                //Limpa Variaveis
                lblDescProduto.Text = "";
                lblDescricaoProduto.Text = "";
                lblDescUnidade.Text = "";
                txtQuantidade.Text = "";
                txtValor.Text = "";
                lblProdutoAux.Text = "";
                idValorOriginal.Value = "";

                lblDescProduto.Visible = false;
                lblDescricaoProduto.Visible = false;
                txtCompDescProduto.Visible = false;
                lblDescUnidade.Visible = false;
                txtQuantidade.Visible = false;
                drpTabela.Visible = false;
                txtValor.Visible = false;
                btnSalvar.Visible = false;
                txtPosicao.Visible = false;

                carregaItems();
            }
        }

        public void carregaItems()
        {
            int quant;
            int cont = 0;
            ltlItems.Text = "";

            quant = novoPedido.numeroItens();

            while (cont < quant && quant > 0)
            {
                ltlItems.Text += "<tr>";
                if (novoPedido.statusPedio == "1" || novoPedido.tipoOperacao == "inclusao")
                {
                    ltlItems.Text += "<td align=\"center\"><a href=\"#\"><img src=\"../imagens/delete.png\" alt=\"delete\" border=\"0\" onclick=\"javascript: return fdelete('" + cont.ToString() + "')\" /></a></td>";
                }
                else
                {
                    ltlItems.Text += "<td></td>";
                }

                ltlItems.Text += "<td class=\"texto\">" + novoPedido.itemPedidoList[cont].codigoProduto + " - " + novoPedido.itemPedidoList[cont].nomeProduto + "</td>";
                ltlItems.Text += "<td class=\"texto\">" + novoPedido.itemPedidoList[cont].unidade.ToString() + "</td>";
                ltlItems.Text += "<td >" + Math.Round(novoPedido.itemPedidoList[cont].quantidade, 2).ToString() + "</td>";
                ltlItems.Text += "<td class=\"grande\">" + novoPedido.itemPedidoList[cont].descricaoTabela.ToString() + "</td>";
                ltlItems.Text += "<td >" + Math.Round(novoPedido.itemPedidoList[cont].valorItem, 2).ToString() + "</td>";
                ltlItems.Text += "<td >" + novoPedido.itemPedidoList[cont].ItPedVendaNumSeq.ToString() + "</td>";
                ltlItems.Text += "<td >" + Math.Round(novoPedido.itemPedidoList[cont].valorTotal, 2).ToString() + "</td>";
                ltlItems.Text += "<td align=\"center\"><a href=\"../cadastros/cadPedidoListaArte.aspx?indmnu=2&idexItem=" + mdlCriptografia.Criptografar(cont.ToString(), "#!$a36?@") + "\"><i class=\"fa fa-cogs fa-2x\"></i></a></td>";
                ltlItems.Text += "</tr>";

                cont++;
            }
            /*if (quant > 0)
            {
                drpConsumo.Enabled = false;
            }
            else 
            {
                drpConsumo.Enabled = true;
            }*/
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            string veioCRM = novoPedido.veioCRM;

            //Remove Pedido da sessão do usuário
            Session.Remove("novoPedido");

            if (veioCRM == "sim")
            {
                //Response.Write("<script>window.location=\"../Entidades/FrmCarteira.aspx?indmnu=2\";</script>");
                Response.Redirect("../Entidades/FrmCarteira.aspx?indmnu=2");
            }
            else
            {
                //Response.Write("<script>window.location=\"../listas/FrmListaPedidos.aspx?indmnu=2\";</script>");
                Response.Redirect("../listas/FrmListaPedidos.aspx?indmnu=2");
            }
        }

        public void detetaItem(string idItem)
        {
            novoPedido.removeItem((int)Convert.ToInt32(idItem));

            carregaItems();
        }

        protected void btnSalvarPedido_Click(object sender, EventArgs e)
        {
            int cont = 0;
            int quant = 0;
            string erroCliche = "";

            ParametroGeral objParametroGeral = new ParametroGeral();

            string OBRIGAPEDIDOCLIENTE = objParametroGeral.RetornaValorStringParametro("OBRIGAPEDIDOCLIENTE");

            if (txtPedCliente.Text == "" && OBRIGAPEDIDOCLIENTE == "Sim")
            {
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Obrigatório informar o número da OC.", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else if (drpDestinacao.SelectedItem.Value == "")
            {
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Obrigatório selecionar Destinação pedido.", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                if (drpEmbarque.SelectedItem.Value == "Selecione")
                {
                    //Retorna Mensagem de Erro
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Selecione se o pedido é embarque imediato.", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                }
                else
                if (drpDestinacao.SelectedItem.Value == null)
                {
                    //Retorna Mensagem de Erro
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Selecione se o pedido é para consumo.", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                }
                else
                if (novoPedido.itemPedidoList != null)
                {
                    if (novoPedido.itemPedidoList.Count != 0)
                    {
                        quant = novoPedido.numeroItens();

                        while (cont < quant && quant > 0)
                        {
                            if (novoPedido.itemPedidoList[cont].obrigaCliche == "SIM")
                            {
                                if (novoPedido.itemPedidoList[cont].compItemPedidoList == null || novoPedido.itemPedidoList[cont].compItemPedidoList.Count == 0)
                                {
                                    erroCliche += "Produto:" + novoPedido.itemPedidoList[cont].codigoProduto + " sem cliche informado.\\n";
                                }
                            }
                            cont++;
                        }

                        if (erroCliche == "")
                        {
                            if (txtDataEntrega.Text != null && txtDataEntrega.Text != "")
                            {
                                //if (mdlfuncoes.FormataDataComparacao(txtDataEntrega.Text) >= mdlfuncoes.FormataDataComparacao(DateTime.Today.ToString("dd/MM/yyyy")))
                                if (Convert.ToDateTime(txtDataEntrega.Text) >= DateTime.Today)
                                {
                                    gravarPedido();
                                }
                                else
                                {
                                    //Retorna Mensagem de Erro
                                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Data entrega deve ser igual ou superior data atual.", true);
                                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                                }
                            }
                            else
                            {
                                //Retorna Mensagem de Erro
                                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Escolha uma data de entrega", true);
                                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                            }
                        }
                        else
                        {
                            //Retorna Mensagem de Erro
                            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(erroCliche.ToString(), true);
                            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                        }
                    }
                    else
                    {

                        //Retorna Mensagem de Erro
                        ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Informe ao menos um item.", true);
                        ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                        ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                    }
                }
                else
                {
                    //Retorna Mensagem de Erro
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Informe ao menos um item.", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                }
            }
        }

        public void gravarPedido()
        {
            //Validação para tratar se pedido está dentro da data de saída
            DataTable DataControladoria = new DataTable();
            OBJControladoria.EmpCod = novoPedido.codigoEmpresa;
            DataControladoria = OBJControladoria.Consulta_Periodos();
            DateTime DataInicial = new DateTime();
            DateTime DataFinal = new DateTime();

            string erro = "";

            if (DataControladoria.Rows.Count > 0)
            {
                foreach (DataRow row in DataControladoria.Rows)
                {
                    DataInicial = Convert.ToDateTime(row["DataInicial"]);
                    DataFinal = Convert.ToDateTime(row["DataFinal"]);
                }
            }

            if (Convert.ToDateTime(txtDataEntrega.Text) >= DataInicial && Convert.ToDateTime(txtDataEntrega.Text) <= DataFinal)
            {

                novoPedido.tipo = drpTipo.SelectedItem.Value;
                novoPedido.dataEmissao = txtDataEmissao.Text;
                novoPedido.dataEntrega = txtDataEntrega.Text;
                novoPedido.tipoFrete = drpTipoFrete.SelectedItem.Value;
                novoPedido.transportadora = txtTransportadora.Text;
                novoPedido.descricaoTransportadora = lblDescTransp.Text;
                novoPedido.condicao = drpCondPag.SelectedItem.Value;
                novoPedido.operacao = drpOperacao1.SelectedItem.Value;
                //novoPedido.especie = drpEspecie.SelectedItem.Value;
                novoPedido.natureza = drpNatureza.SelectedItem.Value;
                novoPedido.embarqueImediato = drpEmbarque.SelectedItem.Value;
                novoPedido.consumo = drpDestinacao.SelectedItem.Value;
                novoPedido.PedVendaNumPedEnt = txtPedCliente.Text;

                if ((drpNatureza.SelectedItem.Value == "Contrutora" ||
                    drpNatureza.SelectedItem.Value == "Entidade Governamental" ||
                    drpNatureza.SelectedItem.Value == "Prestador de Serviços" ||
                    drpNatureza.SelectedItem.Value == "Representante" ||
                    drpNatureza.SelectedItem.Value == "Consumidor Contribuinte" ||
                    drpNatureza.SelectedItem.Value == "Motorista") && lblEntRgIe.Text == "")

                {
                    novoPedido.natureza = "Consumidor";
                }

                if (txtValorFrete.Text != null && txtValorFrete.Text != "")
                {
                    novoPedido.valorFrete = (float)Convert.ToDecimal(txtValorFrete.Text);
                }

                //Chama método que grava pedido no banco de dados
                //erro = novoPedido.gravaPedido();
                erro = novoPedido.gravaPedidoCRM();


                if (erro == "")
                {
                    erro = novoPedido.salvaItens();
                }

                //Seta mensagens de erro da gravação dentro do CRM
                if (erro != "")
                {
                    //Seta mensagem de erro.
                    Session["Msg"] = "Erro na gravacao do Pedido: " + novoPedido.IDPedido.ToString() + ".";
                }

                if (erro == "")
                {
                    //Rotina para enviar pedido para o SAP
                    erro = novoPedido.EnviaPedidoSAP();

                    if (erro != "")
                    {
                        Session["MSg"] = erro;
                    }
                }

                if (erro == "")
                {
                    Session["Msg"] = "Pedido: " + novoPedido.IDPedido.ToString() + " Gravado com sucesso";
                    //Retorna Mensagem de Erro
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Pedido: " + novoPedido.numeroPedido.ToString() + " Gravado com sucesso", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                    string caminho = Server.MapPath("~");
                    mdlLog.gravaLogPedido(caminho, novoPedido);

                    Session.Remove("novoPedido");

                    if (Session["Retornar"] != null)
                    {
                        Response.Redirect(Session["Retornar"].ToString());
                    }
                    else
                    {
                        //Response.Write("<script>alert(\"Não foi informado a página de origem. Entrar em contato com a TI\");</script>");
                        //Response.Write("<script>window.location=\"../listas/FrmListaPedidos.aspx?indmnu=2\";</script>");
                        Response.Redirect("../listas/FrmListaPedidos.aspx?indmnu=2");
                    }
                }
                else
                {
                    mdlMail.enviaEmail("Erro na gravacao do Pedido: " + novoPedido.IDPedido.ToString() + " .", "Pedido: " + novoPedido.IDPedido.ToString() + " Gravado com sucesso", "luiz.carlos@manulifitasa.com.br");

                    string caminho = Server.MapPath("~");
                    mdlLog.gravaLogPedido(caminho, novoPedido);

                    //Retorna Mensagem de Erro
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(erro.ToString(), true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                }

                /*Msg de Erro*/
                if (erro != "")
                {
                    //Retorna Mensagem de Erro
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(erro.ToString(), true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                }
            }
            else
            {
                erro = "Data de saída do pedido está fora do perído permitido. Verificar com controladoria.";
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(erro.ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        public void carregaConsulta()
        {
            string pedVendaNum = "";
            string empCod = "";
            string erro = "";

            btnAlteraEntidade.Visible = false;

            if (Session["EmpCod"] != null)
                empCod = Session["EmpCod"].ToString();

            if (Session["PedVendaNum"] != null)
                pedVendaNum = Session["PedVendaNum"].ToString();

            if (novoPedido.numeroPedido == "0" && novoPedido.tipoOperacao != "inclusao")
            {
                erro = novoPedido.carregaDadosPedido(empCod, pedVendaNum);
            }

            if (erro == "")
            {
                if (novoPedido.statusPedio.ToString() == "1")
                {
                    txtDataEmissao.Text = DateTime.Today.ToString("dd/MM/yyyy");

                    lblDescProduto.Visible = false;
                    lblDescricaoProduto.Visible = false;
                    txtCompDescProduto.Visible = false;
                    lblDescUnidade.Visible = false;
                    txtQuantidade.Visible = false;
                    drpTabela.Visible = false;
                    txtValor.Visible = false;
                    btnSalvar.Visible = false;
                    //txtPedCliente.Enabled = false;

                    carregaDados();
                    btnAprovar.Visible = true;
                    btnCancelarPedido.Visible = true;
                    novoPedido.tipoOperacao = "alteracao";

                    ltlNumPedido.Text = "<span class=\"texto\">Pedido:&nbsp</span>";
                    ltlNumPedido.Text += "<span class=\"texto\">" + novoPedido.numeroPedido.ToString() + "</span><br>";
                }
                else
                {
                    dadosConsulta();
                    btnAprovar.Visible = false;
                    btnCancelarPedido.Visible = false;
                    novoPedido.tipoOperacao = "consulta";
                }

                carregaItems();
            }

            //Caso de algum erro retorna para tela de lista apontando erro identificado
            if (erro != "")
            {
                Session.Remove("pedidoNovo");
                Session["Msg"] = erro;
                Response.Redirect("../listas/FrmListaPedidos.aspx?indmnu=2");
            }

            Session["pedidoNovo"] = novoPedido;
        }

        public void dadosConsulta()
        {
            PedidoClass OBJPedidoClass = new PedidoClass();

            if (Session["PedidoClass"] != null)
            {
                OBJPedidoClass = (PedidoClass)Session["PedidoClass"];
            }

            string codEntidade = "";
            string tranpEnt = "";

            //Carrega dados entidade
            string EntNome;
            string EntNomeFant;
            string EntCpfCgc;
            string EntNat;
            string EntTranspCod;
            string tipoEntidade;
            string EntRgIe;
            double ICMSDevido;
            double ICMSDiferido;

            novoPedido.consultaEntidade(novoPedido.codigoEntidade, out EntNome, out EntNomeFant, out EntCpfCgc, out EntNat, out EntTranspCod, out tipoEntidade, out EntRgIe);
            txtVendedorCadastrado.Text = novoPedido.VendCadastrado.ToString();
            novoPedido.natureza = EntNat;

            btnSearch.Visible = false;
            ltlTotais.Visible = true;

            //Esconde botão Salvar
            btnSalvarPedido.Visible = false;
            btnGerarCopia.Visible = true;
            Button1.Visible = true;

            ltlNumPedido.Text = "<span class=\"texto\">Pedido:&nbsp</span>";
            ltlNumPedido.Text += "<span class=\"texto\">" + novoPedido.numeroPedido.ToString() + "</span><br>";

            //Busca dados do pedido
            lblDescEmpresa.Text = novoPedido.consultaDescrEmpresa(novoPedido.codigoEmpresa, Session["usuario"].ToString());

            //Busca dados Pedido Principal
            codEntidade = novoPedido.codigoEntidade.ToString();
            txtDataEmissao.Text = novoPedido.dataEmissao.ToString();
            txtDataEntrega.Text = novoPedido.dataEntrega.ToString();
            //drpStatus.Items.Add(mdlfuncoes.ExecutaSqlReader("select StatPedVendaDescr from STAT_PED_VENDA where StatPedVendaCod='" + novoPedido.statusPedio.ToString() + "'", "dadosConsulta"));
            drpStatus.Items.Add(mdlfuncoes.ExecutaSqlReader("select DescricaoStatus from CRM_STATUS_PEDIDOS WHERE IDStatus='" + novoPedido.statusPedio.ToString() + "'", "dadosConsulta"));
            drpStatus.SelectedIndex = 0;
            //drpTipo.SelectedValue = novoPedido.tipo.ToString();
            drpNatureza.SelectedValue = novoPedido.natureza.ToString();
            drpTipoFrete.Items.Add(novoPedido.tipoFrete.ToString());
            drpTipoFrete.SelectedIndex = 0;

            tranpEnt = novoPedido.transportadora.ToString();
            //ICMSDevido = novoPedido.consultaICMSDevido(novoPedido.codigoEmpresa, novoPedido.numeroPedido.ToString());
            //ICMSDiferido = novoPedido.consultaICMSDiferido(novoPedido.codigoEmpresa, novoPedido.numeroPedido.ToString());
            ICMSDevido = 0;
            ICMSDiferido = 0;

            ltlTotais.Text = "<br><table><tr><td class=\"texto\">Valor Mercadoria:</td><td class=\"textoR\">" + OBJPedidoClass.PedVendaValMerc ?? "0,00" + "</td></tr>";
            ltlTotais.Text += "<tr><td class=\"texto\">Valor do IPI:</td><td class=\"textoR\">" + OBJPedidoClass.PedVendaValIpiCalc ?? "0,00" + "</td></tr>";
            ltlTotais.Text += "<tr><td class=\"texto\">Valor do ICMS:</td><td class=\"textoR\">" + OBJPedidoClass.PedVendaValIcms ?? "0,00" + "</td></tr>";
            ltlTotais.Text += "<tr><td class=\"texto\">Valor do Diferimento:</td><td class=\"textoR\">" + OBJPedidoClass.IcmsDiferido ?? "0,00" + "</td></tr>";
            ltlTotais.Text += "<tr><td class=\"texto\">Valor do ICMS Devido:</td><td class=\"textoR\">" + OBJPedidoClass.IcmsDevido ?? "0,00" + "</td></tr>";
            ltlTotais.Text += "<tr><td class=\"texto\">Total do Pedido:</td><td class=\"textoR\">" + OBJPedidoClass.PedVendaValTotal ?? "0,00" + "</td></tr>";
            ltlTotais.Text += "</table>";

            Session["codEntidade"] = codEntidade;

            drpStatus.Enabled = false;
            drpTipo.Enabled = false;
            drpNatureza.Enabled = false;
            drpTipoFrete.Enabled = false;
            txtDataEmissao.Enabled = false;
            txtDataEntrega.Enabled = false;
            txtTransportadora.Enabled = false;

            if (novoPedido.valorFrete != 0)
            {
                txtValorFrete.Text = (string)Convert.ToString(novoPedido.valorFrete);
            }

            drpOperacao1.DataSource = mdlfuncoes.Consulta_Operacao_Ped_Venda();
            drpOperacao1.DataTextField = "NomeOperacao";
            drpOperacao1.DataValueField = "IDOperacao";
            drpOperacao1.DataBind();

            drpDestinacao.DataSource = mdlfuncoes.Consulta_Natureza_Destinacao(novoPedido.codigoEntidade);
            drpDestinacao.DataTextField = "Nome";
            drpDestinacao.DataValueField = "IDNaturezaDestinacao";
            drpDestinacao.DataBind();

            drpStatus.DataSource = mdlfuncoes.Consulta_ListaStatus_Ped_Venda();
            drpStatus.DataTextField = "DescricaoStatus";
            drpStatus.DataValueField = "IDStatus";
            drpStatus.DataBind();

            drpTipoFrete.SelectedValue = novoPedido.tipoFrete;
            drpOperacao1.SelectedValue = novoPedido.operacao.ToString();
            drpOperacao1.Enabled = false;
            //drpOperacao1.SelectedIndex = 0;
            //drpEspecie.Items.Add(novoPedido.especie.ToString());
            //drpEspecie.SelectedIndex = 0;
            drpEmbarque.SelectedValue = novoPedido.embarqueImediato.ToString();
            drpDestinacao.SelectedValue = novoPedido.consumo.ToString();

            drpOperacao1.Enabled = false;
            drpEspecie.Enabled = false;

            drpCondPag.Items.Add(novoPedido.nomeCondicao.ToString());
            drpCondPag.SelectedIndex = 0;
            drpCondPag.Enabled = false;
            drpDestinacao.Enabled = false;
            drpEmbarque.Enabled = false;

            novoPedido.tipoEntidade = tipoEntidade;
            lblDescNome.Text = EntNome;
            lblDescFantasia.Text = EntNomeFant;
            lblDescCnpj.Text = EntCpfCgc;
            txtIDEntidade.Text = novoPedido.codigoEntidade;
            txtTransportadora.Text = EntTranspCod;
            lblEntRgIe.Text = EntRgIe;

            //Carrega dados entidade Transportador
            novoPedido.consultaEntidade(novoPedido.transportadora.ToString(), out EntNome, out EntNomeFant, out EntCpfCgc, out EntNat, out EntTranspCod, out tipoEntidade, out EntRgIe);

            txtTransportadora.Text = novoPedido.transportadora.ToString();
            lblDescTransp.Text = EntNome;

            //Items
            lblDescProduto.Visible = false;
            lblDescricaoProduto.Visible = false;
            txtCompDescProduto.Visible = false;
            lblDescUnidade.Visible = false;
            txtQuantidade.Visible = false;
            drpTabela.Visible = false;
            txtValor.Visible = false;
            btnSalvar.Visible = false;
            btnGerarCopia.Visible = true;
            Button1.Visible = true;
            btnAlteraEntidade.Visible = false;
            btnIncluir.Visible = false;
            drpDestinacao.SelectedValue = novoPedido.consumo.ToString();


            txtPedCliente.Text = novoPedido.PedVendaNumPedEnt.ToString();
            //txtPedCliente.Enabled = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            novoPedido.tipo = drpTipo.SelectedItem.Value;
            novoPedido.dataEmissao = txtDataEmissao.Text;
            novoPedido.dataEntrega = txtDataEntrega.Text;
            novoPedido.tipoFrete = drpTipoFrete.SelectedItem.Value;
            novoPedido.transportadora = txtTransportadora.Text;
            novoPedido.descricaoTransportadora = lblDescTransp.Text;
            novoPedido.condicao = drpCondPag.SelectedItem.Value;
            novoPedido.operacao = drpOperacao1.SelectedItem.Value;
            //novoPedido.especie = drpEspecie.SelectedItem.Value;
            novoPedido.natureza = drpNatureza.SelectedItem.Value;
            novoPedido.embarqueImediato = drpEmbarque.SelectedItem.Value;
            novoPedido.consumo = drpDestinacao.SelectedItem.Value;
            novoPedido.PedVendaNumPedEnt = txtPedCliente.Text;

            if (txtValorFrete.Text != null && txtValorFrete.Text != "")
            {
                novoPedido.valorFrete = (float)Convert.ToDecimal(txtValorFrete.Text);
            }
            //Response.Write("<script>window.location=\"../listas/lstTransportadora.aspx?indmnu=2\";</script>");
            Response.Redirect("../listas/lstTransportadora.aspx?indmnu=2");
        }

        protected void btnGerarCopia_Click(object sender, EventArgs e)
        {
            Session.Remove("pedidoNovo");
            /*string empCod = Request.QueryString["idEmp"];
            string pedVendaNum = Request.QueryString["idPed"];
            string codOperacao = Request.QueryString["idOpe"];*/

            //Response.Write("<script>window.location=\"../relatorios/frmCopiaPedido.aspx?indmnu=2&idEmp=" + Session["EmpCod"].ToString() + "&idPed=" + Session["PedVendaNum"].ToString() + "&idOpe=" + Session["Tipo"].ToString() + "   \";</script>");
            Response.Redirect("../relatorios/frmCopiaPedido.aspx?indmnu=2&idEmp=" + Session["EmpCod"].ToString() + "&idPed=" + Session["PedVendaNum"].ToString() + "&idOpe=" + Session["Tipo"].ToString());
        }

        protected void btnAprovar_Click(object sender, EventArgs e)
        {
            GerencialVendas.PedidoClass PedidoClass = new GerencialVendas.PedidoClass();
            string retValor = "";

            if (novoPedido.NumeroEsbocoSAP == "" || novoPedido.NumeroEsbocoSAP == null || novoPedido.NumeroEsbocoSAP == "0")
            {
                retValor = novoPedido.EnviaPedidoSAP();

                //Recupera impostos do pedido
                if (retValor == "")
                {
                    PedidoClass.PedVendaNum = novoPedido.codigoEmpresa;
                    PedidoClass.EmpCod = novoPedido.numeroPedido;
                    PedidoClass.NumeroPedidoSAP = Convert.ToInt32(novoPedido.NumeroPedidoSAP ?? "0");
                    PedidoClass.NumeroEsbocoSAP = Convert.ToInt32(novoPedido.NumeroEsbocoSAP ?? "0");
                    PedidoClass.Consulta_Pedido();
                }
            }

            if (retValor == "")
            {
                retValor = mdlFuncoesBD.aprovaPedido(novoPedido.codigoEmpresa, novoPedido.numeroPedido, Session["usuario"].ToString(), novoPedido.codigoEntidade.ToString());

                if (retValor == "")
                {
                    retValor = novoPedido.TransformaEsbocoPedido();

                    //Atualiza historico pedido
                    if (novoPedido.NumeroPedidoSAP != "" && novoPedido.NumeroPedidoSAP != null && novoPedido.NumeroPedidoSAP != "0")
                    {
                        retValor = novoPedido.AtualizarHistoricoPedidoSAPAPI();
                    }
                }

                if (retValor == "")
                {
                    Session.Remove("pedidoNovo");
                    if (novoPedido.NumeroPedidoSAP != "" && novoPedido.NumeroPedidoSAP != null && novoPedido.NumeroPedidoSAP != "0")
                    {
                        Session["Msg"] = "Pedido aprovado com sucesso.";
                    }
                    else
                    {
                        Session["Msg"] = "Pedido enviado para análise.";
                    }

                    //Retorna Mensagem de Erro
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Pedido aprovado com sucesso.", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                    //Response.Write("<script>window.location=\"../listas/FrmListaPedidos.aspx?indmnu=2\";</script>");
                    Response.Redirect("../listas/FrmListaPedidos.aspx?indmnu=2");
                }
                else
                {
                    Session["Msg"] = retValor;
                    //Retorna Mensagem de Erro
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Erro ao aprovar pedido", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                }
            }
            else
            {
                Session["Msg"] = retValor;
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Erro ao enviar pedido para o SAP.", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            Session.Remove("pedidoNovo");

            /*string empCod = Request.QueryString["idEmp"];
            string pedVendaNum = Request.QueryString["idPed"];
            string codOperacao = Request.QueryString["idOpe"];*/

            //Response.Write("<script>window.location=\"../relatorios/frmCopiaPedidoSemObs.aspx?indmnu=2&idEmp=" + Session["EmpCod"].ToString() + "&idPed=" + Session["PedVendaNum"].ToString() + "&idOpe=" + Session["Tipo"].ToString() + "   \";</script>");
            Response.Redirect("../relatorios/frmCopiaPedidoSemObs.aspx?indmnu=2&idEmp=" + Session["EmpCod"].ToString() + "&idPed=" + Session["PedVendaNum"].ToString() + "&idOpe=" + Session["Tipo"].ToString());
        }

        protected void btnDadosComplementares_Click(object sender, EventArgs e)
        {
            novoPedido.tipo = drpTipo.SelectedItem.Value;
            novoPedido.dataEmissao = txtDataEmissao.Text;
            novoPedido.dataEntrega = txtDataEntrega.Text;
            novoPedido.tipoFrete = drpTipoFrete.SelectedItem.Value;
            novoPedido.transportadora = txtTransportadora.Text;
            novoPedido.descricaoTransportadora = lblDescTransp.Text;
            novoPedido.condicao = drpCondPag.SelectedItem.Value;
            novoPedido.operacao = drpOperacao1.SelectedItem.Value;
            //novoPedido.especie = drpEspecie.SelectedItem.Value;
            novoPedido.natureza = drpNatureza.SelectedItem.Value;
            novoPedido.embarqueImediato = drpEmbarque.SelectedItem.Value;
            novoPedido.consumo = drpDestinacao.SelectedItem.Value;
            novoPedido.PedVendaNumPedEnt = txtPedCliente.Text;

            if (txtValorFrete.Text != null && txtValorFrete.Text != "")
            {
                novoPedido.valorFrete = (float)Convert.ToDecimal(txtValorFrete.Text);
            }

            Session["pedidoNovo"] = novoPedido;

            //Response.Write("<script>window.location=\"cadPedidoTexto.aspx?indmnu=2\";</script>");
            Response.Redirect("cadPedidoTexto.aspx?indmnu=2");
        }

        //Cancelar um pedido somente quando esta em orçamento
        protected void Button2_Click(object sender, EventArgs e)
        {
            string retValor = "";

            if (drpStatus.SelectedValue == "1")
            {
                retValor = novoPedido.cancelaOrcamento();

                if (retValor == "")
                {
                    Session.Remove("pedidoNovo");

                    //Retorna Mensagem de Erro
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Pedido cancelado com sucesso.", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                    Session["Msg"] = "Pedido " + novoPedido.numeroPedido.ToString() + " cancelado com sucesso.";
                    //Response.Write("<script>window.location=\"../listas/FrmListaPedidos.aspx?indmnu=2\";</script>");
                    Response.Redirect("../listas/FrmListaPedidos.aspx?indmnu=2");
                }
                else
                {
                    //Retorna Mensagem de Erro
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Erro ao cancelar pedido.", true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                }
            }
            else
            {
                //Retorna Mensagem de Erro
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Pedido não está em orçamento, cancelar no SAP.", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void txtTransportadora_TextChanged(object sender, EventArgs e)
        {

        }

        protected void drpTipoFrete_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpTipoFrete.SelectedItem.Value == "5")
            {
                txtTransportadora.Text = "";
                lblDescTransp.Text = "";
            }
        }
    }
}