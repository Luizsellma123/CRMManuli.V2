using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb.GerencialVendas
{
    public class LiberaPedidoClasse : clsConexao
    {
        public string NumeroPedido { get; set; }
        public string NumeroPedidoSelecionado { get; set; }
        public string Empresa { get; set; }
        public string DataCancelamento { get; set; }
        public string Alcada { get; set; }
        public string Status { get; set; }
        public string usuario { get; set; }
        public string motivo { get; set; }

        public DataTable Mostra_PedidoBloqueado()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    funcoesBD funcaoBD = new funcoesBD();
                    
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("user_sp_Lista_Pedidos_Bloqueados_Novo", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    //Atribui parametros
                    dbCommand.Parameters.Add(new SqlParameter("@empresa", SqlDbType.VarChar, 15, "empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@pedido", SqlDbType.VarChar, 15, "NumeroPedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@DataCancelamento", SqlDbType.DateTime, 19, "DataCancelamento"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 30, "Status"));
                    dbCommand.Parameters.Add(new SqlParameter("@Alcada", SqlDbType.VarChar, 50, "Alcada"));
                    dbCommand.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar, 50, "Usuario"));


                    dbCommand.Parameters["@empresa"].Value = this.Empresa;
                    dbCommand.Parameters["@pedido"].Value = this.NumeroPedido;
                    dbCommand.Parameters["@DataCancelamento"].Value =this.DataCancelamento;
                    dbCommand.Parameters["@Status"].Value = this.Status;
                    dbCommand.Parameters["@Alcada"].Value = this.Alcada;
                    dbCommand.Parameters["@Usuario"].Value = this.usuario;
                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch { 
           
            }

            return outputTable;
        }

        public DataTable Mostra_PedidoBloqueadoItens()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("user_sp_Lista_Pedidos_Bloqueados_Itens_novo", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    //Atribui parametros
                    dbCommand.Parameters.Add(new SqlParameter("@empresa", SqlDbType.VarChar, 15, "empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@pedido", SqlDbType.VarChar, 30, "pedido"));

                    dbCommand.Parameters["@empresa"].Value = this.Empresa;
                    dbCommand.Parameters["@pedido"].Value = this.NumeroPedidoSelecionado;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

            }

            return outputTable;
        }

        public string LiberarPedido() 
        {
            string erro = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("user_sp_Liberar_Pedidos_Bloqueados", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    //Atribui parametros
                    dbCommand.Parameters.Add(new SqlParameter("@empresa", SqlDbType.VarChar, 15, "empresa"));
                    dbCommand.Parameters.Add(new SqlParameter("@pedido", SqlDbType.VarChar, 30, "pedido"));
                    dbCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 30, "Status"));
                    dbCommand.Parameters.Add(new SqlParameter("@UsuCod", SqlDbType.VarChar, 30, "UsuCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Motivo", SqlDbType.VarChar, 1000, "Motivo"));
                    dbCommand.Parameters.Add(new SqlParameter("@vErro", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "vErro", DataRowVersion.Default, null));
                    
                    dbCommand.Parameters["@empresa"].Value = this.Empresa;
                    dbCommand.Parameters["@pedido"].Value = this.NumeroPedidoSelecionado;
                    dbCommand.Parameters["@Status"].Value = this.Status;
                    dbCommand.Parameters["@Motivo"].Value = this.motivo;
                    dbCommand.Parameters["@UsuCod"].Value = this.usuario;

                    dbCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;

                    dbCommand.ExecuteNonQuery();

                    erro = (string)dbCommand.Parameters["@vErro"].Value;
                }
            }
            catch
            {
                erro = "Erro ao liberar pedido !";
            }

            return erro;
        }

        public DataTable Mostra_AcessoAlcada()
        {
            DataTable outputTable = new DataTable();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand("user_sp_Lista_Usuarios_alcada", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    //Atribui parametros
                    dbCommand.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar, 50, "Usuario"));

                    dbCommand.Parameters["@Usuario"].Value = this.usuario;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }
                }
            }
            catch
            {

            }

            return outputTable;
        }

    }
}