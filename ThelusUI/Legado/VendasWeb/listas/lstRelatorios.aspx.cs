using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.listas
{
    public partial class lstRelatorios : System.Web.UI.Page
    {
        funcoes mdlfuncoes = new funcoes();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            ltlListaRelatorios.Text = linhasRelatorios();
        }

        public string linhasRelatorios()
        {
            string desclinhas = "";
            /*string strSQL = "";
            int cont = 0;*/

            desclinhas += "<table class=\"lstTabela\">";

            desclinhas += "<tr class=\"tabLstCab\">";
            desclinhas += "<td colspan=\"2\">Utilizar os relatórios da intranet</td>";
            desclinhas += "</tr>";
            desclinhas += "<tr>";
            desclinhas += "<td colspan=\"2\">Caso não tenham acesso ao relatório da intranet abrir uma ocorrência pela intranet que iremos configurar o direito.</td>";
            desclinhas += "</tr>";

            /*desclinhas += "<tr class=\"tabLstCab\">";
            desclinhas += "<td colspan=\"2\">Vendas:</td>";
            desclinhas += "</tr>";
            
            desclinhas += "<tr>";
            desclinhas += "<td class=\"extend\">Relatório Vendas</td>";
            desclinhas += "<td><a href=\"../telasRelatorio/parmRelatorioVendas.aspx?indmnu=3\" class=\"imgedit\"><img src=\"../imagens/seta_blue_right.png\" alt=\"Alteração\" border=\"0\" /></td>";
            desclinhas += "</tr>";

            desclinhas += "<tr>";
            desclinhas += "<td class=\"extend\">Tabela Preço</td>";
            desclinhas += "<td><a href=\"#\" class=\"imgedit\"><img src=\"../imagens/seta_blue_right.png\" alt=\"Alteração\" border=\"0\" /></td>";
            desclinhas += "</tr>";

            strSQL = "select COUNT(*) as CNT from GRP_X_USUARIO where UsuCod='" + Session["usuario"].ToString() + "' and GrpUsuCod='GERENCIAL'";
            cont = (int)Convert.ToInt32(mdlfuncoes.ExecutaSqlReader(strSQL, "linhasRelatorios"));

            if (cont > 0)
            {
                desclinhas += "<tr class=\"tabLstCab\">";
                desclinhas += "<td colspan=\"2\">Gerencial:</td>";
                desclinhas += "</tr>";

                desclinhas += "<tr>";
                desclinhas += "<td class=\"extend\">Tabela Dinamica</td>";
                desclinhas += "<td><a href=\"../telasRelatorio/parmRelatorioTabelaDinamicaFaturados.aspx?indmnu=4\" class=\"imgedit\"><img src=\"../imagens/seta_blue_right.png\" alt=\"Alteração\" border=\"0\" /></td>";
                desclinhas += "</tr>";
                
                //Tabela Dinamica base antiga
                desclinhas += "<tr>";
                desclinhas += "<td class=\"extend\">Tabela Dinamica (Base Antiga)</td>";
                desclinhas += "<td><a href=\"../telasRelatorio/parmRelatorioTabelaDinamicaFitasa.aspx?indmnu=4\" class=\"imgedit\"><img src=\"../imagens/seta_blue_right.png\" alt=\"Alteração\" border=\"0\" /></td>";
                desclinhas += "</tr>";
            }*/
            desclinhas += "</table>";

            return desclinhas;
        }
    }
}