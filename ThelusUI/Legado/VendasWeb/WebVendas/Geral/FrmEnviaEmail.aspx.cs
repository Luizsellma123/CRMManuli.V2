using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


namespace VendasWeb.WebVendas.Geral
{
    public partial class FrmEnviaEmail : System.Web.UI.Page
    {
        Classes.EmailClass EmailClass = new Classes.EmailClass();


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (Session["byteViewer"] != null)
                {
                     
                    AnexoFileUpload.Visible = false;
                    AnexoLabel.Text = "Anexo: Pedido Anexado!";
                    
                }
            }

        }

        protected void CancelarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../listas/FrmListaPedidos.aspx?indmnu=2");
        }

        protected void EnviarEmailButton_Click(object sender, EventArgs e)
        {

      

            EmailClass.EmailRemetente = EmailRemetenteTextBox.Text;
            EmailClass.EmailDestinatario = EmailDestinatarioTextBox.Text;
            EmailClass.EmailCopia = EmailCopiaTextBox.Text;
            EmailClass.Assunto = AssuntoTextBox.Text;
            EmailClass.Descricao = DescricaoTextBox.Text;
            EmailClass.Descricao += @"<br><br><img src=""http://www.manulifitasa.com.br/assinatura/assinatura.png"" /> <br> Rua Emilio Romani 1200/1250 | Curitiba, PR – 81460-020 Brasil <br>";
            EmailClass.Descricao += EmailRemetenteTextBox.Text + " |  www.manulifitasa.com.br";
            
            
            
            
            if (Session["byteViewer"] != null)
            {
                byte[] byteViewer = (Byte[])Session["byteViewer"];

                FileStream fs = new FileStream(Server.MapPath("~") + "\\WebVendas\\Geral\\Anexos\\" + Session["usuario"].ToString() + "_" + Session["pedVendaNum"] + ".PDF", FileMode.Create);
                fs.Write(byteViewer, 0, byteViewer.Length);
                
                fs.Close();
                EmailClass.Anexo = fs.Name;
               
            }


            if (AnexoFileUpload.PostedFile != null && AnexoFileUpload.HasFile == true)
            {


                string nomeArquivo = "";

                //Pegamos informação do arquivo
                FileInfo infoarquivo = new FileInfo(AnexoFileUpload.PostedFile.FileName);
                EmailClass.Anexo = Server.MapPath("~") + "\\WebVendas\\Geral\\Anexos\\" + Session["usuario"].ToString() + "_" + infoarquivo.Name;



                FileInfo arquivoServidor = new FileInfo(EmailClass.Anexo);

                if (arquivoServidor.Exists == false)
                {

                    //Salvamos o arquivo
                    AnexoFileUpload.PostedFile.SaveAs(EmailClass.Anexo);

                    FileInfo arquivoPostado = new FileInfo(EmailClass.Anexo);
                    nomeArquivo = AnexoFileUpload.FileName.ToString();

                }

            }

            

            if (EmailClass.EnviarEmails() == true)
            {
                File.Delete(EmailClass.Anexo);
                Response.Write("<script>alert(\"E-mail enviado com Sucesso!\");</script>");
                Response.Redirect("../../listas/FrmListaPedidos.aspx?indmnu=2");
            }
            else
            {
                File.Delete(EmailClass.Anexo);
                Response.Write("<script>alert(\"Erro ao Enviar o E-mail!\");</script>");
            }

            

        }

    }
}