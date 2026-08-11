using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace VendasWeb.AdministracaoSistema
{
    public partial class CadastroParametroGeralWebForm : System.Web.UI.Page
    {
        SessionClass objSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        usuario ObjUsuario = new usuario();
        ParametroGeral ObjParametroGeral = new ParametroGeral();
        modulo ObjModulo = new modulo();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            objSessao.ValidaAcesso();

            if (Session["Msg"] != null)
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Session["Msg"].ToString(), true);
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
                CarregaCombos();
                CarregaDadosTela();
            }
            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
        }

        public void CarregaCombos()
        {
            EmpresaDropDownList.DataSource = ObjUsuario.RetornaEmpresas();
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataBind();

            ObjModulo.Filtro = "";
            ModuloDropDownList.DataSource = ObjModulo.RetornaModulos();
            ModuloDropDownList.DataTextField = "Nome";
            ModuloDropDownList.DataValueField = "Codigo";
            ModuloDropDownList.DataBind();
        }

        public void CarregaDadosTela()
        {
            if (Session["ParametroGeral"] != null)
            {
                ObjParametroGeral = (ParametroGeral)Session["ParametroGeral"];
            }

            if (ObjParametroGeral.Operacao == "alteracao")
            {
                EmpresaDropDownList.SelectedValue = ObjParametroGeral.IDEmpresa.ToString();
                EmpresaDropDownList.Enabled = false;
                ParametroTextBox.Text = ObjParametroGeral.NomeParametro;
                ParametroTextBox.Enabled = false;
                ModuloDropDownList.SelectedValue = ObjParametroGeral.IDModulo.ToString();
                ModuloDropDownList.Enabled = false;
                DescricaoTextBox.Text = ObjParametroGeral.DescricaoParametro;
                DescricaoTextBox.Enabled = false;
                TextoTextBox.Text = ObjParametroGeral.ValorTexto;
                //TextoTextBox.Enabled = false;
                NumericoTextBox.Text = ObjParametroGeral.ValorNumerico.ToString();
                //NumericoTextBox.Enabled = false;

            }
        }

        protected string VerificaCamposPreenchidos()
        {
            string erro = "";

            if (EmpresaDropDownList.SelectedValue == null || EmpresaDropDownList.SelectedValue == "")
            {
                erro = "Escolha uma empresa.";
            }
            else if (ParametroTextBox.Text == "" || ParametroTextBox.Text == null)
            {
                erro = "Informe o parâmetro.";
            }
            else if (ModuloDropDownList.SelectedItem.ToString() == "" || ModuloDropDownList.SelectedItem == null)
            {
                erro = "Escolha um módulo.";
            }
            else if (DescricaoTextBox.Text == "" || DescricaoTextBox.Text == null)
            {
                erro = "Informe uma descrição.";
            }
            else if ((TextoTextBox.Text == "" || TextoTextBox.Text == null) && (NumericoTextBox.Text == "" || NumericoTextBox.Text == null))
            {
                erro = "Informe o valor do texto ou valor do numérico.";
            }
            //else if (NumericoTextBox.Text == "" || NumericoTextBox.Text == null)
            //{
            //    erro = "Informe o valor do numérico.";
            //}

            return erro;
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            erro = VerificaCamposPreenchidos();

            if (erro == "")
            {
                if (Session["ParametroGeral"] != null)
                {
                    ObjParametroGeral = (ParametroGeral)Session["ParametroGeral"];
                }

                ObjParametroGeral.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
                ObjParametroGeral.NomeParametro = ParametroTextBox.Text;
                ObjParametroGeral.IDModulo = Convert.ToInt32(ModuloDropDownList.SelectedValue);
                ObjParametroGeral.DescricaoParametro = DescricaoTextBox.Text;

                if (TextoTextBox.Text == "" || TextoTextBox.Text == null)
                {
                    ObjParametroGeral.ValorTexto = "";
                }
                else
                {
                    ObjParametroGeral.ValorTexto = TextoTextBox.Text;
                }

                if (NumericoTextBox.Text == "" || NumericoTextBox.Text == null)
                {
                    ObjParametroGeral.ValorNumerico = 0;
                }
                else
                {
                    ObjParametroGeral.ValorNumerico = Convert.ToInt32(NumericoTextBox.Text);
                }

                erro = ObjParametroGeral.AdicionaParametroGeral();
            }

            if (erro != "")
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                ObjParametroGeral.Operacao = "alteracao";

                Session["ParametroGeral"] = ObjParametroGeral;

                CarregaCombos();
                CarregaDadosTela();

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Parâmetro salvo com sucesso.", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministracaoSistema/ParametrosGeraisWebForm.aspx?indmnu=5");
        }
    }
}