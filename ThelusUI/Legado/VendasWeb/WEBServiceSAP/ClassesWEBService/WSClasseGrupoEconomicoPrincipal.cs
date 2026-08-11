using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseGrupoEconomicoPrincipal : clsConexao
    {
        public List<WSClassGrupoEconomico> ListaGruposEconomicos { get; set; }

        //Importa dados de países do SAP
        public string AtualizaGruposEconomicos()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClassGrupoEconomico GrupoEconomico in ListaGruposEconomicos)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_GRUPO_ECONOMICO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.VarChar, 50, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeGrupo", SqlDbType.VarChar, 100, "NomeGrupo"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoSAP"].Value = GrupoEconomico.CodigoSAP;
                        dbCommand.Parameters["@NomeGrupo"].Value = GrupoEconomico.NomeGrupo ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação das Naturezas Jurídicas.";
            }

            return erro;
        }
    }
}