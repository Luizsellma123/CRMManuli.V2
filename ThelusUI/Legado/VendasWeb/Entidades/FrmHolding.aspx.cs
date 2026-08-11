using System;
using System.Collections.Generic;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidades
{

    public partial class FrmHolding : System.Web.UI.Page
    {

        SessionClass OBJSessao = new SessionClass();
        usuario ObjUsuarioClass = new usuario();
        funcoes mdlFuncoes = new funcoes();
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        GerencialVendas.clsCondPag ObjCondPag = new GerencialVendas.clsCondPag();
        criptografia mdlCriptografia = new criptografia();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {

                if (Session["clsEntidades"] != null)
                {
                    //Descarrega session
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                    //Verifica qual Tipo de Operacao sera possivel Realizar nessa tela para o Usuario Logado
                    ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                    if (ObjEntidadesClass.TipoOperacao != "Inclusão")
                    {
                        ObjEntidadesClass.ConsultaTipoOperacao("FrmHolding");
                    }


                    //Carrega dados na Tela
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
                            BloqueiaCampos();

                            break;


                    }


                }
            }
        }

        protected void NovaCondicaoButton_Click(object sender, EventArgs e)
        {
            CarregaDadosDaTela();
            Response.Redirect("FrmCondPag.aspx?indmnu=2");
        }

        protected void RemoverButton_Click(object sender, EventArgs e)
        {
            if (Session["clsEntidades"] != null)
            {


                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

                ObjCondPag = new clsCondPag();
                ObjCondPag.EntCod = ObjEntidadesClass.EntCod;
                ObjCondPag.CondPagCod = ((Label)((Control)sender).FindControl("CondPagCodLabel")).Text;
                ObjCondPag.TipoOperacao = "Remover";

                ObjEntidadesClass.RemoverCondPag(ObjCondPag);


                Session["clsEntidades"] = ObjEntidadesClass;

                Atualizar_Grid();

            }
        }

        protected void AlterarButton_Click(object sender, EventArgs e)
        {

            string Retorno = "";
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            CarregaDadosDaTela();

            #region Gravar Condições
            if (ObjEntidadesClass.ListCondPag != null)
            {
                if (ObjEntidadesClass.ListCondPag.Count > 0)
                {
                    //Percorre a lista de contatos
                    for (int i = 0; i < ObjEntidadesClass.ListCondPag.Count; i++)
                    {
                        ObjEntidadesClass.ListCondPag[i].UsuCod = Session["usuario"].ToString();
                        ObjEntidadesClass.ListCondPag[i].EntCod = ObjEntidadesClass.EntCod;

                        //Se igual a incluir
                        if (ObjEntidadesClass.ListCondPag[i].TipoOperacao == "Incluir")
                        {
                            Retorno += ObjEntidadesClass.ListCondPag[i].Incluir_Cond_Pag_Ent();
                        }
                        else
                        {
                            //se alterar
                            if (ObjEntidadesClass.ListCondPag[i].TipoOperacao == "Alterar")
                            {
                                Retorno += ObjEntidadesClass.ListCondPag[i].Altera_Cond_Pag_Ent();
                            }
                            else
                            {
                                //Se Remover
                                if (ObjEntidadesClass.ListCondPag[i].TipoOperacao == "Remover")
                                {
                                    Retorno += ObjEntidadesClass.ListCondPag[i].Remove_Cond_Pag_Ent();
                                }
                            }

                        }
                    }
                }
            }
            #endregion

            //Alterando Holding
            ObjEntidadesClass.Altera_Holding();


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

        protected void FiscalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaFiscal.aspx?indmnu=2");
        }


        public void Atualizar_Grid()
        {
            if (ObjEntidadesClass.ListCondPag != null)
            {
                clsEntidades ObjEntidadesClassTemp = new clsEntidades();

                for (int i = 0; i < ObjEntidadesClass.ListCondPag.Count; i++)
                {
                    ObjCondPag = new GerencialVendas.clsCondPag();
                    ObjCondPag.CondPagNome = ObjEntidadesClass.ListCondPag[i].CondPagNome;
                    ObjCondPag.CondPagCod = ObjEntidadesClass.ListCondPag[i].CondPagCod;
                    ObjCondPag.CondPagEntValAte = ObjEntidadesClass.ListCondPag[i].CondPagEntValAte;
                    ObjCondPag.TipoOperacao = ObjEntidadesClass.ListCondPag[i].TipoOperacao;

                    if (ObjCondPag.TipoOperacao != "Remover")
                    {
                        ObjEntidadesClassTemp.AdicionarCondPag(ObjCondPag);
                    }

                }


                //Carrega Grid na Tela
                CondPagEntCondGridView.DataSource = ObjEntidadesClassTemp.ListCondPag;
                CondPagEntCondGridView.DataBind();

            }


        }

        public void BloqueiaCampos()
        {
            NovaCondicaoButton.Visible = false;
            HoldingTextBox.Enabled = false;
        }

        public void LiberaNavegacao()
        {
            PrincipalButton.Visible = true;
            EnderecoEntregaButton.Visible = true;
            InformacoesButton.Visible = true;
            AnexosButton.Visible = true;
            ObservacoesButton.Visible = true;

            FiscalLinkButton.Visible = true;
            ContatoButton.Visible = true;
            LogisticaLinkButton.Visible = true;
            VendedorLinkButton.Visible = true;
            PedidosLinkButton.Visible = true;
            AgendaLinkButton.Visible = true;
            NotasLinkButton.Visible = true;

            //Verifica se o Usuario possui algum Vendedor //Funcao temporaria para OCultar campos
            if (ObjUsuarioClass.ConsultaVendedorUsuario(Session["usuario"].ToString()) != 0)
            {
                
                LogisticaLinkButton.Visible = false;
            }


        }


        public void CarregaDadosNaTela()
        {
            //Nivel de Comercialização
            HoldingTextBox.Text = ObjEntidadesClass.NIVCOD;
            Atualizar_Grid();




        }


        public void CarregaDadosDaTela()
        {
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            ObjEntidadesClass.NIVCOD = HoldingTextBox.Text.Trim();

            Session["clsEntidades"] = ObjEntidadesClass;
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

        protected void btnListar_Click(object sender, EventArgs e)
        {

            DescricaoHoldingLabel.Visible = true;

            switch (drpHolding.SelectedValue.ToString())
            {
                case "1":
                    ObjEntidadesClass.NIVCOD = txtFiltro.Text;
                    break;

                case "2":
                    ObjEntidadesClass.NivNome = txtFiltro.Text;
                    break;


            }





            HoldingGridView.DataSource = ObjEntidadesClass.Consulta_Holding();
            HoldingGridView.DataBind();
        }




        protected void SelecionarCheckedChanged(object sender, EventArgs e)
        {
            CheckBox CheckBox = (CheckBox)sender;
            GridViewRow oldGridViewRow = (GridViewRow)CheckBox.NamingContainer;

            //Desmarca todos os check
            foreach (GridViewRow OldGridView in HoldingGridView.Rows)
            {
                //Seta todos como falso
                ((RadioButton)OldGridView.FindControl("NivCodRadioButton")).Checked = false;
            }

            //marcando o RadioButton selecionado
            RadioButton RadioButton = (RadioButton)sender;
            GridViewRow GridViewRow = (GridViewRow)RadioButton.NamingContainer;
            ((RadioButton)GridViewRow.FindControl("NivCodRadioButton")).Checked = true;

            //Carregando o Campo com a Holding
            HoldingTextBox.Text = ((Label)((Control)sender).FindControl("NivCodLabel")).Text;
        }

        protected void CondPagCodPrincipalCheckedChanged(object sender, EventArgs e)
        {

            CheckBox CheckBox = (CheckBox)sender;
            GridViewRow oldGridViewRow = (GridViewRow)CheckBox.NamingContainer;

            //Desmarca todos os check
            foreach (GridViewRow OldGridView in CondPagEntCondGridView.Rows)
            {
                //Seta todos como falso
                ((RadioButton)OldGridView.FindControl("CondPagCodRadioButton")).Checked = false;
            }

            //marcando o RadioButton selecionado
            RadioButton RadioButton = (RadioButton)sender;
            GridViewRow GridViewRow = (GridViewRow)RadioButton.NamingContainer;
            ((RadioButton)GridViewRow.FindControl("CondPagCodRadioButton")).Checked = true;




            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
            ObjEntidadesClass.CondPagCod = ((Label)((Control)sender).FindControl("CondPagCodLabel")).Text;
            ObjEntidadesClass.Altera_CondPagCod_Entidade();


        }

        protected void CondPagEntCondGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                if ((e.Row.DataItem != null))
                {
                    //Carrega DropDownlist E-mail
                    string AuxCondPagCod = ((Label)e.Row.FindControl("CondPagCodLabel")).Text;

                    if (ObjEntidadesClass.CondPagCod == AuxCondPagCod)
                    {
                        ((RadioButton)e.Row.FindControl("CondPagCodRadioButton")).Checked = true;
                    }


                }
            }



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