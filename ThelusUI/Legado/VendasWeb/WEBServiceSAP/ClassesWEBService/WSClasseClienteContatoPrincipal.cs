using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseClienteContatoPrincipal : clsConexao
    {
        public List<WSClasseClienteContato> ListaClientesContatos { get; set; }

        //Importa dados de países do SAP
        public string AtualizaClientesContatos()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseClienteContato ClienteContato in ListaClientesContatos)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_CLIENTE_CONTATO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.NVarChar, 15, "CodigoClienteSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@TipoContato", SqlDbType.NVarChar, 50, "TipoContato"));
                        dbCommand.Parameters.Add(new SqlParameter("@Nome", SqlDbType.NVarChar, 50, "Nome"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.Int, 0, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.NVarChar, 20, "Telefone"));
                        dbCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100, "Email"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoClienteSAP"].Value = ClienteContato.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@TipoContato"].Value = ClienteContato.TipoContato ?? "";
                        dbCommand.Parameters["@Nome"].Value = ClienteContato.Nome ?? "";
                        dbCommand.Parameters["@CodigoSAP"].Value = ClienteContato.CodigoSAP;
                        dbCommand.Parameters["@Telefone"].Value = ClienteContato.Telefone ?? "";
                        dbCommand.Parameters["@Email"].Value = ClienteContato.Email ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação das identificações fiscais dos Clientes.";
            }

            return erro;
        }

    }
}