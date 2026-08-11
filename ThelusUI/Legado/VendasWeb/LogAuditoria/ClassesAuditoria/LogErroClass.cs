using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using VendasWeb.GerencialVendas;
using VendasWeb.WEBServiceSAP.ClassesWEBService;

namespace VendasWeb.LogAuditoria.ClassesAuditoria
{
    public class LogErroClass : clsConexao
    {
        public string OperacaoDEBUG { get; set; }
        public string OperacaoAcao { get; set; }
        public int IDusuario { get; set; }
        public static JsonConversao jsonconv = new JsonConversao();

        public void LogErro(Exception ex, string source)
        {
            try
            {
                string LogArquivo = HttpContext.Current.Request.MapPath("~/LogAuditoria/Arquivos/LogErro/Errolog.txt");
                if (!string.IsNullOrEmpty(LogArquivo))
                {
                    string Mensagem = string.Format("{0}{0}=== {1} ==={0}{2}{0}{3}{0}{4}{0}{5}", Environment.NewLine, DateTime.Now, ex.Message, source, ex.InnerException, ex.StackTrace);
                    byte[] binLogString = Encoding.Default.GetBytes(Mensagem);
                    FileStream arquivoLog = new FileStream(LogArquivo, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
                    arquivoLog.Seek(0, System.IO.SeekOrigin.End);
                    arquivoLog.Write(binLogString, 0, binLogString.Length);
                    arquivoLog.Close();

                    this.OperacaoDEBUG = Mensagem;
                    this.GravaLOGErro();
                }
            }
            catch(Exception execao)
            {
            }
        }

        public static void LogErroBanco()
        {

        }

        public string GravaDebug()
        {
            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_GRAVA_DEBUG", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@DescricaoOperacao", SqlDbType.VarChar, 8000, "DescricaoOperacao"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDusuario;
                    dbCommand.Parameters["@Operacao"].Value = this.OperacaoAcao;
                    dbCommand.Parameters["@DescricaoOperacao"].Value = this.OperacaoDEBUG;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

                Retorno = "Erro na Funcao Incluir Debug. Contactar o Suporte!";
            }

            return Retorno;
        }

        public string GravaLOGErro()
        {
            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_GRAVA_LOG_ERRO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@DescricaoOperacao", SqlDbType.VarChar, 8000, "DescricaoOperacao"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDusuario;
                    dbCommand.Parameters["@Operacao"].Value = this.OperacaoAcao;
                    dbCommand.Parameters["@DescricaoOperacao"].Value = this.OperacaoDEBUG;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

                Retorno = "Erro na Funcao Incluir Debug. Contactar o Suporte!";
            }

            return Retorno;
        }

        public static string GravaLOGErroStatic(int IDUsuario, string OperacaoAcao, Exception Execao, string AnaliseDados)
        {
            string Retorno = "";
            string OperacaoDEBUG = string.Format("{0}{0}=== {1} ==={0}{2}{0}{3}{0}{4}{0}{5}", Environment.NewLine, DateTime.Now, Execao.Message, OperacaoAcao, Execao.InnerException, Execao.StackTrace);

            //Caso usuario não tenha sido passado tenta recuperar da Session
            if (IDUsuario == 0)
            {
                if (HttpContext.Current.Session["IDUsuario"] != null)
                {
                    IDUsuario = Convert.ToInt32(HttpContext.Current.Session["IDUsuario"]);
                }
            }

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_GRAVA_LOG_ERRO_DADOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@DescricaoOperacao", SqlDbType.VarChar, 8000, "DescricaoOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@AnaliseDados", SqlDbType.VarChar, 8000, "AnaliseDados"));

                    dbCommand.Parameters["@IDUsuario"].Value = IDUsuario;
                    dbCommand.Parameters["@Operacao"].Value = OperacaoAcao;
                    dbCommand.Parameters["@DescricaoOperacao"].Value = OperacaoDEBUG;
                    dbCommand.Parameters["@AnaliseDados"].Value = AnaliseDados;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

                Retorno = "Erro na Funcao Incluir Debug. Contactar o Suporte!";
            }

            return Retorno;
        }

        public static string GravaDebugStatic(int IDusuario, string OperacaoAcao, string OperacaoDEBUG)
        {
            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_GRAVA_DEBUG", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@DescricaoOperacao", SqlDbType.VarChar, 8000, "DescricaoOperacao"));

                    dbCommand.Parameters["@IDUsuario"].Value = IDusuario;
                    dbCommand.Parameters["@Operacao"].Value = OperacaoAcao;
                    dbCommand.Parameters["@DescricaoOperacao"].Value = OperacaoDEBUG;

                    //Aumentando o timeout do command
                    dbCommand.CommandTimeout = 999999;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

                Retorno = "Erro na Funcao Incluir Debug. Contactar o Suporte!";
            }

            return Retorno;
        }
    }
}