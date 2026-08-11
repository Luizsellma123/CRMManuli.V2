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
    public partial class TabelaDePrecoWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        CrmTabelaPrecoClass ObjCrmTabelaPrecoClass = new CrmTabelaPrecoClass();

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


                if (Session["ObjCrmTabelaPrecoClass"] != null)
                {
                    ObjCrmTabelaPrecoClass = (CrmTabelaPrecoClass)Session["ObjCrmTabelaPrecoClass"];

                    PesquisarPorDropDownList.SelectedValue = ObjCrmTabelaPrecoClass.PesquisarPorDropDownList;
                    PesquisarPorTextBox.Text = ObjCrmTabelaPrecoClass.PesquisarPorTextBox;

                    BuscarButton_Click(null, null);

                    Session["ObjCrmTabelaPrecoClass"] = null;

                }


            }
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            DataTable retornoDados = new DataTable();
            CrmTabelaPrecoClass ObjCrmTabelaPrecoClassAux = new CrmTabelaPrecoClass();

            switch (PesquisarPorDropDownList.SelectedValue)
            {
                case "IdTabela":
                    if (PesquisarPorTextBox.Text != "")
                    {
                        try
                        {
                            ObjCrmTabelaPrecoClassAux.IDTabela = Convert.ToInt32(PesquisarPorTextBox.Text);
                        }
                        catch
                        {
                            erro = "O IdTabela precisa ser apenas um número (sem letras, pontos ou caracteres especiais)";
                        }
                    }
                    break;

                case "Nome":
                    ObjCrmTabelaPrecoClassAux.Nome = PesquisarPorTextBox.Text;
                    break;
            }


            if (erro == "")
            {
                retornoDados = ObjCrmTabelaPrecoClassAux.RetornaTabelaPreco();

                TabelaDePrecoGridView.DataSource = retornoDados;
                TabelaDePrecoGridView.DataBind();
                TabelaDePrecoMultiView.Visible = true;

                Session["ObjCrmTabelaPrecoClass"] = ObjCrmTabelaPrecoClass;
            }
            else
            {
                ApresentaMensagem(erro);
            }
        }

        protected void NovaLinkButton_Click(object sender, EventArgs e)
        {
            Session["ObjCrmTabelaPrecoClass"] = null;
            Response.Redirect("TabelaDePrecoDetalheWebForm.aspx?indmnu=2");
        }

        protected void EditarButton_Click(object sender, EventArgs e)
        {
            Session["ObjCrmTabelaPrecoClass"] = null;
            ObjCrmTabelaPrecoClass = new CrmTabelaPrecoClass();

            ObjCrmTabelaPrecoClass.IDTabela = Convert.ToInt32(((Label)((Control)sender).FindControl("IDTabelaLabel")).Text);
            CarregaDadosDaTela();

            Session["ObjCrmTabelaPrecoClass"] = ObjCrmTabelaPrecoClass;
            Response.Redirect("TabelaDePrecoDetalheWebForm.aspx?indmnu=2");
        }

        public void CarregaDadosDaTela()
        {
            ObjCrmTabelaPrecoClass.PesquisarPorDropDownList = PesquisarPorDropDownList.SelectedValue;
            ObjCrmTabelaPrecoClass.PesquisarPorTextBox = PesquisarPorTextBox.Text;
        }


        protected void TabelaDePrecoGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            TabelaDePrecoGridView.PageIndex = e.NewPageIndex;
            BuscarButton_Click(null, null);
        }

        protected void ApresentaMensagem(string erro = "")
        {
            if (erro != "")
            {
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

    }
}