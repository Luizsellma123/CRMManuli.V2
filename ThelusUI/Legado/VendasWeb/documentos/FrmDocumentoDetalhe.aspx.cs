using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using VendasWeb.classes;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
namespace VendasWeb.documentos
{
    public partial class FrmDocumentoDetalhe : System.Web.UI.Page
    {
        SessionClass OBJSessao = new SessionClass();
        clsDocumentosWeb ObjDocumentosWeb = new clsDocumentosWeb();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();
        }

        protected void CancelarLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAdmDocumentos.aspx?indmnu=2");
        }

        protected void CarregarBannerButton_Click(object sender, EventArgs e)
        {
            DocumentoValidaLabel.Visible = false;

            #region CarregaDocumento
            if (DocumentoFileUpload.HasFile)
            {
                //Pegamos informação do arquivo
                string stipoArquivo = Path.GetExtension(DocumentoFileUpload.PostedFile.FileName).ToLower();
                string NomeArquivo = NomeTextBox.Text;
                string FileName = NomeArquivo + stipoArquivo;

                switch (stipoArquivo.ToUpper())
                {
                    default:
                        try
                        {

                            string[] arquivos = Directory.GetFiles(Server.MapPath("~") + "\\documentos\\ArquivosWeb\\", FileName);

                            if (arquivos.Count() == 0)
                            {

                                DocumentoFileUpload.SaveAs(Server.MapPath("~") + "\\documentos\\ArquivosWeb\\" + FileName);

                                ObjDocumentosWeb.Url = "documentos\\ArquivosWeb\\" + FileName;
                                ObjDocumentosWeb.Nome = NomeTextBox.Text;


                                //Salvando Documento
                                ObjDocumentosWeb.Insere_Documento();


                                Response.Redirect("FrmAdmDocumentos.aspx?indmnu=2");


                            }
                            else
                            {
                                DocumentoValidaLabel.Text = "Ja existe um arquivo chamado: " + DocumentoFileUpload.FileName;
                                DocumentoValidaLabel.ForeColor = System.Drawing.Color.Red;
                                DocumentoValidaLabel.Visible = true;

                            }




                        }
                        catch
                        {
                            DocumentoValidaLabel.Text = "Erro ao carregar o arquivo: " + DocumentoFileUpload.FileName;
                            DocumentoValidaLabel.ForeColor = System.Drawing.Color.Red;
                            DocumentoValidaLabel.Visible = true;
                        }
                        break;

                }

            }
            #endregion
        }
    }
}