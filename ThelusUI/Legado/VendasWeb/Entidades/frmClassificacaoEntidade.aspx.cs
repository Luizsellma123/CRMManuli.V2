using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using System.Data;

namespace VendasWeb.Entidades
{
    public partial class frmClassificacaoEntidade : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        clsEntidades ObjEntidadesClass = new clsEntidades();
        funcoes mdlFuncoes = new funcoes();
        criptografia mdlCriptografia = new criptografia();
        usuario ObjUsuarioClass = new usuario();
        VendedorClass ObjVendedorClass = new VendedorClass();
        produto ObjProduto = new produto();
        HistoricoCRMClass ObjHistoricoCRMClass = new HistoricoCRMClass();

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


            if (Session["Msg"] != null)
            {


                Response.Write("<script>alert(\"" + Session["Msg"].ToString() + "\");</script>");
                Session["Msg"] = null;
            }

            if (!IsPostBack)
            {
                Session["clsEntidades"] = null;
                Session["Lista_Evento_Filtro"] = null;

                /*Tratar Abrir e fechar Div*/
                collapseLiteral.Text = "<div id=\"filtros\" class=\"collapse\" runat=\"server\">";

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
                }
                else
                {
                    StatusEntidadeLabel.Visible = true;
                    StatusEntidadeDropDownList.Visible = true;
                    StatusComercialLabel.Visible = false;
                    StatusComercialDropDownList.Visible = false;
                }

                //Atualiza Grid Com os Vendedores
                Atualiza_Select_Vendedores();
            }
            else
            {
                if (OperacaoHiddenField.Value == "Incluir")
                {
                    OperacaoHiddenField.Value = " ";
                    SalvarButton_JS();
                }
            }

            this.ControlPainel.refreshVendedor();
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            ClientesMultiView.Visible = true;

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
            ObjEntidadesClass.StatEntCod = StatusEntidadeDropDownList.SelectedValue;
            ObjEntidadesClass.StatEntComercial = StatusComercialDropDownList.SelectedValue;

            //Estado
            ObjEntidadesClass.UFSIGLA = UfDropDownList.SelectedValue;

            //Cidade
            RecuperaDados_Select_Cidade();

            //Linha
            ObjEntidadesClass.USERLINHAPRODUTOLISTA = LinhaProdutoDropDownList.SelectedValue;

            //Produto
            RecuperaDados_Select_Produto();

            #region (Faturamento Medio / Removido conforme solicitação chamado 11828 Manuli  - Lizier 2016-04-01

            #endregion

            RecuperaDados_Select();//VendCod selecionados

            ListaEntidadeGridView.DataSource = ObjEntidadesClass.Consulta_Entidade_Carteira();
            ListaEntidadeGridView.DataBind();

            if (ListaEntidadeGridView.Rows.Count > 0)
            {
                // AplicaFootableGrid();
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
                    ObjEntidadesClass.VendCod += VendedoresSelect.Items[i].Value + ",";
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
            ObjVendedorClass.UsuCod = Session["usuario"].ToString();
            ObjVendedorClass.TodosCodigos = "S";
            VendedoresSelect.DataSource = ObjVendedorClass.Consulta_Vendedor();
            VendedoresSelect.DataTextField = "VendNome";
            VendedoresSelect.DataValueField = "VendCod";
            VendedoresSelect.DataBind();
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
            ObjEntidadesClass.EntCod = ((Label)((Control)sender).FindControl("EntCodLabel")).Text;

            /*Carrega em Session*/
            Session["clsEntidades"] = ObjEntidadesClass;

            //ClientesMultiView.Visible = false;

            if (ListaEntidadeGridView.Rows.Count > 0)
            {
                // AplicaFootableGrid();
            }

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
                ObjHistoricoCRMClass.Classificacao = ClassificacaoHiddenField.Value;
                ObjHistoricoCRMClass.Classificacao_Cliente_Alterar();
                
            }

            btnListar_Click(null, null);
        }
    }
}