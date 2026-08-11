using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace VendasWeb.Classes
{
    public class EmailClass
    {

        public string EmailDestinatario { get; set; }
        public string EmailRemetente { get; set; }
        public string EmailCopia { get; set; }
        public string Descricao { get; set; }
        public string Assunto { get; set; }
        public string Anexo { get; set; }

        public bool EnviarEmails()
        {
            try
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(EmailRemetente, EmailRemetente, System.Text.Encoding.GetEncoding("ISO-8859-1"));


                //Assunto do Email
                mail.Subject = Assunto;

                //Descrição do Email
                mail.Body = Descricao;

                //Destinatario
                mail.To.Add(EmailDestinatario);
                

                //Caso tenha Email de Copia
                if (EmailCopia != "")
                {
                    mail.CC.Add(EmailCopia.Replace(";", ","));
                }

               

                //Colocar Anexo
                if (Anexo != "")
                {
                    mail.Attachments.Add(new Attachment(Anexo));
                }

                
                
                //email para resposta(quando o destinatário receber e clicar em responder, vai para:)
                mail.ReplyTo = new MailAddress(EmailRemetente);


                mail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                mail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;   //Prioridade do E-Mail


                SmtpClient client = new SmtpClient();    //Adicionando as credenciais do seu e-mail e senha:
                //client.Credentials = new System.Net.NetworkCredential(EmailRemetente, EmailSenha);
                client.Credentials = new System.Net.NetworkCredential("naoresponda@manupackaging.com.br", "Raiden@!1%");

                client.Port = 587;    //Esta porta é a utilizada pelo Gmail para envio
                client.Host = "177.124.61.75"; //Definindo o provedor que irá disparar o e-mail
                client.EnableSsl = false; //Gmail trabalha com Server Secured Layer
                client.Send(mail);
                mail.Dispose();

                return true;
            }
            catch
            {

                return false;
            }



        }

    }
}