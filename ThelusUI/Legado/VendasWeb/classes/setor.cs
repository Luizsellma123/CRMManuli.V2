using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;

namespace VendasWeb
{
    public class setor : GerencialVendas.clsConexao
    {
        public int IDSetor { get; set; }
        public int IDUsuario { get; set; }
        public int IDGrupo { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }
        public string Filtro { get; set; }
        public string Operacao { get; set; }
        public bool Administrador { get; set; }

        public DataTable RetornaSetores()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_SETORES_GERAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }


            catch
            {

            }

            return outputTable;
        }

        public DataTable ListaSetores()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_SETORES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 8000, "Status"));

                    dbCommand.Parameters["@Filtro"].Value = this.Filtro;
                    dbCommand.Parameters["@Status"].Value = this.Status;

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

        public void CarregaDadosPrincipais()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_DADOS_SETOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.VarChar, 8000, "IDSetor"));

                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.IDSetor = Convert.ToInt32(row["IDSetor"]);
                                this.Nome = row["Descricao"].ToString();
                                this.Status = row["Status"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public string GravaDadosPrincipaisSetor()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_DADOS_SETOR_PRINCIPAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, ParameterDirection.InputOutput, false, 0, 0, "IDSetor", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 0, "Status"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 8000, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "@Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;
                    dbCommand.Parameters["@Status"].Value = this.Status;
                    dbCommand.Parameters["@Nome"].Value = this.Nome;
                    dbCommand.Parameters["@Operacao"].Value = this.Operacao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                    this.IDSetor = (int)dbCommand.Parameters["@IDSetor"].Value;
                    this.Operacao = "alteracao";

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualização do menu.";
                }
            }

            return erro;
        }

        // BUTTON USUARIOS 

        public DataTable ListaUsuariosSetor()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_USUARIOS_SETOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.VarChar, 8000, "IDSetor"));


                    dbCommand.Parameters["@Filtro"].Value = this.Filtro ?? "";
                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;

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

        public DataTable RetornaUsuariosSetor()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_USUARIOS_SETOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));

                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                }
            }
            catch (Exception e)
            {

            }
            return outputTable;
        }

        public string AdicionaUsuariosSetor()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_INCLUI_USUARIOS_SETOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@VErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "VErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDSetor"].Value = IDSetor;
                    dbCommand.Parameters["@IDUsuario"].Value = IDUsuario;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@VErro"].Value;

                }
                catch (Exception ex)
                {

                }

                return erro;
            }

        }

        public void ExcluiUsuariosSetor()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_EXCLUI_USUARIOS_SETOR", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));
                dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                dbCommand.Parameters["@IDSetor"].Value = IDSetor;
                dbCommand.Parameters["@IDUsuario"].Value = IDUsuario;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();
            }

        }

        // END BUTTON USUARIOS 

        //----------------

        // BUTTON GRUPOS 

        public DataTable ListaGruposSetor()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_GRUPOS_SETOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.VarChar, 8000, "IDSetor"));


                    dbCommand.Parameters["@Filtro"].Value = this.Filtro;
                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;

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

        public DataTable RetornaGruposSetor()
        {
            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_GRUPOS_SETOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));

                    dbCommand.Parameters["@IDSetor"].Value = this.IDSetor;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                }
            }
            catch
            {

            }
            return outputTable;
        }

        public string AdicionaGruposSetor()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_INCLUI_GRUPOS_SETOR", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));
                    dbCommand.Parameters.Add(new SqlParameter("@VErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "VErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDSetor"].Value = IDSetor;
                    dbCommand.Parameters["@IDGrupo"].Value = IDGrupo;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@VErro"].Value;

                }
                catch (Exception ex)
                {

                }

                return erro;
            }

        }

        public void ExcluiGruposSetor()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_EXCLUI_GRUPOS_SETOR", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@IDSetor", SqlDbType.Int, 0, "IDSetor"));
                dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));

                dbCommand.Parameters["@IDSetor"].Value = IDSetor;
                dbCommand.Parameters["@IDGrupo"].Value = IDGrupo;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();
            }

        }

        // END BUTTON GRUPOS
    }
}