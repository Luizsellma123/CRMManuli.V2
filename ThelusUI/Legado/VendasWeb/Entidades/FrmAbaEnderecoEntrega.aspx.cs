using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;

namespace VendasWeb.Entidades
{
    public partial class FrmAbaEnderecoEntrega : System.Web.UI.Page
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
                if (Session["clsEntidades"] != null)
                {
                    //Descarrega a Sessao
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                    //Verifica qual Tipo de Operacao sera possivel Realizar nessa tela para o Usuario Logado
                    ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                    if (ObjEntidadesClass.TipoOperacao != "Inclusão")
                    {
                        ObjEntidadesClass.ConsultaTipoOperacao("FrmAbaEnderecoEntrega");
                    }

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

        protected void EntragaCnpjTextBox_TextChanged(object sender, EventArgs e)
        {
            mdlFuncoes = new funcoes();
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            string Retorno = "";
            string AuxEntCpfCgc = EntragaCnpjTextBox.Text.Trim().Replace("-", "");
            AuxEntCpfCgc = AuxEntCpfCgc.Replace(".", "");
            AuxEntCpfCgc = AuxEntCpfCgc.Replace("/", "");

            Retorno = mdlFuncoes.Valida_CPF_CNPJ(AuxEntCpfCgc, "");

            if (Retorno != "Valido")
            {
                Response.Write("<script>alert(\"CPF/CNPJ" + " " + AuxEntCpfCgc + "<br> " + Retorno + "\");</script>");

                EntragaCnpjTextBox.Text = "";
                EntragaCnpjTextBox.Focus();

            }
            else
            {
                if (ObjEntidadesClass.EntCpfCgc == AuxEntCpfCgc)
                {
                    Response.Write("<script>alert(\"CPF/CNPJ" + " " + AuxEntCpfCgc + "<br> Igual ao Informado na Aba Principal\");</script>");
                    EntragaCnpjTextBox.Text = "";
                    EntragaCnpjTextBox.Focus();

                }
                else
                {
                    RazaoSocialEntregaTextBox.Focus();
                }
            }



        }


        #region Endereco Entrega
        protected void CepEntregaTextBox_TextChanged(object sender, EventArgs e)
        {



            ObjEntidadesClass.CepCod = CepEntregaTextBox.Text.Replace("-", "");
            ObjEntidadesClass.Busca_Endereco();


            if (ObjEntidadesClass.CepCod != "" && ObjEntidadesClass.CepCod != null
           && ObjEntidadesClass.CidCod != "" && ObjEntidadesClass.CidCod != null
           && ObjEntidadesClass.UFSIGLA != "" && ObjEntidadesClass.UFSIGLA != null
               )
            {
                EnderecoEntregaTextBox.Text = ObjEntidadesClass.CepEnderLoc.ToString();
                BairroEnderecoEntregaTextBox.Text = ObjEntidadesClass.CepBair1.ToString();
                CidadeEnderecoEntregaDropDownList.SelectedValue = ObjEntidadesClass.CidCod.ToString();
                UFEnderecoEntregaTextBox.Text = ObjEntidadesClass.UFSIGLA.ToString();




                if (ObjEntidadesClass.UFSIGLA != null && ObjEntidadesClass.UFSIGLA != "")
                {
                    UFEnderecoEntregaTextBox.Enabled = false;

                    //Carrega Combo de Cidade
                    Carrega_Combo_Cidade_Entrega(ObjEntidadesClass.UFSIGLA);


                    if (CidadeEnderecoEntregaDropDownList.SelectedValue != "")
                    {
                        NumeroEnderecoEntregaTextBox.Focus();

                    }


                }

                if (EnderecoEntregaTextBox.Text == "")
                {
                    EnderecoEntregaTextBox.Enabled = true;
                }

                if (BairroEnderecoEntregaTextBox.Text == "")
                {
                    BairroEnderecoEntregaTextBox.Enabled = true;
                }

            }
            else
            {
                CepEntregaTextBox.Focus();
                EnderecoEntregaTextBox.Text = "";
                EnderecoEntregaTextBox.Enabled = false;
                Response.Write("<script>alert(\"CEP não encontrado, ou cadastro incompleto.<br> Favor entrar em contato com a ADM Vendas para verificar o CEP\");</script>");


            }


        }

        public void Carrega_Combo_Cidade_Entrega(string UF)
        {

            //Combo Cidade
            CidadeEnderecoEntregaDropDownList.DataSource = mdlFuncoes.Consulta_Cidade(UF);
            CidadeEnderecoEntregaDropDownList.DataTextField = "CidNome";
            CidadeEnderecoEntregaDropDownList.DataValueField = "CidCod";
            CidadeEnderecoEntregaDropDownList.DataBind();
            CidadeEnderecoEntregaDropDownList.Items.Insert(0, new ListItem("Selecione", ""));

        }

        protected void UFEnderecoEntregaTextBox_TextChanged(object sender, EventArgs e)
        {

            Carrega_Combo_Cidade_Entrega(UFEnderecoEntregaTextBox.Text);
        }

        protected void NumeroEnderecoEntregaTextBox_TextChanged(object sender, EventArgs e)
        {

            if (NumeroEnderecoEntregaTextBox.Text.ToUpper() != "S/N")
            {

                try
                {
                    Convert.ToInt32(NumeroEnderecoEntregaTextBox.Text);
                    BairroEnderecoEntregaTextBox.Focus();
                }
                catch
                {
                    NumeroEnderecoEntregaTextBox.Focus();
                    Response.Write("<script>alert(\"Numero Invalido!\");</script>");


                }

            }
            else
            {
                BairroEnderecoEntregaTextBox.Focus();
            }

        }

        protected void EnderecoEntregaEoMesmosDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (EnderecoEntregaEoMesmosDropDownList.SelectedValue == "Sim")
            {
                EnderecoEntregaMultView.Visible = false;

            }
            else
            {
                EnderecoEntregaMultView.Visible = true;

            }

        }


        #endregion Endereco Entrega

        protected void Passo4Button_Click(object sender, EventArgs e)
        {
            string Retorno = "";

            //Descarrega Obj
            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }


            if (ObjEntidadesClass.TipoOperacao != "Consulta")
            {
                //Carrega Dados da Tela
                Retorno = CarregaDadosDaTela();
            }

            //Alimenta Session novamente
            Session["clsEntidades"] = ObjEntidadesClass;


            if (Retorno == "")
            {
                //Chama a proxima Tela
                Response.Redirect("FrmAbaInformacoes.aspx?indmnu=2");
            }
            else
            {
                Response.Write("<script>alert(\"" + Retorno + "\");</script>");
            }
        }


        public string CarregaDadosDaTela()
        {
            string Retorno = "";
            mdlFuncoes = new funcoes();

            #region Aba Endereco de Entrega


            if (EnderecoEntregaEoMesmosDropDownList.SelectedValue == "Não")
            {
                if (ObjEntidadesClass.EnderecoEntregaClass == null)
                {
                    ObjEntidadesClass.EnderecoEntregaClass = new GerencialVendas.EnderecoEntregaClass();
                }



                ObjEntidadesClass.EnderecoEntregaClass.EnderEntEntrega = "Sim";
                ObjEntidadesClass.EnderecoEntregaClass.EnderEntNome = RazaoSocialEntregaTextBox.Text.Trim();
                ObjEntidadesClass.EnderecoEntregaClass.EnderEnt = EnderecoEntregaTextBox.Text.Trim();

                ObjEntidadesClass.EnderecoEntregaClass.EnderEntComp = ComplementoEnderecoEntregaTextBox.Text.Trim();
                ObjEntidadesClass.EnderecoEntregaClass.EnderEntBair = BairroEnderecoEntregaTextBox.Text.Trim();
                ObjEntidadesClass.EnderecoEntregaClass.CidCod = CidadeEnderecoEntregaDropDownList.SelectedValue;
                ObjEntidadesClass.EnderecoEntregaClass.EnderEntCep = CepEntregaTextBox.Text.Replace("-", "");



                ObjEntidadesClass.EnderecoEntregaClass.EnderEntNo = NumeroEnderecoEntregaTextBox.Text.ToUpper().Trim();
                if (ObjEntidadesClass.EnderecoEntregaClass.EnderEntNo != "S/N")
                {

                    try
                    {
                        Convert.ToInt32(ObjEntidadesClass.EnderecoEntregaClass.EnderEntNo);

                        if (Convert.ToInt32(ObjEntidadesClass.EnderecoEntregaClass.EnderEntNo) % 2 == 0)
                            ObjEntidadesClass.EnderecoEntregaClass.EnderEntNoPI = "Par";
                        else
                            ObjEntidadesClass.EnderecoEntregaClass.EnderEntNoPI = "Ímpar";
                    }
                    catch
                    {

                        ObjEntidadesClass.EnderecoEntregaClass.EnderEntNoPI = "Par";

                    }

                }
                else
                {
                    ObjEntidadesClass.EnderecoEntregaClass.EnderEntNoPI = "Par";
                }





                if (EntragaCnpjTextBox.Text != "")
                {

                    ObjEntidadesClass.EnderecoEntregaClass.EnderEntCpfCgc = EntragaCnpjTextBox.Text.Trim().Replace("-", "");
                    ObjEntidadesClass.EnderecoEntregaClass.EnderEntCpfCgc = ObjEntidadesClass.EnderecoEntregaClass.EnderEntCpfCgc.Replace(".", "");
                    ObjEntidadesClass.EnderecoEntregaClass.EnderEntCpfCgc = ObjEntidadesClass.EnderecoEntregaClass.EnderEntCpfCgc.Replace("/", "");


                    Retorno = mdlFuncoes.Valida_CPF_CNPJ(ObjEntidadesClass.EnderecoEntregaClass.EnderEntCpfCgc, ObjEntidadesClass.EntCod);
                    if (Retorno == "Valido")
                    {
                        Retorno = "";
                    }
                    else
                    {
                        Retorno = "CPF/CNPJ Endereço de Entrega Invalido.";
                    }


                    if (ObjEntidadesClass.EnderecoEntregaClass.EnderEntCpfCgc.Length == 11)
                    {
                        ObjEntidadesClass.EnderecoEntregaClass.EnderEntTipoFJ = "Física";
                    }
                    else
                    {
                        ObjEntidadesClass.EnderecoEntregaClass.EnderEntTipoFJ = "Jurídica";
                    }
                }
                else
                {
                    ObjEntidadesClass.EnderecoEntregaClass.EnderEntCpfCgc = "";
                    ObjEntidadesClass.EnderecoEntregaClass.EnderEntTipoFJ = "Jurídica";
                }




                //Caso seja uma alteracao, verifica se o ID já existe
                if (EnderEntSeqLiteral.Text != "" && EnderEntSeqLiteral.Text != null)
                {
                    ObjEntidadesClass.EnderecoEntregaClass.EnderEntSeq = Convert.ToInt32(EnderEntSeqLiteral.Text);
                }
                else
                {
                    ObjEntidadesClass.EnderecoEntregaClass.EnderEntSeq = 0;
                }

                if (EnderEntFoneSeqLiteral.Text != "" && EnderEntFoneSeqLiteral.Text != null)
                {
                    ObjEntidadesClass.EnderecoEntregaClass.EnderEntFoneSeq = Convert.ToInt32(EnderEntFoneSeqLiteral.Text);
                }
                else
                {
                    ObjEntidadesClass.EnderecoEntregaClass.EnderEntFoneSeq = 0;
                }


                ObjEntidadesClass.EnderecoEntregaClass.EnderEntFoneTipo = "Comercial";
                ObjEntidadesClass.EnderecoEntregaClass.EnderEntFoneDDD = DDDTelefoneResponsavelEnderecoTextBox.Text.Trim();
                ObjEntidadesClass.EnderecoEntregaClass.EnderEntFoneNum = TelefoneResponsavelEnderecoTextBox.Text.Trim();
                ObjEntidadesClass.EnderecoEntregaClass.EnderEntFoneRamalBip = "";
                ObjEntidadesClass.EnderecoEntregaClass.EnderEntFoneRamalBipNum = RamalTelefoneResponsavelEnderecoTextBox.Text.Trim();



                ObjEntidadesClass.EnderecoEntregaClass.EnderEntEMail = EmailEntregaTextBox.Text.Trim();
                ObjEntidadesClass.EnderecoEntregaClass.EnderEntContato = ResponsavelEnderecoTextBox.Text.Trim();

                ObjEntidadesClass.EntLocEntregaOMesmo = "Não";
            }
            else
            {

                if (ObjEntidadesClass.EnderecoEntregaClass == null)
                {
                    ObjEntidadesClass.EnderecoEntregaClass = new GerencialVendas.EnderecoEntregaClass();
                }

                ObjEntidadesClass.EnderecoEntregaClass.EnderEntEntrega = "Não";
                ObjEntidadesClass.EntLocEntregaOMesmo = "Sim";
            }
            #endregion


            return Retorno;

        }

        public string CarregaDadosNaTela()
        {

            #region Aba Endereco de Entrega



            if ((ObjEntidadesClass.EntLocEntregaOMesmo == "Não"))
            {

                if (ObjEntidadesClass.EnderecoEntregaClass != null)
                {
                    if (ObjEntidadesClass.EnderecoEntregaClass.EnderEntSeq >= 0)
                    {
                        DescaregaDadosObjetoNaTela();
                    }
                }


            }
            else
            {
                //Caso ainda não tenha sido carregado nada
                if ((ObjEntidadesClass.EntLocEntregaOMesmo == "Sim"))
                {
                    EnderecoEntregaEoMesmosDropDownList.SelectedValue = "Sim";

                    if (ObjEntidadesClass.EnderecoEntregaClass != null)
                    {
                        if (ObjEntidadesClass.EnderecoEntregaClass.EnderEntSeq >= 0)
                        {
                            DescaregaDadosObjetoNaTela();
                        }
                    }
                    else
                    {
                        EnderecoEntregaEoMesmosDropDownList_SelectedIndexChanged(null, null);
                    }


                }
                else
                {
                    EnderecoEntregaEoMesmosDropDownList_SelectedIndexChanged(null, null);
                }
            }



            #endregion


            return "";

        }



        public void BloqueiaCampos()
        {
            EnderecoEntregaEoMesmosDropDownList.Enabled = false;
            EntragaCnpjTextBox.Enabled = false;
            RazaoSocialEntregaTextBox.Enabled = false;
            EmailEntregaTextBox.Enabled = false;
            CepEntregaTextBox.Enabled = false;
            EnderecoEntregaTextBox.Enabled = false;
            NumeroEnderecoEntregaTextBox.Enabled = false;
            BairroEnderecoEntregaTextBox.Enabled = false;
            UFEnderecoEntregaTextBox.Enabled = false;
            CidadeEnderecoEntregaDropDownList.Enabled = false;
            ComplementoEnderecoEntregaTextBox.Enabled = false;
            ResponsavelEnderecoTextBox.Enabled = false;
            DDDTelefoneResponsavelEnderecoTextBox.Enabled = false;
            TelefoneResponsavelEnderecoTextBox.Enabled = false;
            RamalTelefoneResponsavelEnderecoTextBox.Enabled = false;
            Passo4Button.Visible = false;

        }

        protected void AlterarButton_Click(object sender, EventArgs e)
        {
            string Retorno = "";
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            //Carrega os Dados da tela para alterar
            Retorno = CarregaDadosDaTela();

            //Alterando dados de Entega
            ObjEntidadesClass.EnderecoEntregaClass.EntCod = ObjEntidadesClass.EntCod;
            ObjEntidadesClass.EnderecoEntregaClass.UsuCod = Session["usuario"].ToString();

            if (Retorno == "")
            {
                if (ObjEntidadesClass.EnderecoEntregaClass.EnderEntSeq > 0)
                {
                    Retorno += ObjEntidadesClass.EnderecoEntregaClass.Aletar_Endereco_Entrega();
                }
                else
                {
                    if (ObjEntidadesClass.EnderecoEntregaClass.EnderEntEntrega == "Sim")
                    {
                        Retorno += ObjEntidadesClass.EnderecoEntregaClass.Incluir_Endereco_Entrega();
                    }
                    else
                    {
                        Retorno += ObjEntidadesClass.EnderecoEntregaClass.Aletar_Endereco_Entrega();
                    }
                }


                //Verifica se a alteração não esta sendo feita em uma entidade ja ativa, se estiver vai enviar para Cadastro Incompleto
                ObjEntidadesClass.Alterar_Status_Entidade_Cadastro_Incompleto();
            }

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

            Passo4Button.Visible = false;
            PrincipalButton.Visible = true;
            ContatoButton.Visible = true;
            InformacoesButton.Visible = true;
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

        public void DescaregaDadosObjetoNaTela()
        {
            CepEntregaTextBox.Text = (ObjEntidadesClass.EnderecoEntregaClass.EnderEntCep.Substring(0, 5) + "-" + ObjEntidadesClass.EnderecoEntregaClass.EnderEntCep.Substring(5, 3));
            CepEntregaTextBox_TextChanged(null, null);
            CidadeEnderecoEntregaDropDownList.SelectedValue = ObjEntidadesClass.EnderecoEntregaClass.CidCod;
            ComplementoEnderecoEntregaTextBox.Text = ObjEntidadesClass.EnderecoEntregaClass.EnderEntComp;
            BairroEnderecoEntregaTextBox.Text = ObjEntidadesClass.EnderecoEntregaClass.EnderEntBair;
            EnderecoEntregaTextBox.Text = ObjEntidadesClass.EnderecoEntregaClass.EnderEnt;
            NumeroEnderecoEntregaTextBox.Text = ObjEntidadesClass.EnderecoEntregaClass.EnderEntNo;


            EnderecoEntregaEoMesmosDropDownList.SelectedValue = ObjEntidadesClass.EntLocEntregaOMesmo;
            EnderecoEntregaEoMesmosDropDownList_SelectedIndexChanged(null, null);


            RazaoSocialEntregaTextBox.Text = ObjEntidadesClass.EnderecoEntregaClass.EnderEntNome;


            EntragaCnpjTextBox.Text = ObjEntidadesClass.EnderecoEntregaClass.EnderEntCpfCgc;



            DDDTelefoneResponsavelEnderecoTextBox.Text = ObjEntidadesClass.EnderecoEntregaClass.EnderEntFoneDDD;
            TelefoneResponsavelEnderecoTextBox.Text = ObjEntidadesClass.EnderecoEntregaClass.EnderEntFoneNum;
            RamalTelefoneResponsavelEnderecoTextBox.Text = ObjEntidadesClass.EnderecoEntregaClass.EnderEntFoneRamalBipNum;


            EmailEntregaTextBox.Text = ObjEntidadesClass.EnderecoEntregaClass.EnderEntEMail;
            ResponsavelEnderecoTextBox.Text = ObjEntidadesClass.EnderecoEntregaClass.EnderEntContato;

            //Informações uteis para update
            EnderEntSeqLiteral.Text = ObjEntidadesClass.EnderecoEntregaClass.EnderEntSeq.ToString();
            EnderEntFoneSeqLiteral.Text = ObjEntidadesClass.EnderecoEntregaClass.EnderEntFoneSeq.ToString();
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