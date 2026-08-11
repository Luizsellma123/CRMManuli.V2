using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;
using VendasWeb.WEBServiceCRM;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;
using VendasWeb.WEBServiceSAP.ClassesWEBService;

namespace VendasWeb.Clientes
{
    public partial class CadastroClienteWebForm : System.Web.UI.Page
    {
        ClienteClasse OBJCliente = new ClienteClasse();
        VendedorClass ObjVendedorClass = new VendedorClass();
        UtilClass ObjUtilClass = new UtilClass();
        usuario Objusuario = new usuario();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";


                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = "";

                //Verificando se deve mandar alerta
                if (Session["Msg"] != null)
                {

                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(), true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                    Session.Remove("Msg");
                }


                //Carrega vendedores conforme autorização
                CarregaCombos();


                if (Session["clienteClasse"] != null)
                {
                    //Descarega a session da Entidade
                    OBJCliente = (ClienteClasse)Session["clienteClasse"];

                    //Carrega dados
                    CarregaDados();

                    BloqueiaButtonSefaz(OBJCliente);

                    TrataAcesso();
                }
                else
                {
                    //Trata como 0 para não dar erro de conversão
                    IDClienteHiddenField.Value = "0";
                }

                BloqueiaCamposConsultaSefaz();
            }
        }

        public void CarregaDados()
        {
            //recupera dados principais da tela
            OBJCliente.carregaDadosPrincipais();

            IDClienteHiddenField.Value = OBJCliente.IDCliente.ToString();

            if (OBJCliente.CodigoCliente != "")
            {
                CodigoClienteTextBox.Text = OBJCliente.CodigoCliente;
            }
            else
            {
                CodigoClienteTextBox.Text = OBJCliente.IDCliente.ToString();
            }

            NomeClienteTextBox.Text = OBJCliente.NomeCliente;
            NomeFantasiaTextBox.Text = OBJCliente.NomeFantasia;
            NumeroCNPJTextBox.Text = ObjUtilClass.SemFormatacaoCNPJCPF(OBJCliente.CNPJCliente);
            EmailTextBox.Text = OBJCliente.EmailCliente;
            TelefoneTextBox.Text = OBJCliente.TelefoneCliente;
            VendedorDropDownList.SelectedValue = OBJCliente.VendedorCliente;
            ObservacaoBreveTextBox.Text = OBJCliente.ObservacaoBreveCliente;

            //Campos para Validar se Houve alterações
            NomeClienteHiddenField.Value = OBJCliente.NomeCliente;
            NumeroCNPJHiddenField.Value = OBJCliente.CNPJ;

            NumeroCNPJTextBox.Enabled = false;

            Session["ClienteClasse"] = OBJCliente;
        }

        protected void GravarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            OBJCliente.CodigoUsuario = Session["usuario"].ToString();
            OBJCliente.IDCliente = Convert.ToInt32(IDClienteHiddenField.Value);
            OBJCliente.CodigoCliente = CodigoClienteTextBox.Text;
            OBJCliente.NomeCliente = NomeClienteTextBox.Text;
            OBJCliente.NomeFantasia = NomeFantasiaTextBox.Text;
            OBJCliente.EmailCliente = EmailTextBox.Text;
            OBJCliente.TelefoneCliente = TelefoneTextBox.Text;
            OBJCliente.VendedorCliente = VendedorDropDownList.SelectedValue;
            OBJCliente.ObservacaoBreveCliente = ObservacaoBreveTextBox.Text;

            string ValidacaoCpfCnpj = "";

            if (RetornaApenasNumeros(NumeroCNPJTextBox.Text).Length == 11 || RetornaApenasNumeros(NumeroCNPJTextBox.Text).Length == 14)
            {
                OBJCliente.CNPJCliente = ObjUtilClass.FormataCNPJCPF(RetornaApenasNumeros(NumeroCNPJTextBox.Text));
                ValidacaoCpfCnpj = ObjUtilClass.Valida_CPF_CNPJ_CRM(OBJCliente.CNPJCliente, OBJCliente.IDCliente, "C");
            }
            else
            {
                ValidacaoCpfCnpj = "é inválido.";
            }

            if (ValidacaoCpfCnpj == "Valido")
            {
                erro = OBJCliente.gravaDadosPrincipais();

                if (erro == "")
                {
                    if (OBJCliente.CodigoCliente == "" || OBJCliente.CodigoCliente == null)
                    {
                        IDClienteHiddenField.Value = OBJCliente.IDCliente.ToString();
                        OBJCliente.CodigoCliente = OBJCliente.IDCliente.ToString();
                        CodigoClienteTextBox.Text = OBJCliente.CodigoCliente;

                        ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Cliente Incluido com Sucesso!", true);
                        ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                        ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();


                        this.UCCadastroCliente.LiberaNavegacao();

                    }
                    else
                    {

                        //Se houver alteração no Nome ou CNPJ verifica se deve voltar para Analise Fiscal
                        if (NomeClienteHiddenField.Value != OBJCliente.NomeCliente || NumeroCNPJHiddenField.Value != OBJCliente.CNPJ)
                        {
                            TrataAlteracaoStatusAnalise();
                        }

                        ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso("Cliente Alterado com Sucesso!", true);
                        ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                        ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();


                    }
                    //Carrega Dados Atualizados em Sessão
                    Session["clienteClasse"] = OBJCliente;


                }
                else
                {
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();

                }
            }
            else
            {

                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro("CPF/CNPJ " + OBJCliente.CNPJCliente + " " + ValidacaoCpfCnpj, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();


                NumeroCNPJTextBox.Text = "";
                NumeroCNPJTextBox.Focus();

            }

        }

        public void CarregaCombos()
        {
            DataTable Resultado = new DataTable();

            ObjVendedorClass.UsuCod = Session["usuario"].ToString();

            if (Session["clienteClasse"] != null)
            {
                //Descarega o id do Cliente
                ObjVendedorClass.IDCliente = ((ClienteClasse)Session["clienteClasse"]).IDCliente;
            }

            Resultado = ObjVendedorClass.Consulta_Vendedor_Cliente();
            VendedorDropDownList.DataSource = Resultado;
            VendedorDropDownList.DataValueField = "IDVendedor";
            VendedorDropDownList.DataTextField = "NomeVendedor";
            VendedorDropDownList.DataBind();
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Session["clienteClasse"] = null;
            Response.Redirect(Session["RetornarNavegacaoPara"].ToString());

        }

        public void TrataAcesso()
        {
            usuario ObjusuarioAux = new usuario();

            ObjusuarioAux = new usuario();
            ObjusuarioAux.CodigoUsuario = Session["usuario"].ToString();
            ObjusuarioAux.ConsultaGrupos("Ativo");

            switch (OBJCliente.IDStatus)
            {
                case 0: //Novo Cadastro
                    GravarButton.Visible = true;
                    break;

                case 1: //Status Cliente Prospectivo
                    GravarButton.Visible = true;
                    break;

                case 2: //Status Cliente Ativo
                case 3: //Status Cliente Inativo
                    GravarButton.Visible = true;
                    break;

                case 4: //Status Cliente Análise Financeira
                        //Verifica se esta no Grupo Análise Financeira
                    if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 7).Count() > 0)
                    {
                        GravarButton.Visible = true;
                    }
                    else
                    {
                        GravarButton.Visible = false;
                    }

                    break;

                case 5: //Status Cliente Análise Fiscal

                    //Verifica se esta no Grupo Análise Fiscal
                    if (ObjusuarioAux.ListaCrmGrupoUsuarioClass.Where(L => L.IDGrupo == 8).Count() > 0)
                    {
                        GravarButton.Visible = true;
                    }
                    else
                    {
                        GravarButton.Visible = false;
                    }

                    break;

                default:
                    GravarButton.Visible = false;
                    break;
            }


            OBJCliente.carregaDadosPrincipais();

            //Desabilita campos quando cliente for SAP
            if (OBJCliente.CodigoCliente != "")
            {
                NomeClienteTextBox.Enabled = false;
                NomeFantasiaTextBox.Enabled = false;
                NumeroCNPJTextBox.Enabled = false;
            }
        }

        public void TrataAlteracaoStatusAnalise()
        {

            OBJCliente.carregaDadosPrincipais();

            switch (OBJCliente.IDStatus)
            {
                case 0: //Novo Cadastro
                    break;
                case 1: //Status Cliente Prospectivo
                    break;
                case 5: //Status Cliente Fiscal
                    break;
                case 4: //Status Cliente Análise Financeira
                    break;

                case 2: //Status Cliente Ativo
                case 3: //Status Cliente Inativo
                    //Enviar Cliente para Analise Fiscal
                    OBJCliente.CodigoUsuario = Session["usuario"].ToString();
                    OBJCliente.IDStatus = 5; //Analise Fiscal
                    OBJCliente.AlteraStatusCliente();
                    break;

                default:
                    break;



            }
        }

        protected void BuscaSefazLinkButton_Click(object sender, EventArgs e)
        {
            string erro = "", JSON = "";

            try
            {
                OBJCliente.CodigoUsuario = Session["usuario"].ToString();

                OBJCliente.IDCliente = Convert.ToInt32(IDClienteHiddenField.Value);

                if (VendedorDropDownList.SelectedValue == "" || VendedorDropDownList.SelectedValue == null)
                    erro = "Escolha um vendedor";
                else
                    OBJCliente.IDVendedor = Convert.ToInt32(VendedorDropDownList.SelectedValue);

                OBJCliente.ObservacaoBreveCliente = ObservacaoBreveTextBox.Text;

                OBJCliente.CNPJCliente = ObjUtilClass.FormataCNPJCPF(RetornaApenasNumeros(NumeroCNPJTextBox.Text));

                string ValidacaoCpfCnpj = ObjUtilClass.Valida_CPF_CNPJ_CRM(OBJCliente.CNPJCliente, OBJCliente.IDCliente, "C");

                if (ValidacaoCpfCnpj != "Valido")
                {
                    if (ValidacaoCpfCnpj == "Invalido")
                        erro = "CNPJ/CPF inválido.";
                    else
                        erro = "CNPJ/CPF " + ValidacaoCpfCnpj;
                }

                WSSaidaDadosReceita objWSSaidaDadosReceita = new WSSaidaDadosReceita();

                if (erro == "")// Recupera dados da API
                {
                    WSEntradaDadosReceita objWSEntradaDadosReceita = new WSEntradaDadosReceita();

                    //Carrega Objeto para enviar
                    if (RetornaApenasNumeros(NumeroCNPJTextBox.Text).Length <= 11)
                        objWSEntradaDadosReceita.TipoConsulta = "PF";
                    else
                        objWSEntradaDadosReceita.TipoConsulta = "PJ";

                    objWSEntradaDadosReceita.NumeroDocumento = RetornaApenasNumeros(NumeroCNPJTextBox.Text).ToString();

                    JsonConversao jsonconv = new JsonConversao();

                    JSON = jsonconv.ConverteObjectParaJSon<WSEntradaDadosReceita>(objWSEntradaDadosReceita);

                    FuncoesAPIClass OBJApi = new FuncoesAPIClass();

                    WSRetornoJSONClass objWSRetornoJSONClass = new WSRetornoJSONClass();

                    objWSRetornoJSONClass = jsonconv.ConverteJSonParaObject<WSRetornoJSONClass>(OBJApi.RecuperaDadosReceitaCRMAPI(JSON));

                    #region JSON usado para testes

                    string jsonTeste = @"
                            {
                                ""CadastroContribuiente"": null,
                                ""IsentoIE"": ""Não"",
                                ""PossuiSimplesNacional"": ""Sim"",
                                ""PossuiSuframa"": null,
                                ""SintegraWSDadosSimplesNacional"": {
                                    ""agendamentos"": ""Campo descontinuado pelo simples nacional."",
                                    ""cnpj"": ""04807000000159"",
                                    ""cnpj_matriz"": ""04.807.000/0001-59"",
                                    ""code"": ""0"",
                                    ""eventos_futuros_simples_nacional"": ""Não Existem"",
                                    ""eventos_futuros_simples_simei"": ""Não Existem"",
                                    ""message"": ""Pesquisa realizada com sucesso."",
                                    ""nome_empresarial"": ""MANUPACKAGING FITASA DO BRASIL S/A"",
                                    ""situacao_simei"": ""NÃO enquadrado no SIMEI"",
                                    ""situacao_simei_anterior"": ""Não Existem"",
                                    ""situacao_simples_nacional"": ""NÃO optante pelo Simples Nacional"",
                                    ""situacao_simples_nacional_anterior"": ""Não Existem"",
                                    ""status"": ""OK""
                                },
                                ""SintegraWSDadosSintegra"": {
                                    ""bairro"": ""Centro industrial de curitiba"",
                                    ""cep"": ""81460020"",
                                    ""cnae_principal"": {
                                        ""code"": ""2222600"",
                                        ""text"": ""Fabricacao de embalagens de material plastico""
                                    },
                                    ""cnpj"": ""04807000000159"",
                                    ""code"": ""0"",
                                    ""complemento"": ""A"",
                                    ""data_fim_atividade"": """",
                                    ""data_inicio_atividade"": ""01-05-2002"",
                                    ""data_situacao_cadastral"": ""30-06-2023"",
                                    ""ibge"": {
                                        ""codigo_municipio"": ""4106902"",
                                        ""codigo_uf"": ""41""
                                    },
                                    ""informacao_ie_como_destinatario"": """",
                                    ""inscricao_estadual"": ""9025667325"",
                                    ""logradouro"": ""Rua emilio romani"",
                                    ""message"": ""Pesquisa realizada com sucesso."",
                                    ""municipio"": ""Curitiba"",
                                    ""nome_empresarial"": ""Manupackaging fitasa do brasil s/a"",
                                    ""nome_fantasia"": ""Não informado"",
                                    ""numero"": ""1250"",
                                    ""porte_empresa"": ""Não informado"",
                                    ""regime_tributacao"": ""Regime normal / normal - dia 12 do mes+1"",
                                    ""situacao_cnpj"": ""Ativo"",
                                    ""situacao_ie"": ""Ativo"",
                                    ""status"": ""OK"",
                                    ""tipo_inscricao"": """",
                                    ""uf"": ""PR""
                                },
                                ""SintegraWSDadosSuframa"": {
                                    ""code"": ""0"",
                                    ""status"": ""OK"",
                                    ""message"": ""Pesquisa realizada com sucesso."",
                                    ""nome_empresarial"": ""Pst eletronica ltda"",
                                    ""cnpj"": ""84496066000104"",
                                    ""inscricao_suframa"": ""200149172"",
                                    ""endereco_eletronico"": ""jcbackes@stoneridge.com"",
                                    ""telefone"": ""9236141425"",
                                    ""situacao_cadastral"": ""ATIVA"",
                                    ""data_validade_cadastral"": """",
                                    ""natureza_juridica"": {
                                        ""codigo"": ""2062"",
                                        ""descricao"": "" Sociedade Empresária Limitada""
                                    },
                                    ""endereco"": {
                                        ""logradouro"": ""Avenida avenida acai"",
                                        ""numero"": ""2.045"",
                                        ""complemento"": ""Lote 2.2"",
                                        ""bairro"": ""Distrito industrial"",
                                        ""cep"": ""69075-020"",
                                        ""municipio"": ""Manaus"",
                                        ""uf"": ""AM""
                                    },
                                    ""atividade_principal"": {
                                        ""codigo"": ""2945000 FABRICAÇÃ"",
                                        ""descricao"": ""O de material elétrico e eletrônico para veículos automotores, exceto baterias"",
                                        ""atividade_exercida"": true
                                    },
                                    ""atividade_secundaria"": [
                                        {
                                            ""codigo"": ""2610800 FABRICAÇÃ"",
                                            ""descricao"": ""O de componentes eletrônicos"",
                                            ""atividade_exercida"": false
                                        },
                                        {
                                            ""codigo"": ""2640000 FABRICAÇÃ"",
                                            ""descricao"": ""O de aparelhos de recepção, reprodução, gravação e amplificação de áudio e vídeo"",
                                            ""atividade_exercida"": false
                                        }
                                    ],
                                    ""incentivos"": [
                                        {
                                            ""tributo"": ""IPI"",
                                            ""beneficio"": ""Isenção"",
                                            ""finalidade"": ""Consumo Interno, Industrialização e Utilização"",
                                            ""base_legal"": ""Decreto 7.212 de 2010 (Art. 81)""
                                        },
                                        {
                                            ""tributo"": ""ICMS"",
                                            ""beneficio"": ""Isenção"",
                                            ""finalidade"": ""Industrialização e Comercialização"",
                                            ""base_legal"": ""Convênio ICMS n° 65 de 1988""
                                        }
                                    ],
                                    ""file_return"": {
                                        ""ext_file"": ""pdf"",
                                        ""url_file"": ""https://sintegraws.com.br/api/v1/suframa/tipo-retorno/comprovante-pdf/c8996da5-6c2e-4a26-bb64-91140414a174""
                                    },
                                    ""version"": ""1""
                                },
                                ""Suframa"": null,
                                ""abertura"": ""11/12/2001"",
                                ""atividade_principal"": [
                                    {
                                        ""code"": ""22.22-6-00"",
                                        ""text"": ""Fabricação de embalagens de material plástico""
                                    }
                                ],
                                ""atividades_secundarias"": [
                                    {
                                        ""code"": ""46.86-9-02"",
                                        ""text"": ""Comércio atacadista de embalagens""
                                    },
                                    {
                                        ""code"": ""46.62-1-00"",
                                        ""text"": ""Comércio atacadista de máquinas, equipamentos para terraplenagem, mineração e construção; partes e peças""
                                    },
                                    {
                                        ""code"": ""46.89-3-99"",
                                        ""text"": ""Comércio atacadista especializado em outros produtos intermediários não especificados anteriormente""
                                    },
                                    {
                                        ""code"": ""20.91-6-00"",
                                        ""text"": ""Fabricação de adesivos e selantes""
                                    },
                                    {
                                        ""code"": ""18.13-0-99"",
                                        ""text"": ""Impressão de material para outros usos""
                                    }
                                ],
                                ""bairro"": ""CIC"",
                                ""billing"": {
                                    ""database"": true,
                                    ""free"": true
                                },
                                ""capital_social"": ""21909494.86"",
                                ""cep"": ""81.460-020"",
                                ""cnpj"": ""04.807.000/0001-59"",
                                ""complemento"": ""A"",
                                ""data_situacao"": ""27/08/2005"",
                                ""data_situacao_especial"": """",
                                ""efr"": """",
                                ""email"": """",
                                ""extra"": {},
                                ""fantasia"": ""MANUPACKAGING FITASA DO BRASIL"",
                                ""logradouro"": ""R EMILIO ROMANI"",
                                ""message"": null,
                                ""motivo_situacao"": """",
                                ""municipio"": ""CURITIBA"",
                                ""natureza_juridica"": ""205-4 - Sociedade Anônima Fechada"",
                                ""nome"": ""MANUPACKAGING FITASA DO BRASIL S/A"",
                                ""numero"": ""1250"",
                                ""porte"": ""DEMAIS"",
                                ""qsa"": [
                                    {
                                        ""nome"": ""SAVERIO LOMBARDINI"",
                                        ""qual"": ""10-Diretor""
                                    },
                                    {
                                        ""nome"": ""THIAGO SOARES ZORTEA"",
                                        ""qual"": ""10-Diretor""
                                    },
                                    {
                                        ""nome"": ""ADEMIR PRADA JUNIOR"",
                                        ""qual"": ""08-Conselheiro de Administração""
                                    },
                                    {
                                        ""nome"": ""MAURIZIO TAGLIATTI"",
                                        ""qual"": ""10-Diretor""
                                    }
                                ],
                                ""situacao"": ""ATIVA"",
                                ""situacao_especial"": """",
                                ""status"": ""OK"",
                                ""telefone"": ""(41) 2169-6000"",
                                ""tipo"": ""MATRIZ"",
                                ""uf"": ""PR"",
                                ""ultima_atualizacao"": ""2023-06-03T19:33:40.517Z""
                            }";

                    #endregion

                    //objWSRetornoJSONClass.JSONRetorno = jsonTeste;

                    if (objWSRetornoJSONClass.MsgRetorno == "")
                        objWSSaidaDadosReceita = jsonconv.ConverteJSonParaObject<WSSaidaDadosReceita>(objWSRetornoJSONClass.JSONRetorno);
                    else if (erro == "")
                        erro = objWSRetornoJSONClass.MsgRetorno;
                }

                #region VERIFICA SE A PESQUISA FOI REALIZADA COM SUCESSO

                if (erro == "" && objWSSaidaDadosReceita.message != null && objWSSaidaDadosReceita.message != "null")
                {
                    erro = objWSSaidaDadosReceita.message;
                }

                if (erro == "" && objWSSaidaDadosReceita.SintegraWSDadosSimplesNacional.message != "Pesquisa realizada com sucesso.")
                {
                    erro = objWSSaidaDadosReceita.SintegraWSDadosSimplesNacional.message;
                }

                if (erro == "" && objWSSaidaDadosReceita.SintegraWSDadosSintegra.message != "Pesquisa realizada com sucesso.")
                {
                    objWSSaidaDadosReceita.SintegraWSDadosSintegra = null;
                    //erro = objWSSaidaDadosReceita.SintegraWSDadosSintegra.message;
                }

                if (erro == "" && objWSSaidaDadosReceita.SintegraWSDadosSuframa != null
                && objWSSaidaDadosReceita.SintegraWSDadosSuframa.message != "Pesquisa realizada com sucesso.")
                {
                    objWSSaidaDadosReceita.SintegraWSDadosSuframa = null;
                    //erro = objWSSaidaDadosReceita.SintegraWSDadosSuframa.message;
                }

                #endregion

                if (erro == "") erro = OBJCliente.GravaClienteSefaz(objWSSaidaDadosReceita);

                if (erro == "") erro = OBJCliente.GravaClienteEnderecoSefaz(objWSSaidaDadosReceita);

                if (erro == "") erro = OBJCliente.GravaClienteFiscalSefaz(objWSSaidaDadosReceita);

                if (erro == "") erro = GravaHistorico(OBJCliente);

                if (erro == "")
                {
                    Session["clienteClasse"] = OBJCliente;

                    CarregaCombos();

                    CarregaDados();

                    BloqueiaButtonSefaz(OBJCliente);

                    TrataAcesso();

                    this.UCCadastroCliente.LiberaNavegacao();

                    BloqueiaCamposConsultaSefaz();

                    NumeroCNPJTextBox.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            ApresentaMensagem(erro);
        }

        protected void BloqueiaButtonSefaz(ClienteClasse objClienteClasse)
        {
            if (objClienteClasse.CodigoCliente != "") //Alteração
            {
                BuscaSefazLinkButton.Enabled = false;
            }
        }

        protected void BloqueiaCamposConsultaSefaz()
        {
            ClienteClasse objClienteClasse = new ClienteClasse();

            objClienteClasse.CodigoUsuario = Session["usuario"].ToString();

            if (objClienteClasse.RetornaBloqueiaCamposConsultaSefaz())
            {
                NomeClienteTextBox.Enabled = false;
                //NumeroCNPJTextBox.Enabled = false;
            }
        }

        protected void NumeroCNPJTextBox_TextChanged(object sender, EventArgs e)
        {
            ClienteClasse objClienteClasse = new ClienteClasse();

            objClienteClasse.CodigoUsuario = Session["usuario"].ToString();

            if (RetornaApenasNumeros(NumeroCNPJTextBox.Text).Length <= 11)
                NomeClienteTextBox.Enabled = true;
            else if (objClienteClasse.RetornaBloqueiaCamposConsultaSefaz())
            {
                NomeClienteTextBox.Enabled = false;
                NomeClienteTextBox.Text = "";
            }
        }

        protected string RetornaApenasNumeros(string texto)
        {
            return Regex.Replace(texto, @"[^0-9]", "");
        }

        protected string GravaHistorico(ClienteClasse objClienteClasse)
        {
            HistoricosClass objHistorico = new HistoricosClass();

            objHistorico.IDCliente = objClienteClasse.IDCliente;
            objHistorico.IDTipoHistorico = 1;
            objHistorico.IDEvento = 7;
            objHistorico.IDCategoria = 1;
            objHistorico.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
            objHistorico.Historico = "Cliente foi cadastrado com sucesso utilizando integração com Sintegra.";

            return objHistorico.GravaHistoricoCliente();
        }

        protected void ApresentaMensagem(string erro = "")
        {
            if (erro != "")
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
            else
            {
                erro = "Operação realizada com sucesso.";
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(erro, true);
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }
    }
}