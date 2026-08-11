using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.listas
{
    public partial class LiberarPedido : System.Web.UI.Page
    {
        GerencialVendas.LiberaPedidoClasse LiberaPedidoClasse = new GerencialVendas.LiberaPedidoClasse();
        funcoes mdlfuncoes = new funcoes();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                txtDataCancelamento.Text = DateTime.Today.ToString("dd/MM/yyyy");

                drpEmpresa.DataSource = mdlfuncoes.Consulta_Empresa(Session["usuario"].ToString());
                drpEmpresa.DataTextField = "EmpNome";
                drpEmpresa.DataValueField = "EmpCod";
                drpEmpresa.DataBind();

                LiberaPedidoClasse.usuario = Session["usuario"].ToString();
                drpAlcada.DataSource = LiberaPedidoClasse.Mostra_AcessoAlcada();
                drpAlcada.DataTextField = "Alcada";
                drpAlcada.DataValueField = "Alcada";
                drpAlcada.DataBind();

                Atualizar_GridPedidos();


                //int consultaLiberaPedido = LiberaPedidoClasse.ConsultaLiberarPedido(Session["usuario"].ToString());
                //if (consultaLiberaPedido == 0)
                //{
                //LiberarPedidosGridview.Columns.RemoveAt(0);
                //}
            }
        }

        public void Atualizar_GridPedidos()
        {
            funcoesBD funcoesBD = new funcoesBD();
            //int consultaLiberaPedido = LiberaPedidoClasse.ConsultaLiberarPedido(Session["usuario"].ToString());

            LiberaPedidoClasse.Empresa = drpEmpresa.SelectedValue;
            LiberaPedidoClasse.DataCancelamento = funcoesBD.FormataData(txtDataCancelamento.Text);
            LiberaPedidoClasse.NumeroPedido = txtPedido.Text;
            LiberaPedidoClasse.Status = drpStatus.SelectedValue;
            LiberaPedidoClasse.Alcada = drpAlcada.SelectedValue;
            LiberaPedidoClasse.usuario = Session["usuario"].ToString();

            LiberarPedidosGridview.DataSource = LiberaPedidoClasse.Mostra_PedidoBloqueado();
            Session.Add("TEMP_SESSAO", LiberarPedidosGridview.DataSource);

            //Remove botão de liberação do pedido caso usuario não tenha este direito
            //Precisa estar cadastrado como supervisor para liberar
            if (Session["pedidoLiberado"] != null)
            {
                if (Convert.ToInt32(Session["pedidoLiberado"]) == 0)
                {
                    LiberarPedidosGridview.Columns[0].Visible = false;
                    LiberarPedidosGridview.Columns[1].Visible = false;
                }
            }

            LiberarPedidosGridview.DataBind();

        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            Atualizar_GridPedidos();
            LiberaPedidoGridItensview.Visible = false;
        }

        protected void LiberarPedidosGridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LiberarPedidosGridview.PageIndex = e.NewPageIndex;
            Atualizar_GridPedidos();
        }

        protected void PesquisarButton_Click(object sender, EventArgs e)
        {
            string Pedido = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;
            string codigoEmpresa = drpEmpresa.SelectedValue;

            LiberaPedidoClasse.Empresa = codigoEmpresa;
            LiberaPedidoClasse.NumeroPedidoSelecionado = Pedido;            

            Atualizar_GridPedidosItens();

            LiberaPedidoGridItensview.Visible = true;
        }

        public void Atualizar_GridPedidosItens()
        {
            LiberaPedidoGridItensview.DataSource = LiberaPedidoClasse.Mostra_PedidoBloqueadoItens();

            Session.Add("TEMP_SESSAO", LiberarPedidosGridview.DataSource);
            LiberaPedidoGridItensview.DataBind();

        }

        protected void Liberar_Click(object sender, EventArgs e)
        {
            string Pedido = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;
            string codigoEmpresa = drpEmpresa.SelectedValue;
            string Status = ((Label)((Control)sender).FindControl("StatusLabel")).Text;
            string erro = "";
            string Alcada = ((Label)((Control)sender).FindControl("AlcadaLabel")).Text;

            if (Status == "Pendente")
            {
                if (txtMotivo.Text != "")
                {
                    LiberaPedidoClasse.Empresa = codigoEmpresa;
                    LiberaPedidoClasse.NumeroPedidoSelecionado = Pedido;
                    LiberaPedidoClasse.Status = "Liberado";
                    LiberaPedidoClasse.motivo = txtMotivo.Text;
                    LiberaPedidoClasse.usuario = Session["usuario"].ToString();

                    erro = LiberaPedidoClasse.LiberarPedido();

                    if (erro != "")
                    {
                        Response.Write("<script>alert(\"" + erro + "\");</script>");
                    }
                    else
                    {
                        //Limpa Motivo e libera pedido
                        txtMotivo.Text = "";
                        Response.Write("<script>alert(\"Pedido liberado!\");</script>");

                        //Atualiza Grid Para não aparecer mesmo pedido
                        Atualizar_GridPedidos();


                        LiberaPedidoGridItensview.Visible = false;

                    }
                }
                else
                {
                    Response.Write("<script>alert(\"Favor Digitar um Motivo!\");</script>");
                }
            }
            else
            {
                Response.Write("<script>alert(\"Pedido não esta pendente.\");</script>");
            }
        }

        protected void cancelarButton_Click(object sender, EventArgs e)
        {

           // Response.Write("<script language=javascript>");
            //Response.Write("if(confirm('Confirma cancelamento pedido?'))");
            
            //if (mdlfuncoes.ConfirmaResposta())
            //{
                string Pedido = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;
                string codigoEmpresa = drpEmpresa.SelectedValue;
                string Status = ((Label)((Control)sender).FindControl("StatusLabel")).Text;
                string erro = "";

                if (Status == "Pendente")
                {
                    LiberaPedidoClasse.Empresa = codigoEmpresa;
                    LiberaPedidoClasse.NumeroPedidoSelecionado = Pedido;
                    LiberaPedidoClasse.Status = "Cancelado";
                    LiberaPedidoClasse.motivo = txtMotivo.Text;
                    LiberaPedidoClasse.usuario = Session["usuario"].ToString();

                    erro = LiberaPedidoClasse.LiberarPedido();

                    if (erro != "")
                    {
                        Response.Write("<script>alert(\"" + erro + "\");</script>");
                    }
                    else
                    {
                        //Limpa Motivo e libera pedido
                        txtMotivo.Text = "";
                        Response.Write("<script>alert(\"Pedido Cancelado!\");</script>");

                        //Atualiza Grid Para não aparecer mesmo pedido
                        Atualizar_GridPedidos();
                    }
                }                    
                else
                {
                    Response.Write("<script>alert(\"Pedido não esta pendente.\");</script>");
                }
                //Response.Write("</script>");
            //}
        }

        protected void drpEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            Atualizar_GridPedidos();
            LiberaPedidoGridItensview.Visible = false;
        }
    }
}