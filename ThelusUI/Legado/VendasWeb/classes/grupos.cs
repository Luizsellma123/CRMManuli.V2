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
    public class grupos : GerencialVendas.clsConexao
    {
        public string CodigoGrupo { get; set; }

        public string Nome { get; set; }
        public string Status { get; set; }
        public string Filtro { get; set; }
        public string Operacao { get; set; }

        public int IDUsuario { get; set; }
        public int IDGrupo { get; set; }
        public int IDMenu { get; set; }

        public bool Administrador { get; set; }
        public string VErro { get; set; }

        public DataTable ListaGrupos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_GRUPOS", dbConnection);

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

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_DADOS_GRUPO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.VarChar, 8000, "IDGrupo"));

                    dbCommand.Parameters["@IDGrupo"].Value = this.IDGrupo;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.IDGrupo = Convert.ToInt32(row["IDGrupo"]);
                                this.Nome = row["Nome"].ToString();
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

        public string GravaDadosPrincipaisGrupo()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_DADOS_GRUPO_PRINCIPAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, ParameterDirection.InputOutput, false, 0, 0, "IDGrupo", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 8000, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 8000, "Status"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "@Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDGrupo"].Value = this.IDGrupo;
                    dbCommand.Parameters["@Nome"].Value = this.Nome;
                    dbCommand.Parameters["@Status"].Value = this.Status;
                    dbCommand.Parameters["@Operacao"].Value = this.Operacao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                    this.IDGrupo = (int)dbCommand.Parameters["@IDGrupo"].Value;
                    this.Operacao = "alteracao";

                }
                catch (Exception ex)
                {
                    erro = "Erro na atualizacao do usuário.";
                }
            }

            return erro;
        }

        public DataTable RetornaGrupos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_GRUPOS_GERAL", dbConnection);

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

        // BUTTON MENU

        public DataTable ListaMenusGrupos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_MENUS_GRUPOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.VarChar, 8000, "IDGrupo"));

                    dbCommand.Parameters["@Filtro"].Value = this.Filtro;
                    dbCommand.Parameters["@IDGrupo"].Value = this.IDGrupo;

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

        public DataTable RetornaMenusGrupo()
        {
            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_MENUS_GRUPOS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));

                    dbCommand.Parameters["@IDGrupo"].Value = this.IDGrupo;

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

        public string AdicionaMenuGrupo()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_INCLUI_MENUS_GRUPO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMenu", SqlDbType.Int, 0, "IDMenu"));
                    dbCommand.Parameters.Add(new SqlParameter("@VErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "VErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDGrupo"].Value = IDGrupo;
                    dbCommand.Parameters["@IDMenu"].Value = IDMenu;

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

        public void ExcluiMenuGrupo()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_EXCLUI_MENUS_GRUPO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));
                dbCommand.Parameters.Add(new SqlParameter("@IDMenu", SqlDbType.Int, 0, "IDMenu"));

                dbCommand.Parameters["@IDGrupo"].Value = IDGrupo;
                dbCommand.Parameters["@IDMenu"].Value = IDMenu;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();
            }

        }

        // END BUTTON MENU

        //----------------

        // BUTTON USUARIOS 

        public DataTable ListaUsuariosGrupos()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_USUARIOS_GRUPO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.VarChar, 8000, "IDGrupo"));


                    dbCommand.Parameters["@Filtro"].Value = this.Filtro;
                    dbCommand.Parameters["@IDGrupo"].Value = this.IDGrupo;

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

        public DataTable RetornaUsuariosGrupo()
        {
            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_USUARIOS_GRUPO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));

                    dbCommand.Parameters["@IDGrupo"].Value = this.IDGrupo;

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

        public string AdicionaUsuariosGrupo()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_INCLUI_USUARIOS_GRUPO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDGrupo"].Value = IDGrupo;
                    dbCommand.Parameters["@IDUsuario"].Value = IDUsuario;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;

                }
                catch (Exception ex)
                {

                }

                return erro;
            }

        }

        public void ExcluiUsuariosGrupo()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_EXCLUI_USUARIOS_GRUPO", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));
                dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                dbCommand.Parameters["@IDGrupo"].Value = IDGrupo;
                dbCommand.Parameters["@IDUsuario"].Value = IDUsuario;

                SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                outputTable.Load(dataReader);

                dataReader.Close();
            }

        }

        // END BUTTON USUARIOS 

    }
}
