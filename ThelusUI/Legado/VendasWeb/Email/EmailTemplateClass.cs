using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace VendasWeb.Email
{
    public class EmailTemplateClass
    {
        public string Template { get; set; }
        public string EnderecoTemplate { get; set; }
        WebClient OBJWebClient { get; set; }
        public string emailpara { get; set; }

        //Campos para o template
        public string cabecalho { get; set; }
        public string titulo { get; set; }
        public string detalhe { get; set; }
        public string NomeUsuario { get; set; }
        public string setor { get; set; }
        public string data { get; set; }
        //public string cliente { get; set; }
        //public string responsavel { get; set; }
        //public string situacao { get; set; }
        //public string assunto { get; set; }
        //public string descricao { get; set; }

        public EmailTemplateClass()
        {
            this.cabecalho = "";
            this.titulo = "";
            this.detalhe = "";
            this.setor = "";
            this.data = "";

        }

        private void ArrumaStringEndereco()
        {
            this.EnderecoTemplate = this.EnderecoTemplate.Replace("\\/", "\\");

            this.EnderecoTemplate = this.EnderecoTemplate.Replace("/", "\\");
        }

        public string EnviaEmailChamado()
        {
            string erro = "";

            try
            {
                //Adiciona Template
                //this.EnderecoTemplate = HttpContext.Current.Server.MapPath("/Email/Templates") + "/TemplateChamado.html";
                this.EnderecoTemplate = HttpContext.Current.Server.MapPath("~") + "/Email/Templates/TemplateChamado.html";

                ArrumaStringEndereco();

                //Instancia WebCliente
                OBJWebClient = new WebClient();
                OBJWebClient.Encoding = System.Text.Encoding.UTF8;

                //Obtendo o conteúdo do template
                this.Template = OBJWebClient.DownloadString(this.EnderecoTemplate);

                //fazendo o replace dos campos
                this.Template = this.Template.Replace("{$CABECALHO}", this.cabecalho);
                this.Template = this.Template.Replace("{$TITULO}", this.titulo);
                this.Template = this.Template.Replace("{$DETALHE}", this.detalhe);
                this.Template = this.Template.Replace("{$USUARIO}", this.NomeUsuario);
                this.Template = this.Template.Replace("{$SETOR}", this.setor);
                this.Template = this.Template.Replace("{$DATA}", this.data);


                MailMessage oEmail = new MailMessage();
                MailAddress sDe = new MailAddress(this.emailpara, "CRM");

                oEmail.To.Add(this.emailpara); //DIGITE AQUI O E-MAIL PARA O QUAL SERÁ ENCAMINHADO O FORMULARIO
                oEmail.From = sDe;
                oEmail.Priority = MailPriority.Normal;
                oEmail.IsBodyHtml = true;
                oEmail.Subject = this.cabecalho;

                oEmail.Body = this.Template;

                SmtpClient oEnviar = new SmtpClient();
                oEnviar.Host = "177.124.61.75"; //DIGITE AQUI O NOME DO SERVIDOR DE SMTP QUE VOCÊ IRA UTILIZAR
                oEnviar.Credentials = new System.Net.NetworkCredential("naoresponda@manupackaging.com.br", "Raiden@!1%"); // DIGITE UM E-MAIL VÁLIDO E UMA SENHA PARA AUTENTICACAO NO SERVIDOR SMTP
                oEnviar.Port = 587;
                oEnviar.Send(oEmail);
                oEmail.Dispose();

            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return erro;
        }

        public string EnviaEmailRecuperacaoAcesso()
        {
            string erro = "";

            try
            {
                //Adiciona Template
                //this.EnderecoTemplate = HttpContext.Current.Server.MapPath("/Email/Templates") + "/TemplateChamado.html";
                this.EnderecoTemplate = HttpContext.Current.Server.MapPath("~") + "/Email/Templates/TemplateRecuperacaoAcesso.html";

                ArrumaStringEndereco();

                //Instancia WebCliente
                OBJWebClient = new WebClient();
                OBJWebClient.Encoding = System.Text.Encoding.UTF8;

                //Obtendo o conteúdo do template
                this.Template = OBJWebClient.DownloadString(this.EnderecoTemplate);

                //fazendo o replace dos campos
                this.Template = this.Template.Replace("{$CABECALHO}", this.cabecalho);
                this.Template = this.Template.Replace("{$TITULO}", this.titulo);
                this.Template = this.Template.Replace("{$DETALHE}", this.detalhe);
                this.Template = this.Template.Replace("{$USUARIO}", this.NomeUsuario);
                this.Template = this.Template.Replace("{$SETOR}", this.setor);
                this.Template = this.Template.Replace("{$DATA}", this.data);


                MailMessage oEmail = new MailMessage();
                MailAddress sDe = new MailAddress(this.emailpara, "CRM");

                oEmail.To.Add(this.emailpara); //DIGITE AQUI O E-MAIL PARA O QUAL SERÁ ENCAMINHADO O FORMULARIO
                oEmail.From = sDe;
                oEmail.Priority = MailPriority.Normal;
                oEmail.IsBodyHtml = true;
                oEmail.Subject = this.cabecalho;

                oEmail.Body = this.Template;

                SmtpClient oEnviar = new SmtpClient();
                oEnviar.Host = "177.124.61.75"; //DIGITE AQUI O NOME DO SERVIDOR DE SMTP QUE VOCÊ IRA UTILIZAR
                oEnviar.Credentials = new System.Net.NetworkCredential("naoresponda@manupackaging.com.br", "Raiden@!1%"); // DIGITE UM E-MAIL VÁLIDO E UMA SENHA PARA AUTENTICACAO NO SERVIDOR SMTP
                oEnviar.Port = 587;
                oEnviar.Send(oEmail);
                oEmail.Dispose();

            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return erro;
        }

        public string EnviaEmailTicketAnexo()
        {
            string erro = "";

            try
            {
                //Adiciona Template
                //this.EnderecoTemplate = HttpContext.Current.Server.MapPath("/Email/Templates") + "/TemplateTicketAnexo.html";
                this.EnderecoTemplate = HttpContext.Current.Server.MapPath("~") + "/Email/Templates/TemplateTicketAnexo.html";

                ArrumaStringEndereco();

                //Instancia WebCliente
                OBJWebClient = new WebClient();
                OBJWebClient.Encoding = System.Text.Encoding.UTF8;

                //Obtendo o conteúdo do template
                this.Template = OBJWebClient.DownloadString(this.EnderecoTemplate);

                //fazendo o replace dos campos
                this.Template = this.Template.Replace("{$CABECALHO}", this.cabecalho);
                this.Template = this.Template.Replace("{$TITULO}", this.titulo);
                this.Template = this.Template.Replace("{$DETALHE}", this.detalhe);
                this.Template = this.Template.Replace("{$USUARIO}", this.NomeUsuario);
                //this.Template = this.Template.Replace("{$SETOR}", this.setor);
                this.Template = this.Template.Replace("{$DATA}", this.data);


                MailMessage oEmail = new MailMessage();
                MailAddress sDe = new MailAddress(this.emailpara, "CRM");

                oEmail.To.Add(this.emailpara); //DIGITE AQUI O E-MAIL PARA O QUAL SERÁ ENCAMINHADO O FORMULARIO
                oEmail.From = sDe;
                oEmail.Priority = MailPriority.Normal;
                oEmail.IsBodyHtml = true;
                oEmail.Subject = this.cabecalho;

                oEmail.Body = this.Template;

                SmtpClient oEnviar = new SmtpClient();
                oEnviar.Host = "177.124.61.75"; //DIGITE AQUI O NOME DO SERVIDOR DE SMTP QUE VOCÊ IRA UTILIZAR
                oEnviar.Credentials = new System.Net.NetworkCredential("naoresponda@manupackaging.com.br", "Raiden@!1%"); // DIGITE UM E-MAIL VÁLIDO E UMA SENHA PARA AUTENTICACAO NO SERVIDOR SMTP
                oEnviar.Port = 587;
                oEnviar.Send(oEmail);
                oEmail.Dispose();

            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return erro;
        }

        public string EnviaEmailTicket()
        {
            string erro = "";

            try
            {
                //Adiciona Template
                //this.EnderecoTemplate = HttpContext.Current.Server.MapPath("/Email/Templates") + "/TemplateTicketDetalhe.html";
                this.EnderecoTemplate = HttpContext.Current.Server.MapPath("~") + "/Email/Templates/TemplateTicketDetalhe.html";

                ArrumaStringEndereco();

                //Instancia WebCliente
                OBJWebClient = new WebClient();
                OBJWebClient.Encoding = System.Text.Encoding.UTF8;

                //Obtendo o conteúdo do template
                this.Template = OBJWebClient.DownloadString(this.EnderecoTemplate);

                //fazendo o replace dos campos
                this.Template = this.Template.Replace("{$CABECALHO}", this.cabecalho);
                this.Template = this.Template.Replace("{$TITULO}", this.titulo);
                this.Template = this.Template.Replace("{$DETALHE}", this.detalhe);
                this.Template = this.Template.Replace("{$USUARIO}", this.NomeUsuario);
                //this.Template = this.Template.Replace("{$SETOR}", this.setor);
                this.Template = this.Template.Replace("{$DATA}", this.data);


                MailMessage oEmail = new MailMessage();
                MailAddress sDe = new MailAddress(this.emailpara, "CRM");

                oEmail.To.Add(this.emailpara); //DIGITE AQUI O E-MAIL PARA O QUAL SERÁ ENCAMINHADO O FORMULARIO
                oEmail.From = sDe;
                oEmail.Priority = MailPriority.Normal;
                oEmail.IsBodyHtml = true;
                oEmail.Subject = this.cabecalho;

                oEmail.Body = this.Template;

                SmtpClient oEnviar = new SmtpClient();
                oEnviar.Host = "177.124.61.75"; //DIGITE AQUI O NOME DO SERVIDOR DE SMTP QUE VOCÊ IRA UTILIZAR
                oEnviar.Credentials = new System.Net.NetworkCredential("naoresponda@manupackaging.com.br", "Raiden@!1%"); // DIGITE UM E-MAIL VÁLIDO E UMA SENHA PARA AUTENTICACAO NO SERVIDOR SMTP
                oEnviar.Port = 587;
                oEnviar.Send(oEmail);
                oEmail.Dispose();

            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return erro;
        }
    }
}