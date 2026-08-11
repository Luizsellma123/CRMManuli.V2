using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;

namespace VendasWeb.telasRelatorio
{
    public partial class parmRelatorioTabelaDinamicaFitasa : System.Web.UI.Page
    {
        funcoes mdlfuncoes = new funcoes();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                drpEmpresa.DataSource = mdlfuncoes.Consulta_Empresa(Session["usuario"].ToString());
                drpEmpresa.DataTextField = "EmpNome";
                drpEmpresa.DataValueField = "EmpCod";
                drpEmpresa.SelectedValue = "Todas";
                drpEmpresa.DataBind();

                drpLinhaProduto.DataSource = mdlfuncoes.Consulta_Linha_Produto();
                drpLinhaProduto.DataTextField = "DescProduto";
                drpLinhaProduto.DataValueField = "LinhaProduto";
                drpLinhaProduto.SelectedValue = "Todos";
                drpLinhaProduto.DataBind();

                btnGerar.Attributes.Add("onclick", "javascript:return validaDados();");
            }
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            if (drpEmpresa.SelectedItem.Text == "Todas")
            {
                Session["empresa"] = "";
            }
            else
            {
                Session["empresa"] = drpEmpresa.SelectedItem.Value;
            }

            Session["dataInicial"] = mdlfuncoes.FormataData(txtDataInicial.Text);
            Session["datafinal"] = mdlfuncoes.FormataData(txtDataFinal.Text);
            Session["entidade"] = txtEntidade.Text;
            Session["natureza"] = txtNatureza.Text;

            if (drpLinhaProduto.SelectedItem.Text == "Todos")
            {
                Session["linha"] = "";
            }
            else
            {
                Session["linha"] = drpLinhaProduto.SelectedItem.Value;
            }

            Session["produto"] = txtDescricao.Text;
            Session["subFamilia"] = txtSubFamilia.Text;

            Response.Redirect("../relatorios/frmTabelaDinamicaFitasa.aspx?indmnu=4");
        }
    }
}