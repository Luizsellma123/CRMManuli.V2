using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseTipoVendedorPrincipal : clsConexao
    {
        public List<WSClasseTipoVendedor> ListaTipoVendedor { get; set; }

        public string AtualizaTipoVendedor()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseTipoVendedor TipoVendedor in ListaTipoVendedor)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_TIPO_VENDEDOR", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoTipoSAP", SqlDbType.VarChar, 50, "@CodigoTipoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@DescricaoTipo", SqlDbType.VarChar, 100, "@DescricaoTipo"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoTipoSAP"].Value = TipoVendedor.CodigoTipoSAP ?? "";
                        dbCommand.Parameters["@DescricaoTipo"].Value = TipoVendedor.DescricaoTipo ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação dos tipos de vendedor.";
            }

            return erro;
        }
    }
}