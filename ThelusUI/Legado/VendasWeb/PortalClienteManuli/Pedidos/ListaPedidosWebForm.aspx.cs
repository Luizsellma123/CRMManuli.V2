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
    public partial class ListaPedidosWebForm : System.Web.UI.Page
    {
        UsuarioPortalClass OBJusuario = new UsuarioPortalClass();
        PortalClass OBJPortal = new PortalClass();
        PedidoClass PedidoClass = new GerencialVendas.PedidoClass();

        protected void Page_Load(object sender, EventArgs e)
        {

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
            carregaPedidos();            

        }

        public void carregaPedidos()
        {
            DataTable RetornoDados = new DataTable();

            //Recupera usuario da sessão
            OBJusuario = (UsuarioPortalClass)Session["usuarioPortal"];

            //recupera pedidos pendentes
            OBJusuario.EmpCod = EmpresaDropDownList.SelectedValue.ToString();
            OBJusuario.PedVendaNum = PedidoTextBox.Text.ToString();
            OBJusuario.faturados = FaturadosRadioButton.Checked;

            if (DataInicialTextBox.Text != "")
            {
                OBJusuario.DataInicial = Convert.ToDateTime(DataInicialTextBox.Text);
            }else
            {
                OBJusuario.DataInicial = DateTime.Now.AddYears(-1);
            }

            if (DataFinalTextBox.Text != "")
            {
                OBJusuario.DataFinal = Convert.ToDateTime(DataFinalTextBox.Text);
            }else
            {
                OBJusuario.DataFinal = DateTime.Now;
            }

            RetornoDados = OBJusuario.Pedidos_Entidade();

            GridViewPedidosClientes.DataSource = RetornoDados;
            GridViewPedidosClientes.DataBind();
        }

        protected void GridViewPedidosClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPedidosClientes.PageIndex = e.NewPageIndex;
            carregaPedidos();
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            carregaPedidos();
        }

        protected void NovoPedidoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PortalClienteManuli/Pedidos/InclusaoPedidosWebForm.aspx");
        }

        protected void LinkButtonConsulta_Click(object sender, EventArgs e)
        {
            
            PedidoClass = new PedidoClass();
            PedidoClass.PedVendaNum = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;
            PedidoClass.EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            PedidoClass.Consulta_Pedido();
            PedidoClass.PedVendaStatDescr = ((Label)((Control)sender).FindControl("pedvendastatdescrLabel")).Text;
            Session["PedidoClass"] = PedidoClass;

           Response.Redirect("~/PortalClienteManuli/Pedidos/DetalhesPedidosWebForm.aspx");
        }
    }
}