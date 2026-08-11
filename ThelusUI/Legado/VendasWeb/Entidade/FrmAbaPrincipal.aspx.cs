using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidade
{
    public partial class FrmAbaPrincipal : System.Web.UI.Page
    {
        usuario ObjUsuarioClass = new usuario();
        funcoes mdlFuncoes = new funcoes();
        SessionClass OBJSessao = new SessionClass();
        clsEntidades ObjEntidadesClass;
        clsEntWeb ObjclsEntWeb = new clsEntWeb();
        ContatoClass ObjContatoClass = new ContatoClass();
        criptografia mdlCriptografia = new criptografia();
        UtilClass ObjUtilClass = new UtilClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = "";

            //Verificando se deve mandar alerta
            if (Session["Msg"] != null)
            {
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(),true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                Session.Remove("Msg");
            }

            if (!IsPostBack)
            {
                //Valida Acesso
                OBJSessao.ValidaAcesso();

                ObjEntidadesClass = new GerencialVendas.clsEntidades();
                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();

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

                VendCodDropDownList.SelectedValue = vendCod;

                CategoriaDropDownList.Items.Clear();

                CategoriaDropDownList.DataSource = ObjEntidadesClass.Consulta_Categoria_Entidade_Geral("DireitosUsu");
                CategoriaDropDownList.DataTextField = "CategNome";
                CategoriaDropDownList.DataValueField = "CategCodEstr";
                CategoriaDropDownList.DataBind();

                if (ObjEntidadesClass.TipoOperacao == "Cadastro Completo")
                {
                    CategoriaDropDownList.SelectedValue = ObjEntidadesClass.Consulta_Categoria_Entidade_Selecionada("DireitosEnt", ObjEntidadesClass.EntCod);
                }
                else
                {
                    ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                    CategoriaDropDownList.SelectedValue = ObjEntidadesClass.Consulta_Categoria_Usuario("DireitoUsuAtual", ObjEntidadesClass.EntCod);
                }
                #endregion

                if (Session["clsEntidades"] == null)
                {
                    Session["clsEntidades"] = ObjEntidadesClass;
                    ObjEntidadesClass.TipoOperacao = "Inclusão";
                }
                else
                {
                    
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];

                    //Carrega dados na Tela
                    if (ObjEntidadesClass.TipoOperacao != "")//Incluir ou Alterar
                    {
                        CarregaDadosNaTela();

                        if (ObjEntidadesClass.TipoOperacao == "Consultar")
                        {
                            BloqueiaCampos();
                        }
                    }
                    else
                    {
                        ObjEntidadesClass.TipoOperacao = "Alterar";
                    }
                }
            }
        }

        public string CarregaDadosDaTela()
        {
            string Retorno = "";

            if (Session["clsEntidades"] != null)
            {
                ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
                //Informar se o campo é ou não obrigatório setando a propriedade CausesValidation 
                //Cnpj_CpfTextBox.CausesValidation = true;
                //ObjEntidadesClass.TipoOperacao = "Alterar";
            }
           /* else
            {*/
                //ObjEntidadesClass = new clsEntidades();
                //ObjEntidadesClass.TipoOperacao = "Inclusão";

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
            //}

            ObjEntidadesClass.EntCod = EntCodLabel.Text;
            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
            ObjEntidadesClass.EntNome = razaoSocialTextBox.Text.ToUpper().Trim();
            ObjEntidadesClass.EntNomeFant = NomeFantasiaTextBox.Text.ToUpper().Trim();
            ObjEntidadesClass.CategCodEstr = CategoriaDropDownList.SelectedValue;

            ObjEntidadesClass.EntCep = CepTextBox.Text.Replace("-", "");
            ObjEntidadesClass.EntLograd = "";
            ObjEntidadesClass.EntEnder = EnderecoTextBox.Text.ToUpper().Trim();
            ObjEntidadesClass.EntBair = BairroTextBox.Text.ToUpper().Trim();
            ObjEntidadesClass.CidCod = CidadeDropDownList.SelectedValue;
            ObjEntidadesClass.UFSIGLA = UFTextBox.Text.ToUpper();
            ObjEntidadesClass.EntEnderComp = ComplementoTextBox.Text.ToUpper().Trim();
            //ObjEntidadesClass.ObsLogistica = ObsLogisticaTextBox.Text;

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
            
            #region Carregando Dados do Email XML
            /*ObjEntidadesClass.ListEntWeb = new List<classes.clsEntWeb>();
            ObjclsEntWeb = new classes.clsEntWeb();

            ObjclsEntWeb.EntWebTipo = "Financeiro";
            ObjclsEntWeb.EntWebWWW = null;
            ObjclsEntWeb.EntWebEMailPrinc = "Sim";
            ObjclsEntWeb.EntWebEMailPedComp = "Não";
            ObjclsEntWeb.EntWebRecebeEmailOcor = "Não";
            ObjclsEntWeb.EntWebDisparaEmailAgenda = "Não";
            ObjclsEntWeb.EntWebEmailNFe = "Sim";
            ObjclsEntWeb.EntWebEmailNFSe = "Sim";

            ObjEntidadesClass.AdicionarEmail(ObjclsEntWeb);*/
            #endregion
                       
            return Retorno;
        }

        public string CarregaDadosNaTela()
        {
            if (ObjEntidadesClass.TipoOperacao == "Alterar")
            {
                DataCadastroDiv.Attributes.Add("style", "display:block;");
            }

            EntCodLabel.Text = ObjEntidadesClass.EntCod;
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
            //ObsLogisticaTextBox.Text = ObjEntidadesClass.ObsLogistica;

            //Vendedor
            VendCodDropDownList.SelectedValue = ObjEntidadesClass.VendCod;
            /*VendCodDropDownList.Visible = false;
            VendCodLabel.Visible = false;*/

            ObjEntidadesClass.UsuCod = ObjEntidadesClass.Lista_Usuario_Vendedor();
            CategoriaDropDownList.SelectedValue = ObjEntidadesClass.Consulta_Categoria_Usuario("DireitoUsuAtual", ObjEntidadesClass.EntCod);

            //Inscricao Estadual
            InscricaoEstadualTextBox.Text = ObjEntidadesClass.EntRgIe;

            //Data Cadastro
            if (ObjEntidadesClass.EntDataCad != null && ObjEntidadesClass.EntDataCad.ToString() != "")
            {
                DataCadastroLabel.Text = "Data de Cadastro: " + ObjEntidadesClass.EntDataCad.ToString("dd/MM/yyyy");
            }

            //Status
            StatusLabel.Text = ObjEntidadesClass.EntStatDescr;

            //NovaLojaDropDownList.SelectedValue = ObjEntidadesClass.NovaLoja;

           // Cnpj_CpfTextBox.Focus();
            return "";
        }

        protected void Cnpj_CpfTextBox_TextChanged(object sender, EventArgs e)
        {
            string Retorno = "";
            if (ObjEntidadesClass == null)
            {
                if (Session["clsEntidades"] != null)
                {
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
                }
                else
                {
                    ObjEntidadesClass = new GerencialVendas.clsEntidades();
                }
            }                

            ObjEntidadesClass.EntCpfCgc = Cnpj_CpfTextBox.Text.Trim().Replace("-", "");
            ObjEntidadesClass.EntCpfCgc = ObjEntidadesClass.EntCpfCgc.Replace(".", "");
            ObjEntidadesClass.EntCpfCgc = ObjEntidadesClass.EntCpfCgc.Replace("/", "");

            Retorno = mdlFuncoes.Valida_CPF_CNPJ(ObjEntidadesClass.EntCpfCgc, ObjEntidadesClass.EntCod);

            if (Retorno != "Valido")
            {                
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro("CPF/CNPJ " + ObjEntidadesClass.EntCpfCgc + " " + Retorno, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();


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
            if (ObjEntidadesClass == null)
            {
                if (Session["clsEntidades"] != null)
                {
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
                }
                else
                {
                    ObjEntidadesClass = new GerencialVendas.clsEntidades();
                }
            }

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

                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("CEP não encontrado, ou cadastro incompleto. Favor entrar em contato com a ADM Vendas para verificar o CEP", true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
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

                    ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Numero Invalido!", true);
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
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
            if (ObjEntidadesClass == null)
            {
                if (Session["clsEntidades"] != null)
                {
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
                }
                else
                {
                    ObjEntidadesClass = new GerencialVendas.clsEntidades();
                }
            }

            string UF = UFTextBox.Text.ToString().ToUpper();
            string InscricaoEstatual = InscricaoEstadualTextBox.Text.ToString().ToUpper();

            try
            {
                if (ObjEntidadesClass.ValidarInscricaoEstadual(UF, InscricaoEstatual) == false)
                {
                    //Retorna Mensagem de Erro
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Inscrição Estadual Invalida!", true);
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                }
                else
                {
                    //ObsLogisticaTextBox.Focus();
                    ProximoPassoButton.Focus();
                }
            }
            catch
            {
                //Retorna Mensagem de Erro
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta("Inscrição Estadual Invalida!", true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void ProximoPassoButton_Click(object sender, EventArgs e)
        {           
            CarregaDadosDaTela();
            Session["clsEntidades"] = ObjEntidadesClass;

            Response.Redirect("FrmAbaContatos.aspx?indmnu=2");           
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            Session["clsEntidades"] = null;
            Response.Redirect("../Entidades/FrmCarteira.aspx?indmnu=5");
        }

        protected void BloqueiaCampos()
        {
            Cnpj_CpfTextBox.Enabled = false;
            razaoSocialTextBox.Enabled = false;
            NomeFantasiaTextBox.Enabled = false;
            CategoriaDropDownList.Enabled = false;
            CepTextBox.Enabled = false;
            EnderecoTextBox.Enabled = false;
            NumeroTextBox.Enabled = false;
            BairroTextBox.Enabled = false;
            CidadeDropDownList.Enabled = false;
            UFTextBox.Enabled = false;
            ComplementoTextBox.Enabled = false;
            VendCodDropDownList.Enabled = false;
            InscricaoEstadualTextBox.Enabled = false;
            StatusLabel.Enabled = false;
            ProximoPassoButton.CausesValidation = false;
        }
    }
}