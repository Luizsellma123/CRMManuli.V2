using System;
using System.Data;
using System.Web.UI.WebControls;
using VendasWeb.classes;

namespace VendasWeb.GerencialVendas
{
    public partial class SimuladorConsultaControladoriaWebForm : System.Web.UI.Page
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
                //CopiaLinkButton.Visible = false;

                VendedorDropDown.Items.Add("Vendedor");
                VendedorDropDown.Items.Add("Representante");

                EstadoDropDown.DataSource = mdlfuncoes.Consulta_Estado();
                EstadoDropDown.DataTextField = "UfNome";
                EstadoDropDown.DataValueField = "UfSigla";
                EstadoDropDown.DataBind();

                CarregaFreteNovaRegra();

                ClassificacaoComercialDropDownList.DataSource = objClienteClasse.CarregaClassificacaoComercial();
                ClassificacaoComercialDropDownList.DataTextField = "Descricao";
                ClassificacaoComercialDropDownList.DataValueField = "IDClassificacaoComercial";
                ClassificacaoComercialDropDownList.DataBind();

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
                    //ObservBox.Text = AcessoSim.observacao;
                    //ClienteInput.Text = AcessoSim.NomeCliente;

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

                    ClassificacaoComercialDropDownList.SelectedValue = AcessoSim.IDClassificacaoComercial.ToString();
                    FreteDropDownList.SelectedValue = AcessoSim.IDTipoFrete.ToString();
                    AvistaCheckBox.Checked = Convert.ToBoolean(AcessoSim.AVista);

                    //Simulando um click no botão simulação
                    SimularButton_Click(null, null);

                    //Desativando os campos 
                    //ClienteInput.Enabled = false;
                    EmpresaDropDown.Enabled = false;
                    VendedorDropDown.Enabled = false;
                    EstadoDropDown.Enabled = false;
                    ProdutoSelect.Disabled = true;
                    QuantidadeText.Disabled = true;
                    FaturamentoDropDown.Enabled = false;
                    //ObservBox.Enabled = false;
                    TextICMS.Disabled = true;
                    PrecoInput.Disabled = true;
                    //ClienteCheck.Enabled = false;
                    //AnaliseButton.Visible = false;
                    //AnaliseButton.Enabled = false;
                    SimularButton.Visible = false;
                    SimularButton.Enabled = false;
                    //PlusButton.Enabled = false;
                    //CopiaLinkButton.Visible = true;

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
                    //ObservBox.Text = simulador.observacao;

                    //Descobrindo se a pagina foi aberta retornando da lista de clientes ou não
                    if (Session["ClienteSim"] != null)
                    {
                        //ClienteInput.Text = (string)Session["ClienteSim"];
                        Session["ClienteSim"] = null;
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

                this.ControlPainel.refreshVendedor();

            }


        }

        protected void EmpresaDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Limpar campos 
            SimuladorMultiView.Visible = false;
            VendedorDropDown.SelectedIndex = 0;
            EstadoDropDown.SelectedIndex = 0;
            //ClienteInput.Text = "";
            //ObservBox.Text = string.Empty;

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
                        outpout = new DataTable();
                        simulador.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
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
                        outpout = new DataTable();
                        simulador.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
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
                        outpout = new DataTable();
                        simulador.IDUsuario = Convert.ToInt32(Session["IDUsuario"]);
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

        //protected void AnaliseButton_Click(object sender, EventArgs e)
        //{
        //    simulador.codempresa = EmpresaDropDown.SelectedValue.ToString();

        //    //Tornando aviso invisivel para não mistura-lo com operações passadas
        //    ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

        //    bool validacao = false;
        //    if (TextICMS.Value != "" || PrecoInput.Value != "")
        //    {
        //        validacao = true;
        //    }

        //    if (validacao==true)
        //    {
        //        if (ClienteInput.Text != "")
        //        {
        //            try
        //            {

        //                if (EmpresaDropDown.SelectedValue == "1")
        //                {
        //                    simulador.codempresa = "1";
        //                }

        //                if (EmpresaDropDown.SelectedValue == "2")
        //                {
        //                    simulador.codempresa = "2";
        //                }

        //                if (EmpresaDropDown.SelectedValue == "3")
        //                {
        //                    simulador.codempresa = "3";
        //                }

        //                simulador.Arredonda_codempresa();
        //                simulador.estado = EstadoDropDown.SelectedValue;
        //                simulador.produto = ProdutoSelect.Value;
        //                simulador.LocalFaturamento = FaturamentoDropDown.SelectedValue;
        //                simulador.NivelVendedor = VendedorDropDown.SelectedValue;
        //                simulador.quantidade = Convert.ToDecimal(QuantidadeText.Value);
        //                simulador.usucod = Session["usuario"].ToString();

        //                simulador.NomeCliente = ClienteInput.Text;
        //                simulador.observacao = ObservBox.Text;


        //                if (PrecoInput.Value == "")
        //                {
        //                    simulador.ICMS = Convert.ToDecimal(TextICMS.Value);
        //                }
        //                else
        //                {
        //                    simulador.ICMS = Convert.ToDecimal(PrecoInput.Value);
        //                }

        //                if(!ClienteCheck.Checked)
        //                {
        //                    simulador.NovoCliente = "Não";
        //                }
        //                else
        //                {
        //                    simulador.NovoCliente = "Sim";
        //                }
        //                simulador.PreparaEmail();
        //                simulador.EnviaEmail();

        //                Session["Msg"] = "Simulação " + simulador.NumeroSimulacao.ToString() + " gravada com sucesso.";
        //                Response.Redirect("FrmListaSimuladorForm.aspx?indmnu=3");
        //                //string FaltaValores = "Simulação " + simulador.NumeroSimulacao.ToString() + " gravada com sucesso." ;
        //                //((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(FaltaValores, true);
        //                //((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
        //                //((Label)Master.FindControl("MenssagemMasterLabel")).Focus();


        //            }
        //            catch(Exception ex)
        //            {

        //            }
        //        }
        //        else
        //        {
        //            string FaltaValores = "Por favor, preencha o campo Cliente";
        //            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
        //            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
        //            ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
        //        }
        //    }
        //    else
        //    {
        //        if (EmpresaDropDown.SelectedValue == "3")
        //        {
        //            string FaltaValores = "Por favor, preencha o campo preço final";
        //            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
        //            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
        //            ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
        //        }
        //        else
        //        {
        //            string FaltaValores = "Por favor, preencha o campo Ex-ICMS";
        //            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(FaltaValores, true);
        //            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
        //            ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
        //        }

        //    }
        //}

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomeControladoriaWebForm.aspx?indmnu=3");
        }

        protected void EstadoDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaFreteNovaRegra();
        }

        protected void CarregaFreteNovaRegra()
        {
            FretesClass objFretesClass = new FretesClass();

            DataTable fretes = objFretesClass.CarregaFreteIncoterms();

            //Para os estados RS, SC, PR, SP manter CIF e FOB, para o restante somente FOB
            //if (!(EstadoDropDown.SelectedValue == "RS"
            //   || EstadoDropDown.SelectedValue == "SC"
            //   || EstadoDropDown.SelectedValue == "PR"
            //   || EstadoDropDown.SelectedValue == "SP"))
            //{
            //    //tira o CIF do datatable
            //    DataRow[] rowsToDelete = fretes.Select("Descricao = 'CIF'");

            //    foreach (DataRow row in rowsToDelete)
            //    {
            //        fretes.Rows.Remove(row);
            //    }
            //}

            FreteDropDownList.DataSource = fretes;
            FreteDropDownList.DataTextField = "Descricao";
            FreteDropDownList.DataValueField = "IDTipoFrete";
            FreteDropDownList.DataBind();
        }

        //protected void PlusButton_Click(object sender, EventArgs e)
        //{
        //    //Salvando valores em um objeto para mantê-los no retorno a pagina
        //    simulador.empresa = EmpresaDropDown.SelectedValue.ToString();
        //    simulador.estado = EstadoDropDown.SelectedIndex.ToString();
        //    simulador.produto = ProdutoSelect.SelectedIndex.ToString();
        //    if (QuantidadeText.Value != "")
        //    {
        //        simulador.quantidade = Convert.ToDecimal(QuantidadeText.Value);
        //    }
        //    simulador.NivelVendedor = VendedorDropDown.SelectedIndex.ToString();
        //    simulador.LocalFaturamento = FaturamentoDropDown.SelectedIndex.ToString();
        //    if (ObservBox.Text != "")
        //    {
        //        simulador.observacao = ObservBox.Text;
        //    }

        //    if (PrecoInput.Value != "" || TextICMS.Value != "")
        //    {
        //        if (PrecoInput.Disabled == true)
        //        {
        //            simulador.ICMS = Convert.ToDecimal(TextICMS.Value);
        //        }
        //        else
        //        {
        //            simulador.ICMS = Convert.ToDecimal(PrecoInput.Value);
        //        }
        //    }
        //    //Salvando objeto em uma session
        //    Session["ClienteSim"] = ClienteInput.Text.ToString();
        //    Session["ObjSimulacao"] = simulador;

        //    Response.Redirect("ListaClienteSimuladorForm.aspx?indmnu=3");
        //}

        //protected void ClienteCheck_CheckedChanged(object sender, EventArgs e)
        //{
        //    //Deixando o campo cliente editavel ou não-editavel dependendo da marcação da caixa "Novo Cliente"
        //    if (ClienteCheck.Checked == true)
        //    {
        //        Session["ClienteSim"] = null;
        //        //ClienteInput.ReadOnly = false;
        //    }
        //    else
        //    {
        //        if (Session["ClienteSim"] == null)
        //        {
        //            //ClienteInput.Text = "";
        //        }
        //        //ClienteInput.ReadOnly = true;
        //    }
        //}
    }
}