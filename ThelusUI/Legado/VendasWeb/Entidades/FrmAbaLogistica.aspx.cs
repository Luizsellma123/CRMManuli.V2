using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.Entidades
{
    public partial class FrmAbaLogistica : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        usuario ObjUsuarioClass = new usuario();
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        funcoes mdlFuncoes = new funcoes();
        criptografia mdlCriptografia = new criptografia();


        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                if (Session["clsEntidades"] != null)
                {
                    //Descarrega a Sessao
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                    //Verifica qual Tipo de Operacao sera possivel Realizar nessa tela para o Usuario Logado
                    ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                    if (ObjEntidadesClass.TipoOperacao != "Inclusão")
                    {
                        ObjEntidadesClass.ConsultaTipoOperacao("FrmAbaLogistica");
                    }

                    //Carrega Dados na Tela
                    CarregaDadosNaTela();

                    //Verifica a operação
                    switch (ObjEntidadesClass.TipoOperacao)
                    {

                        case "LOGISTICA":
                            LiberaNavegacao();
                            AlterarButton.Visible = true;
                            break;

                        case "ADM_VENDAS":
                            LiberaNavegacao();
                            BloqueiaCampos();
                            break;

                        case "ADM_FISCAL":
                            LiberaNavegacao();
                            BloqueiaCampos();
                            break;

                        case "ADM_FINANCEIRO":
                            LiberaNavegacao();
                            BloqueiaCampos();
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
                            BloqueiaCampos();

                            break;


                    }
                }
            }

        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            switch (drpEntCod.SelectedValue.ToString())
            {
                case "1":
                    ObjEntidadesClass.EntNomeFant = txtFiltroEntCod.Text;
                    break;

                case "2":
                    ObjEntidadesClass.EntNome = txtFiltroEntCod.Text;
                    break;

                case "3":
                    ObjEntidadesClass.EntCod = txtFiltroEntCod.Text;
                    break;

                case "4":
                    ObjEntidadesClass.EntCpfCgc = txtFiltroEntCod.Text;
                    break;

                case "5":
                    ObjEntidadesClass.CIDNOME = txtFiltroEntCod.Text;
                    break;

                case "6":
                    ObjEntidadesClass.UFSIGLA = txtFiltroEntCod.Text;
                    break;
            }





            ListaEntidadeGridView.DataSource = ObjEntidadesClass.Consulta_Transportadora();
            ListaEntidadeGridView.DataBind();
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

        protected void EntTransporteOMesmoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (EntTransporteOMesmoDropDownList.SelectedValue.ToUpper())
            {

                case "NÃO":
                    TransportadorasMultView.Visible = true;
                    EntTranspCodTextBox.Text = "";
                    EntTranspCodEntLabel.Visible = true;
                    EntTranspCodTextBox.Visible = true;
                    EntStatFreteVendaDropDownList.Enabled = true;
                    break;

                case "SIM":
                    TransportadorasMultView.Visible = false;
                    EntTranspCodTextBox.Text = "";
                    EntStatFreteVendaDropDownList.SelectedValue = "Destinatário";
                    EntStatFreteVendaDropDownList.Enabled = false;
                    EntTranspCodEntLabel.Visible = false;
                    EntTranspCodTextBox.Visible = false;

                    break;

            }
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
            Retorno = ObjEntidadesClass.Altera_Logistica_Entidade();


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

        protected void SelecionarCheckedChanged(object sender, EventArgs e)
        {
            CheckBox CheckBox = (CheckBox)sender;
            GridViewRow oldGridViewRow = (GridViewRow)CheckBox.NamingContainer;

            //Desmarca todos os check
            foreach (GridViewRow OldGridView in ListaEntidadeGridView.Rows)
            {
                //Seta todos como falso
                ((RadioButton)OldGridView.FindControl("EntTranspCodEntRadioButton")).Checked = false;
            }

            //marcando o RadioButton selecionado
            RadioButton RadioButton = (RadioButton)sender;
            GridViewRow GridViewRow = (GridViewRow)RadioButton.NamingContainer;
            ((RadioButton)GridViewRow.FindControl("EntTranspCodEntRadioButton")).Checked = true;

            //Carregando o Campo de Transportadora na tela
            EntTranspCodTextBox.Text = ((Label)((Control)sender).FindControl("EntCodLabel")).Text;
        }



        public void LiberaNavegacao()
        {

            PrincipalButton.Visible = true;
            ContatoButton.Visible = true;
            InformacoesButton.Visible = true;
            AnexosButton.Visible = true;
            ObservacoesButton.Visible = true;
            FiscalLinkButton.Visible = true;
            HoldingLinkButton.Visible = true;
            EnderecoEntregaButton.Visible = true;
            VendedorLinkButton.Visible = true;
            PedidosLinkButton.Visible = true;
            AgendaLinkButton.Visible = true;
            NotasLinkButton.Visible = true;

            //Verifica se o Usuario possui algum Vendedor //Funcao temporaria para OCultar campos
            if (ObjUsuarioClass.ConsultaVendedorUsuario(Session["usuario"].ToString()) != 0)
            {
                HoldingLinkButton.Visible = false;
                
            }



        }


        public void BloqueiaCampos()
        {
            EntTransporteOMesmoDropDownList.Enabled = false;
            EntStatFreteVendaDropDownList.Enabled = false;
            TransportadorasMultView.Visible = false;
            UserShelfLifeTextBox.Enabled = false;

        }





        public string CarregaDadosDaTela()
        {

            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
            ObjEntidadesClass.EntTransporteOMesmo = EntTransporteOMesmoDropDownList.SelectedValue;
            ObjEntidadesClass.EntStatFreteVenda = EntStatFreteVendaDropDownList.SelectedValue;
            ObjEntidadesClass.UserShelfLife = Convert.ToInt32(UserShelfLifeTextBox.Text);

            if (ObjEntidadesClass.EntTransporteOMesmo == "Sim")
            {
                ObjEntidadesClass.EntTranspCod = null;
            }
            else
            {
                ObjEntidadesClass.EntTranspCod = EntTranspCodTextBox.Text;
            }




            return "";

        }


        public string CarregaDadosNaTela()
        {

            EntTransporteOMesmoDropDownList.SelectedValue = ObjEntidadesClass.EntTransporteOMesmo;
            EntTransporteOMesmoDropDownList_SelectedIndexChanged(null, null);
            EntStatFreteVendaDropDownList.SelectedValue = ObjEntidadesClass.EntStatFreteVenda;
            EntTranspCodTextBox.Text = ObjEntidadesClass.EntTranspCod ?? "";
            UserShelfLifeTextBox.Text = ObjEntidadesClass.UserShelfLife.ToString();


            return "";

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