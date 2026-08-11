using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.PortalClienteManuli.Pedidos
{
    public partial class InclusaoPedidosWebForm : System.Web.UI.Page
    {
        UsuarioPortalClass OBJusuario = new UsuarioPortalClass();
        UtilClass ObjUtilClass = new UtilClass();

        //Instancia classe pedido
        pedido novoPedido = new pedido();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Seta mensagem para ocultar
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

            //Verifica se tem usuário logado no Portal
            if (Session["usuarioPortal"] == null)
            {
                //Redireciona para tela de login
                Response.Redirect("LoginPortal.aspx");

            }

            if (!IsPostBack)
            {
                //Chama função para carregar dados na tela
                carregaDadosTela();
            }

        }

        public void carregaDadosTela()
        {
            //Recupera usuario da sessão
            OBJusuario = (UsuarioPortalClass)Session["usuarioPortal"];

            //Carrega Empresa
            EmpresaDropDownList.DataSource = OBJusuario.Empresas_Usuario();
            EmpresaDropDownList.DataTextField = "EmpNome";
            EmpresaDropDownList.DataValueField = "EmpCod";
            EmpresaDropDownList.DataBind();

            //Carrega Razão Social Cliente
            RazaoSocialDropDownList.DataSource = OBJusuario.Entidades_Usuario();
            RazaoSocialDropDownList.DataTextField = "EntNome";
            RazaoSocialDropDownList.DataValueField = "EntCod";
            RazaoSocialDropDownList.DataBind();

            //Carrega pedidos na tela
            carregaProdutos();

        }

        public void carregaProdutos()
        {
            DataTable RetornoDados = new DataTable();

            //Recupera usuario da sessão
            OBJusuario = (UsuarioPortalClass)Session["usuarioPortal"];

            //recupera pedidos pendentes
            OBJusuario.EmpCod = EmpresaDropDownList.SelectedValue.ToString();
            OBJusuario.EntCod = RazaoSocialDropDownList.SelectedValue.ToString();

            RetornoDados = OBJusuario.Produtos_Entidade();
            GridViewInclusaoPedidos.DataSource = RetornoDados;
            GridViewInclusaoPedidos.DataBind();
        }

        protected void GridViewInclusaoPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewInclusaoPedidos.PageIndex = e.NewPageIndex;
            carregaProdutos();
        }

        protected void EmpresaDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregaProdutos();
        }

        protected void RazaoSocialDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregaProdutos();
        }

        protected void SalvarButton_Click(object sender, EventArgs e)
        {
            OBJusuario = (UsuarioPortalClass)Session["usuarioPortal"];
            string natureza = "";
            decimal quantidade = 0;
            string erro = "";
            string sucesso = "";
            string MSGLimite = "";

            //Verifica natureza cliente
            natureza = OBJusuario.EntNat;
            if ((natureza == "Contrutora" ||
                    natureza == "Entidade Governamental" ||
                    natureza == "Prestador de Serviços" ||
                    natureza == "Representante" ||
                    natureza == "Consumidor Contribuinte" ||
                    natureza == "Motorista"))
            {
                natureza = "Consumidor";
            }

            foreach (GridViewRow row in GridViewInclusaoPedidos.Rows)
            {
                //Instancia objeto do tipo produto para incluir nos itens
                produto novoProduto = new produto();
                pedido novoPedido = new pedido();

                quantidade = 0;

                if (((TextBox)row.FindControl("QuantidadeTextBox")).Text != "")
                {
                    quantidade = Convert.ToDecimal(((TextBox)row.FindControl("QuantidadeTextBox")).Text);
                }

                if (quantidade != 0)
                {
                    novoPedido.codigoEmpresa = EmpresaDropDownList.SelectedValue.ToString();
                    novoPedido.tipoOperacao = "inclusao";

                    //Tipo do pedido será sempre fixo Total
                    novoPedido.tipo = "Total";
                    novoPedido.dataEmissao = DateTime.Today.ToString("dd/MM/yyyy");
                    novoPedido.dataEntrega = DateTime.Today.ToString("dd/MM/yyyy");
                    //Fixo Emitente
                    novoPedido.tipoFrete = "Emitente";
                    //Fixo Manulifitasa
                    novoPedido.transportadora = "0008647";
                    novoPedido.descricaoTransportadora = "MANULI FITASA DO BRASIL S/A";
                    novoPedido.condicao = ((Label)row.FindControl("CondPagCodLabel")).Text;
                    novoPedido.operacao = "Venda";
                    novoPedido.especie = "Venda";
                    novoPedido.natureza = natureza;
                    novoPedido.embarqueImediato = "Sim";
                    novoPedido.consumo = "Não";
                    novoPedido.PedVendaNumPedEnt = (((TextBox)row.FindControl("NumeroOCTextBox")).Text).ToString();
                    novoPedido.usuario = OBJusuario.UsuarioApolo;
                    novoPedido.codigoEntidade = RazaoSocialDropDownList.SelectedValue.ToString();
                    novoPedido.vendedor = OBJusuario.VendCod;
                    novoPedido.historico = "Pedido Incluido Pelo Portal Clientes.";


                    //Adiciona item no pedido
                    novoProduto.descProduto = ((Label)row.FindControl("DescricaoProdutoLabel")).Text;
                    novoProduto.descricaoProduto = ((Label)row.FindControl("DescricaoProdutoLabel")).Text;
                    novoProduto.unidade = ((Label)row.FindControl("UnidadeLabel")).Text;
                    novoProduto.CompdescricaoProduto = "";
                    novoProduto.quantidade = (float)Convert.ToDecimal(((TextBox)row.FindControl("QuantidadeTextBox")).Text);
                    novoProduto.codigoTabela = ((Label)row.FindControl("TabPVCodLabel")).Text;
                    novoProduto.valorItem = (float)Convert.ToDecimal(((Label)row.FindControl("UnitarioValorLabel")).Text);
                    novoProduto.codigoProduto = ((Label)row.FindControl("CodigoProdutoNumLabel")).Text;
                    novoProduto.numSeq = novoPedido.buscaSequencial();
                    novoProduto.valorOriginal = (float)Convert.ToDecimal(((Label)row.FindControl("UnitarioValorLabel")).Text);
                    novoProduto.ItPedVendaNumSeq = novoProduto.numSeq;

                    novoPedido.incluiItem(novoProduto);

                    //Chama método que grava pedido no banco de dados
                    erro = novoPedido.gravaPedido();

                    if (erro == "")
                    {
                        erro = novoPedido.salvaItens();
                    }

                    if (erro == "")
                    {
                        //Rotina para finalizar cálculos
                        erro = novoPedido.gravaFinalizaPedido();
                    }

                    if (erro == "")
                    {
                        if (sucesso == "")
                        {
                            sucesso += "Pedidos " + novoPedido.numeroPedido;
                        }
                        else
                        {
                            sucesso += ", " + novoPedido.numeroPedido;
                        }
                    }
                }

            }

            if (sucesso != "")
            {
                if(OBJusuario.LimiteDisponivel==0)
                {
                    MSGLimite = "Limite crédito excedido, sujeito análise.";
                }

                string FaltaValores = sucesso + " gravados com sucesso."+ MSGLimite;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(FaltaValores, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

            }
        }
    }
}