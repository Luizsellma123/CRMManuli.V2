using System;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.Logistica_New
{
    public partial class StatusFechamentoFaturaDetalheWebForm : System.Web.UI.Page
    {
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass objSessao = new SessionClass();
        LogisticaClass objLogistica = new LogisticaClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                CarregaDadosNaTela();
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        protected void CarregaDadosNaTela()
        {
            if (Session["Logistica"] != null)
                objLogistica = (LogisticaClass)Session["Logistica"];

            if (objLogistica.Operacao == "Alteracao")
            {
                objLogistica.Filtro = objLogistica.IDStatus.ToString();
                objLogistica.TipoFiltro = "Detalhe";

                DataTable ListaStatusFechamentoFatura = objLogistica.RetornaListaStatusFechamentoFatura();

                if (ListaStatusFechamentoFatura.Rows.Count > 0)
                {
                    foreach (DataRow row in ListaStatusFechamentoFatura.Rows)
                    {
                        CodigoTextBox.Text = row["IDStatus"].ToString();
                        BloqueadoDropDownList.SelectedValue = row["Bloqueado"].ToString();
                        DescricaoTextBox.Text = row["Descricao"].ToString();
                        AtivoDropDownList.SelectedValue = row["Ativo"].ToString();
                    }
                }
            }
        }

        protected string CarregaDadosDaTela()
        {
            objLogistica.IDStatus = CodigoTextBox.Text == "" ? 0 : Convert.ToInt32(CodigoTextBox.Text);
            objLogistica.Bloqueado = Convert.ToInt32(BloqueadoDropDownList.SelectedValue);
            objLogistica.Descricao = DescricaoTextBox.Text;
            objLogistica.Ativo = Convert.ToInt32(AtivoDropDownList.SelectedValue);

            if (objLogistica.Descricao == "") return "Informe a descrição.";

            return "";
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = CarregaDadosDaTela();

            if (erro == "")
            {
                if (Session["IDUsuario"] != null)
                    objLogistica.IDUsuarioAlteracao = Convert.ToInt32(Session["IDUsuario"].ToString());

                erro = objLogistica.GravaStatusFechamentoFatura();
            }

            if (erro == "")
            {
                objLogistica.Operacao = "Alteracao";
                Session["Logistica"] = objLogistica;
                CarregaDadosNaTela();
            }

            ApresentaMensagem(erro);
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
            Response.Redirect("~/Logistica_New/StatusFechamentoFaturaWebForm.aspx?indmnu=5");
        }
    }
}