using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb
{
    public class relatorios
    {
        public static SqlConnection dbConnection = new SqlConnection();

        //Metodo para abrir conexão
        public void abreConexao()
        {
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection = new SqlConnection("server=192.168.0.240; user id=sa; password='eura@!1'; database=Manuli; application name=VendasWeb");
                //dbConnection = new SqlConnection("server=192.168.0.3; user id=sa; password='ssuark.dba'; database=Teste_Manuli1; application name=VendasWeb");
                dbConnection.Open();
            }
        }

        //Método para fechar conexão
        public void fechaConexao()
        {
            if (dbConnection.State == ConnectionState.Open)
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
        }

        public DataTable  relatorioTabelaDinamica(string empresa, string natureza, string produto, string linha, string dataInicial,
            string dataFinal, string entidade, string subFamilia)
        {
            DataTable outputTable = new DataTable();

            //Abre Conexao
            if (dbConnection.State == ConnectionState.Closed)
            {
                this.abreConexao();
            }

            SqlCommand dbCommand = new SqlCommand();

            dbCommand = new SqlCommand("USER_SP_SelecionaTabelaDinamica", dbConnection);

            dbCommand.CommandType = CommandType.StoredProcedure;

            dbCommand.Parameters.Add(new SqlParameter("@Empresa", SqlDbType.VarChar, 30, "Empresa"));
            dbCommand.Parameters.Add(new SqlParameter("@Natureza", SqlDbType.VarChar, 30, "Natureza"));
            dbCommand.Parameters.Add(new SqlParameter("@ProdNome", SqlDbType.VarChar, 30, "ProdNome"));
            dbCommand.Parameters.Add(new SqlParameter("@LinhaProduto", SqlDbType.VarChar, 30, "LinhaProduto"));
            dbCommand.Parameters.Add(new SqlParameter("@DataInicial", SqlDbType.DateTime, 10, "DataInicial"));
            dbCommand.Parameters.Add(new SqlParameter("@DataFinal", SqlDbType.DateTime, 10, "DataFinal"));
            dbCommand.Parameters.Add(new SqlParameter("@EntCod", SqlDbType.VarChar, 7, "EntCod"));
            dbCommand.Parameters.Add(new SqlParameter("@SubFamilia", SqlDbType.VarChar, 30, "SubFamilia"));

            dbCommand.Parameters["@Empresa"].Value = empresa;
            dbCommand.Parameters["@Natureza"].Value = natureza;
            dbCommand.Parameters["@ProdNome"].Value = produto;
            dbCommand.Parameters["@LinhaProduto"].Value = linha;
            dbCommand.Parameters["@DataInicial"].Value = dataInicial;
            dbCommand.Parameters["@DataFinal"].Value = dataFinal;
            dbCommand.Parameters["@EntCod"].Value = entidade;
            dbCommand.Parameters["@SubFamilia"].Value = subFamilia;

            //Aumentando o timeout do command
            dbCommand.CommandTimeout = 320;

            SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

            SqlDataReader dataReader = dbCommand.ExecuteReader();
            outputTable.Load(dataReader);

            dataReader.Close();

            //Fecha Conexao
            if (dbConnection.State == ConnectionState.Open)
            {
                this.fechaConexao();
            }

            return outputTable;
        }
    }
}