using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.IO;

namespace VendasWeb
{
    public class enviarEmail : clsConexao
    {
        #region Campos 

        public int IDTicket { get; set; }
        public string Solicitante { get; set; }

        public string EmailDestinatario { get; set; }
        public string EmailDestinatarioCopia { get; set; }
        public string EmailRemetente { get; set; }
        public string Remetente { get; set; }
        public string Descricao { get; set; }
        public string Texto { get; set; }
        public string TextoFormatado { get; set; }
        public string EmailSenha { get; set; }

        public string Evento { get; set; }
        public string Categoria { get; set; }

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

        #endregion

        public enviarEmail()
        {
            EmailRemetente = "naoresponda@manupackaging.com.br";
            EmailSenha = "Raiden@!1%";
        }

        #region Solicitação Alteração Cliente
        public string TipoSolicitacao { get; set; }
        public Attachment Anexo { get; set; }
        #endregion

        public AttachmentCollection PosicaoDiariaAnexos { get; set; }

        #region enviaEmail

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

        public void enviaEmailPosicaoDiariaFormatadoComAnexos()
        {
            MailMessage oEmail = new MailMessage();
            MailAddress sDe = new MailAddress(this.EmailRemetente, "CRM");

            foreach (var address in this.EmailDestinatario.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                oEmail.To.Add(address); //DIGITE AQUI O E-MAIL PARA O QUAL SERÁ ENCAMINHADO O FORMULARIO
            }

            oEmail.From = sDe;
            oEmail.Priority = MailPriority.Normal;
            oEmail.IsBodyHtml = true;
            oEmail.Subject = this.TituloEmail;
            oEmail.Body = this.TextoFormatado;

            if (this.PosicaoDiariaAnexos != null)
            {
                foreach (Attachment anexo in this.PosicaoDiariaAnexos)
                {
                    oEmail.Attachments.Add(anexo);
                }
            }

            SmtpClient oEnviar = new SmtpClient();
            oEnviar.Host = "177.124.61.75"; //DIGITE AQUI O NOME DO SERVIDOR DE SMTP QUE VOCÊ IRA UTILIZAR
            oEnviar.Credentials = new System.Net.NetworkCredential("naoresponda@manupackaging.com.br", "Raiden@!1%"); // DIGITE UM E-MAIL VÁLIDO E UMA SENHA PARA AUTENTICACAO NO SERVIDOR SMTP
            oEnviar.Port = 587;
            oEnviar.Send(oEmail);
            oEmail.Dispose();
        }

        #endregion

        #region Recupera

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

                    dbCommand.Parameters["@CodigoClienteSAP"].Value = this.NomeCliente.Substring(0, 10);

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

        #endregion

        #region FormataTexto

        public void FormataTexto()
        {
            string CorpoEmail = "";

            CorpoEmail += "<table border=\"1\">";
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

        public void FormataTextoTicket()
        {
            string CorpoEmail = "";

            CorpoEmail += "<table border=\"1\">";
            CorpoEmail += "<tr bgcolor=\"#DCDCDC\"><td colspan=\"2\"><b>" + this.TipoSolicitacao + "</b></td></tr>";
            CorpoEmail += "<tr><td>Empresa:</td><td>" + this.NomeEmpresa + "</td></tr>";
            CorpoEmail += "<tr><td>Cliente:</td><td>" + this.NomeCliente + "</td></tr>";
            CorpoEmail += "<tr><td>Num. Ticket:</td><td>" + this.IDTicket + "</td></tr>";
            CorpoEmail += "<tr><td>Solicitante:</td><td>" + this.Solicitante + "</td></tr>";
            CorpoEmail += "<tr><td>Data Solicitação:</td><td>" + this.DataAlteracao + "</td></tr>";
            CorpoEmail += "<tr bgcolor=\"#DCDCDC\"><td colspan=\"2\"><b>Descrição</b></td></tr>";
            CorpoEmail += "<tr><td colspan=\"2\">" + Historico + "</td></tr>";
            CorpoEmail += "</table>";

            this.TextoFormatado = CorpoEmail;
        }

        public void FormataTextoTicketHistorico()
        {
            string CorpoEmail = "";

            CorpoEmail += "<table border=\"1\">";
            CorpoEmail += "<tr bgcolor=\"#DCDCDC\"><td colspan=\"2\"><b>" + this.TipoSolicitacao + "</b></td></tr>";
            CorpoEmail += "<tr><td>Empresa:</td><td>" + this.NomeEmpresa + "</td></tr>";
            CorpoEmail += "<tr><td>Cliente:</td><td>" + this.NomeCliente + "</td></tr>";
            CorpoEmail += "<tr><td>Num. Ticket:</td><td>" + this.IDTicket + "</td></tr>";
            CorpoEmail += "<tr><td>Solicitante:</td><td>" + this.Solicitante + "</td></tr>";
            CorpoEmail += "<tr><td>Evento:</td><td>" + this.Evento + "</td></tr>";
            CorpoEmail += "<tr><td>Categoria:</td><td>" + this.Categoria + "</td></tr>";
            CorpoEmail += "<tr><td>Data Solicitação:</td><td>" + this.DataAlteracao + "</td></tr>";
            CorpoEmail += "<tr bgcolor=\"#DCDCDC\"><td colspan=\"2\"><b>Descrição</b></td></tr>";
            CorpoEmail += "<tr><td colspan=\"2\">" + Historico + "</td></tr>";
            CorpoEmail += "</table>";

            this.TextoFormatado = CorpoEmail;
        }

        public void FormataTextoClassificacaoComercial
            (string Cliente, string CNPJ, string DataSolicitacao, string Classificacao, string NomeVendedor)
        {
            UtilClass objUtilClass = new UtilClass();

            string CorpoEmail = "";

            CorpoEmail += "<style>td {word-wrap: break-word;}</style>";
            CorpoEmail += "<table border=\"1\">";
            CorpoEmail += "<tr bgcolor=\"#DCDCDC\"><td colspan=\"2\"><b>" + "Classificação Comercial" + "</b></td></tr>";
            CorpoEmail += "<tr><td>Cliente:</td><td>" + Cliente + "</td></tr>";
            CorpoEmail += "<tr><td>CNPJ:</td><td>" + CNPJ + "</td></tr>";
            CorpoEmail += "<tr><td>Data Solicitação:</td><td>" + Convert.ToDateTime(DataSolicitacao).ToString("dd/MM/yyyy") + "</td></tr>";
            CorpoEmail += "<tr><td>Data Classificação:</td><td>" + DateTime.Today.ToString("dd/MM/yyyy") + "</td></tr>";
            //CorpoEmail += "<tr><td>Classificação:</td><td>" + Classificacao + "</td></tr>";
            CorpoEmail += "<tr><td>Vendedor:</td><td>" + NomeVendedor + "</td></tr>";
            CorpoEmail += "<tr bgcolor=\"#DCDCDC\"><td colspan=\"2\"><b>Descrição</b></td></tr>";
            CorpoEmail += "<tr><td colspan=\"2\">" + Historico + "</td></tr>";
            CorpoEmail += "</table>";

            this.TextoFormatado = CorpoEmail;
        }

        #endregion

        #region Posicao Diaria

        protected string FormataTextoPosicaoDiariaPeriodos(int IDPosicaoDiaria)
        {
            ControladoriaClass objControladoriaClass = new ControladoriaClass();

            objControladoriaClass.PeriodoInicial = DateTime.Now;
            objControladoriaClass.PeriodoFinal = DateTime.Now;
            objControladoriaClass.Usuario = "";
            objControladoriaClass.IDPosicaoDiaria = IDPosicaoDiaria;

            DataTable PeriodosDataTable = objControladoriaClass.Consulta_CRM_POSICAO_DIARIA();

            DateTime PeriodoInicial = new DateTime(), PeriodoFinal = new DateTime();

            if (PeriodosDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in PeriodosDataTable.Rows)
                {
                    PeriodoInicial = Convert.ToDateTime(row["PeriodoInicial"]);

                    PeriodoFinal = Convert.ToDateTime(row["PeriodoFinal"]);
                }
            }

            StringBuilder Periodos = new StringBuilder();

            Periodos.AppendLine("Faturado/Devolução: "
            + PeriodoInicial.ToString("dd-MM-yyyy").Replace("-", "/") + " - "
            + PeriodoFinal.ToString("dd-MM-yyyy").Replace("-", "/"));

            Periodos.AppendLine("<br>");

            PeriodoInicial = PeriodoInicial.AddDays(-360);

            Periodos.AppendLine("Pendente: "
            + PeriodoInicial.ToString("dd-MM-yyyy").Replace("-", "/") + " - "
            + PeriodoFinal.ToString("dd-MM-yyyy").Replace("-", "/"));

            Periodos.AppendLine("<br><br>");

            return Periodos.ToString();
        }

        private void FormataTextoPosicaoDiariaAdicionaAnexo(int IDPosicaoDiaria)
        {
            ControladoriaClass objControladoriaClass = new ControladoriaClass();

            objControladoriaClass.IDPosicaoDiaria = IDPosicaoDiaria;

            objControladoriaClass.IDEmpresa = 0;

            objControladoriaClass.Status = "Todos";

            objControladoriaClass.Cliente = "";

            objControladoriaClass.IDGrupo = 0;

            DataTable Excel = new DataTable();

            this.PosicaoDiariaAnexos = new MailMessage().Attachments;

            string tabelaHTML;

            {
                tabelaHTML = "";

                tabelaHTML += FormataHTMLPosicaoDiaria_Tabela(IDPosicaoDiaria, true);

                MemoryStream MSAnexo = new MemoryStream();

                // Converta a string HTML em um fluxo de memória
                byte[] byteArray = Encoding.UTF8.GetBytes(tabelaHTML);
                MSAnexo.Write(byteArray, 0, byteArray.Length);
                MSAnexo.Position = 0;

                Attachment objAttachment = new Attachment(MSAnexo, "ConsolidadoGeral.xls");

                this.PosicaoDiariaAnexos.Add(objAttachment);
            }

            {
                tabelaHTML = "";

                Excel = new DataTable();

                Excel = objControladoriaClass.Consulta_CRM_POSICAO_DIARIA_FATURADOS(1);

                if (Excel.Rows.Count > 0)
                {
                    tabelaHTML = objControladoriaClass.MontaTabelaHtmlDoExcel(Excel);

                    MemoryStream MSAnexo = new MemoryStream();

                    // Converta a string HTML em um fluxo de memória
                    byte[] byteArray = Encoding.UTF8.GetBytes(tabelaHTML);
                    MSAnexo.Write(byteArray, 0, byteArray.Length);
                    MSAnexo.Position = 0;

                    Attachment objAttachment = new Attachment(MSAnexo, "ConsolidadoGeralFaturados.xls");

                    this.PosicaoDiariaAnexos.Add(objAttachment);
                }
            }

            {
                tabelaHTML = "";

                Excel = new DataTable();

                Excel = objControladoriaClass.Consulta_CRM_POSICAO_DIARIA_PENDENTES(1);

                if (Excel.Rows.Count > 0)
                {
                    tabelaHTML = objControladoriaClass.MontaTabelaHtmlDoExcel(Excel);

                    MemoryStream MSAnexo = new MemoryStream();

                    // Converta a string HTML em um fluxo de memória
                    byte[] byteArray = Encoding.UTF8.GetBytes(tabelaHTML);
                    MSAnexo.Write(byteArray, 0, byteArray.Length);
                    MSAnexo.Position = 0;

                    Attachment objAttachment = new Attachment(MSAnexo, "ConsolidadoGeralPendentes.xls");

                    this.PosicaoDiariaAnexos.Add(objAttachment);
                }
            }

            {
                tabelaHTML = "";

                Excel = new DataTable();

                Excel = objControladoriaClass.Consulta_CRM_POSICAO_DIARIA_DEVOLUCOES(1);

                if (Excel.Rows.Count > 0)
                {
                    tabelaHTML = objControladoriaClass.MontaTabelaHtmlDoExcel(Excel);

                    MemoryStream MSAnexo = new MemoryStream();

                    // Converta a string HTML em um fluxo de memória
                    byte[] byteArray = Encoding.UTF8.GetBytes(tabelaHTML);
                    MSAnexo.Write(byteArray, 0, byteArray.Length);
                    MSAnexo.Position = 0;

                    Attachment objAttachment = new Attachment(MSAnexo, "ConsolidadoGeralDevolucoes.xls");

                    this.PosicaoDiariaAnexos.Add(objAttachment);
                }
            }
        }

        public void FormataHTMLPosicaoDiaria(int IDPosicaoDiaria)
        {
            StringBuilder HTML = new StringBuilder();

            this.TituloEmail = "Diario de vendas " + DateTime.Now.ToString("dd MM yyyy");

            HTML.AppendLine(FormataHTMLPosicaoDiaria_Cabecalho(IDPosicaoDiaria));

            HTML.AppendLine(FormataHTMLPosicaoDiaria_Tabela(IDPosicaoDiaria));

            this.TextoFormatado = HTML.ToString();

            FormataTextoPosicaoDiariaAdicionaAnexo(IDPosicaoDiaria);
        }

        protected string FormataHTMLPosicaoDiaria_Cabecalho(int IDPosicaoDiaria)
        {
            StringBuilder HTML = new StringBuilder();

            HTML.AppendLine("<h3>Diario de vendas " + DateTime.Now.ToString("dd MM yyyy") + "</h3>");

            HTML.AppendLine(FormataTextoPosicaoDiariaPeriodos(IDPosicaoDiaria));

            return HTML.ToString();
        }

        bool Excel;

        public string FormataHTMLPosicaoDiaria_Tabela(int IDPosicaoDiaria, bool Excel = false)
        {
            this.Excel = Excel;

            ControladoriaClass objControladoriaClass = new ControladoriaClass();

            objControladoriaClass.IDPosicaoDiaria = IDPosicaoDiaria;

            DataTable PosicaoDiariaDataTable = objControladoriaClass.CRM_SP_RETORNA_POSICAO_DIARIA_ESTRATIFICACAO_Resumo_tabela_Excel_Completo();

            StringBuilder HTML = new StringBuilder();

            HTML.AppendLine("<html xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns:m=\"http://schemas.microsoft.com/office/2004/12/omml\" xmlns=\"http://www.w3.org/TR/REC-html40\">");

            HTML.AppendLine(FormataHTMLPosicaoDiaria_head());

            HTML.AppendLine(FormataHTMLPosicaoDiaria_body(PosicaoDiariaDataTable));

            HTML.AppendLine("</html>");

            return HTML.ToString();
        }

        private string FormataHTMLPosicaoDiaria_head()
        {
            StringBuilder HTML = new StringBuilder();

            HTML.AppendLine("   <head>");
            HTML.AppendLine("      <meta http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\">");
            HTML.AppendLine("      <meta name=Generator content=\"Microsoft Word 15 (filtered medium)\">");
            HTML.AppendLine("      <style>");
            HTML.AppendLine("         <!--");
            HTML.AppendLine("            /* Font Definitions */");
            HTML.AppendLine("            @font-face");
            HTML.AppendLine("            	{font-family:\"Cambria Math\";");
            HTML.AppendLine("            	panose-1:2 4 5 3 5 4 6 3 2 4;}");
            HTML.AppendLine("            @font-face");
            HTML.AppendLine("            	{font-family:Calibri;");
            HTML.AppendLine("            	panose-1:2 15 5 2 2 2 4 3 2 4;}");
            HTML.AppendLine("            @font-face");
            HTML.AppendLine("            	{font-family:\"Calibri Light\";");
            HTML.AppendLine("            	panose-1:2 15 3 2 2 2 4 3 2 4;}");
            HTML.AppendLine("            /* Style Definitions */");
            HTML.AppendLine("            p.MsoNormal, li.MsoNormal, div.MsoNormal");
            HTML.AppendLine("            	{margin:0cm;");
            HTML.AppendLine("            	font-size:11.0pt;");
            HTML.AppendLine("            	font-family:\"Calibri\",sans-serif;");
            HTML.AppendLine("            	mso-ligatures:standardcontextual;");
            HTML.AppendLine("            	mso-fareast-language:EN-US;}");
            HTML.AppendLine("            span.EstiloDeEmail17");
            HTML.AppendLine("            	{mso-style-type:personal-compose;");
            HTML.AppendLine("            	font-family:\"Calibri\",sans-serif;");
            HTML.AppendLine("            	color:windowtext;}");
            HTML.AppendLine("            .MsoChpDefault");
            HTML.AppendLine("            	{mso-style-type:export-only;");
            HTML.AppendLine("            	mso-fareast-language:EN-US;}");
            HTML.AppendLine("            @page WordSection1");
            HTML.AppendLine("            	{size:612.0pt 792.0pt;");
            HTML.AppendLine("            	margin:70.85pt 3.0cm 70.85pt 3.0cm;}");
            HTML.AppendLine("            div.WordSection1");
            HTML.AppendLine("            	{page:WordSection1;}");
            HTML.AppendLine("            -->");
            HTML.AppendLine("      </style>");
            HTML.AppendLine("      <!--[if gte mso 9]>");
            HTML.AppendLine("      <xml>");
            HTML.AppendLine("         <o:shapedefaults v:ext=\"edit\" spidmax=\"1026\" />");
            HTML.AppendLine("      </xml>");
            HTML.AppendLine("      <![endif]--><!--[if gte mso 9]>");
            HTML.AppendLine("      <xml>");
            HTML.AppendLine("         <o:shapelayout v:ext=\"edit\">");
            HTML.AppendLine("            <o:idmap v:ext=\"edit\" data=\"1\" />");
            HTML.AppendLine("         </o:shapelayout>");
            HTML.AppendLine("      </xml>");
            HTML.AppendLine("      <![endif]-->");
            HTML.AppendLine("   </head>");

            return HTML.ToString();
        }

        private string FormataHTMLPosicaoDiaria_body(DataTable PosicaoDiariaDataTable)
        {
            StringBuilder HTML = new StringBuilder();

            HTML.AppendLine("   <body lang=PT-BR link=\"#0563C1\" vlink=\"#954F72\" style='word-wrap:break-word'>");

            HTML.AppendLine("       <div class=WordSection1>");

            HTML.AppendLine(FormataHTMLPosicaoDiaria_body_table(PosicaoDiariaDataTable));

            HTML.AppendLine("       </div>");

            HTML.AppendLine("   </body>");

            return HTML.ToString();
        }

        private string FormataHTMLPosicaoDiaria_body_table(DataTable PosicaoDiariaDataTable)
        {
            StringBuilder HTML = new StringBuilder();

            HTML.AppendLine("           <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width=1600 style='width:1200.00pt;margin-left:.1pt;border-collapse:collapse'>");

            for (int i = 0; i < PosicaoDiariaDataTable.Rows.Count; i++)
            {
                HTML.AppendLine(FormataHTMLPosicaoDiaria_body_table_tr(PosicaoDiariaDataTable, i));
            }

            HTML.AppendLine("           </table>");

            return HTML.ToString();
        }

        int maiorString;

        private string FormataHTMLPosicaoDiaria_body_table_tr(DataTable PosicaoDiariaDataTable, int i)
        {
            StringBuilder HTML = new StringBuilder();

            HTML.AppendLine("             <tr style='height:15.0pt'>");

            for (int j = 0; j < PosicaoDiariaDataTable.Columns.Count; j++)
            {
                maiorString = 0;

                for (int k = 0; k < PosicaoDiariaDataTable.Columns.Count; k++)
                {
                    string palavra = PosicaoDiariaDataTable.Rows[k][j].ToString();

                    int tamanhoPalavra = palavra.Length;

                    if (tamanhoPalavra > maiorString)
                        maiorString = tamanhoPalavra;
                }

                HTML.AppendLine(FormataHTMLPosicaoDiaria_body_table_tr_td(PosicaoDiariaDataTable, i, j));
            }

            HTML.AppendLine("             </tr>");

            return HTML.ToString();
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            StringBuilder HTML = new StringBuilder();

            HTML.AppendLine("               <td width=" + FormataHTMLPosicaoDiaria_body_table_tr_td_style_width()
            + " nowrap valign=bottom style='" + FormataHTMLPosicaoDiaria_body_table_tr_td_style(PosicaoDiariaDataTable, i, j) + "'>");

            HTML.AppendLine(FormataHTMLPosicaoDiaria_body_table_tr_td_p(PosicaoDiariaDataTable, i, j));

            HTML.Append("               </td>");

            return HTML.ToString();
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td_p(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            StringBuilder HTML = new StringBuilder();

            try
            {
                decimal teste = Convert.ToDecimal(PosicaoDiariaDataTable.Rows[i][j].ToString());

                HTML.AppendLine("                   <p class=MsoNormal style='text-align:right'>");
            }
            catch
            {
                if (PosicaoDiariaDataTable.Rows[i][j].ToString() == "STRETCH Manuli CTBA"
                 || PosicaoDiariaDataTable.Rows[i][j].ToString() == "STRETCH Manuli Manaus"
                 || PosicaoDiariaDataTable.Rows[i][j].ToString() == "Total STRETCH Nac+"
                 || PosicaoDiariaDataTable.Rows[i][j].ToString() == "BACKLOG"
                 || (j > 0 && PosicaoDiariaDataTable.Rows[i][j - 1].ToString() == "BACKLOG"))
                    HTML.AppendLine("                   <p class=MsoNormal style='text-align:right'>");
                else
                    HTML.AppendLine("                   <p class=MsoNormal>");
            }

            HTML.Append(FormataHTMLPosicaoDiaria_body_table_tr_td_p_b(PosicaoDiariaDataTable, i, j));

            HTML.Append("                   </p>");

            return HTML.ToString();
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td_p_b(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            StringBuilder HTML = new StringBuilder();

            if (FormataHTMLPosicaoDiaria_body_table_tr_td_p_b_bool(PosicaoDiariaDataTable, i, j))
                HTML.AppendLine("                       <b>");

            HTML.AppendLine(FormataHTMLPosicaoDiaria_body_table_tr_td_p_b_span(PosicaoDiariaDataTable, i, j));

            if (FormataHTMLPosicaoDiaria_body_table_tr_td_p_b_bool(PosicaoDiariaDataTable, i, j))
                HTML.AppendLine("                       </b>");

            return HTML.ToString();
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td_p_b_span(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            StringBuilder HTML = new StringBuilder();

            HTML.AppendLine("                           <span style='font-family:\"Calibri Light\",sans-serif;color:" + FormataHTMLPosicaoDiaria_body_table_tr_td_p_b_span_color(PosicaoDiariaDataTable, i, j) + ";mso-ligatures:none;mso-fareast-language:PT-BR'>");

            HTML.AppendLine(FormataHTMLPosicaoDiaria_body_table_tr_td_p_b_span_string(PosicaoDiariaDataTable, i, j));

            HTML.Append("                           </span>");

            return HTML.ToString();
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td_p_b_span_color(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            if (PosicaoDiariaDataTable.Rows[i][j].ToString() == "Total STRETCH Nac+"
             || PosicaoDiariaDataTable.Rows[i][j].ToString() == "BACKLOG"
             || (j > 0 && PosicaoDiariaDataTable.Rows[i][j - 1].ToString() == "BACKLOG"))
                return "red";

            if (FormataHTMLPosicaoDiaria_body_table_tr_td__p_b_span_color__style_background__white__blue(PosicaoDiariaDataTable, i, j))
                return "white";

            return "black";
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td_p_b_span_string(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            string palavra = PosicaoDiariaDataTable.Rows[i][j].ToString();

            if (Excel)
            {
                UtilClass objUtilClass = new UtilClass();

                palavra = objUtilClass.removerAcentos(palavra);
            }

            StringBuilder HTML = new StringBuilder();

            if (palavra.Trim() != "")
                HTML.AppendLine("                               " + palavra);
            else
                HTML.AppendLine("                               &nbsp;");

            HTML.Append("                               <o:p></o:p>");

            return HTML.ToString();
        }

        private bool FormataHTMLPosicaoDiaria_body_table_tr_td_p_b_bool(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            string palavra = PosicaoDiariaDataTable.Rows[i][j].ToString();

            return (palavra == "Faturado/Devolução"
                 || palavra == "Pendente"
                 || PosicaoDiariaDataTable.Rows[i][0].ToString() == "Empresa"
                 //|| palavra == "Família"
                 //|| palavra == "KG/mq"
                 //|| palavra == "R$"
                 //|| palavra == "AVG"
                 || PosicaoDiariaDataTable.Rows[i][0].ToString() == "MANULI AM 06300 Total"
                 || PosicaoDiariaDataTable.Rows[i][0].ToString() == "MANULI CTBA Total"
                 || PosicaoDiariaDataTable.Rows[i][0].ToString() == "Total Geral"
                 || ((PosicaoDiariaDataTable.Rows[i][0].ToString() == "Total Manaus"
                    || PosicaoDiariaDataTable.Rows[i][0].ToString() == "Total Fitasa")
                    && ((j > 1 && j < 4) || j == 0))
                 || palavra == "Consolidado"
                 || PosicaoDiariaDataTable.Rows[i][0].ToString() == "Total Consolidado"
                 || palavra == "Mil"
                 || palavra == "kR$"
                 || palavra == "PMV"
                 || palavra == "STRETCH Manuli CTBA"
                 || palavra == "STRETCH Manuli Manaus"
                 || palavra == "Total STRETCH Nac+"
                 || palavra == "BACKLOG"
                 || (PosicaoDiariaDataTable.Rows[i][5].ToString() == "Total STRETCH Nac+" && j > 5)
                 || (PosicaoDiariaDataTable.Rows[i][5].ToString() == "BACKLOG" && j > 5));
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td_style(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            StringBuilder HTML = new StringBuilder();

            HTML.Append("width:" + FormataHTMLPosicaoDiaria_body_table_tr_td_style_width() + ";");

            HTML.Append(FormataHTMLPosicaoDiaria_body_table_tr_td_style_borders(PosicaoDiariaDataTable, i, j));

            HTML.Append("background:" + FormataHTMLPosicaoDiaria_body_table_tr_td_style_background(PosicaoDiariaDataTable, i, j) + ";");

            HTML.Append("padding:" + FormataHTMLPosicaoDiaria_body_table_tr_td_style_padding(PosicaoDiariaDataTable, i, j) + ";");

            HTML.Append("height:" + FormataHTMLPosicaoDiaria_body_table_tr_td_style_height(PosicaoDiariaDataTable, i, j) + ";");

            return HTML.ToString();
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td_style_borders(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            StringBuilder HTML = new StringBuilder();

            HTML.Append("border-top:" + FormataHTMLPosicaoDiaria_body_table_tr_td_style_border_top(PosicaoDiariaDataTable, i, j) + ";");

            HTML.Append("border-bottom:" + FormataHTMLPosicaoDiaria_body_table_tr_td_style_border_bottom(PosicaoDiariaDataTable, i, j) + ";");

            HTML.Append("border-left:" + FormataHTMLPosicaoDiaria_body_table_tr_td_style_border_left(PosicaoDiariaDataTable, i, j) + ";");

            HTML.Append("border-right:" + FormataHTMLPosicaoDiaria_body_table_tr_td_style_border_right(PosicaoDiariaDataTable, i, j) + ";");

            return HTML.ToString();
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td_style_width()
        {
            return (maiorString * 5.45).ToString().Replace(",", ".") + "pt";
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td_style_border_top(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            if (PosicaoDiariaDataTable.Rows[i][j].ToString() == "STRETCH Manuli CTBA"
                 || PosicaoDiariaDataTable.Rows[i][j].ToString() == "STRETCH Manuli Manaus"
                 || PosicaoDiariaDataTable.Rows[i][j].ToString() == "Total STRETCH Nac+"
                 || PosicaoDiariaDataTable.Rows[i][j].ToString() == "BACKLOG")
                return "none";

            if (PosicaoDiariaDataTable.Rows[i][0].ToString().Trim() != "Total Geral")
                if (i > 0)
                    if (PosicaoDiariaDataTable.Rows[i - 1][j].ToString().Trim() == ""
                        || (PosicaoDiariaDataTable.Rows[i - 1][j].ToString() == "MANULI AM 06300"
                            || PosicaoDiariaDataTable.Rows[i - 1][j].ToString() == "MANULI CTBA"
                            || PosicaoDiariaDataTable.Rows[i - 1][j].ToString() == "Total STRETCH Nac+"
                            || PosicaoDiariaDataTable.Rows[i - 1][j].ToString() == "Consolidado")
                        || (PosicaoDiariaDataTable.Rows[i][j].ToString() == ""
                            && PosicaoDiariaDataTable.Rows[i - 1][j].ToString() == "BACKLOG")
                        || (j > 0 && PosicaoDiariaDataTable.Rows[i][j - 1].ToString() == "BACKLOG"
                            && PosicaoDiariaDataTable.Rows[i + 1][j].ToString() == "Mil"))
                        return "none";

            return "solid black 1.0pt";
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td_style_border_bottom(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            if (j < (PosicaoDiariaDataTable.Columns.Count - 1))
                if (PosicaoDiariaDataTable.Rows[i][j + 1].ToString().Trim() == "Mil")
                    return "none";

            if (PosicaoDiariaDataTable.Rows[i][j].ToString() == "STRETCH Manuli CTBA"
                 || PosicaoDiariaDataTable.Rows[i][j].ToString() == "STRETCH Manuli Manaus"
                 || PosicaoDiariaDataTable.Rows[i][j].ToString() == "Total STRETCH Nac+"
                 || PosicaoDiariaDataTable.Rows[i][j].ToString() == "BACKLOG")
                return "none";

            if (PosicaoDiariaDataTable.Rows[i][0].ToString().Trim() != "Total Geral")
                if (i < (PosicaoDiariaDataTable.Rows.Count - 1))
                    if (PosicaoDiariaDataTable.Rows[i + 1][j].ToString().Trim() == ""
                        || (PosicaoDiariaDataTable.Rows[i][j].ToString() == "MANULI AM 06300"
                            || PosicaoDiariaDataTable.Rows[i][j].ToString() == "MANULI CTBA")
                        || (PosicaoDiariaDataTable.Rows[i][j].ToString() == ""
                            && PosicaoDiariaDataTable.Rows[i + 1][j].ToString().Trim() == "BACKLOG")
                        || (PosicaoDiariaDataTable.Rows[i][j].ToString().Trim() == ""
                            && j > 0 && PosicaoDiariaDataTable.Rows[i + 1][j - 1].ToString() == "BACKLOG"
                            && PosicaoDiariaDataTable.Rows[i + 2][j].ToString().Trim() == "Mil"))
                        return "none";

            return "solid black 1.0pt";
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td_style_border_left(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            if (j > 0)
                if (PosicaoDiariaDataTable.Rows[i][j - 1].ToString().Trim() == ""
                || (PosicaoDiariaDataTable.Rows[i][j - 1].ToString() == "BACKLOG")
                || (j > 1 && PosicaoDiariaDataTable.Rows[i][j - 2].ToString() == "BACKLOG"))
                    return "none";

            return "solid black 1.0pt";
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td_style_border_right(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            if (j < (PosicaoDiariaDataTable.Columns.Count - 1))
                if (
                    (PosicaoDiariaDataTable.Rows[i][j + 1].ToString().Trim() == ""
                        && !((PosicaoDiariaDataTable.Rows[i][0].ToString() == "Total Fitasa"
                            || PosicaoDiariaDataTable.Rows[i][0].ToString() == "Total Consolidado")
                            && j == 4))
                 || (PosicaoDiariaDataTable.Rows[i][j].ToString() == "BACKLOG")
                 || (j > 0 && PosicaoDiariaDataTable.Rows[i][j - 1].ToString() == "BACKLOG")
                   )
                    return "none";

            return "solid black 1.0pt";
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td_style_background(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            if (FormataHTMLPosicaoDiaria_body_table_tr_td__p_b_span_color__style_background__white__blue(PosicaoDiariaDataTable, i, j))
                return "#203764";

            //#EDEDED cinza

            return "none";
        }

        private bool FormataHTMLPosicaoDiaria_body_table_tr_td__p_b_span_color__style_background__white__blue
            (DataTable PosicaoDiariaDataTable, int i, int j)
        {
            if ((PosicaoDiariaDataTable.Rows[i][j].ToString() == "Mil")
             || (PosicaoDiariaDataTable.Rows[i][j].ToString() == "kR$")
             || (PosicaoDiariaDataTable.Rows[i][j].ToString() == "PMV"))
                return true;

            string primeiraPalavraLinha = PosicaoDiariaDataTable.Rows[i][0].ToString();

            if (j < (PosicaoDiariaDataTable.Columns.Count - 1))
                if (PosicaoDiariaDataTable.Rows[i][j + 1].ToString() == "Mil")
                    return false;

            if (j > 5 && j < 9 && PosicaoDiariaDataTable.Rows[i][5].ToString() == "Total STRETCH Nac+")
                return true;

            if (i > 0)
                if (j > 4 && PosicaoDiariaDataTable.Rows[i - 1][5].ToString() == "Total STRETCH Nac+")
                    return false;

            if (j > 4 && (primeiraPalavraLinha == "Total Fitasa"
            || primeiraPalavraLinha == "Total Consolidado"))
                return false;

            if (j == 9)
                if (PosicaoDiariaDataTable.Rows[i][6].ToString() == "Mil"
                 || PosicaoDiariaDataTable.Rows[i][5].ToString() == "Total STRETCH Nac+")
                    return false;

            return ((i == 0)
                 || (primeiraPalavraLinha == "Total Geral")
                 || (primeiraPalavraLinha == "Empresa" && i > 1)
                 || (primeiraPalavraLinha == "Total Consolidado")
                 || (primeiraPalavraLinha == "Total Manaus")
                 || (primeiraPalavraLinha == "Total Fitasa"));
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td_style_padding(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            return "0cm 3.5pt 0cm 3.5pt";
        }

        private string FormataHTMLPosicaoDiaria_body_table_tr_td_style_height(DataTable PosicaoDiariaDataTable, int i, int j)
        {
            return "15.0pt";
        }

        #endregion
    }
}