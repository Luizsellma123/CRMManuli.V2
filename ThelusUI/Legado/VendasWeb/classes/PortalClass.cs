using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VendasWeb.GerencialVendas
{
    public class PortalClass : clsConexao
    {
        public string Entcod { get; set; }
        public string NomeContato { get; set; }
        public string EmailSetor { get; set; }
        public string Empresa { get; set; }
        public string NotaFiscal { get; set; }
        public string XMLNotaFiscal { get; set; }

        public DataTable FaturamentoEntidade()
        {
            DataTable outputTable = new DataTable();
            SqlCommand dbCommand = new SqlCommand();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    dbCommand = new SqlCommand("USER_SP_PORTAL_FATURAMENTO_MENSAL", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Entcod", SqlDbType.VarChar, 7, "Entcod"));


                    dbCommand.Parameters["@Entcod"].Value = this.Entcod;

                    dbCommand.CommandTimeout = 999999;
                    dbCommand.Connection.Open();

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                    dataReader.Close();

                }
            }
            catch (Exception ex)
            {

            }
            return outputTable;

        }

        public DataTable FaturamentoMesAMes()
        {
            DataTable outputTable = new DataTable();
            SqlCommand dbCommand = new SqlCommand();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    dbCommand = new SqlCommand("USER_SP_PORTAL_FATURAMENTO_MES_A_MES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Entcod", SqlDbType.VarChar, 7, "Entcod"));


                    dbCommand.Parameters["@Entcod"].Value = this.Entcod;

                    dbCommand.CommandTimeout = 999999;
                    dbCommand.Connection.Open();

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                    dataReader.Close();

                }
            }
            catch (Exception ex)
            {

            }
            return outputTable;

        }

        public DataTable LimiteCredito()
        {
            DataTable outputTable = new DataTable();
            SqlCommand dbCommand = new SqlCommand();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    dbCommand = new SqlCommand("USER_SP_PORTAL_LIMITE_CREDITO", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Entcod", SqlDbType.VarChar, 7, "Entcod"));


                    dbCommand.Parameters["@Entcod"].Value = this.Entcod;

                    dbCommand.CommandTimeout = 999999;
                    dbCommand.Connection.Open();

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                    dataReader.Close();

                }
            }
            catch (Exception ex)
            {

            }
            return outputTable;

        }

        public DataTable PedidosPendentes()
        {
            DataTable outputTable = new DataTable();
            SqlCommand dbCommand = new SqlCommand();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    dbCommand = new SqlCommand("USER_SP_PORTAL_PEDIDOS_PENDENTES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@Entcod", SqlDbType.VarChar, 7, "Entcod"));


                    dbCommand.Parameters["@Entcod"].Value = this.Entcod;

                    dbCommand.CommandTimeout = 999999;
                    dbCommand.Connection.Open();

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                    dataReader.Close();

                }
            }
            catch (Exception ex)
            {

            }
            return outputTable;

        }

        //USER_SP_PORTAL_CONTATOS
        public DataTable ContatosPortal()
        {
            DataTable outputTable = new DataTable();
            SqlCommand dbCommand = new SqlCommand();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    dbCommand = new SqlCommand("USER_SP_PORTAL_CONTATOS", dbConnection);

                    dbCommand.Parameters.Add(new SqlParameter("@vNomeContato", SqlDbType.VarChar, 8000, "vNomeContato"));


                    dbCommand.Parameters["@vNomeContato"].Value = this.NomeContato;

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.CommandTimeout = 999999;
                    dbCommand.Connection.Open();

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                    dataReader.Close();

                }
            }
            catch (Exception ex)
            {

            }
            return outputTable;
        }

        //Contato Setor Portal
        public void ContatoSetorPortal()
        {
            DataTable outputTable = new DataTable();
            SqlCommand dbCommand = new SqlCommand();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    dbCommand = new SqlCommand("USER_SP_PORTAL_CONTATO_SETOR", dbConnection);

                    dbCommand.Parameters.Add(new SqlParameter("@vNomeContato", SqlDbType.VarChar, 8000, "vNomeContato"));


                    dbCommand.Parameters["@vNomeContato"].Value = this.NomeContato;

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.CommandTimeout = 999999;
                    dbCommand.Connection.Open();

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                    dataReader.Close();

                }

                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {
                        this.EmailSetor = row["Email"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void BuscaXMLNota()
        {
            DataTable outputTable = new DataTable();
            SqlCommand dbCommand = new SqlCommand();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    dbCommand = new SqlCommand("USER_SP_PORTAL_BUSCA_XML", dbConnection);

                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vNFNum", SqlDbType.VarChar, 10, "vNFNum"));


                    dbCommand.Parameters["@EmpCod"].Value = this.Empresa;
                    dbCommand.Parameters["@vNFNum"].Value = this.NotaFiscal;

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.CommandTimeout = 999999;
                    dbCommand.Connection.Open();

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(dbCommand);

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);

                    dataReader.Close();

                }

                if (outputTable.Rows.Count > 0)
                {
                    foreach (DataRow row in outputTable.Rows)
                    {
                        this.XMLNotaFiscal = row["NFeTransArqXml"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}