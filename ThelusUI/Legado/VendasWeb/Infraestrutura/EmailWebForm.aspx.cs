using System;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Data;

namespace VendasWeb.Infraestrutura
{
    public partial class EmailWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        InfraestruturaClass objInfraestruturaClass = new InfraestruturaClass();
        InfraestruturaEmailClass objEmail = new InfraestruturaEmailClass();

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
                CarregaDadosNaTela();

                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
            }
        }

        protected void CarregaDadosNaTela()
        {
            if (Session["objInfraestruturaClass"] != null)
            {
                objInfraestruturaClass = (InfraestruturaClass)Session["objInfraestruturaClass"];

                DataTable InfraestruturaDataTable = objInfraestruturaClass.CarregaInfoMaquina();

                if (InfraestruturaDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in InfraestruturaDataTable.Rows)
                    {
                        MACTextBox.Text = row["MAC"].ToString();

                        IPTextBox.Text = row["IP"].ToString();

                        NomeTextBox.Text = row["Nome"].ToString();
                    }
                }

                DataTable InfraestruturaAlertasDataTable = objInfraestruturaClass.CarregaInfoAlertasEmail();

                if (InfraestruturaAlertasDataTable.Rows.Count == 0)
                {
                    GravaInfoPadrao();
                }

                InfraestruturaAlertasDataTable = objInfraestruturaClass.CarregaInfoAlertasEmail();

                foreach (DataRow row in InfraestruturaAlertasDataTable.Rows)
                {
                    EmailRemetenteTextBox.Text = row["EmailRemetente"].ToString();

                    EmailRemetenteSenhaTextBox.Text = row["EmailRemetenteSenha"].ToString();

                    EmailHostTextBox.Text = row["EmailHost"].ToString();

                    EmailPortTextBox.Text = row["EmailPort"].ToString();

                    EmailDestinatarioTextBox.Text = row["EmailDestinatario"].ToString();

                    IntervaloEnvioTextBox.Text = row["IntervaloEmailminutos"].ToString();

                    LimiteUsoCPUPorcentagemTextBox.Text = row["LimiteUsoCPUPorcentagem"].ToString();

                    LimiteUsoRAMPorcentagemTextBox.Text = row["LimiteUsoRAMPorcentagem"].ToString();

                    LimiteUsoDiscoPorcentagemTextBox.Text = row["LimiteUsoDiscoPorcentagem"].ToString();

                    if (row["Alertar"].ToString() == "Sim")
                    {
                        AlertarSimCheckBox.Checked = true;
                        AlertarNaoCheckBox.Checked = false;
                    }
                    else
                    {
                        AlertarSimCheckBox.Checked = false;
                        AlertarNaoCheckBox.Checked = true;
                    }
                }
            }
        }

        protected void GravaInfoPadrao()
        {
            objEmail.EmailRemetente = "naoresponda@manupackaging.com.br";

            objEmail.EmailRemetenteSenha = "Raiden@!1%";

            objEmail.EmailHost = "smtp.manupackaging.com.br";

            objEmail.EmailPort = "587";

            objEmail.EmailDestinatario = "ti.infraestrutura@manupackaging.com.br";

            objEmail.IntervaloEmailminutos = "10";

            objEmail.LimiteUsoCPUPorcentagem = "90";

            objEmail.LimiteUsoRAMPorcentagem = "90";

            objEmail.LimiteUsoDiscoPorcentagem = "90";

            objEmail.Alertar = "Sim";

            objEmail.UltimoAlerta = "";

            string erro = objInfraestruturaClass.SalvarAlteracoesEmail(objEmail);

            if (erro != "") ApresentaMensagem(erro);
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Infraestrutura/InfoPCWebForm.aspx?indmnu=5");
        }

        protected void SalvarAlteracoesButton_Click(object sender, EventArgs e)
        {
            if (Session["objInfraestruturaClass"] != null)
                objInfraestruturaClass = (InfraestruturaClass)Session["objInfraestruturaClass"];

            objEmail.EmailRemetente = EmailRemetenteTextBox.Text;

            objEmail.EmailRemetenteSenha = EmailRemetenteSenhaTextBox.Text;

            objEmail.EmailHost = EmailHostTextBox.Text;

            objEmail.EmailPort = EmailPortTextBox.Text;

            objEmail.EmailDestinatario = EmailDestinatarioTextBox.Text;

            objEmail.IntervaloEmailminutos = IntervaloEnvioTextBox.Text;

            objEmail.LimiteUsoCPUPorcentagem = LimiteUsoCPUPorcentagemTextBox.Text;

            objEmail.LimiteUsoRAMPorcentagem = LimiteUsoRAMPorcentagemTextBox.Text;

            objEmail.LimiteUsoDiscoPorcentagem = LimiteUsoDiscoPorcentagemTextBox.Text;

            if (AlertarSimCheckBox.Checked)
                objEmail.Alertar = "Sim";
            else if (AlertarNaoCheckBox.Checked)
                objEmail.Alertar = "Não";

            string erro = objInfraestruturaClass.SalvarAlteracoesEmail(objEmail);

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
    }
}