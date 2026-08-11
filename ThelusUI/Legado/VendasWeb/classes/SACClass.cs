using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using VendasWeb.Email;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;

namespace VendasWeb
{
    public class SACClass : GerencialVendas.clsConexao
    {
        ParametroGeral ObjParametroGeral = new ParametroGeral();

        #region Campos

        public string Filtro { get; set; }

        public string TipoFiltro { get; set; }

        public string Operacao { get; set; }

        public int IDSituacao { get; set; }

        public int IDUsuario { get; set; }

        public string Descricao { get; set; }

        public string DescricaoAtividade { get; set; }

        public bool Ativo { get; set; }

        public bool Bloqueado { get; set; }

        public int IDEmpresa { get; set; }

        public string Empresa { get; set; }

        public int IDTicket { get; set; }

        public string Atividade { get; set; }

        public int IDAtividade { get; set; }

        public int IDSetor { get; set; }

        public int IDUsuarioResponsavel { get; set; }

        public string Assunto { get; set; }

        public string AssuntoAtividade { get; set; }

        public int IDClassificacao { get; set; }

        public bool Padrao { get; set; }

        public int IDPrioridade { get; set; }

        public string Tela { get; set; }

        public string Cliente { get; set; }

        public string CodigoCliente { get; set; }

        public string Pessoa { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public int IDCliente { get; set; }

        public int IDTipo { get; set; }

        public int IDContato { get; set; }

        public string Solicitante { get; set; }

        public string DataSolicitacao { get; set; }

        public string DataFechamento { get; set; }

        public string Historico { get; set; }

        public string TipoHistorico { get; set; }

        public int IDCategoria { get; set; }

        public int IDEvento { get; set; }

        public int IDChamado { get; set; }

        public int IDResponsavel { get; set; }

        public string Evento { get; set; }

        public string Categoria { get; set; }

        public int IDAnexo { get; set; }

        public string CaminhoDestino { get; set; }

        public string Extensao { get; set; }

        public string DataAnexo { get; set; }

        public string DataInicio { get; set; }

        public string DataFim { get; set; }

        public string DataAtividade { get; set; }

        public string Administrador { get; set; }

        public string Ticket { get; set; }

        public string Solucao { get; set; }

        public int IDSolucao { get; set; }

        public string TipoOcorrencia { get; set; }

        public int IDTipoOcorrencia { get; set; }

        public string Motivo { get; set; }

        public int IDMotivo { get; set; }

        public string AberturaInicial { get; set; }

        public string AberturaFinal { get; set; }

        public string FechamentoInicial { get; set; }

        public string FechamentoFinal { get; set; }

        public int IDVendedor { get; set; }

        public int IDNota { get; set; }

        public int NumeroSerial { get; set; }

        public string DataFaturamento { get; set; }

        #endregion

        #region Campos E-Mail

        EmailTemplateClass OBJEmail = new EmailTemplateClass();
        public string EmailPara { get; set; }
        public string EmailOperacao { get; set; }
        public string NomeUsuario { get; set; }
        public string CabecalhoEmail { get; set; }
        public string TituloEmail { get; set; }
        public string DetalheEmail { get; set; }

        #endregion

        #region Campos Anexos

        public string ArquivoAnexo { get; set; }
        public string ArquivoExtensao { get; set; }
        public string NomeArquivo { get; set; }
        public string DescricaoArquivo { get; set; }
        public string CaminhoPadraoAnexos { get; set; }

        #endregion

        ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();

        #region Ticket's

        public DataTable RetornaListaTickets()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_TICKETS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Tela", SqlDbType.VarChar, 8000, "Tela"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTicket", SqlDbType.Int, 0, "IDTicket"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSituacao", SqlDbType.Int, 0, "IDSituacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@AberturaInicial", SqlDbType.VarChar, 8000, "AberturaInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@AberturaFinal", SqlDbType.VarChar, 8000, "AberturaFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@FechamentoInicial", SqlDbType.VarChar, 8000, "FechamentoInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@FechamentoFinal", SqlDbType.VarChar, 8000, "FechamentoFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@Cliente", SqlDbType.VarChar, 8000, "Cliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSolucao", SqlDbType.Int, 0, "IDSolucao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ticket", SqlDbType.VarChar, 8000, "Ticket"));

                    dbCommand.Parameters["@Tela"].Value = this.Tela;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDTicket"].Value = this.IDTicket;
                    dbCommand.Parameters["@IDSituacao"].Value = this.IDSituacao;
                    dbCommand.Parameters["@AberturaInicial"].Value = this.AberturaInicial ?? "";
                    dbCommand.Parameters["@AberturaFinal"].Value = this.AberturaFinal ?? "";
                    dbCommand.Parameters["@FechamentoInicial"].Value = this.FechamentoInicial ?? "";
                    dbCommand.Parameters["@FechamentoFinal"].Value = this.FechamentoFinal ?? "";
                    dbCommand.Parameters["@Cliente"].Value = this.Cliente;
                    dbCommand.Parameters["@IDSolucao"].Value = this.IDSolucao;
                    dbCommand.Parameters["@Ticket"].Value = this.Ticket ?? "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        #endregion

        #region Atividades

        public DataTable RetornaListaAtividades()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_ATIVIDADES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Tela", SqlDbType.VarChar, 8000, "Tela"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTicket", SqlDbType.Int, 0, "IDTicket"));
                    dbCommand.Parameters.Add(new SqlParameter("@Cliente", SqlDbType.VarChar, 8000, "Cliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@Solicitante", SqlDbType.VarChar, 8000, "Solicitante"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAtividade", SqlDbType.Int, 0, "IDAtividade"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSituacao", SqlDbType.Int, 0, "IDSituacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataInicio", SqlDbType.VarChar, 8000, "DataInicio"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFim", SqlDbType.VarChar, 8000, "DataFim"));
                    dbCommand.Parameters.Add(new SqlParameter("@Administrador", SqlDbType.Int, 0, "Administrador"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Atividade", SqlDbType.VarChar, 8000, "Atividade"));

                    dbCommand.Parameters["@Tela"].Value = this.Tela;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;
                    dbCommand.Parameters["@IDTicket"].Value = this.IDTicket;
                    dbCommand.Parameters["@Cliente"].Value = this.Cliente;
                    dbCommand.Parameters["@Solicitante"].Value = this.Solicitante;
                    dbCommand.Parameters["@IDAtividade"].Value = this.IDAtividade;
                    dbCommand.Parameters["@IDSituacao"].Value = this.IDSituacao;
                    dbCommand.Parameters["@DataInicio"].Value = this.DataInicio;
                    dbCommand.Parameters["@DataFim"].Value = this.DataFim;
                    dbCommand.Parameters["@Administrador"].Value = this.Administrador;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@Atividade"].Value = this.Atividade ?? "";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        #endregion

        #region Ticket's Atividades

        public DataTable RetornaListaTicketsAtividades()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_TICKETS_ATIVIDADES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Tela", SqlDbType.VarChar, 8000, "Tela"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTicket", SqlDbType.Int, 0, "IDTicket"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSituacao", SqlDbType.Int, 0, "IDSituacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataInicio", SqlDbType.VarChar, 8000, "DataInicio"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFim", SqlDbType.VarChar, 8000, "DataFim"));
                    dbCommand.Parameters.Add(new SqlParameter("@Atividade", SqlDbType.VarChar, 8000, "Atividade"));

                    dbCommand.Parameters["@Tela"].Value = this.Tela;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDTicket"].Value = this.IDTicket;
                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;
                    dbCommand.Parameters["@IDSituacao"].Value = this.IDSituacao;
                    dbCommand.Parameters["@DataInicio"].Value = this.DataInicio;
                    dbCommand.Parameters["@DataFim"].Value = this.DataFim;
                    dbCommand.Parameters["@Atividade"].Value = this.Atividade;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public string GravaTicketAtividades()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_TICKETS_ATIVIDADES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTicket", SqlDbType.Int, 0, "IDTicket"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAtividade", SqlDbType.Int, 0, "IDAtividade"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSituacao", SqlDbType.Int, 0, "IDSituacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDResponsavel", SqlDbType.Int, 0, "IDResponsavel"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataAtividade", SqlDbType.Date, 8000, "DataAtividade"));
                    dbCommand.Parameters.Add(new SqlParameter("@AssuntoAtividade", SqlDbType.VarChar, 8000, "AssuntoAtividade"));
                    dbCommand.Parameters.Add(new SqlParameter("@DescricaoAtividade", SqlDbType.VarChar, 8000, "DescricaoAtividade"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDTicket"].Value = this.IDTicket;
                    dbCommand.Parameters["@IDAtividade"].Value = this.IDAtividade;
                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;
                    dbCommand.Parameters["@IDSituacao"].Value = this.IDSituacao;
                    dbCommand.Parameters["@IDResponsavel"].Value = this.IDResponsavel;
                    dbCommand.Parameters["@DataAtividade"].Value = this.DataAtividade;
                    dbCommand.Parameters["@AssuntoAtividade"].Value = this.AssuntoAtividade;
                    dbCommand.Parameters["@DescricaoAtividade"].Value = this.DescricaoAtividade;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            this.IDEmpresa = Convert.ToInt32(row["IDEmpresa"].ToString());
                            this.IDTicket = Convert.ToInt32(row["IDTicket"].ToString());
                            this.IDAtividade = Convert.ToInt32(row["IDAtividade"].ToString());
                            this.Operacao = "Alteracao";
                        }
                    }

                    dataReader.Close();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }

                return erro;
            }

        }

        #endregion

        #region Ticket's Detalhe

        public string GravaTickets()
        {
            UtilClass ObjUtilClass = new UtilClass();
            DataTable outputTable = new DataTable();
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_TICKETS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTicket", SqlDbType.Int, 0, "IDTicket"));
                    dbCommand.Parameters.Add(new SqlParameter("@Solicitante", SqlDbType.VarChar, 8000, "Solicitante"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDResponsavel", SqlDbType.Int, 0, "IDResponsavel"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDClassificacao", SqlDbType.Int, 0, "IDClassificacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSituacao", SqlDbType.Int, 0, "IDSituacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataSolicitacao", SqlDbType.VarChar, 8000, "DataSolicitacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFechamento", SqlDbType.VarChar, 8000, "DataFechamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPrioridade", SqlDbType.Int, 0, "IDPrioridade"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSolucao", SqlDbType.Int, 0, "IDSolucao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoOcorrencia", SqlDbType.Int, 0, "IDTipoOcorrencia"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDVendedor", SqlDbType.Int, 0, "IDVendedor"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMotivo", SqlDbType.Int, 0, "IDMotivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDCliente"].Value = this.IDCliente;
                    dbCommand.Parameters["@IDTicket"].Value = this.IDTicket;
                    dbCommand.Parameters["@Solicitante"].Value = this.Solicitante;
                    dbCommand.Parameters["@IDResponsavel"].Value = this.IDResponsavel;
                    dbCommand.Parameters["@IDClassificacao"].Value = this.IDClassificacao;
                    dbCommand.Parameters["@IDSituacao"].Value = this.IDSituacao;
                    dbCommand.Parameters["@DataSolicitacao"].Value = this.DataSolicitacao;
                    dbCommand.Parameters["@DataFechamento"].Value = this.DataFechamento;
                    dbCommand.Parameters["@IDPrioridade"].Value = this.IDPrioridade;
                    dbCommand.Parameters["@IDSolucao"].Value = this.IDSolucao;
                    dbCommand.Parameters["@IDTipoOcorrencia"].Value = this.IDTipoOcorrencia;
                    dbCommand.Parameters["@IDVendedor"].Value = this.IDVendedor;
                    dbCommand.Parameters["@IDMotivo"].Value = this.IDMotivo;
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            this.IDEmpresa = Convert.ToInt32(row["IDEmpresa"].ToString());
                            this.IDTicket = Convert.ToInt32(row["IDTicket"].ToString());
                            this.Operacao = "Alteracao";
                        }
                    }

                    dataReader.Close();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        public string EnviaEmailTicket()
        {
            string erro = "";

            OBJEmail.cabecalho = "SAC - " + this.CabecalhoEmail;
            OBJEmail.titulo = "SAC - " + this.TituloEmail;
            OBJEmail.detalhe = this.DetalheEmail;
            OBJEmail.data = DateTime.Now.ToString("dd/MM/yyyy");
            OBJEmail.data = OBJEmail.data.Replace("-", "/");
            OBJEmail.emailpara = this.RecuperaEmail();
            OBJEmail.NomeUsuario = this.NomeUsuario;

            erro = OBJEmail.EnviaEmailTicket();

            OBJEmail.cabecalho = "";
            OBJEmail.titulo = "";
            OBJEmail.detalhe = "";
            OBJEmail.data = "";
            OBJEmail.emailpara = "";
            OBJEmail.NomeUsuario = "";

            this.CabecalhoEmail = "";
            this.TituloEmail = "";
            this.DetalheEmail = "";
            this.NomeUsuario = "";

            return erro;
        }

        public string RecuperaEmail()
        {
            string Email = "";
            string erro = "";

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_EMAIL_USUARIO_ID", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                Email = row["Email"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return Email;
        }

        #endregion

        #region Ticket's Contato

        public DataTable RetornaListaTicketsContatos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_TICKETS_CONTATOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Tela", SqlDbType.VarChar, 8000, "Tela"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTicket", SqlDbType.Int, 0, "IDTicket"));
                    dbCommand.Parameters.Add(new SqlParameter("@Pessoa", SqlDbType.VarChar, 8000, "Pessoa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 8000, "Email"));
                    dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.VarChar, 8000, "Telefone"));

                    dbCommand.Parameters["@Tela"].Value = this.Tela;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDTicket"].Value = this.IDTicket;
                    dbCommand.Parameters["@Pessoa"].Value = this.Pessoa;
                    dbCommand.Parameters["@Email"].Value = this.Email;
                    dbCommand.Parameters["@Telefone"].Value = this.Telefone;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public string GravaExcluiTicketContato()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_GRAVA_EXCLUI_TICKETS_CONTATOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTicket", SqlDbType.Int, 0, "IDTicket"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDContato", SqlDbType.Int, 0, "IDContato"));
                    dbCommand.Parameters.Add(new SqlParameter("@Pessoa", SqlDbType.VarChar, 8000, "Pessoa"));
                    dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 8000, "Email"));
                    dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.VarChar, 8000, "Telefone"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@Operacao"].Value = this.Operacao;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDTicket"].Value = this.IDTicket;
                    dbCommand.Parameters["@IDContato"].Value = this.IDContato;
                    dbCommand.Parameters["@Pessoa"].Value = this.Pessoa;
                    dbCommand.Parameters["@Email"].Value = this.Email;
                    dbCommand.Parameters["@Telefone"].Value = this.Telefone;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }

                return erro;
            }

        }

        #endregion

        #region Ticket's Historico

        public DataTable RetornaListaTicketsEvento()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_TICKETS_EVENTO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@TipoHistorico", SqlDbType.VarChar, 8000, "TipoHistorico"));

                    dbCommand.Parameters["@TipoHistorico"].Value = this.TipoHistorico;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public DataTable RetornaListaTicketsCategoria()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_TICKETS_CATEGORIA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@TipoHistorico", SqlDbType.VarChar, 8000, "TipoHistorico"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEvento", SqlDbType.Int, 0, "IDEvento"));

                    dbCommand.Parameters["@IDEvento"].Value = this.IDEvento;
                    dbCommand.Parameters["@TipoHistorico"].Value = this.TipoHistorico;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public string GravaTicketHistorico()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_HISTORICO_SAC_TICKETS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTicket", SqlDbType.Int, 0, "IDTicket"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Historico", SqlDbType.VarChar, 8000, "Historico"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoHistorico", SqlDbType.VarChar, 8000, "TipoHistorico"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEvento", SqlDbType.Int, 0, "IDEvento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCategoria", SqlDbType.Int, 0, "IDCategoria"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAtividade", SqlDbType.Int, 0, "IDAtividade"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDTicket"].Value = this.IDTicket;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@Historico"].Value = this.Historico;
                    dbCommand.Parameters["@TipoHistorico"].Value = this.TipoHistorico;
                    dbCommand.Parameters["@IDEvento"].Value = this.IDEvento;
                    dbCommand.Parameters["@IDCategoria"].Value = this.IDCategoria;
                    dbCommand.Parameters["@IDAtividade"].Value = this.IDAtividade;
                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    dataReader.Close();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }

                return erro;
            }

        }

        public void RetornaTicketHistorico()
        {
            //Limpa para não trazer lixo
            this.Historico = "";

            DataTable OBJData = new DataTable();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_HISTORICO_CHAMADOS_SAC_TICKET", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTicket", SqlDbType.Int, 0, "IDTicket"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAtividade", SqlDbType.Int, 0, "IDAtividade"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDTicket"].Value = this.IDTicket;
                    dbCommand.Parameters["@IDAtividade"].Value = this.IDAtividade;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        OBJData.Load(dataReader);

                        if (OBJData.Rows.Count > 0)
                        {
                            foreach (DataRow row in OBJData.Rows)
                            {
                                //Carrega icones
                                Historico += "<div class=\"timeline-entry\"> <div class=\"timeline-stat\"> ";
                                Historico += "<div class=\"" + row["TimeLineButonClass"].ToString() + "\"><i class=\"" + row["TimeLineIconClass"].ToString() + "\"></i> ";

                                //Carrega data
                                Historico += "</div><div class=\"timeline-time\"><b>" + row["DataHistorico"].ToString() + "</b></div> " + "</div><div class=\"timeline-label\"> ";

                                //Carrega Título Historico
                                Historico += "<p class=\"mar-no pad-btm\"> <span class=\"" + row["TimeLineTituloClass"].ToString() + "\">" + row["DescricaoEvento"].ToString() + " " + row["DescricaoCategoria"].ToString();

                                //Carrega Corpo Histórico
                                Historico += "</span> por <a href=\"#\" class=\"btn-link btn-md text-semibold\"> ";
                                Historico += row["CodigoUsuario"].ToString() + "</a></p>";
                                Historico += "<div class=\"well well-xs mar-no\"> ";
                                Historico += row["Historico"].ToString();
                                Historico += "</div>";

                                //Fecha Historico
                                Historico += "</div></div>";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }
        }

        #endregion

        #region Ticket's Anexo

        public DataTable RetornaListaTicketsAnexo()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_TICKETS_ANEXO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Tela", SqlDbType.VarChar, 8000, "Tela"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTicket", SqlDbType.Int, 0, "IDTicket"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAtividade", SqlDbType.Int, 0, "IDAtividade"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));

                    dbCommand.Parameters["@Tela"].Value = this.Tela;
                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDTicket"].Value = this.IDTicket;
                    dbCommand.Parameters["@IDAtividade"].Value = this.IDAtividade;
                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public string GravaArquivoServidor(FileUpload OBJArquivo)
        {
            string diretorio = "";
            string arquivo = "";
            string erro = "";
            string Pasta = "";
            string Extensao = "";
            int cont = 0;

            DataTable ObjDataTable = new DataTable();
            ObjParametroGeral.IDEmpresa = this.IDEmpresa;
            ObjParametroGeral.Filtro = "ANEXOSTICKETSSAC";
            ObjDataTable = ObjParametroGeral.RetornaListaParametrosGerais();

            if (ObjDataTable.Rows.Count > 0)
            {
                foreach (DataRow Row in ObjDataTable.Rows)
                {
                    diretorio = Row["ValorTexto"].ToString();
                }
            }

            //diretorio = "\\\\192.168.0.2\\crm\\AnexosTickets\\";

            Pasta = "Ticket_" + this.IDTicket.ToString();
            diretorio += Pasta + "\\";
            arquivo = diretorio + OBJArquivo.FileName;

            try
            {
                if (OBJArquivo.HasFile == true)
                {
                    //Cria pasta para o chamado caso não exista
                    if (!Directory.Exists(diretorio))
                    {
                        Directory.CreateDirectory(diretorio);
                    }

                    if (!File.Exists(arquivo))
                    {
                        OBJArquivo.PostedFile.SaveAs(arquivo);
                    }
                    else
                    {
                        cont++;

                        while (cont != 0)
                        {
                            cont++;

                            Extensao = Path.GetExtension(arquivo);
                            arquivo = Path.GetFileNameWithoutExtension(arquivo);

                            arquivo = diretorio + arquivo + "_" + cont.ToString() + Extensao;

                            if (!File.Exists(arquivo))
                            {
                                OBJArquivo.SaveAs(arquivo);
                                cont = 0;
                            }
                        }
                    }

                    //Atribui dados do arquivo 
                    this.ArquivoAnexo = arquivo;
                    this.ArquivoExtensao = Path.GetExtension(arquivo);
                    this.NomeArquivo = OBJArquivo.FileName;
                }
                else
                {
                    erro = "Nenhum arquivo selecionado.";
                }
            }
            catch (Exception ex)
            {
                erro = "Erro ao salvar arquivo.";
            }

            return erro;
        }

        public string GravaDadosTicketAnexos()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_TICKETS_ANEXO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTicket", SqlDbType.Int, 0, "IDTicket"));
                    dbCommand.Parameters.Add(new SqlParameter("@Caminhodestino", SqlDbType.VarChar, 8000, "Caminhodestino"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeArquivo", SqlDbType.VarChar, 8000, "NomeArquivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@ExtensaoArquivo", SqlDbType.VarChar, 8000, "ExtensaoArquivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@DescricaoArquivo", SqlDbType.VarChar, 8000, "DescricaoArquivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAtividade", SqlDbType.Int, 0, "IDAtividade"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDTicket"].Value = this.IDTicket;
                    dbCommand.Parameters["@Caminhodestino"].Value = this.ArquivoAnexo;
                    dbCommand.Parameters["@NomeArquivo"].Value = this.NomeArquivo;
                    dbCommand.Parameters["@ExtensaoArquivo"].Value = this.ArquivoExtensao;
                    dbCommand.Parameters["@DescricaoArquivo"].Value = this.DescricaoArquivo;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@IDAtividade"].Value = this.IDAtividade;
                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    this.IDEvento = 1;
                    this.IDCategoria = 5;
                    this.Historico = this.DescricaoArquivo;
                    OBJEmail.titulo = "Anexo de arquivo";
                    OBJEmail.detalhe = "Inserido arquivo: " + this.NomeArquivo;

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        public string ExcluiDadosTicketAnexos()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_TICKETS_ANEXO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTicket", SqlDbType.Int, 0, "IDTicket"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAtividade", SqlDbType.Int, 0, "IDAtividade"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnexo", SqlDbType.Int, 0, "IDAnexo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDTicket"].Value = this.IDTicket;
                    dbCommand.Parameters["@IDAtividade"].Value = this.IDAtividade;
                    dbCommand.Parameters["@IDAnexo"].Value = this.IDAnexo;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    this.IDEvento = 1;
                    this.IDCategoria = 6;
                    this.Historico = this.DescricaoArquivo;
                    OBJEmail.titulo = "Exclusão de arquivo";
                    OBJEmail.detalhe = "Excluido arquivo: " + this.NomeArquivo;

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        public string EnviaEmailAnexos()
        {
            string erro = "";

            //this.DescricaoEmail = this.Historico;
            if (this.IDResponsavel != 0) this.IDUsuario = this.IDResponsavel;
            this.EmailPara = RecuperaEmail();

            OBJEmail.cabecalho = "SAC - Ticket - " + this.IDTicket.ToString();
            if (this.IDAtividade != 0) OBJEmail.cabecalho += " - Atividade - " + this.IDAtividade.ToString();
            //OBJEmail.titulo = "Anexo de arquivo";
            //OBJEmail.detalhe = "Inserido arquivo: " + this.NomeArquivo;
            OBJEmail.data = DateTime.Now.ToString("dd/MM/yyyy");
            OBJEmail.emailpara = this.EmailPara;
            OBJEmail.NomeUsuario = this.NomeUsuario;

            erro = OBJEmail.EnviaEmailTicketAnexo();

            return erro;
        }

        public string ExcluiArquivoServidor()
        {
            string diretorio = "";
            string arquivo = "";
            string erro = "";
            string Pasta = "";

            DataTable ObjDataTable = new DataTable();
            ObjParametroGeral.IDEmpresa = this.IDEmpresa;
            ObjParametroGeral.Filtro = "ANEXOSTICKETSSAC";
            ObjDataTable = ObjParametroGeral.RetornaListaParametrosGerais();

            if (ObjDataTable.Rows.Count > 0)
            {
                foreach (DataRow Row in ObjDataTable.Rows)
                {
                    diretorio = Row["ValorTexto"].ToString();
                }
            }

            //diretorio = "\\\\192.168.0.2\\crm\\AnexosTickets\\";

            Pasta = "Ticket_" + this.IDTicket.ToString();
            diretorio += Pasta + "\\";
            arquivo = diretorio + this.NomeArquivo;

            try
            {
                if (File.Exists(arquivo))
                {
                    File.Delete(arquivo);
                }
                else
                {
                    erro = "Arquivo não existe";
                }
            }
            catch (Exception ex)
            {
                erro = ex.ToString();
            }

            return erro;
        }

        #endregion

        #region Ticket Escolha Cliente

        public DataTable RetornaListaClientes()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_TICKETS_CLIENTES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@TipoFiltro", SqlDbType.VarChar, 8000, "TipoFiltro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));

                    dbCommand.Parameters["@TipoFiltro"].Value = this.TipoFiltro;
                    dbCommand.Parameters["@Filtro"].Value = this.Filtro;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        #endregion

        #region Situações Ticket's

        public DataTable RetornaListaSituacaoTickets()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_SITUACAO_TICKETS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Tela", SqlDbType.VarChar, 8000, "Tela"));

                    dbCommand.Parameters["@Tela"].Value = this.Tela;
                    dbCommand.Parameters["@Filtro"].Value = this.Filtro;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public string GravaSituacaoTickets()
        {
            UtilClass ObjUtilClass = new UtilClass();
            DataTable outputTable = new DataTable();
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_SITUACAO_TICKETS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDSituacao", SqlDbType.Int, 0, "IDSituacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Bloqueado", SqlDbType.Bit, 0, "Bloqueado"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Bit, 0, "@Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDSituacao"].Value = this.IDSituacao;
                    dbCommand.Parameters["@Bloqueado"].Value = this.Bloqueado;
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao;
                    dbCommand.Parameters["@Ativo"].Value = this.Ativo;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            this.IDSituacao = Convert.ToInt32(row["IDSituacao"].ToString());
                            this.Operacao = "Alteracao";
                        }
                    }

                    dataReader.Close();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        #endregion

        #region Situações Atividades

        public DataTable RetornaListaSituacaoAtividades()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_SITUACAO_ATIVIDADES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Tela", SqlDbType.VarChar, 8000, "Tela"));

                    dbCommand.Parameters["@Tela"].Value = this.Tela;
                    dbCommand.Parameters["@Filtro"].Value = this.Filtro;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public string GravaSituacaoAtividades()
        {
            UtilClass ObjUtilClass = new UtilClass();
            DataTable outputTable = new DataTable();
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_SITUACAO_ATIVIDADES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDSituacao", SqlDbType.Int, 0, "IDSituacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Bloqueado", SqlDbType.Bit, 0, "Bloqueado"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Bit, 0, "@Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDSituacao"].Value = this.IDSituacao;
                    dbCommand.Parameters["@Bloqueado"].Value = this.Bloqueado;
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao;
                    dbCommand.Parameters["@Ativo"].Value = this.Ativo;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            this.IDSituacao = Convert.ToInt32(row["IDSituacao"].ToString());
                            this.Operacao = "Alteracao";
                        }
                    }

                    dataReader.Close();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        #endregion

        #region Classificações

        public DataTable RetornaListaClassificacao()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_CLASSIFICACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Tela", SqlDbType.VarChar, 8000, "Tela"));

                    dbCommand.Parameters["@Tela"].Value = this.Tela;
                    dbCommand.Parameters["@Filtro"].Value = this.Filtro;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public string GravaClassificacao()
        {
            UtilClass ObjUtilClass = new UtilClass();
            DataTable outputTable = new DataTable();
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CLASSIFICACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDClassificacao", SqlDbType.Int, 0, "IDClassificacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Padrao", SqlDbType.Bit, 0, "Padrao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Bit, 0, "@Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDClassificacao"].Value = this.IDClassificacao;
                    dbCommand.Parameters["@Padrao"].Value = this.Padrao;
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao;
                    dbCommand.Parameters["@Ativo"].Value = this.Ativo;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            this.IDClassificacao = Convert.ToInt32(row["IDClassificacao"].ToString());
                            this.Operacao = "Alteracao";
                        }
                    }

                    dataReader.Close();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        public string AlteraClassificacaoPadrao()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ALTERA_CLASSIFICACAO_PADRAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDClassificacao", SqlDbType.Int, 0, "IDClassificacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Padrao", SqlDbType.Bit, 0, "Padrao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDClassificacao"].Value = this.IDClassificacao;
                    dbCommand.Parameters["@Padrao"].Value = this.Padrao;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        #endregion

        #region Prioridades

        public DataTable RetornaListaPrioridades()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_SAC_PRIORIDADES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Tela", SqlDbType.VarChar, 8000, "Tela"));

                    dbCommand.Parameters["@Tela"].Value = this.Tela;
                    dbCommand.Parameters["@Filtro"].Value = this.Filtro;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;
        }

        public string GravaPrioridades()
        {
            UtilClass ObjUtilClass = new UtilClass();
            DataTable outputTable = new DataTable();
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_SAC_PRIORIDADES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPrioridade", SqlDbType.Int, 0, "IDPrioridade"));
                    dbCommand.Parameters.Add(new SqlParameter("@Padrao", SqlDbType.Bit, 0, "Padrao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Bit, 0, "@Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDPrioridade"].Value = this.IDPrioridade;
                    dbCommand.Parameters["@Padrao"].Value = this.Padrao;
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao;
                    dbCommand.Parameters["@Ativo"].Value = this.Ativo;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            this.IDPrioridade = Convert.ToInt32(row["IDPrioridade"].ToString());
                            this.Operacao = "Alteracao";
                        }
                    }

                    dataReader.Close();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        public string AlteraPrioridadePadrao()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ALTERA_SAC_PRIORIDADE_PADRAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPrioridade", SqlDbType.Int, 0, "IDPrioridade"));
                    dbCommand.Parameters.Add(new SqlParameter("@Padrao", SqlDbType.Bit, 0, "Padrao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDPrioridade"].Value = this.IDPrioridade;
                    dbCommand.Parameters["@Padrao"].Value = this.Padrao;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        #endregion

        #region Soluções

        public DataTable RetornaListaSolucoes()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_SOLUCAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Solucao", SqlDbType.VarChar, 8000, "Solucao"));

                    dbCommand.Parameters["@Solucao"].Value = this.Solucao;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public string GravaSolucaoPadrao()
        {
            UtilClass ObjUtilClass = new UtilClass();
            DataTable outputTable = new DataTable();
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ALTERA_SAC_SOLUCAO_PADRAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDSolucao", SqlDbType.Int, 0, "IDSolucao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Padrao", SqlDbType.Bit, 0, "Padrao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));

                    dbCommand.Parameters["@IDSolucao"].Value = this.IDSolucao;
                    dbCommand.Parameters["@Padrao"].Value = this.Padrao;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    dataReader.Close();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        #endregion

        #region Soluções Detalhe

        public string GravaSolucao()
        {
            UtilClass ObjUtilClass = new UtilClass();
            DataTable outputTable = new DataTable();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_SOLUCAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDSolucao", SqlDbType.Int, 0, "IDSolucao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Bit, 0, "Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Padrao", SqlDbType.Bit, 0, "Padrao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));

                    dbCommand.Parameters["@IDSolucao"].Value = this.IDSolucao;
                    dbCommand.Parameters["@Ativo"].Value = this.Ativo;
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao;
                    dbCommand.Parameters["@Padrao"].Value = this.Padrao;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            this.IDSolucao = Convert.ToInt32(row["IDSolucao"].ToString());
                            this.Operacao = "Alteracao";
                        }
                    }

                    dataReader.Close();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        #endregion

        #region Tipo Ocorrência

        public DataTable RetornaListaTipoOcorrencia()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_SAC_TICKET_TIPO_OCORRENCIA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@TipoOcorrencia", SqlDbType.VarChar, 8000, "TipoOcorrencia"));

                    dbCommand.Parameters["@TipoOcorrencia"].Value = this.TipoOcorrencia;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public string GravaTipoOcorrenciaPadrao()
        {
            UtilClass ObjUtilClass = new UtilClass();
            DataTable outputTable = new DataTable();
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ALTERA_SAC_TIPO_OCORRENCIA_PADRAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoOcorrencia", SqlDbType.Int, 0, "IDTipoOcorrencia"));
                    dbCommand.Parameters.Add(new SqlParameter("@Padrao", SqlDbType.Bit, 0, "Padrao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));

                    dbCommand.Parameters["@IDTipoOcorrencia"].Value = this.IDTipoOcorrencia;
                    dbCommand.Parameters["@Padrao"].Value = this.Padrao;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    dataReader.Close();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        #endregion

        #region Tipo Ocorrência Detalhe

        public string GravaTipoOcorrencia()
        {
            UtilClass ObjUtilClass = new UtilClass();
            DataTable outputTable = new DataTable();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_SAC_TIPO_OCORRENCIA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTipoOcorrencia", SqlDbType.Int, 0, "IDTipoOcorrencia"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Bit, 0, "Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Padrao", SqlDbType.Bit, 0, "Padrao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));

                    dbCommand.Parameters["@IDTipoOcorrencia"].Value = this.IDTipoOcorrencia;
                    dbCommand.Parameters["@Ativo"].Value = this.Ativo;
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao;
                    dbCommand.Parameters["@Padrao"].Value = this.Padrao;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            this.IDTipoOcorrencia = Convert.ToInt32(row["IDTipoOcorrencia"].ToString());
                            this.Operacao = "Alteracao";
                        }
                    }

                    dataReader.Close();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        #endregion

        #region Motivos

        public DataTable RetornaListaMotivo()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_SAC_TICKET_MOTIVO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Motivo", SqlDbType.VarChar, 8000, "Motivo"));

                    dbCommand.Parameters["@Motivo"].Value = this.Motivo;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public string GravaMotivoPadrao()
        {
            UtilClass ObjUtilClass = new UtilClass();
            DataTable outputTable = new DataTable();
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ALTERA_SAC_MOTIVO_PADRAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDMotivo", SqlDbType.Int, 0, "IDMotivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Padrao", SqlDbType.Bit, 0, "Padrao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));

                    dbCommand.Parameters["@IDMotivo"].Value = this.IDMotivo;
                    dbCommand.Parameters["@Padrao"].Value = this.Padrao;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    dataReader.Close();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        #endregion

        #region Motivos Detalhe

        public string GravaMotivo()
        {
            UtilClass ObjUtilClass = new UtilClass();
            DataTable outputTable = new DataTable();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_MOTIVO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDMotivo", SqlDbType.Int, 0, "IDMotivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ativo", SqlDbType.Bit, 0, "Ativo"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Padrao", SqlDbType.Bit, 0, "Padrao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));

                    dbCommand.Parameters["@IDMotivo"].Value = this.IDMotivo;
                    dbCommand.Parameters["@Ativo"].Value = this.Ativo;
                    dbCommand.Parameters["@Descricao"].Value = this.Descricao;
                    dbCommand.Parameters["@Padrao"].Value = this.Padrao;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    outputTable.Load(dataReader);

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            this.IDMotivo = Convert.ToInt32(row["IDMotivo"].ToString());
                            this.Operacao = "Alteracao";
                        }
                    }

                    dataReader.Close();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        #endregion

        #region Nota Fiscal

        public DataTable RetornaListaNotaFiscal()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_SAC_TICKET_NOTAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDTicket", SqlDbType.Int, 0, "IDTicket"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroSerial", SqlDbType.Int, 0, "NumeroSerial"));

                    dbCommand.Parameters["@IDTicket"].Value = this.IDTicket;
                    dbCommand.Parameters["@NumeroSerial"].Value = this.NumeroSerial;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.ToString();
            }

            return outputTable;
        }

        public string GravaNotaFiscal()
        {
            UtilClass ObjUtilClass = new UtilClass();
            DataTable outputTable = new DataTable();
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_SAC_TICKET_NOTAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTicket", SqlDbType.Int, 0, "IDTicket"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroSerial", SqlDbType.Int, 0, "NumeroSerial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFaturamento", SqlDbType.VarChar, 0, "DataFaturamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDTicket"].Value = this.IDTicket;
                    dbCommand.Parameters["@NumeroSerial"].Value = this.NumeroSerial;
                    dbCommand.Parameters["@DataFaturamento"].Value = this.DataFaturamento;
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            if (row["Erro"].ToString() != "")
                                return row["Erro"].ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        public string ExcluiNotaFiscal()
        {
            UtilClass ObjUtilClass = new UtilClass();
            DataTable outputTable = new DataTable();
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_SAC_TICKET_NOTAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int, 0, "IDEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTicket", SqlDbType.Int, 0, "IDTicket"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroSerial", SqlDbType.Int, 0, "NumeroSerial"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDNota", SqlDbType.Int, 0, "IDNota"));                    
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDEmpresa"].Value = this.IDEmpresa;
                    dbCommand.Parameters["@IDTicket"].Value = this.IDTicket;
                    dbCommand.Parameters["@IDNota"].Value = this.IDNota;
                    dbCommand.Parameters["@NumeroSerial"].Value = this.NumeroSerial;                    
                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();

                    dataReader.Close();

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        public string RetornaDataFaturamentoNotaFiscal()
        {
            DataTable OBJDataTable = new DataTable();

            string StringSQL = "";

            StringSQL += "select DocDate ";
            StringSQL += "from OINV ";
            StringSQL += "where Serial = " + this.NumeroSerial + " ";
            this.CodigoCliente = this.CodigoCliente.Trim();
            StringSQL += "and CardCode = '" + this.CodigoCliente + "' ";

            OBJDataTable = OBJComunicacaoSAP.RetornaDadosConsultaSAP(StringSQL);

            if (OBJDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in OBJDataTable.Rows)
                {
                    this.DataFaturamento = Convert.ToDateTime(row["DocDate"]).ToString("yyyy-MM-dd");

                    return "";
                }
            }

            return " Numero da nota não encontrado no SAP para este cliente.";
        }

        #endregion
    }
}
