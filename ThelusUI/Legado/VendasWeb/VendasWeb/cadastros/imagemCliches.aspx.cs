using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.cadastros
{
    public partial class imagemCliches : System.Web.UI.Page
    {
        funcoes mdlfuncsMan = new funcoes();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strSQL;
            string codigoProduto;

            codigoProduto = Request.QueryString["codProd"];

            strSQL = "select FP.ProdFoto, P.ProdNome from FOTO_PROD FP, Produto P  where P.ProdCodEstr ='" + codigoProduto + "' and FP.ProdCodEstr=P.ProdCodEstr ";

            using (SqlConnection dbConnection = new SqlConnection(mdlfuncsMan.getString().ToString()))
            {
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection);                

                SqlDataReader drDados = dbCommand.ExecuteReader();

                if (drDados.Read())
                {
                    //lblTitulo.Text = codigoProduto + " - " + drDados["ProdNome"];

                    Response.ContentType = "image/png";
                    Response.BinaryWrite((byte[])drDados["ProdFoto"]);
                }
                drDados.Close();
            }
        }
    }
}