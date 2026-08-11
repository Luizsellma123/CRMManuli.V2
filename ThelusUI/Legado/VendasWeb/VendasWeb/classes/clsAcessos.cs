using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace VendasWeb.GerencialVendas
{
    public class clsAcessos : GerencialVendas.clsConexao
    {


        public string id_acessos { get; set; }
        public string descricao { get; set; }
        public string hostport { get; set; }
        public string usuario { get; set; }
        public string senha { get; set; }
        public string unidade { get; set; }


        public String Tela { get; set; }
        public String UsuCod { get; set; }
        public String GrpUsuCod { get; set; }
        public String GrpUsuAcesso { get; set; }
        public String GrpUsuInclui { get; set; }
        public String GrpUsuExclui { get; set; }
        public String GrpUsuAltera { get; set; }
        public String GrpUsuConsulta { get; set; }
        public String GrpUsuSuperv { get; set; }




        //Metodo Para Acessos Usuarios
        public DataTable Consulta_Acesso_Usuario()
        {

            DataTable AcessosDataTable = new DataTable();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {

                //Abre Conexao
                dbConnection.Open();

                SqlCommand dbCommand = new SqlCommand();

                dbCommand = new SqlCommand("user_sp_consulta_acesso_usuario", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;

                dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 100, "UsuCod"));
                /*dbCommand.Parameters.Add(new SqlParameter("@GrpUsuCod", SqlDbType.VarChar, 100, "GrpUsuCod"));
                dbCommand.Parameters.Add(new SqlParameter("@GrpUsuAcesso", SqlDbType.VarChar, 5, "GrpUsuAcesso"));
                dbCommand.Parameters.Add(new SqlParameter("@GrpUsuInclui", SqlDbType.VarChar, 5, "GrpUsuInclui"));
                dbCommand.Parameters.Add(new SqlParameter("@GrpUsuExclui", SqlDbType.VarChar, 5, "GrpUsuExclui"));
                dbCommand.Parameters.Add(new SqlParameter("@GrpUsuAltera", SqlDbType.VarChar, 5, "GrpUsuAltera"));
                dbCommand.Parameters.Add(new SqlParameter("@GrpUsuConsulta", SqlDbType.VarChar, 5, "GrpUsuConsulta"));
                dbCommand.Parameters.Add(new SqlParameter("@GrpUsuSuperv", SqlDbType.VarChar, 5, "GrpUsuSuperv"));*/


                dbCommand.Parameters["@UsuCod"].Value = UsuCod;
                /*dbCommand.Parameters["@GrpUsuCod"].Value = GrpUsuCod;//Branco para trazer todos
                dbCommand.Parameters["@GrpUsuAcesso"].Value = GrpUsuAcesso;//Branco para trazer todos
                dbCommand.Parameters["@GrpUsuInclui"].Value = GrpUsuInclui;//Branco para trazer todos
                dbCommand.Parameters["@GrpUsuExclui"].Value = GrpUsuExclui;//Branco para trazer todos
                dbCommand.Parameters["@GrpUsuAltera"].Value = GrpUsuAltera;//Branco para trazer todos
                dbCommand.Parameters["@GrpUsuConsulta"].Value = GrpUsuConsulta;//Branco para trazer todos
                dbCommand.Parameters["@GrpUsuSuperv"].Value = GrpUsuSuperv;//Branco para trazer todos*/

                SqlDataReader dataReader = dbCommand.ExecuteReader();
                AcessosDataTable.Load(dataReader);
                dataReader.Close();


                if (AcessosDataTable.Rows.Count > 0)
                {

                    foreach (DataRow row in AcessosDataTable.Rows)
                    {

                        GrpUsuCod = row["GrpUsuCod"].ToString().ToUpper();
                        /*GrpUsuAcesso = row["GrpUsuAcesso"].ToString().ToUpper();
                        GrpUsuInclui = row["GrpUsuInclui"].ToString().ToUpper();
                        GrpUsuExclui = row["GrpUsuExclui"].ToString().ToUpper();
                        GrpUsuAltera = row["GrpUsuAltera"].ToString().ToUpper();
                        GrpUsuConsulta = row["GrpUsuConsulta"].ToString().ToUpper();
                        GrpUsuSuperv = row["GrpUsuSuperv"].ToString().ToUpper();*/

                    }
                }
                else
                {
                    GrpUsuAcesso = "";
                    GrpUsuInclui = "";
                    GrpUsuExclui = "";
                    GrpUsuAltera = "";
                    GrpUsuConsulta = "";
                    GrpUsuSuperv = "";
                }

            }

            return AcessosDataTable;

        }

       




    }
}