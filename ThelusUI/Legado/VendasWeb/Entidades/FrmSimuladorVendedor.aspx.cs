using System;
using System.Data;
using System.Web.UI.WebControls;
using VendasWeb.classes;

namespace VendasWeb.GerencialVendas
{
    public partial class FrmSimuladorVendedor : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        funcoes mdlfuncoes = new funcoes();
        SimuladorClass simulador = new SimuladorClass();
        SimuladorClass AcessoSim = new SimuladorClass();
        ClienteClasse objClienteClasse = new ClienteClasse();

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

                //Desabilita Combo Tipo Vendedor
                VendedorDropDown.Enabled = false;
                if (simulador.TipoVendedor != "" && simulador != null && simulador.TipoVendedor != "Todos")
                {
                    VendedorDropDown.SelectedValue = simulador.TipoVendedor;
                }
                else
                {
                    if (simulador.TipoVendedor == "Todos")
                    {
                        VendedorDropDown.Enabled = true;
                    }
                }

                CarregaFreteNovaRegra();

                ClassificacaoComercialDropDownList.DataSource = objClienteClasse.CarregaClassificacaoComercial();
                ClassificacaoComercialDropDownList.DataTextField = "Descricao";
                ClassificacaoComercialDropDownList.DataValueField = "IDClassificacaoComercial";
                ClassificacaoComercialDropDownList.DataBind();

                EstadoDropDown.DataSource = mdlfuncoes.Consulta_Estado();
                EstadoDropDown.DataTextField = "UfNome";
                EstadoDropDown.DataValueField = "UfSigla";
                EstadoDropDown.DataBind();

                /*Estado será bloqueado na entrada e somente liberado para cliente novo*/
                EstadoDropDown.Enabled = false;

                /*
                EmpresaDropDown.Items.Insert(1,"1 - MANULI CTBA");
                EmpresaDropDown.Items.Insert("2","1.3 - MANULI SP");
                EmpresaDropDown.Items.Add("2 - MANULI AM 06300");
                */
                simulador.codempresa = "1";


                FaturamentoDropDown.DataSource = simulador.Consulta_Local();
                FaturamentoDropDown.DataTextField = "LocalFaturamento";
                FaturamentoDropDown.DataBind();
                MudaInputs();

                DataTable data = new DataTable();
                data = simulador.Consulta_Produto();
                data.Columns.Add("CodNome", typeof(string), "CodigoProduto + ' - ' + NomeProduto");
                ProdutoSelect.DataSource = data;
                ProdutoSelect.DataTextField = ("CodNome");
                ProdutoSelect.DataValueField = "CodigoProduto";
                ProdutoSelect.DataBind();

                bool classificacaoCarregada = false;

                if (Session["AcessoSim"] != null)
                {
                    AcessoSim = (SimuladorClass)Session["AcessoSim"];

                    EmpresaDropDown.SelectedValue = AcessoSim.empresa;
                    simulador.codempresa = AcessoSim.empresa;

                    EmpresaDropDown_SelectedIndexChanged(null, null);

                    //Recerrega o LocalFaturamento
                    FaturamentoDropDown.Items.Clear();
                    FaturamentoDropDown.DataSource = simulador.Consulta_Local();
                    FaturamentoDropDown.DataTextField = "LocalFaturamento";
                    FaturamentoDropDown.DataBind();

                    VendedorDropDown.SelectedValue = AcessoSim.NivelVendedor;
                    EstadoDropDown.SelectedValue = AcessoSim.estado;
                    //ProdutoSelect.Items.FindByValue(AcessoSim.produto).Selected=true;
                    ProdutoSelect.Items[ProdutoSelect.SelectedIndex].Value = AcessoSim.produto;
                    ProdutoSelect.Items[ProdutoSelect.SelectedIndex].Text = AcessoSim.produtoNome;
                    //AcessoSim.quantidade = Math.Round(AcessoSim.quantidade, 2);
                    QuantidadeText.Value = Convert.ToString(AcessoSim.quantidade);
                    FaturamentoDropDown.SelectedValue = AcessoSim.LocalFaturamento;
                    ObservBox.Text = AcessoSim.observacao;
                    ClienteInput.Text = AcessoSim.NomeCliente;

                    if (AcessoSim.empresa == "3")
                    {
                        //AcessoSim.ValorICMS = Math.Round(AcessoSim.ValorICMS, 2);
                        PrecoInput.Value = AcessoSim.ValorICMS.ToString();
                    }
                    else
                    {
                        //AcessoSim.ValorICMS = Math.Round(AcessoSim.ValorICMS, 2);
                        TextICMS.Value = AcessoSim.ValorICMS.ToString();
                    }

                    //Seta classificação comercial caso seja alteração e não inclusão
                    {
                        ClassificacaoComercialDropDownList.SelectedValue = AcessoSim.IDClassificacaoComercial.ToString();

                        if (ClassificacaoComercialDropDownList.SelectedValue != null)
                            classificacaoCarregada = true;
                    }

                    FreteDropDownList.SelectedValue = AcessoSim.IDTipoFrete.ToString();
                    AvistaCheckBox.Checked = Convert.ToBoolean(AcessoSim.AVista);

                    //Simulando um click no botão simulação
                    SimularButton_Click(null, null);

                    //Desativando os campos 
                    ClienteInput.Enabled = false;
                    EmpresaDropDown.Enabled = false;
                    VendedorDropDown.Enabled = false;
                    EstadoDropDown.Enabled = false;
                    ProdutoSelect.Disabled = true;
                    QuantidadeText.Disabled = true;
                    FaturamentoDropDown.Enabled = false;
                    ObservBox.Enabled = false;
                    TextICMS.Disabled = true;
                    PrecoInput.Disabled = true;
                    ClienteCheck.Enabled = false;
                    AnaliseButton.Visible = false;
                    AnaliseButton.Enabled = false;
                    SimularButton.Visible = false;
                    SimularButton.Enabled = false;
                    PlusButton.Enabled = false;
                    AprovarLinkButton.Visible = false;
                    CopiaLinkButton.Visible = true;
                    FreteDropDownList.Enabled = false;
                    AvistaCheckBox.Enabled = false;

                    Session["AcessoSim"] = null;
                }

                if (Session["ObjSimulacao"] != null)
                {
                    simulador = (SimuladorClass)Session["ObjSimulacao"];

                    EmpresaDropDown.SelectedValue = simulador.empresa;

                    EmpresaDropDown_SelectedIndexChanged(null, null);

                    //Recerrega o LocalFaturamento
                    FaturamentoDropDown.Items.Clear();
                    FaturamentoDropDown.DataSource = simulador.Consulta_Local();
                    FaturamentoDropDown.DataTextField = "LocalFaturamento";
                    FaturamentoDropDown.DataBind();

                    VendedorDropDown.SelectedIndex = Convert.ToInt32(simulador.NivelVendedor);
                    EstadoDropDown.SelectedIndex = Convert.ToInt32(simulador.estado);
                    ProdutoSelect.SelectedIndex = Convert.ToInt32(simulador.produto);
                    QuantidadeText.Value = Convert.ToString(simulador.quantidade);
                    FaturamentoDropDown.SelectedIndex = Convert.ToInt32(simulador.LocalFaturamento);
                    ObservBox.Text = simulador.observacao;

                    //Descobrindo se a pagina foi aberta retornando da lista de clientes ou não
                    if (Session["ClienteSim"] != null)
                    {
                        ClienteInput.Text = (string)Session["ClienteSim"];
                        EstadoDropDown.SelectedValue = (string)Session["UfSigla"];
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
                                        break;
                                    else
                                        ClassificacaoComercialDropDownList.Enabled = false;
                                }
                            }
                        }
                    }

                    MudaInputs();
                    if (PrecoInput.Disabled == true)
                    {
                        TextICMS.Value = simulador.ICMS.ToString();
                    }
                    else
                    {
                        PrecoInput.Value = simulador.ICMS.ToString();
                    }

                    Session["ObjSimulacao"] = null;
                }

                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                //this.ControlPainel.refreshVendedor();

            }


        }

        protected void EmpresaDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Limpar campos 
            SimuladorMultiView.Visible = false;
            VendedorDropDown.SelectedIndex = 0;
            EstadoDropDown.SelectedIndex = 0;
            ClienteInput.Text = "";
            ObservBox.Text = string.Empty;

            /*
            if (EmpresaDropDown.SelectedValue == "1 - MANULI CTBA")
             {
                 simulador.codempresa = "1";
             }

             if (EmpresaDropDown.SelectedValue == "1.3 - MANULI SP")
             {
                 simulador.codempresa = "2";
             }

             if (EmpresaDropDown.SelectedValue == "2 - MANULI AM")
             {
                 simulador.codempresa = "3";
             }
             */

            simulador.codempresa = EmpresaDropDown.SelectedValue.ToString();

            DataTable data = new DataTable();
            data = simulador.Consulta_Produto();
            data.Columns.Add("CodNome", typeof(string), "CodigoProduto + ' - ' + NomeProduto");
            ProdutoSelect.DataSource = data;
            ProdutoSelect.DataTextField = ("CodNome");
            ProdutoSelect.DataValueField = "CodigoProduto";
            ProdutoSelect.DataBind();

            FaturamentoDropDown.DataSource = simulador.Consulta_Local();
            FaturamentoDropDown.DataTextField = "LocalFaturamento";
            FaturamentoDropDown.DataBind();

            MudaInputs();

            simulador.CodigoUsuario = Session["usuario"].ToString();
            simulador.RecuperaNivelVendedor();

            //Desabilita Combo Tipo Vendedor
            VendedorDropDown.Enabled = false;
            if (simulador.TipoVendedor != "" && simulador != null && simulador.TipoVendedor != "Todos")
            {
                VendedorDropDown.SelectedValue = simulador.TipoVendedor;
            }
            else
            {
                if (simulador.TipoVendedor == "Todos")
                {
                    VendedorDropDown.Enabled = true;
                }
            }

            FretesClass objFretesClass = new FretesClass();

            objFretesClass.empcod = EmpresaDropDown.SelectedValue;

            FreteDropDownList.DataSource = objFretesClass.CarregaFreteIncoterms();
            FreteDropDownList.DataTextField = "Descricao";
            FreteDropDownList.DataValueField = "IDTipoFrete";
            FreteDropDownList.DataBind();
        }

        protected void SimularButton_Click(object sender, EventArgs e)
        {
            //Tornando aviso invisivel para não mistura-lo com operações passadas
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;
            string erro = "";

            simulador.codempresa = EmpresaDropDown.SelectedValue.ToString();
            simulador.Arredonda_codempresa();
            simulador.estado = EstadoDropDown.SelectedValue;
            simulador.produto = ProdutoSelect.Value;
            simulador.LocalFaturamento = FaturamentoDropDown.SelectedValue;
            simulador.NivelVendedor = VendedorDropDown.SelectedValue;

            if (VendedorDropDown.SelectedValue == "")
            {
                erro = "Esolher Tipo Vendedor.";
                string FaltaValores = "Necessário escolher o Nível de vendedor.";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }

            if (erro == "" && ClassificacaoComercialDropDownList.SelectedValue == "0")
            {
                erro = "Escolha a classificação comercial.";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }

            if (erro == "" && FreteDropDownList.SelectedValue == "0")
            {
                erro = "Escolha o tipo de frete.";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }

            if (erro == "")
            {
                {
                    simulador.IDClassificacaoComercial = Convert.ToInt32(ClassificacaoComercialDropDownList.SelectedValue);
                    simulador.IDTipoFrete = Convert.ToInt32(FreteDropDownList.SelectedValue);

                    if (AvistaCheckBox.Checked)
                        simulador.AVista = 1;
                    else
                        simulador.AVista = 0;
                }

                DataTable outpout = new DataTable();
                if (EmpresaDropDown.SelectedValue == "1")
                {
                    simulador.codempresa = "1";
                    if (TextICMS.Value != "")

                    {
                        simulador.ICMS = Convert.ToDecimal(TextICMS.Value);
                        simulador.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
                        outpout = new DataTable();
                        outpout = simulador.SimulacaoVendedor();
                        SimulacaoGridView.DataSource = outpout;
                        SimulacaoGridView.DataBind();
                        SimuladorMultiView.Visible = true;
                    }
                    else
                    {
                        string FaltaValores = "Por favor, preencha o campo Ex-ICMS";
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                    }
                }

                if (EmpresaDropDown.SelectedValue == "2")
                {
                    simulador.codempresa = "2";

                    if (TextICMS.Value != "")
                    {
                        simulador.ICMS = Convert.ToDecimal(TextICMS.Value);
                        simulador.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
                        outpout = new DataTable();
                        outpout = simulador.SimulacaoVendedor();
                        SimulacaoGridView.DataSource = outpout;
                        SimulacaoGridView.DataBind();
                        SimuladorMultiView.Visible = true;
                    }

                    else
                    {
                        string FaltaValores = "Por favor, preencha o campo Ex-ICMS";
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                    }
                }

                if (EmpresaDropDown.SelectedValue == "3")
                {
                    simulador.codempresa = "3";
                    if (PrecoInput.Value != "")
                    {
                        simulador.ICMS = Convert.ToDecimal(PrecoInput.Value);
                        simulador.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
                        outpout = new DataTable();
                        outpout = simulador.SimulacaoVendedor();
                        SimulacaoGridView.DataSource = outpout;
                        SimulacaoGridView.DataBind();
                        SimuladorMultiView.Visible = true;
                    }
                    else
                    {
                        string FaltaValores = "Por favor, preencha o campo preço final";
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                    }
                }
            }
        }

        protected void MudaInputs()
        {
            //Tornando aviso invisivel para não mistura-lo com operações passadas
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

            TextICMS.Value = null;
            PrecoInput.Value = null;

            if (FaturamentoDropDown.SelectedValue == "Curitiba - EX-ICM" || EmpresaDropDown.SelectedValue == "1" || EmpresaDropDown.SelectedValue == "2")
            {
                PrecoInput.Disabled = true;
                TextICMS.Disabled = false;
            }

            else
            {
                PrecoInput.Disabled = false;
                TextICMS.Disabled = true;
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
            simulador.empresa = EmpresaDropDown.SelectedValue.ToString();
            simulador.estado = EstadoDropDown.SelectedIndex.ToString();
            simulador.produto = ProdutoSelect.SelectedIndex.ToString();
            if (QuantidadeText.Value != "")
            {
                simulador.quantidade = Convert.ToDecimal(QuantidadeText.Value);
            }
            simulador.NivelVendedor = VendedorDropDown.SelectedIndex.ToString();
            simulador.LocalFaturamento = FaturamentoDropDown.SelectedIndex.ToString();
            if (ObservBox.Text != "")
            {
                simulador.observacao = ObservBox.Text;
            }

            if (PrecoInput.Value != "" || TextICMS.Value != "")
            {
                if (PrecoInput.Disabled == true)
                {
                    simulador.ICMS = Convert.ToDecimal(TextICMS.Value);
                }
                else
                {
                    simulador.ICMS = Convert.ToDecimal(PrecoInput.Value);
                }
            }
            //Salvando objeto em uma session
            Session["ClienteSim"] = ClienteInput.Text.ToString();
            Session["ObjSimulacao"] = simulador;

            Response.Redirect("ListaClienteSimuladorVendedorForm.aspx?indmnu=3");
        }

        protected void ClienteCheck_CheckedChanged(object sender, EventArgs e)
        {
            //Deixando o campo cliente editavel ou não-editavel dependendo da marcação da caixa "Novo Cliente"
            if (ClienteCheck.Checked == true)
            {
                Session["ClienteSim"] = null;
                ClienteInput.ReadOnly = false;
                EstadoDropDown.Enabled = true;
                ClassificacaoComercialDropDownList.Enabled = true;
            }
            else
            {
                if (Session["ClienteSim"] == null)
                {
                    ClienteInput.Text = "";
                }
                ClienteInput.ReadOnly = true;
                EstadoDropDown.Enabled = false;
            }
        }

        protected void CopiaLinkButton_Click(object sender, EventArgs e)
        {
            //Ativando os campos 
            ClienteInput.Enabled = true;
            EmpresaDropDown.Enabled = true;
            VendedorDropDown.Enabled = true;
            EstadoDropDown.Enabled = false;
            ProdutoSelect.Disabled = false;
            QuantidadeText.Disabled = false;
            FaturamentoDropDown.Enabled = true;
            ObservBox.Enabled = true;
            ObservBox.Text = "";
            TextICMS.Disabled = false;
            TextICMS.Value = "";
            PrecoInput.Disabled = false;
            PrecoInput.Value = "";
            ClienteCheck.Enabled = true;
            AnaliseButton.Visible = false;
            AnaliseButton.Enabled = false;
            SimularButton.Visible = true;
            SimularButton.Enabled = true;
            CopiaLinkButton.Visible = false;
            AprovarLinkButton.Visible = true;
            PlusButton.Enabled = true;

            MudaInputs();
        }

        protected void AprovarButton_Click(object sender, EventArgs e)
        {
            string erro = "";

            foreach (GridViewRow row in SimulacaoGridView.Rows)
            {
                Label AprovacaoHind = (Label)row.FindControl("AlcadaGrid");
                Label ProdutoGridHind = (Label)row.FindControl("ProdutoGrid");

                if (AprovacaoHind.Text == "Bloqueado")
                {
                    erro = "Item Bloqueado.";
                    string FaltaValores = "Item " + ProdutoGridHind.Text + " não liberado neste valor.";
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                    ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                }
            }

            if (VendedorDropDown.SelectedValue == "")
            {
                erro = "Esolher Tipo Vendedor.";
                string FaltaValores = "Necessário escolher o Nível de vendedor.";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }

            if (erro == "")
            {
                erro = SalvaSimulacao();
            }

            if (erro == "")
            {
                erro = AprovaSimulacao();
            }

            //Se não deu nenhum erro retorna para a lista
            if (erro == "")
            {
                Response.Redirect("FrmListaSimuladorVendedorForm.aspx?indmnu=3");
            }
        }

        public string SalvaSimulacao()
        {
            string erro = "";
            simulador.codempresa = EmpresaDropDown.SelectedValue.ToString();

            //Tornando aviso invisivel para não mistura-lo com operações passadas
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

            bool validacao = false;

            if (erro == "" && ClassificacaoComercialDropDownList.SelectedValue == "0")
            {
                erro = "Escolha a classificação comercial.";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }

            if (erro == "" && FreteDropDownList.SelectedValue == "0")
            {
                erro = "Escolha o tipo de frete.";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }

            if (erro == "")
            {
                {
                    simulador.IDClassificacaoComercial = Convert.ToInt32(ClassificacaoComercialDropDownList.SelectedValue);
                    simulador.IDTipoFrete = Convert.ToInt32(FreteDropDownList.SelectedValue);

                    if (AvistaCheckBox.Checked)
                        simulador.AVista = 1;
                    else
                        simulador.AVista = 0;
                }

                if (TextICMS.Value != "" || PrecoInput.Value != "")
                {
                    validacao = true;
                }

                if (validacao)
                {
                    if (ClienteInput.Text != "")
                    {
                        if (QuantidadeText.Value != "" && QuantidadeText.Value != "0")
                        {
                            try
                            {

                                if (EmpresaDropDown.SelectedValue == "1")
                                {
                                    simulador.codempresa = "1";
                                }

                                if (EmpresaDropDown.SelectedValue == "2")
                                {
                                    simulador.codempresa = "2";
                                }

                                if (EmpresaDropDown.SelectedValue == "3")
                                {
                                    simulador.codempresa = "3";
                                }

                                simulador.Arredonda_codempresa();
                                simulador.estado = EstadoDropDown.SelectedValue;
                                simulador.produto = ProdutoSelect.Value;
                                simulador.LocalFaturamento = FaturamentoDropDown.SelectedValue;
                                simulador.NivelVendedor = VendedorDropDown.SelectedValue;
                                simulador.quantidade = Convert.ToDecimal(QuantidadeText.Value);
                                simulador.usucod = Session["usuario"].ToString();

                                simulador.NomeCliente = ClienteInput.Text;
                                simulador.observacao = ObservBox.Text;


                                if (PrecoInput.Value == "")
                                {
                                    simulador.ICMS = Convert.ToDecimal(TextICMS.Value);
                                }
                                else
                                {
                                    simulador.ICMS = Convert.ToDecimal(PrecoInput.Value);
                                }

                                if (!ClienteCheck.Checked)
                                {
                                    simulador.NovoCliente = "Não";
                                }
                                else
                                {
                                    simulador.NovoCliente = "Sim";
                                }

                                simulador.PreparaEmail();
                                simulador.EnviaEmail();

                                //Session["Msg"] = "Simulação " + simulador.NumeroSimulacao.ToString() + " gravada com sucesso.";
                                //Response.Redirect("FrmListaSimuladorForm.aspx?indmnu=3");
                                //string FaltaValores = "Simulação " + simulador.NumeroSimulacao.ToString() + " gravada com sucesso." ;
                                //((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(FaltaValores, true);
                                //((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                                //((Label)Master.FindControl("MenssagemMasterLabel")).Focus();


                            }
                            catch (Exception ex)
                            {
                                erro = ex.ToString();
                            }
                        }
                        else
                        {
                            erro = "erro quantidade.";
                            string FaltaValores = "Por favor, preencher a quantidade.";
                            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
                            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                            ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                        }
                    }
                    else
                    {
                        erro = "erro cliente.";
                        string FaltaValores = "Por favor, preencha o campo Cliente";
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                    }
                }
                else
                {
                    erro = "erro valor.";
                    if (EmpresaDropDown.SelectedValue == "3")
                    {
                        string FaltaValores = "Por favor, preencha o campo preço final";
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                    }
                    else
                    {
                        string FaltaValores = "Por favor, preencha o campo Ex-ICMS";
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                        ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                    }

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
                //Response.Redirect("ListaSimuladorControladoria.aspx?indmnu=3");
                //string sucesso = "Simulação aprovada com sucesso.";
                //((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(sucesso, true);
                //((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                //((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                //Impedindo novas edições na simulação
                //AprovarButton.Visible = false;
                //ReprovarButton.Visible = false;
                //NovoHistBox.Visible = false;
                //NovoHistoricoLabel.Visible = false;
                /*
                DivHist.Attributes.Add("Class", "col-sm-12");

                DateTime now = DateTime.Now;
                if (ObservBox.Text != "")
                {
                    ObservBox.Text += "\n\n" + now + ": " + Session["usuario"].ToString() + "\n" + NovoHistBox.Text;
                }

                else
                {
                    ObservBox.Text = now + ": " + Session["usuario"].ToString() + "\n" + NovoHistBox.Text;
                }
                */
            }
            else
            {
                erro = "Ocorreu um erro ao atualizar a simulação";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }


            return erro;
        }

        protected void EstadoDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFreteNovaRegra();
        }

        protected void CarregaFreteNovaRegra()
        {
            FretesClass objFretesClass = new FretesClass();

            DataTable fretes = objFretesClass.CarregaFreteIncoterms();

            objFretesClass.empcod = EmpresaDropDown.SelectedValue;

            //Para os estados RS, SC, PR, SP manter CIF e FOB, para o restante somente FOB
            if (!(EstadoDropDown.SelectedValue == "RS"
               || EstadoDropDown.SelectedValue == "SC"
               || EstadoDropDown.SelectedValue == "PR"
               || EstadoDropDown.SelectedValue == "SP"))
            {
                //tira o CIF do datatable
                DataRow[] rowsToDelete = fretes.Select("Descricao = 'CIF'");

                foreach (DataRow row in rowsToDelete)
                {
                    fretes.Rows.Remove(row);
                }
            }

            FreteDropDownList.DataSource = fretes;
            FreteDropDownList.DataTextField = "Descricao";
            FreteDropDownList.DataValueField = "IDTipoFrete";
            FreteDropDownList.DataBind();
        }
    }
}