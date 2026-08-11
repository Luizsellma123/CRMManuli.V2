using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseEmpresaFilialPrincipal : clsConexao
    {
        public List<WSClasseEmpresasFiliais> ListaEmpresaFilial { get; set; }

        //Importa dados de países do SAP
        public string AtualizaEmpresasFiliais()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseEmpresasFiliais EmpresaFilial in ListaEmpresaFilial)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_EMPRESA_FILIAL", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoSAP", SqlDbType.Int, 0, "CodigoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeEmpresa", SqlDbType.VarChar, 100, "NomeEmpresa"));
                        dbCommand.Parameters.Add(new SqlParameter("@CNPJ", SqlDbType.VarChar, 32, "CNPJ"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        //dbCommand.Parameters["@CodigoClienteSAP"].Value = ClienteAnexo.CodigoClienteSAP ?? "";
                        dbCommand.Parameters["@CodigoSAP"].Value = EmpresaFilial.CodigoSAP;
                        dbCommand.Parameters["@NomeEmpresa"].Value = EmpresaFilial.NomeEmpresa ?? "";
                        dbCommand.Parameters["@CNPJ"].Value = EmpresaFilial.CNPJ ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação da tabela de empresas.";
            }

            return erro;
        }
    }
}