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
    public class InfraestruturaClass : clsConexao
    {
        #region Campos

        public int IDPerformance { get; set; }

        public string MAC { get; set; }

        public string IP { get; set; }

        public string Nome { get; set; }

        #endregion

        #region Métodos

        public DataTable CarregaListaMaquinas()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_INFRAESTRUTURA_PERFORMANCE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@MAC", SqlDbType.VarChar, 8000, "MAC"));
                    dbCommand.Parameters.Add(new SqlParameter("@IP", SqlDbType.VarChar, 8000, "IP"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 8000, "Nome"));

                    dbCommand.Parameters["@MAC"].Value = this.MAC;
                    dbCommand.Parameters["@IP"].Value = this.IP;
                    dbCommand.Parameters["@Nome"].Value = this.Nome;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public DataTable CarregaInfoMaquina()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_INFRAESTRUTURA_PERFORMANCE_INFORMACOES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPerformance", SqlDbType.Int, 0, "IDPerformance"));                    

                    dbCommand.Parameters["@IDPerformance"].Value = this.IDPerformance;                 

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public DataTable CarregaRAMMaquina()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_INFRAESTRUTURA_PERFORMANCE_RAM", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPerformance", SqlDbType.Int, 0, "IDPerformance"));

                    dbCommand.Parameters["@IDPerformance"].Value = this.IDPerformance;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public DataTable CarregaDiscosMaquina()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_INFRAESTRUTURA_PERFORMANCE_DISCOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPerformance", SqlDbType.Int, 0, "IDPerformance"));

                    dbCommand.Parameters["@IDPerformance"].Value = this.IDPerformance;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public DataTable CarregaProcessosMaquina(string nomeProcesso)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_INFRAESTRUTURA_PERFORMANCE_PROCESSOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPerformance", SqlDbType.Int, 0, "IDPerformance"));
                    dbCommand.Parameters.Add(new SqlParameter("@nomeProcesso", SqlDbType.VarChar, 8000, "nomeProcesso"));

                    dbCommand.Parameters["@IDPerformance"].Value = this.IDPerformance;
                    dbCommand.Parameters["@nomeProcesso"].Value = nomeProcesso;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        public DataTable CarregaProgramasInstaladosMaquina(string nomePrograma)
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_INFRAESTRUTURA_PERFORMANCE_PROGRAMAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPerformance", SqlDbType.Int, 0, "IDPerformance"));
                    dbCommand.Parameters.Add(new SqlParameter("@nomePrograma", SqlDbType.VarChar, 8000, "nomePrograma"));

                    dbCommand.Parameters["@IDPerformance"].Value = this.IDPerformance;
                    dbCommand.Parameters["@nomePrograma"].Value = nomePrograma;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }
        
        public DataTable CarregaInfoAlertasEmail()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_INFRAESTRUTURA_ALERTAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPerformance", SqlDbType.Int, 0, "IDPerformance"));

                    dbCommand.Parameters["@IDPerformance"].Value = this.IDPerformance;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return outputTable;
        }

        protected void VerificaDadosAlteracoesEmail(InfraestruturaEmailClass objEmail)
        {
            try
            {
                Convert.ToInt32(objEmail.IntervaloEmailminutos);
            }
            catch
            {
                throw new Exception("O intervalo de envio deve ser um valor inteiro.");
            }

            try
            {
                Convert.ToInt32(objEmail.LimiteUsoCPUPorcentagem);
            }
            catch
            {
                throw new Exception("O intervalo de cpu deve ser um valor inteiro.");
            }

            try
            {
                Convert.ToInt32(objEmail.LimiteUsoRAMPorcentagem);
            }
            catch
            {
                throw new Exception("O intervalo de ram deve ser um valor inteiro.");
            }

            try
            {
                Convert.ToInt32(objEmail.LimiteUsoDiscoPorcentagem);
            }
            catch
            {
                throw new Exception("O intervalo de disco deve ser um valor inteiro.");
            }

            if (!(objEmail.Alertar == "Sim" || objEmail.Alertar == "Não"))
                throw new Exception("O alerta deve ser \"Sim\" ou \"Não\".");
        }

        public string SalvarAlteracoesEmail(InfraestruturaEmailClass objEmail)
        {
            try
            {
                VerificaDadosAlteracoesEmail(objEmail);

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_INFRAESTRUTURA_ALERTAS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDPerformance", SqlDbType.Int, 0, "IDPerformance"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmailRemetente", SqlDbType.VarChar, 8000, "EmailRemetente"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmailRemetenteSenha", SqlDbType.VarChar, 8000, "EmailRemetenteSenha"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmailHost", SqlDbType.VarChar, 8000, "EmailHost"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmailPort", SqlDbType.VarChar, 8000, "EmailPort"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmailDestinatario", SqlDbType.VarChar, 8000, "EmailDestinatario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IntervaloEmailminutos", SqlDbType.VarChar, 8000, "IntervaloEmailminutos"));
                    dbCommand.Parameters.Add(new SqlParameter("@LimiteUsoCPUPorcentagem", SqlDbType.VarChar, 8000, "LimiteUsoCPUPorcentagem"));
                    dbCommand.Parameters.Add(new SqlParameter("@LimiteUsoRAMPorcentagem", SqlDbType.VarChar, 8000, "LimiteUsoRAMPorcentagem"));
                    dbCommand.Parameters.Add(new SqlParameter("@LimiteUsoDiscoPorcentagem", SqlDbType.VarChar, 8000, "LimiteUsoDiscoPorcentagem"));
                    dbCommand.Parameters.Add(new SqlParameter("@Alertar", SqlDbType.VarChar, 8000, "Alertar"));
                    dbCommand.Parameters.Add(new SqlParameter("@UltimoAlerta", SqlDbType.VarChar, 8000, "UltimoAlerta"));

                    dbCommand.Parameters["@IDPerformance"].Value = IDPerformance;
                    dbCommand.Parameters["@EmailRemetente"].Value = objEmail.EmailRemetente;
                    dbCommand.Parameters["@EmailRemetenteSenha"].Value = objEmail.EmailRemetenteSenha;
                    dbCommand.Parameters["@EmailHost"].Value = objEmail.EmailHost;
                    dbCommand.Parameters["@EmailPort"].Value = objEmail.EmailPort;
                    dbCommand.Parameters["@EmailDestinatario"].Value = objEmail.EmailDestinatario;
                    dbCommand.Parameters["@IntervaloEmailminutos"].Value = objEmail.IntervaloEmailminutos;
                    dbCommand.Parameters["@LimiteUsoCPUPorcentagem"].Value = objEmail.LimiteUsoCPUPorcentagem;
                    dbCommand.Parameters["@LimiteUsoRAMPorcentagem"].Value = objEmail.LimiteUsoRAMPorcentagem;
                    dbCommand.Parameters["@LimiteUsoDiscoPorcentagem"].Value = objEmail.LimiteUsoDiscoPorcentagem;
                    dbCommand.Parameters["@Alertar"].Value = objEmail.Alertar;
                    dbCommand.Parameters["@UltimoAlerta"].Value = "";

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }

        #endregion
    }
}