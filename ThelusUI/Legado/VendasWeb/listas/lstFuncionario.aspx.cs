using System;
using System.Collections.Generic;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.listas
{
    public partial class lstFuncionario : System.Web.UI.Page
    {
        funcoes mdlfuncoes = new funcoes();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                int numPagina = 14;

                TextBox1.Text = "0";
                TextBox2.Text = "0";
                TextBox3.Text = "0";

                ltlListaFuncionario.Text = gerLista(numPagina);
            }
        }

        protected void btnListar_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "0";
            TextBox2.Text = "0";
            ltlListaFuncionario.Text = gerLista(14);  
        }

        public string gerLista(int quant)
        {
            int indexPage = 0;
            int fimPage = 0;
            int numFunc = 0;

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

            if (txtFiltroFunc.Text == "" || txtFiltroFunc.Text == null)
            {
                numFunc = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("Select COUNT(*) from Funcionario where FuncStat = 'Trabalhando' ", "gerLista")).ToString());
            }
            else
            {
                int tipoConsulta = Convert.ToInt32(drpFunc.SelectedItem.Value);
                string valorConsulta = txtFiltroFunc.Text;
                if (tipoConsulta == 1)
                {
                    numFunc = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("Select COUNT(*) from Funcionario where FuncNome like '" + valorConsulta + "' and FuncStat = 'Trabalhando' ", "gerLista")).ToString());
                }
                else
                {
                    numFunc = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("Select COUNT(*) from Funcionario where FuncCod like '" + valorConsulta + "' and FuncStat = 'Trabalhando'", "gerLista")).ToString());
                }
            }

            if (fimPage >= numFunc)
            {
                LinkButton2.Visible = false;
            }
            else
            {
                LinkButton2.Visible = true;
            }
            return linhasFuncionario(indexPage, fimPage);
        }

        public string linhasFuncionario(int indexPage, int fimPage)
        {
            string descLinhas = "";
            string strSQL = "";
            if (txtFiltroFunc.Text == "" || txtFiltroFunc.Text == null)
            {
                strSQL += "select FuncCod, FuncNome, reg from (select FuncCod, FuncNome, ROW_NUMBER() OVER(ORDER BY FuncCod) as reg  from Funcionario) a WHERE reg between " + indexPage + " and " + fimPage + ";";
            }
            else
            {
                int tipoConsulta = Convert.ToInt32(drpFunc.SelectedItem.Value);
                string valorConsulta = txtFiltroFunc.Text;
                if (tipoConsulta == 1)
                {
                    strSQL += "select  FuncCod, FuncNome, reg from (select  FuncCod, FuncNome, ROW_NUMBER() OVER(ORDER BY FuncCod) as reg  from Funcionario WHERE FuncNome like'" + valorConsulta + "') a WHERE reg between " + indexPage + " and " + fimPage + ";";
                }
                else
                {
                    strSQL += "select  FuncCod, FuncNome, reg from (select  FuncCod, FuncNome, ROW_NUMBER() OVER(ORDER BY FuncCod) as reg  from Funcionario WHERE FuncCod like'" + valorConsulta + "') a WHERE reg between " + indexPage + " and " + fimPage + ";";
                }
            }
            using (SqlConnection dbConnection = new SqlConnection(mdlfuncoes.getString().ToString()))
            {
                using (SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection))
                {
                    dbConnection.Open();
                    using (SqlDataReader drFuncionario = dbCommand.ExecuteReader())
                    {
                        if (drFuncionario.HasRows)
                        {
                            //Inicio da tabela
                            descLinhas += "<table class=\"lstTabela\">";

                            //cabeçario da tabela
                            descLinhas += "<tr class=\"tabLstCab\">";
                            descLinhas += "<td></td>";
                            descLinhas += "<td>Codigo:</td>";
                            descLinhas += "<td>Nome:</td>";

                            descLinhas += "</tr>";

                            while (drFuncionario.Read())
                            {
                                descLinhas += "<td class=\"edicao\"><a href=\"../apontamento/apontamento.aspx?indmnu=3&FuncCod=" + drFuncionario["FuncCod"] + "&FuncNome=" + drFuncionario["FuncNome"] + "\" class=\"imgedit\"><img src=\"../imagens/edit.png\" alt=\"Selecao\" border=\"0\" /></a></td>";
                                descLinhas += "<td class=\"codigo\">" + drFuncionario["FuncCod"] + "</td>";
                                descLinhas += "<td class=\"extend\">" + drFuncionario["FuncNome"] + "</td>";
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
            ltlListaFuncionario.Text = gerLista(-14);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            ltlListaFuncionario.Text = gerLista(+14);
        }
    }
}