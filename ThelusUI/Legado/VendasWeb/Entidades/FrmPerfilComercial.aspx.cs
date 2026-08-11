using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.Entidades
{
    public partial class FrmPerfilComercial : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        usuario ObjUsuarioClass = new usuario();
        funcoes mdlFuncoes = new funcoes();

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

            if (ObjEntidadesClass.EntDataCad != null && ObjEntidadesClass.EntDataCad.ToString() != "")
            {
                EntDataCadLabel.Text = ObjEntidadesClass.EntDataCad.ToString("dd/MM/yyyy");
            }


            if (ObjEntidadesClass.ListVendEnt != null)
            {
                if (ObjEntidadesClass.ListVendEnt.Count > 0)
                {
                    VendNomeLabel.Text = ObjEntidadesClass.ListVendEnt[0].VendNome;
                }
            }


            if (ObjEntidadesClass.NFDataEmis.ToString() != "" && ObjEntidadesClass.NFDataEmis != null)
            {
                NFDataEmisLabel.Text = ObjEntidadesClass.NFDataEmis.ToString("dd/MM/yyyy");
            }

            if (ObjEntidadesClass.EntValLimCred != null)
            {
                EntValLimCredLabel.Text = "R$" + ObjEntidadesClass.EntValLimCred.ToString();
            }

            if (ObjEntidadesClass.SaldoLimiteCliente != null)
            {
                SaldoLimiteClienteLabel.Text = "R$" + ObjEntidadesClass.SaldoLimiteCliente.ToString();
            }

            /*Grid Produtos*/
            AtualizaGridProduto();
            

            /*Grid Cond Pag*/
            AtualizaGridCondPag();

            /*Grid Duplicatas*/
            AtualizaGridDuplicata();

            AtualizarTotalFamiliaEternidadeGridView();
            AtualizarTotalFamiliaSemestreGridView();

            return "";

        }

        protected void ListaProdutosGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }

            ListaProdutosGridView.PageIndex = e.NewPageIndex;
            AtualizaGridProduto();
        }

        public void AtualizaGridProduto()
        {
            ListaProdutosGridView.DataSource = ObjEntidadesClass.Mostra_Perfil_Comercial_Produto();
            ListaProdutosGridView.DataBind();
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
            CondPagEntCondGridView.DataSource = ObjEntidadesClass.Consulta_Cod_Pag_Pedidos_EntCod();
            CondPagEntCondGridView.DataBind();
        }

        
        protected void DuplicatasGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }

            DuplicatasGridView.PageIndex = e.NewPageIndex;
            AtualizaGridDuplicata();
        }


        public void AtualizaGridDuplicata()
        {
            DuplicatasGridView.DataSource = ObjEntidadesClass.Mostra_Duplicatas();
            DuplicatasGridView.DataBind();
        }

        public void AtualizarTotalFamiliaSemestreGridView()
        {
            TotalFamiliaSemestreGridView.DataSource = ObjEntidadesClass.Lista_Total_Vendido_Semestre_Familia_Produto();
            TotalFamiliaSemestreGridView.DataBind();
        }

        public void AtualizarTotalFamiliaEternidadeGridView()
        {
            TotalFamiliaEternidadeGridView.DataSource = ObjEntidadesClass.Lista_Total_Vendido_Eternidade_Familia_Produto();
            TotalFamiliaEternidadeGridView.DataBind();
        }

        protected void TotalFamiliaSemestreGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }

            TotalFamiliaSemestreGridView.PageIndex = e.NewPageIndex;
            AtualizarTotalFamiliaSemestreGridView();
        }

        protected void TotalFamiliaEternidadeGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {

            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }

            TotalFamiliaEternidadeGridView.PageIndex = e.NewPageIndex;
            AtualizarTotalFamiliaEternidadeGridView();
        }

    }
}