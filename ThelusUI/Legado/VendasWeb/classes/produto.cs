using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Data.SqlClient;

namespace VendasWeb
{
    public class produto : GerencialVendas.clsConexao
    {
        public string UsuCod { get; set; }
        public string codigoProduto { get; set; }
        public string descricaoProduto { get; set; }
        public string CompdescricaoProduto { get; set; }
        public string descProduto { get; set; }
        public string revenda { get; set; }
        public string codigoTabela { get; set; }
        public float valorTabela { get; set; }
        public float valorItem { get; set; }
        public string unidade { get; set; }
        public double quantidade { get; set; }
        public int numSeq { get; set; }
        public int ItPedVendaNumSeq { get; set; }
        public float valorOriginal { get; set; }
        public string USERLINHAPRODUTOLISTA { get; set; }
        public string CodigoProdutoCliche { get; set; }
        public string CodigoProdutoArruela { get; set; }
        public string xPed { get; set; }
        public string nItem { get; set; }

        public DataTable verificaEstoque(string empCod, string codigoProduto)
        {
            funcoes mdlFuncoes = new funcoes();
            DataTable dadosTable = new DataTable();

            string strSQL = "select LocArmazCodEstr, EstqLocArmazQtd from ESTQ_LOC_ARMAZ ";
            strSQL += "where EmpCod='" + empCod + "' and ProdCodEstr='" + codigoProduto + "' and EstqLocArmazQtd<>0";

            dadosTable = mdlFuncoes.Executa_DataTable(strSQL, "verificaEstoque - produto.cs");

            return dadosTable;
        }

        public DataTable Consulta_Linha_Produto()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("user_consulta_linha_produto", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 150, "UsuCod"));
                    dbCommand.Parameters["@UsuCod"].Value = UsuCod;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch
            {


            }

            return outputTable;

        }

        public DataTable Consulta_Produto()
        {

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("user_consulta_produto", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@USERLINHAPRODUTOLISTA", SqlDbType.VarChar, 200, "USERLINHAPRODUTOLISTA"));
                    dbCommand.Parameters["@USERLINHAPRODUTOLISTA"].Value = USERLINHAPRODUTOLISTA;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();


                }
            }
            catch
            {


            }

            return outputTable;

        }
    }
}