using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VendasWeb.classes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;

namespace VendasWeb.documentos
{
    public partial class FrmAdmDocumentos : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        clsDocumentosWeb ObjDocumentosWeb = new clsDocumentosWeb();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                atualiza_Grid();
            }
        }

        protected void NovoDocumentoLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmDocumentoDetalhe.aspx?indmnu=2");
        }



        public void atualiza_Grid()
        {
            DocumentoGridView.DataSource = ObjDocumentosWeb.Mostra_Documento();
            DocumentoGridView.DataBind();
        }

        protected void RemoverButton_Click(object sender, EventArgs e)
        {
            ObjDocumentosWeb.UserDocumentoID = Convert.ToInt32(((Label)((Control)sender).FindControl("UserDocumentoIDLabel")).Text);
            ObjDocumentosWeb.Url = ((Label)((Control)sender).FindControl("UrlLabel")).Text;

            ObjDocumentosWeb.Deleta_Documento();

            //Apagnado arquivo do Diretorio
            string CaminhoLocal = Server.MapPath("~") + @"\" + ObjDocumentosWeb.Url;
            FileInfo fi = new System.IO.FileInfo(CaminhoLocal);
            try
            {
                fi.Delete();
            }
            catch (System.IO.IOException ex)
            {
                Console.WriteLine(ex.Message);
            }



            atualiza_Grid();
        }



    }

}