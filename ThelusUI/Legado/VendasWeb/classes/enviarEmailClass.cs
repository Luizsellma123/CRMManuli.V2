using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace VendasWeb
{
    public class enviarEmailClass
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
                oEnviar.Host = "177.124.61.75"; //DIGITE AQUI O NOME DO SERVIDOR DE SMTP QUE VOCÊ IRA UTILIZAR
                oEnviar.Credentials = new System.Net.NetworkCredential("naoresponda@manupackaging.com.br", "Raiden@!1%"); // DIGITE UM E-MAIL VÁLIDO E UMA SENHA PARA AUTENTICACAO NO SERVIDOR SMTP
                oEnviar.Port = 587;
                oEnviar.Send(oEmail);
                oEmail.Dispose();
            

        }
  
        public void enviarEmails()
        {


            EmailRemetente = "naoresponda@manupackaging.com.br";
            EmailSenha = "Raiden@!1%";




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
            client.Host = "177.124.61.75"; //Definindo o provedor que irá disparar o e-mail
            client.EnableSsl = false; //Gmail trabalha com Server Secured Layer deixar como true
            client.Send(mail);
            mail.Dispose();

        }

        public void SolicitacaoBoletoEmail(string titulo, string parcela, string empresa, string cliente, string emailpara)
        {
            string corpo = "";

            MailMessage oEmail = new MailMessage();
            MailAddress sDe = new MailAddress("naoresponda@manupackaging.com.br", "Portal Cliente");

            oEmail.To.Add(emailpara); //DIGITE AQUI O E-MAIL PARA O QUAL SERÁ ENCAMINHADO O FORMULARIO
            oEmail.From = sDe;
            oEmail.Priority = MailPriority.Normal;
            oEmail.IsBodyHtml = true;
            oEmail.Subject = titulo.ToString();

            // Monta o corpo da mensagem a ser enviada
            // mensagem = new StringBuilder();
            //mensagem.Append("TESTE").Append(Environment.NewLine);
            //mensagem.Append("E-mail do Contato: " + txtEmail.Text + "").Append(Environment.NewLine);
            //mensagem.Append("Assunto: " + txtAssunto.Text + "").Append(Environment.NewLine);
            //mensagem.Append("Mensagem: " + txtMensagem.Text + "");

            //oEmail.Body = mensagem.ToString();

            corpo = montaCorpoBoleto(empresa, parcela , cliente);

            oEmail.Body = corpo.ToString();

            SmtpClient oEnviar = new SmtpClient();
            oEnviar.Host = "177.124.61.75"; //DIGITE AQUI O NOME DO SERVIDOR DE SMTP QUE VOCÊ IRA UTILIZAR
            oEnviar.Credentials = new System.Net.NetworkCredential("naoresponda@manupackaging.com.br", "Raiden@!1%"); // DIGITE UM E-MAIL VÁLIDO E UMA SENHA PARA AUTENTICACAO NO SERVIDOR SMTP
            oEnviar.Port = 587;
            oEnviar.Send(oEmail);
            oEmail.Dispose();


        }

        public string montaCorpoBoleto(string empresa, string parcela, string cliente)
        {
            string corpo = "";

            corpo = "<div id=:wy class=a3s style=overflow: hidden;>";
            corpo += "<u></u>";
            corpo += "<div style=margin:0;padding:0;background-color: #303c66>";
            corpo += "<div style=background-color: #303c66>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;color:#b9b9b9;font-family:&quot;Open Sans&quot;,sans-serif align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:10px 0 5px 0;vertical-align:top width=300></td>";
            corpo += "<td style=text-align:right;padding:10px 0 5px 0;vertical-align:top width=300></td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0 width=600>";
            corpo += "<div style=font-size:26px;line-height:32px;Margin-top:29px;Margin-bottom:29px;color:#c3ced9;font-family:Roboto,Tahoma,sans-serif>";
            corpo += "<div style=font-size:0px!important;line-height:0!important align=center>";
            //corpo += "<img style=min-height:auto;width:100%;border:0;max-width:292px src=http://manulifitasa.com.br/wp-content/uploads/2018/05/logo-manuli-antiga-white.png alt= width=292 height=79 class=CToWUd>";      
            corpo += "</a>";
            corpo += "</div>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#303c66; align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<div style=line-height:5px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px>";
            corpo += "<h1 style=Margin-top:0;Margin-bottom:0;font-style:normal;font-weight:normal;font-size:20px;line-height:28px;color:#44a8c7; font-family:open sans,sans-serif;text-align:center>";
            corpo += "<span>";
            corpo += "<center><span style=color:#f5f7fa>Olá Luiz.Carlos! Você possui uma solicitação !</span></center>";
            corpo += "<center><span style=color:#f5f7fa> <h1> Solicitação Boleto !</h1> </span></center>";
            corpo += "</span>";
            corpo += "</h1>";
            corpo += "<p style=Margin-top:20px;Margin-bottom:20px;font-family:cabin,avenir,sans-serif;font-size:40px;line-height:47px;text-align:center>";
            corpo += "<span>";
            corpo += "<strong>";
            corpo += "<span style=color:#ffffff>" + parcela.ToString();
            corpo += "</span>";
            corpo += "</strong>";
            corpo += "</span>";
            corpo += "</p>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px>";
            corpo += "<div style=line-height:5px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<div style=line-height:5px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<div style=font-size:18px;line-height:18px>&nbsp;</div>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#ffffff align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<div style=line-height:10px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px>";
            corpo += "<div style=line-height:40px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<h2 style=Margin-top:0;Margin-bottom:0;font-style:normal;font-weight:normal;font-size:18px;line-height:28px;color:#44a8c7;text-align:left>";
            corpo += "<span style=color:#696969>Descrição da Solicitação: </span> <br/>";
            corpo += "<span style=color:#696969>Empresa: " + empresa.ToString() + "</span><br/>";
            corpo += "<span style=color:#696969>Cliente: " + cliente.ToString() + "</span><br/>";
            corpo += "<span style=color:#696969>Parcela: " + parcela.ToString() + "</span>";
            corpo += "</h2>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#ffffff align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<span style=color:#696969>Atenciosamente,</span><br>";
            corpo += "<span style=color:#696969>Equipe Portal Cliente</span>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<div style=line-height:40px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<div style=font-size:20px;line-height:20px>&nbsp;</div>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#ffffff align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<div style=line-height:10px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<h2 style=Margin-top:0;Margin-bottom:0;font-style:normal;font-weight:normal;font-size:13px;line-height:21px;color:#44a8c7;text-align:left>";
            corpo += "<span style=color:#a1a1a1>Esta mensagem de e-mail foi enviada de um endere&ccedil;o de e-mail que apenas envia mensagens, n&atilde;o responda!</span>";
            corpo += "</h2>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-right:auto;Margin-left:auto;border-spacing:0 width=600 align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0 0 40px 0>";
            corpo += "<table style=border-collapse:collapse;table-layout:auto;border-spacing:0 align=right>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0></td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;border-spacing:0;color:#b9b9b9;font-family:&quot;Open Sans&quot;,sans-serif width=400>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0;font-size:12px;line-height:19px>";
            corpo += "<br>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "</div>";
            corpo += "</div>";
            corpo += "</div>";
            corpo += "<div class=adL>"; ;
            corpo += "</div>";
            corpo += "</div>";

            return corpo;
        }

        public void SolicitacaoNotaEmail(string titulo, string nota, string empresa, string cliente, string emailpara)
        {
            string corpo = "";

            MailMessage oEmail = new MailMessage();
            MailAddress sDe = new MailAddress("naoresponda@manupackaging.com.br", "Portal Cliente");

            oEmail.To.Add(emailpara); //DIGITE AQUI O E-MAIL PARA O QUAL SERÁ ENCAMINHADO O FORMULARIO
            oEmail.From = sDe;
            oEmail.Priority = MailPriority.Normal;
            oEmail.IsBodyHtml = true;
            oEmail.Subject = titulo.ToString();

            // Monta o corpo da mensagem a ser enviada
            // mensagem = new StringBuilder();
            //mensagem.Append("TESTE").Append(Environment.NewLine);
            //mensagem.Append("E-mail do Contato: " + txtEmail.Text + "").Append(Environment.NewLine);
            //mensagem.Append("Assunto: " + txtAssunto.Text + "").Append(Environment.NewLine);
            //mensagem.Append("Mensagem: " + txtMensagem.Text + "");

            //oEmail.Body = mensagem.ToString();

            corpo = montaCorpoNota(empresa, nota, cliente);

            oEmail.Body = corpo.ToString();

            SmtpClient oEnviar = new SmtpClient();
            oEnviar.Host = "177.124.61.75"; //DIGITE AQUI O NOME DO SERVIDOR DE SMTP QUE VOCÊ IRA UTILIZAR
            oEnviar.Credentials = new System.Net.NetworkCredential("naoresponda@manupackaging.com.br", "Raiden@!1%"); // DIGITE UM E-MAIL VÁLIDO E UMA SENHA PARA AUTENTICACAO NO SERVIDOR SMTP
            oEnviar.Port = 587;
            oEnviar.Send(oEmail);
            oEmail.Dispose();


        }

        public string montaCorpoNota(string empresa, string nota, string cliente)
        {
            string corpo = "";

            corpo = "<div id=:wy class=a3s style=overflow: hidden;>";
            corpo += "<u></u>";
            corpo += "<div style=margin:0;padding:0;background-color: #303c66>";
            corpo += "<div style=background-color: #303c66>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;color:#b9b9b9;font-family:&quot;Open Sans&quot;,sans-serif align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:10px 0 5px 0;vertical-align:top width=300></td>";
            corpo += "<td style=text-align:right;padding:10px 0 5px 0;vertical-align:top width=300></td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0 width=600>";
            corpo += "<div style=font-size:26px;line-height:32px;Margin-top:29px;Margin-bottom:29px;color:#c3ced9;font-family:Roboto,Tahoma,sans-serif>";
            corpo += "<div style=font-size:0px!important;line-height:0!important align=center>";
            //corpo += "<img style=min-height:auto;width:100%;border:0;max-width:292px src=http://manulifitasa.com.br/wp-content/uploads/2018/05/logo-manuli-antiga-white.png alt= width=292 height=79 class=CToWUd>";      
            corpo += "</a>";
            corpo += "</div>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#303c66; align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<div style=line-height:5px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px>";
            corpo += "<h1 style=Margin-top:0;Margin-bottom:0;font-style:normal;font-weight:normal;font-size:20px;line-height:28px;color:#44a8c7; font-family:open sans,sans-serif;text-align:center>";
            corpo += "<span>";
            corpo += "<center><span style=color:#f5f7fa>Olá Luiz.Carlos! Você possui uma solicitação !</span></center>";
            corpo += "<center><span style=color:#f5f7fa> <h1> Solicitação Nota Fiscal !</h1> </span></center>";
            corpo += "</span>";
            corpo += "</h1>";
            corpo += "<p style=Margin-top:20px;Margin-bottom:20px;font-family:cabin,avenir,sans-serif;font-size:40px;line-height:47px;text-align:center>";
            corpo += "<span>";
            corpo += "<strong>";
            corpo += "<span style=color:#ffffff>" + nota.ToString();
            corpo += "</span>";
            corpo += "</strong>";
            corpo += "</span>";
            corpo += "</p>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px>";
            corpo += "<div style=line-height:5px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<div style=line-height:5px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<div style=font-size:18px;line-height:18px>&nbsp;</div>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#ffffff align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<div style=line-height:10px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px>";
            corpo += "<div style=line-height:40px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<h2 style=Margin-top:0;Margin-bottom:0;font-style:normal;font-weight:normal;font-size:18px;line-height:28px;color:#44a8c7;text-align:left>";
            corpo += "<span style=color:#696969>Descrição da Solicitação: </span> <br/>";
            corpo += "<span style=color:#696969>Empresa: " + empresa.ToString() + "</span><br/>";
            corpo += "<span style=color:#696969>Cliente: " + cliente.ToString() + "</span><br/>";
            corpo += "<span style=color:#696969>Nota Fiscal: " + nota.ToString() + "</span>";
            corpo += "</h2>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#ffffff align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<span style=color:#696969>Atenciosamente,</span><br>";
            corpo += "<span style=color:#696969>Equipe Portal Cliente</span>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<div style=line-height:40px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<div style=font-size:20px;line-height:20px>&nbsp;</div>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#ffffff align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<div style=line-height:10px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<h2 style=Margin-top:0;Margin-bottom:0;font-style:normal;font-weight:normal;font-size:13px;line-height:21px;color:#44a8c7;text-align:left>";
            corpo += "<span style=color:#a1a1a1>Esta mensagem de e-mail foi enviada de um endere&ccedil;o de e-mail que apenas envia mensagens, n&atilde;o responda!</span>";
            corpo += "</h2>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-right:auto;Margin-left:auto;border-spacing:0 width=600 align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0 0 40px 0>";
            corpo += "<table style=border-collapse:collapse;table-layout:auto;border-spacing:0 align=right>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0></td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;border-spacing:0;color:#b9b9b9;font-family:&quot;Open Sans&quot;,sans-serif width=400>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0;font-size:12px;line-height:19px>";
            corpo += "<br>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "</div>";
            corpo += "</div>";
            corpo += "</div>";
            corpo += "<div class=adL>"; ;
            corpo += "</div>";
            corpo += "</div>";

            return corpo;
        }

        public void SolicitacaoLimiteEmail(string titulo, string descricao, string cliente, string emailpara, Attachment anexar)
        {
            string corpo = "";

            MailMessage oEmail = new MailMessage();
            MailAddress sDe = new MailAddress("naoresponda@manupackaging.com.br", "Portal Cliente");

            oEmail.To.Add(emailpara); //DIGITE AQUI O E-MAIL PARA O QUAL SERÁ ENCAMINHADO O FORMULARIO
            oEmail.From = sDe;
            oEmail.Priority = MailPriority.Normal;
            oEmail.IsBodyHtml = true;
            oEmail.Subject = titulo.ToString();

            if (anexar.ContentStream.Length>0)
            {
                oEmail.Attachments.Add(anexar);
            }

            // Monta o corpo da mensagem a ser enviada
            // mensagem = new StringBuilder();
            //mensagem.Append("TESTE").Append(Environment.NewLine);
            //mensagem.Append("E-mail do Contato: " + txtEmail.Text + "").Append(Environment.NewLine);
            //mensagem.Append("Assunto: " + txtAssunto.Text + "").Append(Environment.NewLine);
            //mensagem.Append("Mensagem: " + txtMensagem.Text + "");

            //oEmail.Body = mensagem.ToString();

            corpo = montaCorpoSolicitacaoLimite(descricao, cliente);

            oEmail.Body = corpo.ToString();

            SmtpClient oEnviar = new SmtpClient();
            oEnviar.Host = "177.124.61.75"; //DIGITE AQUI O NOME DO SERVIDOR DE SMTP QUE VOCÊ IRA UTILIZAR
            oEnviar.Credentials = new System.Net.NetworkCredential("naoresponda@manupackaging.com.br", "Raiden@!1%"); // DIGITE UM E-MAIL VÁLIDO E UMA SENHA PARA AUTENTICACAO NO SERVIDOR SMTP
            oEnviar.Port = 587;
            oEnviar.Send(oEmail);
            oEmail.Dispose();


        }

        public string montaCorpoSolicitacaoLimite(string descricao, string cliente)
        {
            string corpo = "";

            corpo = "<div id=:wy class=a3s style=overflow: hidden;>";
            corpo += "<u></u>";
            corpo += "<div style=margin:0;padding:0;background-color: #303c66>";
            corpo += "<div style=background-color: #303c66>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;color:#b9b9b9;font-family:&quot;Open Sans&quot;,sans-serif align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:10px 0 5px 0;vertical-align:top width=300></td>";
            corpo += "<td style=text-align:right;padding:10px 0 5px 0;vertical-align:top width=300></td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0 width=600>";
            corpo += "<div style=font-size:26px;line-height:32px;Margin-top:29px;Margin-bottom:29px;color:#c3ced9;font-family:Roboto,Tahoma,sans-serif>";
            corpo += "<div style=font-size:0px!important;line-height:0!important align=center>";
            //corpo += "<img style=min-height:auto;width:100%;border:0;max-width:292px src=http://manulifitasa.com.br/wp-content/uploads/2018/05/logo-manuli-antiga-white.png alt= width=292 height=79 class=CToWUd>";      
            corpo += "</a>";
            corpo += "</div>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#303c66; align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<div style=line-height:5px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px>";
            corpo += "<h1 style=Margin-top:0;Margin-bottom:0;font-style:normal;font-weight:normal;font-size:20px;line-height:28px;color:#44a8c7; font-family:open sans,sans-serif;text-align:center>";
            corpo += "<span>";
            corpo += "<center><span style=color:#f5f7fa>Olá Luiz.Carlos! Você possui uma solicitação !</span></center>";
            corpo += "<center><span style=color:#f5f7fa> <h1> Solicitação De Limite !</h1> </span></center>";
            corpo += "</span>";
            corpo += "</h1>";
            corpo += "<p style=Margin-top:20px;Margin-bottom:20px;font-family:cabin,avenir,sans-serif;font-size:40px;line-height:47px;text-align:center>";
            corpo += "<span>";
            corpo += "<strong>";
            corpo += "<span style=color:#ffffff>" + cliente.ToString();
            corpo += "</span>";
            corpo += "</strong>";
            corpo += "</span>";
            corpo += "</p>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px>";
            corpo += "<div style=line-height:5px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<div style=line-height:5px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<div style=font-size:18px;line-height:18px>&nbsp;</div>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#ffffff align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<div style=line-height:10px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px>";
            corpo += "<div style=line-height:40px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<h2 style=Margin-top:0;Margin-bottom:0;font-style:normal;font-weight:normal;font-size:18px;line-height:28px;color:#44a8c7;text-align:left>";
            corpo += "<span style=color:#696969>Descrição da Solicitação: </span> <br/>";
            corpo += "<span style=color:#696969>" + descricao.ToString() + "</span>";
            corpo += "</h2>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#ffffff align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<span style=color:#696969>Atenciosamente,</span><br>";
            corpo += "<span style=color:#696969>Equipe Portal Cliente</span>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<div style=line-height:40px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<div style=font-size:20px;line-height:20px>&nbsp;</div>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#ffffff align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<div style=line-height:10px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<h2 style=Margin-top:0;Margin-bottom:0;font-style:normal;font-weight:normal;font-size:13px;line-height:21px;color:#44a8c7;text-align:left>";
            corpo += "<span style=color:#a1a1a1>Esta mensagem de e-mail foi enviada de um endere&ccedil;o de e-mail que apenas envia mensagens, n&atilde;o responda!</span>";
            corpo += "</h2>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-right:auto;Margin-left:auto;border-spacing:0 width=600 align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0 0 40px 0>";
            corpo += "<table style=border-collapse:collapse;table-layout:auto;border-spacing:0 align=right>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0></td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;border-spacing:0;color:#b9b9b9;font-family:&quot;Open Sans&quot;,sans-serif width=400>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0;font-size:12px;line-height:19px>";
            corpo += "<br>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "</div>";
            corpo += "</div>";
            corpo += "</div>";
            corpo += "<div class=adL>"; ;
            corpo += "</div>";
            corpo += "</div>";

            return corpo;
        }

        public void ContatoClienteEmail(string titulo, string descricao, string cliente, string emailpara, Attachment anexar)
        {
            string corpo = "";

            MailMessage oEmail = new MailMessage();
            MailAddress sDe = new MailAddress("naoresponda@manupackaging.com.br", "Portal Cliente");

            oEmail.To.Add(emailpara); //DIGITE AQUI O E-MAIL PARA O QUAL SERÁ ENCAMINHADO O FORMULARIO
            oEmail.From = sDe;
            oEmail.Priority = MailPriority.Normal;
            oEmail.IsBodyHtml = true;
            oEmail.Subject = titulo.ToString();

            if (anexar.ContentStream.Length > 0)
            {
                oEmail.Attachments.Add(anexar);
            }

            // Monta o corpo da mensagem a ser enviada
            // mensagem = new StringBuilder();
            //mensagem.Append("TESTE").Append(Environment.NewLine);
            //mensagem.Append("E-mail do Contato: " + txtEmail.Text + "").Append(Environment.NewLine);
            //mensagem.Append("Assunto: " + txtAssunto.Text + "").Append(Environment.NewLine);
            //mensagem.Append("Mensagem: " + txtMensagem.Text + "");

            //oEmail.Body = mensagem.ToString();

            corpo = montaCorpoContatoCliente(descricao, cliente);

            oEmail.Body = corpo.ToString();

            SmtpClient oEnviar = new SmtpClient();
            oEnviar.Host = "177.124.61.75"; //DIGITE AQUI O NOME DO SERVIDOR DE SMTP QUE VOCÊ IRA UTILIZAR
            oEnviar.Credentials = new System.Net.NetworkCredential("naoresponda@manupackaging.com.br", "Raiden@!1%"); // DIGITE UM E-MAIL VÁLIDO E UMA SENHA PARA AUTENTICACAO NO SERVIDOR SMTP
            oEnviar.Port = 587;
            oEnviar.Send(oEmail);
            oEmail.Dispose();


        }

        public string montaCorpoContatoCliente(string descricao, string cliente)
        {
            string corpo = "";

            corpo = "<div id=:wy class=a3s style=overflow: hidden;>";
            corpo += "<u></u>";
            corpo += "<div style=margin:0;padding:0;background-color: #303c66>";
            corpo += "<div style=background-color: #303c66>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;color:#b9b9b9;font-family:&quot;Open Sans&quot;,sans-serif align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:10px 0 5px 0;vertical-align:top width=300></td>";
            corpo += "<td style=text-align:right;padding:10px 0 5px 0;vertical-align:top width=300></td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0 width=600>";
            corpo += "<div style=font-size:26px;line-height:32px;Margin-top:29px;Margin-bottom:29px;color:#c3ced9;font-family:Roboto,Tahoma,sans-serif>";
            corpo += "<div style=font-size:0px!important;line-height:0!important align=center>";
            //corpo += "<img style=min-height:auto;width:100%;border:0;max-width:292px src=http://manulifitasa.com.br/wp-content/uploads/2018/05/logo-manuli-antiga-white.png alt= width=292 height=79 class=CToWUd>";      
            corpo += "</a>";
            corpo += "</div>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#303c66; align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<div style=line-height:5px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px>";
            corpo += "<h1 style=Margin-top:0;Margin-bottom:0;font-style:normal;font-weight:normal;font-size:20px;line-height:28px;color:#44a8c7; font-family:open sans,sans-serif;text-align:center>";
            corpo += "<span>";
            corpo += "<center><span style=color:#f5f7fa>Olá Luiz.Carlos! Você possui uma mensagem !</span></center>";
            corpo += "<center><span style=color:#f5f7fa> <h1> Contato Cliente !</h1> </span></center>";
            corpo += "</span>";
            corpo += "</h1>";
            corpo += "<p style=Margin-top:20px;Margin-bottom:20px;font-family:cabin,avenir,sans-serif;font-size:40px;line-height:47px;text-align:center>";
            corpo += "<span>";
            corpo += "<strong>";
            corpo += "<span style=color:#ffffff>" + cliente.ToString();
            corpo += "</span>";
            corpo += "</strong>";
            corpo += "</span>";
            corpo += "</p>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px>";
            corpo += "<div style=line-height:5px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<div style=line-height:5px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<div style=font-size:18px;line-height:18px>&nbsp;</div>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#ffffff align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<div style=line-height:10px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px>";
            corpo += "<div style=line-height:40px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<h2 style=Margin-top:0;Margin-bottom:0;font-style:normal;font-weight:normal;font-size:18px;line-height:28px;color:#44a8c7;text-align:left>";
            corpo += "<span style=color:#696969>Descrição da Solicitação: </span> <br/>";
            corpo += "<span style=color:#696969>" + descricao.ToString() + "</span>";
            corpo += "</h2>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#ffffff align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<span style=color:#696969>Atenciosamente,</span><br>";
            corpo += "<span style=color:#696969>Equipe Portal Cliente</span>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<div style=line-height:40px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<div style=font-size:20px;line-height:20px>&nbsp;</div>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-left:auto;Margin-right:auto;word-wrap:break-word;word-break:break-word;background-color:#ffffff align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=font-size:14px;line-height:21px;padding:0;text-align:left;vertical-align:top;color:#60666d;font-family:&quot;Open Sans&quot;,sans-serif width=600>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-top:24px>";
            corpo += "<div style=line-height:10px;font-size:1px>&nbsp;</div>";
            corpo += "</div>";
            corpo += "<div style=Margin-left:20px;Margin-right:20px;Margin-bottom:24px>";
            corpo += "<h2 style=Margin-top:0;Margin-bottom:0;font-style:normal;font-weight:normal;font-size:13px;line-height:21px;color:#44a8c7;text-align:left>";
            corpo += "<span style=color:#a1a1a1>Esta mensagem de e-mail foi enviada de um endere&ccedil;o de e-mail que apenas envia mensagens, n&atilde;o responda!</span>";
            corpo += "</h2>";
            corpo += "</div>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;Margin-right:auto;Margin-left:auto;border-spacing:0 width=600 align=center>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0 0 40px 0>";
            corpo += "<table style=border-collapse:collapse;table-layout:auto;border-spacing:0 align=right>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0></td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "<table style=border-collapse:collapse;table-layout:fixed;border-spacing:0;color:#b9b9b9;font-family:&quot;Open Sans&quot;,sans-serif width=400>";
            corpo += "<tbody>";
            corpo += "<tr>";
            corpo += "<td style=padding:0;font-size:12px;line-height:19px>";
            corpo += "<br>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "</td>";
            corpo += "</tr>";
            corpo += "</tbody>";
            corpo += "</table>";
            corpo += "</div>";
            corpo += "</div>";
            corpo += "</div>";
            corpo += "<div class=adL>"; ;
            corpo += "</div>";
            corpo += "</div>";

            return corpo;
        }
    }
}