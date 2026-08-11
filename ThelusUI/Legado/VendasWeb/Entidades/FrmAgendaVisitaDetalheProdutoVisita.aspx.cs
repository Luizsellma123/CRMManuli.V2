using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidades
{
    public partial class FrmAgendaVisitaDetalheProdutoVisita : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        public produto ObjProduto = new produto();
        public ProdutoVisitaClass ObjProdutoVisita = new ProdutoVisitaClass();
        public AgendaVisitaClass ObjAgendaVisitaClass = new AgendaVisitaClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {


                //Linha do Produto
                ObjProduto = new produto();
                ObjProduto.UsuCod = Session["usuario"].ToString();

                LinhaProdutoDropDownList.DataSource = ObjProduto.Consulta_Linha_Produto();
                LinhaProdutoDropDownList.DataTextField = "USERLINHAPRODUTOLISTA";
                LinhaProdutoDropDownList.DataValueField = "USERLINHAPRODUTOLISTA";
                LinhaProdutoDropDownList.DataBind();
                LinhaProdutoDropDownList.Items.Insert(0, new ListItem("Selecione", ""));
                ProdutoDropDownList.Items.Insert(0, new ListItem("Selecione a Linha do Produto", ""));
                



                if (Session["ObjProdutoVisita"] != null)
                {
                    ObjProdutoVisita = (ProdutoVisitaClass)Session["ObjProdutoVisita"];
                    CarregaDadosNaTela();
                }


            }


        }

        protected void LinhaProdutoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjProduto = new produto();

            if (LinhaProdutoDropDownList.SelectedValue != "")
            {

                ObjProduto.USERLINHAPRODUTOLISTA = LinhaProdutoDropDownList.SelectedValue;

                ProdutoDropDownList.DataSource = ObjProduto.Consulta_Produto();
                ProdutoDropDownList.DataTextField = "ProdNome";
                ProdutoDropDownList.DataValueField = "ProdCodEstr";
                ProdutoDropDownList.DataBind();

                switch (LinhaProdutoDropDownList.SelectedValue.ToUpper())
                {
                    case "FITA PP":
                    case "FITA IMP":
                    case "ESP. IND.":
                    case "MAQ E EQUIP":
                        ClasseQtdRadioButtonList.Items[0].Text = "A - <b>Maior</b> que R$5.000";
                        ClasseQtdRadioButtonList.Items[1].Text = "B - <b>De</b> R$1.000 <b>Até</b> R$5.000 ";
                        ClasseQtdRadioButtonList.Items[2].Text = "C - <b>Até</b> R$1.000";

                        break;


                    case "STRETCH":
                        ClasseQtdRadioButtonList.Items[0].Text = "A - <b>Maior</b> que 5.000Kg";
                        ClasseQtdRadioButtonList.Items[1].Text = "B - <b>De</b> 1.000Kg <b>Até</b> 5.000Kg ";
                        ClasseQtdRadioButtonList.Items[2].Text = "C - <b>Até</b> 1.000Kg";


                        break;
                }


                    
               

            }

            ProdutoDropDownList.Items.Insert(0, new ListItem("Selecione", ""));
                

            

        }

        protected void VoltarLinkButton_Click(object sender, EventArgs e)
        {
            Session["ObjProdutoVisita"] = null;
            Response.Redirect("FrmAgendaVisitaDetalhe.aspx?indmnu=5");
        }

        protected void SalvarLinkButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();

            ObjAgendaVisitaClass = (GerencialVendas.AgendaVisitaClass)Session["ObjAgendaVisitaClass"];

            if (ObjProdutoVisita.TipoOperacao == "Incluir")
            {
                ObjAgendaVisitaClass.Adicionar_ProdutoVisita(ObjProdutoVisita);
            }
            else
            {
                ObjAgendaVisitaClass.Altera_ProdutoVisita(ObjProdutoVisita);
            }


            Session["ObjAgendaVisitaClass"] = ObjAgendaVisitaClass;
            Response.Redirect("FrmAgendaVisitaDetalhe.aspx?indmnu=5");

        }


        public void CarregaDadosNaTela()
        {
            LinhaProdutoDropDownList.SelectedValue = ObjProdutoVisita.USERLINHAPRODUTOLISTA;
            LinhaProdutoDropDownList_SelectedIndexChanged(null, null);

            ProdutoDropDownList.SelectedValue = ObjProdutoVisita.ProdCodEstr;
            ClasseQtdRadioButtonList.SelectedValue = ObjProdutoVisita.ClasseQtd;
            PrazoPotencialMesCorrenteRadioButtonList.SelectedValue = ObjProdutoVisita.PrazoPotencialMesCorrente;
            PrazoPotencialMes1RadioButtonList.SelectedValue = ObjProdutoVisita.PrazoPotencialMes1;
            PrazoPotencialMes3RadioButtonList.SelectedValue = ObjProdutoVisita.PrazoPotencialMes3;
            PrazoPotencialMesSuperiorRadioButtonList.SelectedValue = ObjProdutoVisita.PrazoPotencialMesSuperior;

            if (Session["ObjAgendaVisitaClass"] != null)
            {
                ObjAgendaVisitaClass = (GerencialVendas.AgendaVisitaClass)Session["ObjAgendaVisitaClass"];
                if (ObjAgendaVisitaClass.AgendaStatus.ToUpper() == "FINALIZADA")
                {
                    SalvarLinkButton.Visible = false;
                }
            }


        }

        public void CarregaDadosDaTela()
        {

            if (Session["ObjProdutoVisita"] != null)
            {
                ObjProdutoVisita = (ProdutoVisitaClass)Session["ObjProdutoVisita"];
            }
            else
            {
                ObjProdutoVisita = new ProdutoVisitaClass();

            }


            if (ObjProdutoVisita.PRODUTO_VISITA_ID != 0)
            {
                ObjProdutoVisita.TipoOperacao = "Alterar";
            }
            else
            {
                ObjProdutoVisita.TipoOperacao = "Incluir";
            }

            ObjProdutoVisita.USERLINHAPRODUTOLISTA = LinhaProdutoDropDownList.SelectedValue;
            ObjProdutoVisita.ProdNome = ProdutoDropDownList.SelectedItem.Text;
            ObjProdutoVisita.ProdCodEstr = ProdutoDropDownList.SelectedValue;
            ObjProdutoVisita.ClasseQtd = ClasseQtdRadioButtonList.SelectedValue;
            ObjProdutoVisita.PrazoPotencialMesCorrente = PrazoPotencialMesCorrenteRadioButtonList.SelectedValue;
            ObjProdutoVisita.PrazoPotencialMes1 = PrazoPotencialMes1RadioButtonList.SelectedValue;
            ObjProdutoVisita.PrazoPotencialMes3 = PrazoPotencialMes3RadioButtonList.SelectedValue;
            ObjProdutoVisita.PrazoPotencialMesSuperior = PrazoPotencialMesSuperiorRadioButtonList.SelectedValue;





        }


    }
}