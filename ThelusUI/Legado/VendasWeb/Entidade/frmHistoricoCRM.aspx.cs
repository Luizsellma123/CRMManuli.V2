using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Data;


namespace VendasWeb.Entidade
{
    public partial class frmHistoricoCRM : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        HistoricoCRMClass HistoricoCRMClass = new GerencialVendas.HistoricoCRMClass();
        clsEntidades clsEntidades = new clsEntidades();
        VendedorClass VendedorClass = new VendedorClass();
        ContatoClass ObjContato = new ContatoClass();
        UtilClass ObjUtilClass = new UtilClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {

                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                Session["Msg"] = null;
            }

            if (!IsPostBack)
            {
                clsEntidades = ((clsEntidades)Session["clsEntidades"]);
                Session["clsEntidades"] = clsEntidades;
                //Consulta evento
                drpEvento.DataSource = HistoricoCRMClass.Lista_Evento();
                drpEvento.DataTextField = "Descricao";
                drpEvento.DataValueField = "Codigo";
                drpEvento.DataBind();
                drpEvento.Items.Insert(0, new ListItem("Selecione", "0"));



                //drpEvento.SelectedValue = "1";
                HistoricoCRMClass.CodigoPai = "1";

                //Consulta evento de filtro
                drpEventoFiltro.DataSource = HistoricoCRMClass.Lista_Evento_Filtro();
                drpEventoFiltro.DataTextField = "Descricao";
                drpEventoFiltro.DataValueField = "Codigo";
                drpEventoFiltro.DataBind();
                drpEventoFiltro.Items.Insert(0, new ListItem("Todos", "0"));


                this.drpEvento.SelectedIndexChanged += new System.EventHandler(drpEvento_SelectedIndexChanged);
                this.drpEventoFiltro.SelectedIndexChanged += new System.EventHandler(drpEventoFiltro_SelectedIndexChanged);

                //Consulta Categoria
                drpCategoria.DataSource = HistoricoCRMClass.Lista_Categoria();
                drpCategoria.DataTextField = "Descricao";
                drpCategoria.DataValueField = "Codigo";
                drpCategoria.DataBind();
                drpCategoria.Items.Insert(0, new ListItem("Selecione", "0"));



                //Consulta Categoria de Filtro
                drpCategoriaFiltro.DataSource = HistoricoCRMClass.Lista_Categoria_Filtro();
                drpCategoriaFiltro.DataTextField = "Descricao";
                drpCategoriaFiltro.DataValueField = "Codigo";
                drpCategoriaFiltro.DataBind();
                drpCategoriaFiltro.Items.Insert(0, new ListItem("Todos", "0"));


                int Hora;
                for (Hora = 0; Hora <= 23; Hora++)
                {
                    drpHora.Items.Add(Convert.ToString(Hora));
                }


                HistoricoCRMClass.EntCod = clsEntidades.EntCod;
                HistoricoCRMClass.Historico_Listar();
                //txtHistorico.Text = HistoricoCRMClass.Historico;

                Atualizar_Grid();

                clsEntidades.Mostra_Entidade();
                LblCliente.Text = clsEntidades.EntNome;

                //Oculta botoes caso operacao de Incluir ou excluir da Carteira
                switch (clsEntidades.TipoOperacao)
                {
                    case "Incluir Carteira":
                    case "Excluir Carteira":

                        break;
                }


            }
        }

        public void Atualizar_Grid()
        {
            DataTable outputTable = new DataTable();


            HistoricoCRMClass.EntCod = ((clsEntidades)Session["clsEntidades"]).EntCod;
            HistoricoCRMClass.CodigoCategoria = Convert.ToInt32(drpCategoriaFiltro.SelectedValue);
            HistoricoCRMClass.CodigoEvento = Convert.ToInt32(drpEventoFiltro.SelectedValue);
            HistoricoCRMClass.UsuCod = Session["usuario"].ToString();

            outputTable = HistoricoCRMClass.Historico_Listar();

            lblHistorico.Text = "";

            if (outputTable.Rows.Count > 0)
            {
                foreach (DataRow row in outputTable.Rows)
                {

                    lblHistorico.Text = lblHistorico.Text + "<div class=\"timeline-entry\"> <div class=\"timeline-stat\"> ";


                    switch (row["Evento"].ToString())
                    {
                        case "Atendimento":
                            lblHistorico.Text = lblHistorico.Text + "<div class=\"timeline-icon bg-info\"><i class=\"fa fa-comment-o fa-lg\"></i> ";
                            break;

                        case "Visita Teste":
                            lblHistorico.Text = lblHistorico.Text + "<div class=\"timeline-icon bg-primary\"><i class=\"fa fa-car fa-lg\"></i> ";
                            break;

                        case "Negociação":
                            lblHistorico.Text = lblHistorico.Text + "<div class=\"timeline-icon bg-dark\"><i class=\"fa fa-comments-o fa-lg\"></i> ";
                            break;

                        case "Venda Fechada":
                            lblHistorico.Text = lblHistorico.Text + "<div class=\"timeline-icon bg-success\"><i class=\"fa fa-thumbs-o-up fa-lg\"></i> ";
                            break;

                        case "Venda Perdida":
                            lblHistorico.Text = lblHistorico.Text + "<div class=\"timeline-icon bg-danger\"><i class=\"fa fa-thumbs-down fa-lg\"></i> ";
                            break;

                        case "Outros":
                            lblHistorico.Text = lblHistorico.Text + "<div class=\"timeline-icon bg-gra\"><i class=\"fa fa-plus-square-o fa-lg\"></i> ";
                            break;

                        case "Pedido":
                            lblHistorico.Text = lblHistorico.Text + "<div class=\"timeline-icon bg-mint\"><i class=\"fa fa-shopping-cart fa-lg\"></i> ";
                            break;

                        case "Nota":
                            lblHistorico.Text = lblHistorico.Text + "<div class=\"timeline-icon bg-warning\"><i class=\"fa fa-pencil-square-o fa-lg\"></i> ";
                            break;

                        case "Observações":
                            lblHistorico.Text = lblHistorico.Text + "<div class=\"timeline-icon bg-purple\"><i class=\"fa fa-warning fa-lg\"></i> ";
                            break;

                        case "Mudança":
                            lblHistorico.Text = lblHistorico.Text + "<div class=\"timeline-icon bg-pink\"><i class=\"fa fa-random fa-lg\"></i> ";
                            break;
                    }



                    lblHistorico.Text = lblHistorico.Text + "</div><div class=\"timeline-time\"><b>"
                                                          + row["DataCad"].ToString() + "</b></div> "
                                                          + "</div><div class=\"timeline-label\"> ";

                    if (row["Evento"].ToString() == "Atendimento")
                    {
                        lblHistorico.Text = lblHistorico.Text + "<p class=\"mar-no pad-btm\"> <span class=\"badge badge-info\">" + row["Evento"].ToString() + " via ";

                        switch (row["Categoria"].ToString())
                        {
                            case "Telefone":
                                lblHistorico.Text = lblHistorico.Text + "<i class=\"fa fa-phone\"></i> " + row["Categoria"].ToString() + "</span>";
                                break;

                            case "E-mail":
                                lblHistorico.Text = lblHistorico.Text + "<i class=\"fa fa-envelope\"></i> " + row["Categoria"].ToString() + "</span>";
                                break;

                            case "Visita":
                                lblHistorico.Text = lblHistorico.Text + "<i class=\"fa fa-car\"></i> " + row["Categoria"].ToString() + "</span>";
                                break;

                            case "Online":
                                lblHistorico.Text = lblHistorico.Text + "<i class=\"fa fa-desktop\"></i> " + row["Categoria"].ToString() + "</span>";
                                break;
                        }



                    }
                    else
                    {

                        switch (row["Evento"].ToString())
                        {
                            case "Visita Teste":
                                lblHistorico.Text = lblHistorico.Text + "<p class=\"mar-no pad-btm\"> <span class=\"badge badge-primary\">" + row["Evento"].ToString() + " " + row["Categoria"].ToString();
                                break;

                            case "Negociação":
                                lblHistorico.Text = lblHistorico.Text + "<p class=\"mar-no pad-btm\"> <span class=\"badge badge-dark\">" + row["Evento"].ToString() + " " + row["Categoria"].ToString();
                                break;

                            case "Venda Fechada":
                                lblHistorico.Text = lblHistorico.Text + "<p class=\"mar-no pad-btm\"> <span class=\"badge badge-success\">" + row["Evento"].ToString() + " " + row["Categoria"].ToString();
                                break;

                            case "Venda Perdida":
                                lblHistorico.Text = lblHistorico.Text + "<p class=\"mar-no pad-btm\"> <span class=\"badge badge-danger\">" + row["Evento"].ToString() + " " + row["Categoria"].ToString();
                                break;

                            case "Outros":
                                lblHistorico.Text = lblHistorico.Text + "<p class=\"mar-no pad-btm\"> <span class=\"badge badge-gray\">" + row["Evento"].ToString() + " " + row["Categoria"].ToString();
                                break;

                            case "Pedido":
                                lblHistorico.Text = lblHistorico.Text + "<p class=\"mar-no pad-btm\"> <span class=\"badge badge-mint\">" + row["Evento"].ToString() + " " + row["Categoria"].ToString();
                                break;

                            case "Nota":
                                lblHistorico.Text = lblHistorico.Text + "<p class=\"mar-no pad-btm\"> <span class=\"badge badge-warning\">" + row["Evento"].ToString() + " " + row["Categoria"].ToString();
                                break;

                            case "Observações":
                                lblHistorico.Text = lblHistorico.Text + "<p class=\"mar-no pad-btm\"> <span class=\"badge badge-purple\">" + row["Evento"].ToString() + " " + row["Categoria"].ToString();
                                break;

                            case "Mudança":
                                lblHistorico.Text = lblHistorico.Text + "<p class=\"mar-no pad-btm\"> <span class=\"badge badge-pink\">" + row["Evento"].ToString() + " " + row["Categoria"].ToString();
                                break;
                        }

                    }


                    lblHistorico.Text = lblHistorico.Text + "</span> por <a href=\"#\" class=\"btn-link btn-md text-semibold\"> "
                                                          + row["UsuCod"].ToString() + "</a></p>"
                                                          + "<div class=\"well well-xs mar-no\"> "
                                                          + row["Historico"].ToString()
                                                          + "</div>";

                    if (row["DataAgenda"].ToString() != "")
                    {
                        lblHistorico.Text = lblHistorico.Text + "<p class=\"text-default mar-no pad-top\"><i class=\"fa fa-clock-o\"></i> Agendamento para novo contato - " + row["DataAgenda"].ToString() + "</p>";
                    }

                    lblHistorico.Text = lblHistorico.Text + "</div></div>";
                }
            }
        }

        protected void drpEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            HistoricoCRMClass.CodigoPai = drpEvento.SelectedValue;
            drpCategoria.DataSource = HistoricoCRMClass.Lista_Categoria();
            drpCategoria.DataBind();
            drpCategoria.Items.Insert(0, new ListItem("Selecione", "0"));

        }

        protected void drpEventoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            HistoricoCRMClass.CodigoPai = drpEventoFiltro.SelectedValue;
            drpCategoriaFiltro.DataSource = HistoricoCRMClass.Lista_Categoria_Filtro();
            drpCategoriaFiltro.DataBind();
            drpCategoriaFiltro.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            Atualizar_Grid();
        }

        protected void SalvarButton_Click(object sender, EventArgs e)
        {
            if (drpCategoria.SelectedValue == "0" || drpEvento.SelectedValue == "0")
            {

                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro("Obrigatório informar a categoria e o evento.", true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                string erro = "";

                clsEntidades = new GerencialVendas.clsEntidades();
                clsEntidades = ((clsEntidades)Session["clsEntidades"]);
                HistoricoCRMClass.CodigoCategoria = Convert.ToInt32(drpCategoria.SelectedValue);
                HistoricoCRMClass.CodigoEvento = Convert.ToInt32(drpEvento.SelectedValue);
                HistoricoCRMClass.DataCad = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                HistoricoCRMClass.EntCod = clsEntidades.EntCod;
                HistoricoCRMClass.UsuCod = Session["usuario"].ToString();
                HistoricoCRMClass.Historico = txtNovoHistorico.Text;

                if (txtData.Text == "")
                {
                    HistoricoCRMClass.DataAgenda = "";
                }
                else
                {
                    HistoricoCRMClass.DataAgenda = txtData.Text + " " + drpHora.SelectedValue + ":00";
                }

                if (HistoricoCRMClass.Historico.Length >= 20)
                {
                    erro = HistoricoCRMClass.Historico_Inserir();

                    if (erro == "")
                    {
                        Session["TemHistorico"] = "Sim";
                        //Verifica a operacao
                        switch (clsEntidades.TipoOperacao)
                        {
                            case "Incluir Carteira":
                                VendedorClass.EntCod = clsEntidades.EntCod;
                                VendedorClass.UsuCod = Session["usuario"].ToString();
                                //VendedorClass.Consulta_Codigo_Vendedor_UsuCod();//Pega o Vendedor do Usuario
                                VendedorClass.VendCod = clsEntidades.NovoVendCod;
                                VendedorClass.VendEntPrinc = "Não";

                                if (VendedorClass.VendCod != "" && VendedorClass.VendCod != null)
                                {
                                    VendedorClass.Incluir_Vend_Ent();
                                    clsEntidades.TipoOperacao = "";
                                    Session["clsEntidades"] = clsEntidades;

                                    ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Entidade vinculada com sucesso.", true);
                                    ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                                    ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();


                                }
                                else
                                {

                                    ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Sem Vendedor selecionado, verifique.", true);
                                    ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                                    ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                                }
                                break;

                            case "Excluir Carteira":
                                VendedorClass.EntCod = clsEntidades.EntCod;
                                VendedorClass.UsuCod = Session["usuario"].ToString();
                                VendedorClass.VendCod = "";
                                VendedorClass.VendEntPrinc = "Não";

                                VendedorClass.Remove_Cond_Vend_Ent();

                                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Entidade desvinculada com sucesso.", true);
                                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                                break;

                            default:
                                clsEntidades.TipoOperacao = "";
                                Session["clsEntidades"] = clsEntidades;

                                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Histórico inserido com sucesso.", true);
                                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                                break;
                        }


                    }
                    else
                        Session["TemHistorico"] = "Não";

                    Atualizar_Grid();
                    /*HistoricoCRMClass.Historico_Listar();
                    txtHistorico.Text = HistoricoCRMClass.Historico;*/

                    txtNovoHistorico.Text = "";
                }
                else
                {
                    Session["TemHistorico"] = "Não";

                    ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Histórico deve ter no mínimo 20 caracteres.", true);
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                }
            }
        }





    }
}