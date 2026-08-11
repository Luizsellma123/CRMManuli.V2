using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.classes;

namespace VendasWeb.financeiro
{
    public partial class aprovarPedido : System.Web.UI.Page
    {

        //Instancia classe pedido
        funcoes mdlfuncoes = new funcoes();
        funcoesBD mdlfuncoesBD = new funcoesBD();
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

         
            if (!IsPostBack)
            {
                string codOperacao;
                txtNovoHistorico.Text = "";
                codOperacao = mdlCriptografia.Descriptografar(Request.QueryString["idOpe"], "#!$a36?@");
                novoPedido.tipoOperacao = "alteracao";
                carregaConsulta();

                btnAprovar.Attributes.Add("onclick", "javascript:return validaItem();");                
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
            
            lblDescNome.Text = EntNome;
            lblDescFantasia.Text = EntNomeFant;
            lblDescCnpj.Text = EntCpfCgc;
            txtIDEntidade.Text = novoPedido.codigoEntidade;

            lblLimiteCredito.Text = "Limite de Credito: " + novoPedido.ENTVALLIMCRED;
            lblCadastro.Text = "Cadastro: " + novoPedido.EntDataCad;

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
            
            //
            lblFinanceiro.Text = carregaItemsFinanceiro() ;

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

                ltlItems.Text += "<td ><input type=\"Text\" ReadOnly=\"true\" class=\"campo\" name=\"item_" + cont.ToString() + "\" id=\"item_" + cont.ToString() + "\" value=\"" + Math.Round(novoPedido.itemPedidoList[cont].quantidade, 2).ToString() + "\" /></td>";
                ltlItems.Text += "<td class=\"grande\">" + novoPedido.itemPedidoList[cont].descricaoTabela.ToString() + "</td>";
                ltlItems.Text += "<td >" + Math.Round(novoPedido.itemPedidoList[cont].valorItem, 2).ToString() + "</td>";
                ltlItems.Text += "<td >" + Math.Round(novoPedido.itemPedidoList[cont].valorTotal, 2).ToString() + "</td>";
                ltlItems.Text += "<td class=\"tdcentersmall\"><a href=\"#\" class=\"imgedit\"><img src=\"../imagens/comp.png\" alt=\"Alteração\" border=\"0\" onclick=\"javascript: return consultaSaldo('" + cont.ToString() + "')\" /></a></td>";
                ltlItems.Text += "</tr>";

                cont++;
            }
        }

        public string carregaItemsFinanceiro()
        {
            funcoes mdlFuncoes = new funcoes();
            string retorno = "";
            string strSQL = "";

            strSQL += "SELECT A.EMPCOD,";
            strSQL += " case B.DOCFINTIPOLANC  when 'PAG' then 'PAGAR' when 'REC' then 'RECEBER'  end as DOCFINTIPOLANC";
            strSQL += " ,A.PARCDOCFINDUPNUM,CONVERT(nvarchar(10), A.PARCDOCFINDATAEMISSAO, 103) as PARCDOCFINDATAEMISSAO, CONVERT(nvarchar(10), A.PARCDOCFINDATAVENC, 103) as PARCDOCFINDATAVENC, CONVERT(nvarchar(10), A.PARCDOCFINDATAPRORROG, 103) as PARCDOCFINDATAPRORROG,CONVERT(nvarchar(10), A.PARCDOCFINDATAPAG, 103) as PARCDOCFINDATAPAG,";
            strSQL += " DATEDIFF(day,A.PARCDOCFINDATAVENC, A.PARCDOCFINDATAPAG ) as atraso ";
            strSQL += " FROM PARC_DOC_FIN A, DOC_FIN B ";
 
            strSQL += " where (B.EMPCOD = A.EMPCOD AND B.DOCFINCHV = A.DOCFINCHV )and  B.EMPCOD ";
            strSQL += " IN ('1','1.1','1.2','1.3','1.4','1.99','2','2.1') and B.ENTCOD = '" + novoPedido.codigoEntidade + "'";
            strSQL += " AND B.DOCFINPROJECAO = 'Não'";
            strSQL += " order by B.DOCFINTIPOLANC, A.PARCDOCFINDUPNUM";

            SqlConnection dbConnection = new SqlConnection();
            using (dbConnection = new SqlConnection(mdlFuncoes.getString().ToString()))
            {
                using (SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection))
                {
                    dbConnection.Open();
                    using (SqlDataReader drPedido = dbCommand.ExecuteReader())
                    {
                        if (drPedido.HasRows)
                        {
                            //Inicio da tabela
                            retorno += "<table class=\"lstTabela\">";

                            //cabeçario da tabela
                            retorno += "<tr class=\"tabLstCab\">";
                            retorno += "<td>Empresa:</td>";
                            retorno += "<td>Tipo:</td>";
                            retorno += "<td>Documento:</td>";
                            retorno += "<td>Emissão:</td>";
                            retorno += "<td>Vencimento:</td>";
                            retorno += "<td>Prorrogação:</td>";
                            retorno += "<td>Pagamento:</td>";
                            retorno += "<td>Atraso:</td>";

                            retorno += "</tr>";

                            while (drPedido.Read())
                            {
                                retorno += "<td>" + drPedido["EMPCOD"] + "</td>";
                                retorno += "<td>" + drPedido["DOCFINTIPOLANC"] + "</td>";
                                retorno += "<td>" + drPedido["PARCDOCFINDUPNUM"] + "</td>";
                                retorno += "<td>" + drPedido["PARCDOCFINDATAEMISSAO"] + "</td>";
                                retorno += "<td>" + drPedido["PARCDOCFINDATAVENC"] + "</td>";
                                retorno += "<td>" + drPedido["PARCDOCFINDATAPRORROG"] + "</td>";
                                retorno += "<td>" + drPedido["PARCDOCFINDATAPAG"] + "</td>";
                                retorno += "<td>" + drPedido["atraso"] + "</td>";

                                retorno += "</tr>";
                            }

                            //Fim tabela
                            retorno += "</table><br />";
                        }
                    }
                }
            }         
            return retorno;
        }
      
        protected void btnAlteraEntidade_Click(object sender, EventArgs e)
        {
            //Remove Pedido da sessão do usuário
            Session.Remove("novoPedido");

            Response.Write("<script>window.location=\"../listas/lstAprovarPedidos.aspx?indmnu=3\";</script>");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Remove Pedido da sessão do usuário
            Session.Remove("novoPedido");

            Response.Write("<script>window.location=\"../listas/lstAprovarPedidos.aspx?indmnu=3\";</script>");
        }

        protected void btnAprovar_Click(object sender, EventArgs e)
        {
            /*********************************************************             
             Aprovar Pedido             
            *********************************************************/
            
            string erro = "";

            if (erro == "")
            {
                string retValor = mdlfuncoesBD.alteraSatusPedido(novoPedido.codigoEmpresa, novoPedido.numeroPedido, Session["usuario"].ToString(), novoPedido.codigoEntidade.ToString(), "06", "Expedição");

                if (retValor == "")
                {
                    Response.Write("<script>alert(\"Pedido " + novoPedido.numeroPedido.ToString() + " Aprovado com sucesso.\");</script>");

                    Session.Remove("pedidoNovo");
                    Response.Write("<script>window.location=\"../listas/lstAprovarPedidos.aspx?indmnu=3\";</script>");
                }
                else
                {
                    Response.Write("<script>alert(\"Erro ao Aprovar pedido.\");</script>");
                }

            }
            carregaConsulta();
        }

        protected void btnOrcamento_Click(object sender, EventArgs e)
        {
            /*********************************************************
             
                Colocar em Orçamento o Pedido
             
            *********************************************************/

            string erro = "";


            if (erro == "")
            {
                string retValor = mdlfuncoesBD.alteraSatusPedido(novoPedido.codigoEmpresa, novoPedido.numeroPedido, Session["usuario"].ToString(), novoPedido.codigoEntidade.ToString(), "13", "Orçamento");

                if (retValor == "")
                {
                    Response.Write("<script>alert(\"Pedido " + novoPedido.numeroPedido.ToString() + " colocardo em Orçamento com sucesso.\");</script>");

                    Session.Remove("pedidoNovo");
                    Response.Write("<script>window.location=\"../listas/lstAprovarPedidos.aspx?indmnu=3\";</script>");
                }
                else
                {
                    Response.Write("<script>alert(\"Erro ao colocar pedido em Orçamento.\");</script>");
                }

            }
            carregaConsulta();
        }

        protected void btnFaturar_Click(object sender, EventArgs e)
        {

            /*********************************************************
             
            Colocar em Faturar o Pedido
             
            *********************************************************/

            string erro = "";


            if (erro == "")
            {
                string retValor = mdlfuncoesBD.alteraSatusPedido(novoPedido.codigoEmpresa, novoPedido.numeroPedido, Session["usuario"].ToString(), novoPedido.codigoEntidade.ToString(), "07", "Faturar");

                if (retValor == "")
                {

                    Response.Write("<script>alert(\"Pedido " + novoPedido.numeroPedido.ToString() + " colocardo em Orçamento com sucesso.\");</script>");

                    Session.Remove("pedidoNovo");
                    Response.Write("<script>window.location=\"../listas/lstAprovarPedidos.aspx?indmnu=3\";</script>");
                }
                else
                {
                    Response.Write("<script>alert(\"Erro ao colocar pedido em Orçamento.\");</script>");
                }
            }
            carregaConsulta();
        }

        protected void btnSalvarNovoHistorico_Click(object sender, EventArgs e)
        {            
            novoPedido.usuario = Session["usuario"].ToString();
            string data = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            txtHistorico.Text = txtHistorico.Text + "\n\n " + data + " - " + novoPedido.usuario + "\n" + txtNovoHistorico.Text;

            mdlfuncoesBD.atualizaHistorico(novoPedido.codigoEmpresa, novoPedido.numeroPedido, txtHistorico.Text);
            txtNovoHistorico.Text = "";

            Response.Write("<script>alert(\"Novo historico Adcionado.\");</script>");         
        }      
    }
}