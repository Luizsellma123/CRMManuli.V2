using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;

namespace VendasWeb.GerencialVendas
{
    public class clsConexao
    {

        public static string strConec { get; set; }


        //private string ConexaoPrincipal = "Data Source=192.168.0.15; Initial Catalog=SBO_ManuliFitasa_TST;User ID=sa;Password='bdsapb12019@!1'";
        //private string ConexaoPrincipal = "server=192.168.0.3; user id=sa; password='ssuark.dba'; database=manuli; application name=CRM_MANULI;";
        
        /****IMPORTANTE NÃO ESQUECER DE MUDAR O WEB SERVICE PARA TESTES****/
        private string ConexaoPrincipal = System.Configuration.ConfigurationManager.AppSettings["StringConexaoBD"];
        private string ConexaoContingencia = System.Configuration.ConfigurationManager.AppSettings["StringConexaoBD"];


        public clsConexao()
        {

            try
            {
                #region Testando Conexao Principal
                using (SqlConnection dbConnection = new SqlConnection(ConexaoPrincipal))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    //Fecha Conexao
                    dbConnection.Close();

                    strConec = ConexaoPrincipal;

                }
                #endregion

            }
            catch (Exception)
            {

                try
                {
                    #region Testando Conexao Contingencia
                    using (SqlConnection dbConnection = new SqlConnection(ConexaoContingencia))
                    {
                        //Abre Conexao
                        dbConnection.Open();

                        //Fecha Conexao
                        dbConnection.Close();

                        strConec = ConexaoContingencia;

                    }
                    #endregion
                }
                catch (Exception)
                {
                    strConec = "";//Se nao acessar na De Contingencia nao retornar nada   
                }

            }
        }

        public string getString()
        {
            return strConec;
        }

    }
}
