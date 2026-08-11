using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Infraestrutura
{
    public partial class PainelWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        InfraestruturaClass objInfraestruturaClass = new InfraestruturaClass();

        protected void Page_Load(object sender, EventArgs e)
        { //Valida Acesso
            OBJSessao.ValidaAcesso();

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }

            if (!IsPostBack)
            {
                BuscarButton_Click(null, null);

                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
            }
        }

        protected void CarregaDadosDaTela()
        {
            objInfraestruturaClass.MAC = MACTextBox.Text;

            objInfraestruturaClass.IP = IPTextBox.Text;

            objInfraestruturaClass.Nome = NomeTextBox.Text;
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();

            InfraestruturaGridView.DataSource = objInfraestruturaClass.CarregaListaMaquinas();
            InfraestruturaGridView.DataBind();
            InfraestruturaMultiView.Visible = true;
        }

        protected void InfraestruturaGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            InfraestruturaGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }

        protected void AcessarLinkButton_Click(object sender, EventArgs e)
        {
            objInfraestruturaClass.IDPerformance = Convert.ToInt32(((Label)((Control)sender).FindControl("IDPerformanceLabel")).Text);

            Session["objInfraestruturaClass"] = objInfraestruturaClass;

            Response.Redirect("~/Infraestrutura/InfoPCWebForm.aspx?indmnu=5");
        }

        public void ApresentaMensagem(string erro = "")
        {
            if (erro == "")
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Operação realizada com sucesso", true);
            else
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
        }
    }
}