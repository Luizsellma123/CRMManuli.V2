using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class CrmEmpresaFilialClass : clsConexao
    {
        public int IDEmpresa { get; set; }
        public int CodigoSAP { get; set; }
        public string NomeEmpresa { get; set; }
        public string CNPJ { get; set; }



        public DataTable RetornaEmpresaFilial()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("CRM_SP_RETORNA_EMPRESA_FILIAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;
                   

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return outputTable;

        }

    }
}