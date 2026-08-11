using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.classes;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendasWeb.listas
{
    public partial class lstconsulta : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        funcoes mdlFuncoes = new funcoes();

         protected void Page_Load(object sender, EventArgs e)
        {

            //Valida Acesso
            OBJSessao.ValidaAcesso();

            ltlListaConsulta.Text = linhasRelatorios();
        }

         public string linhasRelatorios()
         {
             string desclinhas = "";
             //string strSQL = "";
             //int cont = 0;

             desclinhas += "<table class=\"lstTabela\">";

             desclinhas += "<tr class=\"tabLstCab\">";
             desclinhas += "<td colspan=\"2\">Consulta:</td>";
             desclinhas += "</tr>";

             desclinhas += "<tr>";
             desclinhas += "<td class=\"extend\">Tabela de Preço</td>";
             desclinhas += "<td><a href=\"../listas/lstConsultaPreco.aspx?indmnu=7\" class=\"imgedit\"><img src=\"../imagens/seta_blue_right.png\" alt=\"Alteração\" border=\"0\" /></td>";
             desclinhas += "</tr>";

             desclinhas += "</table>";

             return desclinhas;
         }
    }
}