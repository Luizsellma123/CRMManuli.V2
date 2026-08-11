using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.listas
{
    public partial class lstTransportadora : System.Web.UI.Page
    {
        funcoes mdlfuncoes = new funcoes();
        criptografia mdlCriptografia = new criptografia();
        SessionClass OBJSessao = new SessionClass();
        //Instancia classe pedido
        pedido novoPedido = new pedido();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            //Recupera objeto pedido da sessao do usuário
            novoPedido = (pedido)Session["pedidoNovo"];

            if (!IsPostBack)
            {
                LinkButton1.Visible = false;
                LinkButton2.Visible = false;

                Session["Origem"] = "Transportadora";
            }
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "0";
            TextBox2.Text = "0";
            ltlListaEntidade.Text = gerLista(14);
        }

        public string gerLista(int quant)
        {
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
            return linhasEntidade(indexPage, fimPage);
        }

        public string linhasEntidade(int indexPage, int fimPage)
        {
            string descLinhas = "";
            string strSQL = "";
            string codEmp = novoPedido.codigoEmpresa.ToString();
            string strconec;

            strSQL = sqlConsulta(indexPage, fimPage);

            if (codEmp != "99")
            {
                strconec = mdlfuncoes.getString().ToString();
            }
            else
            {
                strconec = mdlfuncoes.getString().ToString();
            }
            using (SqlConnection dbConnection = new SqlConnection(strconec))
            {
                using (SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection))
                {
                    dbConnection.Open();
                    using (SqlDataReader drEntidade = dbCommand.ExecuteReader())
                    {
                        if (drEntidade.HasRows)
                        {
                            //Inicio da tabela
                            descLinhas += "<table table class=\"table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed\"style=\"border-collapse:collapse;border-collapse: collapse; max-width: 100%\">";

                            //cabeçario da tabela
                            descLinhas += "<tr>";
                            descLinhas += "<th scope=\"col\">Selecione:</td>";
                            descLinhas += "<th scope=\"col\">Código:</td>";
                            descLinhas += "<th scope=\"col\">Nome:</td>";
                            descLinhas += "<th scope=\"col\">Fantasia:</td>";
                            descLinhas += "<th scope=\"col\">CNPJ/CPF:</td>";
                            descLinhas += "</tr>";

                            while (drEntidade.Read())
                            {
                                descLinhas += "<td align=\"center\"><a href=\"../cadastros/cadPedidoPrincipal.aspx?indmnu=2&idEmp=" + mdlCriptografia.Criptografar(codEmp.ToString(), "#!$a36?@") + "&idTra=" + mdlCriptografia.Criptografar(drEntidade["CodigoClienteSAP"].ToString(), "#!$a36?@") + "&idOpe=" + mdlCriptografia.Criptografar(novoPedido.tipoOperacao.ToString(), "#!$a36?@") + "&idPed=" + mdlCriptografia.Criptografar(novoPedido.numeroPedido.ToString(), "#!$a36?@") + " \"><i class=\"fa fa-check-circle-o fa-2x\"></i></a></td>";
                                descLinhas += "<td>" + drEntidade["CodigoClienteSAP"] + "</td>";
                                descLinhas += "<td>" + drEntidade["NomeCliente"] + "</td>";
                                descLinhas += "<td>" + drEntidade["NomeFantasia"] + "</td>";
                                descLinhas += "<td>" + drEntidade["CNPJ"] + "</td>";
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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ltlListaEntidade.Text = gerLista(-14);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            ltlListaEntidade.Text = gerLista(+14);
        }

        public int numergoRegistros()
        {
            int numPad = 0;
            string codEmp = novoPedido.codigoEmpresa.ToString();

            if (txtFiltroEntCod.Text == "" || txtFiltroEntCod.Text == null)
            {
                numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select count(*) as CNT from CRM_CLIENTE where TipoCliente='S'", "numergoRegistros")).ToString());
            }
            else
            {
                int tipoConsulta = Convert.ToInt32(drpEntCod.SelectedItem.Value);
                string valorConsulta = txtFiltroEntCod.Text;
                if (tipoConsulta == 1)
                {

                    numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select count(*) as CNT from CRM_CLIENTE where TipoCliente='S' and NomeCliente like '%" + valorConsulta + "%'", "numergoRegistros")).ToString());

                }
                else
                {
                    numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select * from CRM_CLIENTE where TipoCliente='S' and CodigoClienteSAP like '%" + valorConsulta + "%'", "numergoRegistros")).ToString());
                }
            }
            return numPad;
        }

        public string sqlConsulta(int indexPage, int fimPage)
        {
            string strSQL = "";
            string codEmp = novoPedido.codigoEmpresa.ToString();

            if (txtFiltroEntCod.Text == "" || txtFiltroEntCod.Text == null)
            {
                strSQL = "select CodigoClienteSAP, NomeCliente, NomeFantasia, CNPJ, REG from ";
                strSQL += "( ";
                strSQL += "select CodigoClienteSAP, NomeCliente, NomeFantasia, CNPJ, ";
                strSQL += "ROW_NUMBER() OVER(ORDER BY CodigoClienteSAP) as reg from ";
                strSQL += "CRM_CLIENTE where TipoCliente = 'S' and NomeCliente like '%%' ";
                strSQL += ") a WHERE reg between " + indexPage + " and " + fimPage + "";
            }
            else
            {
                int tipoConsulta = Convert.ToInt32(drpEntCod.SelectedItem.Value);
                string valorConsulta = txtFiltroEntCod.Text;
                if (tipoConsulta == 1)
                {
                    strSQL = "select CodigoClienteSAP, NomeCliente, NomeFantasia, CNPJ, REG from ";
                    strSQL += "( ";
                    strSQL += "select CodigoClienteSAP, NomeCliente, NomeFantasia, CNPJ, ";
                    strSQL += "ROW_NUMBER() OVER(ORDER BY CodigoClienteSAP) as reg from ";
                    strSQL += "CRM_CLIENTE where TipoCliente = 'S' and NomeCliente like '%" + valorConsulta + "%' ";
                    strSQL += ") a WHERE reg between " + indexPage + " and " + fimPage + "";
                }
                else
                {
                    strSQL = "select CodigoClienteSAP, NomeCliente, NomeFantasia, CNPJ, REG from ";
                    strSQL += "( ";
                    strSQL += "select CodigoClienteSAP, NomeCliente, NomeFantasia, CNPJ, ";
                    strSQL += "ROW_NUMBER() OVER(ORDER BY CodigoClienteSAP) as reg from ";
                    strSQL += "CRM_CLIENTE where TipoCliente = 'S' and CodigoClienteSAP like '%" + valorConsulta + "%' ";
                    strSQL += ") a WHERE reg between " + indexPage + " and " + fimPage + "";
                }
            }
            return strSQL;
        }
    }
}