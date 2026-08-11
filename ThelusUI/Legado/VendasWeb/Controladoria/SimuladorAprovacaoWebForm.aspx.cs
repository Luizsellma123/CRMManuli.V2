using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Controladoria
{
    public partial class SimuladorAprovacaoWebForm : System.Web.UI.Page
    {
        UtilClass ObjUtilClass = new UtilClass();
        funcoes mdlfuncoes = new funcoes();
        SimuladorClass simulador = new SimuladorClass();
        SimuladorClass AcessoSim = new SimuladorClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                VendedorDropDown.Items.Add("Vendedor");
                VendedorDropDown.Items.Add("Representante");

                FretesClass objFretesClass = new FretesClass();

                FreteDropDownList.DataSource = objFretesClass.CarregaFreteIncoterms();
                FreteDropDownList.DataTextField = "Descricao";
                FreteDropDownList.DataValueField = "IDTipoFrete";
                FreteDropDownList.DataBind();

                ClienteClasse objClienteClasse = new ClienteClasse();

                ClassificacaoComercialDropDownList.DataSource = objClienteClasse.CarregaClassificacaoComercial();
                ClassificacaoComercialDropDownList.DataTextField = "Descricao";
                ClassificacaoComercialDropDownList.DataValueField = "IDClassificacaoComercial";
                ClassificacaoComercialDropDownList.DataBind();

                EstadoDropDown.DataSource = mdlfuncoes.Consulta_Estado();
                EstadoDropDown.DataTextField = "UfNome";
                EstadoDropDown.DataValueField = "UfSigla";
                EstadoDropDown.DataBind();

                //EmpresaDropDown.Items.Add("1 - MANULI CTBA");
                //EmpresaDropDown.Items.Add("1.3 - MANULI SP");
                //EmpresaDropDown.Items.Add("2 - MANULI AM 06300");
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

                if (Session["SimControl"] != null)
                {
                    AcessoSim = (SimuladorClass)Session["SimControl"];

                    EmpresaDropDown.SelectedValue = AcessoSim.empresa;

                    EmpresaDropDown_SelectedIndexChanged(null, null);

                    //Recerrega o LocalFaturamento
                    FaturamentoDropDown.Items.Clear();
                    FaturamentoDropDown.DataSource = simulador.Consulta_Local();
                    FaturamentoDropDown.DataTextField = "LocalFaturamento";
                    FaturamentoDropDown.DataBind();

                    VendedorDropDown.SelectedValue = AcessoSim.NivelVendedor;
                    EstadoDropDown.SelectedValue = AcessoSim.estado;
                    ProdutoSelect.Items[ProdutoSelect.SelectedIndex].Value = AcessoSim.produto;
                    ProdutoSelect.Items[ProdutoSelect.SelectedIndex].Text = AcessoSim.produtoNome;
                    //AcessoSim.quantidade = Math.Round(AcessoSim.quantidade, 2);
                    QuantidadeText.Value = Convert.ToString(AcessoSim.quantidade);
                    FaturamentoDropDown.SelectedValue = AcessoSim.LocalFaturamento;
                    ObservBox.Text = AcessoSim.observacao;
                    ClienteInput.Text = AcessoSim.NomeCliente;
                    MargemContribuicaoTextBox.Text = AcessoSim.MargemContribuicao.ToString();

                    if (AcessoSim.NovoCliente == "Sim")
                    {
                        ClienteCheck.Checked = true;
                    }

                    MudaInputs();
                    if (PrecoInput.Disabled == true)
                    {
                        //AcessoSim.ValorICMS = Math.Round(AcessoSim.ValorICMS, 2);
                        TextICMS.Value = AcessoSim.ValorICMS.ToString();
                    }
                    else
                    {
                        //AcessoSim.ValorICMS = Math.Round(AcessoSim.ValorICMS, 2);
                        PrecoInput.Value = AcessoSim.ValorICMS.ToString();
                    }


                    ClassificacaoComercialDropDownList.SelectedValue = AcessoSim.IDClassificacaoComercial.ToString();
                    FreteDropDownList.SelectedValue = AcessoSim.IDTipoFrete.ToString();
                    AvistaCheckBox.Checked = Convert.ToBoolean(AcessoSim.AVista);

                    //Simulando um click no botão simulação
                    Simulacao();

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

                    if (AcessoSim.situacao == "Aprovado" || AcessoSim.situacao == "Reprovado")
                    {
                        //Deixando campos bloqueados invisiveis
                        AprovarButton.Visible = false;
                        ReprovarButton.Visible = false;
                        NovoHistBox.Enabled = false;
                        NovoHistBox.Visible = false;
                        NovoHistoricoLabel.Visible = false;

                        //Fazendo o campo histórico ocupar toda a linha, agora que novo histórico ficou invisivel
                        DivHist.Attributes.Add("Class", "col-sm-12");
                    }
                }
            }
            PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

            this.ControlPainel.refreshVendedor();

        }


        protected void EmpresaDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Limpar campos 
            SimuladorMultiView.Visible = false;
            VendedorDropDown.SelectedIndex = 0;
            EstadoDropDown.SelectedIndex = 0;
            ClienteInput.Text = "";
            ObservBox.Text = string.Empty;


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


        protected void Simulacao()
        {
            //Tornando aviso invisivel para não mistura-lo com operações passadas
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;
            string erro = "";

            simulador.Arredonda_codempresa();
            simulador.estado = EstadoDropDown.SelectedValue;
            simulador.produto = ProdutoSelect.Value;
            simulador.LocalFaturamento = FaturamentoDropDown.SelectedValue;
            simulador.NivelVendedor = VendedorDropDown.SelectedValue;

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
                        outpout = simulador.Simulacao();
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
                        outpout = simulador.Simulacao();
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
                        outpout = simulador.Simulacao();
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

        protected void SimulacaoGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            SimulacaoGridView.PageIndex = e.NewPageIndex;
            Simulacao();
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Session["SimControl"] = null;
            Response.Redirect("ListaSimuladorControladoria.aspx?indmnu=3");
        }

        protected void AprovarButton_Click(object sender, EventArgs e)
        {
            simulador = (SimuladorClass)Session["SimControl"];
            simulador.observacao = NovoHistBox.Text;
            simulador.situacao = "Aprovado";
            simulador.usucod = Session["usuario"].ToString();
            string retorno = simulador.Atualiza_Simulacao();
            if (retorno == "sucesso")
            {
                Session["Msg"] = "Simulação " + simulador.IdSimulacao.ToString() + " aprovada com sucesso.";
                Session["SimControl"] = null;
                Response.Redirect("ListaSimuladorControladoria.aspx?indmnu=3");
                //string sucesso = "Simulação aprovada com sucesso.";
                //((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(sucesso, true);
                //((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                //((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                //Impedindo novas edições na simulação
                AprovarButton.Visible = false;
                ReprovarButton.Visible = false;
                NovoHistBox.Visible = false;
                NovoHistoricoLabel.Visible = false;

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
            }
            else
            {
                string erro = "Ocorreu um erro ao atualizar a simulação";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }

        protected void ReprovarButton_Click(object sender, EventArgs e)
        {
            simulador = (SimuladorClass)Session["SimControl"];
            simulador.observacao = NovoHistBox.Text;
            simulador.situacao = "Reprovado";
            simulador.usucod = Session["usuario"].ToString();
            string retorno = simulador.Atualiza_Simulacao();
            if (retorno == "sucesso")
            {
                Session["Msg"] = "Simulação " + simulador.IdSimulacao.ToString() + " reprovada com sucesso.";
                Session["SimControl"] = null;
                Response.Redirect("ListaSimuladorControladoria.aspx?indmnu=3");
                //string sucesso = "Simulação reprovada com sucesso.";
                //((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(sucesso, true);
                //((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                //((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                //Impedindo novas edições na simulação
                AprovarButton.Visible = false;
                ReprovarButton.Visible = false;
                NovoHistBox.Visible = false;
                NovoHistoricoLabel.Visible = false;

                DivHist.Attributes.Add("Class", "col-sm-12");


                DateTime now = DateTime.Now;
                if (ObservBox.Text != "")
                {
                    ObservBox.Text += "\n\n" + "Atualização: " + now + "\n" + NovoHistBox.Text;
                }

                else
                {
                    ObservBox.Text = "Atualização: " + now + "\n" + NovoHistBox.Text;
                }
            }
            else
            {
                string erro = "Ocorreu um erro ao atualizar a simulação.";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
            }
        }
    }
}