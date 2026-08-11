using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class DebugClass : ConexaoClass
    {
        private bool GeraDebug { get; set; }
        private int IDUsuario { get; set; }
        private string CodigoUsuario { get; set; }
        private string Operacao { get; set; }
        private DateTime DataOperacao { get; set; }
        private string Descricao { get; set; }
        private string IDSessao { get; set; }

        public DebugClass()
        {
            this.CarregagDebug();
            this.IDSessao = WebApiApplication.SessaoID;
        }

        public void CarregagDebug()
        {

            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_API_PARAMETROS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Parametro", SqlDbType.VarChar, 8000, "Parametro"));

                    dbCommand.Parameters["@Parametro"].Value = "DEBUGSERVICELAYER";

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())

                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.GeraDebug = Convert.ToBoolean(row["ValorNumerico"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.GeraDebug = false;
            }
        }

        public void GerarDadosDebug()
        {
            string erro = "";

            if (this.GeraDebug == true)
            {
                DataOperacao = DateTime.Now;
                erro=GravaDadosDebug();
            }
        }

        public string GravaDadosDebug()
        {
        
            string erro = "";
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_DEBUG_API", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 0, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataOperacao", SqlDbType.DateTime, 0, "DataOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 0, "Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@DescricaoOperacao", SqlDbType.VarChar, 0, "DescricaoOperacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSessao", SqlDbType.VarChar, 0, "IDSessao"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario ?? "";
                    dbCommand.Parameters["@DataOperacao"].Value = this.DataOperacao;
                    dbCommand.Parameters["@Operacao"].Value = this.Operacao ?? "";
                    dbCommand.Parameters["@DescricaoOperacao"].Value = this.Descricao ?? "";
                    //dbCommand.Parameters["@IDSessao"].Value = this.IDSessao ?? "";
                    dbCommand.Parameters["@IDSessao"].Value = WebApiApplication.SessaoID ?? "";


                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }


                return erro;
           
        }
    }

        public string SerializarObjeto(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }


        #region ***METODOS DE ACESO****/
        public bool GetGeraDebug()
        {
            return this.GeraDebug;
        }

        // Métodos GET e SET para IDUsuario
        public int GetIDUsuario()
        {
            return this.IDUsuario;
        }

        public void SetIDUsuario(int value)
        {
            this.IDUsuario = value;
        }

        // Métodos GET e SET para CodigoUsuario
        public string GetCodigoUsuario()
        {
            return this.CodigoUsuario;
        }

        public void SetCodigoUsuario(string value)
        {
            this.CodigoUsuario = value;
        }

        // Métodos GET e SET para Operacao
        public string GetOperacao()
        {
            return this.Operacao;
        }

        public void SetOperacao(string value)
        {
            this.Operacao = value;
        }

        // Métodos GET e SET para DataOperacao
        public DateTime GetDataOperacao()
        {
            return this.DataOperacao;
        }

        public void SetDataOperacao(DateTime value)
        {
            this.DataOperacao = value;
        }

        // Métodos GET e SET para Descricao
        public string GetDescricao()
        {
            return this.Descricao;
        }

        public void SetDescricao(string value)
        {
            this.Descricao = value;
        }


        #endregion

    }
}
