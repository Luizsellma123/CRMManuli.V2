using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;

namespace VendasWeb.listas
{
    public partial class lstPedido : System.Web.UI.Page
    {
        funcoes mdlfuncoes = new funcoes();
        GerencialVendas.PedidoClass PedidoClass = new GerencialVendas.PedidoClass();
        criptografia mdlCriptografia = new criptografia();
        GerencialVendas.clsEntidades ObjEntidadesClass = new GerencialVendas.clsEntidades();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            //Caso exista pedido pendente limpa
            Session.Remove("pedidoNovo");

            if (!IsPostBack)
            {
                CarregaDatas();

                drpEmpresa.DataSource = mdlfuncoes.Consulta_Empresa(Session["usuario"].ToString());
                drpEmpresa.DataTextField = "EmpNome";
                drpEmpresa.DataValueField = "EmpCod";
                drpEmpresa.DataBind();

                drpListFiltroStat.DataSource = mdlfuncoes.Consulta_ListaStatus_Ped_Venda();
                drpListFiltroStat.DataTextField = "StatPedVendaDescr";
                drpListFiltroStat.DataValueField = "StatPedVendaCod";               
                drpListFiltroStat.DataBind();

                drpListFiltroStat.Items.Insert(0, "Todos");
                drpListFiltroStat.SelectedIndex = 0;

                drpEmpresa.DataSource = mdlfuncoes.Consultar_Empresas();
                drpEmpresa.DataTextField = "EmpNome";
                drpEmpresa.DataValueField = "EmpCod";
                drpEmpresa.DataBind();

                txtFiltro.Text = "";
                //Verifica se esta vindo para da tela de Entidade
                if (Session["clsEntidades"] != null)
                {
                    /*Seta valores para consultar os pedidos da Entidade selecionada*/
                    ObjEntidadesClass = (GerencialVendas.clsEntidades)Session["clsEntidades"];
                    drpListFiltroPri.SelectedValue = "1";
                    drpEmpresa.SelectedValue = ObjEntidadesClass.EmpCod;
                    txtFiltro.Text = ObjEntidadesClass.EntCod;

                    
                }
                VoltarButton.Visible = true;

                Atualizar_Grid();

                /*TextBox1.Text = "0";
                TextBox2.Text = "0";
                TextBox3.Text = "0";*/

                //carregaCabecario();
            }
        }

        protected void CarregaDatas()
        {
            DateTime hoje = DateTime.Today;

            DateTime primeiroDiaDoAno = new DateTime(hoje.Year, 1, 1);

            DataInicialTextBox.Text = primeiroDiaDoAno.ToString("yyyy-MM-dd");

            DataFinalTextBox.Text = hoje.ToString("yyyy-MM-dd");
        }

        protected void btnFiltro_Click(object sender, EventArgs e)
        {
            /*TextBox1.Text = "0";
            TextBox2.Text = "0";
            TextBox3.Text = "0";
            ltlTabelaPedidos.Text = gerLista(14);*/

            Atualizar_Grid();
        }

        public void Atualizar_Grid()
        {
            PedidoClass.EmpCod = drpEmpresa.SelectedItem.Value;
            PedidoClass.PedVendaStatDescr = drpListFiltroStat.SelectedItem.Text;
            PedidoClass.PedVendaTipo = drpListFiltroTipo.SelectedItem.Text;
            PedidoClass.UsuCod = Session["usuario"].ToString();
            PedidoClass.Nivel = Convert.ToInt32(Session["nivel"].ToString());
            PedidoClass.valorConsulta = txtFiltro.Text;

            PedidoClass.DataInicial = DataInicialTextBox.Text == "" ? "" : Convert.ToDateTime(DataInicialTextBox.Text).ToString("yyyy-MM-dd");
            PedidoClass.DataFinal = DataFinalTextBox.Text == "" ? "" : Convert.ToDateTime(DataFinalTextBox.Text).ToString("yyyy-MM-dd");

            PedidosGridView.DataSource = PedidoClass.Lista_Pedidos();
            Session.Add("TEMP_SESSAO", PedidosGridView.DataSource);
            PedidosGridView.DataBind();
        }

        protected void PedidosGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataTable dtTable = (DataTable)Session["TEMP_SESSAO"];
                String coluna = e.SortExpression;

                if (coluna.Equals(Session["GuardaColuna"].ToString()))
                    coluna = coluna + " desc";

                Session["GuardaColuna"] = coluna;
                dtTable.DefaultView.Sort = coluna;
                this.PedidosGridView.DataSource = dtTable;
                this.PedidosGridView.DataBind();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ConsultaButton_Click(object sender, EventArgs e)
        {
            Session["EmpCod"] = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            Session["PedVendaNum"] = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;
            Session["Tipo"] = "Consulta";
            Response.Redirect("../cadastros/cadPedidoPrincipal.aspx?indmnu=2");
        }

        protected void ImprimirButton_Click(object sender, EventArgs e)
        {
            Session["EmpCod"] = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            Session["PedVendaNum"] = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;
            Session["Tipo"] = "Consulta";
            Response.Redirect("../relatorios/frmCopiaPedido.aspx?indmnu=2");
        }

        protected void ItemButton_Click(object sender, EventArgs e)
        {
            PedidoClass.EmpCod = ((Label)((Control)sender).FindControl("EmpCodLabel")).Text;
            PedidoClass.PedVendaNum = ((Label)((Control)sender).FindControl("PedVendaNumLabel")).Text;
            Atualizar_GridItem();
        }

        protected void PedidosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PedidosGridView.PageIndex = e.NewPageIndex;
            Atualizar_Grid();
        }

        protected void ItemGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ItemGridView.PageIndex = e.NewPageIndex;
            Atualizar_GridItem();
        }

        public void Atualizar_GridItem()
        {
            ItemGridView.DataSource = PedidoClass.Lista_Item_Pedido();
            Session.Add("TEMP_SESSAO", ItemGridView.DataSource);
            ItemGridView.DataBind();
        }

        protected void ItemGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                DataTable dtTable = (DataTable)Session["TEMP_SESSAO"];
                String coluna = e.SortExpression;

                if (coluna.Equals(Session["GuardaColuna"].ToString()))
                    coluna = coluna + " desc";

                Session["GuardaColuna"] = coluna;
                dtTable.DefaultView.Sort = coluna;
                this.ItemGridView.DataSource = dtTable;
                this.ItemGridView.DataBind();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

                                //descLinhas += "<td class=\"edicao\"><a href=\"../cadastros/cadPedidoPrincipal.aspx?indmnu=2&idEmp=" + mdlCriptografia.Criptografar(codEmp.ToString(), "#!$a36?@") + "&idPed=" + mdlCriptografia.Criptografar(drPedido["PedVendaNum"].ToString(), "#!$a36?@") + "&idOpe=" + mdlCriptografia.Criptografar("consulta", "#!$a36?@") + " \" class=\"imgedit\"><img src=\"../imagens/edit.png\" alt=\"Alteração\" border=\"0\" /></a></td>";
                                //descLinhas += "<td class=\"edicao\"><a href=\"../relatorios/frmCopiaPedido.aspx?indmnu=2&idEmp=" + mdlCriptografia.Criptografar(codEmp.ToString(), "#!$a36?@") + "&idPed=" + mdlCriptografia.Criptografar(drPedido["PedVendaNum"].ToString(), "#!$a36?@") + "&idOpe=" + mdlCriptografia.Criptografar("consulta", "#!$a36?@") + "  \" class=\"imgedit\"><img src=\"../imagens/print.png\" alt=\"Alteração\" border=\"0\" /></a></td>";

        /*public string gerLista(int quant)
        {

            //string descLinhas = "";
            int indexPage = 0;
            int fimPage = 0;
            int numPad = 0;

            if (quant > 0)
            {
                indexPage = Convert.ToInt32(TextBox1.Text);
                fimPage = Convert.ToInt32(TextBox1.Text);
                fimPage = fimPage + 14;
                indexPage = indexPage + 1;
            }
            else
            {
                indexPage = Convert.ToInt32(TextBox2.Text);
                fimPage = Convert.ToInt32(TextBox2.Text);
                indexPage = indexPage - 14;
                fimPage = fimPage - 1;
            }

            if (indexPage <= 0 || indexPage == 1)
            {
                LinkButton1.Visible = false;
            }
            else
            {
                LinkButton1.Visible = true;
            }
            
            numPad = numergoRegistros();     

            if (fimPage >= numPad)
            {
                LinkButton2.Visible = false;
            }
            else
            {
                LinkButton2.Visible = true;
            }

            return linhasPedidos(indexPage, fimPage);            
        }*/

        /*public string linhasPedidos(int indexPage, int fimPage)
        {

            string descLinhas = "";
            string strSQL = "";
            string codEmp = drpEmpresa.SelectedItem.Value;
            string strconec;

            strSQL = sqlConsulta(indexPage, fimPage);

            strconec = mdlfuncoes.getString().ToString();                 

            using (SqlConnection dbConnection = new SqlConnection(strconec))
            {
                using (SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection))
                {
                    dbConnection.Open();
                    using (SqlDataReader drPedido = dbCommand.ExecuteReader())
                    {
                        if (drPedido.HasRows)
                        {
                            //Inicio da tabela
                            descLinhas += "<table class=\"lstTabela\">";

                            //cabeçario da tabela
                            descLinhas += "<tr class=\"tabLstCab\">";
                            //descLinhas += "<td class=\"extend\">Titulo</td>";
                            //descLinhas += "<td>Descricao</td>"
                            descLinhas += "<td>Consulta:</td>";
                            descLinhas += "<td>C&oacute;pia:</td>";
                            descLinhas += "<td>Empresa:</td>";
                            descLinhas += "<td>Pedido:</td>";
                            descLinhas += "<td>Nome Cliente:</td>";
                            descLinhas += "<td>Nota Fiscal:</td>";
                            descLinhas += "<td>Faturamento:</td>";
                            descLinhas += "<td>Status:</td>";
                            descLinhas += "<td>Tipo:</td>";
                            descLinhas += "<td>N°. OC:</td>";
                            descLinhas += "</tr>";
                            
                            while (drPedido.Read())
                            {
                                descLinhas += "<td class=\"edicao\"><a href=\"../cadastros/cadPedidoPrincipal.aspx?indmnu=2&idEmp=" + mdlCriptografia.Criptografar(codEmp.ToString(), "#!$a36?@") + "&idPed=" + mdlCriptografia.Criptografar(drPedido["PedVendaNum"].ToString(), "#!$a36?@") + "&idOpe=" + mdlCriptografia.Criptografar("consulta", "#!$a36?@") + " \" class=\"imgedit\"><img src=\"../imagens/edit.png\" alt=\"Alteração\" border=\"0\" /></a></td>";
                                descLinhas += "<td class=\"edicao\"><a href=\"../relatorios/frmCopiaPedido.aspx?indmnu=2&idEmp=" + mdlCriptografia.Criptografar(codEmp.ToString(), "#!$a36?@") + "&idPed=" + mdlCriptografia.Criptografar(drPedido["PedVendaNum"].ToString(), "#!$a36?@") + "&idOpe=" + mdlCriptografia.Criptografar("consulta", "#!$a36?@") + "  \" class=\"imgedit\"><img src=\"../imagens/print.png\" alt=\"Alteração\" border=\"0\" /></a></td>";
                                descLinhas += "<td class=\"codigo\">" + drPedido["EmpCod"] + "</td>";
                                descLinhas += "<td>" + drPedido["PedVendaNum"] + "</td>";
                                descLinhas += "<td class=\"extend\">" + drPedido["PedVendaEntNomeDiv"] + "</td>";
                                descLinhas += "<td>" + drPedido["Nota"] + "</td>";
                                descLinhas += "<td>" + drPedido["NFDataEmis"] + "</td>";
                                descLinhas += "<td>" + drPedido["PedVendaStatDescr"] + "</td>";
                                descLinhas += "<td>" + drPedido["PedVendaTipo"] + "</td>";
                                descLinhas += "<td>" + drPedido["PedVendaNumPedEnt"] + "</td>";
                                descLinhas += "</tr>";
                            }
                            //Fim tabela
                            descLinhas += "</table><br />";
                        }
                    }
                }
            }
            TextBox1.Text = fimPage.ToString();
            TextBox2.Text = indexPage.ToString();
            return descLinhas;
        }*/

        /*public void carregaCabecario() {

            LinkButton1.Visible = false;
            LinkButton2.Visible = false;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ltlTabelaPedidos.Text = gerLista(-14);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            ltlTabelaPedidos.Text = gerLista(+14);
        }*/

        /*public int numergoRegistros() {
            string codEmp = drpEmpresa.SelectedItem.Value;
            string codStatus = drpListFiltroStat.SelectedItem.Text;
            string codTipo = drpListFiltroTipo.SelectedItem.Text;
            string auxEmp = "1";
            int numPad = 0;

            if (codStatus == "Todos")
                codStatus = "";

            if (codTipo == "Todos")
                codTipo = "";            

            if (txtFiltro.Text == "" || txtFiltro.Text == null)
            {
                if (codEmp != "99")
                {
                    if (Convert.ToInt32(Session["nivel"]) == 0)
                    {
                        numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN VEND_ENT VE ON PV.EntCod=VE.EntCod join Vendedor Ven on Ven.VendCod = Ve.VendCod JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum LEFT JOIN PED_VENDA_NOTA_FISCAL PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum  where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and Ven.UsuCod='" + Session["usuario"].ToString() + "' and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                    }
                    else {
                        numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum LEFT JOIN PED_VENDA_NOTA_FISCAL PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                    }
                }
                else
                {
                    if (Convert.ToInt32(Session["nivel"]) == 0)
                    {
                        numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN VEND_ENT VE ON PV.EntCod=VE.EntCod join Vendedor Ven on Ven.VendCod = Ve.VendCod JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum LEFT JOIN PED_VENDA_NOTA_FISCAL PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and Ven.UsuCod='" + Session["usuario"].ToString() + "'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                    }
                    else {
                        numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum LEFT JOIN PED_VENDA_NOTA_FISCAL PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum where PV.EmpCod='" + auxEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                    }
                }
            }
            else
            {
                int tipoConsulta = Convert.ToInt32(drpListFiltroPri.SelectedItem.Value);
                string valorConsulta = txtFiltro.Text;
                if (tipoConsulta == 1)
                {
                    if (codEmp != "99")
                    {
                        if (Convert.ToInt32(Session["nivel"]) == 0)
                        {
                            numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN VEND_ENT VE ON PV.EntCod=VE.EntCod join Vendedor Ven on Ven.VendCod = Ve.VendCod JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum LEFT JOIN PED_VENDA_NOTA_FISCAL PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and Ven.UsuCod='" + Session["usuario"].ToString() + "' and PV.PedVendaEntNomeDiv like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                        }
                        else {
                            numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum LEFT JOIN PED_VENDA_NOTA_FISCAL PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and PV.PedVendaEntNomeDiv like '" + valorConsulta + "'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Session["nivel"]) == 0)
                        {
                            numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN VEND_ENT VE ON PV.EntCod=VE.EntCod join Vendedor Ven on Ven.VendCod = Ve.VendCod JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum LEFT JOIN PED_VENDA_NOTA_FISCAL PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and Ven.UsuCod='" + Session["usuario"].ToString() + "' and PV.PedVendaEntNomeDiv like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                        }
                        else
                        {
                            numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum LEFT JOIN PED_VENDA_NOTA_FISCAL PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and PV.PedVendaEntNomeDiv like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                        }
                    }
                }
                else
                {
                    if (tipoConsulta == 2)
                    {
                        if (codEmp != "99")
                        {
                            if (Convert.ToInt32(Session["nivel"]) == 0)
                            {
                                numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN VEND_ENT VE ON PV.EntCod=VE.EntCod join Vendedor Ven on Ven.VendCod = Ve.VendCod JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum LEFT JOIN PED_VENDA_NOTA_FISCAL PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and Ven.UsuCod='" + Session["usuario"].ToString() + "' and PV.PedVendaNum like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                            }
                            else
                            {
                                numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum LEFT JOIN PED_VENDA_NOTA_FISCAL PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and PV.PedVendaNum like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(Session["nivel"]) == 0)
                            {
                                numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN VEND_ENT VE ON PV.EntCod=VE.EntCod join Vendedor Ven on Ven.VendCod = Ve.VendCod INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum LEFT JOIN PED_VENDA_NOTA_FISCAL PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and Ven.UsuCod='" + Session["usuario"].ToString() + "' and PV.PedVendaNum like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                            }
                            else
                            {
                                numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum LEFT JOIN PED_VENDA_NOTA_FISCAL PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and PV.PedVendaNum like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                            }
                        }
                    }
                    else 
                    {
                        if (Convert.ToInt32(Session["nivel"]) == 0)
                        {
                            numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN VEND_ENT VE ON PV.EntCod=VE.EntCod join Vendedor Ven on Ven.VendCod = Ve.VendCod INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum LEFT JOIN PED_VENDA_NOTA_FISCAL PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and Ven.UsuCod='" + Session["usuario"].ToString() + "' and PVF.NFNum like '%'+'" + valorConsulta + "'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                        }
                        else
                        {
                            numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum LEFT JOIN PED_VENDA_NOTA_FISCAL PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and PVF.NFNum like '%'+'" + valorConsulta + "'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                        }                    
                    }
                }
            }
            return numPad;        
        }*/

        /*public string sqlConsulta(int indexPage, int fimPage)
        {
            string codEmp = drpEmpresa.SelectedItem.Value;
            string codStatus = drpListFiltroStat.SelectedItem.Text;
            string codTipo = drpListFiltroTipo.SelectedItem.Text;
            string auxEmp = "1";
            string strSQL = "";

            if (codStatus == "Todos")
                codStatus = "";

            if (codTipo == "Todos")
                codTipo = "";

            if (txtFiltro.Text == "" || txtFiltro.Text == null)
            {
                if (codEmp != "99")
                {                    
                    if (Convert.ToInt32(Session["nivel"]) == 0)
                    {
                        strSQL = "select PedVendaNumPedEnt,EmpCod, PedVendaNum,  PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg, Nota, NFDataEmis from ( ";
                        strSQL += "select PV.PedVendaNumPedEnt, ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, ";
                        strSQL += "PV.PedVendaStatDescr, coalesce(PVF.NFNum, '') as Nota, CONVERT(VARCHAR(10), NF.NFDATAEMIS, 103) AS NFDATAEMIS FROM PED_VENDA PV INNER JOIN VEND_ENT VE ON PV.EntCod=VE.EntCod ";
                        strSQL += "join Vendedor Ven on Ven.VendCod = Ve.VendCod INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                        strSQL += "LEFT JOIN PED_VENDA_NOTA_FISCAL  PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum ";
                        strSQL += "LEFT JOIN NOTA_FISCAL NF ON NF.EmpCod=PVF.EmpCod AND NF.NFNum=PVF.NFNum AND NF.CtrlDFModForm=PVF.CtrlDFModForm AND NF.CtrlDFSerie=PVF.CtrlDFSerie ";
                        strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and Ven.UsuCod='" + Session["usuario"].ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                        strSQL += "and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                        strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                    }
                    else
                    {
                        strSQL = "select PedVendaNumPedEnt,EmpCod, PedVendaNum,  PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg, Nota, NFDataEmis from ( ";
                        strSQL += "select PV.PedVendaNumPedEnt, ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr, coalesce(PVF.NFNum, '') as Nota, CONVERT(VARCHAR(10), NF.NFDATAEMIS, 103) AS NFDATAEMIS FROM PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                        strSQL += "LEFT JOIN PED_VENDA_NOTA_FISCAL  PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum ";
                        strSQL += "LEFT JOIN NOTA_FISCAL NF ON NF.EmpCod=PVF.EmpCod AND NF.NFNum=PVF.NFNum AND NF.CtrlDFModForm=PVF.CtrlDFModForm AND NF.CtrlDFSerie=PVF.CtrlDFSerie ";
                        strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                        strSQL += "and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                        strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                    }
                }
                else
                {
                    if (Convert.ToInt32(Session["nivel"]) == 0)
                    {
                        auxEmp = "1";
                        strSQL = "select PedVendaNumPedEnt,EmpCod, PedVendaNum,  PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg, Nota, NFDataEmis from ( ";
                        strSQL += "select PV.PedVendaNumPedEnt, ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr, coalesce(PVF.NFNum, '') as Nota, CONVERT(VARCHAR(10), NF.NFDATAEMIS, 103) AS NFDATAEMIS FROM PED_VENDA PV INNER JOIN VEND_ENT VE ON PV.EntCod=VE.EntCod INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                        strSQL += "join Vendedor Ven on Ven.VendCod = Ve.VendCod LEFT JOIN PED_VENDA_NOTA_FISCAL  PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum ";
                        strSQL += "LEFT JOIN NOTA_FISCAL NF ON NF.EmpCod=PVF.EmpCod AND NF.NFNum=PVF.NFNum AND NF.CtrlDFModForm=PVF.CtrlDFModForm AND NF.CtrlDFSerie=PVF.CtrlDFSerie ";
                        strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and Ven.UsuCod='" + Session["usuario"].ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                        strSQL += "and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                        strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                    }
                    else
                    {
                        auxEmp = "1";
                        strSQL = "select PedVendaNumPedEnt,EmpCod, PedVendaNum,  PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg, Nota, NFDataEmis from ( ";
                        strSQL += "select PV.PedVendaNumPedEnt, ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr, coalesce(PVF.NFNum, '') as Nota, CONVERT(VARCHAR(10), NF.NFDATAEMIS, 103) AS NFDATAEMIS FROM PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                        strSQL += "LEFT JOIN PED_VENDA_NOTA_FISCAL  PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum ";
                        strSQL += "LEFT JOIN NOTA_FISCAL NF ON NF.EmpCod=PVF.EmpCod AND NF.NFNum=PVF.NFNum AND NF.CtrlDFModForm=PVF.CtrlDFModForm AND NF.CtrlDFSerie=PVF.CtrlDFSerie ";
                        strSQL += "where PV.EmpCod='" + auxEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                        strSQL += "and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                        strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                    }
                }
            }
            else
            {
                int tipoConsulta = Convert.ToInt32(drpListFiltroPri.SelectedItem.Value);
                string valorConsulta = txtFiltro.Text;
                if (tipoConsulta == 1)
                {
                    if (codEmp != "99")
                    {
                        if (Convert.ToInt32(Session["nivel"]) == 0)
                        {
                            strSQL = "select PedVendaNumPedEnt,EmpCod, PedVendaNum, PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg, Nota, NFDataEmis from ( ";
                            strSQL += "select PV.PedVendaNumPedEnt, ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr, coalesce(PVF.NFNum, '') as Nota, CONVERT(VARCHAR(10), NF.NFDATAEMIS, 103) AS NFDATAEMIS FROM PED_VENDA PV INNER JOIN VEND_ENT VE ON PV.EntCod=VE.EntCod INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                            strSQL += "join Vendedor Ven on Ven.VendCod = Ve.VendCod LEFT JOIN PED_VENDA_NOTA_FISCAL  PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum ";
                            strSQL += "LEFT JOIN NOTA_FISCAL NF ON NF.EmpCod=PVF.EmpCod AND NF.NFNum=PVF.NFNum AND NF.CtrlDFModForm=PVF.CtrlDFModForm AND NF.CtrlDFSerie=PVF.CtrlDFSerie ";
                            strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and Ven.UsuCod='" + Session["usuario"].ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                            strSQL += "and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and PV.PedVendaEntNomeDiv like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                            strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                        }
                        else
                        {
                            strSQL = "select PedVendaNumPedEnt,EmpCod, PedVendaNum,  PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg, Nota, NFDataEmis from ( ";
                            strSQL += "select PV.PedVendaNumPedEnt, ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr, coalesce(PVF.NFNum, '') as Nota, CONVERT(VARCHAR(10), NF.NFDATAEMIS, 103) AS NFDATAEMIS FROM PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                            strSQL += "LEFT JOIN PED_VENDA_NOTA_FISCAL  PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum ";
                            strSQL += "LEFT JOIN NOTA_FISCAL NF ON NF.EmpCod=PVF.EmpCod AND NF.NFNum=PVF.NFNum AND NF.CtrlDFModForm=PVF.CtrlDFModForm AND NF.CtrlDFSerie=PVF.CtrlDFSerie ";
                            strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                            strSQL += "and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and PV.PedVendaEntNomeDiv like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                            strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Session["nivel"]) == 0)
                        {
                            strSQL = "select PedVendaNumPedEnt,EmpCod, PedVendaNum, PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg, Nota, NFDataEmis from ( ";
                            strSQL += "select PV.PedVendaNumPedEnt, ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr, coalesce(PVF.NFNum, '') as Nota, CONVERT(VARCHAR(10), NF.NFDATAEMIS, 103) AS NFDATAEMIS FROM PED_VENDA PV INNER JOIN VEND_ENT VE ON PV.EntCod=VE.EntCod INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                            strSQL += "join Vendedor Ven on Ven.VendCod = Ve.VendCod LEFT JOIN PED_VENDA_NOTA_FISCAL  PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum ";
                            strSQL += "LEFT JOIN NOTA_FISCAL NF ON NF.EmpCod=PVF.EmpCod AND NF.NFNum=PVF.NFNum AND NF.CtrlDFModForm=PVF.CtrlDFModForm AND NF.CtrlDFSerie=PVF.CtrlDFSerie ";
                            strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and Ven.UsuCod='" + Session["usuario"].ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                            strSQL += "and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and PV.PedVendaEntNomeDiv like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                            strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                        }
                        else
                        {
                            strSQL = "select PedVendaNumPedEnt,EmpCod, PedVendaNum,  PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg, Nota, NFDataEmis from ( ";
                            strSQL += "select PV.PedVendaNumPedEnt, ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr, coalesce(PVF.NFNum, '') as Nota, CONVERT(VARCHAR(10), NF.NFDATAEMIS, 103) AS NFDATAEMIS FROM PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                            strSQL += "LEFT JOIN PED_VENDA_NOTA_FISCAL  PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum ";
                            strSQL += "LEFT JOIN NOTA_FISCAL NF ON NF.EmpCod=PVF.EmpCod AND NF.NFNum=PVF.NFNum AND NF.CtrlDFModForm=PVF.CtrlDFModForm AND NF.CtrlDFSerie=PVF.CtrlDFSerie ";
                            strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                            strSQL += "and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and PV.PedVendaEntNomeDiv like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                            strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                        }
                    }
                }
                else
                {
                    if (tipoConsulta == 2)
                    {
                        if (codEmp != "99")
                        {
                            if (Convert.ToInt32(Session["nivel"]) == 0)
                            {
                                strSQL = "select PedVendaNumPedEnt,EmpCod, PedVendaNum, PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg, Nota, NFDataEmis from ( ";
                                strSQL += "select PV.PedVendaNumPedEnt, ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr, coalesce(PVF.NFNum, '') as Nota, CONVERT(VARCHAR(10), NF.NFDATAEMIS, 103) AS NFDATAEMIS FROM PED_VENDA PV INNER JOIN VEND_ENT VE ON PV.EntCod=VE.EntCod INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                                strSQL += "join Vendedor Ven on Ven.VendCod = Ve.VendCod LEFT JOIN PED_VENDA_NOTA_FISCAL  PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum ";
                                strSQL += "LEFT JOIN NOTA_FISCAL NF ON NF.EmpCod=PVF.EmpCod AND NF.NFNum=PVF.NFNum AND NF.CtrlDFModForm=PVF.CtrlDFModForm AND NF.CtrlDFSerie=PVF.CtrlDFSerie ";
                                strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and Ven.UsuCod='" + Session["usuario"].ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                                strSQL += "and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and PV.PedVendaNum like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                                strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                            }
                            else
                            {
                                strSQL = "select PedVendaNumPedEnt,EmpCod, PedVendaNum,  PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg, Nota, NFDataEmis from ( ";
                                strSQL += "select PV.PedVendaNumPedEnt, ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr, coalesce(PVF.NFNum, '') as Nota, CONVERT(VARCHAR(10), NF.NFDATAEMIS, 103) AS NFDATAEMIS FROM PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                                strSQL += "LEFT JOIN PED_VENDA_NOTA_FISCAL  PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum ";
                                strSQL += "LEFT JOIN NOTA_FISCAL NF ON NF.EmpCod=PVF.EmpCod AND NF.NFNum=PVF.NFNum AND NF.CtrlDFModForm=PVF.CtrlDFModForm AND NF.CtrlDFSerie=PVF.CtrlDFSerie ";
                                strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                                strSQL += "and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and PV.PedVendaNum like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                                strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(Session["nivel"]) == 0)
                            {
                                strSQL = "select PedVendaNumPedEnt,EmpCod, PedVendaNum, PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg, Nota, NFDataEmis from ( ";
                                strSQL += "select PV.PedVendaNumPedEnt, ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr, coalesce(PVF.NFNum, '') as Nota, CONVERT(VARCHAR(10), NF.NFDATAEMIS, 103) AS NFDATAEMIS FROM PED_VENDA PV INNER JOIN VEND_ENT VE ON PV.EntCod=VE.EntCod INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                                strSQL += "join Vendedor Ven on Ven.VendCod = Ve.VendCod LEFT JOIN PED_VENDA_NOTA_FISCAL  PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum ";
                                strSQL += "LEFT JOIN NOTA_FISCAL NF ON NF.EmpCod=PVF.EmpCod AND NF.NFNum=PVF.NFNum AND NF.CtrlDFModForm=PVF.CtrlDFModForm AND NF.CtrlDFSerie=PVF.CtrlDFSerie ";
                                strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and Ven.UsuCod='" + Session["usuario"].ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                                strSQL += "and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and PV.PedVendaNum like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                                strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                            }
                            else
                            {
                                strSQL = "select PedVendaNumPedEnt,EmpCod, PedVendaNum,  PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg, Nota, NFDataEmis from ( ";
                                strSQL += "select PV.PedVendaNumPedEnt, ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr, coalesce(PVF.NFNum, '') as Nota, CONVERT(VARCHAR(10), NF.NFDATAEMIS, 103) AS NFDATAEMIS FROM PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                                strSQL += "LEFT JOIN PED_VENDA_NOTA_FISCAL  PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum ";
                                strSQL += "LEFT JOIN NOTA_FISCAL NF ON NF.EmpCod=PVF.EmpCod AND NF.NFNum=PVF.NFNum AND NF.CtrlDFModForm=PVF.CtrlDFModForm AND NF.CtrlDFSerie=PVF.CtrlDFSerie ";
                                strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                                strSQL += "and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and PV.PedVendaNum like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                                strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                            }
                        }
                    }
                    else 
                    {
                        if (Convert.ToInt32(Session["nivel"]) == 0)
                        {
                            strSQL = "select PedVendaNumPedEnt,EmpCod, PedVendaNum, PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg, Nota, NFDataEmis from ( ";
                            strSQL += "select PV.PedVendaNumPedEnt, ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr, coalesce(PVF.NFNum, '') as Nota, CONVERT(VARCHAR(10), NF.NFDATAEMIS, 103) AS NFDATAEMIS FROM PED_VENDA PV INNER JOIN VEND_ENT VE ON PV.EntCod=VE.EntCod INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                            strSQL += "join Vendedor Ven on Ven.VendCod = Ve.VendCod LEFT JOIN PED_VENDA_NOTA_FISCAL  PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum ";
                            strSQL += "LEFT JOIN NOTA_FISCAL NF ON NF.EmpCod=PVF.EmpCod AND NF.NFNum=PVF.NFNum AND NF.CtrlDFModForm=PVF.CtrlDFModForm AND NF.CtrlDFSerie=PVF.CtrlDFSerie ";
                            strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and Ven.UsuCod='" + Session["usuario"].ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                            strSQL += "and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and PVF.NFNum like '%'+'" + valorConsulta + "'  and PV.PedVendaTipo<>'Previsão') a ";
                            strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                        }
                        else
                        {
                            strSQL = "select PedVendaNumPedEnt,EmpCod, PedVendaNum,  PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg, Nota, NFDataEmis from ( ";
                            strSQL += "select PV.PedVendaNumPedEnt, ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr, coalesce(PVF.NFNum, '') as Nota, CONVERT(VARCHAR(10), NF.NFDATAEMIS, 103) AS NFDATAEMIS FROM PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                            strSQL += "LEFT JOIN PED_VENDA_NOTA_FISCAL  PVF ON PVF.EmpCod=PV.EmpCod and PVF.PedVendaNum=PV.PedVendaNum ";
                            strSQL += "LEFT JOIN NOTA_FISCAL NF ON NF.EmpCod=PVF.EmpCod AND NF.NFNum=PVF.NFNum AND NF.CtrlDFModForm=PVF.CtrlDFModForm AND NF.CtrlDFSerie=PVF.CtrlDFSerie ";
                            strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%'  ";
                            strSQL += "and PV.PedVendaStatDescr like '%" + codStatus.ToString() + "%' and PVF.NFNum like '%'+'" + valorConsulta + "'  and PV.PedVendaTipo<>'Previsão') a ";
                            strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                        }
                    }
                }
            }
            return strSQL;                
        }*/

        protected void btnInclusao_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location=\"../cadastros/cadPedido.aspx?indmnu=2\";</script>");
        }

        protected void VoltarButton_Click(object sender, EventArgs e)
        {
            if (Session["Retornar"] != null)
            {
                Response.Redirect(Session["Retornar"].ToString());
            }
            else
            {
                Response.Write("<script>alert(\"Não foi informado a página de origem. Entrar em contato com a TI\");</script>");
                //Response.Write("<script>window.location=\"FrmAbaPrincipal.aspx?indmnu=32\";</script>");
            }
        }
    }
}