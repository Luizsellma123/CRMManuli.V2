using System;
using System.Web.UI;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI.WebControls;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;

namespace VendasWeb.Controladoria
{
    public partial class PosicaoFinanceiraWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        ControladoriaClass objControladoriaClass = new ControladoriaClass();
        DateTime primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(Session["Msg"].ToString(), true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = false;
            }

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            PeriodoInicialTextBox.Text = primeiroDiaMes.ToString("yyyy-MM-dd");

            PeriodoFinalTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

            PeriodoInicialModalTextBox.Text = primeiroDiaMes.ToString("yyyy-MM-dd");

            PeriodoFinalModalTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

            BuscarButton_Click(null, null);
        }

        protected string CarregaDadosDaTela()
        {
            objControladoriaClass.PeriodoInicial = Convert.ToDateTime(PeriodoInicialTextBox.Text);

            objControladoriaClass.PeriodoFinal = Convert.ToDateTime(PeriodoFinalTextBox.Text);

            if (objControladoriaClass.PeriodoFinal < objControladoriaClass.PeriodoInicial)
                return "O Período final não pode ser menor que o inicial.";

            objControladoriaClass.Usuario = UsuarioTextBox.Text;

            return "";
        }

        protected void GerarModalLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (PeriodoInicialModalTextBox.Text == "" || PeriodoInicialModalTextBox.Text == null)
            {
                erro = "Escolha o período incial.";

                objControladoriaClass.PeriodoInicial = Convert.ToDateTime("01-01-0001");
            }
            else
                objControladoriaClass.PeriodoInicial = Convert.ToDateTime(PeriodoInicialModalTextBox.Text);

            if (erro == "")
            {
                if (PeriodoFinalModalTextBox.Text == "" || PeriodoFinalModalTextBox.Text == null)
                {
                    erro = "Escolha o período final.";

                    objControladoriaClass.PeriodoFinal = Convert.ToDateTime("01-01-0001");
                }
                else
                    objControladoriaClass.PeriodoFinal = Convert.ToDateTime(PeriodoFinalModalTextBox.Text);
            }

            if (erro == "")
            {
                if (objControladoriaClass.PeriodoFinal < objControladoriaClass.PeriodoInicial)
                    erro = "O Período final não pode ser menor que o inicial.";
            }

            if (erro == "")
            {
                WSRetornoJSONClass objWSRetornoJSONClass = objControladoriaClass.Gera_Posicao_Diaria(Convert.ToInt32(Session["IDUsuario"]));

                erro = objWSRetornoJSONClass.MsgRetorno;
            }

            if (erro != "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "", true);

                ApresentaMensagem(erro);
            }
            else
            {
                BuscarButton_Click(null, null);
            }
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            string erro = CarregaDadosDaTela();

            if (erro == "")
            {
                PosicaoFinanceiraGridView.DataSource = objControladoriaClass.Consulta_CRM_POSICAO_DIARIA();
                PosicaoFinanceiraGridView.DataBind();
                PosicaoFinanceiraMultiView.Visible = true;
            }
            else
            {
                ApresentaMensagem(erro);
            }
        }

        protected void DetalhesLinkButton_Click(object sender, EventArgs e)
        {
            objControladoriaClass = new ControladoriaClass();

            objControladoriaClass.IDPosicaoDiaria = Convert.ToInt32(((Label)((Control)sender).FindControl("PosicaoGridViewLabel")).Text);

            objControladoriaClass.Usuario = ((Label)((Control)sender).FindControl("UsuarioGridViewLabel")).Text;

            Session["PosicaoFinanceiraDetalhe"] = objControladoriaClass;

            Response.Redirect("~/Controladoria/PosicaoFinanceiraResumoWebForm.aspx?indmnu=5");
        }

        protected void PosicaoFinanceiraGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PosicaoFinanceiraGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }

        protected void ApresentaMensagem(string erro = "")
        {
            if (erro != "")
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                erro = "Operação realizada com sucesso.";
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Controladoria/HomeControladoriaWebForm.aspx?indmnu=3");
        }
    }
}