using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using VendasWeb.Email;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class ChamadoClass : clsConexao
    {
        #region Campos

        public int NumeroChamado { get; set; }

        public int IDUsuarioOperacao { get; set; }
        public int IDUsuarioSolicitante { get; set; }
        public int IDUsuarioResponsavel { get; set; }
        public int IDUsuarioKeyUser { get; set; }
        public int IDUsuarioResponsavelPadrao { get; set; }
        public int IDApontamento { get; set; }

        public DateTime DataChamado { get; set; }
        public string DataAprovacao { get; set; }
        public string DataApontamento { get; set; }
        public int NumeroHoras { get; set; }
        public int IDSistema { get; set; }
        public int IDStatus { get; set; }
        public int IDClassificacao { get; set; }
        public int IDPrioridade { get; set; }
        public string Assunto { get; set; }
        public string descricao { get; set; }

        public string Chamado { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public string Ordenacao { get; set; }
        public int IDSetor { get; set; }

        /*Campos Projeto*/
        public string projeto { get; set; }
        public int IDHorasProjeto { get; set; }
        public int IDPrioridadeProjeto { get; set; }
        public int HorasPrevistas { get; set; }
        public int HorasRealizadas { get; set; }
        public DateTime PrevisaoEntrega { get; set; }
        public string DescricaoProjeto { get; set; }
        public DateTime DataRecalculoPrevisao { get; set; }

        /***Campos Historico****/
        public int Evento { get; set; }
        public int Categoria { get; set; }
        public string Historico { get; set; }

        /***Campos email***/
        EmailTemplateClass OBJEmail = new EmailTemplateClass();
        public string DescricaoEmail { get; set; }
        public string NomeEmail { get; set; }
        public string SetorEmail { get; set; }
        public string EmailPara { get; set; }
        public string EmailSolicitante { get; set; }
        public List<string> EmailResponsavel { get; set; }
        public string EmailOperacao { get; set; }
        public string NomeUsuario { get; set; }
        public string EmailKeyUser { get; set; }

        /****Campos Anexos****/
        public string ArquivoAnexo { get; set; }
        public string ArquivoExtensao { get; set; }
        public string NomeArquivo { get; set; }
        public string DescricaoArquivo { get; set; }
        public int IDAnexo { get; set; }
        public string CaminhoPadraoAnexos { get; set; }

        public string CodigoUsuario { get; set; }

        #endregion

        #region Métodos

        #region Carrega Combos Tela

        public DataTable CarregaUsuarios()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CHAMADO_USUARIOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception)
            {

            }

            return outputTable;

        }

        public DataTable CarregaUsuariosSuporte()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CHAMADO_USUARIOS_SUPORTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception)
            {

            }

            return outputTable;

        }

        public DataTable CarregaSistemas()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CHAMADO_SISTEMAS", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception)
            {

            }

            return outputTable;

        }

        public DataTable CarregaStatus()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CHAMADO_STATUS", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception)
            {

            }

            return outputTable;

        }

        public DataTable CarregaClassificacoes()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CHAMADO_CLASSIFICACAO", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception)
            {

            }

            return outputTable;

        }

        public DataTable CarregaPrioridades()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CHAMADO_PRIORIDADES", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception)
            {

            }

            return outputTable;

        }

        #endregion        

        #region Principais

        public DataTable RecuperaDadosPrincipais()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CHAMADO_PRINCIPAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));

                    dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.IDUsuarioSolicitante = Convert.ToInt32(row["IDUsuario"]);
                                this.IDUsuarioResponsavel = Convert.ToInt32(row["IDUsuarioResponsavel"]);
                                this.IDClassificacao = Convert.ToInt32(row["IDClassificacao"]);
                                this.IDStatus = Convert.ToInt32(row["IDStatus"]);
                                this.IDPrioridade = Convert.ToInt32(row["IDPrioridade"]);
                                this.IDSistema = Convert.ToInt32(row["IDSistema"]);
                                this.IDUsuarioKeyUser = Convert.ToInt32(row["IDUsuarioKeyUser"]);
                                this.DataChamado = Convert.ToDateTime(row["DataAbertura"]);
                                this.Assunto = Convert.ToString(row["Assunto"]);
                                this.descricao = Convert.ToString(row["Descricao"]);
                                this.IDSetor = Convert.ToInt32(row["IDSetor"]);

                                /****recupera dados do projeto****/
                                this.projeto = Convert.ToString(row["Projeto"]);
                                this.IDHorasProjeto = Convert.ToInt32(row["HorasTrabalhoProjeto"]);
                                this.IDPrioridadeProjeto = Convert.ToInt32(row["PrioridadeProjeto"]);
                                this.DescricaoProjeto = Convert.ToString(row["DescricaoProjeto"]);
                                this.HorasPrevistas = Convert.ToInt32(row["HorasPrevistas"]);
                                this.HorasRealizadas = Convert.ToInt32(row["HorasRealizadas"]);
                                this.PrevisaoEntrega = Convert.ToDateTime(row["PrevisaoEntrega"]);

                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return outputTable;
        }

        public string GravaDadosPrincipaisChamado(string descricaoAlteracaoUsuario = "", string descricaoAlteracaoInformacoes = "")
        {
            string erro = "";

            bool alteracao = true;

            if (this.NumeroChamado == 0) alteracao = false;

            try
            {
                //Nao pode ser feito chamado para datas futuras
                if (this.DataChamado.Date > DateTime.Now.Date)
                {
                    erro = "Data do chamado não pode ser futura.";

                    return erro;
                }

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    dbConnection.Open();
                    {
                        SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CHAMADO_PRINCIPAL", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioResponsavel", SqlDbType.Int, 0, "IDUsuarioResponsavel"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDClassificacao", SqlDbType.Int, 0, "IDClassificacao"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDPrioridade", SqlDbType.Int, 0, "IDPrioridade"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDSistema", SqlDbType.Int, 0, "IDSistema"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioKeyUser", SqlDbType.Int, 0, "IDUsuarioKeyUser"));
                        dbCommand.Parameters.Add(new SqlParameter("@DataAbertura", SqlDbType.DateTime, 0, "DataAbertura"));
                        dbCommand.Parameters.Add(new SqlParameter("@Assunto", SqlDbType.VarChar, 8000, "Assunto"));
                        dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, ParameterDirection.InputOutput, false, 0, 0, "IDChamado", DataRowVersion.Default, null));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuarioOperacao;
                        dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuarioSolicitante;
                        dbCommand.Parameters["@IDUsuarioResponsavel"].Value = this.IDUsuarioResponsavel;
                        dbCommand.Parameters["@IDClassificacao"].Value = this.IDClassificacao;
                        dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;
                        dbCommand.Parameters["@IDPrioridade"].Value = this.IDPrioridade;
                        dbCommand.Parameters["@IDSistema"].Value = this.IDSistema;
                        dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;
                        dbCommand.Parameters["@IDUsuarioKeyUser"].Value = this.IDUsuarioKeyUser;
                        dbCommand.Parameters["@DataAbertura"].Value = this.DataChamado;
                        dbCommand.Parameters["@Assunto"].Value = this.Assunto;
                        dbCommand.Parameters["@Descricao"].Value = this.descricao;
                        dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                        this.NumeroChamado = Convert.ToInt32(dbCommand.Parameters["@IDChamado"].Value);
                    }

                    {
                        if (alteracao)
                        {
                            if (descricaoAlteracaoInformacoes != "")
                                this.descricao = descricaoAlteracaoInformacoes;
                        }

                        if (erro == "") erro = GravaHistoricoAposGravacao(this.NumeroChamado);

                        if (alteracao)
                        {
                            this.Assunto = "Alteração do chamado";

                            if (descricaoAlteracaoUsuario != "" && descricaoAlteracaoInformacoes != "")
                                this.descricao = descricaoAlteracaoUsuario + descricaoAlteracaoInformacoes;
                        }

                        if (!this.descricao.Contains("<br>"))
                        {
                            this.descricao = this.descricao.Replace("\n\n", "<br>");

                            this.descricao = this.descricao.Replace("\n", "<br>");
                        }

                        if (erro == "") erro = EnviaEmailAposGravacao();

                        if (!alteracao)
                        {
                            this.Assunto = "Aprovação de chamado";

                            this.descricao = "Foi aberto um novo chamado que precisa da sua aprovação";

                            this.CodigoUsuario = "CRM API";
                        }

                        if (erro == "") erro = EnviaEmailKeyUser();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return erro;
        }

        public string GravaChamadoFinalizado()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    {
                        SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CHAMADO_Finalizado", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;
                    }

                    string Historico = "Chamado finalizado após o prazo de homologação do usuário.";

                    {
                        ParametroGeral objParametroGeral = new ParametroGeral();

                        this.Evento = objParametroGeral.RetornaValorNumericoParametro("EVENTOCHAMADOS");
                        this.Categoria = objParametroGeral.RetornaValorNumericoParametro("CATEGORIAFECHAMENTOCHAMADOS");
                        this.IDUsuarioOperacao = objParametroGeral.RetornaValorNumericoParametro("IDUSUARIOINTEGRACAO");

                        this.Historico = Historico;

                        if (erro == "") erro = GravaHistorico();
                    }

                    {
                        this.Assunto = "Finalização automática do chamado";

                        this.descricao = Historico;

                        if (erro == "") erro = EnviaEmailAposGravacao();
                    }

                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        public string AprovacaoChamado(string Operacao)
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    {
                        SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CHAMADO_APROVACAO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioResponsavel", SqlDbType.Int, 0, "IDUsuarioResponsavel"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioKeyUser", SqlDbType.Int, 0, "IDUsuarioKeyUser"));
                        dbCommand.Parameters.Add(new SqlParameter("@DataAprovacao", SqlDbType.VarChar, 8000, "DataAprovacao"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;
                        dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuarioOperacao;
                        dbCommand.Parameters["@IDUsuarioResponsavel"].Value = this.IDUsuarioResponsavel;
                        dbCommand.Parameters["@IDUsuarioKeyUser"].Value = this.IDUsuarioKeyUser;
                        dbCommand.Parameters["@DataAprovacao"].Value = this.DataAprovacao;

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;
                    }

                    {
                        if (erro == "") erro = GravaHistoricoAposGravacao(this.NumeroChamado, Operacao);

                        this.DescricaoEmail = "Chamado " + Operacao.ToLower() + " - " + this.DescricaoEmail;

                        if (erro == "") erro = EnviaEmailAposGravacao();
                    }
                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        public DataTable CarregaListaChamados()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CHAMADOS_ADMINISTRACAO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Chamado", SqlDbType.VarChar, 8000, "Chamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.DateTime, 0, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.DateTime, 0, "DataFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@Solicitante", SqlDbType.Int, 0, "Solicitante"));
                    dbCommand.Parameters.Add(new SqlParameter("@Responsavel", SqlDbType.Int, 0, "Responsavel"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatusChamado", SqlDbType.Int, 0, "StatusChamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));

                    dbCommand.Parameters["@Chamado"].Value = this.Chamado ?? "";
                    dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                    dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;
                    dbCommand.Parameters["@Solicitante"].Value = this.IDUsuarioSolicitante;
                    dbCommand.Parameters["@Responsavel"].Value = this.IDUsuarioResponsavel;
                    dbCommand.Parameters["@StatusChamado"].Value = this.IDStatus;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuarioOperacao;
                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())

                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception)
            {

            }

            return outputTable;
        }

        public void RecuperaUsuarioPadraoPrincipais()
        {
            DataTable outputTable = new DataTable();
            string CodigoUsuarioPadrao = "";
            usuario OBJUsuarioAux = new usuario();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_PARAMETROS_GERAIS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Parametro", SqlDbType.VarChar, 8000, "Parametro"));

                    dbCommand.Parameters["@Parametro"].Value = "URESPONSAVELPADRAOCHAMADOS";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())

                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                CodigoUsuarioPadrao = Convert.ToString(row["ValorString"]);
                                OBJUsuarioAux.CodigoUsuario = CodigoUsuarioPadrao;
                                OBJUsuarioAux.CarregaDadosPrincipais();

                                this.IDUsuarioResponsavelPadrao = OBJUsuarioAux.IDUsuario;

                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public string RecuperaUsuarioChave()
        {
            string erro = "";
            DataTable outputTable = new DataTable();
            usuario OBJUsuarioAux = new usuario();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CHAMADOS_USUARIO_CHAVE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSistema", SqlDbType.Int, 0, "IDSistema"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuarioSolicitante;
                    dbCommand.Parameters["@IDSistema"].Value = this.IDSistema;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())

                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.IDUsuarioKeyUser = Convert.ToInt32(row["UsuarioChave"]);
                            }
                        }
                        else
                        {
                            erro = "Solicitante não possui KeyUser.";
                        }
                    }
                }
            }
            catch (Exception)
            {
                erro = "Erro ao retornar usuário KeyUser.";
            }

            return erro;
        }

        public DataTable CarregaListaPreferenciasUsuario()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CHAMADOS_LISTA_PREFERENCIAS_USUARIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuarioOperacao;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception)
            {

            }

            return outputTable;
        }

        public string GravaListaPreferenciasUsuario()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CHAMADOS_LISTA_PREFERENCIAS_USUARIO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioSolicitante", SqlDbType.Int, 0, "IDUsuarioSolicitante"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioResponsavel", SqlDbType.Int, 0, "IDUsuarioResponsavel"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDStatus", SqlDbType.Int, 0, "IDStatus"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuarioOperacao;
                    dbCommand.Parameters["@IDUsuarioSolicitante"].Value = this.IDUsuarioSolicitante;
                    dbCommand.Parameters["@IDUsuarioResponsavel"].Value = this.IDUsuarioResponsavel;
                    dbCommand.Parameters["@IDStatus"].Value = this.IDStatus;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        public DataTable CarregaListaChamadosHomologados()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CHAMADOS_Homologando", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception)
            {

            }

            return outputTable;
        }

        public DataTable CarregaListaChamadosKeyUsers()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CHAMADOS_KeyUsers", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception)
            {

            }

            return outputTable;
        }

        public int RetornaChamadosIDStatusHomologando()
        {
            DataTable status = this.CarregaStatus();

            foreach (DataRow row in status.Rows)
            {
                if (row["Descricao"].ToString().Trim() == "Homologando")
                {
                    return Convert.ToInt32(row["IDStatus"]);
                }
            }

            return 0;
        }

        #endregion

        #region Historico

        public string GravaHistoricoAposGravacao(int ContAbertura, string Operacao = "")
        {
            ParametroGeral objParametroGeral = new ParametroGeral();

            //Chama rotina tratar dados na inserção do chamado
            if ((ContAbertura <= 0 && Operacao == "") || (this.Evento == 0 && this.Categoria == 0))
            {
                //Atribui o campo historico não na abertura do chamado
                this.projeto = "nao";

                this.Evento = objParametroGeral.RetornaValorNumericoParametro("EVENTOCHAMADOS");

                this.Categoria = objParametroGeral.RetornaValorNumericoParametro("CATEGORIAABERTURACHAMADOS");
            }

            this.Historico = descricao;

            return GravaHistorico();
        }

        public string GravaHistorico()
        {
            HistoricosClass OBJHistorico = new HistoricosClass();

            ParametroGeral objParametroGeral = new ParametroGeral();

            //Seta Evento e categoria
            OBJHistorico.IDTipoHistorico = objParametroGeral.RetornaValorNumericoParametro("TIPOPADRAOCHAMADOS");
            OBJHistorico.IDEvento = this.Evento;
            OBJHistorico.IDCategoria = this.Categoria;
            OBJHistorico.Historico = this.Historico;
            OBJHistorico.IDUsuario = this.IDUsuarioOperacao;
            OBJHistorico.IDChamado = this.NumeroChamado;

            return OBJHistorico.GravaHistoricoChamado();
        }

        #endregion

        #region Responsáveis

        public DataTable CarregaListaResponsaveisChamado()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CHAMADO_RESPONSAVEIS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));

                    dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception)
            {

            }

            return outputTable;
        }

        public string AdicionaResponsavel()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CHAMADO_RESPONSAVEL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioResponsavel", SqlDbType.Int, 0, "IDUsuarioResponsavel"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuarioOperacao;
                    dbCommand.Parameters["@IDUsuarioResponsavel"].Value = this.IDUsuarioResponsavel;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        public string ExcluiResponsavel()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_DELETA_CHAMADO_RESPONSAVEL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioResponsavel", SqlDbType.Int, 0, "IDUsuarioResponsavel"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuarioOperacao;
                    dbCommand.Parameters["@IDUsuarioResponsavel"].Value = this.IDUsuarioResponsavel;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        public string GravaResponsavelPrincipal()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CHAMADO_RESPONSAVEL_PRINCIPAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioResponsavel", SqlDbType.Int, 0, "IDUsuarioResponsavel"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuarioOperacao;
                    dbCommand.Parameters["@IDUsuarioResponsavel"].Value = this.IDUsuarioResponsavel;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        #endregion

        #region Email

        public string EnviaEmailAposGravacao()
        {
            string erro = "";

            try
            {
                //Envia email
                CarregaEmailsChamado();

                List<string> listEmails = new List<string>();

                this.DescricaoEmail = this.descricao;

                listEmails.Add(this.EmailSolicitante);

                //Pega os emails dos responsaveis
                {
                    foreach (string email in EmailResponsavel)
                    {
                        if (!listEmails.Contains(email))
                            listEmails.Add(email);
                    }
                }

                //Pega o email do usuário logado
                if (!listEmails.Contains(this.EmailOperacao))
                    listEmails.Add(this.EmailOperacao);

                foreach (string email in listEmails)
                {
                    if (email.Trim() != "")
                    {
                        this.EmailPara = email;

                        erro = this.EnviaEmail();

                        if (erro != "") return erro;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }

        public string EnviaEmailKeyUser(string email = "")
        {
            try
            {
                CarregaEmailsChamado();

                if (this.EmailOperacao != this.EmailKeyUser)
                {
                    if (email == "")
                        this.EmailPara = this.EmailKeyUser;
                    else
                        this.EmailPara = email;

                    this.DescricaoEmail = this.descricao;

                    string erro = this.EnviaEmail();

                    if (erro != "") return erro;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }

        public string EnviaEmail()
        {
            if (this.NumeroChamado != 0)
                OBJEmail.cabecalho = "Chamado - " + this.NumeroChamado.ToString();
            else
                OBJEmail.cabecalho = "Chamados";

            OBJEmail.titulo = this.Assunto;
            OBJEmail.detalhe = this.DescricaoEmail;
            OBJEmail.data = DateTime.Now.ToString("dd/MM/yyyy");
            OBJEmail.emailpara = this.EmailPara;
            OBJEmail.NomeUsuario = this.CodigoUsuario;

            return OBJEmail.EnviaEmailChamado();
        }

        public void CarregaEmailsChamado()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CHAMADO_DADOS_EMAIL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));

                    dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuarioOperacao;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        EmailResponsavel = new List<string>();

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.EmailSolicitante = Convert.ToString(row["EmailSolicitante"]);

                                if (!EmailResponsavel.Contains(Convert.ToString(row["EmailResponsavel"])))
                                    EmailResponsavel.Add(Convert.ToString(row["EmailResponsavel"]));

                                this.EmailOperacao = Convert.ToString(row["EmailOperacao"]);
                                this.NomeUsuario = Convert.ToString(row["NomeUsuarioOperacao"]);
                                this.SetorEmail = Convert.ToString(row["SetorOperacao"]);

                                this.EmailKeyUser = Convert.ToString(row["EmailKeyUser"]);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        #endregion

        #region Anexos

        public DataTable RecuperaDadosAnexos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CHAMADO_ANEXOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));

                    dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())

                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception)
            {

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

            ParametroGeral objParametroGeral = new ParametroGeral();

            diretorio = objParametroGeral.RetornaValorStringParametro("CAMINHOPADRAOANEXOSCHAMADOS");

            Pasta = "Chamado_" + this.NumeroChamado.ToString();

            diretorio += "\\" + Pasta + "\\";

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
            catch (Exception)
            {
                erro = "Erro ao salvar arquivo.";
            }


            return erro;
        }

        public string GravaArquivoServidor(byte[] fileBytes, string filename, string contentType)
        {
            string diretorio = "";
            string arquivo = "";
            string erro = "";
            string Pasta = "";
            string Extensao = "";
            int cont = 0;

            ParametroGeral objParametroGeral = new ParametroGeral();

            diretorio = objParametroGeral.RetornaValorStringParametro("CAMINHOPADRAOANEXOSCHAMADOS");

            Pasta = "Chamado_" + this.NumeroChamado.ToString();

            diretorio += "\\" + Pasta + "\\";

            arquivo = diretorio + filename;

            try
            {
                if (!Directory.Exists(diretorio))
                {
                    Directory.CreateDirectory(diretorio);
                }

                if (!File.Exists(arquivo))
                {
                    File.WriteAllBytes(arquivo, fileBytes);
                }
                else
                {
                    cont++;

                    while (cont != 0)
                    {
                        cont++;

                        Extensao = Path.GetExtension(arquivo);

                        arquivo = Path.GetFileNameWithoutExtension(arquivo);

                        filename = arquivo + "_" + cont.ToString() + Extensao;

                        arquivo = diretorio + filename;

                        if (!File.Exists(arquivo))
                        {
                            File.WriteAllBytes(arquivo, fileBytes);

                            cont = 0;
                        }
                    }
                }

                //Atribui dados do arquivo 
                this.ArquivoAnexo = arquivo;

                this.ArquivoExtensao = Path.GetExtension(arquivo);

                this.NomeArquivo = filename;
            }
            catch (Exception)
            {
                erro = "Erro ao salvar arquivo.";
            }

            return erro;
        }

        public string GravaDadosAnexosChamado()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CHAMADO_ANEXOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@Caminhodestino", SqlDbType.VarChar, 8000, "Caminhodestino"));
                    dbCommand.Parameters.Add(new SqlParameter("@NomeArquivo", SqlDbType.VarChar, 8000, "NomeArquivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@ExtensaoArquivo", SqlDbType.VarChar, 8000, "ExtensaoArquivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@DescricaoArquivo", SqlDbType.VarChar, 8000, "DescricaoArquivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;
                    dbCommand.Parameters["@Caminhodestino"].Value = this.ArquivoAnexo;
                    dbCommand.Parameters["@NomeArquivo"].Value = this.NomeArquivo;
                    dbCommand.Parameters["@ExtensaoArquivo"].Value = this.ArquivoExtensao;
                    dbCommand.Parameters["@DescricaoArquivo"].Value = this.DescricaoArquivo;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuarioOperacao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                    {
                        ParametroGeral objParametroGeral = new ParametroGeral();

                        this.Evento = objParametroGeral.RetornaValorNumericoParametro("EVENTOCHAMADOS");
                        this.Categoria = objParametroGeral.RetornaValorNumericoParametro("CATEGORIAADICIONARANEXOCHAMADOS");

                        string Historico = "";

                        {
                            Historico += "Anexado arquivo: " + this.NomeArquivo;
                            Historico += "<br>";
                            Historico += "Tipo arquivo: " + this.ArquivoExtensao;
                            Historico += "<br>";
                            Historico += "Descrição: " + this.DescricaoArquivo;
                        }

                        this.Historico = Historico;

                        if (erro == "") erro = GravaHistorico();

                        {
                            this.Assunto = "Anexo de arquivo";

                            this.descricao = Historico;
                        }

                        if (erro == "") erro = EnviaEmailAposGravacao();
                    }

                }
                catch (Exception)
                {
                    erro = "Erro na inserção do anexo.";
                }
            }

            return erro;
        }

        public string ExcluiDadosAnexosChamadoServidor()
        {
            ParametroGeral objParametroGeral = new ParametroGeral();

            string diretorio = objParametroGeral.RetornaValorStringParametro("CAMINHOPADRAOANEXOSCHAMADOS");

            string Pasta = "Chamado_" + this.NumeroChamado.ToString();

            diretorio += Pasta + "\\";

            string arquivo = diretorio + this.NomeArquivo;

            try
            {
                if (File.Exists(arquivo)) File.Delete(arquivo);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }

        public string ExcluiDadosAnexosChamado()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_CHAMADO_ANEXOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDAnexo", SqlDbType.Int, 0, "IDAnexo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;
                    dbCommand.Parameters["@IDAnexo"].Value = this.IDAnexo;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuarioOperacao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                    {
                        ParametroGeral objParametroGeral = new ParametroGeral();

                        this.Evento = objParametroGeral.RetornaValorNumericoParametro("EVENTOCHAMADOS");
                        this.Categoria = objParametroGeral.RetornaValorNumericoParametro("CATEGORIAEXCLUIRANEXOCHAMADOS");
                        this.Historico = this.DescricaoArquivo;

                        if (erro == "") erro = GravaHistorico();

                        this.Assunto = "Excluído arquivo - " + this.DescricaoArquivo;

                        if (erro == "") erro = EnviaEmailAposGravacao();
                    }

                }
                catch (Exception)
                {
                    erro = "Erro na inserção de dados do projeto do chamado.";
                }
            }

            return erro;
        }

        #endregion

        #region Projeto

        public DataTable CarregaListaChamadosProjetos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CHAMADOS_PROJETOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Chamado", SqlDbType.VarChar, 8000, "Chamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.DateTime, 0, "DataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.DateTime, 0, "DataFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@Solicitante", SqlDbType.Int, 0, "Solicitante"));
                    dbCommand.Parameters.Add(new SqlParameter("@Responsavel", SqlDbType.Int, 0, "Responsavel"));
                    dbCommand.Parameters.Add(new SqlParameter("@StatusChamado", SqlDbType.Int, 0, "StatusChamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPrioridade", SqlDbType.Int, 0, "IDPrioridade"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ordenacao", SqlDbType.VarChar, 8000, "Ordenacao"));

                    dbCommand.Parameters["@Chamado"].Value = this.Chamado ?? "";
                    dbCommand.Parameters["@DataInicial"].Value = this.DataInicial;
                    dbCommand.Parameters["@DataFinal"].Value = this.DataFinal;
                    dbCommand.Parameters["@Solicitante"].Value = this.IDUsuarioSolicitante;
                    dbCommand.Parameters["@Responsavel"].Value = this.IDUsuarioResponsavel;
                    dbCommand.Parameters["@StatusChamado"].Value = this.IDStatus;
                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;
                    dbCommand.Parameters["@IDPrioridade"].Value = this.IDPrioridadeProjeto;
                    dbCommand.Parameters["@Ordenacao"].Value = this.Ordenacao;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())

                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception)
            {

            }

            return outputTable;
        }

        public DataTable CarregaPrioridadesProjeto()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_CHAMADO_PRIORIDADES_PROJETOS", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception)
            {

            }

            return outputTable;

        }

        public DataTable CarregaHorasDesenvolvimentoProjeto()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_PROJETOS_HORAS_DESENVOLVIMENTO", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception)
            {

            }

            return outputTable;

        }

        public string GravaDadosProjeto()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CHAMADO_PROJETO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@DescricaoProjeto", SqlDbType.VarChar, 8000, "DescricaoProjeto"));
                    dbCommand.Parameters.Add(new SqlParameter("@HorasPrevistas", SqlDbType.Int, 0, "HorasPrevistas"));
                    dbCommand.Parameters.Add(new SqlParameter("@HorasRealizadas", SqlDbType.Int, 0, "HorasRealizadas"));
                    dbCommand.Parameters.Add(new SqlParameter("@PrevisaoEntrega", SqlDbType.DateTime, 0, "PrevisaoEntrega"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDPrioridadeProjeto", SqlDbType.Int, 0, "IDPrioridadeProjeto"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDHoras", SqlDbType.Int, 0, "IDHoras"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;
                    dbCommand.Parameters["@DescricaoProjeto"].Value = this.DescricaoProjeto;
                    dbCommand.Parameters["@HorasPrevistas"].Value = this.HorasPrevistas;
                    dbCommand.Parameters["@HorasRealizadas"].Value = this.HorasRealizadas;
                    dbCommand.Parameters["@PrevisaoEntrega"].Value = this.PrevisaoEntrega;
                    dbCommand.Parameters["@IDPrioridadeProjeto"].Value = this.IDPrioridadeProjeto;
                    dbCommand.Parameters["@IDHoras"].Value = this.IDHorasProjeto;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuarioOperacao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                    this.NumeroChamado = Convert.ToInt32(dbCommand.Parameters["@IDChamado"].Value);

                    //Chama rotina para gravar histórico
                    if (this.projeto == "nao")
                    {
                        ParametroGeral objParametroGeral = new ParametroGeral();

                        this.Evento = objParametroGeral.RetornaValorNumericoParametro("EVENTOPADRAOPROJETO");
                        this.Categoria = objParametroGeral.RetornaValorNumericoParametro("CATEGORIAEVENTOENVIARPROJETO");
                        this.Historico = this.DescricaoProjeto;

                        GravaHistorico();
                    }

                }
                catch (Exception)
                {
                    erro = "Erro na inserção de dados do projeto do chamado.";
                }
            }

            return erro;
        }

        public string GravaRecalculoProjeto()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_PREVISAO_PROJETO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataCalculoPrevisao", SqlDbType.DateTime, 0, "DataCalculoPrevisao"));
                    dbCommand.Parameters.Add(new SqlParameter("@VErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuarioOperacao;
                    dbCommand.Parameters["@DataCalculoPrevisao"].Value = this.DataRecalculoPrevisao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                    //Chama rotina para atualizar projetos
                    if (erro == "")
                    {
                        AtualizaProjetosPrevisao();
                    }

                }
                catch (Exception)
                {
                    erro = "Erro na recalculo do projeto.";
                }
            }

            return erro;
        }

        public string AtualizaProjetosPrevisao()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_ATUALIZA_PREVISAO_PROJETOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception)
                {
                    erro = "Erro na atualização de previsão dos projetos.";
                }
            }

            return erro;
        }

        #endregion

        #region Apontamento Horas

        public DataTable RecuperaDadosApontamentoHoras()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CHAMADO_APONTAMENTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));

                    dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())

                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception)
            {

            }

            return outputTable;
        }

        public string AdicionaApontamentoHoras()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_CHAMADO_APONTAMENTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioResponsavel", SqlDbType.Int, 0, "IDUsuarioResponsavel"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataApontamento", SqlDbType.VarChar, 8000, "DataApontamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@NumeroHoras", SqlDbType.Int, 0, "NumeroHoras"));
                    dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 8000, "Descricao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuarioOperacao;
                    dbCommand.Parameters["@IDUsuarioResponsavel"].Value = this.IDUsuarioResponsavel;
                    dbCommand.Parameters["@DataApontamento"].Value = this.DataApontamento;
                    dbCommand.Parameters["@NumeroHoras"].Value = this.NumeroHoras;
                    dbCommand.Parameters["@Descricao"].Value = this.descricao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        public string ExcluiApontamentoHoras()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_DELETA_CHAMADO_APONTAMENTOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioOperacao", SqlDbType.Int, 0, "IDUsuarioOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioResponsavel", SqlDbType.Int, 0, "IDUsuarioResponsavel"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDApontamento", SqlDbType.Int, 0, "IDApontamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;
                    dbCommand.Parameters["@IDUsuarioOperacao"].Value = this.IDUsuarioOperacao;
                    dbCommand.Parameters["@IDUsuarioResponsavel"].Value = this.IDUsuarioResponsavel;
                    dbCommand.Parameters["@IDApontamento"].Value = this.IDApontamento;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
            }

            return erro;
        }

        public DataTable RecuperaDadosApontamentoHorasDetalhe()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_CHAMADO_APONTAMENTOS_DETALHE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDChamado", SqlDbType.Int, 0, "IDChamado"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuarioResponsavel", SqlDbType.Int, 0, "IDUsuarioResponsavel"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDApontamento", SqlDbType.Int, 0, "IDApontamento"));

                    dbCommand.Parameters["@IDChamado"].Value = this.NumeroChamado;
                    dbCommand.Parameters["@IDUsuarioResponsavel"].Value = this.IDUsuarioResponsavel;
                    dbCommand.Parameters["@IDApontamento"].Value = this.IDApontamento;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())

                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception)
            {

            }

            return outputTable;
        }

        #endregion

        #endregion
    }
}