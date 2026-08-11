using System;
using System.Collections.Generic;
using System.IO;
using VendasWeb.classes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;


namespace VendasWeb.documentos
{
    public partial class FrmDocumentos : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        clsDocumentosWeb ObjDocumentosWeb = new clsDocumentosWeb();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                ObjDocumentosWeb.Usucod = Session["usuario"].ToString();
                ObjDocumentosWeb.UserDocumentoID = 0;


                DocumentoGridView.DataSource = ObjDocumentosWeb.Mostra_Documento_Usuario();
                DocumentoGridView.DataBind();
            }


        }

        protected void SelecionarButton_Click(object sender, EventArgs e)
        {

            //Pegando Caminho do Arquivo(para teste tirar o + "\\" +
            string DocEntPathArq = Server.MapPath("~") + "\\" + ((Label)((Control)sender).FindControl("UrlLabel")).Text;


            //Lendo e Criando arquivo para Download
            System.IO.FileStream fs = new System.IO.FileStream(DocEntPathArq, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] ar = new byte[(int)fs.Length];
            fs.Read(ar, 0, (int)fs.Length);
            fs.Close();

            //Pegando nome do Arquivo
            string fileName = Path.GetFileName(fs.Name);

            //Enviando requisicao de Download
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.ContentType = "application/octectstream";
            Response.BinaryWrite(ar);
            Response.End();
        }

    }
}