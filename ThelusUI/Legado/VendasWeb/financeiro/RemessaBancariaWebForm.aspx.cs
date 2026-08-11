using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;

namespace VendasWeb.financeiro
{
    public partial class RemessaBancariaWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        FiltroClass ObjFiltroClass = new FiltroClass();
        FinanceiroClass OBJFinanceiro = new FinanceiroClass();
        funcoes mdlFuncoes = new funcoes();
        UtilClass ObjUtilClass = new UtilClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

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

            if (Session["OBJFinanceiro"] != null)
            {
                //Descarega a session Financeiro
                OBJFinanceiro = (FinanceiroClass)Session["OBJFinanceiro"];
            }


            if (!IsPostBack)
            {
                //Carrega Combos
                CarregaCombos();

                //Carrega dados
                CarregaDadosNaTela();
               
            }

            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            string aux = "";
        }

        protected void RemessasGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RemessasGridView.PageIndex = e.NewPageIndex;
            //CarregaGrid();
        }

        protected void RetornarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/financeiro/ListaBancosWebForm.aspx?indmnu=3");
        }

        protected void GravarRemessaButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            OBJFinanceiro.IDUsuarioCRM = Convert.ToInt32(Session["IDUsuario"]);
            OBJFinanceiro.IDEmpresa = Convert.ToInt32(EmpresaDropDownList.SelectedValue);
            OBJFinanceiro.IDBanco = Convert.ToInt32(BancoDropDownList.SelectedValue);
            OBJFinanceiro.IDAgencia = Convert.ToInt32(AgenciaDropDownList.SelectedValue);
            OBJFinanceiro.IDContaCorrente = Convert.ToInt32(ContaCorrenteDropDownList.SelectedValue);
            OBJFinanceiro.IDStatusRemessa = Convert.ToInt32(StatusDropDownList.SelectedValue);
            OBJFinanceiro.DataRemessa = Convert.ToDateTime(DataTextBox.Text);

            erro = OBJFinanceiro.GravaDadosPrincipaisRemessa();

            if (erro == "")
            {
                FinanceiroBancosRemessasWebUserControl.LiberaNavegacao();
                TrataDadosPrincipais();

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Remessa gravada com sucesso!", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro("Erro na inclusão da remessa!", true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }

        }

        public void CarregaCombos()
        {
            //Inserindo datasource para dropdown empresa
            EmpresaDropDownList.DataSource = mdlFuncoes.Consulta_Empresa(Session["usuario"].ToString());
            EmpresaDropDownList.DataValueField = "IDEmpresa";
            EmpresaDropDownList.DataTextField = "NomeEmpresa";
            EmpresaDropDownList.DataBind();

            //Inserindo datasource para dropdown Status
            StatusDropDownList.DataSource = OBJFinanceiro.RecuperaStatusRemessas();
            StatusDropDownList.DataValueField = "IDStatus";
            StatusDropDownList.DataTextField = "DescricaoStatus";
            StatusDropDownList.DataBind();

            //Recupera Bancos
            BancoDropDownList.DataSource = OBJFinanceiro.RecuperaBancos();
            BancoDropDownList.DataValueField = "IDBanco";
            BancoDropDownList.DataTextField = "NomeBanco";
            BancoDropDownList.DataBind();

            //Carrega combo Agência bancária
            CarregaComboAgenciaBancaria();

            //Carrega combo Conta Corrente
            CarregaComboContaCorrente();

        }

        public void CarregaComboAgenciaBancaria()
        {
            OBJFinanceiro.IDBanco = Convert.ToInt32(BancoDropDownList.SelectedValue);
            AgenciaDropDownList.DataSource = OBJFinanceiro.RecuperaBancoAgencias();
            AgenciaDropDownList.DataValueField = "IDAgencia";
            AgenciaDropDownList.DataTextField = "NomeAgencia";
            AgenciaDropDownList.DataBind();
        }

        public void CarregaComboContaCorrente()
        {
            OBJFinanceiro.IDBanco = Convert.ToInt32(BancoDropDownList.SelectedValue);
            OBJFinanceiro.IDAgencia = Convert.ToInt32(AgenciaDropDownList.SelectedValue);
            ContaCorrenteDropDownList.DataSource = OBJFinanceiro.RecuperaBancoAgenciaContas();
            ContaCorrenteDropDownList.DataValueField = "IDConta";
            ContaCorrenteDropDownList.DataTextField = "DescricaoConta";
            ContaCorrenteDropDownList.DataBind();
        }

        protected void BancoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaComboAgenciaBancaria();
            CarregaComboContaCorrente();
        }

        protected void AgenciaDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaComboContaCorrente();
        }

        public void CarregaDadosNaTela()
        {
            if (OBJFinanceiro.NumeroRemessa == "" || OBJFinanceiro.NumeroRemessa == null)
            {
                DataTextBox.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }else
            {

                //Trata Dados Principais
                TrataDadosPrincipais();

                //Libera navegação
                FinanceiroBancosRemessasWebUserControl.LiberaNavegacao();
            }
        }

        public void TrataDadosPrincipais()
        {
            NumeroRemessaTextBox.Text = OBJFinanceiro.NumeroRemessa;
            DataTextBox.Text = OBJFinanceiro.DataRemessa.ToString("yyyy-MM-dd");
            StatusDropDownList.SelectedValue = OBJFinanceiro.IDStatusRemessa.ToString();
            EmpresaDropDownList.SelectedValue = OBJFinanceiro.IDEmpresa.ToString();

            //Carrega o combo da agencia bancaria
            CarregaComboAgenciaBancaria();

            AgenciaDropDownList.SelectedValue = OBJFinanceiro.IDAgencia.ToString();

            //Carrega o com da conta corrente
            CarregaComboContaCorrente();

            ContaCorrenteDropDownList.SelectedValue = OBJFinanceiro.IDContaCorrente.ToString();

            //Bloqueia campos para edição
            EmpresaDropDownList.Enabled = false;
            AgenciaDropDownList.Enabled = false;
            BancoDropDownList.Enabled = false;
            ContaCorrenteDropDownList.Enabled = false;
        }
    }
}