using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace VendasWeb
{
    public class enviarEmail
    {



        public string EmailDestinatario { get; set; }
        public string EmailDestinatarioCopia { get; set; }
        public string EmailRemetente { get; set; }
        public string Remetente { get; set; }
        public string Descricao { get; set; }
        public string Texto { get; set; }
        public string EmailSenha { get; set; }

        public void enviaEmail(string titulo, string corpo, string emailpara)
        {

      
                MailMessage oEmail = new MailMessage();
                MailAddress sDe = new MailAddress(emailpara, "VendasWEB");

                oEmail.To.Add(emailpara); //DIGITE AQUI O E-MAIL PARA O QUAL SERÁ ENCAMINHADO O FORMULARIO
                oEmail.From = sDe;
                oEmail.Priority = MailPriority.Normal;
                oEmail.IsBodyHtml = false;
                oEmail.Subject = titulo.ToString();

                // Monta o corpo da mensagem a ser enviada
                // mensagem = new StringBuilder();
                //mensagem.Append("TESTE").Append(Environment.NewLine);
                //mensagem.Append("E-mail do Contato: " + txtEmail.Text + "").Append(Environment.NewLine);
                //mensagem.Append("Assunto: " + txtAssunto.Text + "").Append(Environment.NewLine);
                //mensagem.Append("Mensagem: " + txtMensagem.Text + "");

                //oEmail.Body = mensagem.ToString();
                oEmail.Body = corpo.ToString();

                SmtpClient oEnviar = new SmtpClient();
                oEnviar.Host = "smtp.manulifitasa.com.br"; //DIGITE AQUI O NOME DO SERVIDOR DE SMTP QUE VOCÊ IRA UTILIZAR
                oEnviar.Credentials = new System.Net.NetworkCredential("naoresponda@manulifitasa.com.br", "raiden"); // DIGITE UM E-MAIL VÁLIDO E UMA SENHA PARA AUTENTICACAO NO SERVIDOR SMTP
                oEnviar.Port = 587;
                oEnviar.Send(oEmail);
                oEmail.Dispose();
            

        }


        



        public void enviarEmails()
        {


            EmailRemetente = "naoresponda@manulifitasa.com.br";
            EmailSenha = "raiden";




            MailMessage mail = new MailMessage();
            mail.To.Add(EmailDestinatario);
            if (EmailDestinatarioCopia != "" && EmailDestinatarioCopia != null)
            {
                mail.Bcc.Add(EmailDestinatarioCopia);
            }
            mail.From = new MailAddress(EmailRemetente, Remetente, System.Text.Encoding.GetEncoding("ISO-8859-1"));
            mail.Subject = Descricao;
            mail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            mail.Body = Texto;
            mail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;   //Prioridade do E-Mail



            SmtpClient client = new SmtpClient();    //Adicionando as credenciais do seu e-mail e senha:
            //client.Credentials = new System.Net.NetworkCredential(EmailRemetente, EmailSenha);
            client.Credentials = new System.Net.NetworkCredential(EmailRemetente, EmailSenha);

            client.Port = 587;    //(PORTA DE SAIDA)
            client.Host = "smtp.manulifitasa.com.br"; //Definindo o provedor que irá disparar o e-mail
            client.EnableSsl = false; //Gmail trabalha com Server Secured Layer deixar como true
            client.Send(mail);
            mail.Dispose();

        }
    }
}