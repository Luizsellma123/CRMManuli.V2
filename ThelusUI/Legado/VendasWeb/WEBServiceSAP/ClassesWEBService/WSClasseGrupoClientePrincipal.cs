using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseGrupoClientePrincipal : clsConexao
    {
        public List<WSClasseGrupoCliente> ListaGrupoClientes { get; set; }

        //Importa dados de países do SAP
        public string AtualizaGruposClientes()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseGrupoCliente GrupoClientes in ListaGrupoClientes)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_GRUPO_CLIENTE", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoGrupoSAP", SqlDbType.Int, 0, "CodigoGrupoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 20, "Nome"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoGrupoSAP"].Value = GrupoClientes.CodigoGrupoSAP;
                        dbCommand.Parameters["@Nome"].Value = GrupoClientes.Nome ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação dos grupos de clientes.";
            }

            return erro;
        }
    }
}