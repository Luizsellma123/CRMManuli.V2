using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseVendedorClassePrincipal : clsConexao
    {
        public List<WSClasseVendedorClasse> ListaClassesVendedores { get; set; }

        //Importa classes de vendedores do SAP
        public string AtualizaClassesVendedores()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseVendedorClasse classe in ListaClassesVendedores)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_CLASSE_VENDEDOR", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoClasseSAP", SqlDbType.NVarChar, 50, "CodigoClasseSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeClasse", SqlDbType.NVarChar, 100, "NomeClasse"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoClasseSAP"].Value = classe.CodigoClasse ?? "";
                        dbCommand.Parameters["@NomeClasse"].Value = classe.NomeClasse ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação das classes dos vendedores.";
            }

            return erro;
        }

    }
}