using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.AprovarOrcamento
{
    public partial class FrmOrcamentoLogistica : System.Web.UI.Page
    {
        FiltroClass ObjFiltroClass = new FiltroClass();
        clsOrcamento objOrcamento = new clsOrcamento();
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
                EmpresaDropDown.DataTextField = "EmpNome";
                EmpresaDropDown.DataValueField = "EmpCod";
                EmpresaDropDown.DataBind();

                if (Session["objOrcamento"] != null)
                {
                    objOrcamento = (clsOrcamento)Session["objOrcamento"];
                    Session["objOrcamento"] = null;
                    CarregaDadosNaTela();
                    BuscarLinkButton_Click(null, null);

                }

                if (Session["ObjFiltroClass"] != null)
                {
                    ObjFiltroClass = (FiltroClass)Session["ObjFiltroClass"];
                    EmpresaDropDown.SelectedValue = ObjFiltroClass.EmpCodLiberarOrcamento;
                    PedVendaNumTextBox.Text = ObjFiltroClass.PedVendaNumOrcamento;
                    EntidadeTextBox.Text = ObjFiltroClass.EntidadeOrcamento;
                    SituacaoDropDown.SelectedValue = ObjFiltroClass.SituacaoOrcamento;
                    //OrcamentoGridView.PageIndex = ObjFiltroClass.indice;

                    Session["ObjFiltroClass"] = null;

                    BuscarLinkButton_Click(sender, e);

                }
            }

        }

        public void CarregaDadosNaTela()
        {
            EmpresaDropDown.SelectedValue = objOrcamento.EmpCodConsulta;
            PedVendaNumTextBox.Text = objOrcamento.PedVendaNumConsulta;

            EntidadeTextBox.Text = objOrcamento.Entidade;
            SituacaoDropDown.SelectedValue = objOrcamento.Situacao; 

        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            objOrcamento = new clsOrcamento();
            
            CarregaDadosDaTela();

            OrcamentoGridView.DataSource = objOrcamento.Consulta_Liberacoes_Orcamento_Logisitica();
            OrcamentoGridView.DataBind();

            MultiView.Visible = true;

        }

        public void CarregaDadosDaTela()
        {
            objOrcamento.EmpCodConsulta = EmpresaDropDown.SelectedValue;
            objOrcamento.PedVendaNumConsulta = PedVendaNumTextBox.Text;

            objOrcamento.Entidade = EntidadeTextBox.Text;
            objOrcamento.Situacao = SituacaoDropDown.SelectedValue;
            objOrcamento.UsuCod = Session["usuario"].ToString();
        }

        protected void OrcamentoGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            OrcamentoGridView.PageIndex = e.NewPageIndex;
            BuscarLinkButton_Click(null, null);

        }

        protected void btnAcessar_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();
            objOrcamento.EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            objOrcamento.PedVendaNum = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;

            ObjFiltroClass.EmpCodLiberarOrcamento = EmpresaDropDown.SelectedValue;
            ObjFiltroClass.PedVendaNumOrcamento = PedVendaNumTextBox.Text;
            ObjFiltroClass.EntidadeOrcamento = EntidadeTextBox.Text;
            ObjFiltroClass.SituacaoOrcamento = SituacaoDropDown.SelectedValue;
            ObjFiltroClass.UsuCodOrcamento = Session["usuario"].ToString();
            ObjFiltroClass.indice = Convert.ToInt32(OrcamentoGridView.PageIndex);

            Session["ObjFiltroClass"] = ObjFiltroClass;
            Session["objOrcamento"] = objOrcamento;
            Response.Redirect("FrmOrcamentoDetalheLogistica.aspx?indmnu=2");
        }
    }
}