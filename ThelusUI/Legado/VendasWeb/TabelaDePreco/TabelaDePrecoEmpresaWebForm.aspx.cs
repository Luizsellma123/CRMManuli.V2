using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;


namespace VendasWeb.TabelaDePreco
{
    public partial class TabelaDePrecoEmpresaWebForm : System.Web.UI.Page
    {

        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        CrmTabelaPrecoClass ObjCrmTabelaPrecoClass = new CrmTabelaPrecoClass();
        CrmTabelaPrecoEmpresaClass ObjCrmTabelaPrecoEmpresaClass = new CrmTabelaPrecoEmpresaClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

            if (Session["Msg"] != null)
            {

                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;

            }


            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"false\">";


                CarregaCombo();


                if (Session["ObjCrmTabelaPrecoClass"] != null)
                {

                    ObjCrmTabelaPrecoClass = (CrmTabelaPrecoClass)Session["ObjCrmTabelaPrecoClass"];

                    if (ObjCrmTabelaPrecoClass.IDTabela > 0)
                    {
                        ObjCrmTabelaPrecoClass.ManutencaoTabelaPreco();

                        CarregaDadosNaTela();
                    }

                }

            }

        }


        public void CarregaCombo()
        {
            DataTable RetornoDados = new DataTable();
            CrmEmpresaFilialClass ObjCrmEmpresaFilialClassAux = new CrmEmpresaFilialClass();

            RetornoDados = ObjCrmEmpresaFilialClassAux.RetornaEmpresaFilial();
            IDEmpresaDropDownList.DataSource = RetornoDados;
            IDEmpresaDropDownList.DataValueField = "IDEmpresa";
            IDEmpresaDropDownList.DataTextField = "NomeEmpresa";
            IDEmpresaDropDownList.DataBind();
            IDEmpresaDropDownList.Items.Insert(0, new ListItem("Selecione", ""));
        }


        public void CarregaDadosNaTela()
        {
            IDTabelaTextBox.Text = ObjCrmTabelaPrecoClass.IDTabela.ToString();
            NomeTextBox.Text = ObjCrmTabelaPrecoClass.Nome;

            AtualizaGrid();


        }

        public void CarregaDadosDaTela()
        {
            ObjCrmTabelaPrecoClass = (CrmTabelaPrecoClass)Session["ObjCrmTabelaPrecoClass"];

            ObjCrmTabelaPrecoEmpresaClass = new CrmTabelaPrecoEmpresaClass();
            ObjCrmTabelaPrecoEmpresaClass.CodigoUsuario = Session["usuario"].ToString();
            ObjCrmTabelaPrecoEmpresaClass.IDTabela = ObjCrmTabelaPrecoClass.IDTabela;
            ObjCrmTabelaPrecoEmpresaClass.IDEmpresa = Convert.ToInt32(IDEmpresaDropDownList.SelectedValue);
            
        }


        protected void GravarButton_Click(object sender, EventArgs e)
        {


            string erro = "";

            if (Session["ObjCrmTabelaPrecoClass"] != null)
            {
                
                CarregaDadosDaTela();

                erro = ObjCrmTabelaPrecoEmpresaClass.GravaTabelaEmpresa();

            }
            else
            {
                erro = "Session foi finalizada antes da conclusão da Operação, favor sair e tentar novamente";
            }

            if (erro == "")
            {

                
                LimpaCampos();
                AtualizaGrid();


                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Empresa Vinculada a Tabela com Sucesso!", true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();


            }
            else
            {
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

            }

        }

        public void LimpaCampos()
        {
            IDEmpresaDropDownList.SelectedValue = "";
        }

        public void AtualizaGrid()
        {
            DataTable retornoDados = new DataTable();

            ObjCrmTabelaPrecoEmpresaClass = new CrmTabelaPrecoEmpresaClass();
            ObjCrmTabelaPrecoEmpresaClass.IDTabela = ((CrmTabelaPrecoClass)Session["ObjCrmTabelaPrecoClass"]).IDTabela;

            retornoDados = ObjCrmTabelaPrecoEmpresaClass.RetornaTabelaEmpresa();

            EmpresaGridView.DataSource = retornoDados;
            EmpresaGridView.DataBind();
            EmpresaMultiView.Visible = true;
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("TabelaDePrecoDetalheWebForm.aspx?indmnu=2");
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {


            string erro = "";

            if (Session["ObjCrmTabelaPrecoClass"] != null)
            {
                
                ObjCrmTabelaPrecoClass = (CrmTabelaPrecoClass)Session["ObjCrmTabelaPrecoClass"];

                ObjCrmTabelaPrecoEmpresaClass = new CrmTabelaPrecoEmpresaClass();
                ObjCrmTabelaPrecoEmpresaClass.CodigoUsuario = Session["usuario"].ToString();
                ObjCrmTabelaPrecoEmpresaClass.IDTabela = ObjCrmTabelaPrecoClass.IDTabela;
                ObjCrmTabelaPrecoEmpresaClass.IDEmpresa = Convert.ToInt32(((Label)((Control)sender).FindControl("IDEmpresaLabel")).Text);

                erro = ObjCrmTabelaPrecoEmpresaClass.ExcluiTabelaEmpresa();
            }
            else
            {
                erro = "Session foi finalizada antes da conclusão da Operação, favor sair e tentar novamente";
            }


            if (erro == "")
            {
                
                LimpaCampos();
                AtualizaGrid();

                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Tabela desvinculada da Empresa com Sucesso!", true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();


            }
            else
            {
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

            }

        }
    }
}