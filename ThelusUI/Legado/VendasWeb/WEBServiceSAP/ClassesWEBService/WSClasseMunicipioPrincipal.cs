using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseMunicipioPrincipal : clsConexao
    {
        public List<WSClasseMunicipio> ListaMunicipios { get; set; }

        //Importa dados de países do SAP
        public string AtualizaMunicipios()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseMunicipio Municipio in ListaMunicipios)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
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
            catch (Exception ex)
            {
                erro = "Erro na importação dos estados.";
            }

            return erro;
        }
    }
}