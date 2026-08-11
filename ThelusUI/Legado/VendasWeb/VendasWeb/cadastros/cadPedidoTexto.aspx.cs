using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.cadastros
{
    public partial class cadPedidoTexto : System.Web.UI.Page
    {
        //Instancia classe pedido
        pedido novoPedido = new pedido();
        criptografia mdlCriptografia = new criptografia(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica se usuário esta logado
            int varmenu = Convert.ToInt32(Request.QueryString["indmnu"]);
            if (Session["usuario"] == null && varmenu != 0 && varmenu < 99)
            {
                //Redireciona para tela de login
                Response.Redirect("~/Default.aspx");
            }

            //Recupera objeto pedido da sessao do usuário
            if (Session["pedidoNovo"] != null)
            {
                novoPedido = (pedido)Session["pedidoNovo"];
            }

            if (!IsPostBack)
            {
                Session["Origem"] = "DadosComplementar";

                txtNovoHistorico.Text = novoPedido.historico.ToString();
                txtHistorico.Text = novoPedido.historicoAntigo.ToString();
                txtTextoLivre.Text = novoPedido.observacao.ToString();

                if (novoPedido.tipoOperacao == "inclusao" || novoPedido.tipoOperacao == "alteracao")
                {
                    btnSalvar.Visible = true;
                }
                else
                {
                    btnSalvar.Visible = false;
                }
            }  
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            novoPedido.historico = txtNovoHistorico.Text.ToString();
            novoPedido.observacao = txtTextoLivre.Text.ToString();

            Session["pedidoNovo"] = novoPedido;

            Response.Write("<script>window.location=\"../cadastros/cadPedidoPrincipal.aspx?indmnu=2&idEmp=" + mdlCriptografia.Criptografar(novoPedido.codigoEmpresa, "#!$a36?@") + "&idOpe=" + mdlCriptografia.Criptografar(novoPedido.tipoOperacao, "#!$a36?@") + "&idPed=" + mdlCriptografia.Criptografar(novoPedido.numeroPedido, "#!$a36?@") + " \";</script>");
        }
    }
}