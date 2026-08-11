using System;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.Logistica_New
{
    public partial class CadastroTransportadorDetalheWebForm : System.Web.UI.Page
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
            if (Session["CadastroTransportador"] != null)
                objLogistica = (LogisticaClass)Session["CadastroTransportador"];

            if (objLogistica.Operacao == "Alteracao" || objLogistica.IDTransportador != 0)
            {
                this.CadastroTransportadorWebUserControl.LiberaMenus(true);

                objLogistica.Filtro = objLogistica.IDTransportador.ToString();

                objLogistica.TipoFiltro = "Detalhe";

                DataTable ListaTransportador = objLogistica.RetornaListaTransportador();

                if (ListaTransportador.Rows.Count > 0)
                {
                    foreach (DataRow row in ListaTransportador.Rows)
                    {
                        CodigoTextBox.Text = row["IDTransportador"].ToString();
                        DescricaoTextBox.Text = row["Descricao"].ToString();

                        if (row["Ativo"].ToString() == "True")
                            StatusDropDownList.SelectedValue = "1";
                        else
                            StatusDropDownList.SelectedValue = "0";
                    }
                }
            }
            else
            {
                this.CadastroTransportadorWebUserControl.LiberaMenus(false);
            }
        }

        protected string CarregaDadosDaTela()
        {
            objLogistica.IDTransportador = CodigoTextBox.Text == "" ? 0 : Convert.ToInt32(CodigoTextBox.Text);
            objLogistica.Descricao = DescricaoTextBox.Text;
            objLogistica.Ativo = Convert.ToInt32(StatusDropDownList.SelectedValue);

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

                erro = objLogistica.GravaTransportador();
            }

            if (erro == "")
            {
                objLogistica.Operacao = "Alteracao";
                Session["CadastroTransportador"] = objLogistica;
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
            Response.Redirect("~/Logistica_New/CadastroTransportadorWebForm.aspx?indmnu=5");
        }
    }
}