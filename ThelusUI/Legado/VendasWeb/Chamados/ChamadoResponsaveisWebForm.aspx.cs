using System;
using System.Web.UI;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI.WebControls;

namespace VendasWeb.Chamados
{
    public partial class ChamadoResponsaveisWebForm : System.Web.UI.Page
    {
        ChamadoClass objChamado = new ChamadoClass();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ApresentaMensagem(Session["Msg"].ToString());

                Session.Remove("Msg");
            }

            if (Session["objChamado"] != null) objChamado = (ChamadoClass)Session["objChamado"];

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                CarregaTela();
            }
        }

        public void CarregaTela()
        {
            CarregaCombos();

            CarregaDadosNaTela();

            CarregaGridView();
        }

        public void CarregaCombos()
        {
            SolicitanteDropDownList.DataSource = objChamado.CarregaUsuarios();
            SolicitanteDropDownList.DataValueField = "IDUsuario";
            SolicitanteDropDownList.DataTextField = "Nome";
            SolicitanteDropDownList.DataBind();

            ResponsavelDropDownList.DataSource = objChamado.CarregaUsuariosSuporte();
            ResponsavelDropDownList.DataValueField = "IDUsuario";
            ResponsavelDropDownList.DataTextField = "CodigoUsuario";
            ResponsavelDropDownList.DataBind();
        }

        public void CarregaDadosNaTela()
        {
            objChamado.RecuperaDadosPrincipais();

            NumeroChamadoTextBox.Text = objChamado.NumeroChamado.ToString();

            SolicitanteDropDownList.SelectedValue = objChamado.IDUsuarioSolicitante.ToString();
        }

        public void CarregaGridView()
        {
            ChamadosGridView.DataSource = objChamado.CarregaListaResponsaveisChamado();
            ChamadosGridView.DataBind();
            ChamadosMultiView.Visible = true;
        }

        public void CarregaDadosDaTela(object sender, EventArgs e)
        {
            objChamado.NumeroChamado = Convert.ToInt32(NumeroChamadoTextBox.Text);

            objChamado.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"].ToString());

            switch (((System.Web.UI.Control)sender).ID)
            {
                case "AdicionarLinkButton":
                    objChamado.IDUsuarioResponsavel = Convert.ToInt32(ResponsavelDropDownList.SelectedValue);
                    break;

                default:
                    objChamado.IDUsuarioResponsavel = Convert.ToInt32(((Label)((Control)sender).FindControl("IDResponsavelLabel")).Text);
                    break;
            }
        }

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela(sender, e);

            string erro = objChamado.AdicionaResponsavel();

            if (erro == "") CarregaTela();
            else ApresentaMensagem(erro);
        }

        protected void ExcluirLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela(sender, e);

            string erro = objChamado.ExcluiResponsavel();

            if (erro == "") CarregaTela();
            else ApresentaMensagem(erro);
        }

        protected void PrincipalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CarregaDadosDaTela(sender, e);

            string erro = objChamado.GravaResponsavelPrincipal();

            if (erro == "" || erro == "O chamado tem que ter pelo menos um responsável principal.")
            {
                CarregaGridView();
                ApresentaMensagem(erro);
            }
            else
                ApresentaMensagem(erro);
        }

        protected void ChamadosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ChamadosGridView.PageIndex = e.NewPageIndex;
            CarregaGridView();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Chamados/ChamadoPrincipalWebForm.aspx?indmnu=5");
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