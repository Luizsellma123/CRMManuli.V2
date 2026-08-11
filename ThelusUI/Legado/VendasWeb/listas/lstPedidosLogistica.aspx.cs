using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.listas
{
    public partial class lstPedidosLogistica : System.Web.UI.Page
    {
        funcoes mdlfuncoes = new funcoes();
        criptografia mdlCriptografia = new criptografia();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            //Caso exista pedido pendente limpa
            Session.Remove("pedidoNovo");

            if (!IsPostBack)
            {
                drpEmpresa.DataSource = mdlfuncoes.Consulta_Empresa(Session["usuario"].ToString());
                drpEmpresa.DataTextField = "EmpNome";
                drpEmpresa.DataValueField = "EmpCod";
                drpEmpresa.DataBind();

                TextBox1.Text = "0";
                TextBox2.Text = "0";
                TextBox3.Text = "0";

                carregaCabecario();
            }
        }

        public void carregaCabecario()
        {
            LinkButton1.Visible = false;
            LinkButton2.Visible = false;
        }

        public string gerLista(int quant)
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

        }

        public string linhasPedidos(int indexPage, int fimPage)
        {
            string descLinhas = "";
            string strSQL = "";
            string codEmp = drpEmpresa.SelectedItem.Value;

            strSQL = sqlConsulta(indexPage, fimPage);

            using (SqlConnection dbConnection = new SqlConnection(mdlfuncoes.getString().ToString()))
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
                            descLinhas += "<td>Consulta:</td>";
                            descLinhas += "<td>C&oacute;pia:</td>";
                            descLinhas += "<td>Empresa:</td>";
                            descLinhas += "<td>Pedido:</td>";
                            descLinhas += "<td>Nome Cliente:</td>";
                            descLinhas += "<td>Status:</td>";
                            descLinhas += "<td>Tipo:</td>";
                            descLinhas += "</tr>";

                            while (drPedido.Read())
                            {
                                descLinhas += "<td class=\"edicao\"><a href=\"../logistica/alteracaoPedido.aspx?indmnu=3&idEmp=" + mdlCriptografia.Criptografar(codEmp.ToString(), "#!$a36?@") + "&idPed=" + mdlCriptografia.Criptografar(drPedido["PedVendaNum"].ToString(), "#!$a36?@") + "&idOpe=" + mdlCriptografia.Criptografar("consulta", "#!$a36?@") + " \" class=\"imgedit\"><img src=\"../imagens/edit.png\" alt=\"Alteração\" border=\"0\" /></a></td>";
                                descLinhas += "<td class=\"edicao\"><a href=\"../relatorios/frmCopiaPedido.aspx?indmnu=3&idEmp=" + mdlCriptografia.Criptografar(codEmp.ToString(), "#!$a36?@") + "&idPed=" + mdlCriptografia.Criptografar(drPedido["PedVendaNum"].ToString(), "#!$a36?@") + "&idOpe=" + mdlCriptografia.Criptografar("consulta", "#!$a36?@") + "  \" class=\"imgedit\"><img src=\"../imagens/print.png\" alt=\"Alteração\" border=\"0\" /></a></td>";
                                descLinhas += "<td class=\"codigo\">" + drPedido["EmpCod"] + "</td>";
                                descLinhas += "<td>" + drPedido["PedVendaNum"] + "</td>";
                                descLinhas += "<td class=\"extend\">" + drPedido["PedVendaEntNomeDiv"] + "</td>";
                                descLinhas += "<td class=\"extend\">" + drPedido["PedVendaStatDescr"] + "</td>";
                                descLinhas += "<td>" + drPedido["PedVendaTipo"] + "</td>";
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

        public int numergoRegistros()
        {
            string codEmp = drpEmpresa.SelectedItem.Value;
            string codStatus = drpListFiltroStat.SelectedItem.Text;
            string codTipo = drpListFiltroTipo.SelectedItem.Text;
            string auxEmp = "1";
            int numPad = 0;

            if (codStatus == "Todos")
            {
                codStatus = "('Produção', 'Expedição')";
            }
            else 
            {
                codStatus = "('" + codStatus.ToString() + "')";
            }

            if (codTipo == "Todos")
                codTipo = "";

            if (txtFiltro.Text == "" || txtFiltro.Text == null)
            {
                if (codEmp != "99")
                {
                    numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr IN " + codStatus.ToString() + "  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());   
                }
                else
                {
                    numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum where PV.EmpCod='" + auxEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr IN " + codStatus.ToString() + "  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());   
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
                        numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr IN " + codStatus.ToString() + " and PV.PedVendaEntNomeDiv like '" + valorConsulta + "'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                    }
                    else
                    {
                        numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr IN " + codStatus.ToString() + " and PV.PedVendaEntNomeDiv like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());   
                    }
                }
                else
                {
                    if (codEmp != "99")
                    {
                        numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr IN " + codStatus.ToString() + " and PV.PedVendaNum like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());   
                    }
                    else
                    {
                        numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select COUNT(*) as CNT from PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' and PV.PedVendaStatDescr IN " + codStatus.ToString() + " and PV.PedVendaNum like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão'", "numergoRegistros")).ToString());
                    }
                }
            }

            return numPad;
        }

        public string sqlConsulta(int indexPage, int fimPage)
        {
            string codEmp = drpEmpresa.SelectedItem.Value;
            string codStatus = drpListFiltroStat.SelectedItem.Text;
            string codTipo = drpListFiltroTipo.SelectedItem.Text;
            string auxEmp = "1";
            string strSQL = "";

            if (codStatus == "Todos")
            {
                codStatus = "('Produção', 'Expedição')";
            }
            else
            {
                codStatus = "('" + codStatus.ToString() + "')";
            }

            if (codTipo == "Todos")
                codTipo = "";

            if (txtFiltro.Text == "" || txtFiltro.Text == null)
            {
                if (codEmp != "99")
                {
                    strSQL = "select EmpCod, PedVendaNum,  PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg from ( ";
                    strSQL += "select ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr FROM PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                    strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                    strSQL += "and PV.PedVendaStatDescr IN " + codStatus.ToString() + "  and PV.PedVendaTipo<>'Previsão') a ";
                    strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                }
                else
                {
                    auxEmp = "1";
                    strSQL = "select EmpCod, PedVendaNum,  PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg from ( ";
                    strSQL += "select ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr FROM PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                    strSQL += "where PV.EmpCod='" + auxEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                    strSQL += "and PV.PedVendaStatDescr IN " + codStatus.ToString() + "  and PV.PedVendaTipo<>'Previsão') a ";
                    strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
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
                        strSQL = "select EmpCod, PedVendaNum,  PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg from ( ";
                        strSQL += "select ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr FROM PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                        strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                        strSQL += "and PV.PedVendaStatDescr IN " + codStatus.ToString() + " and PV.PedVendaEntNomeDiv like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                        strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";   
                    }
                    else
                    {
                        auxEmp = "1";
                        strSQL = "select EmpCod, PedVendaNum,  PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg from ( ";
                        strSQL += "select ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr FROM PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                        strSQL += "where PV.EmpCod='" + auxEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                        strSQL += "and PV.PedVendaStatDescr IN " + codStatus.ToString() + " and PV.PedVendaEntNomeDiv like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                        strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";   
                    }
                }
                else
                {
                    if (codEmp != "99")
                    {
                        strSQL = "select EmpCod, PedVendaNum,  PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg from ( ";
                        strSQL += "select ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr FROM PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                        strSQL += "where PV.EmpCod='" + codEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                        strSQL += "and PV.PedVendaStatDescr IN " + codStatus.ToString() + " and PV.PedVendaNum like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                        strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";   
                    }
                    else
                    {
                        auxEmp = "1";
                        strSQL = "select EmpCod, PedVendaNum,  PedVendaEntNomeDiv, PedVendaTipo, PedVendaStatDescr, reg from ( ";
                        strSQL += "select ROW_NUMBER() OVER(ORDER BY PV.PedVendaNum DESC) as reg, PV.EmpCod, PV.PedVendaNum,  PV.PedVendaEntNomeDiv, PV.PedVendaTipo, PV.PedVendaStatDescr FROM PED_VENDA PV INNER JOIN PED_VENDA1 PV1 ON PV.EmpCod=PV1.EmpCod and PV.PedVendaNum=PV1.PedVendaNum ";
                        strSQL += "where PV.EmpCod='" + auxEmp.ToString() + "' and PV.PedVendaTipo like '%" + codTipo.ToString() + "%' ";
                        strSQL += "and PV.PedVendaStatDescr IN " + codStatus.ToString() + " and PV.PedVendaNum like '" + valorConsulta + "%'  and PV.PedVendaTipo<>'Previsão') a ";
                        strSQL += "WHERE reg between '" + indexPage.ToString() + "' and '" + fimPage.ToString() + "'";
                    }
                }
            }
            return strSQL;
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            ltlTabelaPedidos.Text = gerLista(+14);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ltlTabelaPedidos.Text = gerLista(-14);
        }

        protected void btnFiltro_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "0";
            TextBox2.Text = "0";
            TextBox3.Text = "0";
            ltlTabelaPedidos.Text = gerLista(+14);
        }
    }
}