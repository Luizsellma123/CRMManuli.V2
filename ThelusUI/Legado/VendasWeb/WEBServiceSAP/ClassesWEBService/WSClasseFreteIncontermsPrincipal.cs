using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseFreteIncontermsPrincipal : clsConexao
    {
        public List<WSClasseFreteInconterms> ListaFretes { get; set; }

        //Importa dados de países do SAP
        public string AtualizaFretes()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseFreteInconterms Frete in ListaFretes)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_FRETE_INCOTERMS", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.NVarChar, 50, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Descricao", SqlDbType.VarChar, 100, "Descricao"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        //dbCommand.Parameters["@CodigoClienteSAP"].Value = ClienteAnexo.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@CodigoSAP"].Value = Frete.CodigoSAP ?? "";
                        dbCommand.Parameters["@Descricao"].Value = Frete.Descricao ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação da tabela de fretes.";
            }

            return erro;
        }
    }
}