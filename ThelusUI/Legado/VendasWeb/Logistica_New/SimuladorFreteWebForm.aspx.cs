using System;
using System.Data;
using VendasWeb.classes;
using System.Globalization;
using VendasWeb.GerencialVendas;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Linq;

namespace VendasWeb.Logistica_New
{
    public partial class SimuladorFreteWebForm : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        UtilClass ObjUtilClass = new UtilClass();
        funcoes mdlfuncoes = new funcoes();
        SimuladorClassBkp simulador = new SimuladorClassBkp();
        SimuladorClassBkp AcessoSim = new SimuladorClassBkp();
        ClienteClasse objClienteClasse = new ClienteClasse();
        LogisticaClass objLogistica = new LogisticaClass();
        ClienteClasse objCliente = new ClienteClasse();

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;

            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                Session["GridViewProdutosDataTable"] = null;

                CarregaCombos();

                simulador.codempresa = "1";

                DataTable data = new DataTable();
                data = simulador.Consulta_Produto(1);
                data.Columns.Add("CodNome", typeof(string), "CodigoProduto + ' - ' + NomeProduto");
                ProdutoDropDownList.DataSource = data;
                ProdutoDropDownList.DataTextField = ("CodNome");
                ProdutoDropDownList.DataValueField = "CodigoProduto";
                ProdutoDropDownList.DataBind();

                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";

                ProdutosGridView.DataSource = new DataTable();
                ProdutosGridView.DataBind();
                ProdutosMultiView.Visible = true;

                ProdutosRow.Visible = false;
            }
        }

        protected void CarregaCombos()
        {
            FretesClass objFretesClass = new FretesClass();

            objFretesClass.empcod = EmpresaDropDownList.SelectedValue;

            FreteDropDownList.DataSource = objFretesClass.CarregaFreteIncoterms();
            FreteDropDownList.DataTextField = "Descricao";
            FreteDropDownList.DataValueField = "IDTipoFrete";
            FreteDropDownList.DataBind();

            objCliente.IDPais = PaisDropDownList.SelectedValue;

            objCliente.IDEstado = "0";

            EstadoDropDownList.DataSource = objCliente.RetornaListaEstados();
            EstadoDropDownList.DataTextField = "Nome";
            EstadoDropDownList.DataValueField = "IDEstado";
            EstadoDropDownList.DataBind();

            EstadoDropDownList_SelectedIndexChanged(null, null);
        }

        protected void EmpresaDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Limpar campos 
            SimuladorMultiView.Visible = false;
            EstadoDropDownList.SelectedIndex = 0;

            simulador.codempresa = EmpresaDropDownList.SelectedValue.ToString();

            DataTable data = new DataTable();
            data = simulador.Consulta_Produto(0);
            data.Columns.Add("CodNome", typeof(string), "CodigoProduto + ' - ' + NomeProduto");
            ProdutoDropDownList.DataSource = data;
            ProdutoDropDownList.DataTextField = ("CodNome");
            ProdutoDropDownList.DataValueField = "CodigoProduto";
            ProdutoDropDownList.DataBind();

            if (EmpresaDropDownList.SelectedValue == "3")
                BloqueiaCamposFrete();
            else if (FreteDropDownList.SelectedItem.Text == "FOB")
                BloqueiaCamposFrete();
            else
                LiberaCamposFrete();
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
        }

        protected string CarregaDadosDaTela()
        {
            if (ProdutosGridView.Rows.Count == 0) return "Insira algum produto";

            if (ProdutoDropDownList.SelectedValue == "") return "Escolha o Produto";

            if (EmpresaDropDownList.SelectedValue == "") return "Escolha a empresa";

            if (ValorNotaTextBox.Text == "") return "Informe o valor da nota";

            if (PaisDropDownList.SelectedValue == "") return "Escolha o pais";

            if (EstadoDropDownList.SelectedValue == "") return "Escolha o estado";

            if (MunicipioDropDownList.SelectedValue == "") return "Escolha o municipio";

            CarregaDadosDaTela_objLogistica();

            return "";
        }

        protected void CarregaDadosDaTela_objLogistica()
        {
            objLogistica.IDPais = Convert.ToInt32(PaisDropDownList.SelectedValue);

            objLogistica.IDEstado = Convert.ToInt32(EstadoDropDownList.SelectedValue);

            objLogistica.IDMunicipio = Convert.ToInt32(MunicipioDropDownList.SelectedValue);

            objLogistica.PesoNota = Convert.ToDecimal(PesoTotalTextBox.Text);

            objLogistica.ValorNota = Convert.ToDecimal(ValorNotaTextBox.Text);
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

            MunicipioDropDownList.Enabled = true;

            SimularButton.Enabled = true;
        }

        protected void BloqueiaCamposFrete()
        {
            MunicipioDropDownList.CssClass = "form-control";

            MunicipioDropDownList.Enabled = false;

            SimularButton.Enabled = false;
        }

        protected void SimularButton_Click(object sender, EventArgs e)
        {
            string erro = CarregaDadosDaTela();

            if (erro == "")
            {
                SimulacaoGridView.DataSource = objLogistica.RetornaFretes();
                SimulacaoGridView.DataBind();
                SimuladorMultiView.Visible = true;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "DescerTela",
               "setTimeout(function() { window.scrollTo(0, document.body.scrollHeight); }, 100);", true);
            }
            else
            {
                ApresentaMensagemErro(erro);
            }
        }

        protected void AdicionarProdutoLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable objDataTable = new DataTable();

                if (Session["GridViewProdutosDataTable"] != null)
                    objDataTable = (DataTable)Session["GridViewProdutosDataTable"];

                if (objDataTable.Rows.Count == 0)
                {
                    objDataTable.Columns.Add("IDLocalProduto");

                    objDataTable.Columns.Add("CodigoProduto");

                    objDataTable.Columns.Add("NomeProduto");

                    objDataTable.Columns.Add("QuantidadeProduto");

                    objDataTable.Columns.Add("QuantidadeConvertidaProduto");

                    objDataTable.Columns.Add("PesoProduto");
                }

                {
                    string CodigoProduto = "", NomeProduto = "", QuantidadeProduto = "", QuantidadeConvertidaProduto = "", PesoProduto = "";

                    if (ProdutoDropDownList.SelectedValue == "") throw new Exception("Informe um produto.");

                    CodigoProduto = ProdutoDropDownList.SelectedValue;

                    NomeProduto = ProdutoDropDownList.SelectedItem.Text;

                    if (string.IsNullOrEmpty(QuantidadeTextBox.Text)) throw new Exception("Informe a quantidade do produto.");

                    int resultadoQuantidade = 0;

                    if (!int.TryParse(QuantidadeTextBox.Text, out resultadoQuantidade)) throw new Exception("Informe um valor válido para a quantidade");

                    QuantidadeProduto = QuantidadeTextBox.Text;

                    //objLogistica.PesoNota
                    {
                        CrmProdutoClass objCrmProdutoClass = new CrmProdutoClass();

                        objCrmProdutoClass.CodigoProdutoSAP = CodigoProduto;

                        //objCrmProdutoClass.IDProduto
                        {
                            DataTable ProdutoDataTable = objCrmProdutoClass.RetornaProdutoPorCodigoProdutoSAP();

                            foreach (DataRow row in ProdutoDataTable.Rows)
                            {
                                objCrmProdutoClass.IDProduto = Convert.ToInt32(row["IDProduto"]);

                                break;
                            }
                        }

                        decimal quantidade = objCrmProdutoClass.RetornaQuantidadeConvertida(Convert.ToDecimal(QuantidadeProduto));

                        QuantidadeConvertidaProduto = quantidade.ToString();

                        decimal fatorConversao = objCrmProdutoClass.RetornaProdutoFatorConversao();

                        PesoProduto = (quantidade * fatorConversao).ToString();
                    }

                    objDataTable.Rows.Add(RetornaIDLocalProdutoMax() + 1, CodigoProduto, NomeProduto, QuantidadeProduto, QuantidadeConvertidaProduto, PesoProduto);
                }

                Session["GridViewProdutosDataTable"] = objDataTable;

                AtualizaGridViewProdutos();

                ProdutosRow.Visible = true;
            }
            catch (Exception ex)
            {
                ApresentaMensagemErro(ex.Message);
            }
        }

        protected int RetornaIDLocalProdutoMax()
        {
            int maxID = 0;

            DataTable objDataTable = new DataTable();

            if (Session["GridViewProdutosDataTable"] != null)
                objDataTable = (DataTable)Session["GridViewProdutosDataTable"];

            if (objDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in objDataTable.Rows)
                {
                    if (Convert.ToInt32(row["IDLocalProduto"]) > maxID)
                        maxID = Convert.ToInt32(row["IDLocalProduto"]);
                }
            }

            return maxID;
        }

        protected void ExcluirLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                string IDLocalProduto = ((Label)((Control)sender).FindControl("IDLocalProdutoLabelGridView")).Text;

                if (Session["GridViewProdutosDataTable"] != null)
                {
                    DataTable objDataTable = (DataTable)Session["GridViewProdutosDataTable"];

                    if (objDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in objDataTable.Rows)
                        {
                            if (row["IDLocalProduto"].ToString() == IDLocalProduto)
                            {
                                objDataTable.Rows.Remove(row);

                                break;
                            }
                        }
                    }

                    Session["GridViewProdutosDataTable"] = objDataTable;
                }

                AtualizaGridViewProdutos();

                ProdutosRow.Visible = true;
            }
            catch (Exception ex)
            {
                ApresentaMensagemErro(ex.Message);
            }
        }

        protected void QuantidadeTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int IDLocalProduto = Convert.ToInt32(((Label)((Control)sender).FindControl("IDLocalProdutoLabelGridView")).Text);

                string CodigoProduto = ((Label)((Control)sender).FindControl("CodigoProdutoLabelGridView")).Text;

                if (string.IsNullOrEmpty(((TextBox)((Control)sender).FindControl("QuantidadeTextBoxGridView")).Text))
                    throw new Exception("Informe a quantidade do produto.");

                decimal quantidade = Convert.ToDecimal(((TextBox)((Control)sender).FindControl("QuantidadeTextBoxGridView")).Text);

                if (Session["GridViewProdutosDataTable"] != null)
                {
                    DataTable objDataTable = (DataTable)Session["GridViewProdutosDataTable"];

                    if (objDataTable.Rows.Count > 0)
                    {
                        foreach (System.Data.DataColumn col in objDataTable.Columns)
                        {
                            col.ReadOnly = false;
                        }

                        CrmProdutoClass objCrmProdutoClass = new CrmProdutoClass();

                        objCrmProdutoClass.CodigoProdutoSAP = CodigoProduto;

                        // Pega o ID do produto
                        {
                            DataTable ProdutoDataTable = objCrmProdutoClass.RetornaProdutoPorCodigoProdutoSAP();

                            foreach (DataRow row in ProdutoDataTable.Rows)
                            {
                                objCrmProdutoClass.IDProduto = Convert.ToInt32(row["IDProduto"]);

                                break;
                            }
                        }

                        //Atualiza quantidade

                        objDataTable.AsEnumerable()
                            .Where(row => row.Field<string>(("IDLocalProduto")) == IDLocalProduto.ToString())
                                .Select(b => b["QuantidadeProduto"] = quantidade.ToString()).ToList();

                        //Atualiza quantidade convertida

                        decimal QuantidadeConvertidaProduto = objCrmProdutoClass.RetornaQuantidadeConvertida(Convert.ToDecimal(quantidade));

                        objDataTable.AsEnumerable()
                           .Where(row => row.Field<string>(("IDLocalProduto")) == IDLocalProduto.ToString())
                               .Select(b => b["QuantidadeConvertidaProduto"] = QuantidadeConvertidaProduto.ToString()).ToList();

                        decimal fatorConversao = objCrmProdutoClass.RetornaProdutoFatorConversao();

                        string PesoProduto = (quantidade * fatorConversao).ToString();

                        //Atualiza peso do pruduto

                        objDataTable.AsEnumerable()
                            .Where(row => row.Field<string>(("IDLocalProduto")) == IDLocalProduto.ToString())
                                .Select(b => b["PesoProduto"] = PesoProduto).ToList();
                    }

                    Session["GridViewProdutosDataTable"] = objDataTable;
                }

                AtualizaGridViewProdutos();

                ProdutosRow.Visible = true;
            }
            catch (Exception ex)
            {
                ApresentaMensagemErro(ex.Message);
            }
        }

        public void AtualizaGridViewProdutos()
        {
            DataTable objDataTable = new DataTable();

            if (Session["GridViewProdutosDataTable"] != null)
            {
                objDataTable = (DataTable)Session["GridViewProdutosDataTable"];

                if (objDataTable.Rows.Count > 0)
                {
                    decimal qtdTotal = 0, qtdTotalConv = 0, pesoTotal = 0;

                    foreach (DataRow row in objDataTable.Rows)
                    {
                        qtdTotal += Convert.ToDecimal(row["QuantidadeProduto"]);

                        qtdTotalConv += Convert.ToDecimal(row["QuantidadeConvertidaProduto"]);

                        pesoTotal += Convert.ToDecimal(row["PesoProduto"]);
                    }

                    QuantidadeTotalTextBox.Text = qtdTotal.ToString();

                    QuantidadeTotalConvertidaTextBox.Text = qtdTotalConv.ToString();

                    PesoTotalTextBox.Text = pesoTotal.ToString();

                    ProdutosGridView.DataSource = objDataTable;
                    ProdutosGridView.DataBind();
                    ProdutosMultiView.Visible = true;
                }
                else
                {
                    QuantidadeTotalTextBox.Text = "";

                    QuantidadeTotalConvertidaTextBox.Text = "";

                    PesoTotalTextBox.Text = "";

                    ProdutosMultiView.Visible = false;
                }
            }

            if (objDataTable.Rows.Count > 0)
                Session["GridViewProdutosDataTable"] = objDataTable;
            else
                Session["GridViewProdutosDataTable"] = null;
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            if (Session["VemTelaControladoriaSimuladorFrete"] != null)
            {
                if (Session["VemTelaControladoriaSimuladorFrete"].ToString() == "Sim")
                {
                    Session["VemTelaControladoriaSimuladorFrete"] = null;

                    Response.Redirect("~/Controladoria/HomeControladoriaWebForm.aspx?indmnu=3");
                }
            }
            else
            {
                Response.Redirect("~/Logistica_New/HomeWebForm.aspx?indmnu=5");
            }
        }

        protected void ApresentaMenssagemSucesso(string sucesso)
        {
            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(sucesso, true);
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "SubirTela", "setTimeout(function() { window.scrollTo(0, 0); }, 100);", true);
        }

        protected void ApresentaMensagemErro(string erro)
        {
            ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemErro(erro, true);
            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
            ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "SubirTela", "setTimeout(function() { window.scrollTo(0, 0); }, 100);", true);
        }
    }
}