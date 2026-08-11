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
    public partial class TabelaDePrecoDetalheWebForm : System.Web.UI.Page
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

                    if (ObjCrmTabelaPrecoClass.IDTabela > 0)
                    {
                        ObjCrmTabelaPrecoClass.ManutencaoTabelaPreco();
                        CarregaDadosNaTela();
                    }
                    
                }

            }
        }


        public void CarregaDadosNaTela()
        {
            IDTabelaTextBox.Text = ObjCrmTabelaPrecoClass.IDTabela.ToString();
            NomeTextBox.Text = ObjCrmTabelaPrecoClass.Nome;
            StatusDropDownList.SelectedValue = ObjCrmTabelaPrecoClass.Status;

            
            IDTabelaDivL.Visible = true;
            IDTabelaDivT.Visible = true;
            StatusDiv.Visible = true;
        }

        public void CarregaDadosDaTela()
        {
            
            if(IDTabelaTextBox.Text != "")
            {
                ObjCrmTabelaPrecoClass.IDTabela = Convert.ToInt32(IDTabelaTextBox.Text);
            }

            ObjCrmTabelaPrecoClass.CodigoUsuario = Session["usuario"].ToString();
            ObjCrmTabelaPrecoClass.Nome = NomeTextBox.Text;
            ObjCrmTabelaPrecoClass.Status = StatusDropDownList.SelectedValue;
        }


        protected void GravarButton_Click(object sender, EventArgs e)
        {

            string erro = "";
            

            if (Session["ObjCrmTabelaPrecoClass"] != null)
            {
                ObjCrmTabelaPrecoClass = (CrmTabelaPrecoClass)Session["ObjCrmTabelaPrecoClass"];
            }
            else
            {
                ObjCrmTabelaPrecoClass = new CrmTabelaPrecoClass();
            }


            CarregaDadosDaTela();

            

             erro = ObjCrmTabelaPrecoClass.GravaTabelaPreco();

            if (erro == "")
            {
                if (IDTabelaTextBox.Text == "")
                {

                    ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Tabela Incluida com Sucesso!", true);
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();


                    this.UCTabelaPreco.LiberaNavegacao();

                    CarregaDadosNaTela();

                }
                else
                {



                    ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Tabela Alterada com Sucesso!", true);
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();


                }
                //Carrega Dados Atualizados em Sessão
                Session["ObjCrmTabelaPrecoClass"] = ObjCrmTabelaPrecoClass;


            }
            else
            {
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

            }

        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("TabelaDePrecoWebForm.aspx?indmnu=2");
        }
    }
}