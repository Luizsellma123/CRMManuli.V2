using System;
using System.Collections.Generic;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.Entidades
{
    public partial class FrmAbaObservacoes : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        criptografia mdlCriptografia = new criptografia();
        usuario ObjUsuarioClass = new usuario();


        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                if (Session["clsEntidades"] != null)
                {
                    //Descarega a session da Entidade
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                    //Verifica qual Tipo de Operacao sera possivel Realizar nessa tela para o Usuario Logado
                    ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                    if (ObjEntidadesClass.TipoOperacao != "Inclusão")
                    {
                        ObjEntidadesClass.ConsultaTipoOperacao("FrmAbaObservacoes");
                    }

                    CarregaDadosNaTela();

                    //Verifica a operação
                    switch (ObjEntidadesClass.TipoOperacao)
                    {

                        case "ADM_VENDAS":
                            AlterarButton.Visible = true;
                            LiberaNavegacao();
                            break;

                        case "ADM_FISCAL":
                            AlterarButton.Visible = true;
                            LiberaNavegacao();
                            break;

                        case "ADM_FINANCEIRO":
                            AlterarButton.Visible = true;
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



                            break;


                    }

                }
            }
        }


        protected void FinalizarButton_Click(object sender, EventArgs e)
        {
            if (ObjEntidadesClass.TipoOperacao != "Consulta")
            {
                CarregaDadosDaTela();
            }
            Response.Redirect("../Entidades/FrmFinalizaCadastroEntidade.aspx?indmnu=2");
        }

        public void CarregaDadosDaTela()
        {
            //Descarega a session da Entidade
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            ObjEntidadesClass.EntTextoHist = ObservacaoTextBox.Text.Trim();
            Session["clsEntidades"] = ObjEntidadesClass;
        }

        public void CarregaDadosNaTela()
        {
            //Descarega a session da Entidade
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            if (ObjEntidadesClass.EntTextoHist != null && ObjEntidadesClass.EntTextoHist != "")
            {
                ObservacaoAnteriorTextBox.Text = ObjEntidadesClass.EntTextoHist;
                ObservacaoAnteriorLabel.Visible = true;
                ObservacaoAnteriorTextBox.Visible = true;


            }




        }

        public void bloqueiaCampo()
        {
            ObservacaoLabel.Visible = false;
            ObservacaoTextBox.Visible = false;
            FinalizarButton.Visible = false;
        }

        protected void AlterarButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
            Session["Msg"] = ObjEntidadesClass.Atualizar_Historico_Entidade();
            Session["clsEntidades"] = null;

            //Verifica se a alteração não esta sendo feita em uma entidade ja ativa, se estiver vai enviar para Cadastro Incompleto
            ObjEntidadesClass.Alterar_Status_Entidade_Cadastro_Incompleto();

            if (Session["Retornar"] != null)
            {
                Response.Redirect(Session["Retornar"].ToString());
            }
            else
            {

                Response.Redirect("FrmCarteira.aspx?indmnu=2");
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
            FinalizarButton.Visible = false;
            PrincipalButton.Visible = true;
            ContatoButton.Visible = true;
            EnderecoEntregaButton.Visible = true;
            InformacoesButton.Visible = true;
            AnexosButton.Visible = true;

            FiscalLinkButton.Visible = true;
            HoldingLinkButton.Visible = true;
            LogisticaLinkButton.Visible = true;
            VendedorLinkButton.Visible = true;
            PedidosLinkButton.Visible = true;
            AgendaLinkButton.Visible = true;
            NotasLinkButton.Visible = true;


            //Verifica se o Usuario possui algum Vendedor //Funcao temporaria para OCultar campos
            if (ObjUsuarioClass.ConsultaVendedorUsuario(Session["usuario"].ToString()) != 0)
            {
                HoldingLinkButton.Visible = false;
                LogisticaLinkButton.Visible = false;
            }


        }

        protected void FiscalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaFiscal.aspx?indmnu=2");
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