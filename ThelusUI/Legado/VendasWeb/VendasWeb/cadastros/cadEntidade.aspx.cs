using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.cadastros
{
    
    public partial class cadEntidade : System.Web.UI.Page
    {
        funcoes mdlfuncs = new funcoes();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Verifica se usuário esta logado
            int varmenu = Convert.ToInt32(Request.QueryString["indmnu"]);
            if (Session["usuario"] == null && varmenu != 0 && varmenu < 99)
            {
                //Redireciona para tela de login
                Response.Redirect("~/Default.aspx");
            }
            
            
            if (!IsPostBack)
            {
                string codEnt = Request.QueryString["idEnt"];

                carregaCabecario(codEnt);
                
                carregaContato(codEnt);

                carregaEmail(codEnt);
            }
        }

        public void carregaContato(string codEnt) { 
            
            string strSQL="";
            string descLinhas = "";
            strSQL += "select EntFoneSeq, EntFoneTipo, EntFoneDDI, EntFoneDDD, EntFoneNum from ENT_FONE where EntCod =" + codEnt.ToString() + ";";

            using (SqlConnection dbConnection = new SqlConnection(mdlfuncs.getString().ToString()))
            {
                SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection);

                dbCommand.Connection.Open();

                SqlDataReader drFone = dbCommand.ExecuteReader();


                //Inicio da tabela
                descLinhas += "<table class=\"lstTabela\">";

                //cabeçario da tabela
                descLinhas += "<tr class=\"tabLstCab\" ><td colspan=\"4\" align=\"center\">Dados Telefones</td>";
                descLinhas += "<td align=\"center\"><a href=\"../cadastros/cadEntFone.aspx?indmnu=3&ident=" + codEnt.ToString() + "\" class=\"imgeditent\"><img src=\"../imagens/adiciona.png\" alt=\"Alteração\" border=\"0\" /></a></td></tr>";
                descLinhas += "<tr class=\"tabLstCab\">";
                descLinhas += "<td>Edição:</td>";
                descLinhas += "<td>Tipo:</td>";
                descLinhas += "<td>DDI:</td>";
                descLinhas += "<td>DDD:</td>";
                descLinhas += "<td>Número:</td>";
                descLinhas += "</tr>";
                while (drFone.Read())
                {
                    descLinhas += "<td class=\"edicao\"><a href=\"../cadastros/cadEntFone.aspx?indmnu=3&ident=" + codEnt.ToString() + "&idFoneSeq=" + drFone["EntFoneSeq"].ToString() + "\" class=\"imgedit\"><img src=\"../imagens/edit.png\" alt=\"Alteração\" border=\"0\" /></a></td>";
                    descLinhas += "<td>" + drFone["EntFoneTipo"] + "</td>";
                    descLinhas += "<td>" + drFone["EntFoneDDI"] + "</td>";
                    descLinhas += "<td>" + drFone["EntFoneDDD"] + "</td>";
                    descLinhas += "<td>" + drFone["EntFoneNum"] + "</td>";
                    descLinhas += "</tr>";
                }
                drFone.Close();

                //Fim tabela
                descLinhas += "</table><br />";
                ltlContatoEntidade.Text = descLinhas;
            }
            
        }

        public void carregaEmail(string codEnt) {
            string strSQL = "";
            string descLinhas = "";
            strSQL += "select EntWebTipo, EntWebEMail, EntWebWWW, EntWebEMailPrinc from ENT_WEB where EntCod =" + codEnt.ToString() + ";";

            using (SqlConnection dbConnection = new SqlConnection(mdlfuncs.getString().ToString()))
            {
                SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection);

                dbCommand.Connection.Open();

                SqlDataReader drFone = dbCommand.ExecuteReader();

                //Inicio da tabela
                descLinhas += "<table class=\"lstTabela\">";

                //cabeçario da tabela
                descLinhas += "<tr class=\"tabLstCab\" ><td colspan=\"4\" align=\"center\">Dados WEB</td>";
                descLinhas += "<td align=\"center\"><a href=\"../cadastros/cadFoneaspx?indmnu=3&ident=" + codEnt.ToString() + "\" class=\"imgeditent\"><img src=\"../imagens/adiciona.png\" alt=\"Alteração\" border=\"0\" /></a></td></tr>";
                descLinhas += "<tr class=\"tabLstCab\">";
                descLinhas += "<td>Edição:</td>";
                descLinhas += "<td>Tipo:</td>";
                descLinhas += "<td>Email:</td>";
                descLinhas += "<td>Pagina:</td>";
                descLinhas += "<td>Princial:</td>";
                descLinhas += "</tr>";
                while (drFone.Read())
                {
                    descLinhas += "<td class=\"edicao\"><a href=\"../cadastros/cadEntFone.aspx?indmnu=3&ident=" + codEnt.ToString() + "\" class=\"imgedit\"><img src=\"../imagens/edit.png\" alt=\"Alteração\" border=\"0\" /></a></td>";
                    descLinhas += "<td>" + drFone["EntWebTipo"] + "</td>";
                    descLinhas += "<td>" + drFone["EntWebEMail"] + "</td>";
                    descLinhas += "<td>" + drFone["EntWebWWW"] + "</td>";
                    descLinhas += "<td>" + drFone["EntWebEMailPrinc"] + "</td>";
                    descLinhas += "</tr>";
                }

                drFone.Close();

                //Fim tabela
                descLinhas += "</table><br />";
                ltlWebEntidade.Text = descLinhas;
            }
                    
        }

        public void carregaCabecario(string codEnt) {

            string strSQL = "";

            strSQL += "select EntTexto, EntTextoHist, EntCod, EntNome, EntNomeFant, EntCpfCgc from ENTIDADE where EntCod =" + codEnt.ToString() + ";";

            using (SqlConnection dbConnection = new SqlConnection(mdlfuncs.getString().ToString()))
            {
                SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection);

                dbConnection.Open();

                SqlDataReader drEntidade = dbCommand.ExecuteReader();

                if (drEntidade.Read())
                {
                    lblDescNome.Text = drEntidade["Entnome"].ToString();
                    lblDescFantasia.Text = drEntidade["EntNomeFant"].ToString();
                    lblDescCnpj.Text = drEntidade["EntCpfCgc"].ToString();
                    txtHistorico.Text = drEntidade["EntTextoHist"].ToString();
                    txtTextoLivre.Text = drEntidade["EntTexto"].ToString();
                    txtIDEntidade.Text = codEnt.ToString();
                }
                drEntidade.Close();

            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location=\"../listas/lstEntidade.aspx?indmnu=3&idEnt=" + txtIDEntidade.Text.ToString() + "\";</script>");
        }


    }
}
