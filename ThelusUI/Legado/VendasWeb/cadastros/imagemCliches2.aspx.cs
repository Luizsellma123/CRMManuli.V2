using System;
using System.Collections.Generic;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace VendasWeb.cadastros
{
    public partial class imagemCliches2 : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        funcoes mdlfuncsMan = new funcoes();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            string strSQL;
            string codigoProduto;
            string caminho = "\\\\192.168.0.2\\Sap\\Imagens\\";
            string nome = "";

            codigoProduto = Request.QueryString["codProd"];

            //strSQL = "select FP.ProdFoto, P.ProdNome from FOTO_PROD FP, Produto P  where P.ProdCodEstr ='" + codigoProduto + "' and FP.ProdCodEstr=P.ProdCodEstr ";
            strSQL = "select ImagemProduto from CRM_PRODUTO where ImagemProduto<>'' and CodigoProdutoSAP='" + codigoProduto + "'";

            using (SqlConnection dbConnection = new SqlConnection(mdlfuncsMan.getString().ToString()))
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand(strSQL, dbConnection);
            
                SqlDataReader drDados = dbCommand.ExecuteReader();

                if (drDados.Read())
                {
                    nome = drDados["ImagemProduto"].ToString();

                    System.Drawing.Image img = Bitmap.FromFile(caminho + nome);

                    byte[] b = ConvertImageToByteArray(img, ImageFormat.Png);
                    Response.ContentType = "image/png";
                    Response.BinaryWrite(b);
                }
                drDados.Close();
            }
            
        }

        private static byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert, ImageFormat formatOfImage)
        {
            byte[] Ret;

            try
            {

                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }

            return Ret;
        }
    }
}