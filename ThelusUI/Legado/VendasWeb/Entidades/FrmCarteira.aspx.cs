using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Data;
using VendasWeb.classes;

namespace VendasWeb.cadastros
{
    public partial class FrmCarteira : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        clsEntidades ObjEntidadesClass = new clsEntidades();
        funcoes mdlFuncoes = new funcoes();
        criptografia mdlCriptografia = new criptografia();
        usuario ObjUsuarioClass = new usuario();
        VendedorClass ObjVendedorClass = new VendedorClass();
        produto ObjProduto = new produto();
        HistoricoCRMClass ObjHistoricoCRMClass = new HistoricoCRMClass();
        ClienteClasse OBJCliente = new ClienteClasse();
        UtilClass ObjUtilClass = new UtilClass();

        List<clsEntidades> ListObjEntidadesRotas = new List<clsEntidades>();

        protected void Page_Load(object sender, EventArgs e)
        {
            string parameter = Request["__EVENTARGUMENT"]; // parameter

            #region Registrando as Picker


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anything", "Picker();", true);


            #endregion

            ObjEntidadesClass = new clsEntidades();
            mdlFuncoes = new funcoes();
            ObjProduto = new produto();
            ObjHistoricoCRMClass = new HistoricoCRMClass();

            //Valida Acesso
            OBJSessao.ValidaAcesso();

            ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = false;
            if (Session["Msg"] != null)
            {
                ///Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemAlerta(Session["Msg"].ToString(),true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();
                Session["Msg"] = null;
            }

            if (!IsPostBack)
            {
                Session["RetornarNavegacaoPara"] = "../Entidades/FrmCarteira.aspx?indmnu=2";
                Session["clsEntidades"] = null;
                Session["Lista_Evento_Filtro"] = null;

                /*Tratar Abrir e fechar Div*/
                collapseLiteral.Text = "<div id=\"filtros\" class=\"collapse\" runat=\"server\">";

                /*
                //Combo Status para Consulta
                StatusEntidadeDropDownList.DataSource = ObjEntidadesClass.Consulta_Stat_Ent("Consulta");
                StatusEntidadeDropDownList.DataTextField = "StatEntDescr";
                StatusEntidadeDropDownList.DataValueField = "StatEntCod";
                StatusEntidadeDropDownList.DataBind();
                StatusEntidadeDropDownList.Items.Insert(0, new ListItem("Todos", ""));

                //Combo Status Comercial
                StatusComercialDropDownList.DataSource = ObjEntidadesClass.Consulta_Stat_Ent_Comercial();
                StatusComercialDropDownList.DataTextField = "StatEntComercial";
                StatusComercialDropDownList.DataValueField = "StatEntComercial";
                StatusComercialDropDownList.DataBind();
                StatusComercialDropDownList.Items.Insert(0, new ListItem("Todos", ""));

                //Combo Estados
                UfDropDownList.DataSource = mdlFuncoes.Consulta_Estado();
                UfDropDownList.DataTextField = "UfNome";
                UfDropDownList.DataValueField = "UfSigla";
                UfDropDownList.DataBind();
                UfDropDownList.Items.Insert(0, new ListItem("Todos", ""));

                //Combo Cidade
                CidadeSelect.Items.Insert(0, new ListItem("Todas", "TODOS"));

                //Linha do Produto
                ObjProduto.UsuCod = Session["usuario"].ToString();

                LinhaProdutoDropDownList.DataSource = ObjProduto.Consulta_Linha_Produto();
                LinhaProdutoDropDownList.DataTextField = "USERLINHAPRODUTOLISTA";
                LinhaProdutoDropDownList.DataValueField = "USERLINHAPRODUTOLISTA";
                LinhaProdutoDropDownList.DataBind();
                LinhaProdutoDropDownList.Items.Insert(0, new ListItem("Todas", ""));

                //Produtos
                ProdutoSelect.Items.Insert(0, new ListItem("Todos", "TODOS"));
                */

                //Verifica qual Tipo de Operacao sera possivel Realizar nessa tela para o Usuario Logado
                ObjEntidadesClass = new clsEntidades();
                ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
                ObjEntidadesClass.ConsultaTipoOperacao("FrmCarteira");
                Session["TipoOperacao"] = ObjEntidadesClass.TipoOperacao; //Session Criada aqui apenas para evitar processamentos futuros, utilizada apenas nessa tela.


                //Verifica se o Usuario possui algum Vendedor para Definir qual tipo de Status Mostrar
                if (ObjUsuarioClass.ConsultaVendedorUsuario(Session["usuario"].ToString()) != 0)
                {
                    StatusEntidadeLabel.Visible = false;
                    StatusEntidadeDropDownList.Visible = false;
                    StatusComercialLabel.Visible = true;
                    StatusComercialDropDownList.Visible = true;

                    ListaEntidadeGridView.Columns[7].Visible = false;
                }
                else
                {
                    StatusEntidadeLabel.Visible = true;
                    StatusEntidadeDropDownList.Visible = true;
                    StatusComercialLabel.Visible = false;
                    StatusComercialDropDownList.Visible = false;

                    ListaEntidadeGridView.Columns[8].Visible = false;
                }

                this.ControlPainel.Desabilitar_Botoes();

                //Atualiza Grid Com os Vendedores
                Atualiza_Select_Vendedores();

                //Combo Categoria Principal 
                DataTable DtEntCateg = new DataTable();
                DtEntCateg = ObjEntidadesClass.Consulta_Categoria_Entidade_Geral("Cliente");

                CategoriaDropDownList.DataSource = DtEntCateg;
                CategoriaDropDownList.DataTextField = "Categoria";
                CategoriaDropDownList.DataValueField = "CategCodEstr";
                CategoriaDropDownList.DataBind();
                CategoriaDropDownList.Items.Insert(0, new ListItem("Todas", "0000000"));

                //Combo Categoria secundária               
                CategoriaSecundariaDropDownList.DataSource = DtEntCateg;
                CategoriaSecundariaDropDownList.DataTextField = "Categoria";
                CategoriaSecundariaDropDownList.DataValueField = "CategCodEstr";
                CategoriaSecundariaDropDownList.DataBind();
                CategoriaSecundariaDropDownList.Items.Insert(0, new ListItem("Todas", "0000000"));

                //Combo Status Compra
                StatEntCompraDropDownList.DataSource = ObjEntidadesClass.Consulta_Stat_Compra();
                StatEntCompraDropDownList.DataTextField = "StatEntCompra";
                StatEntCompraDropDownList.DataValueField = "StatEntCompra";
                StatEntCompraDropDownList.DataBind();
                StatEntCompraDropDownList.Items.Insert(0, new ListItem("Todos", "0000000"));

                if (Session["ObjEntidadesClassFiltro"] != null)
                {
                    ObjEntidadesClass = (clsEntidades)Session["ObjEntidadesClassFiltro"];

                    Atualiza_Grid();//Atualiza Grid

                    Session["ObjEntidadesClassFiltro"] = null;
                }

                //Verifica se a Session de Entidades para Criar rota esta criad
                if (Session["ListObjEntidadesRotas"] != null)
                {
                    //Pega Valores
                    ListObjEntidadesRotas = (List<clsEntidades>)Session["ListObjEntidadesRotas"];

                    //Verifica se contem alguma entidade
                    if (ListObjEntidadesRotas.Count > 0)
                    {
                        //Libera Menu de Roterizacao
                        this.ControlPainel.Libera_Roterizacao();
                    }
                }
            }
            else
            {
                if (OperacaoHiddenField.Value == "Incluir")
                {
                    OperacaoHiddenField.Value = " ";
                    SalvarButton_JS();
                }
            }
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {            
            switch (drpEntCod.SelectedValue.ToString())
            {
                case "1":
                    ObjEntidadesClass.EntNomeFant = txtFiltroEntCod.Text;
                    break;

                case "2":
                    ObjEntidadesClass.EntNome = txtFiltroEntCod.Text;
                    break;

                case "3":
                    ObjEntidadesClass.EntCod = txtFiltroEntCod.Text;
                    break;

                case "4":
                    ObjEntidadesClass.EntCpfCgc = txtFiltroEntCod.Text;
                    break;
            }


            ObjEntidadesClass.UsuCod = Session["usuario"].ToString();
            //ObjEntidadesClass.StatEntCod = StatusEntidadeDropDownList.SelectedValue;
            //ObjEntidadesClass.StatEntComercial = StatusComercialDropDownList.SelectedValue;

            //Estado
            //ObjEntidadesClass.UFSIGLA = UfDropDownList.SelectedValue;

            //Cidade
            //RecuperaDados_Select_Cidade();




            //Linha
            //ObjEntidadesClass.USERLINHAPRODUTOLISTA = LinhaProdutoDropDownList.SelectedValue;

            //Produto
            //RecuperaDados_Select_Produto();



            //Periodo de Compra
            //ObjEntidadesClass.PeriodoCompraInicial = PeriodoCompraInicialTextBox.Text;
            //ObjEntidadesClass.PeriodoCompraFinal = PeriodoCompraFinalTextBox.Text;


            #region (Faturamento Medio / Removido conforme solicitação chamado 11828 Manuli  - Lizier 2016-04-01
            /*
            //Fita
            switch (FaturamentoMedioFitaDropDownList.SelectedValue.ToString())
            {
                case "0":
                    ObjEntidadesClass.FaturamentoMedioFitaInicial = 1;
                    ObjEntidadesClass.FaturamentoMedioFitaFinal = 2000;
                    break;

                case "1":
                    ObjEntidadesClass.FaturamentoMedioFitaInicial = 2001;
                    ObjEntidadesClass.FaturamentoMedioFitaFinal = 5000;
                    break;

                case "2":
                    ObjEntidadesClass.FaturamentoMedioFitaInicial = 5001;
                    ObjEntidadesClass.FaturamentoMedioFitaFinal = 10000;
                    break;

                case "3":
                    ObjEntidadesClass.FaturamentoMedioFitaInicial = 10001;
                    ObjEntidadesClass.FaturamentoMedioFitaFinal = 20000;
                    break;


                case "4":
                    ObjEntidadesClass.FaturamentoMedioFitaInicial = 20001;
                    ObjEntidadesClass.FaturamentoMedioFitaFinal = 40000;
                    break;



                case "5":
                    ObjEntidadesClass.FaturamentoMedioFitaInicial = 40001;
                    ObjEntidadesClass.FaturamentoMedioFitaFinal = 60000;
                    break;

                case "6":
                    ObjEntidadesClass.FaturamentoMedioFitaInicial = 60001;
                    ObjEntidadesClass.FaturamentoMedioFitaFinal = 100000;
                    break;

                case "7":
                    ObjEntidadesClass.FaturamentoMedioFitaInicial = 100001;
                    ObjEntidadesClass.FaturamentoMedioFitaFinal = 200000;
                    break;

                case "8":
                    ObjEntidadesClass.FaturamentoMedioFitaInicial = 200001;
                    ObjEntidadesClass.FaturamentoMedioFitaFinal = 500000;
                    break;

                case "9":
                    ObjEntidadesClass.FaturamentoMedioFitaInicial = 500001;
                    ObjEntidadesClass.FaturamentoMedioFitaFinal = 1000000;
                    break;


                default:
                    ObjEntidadesClass.FaturamentoMedioFitaInicial = 0;
                    ObjEntidadesClass.FaturamentoMedioFitaFinal = 1000000000;
                    break;
            }





            //Stretche
            switch (FaturamentoMedioStretchDropDownList.SelectedValue.ToString())
            {
                case "0":
                    ObjEntidadesClass.FaturamentoMedioStretchInicial = 1;
                    ObjEntidadesClass.FaturamentoMedioStretchFinal = 2000;
                    break;

                case "1":
                    ObjEntidadesClass.FaturamentoMedioStretchInicial = 2001;
                    ObjEntidadesClass.FaturamentoMedioStretchFinal = 5000;
                    break;

                case "2":
                    ObjEntidadesClass.FaturamentoMedioStretchInicial = 5001;
                    ObjEntidadesClass.FaturamentoMedioStretchFinal = 10000;
                    break;

                case "3":
                    ObjEntidadesClass.FaturamentoMedioStretchInicial = 10001;
                    ObjEntidadesClass.FaturamentoMedioStretchFinal = 20000;
                    break;


                case "4":
                    ObjEntidadesClass.FaturamentoMedioStretchInicial = 20001;
                    ObjEntidadesClass.FaturamentoMedioStretchFinal = 40000;
                    break;



                case "5":
                    ObjEntidadesClass.FaturamentoMedioStretchInicial = 40001;
                    ObjEntidadesClass.FaturamentoMedioStretchFinal = 60000;
                    break;

                case "6":
                    ObjEntidadesClass.FaturamentoMedioStretchInicial = 60001;
                    ObjEntidadesClass.FaturamentoMedioStretchFinal = 100000;
                    break;

                case "7":
                    ObjEntidadesClass.FaturamentoMedioStretchInicial = 100001;
                    ObjEntidadesClass.FaturamentoMedioStretchFinal = 200000;
                    break;

                case "8":
                    ObjEntidadesClass.FaturamentoMedioStretchInicial = 200001;
                    ObjEntidadesClass.FaturamentoMedioStretchFinal = 500000;
                    break;

                case "9":
                    ObjEntidadesClass.FaturamentoMedioStretchInicial = 500001;
                    ObjEntidadesClass.FaturamentoMedioStretchFinal = 1000000;
                    break;


                default:
                    ObjEntidadesClass.FaturamentoMedioStretchInicial = 0;
                    ObjEntidadesClass.FaturamentoMedioStretchFinal = 1000000000;
                    break;
            }*/
            #endregion


            RecuperaDados_Select();//VendCod selecionados


            //RecuperaDados_Select_Classe();//Classes Selecionadas


            //RecuperaDados_Select_EntCateg();//Categorias selecioanadas

            //RecuperaDados_Select_StatusCompra(); //Status de Compra

            Atualiza_Grid();//Atualiza Grid
            


        }


        public void Atualiza_Grid()
        {

            ClientesMultiView.Visible = true;

            DataTable outputTable = new DataTable();
            outputTable = ObjEntidadesClass.Consulta_Entidade();

            ListaEntidadeGridView.DataSource = outputTable;
            ListaEntidadeGridView.DataBind();


            if (outputTable.Rows.Count > 0)
            {

                //Guarda Session de Filtro Realizado
                Session["ObjEntidadesClassFiltro"] = ObjEntidadesClass;

                // AplicaFootableGrid();

                /*
                //Libera Menu de Geomapeamento
                this.ControlPainel.Libera_Geomapeamento();

                #region Armazena Codigos Entidades para Carregar no Maps

                string EntCodMaps = "";
                Session["EntCodMaps"] = "";

                foreach (DataRow row in outputTable.Rows)
                {

                    EntCodMaps += row["EntCod"].ToString() + ",";

                }


                Session["EntCodMaps"] = EntCodMaps;

                #endregion
                */

            }

        }



        protected void RecuperaDados_Select_StatusCompra()
        {

            ObjEntidadesClass.StatEntCompra = "";

            for (int i = 0; i < StatEntCompraDropDownList.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (StatEntCompraDropDownList.Items[i].Selected == true)
                {
                    ObjEntidadesClass.StatEntCompra += StatEntCompraDropDownList.Items[i].Value + ",";
                }
            }


          

        }


        protected void RecuperaDados_Select_EntCateg()
        {

            ObjEntidadesClass.CNAE_P = "";

            for (int i = 0; i < CategoriaDropDownList.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (CategoriaDropDownList.Items[i].Selected == true)
                {
                    ObjEntidadesClass.CNAE_P += CategoriaDropDownList.Items[i].Value + ",";
                }
            }


            ObjEntidadesClass.CNAE_S = "";

            for (int i = 0; i < CategoriaSecundariaDropDownList.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (CategoriaSecundariaDropDownList.Items[i].Selected == true)
                {
                    ObjEntidadesClass.CNAE_S += CategoriaSecundariaDropDownList.Items[i].Value + ",";
                }
            }


        }


        protected void RecuperaDados_Select_Classe()
        {

            ObjEntidadesClass.VendClasseCod = "";

            for (int i = 0; i < VendClasseDropDownList.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (VendClasseDropDownList.Items[i].Selected == true)
                {
                    ObjEntidadesClass.VendClasseCod += VendClasseDropDownList.Items[i].Value + ",";
                }
            }


        }


        protected void RecuperaDados_Select()
        {

            ObjEntidadesClass.VendCod = "";

            for (int i = 0; i < VendedoresSelect.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (VendedoresSelect.Items[i].Selected == true)
                {
                    if (i == 0)
                    {
                        ObjEntidadesClass.VendCod = VendedoresSelect.Items[i].Value;
                    }
                    else
                    {
                        ObjEntidadesClass.VendCod += "," + VendedoresSelect.Items[i].Value;
                    }
                }
            }


        }


        protected void RecuperaDados_Select_Cidade()
        {

            ObjEntidadesClass.CidCod = "";

            for (int i = 0; i < CidadeSelect.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (CidadeSelect.Items[i].Selected == true)
                {
                    ObjEntidadesClass.CidCod += CidadeSelect.Items[i].Value + ",";
                }
            }





        }

        protected void RecuperaDados_Select_Produto()
        {

            ObjEntidadesClass.ProdCodEstr = "";

            for (int i = 0; i < ProdutoSelect.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (ProdutoSelect.Items[i].Selected == true)
                {
                    ObjEntidadesClass.ProdCodEstr += ProdutoSelect.Items[i].Value + ",";
                }
            }





        }



        protected void ListaEntidadeGridView_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            ListaEntidadeGridView.PageIndex = e.NewPageIndex;
            btnListar_Click(null, null);
        }


        protected void Atualiza_Select_Vendedores()
        {
            DataTable Resultado = new DataTable();

            ObjVendedorClass.UsuCod = Session["usuario"].ToString();
            ObjVendedorClass.TodosCodigos = "S";
            Resultado = ObjVendedorClass.Consulta_Vendedor();

            VendedoresSelect.DataSource = Resultado;
            VendedoresSelect.DataTextField = "NomeVendedor";
            VendedoresSelect.DataValueField = "IDVendedor";
            VendedoresSelect.DataBind();

            /*
            //Retira  o resultado em Branco da Consulta de Vendedores utilizado acima
            var result = from r in Resultado.AsEnumerable()
                         where r.Field<string>("VendClasseCod") != "" &&
                               r.Field<string>("VendClasseDescr") != ""
                         select r;
            DataTable dtResult = result.CopyToDataTable();
            */

            /*
            VendClasseDropDownList.DataSource = dtResult;
            VendClasseDropDownList.DataTextField = "VendClasseDescr";
            VendClasseDropDownList.DataValueField = "VendClasseCod";
            VendClasseDropDownList.DataBind();
            VendClasseDropDownList.Items.Insert(0, new ListItem("Todas", "0000000"));
            */

        }


        protected void UfDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            mdlFuncoes = new funcoes();

            CidadeSelect.Items.Clear();


            if (UfDropDownList.SelectedValue != "")
            {

                CidadeSelect.DataSource = mdlFuncoes.Consulta_Cidade(UfDropDownList.SelectedValue.ToUpper());
                CidadeSelect.DataTextField = "CidNome";
                CidadeSelect.DataValueField = "CidCod";
                CidadeSelect.DataBind();

            }

            CidadeSelect.Items.Insert(0, new ListItem("Todas", "TODOS"));

            /*Tratar Abrir e fechar Div*/
            collapseLiteral.Text = "<div id=\"filtros\" class=\"collapse in\" runat=\"server\">";

        }


        protected void LinhaProdutoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            ObjProduto = new produto();

            ProdutoSelect.Items.Clear();

            if (LinhaProdutoDropDownList.SelectedValue != "")
            {

                ObjProduto.USERLINHAPRODUTOLISTA = LinhaProdutoDropDownList.SelectedValue;

                ProdutoSelect.DataSource = ObjProduto.Consulta_Produto();
                ProdutoSelect.DataTextField = "ProdNome";
                ProdutoSelect.DataValueField = "ProdCodEstr";
                ProdutoSelect.DataBind();



            }

            ProdutoSelect.Items.Insert(0, new ListItem("Todos", "TODOS"));
            /*Tratar Abrir e fechar Div*/
            collapseLiteral.Text = "<div id=\"filtros\" class=\"collapse in\" runat=\"server\">";

        }



        protected void SelecionarCheckedChanged(object sender, EventArgs e)
        {
            CheckBox CheckBox = (CheckBox)sender;
            GridViewRow oldGridViewRow = (GridViewRow)CheckBox.NamingContainer;

            //Desmarca todos os check
            foreach (GridViewRow OldGridView in ListaEntidadeGridView.Rows)
            {
                //Seta todos como falso
                ((RadioButton)OldGridView.FindControl("SelecionarRadioButton")).Checked = false;
            }

            //marcando o RadioButton selecionado
            RadioButton RadioButton = (RadioButton)sender;
            GridViewRow GridViewRow = (GridViewRow)RadioButton.NamingContainer;
            ((RadioButton)GridViewRow.FindControl("SelecionarRadioButton")).Checked = true;


            /*Pega o codigo da Entidade Selecionada*/
            ObjEntidadesClass = new clsEntidades();
            ObjEntidadesClass.IDCliente = Convert.ToInt32(((Label)((Control)sender).FindControl("IDClienteLabel")).Text);
            ObjEntidadesClass.CodigoClienteSAP = ((Label)((Control)sender).FindControl("CodigoLabel")).Text;
            ObjEntidadesClass.SituacaoComercial = ((Label)((Control)sender).FindControl("StatEntComercialLabel")).Text;
            ObjEntidadesClass.EntCod = ObjEntidadesClass.IDCliente.ToString();

            /*Recupera dados do cliente*/
            OBJCliente.CodigoCliente = ObjEntidadesClass.CodigoClienteSAP;
            OBJCliente.IDCliente = Convert.ToInt32(ObjEntidadesClass.EntCod ?? "0");
            Session["clienteClasse"] = OBJCliente;

            /*Carrega em Session*/
            Session["clsEntidades"] = ObjEntidadesClass;


            //ClientesMultiView.Visible = false;

            //ClientesMultiView.Visible = false;
            /*
            if (ListaEntidadeGridView.Rows.Count > 0)
            {
                // AplicaFootableGrid();
            }
            */


            //Grava Codigo Entidade para Apresentar no Maps
            Session["EntCodMaps"] = ObjEntidadesClass.EntCod;

            this.ControlPainel.refresh();


        }



        public void AplicaFootableGrid()
        {

            ListaEntidadeGridView.UseAccessibleHeader = true;
            ListaEntidadeGridView.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
            ListaEntidadeGridView.HeaderRow.Cells[7].Attributes["data-hide"] = "tablet,phone";
            ListaEntidadeGridView.HeaderRow.Cells[8].Attributes["data-hide"] = "tablet,phone";
            ListaEntidadeGridView.HeaderRow.Cells[9].Attributes["data-hide"] = "tablet,phone";
            ListaEntidadeGridView.HeaderRow.Cells[10].Attributes["data-hide"] = "tablet,phone";
            ListaEntidadeGridView.HeaderRow.Cells[11].Attributes["data-hide"] = "tablet,phone";
            ListaEntidadeGridView.HeaderRow.Cells[12].Attributes["data-hide"] = "tablet,phone";
            ListaEntidadeGridView.HeaderRow.Cells[13].Attributes["data-hide"] = "tablet,phone";


            //ListaEntidadeGridView.HeaderRow.Cells[0].Attributes["data-sort-ignore"] = "true";
            ListaEntidadeGridView.HeaderRow.Cells[1].Attributes["data-sort-ignore"] = "true";
            ListaEntidadeGridView.HeaderRow.Cells[2].Attributes["data-sort-ignore"] = "true";
            ListaEntidadeGridView.HeaderRow.Cells[3].Attributes["data-sort-ignore"] = "true";
            ListaEntidadeGridView.HeaderRow.Cells[4].Attributes["data-sort-ignore"] = "true";
            ListaEntidadeGridView.HeaderRow.Cells[5].Attributes["data-sort-ignore"] = "true";
            ListaEntidadeGridView.HeaderRow.Cells[6].Attributes["data-sort-ignore"] = "true";
            ListaEntidadeGridView.HeaderRow.Cells[7].Attributes["data-sort-ignore"] = "true";
            ListaEntidadeGridView.HeaderRow.Cells[8].Attributes["data-sort-ignore"] = "true";
            ListaEntidadeGridView.HeaderRow.Cells[9].Attributes["data-sort-ignore"] = "true";
            ListaEntidadeGridView.HeaderRow.Cells[10].Attributes["data-sort-ignore"] = "true";
            ListaEntidadeGridView.HeaderRow.Cells[11].Attributes["data-sort-ignore"] = "true";
            ListaEntidadeGridView.HeaderRow.Cells[12].Attributes["data-sort-ignore"] = "true";
            ListaEntidadeGridView.HeaderRow.Cells[13].Attributes["data-sort-ignore"] = "true";


            ListaEntidadeGridView.HeaderRow.TableSection = TableRowSection.TableHeader;

            //Quando utilizado Footable chamado esta propriedade para colocar a paginação no Tfoot;
            ListaEntidadeGridView.BottomPagerRow.TableSection = TableRowSection.TableFooter;

        }


        protected void ListaEntidadeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            ObjHistoricoCRMClass = new HistoricoCRMClass();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList drpEvento = e.Row.Cells[13].FindControl("drpEvento") as DropDownList;
                DropDownList drpCategoria = e.Row.Cells[13].FindControl("drpCategoria") as DropDownList;


                if (Session["Lista_Evento_Filtro"] == null)
                {
                    Session["Lista_Evento_Filtro"] = ObjHistoricoCRMClass.Lista_Evento_Filtro();
                }


                //Consulta evento de filtro
                drpEvento.DataSource = (DataTable)Session["Lista_Evento_Filtro"];
                drpEvento.DataTextField = "Descricao";
                drpEvento.DataValueField = "Codigo";
                drpEvento.DataBind();
                drpEvento.Items.Insert(0, new ListItem("Selecione", "0"));
                drpCategoria.Items.Insert(0, new ListItem("Selecione", "0"));




            }

        }


        protected void drpEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjHistoricoCRMClass = new HistoricoCRMClass();
            DropDownList AuxdrpEvento = (DropDownList)((Control)sender).FindControl("drpEvento");
            DropDownList AuxdrpCategoria = (DropDownList)((Control)sender).FindControl("drpCategoria");

            ObjHistoricoCRMClass.CodigoPai = AuxdrpEvento.SelectedValue;
            AuxdrpCategoria.DataSource = ObjHistoricoCRMClass.Lista_Categoria();
            AuxdrpCategoria.DataTextField = "Descricao";
            AuxdrpCategoria.DataValueField = "Codigo";
            AuxdrpCategoria.DataBind();
            AuxdrpCategoria.Items.Insert(0, new ListItem("Selecione", "0"));

            AuxdrpEvento.CssClass = "selectpicker show-tick";
            AuxdrpCategoria.CssClass = "selectpicker show-tick";

        }


        protected void SalvarButton_Click(object sender, EventArgs e)
        {

            string erro = "";


            ObjHistoricoCRMClass = new HistoricoCRMClass();

            ObjHistoricoCRMClass.CodigoCategoria = Convert.ToInt32(((DropDownList)((Control)sender).FindControl("drpCategoria")).SelectedValue);
            ObjHistoricoCRMClass.CodigoEvento = Convert.ToInt32(((DropDownList)((Control)sender).FindControl("drpEvento")).SelectedValue);
            ObjHistoricoCRMClass.DataCad = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            ObjHistoricoCRMClass.EntCod = ((Label)((Control)sender).FindControl("EntCodLabel")).Text;
            ObjHistoricoCRMClass.UsuCod = Session["usuario"].ToString();
            ObjHistoricoCRMClass.Historico = ((TextBox)((Control)sender).FindControl("HistoricoTextBox")).Text;



            if (((TextBox)((Control)sender).FindControl("txtData")).Text != "")
            {

                ObjHistoricoCRMClass.DataAgenda = Convert.ToDateTime(((TextBox)((Control)sender).FindControl("txtData")).Text
                                                                        + " " +
                                                                      ((DropDownList)((Control)sender).FindControl("drpHora")).SelectedValue
                                                                        + ":00"
                                                                      ).ToString();


            }
            else
            {
                ObjHistoricoCRMClass.DataAgenda = "";
            }




            if (ObjHistoricoCRMClass.Historico.Length < 20)
            {
                erro = "Histórico deve ter no mínimo 20 caracteres.";
            }


            if (ObjHistoricoCRMClass.CodigoCategoria == 0 || ObjHistoricoCRMClass.CodigoEvento == 0)
            {
                erro = "Obrigatório informar a categoria e o evento";
            }

            if (erro == "")
            {
                erro = ObjHistoricoCRMClass.Historico_Inserir();
            }

            if (erro != "")
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + erro + "');", true);
            }
            else
            {


                ((Label)((Control)sender).FindControl("DataUltimoContatoLabel")).Text = ObjHistoricoCRMClass.DataCad.ToString();
                ((Label)((Control)sender).FindControl("UsuarioUltimoHistoricoLabel")).Text = ObjHistoricoCRMClass.UsuCod.ToString();
                ((Label)((Control)sender).FindControl("UltimoHistoricoLabel")).Text = ObjHistoricoCRMClass.Historico.ToString();



                ((TextBox)((Control)sender).FindControl("HistoricoTextBox")).Text = "";
                ((DropDownList)((Control)sender).FindControl("drpEvento")).SelectedValue = "0";
                ((DropDownList)((Control)sender).FindControl("drpCategoria")).SelectedValue = "0";
                ((TextBox)((Control)sender).FindControl("txtData")).Text = "";



                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Historico Incluido com Sucesso!');", true);


                /*
                Session["Msg"] = "Historico Incluido com Sucesso!";
                Response.Redirect("../Entidades/FrmCarteira.aspx?indmnu=5");*/


            }



        }

        protected void SalvarButton_JS()
        {

            string erro = "";


            ObjHistoricoCRMClass = new HistoricoCRMClass();

            ObjHistoricoCRMClass.CodigoCategoria = Convert.ToInt32(CategoriaHiddenField.Value);
            ObjHistoricoCRMClass.CodigoEvento = Convert.ToInt32(EventoHiddenField.Value);
            ObjHistoricoCRMClass.DataCad = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            ObjHistoricoCRMClass.EntCod = CodigoHiddenField.Value;
            ObjHistoricoCRMClass.UsuCod = Session["usuario"].ToString();
            ObjHistoricoCRMClass.Historico = HistoricoHiddenField.Value;



            if (DataHiddenField.Value != "")
            {

                ObjHistoricoCRMClass.DataAgenda = Convert.ToDateTime(DataHiddenField.Value
                                                                        + " " +
                                                                      HoraHiddenField.Value
                                                                        + ":00"
                                                                      ).ToString();


            }
            else
            {
                ObjHistoricoCRMClass.DataAgenda = "";
            }




            /*if (ObjHistoricoCRMClass.Historico.Length < 20)
            {
                erro = "Histórico deve ter no mínimo 20 caracteres.";
            }
            */

            if (ObjHistoricoCRMClass.CodigoCategoria == 0 || ObjHistoricoCRMClass.CodigoEvento == 0)
            {
                erro = "Obrigatório informar a categoria e o evento";
            }

            if (erro == "")
            {
                erro = ObjHistoricoCRMClass.Historico_Inserir();
            }

            if (erro != "")
            {
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + erro + "');", true);

            }
            else
            {
                // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Historico Incluido com Sucesso!');", true);
                //Response.Write("<script>alert(\"Historico Incluido com Sucesso!\");</script>");
            }



            btnListar_Click(null, null);


        }

        protected void RoterizacaoButton_Click(object sender, EventArgs e)
        {
            clsEntidades ObjclsEntidadesAux =  new clsEntidades();
            
            ObjclsEntidadesAux.EntCod = ((Label)((Control)sender).FindControl("EntCodLabel")).Text;
            ObjclsEntidadesAux.EntNome = ((Label)((Control)sender).FindControl("EntNomeLabel")).Text;

            if (Session["ListObjEntidadesRotas"] != null)
            {
                ListObjEntidadesRotas = (List<clsEntidades>)Session["ListObjEntidadesRotas"];
            }
            else
            {
                ListObjEntidadesRotas = new List<clsEntidades>();
            }


                if(ListObjEntidadesRotas.Count > 0)
                {
                    //Pega a ultima ordeacao incluida e adicioa mais 1
                    int MaxOrdenRoterizacao = ListObjEntidadesRotas.OrderBy(R => R.OrdenRoterizacao).Max(Order => Order.OrdenRoterizacao);
                    ObjclsEntidadesAux.OrdenRoterizacao = MaxOrdenRoterizacao + 1;

                    ListObjEntidadesRotas.Add(ObjclsEntidadesAux);
                }
                else
                {
                    ListObjEntidadesRotas = new List<clsEntidades>();

                    ObjclsEntidadesAux.OrdenRoterizacao = 1;//Primeiro endereco da Lista para Roterizar
                    ListObjEntidadesRotas.Add(ObjclsEntidadesAux);
                }

                Session["ListObjEntidadesRotas"] = ListObjEntidadesRotas;

                string Mensagem = "Cliente " + ObjclsEntidadesAux.EntCod + " - " + ObjclsEntidadesAux.EntNome + ", adicionado ao Plano de Rota!";
                ((Label)Master.FindControl("MenssagemMasterLabel")).Text = ObjUtilClass.MenssagemSucesso(Mensagem, true);
                ((Label)Master.FindControl("MenssagemMasterLabel")).Visible = true;
                ((Label)Master.FindControl("MenssagemMasterLabel")).Focus();

                //Libera Menu de Roterizacao
                this.ControlPainel.Libera_Roterizacao();
        }


    }
}