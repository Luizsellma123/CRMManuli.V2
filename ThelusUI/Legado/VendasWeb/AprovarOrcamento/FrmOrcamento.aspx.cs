using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Data;
using VendasWeb.GerencialVendas;

namespace VendasWeb.AprovarOrcamento
{
    public partial class FrmOrcamento : System.Web.UI.Page
    {
        FiltroClass ObjFiltroClass = new FiltroClass();
        clsOrcamento objOrcamento = new clsOrcamento();
        VendedorClass ObjVendedorClass = new VendedorClass();
        funcoes mdlfuncoes = new funcoes();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {

                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }


            if (!IsPostBack)
            {

                /*Tratar Abrir e fechar Div*/
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";


                EmpresaDropDown.DataSource = mdlfuncoes.Consulta_Empresa(Session["usuario"].ToString());
                EmpresaDropDown.DataTextField = "NomeEmpresa";
                EmpresaDropDown.DataValueField = "IDEmpresa";
                EmpresaDropDown.DataBind();

                //SEtando para todos.
                ConcluidoDropDown.SelectedValue = "";
                AprovadosDropDown.SelectedValue = "";


                if (Session["objOrcamento"] != null)
                {
                    objOrcamento = (clsOrcamento)Session["objOrcamento"];
                    Session["objOrcamento"] = null;
                    CarregaDadosNaTela();
                    BuscarLinkButton_Click1(null, null);

                }

                //Carrega alcada do usuário
                string alcada = "";

                alcada = objOrcamento.recupera_Alcada(Session["usuario"].ToString());

                if (alcada != "")
                {

                    switch (alcada)
                    {
                        case "Controladoria":
                            AlcadaControladoriaCheckBox.Checked = true;
                            AlcadaSupervisorCheckBox.Checked = true;
                            AlcadaTodosCheckBox.Checked = true;
                            break;
                        case "Supervisor":
                            AlcadaSupervisorCheckBox.Checked = true;
                            break;
                        default:
                            AlcadaTodosCheckBox.Checked = true;
                            break;
                    }
                }

                if (Session["ObjFiltroClass"] != null)
                {
                    ObjFiltroClass = (FiltroClass)Session["ObjFiltroClass"];
                    EmpresaDropDown.SelectedValue = ObjFiltroClass.EmpCodLiberarOrcamento;
                    PedVendaNumTextBox.Text = ObjFiltroClass.PedVendaNumOrcamento;
                    EntidadeTextBox.Text = ObjFiltroClass.EntidadeOrcamento;
                    SituacaoDropDown.SelectedValue = ObjFiltroClass.SituacaoOrcamento;
                    AprovadosDropDown.SelectedValue = ObjFiltroClass.AprovadoOrcamento;
                    OrcamentoGridView.PageIndex = ObjFiltroClass.indice;

                    Session["ObjFiltroClass"] = null;

                    BuscarLinkButton_Click1(sender, e);

                }

            }

        }



        protected void Atualiza_Select_Vendedores()
        {
            DataTable Resultado = new DataTable();

            ObjVendedorClass.UsuCod = Session["usuario"].ToString();
            ObjVendedorClass.TodosCodigos = "S";
            Resultado = ObjVendedorClass.Consulta_Vendedor();
        }


        protected void OrcamentoGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            OrcamentoGridView.PageIndex = e.NewPageIndex;
            BuscarLinkButton_Click1(null, null);

        }

        protected void BuscarLinkButton_Click1(object sender, EventArgs e)
        {
            objOrcamento = new clsOrcamento();

            CarregaDadosDaTela();


            OrcamentoGridView.DataSource = objOrcamento.Consulta_Liberacoes_Orcamento();
            OrcamentoGridView.DataBind();

            MultiView.Visible = true;
        }

        protected void btnAcessar_Click(object sender, EventArgs e)
        {
            GridViewPageEventArgs aux;
            objOrcamento = new clsOrcamento();
            CarregaDadosDaTela();
            objOrcamento.EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            objOrcamento.PedVendaNum = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;
            objOrcamento.NumeroEsbocoSAP = ((Label)((Control)sender).FindControl("NumeroEsbocoSAPLabel")).Text;

            ObjFiltroClass.EmpCodLiberarOrcamento = EmpresaDropDown.SelectedValue;
            ObjFiltroClass.PedVendaNumOrcamento = PedVendaNumTextBox.Text;
            ObjFiltroClass.EntidadeOrcamento = EntidadeTextBox.Text;
            ObjFiltroClass.SituacaoOrcamento = SituacaoDropDown.SelectedValue;
            ObjFiltroClass.AprovadoOrcamento = AprovadosDropDown.SelectedValue;
            ObjFiltroClass.UsuCodOrcamento = Session["usuario"].ToString();
            ObjFiltroClass.indice = Convert.ToInt32(OrcamentoGridView.PageIndex);

            Session["ObjFiltroClass"] = ObjFiltroClass;
            Session["objOrcamento"] = objOrcamento;
            Response.Redirect("FrmOrcamentoDetalhe.aspx?indmnu=2");
        }


        public void CarregaDadosDaTela()
        {
            objOrcamento.EmpCodConsulta = EmpresaDropDown.SelectedValue;
            objOrcamento.PedVendaNumConsulta = PedVendaNumTextBox.Text;

            objOrcamento.Entidade = EntidadeTextBox.Text;
            objOrcamento.Situacao = SituacaoDropDown.SelectedValue;
            objOrcamento.Concluido = ConcluidoDropDown.SelectedValue;
            objOrcamento.AprovadoPrincipal = AprovadosDropDown.SelectedValue;
            objOrcamento.UsuCod = Session["usuario"].ToString();

            objOrcamento.DataInicial = DataInicialTextBox.Text;
            objOrcamento.DataFinal = DataFinalTextBox.Text;


            objOrcamento.Alcada = "";
            if (AlcadaSupervisorCheckBox.Checked == true)
            {
                objOrcamento.Alcada += "Supervisor,";
            }

            /*
            if (AlcadaRegionalCheckBox.Checked == true)
            {
                objOrcamento.Alcada += "Reginal,";
            }
             

            if (AlcadaDiretoriaCheckBox.Checked == true)
            {
                objOrcamento.Alcada += "Diretoria,";
            }
             */

            if (AlcadaControladoriaCheckBox.Checked == true)
            {
                objOrcamento.Alcada += "Controladoria,";
            }

            if (AlcadaTodosCheckBox.Checked == true)
            {
                objOrcamento.Alcada = "Supervisor,Diretoria,Controladoria,Todos,";
            }
        }

        public void CarregaDadosNaTela()
        {
            EmpresaDropDown.SelectedValue = objOrcamento.EmpCodConsulta;
            PedVendaNumTextBox.Text = objOrcamento.PedVendaNumConsulta;

            EntidadeTextBox.Text = objOrcamento.Entidade;
            SituacaoDropDown.SelectedValue = objOrcamento.Situacao;
            ConcluidoDropDown.SelectedValue = objOrcamento.Concluido;
            /*AprovadosDropDown.SelectedValue = objOrcamento.AprovadoPrincipal;*/


            objOrcamento.Alcada = "";
            if (objOrcamento.Alcada.Contains("Supervisor") == true)
            {
                AlcadaSupervisorCheckBox.Checked = true;
            }

            /*
            if (objOrcamento.Alcada.Contains("Reginal") == true)
            {
                AlcadaRegionalCheckBox.Checked = true;
            }
             

            if (objOrcamento.Alcada.Contains("Diretoria") == true)
            {
                AlcadaDiretoriaCheckBox.Checked = true;
            }
             */

            if (objOrcamento.Alcada.Contains("Controladoria") == true)
            {
                AlcadaControladoriaCheckBox.Checked = true;
            }

            if (objOrcamento.Alcada.Contains("Todos") == true)
            {
                AlcadaTodosCheckBox.Checked = true;
            }
        }




    }
}