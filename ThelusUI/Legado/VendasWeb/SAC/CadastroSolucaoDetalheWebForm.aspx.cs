using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.SAC
{
    public partial class CadastroSolucaoDetalheWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        SACClass ObjSAC = new SACClass();

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
            if (Session["CadastroSolucao"] != null)
            {
                ObjSAC = (SACClass)Session["CadastroSolucao"];

                ObjSAC.Solucao = ObjSAC.IDSolucao.ToString();
                DataTable ListaSolucoes = ObjSAC.RetornaListaSolucoes();

                if (ListaSolucoes.Rows.Count > 0)
                {
                    foreach (DataRow row in ListaSolucoes.Rows)
                    {
                        if (row["IDSolucao"].ToString() == ObjSAC.IDSolucao.ToString())
                        {
                            CodigoTextBox.Text = ObjSAC.IDSolucao.ToString();
                            AtivoDropDownList.SelectedValue = Convert.ToInt32(Convert.ToBoolean(row["Ativo"])).ToString();
                            DescricaoTextBox.Text = row["Descricao"].ToString();
                            PadraoDropDownList.SelectedValue = Convert.ToInt32(Convert.ToBoolean(row["Padrao"])).ToString();
                        }
                    }
                }
            }

            PadraoDropDownList.SelectedValue = "0";
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            if (DescricaoTextBox.Text != "")
            {
                ObjSAC.IDSolucao = Convert.ToInt32(CodigoTextBox.Text == "" ? "0" : CodigoTextBox.Text);
                ObjSAC.Ativo = Convert.ToBoolean(Convert.ToInt32(AtivoDropDownList.SelectedValue));
                ObjSAC.Descricao = DescricaoTextBox.Text;
                ObjSAC.Padrao = Convert.ToBoolean(Convert.ToInt32(PadraoDropDownList.SelectedValue));
                if (Session["IDUsuario"] != null) ObjSAC.IDUsuario = Convert.ToInt32(Session["IDUsuario"].ToString());

                erro = ObjSAC.GravaSolucao();

                if (erro == "") Session["CadastroSolucao"] = ObjSAC;

                CarregaDadosNaTela();
            }
            else
            {
                erro = "Informe uma descrição.";
            }

            ApresentaMensagem(erro);
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SAC/CadastroSolucaoWebForm.aspx?indmnu=5");
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
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }
    }
}