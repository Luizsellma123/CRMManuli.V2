using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseClienteAnexoPrincipal : clsConexao
    {
        public List<WSClasseClienteAnexo> ListaClientesAnexos { get; set; }

        //Importa dados de países do SAP
        public string AtualizaClientesAnexos()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseClienteAnexo ClienteAnexo in ListaClientesAnexos)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_CLIENTE_ANEXOS", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        //dbCommand.Parameters.Add(new SqlParameter("@CodigoClienteSAP", SqlDbType.NVarChar, 15, "CodigoClienteSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@CaminhoDestino", SqlDbType.NText, 0, "CaminhoDestino"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeArquivo", SqlDbType.NVarChar, 254, "NomeArquivo"));
                        dbCommand.Parameters.Add(new SqlParameter("@ExtensaoArquivo", SqlDbType.NVarChar, 8, "ExtensaoArquivo"));
                        dbCommand.Parameters.Add(new SqlParameter("@DataAnexo", SqlDbType.Date, 0, "DataAnexo"));
                        dbCommand.Parameters.Add(new SqlParameter("@TextoLivre", SqlDbType.NVarChar, 100, "TextoLivre"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.Int, 0, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@IDAnexoSAP", SqlDbType.Int, 0, "IDAnexoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        //dbCommand.Parameters["@CodigoClienteSAP"].Value = ClienteAnexo.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@CaminhoDestino"].Value = ClienteAnexo.CaminhoDestino ?? "";
                        dbCommand.Parameters["@NomeArquivo"].Value = ClienteAnexo.NomeArquivo ?? "";
                        dbCommand.Parameters["@ExtensaoArquivo"].Value = ClienteAnexo.ExtensaoArquivo ?? "";
                        //dbCommand.Parameters["@DataAnexo"].Value = Convert.ToDateTime(ClienteAnexo.DataAnexo ?? "");
                        dbCommand.Parameters["@DataAnexo"].Value = ClienteAnexo.DataAnexo ?? "";
                        dbCommand.Parameters["@TextoLivre"].Value = ClienteAnexo.TextoLivre ?? "";
                        dbCommand.Parameters["@CodigoSAP"].Value = ClienteAnexo.CodigoSAP;
                        dbCommand.Parameters["@IDAnexoSAP"].Value = ClienteAnexo.IDAnexoSAP;
                        

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação dos anexos dos Clientes.";
            }

            return erro;
        }
    }
}