using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VendasWeb.cadastros
{
    public partial class cadEntFone : System.Web.UI.Page
    {
        funcoes mdlfuncoes = new funcoes();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {           
            //Valida Acesso
            OBJSessao.ValidaAcesso();


            if (!IsPostBack)
            {
                string codEnt = Request.QueryString["idEnt"];
                carregaCabecario(codEnt);

                if (Request.QueryString["idFoneSeq"] != null)
                    carregaDados(codEnt, Convert.ToInt32(Request.QueryString["idFoneSeq"]));
            }
        }

        public void carregaCabecario(string codEnt)
        {
            string strSQL = "";

            strSQL += "select EntTexto, EntTextoHist, EntCod, EntNome, EntNomeFant, EntCpfCgc from ENTIDADE where EntCod =" + codEnt.ToString() + ";";

            using (SqlConnection dbConnection = new SqlConnection(mdlfuncoes.getString().ToString()))
            {
                using (SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection))
                {
                    dbConnection.Open();
                    using (SqlDataReader drEntidade = dbCommand.ExecuteReader())
                    {
                        if (drEntidade.Read())
                        {
                            lblDescNome.Text = drEntidade["Entnome"].ToString();
                            lblDescFantasia.Text = drEntidade["EntNomeFant"].ToString();
                            lblDescCnpj.Text = drEntidade["EntCpfCgc"].ToString();
                            txtIDEntidade.Text = codEnt.ToString();
                        }
                    }
                }
            }
        }

        public void carregaDados(string entCod, int entCodSeq) 
        { 
            string strSQL = "";

            strSQL += "select EntFoneTipo, EntFoneDDD, EntFoneDDI, EntFoneNum, EntFoneRamalBip, EntFonePrinc from ENT_FONE WHERE EntCod =" + entCod.ToString() + " and EntFoneSeq = " + entCodSeq.ToString() + ";";

            using (SqlConnection dbConnection = new SqlConnection(mdlfuncoes.getString().ToString()))
            {
                using (SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection))
                {
                    dbConnection.Open();

                    using (SqlDataReader drFones = dbCommand.ExecuteReader())
                    {
                        if (drFones.Read())
                        {
                            drpTipo.SelectedValue = drFones["EntFoneTipo"].ToString();
                            txtDDI.Text = drFones["EntFoneDDI"].ToString();
                            txtDDD.Text = drFones["EntFoneDDD"].ToString();
                            txtNumero.Text = drFones["EntFoneNum"].ToString();
                            txtRamal.Text = drFones["EntFoneRamalBip"].ToString();
                            drpPrincipal.SelectedItem.Text = drFones["EntFonePrinc"].ToString();
                        }
                    }
                }
            }        
        }

        protected void btnCancelar_Click1(object sender, EventArgs e)
        {
            Response.Write("<script>window.location=\"../cadastros/cadEntidade.aspx?indmnu=3&idEnt=" + txtIDEntidade.Text.ToString() + "\";</script>");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            string strSQL = "";
            int maxVal = 0;
            Boolean confirm;

            strSQL = "select max(EntFoneSeq) as max from ENT_FONE Where EntCod ='" + txtIDEntidade.Text.ToString() + "';";
            maxVal = Convert.ToInt32(mdlfuncoes.ExecutaSqlReader(strSQL, "btnSalvar_Click CadEntFone"));
            maxVal++;
            
            strSQL = "INSERT INTO ENT_FONE (EntCod, EntFoneSeq, EntFoneTipo, EntFoneDDI, ";
		    strSQL += "EntFoneDDD, EntFoneNum, EntFoneRamalBip, ";
		    strSQL += "EntFonePrinc) ";
            strSQL += "values ('" + txtIDEntidade.Text.ToString() + "', '" + maxVal.ToString() + "', '" + drpTipo.SelectedItem.Value.ToString() + "', '" + txtDDI.Text.ToString() + "' ";
            strSQL += ",'" + txtDDD.Text.ToString() + "', '" + txtNumero.Text.ToString() + "', '" + txtRamal.Text.ToString() + "' ";
            strSQL += ",'" + drpPrincipal.SelectedItem.Value.ToString() + "')";

            confirm = mdlfuncoes.ExecutaSQL(strSQL);

            if(confirm == true)
                Response.Write("<script>window.location=\"../cadastros/cadEntidade.aspx?indmnu=3&idEnt=" + txtIDEntidade.Text.ToString() + "\";</script>");
        }
    }
}