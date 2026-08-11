using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;

namespace VendasWeb.Entidades
{
    public partial class FrmAbaInformacoes : System.Web.UI.Page
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
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                //Verifica qual Tipo de Operacao sera possivel Realizar nessa tela para o Usuario Logado
                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                if (ObjEntidadesClass.TipoOperacao != "Inclusão")
                {
                    ObjEntidadesClass.ConsultaTipoOperacao("FrmAbaInformacoes");
                }



                //Combo Condição de Recebimento
                CondicaoRecebimentoDropDownList.DataSource = ObjEntidadesClass.Consulta_Condicao_Recebimento();
                CondicaoRecebimentoDropDownList.DataTextField = "CondPagNome";
                CondicaoRecebimentoDropDownList.DataValueField = "CondPagCod";
                CondicaoRecebimentoDropDownList.DataBind();
                CondicaoRecebimentoDropDownList.Items.Insert(0, new ListItem("Selecione", ""));

                //Combo Tipo de Cobranca
                TipoCobCodDropDownList.DataSource = ObjEntidadesClass.Consulta_Tipo_Cobranca();
                TipoCobCodDropDownList.DataTextField = "TipoCobNome";
                TipoCobCodDropDownList.DataValueField = "TipoCobCod";
                TipoCobCodDropDownList.DataBind();
                TipoCobCodDropDownList.Items.Insert(0, new ListItem("Selecione", ""));



                //Combo Categoria                
                CategoriaDropDownList.DataSource = ObjEntidadesClass.Consulta_Categoria_Entidade_Geral("Cliente");
                CategoriaDropDownList.DataTextField = "CategNome";
                CategoriaDropDownList.DataValueField = "CategCodEstr";
                CategoriaDropDownList.DataBind();
                CategoriaDropDownList.Items.Insert(0, new ListItem("Selecione", ""));


                //Carrega Dados na Tela
                CarregaDadosNaTela();

                //Verifica a operação
                switch (ObjEntidadesClass.TipoOperacao)
                {

                    case "ADM_VENDAS":
                        AlterarButton.Visible = true;

                        LiberaNavegacao();
                        break;

                    case "ADM_FISCAL":
                        LiberaNavegacao();
                        break;

                    case "ADM_FINANCEIRO":
                        AlterarButton.Visible = true;
                        CondicaoRecebimentoDropDownList.Enabled = false;//Nao pode editar a condição de Recebimento
                        EntValLimCredLabel.Visible = true;
                        EntValLimCredTextBox.Visible = true;
                        ENTQTDDIASATRASOLabel.Visible = true;
                        ENTQTDDIASATRASOTextBox.Visible = true;

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

        protected void ComoChegouRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ComoChegouDropDownList.SelectedValue == "OUTROS")
            {
                OutrosTextBox.Visible = true;
            }
            else
            {
                OutrosTextBox.Visible = false;
                OutrosTextBox.Text = "";
            }
        }


        protected void Passo5Button_Click(object sender, EventArgs e)
        {
            //Descarega a sessao
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


            if (ObjEntidadesClass.TipoOperacao != "Consulta")
            {
                //Carrega os Dados da Tela
                CarregaDadosDaTela();
            }

            //Guarda os dados em Session
            Session["clsEntidades"] = ObjEntidadesClass;


            //Chama a proxima Tela
            Response.Redirect("FrmAbaAnexo.aspx?indmnu=2");
        }


        public string CarregaDadosDaTela()
        {

            #region INFORMAÇÕES

            //Condicao de Pagamento
            ObjEntidadesClass.CondPagCod = CondicaoRecebimentoDropDownList.SelectedValue;

            //Indicacao
            ObjEntidadesClass.TipoIndicacao = ComoChegouDropDownList.SelectedValue;
            ObjEntidadesClass.Descricao = OutrosTextBox.Text.ToString().Trim();

            //Tipo de Cobranca(Forma de Pagamento)
            ObjEntidadesClass.TipoCobCod = TipoCobCodDropDownList.SelectedValue;

            //Outra Condição de Recebimento
            ObjEntidadesClass.UserOutrosCondPagCod = OutraCondPagTextBox.Text.Trim();

            ObjEntidadesClass.UserPrevisaoFaturamentoMes = Convert.ToDecimal(UserPrevisaoFaturamentoMesTextBox.Text);
            ObjEntidadesClass.UserValorPrimeiraCompra = Convert.ToDecimal(UserValorPrimeiraCompraTextBox.Text);



            if (EntValLimCredTextBox.Text != "" && EntValLimCredTextBox.Text != null)
            {
                ObjEntidadesClass.EntValLimCred = Convert.ToDecimal(EntValLimCredTextBox.Text);
            }
            else
            {
                ObjEntidadesClass.EntValLimCred = 0;
            }


            if (ENTQTDDIASATRASOTextBox.Text != "" && ENTQTDDIASATRASOTextBox.Text != null)
            {
                ObjEntidadesClass.ENTQTDDIASATRASO = Convert.ToInt32(ENTQTDDIASATRASOTextBox.Text);
            }
            else
            {
                ObjEntidadesClass.ENTQTDDIASATRASO = 0;
            }



            //Categoria
            ObjEntidadesClass.CategCodEstr = CategoriaDropDownList.SelectedValue;

            #endregion


            return "";
        }


        public string CarregaDadosNaTela()
        {

            #region INFORMAÇÕES



            //Condicao de Recebimento
            OutraCondPagTextBox.Text = ObjEntidadesClass.UserOutrosCondPagCod ?? "";
            if (ObjEntidadesClass.TipoOperacao != "Inclusão")
            {
                if (OutraCondPagTextBox.Text != "")
                {
                    CondicaoRecebimentoDropDownList.SelectedValue = "OUTRAS";
                }

                OutraCondPagLabel.Text = "Outra Condição de Recebimento foi Solicitada: ";
                CondicaoRecebimentoDropDownList_SelectedIndexChanged(null, null);
                CondicaoRecebimentoLabel.Visible = false;
                CondicaoRecebimentoDropDownList.Visible = false;
            }
            else
            {
                CondicaoRecebimentoDropDownList.SelectedValue = ObjEntidadesClass.CondPagCod;
                OutraCondPagLabel.Text = "Qual?";
                CondicaoRecebimentoDropDownList_SelectedIndexChanged(null, null);
            }


            //Indicacao
            ComoChegouDropDownList.SelectedValue = ObjEntidadesClass.TipoIndicacao;
            OutrosTextBox.Text = ObjEntidadesClass.Descricao;


            ComoChegouRadioButtonList_SelectedIndexChanged(null, null);

            //Tipo de Cobranca(Forma de Pagamento)
            TipoCobCodDropDownList.SelectedValue = ObjEntidadesClass.TipoCobCod;


            UserPrevisaoFaturamentoMesTextBox.Text = ObjEntidadesClass.UserPrevisaoFaturamentoMes.ToString();
            UserValorPrimeiraCompraTextBox.Text = ObjEntidadesClass.UserValorPrimeiraCompra.ToString();

            EntValLimCredTextBox.Text = ObjEntidadesClass.EntValLimCred.ToString();
            ENTQTDDIASATRASOTextBox.Text = ObjEntidadesClass.ENTQTDDIASATRASO.ToString();



            //Categoria
            if (ObjEntidadesClass.CategCodEstr != "" && ObjEntidadesClass.CategCodEstr != null)
            {
                string AuxcategCodEstr = ObjEntidadesClass.Lista_Categoria_Usuario_Logado();
                if (AuxcategCodEstr != "" && AuxcategCodEstr != null)
                {
                    CategoriaDropDownList.Enabled = false;
                    CategoriaDropDownList.SelectedValue = AuxcategCodEstr;
                }
            }
            else
            {
                CategoriaDropDownList.SelectedValue = ObjEntidadesClass.CategCodEstr;
            }




            #endregion


            return "";
        }




        public void BloqueiaCampos()
        {

            CondicaoRecebimentoDropDownList.Enabled = false;
            ComoChegouDropDownList.Enabled = false;
            OutrosTextBox.Enabled = false;
            Passo5Button.Visible = false;
            UserPrevisaoFaturamentoMesTextBox.Enabled = false;
            UserValorPrimeiraCompraTextBox.Enabled = false;
            TipoCobCodDropDownList.Enabled = false;

            EntValLimCredLabel.Visible = true;
            EntValLimCredTextBox.Visible = true;
            EntValLimCredTextBox.Enabled = false;

            ENTQTDDIASATRASOLabel.Visible = true;
            ENTQTDDIASATRASOTextBox.Visible = true;
            ENTQTDDIASATRASOTextBox.Enabled = false;
        }



        protected void AlterarButton_Click(object sender, EventArgs e)
        {
            string Retorno = "";
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            //Carrega os Dados da tela para alterar
            CarregaDadosDaTela();
            //Pega o usuario que esta alterando
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

            //Altera 
            Retorno = ObjEntidadesClass.Alterando_Informacoes_Entidade();


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
            Passo5Button.Visible = false;
            PrincipalButton.Visible = true;
            ContatoButton.Visible = true;
            EnderecoEntregaButton.Visible = true;
            AnexosButton.Visible = true;
            ObservacoesButton.Visible = true;
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

        protected void CondicaoRecebimentoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CondicaoRecebimentoDropDownList.SelectedValue == "OUTRAS")
            {
                OutraCondPagLabel.Visible = true;
                OutraCondPagTextBox.Visible = true;
            }
            else
            {
                OutraCondPagLabel.Visible = false;
                OutraCondPagTextBox.Visible = false;
                OutraCondPagTextBox.Text = "";
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