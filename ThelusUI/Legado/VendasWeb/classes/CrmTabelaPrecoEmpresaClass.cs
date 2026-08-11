using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class CrmTabelaPrecoEmpresaClass : clsConexao
    {


        public string CodigoUsuario { get; set; }
        public int IDTabela { get; set; }

        public int IDEmpresa { get; set; }




        public string GravaTabelaEmpresa()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_TABELA_EMPRESA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDTabela", SqlDbType.Int, 0, "IDTabela"));
                    dbCommand.Parameters.Add(new SqlParameter("@idEmpresa", SqlDbType.Int, 0, "idEmpresa"));


                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));


                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@IDTabela"].Value = this.IDTabela;
                    dbCommand.Parameters["@idEmpresa"].Value = this.IDEmpresa;



                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                    

                }
                catch (Exception ex)
                {
                    erro = "Erro GravaTabelaEmpresa:" + ex.Message;
                }
            }

            return erro;
        }


        public DataTable RetornaTabelaEmpresa()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_TABELA_EMPRESA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDTabela", SqlDbType.Int, 0, "IDTabela"));
                    


                    dbCommand.Parameters["@IDTabela"].Value = this.IDTabela;
                    


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


        public string ExcluiTabelaEmpresa()
        {
            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_EXCLUI_TABELA_EMPRESA", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.NVarChar, 250, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@idTabela", SqlDbType.Int, 0, "idTabela"));
                    dbCommand.Parameters.Add(new SqlParameter("@idEmpresa", SqlDbType.Int, 0, "idEmpresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.CodigoUsuario;
                    dbCommand.Parameters["@idTabela"].Value = this.IDTabela;
                    dbCommand.Parameters["@idEmpresa"].Value = Convert.ToInt32(this.IDEmpresa);

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {
                    erro = "Erro na exclusão da Tabela da Empresa.";
                }
            }

            return erro;
        }

    }




}