using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseEstadoPrincipal : clsConexao
    {
        public List<WSClasseEstado> ListaEstados { get; set; }

        //Importa dados de países do SAP
        public string AtualizaEstados()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseEstado Estado in ListaEstados)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();


                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_ESTADO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoEstadoSAP", SqlDbType.VarChar, 3, "CodigoEstadoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoPaisSAP", SqlDbType.VarChar, 3, "CodigoPaisSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar, 100, "Nome"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoEstadoSAP"].Value = Estado.CodigoEstadoSAP.ToString();
                        dbCommand.Parameters["@CodigoPaisSAP"].Value = Estado.PaisSap.ToString();
                        dbCommand.Parameters["@Nome"].Value = Estado.Nome.ToString();

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação dos estados.";
            }

            return erro;
        }
    }
}