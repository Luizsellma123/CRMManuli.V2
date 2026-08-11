using System;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI.WebControls;

namespace VendasWeb.Chamados
{
    public partial class ChamadoProjetoWebForm : System.Web.UI.Page
    {
        ChamadoClass OBJChamado = new ChamadoClass();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {
                ApresentaMensagem(Session["Msg"].ToString());

                Session.Remove("Msg");
            }

            if (Session["OBJChamado"] != null)
                OBJChamado = (ChamadoClass)Session["OBJChamado"];

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                //Carrega vendedores conforme autorização
                CarregaCombos();

                //Carrega dados na tela
                CarregaDadosNaTela();
            }

        }

        public void CarregaCombos()
        {
            SolicitanteDropDownList.DataSource = OBJChamado.CarregaUsuarios();
            SolicitanteDropDownList.DataValueField = "IDUsuario";
            SolicitanteDropDownList.DataTextField = "Nome";
            SolicitanteDropDownList.DataBind();

            PrioridadeProjetoDropDownList.DataSource = OBJChamado.CarregaPrioridadesProjeto();
            PrioridadeProjetoDropDownList.DataValueField = "IDPrioridadeProjeto";
            PrioridadeProjetoDropDownList.DataTextField = "Descricao";
            PrioridadeProjetoDropDownList.DataBind();

            HorasDesenvolvimentoDropDownList.DataSource = OBJChamado.CarregaHorasDesenvolvimentoProjeto();
            HorasDesenvolvimentoDropDownList.DataValueField = "IDHoras";
            HorasDesenvolvimentoDropDownList.DataTextField = "Descricao";
            HorasDesenvolvimentoDropDownList.DataBind();
        }

        public void CarregaDadosNaTela()
        {
            SolicitanteDropDownList.SelectedValue = OBJChamado.IDUsuarioSolicitante.ToString();
            NumeroChamadoTextBox.Text = OBJChamado.NumeroChamado.ToString();
            ProjetoDropDownList.SelectedValue = OBJChamado.projeto ?? "nao";

            if (OBJChamado.projeto == "sim")
            {
                //Atribui valores
                HorasPrevistasTextBox.Text = OBJChamado.HorasPrevistas.ToString();
                HorasRealizadasTextBox.Text = OBJChamado.HorasRealizadas.ToString();
                PrevisaoEntregaTextBox.Text = OBJChamado.PrevisaoEntrega.ToString("yyyy-MM-dd");
                PrioridadeProjetoDropDownList.SelectedValue = OBJChamado.IDPrioridadeProjeto.ToString();
                HorasDesenvolvimentoDropDownList.SelectedValue = OBJChamado.IDHorasProjeto.ToString();
                DescricaoProjetoTextBox.Text = OBJChamado.DescricaoProjeto;
            }
            else
            {
                PrevisaoEntregaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void GravarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (HorasPrevistasTextBox.Text != "")
                OBJChamado.HorasPrevistas = Convert.ToInt32(HorasPrevistasTextBox.Text);
            else
                OBJChamado.HorasPrevistas = 0;

            if (HorasRealizadasTextBox.Text != "")
                OBJChamado.HorasRealizadas = Convert.ToInt32(HorasRealizadasTextBox.Text);
            else
                OBJChamado.HorasRealizadas = 0;

            OBJChamado.IDUsuarioOperacao = Convert.ToInt32(Session["IDUsuario"]);
            OBJChamado.PrevisaoEntrega = Convert.ToDateTime(PrevisaoEntregaTextBox.Text);
            OBJChamado.IDPrioridadeProjeto = Convert.ToInt32(PrioridadeProjetoDropDownList.SelectedValue);
            OBJChamado.IDHorasProjeto = Convert.ToInt32(HorasDesenvolvimentoDropDownList.SelectedValue);
            OBJChamado.DescricaoProjeto = DescricaoProjetoTextBox.Text.ToString();

            erro = OBJChamado.GravaDadosProjeto();

            if (erro == "")
            {
                OBJChamado.projeto = "sim";
                ProjetoDropDownList.SelectedValue = OBJChamado.projeto;

                //Carrega dados na Session
                Session["OBJChamado"] = OBJChamado;
            }

            ApresentaMensagem(erro);
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

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Chamados/ChamadoPrincipalWebForm.aspx?indmnu=5");
        }
    }
}