using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;

namespace VendasWeb.Logistica_New
{
    public partial class FechamentoFaturaWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        LogisticaClass objLogistica = new LogisticaClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                CarregaCombos();
                BuscarLinkButton_Click(null, null);
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected string CarregaDadosDaTela()
        {
            objLogistica.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
            objLogistica.DataInicial = DataInicialTextBox.Text == "" ? "" : Convert.ToDateTime(DataInicialTextBox.Text).ToString("yyyy-MM-dd");
            objLogistica.DataFinal = DataFinalTextBox.Text == "" ? "" : Convert.ToDateTime(DataFinalTextBox.Text).ToString("yyyy-MM-dd");
            objLogistica.NumeroFatura = Convert.ToInt32(NumeroFaturaTextBox.Text == "" ? "0" : NumeroFaturaTextBox.Text);
            objLogistica.IDStatus = Convert.ToInt32(StatusDropDownList.SelectedValue);
            objLogistica.Parceiro = ParceiroTextBox.Text;
            objLogistica.Fechamento = Convert.ToInt32(FechamentoTextBox.Text == "" ? "0" : FechamentoTextBox.Text);

            if (DataInicialTextBox.Text != "" && DataFinalTextBox.Text != "")
                if (Convert.ToDateTime(objLogistica.DataInicial) > Convert.ToDateTime(objLogistica.DataFinal))
                    return "A data incial não pode ser maior que a final.";

            return "";
        }

        protected void CarregaCombos()
        {
            usuario ObjUsuario = new usuario();

            ObjUsuario.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            EmpresaDropDownList.DataSource = ObjUsuario.ListaEmpresasUsuario();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();

            objLogistica.TipoFiltro = "StatusDropDownList";
            objLogistica.Filtro = "";
            StatusDropDownList.DataSource = objLogistica.RetornaListaStatusFechamentoFatura();
            StatusDropDownList.DataTextField = "Descricao";
            StatusDropDownList.DataValueField = "IDStatus";
            StatusDropDownList.DataBind();
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = CarregaDadosDaTela();

            if (erro == "")
            {
                GridView.DataSource = objLogistica.RetornaListaFechamentoFatura();
                GridView.DataBind();
                MultiView.Visible = true;
            }
            else
                ApresentaMensagem(erro);
        }

        protected void NovoLinkButton_Click(object sender, EventArgs e)
        {
            objLogistica.Operacao = "Inclusao";
            Session["Logistica"] = objLogistica;
            Response.Redirect("~/Logistica_New/FechamentoFaturaDetalheWebForm.aspx?indmnu=5");
        }

        protected void SelecionarGridViewLinkButton_Click(object sender, EventArgs e)
        {
            objLogistica.Operacao = "Alteracao";
            objLogistica.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaGridViewLabel")).Text);
            objLogistica.Fechamento = Convert.ToInt32(((Label)((Control)sender).FindControl("FechamentoGridViewLabel")).Text);
            Session["Logistica"] = objLogistica;
            Response.Redirect("~/Logistica_New/FechamentoFaturaDetalheWebForm.aspx?indmnu=5");
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;

            BuscarLinkButton_Click(null, null);
        }

        protected void ApresentaMensagem(string erro)
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
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Logistica_New/HomeWebForm.aspx?indmnu=5");
        }

    }
}