using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseCodigoCNAEPrincipal : clsConexao
    {
        public List<WSClassesCodigoCNAE> ListaCNAE { get; set; }

        //Atualiza códigos CNAE
        public string AtualizaCodigosCNAE()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClassesCodigoCNAE OBJCNAE in ListaCNAE)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();


                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_CODIGOS_CNAE", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoCNAESap", SqlDbType.VarChar, 9, "CodigoCNAESap"));
                        dbCommand.Parameters.Add(new SqlParameter("@DescricaoCNAE", SqlDbType.NText, 0, "DescricaoCNAE"));
                        dbCommand.Parameters.Add(new SqlParameter("@AbsIdSAP", SqlDbType.Int, 0, "AbsIdSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoCNAESap"].Value = OBJCNAE.CodigoCNAESap.ToString();
                        dbCommand.Parameters["@DescricaoCNAE"].Value = OBJCNAE.DescricaoCNAE ?? "";
                        dbCommand.Parameters["@AbsIdSAP"].Value = OBJCNAE.AbsIdSAP;

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação dos Códigos CNAE.";
            }

            return erro;
        }
    }
}