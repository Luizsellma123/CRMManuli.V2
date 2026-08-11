using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;

namespace VendasWeb.Entidades
{
    public partial class FrmAbaPrincipal : System.Web.UI.Page
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
                    //Descarega a session da Entidade
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];


                    //Verifica qual Tipo de Operacao sera possivel Realizar nessa tela para o Usuario Logado
                    ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                    if (ObjEntidadesClass.TipoOperacao != "Inclusão")
                    {
                        ObjEntidadesClass.ConsultaTipoOperacao("FrmAbaPrincipal");
                    }



                    //Carrega dados na Tela
                    if (ObjEntidadesClass.TipoOperacao != "")//Incluir ou Alterar
                    {
                        CarregaDadosNaTela();



                        //Verifica a operação
                        switch (ObjEntidadesClass.TipoOperacao)
                        {


                            case "Cadastro Completo":
                                #region
                                LiberaNavegacao();
                                AlterarButton.Visible = true;
                                #endregion
                                break;

                            case "Cadastro Incompleto":
                                #region
                                LiberaNavegacao();
                                AlterarButton.Visible = true;
                                CadastroCompletoLinkButton.Visible = true;
                                #endregion
                                break;

                            case "ADM_VENDAS":
                                #region
                                LiberaNavegacao();
                                AlterarButton.Visible = true;
                                EnviarParaFiscalButton.Visible = true;
                                CadastroIncompletoLinkButton.Visible = true;
                                ReprovarCadastroAdmVendasButton.Visible = true;

                                //Verifica se já passou pelo Fiscal
                                if (ObjEntidadesClass.ConsultaPassagemPorStatus("11"))
                                {
                                    //Libera botão para Enviar para Analise Financeira
                                    EnviarParaFinanceiroButton.Visible = true;
                                }
                                #endregion
                                break;

                            case "ADM_FISCAL":
                                #region
                                BloqueiaCampos();
                                LiberaNavegacao();
                                EnviarParaLogisticaButton.Visible = true;
                                #endregion
                                break;

                            case "LOGISTICA":
                                #region
                                BloqueiaCampos();
                                LiberaNavegacao();
                                EnviarParaFinanceiroButton.Visible = true;
                                #endregion
                                break;

                            case "ADM_FINANCEIRO":
                                #region
                                BloqueiaCampos();
                                LiberaNavegacao();
                                AprovarCadastroFinanceiraButton.Visible = true;
                                ReprovarCadastroFinanceiroLinkButton.Visible = true;
                                RetornarAdmVendasLinkButton.Visible = true;
                                #endregion
                                break;


                            

                            case "Consulta":
                                #region
                                LiberaNavegacao();

                                //Se tiver em status "Cadastro Incompleto" libera campo para Finalizar Cadastro
                                if (ObjEntidadesClass.StatEntCod == "13")
                                {
                                    CadastroCompletoLinkButton.Visible = true;
                                    AlterarButton.Visible = true;
                                }
                                else
                                {
                                    BloqueiaCampos();
                                }
                                #endregion
                                break;




                        }





                    }



                }

                if (Session["UsuCodADM"] != null)
                {
                    VendedorLogadoLabel.Text = Session["UsuCodADM"].ToString().ToUpper();
                }
                else
                {
                    if (Session["usuario"] != null)
                    {
                        VendedorLogadoLabel.Text = Session["usuario"].ToString().ToUpper();
                    }

                }


                //Combo vendedor
                #region Combo Vendedor
                mdlFuncoes.Usucod = Session["usuario"].ToString();
                VendCodDropDownList.DataSource = mdlFuncoes.Consulta_Vendedor(Session["usuario"].ToString());
                VendCodDropDownList.DataTextField = "VendNome";
                VendCodDropDownList.DataValueField = "VendCod";
                VendCodDropDownList.DataBind();
                VendCodDropDownList.Items.Insert(0, new ListItem("Selecione", ""));
                VendCodDropDownList.Focus();


                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                string vendCod = ObjEntidadesClass.Lista_Vendedor_Logado();

                if (vendCod != "")
                    VendCodDropDownList.Enabled = false;

                VendCodDropDownList.SelectedValue = vendCod;

                #endregion



            }

        }





        public string CarregaDadosDaTela()
        {
            string Retorno = "";

            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
            ObjEntidadesClass.EntNome = razaoSocialTextBox.Text.ToUpper().Trim();
            ObjEntidadesClass.EntNomeFant = NomeFantasiaTextBox.Text.ToUpper().Trim();
            ObjEntidadesClass.EntCep = CepTextBox.Text.Replace("-", "");
            ObjEntidadesClass.EntLograd = "";
            ObjEntidadesClass.EntEnder = EnderecoTextBox.Text.ToUpper().Trim();

            ObjEntidadesClass.EntBair = BairroTextBox.Text.ToUpper().Trim();
            ObjEntidadesClass.CidCod = CidadeDropDownList.SelectedValue;
            ObjEntidadesClass.UFSIGLA = UFTextBox.Text.ToUpper();
            ObjEntidadesClass.EntEnderComp = ComplementoTextBox.Text.ToUpper().Trim();
            ObjEntidadesClass.EntInscSuframa = SuframaTextBox.Text.ToUpper().Trim();


            //Regime Especial
            ObjEntidadesClass.RegEspecNum = ConcessaoDropDownList.SelectedValue;


            //Vendedor
            ObjEntidadesClass.VendCod = VendCodDropDownList.SelectedValue;

            //Inscricao Estadual
            ObjEntidadesClass.EntRgIe = InscricaoEstadualTextBox.Text.Trim();


            //Numero Endereco
            ObjEntidadesClass.EntEnderNo = NumeroTextBox.Text.ToString().ToUpper().Trim();
            if (ObjEntidadesClass.EntEnderNo != "S/N")
            {

                try
                {
                    Convert.ToInt32(NumeroTextBox.Text);

                    if (Convert.ToInt32(ObjEntidadesClass.EntEnderNo) % 2 == 0)
                        ObjEntidadesClass.EntEnderNoPI = "Par";
                    else
                        ObjEntidadesClass.EntEnderNoPI = "Ímpar";
                }
                catch
                {

                    ObjEntidadesClass.EntEnderNoPI = "Par";

                }

            }
            else
            {
                ObjEntidadesClass.EntEnderNoPI = "Par";
            }



            //CPF CNPJ
            ObjEntidadesClass.EntCpfCgc = Cnpj_CpfTextBox.Text.Trim().Replace("-", "");
            ObjEntidadesClass.EntCpfCgc = ObjEntidadesClass.EntCpfCgc.Replace(".", "");
            ObjEntidadesClass.EntCpfCgc = ObjEntidadesClass.EntCpfCgc.Replace("/", "");

            Retorno = mdlFuncoes.Valida_CPF_CNPJ(ObjEntidadesClass.EntCpfCgc, ObjEntidadesClass.EntCod);
            if (Retorno == "Valido")
            {
                Retorno = "";


                if (ObjEntidadesClass.EntCpfCgc.Length == 11)
                {
                    ObjEntidadesClass.EntTipoFJ = "Física";
                }
                else
                {
                    ObjEntidadesClass.EntTipoFJ = "Jurídica";
                }


            }
            else
            {
                Retorno = "CPF/CNPJ Invalido";
            }


            #region Carregando Dados do Email XML
            ObjEntidadesClass.ListEntWeb = new List<GerencialVendas.clsEntWeb>();
            ObjclsEntWeb = new GerencialVendas.clsEntWeb();

            if (EntWebSeqEmailXmlLiteral.Text != "" && EntWebSeqEmailXmlLiteral.Text != null)
            {
                ObjclsEntWeb.EntWebSeq = Convert.ToInt32(EntWebSeqEmailXmlLiteral.Text);
            }
            else
            {
                ObjclsEntWeb.EntWebSeq = 0;
            }

            ObjclsEntWeb.EntWebTipo = "Financeiro";
            ObjclsEntWeb.EntWebWWW = null;
            ObjclsEntWeb.EntWebEMail = EmailXmlTextBox.Text.ToLower().Trim();
            ObjclsEntWeb.EntWebEMailPrinc = "Sim";
            ObjclsEntWeb.EntWebEMailPedComp = "Não";
            ObjclsEntWeb.EntWebRecebeEmailOcor = "Não";
            ObjclsEntWeb.EntWebDisparaEmailAgenda = "Não";
            ObjclsEntWeb.EntWebEmailNFe = "Sim";
            ObjclsEntWeb.EntWebEmailNFSe = "Sim";

            ObjEntidadesClass.AdicionarEmail(ObjclsEntWeb);
            #endregion


            #region Carregando Dados  Responsavel

            ObjContatoClass = new GerencialVendas.ContatoClass();
            ObjContatoClass.Nome = NomeResponsavelTextBox.Text.ToString().ToUpper().Trim();
            ObjContatoClass.Email = EmailTextBox.Text.ToString().ToUpper().Trim();
            ObjContatoClass.DDDTelefone = DDDTelefoneResponsavelTextBox.Text.ToString().Trim();
            ObjContatoClass.Telefone = TelefoneResponsavelTextBox.Text.ToString().Trim();
            ObjContatoClass.Ramal = RamalTelefoneResponsavelTextBox.Text.Trim();
            ObjContatoClass.DDDCelular = DDDCelularTextBox.Text.ToString().Trim();
            ObjContatoClass.Celular = TelCelularTextBox.Text.Trim();
            ObjContatoClass.TipoContato = "Responsavel";
            ObjContatoClass.Cargo = CargoTextBox.Text.ToUpper().Trim();

            //Caso seja uma alteracao, verifica se o ID do responsavel já existe
            if (ENTCONTATOIDLiteral.Text != "" && ENTCONTATOIDLiteral.Text != null)
            {
                ObjContatoClass.ENTCONTATOID = Convert.ToInt32(ENTCONTATOIDLiteral.Text);
            }
            else
            {
                ObjContatoClass.ENTCONTATOID = 0;
            }

            ObjEntidadesClass.AdicionarContato(ObjContatoClass);



            #endregion


            //ObjEntidadesClass.NovaLoja = NovaLojaDropDownList.SelectedValue;

            return Retorno;

        }


        public string CarregaDadosNaTela()
        {

            Cnpj_CpfTextBox.Text = ObjEntidadesClass.EntCpfCgc;
            razaoSocialTextBox.Text = ObjEntidadesClass.EntNome;
            NomeFantasiaTextBox.Text = ObjEntidadesClass.EntNomeFant;

            if (ObjEntidadesClass.EntCep != null && ObjEntidadesClass.EntCep != "")
            {
                CepTextBox.Text = (ObjEntidadesClass.EntCep.ToString().Substring(0, 5) + "-" + ObjEntidadesClass.EntCep.ToString().Substring(5, 3));
            }
            EnderecoTextBox.Text = ObjEntidadesClass.EntEnder;
            NumeroTextBox.Text = ObjEntidadesClass.EntEnderNo ?? "";
            BairroTextBox.Text = ObjEntidadesClass.EntBair;

            //Busca a Cidade de Acordo com o CEP
            ObjEntidadesClass.Busca_Endereco();
            Carrega_Combo_Cidade(ObjEntidadesClass.UFSIGLA);
            CidadeDropDownList.SelectedValue = ObjEntidadesClass.CidCod;
            UFTextBox.Text = ObjEntidadesClass.UFSIGLA;
            ComplementoTextBox.Text = ObjEntidadesClass.EntEnderComp;

            SuframaTextBox.Text = ObjEntidadesClass.EntInscSuframa;


            //Regime Especial
            ConcessaoDropDownList.SelectedValue = ObjEntidadesClass.RegEspecNum;


            //Vendedor
            //VendCodDropDownList.SelectedValue = ObjEntidadesClass.VendCod;
            VendCodDropDownList.Visible = false;
            VendCodLabel.Visible = false;

            //Inscricao Estadual
            InscricaoEstadualTextBox.Text = ObjEntidadesClass.EntRgIe;

            //Email Entidade
            if (ObjEntidadesClass.ListEntWeb != null)
            {

                if (ObjEntidadesClass.ListEntWeb.Count > 0)
                {
                    for (int i = 0; i < ObjEntidadesClass.ListEntWeb.Count; i++)
                    {
                        if (ObjEntidadesClass.ListEntWeb[i].EntWebTipo == "Financeiro")
                        {
                            EmailXmlTextBox.Text = ObjEntidadesClass.ListEntWeb[i].EntWebEMail;
                            EntWebSeqEmailXmlLiteral.Text = ObjEntidadesClass.ListEntWeb[i].EntWebSeq.ToString();
                        }
                    }
                }
            }




            #region Carregando Dados  Responsavel

            if (ObjEntidadesClass.ListContatoClass != null)
            {
                if (ObjEntidadesClass.ListContatoClass.Count > 0)
                {
                    for (int i = 0; i < ObjEntidadesClass.ListContatoClass.Count; i++)
                    {
                        if (ObjEntidadesClass.ListContatoClass[i].TipoContato == "Responsavel")
                        {
                            NomeResponsavelTextBox.Text = ObjEntidadesClass.ListContatoClass[i].Nome;
                            EmailTextBox.Text = ObjEntidadesClass.ListContatoClass[i].Email;
                            DDDTelefoneResponsavelTextBox.Text = ObjEntidadesClass.ListContatoClass[i].DDDTelefone;
                            TelefoneResponsavelTextBox.Text = ObjEntidadesClass.ListContatoClass[i].Telefone;
                            RamalTelefoneResponsavelTextBox.Text = ObjEntidadesClass.ListContatoClass[i].Ramal;
                            DDDCelularTextBox.Text = ObjEntidadesClass.ListContatoClass[i].DDDCelular;
                            TelCelularTextBox.Text = ObjEntidadesClass.ListContatoClass[i].Celular;
                            CargoTextBox.Text = ObjEntidadesClass.ListContatoClass[i].Cargo;
                            ENTCONTATOIDLiteral.Text = ObjEntidadesClass.ListContatoClass[i].ENTCONTATOID.ToString();
                        }
                    }
                }
            }



            #endregion


            //Data Cadastro
            if (ObjEntidadesClass.EntDataCad != null && ObjEntidadesClass.EntDataCad.ToString() != "")
            {
                DataCadastroLabel.Text = "Data de Cadastro: " + ObjEntidadesClass.EntDataCad.ToString("dd/MM/yyyy");
            }

            //Status
            StatusLabel.Text = ObjEntidadesClass.EntStatDescr;

            //NovaLojaDropDownList.SelectedValue = ObjEntidadesClass.NovaLoja;

            return "";

        }


        protected void Cnpj_CpfTextBox_TextChanged(object sender, EventArgs e)
        {

            string Retorno = "";
            ObjEntidadesClass = new GerencialVendas.clsEntidades();
            ObjEntidadesClass.EntCpfCgc = Cnpj_CpfTextBox.Text.Trim().Replace("-", "");
            ObjEntidadesClass.EntCpfCgc = ObjEntidadesClass.EntCpfCgc.Replace(".", "");
            ObjEntidadesClass.EntCpfCgc = ObjEntidadesClass.EntCpfCgc.Replace("/", "");

            Retorno = mdlFuncoes.Valida_CPF_CNPJ(ObjEntidadesClass.EntCpfCgc,"");

            if (Retorno != "Valido")
            {

                Response.Write("<script>alert(\"CPF/CNPJ" + " " + ObjEntidadesClass.EntCpfCgc + "<br> " + Retorno + "\");</script>");

                Cnpj_CpfTextBox.Text = "";
                Cnpj_CpfTextBox.Focus();

            }
            else
            {
                razaoSocialTextBox.Focus();
            }


        }


        #region Endereco

        protected void CepTextBox_TextChanged(object sender, EventArgs e)
        {

            ObjEntidadesClass.CepCod = CepTextBox.Text.Replace("-", "");
            ObjEntidadesClass.Busca_Endereco();

            if (ObjEntidadesClass.CepCod != "" && ObjEntidadesClass.CepCod != null
            && ObjEntidadesClass.CidCod != "" && ObjEntidadesClass.CidCod != null
            && ObjEntidadesClass.UFSIGLA != "" && ObjEntidadesClass.UFSIGLA != null
                )
            {
                EnderecoTextBox.Text = ObjEntidadesClass.CepEnderLoc.ToString();
                BairroTextBox.Text = ObjEntidadesClass.CepBair1.ToString();
                CidadeDropDownList.SelectedValue = ObjEntidadesClass.CidCod.ToString();
                UFTextBox.Text = ObjEntidadesClass.UFSIGLA.ToString();


                if (ObjEntidadesClass.UFSIGLA != null && ObjEntidadesClass.UFSIGLA != "")
                {

                    //Carrega Combo de Cidade
                    Carrega_Combo_Cidade(ObjEntidadesClass.UFSIGLA);


                    if (CidadeDropDownList.SelectedValue != "")
                    {

                        CidadeDropDownList_SelectedIndexChanged(null, null);
                        NumeroTextBox.Focus();

                    }


                }

                if (EnderecoTextBox.Text == "")
                {
                    EnderecoTextBox.Enabled = true;
                }

                if (BairroTextBox.Text == "")
                {
                    BairroTextBox.Enabled = true;
                }

            }
            else
            {
                CepTextBox.Focus();
                EnderecoTextBox.Text = "";
                BairroTextBox.Text = "";
                UFTextBox.Text = "";

                Response.Write("<script>alert(\"CEP não encontrado, ou cadastro incompleto.<br> Favor entrar em contato com a ADM Vendas para verificar o CEP\");</script>");

            }


        }

        public void Carrega_Combo_Cidade(string UF)
        {

            //Combo Cidade
            CidadeDropDownList.DataSource = mdlFuncoes.Consulta_Cidade(UF);
            CidadeDropDownList.DataTextField = "CidNome";
            CidadeDropDownList.DataValueField = "CidCod";
            CidadeDropDownList.DataBind();
            CidadeDropDownList.Items.Insert(0, new ListItem("Selecione", ""));

        }


        protected void CidadeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            ObjEntidadesClass.CidCod = CidadeDropDownList.SelectedValue;

            if (ObjEntidadesClass.Consulta_Cidade() == "Sim")
            {
                SuframaTextBox.Visible = true;
                SuframaLabel.Visible = true;
            }
            else
            {
                SuframaTextBox.Visible = false;
                SuframaLabel.Visible = false;
            }

        }

        protected void UFTextBox_TextChanged(object sender, EventArgs e)
        {
            //Carrega Combo de Cidade
            Carrega_Combo_Cidade(UFTextBox.Text);

        }


        protected void NumeroTextBox_TextChanged(object sender, EventArgs e)
        {

            if (NumeroTextBox.Text.ToUpper() != "S/N")
            {

                try
                {
                    Convert.ToInt32(NumeroTextBox.Text);
                    BairroTextBox.Focus();
                }
                catch
                {
                    NumeroTextBox.Focus();
                    Response.Write("<script>alert(\"Numero Invalido!\");</script>");

                }

            }
            else
            {
                BairroTextBox.Focus();
            }
        }

        #endregion

        protected void InscricaoEstadualTextBox_TextChanged(object sender, EventArgs e)
        {
            string UF = UFTextBox.Text.ToString().ToUpper();
            string InscricaoEstatual = InscricaoEstadualTextBox.Text.ToString().ToUpper();

            if (ObjEntidadesClass.ValidarInscricaoEstadual(UF, InscricaoEstatual) == false)
            {

                //Retorna Mensagem de Erro
                InscricaoEstadualTextBox.Focus();
                Response.Write("<script>alert(\"Inscrição Invalida!\");</script>");

            }
            else
            {
                NomeResponsavelTextBox.Focus();
            }
        }


        public void BloqueiaCampos()
        {
            VendCodDropDownList.Enabled = false;
            Cnpj_CpfTextBox.Enabled = false;
            //NovaLojaDropDownList.Enabled = false;
            razaoSocialTextBox.Enabled = false;
            NomeFantasiaTextBox.Enabled = false;
            EmailXmlTextBox.Enabled = false;
            CepTextBox.Enabled = false;
            EnderecoTextBox.Enabled = false;
            NumeroTextBox.Enabled = false;
            BairroTextBox.Enabled = false;
            UFTextBox.Enabled = false;
            CidadeDropDownList.Enabled = false;
            ComplementoTextBox.Enabled = false;
            InscricaoEstadualTextBox.Enabled = false;
            SuframaTextBox.Enabled = false;
            ConcessaoDropDownList.Enabled = false;
            NomeResponsavelTextBox.Enabled = false;
            EmailTextBox.Enabled = false;
            DDDTelefoneResponsavelTextBox.Enabled = false;
            TelefoneResponsavelTextBox.Enabled = false;
            RamalTelefoneResponsavelTextBox.Enabled = false;
            DDDCelularTextBox.Enabled = false;
            TelCelularTextBox.Enabled = false;
            CargoTextBox.Enabled = false;
            ProximoButton.Visible = false;

        }

        public void LiberaNavegacao()
        {
            ProximoButton.Visible = false;
            ContatoButton.Visible = true;
            //EnderecoEntregaButton.Visible = true;
            InformacoesButton.Visible = true;
            AnexosButton.Visible = true;
            //ObservacoesButton.Visible = true;
            FiscalLinkButton.Visible = true;
            HoldingLinkButton.Visible = true;
            LogisticaLinkButton.Visible = true;
            //VendedorLinkButton.Visible = true;
            DuplicataLinkButton.Visible = true;
            CRMLinkButton.Visible = true;
            PedidosLinkButton.Visible = true;
            //AgendaLinkButton.Visible = true;
            NotasLinkButton.Visible = true;


            //Verifica se o Usuario possui algum Vendedor //Funcao temporaria para OCultar campos
            if (ObjUsuarioClass.ConsultaVendedorUsuario(Session["usuario"].ToString()) != 0)
            {
                HoldingLinkButton.Visible = false;
                LogisticaLinkButton.Visible = false;
            }
        }



        protected void AlterarButton_Click(object sender, EventArgs e)
        {
            string Retorno = "";
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

            //Carrega os Dados da tela para alterar
            Retorno=CarregaDadosDaTela();
            //Pega o usuario que esta alterando
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
            //Altera a Entidade

            if (Retorno == "")
            {
                Retorno = ObjEntidadesClass.Altera_Entidade();

                #region Altera Responsavel
                //Percorre os contato para pegar o Responsavel que sera alterado nessa tela
                #region
                /*if (ObjEntidadesClass.ListContatoClass.Count > 0)
                {

                    for (int t = 0; t < ObjEntidadesClass.ListContatoClass.Count; t++)
                    {
                        ObjEntidadesClass.ListContatoClass[t].EntCod = ObjEntidadesClass.EntCod;
                        ObjEntidadesClass.ListContatoClass[t].UsuCod = Session["usuario"].ToString();

                        if (ObjContatoClass.ENTCONTATOID > 0)
                        {
                            Retorno += ObjEntidadesClass.ListContatoClass[t].Alterar_Contato();
                        }
                        else
                        {
                            Retorno += ObjEntidadesClass.ListContatoClass[t].Incluir_Contato();
                        }

                    }
                }*/

                if (EmailTextBox.Text != "")
                {

                    ObjContatoClass = new GerencialVendas.ContatoClass();
                    ObjContatoClass.EntCod = ObjEntidadesClass.EntCod;
                    ObjContatoClass.Nome = NomeResponsavelTextBox.Text.ToString().ToUpper().Trim();
                    ObjContatoClass.Email = EmailTextBox.Text.ToString().ToUpper().Trim();
                    ObjContatoClass.DDDTelefone = DDDTelefoneResponsavelTextBox.Text.ToString().Trim();
                    ObjContatoClass.Telefone = TelefoneResponsavelTextBox.Text.ToString().Trim();
                    ObjContatoClass.Ramal = RamalTelefoneResponsavelTextBox.Text.Trim();
                    ObjContatoClass.DDDCelular = DDDCelularTextBox.Text.ToString().Trim();
                    ObjContatoClass.Celular = TelCelularTextBox.Text.Trim();
                    ObjContatoClass.TipoContato = "Responsavel";
                    ObjContatoClass.Cargo = CargoTextBox.Text.ToUpper().Trim();
                    ObjContatoClass.UsuCod = Session["usuario"].ToString();
                    ObjContatoClass.Empresa = "";

                    //Caso seja uma alteracao, verifica se o ID do responsavel já existe
                    if (ENTCONTATOIDLiteral.Text != "" && ENTCONTATOIDLiteral.Text != null)
                    {
                        ObjContatoClass.ENTCONTATOID = Convert.ToInt32(ENTCONTATOIDLiteral.Text);
                        ObjContatoClass.Alterar_Contato();
                    }
                    else
                    {
                        ObjContatoClass.ENTCONTATOID = 0;
                        ObjContatoClass.Incluir_Contato();
                    }

                }

                #endregion
                #endregion


                #region Altera Email XML
                //Muda Cadastro email XML
                #region
                /*if (ObjEntidadesClass.ListEntWeb != null)
                {

                    if (ObjEntidadesClass.ListEntWeb.Count > 0)
                    {
                        for (int i = 0; i < ObjEntidadesClass.ListEntWeb.Count; i++)
                        {

                            ObjEntidadesClass.ListEntWeb[i].EntCod = ObjEntidadesClass.EntCod;

                            if (ObjEntidadesClass.ListEntWeb[i].EntWebTipo == "Financeiro")
                            {
                                if (ObjEntidadesClass.ListEntWeb[i].EntWebSeq == 0)
                                {
                                    Retorno += ObjEntidadesClass.ListEntWeb[i].Incluir_Email();
                                }
                                else
                                {
                                    Retorno += ObjEntidadesClass.ListEntWeb[i].Altera_Email();
                                }

                            }
                        }
                    }
                }*/


                if (EmailXmlTextBox.Text != "")
                {
                    ObjclsEntWeb = new GerencialVendas.clsEntWeb();
                    ObjclsEntWeb.EntCod = ObjEntidadesClass.EntCod;
                    ObjclsEntWeb.EntWebTipo = "Financeiro";
                    ObjclsEntWeb.EntWebWWW = null;
                    ObjclsEntWeb.EntWebEMail = EmailXmlTextBox.Text.ToLower().Trim();
                    ObjclsEntWeb.EntWebEMailPrinc = "Sim";
                    ObjclsEntWeb.EntWebEMailPedComp = "Não";
                    ObjclsEntWeb.EntWebRecebeEmailOcor = "Não";
                    ObjclsEntWeb.EntWebDisparaEmailAgenda = "Não";
                    ObjclsEntWeb.EntWebEmailNFe = "Sim";
                    ObjclsEntWeb.EntWebEmailNFSe = "Sim";

                    if (EntWebSeqEmailXmlLiteral.Text != "" && EntWebSeqEmailXmlLiteral.Text != null)
                    {
                        ObjclsEntWeb.EntWebSeq = Convert.ToInt32(EntWebSeqEmailXmlLiteral.Text);
                        ObjclsEntWeb.Altera_Email();
                    }
                    else
                    {
                        ObjclsEntWeb.EntWebSeq = 0;
                        ObjclsEntWeb.Incluir_Email();
                    }

                }

                #endregion
                #endregion


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

        protected void Passo1Button_Click(object sender, EventArgs e)
        {

            string Retorno = "";

            //Verifica se a session esta Carregada
            if (Session["clsEntidades"] != null)
            {

                //Descarega a session da Entidade
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            }
            else
            {
                //Caso nao esteja, cria um session Nova
                ObjEntidadesClass = new GerencialVendas.clsEntidades();

                //Gera um Codigo para a Entidade
                ObjEntidadesClass.Gera_Codigo_Entidade();
                //Define o operacao como Inclusao
                ObjEntidadesClass.TipoOperacao = "Inclusão";

                ObjEntidadesClass.StatEntCod = "13";
                ObjEntidadesClass.EntStatDescr = "Cadastro Incompleto";

            }


            if (ObjEntidadesClass.TipoOperacao != "Consulta")//Se for consulta nao pode mudar os dados
            {
                //Carrega os Dados da Tela
                Retorno = CarregaDadosDaTela();
            }

            //Guarda os dados em Session
            Session["clsEntidades"] = ObjEntidadesClass;

            if (Retorno == "")
            {
                if (ObjEntidadesClass.EntNome != "")
                {
                    if (ObjEntidadesClass.EntCpfCgc != "")
                    {
                        if (ObjEntidadesClass.VendCod != "")
                        {
                            //Chama a proxima Tela
                            Response.Redirect("FrmAbaContatos.aspx?indmnu=2");
                        }
                        else
                        {
                            Response.Write("<script>alert(\"Selecione um Vendedor!\");</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert(\"Informe o CFP ou CNPJ da Entidade!\");</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert(\"Informe o nome da Entidade!\");</script>");
                }
            }
            else
            {
                Response.Write("<script>alert(\"" + Retorno + "\");</script>");
            }
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

        protected void EnviarParaFicalButton_Click(object sender, EventArgs e)
        {

            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

            //Envia para o Fiscal
            if (ObjEntidadesClass.Alterar_Status_Entidade("11") == "")
            {
                Session["Msg"] = "Entidade enviada para Analise Fiscal";

                //Texto Email
                ObjEntidadesClass.OperacaoEmail = "";//Branco pega o Resposavel no Status que a entidade estiver
                ObjEntidadesClass.Remetente = "Cadastro de Entidades - Web Manuli";
                ObjEntidadesClass.DescricaoEmail = "Alteração WorkFlow - Entidade Codigo: " + ObjEntidadesClass.EntCod;
                ObjEntidadesClass.Texto = " Cadastro encaminhado por : " + ObjEntidadesClass.UsuCod + "<BR>";
                ObjEntidadesClass.Texto += " Codigo Entidade : " + ObjEntidadesClass.EntCod + "<BR>";
                ObjEntidadesClass.Texto += " Nome : " + ObjEntidadesClass.EntNome + "<BR>";
                ObjEntidadesClass.Texto += " CNPJ/CPF : " + ObjEntidadesClass.EntCpfCgc + "<BR>";
                ObjEntidadesClass.Texto += " Data do Encaminhamento : " + DateTime.Now.ToString("dd/MM/yyyy") + "<BR>";

                ObjEntidadesClass.Envia_Email_Entidade();


                if (Session["Retornar"] != null)
                {
                    Response.Redirect(Session["Retornar"].ToString());
                }
                else
                {
                    Session["Retornar"] = "FrmCarteira.aspx?indmnu=2";
                    Response.Redirect("frmHistoricoCRM.aspx?indmnu=2");
                }

            }
        }

        protected void EnviarParaLogisticaButton_Click(object sender, EventArgs e)
        {
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

            //Envia para o Financeiro
            if (ObjEntidadesClass.Alterar_Status_Entidade("14") == "")
            {
                Session["Msg"] = "Entidade enviada para Logistica";

                //Texto Email
                ObjEntidadesClass.OperacaoEmail = "";
                ObjEntidadesClass.Remetente = "Cadastro de Entidades - Web Manuli";
                ObjEntidadesClass.DescricaoEmail = "Alteração WorkFlow - Entidade Codigo: " + ObjEntidadesClass.EntCod;
                ObjEntidadesClass.Texto = " Cadastro encaminhado por : " + ObjEntidadesClass.UsuCod + "<BR>";
                ObjEntidadesClass.Texto += " Codigo Entidade : " + ObjEntidadesClass.EntCod + "<BR>";
                ObjEntidadesClass.Texto += " Nome : " + ObjEntidadesClass.EntNome + "<BR>";
                ObjEntidadesClass.Texto += " CNPJ/CPF : " + ObjEntidadesClass.EntCpfCgc + "<BR>";
                ObjEntidadesClass.Texto += " Data do Encaminhamento : " + DateTime.Now.ToString("dd/MM/yyyy") + "<BR>";

                ObjEntidadesClass.Envia_Email_Entidade();

                if (Session["Retornar"] != null)
                {
                    Response.Redirect(Session["Retornar"].ToString());
                }
                else
                {
                    Session["Retornar"] = "FrmCarteira.aspx?indmnu=2";
                    Response.Redirect("frmHistoricoCRM.aspx?indmnu=2");
                }
            }
        }

        protected void EnviarParaFinanceiroButton_Click(object sender, EventArgs e)
        {
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

            //Envia para o Financeiro
            if (ObjEntidadesClass.Alterar_Status_Entidade("12") == "")
            {
                Session["Msg"] = "Entidade enviada para Analise Financeira";

                //Texto Email
                ObjEntidadesClass.OperacaoEmail = "";
                ObjEntidadesClass.Remetente = "Cadastro de Entidades - Web Manuli";
                ObjEntidadesClass.DescricaoEmail = "Alteração WorkFlow - Entidade Codigo: " + ObjEntidadesClass.EntCod;
                ObjEntidadesClass.Texto = " Cadastro encaminhado por : " + ObjEntidadesClass.UsuCod + "<BR>";
                ObjEntidadesClass.Texto += " Codigo Entidade : " + ObjEntidadesClass.EntCod + "<BR>";
                ObjEntidadesClass.Texto += " Nome : " + ObjEntidadesClass.EntNome + "<BR>";
                ObjEntidadesClass.Texto += " CNPJ/CPF : " + ObjEntidadesClass.EntCpfCgc + "<BR>";
                ObjEntidadesClass.Texto += " Data do Encaminhamento : " + DateTime.Now.ToString("dd/MM/yyyy") + "<BR>";

                ObjEntidadesClass.Envia_Email_Entidade();

                if (Session["Retornar"] != null)
                {
                    Response.Redirect(Session["Retornar"].ToString());
                }
                else
                {
                    Session["Retornar"] = "FrmCarteira.aspx?indmnu=2";
                    Response.Redirect("frmHistoricoCRM.aspx?indmnu=2");
                }
            }
        }

        protected void AprovarCadastroButton_Click(object sender, EventArgs e)
        {
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

            //Aprova Entidade
            if (ObjEntidadesClass.Alterar_Status_Entidade("01") == "")
            {
                Session["Msg"] = "Entidade Aprovado";

                //Texto Email
                ObjEntidadesClass.OperacaoEmail = "";
                ObjEntidadesClass.Remetente = "Cadastro de Entidades - Web Manuli";
                ObjEntidadesClass.DescricaoEmail = "Cadastro Aprovado - Entidade Codigo: " + ObjEntidadesClass.EntCod;
                ObjEntidadesClass.Texto = " Cadastro Aprovado por : " + ObjEntidadesClass.UsuCod + "<BR>";
                ObjEntidadesClass.Texto += " Codigo Entidade : " + ObjEntidadesClass.EntCod + "<BR>";
                ObjEntidadesClass.Texto += " Nome : " + ObjEntidadesClass.EntNome + "<BR>";
                ObjEntidadesClass.Texto += " CNPJ/CPF : " + ObjEntidadesClass.EntCpfCgc + "<BR>";
                ObjEntidadesClass.Texto += " Data : " + DateTime.Now.ToString("dd/MM/yyyy") + "<BR>";

                ObjEntidadesClass.Envia_Email_Entidade();

                if (Session["Retornar"] != null)
                {
                    Response.Redirect(Session["Retornar"].ToString());
                }
                else
                {
                    Session["Retornar"] = "FrmCarteira.aspx?indmnu=2";
                    Response.Redirect("frmHistoricoCRM.aspx?indmnu=2");
                }



            }

        }

        protected void InativaCadastroAdmVendasButton_Click(object sender, EventArgs e)
        {
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

            //Envia para o inativo Adm Vendas
            if (ObjEntidadesClass.Alterar_Status_Entidade("04") == "")
            {
                Session["Msg"] = "Entidade Inativado";


                //Texto Email
                ObjEntidadesClass.OperacaoEmail = "RECUSADO";
                ObjEntidadesClass.Remetente = "Cadastro de Entidades - Web Manuli";
                ObjEntidadesClass.DescricaoEmail = "Cadastro Recusado - Entidade Codigo: " + ObjEntidadesClass.EntCod;
                ObjEntidadesClass.Texto = " Cadastro Recusado por : " + ObjEntidadesClass.UsuCod + "<BR>";
                ObjEntidadesClass.Texto += " Codigo Entidade : " + ObjEntidadesClass.EntCod + "<BR>";
                ObjEntidadesClass.Texto += " Nome : " + ObjEntidadesClass.EntNome + "<BR>";
                ObjEntidadesClass.Texto += " CNPJ/CPF : " + ObjEntidadesClass.EntCpfCgc + "<BR>";
                ObjEntidadesClass.Texto += " Data : " + DateTime.Now.ToString("dd/MM/yyyy") + "<BR>";

                ObjEntidadesClass.Envia_Email_Entidade();


                if (Session["Retornar"] != null)
                {
                    Response.Redirect(Session["Retornar"].ToString());
                }
                else
                {
                    Session["Retornar"] = "FrmCarteira.aspx?indmnu=2";
                    Response.Redirect("frmHistoricoCRM.aspx?indmnu=2");
                }
            }

        }

        protected void InativaCadastroFinanceiroButton_Click(object sender, EventArgs e)
        {
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

            //Envia para o inativo Financeiro
            if (ObjEntidadesClass.Alterar_Status_Entidade("02") == "")
            {
                Session["Msg"] = "Entidade Bloqueada";


                //Texto Email
                ObjEntidadesClass.OperacaoEmail = "RECUSADO";
                ObjEntidadesClass.Remetente = "Cadastro de Entidades Recusado pelo Financeiro - Web Manuli";
                ObjEntidadesClass.DescricaoEmail = "Recusado pelo Financeiro - Entidade Codigo: " + ObjEntidadesClass.EntCod;
                ObjEntidadesClass.Texto = " Cadastro encaminhado por : " + ObjEntidadesClass.UsuCod + "<BR>";
                ObjEntidadesClass.Texto += " Codigo Entidade : " + ObjEntidadesClass.EntCod + "<BR>";
                ObjEntidadesClass.Texto += " Nome : " + ObjEntidadesClass.EntNome + "<BR>";
                ObjEntidadesClass.Texto += " CNPJ/CPF : " + ObjEntidadesClass.EntCpfCgc + "<BR>";
                ObjEntidadesClass.Texto += " Data do Encaminhamento : " + DateTime.Now.ToString("dd/MM/yyyy") + "<BR>";

                ObjEntidadesClass.Envia_Email_Entidade();

                if (Session["Retornar"] != null)
                {
                    Response.Redirect(Session["Retornar"].ToString());
                }
                else
                {
                    Session["Retornar"] = "FrmCarteira.aspx?indmnu=2";
                    Response.Redirect("frmHistoricoCRM.aspx?indmnu=2");
                }
            }

        }

        protected void FiscalLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAbaFiscal.aspx?indmnu=2");
        }

        protected void CadastroIncompletoLinkButton_Click(object sender, EventArgs e)
        {
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

            //Envia para o Bloqueado
            if (ObjEntidadesClass.Alterar_Status_Entidade("13") == "")
            {
                Session["Msg"] = "Cadastro Recusado";


                //Texto Email
                ObjEntidadesClass.OperacaoEmail = "RECUSADO";
                ObjEntidadesClass.Remetente = "Cadastro de Entidades - Web Manuli";
                ObjEntidadesClass.DescricaoEmail = "Cadastro Incompleto - Entidade Codigo: " + ObjEntidadesClass.EntCod;
                ObjEntidadesClass.Texto = " Cadastro informado como Incompleto por : " + ObjEntidadesClass.UsuCod + "<BR>";
                ObjEntidadesClass.Texto += " Codigo Entidade : " + ObjEntidadesClass.EntCod + "<BR>";
                ObjEntidadesClass.Texto += " Nome : " + ObjEntidadesClass.EntNome + "<BR>";
                ObjEntidadesClass.Texto += " CNPJ/CPF : " + ObjEntidadesClass.EntCpfCgc + "<BR>";
                ObjEntidadesClass.Texto += " Data : " + DateTime.Now.ToString("dd/MM/yyyy") + "<BR>";

                ObjEntidadesClass.Envia_Email_Entidade();

                if (Session["Retornar"] != null)
                {
                    Response.Redirect(Session["Retornar"].ToString());
                }
                else
                {
                    Session["Retornar"] = "FrmCarteira.aspx?indmnu=2";
                    Response.Redirect("frmHistoricoCRM.aspx?indmnu=2");
                }
            }
        }

        protected void CadastroCompletoLinkButton_Click(object sender, EventArgs e)
        {
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

            if (ObjEntidadesClass.Alterar_Status_Entidade("10") == "")
            {
                Session["Msg"] = "Entidade enviada para Analise ADM Vendas";


                //Texto Email
                ObjEntidadesClass.OperacaoEmail = "";
                ObjEntidadesClass.Remetente = "Cadastro de Entidades - Web Manuli";
                ObjEntidadesClass.DescricaoEmail = "Alteração WorkFlow - Entidade Codigo: " + ObjEntidadesClass.EntCod;
                ObjEntidadesClass.Texto = " Cadastro encaminhado por : " + ObjEntidadesClass.UsuCod + "<BR>";
                ObjEntidadesClass.Texto += " Codigo Entidade : " + ObjEntidadesClass.EntCod + "<BR>";
                ObjEntidadesClass.Texto += " Nome : " + ObjEntidadesClass.EntNome + "<BR>";
                ObjEntidadesClass.Texto += " CNPJ/CPF : " + ObjEntidadesClass.EntCpfCgc + "<BR>";
                ObjEntidadesClass.Texto += " Data do Encaminhamento : " + DateTime.Now.ToString("dd/MM/yyyy") + "<BR>";


                ObjEntidadesClass.Envia_Email_Entidade();

                if (Session["Retornar"] != null)
                {
                    Response.Redirect(Session["Retornar"].ToString());
                }
                else
                {
                    Session["Retornar"] = "FrmCarteira.aspx?indmnu=2";
                    Response.Redirect("frmHistoricoCRM.aspx?indmnu=2");
                }

            }
        }

        protected void RetornarAdmVendasButton_Click(object sender, EventArgs e)
        {
            ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

            //Envia para o Bloqueado
            if (ObjEntidadesClass.Alterar_Status_Entidade("10") == "")
            {
                Session["Msg"] = "Entidade Enviada para Adm Vendas";


                //Texto Email
                ObjEntidadesClass.OperacaoEmail = "";
                ObjEntidadesClass.Remetente = "Cadastro de Entidades - Web Manuli";
                ObjEntidadesClass.DescricaoEmail = "Cadastro Recusado - Entidade Codigo: " + ObjEntidadesClass.EntCod;
                ObjEntidadesClass.Texto = " Cadastro Recusado por : " + ObjEntidadesClass.UsuCod + "<BR>";
                ObjEntidadesClass.Texto += " Codigo Entidade : " + ObjEntidadesClass.EntCod + "<BR>";
                ObjEntidadesClass.Texto += " Nome : " + ObjEntidadesClass.EntNome + "<BR>";
                ObjEntidadesClass.Texto += " CNPJ/CPF : " + ObjEntidadesClass.EntCpfCgc + "<BR>";
                ObjEntidadesClass.Texto += " Data : " + DateTime.Now.ToString("dd/MM/yyyy") + "<BR>";

                ObjEntidadesClass.Envia_Email_Entidade();

                if (Session["Retornar"] != null)
                {
                    Response.Redirect(Session["Retornar"].ToString());
                }
                else
                {
                    Session["Retornar"] = "FrmCarteira.aspx?indmnu=2";
                    Response.Redirect("frmHistoricoCRM.aspx?indmnu=2");
                }
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