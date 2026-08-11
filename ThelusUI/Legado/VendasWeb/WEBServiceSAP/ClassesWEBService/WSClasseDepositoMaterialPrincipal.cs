using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseDepositoMaterialPrincipal : clsConexao
    {
        public List<WSClasseDepositoMaterial> ListaDepositos { get; set; }

        //Importa dados de países do SAP
        public string AtualizaDepositosMaterial()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseDepositoMaterial Deposito in ListaDepositos)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_DEPOSITOS_MATERIAL", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoEmpresaSAP", SqlDbType.Int, 0, "CodigoEmpresaSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoDepositoSAP", SqlDbType.NVarChar, 8, "CodigoDepositoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeDepositoSAP", SqlDbType.NVarChar, 100, "NomeDepositoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoEmpresaSAP"].Value = Deposito.CodigoEmpresaSAP;
                        dbCommand.Parameters["@CodigoDepositoSAP"].Value = Deposito.CodigoDepositoSAP ?? "";
                        dbCommand.Parameters["@NomeDepositoSAP"].Value = Deposito.NomeDepositoSAP ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação dos depósitos materiais.";
            }

            return erro;
        }
    }
}