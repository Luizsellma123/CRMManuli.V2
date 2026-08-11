using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.listas
{
    public partial class lstEntidade : System.Web.UI.Page
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

                ltlListaEntidade.Text = gerLista(numPagina);
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

            if (txtFiltroEntCod.Text == "" || txtFiltroEntCod.Text == null)
            {
                numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select count(*) as CNT from ENTIDADE", "gerLista lstEntidade.aspx")).ToString());
            }
            else
            {
                int tipoConsulta = Convert.ToInt32(drpEntCod.SelectedItem.Value);
                string valorConsulta = txtFiltroEntCod.Text;
                if (tipoConsulta == 1)
                {
                    numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select count(*) as CNT from ENTIDADE where EntNome like'" + valorConsulta + "'", "gerLista lstEntidade.aspx")).ToString());
                }
                else
                {
                    numPad = Convert.ToInt32((mdlfuncoes.ExecutaSqlReader("select count(*) as CNT from ENTIDADE where EntCod like'" + valorConsulta + "'", "gerLista lstEntidade.aspx")).ToString());
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
            return linhasEntidade(indexPage, fimPage);
        }

        public string linhasEntidade(int indexPage, int fimPage)
        {
            string descLinhas = "";
            string strSQL = "";
            if (txtFiltroEntCod.Text == "" || txtFiltroEntCod.Text == null)
            {
                strSQL += "select EntCod, EntNome, EntNomeFant, EntCpfCgc,UsuCod, reg from (select ve.UsuCod,ent.EntCod, EntNome, EntNomeFant, EntCpfCgc, ROW_NUMBER() OVER(ORDER BY EntCod) as reg  from Entidade ent ";
                strSQL += " left join VEND_ENT vend ON vend.EntCod = ent.EntCod ";
                strSQL += " left join VENDEDOR ve ON ve.VendCod = vend.VendCod ";
                strSQL += " ) a WHERE reg between " + indexPage + " and " + fimPage + ";";
            }
            else
            {
                int tipoConsulta = Convert.ToInt32(drpEntCod.SelectedItem.Value);
                string valorConsulta = txtFiltroEntCod.Text;
                if (tipoConsulta == 1)
                {
                    strSQL += "select EntCod, EntNome, EntNomeFant, EntCpfCgc,UsuCod, reg from (select ve.UsuCod,ent.EntCod, EntNome, EntNomeFant, EntCpfCgc, ROW_NUMBER() OVER(ORDER BY EntCod) as reg  from Entidade ent ";
                    strSQL += " left join VEND_ENT vend ON vend.EntCod = ent.EntCod ";
                    strSQL += " left join VENDEDOR ve ON ve.VendCod = vend.VendCod ";
                    strSQL += " WHERE EntNome like'" + valorConsulta + "') a WHERE reg between " + indexPage + " and " + fimPage + ";";
                }
                else
                {
                    strSQL += "select EntCod, EntNome, EntNomeFant, EntCpfCgc,UsuCod, reg from (select ve.UsuCod,ent.EntCod, EntNome, EntNomeFant, EntCpfCgc, ROW_NUMBER() OVER(ORDER BY EntCod) as reg  from Entidade ent ";
                    strSQL += " left join VEND_ENT vend ON vend.EntCod = ent.EntCod ";
                    strSQL += " left join VENDEDOR ve ON ve.VendCod = vend.VendCod ";
                    strSQL += " WHERE EntCod like'" + valorConsulta + "') a WHERE reg between " + indexPage + " and " + fimPage + ";";
                }
            }
            using (SqlConnection dbConnection = new SqlConnection(mdlfuncoes.getString().ToString()))
            {
                using (SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection))
                {
                    dbConnection.Open();
                    using (SqlDataReader drEntidade = dbCommand.ExecuteReader())
                    {
                        if (drEntidade.HasRows)
                        {
                            //Inicio da tabela
                            descLinhas += "<table class=\"lstTabela\">";

                            //cabeçario da tabela
                            descLinhas += "<tr class=\"tabLstCab\">";
                            descLinhas += "<td>Edição:</td>";
                            descLinhas += "<td>Código:</td>";
                            descLinhas += "<td>Nome:</td>";
                            descLinhas += "<td>Fantasia:</td>";
                            descLinhas += "<td>CNPJ//CPF:</td>";
                            descLinhas += "</tr>";

                            while (drEntidade.Read())
                            {
                                descLinhas += "<td class=\"edicao\"><a href=\"../cadastros/cadEntidade.aspx?indmnu=3&idEnt=" + drEntidade["EntCod"] + "\" class=\"imgedit\"><img src=\"../imagens/edit.png\" alt=\"Alteração\" border=\"0\" /></a></td>";
                                descLinhas += "<td class=\"codigo\">" + drEntidade["EntCod"] + "</td>";
                                descLinhas += "<td class=\"extend\">" + drEntidade["EntNome"] + "</td>";
                                descLinhas += "<td class=\"extend\">" + drEntidade["EntNomeFant"] + "</td>";
                                descLinhas += "<td>" + drEntidade["EntCpfCgc"] + "</td>";
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
    }
}