using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VendasWeb.GerencialVendas
{
    public class UsuarioPortalClass : clsConexao
    {
        public int IDUsuario { get; set; }
        public string codigo { get; set; }
        public string Telefone { get; set; }
        public string email { get; set; }
        public string nome { get; set; }
        public string senha { get; set; }
        public string status { get; set; }
        public string EntCod { get; set; }
        public string EntNat { get; set; }
        public string VendCod { get; set; }
        public string UsuarioApolo { get; set; }
        public decimal LimiteDisponivel { get; set; }

        /***campos para filtro**/
        public string EmpCod { get; set; }
        public string PedVendaNum { get; set; }
        public string ParcDocNum { get; set; }
        public string NFNum { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public bool faturados { get; set; }
        public bool abertas { get; set; }

        public string Valida_Usuario()
        {

            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_VALIDA_USUARIO_PORTAL", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 5000, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@SenhaUsuario", SqlDbType.VarChar, 5000, "SenhaUsuario"));

                    dbCommand.Parameters["@CodigoUsuario"].Value = this.codigo;
                    dbCommand.Parameters["@SenhaUsuario"].Value = this.senha;


                    dbCommand.CommandTimeout = 999999;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            Retorno = row["Msg"].ToString();
                            this.IDUsuario = Convert.ToInt32(row["IDUsuario"].ToString());
                            this.nome = row["EntNome"].ToString();
                            this.email = row["email"].ToString();
                            this.Telefone = row["telefone"].ToString();
                            this.EntCod = row["EntCod"].ToString();
                            this.EntNat = row["EntNat"].ToString();
                            this.VendCod = row["VendCod"].ToString();
                            this.UsuarioApolo = row["UsuarioVendedor"].ToString();
                            this.LimiteDisponivel = Convert.ToDecimal(row["LimiteDisponivel"].ToString());
                        }
                    }
                    else
                    {
                        Retorno = "Usuario ou Senha Invalido.";
                    }


                }
            }
            catch (Exception ex)
            {

                Retorno = "Erro na Funcao Valida Usuario. Contactar o Suporte!";
            }




            return Retorno;

        }

        public string Atualiza_Usuario()
        {

            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_ATUALIZA_USUARIO_PORTAL", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;


                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@CodigoUsuario", SqlDbType.VarChar, 5000, "CodigoUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 8000, "email"));
                    dbCommand.Parameters.Add(new SqlParameter("@telefone", SqlDbType.VarChar, 8000, "telefone"));
                    dbCommand.Parameters.Add(new SqlParameter("@SenhaUsuario", SqlDbType.VarChar, 5000, "SenhaUsuario"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@CodigoUsuario"].Value = this.codigo;
                    dbCommand.Parameters["@email"].Value = this.email;
                    dbCommand.Parameters["@telefone"].Value = this.Telefone;
                    dbCommand.Parameters["@SenhaUsuario"].Value = this.senha;

                    dbCommand.CommandTimeout = 999999;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            Retorno = row["Msg"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                Retorno = "Erro na Funcao atualizar usuario. Contactar o Suporte!";
            }

            return Retorno;

        }

        public DataTable Empresas_Usuario()
        {

            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_PORTAL_EMPRESAS_USUARIO", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    dbCommand.CommandTimeout = 999999;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            Retorno = row["Msg"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                Retorno = "Erro na Funcao atualizar usuario. Contactar o Suporte!";
            }

            return outputTable;

        }

        public DataTable Entidades_Usuario()
        {

            string Retorno = "";

            DataTable outputTable = new DataTable();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("USER_SP_PORTAL_ENTIDADES_USUARIO", dbConnection);
                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;

                    dbCommand.CommandTimeout = 999999;

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }


                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            Retorno = row["Msg"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                Retorno = "Erro na Funcao atualizar usuario. Contactar o Suporte!";
            }

            return outputTable;

        }

        public DataTable Pedidos_Entidade()
        {
            DataTable outputTable = new DataTable();
            SqlCommand dbCommand = new SqlCommand();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    dbCommand = new SqlCommand("USER_SP_PORTAL_PEDIDOS_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Entcod", SqlDbType.VarChar, 10, "Entcod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vPedVendaNum", SqlDbType.VarChar, 10, "vPedVendaNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@dDataInicial", SqlDbType.DateTime, 0, "dDataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@dDataFinal", SqlDbType.DateTime, 0, "dDataFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@vStatus", SqlDbType.VarChar, 10, "vStatus"));


                    dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;
                    dbCommand.Parameters["@Entcod"].Value = this.EntCod;
                    dbCommand.Parameters["@vPedVendaNum"].Value = this.PedVendaNum;
                    dbCommand.Parameters["@dDataInicial"].Value = this.DataInicial;
                    dbCommand.Parameters["@dDataFinal"].Value = this.DataFinal;

                    if (this.faturados == true)
                    {
                        dbCommand.Parameters["@vStatus"].Value = "Faturado";
                    }else
                    {
                        dbCommand.Parameters["@vStatus"].Value = "";
                    }

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

        public DataTable Produtos_Entidade()
        {
            DataTable outputTable = new DataTable();
            SqlCommand dbCommand = new SqlCommand();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    dbCommand = new SqlCommand("USER_SP_PORTAL_PRODUTOS_CLIENTES", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@IDUsuario", SqlDbType.Int, 0, "IDUsuario"));
                    dbCommand.Parameters.Add(new SqlParameter("@Entcod", SqlDbType.VarChar, 10, "Entcod"));
                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));

                    dbCommand.Parameters["@IDUsuario"].Value = this.IDUsuario;
                    dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;
                    dbCommand.Parameters["@Entcod"].Value = this.EntCod;

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

        public DataTable Parcelas_Entidade()
        {
            DataTable outputTable = new DataTable();
            SqlCommand dbCommand = new SqlCommand();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    dbCommand = new SqlCommand("USER_SP_PORTAL_PARCELAS_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Entcod", SqlDbType.VarChar, 10, "Entcod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vParcDocNum", SqlDbType.VarChar, 10, "vParcDocNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@dDataInicial", SqlDbType.DateTime, 0, "dDataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@dDataFinal", SqlDbType.DateTime, 0, "dDataFinal"));
                    dbCommand.Parameters.Add(new SqlParameter("@vStatus", SqlDbType.VarChar, 10, "vStatus"));

                    dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;
                    dbCommand.Parameters["@Entcod"].Value = this.EntCod;
                    dbCommand.Parameters["@vParcDocNum"].Value = this.ParcDocNum;
                    dbCommand.Parameters["@dDataInicial"].Value = this.DataInicial;
                    dbCommand.Parameters["@dDataFinal"].Value = this.DataFinal;

                    if (this.abertas == true)
                    {
                        dbCommand.Parameters["@vStatus"].Value = "Abertas";
                    }
                    else
                    {
                        dbCommand.Parameters["@vStatus"].Value = "";
                    }

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

        public DataTable Notas_Entidade()
        {
            DataTable outputTable = new DataTable();
            SqlCommand dbCommand = new SqlCommand();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(strConec))
                {

                    dbCommand = new SqlCommand("USER_SP_PORTAL_NOTAS_CLIENTE", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@EmpCod", SqlDbType.VarChar, 10, "EmpCod"));
                    dbCommand.Parameters.Add(new SqlParameter("@Entcod", SqlDbType.VarChar, 10, "Entcod"));
                    dbCommand.Parameters.Add(new SqlParameter("@vNFNum", SqlDbType.VarChar, 10, "vNFNum"));
                    dbCommand.Parameters.Add(new SqlParameter("@dDataInicial", SqlDbType.DateTime, 0, "dDataInicial"));
                    dbCommand.Parameters.Add(new SqlParameter("@dDataFinal", SqlDbType.DateTime, 0, "dDataFinal"));

                    dbCommand.Parameters["@EmpCod"].Value = this.EmpCod;
                    dbCommand.Parameters["@Entcod"].Value = this.EntCod;
                    dbCommand.Parameters["@vNFNum"].Value = this.NFNum;
                    dbCommand.Parameters["@dDataInicial"].Value = this.DataInicial;
                    dbCommand.Parameters["@dDataFinal"].Value = this.DataFinal;

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
    }
}
