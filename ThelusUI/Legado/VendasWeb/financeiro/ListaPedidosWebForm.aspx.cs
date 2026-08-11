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

namespace VendasWeb.listas
{
    public partial class ListaPedidosWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        FiltroClass ObjFiltroClass = new FiltroClass();
        funcoes mdlfuncoes = new funcoes();
        GerencialVendas.PedidoClass PedidoClass = new GerencialVendas.PedidoClass();
        criptografia mdlCriptografia = new criptografia();
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        UtilClass ObjUtilClass = new UtilClass();

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


            if (!IsPostBack)
            {
                CarregaDatas();

                //this.ControlPainel.Desabilitar_Botoes();

                /*Tratar Abrir e fechar Div*/
                collapseLiteral.Text = "<div id=\"filtros\" class=\"collapse in\" runat=\"server\">";

                Session["EmpCod"] = "";
                Session["PedVendaNum"] = "";
                Session["Tipo"] = "";                

                drpEmpresa.DataSource = mdlfuncoes.Consulta_Empresa(Session["usuario"].ToString());
                drpEmpresa.DataTextField = "NomeEmpresa";
                drpEmpresa.DataValueField = "IDEmpresa";
                drpEmpresa.DataBind();

                drpListFiltroStat.DataSource = mdlfuncoes.Consulta_ListaStatus_Ped_Venda();
                drpListFiltroStat.DataTextField = "DescricaoStatus";
                drpListFiltroStat.DataValueField = "IDStatus";
                drpListFiltroStat.DataBind();
                

                drpListFiltroStat.Items.Insert(0, "Todos");
                drpListFiltroStat.SelectedIndex = 0;

                drpEmpresa.DataSource = mdlfuncoes.Consultar_Empresas();
                drpEmpresa.DataTextField = "NomeEmpresa";
                drpEmpresa.DataValueField = "IDEmpresa";
                drpEmpresa.DataBind();

                txtFiltro.Text = "";
                //Verifica se esta vindo para da tela de Entidade
                if (Session["clsEntidades"] != null)
                {
                    /*Seta valores para consultar os pedidos da Entidade selecionada*/
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
                    drpListFiltroPri.SelectedValue = "1";
                    drpEmpresa.SelectedValue = ObjEntidadesClass.EmpCod;
                    txtFiltro.Text = ObjEntidadesClass.CodigoClienteSAP ?? "";
                }

                if (Session["ObjFiltroClass"] != null)
                {

                    ObjFiltroClass = (FiltroClass)Session["ObjFiltroClass"];
                    int cont = 0;
                    if (ObjFiltroClass.itemProdutoList != null) 
                    { 
                        cont = ObjFiltroClass.itemProdutoList.Count;
                    }
                    string produtos = "";

                    Session["ObjFiltroClass"] = null;
                    drpEmpresa.SelectedValue= ObjFiltroClass.EmpCod;
                    drpListFiltroStat.SelectedItem.Text = ObjFiltroClass.PedVendaStatDescr;
                    //drpListFiltroTipo.SelectedItem.Text = ObjFiltroClass.PedVendaTipo;
                    drpListFiltroPri.SelectedValue = ObjFiltroClass.DropOpcaoFiltro;

                    if (cont > 0)
                    {
                        foreach (var row in ObjFiltroClass.itemProdutoList)
                        {
                            produtos += row.codigoProduto + ',';
                        }

                        HiddenFieldListaProdutos.Value = produtos;
                        //ProdutosTextBox.Text = produtos;
                    }

                    //Comentado porque substituia o entcod anterior que é o certo, aqui passava da entidade anterior
                    //txtFiltro.Text = ObjFiltroClass.TextoFiltro;

                    //Atualizar_Grid();
                }
            }
            else
            {
                switch (TipoHiddenField.Value)
                {
                    case "Consulta":
                        TipoHiddenField.Value = " ";
                        Session["EmpCod"] = EmpCodHiddenField.Value;
                        Session["PedVendaNum"] = PedVendaNumHiddenField.Value;
                        Session["Tipo"] = "Consulta";
                        Session["pedidoNovo"] = null;

                        ObjFiltroClass = new FiltroClass();
                        ObjFiltroClass.EmpCod = drpEmpresa.SelectedItem.Value;
                        ObjFiltroClass.PedVendaStatDescr = drpListFiltroStat.SelectedItem.Text;
                        //ObjFiltroClass.PedVendaTipo = drpListFiltroTipo.SelectedItem.Text;
                        ObjFiltroClass.DropOpcaoFiltro = drpListFiltroPri.SelectedValue;
                        ObjFiltroClass.TextoFiltro = txtFiltro.Text;
                        Session["ObjFiltroClass"] = ObjFiltroClass;

                        Response.Redirect("../cadastros/cadPedidoPrincipal.aspx?indmnu=2");

                        break;

                    case "Imprimir":
                        TipoHiddenField.Value = " ";
                        Session["EmpCod"] = EmpCodHiddenField.Value;
                        Session["PedVendaNum"] = PedVendaNumHiddenField.Value;
                        Session["Tipo"] = "Consulta";
                        //Response.Redirect("../relatorios/frmCopiaPedido.aspx?indmnu=2");
                        //Abrir Nova Guia
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect", "window.open('../relatorios/frmCopiaPedido.aspx?indmnu=2');", true);
                        break;

                    case "ImprimirSemHist":
                        TipoHiddenField.Value = " ";
                        Session["EmpCod"] = EmpCodHiddenField.Value;
                        Session["PedVendaNum"] = PedVendaNumHiddenField.Value;
                        Session["Tipo"] = "Consulta";
                        //Response.Redirect("../relatorios/frmCopiaPedidoSemObs.aspx?indmnu=2");
                        //Abrir Nova Guia
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect", "window.open('../relatorios/frmCopiaPedidoSemObs.aspx?indmnu=2');", true);

                        break;
                }
            }
        }

        protected void CarregaDatas()
        {
            DateTime hoje = DateTime.Today;

            DateTime primeiroDiaDoAno = new DateTime(hoje.Year, 1, 1);

            DataInicialTextBox.Text = primeiroDiaDoAno.ToString("yyyy-MM-dd");

            DataFinalTextBox.Text = hoje.ToString("yyyy-MM-dd");
        }

        protected void ListaPedidosGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            ListaPedidosGridView.PageIndex = e.NewPageIndex;
            Atualizar_Grid();
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            Atualizar_Grid();
        }


        public void Atualizar_Grid()
        {
            ObjFiltroClass = (FiltroClass)Session["ObjFiltroClass"];

            PedidoClass.EmpCod = drpEmpresa.SelectedItem.Value;
            PedidoClass.PedVendaStatDescr = drpListFiltroStat.SelectedValue.ToString();
            //PedidoClass.PedVendaTipo = drpListFiltroTipo.SelectedItem.Text;
            PedidoClass.UsuCod = Session["usuario"].ToString();
            //PedidoClass.Nivel = Convert.ToInt32(Session["nivel"].ToString());
            PedidoClass.valorConsulta = txtFiltro.Text;
            //PedidoClass.Produtos = ProdutosTextBox.Text;

            PedidoClass.DataInicial = DataInicialTextBox.Text == "" ? "" : Convert.ToDateTime(DataInicialTextBox.Text).ToString("yyyy-MM-dd");
            PedidoClass.DataFinal = DataFinalTextBox.Text == "" ? "" : Convert.ToDateTime(DataFinalTextBox.Text).ToString("yyyy-MM-dd");

            switch (drpListFiltroPri.SelectedValue.ToString())
            {
                case "1":
                    PedidoClass.EntCod = txtFiltro.Text;
                    break;

                case "2":
                    PedidoClass.EntNome = txtFiltro.Text;
                    break;

                case "3":
                    PedidoClass.PedVendaNum = txtFiltro.Text;
                    break;

                case "4":
                    PedidoClass.NumeroNotaFiscal = txtFiltro.Text;
                    break;
            }

            PedidoClass.Consulta_Copia_Pedido();
            ListaPedidosGridView.DataSource = PedidoClass.Lista_Pedidos();
            //Session.Add("TEMP_SESSAO", ListaPedidosGridView.DataSource);
            ListaPedidosGridView.DataBind();

            PedidosMultiView.Visible = true;
            ListaPedidosGridView.Columns[13].Visible = Convert.ToBoolean(PedidoClass.UsuCodCopia);
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

        protected void btnVerDetalhe_Click(object sender, EventArgs e)
        {
            ObjFiltroClass = new FiltroClass();
            ObjFiltroClass.EmpCod = drpEmpresa.SelectedItem.Value;
            ObjFiltroClass.PedVendaStatDescr = drpListFiltroStat.SelectedItem.Text;
            //ObjFiltroClass.PedVendaTipo = drpListFiltroTipo.SelectedItem.Text;
            ObjFiltroClass.DropOpcaoFiltro = drpListFiltroPri.SelectedValue;
            ObjFiltroClass.TextoFiltro = txtFiltro.Text;
            Session["ObjFiltroClass"] = ObjFiltroClass;

            PedidoClass = new GerencialVendas.PedidoClass();
            PedidoClass.PedVendaNum = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;
            PedidoClass.EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            PedidoClass.NumeroPedidoSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("NumeroPedidoSAPLabel")).Text ?? "0");
            PedidoClass.NumeroEsbocoSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("NumeroEsbocoSAPLabel")).Text ?? "0");
            PedidoClass.Consulta_Pedido();
            Session["PedidoClass"] = PedidoClass;

            Response.Redirect("~/financeiro/PedidoDetalheWebForm.aspx?indmnu=5");
        }

        protected void IncluirProdutoLinkButton_Click(object sender, EventArgs e)
        {
            ObjFiltroClass = new FiltroClass();
            ObjFiltroClass.EmpCod = drpEmpresa.SelectedItem.Value;
            ObjFiltroClass.PedVendaStatDescr = drpListFiltroStat.SelectedItem.Text;
            //ObjFiltroClass.PedVendaTipo = drpListFiltroTipo.SelectedItem.Text;
            ObjFiltroClass.DropOpcaoFiltro = drpListFiltroPri.SelectedValue;
            ObjFiltroClass.TextoFiltro = txtFiltro.Text;
            Session["ObjFiltroClass"] = ObjFiltroClass;

            PedidoClass = new GerencialVendas.PedidoClass();

            Response.Redirect("~/listas/FrmListaPedidosProdutos.aspx?indmnu=5");
        }

        protected void CopiaLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";
            ObjFiltroClass = new FiltroClass();
            ObjFiltroClass.EmpCod = drpEmpresa.SelectedItem.Value;
            ObjFiltroClass.PedVendaStatDescr = drpListFiltroStat.SelectedItem.Text;
            //ObjFiltroClass.PedVendaTipo = drpListFiltroTipo.SelectedItem.Text;
            ObjFiltroClass.DropOpcaoFiltro = drpListFiltroPri.SelectedValue;
            ObjFiltroClass.TextoFiltro = txtFiltro.Text;
            Session["ObjFiltroClass"] = ObjFiltroClass;

            PedidoClass = new GerencialVendas.PedidoClass();
            PedidoClass.PedVendaNum = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;
            PedidoClass.EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            PedidoClass.NumeroPedidoSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("NumeroPedidoSAPLabel")).Text ?? "0");
            PedidoClass.NumeroEsbocoSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("NumeroEsbocoSAPLabel")).Text ?? "0");
            PedidoClass.DescricaoStatus = ((Label)((Control)sender).FindControl("PedVendaStatDescrLabel")).Text;
            erro = PedidoClass.Gera_Copia();

            Session["PedidoClass"] = PedidoClass;

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
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Novo pedido gerado com número "+ PedidoClass.PedVendaNumCopia +".", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

            }
        }

        protected void AtualizarLinkButton_Click(object sender, EventArgs e)
        {
            PedidoClass.PedVendaNum = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;
            PedidoClass.EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            PedidoClass.NumeroPedidoSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("NumeroPedidoSAPLabel")).Text ?? "0");
            PedidoClass.NumeroEsbocoSAP = Convert.ToInt32(((Label)((Control)sender).FindControl("NumeroEsbocoSAPLabel")).Text ?? "0");
            PedidoClass.DescricaoStatus = ((Label)((Control)sender).FindControl("PedVendaStatDescrLabel")).Text;

            PedidoClass.Atualiza_Dados_Pedido_SAP();

            //Zera número para não interferir no recarregamento da página
            PedidoClass.PedVendaNum = "";
            Atualizar_Grid();
        }

    }
}