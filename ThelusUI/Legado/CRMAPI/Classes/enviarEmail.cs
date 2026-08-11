using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;

namespace CRMAPI.Classes
{
    public class enviarEmail : ConexaoClass
    {



        public string EmailDestinatario { get; set; }
        public string EmailDestinatarioCopia { get; set; }
        public string EmailRemetente { get; set; }
        public string Remetente { get; set; }
        public string Descricao { get; set; }
        public string Texto { get; set; }
        public string TextoFormatado { get; set; }
        public string EmailSenha { get; set; }

        /*Campos para o pedidos*/
        public string CodigoEmpresa { get; set; }
        public string NomeEmpresa { get; set; }
        public string NumeroPedidoCRM { get; set; }
        public string NomeCliente { get; set; }
        public string DataAlteracao { get; set; }
        public string Situacao { get; set; }
        public string Status { get; set; }
        public string Historico { get; set; }
        public string HistoricoDetalhado { get; set; }
        public string UsuarioCRM { get; set; }
        public string TituloEmail { get; set; }

        public enviarEmail()
        {
            EmailRemetente = "naoresponda@manupackaging.com.br";
            EmailSenha = "Raiden@!1%";
        }

        #region Solicitação Alteração Cliente
        public string TipoSolicitacao { get; set; }
        public Attachment Anexo { get; set; }
        #endregion

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

        public void FormataTexto()
        {
            string CorpoEmail = "";

            CorpoEmail +="<table border=\"1\">";
            CorpoEmail += "<tr bgcolor=\"#DCDCDC\"><td colspan=\"2\"><b>" + this.CodigoEmpresa + "</b></td></tr>";
            CorpoEmail += "<tr><td>Pedido:</td><td>" + this.NumeroPedidoCRM + "</td></tr>";
            CorpoEmail += "<tr><td>Cliente:</td><td>" + this.NomeCliente + "</td></tr>";
            CorpoEmail += "<tr><td>Alteracao:</td><td>" + this.DataAlteracao + "</td></tr>";
            CorpoEmail += "<tr><td>Situacao:</td><td>" + this.Situacao + "</td></tr>";
            CorpoEmail += "<tr><td>Status:</td><td>" + this.Status + "</td></tr>";
            CorpoEmail += "<tr><td>Analisado:</td><td>" + this.UsuarioCRM + "</td></tr>";
            CorpoEmail += "<tr bgcolor=\"#DCDCDC\"><td colspan=\"2\"><b>MOTIVO</b></td></tr>";

            CorpoEmail += "<tr><td colspan=\"2\">" + Historico + "</td></tr>";

            CorpoEmail += "<tr bgcolor=\"#DCDCDC\"><td colspan=\"2\"><b>Historico Detalhado</b></td></tr>";

            CorpoEmail += "<tr><td colspan=\"2\">" + this.HistoricoDetalhado + "</td></tr>";

            CorpoEmail += "</table>";

            this.TextoFormatado = CorpoEmail;
        }

        public void RecuperaEmailDestinatario()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_EMAIL_VENDEDOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.VarChar, 100, "CodigoClienteSAP"));

                    dbCommand.Parameters["@CodigoClienteSAP"].Value = this.NomeCliente.Substring(0,10);

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.EmailDestinatario = row["Email"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public string RecuperaEmailAlteracaoFinanceiro()
        {
            string Emails = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_EMAILS_VENDEDOR_LIBERACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.VarChar, 8000, "CodigoClienteSAP"));
                    dbCommand.Parameters.Add(new SqlParameter("@Emails", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "Emails", DataRowVersion.Default, null));


                    dbCommand.Parameters["@CodigoClienteSAP"].Value = this.NomeCliente.Substring(0, 10); 
                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    Emails = (string)dbCommand.Parameters["@Emails"].Value;

                }
                catch (Exception ex)
                {
                    Emails = "";
                }
            }

            return Emails;
        }

        public void enviaEmailFormatado()
        {


            MailMessage oEmail = new MailMessage();
            MailAddress sDe = new MailAddress(this.EmailRemetente, "VendasWEB");

            oEmail.To.Add(this.EmailDestinatario); //DIGITE AQUI O E-MAIL PARA O QUAL SERÁ ENCAMINHADO O FORMULARIO
            oEmail.From = sDe;
            oEmail.Priority = MailPriority.Normal;
            oEmail.IsBodyHtml = true;
            oEmail.Subject = this.TituloEmail;

            // Monta o corpo da mensagem a ser enviada
            // mensagem = new StringBuilder();
            //mensagem.Append("TESTE").Append(Environment.NewLine);
            //mensagem.Append("E-mail do Contato: " + txtEmail.Text + "").Append(Environment.NewLine);
            //mensagem.Append("Assunto: " + txtAssunto.Text + "").Append(Environment.NewLine);
            //mensagem.Append("Mensagem: " + txtMensagem.Text + "");

            //oEmail.Body = mensagem.ToString();
            oEmail.Body = this.TextoFormatado;

            SmtpClient oEnviar = new SmtpClient();
            oEnviar.Host = "177.124.61.75"; //DIGITE AQUI O NOME DO SERVIDOR DE SMTP QUE VOCÊ IRA UTILIZAR
            oEnviar.Credentials = new System.Net.NetworkCredential("naoresponda@manupackaging.com.br", "Raiden@!1%"); // DIGITE UM E-MAIL VÁLIDO E UMA SENHA PARA AUTENTICACAO NO SERVIDOR SMTP
            oEnviar.Port = 587;
            oEnviar.Send(oEmail);
            oEmail.Dispose();


        }

        public void enviaEmailFormatadoAnexo()
        {
            MailMessage oEmail = new MailMessage();
            MailAddress sDe = new MailAddress(this.EmailRemetente, "VendasWEB");

            foreach (var address in this.EmailDestinatario.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                oEmail.To.Add(address); //DIGITE AQUI O E-MAIL PARA O QUAL SERÁ ENCAMINHADO O FORMULARIO
            }
            
            oEmail.From = sDe;
            oEmail.Priority = MailPriority.Normal;
            oEmail.IsBodyHtml = true;
            oEmail.Subject = this.TituloEmail;

            // Monta o corpo da mensagem a ser enviada
            // mensagem = new StringBuilder();
            //mensagem.Append("TESTE").Append(Environment.NewLine);
            //mensagem.Append("E-mail do Contato: " + txtEmail.Text + "").Append(Environment.NewLine);
            //mensagem.Append("Assunto: " + txtAssunto.Text + "").Append(Environment.NewLine);
            //mensagem.Append("Mensagem: " + txtMensagem.Text + "");

            //oEmail.Body = mensagem.ToString();
            oEmail.Body = this.TextoFormatado;

            if (this.Anexo != null)
            {
                oEmail.Attachments.Add(this.Anexo);
            }

            SmtpClient oEnviar = new SmtpClient();
            oEnviar.Host = "177.124.61.75"; //DIGITE AQUI O NOME DO SERVIDOR DE SMTP QUE VOCÊ IRA UTILIZAR
            oEnviar.Credentials = new System.Net.NetworkCredential("naoresponda@manupackaging.com.br", "Raiden@!1%"); // DIGITE UM E-MAIL VÁLIDO E UMA SENHA PARA AUTENTICACAO NO SERVIDOR SMTP
            oEnviar.Port = 587;
            oEnviar.Send(oEmail);
            oEmail.Dispose();


        }

        public void FormataTextoSolicitacaoCliente()
        {
            string CorpoEmail = "";

            CorpoEmail += "<table border=\"1\">";
            CorpoEmail += "<tr bgcolor=\"#DCDCDC\"><td colspan=\"2\"><b>" + this.TipoSolicitacao + "</b></td></tr>";
            CorpoEmail += "<tr><td>Cliente:</td><td>" + this.NomeCliente + "</td></tr>";
            CorpoEmail += "<tr><td>Data Solicitação:</td><td>" + this.DataAlteracao + "</td></tr>";
            CorpoEmail += "<tr bgcolor=\"#DCDCDC\"><td colspan=\"2\"><b>DESCRIÇÃO SOLICITAÇÃO</b></td></tr>";

            CorpoEmail += "<tr><td>" + this.DataAlteracao + "</td><td>" + this.Historico + "</td></tr>";

            this.TextoFormatado = CorpoEmail;
        }

        public void FormataTextoHistoricoCliente()
        {
            string CorpoEmail = "";

            CorpoEmail += "<table border=\"1\">";
            CorpoEmail += "<tr bgcolor=\"#DCDCDC\"><td colspan=\"2\"><b>" + this.TipoSolicitacao + "</b></td></tr>";
            CorpoEmail += "<tr><td>Cliente:</td><td>" + this.NomeCliente + "</td></tr>";
            CorpoEmail += "<tr><td>Data Histórico:</td><td>" + this.DataAlteracao + "</td></tr>";
            CorpoEmail += "<tr><td>Usuário:</td><td>" + this.UsuarioCRM + "</td></tr>";
            CorpoEmail += "<tr bgcolor=\"#DCDCDC\"><td colspan=\"2\"><b>HISTÓRICO DETALHADO</b></td></tr>";

            CorpoEmail += "<tr><td td colspan=\"2\">" + this.Historico + "</td></tr>";

            this.TextoFormatado = CorpoEmail;
        }

        public void FormataTextoHistoricoPedido()
        {
            string CorpoEmail = "";

            CorpoEmail += "<table border=\"1\">";
            CorpoEmail += "<tr bgcolor=\"#DCDCDC\"><td colspan=\"2\"><b>" + this.CodigoEmpresa + "</b></td></tr>";
            CorpoEmail += "<tr><td>Pedido:</td><td>" + this.NumeroPedidoCRM + "</td></tr>";
            CorpoEmail += "<tr><td>Cliente:</td><td>" + this.NomeCliente + "</td></tr>";
            CorpoEmail += "<tr><td>Alteracao:</td><td>" + this.DataAlteracao + "</td></tr>";
            CorpoEmail += "<tr><td>Usuário:</td><td>" + this.UsuarioCRM + "</td></tr>";
            CorpoEmail += "<tr bgcolor=\"#DCDCDC\"><td colspan=\"2\"><b>Histórico</b></td></tr>";

            CorpoEmail += "<tr><td colspan=\"2\">" + Historico + "</td></tr>";

            CorpoEmail += "</table>";

            this.TextoFormatado = CorpoEmail;
        }
    }
}