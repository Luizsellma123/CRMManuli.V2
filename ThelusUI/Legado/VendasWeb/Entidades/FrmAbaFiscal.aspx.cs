using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.Entidades
{
    public partial class FrmAbaFiscal : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        usuario ObjUsuarioClass = new usuario();
        funcoes mdlFuncoes = new funcoes();
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        criptografia mdlCriptografia = new criptografia();


        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                //Verifica se a session esta Carregada
                if (Session["clsEntidades"] != null)
                {

                    //Descarega a session da Entidade
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                    //Verifica qual Tipo de Operacao sera possivel Realizar nessa tela para o Usuario Logado
                    ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                    if (ObjEntidadesClass.TipoOperacao != "Inclusão")
                    {
                        ObjEntidadesClass.ConsultaTipoOperacao("FrmAbaFiscal");
                    }

                    //Carrega Dados na Tela
                    CarregaDadosNaTela();

                    //Verifica a operação
                    switch (ObjEntidadesClass.TipoOperacao)
                    {

                        case "ADM_VENDAS":
                            LiberaNavegacao();
                            AlterarButton.Visible = true;
                            break;

                        case "ADM_FISCAL":
                            AlterarButton.Visible = true;
                            LiberaNavegacao();
                            break;

                        case "ADM_FINANCEIRO":
                            LiberaNavegacao();
                            break;

                        case "Cadastro Incompleto":
                            #region
                            AlterarButton.Visible = true;
                            LiberaNavegacao();
                            #endregion
                            break;

                        case "Cadastro Completo":
                            #region
                            AlterarButton.Visible = true;
                            LiberaNavegacao();
                            #endregion
                            break;

                        case "Consulta":
                            LiberaNavegacao();

                            //Se tiver em status "Cadastro Incompleto" libera campo para Finalizar Cadastro
                            if (ObjEntidadesClass.StatEntCod == "13")
                            {
                                AlterarButton.Visible = true;
                            }
                            else
                            {
                                BloqueiaCampos();
                            }



                            break;


                    }

                }
            }

        }

        protected void NaturezaJuridicaDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (NaturezaJuridicaDropDownList.SelectedValue == "Entidade Governamental")
            {
                ClassificacaoLabel.Visible = true;
                ClassificacaoDropDownList.Visible = true;
                ClassificacaoDropDownList.CausesValidation = true;

            }
            else
            {
                ClassificacaoLabel.Visible = false;
                ClassificacaoDropDownList.Visible = false;
                ClassificacaoDropDownList.CausesValidation = false;

            }


        }

        protected void Passo2Button_Click(object sender, EventArgs e)
        {
            //Verifica se a session esta Carregada
            if (Session["clsEntidades"] != null)
            {

                //Descarega a session da Entidade
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                if (ObjEntidadesClass.TipoOperacao != "Consulta")
                {
                    //Carrega os Dados da Tela
                    CarregaDadosDaTela();
                }

                //Guarda os dados em Session
                Session["clsEntidades"] = ObjEntidadesClass;


                //Chama a proxima Tela
                Response.Redirect("FrmAbaEnderecoEntrega.aspx?indmnu=2");

            }

        }

        public string CarregaDadosDaTela()
        {
            //Optante Simples
            ObjEntidadesClass.EntOptanteSimplesFed = OptanteSimplesDropDownList.SelectedValue;


            //Natureza
            ObjEntidadesClass.EntNat = NaturezaJuridicaDropDownList.SelectedValue;
            ObjEntidadesClass.EntNatGov = ClassificacaoDropDownList.SelectedValue;



            //Finalidade Produto
            ObjEntidadesClass.UserEntFinalidadeProduto = FinalidadeProdutoDropDownList.SelectedValue;



            return "";

        }

        public string CarregaDadosNaTela()
        {
            //Optante Simples
            OptanteSimplesDropDownList.SelectedValue = ObjEntidadesClass.EntOptanteSimplesFed;




            /*Se pessoa Fisica a Natureza e Finalidade do Produto serao fixas*/
            //Natureza
            //Finalidade Produto
            if (ObjEntidadesClass.EntTipoFJ == "Física")
            {
                FinalidadeProdutoDropDownList.SelectedValue = "CONSUMO";
                NaturezaJuridicaDropDownList.SelectedValue = "Consumidor";
                FinalidadeProdutoDropDownList.Enabled = false;
                NaturezaJuridicaDropDownList.Enabled = false;
            }
            else
            {
                FinalidadeProdutoDropDownList.SelectedValue = ObjEntidadesClass.UserEntFinalidadeProduto;
                NaturezaJuridicaDropDownList.SelectedValue = ObjEntidadesClass.EntNat;

                FinalidadeProdutoDropDownList.Enabled = true;
                NaturezaJuridicaDropDownList.Enabled = true;
            }





            //Verifica Natureza
            NaturezaJuridicaDropDownList_SelectedIndexChanged(null, null);

            //Classificacao
            ClassificacaoDropDownList.SelectedValue = ObjEntidadesClass.EntNatGov;






            return "";

        }

        public void BloqueiaCampos()
        {
            OptanteSimplesDropDownList.Enabled = false;
            FinalidadeProdutoDropDownList.Enabled = false;
            NaturezaJuridicaDropDownList.Enabled = false;
            ClassificacaoDropDownList.Enabled = false;
            Passo2Button.Visible = false;


        }

        protected void AlterarButton_Click(object sender, EventArgs e)
        {

            string Retorno = "";
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            //Carrega os Dados da tela para alterar
            CarregaDadosDaTela();
            //Pega o usuario que esta alterando
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
            //Altera Fiscal Entidade
            Retorno = ObjEntidadesClass.Altera_Fiscal_Entidade();


            //Verifica se a alteração não esta sendo feita em uma entidade ja ativa, se estiver vai enviar para Cadastro Incompleto
            ObjEntidadesClass.Alterar_Status_Entidade_Cadastro_Incompleto();

            if (Retorno != "")
            {

                Response.Write("<script>alert(\"" + Retorno + "\");</script>");

            }
            else
            {
                Session["clsEntidades"] = ObjEntidadesClass;
                Response.Write("<script>alert(\"Cadastro Atualizado com Sucesso!\");</script>");

            }




        }

        protected void PrincipalButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaPrincipal.aspx?indmnu=2");
        }

        protected void ContatoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaContatos.aspx?indmnu=2");
        }

        protected void EnderecoEntregaButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaEnderecoEntrega.aspx?indmnu=2");
        }

        protected void InformacoesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaInformacoes.aspx?indmnu=2");
        }

        protected void AnexosButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaAnexo.aspx?indmnu=2");
        }

        protected void ObservacoesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaObservacoes.aspx?indmnu=2");
        }

        public void LiberaNavegacao()
        {

            Passo2Button.Visible = false;
            PrincipalButton.Visible = true;
            ContatoButton.Visible = true;
            EnderecoEntregaButton.Visible = true;
            InformacoesButton.Visible = true;
            AnexosButton.Visible = true;
            ObservacoesButton.Visible = true;
            HoldingLinkButton.Visible = true;
            LogisticaLinkButton.Visible = true;
            VendedorLinkButton.Visible = true;

            //Verifica se o Usuario possui algum Vendedor //Funcao temporaria para OCultar campos
            if (ObjUsuarioClass.ConsultaVendedorUsuario(Session["usuario"].ToString()) != 0)
            {
                HoldingLinkButton.Visible = false;
                LogisticaLinkButton.Visible = false;
            }


        }

        protected void HoldingButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmHolding.aspx?indmnu=2");
        }

        protected void CancelarOperacaoButton_Click(object sender, EventArgs e)
        {

            Session.Remove("ObjEntidadesClass");

            if (Session["Retornar"] != null)
            {
                Response.Redirect(Session["Retornar"].ToString());
            }
            else
            {

                Response.Redirect("FrmCarteira.aspx?indmnu=2");
            }
        }

        protected void LogisticaButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaLogistica.aspx?indmnu=2");
        }

        protected void VendedorButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmVendedorEntidade.aspx?indmnu=2");
        }


        protected void CrmButton_Click(object sender, EventArgs e)
        {

            Response.Redirect("FrmHistoricoCRM.aspx?indmnu=12");

        }

        protected void DuplicatasButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaDuplicata.aspx?indmnu=2");
        }

        protected void PedidosButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../listas/FrmListaPedidos.aspx?indmnu=2");
        }

        protected void NotasButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmListaNotasFiscais.aspx?indmnu=2");
        }

        protected void AgendaButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAgenda.aspx?indmnu=2");
        }


    }
}