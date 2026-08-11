using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting;
using Microsoft.Reporting.WebForms;


namespace VendasWeb.relatorios
{
    public partial class frmRelatorioVendas : System.Web.UI.Page
    {
        funcoes mdlfuncoes = new funcoes();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                string strSQL = "";

                strSQL = "SELECT empcod, VENDCOD, pedvendanum, statpedvendacod, pedvendadata, pedvendadataentrega, entcod, entnome, pedvendaqtdvol, statpedvendasist, pedvendavalmerc, pedvendavaltotal, ctrlcarganum, nfnum, nfdataemis ";
                strSQL += "FROM USER_WebRep_Relatorio_Vendas ";
                strSQL += "WHERE (empcod = '" + (string)Session["empresa"].ToString() + "' AND (VENDCOD = '" + (string)Session["vendedor"].ToString() + "') AND ";
                strSQL += "(statpedvendacod IN (" + (string)Session["status"].ToString() + ")) AND (CONVERT (date, (CASE WHEN statpedvendacod = '08' THEN nfDataEmis ELSE pedvendadata END), 103) BETWEEN '" + (string)Session["dataInicial"].ToString() + "' AND '" + (string)Session["dataFinal"].ToString() + "'));";

                using (SqlConnection dbConnection = new SqlConnection(mdlfuncoes.getString()))
                {
                    SqlDataAdapter DataAdapter = new SqlDataAdapter(strSQL, dbConnection);
                    DataSet dtDados = new DataSet();

                    DataAdapter.Fill(dtDados);

                    ReportDataSource rptDados = new ReportDataSource();

                    rptDados.Name = "relatorioVendas";
                    rptDados.DataMember = "USER_WebRep_Relatorio_Vendas";
                    rptDados.Value = dtDados.Tables[0];

                    this.rptVendas.LocalReport.DataSources.Add(rptDados);
                    this.rptVendas.DataBind();

                    Session.Remove("empresa");
                    Session.Remove("dataInicial");
                    Session.Remove("dataFinal");
                    Session.Remove("vendedor");
                    Session.Remove("status");
                }
            }
        }
    }
}