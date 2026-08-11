using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.cadastros
{
    public partial class PedidoHistoricoWebForm : System.Web.UI.Page
    {
        PedidoClass OBJPedidoClass = new PedidoClass();
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        pedido OBJPedido = new pedido();
        enviarEmail OBJMail = new enviarEmail();

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

            //Recupera objeto pedido da sessao do usuário
            if (Session["PedidoClass"] != null)
            {
                OBJPedidoClass = (PedidoClass)Session["PedidoClass"];
            }

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
            }

        }

        public void CarregaDadosNaTela()
        {
            lblDescNome.Text = OBJPedidoClass.EntNome;
            //lblDescFantasia.Text = drEntidade["NomeFantasia"].ToString();
            lblDescCnpj.Text = OBJPedidoClass.EntCpfCgc;
            txtIDEntidade.Text = OBJPedidoClass.EntCod;
            ltlNumPedido.Text = OBJPedidoClass.PedVendaNum;
            lblDescEmpresa.Text = OBJPedidoClass.EmpCod;
            lblDescEmpresa.Text = OBJPedidoClass.EmpNome;

            txtNovoHistorico.Text = "";
            txtHistorico.Text = OBJPedidoClass.PedVendaTextoHist.ToString();
        }

        protected void btnAlteraEntidade_Click(object sender, EventArgs e)
        {
            OBJPedidoClass.Consulta_Pedido();
            Session["PedidoClass"] = OBJPedidoClass;
            Response.Redirect("../cadastros/FrmPedidoDetalhe.aspx?indmnu=5");
        }

        protected void SalvarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            OBJPedido.codigoEmpresa = OBJPedidoClass.EmpCod;
            OBJPedido.IDPedido = Convert.ToInt32(OBJPedidoClass.PedVendaNum);
            OBJPedido.historicoAntigo = OBJPedidoClass.PedVendaTextoHist;
            OBJPedido.CodigoUsuario = Session["usuario"].ToString();
            OBJPedido.historico = txtNovoHistorico.Text;

            erro = OBJPedido.AtualizaHistoricoPedidoSAP();

            if(erro == "")
            {
                EnviaEmail(OBJPedido.HistoricoAtualizado);

                Session["Msg"] = "Inclusão de Histórico efetuada com sucesso!";
                OBJPedidoClass.Consulta_Pedido();
                Session["PedidoClass"] = OBJPedidoClass;
                Response.Redirect("../cadastros/FrmPedidoDetalhe.aspx?indmnu=5");
            }else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        public string EnviaEmail(string historico)
        {
            string erro = "";

            try
            {
                OBJMail.CodigoEmpresa = OBJPedidoClass.EmpCod + " - " + OBJPedidoClass.EmpNome;
                OBJMail.NumeroPedidoCRM = OBJPedidoClass.PedVendaNum;
                OBJMail.NomeCliente = OBJPedidoClass.CodigoClienteSAP + " - " + OBJPedidoClass.EntNome;
                OBJMail.DataAlteracao = DateTime.Today.ToString("dd/MM/yyyy");
                OBJMail.Historico = historico;
                OBJMail.TituloEmail = "Inclusão Histórico Pedido " + OBJPedidoClass.PedVendaNum + ".";
                OBJMail.UsuarioCRM = Session["usuario"].ToString();
                OBJMail.FormataTextoHistoricoPedido();

                OBJMail.RecuperaEmailDestinatario();
                //OBJMail.EmailDestinatario = "luiz.carlos@manulifitasa.com.br";

                OBJMail.enviaEmailFormatadoAnexo();
            }
            catch (Exception ex)
            {
                erro = "Erro ao enviar e-mail.";
            }

            return erro;
        }
    }
}