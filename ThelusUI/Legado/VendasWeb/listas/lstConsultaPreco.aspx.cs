using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VendasWeb.classes;
using System.Data.SqlClient;

namespace VendasWeb.listas
{
    public partial class lstConsultaPreco : System.Web.UI.Page
    {
        funcoes mdlfuncoes = new funcoes();
        criptografia mdlcriptografia = new criptografia();
        SessionClass OBJSessao = new SessionClass();
        //Instancia Objeto do tipo pedido
        pedido novoPedido = new pedido();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                drpEmpresa.DataSource = mdlfuncoes.Consulta_Empresa(Session["usuario"].ToString());
                drpEmpresa.DataTextField = "EmpNome";
                drpEmpresa.DataValueField = "EmpCod";
                drpEmpresa.DataBind();

                Session["Vendcod"] = mdlfuncoes.Consulta_CodVendedorAtivo_Usuario(Session["usuario"].ToString()).ToString();

                if ((string)Session["Vendcod"].ToString() == "0") {

                    Session["Vendcod"] = "0000002";
                }
            }
            
            novoPedido = (pedido)Session["pedidoNovo"];

            LinkButton1.Visible = false;
            LinkButton2.Visible = false;
        }        

        public string gerarLista(int quant) 
        {
            int indexPage = 0;
            int fimPage = 0;
            int numPad = 0;
            string auxemp = "";
            string strSQL;

            auxemp = drpEmpresa.SelectedItem.Value;

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

            strSQL = "select COUNT(*) as CNT from item_tab_pv_unid_Med itab INNER JOIN PRODUTO P ";
            strSQL += "ON P.ProdCodEstr=itab.ProdCodEstr INNER JOIN TAB_PV TP ON ";
            strSQL += "TP.EmpCod = itab.EmpCod and TP.TabPVCod=itab.TabPVCod ";
            strSQL += "INNER JOIN crk_WebRep_TabPreco_Vendedor('" + Session["vendcod"].ToString() + "','" + auxemp.ToString() + "') as TBVEN ON TBVEN.tabpvcod=TP.TabPVCod ";
            strSQL += "where P.ProdStat = 'Ativado' and P.ProdEntraPesqInternet='Sim' and itab.ProdUnidMedPos='1' ";
            strSQL += "and itab.TabPVData = (select MAX(itab2.TabPVData) from item_tab_pv_unid_Med itab2 where itab.TabPVCod=itab2.TabPVCod ";
            strSQL += "and itab.EmpCod=itab2.EmpCod and itab.ProdCodEstr=itab2.ProdCodEstr) and ";
            strSQL += "(TP.Tabpvdatafim>=getdate() or TP.Tabpvdatafim is null) ";
            
            if (txtFiltroProd.Text == "" || txtFiltroProd.Text == null)
            {
                strSQL += " and itab.EmpCod='" + auxemp.ToString() + "'";
                numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader(strSQL, "gerarLista")).ToString());                
            }
            else
            {
                int tipoConsulta = Convert.ToInt32(drpProdutos.SelectedItem.Value);
                string valorConsulta = txtFiltroProd.Text;
                if (tipoConsulta == 1)
                {
                    strSQL += " and itab.EmpCod='" + auxemp.ToString() + "' and P.ProdNome like '" + valorConsulta.ToString() + "%'";
                    numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader(strSQL, "gerarLista")).ToString());                    
                }
                else
                {
                    strSQL += " and itab.EmpCod='" + auxemp.ToString() + "' and P.ProdCodEstr like '" + valorConsulta.ToString() + "%'";
                    numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader(strSQL, "gerarLista")).ToString());                    
                }
            }

            if (fimPage >= numPad)
            {
                LinkButton2.Visible = false;
            }
            else
            {
                LinkButton2.Visible = true;
            }

            return linhasProduto(indexPage, fimPage);
        }

        public string linhasProduto(int indexPage, int fimPage) {

            string descLinhas = "";
            string codEmp = drpEmpresa.SelectedItem.Value;
            string strSQL = "";
            string auxemp = "";
            string strconec;

            auxemp = drpEmpresa.SelectedItem.Value;

            if (codEmp != "99")
            {
                auxemp = codEmp;
            }
            else
            {
                auxemp = "1";
            }
            
            strSQL += "select TabPVCod, TabPVNome, ProdCodEstr, ProdNome, ProdUnidMedCod, ItTabPVUnMedVal, reg FROM ( ";
            strSQL += "select itab.TabPVCod, TP.TabPVNome, P.ProdCodEstr, P.ProdNome, itab.ProdUnidMedCod, itab.ItTabPVUnMedVal,  ROW_NUMBER() OVER(ORDER BY P.ProdCodEstr) as reg from item_tab_pv_unid_Med itab INNER JOIN PRODUTO P ";
            strSQL += "ON P.ProdCodEstr=itab.ProdCodEstr INNER JOIN TAB_PV TP ON ";
            strSQL += "TP.EmpCod = itab.EmpCod and TP.TabPVCod=itab.TabPVCod ";
            strSQL += "INNER JOIN crk_WebRep_TabPreco_Vendedor('" + Session["vendCod"].ToString() + "','" + auxemp.ToString() + "') as TBVEN ON TBVEN.tabpvcod=TP.TabPVCod ";
            strSQL += "where P.ProdStat = 'Ativado' and P.ProdEntraPesqInternet='Sim' and itab.ProdUnidMedPos='1' ";
            strSQL += "and itab.TabPVData = (select MAX(itab2.TabPVData) from item_tab_pv_unid_Med itab2 where itab.TabPVCod=itab2.TabPVCod ";
            strSQL += "and itab.EmpCod=itab2.EmpCod and itab.ProdCodEstr=itab2.ProdCodEstr) and ";
            strSQL += "(TP.Tabpvdatafim>=getdate() or TP.Tabpvdatafim is null) ";

            if (txtFiltroProd.Text == "" || txtFiltroProd.Text == null)
            {

                if (codEmp != "99")
                {
                    strSQL += " and itab.EmpCod='" + codEmp.ToString() + "'";
                }
                else
                {
                    strSQL += " and itab.EmpCod='1'";
                }
            }
            else
            {
                int tipoConsulta = Convert.ToInt32(drpProdutos.SelectedItem.Value);
                string valorConsulta = txtFiltroProd.Text;
                if (tipoConsulta == 1)
                {
                    if (codEmp != "99")
                    {
                        strSQL += " and itab.EmpCod='" + codEmp.ToString() + "' and P.ProdNome like '" + valorConsulta.ToString() + "%'";
                    }
                    else
                    {
                        strSQL += " and itab.EmpCod='1' and P.ProdNome like '" + valorConsulta.ToString() + "%'";
                    }
                }
                else
                {
                    if (codEmp != "99")
                    {
                        strSQL += " and itab.EmpCod='" + codEmp.ToString() + "' and P.ProdCodEstr like '" + valorConsulta.ToString() + "%'";
                    }
                    else
                    {
                        strSQL += " and itab.EmpCod='1' and P.ProdCodEstr like '" + valorConsulta.ToString() + "%'";
                    }
                }
            }

            strSQL += ") a where reg BETWEEN '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";

            strconec = mdlfuncoes.getString().ToString();                 

            using (SqlConnection dbConnection = new SqlConnection(strconec))
            {
                using (SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection))
                {
                    dbConnection.Open();
                    using (SqlDataReader drProduto = dbCommand.ExecuteReader())
                    {
                        if (drProduto.HasRows)
                        {
                            //Inicio da tabela
                            descLinhas += "<table class=\"lstTabela\">";

                            //cabeçario da tabela
                            descLinhas += "<tr class=\"tabLstCab\">";
                            descLinhas += "<td width='200px'>Tabela:</td>";
                            descLinhas += "<td width='200px'>Código</td>";
                            descLinhas += "<td class=\"extend\">Nome:</td>";
                            descLinhas += "<td>Unidade:</td>";
                            descLinhas += "<td align=\"right\">Valor:</td>";
                            descLinhas += "</tr>";

                            while (drProduto.Read())
                            {
                                descLinhas += "<td>" + drProduto["TabPVNome"] + "</td>";
                                descLinhas += "<td>" + drProduto["ProdCodEstr"] + "</td>";
                                descLinhas += "<td>" + drProduto["ProdNome"] + "</td>";
                                descLinhas += "<td>" + drProduto["ProdUnidMedCod"] + "</td>";
                                descLinhas += "<td align=\"right\">" + Math.Round(Convert.ToDecimal(drProduto["ItTabPVUnMedVal"]), 2).ToString() + "</td>";
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
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "0";
            TextBox2.Text = "0";
            ltlListaProdutos.Text = gerarLista(14);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ltlListaProdutos.Text = gerarLista(-14);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            ltlListaProdutos.Text = gerarLista(14);
        }
    }
}