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
    public class menu : GerencialVendas.clsConexao
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string IconeCSS { get; set; }
        public string Ordem { get; set; }
        public string Status { get; set; }
        public string Filtro { get; set; }
        public string Operacao { get; set; }
        public int IDMenu { get; set; }
        public int IDUsuario { get; set; }
        public int IDGrupo { get; set; }

        public DataTable RetornaMenus()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_MENUS_GERAL", dbConnection);

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

        public DataTable ListaMenus()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_MENUS", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 8000, "StatusMenu"));

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

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_DADOS_MENU", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDMenu", SqlDbType.VarChar, 8000, "IDMenu"));

                    dbCommand.Parameters["@IDMenu"].Value = this.IDMenu;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);

                        if (outputTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in outputTable.Rows)
                            {
                                this.IDMenu = Convert.ToInt32(row["IDMenu"]);
                                this.Nome = row["Nome"].ToString();
                                this.IconeCSS = row["IconeCSS"].ToString();
                                this.Endereco = row["Endereco"].ToString();
                                this.Ordem = row["Ordem"].ToString();
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

        public string GravaDadosPrincipaisMenu()
        {
            UtilClass ObjUtilClass = new UtilClass();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                try
                {
                    SqlCommand dbCommand = new SqlCommand("CRM_SP_GRAVA_DADOS_MENU_PRINCIPAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDMenu", SqlDbType.Int, 0, ParameterDirection.InputOutput, false, 0, 0, "IDMenu", DataRowVersion.Default, null));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 0, "Status"));
                    dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 8000, "Nome"));
                    dbCommand.Parameters.Add(new SqlParameter("@Endereco", SqlDbType.VarChar, 8000, "Endereco"));
                    dbCommand.Parameters.Add(new SqlParameter("@IconeCSS", SqlDbType.VarChar, 8000, "IconeCSS"));
                    dbCommand.Parameters.Add(new SqlParameter("@Ordem", SqlDbType.VarChar, 8000, "Ordem"));
                    dbCommand.Parameters.Add(new SqlParameter("@Operacao", SqlDbType.VarChar, 8000, "@Operacao"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDMenu"].Value = this.IDMenu;
                    dbCommand.Parameters["@Status"].Value = this.Status;
                    dbCommand.Parameters["@Nome"].Value = this.Nome;
                    dbCommand.Parameters["@Endereco"].Value = this.Endereco;
                    dbCommand.Parameters["@IconeCSS"].Value = this.IconeCSS;
                    dbCommand.Parameters["@Ordem"].Value = this.Ordem;
                    dbCommand.Parameters["@Operacao"].Value = this.Operacao;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                    this.IDMenu = (int)dbCommand.Parameters["@IDMenu"].Value;
                    this.Operacao = "alteracao";

                }
                catch (Exception ex)
                {
                    erro = ex.ToString();
                }
            }

            return erro;
        }

        // BUTTON USUARIOS 

        public DataTable ListaUsuariosMenus()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_USUARIOS_MENU", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMenu", SqlDbType.VarChar, 8000, "IDMenu"));


                    dbCommand.Parameters["@Filtro"].Value = this.Filtro;
                    dbCommand.Parameters["@IDMenu"].Value = this.IDMenu;

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

        public DataTable RetornaUsuariosMenu()
        {
            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_USUARIOS_MENU", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDMenu", SqlDbType.Int, 0, "IDMenu"));

                    dbCommand.Parameters["@IDMenu"].Value = this.IDMenu;

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

        public string AdicionaUsuariosMenu()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_INCLUI_USUARIOS_MENU", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDMenu", SqlDbType.Int, 0, "IDMenu"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@VErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "VErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDMenu"].Value = IDMenu;
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

        public void ExcluiUsuariosMenu()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_EXCLUI_USUARIOS_MENU", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@IDMenu", SqlDbType.Int, 0, "IDMenu"));
                dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                dbCommand.Parameters["@IDMenu"].Value = IDMenu;
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

        public DataTable ListaGruposMenus()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_LISTA_GRUPOS_MENU", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar, 8000, "Filtro"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDMenu", SqlDbType.VarChar, 8000, "IDMenu"));


                    dbCommand.Parameters["@Filtro"].Value = this.Filtro;
                    dbCommand.Parameters["@IDMenu"].Value = this.IDMenu;

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

        public DataTable RetornaGruposMenu()
        {
            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_RETORNA_GRUPOS_MENU", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.Parameters.Add(new SqlParameter("@IDMenu", SqlDbType.Int, 0, "IDMenu"));

                    dbCommand.Parameters["@IDMenu"].Value = this.IDMenu;

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

        public string AdicionaGruposMenu()
        {
            DataTable outputTable = new DataTable();

            string erro = "";

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                try
                {

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_INCLUI_GRUPOS_MENU", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDMenu", SqlDbType.Int, 0, "IDMenu"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));
                    dbCommand.Parameters.Add(new SqlParameter("@VErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "VErro", DataRowVersion.Default, null));

                    dbCommand.Parameters["@IDMenu"].Value = IDMenu;
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

        public void ExcluiGruposMenu()
        {
            DataTable outputTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("CRM_SP_EXCLUI_GRUPOS_MENU", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@IDMenu", SqlDbType.Int, 0, "IDMenu"));
                dbCommand.Parameters.Add(new SqlParameter("@IDGrupo", SqlDbType.Int, 0, "IDGrupo"));

                dbCommand.Parameters["@IDMenu"].Value = IDMenu;
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