using System;
using System.Data;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Chamados
{
    public partial class ChamadoApontamentoHorasWebForm : System.Web.UI.Page
    {
        ChamadoClass objChamado = new ChamadoClass();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();
        usuario Objusuario = new usuario();
        CrmGrupoUsuarioClass GruposUsuario = new CrmGrupoUsuarioClass();
        ParametroGeral objParametroGeral = new ParametroGeral();

        protected void Page_Load(object sender, EventArgs e)
        {
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ApresentaMensagem(Session["Msg"].ToString());
                Session.Remove("Msg");
            }

            if (Session["objChamado"] != null)
                objChamado = (ChamadoClass)Session["objChamado"];


            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                CarregaPrincipal();
            }
        }

        protected void CarregaPrincipal()
        {
            CarregaCombos();

            CarregaDadosNaTela();

            CarregaGrid();
        }

        public void CarregaCombos()
        {
            SolicitanteDropDownList.DataSource = objChamado.CarregaUsuarios();
            SolicitanteDropDownList.DataValueField = "IDUsuario";
            SolicitanteDropDownList.DataTextField = "Nome";
            SolicitanteDropDownList.DataBind();

            ResponsavelDropDownList.DataSource = objChamado.CarregaListaResponsaveisChamado();
            ResponsavelDropDownList.DataValueField = "IDResponsavel";
            ResponsavelDropDownList.DataTextField = "Responsavel";
            ResponsavelDropDownList.DataBind();
        }

        public void CarregaDadosNaTela()
        {
            objChamado.RecuperaDadosPrincipais();

            SolicitanteDropDownList.SelectedValue = objChamado.IDUsuarioSolicitante.ToString();

            NumeroChamadoTextBox.Text = objChamado.NumeroChamado.ToString();

            TrataAcessoResponsavelDropDownList();

            DataTextBox.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        public void CarregaGrid()
        {
            ApontamentoHorasGridView.DataSource = objChamado.RecuperaDadosApontamentoHoras();
            ApontamentoHorasGridView.DataBind();
            ApontamentoHorasMultiView.Visible = true;
        }

        public string CarregaDadosDaTela(string metodo, object sender, EventArgs e)
        {
            objChamado.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"]);

            objChamado.NumeroChamado = Convert.ToInt32(NumeroChamadoTextBox.Text);

            switch (metodo)
            {
                case "AdicionarLinkButton":
                    objChamado.IDUsuarioResponsavel = Convert.ToInt32(ResponsavelDropDownList.SelectedValue);
                    objChamado.DataApontamento = Convert.ToDateTime(DataTextBox.Text).ToString("yyyy-MM-dd");
                    if (NumeroHorasTextBox.Text == "") return "Informe o número de horas.";
                    objChamado.NumeroHoras = Convert.ToInt32(NumeroHorasTextBox.Text);
                    objChamado.descricao = DescricaoTextBox.Text;

                    break;

                case "ExcluirLinkButton":
                    objChamado.IDUsuarioResponsavel = Convert.ToInt32(((Label)((Control)sender).FindControl("IDUsuarioResponsavelLabel")).Text);
                    objChamado.IDApontamento = Convert.ToInt32(((Label)((Control)sender).FindControl("IDApontamentoLabel")).Text);
                    break;
            }

            return "";
        }

        protected void AdicionarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = CarregaDadosDaTela(((System.Web.UI.Control)sender).ID, sender, e);

            if (erro == "")
            {
                erro = objChamado.AdicionaApontamentoHoras();

                CarregaPrincipal();
            }

            ApresentaMensagem(erro);
        }

        protected void ExcluirLinkButton_Click(object sender, EventArgs e)
        {
            string erro = CarregaDadosDaTela(((System.Web.UI.Control)sender).ID, sender, e);

            if (erro == "")
            {
                erro = objChamado.ExcluiApontamentoHoras();

                CarregaPrincipal();
            }

            ApresentaMensagem(erro);
        }

        public void ConsultaGrupoSuporteUsuario()
        {
            //Consulta grupos do usuário logado

            if (Session["usuario"] != null)
                Objusuario.CodigoUsuario = Session["usuario"].ToString();

            //Grupo de Suporte
            GruposUsuario = Objusuario.ConsultaGrupos("Ativo", objParametroGeral.RetornaValorNumericoParametro("GRUPOCHAMADOSSUPORTE"));
        }

        public void TrataAcessoResponsavelDropDownList()
        {
            bool admSuporte = false;

            //Verifica se o usuário logado é administrador do grupo de suporte
            {
                ConsultaGrupoSuporteUsuario();

                if (GruposUsuario != null) admSuporte = Convert.ToBoolean(GruposUsuario.Administrador);
            }

            if (!admSuporte)
            {
                ResponsavelDropDownList.CssClass = "form-control";
                ResponsavelDropDownList.Enabled = false;
            }

            DataTable ResponsavelDataTable = (DataTable)ResponsavelDropDownList.DataSource;

            foreach (DataRow row in ResponsavelDataTable.Rows)
            {
                if (row["IDResponsavel"].ToString() == Session["IDUsuario"].ToString())
                    ResponsavelDropDownList.SelectedValue = Session["IDUsuario"].ToString();
            }
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

        protected void ApontamentoHorasGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ApontamentoHorasGridView.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }
    }
}