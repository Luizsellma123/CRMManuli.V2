using System;
using System.Data;
using System.Text;
using CRMAPI.Classes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CRMAPI.Models
{
    public class AtualizaMunicipiosModel
    {
        public string CodigoMunicipioSAP { get; set; }

        List<MunicipioClass> Municipios = new List<MunicipioClass>();

        ComunicacaoServiceLayerSAPClass objComunicacaoServiceLayerSAP = new ComunicacaoServiceLayerSAPClass();

        private void CarregaMunicipios()
        {
            try
            {
                StringBuilder stringSQL = new StringBuilder();

                stringSQL.AppendLine("select AbsId AbsIdSAP, Code CodigoMunicipioSAP, ");
                stringSQL.AppendLine("Country CodigoPaisSAP, [State] CodigoEstadoSAP, ");
                stringSQL.AppendLine("Name NomeMunicipio, isnull(IbgeCode,'') CodigoIBGE ");
                stringSQL.AppendLine("from OCNT ");
                stringSQL.AppendLine("where (Code = '" + CodigoMunicipioSAP + "' or '' = '" + CodigoMunicipioSAP + "')");

                DataTable ConsultaSAP = objComunicacaoServiceLayerSAP.RetornaDadosConsultaSAP(stringSQL.ToString());

                UtilClass objUtilClass = new UtilClass();

                Municipios = objUtilClass.ConvertDataTable<MunicipioClass>(ConsultaSAP);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                throw new Exception("Erro ao carregar os Municipios do SAP.");
            }
        }

        //Importa dados de países do SAP
        public string AtualizaMunicipios()
        {
            string erro = "";

            try
            {
                CarregaMunicipios();

                ConexaoClass objConexaoClass = new ConexaoClass();

                foreach (MunicipioClass Municipio in Municipios)
                {
                    using (SqlConnection dbConnection = new SqlConnection(objConexaoClass.getString()))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_MUNICIPIO", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;

                        dbCommand.Parameters.Add(new SqlParameter("@CodigoEstadoSAP", SqlDbType.VarChar, 3, "CodigoEstadoSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoPaisSAP", SqlDbType.VarChar, 3, "CodigoPaisSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeMunicipio", SqlDbType.VarChar, 100, "NomeMunicipio"));
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoIBGE", SqlDbType.VarChar, 10, "CodigoIBGE"));
                        dbCommand.Parameters.Add(new SqlParameter("@AbsIdSAP", SqlDbType.Int, 0, "AbsIdSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@MunicipioSAP", SqlDbType.VarChar, 7, "MunicipioSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoEstadoSAP"].Value = Municipio.CodigoEstadoSAP.ToString();
                        dbCommand.Parameters["@CodigoPaisSAP"].Value = Municipio.CodigoPaisSAP.ToString();
                        dbCommand.Parameters["@NomeMunicipio"].Value = Municipio.NomeMunicipio ?? "";
                        dbCommand.Parameters["@CodigoIBGE"].Value = Municipio.CodigoIBGE ?? "";
                        dbCommand.Parameters["@AbsIdSAP"].Value = Municipio.AbsIdSAP;
                        dbCommand.Parameters["@MunicipioSAP"].Value = Municipio.CodigoMunicipioSAP ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch //(Exception ex)
            {
                erro = "Erro na importação dos Municipios.";
            }           

            return erro;
        }
    }
}