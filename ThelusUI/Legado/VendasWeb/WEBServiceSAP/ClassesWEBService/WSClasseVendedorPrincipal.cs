using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    public class WSClasseVendedorPrincipal : clsConexao
    {
        public List<WSClasseVendedor> ListaVendedores { get; set; }

        //Importa vendedores do SAP
        public string AtualizaVendedor()
        {
            string erro = "";

            //Percorre LIST Chamando procedure para atualização/inserção/deleção de dados
            try
            {
                foreach (WSClasseVendedor Vendedor in ListaVendedores)
                {
                    using (SqlConnection dbConnection = new SqlConnection(strConec))
                    {
                        //Abre Conexão com o banco de dados
                        dbConnection.Open();

                        SqlCommand dbCommand = new SqlCommand("CRM_SP_IMPORTA_VENDEDOR", dbConnection);

                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.Add(new SqlParameter("@CodigoVendedorSAP", SqlDbType.Int, 0, "CodigoVendedorSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@NomeVendedor", SqlDbType.NVarChar, 155, "NomeVendedor"));
                        dbCommand.Parameters.Add(new SqlParameter("@ClasseVendedor", SqlDbType.NVarChar, 50, "ClasseVendedor"));
                        dbCommand.Parameters.Add(new SqlParameter("@EmailVendedor", SqlDbType.NVarChar, 100, "EmailVendedor"));
                        dbCommand.Parameters.Add(new SqlParameter("@TipoVendedorSAP", SqlDbType.NVarChar, 50, "TipoVendedorSAP"));
                        dbCommand.Parameters.Add(new SqlParameter("@StatusVendedor", SqlDbType.NVarChar, 50, "StatusVendedor"));
                        dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));

                        dbCommand.Parameters["@CodigoVendedorSAP"].Value = Convert.ToInt64(Vendedor.CodigoVendedorSAP);
                        dbCommand.Parameters["@NomeVendedor"].Value = Vendedor.NomeVendedor ?? "";
                        dbCommand.Parameters["@ClasseVendedor"].Value = Vendedor.ClasseVendedor ?? "";
                        dbCommand.Parameters["@EmailVendedor"].Value = Vendedor.EmailVendedor ?? "";
                        dbCommand.Parameters["@TipoVendedorSAP"].Value = Vendedor.TipoVendedor ?? "";
                        dbCommand.Parameters["@StatusVendedor"].Value = Vendedor.StatusVendedor ?? "";

                        dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                        dbCommand.ExecuteNonQuery();

                        erro = (string)dbCommand.Parameters["@vErro"].Value;

                    }
                }

            }
            catch (Exception ex)
            {
                erro = "Erro na importação dos vendedores.";
            }

            return erro;
        }
    }
}