using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.logistica
{
    public partial class alteracaoPedido : System.Web.UI.Page
    {
        //Instancia classe pedido
        funcoes mdlfuncoes = new funcoes();
        funcoesBD mdlFuncoesBD = new funcoesBD();
        pedido novoPedido = new pedido();
        criptografia mdlCriptografia = new criptografia();
        enviarEmail mdlMail = new enviarEmail();
        tratamentoLog mdlLog = new tratamentoLog();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            //Recupera objeto pedido da sessao do usuário
            if (Session["pedidoNovo"] != null)
            {
                novoPedido = (pedido)Session["pedidoNovo"];
            }
            //Deixa botao invisivel, vai que voltam a tras.
            btnProjeto.Visible = false;

            if (!IsPostBack)
            {
                string codOperacao;

                codOperacao = mdlCriptografia.Descriptografar(Request.QueryString["idOpe"], "#!$a36?@");
                novoPedido.tipoOperacao = "alteracao";

                carregaConsulta();

                btnSalvarPedido.Attributes.Add("onclick", "javascript:return validaItem();");
                txtQuantidade.Focus();
            }
            else
            {
                string idItem = Page.Request["idItem"];

                if (idItem != null && idItem != "")
                {
                    salvaItems(idItem);
                }
            }
        }

        public void carregaConsulta()
        {
            string pedVendaNum = "";
            string empCod = "";

            empCod = mdlCriptografia.Descriptografar(Request.QueryString["idEmp"], "#!$a36?@");
            pedVendaNum = mdlCriptografia.Descriptografar(Request.QueryString["idPed"], "#!$a36?@");

            if (novoPedido.numeroPedido == "0" && novoPedido.tipoOperacao != "inclusao")
            {
                novoPedido.carregaDadosPedido(empCod, pedVendaNum);
                novoPedido.carregaDadosListaAnterior();
            }

            carregaDados();

            ltlNumPedido.Text = "<span class=\"texto\">Pedido:&nbsp</span>";
            ltlNumPedido.Text += "<span class=\"texto\">" + novoPedido.numeroPedido.ToString() + "</span><br>";
        }

        public void carregaDados()
        {
            //Carrega o cabecario
            carregaCabecario();
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

            novoPedido.natureza = EntNat;

            novoPedido.tipoEntidade = tipoEntidade;
            txtHistorico.Text = novoPedido.historicoAntigo;
            txtNovoHistorico.Text = novoPedido.historico;
            lblDescNome.Text = EntNome;
            lblDescFantasia.Text = EntNomeFant;
            lblDescCnpj.Text = EntCpfCgc;
            txtIDEntidade.Text = novoPedido.codigoEntidade;

            //Descricao Empresa
            lblDescEmpresa.Text = novoPedido.consultaDescrEmpresa(novoPedido.codigoEmpresa, Session["usuario"].ToString());
            txtTipo.Text = novoPedido.tipo.ToString();
            txtStatus.Text = mdlfuncoes.Consulta_Status_Ped_Venda(novoPedido.codigoEmpresa.ToString(), novoPedido.numeroPedido.ToString());
            txtDataEntrega.Text = novoPedido.dataEntrega.ToString();
            txtDataEmissao.Text = novoPedido.dataEmissao.ToString();
            txtNatureza.Text = novoPedido.natureza.ToString();
            txtOperacao.Text = novoPedido.operacao.ToString();
            txtEspecie.Text = novoPedido.especie.ToString();
            txtCondicao.Text = novoPedido.nomeCondicao.ToString();
            txtTipFrete.Text = novoPedido.tipoFrete.ToString();
            txtValorFrete.Text = novoPedido.valorFrete.ToString();
            txtQuantidade.Text = Convert.ToString(novoPedido.QuantidadeVolumes.ToString());
            txtCodTransportadora.Text = novoPedido.transportadora.ToString();
            lblDescNomeTransportadora.Text = novoPedido.descricaoTransportadora.ToString();
            txtEspecieVolume.Text = Convert.ToString(novoPedido.EspecieVolume.ToString());
            txtPesoLiquido.Text = Convert.ToString(novoPedido.PesoLiquido.ToString());
            txtPesoBruto.Text = Convert.ToString(novoPedido.PesoBruto.ToString());

            txtImbarqueImediato.Text = novoPedido.embarqueImediato.ToString();

            //função para carregar itens
            carregaItems();

            Session["pedidoNovo"] = novoPedido;
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
                if (novoPedido.statusPedio == "13" || novoPedido.tipoOperacao == "inclusao")
                {
                    ltlItems.Text += "<td align=\"center\"><a href=\"#\"><img src=\"../imagens/delete.png\" alt=\"delete\" border=\"0\" onclick=\"javascript: return fdelete('" + cont.ToString() + "')\" /></a></td>";
                }

                ltlItems.Text += "<td class=\"texto\">" + novoPedido.itemPedidoList[cont].codigoProduto + " - " + novoPedido.itemPedidoList[cont].nomeProduto + "</td>";
                ltlItems.Text += "<td class=\"texto\">" + novoPedido.itemPedidoList[cont].unidade.ToString() + "</td>";

                ltlItems.Text += "<td ><input type=\"Text\" class=\"campo\" name=\"item_" + cont.ToString() + "\" id=\"item_" + cont.ToString() + "\" value=\"" + Math.Round(novoPedido.itemPedidoList[cont].quantidade, 2).ToString() + "\" /></td>";
                ltlItems.Text += "<td class=\"grande\">" + novoPedido.itemPedidoList[cont].descricaoTabela.ToString() + "</td>";
                ltlItems.Text += "<td >" + Math.Round(novoPedido.itemPedidoList[cont].valorItem, 2).ToString() + "</td>";
                ltlItems.Text += "<td >" + Math.Round(novoPedido.itemPedidoList[cont].valorTotal, 2).ToString() + "</td>";
                ltlItems.Text += "<td class=\"tdcentersmall\"><a href=\"#\" class=\"imgedit\"><img src=\"../imagens/comp.png\" alt=\"Alteração\" border=\"0\" onclick=\"javascript: return consultaSaldo('" + cont.ToString() + "')\" /></a></td>";
                ltlItems.Text += "</tr>";

                cont++;
            }
        }

        public void salvaItems(string idItem)
        {
            int quant;
            int cont = 0;
            string codigoProduto;

            ltlItems.Text = "";
            novoPedido.historico = txtNovoHistorico.Text;
            novoPedido.EspecieVolume = (string)txtEspecieVolume.Text.ToString();

            if (txtPesoLiquido.Text != "")
            {
                novoPedido.PesoLiquido = (double)Convert.ToDouble(txtPesoLiquido.Text);
            }
            else
            {
                novoPedido.PesoLiquido = 0;
            }

            if (txtPesoBruto.Text != "")
            {
                novoPedido.PesoBruto = (double)Convert.ToDouble(txtPesoBruto.Text);
            }
            else
            {
                novoPedido.PesoBruto = 0;
            }

            if (txtQuantidade.Text != "")
            {
                novoPedido.QuantidadeVolumes = (double)Convert.ToDouble(txtQuantidade.Text);
            }
            else
            {
                novoPedido.QuantidadeVolumes = 0;
            }

            quant = novoPedido.numeroItens();

            while (cont < quant && quant > 0)
            {
                if (Request.Form["item_" + cont.ToString()].ToString() != "")
                {
                    novoPedido.itemPedidoList[cont].quantidade = (double)Convert.ToDouble(Request.Form["item_" + cont.ToString()].ToString());
                }
                else
                {
                    novoPedido.itemPedidoList[cont].quantidade = 0;
                }

                novoPedido.itemPedidoList[cont].valorTotal = novoPedido.itemPedidoList[cont].calculaTotal(novoPedido.itemPedidoList[cont].valorItem, novoPedido.itemPedidoList[cont].quantidade);

                cont++;
            }

            Session["pedidoNovo"] = novoPedido;

            codigoProduto = novoPedido.itemPedidoList[(int)Convert.ToInt32(idItem)].codigoProduto;

            Response.Write("<script>window.location=\"../logistica/alteracaoPedidoConsultaSaldo.aspx?indmnu=3&idOpe=" + mdlCriptografia.Criptografar("alteracao", "#!$a36?@") + "&idProd=" + mdlCriptografia.Criptografar(codigoProduto, "#!$a36?@") + " \";</script>");
        }

        protected void btnSalvarPedido_Click(object sender, EventArgs e)
        {
            string erro = "";

            //Grava dados do histórico
            novoPedido.historico = txtNovoHistorico.Text;
            novoPedido.EspecieVolume = txtEspecieVolume.Text;

            if (txtQuantidade.Text != "")
            {
                novoPedido.QuantidadeVolumes = (double)Convert.ToDouble(txtQuantidade.Text);
            }
            else
            {
                novoPedido.QuantidadeVolumes = 0;
            }

            if (txtPesoLiquido.Text != "")
            {
                novoPedido.PesoLiquido = (double)Convert.ToDouble(txtPesoLiquido.Text);
            }
            else
            {
                novoPedido.PesoLiquido = 0;
            }

            if (txtPesoBruto.Text != "")
            {
                novoPedido.PesoBruto = (double)Convert.ToDouble(txtPesoBruto.Text);
            }
            else
            {
                novoPedido.PesoBruto = 0;
            }

            //Caso valor do peso bruto não seja informado assume o mesmo do líquido
            if (novoPedido.PesoBruto == 0)
            {
                novoPedido.PesoBruto = novoPedido.PesoLiquido;
            }

            ltlItems.Text = "";

            int quant;
            int cont = 0;

            quant = novoPedido.numeroItens();

            while (cont < quant && quant > 0)
            {
                if (Request.Form["item_" + cont.ToString()].ToString() != "")
                {
                    novoPedido.itemPedidoList[cont].quantidade = (double)Convert.ToDouble(Request.Form["item_" + cont.ToString()].ToString());
                }
                else
                {
                    novoPedido.itemPedidoList[cont].quantidade = 0;
                }

                novoPedido.itemPedidoList[cont].valorTotal = novoPedido.itemPedidoList[cont].calculaTotal(novoPedido.itemPedidoList[cont].valorItem, novoPedido.itemPedidoList[cont].quantidade);

                cont++;
            }

            if (erro == "")
            {
                erro = salvaPedido();
            }
            else
            {
                Response.Write("<script>alert(\"" + erro.ToString() + "\");</script>");
            }

            if (erro == "")
            {

                if (erro == "")
                {
                    //Envai email para faturamento conforme enviado para faturamento
                    Response.Write("<script>alert(\"Pedido " + novoPedido.numeroPedido.ToString() + " salvo com sucesso.\");</script>");
                }
                else
                {
                    Response.Write("<script language=\"javascript\">alert(\"" + erro.ToString() + "\");</script>");
                }
            }

            carregaConsulta();
        }

        protected void btnAlteraEntidade_Click(object sender, EventArgs e)
        {
            //Remove Pedido da sessão do usuário
            Session.Remove("novoPedido");

            Response.Write("<script>window.location=\"../listas/lstPedidosLogistica.aspx?indmnu=3\";</script>");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Remove Pedido da sessão do usuário
            Session.Remove("novoPedido");

            Response.Write("<script>window.location=\"../listas/lstPedidosLogistica.aspx?indmnu=3\";</script>");
        }

        protected void btnGerarCopia_Click(object sender, EventArgs e)
        {

            string empCod = mdlCriptografia.Criptografar(novoPedido.codigoEmpresa.ToString(), "#!$a36?@");
            string pedVendaNum = mdlCriptografia.Criptografar(novoPedido.numeroPedido.ToString(), "#!$a36?@");
            string codOperacao = mdlCriptografia.Criptografar(novoPedido.tipoOperacao.ToString(), "#!$a36?@");
            Session.Remove("pedidoNovo");

            Response.Write("<script>window.location=\"../relatorios/frmCopiaPedido.aspx?indmnu=3&idEmp=" + empCod.ToString() + "&idPed=" + pedVendaNum.ToString() + "&idOpe=" + codOperacao.ToString() + "   \";</script>");
        }

        protected void btnAprovar_Click(object sender, EventArgs e)
        {
            funcoesBD mdlFuncoes = new funcoesBD();

            int quant;
            int cont = 0;
            string erro = "";
            double vlrQuantidade = 0;

            //Grava dados do histórico
            novoPedido.historico = txtNovoHistorico.Text;
            novoPedido.EspecieVolume = txtEspecieVolume.Text;

            if (txtQuantidade.Text != "")
            {
                novoPedido.QuantidadeVolumes = (double)Convert.ToDouble(txtQuantidade.Text);
            }
            else
            {
                novoPedido.QuantidadeVolumes = 0;
            }

            if (txtPesoLiquido.Text != "")
            {
                novoPedido.PesoLiquido = (double)Convert.ToDouble(txtPesoLiquido.Text);
            }
            else
            {
                novoPedido.PesoLiquido = 0;
            }

            if (txtPesoBruto.Text != "")
            {
                novoPedido.PesoBruto = (double)Convert.ToDouble(txtPesoBruto.Text);
            }
            else
            {
                novoPedido.PesoBruto = 0;
            }

            //Caso valor do peso bruto não seja informado assume o mesmo do líquido
            if (novoPedido.PesoBruto == 0)
            {
                novoPedido.PesoBruto = novoPedido.PesoLiquido;
            }
            quant = novoPedido.numeroItens();

            while (cont < quant && quant > 0)
            {
                if (Request.Form["item_" + cont.ToString()].ToString() != "")
                {
                    vlrQuantidade = (double)Convert.ToDouble(Request.Form["item_" + cont.ToString()].ToString());
                }
                else
                {
                    vlrQuantidade = 0;
                }

                if (Request.Form["item_" + cont.ToString()].ToString() != "")
                {
                    novoPedido.itemPedidoList[cont].quantidade = (double)Convert.ToDouble(Request.Form["item_" + cont.ToString()].ToString());
                }
                else
                {
                    novoPedido.itemPedidoList[cont].quantidade = 0;
                }

                novoPedido.itemPedidoList[cont].valorTotal = novoPedido.itemPedidoList[cont].calculaTotal(novoPedido.itemPedidoList[cont].valorItem, novoPedido.itemPedidoList[cont].quantidade);

                cont++;
            }

            if (erro == "")
            {
                erro = salvaPedido();

                if (erro == "")
                {
                    string retValor = mdlFuncoes.alteraSatusPedido(novoPedido.codigoEmpresa, novoPedido.numeroPedido, Session["usuario"].ToString(), novoPedido.codigoEntidade.ToString(), "07", "Faturar");

                    if (retValor == "")
                    {
                        //Envai email para faturamento conforme enviado para faturamento
                        Response.Write("<script>alert(\"Pedido " + novoPedido.numeroPedido.ToString() + " encaminhado para faturamento com sucesso.\");</script>");

                        Session.Remove("pedidoNovo");
                        Response.Write("<script>window.location=\"../listas/lstPedidosLogistica.aspx?indmnu=3\";</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert(\"Erro ao passar pedido para faturar pedido.\");</script>");
                    }
                }
                else
                {
                    //Retorna erro para o Usuario
                    Response.Write("<script language=\"javascript\">alert(\"" + erro.ToString() + "\");</script>");
                }
            }
            else
            {
                //Retorna erro para o Usuario
                Response.Write("<script language=\"javascript\">alert(\"" + erro.ToString() + "\");</script>");
            }

            carregaConsulta();
        }

        protected void btnProjeto_Click(object sender, EventArgs e)
        {
            funcoesBD mdlFuncoes = new funcoesBD();
            int quant;
            string retValor = "";

            //Grava dados do histórico
            novoPedido.historico = txtNovoHistorico.Text;
            novoPedido.EspecieVolume = txtEspecieVolume.Text;

            if (txtQuantidade.Text != "")
            {
                novoPedido.QuantidadeVolumes = (double)Convert.ToDouble(txtQuantidade.Text);
            }
            else
            {
                novoPedido.QuantidadeVolumes = 0;
            }

            if (txtPesoLiquido.Text != "")
            {
                novoPedido.PesoLiquido = (double)Convert.ToDouble(txtPesoLiquido.Text);
            }
            else
            {
                novoPedido.PesoLiquido = 0;
            }

            if (txtPesoBruto.Text != "")
            {
                novoPedido.PesoBruto = (double)Convert.ToDouble(txtPesoBruto.Text);
            }
            else
            {
                novoPedido.PesoBruto = 0;
            }

            //Caso valor do peso bruto não seja informado assume o mesmo do líquido
            if (novoPedido.PesoBruto == 0)
            {
                novoPedido.PesoBruto = novoPedido.PesoLiquido;
            }

            retValor = salvaPedido();

            if (retValor == "")
            {
                quant = novoPedido.numeroItens();
                retValor = mdlFuncoes.alteraSatusPedido(novoPedido.codigoEmpresa, novoPedido.numeroPedido, Session["usuario"].ToString(), novoPedido.codigoEntidade.ToString(), "15", "Projeto");

                if (retValor == "")
                {
                    Session.Remove("pedidoNovo");
                    Response.Write("<script>alert(\"Pedido encaminhado para Projeto com sucesso.\");</script>");
                    Response.Write("<script>window.location=\"../listas/lstPedidosLogistica.aspx?indmnu=3\";</script>");
                }
                else
                {
                    Response.Write("<script>alert(\"Erro ao passar pedido para Projeto.\");</script>");
                }
                carregaConsulta();
            }
        }

        public string salvaPedido()
        {
            string erro = "";

            //Chama método que grava pedido no banco de dados
            erro = novoPedido.gravaPedido();

            if (erro == "")
            {
                if (novoPedido.tipoOperacao == "alteracao")
                {
                    erro = novoPedido.testaRegraItens();
                }
            }

            //Se não der erro na gravação do pedido grava itens
            if (erro == "")
            {
                erro = novoPedido.salvaItens();
            }

            if (erro == "")
            {
                //Rotina para finalizar cálculos
                erro = novoPedido.gravaFinalizaPedido();
            }

            string caminho = Server.MapPath("~");

            if (erro == "")
            {
                Session.Remove("novoPedido");
            }
            else
            {
                //Envia Email de confirmação
                mdlMail.enviaEmail("Erro na gravacao do Pedido Logística: " + novoPedido.numeroPedido.ToString() + " .", "Pedido: " + novoPedido.numeroPedido.ToString() + " Gravado com sucesso", "luiz.carlos@manulifitasa.com.br");
                mdlLog.gravaLogPedido(caminho, novoPedido);
            }

            return erro;
        }

        protected void btnProgramar_Click(object sender, EventArgs e)
        {
            /*********************************************************
             
             Colocar o Pedido para Programado
             
            *********************************************************/

            int quant;
            int cont = 0;
            string erro = "";
            double vlrQuantidade = 0;

            //Grava dados do histórico
            novoPedido.historico = txtNovoHistorico.Text;
            novoPedido.EspecieVolume = txtEspecieVolume.Text;

            if (txtQuantidade.Text != "")
            {
                novoPedido.QuantidadeVolumes = (double)Convert.ToDouble(txtQuantidade.Text);
            }
            else
            {
                novoPedido.QuantidadeVolumes = 0;
            }

            if (txtPesoLiquido.Text != "")
            {
                novoPedido.PesoLiquido = (double)Convert.ToDouble(txtPesoLiquido.Text);
            }
            else
            {
                novoPedido.PesoLiquido = 0;
            }

            if (txtPesoBruto.Text != "")
            {
                novoPedido.PesoBruto = (double)Convert.ToDouble(txtPesoBruto.Text);
            }
            else
            {
                novoPedido.PesoBruto = 0;
            }

            //Caso valor do peso bruto não seja informado assume o mesmo do líquido
            if (novoPedido.PesoBruto == 0)
            {
                novoPedido.PesoBruto = novoPedido.PesoLiquido;
            }

            quant = novoPedido.numeroItens();

            while (cont < quant && quant > 0)
            {
                if (Request.Form["item_" + cont.ToString()].ToString() != "")
                {
                    vlrQuantidade = (double)Convert.ToDouble(Request.Form["item_" + cont.ToString()].ToString());
                }
                else
                {
                    vlrQuantidade = 0;
                }

                if (Request.Form["item_" + cont.ToString()].ToString() != "")
                {
                    novoPedido.itemPedidoList[cont].quantidade = (double)Convert.ToDouble(Request.Form["item_" + cont.ToString()].ToString());
                }
                else
                {
                    novoPedido.itemPedidoList[cont].quantidade = 0;
                }

                novoPedido.itemPedidoList[cont].valorTotal = novoPedido.itemPedidoList[cont].calculaTotal(novoPedido.itemPedidoList[cont].valorItem, novoPedido.itemPedidoList[cont].quantidade);

                cont++;
            }


            if (erro == "")
            {
                erro = salvaPedido();

                if (erro == "")
                {
                    string retValor = mdlFuncoesBD.alteraSatusPedido(novoPedido.codigoEmpresa, novoPedido.numeroPedido, Session["usuario"].ToString(), novoPedido.codigoEntidade.ToString(), "03", "Programado");

                    if (retValor == "")
                    {
                        Response.Write("<script>alert(\"Pedido " + novoPedido.numeroPedido.ToString() + " encaminhado para Programado com sucesso.\");</script>");

                        Session.Remove("pedidoNovo");
                        Response.Write("<script>window.location=\"../listas/lstPedidosLogistica.aspx?indmnu=3\";</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert(\"Erro ao passar pedido para Programado pedido.\");</script>");
                    }
                }
                else
                {
                    //Retorna erro para o Usuario
                    Response.Write("<script language=\"javascript\">alert(\"" + erro.ToString() + "\");</script>");
                }
            }
            else
            {
                //Retorna erro para o Usuario
                Response.Write("<script language=\"javascript\">alert(\"" + erro.ToString() + "\");</script>");
            }

            carregaConsulta();
        }

        protected void btnProducao_Click(object sender, EventArgs e)

        {
            funcoesBD mdlFuncoes = new funcoesBD();
            int quant;
            string retValor = "";

            //Grava dados do histórico
            novoPedido.historico = txtNovoHistorico.Text;
            novoPedido.EspecieVolume = txtEspecieVolume.Text;

            if (txtQuantidade.Text != "")
            {
                novoPedido.QuantidadeVolumes = (double)Convert.ToDouble(txtQuantidade.Text);
            }
            else
            {
                novoPedido.QuantidadeVolumes = 0;
            }

            if (txtPesoLiquido.Text != "")
            {
                novoPedido.PesoLiquido = (double)Convert.ToDouble(txtPesoLiquido.Text);
            }
            else
            {
                novoPedido.PesoLiquido = 0;
            }

            if (txtPesoBruto.Text != "")
            {
                novoPedido.PesoBruto = (double)Convert.ToDouble(txtPesoBruto.Text);
            }
            else
            {
                novoPedido.PesoBruto = 0;
            }

            //Caso valor do peso bruto não seja informado assume o mesmo do líquido
            if (novoPedido.PesoBruto == 0)
            {
                novoPedido.PesoBruto = novoPedido.PesoLiquido;
            }

            retValor = salvaPedido();

            if (retValor == "")
            {
                quant = novoPedido.numeroItens();
                retValor = mdlFuncoes.alteraSatusPedido(novoPedido.codigoEmpresa, novoPedido.numeroPedido, Session["usuario"].ToString(), novoPedido.codigoEntidade.ToString(), "09", "Produção");

                if (retValor == "")
                {
                    Session.Remove("pedidoNovo");
                    Response.Write("<script>alert(\"Pedido encaminhado para Projeto com sucesso.\");</script>");
                    Response.Write("<script>window.location=\"../listas/lstPedidosLogistica.aspx?indmnu=3\";</script>");
                }
                else
                {
                    Response.Write("<script>alert(\"Erro ao passar pedido para Projeto.\");</script>");
                }
                carregaConsulta();
            }
        }
    }
}