using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.Entidades
{
    public partial class FrmAbaFinanceiro : System.Web.UI.Page
    {

        usuario ObjUsuarioClass = new usuario();
        funcoes mdlFuncoes = new funcoes();
        SessionClass OBJSessao = new SessionClass();
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        GerencialVendas.clsEntWeb ObjclsEntWeb = new GerencialVendas.clsEntWeb();
        GerencialVendas.ContatoClass ObjContatoClass = new GerencialVendas.ContatoClass();
        criptografia mdlCriptografia = new criptografia();



        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {


                Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                Session.Remove("Msg");
            }

            if (!IsPostBack)
            {

                if (Session["clsEntidades"] != null)
                {

                    /*Tratar Abrir e fechar Div*/
                    collapseLiteral.Text = "<div id=\"filtros\" class=\"collapse in\" runat=\"server\">";


                    //Descarega a session da Entidade
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                    //Carrega dados na Tela
                    if (ObjEntidadesClass.TipoOperacao != "")//Incluir ou Alterar
                    {
                        CarregaDadosNaTela();


                    }



                }


            }










        }



        public string CarregaDadosNaTela()
        {


            EntNomeLabel.Text = ObjEntidadesClass.EntNome;

            if (ObjEntidadesClass.CepCod != null)
            {
                ObjEntidadesClass.Busca_Endereco();
            }

            EnderecoLabel.Text = ObjEntidadesClass.EntLograd.ToString();

            if (ObjEntidadesClass.EntEnder != null)
            {
                EnderecoLabel.Text += " ";
                EnderecoLabel.Text += ObjEntidadesClass.EntEnder.ToString();
            }

            if (ObjEntidadesClass.EntEnderNo != null)
            {
                EnderecoLabel.Text += ", ";
                EnderecoLabel.Text += ObjEntidadesClass.EntEnderNo.ToString();
            }


            if (ObjEntidadesClass.EntBair != null)
            {
                EnderecoLabel.Text += " - ";
                EnderecoLabel.Text += ObjEntidadesClass.EntBair.ToString();
            }

            if (ObjEntidadesClass.UFSIGLA != null)
            {
                EnderecoLabel.Text += "/";
                EnderecoLabel.Text += ObjEntidadesClass.UFSIGLA.ToString();
            }


            StatusComercialLabel.Text = ObjEntidadesClass.StatEntComercial;

          

            /*Grid Cond Pag*/
            AtualizaGridCondPag();


            return "";

        }

        

        protected void CondPagEntCondGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            if (Session["clsEntidades"] != null)
            {

                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }

            CondPagEntCondGridView.PageIndex = e.NewPageIndex;
            AtualizaGridCondPag();
        }


        public void AtualizaGridCondPag()
        {
            CondPagEntCondGridView.DataSource = ObjEntidadesClass.ListCondPag;
            CondPagEntCondGridView.DataBind();
        }

    }
}