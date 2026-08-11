using System;
using System.Data;
using VendasWeb.classes;
using System.Globalization;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Entidades
{
    public partial class FrmSimuladorVendedorBkp : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        funcoes mdlfuncoes = new funcoes();
        SimuladorClassBkp simulador = new SimuladorClassBkp();
        SimuladorClassBkp AcessoSim = new SimuladorClassBkp();
        ClienteClasse objClienteClasse = new ClienteClasse();
        ClienteClasse objCliente = new ClienteClasse();
        LogisticaClass objLogistica = new LogisticaClass();
        bool classificacaoCarregada;

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                CopiaLinkButton.Visible = false;

                simulador.CodigoUsuario = Session["usuario"].ToString();
                simulador.RecuperaNivelVendedor();

                VendedorDropDownList.CssClass = "form-control";

                VendedorDropDownList.Enabled = false;

                if (simulador.TipoVendedor != "" && simulador != null && simulador.TipoVendedor != "Todos")
                    VendedorDropDownList.SelectedValue = simulador.TipoVendedor;
                else if (simulador.TipoVendedor == "Todos")
                {
                    VendedorDropDownList.Enabled = true;
                    VendedorDropDownList.CssClass = "form-control fstdropdown-select";
                }

                CarregaCombos();

                simulador.codempresa = "1";

                FaturamentoDropDownList.DataSource = simulador.Consulta_Local();
                FaturamentoDropDownList.DataTextField = "LocalFaturamento";
                FaturamentoDropDownList.DataBind();

                MudaInputs();

                DataTable data = new DataTable();
                data = simulador.Consulta_Produto();
                data.Columns.Add("CodNome", typeof(string), "CodigoProduto + ' - ' + NomeProduto");
                ProdutoDropDownList.DataSource = data;
                ProdutoDropDownList.DataTextField = ("CodNome");
                ProdutoDropDownList.DataValueField = "CodigoProduto";
                ProdutoDropDownList.DataBind();

                classificacaoCarregada = false;

                CarregaDadosSimulacaoSalvaNaTela();

                CarregaDadosSessionSimulacaoSalvaNaTela();

                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
            }
        }

        protected void CarregaDadosSimulacaoSalvaNaTela()
        {
            if (Session["AcessoSim"] != null)
            {
                AcessoSim = (SimuladorClassBkp)Session["AcessoSim"];

                EmpresaDropDownList.SelectedValue = AcessoSim.empresa;

                simulador.codempresa = AcessoSim.empresa;

                EmpresaDropDownList_SelectedIndexChanged(null, null);

                //Recerrega o LocalFaturamento
                FaturamentoDropDownList.Items.Clear();
                FaturamentoDropDownList.DataSource = simulador.Consulta_Local();
                FaturamentoDropDownList.DataTextField = "LocalFaturamento";
                FaturamentoDropDownList.DataBind();

                VendedorDropDownList.SelectedValue = AcessoSim.NivelVendedor;
                ProdutoDropDownList.Items[ProdutoDropDownList.SelectedIndex].Value = AcessoSim.produto;
                ProdutoDropDownList.Items[ProdutoDropDownList.SelectedIndex].Text = AcessoSim.produtoNome;
                QuantidadeTextBox.Text = Convert.ToString(AcessoSim.quantidade);
                FaturamentoDropDownList.SelectedValue = AcessoSim.LocalFaturamento;
                ObservBox.Text = AcessoSim.observacao;
                ClienteInput.Text = AcessoSim.NomeCliente;

                if (AcessoSim.empresa == "3")
                    PrecoInputTextBox.Text = AcessoSim.ValorICMS.ToString();
                else
                    ICMSTextBox.Text = AcessoSim.ValorICMS.ToString();

                //Seta classificação comercial caso seja alteração e não inclusão
                {
                    ClassificacaoComercialDropDownList.SelectedValue = AcessoSim.IDClassificacaoComercial.ToString();

                    if (ClassificacaoComercialDropDownList.SelectedValue != null)
                        classificacaoCarregada = true;
                }

                FreteDropDownList.SelectedValue = AcessoSim.IDTipoFrete.ToString();

                NovoClienteCheck.Checked = (AcessoSim.NovoCliente == "Sim");

                AvistaCheckBox.Checked = Convert.ToBoolean(AcessoSim.AVista);

                PaisDropDownList.SelectedValue = AcessoSim.IDPais.ToString();

                EstadoDropDownList.SelectedValue = AcessoSim.IDEstado.ToString();

                EstadoDropDownList_SelectedIndexChanged(null, null);

                MunicipioDropDownList.SelectedValue = AcessoSim.IDMunicipio.ToString();

                MunicipioDropDownList_SelectedIndexChanged(null, null);

                TransportadorDropDownList.SelectedValue = AcessoSim.IDTransportador.ToString();

                ValorFreteHiddenField.Value = Convert.ToDecimal(AcessoSim.ValorFrete).ToString("C", CultureInfo.GetCultureInfo("pt-BR"));

                PrevisaoEntregaHiddenField.Value = AcessoSim.PrevisaoEntrega;

                ValorItemTextBox.Text = AcessoSim.ValorICMS.ToString();

                DescontoTextBox.Text = AcessoSim.Desconto.ToString();

                ValorComDescontoTextBox.Text = AcessoSim.ValorComDesconto.ToString();

                //Simulando um click no botão simulação
                SimularButton_Click(null, null);

                //Desativando os campos 
                ClienteInput.Enabled = false;
                EmpresaDropDownList.CssClass = "form-control";
                EmpresaDropDownList.Enabled = false;
                VendedorDropDownList.CssClass = "form-control";
                VendedorDropDownList.Enabled = false;
                ProdutoDropDownList.CssClass = "form-control";
                ProdutoDropDownList.Enabled = false;
                QuantidadeTextBox.Enabled = false;
                FaturamentoDropDownList.CssClass = "form-control";
                FaturamentoDropDownList.Enabled = false;
                ClassificacaoComercialDropDownList.CssClass = "form-control";
                ClassificacaoComercialDropDownList.Enabled = false;
                PaisDropDownList.CssClass = "form-control";
                PaisDropDownList.Enabled = false;
                EstadoDropDownList.CssClass = "form-control";
                EstadoDropDownList.Enabled = false;
                MunicipioDropDownList.CssClass = "form-control";
                MunicipioDropDownList.Enabled = false;
                TransportadorDropDownList.CssClass = "form-control";
                TransportadorDropDownList.Enabled = false;
                //CalcularFreteLinkButton.Enabled = false;
                ObservBox.Enabled = false;
                ICMSTextBox.Enabled = false;
                PrecoInputTextBox.Enabled = false;
                NovoClienteCheck.Enabled = false;
                AnaliseButton.Visible = false;
                AnaliseButton.Enabled = false;
                SimularButton.Visible = false;
                SimularButton.Enabled = false;
                PlusButton.Enabled = false;
                SalvaSimulacaoLinkButton.Visible = false;
                CopiaLinkButton.Visible = true;
                FreteDropDownList.CssClass = "form-control";
                FreteDropDownList.Enabled = false;
                AvistaCheckBox.Enabled = false;
                DescontoTextBox.Enabled = false;
                CalcularDescontoLinkButton.Enabled = false;

                Session["AcessoSim"] = null;
            }
        }

        protected void CarregaDadosSessionSimulacaoSalvaNaTela()
        {
            if (Session["ObjSimulacao"] != null)
            {
                simulador = (SimuladorClassBkp)Session["ObjSimulacao"];

                EmpresaDropDownList.SelectedValue = simulador.codempresa;

                EmpresaDropDownList_SelectedIndexChanged(null, null);

                //Recerrega o LocalFaturamento
                FaturamentoDropDownList.Items.Clear();
                FaturamentoDropDownList.DataSource = simulador.Consulta_Local();
                FaturamentoDropDownList.DataTextField = "LocalFaturamento";
                FaturamentoDropDownList.DataBind();

                VendedorDropDownList.SelectedValue = simulador.NivelVendedor;
                ProdutoDropDownList.SelectedValue = simulador.produto;
                QuantidadeTextBox.Text = Convert.ToString(simulador.Quantidade);
                FaturamentoDropDownList.SelectedValue = simulador.LocalFaturamento;
                ObservBox.Text = simulador.observacao;

                PaisDropDownList.SelectedValue = simulador.IDPais.ToString();

                EstadoDropDownList.SelectedValue = simulador.IDEstado.ToString();

                EstadoDropDownList_SelectedIndexChanged(null, null);

                MunicipioDropDownList.SelectedValue = simulador.IDMunicipio.ToString();

                MunicipioDropDownList_SelectedIndexChanged(null, null);

                TransportadorDropDownList.SelectedValue = simulador.IDTransportador.ToString();

                ValorFreteHiddenField.Value = Convert.ToDecimal(simulador.ValorFrete).ToString("C", CultureInfo.GetCultureInfo("pt-BR"));

                DescontoTextBox.Text = simulador.Desconto.ToString();

                ValorComDescontoTextBox.Text = simulador.ValorComDesconto.ToString();

                //Descobrindo se a pagina foi aberta retornando da lista de clientes ou não
                if (Session["ClienteSim"] != null)
                {
                    ClienteInput.Text = (string)Session["ClienteSim"];

                    Session["ClienteSim"] = null;

                    Session["UfSigla"] = null;

                    if (Session["CodigoClienteSimulador"] != null && !classificacaoCarregada)
                    {
                        objClienteClasse.CodigoCliente = (string)Session["CodigoClienteSimulador"];

                        DataTable ClassificacaoComercialDataTable = objClienteClasse.CarregaClassificacaoComercial();

                        if (ClassificacaoComercialDataTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in ClassificacaoComercialDataTable.Rows)
                            {
                                ClassificacaoComercialDropDownList.SelectedValue = row["IDClassificacaoComercial"].ToString();

                                if (ClassificacaoComercialDropDownList.SelectedValue == "0")
                                {
                                    ClassificacaoComercialDropDownList.SelectedValue = simulador.IDClassificacaoComercial.ToString();
                                }
                                else
                                {
                                    ClassificacaoComercialDropDownList.CssClass = "form-control";

                                    ClassificacaoComercialDropDownList.Enabled = false;
                                }
                            }
                        }
                    }
                }

                MudaInputs();

                if (PrecoInputTextBox.Enabled == false) ICMSTextBox.Text = simulador.ICMS.ToString();
                else PrecoInputTextBox.Text = simulador.ICMS.ToString();

                ValorItemTextBox.Text = simulador.ICMS.ToString();

                Session["ObjSimulacao"] = null;
            }
        }

        protected void EmpresaDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Limpar campos 
            SimuladorMultiView.Visible = false;
            VendedorDropDownList.SelectedIndex = 0;
            ClienteInput.Text = "";
            ObservBox.Text = string.Empty;

            simulador.codempresa = EmpresaDropDownList.SelectedValue.ToString();

            DataTable data = new DataTable();
            data = simulador.Consulta_Produto();
            data.Columns.Add("CodNome", typeof(string), "CodigoProduto + ' - ' + NomeProduto");
            ProdutoDropDownList.DataSource = data;
            ProdutoDropDownList.DataTextField = ("CodNome");
            ProdutoDropDownList.DataValueField = "CodigoProduto";
            ProdutoDropDownList.DataBind();

            FaturamentoDropDownList.DataSource = simulador.Consulta_Local();
            FaturamentoDropDownList.DataTextField = "LocalFaturamento";
            FaturamentoDropDownList.DataBind();

            MudaInputs();

            simulador.CodigoUsuario = Session["usuario"].ToString();
            simulador.RecuperaNivelVendedor();

            //Desabilita Combo Tipo Vendedor
            VendedorDropDownList.CssClass = "form-control";
            VendedorDropDownList.Enabled = false;

            if (simulador.TipoVendedor != "" && simulador != null && simulador.TipoVendedor != "Todos")
                VendedorDropDownList.SelectedValue = simulador.TipoVendedor;
            else if (simulador.TipoVendedor == "Todos")
            {
                VendedorDropDownList.Enabled = true;
                VendedorDropDownList.CssClass = "form-control fstdropdown-select";
            }

            FretesClass objFretesClass = new FretesClass();

            objFretesClass.empcod = EmpresaDropDownList.SelectedValue;

            FreteDropDownList.DataSource = objFretesClass.CarregaFreteIncoterms();
            FreteDropDownList.DataTextField = "Descricao";
            FreteDropDownList.DataValueField = "IDTipoFrete";
            FreteDropDownList.DataBind();

            if (EmpresaDropDownList.SelectedValue == "3")
                BloqueiaCamposFrete();
            else if (FreteDropDownList.SelectedItem.Text == "FOB")
                BloqueiaCamposFrete();
            else
                LiberaCamposFrete();
        }

        protected void CarregaCombos()
        {
            FretesClass objFretesClass = new FretesClass();

            objFretesClass.empcod = EmpresaDropDownList.SelectedValue;

            FreteDropDownList.DataSource = objFretesClass.CarregaFreteIncoterms();
            FreteDropDownList.DataTextField = "Descricao";
            FreteDropDownList.DataValueField = "IDTipoFrete";
            FreteDropDownList.DataBind();

            ClassificacaoComercialDropDownList.DataSource = objClienteClasse.CarregaClassificacaoComercial();
            ClassificacaoComercialDropDownList.DataTextField = "Descricao";
            ClassificacaoComercialDropDownList.DataValueField = "IDClassificacaoComercial";
            ClassificacaoComercialDropDownList.DataBind();

            objCliente.IDPais = PaisDropDownList.SelectedValue;

            objCliente.IDEstado = "0";

            EstadoDropDownList.DataSource = objCliente.RetornaListaEstados();
            EstadoDropDownList.DataTextField = "Nome";
            EstadoDropDownList.DataValueField = "IDEstado";
            EstadoDropDownList.DataBind();

            EstadoDropDownList_SelectedIndexChanged(null, null);
        }

        protected void EstadoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            objCliente.IDPais = PaisDropDownList.SelectedValue;

            objCliente.IDEstado = EstadoDropDownList.SelectedValue;

            objCliente.IDMunicipio = "0";

            MunicipioDropDownList.DataSource = objCliente.RetornaListaMunicipios();
            MunicipioDropDownList.DataValueField = "IDMunicipio";
            MunicipioDropDownList.DataTextField = "NomeMunicipio";
            MunicipioDropDownList.DataBind();

            MunicipioDropDownList_SelectedIndexChanged(null, null);
        }

        protected void MunicipioDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogisticaClass objLogistica = new LogisticaClass();

            objLogistica.IDPais = Convert.ToInt32(PaisDropDownList.SelectedValue);

            objLogistica.IDEstado = Convert.ToInt32(EstadoDropDownList.SelectedValue);

            objLogistica.IDMunicipio = Convert.ToInt32(MunicipioDropDownList.SelectedValue);

            TransportadorDropDownList.DataSource = objLogistica.RetornaListaTransportadorRegiaoMunicipio_Transportador();
            TransportadorDropDownList.DataTextField = "Descricao";
            TransportadorDropDownList.DataValueField = "IDTransportador";
            TransportadorDropDownList.DataBind();
        }

        protected string CarregaDadosDaTela(string metodo)
        {
            if (metodo != "PlusButton_Click")
            {
                if (QuantidadeTextBox.Text == "") return "Informe a quantidade";

                if (ProdutoDropDownList.SelectedValue == "") return "Escolha o Produto";

                if (EmpresaDropDownList.SelectedValue == "") return "Escolha a empresa";

                if (EmpresaDropDownList.SelectedValue == "1" || EmpresaDropDownList.SelectedValue == "2")
                {
                    if (ICMSTextBox.Text == "") return "Informe o Ex-ICMS";
                }
                else if (PrecoInputTextBox.Text == "") return "Informe o preço final";

                if (PaisDropDownList.SelectedValue == "") return "Escolha o pais";

                if (EstadoDropDownList.SelectedValue == "") return "Escolha o estado";

                if (MunicipioDropDownList.SelectedValue == "") return "Escolha o municipio";
            }

            if (metodo == "SimularButton_Click" || metodo == "SalvaSimulacao")
            {
                if (FaturamentoDropDownList.SelectedValue == "") return "Escolha o local do faturamento";

                if (VendedorDropDownList.SelectedValue == "") return "Escolha o Nível do vendedor";

                if (ClassificacaoComercialDropDownList.SelectedValue == "0") return "Escolha a classificação comercial.";

                if (FreteDropDownList.SelectedValue == "0") return "Escolha o tipo de frete.";

                if (metodo == "SalvaSimulacao")
                {
                    if (ClienteInput.Text == "") return "Informe o cliente";

                    if (QuantidadeTextBox.Text == "" && QuantidadeTextBox.Text == "0") return "Informe a quantidade";
                }

                CarregaDadosDaTela_simulador();

                if (FreteDropDownList.SelectedItem.Text == "CIF" && ValorFreteHiddenField.Value == "" && EmpresaDropDownList.SelectedValue != "3")
                    //return "Não foi possível calcular o valor do frete";
                    return CarregaDadosDaTela("CalcularFreteLinkButton_Click");
            }
            else if (metodo == "CalcularFreteLinkButton_Click")
            {
                if (TransportadorDropDownList.SelectedValue == "") return "Escolha o transportador (Se não aparecer transportadora para este município cadastre uma)";

                CarregaDadosDaTela_objLogistica();

                if (objLogistica.IDRegiao == 0) return "Não foi possível retornar a região da transportadora";
            }
            else if (metodo == "PlusButton_Click")
            {
                CarregaDadosDaTela_simulador();
            }

            return "";
        }

        protected void CarregaDadosDaTela_simulador()
        {
            simulador.codempresa = EmpresaDropDownList.SelectedValue.ToString();

            simulador.Arredonda_codempresa();

            simulador.produto = ProdutoDropDownList.SelectedValue;

            simulador.LocalFaturamento = FaturamentoDropDownList.SelectedValue;

            simulador.NivelVendedor = VendedorDropDownList.SelectedValue;

            simulador.IDClassificacaoComercial = Convert.ToInt32(ClassificacaoComercialDropDownList.SelectedValue);

            simulador.IDTipoFrete = Convert.ToInt32(FreteDropDownList.SelectedValue);

            simulador.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);

            if (AvistaCheckBox.Checked)
                simulador.AVista = 1;
            else
                simulador.AVista = 0;

            if (simulador.codempresa == "1" || simulador.codempresa == "2")
            {
                simulador.ICMS = Convert.ToDecimal(ICMSTextBox.Text == "" ? "0" : ICMSTextBox.Text);

                simulador.ValorICMS = Convert.ToDecimal(ICMSTextBox.Text == "" ? "0" : ICMSTextBox.Text);
            }
            else if (PrecoInputTextBox.Text != "")
            {
                simulador.ICMS = Convert.ToDecimal(PrecoInputTextBox.Text == "" ? "0" : PrecoInputTextBox.Text);

                simulador.ValorICMS = Convert.ToDecimal(PrecoInputTextBox.Text == "" ? "0" : PrecoInputTextBox.Text);
            }

            simulador.Quantidade = Convert.ToDecimal(QuantidadeTextBox.Text == "" ? "0" : QuantidadeTextBox.Text);

            simulador.NomeCliente = ClienteInput.Text;

            simulador.usucod = Session["usuario"].ToString();

            simulador.observacao = ObservBox.Text;

            if (!NovoClienteCheck.Checked)
                simulador.NovoCliente = "Não";
            else
                simulador.NovoCliente = "Sim";

            simulador.DataSimulacao = DateTime.Now;

            // simulador.CodigoEstadoSAP
            {
                objCliente.IDPais = PaisDropDownList.SelectedValue;

                objCliente.IDEstado = EstadoDropDownList.SelectedValue;

                DataTable TransportadorasDataTable = objCliente.RetornaListaEstados();

                foreach (DataRow row in TransportadorasDataTable.Rows)
                {
                    simulador.CodigoEstadoSAP = row["CodigoEstadoSAP"].ToString();

                    simulador.estado = row["CodigoEstadoSAP"].ToString();

                    break;
                }
            }

            simulador.IDPais = Convert.ToInt32(PaisDropDownList.SelectedValue);

            simulador.IDEstado = Convert.ToInt32(EstadoDropDownList.SelectedValue);

            if (FreteDropDownList.SelectedItem.Text == "CIF" && EmpresaDropDownList.SelectedValue != "3")
            {
                CalcularFreteLinkButton_Click(null, null);

                if (ValorFreteHiddenField.Value != "")
                {
                    simulador.IDTransportador = objLogistica.IDTransportador;

                    simulador.IDMunicipio = objLogistica.IDMunicipio;

                    simulador.IDRegiao = objLogistica.IDRegiao;

                    simulador.ValorFrete = decimal.Parse(ValorFreteHiddenField.Value, NumberStyles.Currency, new CultureInfo("pt-BR"));

                    simulador.PrevisaoEntrega = PrevisaoEntregaHiddenField.Value;
                }
            }

            simulador.Desconto = Convert.ToDecimal(DescontoTextBox.Text == "" ? "0" : DescontoTextBox.Text);

            simulador.ValorComDesconto = Convert.ToDecimal(ValorComDescontoTextBox.Text == "" ? "0" : ValorComDescontoTextBox.Text.Replace("R$ ", ""));
        }

        protected void CarregaDadosDaTela_objLogistica()
        {
            objLogistica.IDPais = Convert.ToInt32(PaisDropDownList.SelectedValue);

            objLogistica.IDEstado = Convert.ToInt32(EstadoDropDownList.SelectedValue);

            objLogistica.IDMunicipio = Convert.ToInt32(MunicipioDropDownList.SelectedValue);

            objLogistica.IDTransportador = Convert.ToInt32(TransportadorDropDownList.SelectedValue);

            //objLogistica.IDRegiao
            {
                DataTable TransportadorasDataTable = objLogistica.RetornaListaTransportadorRegiaoMunicipio();

                foreach (DataRow row in TransportadorasDataTable.Rows)
                {
                    objLogistica.IDRegiao = Convert.ToInt32(row["IDRegiao"]);

                    break;
                }
            }

            //objLogistica.PesoNota
            {
                CrmProdutoClass objCrmProdutoClass = new CrmProdutoClass();

                objCrmProdutoClass.CodigoProdutoSAP = ProdutoDropDownList.SelectedValue;

                //objCrmProdutoClass.IDProduto
                {
                    DataTable ProdutoDataTable = objCrmProdutoClass.RetornaProdutoPorCodigoProdutoSAP();

                    foreach (DataRow row in ProdutoDataTable.Rows)
                    {
                        objCrmProdutoClass.IDProduto = Convert.ToInt32(row["IDProduto"]);

                        break;
                    }
                }

                decimal quantidade = objCrmProdutoClass.RetornaQuantidadeConvertida(Convert.ToDecimal(QuantidadeTextBox.Text));

                decimal fatorConversao = objCrmProdutoClass.RetornaProdutoFatorConversao();

                objLogistica.PesoNota = quantidade * fatorConversao;
            }

            decimal Preco = 0;

            if (EmpresaDropDownList.SelectedValue == "1" || EmpresaDropDownList.SelectedValue == "2")
                Preco = Convert.ToDecimal(ICMSTextBox.Text);
            else
                Preco = Convert.ToDecimal(PrecoInputTextBox.Text);

            objLogistica.ValorNota = Convert.ToDecimal(QuantidadeTextBox.Text) * Preco;
        }

        protected void SimularButton_Click(object sender, EventArgs e)
        {
            //Tornando aviso invisivel para não mistura-lo com operações passadas
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

            string erro = CarregaDadosDaTela("SimularButton_Click");

            if (erro == "" && sender != null && e != null) erro = CalcularDesconto(null, null);

            if (erro == "")
            {
                if (ValorFreteHiddenField.Value != ""
                    || FreteDropDownList.SelectedItem.Text == "FOB"
                    || EmpresaDropDownList.SelectedValue == "3")
                {
                    {
                        string ValorComDescontoText = ValorComDescontoTextBox.Text.Replace("R$", "");

                        decimal ValorComDesconto = Convert.ToDecimal(ValorComDescontoText);

                        simulador.ICMS = ValorComDesconto;

                        simulador.ValorICMS = ValorComDesconto;
                    }

                    SimulacaoGridView.DataSource = simulador.SimulacaoVendedor();
                    SimulacaoGridView.DataBind();
                    SimuladorMultiView.Visible = true;
                }
            }
            else
            {
                ApresentaMensagemErro(erro);

                SimuladorMultiView.Visible = false;
            }
        }

        protected void MudaInputs()
        {
            //Tornando aviso invisivel para não mistura-lo com operações passadas
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

            ICMSTextBox.Text = null;
            PrecoInputTextBox.Text = null;

            if (FaturamentoDropDownList.SelectedValue == "Curitiba - EX-ICM" || EmpresaDropDownList.SelectedValue == "1" || EmpresaDropDownList.SelectedValue == "2")
            {
                PrecoInputTextBox.Enabled = false;
                ICMSTextBox.Enabled = true;
            }
            else
            {
                PrecoInputTextBox.Enabled = true;
                ICMSTextBox.Enabled = false;
            }
        }

        protected void SimulacaoGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            SimulacaoGridView.PageIndex = e.NewPageIndex;
            SimularButton_Click(null, null);
        }

        protected void AnaliseButton_Click(object sender, EventArgs e)
        {
            SalvaSimulacao();
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmListaSimuladorVendedorForm.aspx?indmnu=3");
        }

        protected void PlusButton_Click(object sender, EventArgs e)
        {
            //Salvando valores em um objeto para mantê-los no retorno a pagina
            CarregaDadosDaTela("PlusButton_Click");

            //Salvando objeto em uma session
            Session["ClienteSim"] = ClienteInput.Text.ToString();
            Session["ObjSimulacao"] = simulador;

            Response.Redirect("ListaClienteSimuladorVendedorForm.aspx?indmnu=3");
        }

        protected void NovoClienteCheck_CheckedChanged(object sender, EventArgs e)
        {
            //Deixando o campo cliente editavel ou não-editavel dependendo da marcação da caixa "Novo Cliente"
            if (NovoClienteCheck.Checked == true)
            {
                Session["ClienteSim"] = null;
                ClienteInput.ReadOnly = false;
                ClassificacaoComercialDropDownList.Enabled = true;
                ClassificacaoComercialDropDownList.CssClass = "form-control fstdropdown-select";
            }
            else
            {
                if (Session["ClienteSim"] == null)
                    ClienteInput.Text = "";

                ClienteInput.ReadOnly = true;
            }
        }

        protected void CopiaLinkButton_Click(object sender, EventArgs e)
        {
            //Ativando os campos 
            ClienteInput.Enabled = true;
            EmpresaDropDownList.Enabled = true;
            EmpresaDropDownList.CssClass = "form-control fstdropdown-select";
            VendedorDropDownList.Enabled = true;
            VendedorDropDownList.CssClass = "form-control fstdropdown-select";
            ProdutoDropDownList.Enabled = true;
            ProdutoDropDownList.CssClass = "form-control fstdropdown-select";
            QuantidadeTextBox.Enabled = true;
            FaturamentoDropDownList.Enabled = true;
            FaturamentoDropDownList.CssClass = "form-control fstdropdown-select";
            ObservBox.Enabled = true;
            ObservBox.Text = "";
            ICMSTextBox.Enabled = true;
            ICMSTextBox.Text = "";
            PrecoInputTextBox.Enabled = true;
            PrecoInputTextBox.Text = "";
            NovoClienteCheck.Enabled = true;
            AnaliseButton.Visible = false;
            AnaliseButton.Enabled = false;
            SimularButton.Visible = true;
            SimularButton.Enabled = true;
            CopiaLinkButton.Visible = false;
            SalvaSimulacaoLinkButton.Visible = true;
            PlusButton.Enabled = true;

            MudaInputs();
        }

        protected void SalvaSimulacaoButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            foreach (GridViewRow row in SimulacaoGridView.Rows)
            {
                Label AprovacaoHind = (Label)row.FindControl("AlcadaGrid");
                Label ProdutoGridHind = (Label)row.FindControl("ProdutoGrid");

                if (AprovacaoHind.Text == "Bloqueado")
                    erro = "";//"Item " + ProdutoGridHind.Text + " não liberado neste valor.";
            }

            if (erro == "" && VendedorDropDownList.SelectedValue == "")
                erro = "Escolha o Nível do vendedor";

            if (erro == "") erro = SalvaSimulacao();

            if (erro == "") erro = AprovaSimulacao();

            if (erro == "")
            {
                Session["Msg"] = "Simulação salva com sucesso";

                RetornarButton_Click(null, null);
            }
            else
            {
                ApresentaMensagemErro(erro);
            }
        }

        public string SalvaSimulacao()
        {
            string erro = "";

            //Tornando aviso invisivel para não mistura-lo com operações passadas
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

            if (erro == "")
            {
                try
                {
                    erro = CarregaDadosDaTela("SalvaSimulacao");

                    if (erro == "") erro = CalcularDesconto(null, null);

                    if (erro == "") erro = simulador.SimulaPreco();

                    if (erro == "") erro = simulador.SalvaSimulacao();
                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        public string AprovaSimulacao()
        {
            string erro = "";

            simulador.observacao = "Aprovado";

            simulador.situacao = "Aprovado";

            simulador.usucod = Session["usuario"].ToString();

            string retorno = simulador.Atualiza_Simulacao();

            if (retorno == "sucesso")
            {
                Session["Msg"] = "Simulação " + simulador.IdSimulacao.ToString() + " aprovada com sucesso.";

                Session["SimControl"] = null;
            }
            else
            {
                ApresentaMensagemErro("Ocorreu um erro ao atualizar a simulação");
            }

            return erro;
        }

        protected void FreteDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FreteDropDownList.SelectedItem.Text == "FOB")
                BloqueiaCamposFrete();
            else if (EmpresaDropDownList.SelectedValue != "3")
                LiberaCamposFrete();
        }

        protected void LiberaCamposFrete()
        {
            MunicipioDropDownList.CssClass = "form-control fstdropdown-select";
            TransportadorDropDownList.CssClass = "form-control fstdropdown-select";

            MunicipioDropDownList.Enabled = true;

            TransportadorDropDownList.Enabled = true;

            //CalcularFreteLinkButton.Enabled = true;
        }

        protected void BloqueiaCamposFrete()
        {
            MunicipioDropDownList.CssClass = "form-control";
            TransportadorDropDownList.CssClass = "form-control";

            MunicipioDropDownList.Enabled = false;

            TransportadorDropDownList.Enabled = false;

            //CalcularFreteLinkButton.Enabled = false;

            ValorFreteHiddenField.Value = "";

            PrevisaoEntregaHiddenField.Value = "";
        }

        protected void ApresentaMenssagemSucesso(string sucesso)
        {
            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(sucesso, true);
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
        }

        protected void ApresentaMensagemErro(string erro)
        {
            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
        }

        protected void CalcularFreteLinkButton_Click(object sender, EventArgs e)
        {
            CalcularFrete(sender, e);
        }

        protected string CalcularFrete(object sender, EventArgs e)
        {
            string erro = CarregaDadosDaTela("CalcularFreteLinkButton_Click");

            if (erro == "")
            {
                objLogistica.SimulaFrete();

                if (!string.IsNullOrEmpty(objLogistica.ValorFrete))
                    ValorFreteHiddenField.Value = Convert.ToDecimal(objLogistica.ValorFrete).ToString("C", CultureInfo.GetCultureInfo("pt-BR"));

                if (objLogistica.PrevisaoEntrega != "")
                    PrevisaoEntregaHiddenField.Value = objLogistica.PrevisaoEntrega;
            }
            else if (sender != null && e != null)
            {
                ApresentaMensagemErro(erro);
            }

            return erro;
        }

        protected void CalcularDescontoLinkButton_Click(object sender, EventArgs e)
        {
            CalcularDesconto(sender, e);
        }

        protected string CalcularDesconto(object sender, EventArgs e)
        {
            string erro = "";

            decimal ValorFrete = 0, Quantidade = 0, FretePorUnidade = 0, Desconto = 0, ValorItem = 0, TotalComDesconto = 0;

            if (erro == "")
            {
                try
                {
                    Quantidade = Convert.ToDecimal(QuantidadeTextBox.Text == "" ? "0" : QuantidadeTextBox.Text);

                    Desconto = Convert.ToDecimal(DescontoTextBox.Text == "" ? "0" : DescontoTextBox.Text);

                    if (FaturamentoDropDownList.SelectedValue == "Curitiba - EX-ICM"
                        || EmpresaDropDownList.SelectedValue == "1"
                        || EmpresaDropDownList.SelectedValue == "2")
                    {
                        ValorItemTextBox.Text = ICMSTextBox.Text;

                        ValorItem = Convert.ToDecimal(ValorItemTextBox.Text == "" ? "0" : ValorItemTextBox.Text);

                        if (FreteDropDownList.SelectedItem.Text == "CIF")
                        {
                            erro = CalcularFrete(null, null);

                            ValorFrete = Convert.ToDecimal(ValorFreteHiddenField.Value == "" ? "0" : ValorFreteHiddenField.Value.Replace("R$ ", ""));

                            FretePorUnidade = ValorFrete / Quantidade;

                            TotalComDesconto = ValorItem - ((ValorItem - FretePorUnidade) * (Desconto / 100));
                        }
                        else if (FreteDropDownList.SelectedItem.Text == "FOB")
                        {
                            TotalComDesconto = ValorItem - (ValorItem * (Desconto / 100));
                        }
                    }
                    else
                    {
                        ValorItemTextBox.Text = PrecoInputTextBox.Text;

                        ValorItem = Convert.ToDecimal(ValorItemTextBox.Text == "" ? "0" : ValorItemTextBox.Text);

                        TotalComDesconto = ValorItem - (ValorItem * (Desconto / 100));
                    }

                    DescontoTextBox.Text = Desconto.ToString();

                    ValorComDescontoTextBox.Text = Convert.ToDecimal(TotalComDesconto).ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
                }
                catch (Exception ex)
                {
                    erro = "Erro ao calcular desconto: " + ex.Message;
                }
            }
            else if (sender != null && e != null)
            {
                ApresentaMensagemErro(erro);
            }

            return erro;
        }
    }
}